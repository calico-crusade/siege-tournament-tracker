#!/bin/bash

# Get the ssh / sftp url from our command line args
DEPLOY_URL=$1

# cd to the route directory of the project
cd ..

# sftp copy the publish.zip file to the server
sftp -v $DEPLOY_URL <<EOF

cd /nas/websites/stt
put publish.zip
quit

EOF

# ssh into the server to install our web-app
ssh $DEPLOY_URL <<EOF

sudo systemctl stop website@stt
cd /nas/websites/stt
unzip -o publish.zip
sudo systemctl start website@stt

EOF

# delete our published binaries (not necessary, but keeps the clutter down)
rm publish.zip
