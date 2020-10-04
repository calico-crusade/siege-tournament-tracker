#!/bin/bash

# get to SSH IP for our deployment, like: <user>@<ip>
DEPLOY_URL=$1

# run our build script
./build.sh
# run our package script
./package.sh
# run our deploy script
./deploy.sh $DEPLOY_URL
