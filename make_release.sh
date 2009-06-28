#!/bin/bash

GRGENDIR=GrGenNET-V$1-`date +"%F"`
GRGENDIRSRC=$GRGENDIR-src

#make source distribution - contains nearly everthing; some temporary/internal stuff missing
#contains binaries, examples, documentation, source code
#excluded internal stuff

svn export https://pp.info.uni-karlsruhe.de/svn/firm/trunk/grgen $GRGENDIRSRC

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

tar cjf $GRGENDIRSRC.tar.bz2 $GRGENDIRSRC
zip -r $GRGENDIRSRC.zip $GRGENDIRSRC


#make binary distribution
#contains binaries, examples, documentation
#excluded internal stuff,  source code

svn export https://pp.info.uni-karlsruhe.de/svn/firm/trunk/grgen/engine-net-2 $GRGENDIR
rm -rf $GRGENDIR/src
rm -rf $GRGENDIR/tools
rm $GRGENDIR/*

rm -rf $GRGENDIR/examples/UML
rm -rf $GRGENDIR/examples/Firm-IFConv

svn cat https://pp.info.uni-karlsruhe.de/svn/firm/trunk/grgen/LICENSE.txt > $GRGENDIR/LICENSE.txt

cp $GRGENDIRSRC/doc $GRGENDIR/doc

tar cjf $GRGENDIR.tar.bz2 $GRGENDIR
zip -r $GRGENDIR.zip $GRGENDIR
