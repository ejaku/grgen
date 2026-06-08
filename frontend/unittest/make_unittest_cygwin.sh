#!/bin/bash
# Builds the frontend and compiles+runs the JUnit acceptance tests.
# Must be run from the frontend/ directory (parent of unittest/).
# Completes much quicker than the full test suite(s).
# When working with an IDE like Eclipse you typically don't need this file.

set -e

# Build frontend first (ensures build/ is up to date)
make

# Compile acceptance tests
mkdir -p unittest/build
javac -encoding ISO8859_1 \
    -cp "jars/antlr-runtime-3.4.jar;jars/junit-4.13.2.jar;jars/hamcrest-core-1.3.jar;build" \
    -d unittest/build \
    unittest/AcceptanceTest.java

# Run acceptance tests - the class name must be prefixed by the package name so that this works under Windows with cygwin bash
java -cp "jars/antlr-runtime-3.4.jar;jars/junit-4.13.2.jar;jars/hamcrest-core-1.3.jar;build;unittest/build" \
    org.junit.runner.JUnitCore unittest.AcceptanceTest
