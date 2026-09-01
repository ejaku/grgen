# Build Commands

```bash
# Build entire solution (compiler frontend)
dotnet build Frontend.sln

# Build specific configuration
dotnet build Frontend.sln -c Release

# Generate parsers first (required if .g grammar files changed)
./genparser.sh   # Linux
genparser.bat    # Windows
```

Parser generation uses ANTLR to generate C# from `.g` files in:
- `de/unika/ipd/grgen/parser/antlr`

Under Windows, you may also use msbuild or the Visual Studio GUI.

Output exe: `../engine-net-2/bin/FrontendGrGen.exe`
