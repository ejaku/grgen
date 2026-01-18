#!/bin/bash
(cd src/libGr && bash ./genparser.sh) || exit 1
(cd src/libGrShell && bash ./genparser.sh) || exit 1
(cd src/graphViewerAndSequenceDebugger && bash ./genparser.sh) || exit 1
