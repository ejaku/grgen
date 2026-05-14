#! /bin/bash

if [ $# != 0 -a "$1" == "--mono" ]; then
  exeprefix=mono
  shift
fi

if [ $# != 0 ]; then
  targets=$*
  for dirname in $targets; do
    if [ ! -d $dirname ]; then
      echo "$dirname is not a directory!"
      continue;
    fi
  done
else
  targets=*
fi

if [[ "$(uname -s)" == "Linux" ]]; then
  exeprefix=mono
fi

if [ exeprefix ]; then
  echo "Using $exeprefix"
fi

rm testlog.txt

for dirname in $targets; do
  if [ $dirname == "lib" -o ! -d $dirname ]; then continue; fi
  cd $dirname
  echo $dirname
  echo "$dirname:" >> ../testlog.txt

  for grs in *.grs; do
    echo -n "- $grs:"
    $exeprefix ../../bin/GrShell.exe -N $grs < /dev/null #reading from /dev/null works with cygwin, but fails under LINUX bash, exit examples with quit or search for working solution
    echo "  $grs -> $?"  >> ../testlog.txt
  done
  cd ..
done

