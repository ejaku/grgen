#! /bin/bash

if [ $# != 1 ]; then
  echo "Usage: $0 <test-dir-name>"
  exit 1
fi

for grs in "$1"/*.grs; do
  if [ ! -f $grs ]; then
    echo "$1 does not contain any .grs files!"
    exit 1
  fi
  
  gmfile=`echo "$1"/*.gm`
  echo "$gmfile"
  if [ $gmfile != "" ]; then
    echo "No model files found in $1!"
    exit 1
  fi
  echo "Prepare $grs"
  < $grs sed -e "s/\\.\\.\\\\bin/\\.\\.\\\\\\.\\.\\\\bin/" -e "s/new graph \"/new graph \"lib\//" -e "s/select actions \"/select actions \"lib\//" -e "s/debug grs/grs/" -e "s/debug xgrs/xgrs/" | awk "BEGIN {
      # Collect all node and edge types from the model file
      i = 1
      j = 1
      gmfiles = \"$gmfile\"
      if(gmfiles != \"\")
      {
        do
        {
          spaceind = index(gmfiles, \" \")
          if(spaceind != 0)
          {
            gmfile = substr(gmfiles, 1, spaceind - 1)
            gmfiles = substr(gmfiles, spaceind + 1)
          }
          else
          {
            gmfile = gmfiles
            gmfiles = \"\"
          }
          while ((getline < gmfile) > 0)
          {
            if(\$1 == \"node\")
            {
              name = \$3
              if(index(name, \";\") != 0)
                name = substr(name, 1, length(name) - 1)
              nodetypes[i++] = name
            }
            else if(\$1 == \"edge\")
            {
              name = \$3
              if(index(name, \";\") != 0)
                name = substr(name, 1, length(name) - 1)
              edgetypes[j++] = name
            }
          }
        }
        while(gmfiles != \"\")
      }
    }
    /^dump graph/ { next }
    /^show graph/ { next }
    /^debug enable/ { next }
    /^debug disable/ { next }
    /^grs / { printShows() }
    /^xgrs / { printShows() }
    /^quit/ {
      printShows()
      print \"quit\"
      quitfound = 1
    }
    { print \$0 }
    function printShows() {
      print \"show num nodes\"
      print \"show num edges\"
      for(nodetype in nodetypes)
        print \"show num nodes \\\"\" nodetypes[nodetype] \"\\\"\"
      for(edgetype in edgetypes)
        print \"show num edges \\\"\" edgetypes[edgetype] \"\\\"\"
    }
    END {
      if(quitfound == 0)
      {
        printShows()
        print \"quit\"
      }
    }" > $grs.tmp
  mv $grs.tmp $grs
done
echo "Done"
