# copy-app.sh

#!/bin/bash

#
# Script to copy an existing app and deploy a specific branch to it.
# 1. Creates the new app
# 2. Provisions a new Postgres database and copies the rows over from the existing app
# 3. Deploys the specified branch to the new app
#
# USAGE:
#   ./copy-app.sh appName 
#

# Defines the parameters passed in to the script. Requires the name of the
# parent app, the name of the new app, and the name of the branch to push to the new app to build.
set -e
appName=$1
userName=$2
password=$3
# Create the new app
heroku create "$appName"
heroku config:set LIGHTHOUSE_USERNAME=$userName LIGHTHOUSE_PASSWORD=$password

# Provision new Postgres database
heroku addons:create heroku-postgresql:hobby-dev -a "$appName"

# Push the source code of the branch up to the new app to build
git subtree push --prefix lighthouse/app https://heroku:"$HEROKU_API_KEY"@git.heroku.com/"$appName".git master


