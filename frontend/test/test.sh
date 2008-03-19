#! /bin/sh

trap "echo; exit 1" INT QUIT HUP TERM

GRGENDIR=".."
JARGS="$GRGENDIR/jars/jargs.jar"
ANTLR="$GRGENDIR/jars/antlr.jar"
[ "$GRGENNET" ] || GRGENNET="$GRGENDIR/../engine-net-2/out/bin/"
BE_CSC=de.unika.ipd.grgen.be.Csharp.SearchPlanBackend2


GOLD=summary_gold.log
LOG=summary.log
TESTS="should_pass/*.grg should_warn/*.grg should_fail/*.grg"

OUTPUTSUFF=_out

APPEND=""
ONLY_FRONTEND=""
ONLY_NEW=""
VERBOSE=""

do_diff()
{
	diff -U0 $GOLD $LOG | grep "^\+"
}

while [ "$1" ]; do
	case "$1" in
		--) shift; break;;
		-a) shift; APPEND="TRUE";;
		-c) rm -fr */*$OUTPUTSUFF; exit 0;;
		-d) do_diff; exit 0;;
        -f) shift; ONLY_FRONTEND="TRUE";;
		-n) shift; ONLY_NEW="TRUE";;
		-v) shift; VERBOSE="TRUE";;
		* ) break;;
	esac
done

if [ "$ONLY_FRONTEND" ]; then LOG=summary_fe.log; fi

[ "$APPEND" ] || rm -f $LOG
touch $LOG

if uname -s | grep -iq "cygwin"; then
	SEP=";"
	MONO=
else
	SEP=":"
	MONO="mono"
fi
CLASSPATH=$JARGS$SEP$ANTLR$SEP$GRGENNET/grgen.jar

JAVA_ARGS="-Xmx256M -cp $CLASSPATH -ea de.unika.ipd.grgen.Main -b $BE_CSC"

do_test()
{
	local FILE="$1"
	local DIR="`echo "$FILE" | sed -e s/\\.grg\$/$OUTPUTSUFF/`"
	if [ "$ONLY_NEW" -a "$DIR" -nt "$FILE" ]; then return 0; fi
	rm -fr -- "$DIR"
	mkdir  -- "$DIR"
	echo -n "===> TEST $FILE"
	if java $JAVA_ARGS -o "$DIR" "$FILE" > "$DIR/log" 2>&1; then
        if grep -q "WARNING" < "$DIR/log"; then
            echo -n " ... WARNED"
            local WARNED="TRUE";
        else
      	    echo -n " ... OK"
            local WARNED="";
        fi
        if [ "$ONLY_FRONTEND" ]; then
            echo
            if [ "$WARNED" ]; then
                echo "WARNED $FILE" >> "$LOG"
            else
        		echo "OK     $FILE" >> "$LOG"
            fi
        else
    		if $MONO "$GRGENNET/GrGen.exe" -keep -use "$DIR" -o "$DIR" "$FILE" >> "$DIR/log" 2>&1; then
	    		if [ "$WARNED" ]; then
		    		echo " ... WARNED"
			    	echo "WARNED $FILE" >> "$LOG"
    			else
	    			echo " ... OK(C#)"
		    		echo "OK     $FILE" >> "$LOG"
			    fi
    		else
	    		echo " ... FAILED(C#)"
		    	echo "FAILED $FILE" >> "$LOG"
            fi
		fi
	elif grep -q -v "ERROR\|WARNING" < "$DIR/log"; then
		echo " ... ABEND"
		echo "ABEND  $FILE" >> "$LOG"
	else
		echo " ... ERROR"
		echo "ERROR  $FILE" >> "$LOG"
	fi
	if [ "$VERBOSE" ]; then cat "$DIR/log"; fi
}

if [ "$1" ]; then
	for i in "$@";  do
		if echo "$i" | grep -q "\\.grg\$"; then
			do_test "$i"; 
		fi
	done
else
	for i in $TESTS; do do_test "$i"; done
fi

do_diff
