#! /bin/sh
# module add jdk-1.4.1-sun
source envs.sh
cd de/unika/ipd/grgen/parser/antlr/
java antlr.Tool grgen.g
cd ../../../../..
find . -type f -name "*.java" | xargs javac -source 1.4
