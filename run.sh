#! /bin/sh
BE=de.unika.ipd.grgen.be.C.PGSQLBackend
OUTDIR=$2
INFILE=$1
shift 2

java -ea de.unika.ipd.grgen.Main -b $BE -p settings.xml $* -o $OUTDIR $INFILE 
