#! /bin/bash
if [ $# != 1 ]; then
  echo "Usage: $0 <test-dir-name>"
  exit 1
fi

if [[ "$(uname -s)" == "Linux" ]]; then
  exeprefix=mono
fi

$exeprefix ../bin/GrGen.exe -o lib "$1"/*.grg > /dev/null && echo "Library files generated" || echo "Library file generation failed!"
