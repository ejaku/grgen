#!/bin/bash

modpath="$(dirname "$(readlink -f "$0")")"
projpath="$modpath"
#csharpccjar="$projpath"/engine-net-2/csharpcc.jar

(cd "$projpath"/frontend && make) || exit 1
(cd "$projpath"/engine-net-2/src/libGr && bash ./genparser.sh) || exit 1
(cd "$projpath"/engine-net-2/src/libGrShell && bash ./genparser.sh) || exit 1
(cd "$projpath"/engine-net-2/src/graphViewerAndSequenceDebugger && bash ./genparser.sh) || exit 1
(cd "$projpath"/engine-net-2 && dotnet build GrGen.sln) || exit 1
#(cd "$projpath"/engine-net-2 && mdtool build -f:GrGen.sln) || exit 1 #outdated build tool from mono (develop), you may use this one instead of dotnet from the line above
