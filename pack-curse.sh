#!/usr/bin/env bash

# see http://redsymbol.net/articles/unofficial-bash-strict-mode/
set -euo pipefail
IFS=$'\n\t'
source ./CONFIG.inc

clean() {
	rm -fR $FILE
	if [ ! -d Archive ] ; then
		mkdir Archive
	fi
}

pwd=$(pwd)
FILE=${pwd}/Archive/$PACKAGE-$VERSION${PROJECT_STATE}-CurseForge.zip
echo $FILE
clean
cd GameData

zip -r $FILE ./$PACKAGE/* -x ".*"
zip -d $FILE "__MACOSX/*" "**/.DS_Store"
cd $pwd
