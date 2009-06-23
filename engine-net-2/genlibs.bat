@echo off
for /d %%i in (test examples-api\*) do (
	if exist "%%i\genlib.bat" (
		echo Generating for %%i...
		pushd %%i
		call genlib.bat
		popd
	)
)
