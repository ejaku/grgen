#! /bin/sh

FILE="$1"
ONLY_NEW="$2"
ONLY_FRONTEND="$3"
LOG="$4"
OUTPUTSUFF="$5"


GRGENDIR=".."
JARGS="$GRGENDIR/jars/jargs.jar"
ANTLR="$GRGENDIR/jars/antlr.jar"
[ "$GRGENNET" ] || GRGENNET="$GRGENDIR/../engine-net-2/out/bin/"
BE_CSC=de.unika.ipd.grgen.be.Csharp.SearchPlanBackend2

if uname -s | grep -iq "cygwin"; then
	SEP=";"
	MONO=
else
	SEP=":"
	MONO="mono"
fi
CLASSPATH=$JARGS$SEP$ANTLR$SEP$GRGENNET/grgen.jar

JAVA_ARGS="-Xms256M -Xmx512M -cp $CLASSPATH -ea de.unika.ipd.grgen.Main -b $BE_CSC -t"


DIR="`echo "$FILE" | sed -e s/\\.grg\$/$OUTPUTSUFF/`"
if [ "$ONLY_NEW" -a "$DIR" -nt "$FILE" ]; then return 0; fi
rm -fr -- "$DIR"
mkdir  -- "$DIR"

if java $JAVA_ARGS -o "$DIR" "$FILE" > "$DIR/log" 2>&1; then
	if grep -q "WARNING" < "$DIR/log"; then
		WARNED="TRUE";
	else
		WARNED="";
	fi
	if [ "$ONLY_FRONTEND" ]; then
		if [ "$WARNED" ]; then
			exit 10    # WARNED
		else
			exit 11    # OK
		fi
	else
		if $MONO "$GRGENNET/GrGen.exe" -keep -use "$DIR" -o "$DIR" "$FILE" >> "$DIR/log" 2>&1; then
			if [ "$WARNED" ]; then
				exit 12    # WARNED ... OK(C#)
			else
				exit 13    # OK ... OK(C#)
			fi
		else
			if [ "$WARNED" ]; then
				exit 14    # WARNED ... FAILED(C#)
			else
				exit 15    # OK ... FAILED(C#)
			fi
		fi
	fi
elif grep -q -v "ERROR\|WARNING" < "$DIR/log"; then
	exit 16    # ABEND
else
	exit 17    # ERROR
fi
