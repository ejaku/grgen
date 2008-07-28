#!/bin/bash

GRGENDIR=GrGenNET-V$1-`date +"%F"`
GRGENDIRSRC=$GRGENDIR-src

# export all
svn export file:///ben/firm/svn/trunk/grgen $GRGENDIRSRC

# delete doc-sources
mv $GRGENDIRSRC/doc/grgen.pdf $GRGENDIRSRC/
mv $GRGENDIRSRC/doc/VeryShortIntroductionToVersion2.txt $GRGENDIRSRC/
rm -rf $GRGENDIRSRC/doc
mkdir $GRGENDIRSRC/doc
mv $GRGENDIRSRC/grgen.pdf $GRGENDIRSRC/doc/grgen.pdf
mv $GRGENDIRSRC/VeryShortIntroductionToVersion2.txt $GRGENDIRSRC/doc/VeryShortIntroductionToVersion2.txt

# delete old GrGen.NET 1.0 engine
rm -rf $GRGENDIRSRC/engine-net

rm -rf  $GRGENDIRSRC/engine-net-2/out/examples/UML
rm -rf  $GRGENDIRSRC/engine-net-2/out/examples/Firm-IFConv

# make tar
tar cjf $GRGENDIRSRC.tar.bz2 $GRGENDIRSRC


# export binaries and examples
svn export file:///ben/firm/svn/trunk/grgen/engine-net-2/out/ $GRGENDIR

rm -rf  $GRGENDIR/examples/UML
rm -rf  $GRGENDIR/examples/Firm-IFConv

# make tar
tar cjf $GRGENDIR.tar.bz2 $GRGENDIR

