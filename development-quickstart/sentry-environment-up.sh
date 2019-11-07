#!/bin/bash

echo "Migrating the Sentry Database"
docker-compose -f sentry-docker-compose.yml run --rm web upgrade --noinput

echo "Creating Sentry User"
docker-compose -f sentry-docker-compose.yml run --rm web createuser

echo "Bringing up the Environment"
docker-compose -f sentry-docker-compose.yml up