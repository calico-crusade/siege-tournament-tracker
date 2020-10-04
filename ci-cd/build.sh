#!/bin/bash

# go to our angular app's root directory
cd ../siege-tour-tracker

# install any NPM depedencies
npm install

# run the compile procedure for a production deployment
npm run prod

# go to the web app's root directory
cd ./../SiegeTournamentTracker.Web

# compile the .net core's app using the linux runtime
dotnet publish --runtime=linux-x64 --configuration=Release SiegeTournamentTracker.Web.csproj
