#!/bin/bash

GRGENDIR=GrGenNET-V$1-`date +"%F"`
GRGENDIRSRC=$GRGENDIR-src

# export all
svn export file:///ben/firm/svn/trunk/grgen $GRGENDIRSRC

# delete doc-sources
mv $GRGENDIRSRC/doc/grgen.pdf $GRGENDIRSRC/
rm -rf $GRGENDIRSRC/doc
mkdir $GRGENDIRSRC/doc
mv $GRGENDIRSRC/grgen.pdf $GRGENDIRSRC/doc/grgen.pdf

# delete new GrGen.NET 2.0 engine
rm -rf $GRGENDIRSRC/engine-net-2

# make tar
tar cjf $GRGENDIRSRC.tar.bz2 $GRGENDIRSRC


# export binaries and examples
svn export file:///ben/firm/svn/trunk/grgen/engine-net/out/ $GRGENDIR

# make tar
tar cjf $GRGENDIR.tar.bz2 $GRGENDIR

