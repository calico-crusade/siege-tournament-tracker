#!/bin/bash

DEPLOY_URL=$1

./build.sh
./package.sh
./deploy.sh $DEPLOY_URL
