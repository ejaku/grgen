@echo off
set CP=..\..\release\antlr.jar;%CLASSPATH%
set cmd=java -classpath "%CP%" antlr.Tool %*
echo %cmd%
%cmd%
set CP=
