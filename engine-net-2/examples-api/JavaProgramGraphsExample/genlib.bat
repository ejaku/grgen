..\..\bin\grgen -keep . ..\..\examples\JavaProgramGraphs\JavaProgramGraphs.grg
@if ERRORLEVEL 1 PAUSE
copy ..\..\examples\JavaProgramGraphs\InstanceGraph.grs .
