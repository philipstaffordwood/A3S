# Integrating A3S Into A Docker-Compose by Convention

The [quickstart guide](./../quickstart/README.md) uses [docker-compose](https://docs.docker.com/compose) to compose several inter-related docker containers into a functioning environment. However, the components of that environment are dependent on one another.

* The `a3s` is dependent on the `a3s-postgresql` [Postgresql](https://www.postgresql.org) docker container, as it uses [Postgresql](https://www.postgresql.org) as it's relational data store. The `a3s` container must be configured to connect to this database.
* The `a3s-identity-server` component also depends on the `a3s-postgresql` [Postgresql](https://www.postgresql.org) docker container, as it needs to read many of the same relational stores that A3S operates on to issue [access tokens](./glossary.md#access-token) to [users](./glossary.md#access-token).
* The `a3s` container is dependent on the `a3s-identity-server` to be it's [access token](./glossary.md#access-token) issuer.

How do the containers running in this enviroment know where to connect? Internally, the docker images that are built for A3S and A3S-Identity-Server have a default internal configuration which assumes the setup within the [quickstart docker-compose file](./../quickstart/docker-compose.yml). All the settings can be overridden by mounting in custom configuration files into the various containers, but this couples the integrating party to the changing configuration of A3S and IDS4, which is undesirable. Therefore, when pulling A3S and IDS4 containers into your own docker-compose environments, understanding and using the following conventions will enable you to pull the A3S, IDS4, and mutually required [Postgresql](https://www.postgresql.org) container into your docker-compose environment without the need to configure (and therefore mount in configurations into the containers) for each of the containers.

# Default Configurations

* `a3s` and the `a3s-identity-server` containers connect to the [Postgresql](https://www.postgresql.org) container using the following settings:
  * `hostname`: `a3s-postgresql`. This necessitates the name of the service within the [quickstart docker-compose file](./../quickstart/docker-compose.yml) to be this value.
  * `username`: `postgres`. This is the reason the `POSTGRES_USER=postgres` environment variable (within the [Postgresql](https://www.postgresql.org) container) is configured as such.
  * `password`: `postgres`. This is the reason the `POSTGRES_PASSWORD=postgres` environment variable (within the [Postgresql](https://www.postgresql.org) container) is configured as such.
  * `database`: `identity_server`. This is the reason the `POSTGRES_DB=identity_server` environment variable (within the [Postgresql](https://www.postgresql.org) container) is configured as such.

* `a3s` also protects it's own [resources](./glossary.md#resource) with itself and requires [access tokens](./glossary.md#access-token) issued by `a3s-identity-server` for authenticated access. Therefore, A3S must be configured with `a3s-identity-server` to be it's [access tokens](./glossary.md#access-token) issuer. The default configuration for this `IssuerURL` is `http://a3s-identity-server`, which necessitates the name of the IDS4 container to be `a3s-identity-server` with the docker-compose.yml.

# Docker Compose Convention

When pulling in A3S containers into your own [docker-compose](https://docs.docker.com/compose) environments, ensure that the following naming conventions are adhered to within your `docker-compose.yml` file and you will not need to configure any of the A3S related containers to connect to one another. **Note** The A3S [quickstart docker-compose file](./../quickstart/docker-compose.yml) adheres to these conventions and can be used as a reference:

* Name the A3S service `a3s`.
* Name the Identity Server 4 service: `a3s-identity-server.`
* Name the Postgresql service: `a3s-postgresql`.
* Ensure the Postgresql username is: `postgres`
* Ensure the Postgresql password is: `postgres`
* Ensure the Postgresql instance has a database called: `identity_server`.
* If using the official Postgres image, this can all be done as follows:

```yaml
a3s-postgresql:
    networks:
      - a3s-quickstart
    image: "postgres:10.7-alpine"
    restart: always
    ports:
      # Use a non standard port mapping to avoid potential collisions with other Postgres instances running on the host.
      - '5478:5432'
    environment:
      - POSTGRES_PASSWORD=postgres
      - POSTGRES_USER=postgres
      - POSTGRES_DB=identity_server
```
*Figure 1: An excerpt from the AS3 quickstart docker-compose.yml showing how to configure credentials and a default database when using the official Postgres docker image.*

# Automatically Running Security Contracts on Docker-compose Up

[Security Contracts](./glossary.md#security-contract) are a central concept within [A3S](https://github.com/GrindrodBank/A3S). Therefore, the [A3S quickstart guide](./../quickstart/README.md) intentionally includes instructions for manually applying a [security contract](./glossary.md#security-contract) in order to expose the user to these concepts. This is also the reason why [quickstart docker-compose file](./../quickstart/docker-compose.yml) does not contain any setup for automatically applying a [security contract](./glossary.md#security-contract). 

When integrating [A3S](https://github.com/GrindrodBank/A3S) into your [docker-compose](https://docs.docker.com/compose) environment, it is not necessary to manually apply the [security contract](./glossary.md#security-contract) in the same fashion as the [A3S quickstart guide](./../quickstart/README.md). The application of a [security contract](./glossary.md#security-contract) is simply:
1. Obtaining an [access token](./glossary.md#access-token) for a [user](./glossary.md#user) that has the `a3s.securityContracts.update` [permission](./glossary.md#permission).
2. PUT the [security contract](./glossary.md#security-contract) using the API.

This can be achieved with any user agent capable of making the required HTTP API requests. The [A3S](https://github.com/GrindrodBank/A3S) makes use of [Postman](https://www.getpostman.com) for API testing and discoverability. Therefore, the following example demonstrates how to use a [Newman](https://learning.getpostman.com/docs/postman/collection-runs/command-line-integration-with-newman/) docker container to programatically run a [Postman](https://www.getpostman.com) collection when starting up a [docker-compose](https://docs.docker.com/compose) environment. The [Postman](https://www.getpostman.com) collection simply performs the same steps as `1.` and `2.` above.

```yaml
newman:
   networks:
     - a3s-quickstart   
   image: "postman/newman_alpine33" 
   command: run /etc/newman/register-a3s-contract.postman_collection.json --global-var client-id='a3s-default' --global-var client-secret='secret'  --global-var a3s-host=http://a3s --global-var auth-server-base-url=http://a3s-identity-server --delay-request 15000 
   volumes:
       - ../../postman:/etc/newman
   depends_on:
     - a3s-identity-server
```
*Figure 2: An excerpt from a docker-compose.yml file that uses a Newman container to automatically run a Postman collection to apply a desired security contract on `docker-compose up`. *Note!* The required `register-a3s-contract.postman_collection.json` is mounted in as a volume and would be expected to be inside the `../../postman` folder on the host machine in this example.*



