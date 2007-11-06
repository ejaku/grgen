#! /bin/bash
if [ $# == 0 ]; then
  echo "Usage: $0 <validated-grs-script>"
  exit 1
fi

if [[ "$(uname -s)" == "Linux" ]]; then
  exeprefix=mono
fi

for scriptfile in $*; do
  if [ ! -f "$scriptfile" ]; then
    echo "$scriptfile is not a file!"
    exit 1
  fi

  if [[ "$scriptfile" != *\.grs ]]; then
    echo "$scriptfile is not a .grs file!"
    exit 1
  fi
  
  echo "$scriptfile"

  $exeprefix ../bin/GrShell.exe "$scriptfile" | awk "/matches found/ { print \$2 }
    /rewrites performed/ { print \$2 }
    /Number/ { print \$8 }
    /value of attribute/ {
      value = \"\"
      for(i = 7; i <= NF; i++)
      {
        if(i == 7) value = \$i
        else value = value \" \" \$i
      }
      print value
    }" > "$scriptfile".data && echo "Data file generated" || echo "Data file generation failed!"    
done