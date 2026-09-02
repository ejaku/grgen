#!/bin/bash

modpath="$(dirname "$(readlink -f "$0")")"
projpath="$modpath"

(cd "$projpath"/frontend && bash ./genparser.sh) || exit 1
(cd "$projpath"/frontend && dotnet build Frontend.sln) || exit 1

(cd "$projpath"/engine-net-2/src/libGr && bash ./genparser.sh) || exit 1
(cd "$projpath"/engine-net-2/src/libGrShell && bash ./genparser.sh) || exit 1
(cd "$projpath"/engine-net-2/src/graphViewerAndSequenceDebugger && bash ./genparser.sh) || exit 1
(cd "$projpath"/engine-net-2 && dotnet build EngineNet2.sln) || exit 1
#(cd "$projpath"/engine-net-2 && mdtool build -f:EngineNet2.sln) || exit 1 #outdated build tool from mono (develop), you may use this one instead of dotnet from the line above

#(cd "$projpath" && dotnet build GrGen.sln) || exit 1 #alternatively build frontend and engine-net-2 from a single solution file