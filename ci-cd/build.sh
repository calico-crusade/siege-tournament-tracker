#!/bin/bash

cd ../siege-tour-tracker

npm install

npm run prod

cd ./../SiegeTournamentTracker.Web

dotnet publish --runtime=linux-x64 --configuration=Release SiegeTournamentTracker.Web.csproj
