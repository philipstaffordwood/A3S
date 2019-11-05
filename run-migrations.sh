#!/bin/sh
#
# *************************************************
# Copyright (c) 2019, Grindrod Bank Limited
# License MIT: https://opensource.org/licenses/MIT
# **************************************************
#


# Run flyway migrations
cd db
flyway -configFiles=flyway-a3s.conf migrate
flyway -configFiles=flyway-ids4.conf migrate
cd ..