#! /bin/sh

FLAGS="-a -i "
BE=de.unika.ipd.grgen.be.C.PGSQLBackend
OUTDIR=$2
INFILE=$1
shift 2

java -ea de.unika.ipd.grgen.Main $FLAGS -b $BE -p settings.xml $* -o $OUTDIR $INFILE 
