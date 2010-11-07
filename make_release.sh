#!/bin/bash

GRGENDIR=GrGenNET-V$1-`date +"%F"`
GRGENDIRSRC=$GRGENDIR-src

#make release distribution 
#contains binaries, examples, documentation
#excluded internal stuff, source code

hg pull
hg update
hg archive $GRGENDIRSRC

mkdir $GRGENDIR
mkdir $GRGENDIR/doc
cp $GRGENDIRSRC/doc/grgen.pdf $GRGENDIR/doc/GrGenNET-Manual.pdf
cp $GRGENDIRSRC/doc/ChangeLog.txt $GRGENDIR/doc
cp $GRGENDIRSRC/doc/README.txt $GRGENDIR/doc

rm $GRGENDIRSRC/engine-net-2/*
rm -rf $GRGENDIRSRC/engine-net-2/src
rm -rf $GRGENDIRSRC/engine-net-2/tools
rm -rf $GRGENDIRSRC/engine-net-2/examples/UML
rm -rf $GRGENDIRSRC/engine-net-2/examples/Firm-IFConv
cp -rf $GRGENDIRSRC/engine-net-2/* $GRGENDIR

cp -rf $GRGENDIRSRC/frontend/test $GRGENDIR

cp -rf $GRGENDIRSRC/syntaxhighlighting $GRGENDIR

cp $GRGENDIRSRC/LICENSE.txt $GRGENDIR

tar cjf $GRGENDIR.tar.bz2 $GRGENDIR
zip -r $GRGENDIR.zip $GRGENDIR

rm -rf $GRGENDIRSRC
rm -rf $GRGENDIR