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

  $exeprefix ../bin/GrShell.exe "$scriptfile" 2>&1 | awk "{sub(\"\\r\$\", \"\")}
    /^All attributes/ {
      do {
        getline
        while(\$0 ~ /^ - /) {
          print getAttribute(4)
          getline
        }
      }
      while(\$0 ~ /^All attributes/)
    }
	/(^The available attributes for)|(^(Node|Edge) types)|(^(Sub|Super) types of (node|edge) type)/ {
      do {
        getline
        while(\$0 ~ /^ - /) {
          print \$0
          getline
        }
      }
      while(\$0 ~ /(^The available attributes for)|(^(Node|Edge) types)|(^(Sub|Super) types of (node|edge) type)/)
    }
    /matches found/ { print \$2 }
    /rewrites performed/ { print \$2 }
    /Number/ { print \$8 }
    /value of attribute/ { print getAttribute(7) }
    /value of variable/ { print getAttribute(10) }
    /The graph is/ { print \$4 }

    function getAttribute(startindex)
    {
      if(startindex > NF) return \"\"

      value = \$startindex
      for(i = startindex + 1; i <= NF; i++)
      {
        value = value \" \" \$i
      }
      return value
    }" > "$scriptfile".data && echo "Data file generated" || echo "Data file generation failed!"
done
