#!/usr/bin/env bash

source ./CONFIG.inc

clean() {
	rm $FILE
	if [ ! -d Archive ] ; then
		rm -f Archive
		mkdir Archive
	fi
}

FILE=$PACKAGE-$VERSION$PROJECT_STATE.zip
echo $FILE
clean
zip $FILE "INSTALL.md"
zip -r $FILE ./GameData/* -x ".*"
zip -r $FILE ./PluginData/* -x ".*"
zip -d $FILE __MACOSX "**/.DS_Store"
mv $FILE ./Archive
