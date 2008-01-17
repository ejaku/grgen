#! /bin/sh

trap "exit 1" INT QUIT TERM HUP

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
		--) shift;                  break;;
		-a) shift; APPEND="TRUE";   break;;
		-c) rm -rf *_fe;            exit 0;;
		-n) shift; ONLY_NEW="TRUE"; break;;
		-v) shift; VERBOSE="TRUE";  break;;
		* )                         break;;
	esac
done

if [ ! "$VERBOSE" ]; then
	[ ! "$APPEND" ] || rm -f "$SUCCESSFE" "$FAILEDFE" "$ABENDFE"
	touch "$SUCCESSFE" "$FAILEDFE" "$ABENDFE"
fi

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
	if [ "$VERBOSE" ]; then
		echo
		java $JAVA_ARGS -o "$DIR" "$FILE"
	else
		(java $JAVA_ARGS -o "$DIR" "$FILE" && echo "$FILE succeeded" >> "$SUCCESSFE" || echo "$FILE failed" >> "$FAILEDFE") 2>&1 > /dev/null | grep -q -v "ERROR" && echo "$FILE abend" >> "$ABENDFE" && echo -n " ... ABEND" || echo -n " ... FAILED"
		echo
	fi
}

if [ "$1" ]; then
	for i in "$@";  do
		if echo "$i" | grep -q -v '\.grg$'; then
			echo "$i is not a .grg"
			continue
		fi
		do_test "$i"
	done
else
	for i in *.grg; do do_test "$i"; done
fi
