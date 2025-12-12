#! /bin/bash

if [ $# != 0 ]; then
  targets=$*
  for filename in $targets; do
    if [ ! -d $filename ]; then
      echo "$filename is not a directory!"
      continue;
    fi
  done
else
  targets=*
fi

for filename in $targets; do
  if [ $filename == "lib" -o ! -d $filename ]; then continue; fi

  for grs in "$filename"/*.grs; do
    if [ ! -f $grs.data ]; then
      continue
    fi

    echo $grs
  done
done
