# Semantic/Backend tests

## Running Tests

### Semantic Tests (`tests/`)

```bash
# All backend tests
cd tests && ./test.sh

# Specific test directories
cd tests && ./test.sh array1 arith

# With explicit Mono prefix
cd tests && ./test.sh --mono
```

Tests compare GrShell output against `.grs.data` reference files.
(More than 100 test directories containing more than 1000 `.grs` test scripts.)

### NUnit Tests (`unittests/`)

```bash
cd unittests/GraphAndModelTests && dotnet test
```

NUnit 3 tests that exercise the C# backend API directly (graph creation, node/edge manipulation, model loading at the time of writing).
The project references `libGr` and `lgspBackend` from `bin/`.

### Test Structure

Each test directory contains:
- `.gm` - Model definition
- `.grg` - Rules/grammar
- `.grs` - Test script(s) — multiple scripts can share the same `.gm`/`.grg`
- `.grs.data` - Expected output

## Generating Tests

For generating tests see tests/TESTGENERATION.md
It contains details on test creation and points to a GrGen language reference.
