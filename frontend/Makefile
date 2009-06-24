#
# build the grgen-tool, grrr
#
PARSER_DIR = de/unika/ipd/grgen/parser/antlr
ANTLR_JAR  = $(PWD)/jars/antlr-3.1.jar
ANTLR_RUNTIME_JAR = $(PWD)/jars/antlr-runtime-3.1.jar
JARGS_JAR  = $(PWD)/jars/jargs.jar

ANTLR = java -cp $(ANTLR_JAR) org.antlr.Tool

GRGENNET_DIR ?= ../engine-net-2/bin
GRGEN_JAR = $(GRGENNET_DIR)/grgen.jar

all:	grgen

grgen: .grammar
	@if [ ! -e .generator_build -o ! -f $(GRGEN_JAR) ]; then \
	  $(MAKE) .generator_build; \
	else \
	  if [ "`find . -type f -name "*.java" -cnewer .generator_build`" != "" ]; then \
	    $(MAKE) .generator_build; \
	  fi; \
	fi

fast: .grammar
	@if [ ! -e .generator_build -o ! -f $(GRGEN_JAR) ]; then \
	  $(MAKE) .generator_build; \
	else \
	  if [ "`find . -type f -name "*.java" -cnewer .generator_build`" != "" ]; then \
	    $(MAKE) .generator_build_fast; \
	  fi; \
	fi

.generator_build: dir .grammar $(ANTLR_JAR) $(JARGS_JAR)
	find de -type f -name "*.java" | xargs javac -encoding ISO8859_1 -d build -classpath $(ANTLR_RUNTIME_JAR):$(JARGS_JAR) -source 1.5 -target 1.5
	jar -cmf GrGenManifest.mf $(GRGEN_JAR) -C build/ .
	@touch .generator_build

.generator_build_fast: dir .grammar $(ANTLR_JAR) $(JARGS_JAR)
	find de -type f -name "*.java" -cnewer .generator_build | xargs javac -encoding ISO8859_1 -d build -classpath $(ANTLR_RUNTIME_JAR):$(JARGS_JAR):build -source 1.5 -target 1.5
	jar -cmf GrGenManifest.mf $(GRGEN_JAR) -C build/ .
	@touch .generator_build

.PHONY: dir
dir:
	test -d build || mkdir build

.grammar: $(PARSER_DIR)/GrGen.g $(ANTLR_JAR)
	cd $(PARSER_DIR) && $(ANTLR) GrGen.g
	@touch .grammar

clean:
	rm -rf build/ .generator_build .grammar $(GRGEN_JAR)
	cd $(PARSER_DIR) && rm -f GrGenParser.java GrGenLexer.java GrGen.tokens
