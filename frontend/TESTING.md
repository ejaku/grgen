# Frontend/Compiler tests

## Running Tests

### Compiler Tests (`test/`)

```bash
# All tests
cd test && ./test.sh

# Frontend only (skip C# compilation, faster)
cd test && ./test.sh -f

# Specific test file
cd test && ./test.sh should_pass/mytest.grg

# Verbose output
cd test && ./test.sh -v

# Clean test outputs
cd test && ./test.sh -c
```

Test categories:
- `test/should_pass/` - Must compile successfully (500-1000 files)
- `test/should_fail/` - Must fail with errors (more than 1000 files)
- `test/should_warn/` - Must compile with warnings (more than 50 files)

Test outcomes:
- **OK**: Frontend compiles successfully, no warnings (and C# backend compiles if not `-f` mode)
- **WARNED**: Frontend compiles successfully but produces WARNING output
- **ERROR**: Frontend exits non-zero and produces ERROR output
- **ABEND**: Frontend exits non-zero without ERROR/WARNING (unexpected crash)
- **FAILED(C#)**: Frontend OK but C# backend compilation fails

Results are written to `test/summary.log` (or `test/summary_fe.log` with `-f`) and compared against `test/summary_gold.log`.

### JUnit Unit/Acceptance Tests (`unittest/`)

```bash
# Build frontend and run acceptance tests
./unittest/make_unittest.sh
```

Quick-running JUnit 4 tests that exercise the full compiler pipeline (parse, AST, IR, code generation) on small `.grg`/`.gm` input files.
The test files live in `unittest/` alongside `AcceptanceTest.java`.

## Generating Test

For generating tests see TESTGENERATION.md
