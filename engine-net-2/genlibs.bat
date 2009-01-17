@echo off
for /d %%i in (test out\examples-lgsp\*) do (
	if exist "%%i\genlib.bat" (
		echo Generating for %%i...
		pushd %%i
		call genlib.bat
		popd
	)
)
