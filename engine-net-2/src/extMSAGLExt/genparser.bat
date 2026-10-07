@echo off
rem Generates VcgLexer.cs and VcgParser.cs from Vcg.g using ANTLR 3.5.
rem Run from the engine-net-2/src/extMSAGLExt directory.
.\..\..\..\frontend\antlr-dotnet-csharpbootstrap-3.5.0.2\Antlr3.exe Vcg.g
@if ERRORLEVEL 1 PAUSE
