#!/bin/bash

modpath="$(dirname "$(readlink -f "$0")")"
projpath="$modpath"
#csharpccjar="$projpath"/engine-net-2/csharpcc.jar

(cd "$projpath"/frontend && make -f Makefile_Cygwin all) || exit 1
(cd "$projpath"/engine-net-2/src/libGr && pwd && cmd /c genparser.bat) || exit 1 #takes some time
(cd "$projpath"/engine-net-2/src/libGrShell && cmd /c genparser.bat) || exit 1
(cd "$projpath"/engine-net-2/src/graphViewerAndSequenceDebugger && cmd /c genparser.bat) || exit 1
(cd "$projpath"/engine-net-2 && dotnet build GrGen.sln) || exit 1
#(cd "$projpath"/engine-net-2 && msbuild GrGen.sln) || exit 1 #you may use msbuild from VisualStudio instead of dotnet from the line above
