#! /bin/bash

(
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

if [[ "$(uname -s)" == "Linux" ]]; then
  exeprefix=mono
fi

for filename in $targets; do
  if [ $filename == "lib" -o ! -d $filename ]; then continue; fi
  echo $filename
  
  found=0
  for datafile in "$filename"/*.data; do
    if [ ! -f $datafile ]; then continue; fi
    found=1
    break
  done
  
  if [ $found = 0 ]; then
    echo "No data files found in subdirectory!"
    continue
  fi

  echo -n "- Building libraries:"
  $exeprefix ../bin/GrGen.exe -o lib "$filename"/*.grg > /dev/null
  if [ $? -ne 0 ]; then
    echo -e "\n    GrGen-Processing failed!"
    continue
  fi
  
  echo " Success!"
  
  for grs in "$filename"/*.grs; do
    echo -n "- $grs:"
    if [ ! -f $grs.data ]; then
      echo -e "\nOutput data file for $grs not found!"
      continue
    fi
  
    $exeprefix ../bin/GrShell.exe $grs < /dev/null | awk "BEGIN { testnum = 0 }
      {sub(\"\\r\$\", \"\")}
      /^All attributes/ {
        do {
          getline
          while(\$0 ~ /^ - /) {
            testnum++
            value = getAttribute(4)
            getline correctvalue < \"$grs.data\"
            if(value != correctvalue)
              fail(testnum, \"\n  Test \" testnum \" failed: Expected value of attribute = \" correctvalue \", Found \" value)
            getline            
          }
        }
        while(\$0 ~ /^All attributes/)
      }
      /NOT valid/ {
        print \"\n    Graph validation failed after test \" testnum \": \" > \"/dev/stderr\"
        getline
        while(\$1 == \"CAE:\") {
          print \"    \" \$0 > \"/dev/stderr\"
          getline
        }
        fail(testnum, 0)
      }
      /^> / {
        fail(testnum, \"\n  Test failed! It is waiting for user input!\")
      }
      /Sequence done/ { time += \$7 }
      /analyzed in/ { time += \$(NF-1) }
      /generated in/ { time += \$(NF-1) }
      /matches found/ {
        testnum++
        getline correctmatches < \"$grs.data\"
        if(\$2 != correctmatches)
          fail(testnum, \"\n  Test \" testnum \" failed: Expected matches = \" correctmatches \", Found matches = \" \$2)
      }
      /rewrites performed/ {
        testnum++
        getline correctrewrites < \"$grs.data\"
        if(\$2 != correctrewrites)
          fail(testnum, \"\n  Test \" testnum \" failed: Expected rewrites = \" correctrewrites \", Found rewrites = \" \$2)
      }
      /Number/ {
        testnum++
        getline correctnum < \"$grs.data\"
        if(\$8 != correctnum)
          fail(testnum, \"\n  Test \" testnum \" failed: Expected number = \" correctnum \", Found \" \$0)
      }
      /value of attribute/ {
        testnum++
        value = getAttribute(7)
        getline correctvalue < \"$grs.data\"
        if(value != correctvalue)
          fail(testnum, \"\n  Test \" testnum \" failed: Expected value of attribute = \" correctvalue \", Found \" value)
      }
      END {
        if(failed) exit 1
        
        if((getline noline < \"$grs.data\") > 0)
          fail(testnum, \"\n  Unexpected end of test after Test \" testnum \"!\")
        
        print \" Success! Total told time: \" time \" ms\"
      }
      
      function getAttribute(startindex)
      {
        if(startindex > NF) return \"\"
        
        value = \$startindex
        for(i = startindex + 1; i <= NF; i++)
        {
          value = value \" \" \$i
        }
        return value
      }
      
      function fail(testnum, string)
      {
        if(string)
          print string > \"/dev/stderr\"
        failed = 1
        exit 1
      }"

  done
done
) 2>&1 | tee test-`date +%Y%m%d-%H%M%S`.log
