#!/bin/bash

GRGENDIR=GrGenNET-V$1-`date +"%F"`
GRGENDIRSRC=$GRGENDIR-src

# export all
svn export https://pp.info.uni-karlsruhe.de/svn/firm/trunk/grgen $GRGENDIRSRC

# delete doc-sources
mv $GRGENDIRSRC/doc/grgen.pdf $GRGENDIRSRC/
mv $GRGENDIRSRC/doc/VeryShortIntroductionToVersion2.txt $GRGENDIRSRC/
mv $GRGENDIRSRC/doc/ChangeLog.txt $GRGENDIRSRC/
mv $GRGENDIRSRC/doc/README.txt $GRGENDIRSRC/
rm -rf $GRGENDIRSRC/doc
mkdir $GRGENDIRSRC/doc
mv $GRGENDIRSRC/grgen.pdf $GRGENDIRSRC/doc/grgen.pdf
mv $GRGENDIRSRC/VeryShortIntroductionToVersion2.txt $GRGENDIRSRC/doc/VeryShortIntroductionToVersion2.txt
mv $GRGENDIRSRC/ChangeLog.txt $GRGENDIRSRC/doc/ChangeLog.txt
mv $GRGENDIRSRC/README.txt $GRGENDIRSRC/doc/README.txt

rm $GRGENDIRSRC/make_release.sh
rm $GRGENDIRSRC/make_betabins.sh
rm -rf $GRGENDIRSRC/todo

rm -rf $GRGENDIRSRC/engine-net-2/tools/ChangeFileHeaders
rm -rf $GRGENDIRSRC/engine-net-2/tools/test
rm -rf $GRGENDIRSRC/engine-net-2/examples/UML
rm -rf $GRGENDIRSRC/engine-net-2/examples/Firm-IFConv

# make tar
tar cjf $GRGENDIRSRC.tar.bz2 $GRGENDIRSRC
zip -r $GRGENDIRSRC.zip $GRGENDIRSRC


#make binary distribution
bash make_betabins.sh $1
