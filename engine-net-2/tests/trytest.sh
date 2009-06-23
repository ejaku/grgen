#! /bin/bash

if [ $# != 1 ]; then
  echo "Usage: $0 <grs-script>"
  exit 1
fi

if [[ "$(uname -s)" == "Linux" ]]; then
  exeprefix=mono
fi

< "$1" grep -v '^quit' > tmptest.grs
$exeprefix ../bin/GrShell.exe tmptest.grs
rm tmptest.grs
