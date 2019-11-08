#!/bin/bash
#
# *************************************************
# Copyright (c) 2019, Grindrod Bank Limited
# License MIT: https://opensource.org/licenses/MIT
# **************************************************
#


echo "Migrating the Sentry Database"
docker-compose -f sentry-docker-compose.yml run --rm web upgrade --noinput

echo "Creating Sentry User"
docker-compose -f sentry-docker-compose.yml run --rm web createuser

echo "Bringing up the Environment"
docker-compose -f sentry-docker-compose.yml up

echo "Sentry is now listening on `localhost:9000`"