#!/bin/bash

# go to the build linux application root
cd ../SiegeTournamentTracker.Web/bin/Release/netcoreapp3.1/linux-x64

# zip the publish folder
zip publish -r publish.zip publish

# move the publish.zip file to the root of our project
mv publish.zip ./../../../../../

# move back to the root of our project (not necessary anymore now that the scripts are broken up)
cd ./../../../../../
