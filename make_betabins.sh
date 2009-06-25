#!/bin/bash
#this script is used by make_release to build the binary distribution and stand alone to build beta only binary distributions

GRGENDIR=GrGenNET-V$1-`date +"%F"`

# export binaries and examples
svn export https://pp.info.uni-karlsruhe.de/svn/firm/trunk/grgen/engine-net-2 $GRGENDIR
	#keep bin, examples, examples-api,tests
rm -rf src
rm -rf tools

svn cat https://pp.info.uni-karlsruhe.de/svn/firm/trunk/grgen/LICENSE.txt > $GRGENDIR/LICENSE.txt

rm -rf $GRGENDIR/examples/UML
rm -rf $GRGENDIR/examples/Firm-IFConv

# make tar
tar cjf $GRGENDIR.tar.bz2 $GRGENDIR
zip -r $GRGENDIR.zip $GRGENDIR
