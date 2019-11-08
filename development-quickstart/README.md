# Development Docker Compose Usage Instructions

This docker-compose setup is intended for use when developing the A3S platform. It includes:
* An instance of [Postgresql](https://www.postgresql.org) database.
* An instance of [Open LDAP](https://www.openldap.org). A light weight LDAP implementation.
* An instanec of [PHP LDAP admin](https://www.openldap.org). A web-based LDAP client for accessing the Open LDAP instance.
* An instance of an simple SMTP server that can be used to test email-based two factor authentication.

## Pre-requisites

* [Docker v17.04.0+](http://docker.com).
* [Docker compose v1.13.0+](https://docs.docker.com/compose/).
* A command-line terminal for running the required Docker compose commands.

## Running

Using a CLI tool, navigate to the following directory relative to the root of the code repository: "docker-compose/a3s-development". Run ```docker-compose up -d``` to start all the services in a detached manner.

## Postgresql

* Configured username: "postgres"
* Configured password: "postgres"

## Open LDAP

* Configured domain: "bigbaobab.org"
* Configured admin password: "admin"

**Note:** There are some comments inside the docker-compose.yml file pertainig to configuring Open LDAP with data persistence.

## PHP LDAP Admin

After the ```docker-compose up``` command is run, PHP admin LDAP will be accessible at: "http://localhost:8085". Note: You will see a warning on the login screen pertaining to an HTTP only connection. if you require HTTPS, open the docker-compose file, and comment out the "PHPLDAPADMIN_HTTPS=false" environment variable within the docker-compose.yml file. This will result in PHP LDAP Admin being available at: "https://localhost:6443". This configuration uses self signed SSL certificates which will need to be accepted within the browser on warning.

**NOTE!** Please use the Chrome browser to access PHP admin LDAP, as there have been known issues using other browsers.

To log into the Open Ldap Admin instance from PHP Admin LDAP, click the login button on the UI. Note: The PHP admin LDAP instance has already been configured to connect to the Open LDAP instance, so a host it not required.

* Login DN: "cn=admin,dc=bigbaobab,dc=org"
* Password: "admin"

## Sentry

[Sentry](https://sentry.io/welcome/) is currently going to be used for runtime exceptions monitoring. An optional add on docker-compose environment has been developed for adding [sentry](https://sentry.io/welcome/) into the development environment. It is in a separate sentry-docker-compose.yml file, as bringing up the environment is a little more complex than a `docker-compose up`. A script has been developed to ensure this process happens correctly. To bring up the sentry environment:

* Run `docker-compose run --rm web config generate-secret-key | tail -n 1 | tr -d '\r\n' | awk '{print "SENTRY_SECRET_KEY="$1}' > .env`. This generates the required sentry secret key and places it into a `.env` file.
* Run `./sentry-environment-up.sh`.
  * This will migrate the sentry database (which happens prior to the actual docker-compose up).
  * Prompt the user to create a sentry user. This is interactive and will require human input.
  * Bring up the environment once the prior steps are completed.

To bring down the environment:

* Run `./sentry-environment-down.sh` to bring down the entire environment.

**Note!** The sentry environment requires a persistent docker volume to function. This volume is not destroyed when running the `./sentry-environment-down.sh` script. To truly destroy the environment state, this docker volume will need to be destroyed.

* Run `docker volume rm development-quickstart_sentry-postgres` to destroy the volume and truly reset the state.
