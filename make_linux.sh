#!/bin/bash

modpath="$(dirname "$(readlink -f "$0")")"
projpath="$modpath"
csharpccjar="$projpath"/engine-net-2/csharpcc.jar

function gen_csharpcc() {
    [ -f "$csharpccjar" ] || { echo "no csharpcc jar at '$csharpccjar' found!"; exit 1; }
    java -classpath $csharpccjar csharpcc "$@"
}

(cd "$projpath"/frontend && make) || exit 1
(cd "$projpath"/engine-net-2/src/libGr && gen_csharpcc SequenceParser.csc) || exit 1
(cd "$projpath"/engine-net-2/src/libGr/GRSImporter && gen_csharpcc GRSImporter.csc) || exit 1
(cd "$projpath"/engine-net-2/src/GrShell && gen_csharpcc GrShell.csc) || exit 1
(cd "$projpath"/engine-net-2 && mdtool build -f:GrGen.sln) || exit 1
