#!/bin/bash

modpath="$(dirname "$(readlink -f "$0")")"
projpath="$modpath"

(cd "$projpath"/frontend && pwd && cmd /c genparser.bat) || exit 1
(cd "$projpath"/frontend && dotnet build Frontend.sln) || exit 1

(cd "$projpath"/engine-net-2/src/libGr && pwd && cmd /c genparser.bat) || exit 1 #takes some time
(cd "$projpath"/engine-net-2/src/libGrShell && pwd && cmd /c genparser.bat) || exit 1
(cd "$projpath"/engine-net-2/src/graphViewerAndSequenceDebugger && pwd && cmd /c genparser.bat) || exit 1
(cd "$projpath"/engine-net-2/src/extMSAGLExt && pwd && cmd /c genparser.bat) || exit 1
(cd "$projpath"/engine-net-2 && pwd && dotnet build EngineNet2.sln) || exit 1
#(cd "$projpath"/engine-net-2 && msbuild EngineNet2.sln) || exit 1 #you may use msbuild from VisualStudio instead of dotnet from the line above

#(cd "$projpath" && dotnet build GrGen.sln) || exit 1 #alternatively build frontend and engine-net-2 from a single solution file