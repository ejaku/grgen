# Build Commands

```bash
# Build compiler frontend (produces grgen.jar)
make

# Fast incremental build
make fast

# Regenerate ANTLR parser (after grammar changes)
make .grammar

# Clean build artifacts
make clean
```

Output JAR: `../engine-net-2/bin/grgen.jar`

Under Windows you have to use cygwin:
```bash
make -f Makefile_Cygwin
```

You may also use an IDE but have to add the jars from the jars folder to the build path manually in that case (the test jars are only needed when executing unit tests).
