#! /bin/bash

trap "echo; exit 1" INT QUIT HUP TERM

MAXTHREADS=6

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
        -f) shift; ONLY_FRONTEND="TRUE"; LOG=summary_fe.log;;
		-n) shift; ONLY_NEW="TRUE";;
		-v) shift; VERBOSE="TRUE";;
		* ) break;;
	esac
done

[ "$APPEND" ] || rm -f $LOG
touch $LOG

NEXTTHREAD=-1

do_waitandoutput()
{
	local FILE=${Threadfile[$1]}
	echo -n "=$1==> TEST $FILE"
	wait ${Threads[$1]}
	local STATUS=$?
	case "$STATUS" in
		10) echo " ... WARNED"
			echo "WARNED $FILE" >> "$LOG";;
		11) echo " ... OK"
			echo "OK     $FILE" >> "$LOG";;
		12) echo " ... WARNED ... OK(C#)"
			echo "WARNED $FILE" >> "$LOG";;
		13) echo " ... OK ... OK(C#)"
			echo "OK     $FILE" >> "$LOG";;
		14) echo " ... WARNED ... FAILED(C#)"
			echo "FAILED $FILE" >> "$LOG";;
		15) echo " ... OK ... FAILED(C#)"
			echo "FAILED $FILE" >> "$LOG";;
		16) echo " ... ABEND"
			echo "ABEND  $FILE" >> "$LOG";;
		17) echo " ... ERROR"
			echo "ERROR  $FILE" >> "$LOG";;
		*)  echo " --- Unknown return value $STATUS!!"
			echo "UNKNOWN($STATUS) $FILE" >> "$LOG";;
	esac
	if [ "$VERBOSE" ]; then
		local DIR="`echo "$FILE" | sed -e s/\\.grg\$/$OUTPUTSUFF/`"
		cat "$DIR/log"
	fi
}

do_test()
{
	NEXTTHREAD=`expr $NEXTTHREAD + 1`
	if [ $NEXTTHREAD -eq $MAXTHREADS ]; then NEXTTHREAD=0; fi
	if [ ${Threads[$NEXTTHREAD]} ]
	then
		do_waitandoutput "$NEXTTHREAD"
	fi
	
	./testpar_do.sh "$1" "$ONLY_NEW" "$ONLY_FRONTEND" "$LOG" "$OUTPUTSUFF" &
	Threads[$NEXTTHREAD]=$!
	Threadfile[$NEXTTHREAD]=$1
}

do_waitforrest()
{
	local LASTTHREAD=$NEXTTHREAD
	
	while true
	do
		NEXTTHREAD=`expr $NEXTTHREAD + 1`
		if [ $NEXTTHREAD -eq $MAXTHREADS ]; then NEXTTHREAD=0; fi
	
		if [ ${Threads[$NEXTTHREAD]} ]
		then
			do_waitandoutput "$NEXTTHREAD"
		fi
		
		if [ $NEXTTHREAD -eq $LASTTHREAD ]; then break; fi
	done
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

do_waitforrest

do_diff
