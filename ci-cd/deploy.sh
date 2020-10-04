#!/bin/bash

DEPLOY_URL=$1

cd ..

sftp -v $DEPLOY_URL <<EOF

cd /nas/websites/stt
put publish.zip
quit

EOF

ssh $DEPLOY_URL <<EOF

sudo systemctl stop website@stt
cd /nas/websites/stt
unzip -o publish.zip
sudo systemctl start website@stt

EOF

rm publish.zip
