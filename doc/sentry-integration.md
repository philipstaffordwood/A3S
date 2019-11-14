# Sentry Integration

[Sentry](https://sentry.io/welcome/) provides open-source and hosted error monitoring that helps all software 
teams discover, triage, and prioritize errors in real-time. However, in order to use this functionality, [Sentry](https://sentry.io/welcome/) specific agent code needs to be inserted into the application. It is for this reason that it has been decided to not have [Sentry](https://sentry.io/welcome/) integrated by default. This document describes the process of integrating A3S with [Sentry](https://sentry.io/welcome/), should that be desired.

# Basic Archictecture

[Sentry](https://sentry.io/welcome/) monitoring consists of two components:

* The [Sentry](https://sentry.io/welcome/) server, which aggregates all the reported issues and exposes a UI for enabling teams to discover and address these issues.
* An application specific [Sentry](https://sentry.io/welcome/) agent. This programming language specific agent is integrated into the application at a code level to catch all exceptions and publish them to the [Sentry](https://sentry.io/welcome/) server.

# Setting Up A Sentry Server

## Docker-compose environment

Sentry provides cloud and on premise deployment options for the server side component of the server component. The following excerpt is a docker-compose configuration file that can be used to deploy a Sentry Server. Note, some configuration needs to be done prior to this environment being spun up. Please refer to the subsequent sections for these intructions.

```
version: '3.4'
x-defaults: &defaults
  networks:
    - a3s-development
  restart: unless-stopped
  image: sentry:9.1
  depends_on:
    - redis
    - postgres
    - memcached
    # - smtp
  env_file: .env
  environment:
    SENTRY_MEMCACHED_HOST: memcached
    SENTRY_REDIS_HOST: redis
    SENTRY_POSTGRES_HOST: postgres
    # SENTRY_EMAIL_HOST: smtp
  # volumes:
  #   - sentry-data:/var/lib/sentry/files

services:
  memcached:
    networks:
      - a3s-development
    restart: unless-stopped
    image: memcached:1.5-alpine

  redis:
    networks:
      - a3s-development
    restart: unless-stopped
    image: redis:3.2-alpine

  postgres:
    networks:
      - a3s-development
    restart: unless-stopped
    image: postgres:9.5
    volumes:
      - sentry-postgres:/var/lib/postgresql/data

  web:
    <<: *defaults
    ports:
      - '9000:9000'

  cron:
    <<: *defaults
    command: run cron

  worker:
    <<: *defaults
    command: run worker

volumes:
   sentry-postgres:
     # external: true
      
networks:
  a3s-development:
```
*Figure 1: Docker compose configuration file for bringing up a sentry server.*

## Configure the Docker-compose environemnt

Sentry requires some manual steps to be run using the docker-compose environment defined in figure 1. These are listed below and must be run **prior to `running docker-compose up`**.

* Create a secret key. Run:
  *`docker-compose run --rm web config generate-secret-key | tail -n 1 | tr -d '\r\n' | awk '{print "SENTRY_SECRET_KEY="$1}' > .env`. 
The docker-compose assumes this .env file is present.
* Migrate the database. Run:
  *`docker-compose -f sentry-docker-compose.yml run --rm web upgrade --noinput`.
* Create a user. Run:
  *`docker-compose -f sentry-docker-compose.yml run --rm web createuser`. **Note** 
This is an interactive process that will require human input.
