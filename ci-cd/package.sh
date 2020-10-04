#!/bin/bash

cd ../SiegeTournamentTracker.Web/bin/Release/netcoreapp3.1/linux-x64

zip publish -r publish.zip publish

mv publish.zip ./../../../../../

cd ./../../../../../
