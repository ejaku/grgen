#!/bin/bash

GRGENDIR=GrGenNET-V$1-`date +"%F"`

#make beta only binary distribution
#contains binaries, examples
#excluded internal stuff, documentation, source code

svn export https://pp.info.uni-karlsruhe.de/svn/firm/trunk/grgen/engine-net-2 $GRGENDIR
rm -rf $GRGENDIR/src
rm -rf $GRGENDIR/tools
rm $GRGENDIR/*

rm -rf $GRGENDIR/examples/UML
rm -rf $GRGENDIR/examples/Firm-IFConv

svn cat https://pp.info.uni-karlsruhe.de/svn/firm/trunk/grgen/LICENSE.txt > $GRGENDIR/LICENSE.txt

tar cjf $GRGENDIR.tar.bz2 $GRGENDIR
zip -r $GRGENDIR.zip $GRGENDIR
