#! /bin/sh

trap "echo; exit 1" INT QUIT HUP TERM

GRGENDIR="../.."
JARGS="$GRGENDIR/jars/jargs.jar"
ANTLR="$GRGENDIR/jars/antlr.jar"
[ "$GRGENNET" ] || GRGENNET="$GRGENDIR/../engine-net-2/out/bin/"
BE_CSC=de.unika.ipd.grgen.be.Csharp.SearchPlanBackend2


SUCCESSFE=successFE.log
FAILEDFE=failedFE.log
ABENDFE=abendFE.log

APPEND=""
ONLY_NEW=""
VERBOSE=""

while [ "$1" ]; do
	case "$1" in
		--) shift; break;;
		-a) shift; APPEND="TRUE";;
		-c) shift; rm -fr *_fe; exit 0;;
		-n) shift; ONLY_NEW="TRUE";;
		-v) shift; VERBOSE="TRUE";;
		* ) break;;
	esac
done

[ "$APPEND" ] || rm -f "$SUCCESSFE" "$FAILEDFE" "$ABENDFE"
touch "$SUCCESSFE" "$FAILEDFE" "$ABENDFE"

if uname -s | grep -iq "cygwin"; then
	SEP=";"
else
	SEP=":"
fi
CLASSPATH=$JARGS$SEP$ANTLR$SEP$GRGENNET/grgen.jar

JAVA_ARGS="-Xmx256M -cp $CLASSPATH -ea de.unika.ipd.grgen.Main -n -b $BE_CSC"

do_test()
{
	local FILE="$1"
	local DIR="`echo "$FILE" | sed -e 's/\.grg$/_fe/'`"
	if [ "$ONLY_NEW" -a "$DIR" -nt "$FILE" ]; then return 0; fi
	rm -fr -- "$DIR"
	mkdir  -- "$DIR"
	echo -n "===> TEST $FILE"
	if java $JAVA_ARGS -o "$DIR" "$FILE" > "$DIR/log" 2>&1; then
		echo " ... OK"
		echo "$FILE" >> "$SUCCESSFE"
	elif grep -q -v "ERROR\|WARNING" < "$DIR/log"; then
		echo " ... ABEND"
		echo "$FILE" >> "$ABENDFE"
	else
		echo " ... FAILED"
		echo "$FILE" >> "$FAILEDFE"
	fi
	if [ "$VERBOSE" ]; then cat "$DIR/log"; fi
}

if [ "$1" ]; then
	for i in "$@";  do do_test "$i"; done
else
	for i in *.grg; do do_test "$i"; done
fi

echo SUCCESS
cat "$SUCCESSFE"
echo FAILED
cat "$FAILEDFE"
echo ABEND
cat "$ABENDFE"
