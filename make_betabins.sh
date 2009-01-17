#!/bin/bash

GRGENDIR=GrGenNET-V$1-`date +"%F"`
GRGENDIRSRC=$GRGENDIR-src

# export binaries and examples
svn export https://pp.info.uni-karlsruhe.de/svn/firm/trunk/grgen/engine-net-2/out/ $GRGENDIR
svn cat https://pp.info.uni-karlsruhe.de/svn/firm/trunk/grgen/LICENSE.txt > $GRGENDIR/LICENSE.txt

rm -rf $GRGENDIR/examples/UML
rm -rf $GRGENDIR/examples/Firm-IFConv

# make tar
tar cjf $GRGENDIR.tar.bz2 $GRGENDIR
zip -r $GRGENDIR.zip $GRGENDIR
