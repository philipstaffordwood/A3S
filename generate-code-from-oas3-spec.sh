#!/bin/bash
#
# *************************************************
# Copyright (c) 2019, Grindrod Bank Limited
# License MIT: https://opensource.org/licenses/MIT
# **************************************************
#


bold=$(tput bold)
normal=$(tput sgr0)

echoBold(){
   echo "${bold}$1${normal}"
}

echoBold "Generating source code from the A3S OAS3 specification."

echo "Creating temporary folder in: 'src/za.co.grindrodbank.a3s/temp'. This will be removed after this script completes"

mkdir src/za.co.grindrodbank.a3s/temp
docker run --rm -v ${PWD}:/local openapitools/openapi-generator-cli:latest generate \
 -i ./local/doc/a3s-openapi.yaml -g aspnetcore -o /local/src/za.co.grindrodbank.a3s/temp \
 -c ./local/doc/openapi-config.json -t ./local/.openapi-generator/templates/aspnetcore/2.1

echoBold "Copying desired generated artifcats"
echo "Copying generated controllers to src/za.co.grindrodbank.a3s/AbstractApiControllers"

cp -rf src/za.co.grindrodbank.a3s/temp/src/za.co.grindrodbank.a3s/Controllers/*.cs src/za.co.grindrodbank.a3s/AbstractApiControllers

echo "Copying generated resource models to 'src/za.co.grindrodbank.a3s/A3SApiResources'"
cp -rf src/za.co.grindrodbank.a3s/temp/src/za.co.grindrodbank.a3s/Models/*.cs src/za.co.grindrodbank.a3s/A3SApiResources

echo "Copying generated resource attributes to 'src/za.co.grindrodbank.a3s/Attributes'"
cp -rf src/za.co.grindrodbank.a3s/temp/src/za.co.grindrodbank.a3s/Attributes/*.cs src/za.co.grindrodbank.a3s/Attributes

echo "Copying generated converters to 'src/za.co.grindrodbank.a3s/Converters'"
cp -rf src/za.co.grindrodbank.a3s/temp/src/za.co.grindrodbank.a3s/Converters/*.cs src/za.co.grindrodbank.a3s/Converters

echo "Copying generated wwwroot artifacts (including openapi-original.json) to 'src/za.co.grindrodbank.a3s/wwwroot'"
cp -rf src/za.co.grindrodbank.a3s/temp/src/za.co.grindrodbank.a3s/wwwroot/index.html src/za.co.grindrodbank.a3s/wwwroot/index.html
cp -rf src/za.co.grindrodbank.a3s/temp/src/za.co.grindrodbank.a3s/wwwroot/openapi-original.json src/za.co.grindrodbank.a3s/wwwroot/openapi-original.json

echoBold "Manually Cleaning up generation errors"

# The generator imposes the structure of the application and therefore the namespaces generated fro certain components. Tweaking here to fit the current structure of A3S.
echo "Tweaking auto-generated namespace of 'Models' to 'A3SApiResources' to match the desired a3s application structure."
find ${PWD}/src/za.co.grindrodbank.a3s/A3SApiResources \( -type d -name .cs -prune \) -o -type f -print0 | xargs -0 sed -i '' 's/za.co.grindrodbank.a3s.Models/za.co.grindrodbank.a3s.A3SApiResources/g'

find ${PWD}/src/za.co.grindrodbank.a3s/AbstractApiControllers \( -type d -name .cs -prune \) -o -type f -print0 | xargs -0 sed -i '' 's/za.co.grindrodbank.a3s.Models/za.co.grindrodbank.a3s.A3SApiResources/g'

# The generator imposes the structure of the application and therefore the namespaces generated fro certain components. Tweaking here to fit the current structure of A3S.
echo "Tweaking auto-generated namespace of 'Controllers' to 'AbstractApiControllers' to match the desired a3s application structure."
find ${PWD}/src/za.co.grindrodbank.a3s/AbstractApiControllers \( -type d -name .cs -prune \) -o -type f -print0 | xargs -0 sed -i '' 's/za.co.grindrodbank.a3s.Controllers/za.co.grindrodbank.a3s.AbstractApiControllers/g'

echoBold "Cleaning up temp folder"
rm -rf src/za.co.grindrodbank.a3s/temp

echoBold "Done"
