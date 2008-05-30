#
# build the grgen-tool, grrr
#
PARSER_DIR = de/unika/ipd/grgen/parser/antlr
ANTLR_JAR  = $(PWD)/jars/antlr.jar
JARGS_JAR  = $(PWD)/jars/jargs.jar

ANTLR = java -cp $(ANTLR_JAR) antlr.Tool

GRGENNET_DIR ?= ../engine-net-2/out/bin
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
	find de -type f -name "*.java" | xargs javac -encoding ISO8859_1 -d build -classpath $(ANTLR_JAR):$(JARGS_JAR) -source 1.5 -target 1.5
	jar -cmf GrGenManifest.mf $(GRGEN_JAR) -C build/ .
	@touch .generator_build

.generator_build_fast: dir .grammar $(ANTLR_JAR) $(JARGS_JAR)
	find de -type f -name "*.java" -cnewer .generator_build | xargs javac -encoding ISO8859_1 -d build -classpath $(ANTLR_JAR):$(JARGS_JAR):build -source 1.5 -target 1.5
	jar -cmf GrGenManifest.mf $(GRGEN_JAR) -C build/ .
	@touch .generator_build

.PHONY: dir
dir:
	test -d build || mkdir build

.grammar: $(PARSER_DIR)/lexer.g $(PARSER_DIR)/base.g $(PARSER_DIR)/types.g $(PARSER_DIR)/actions.g $(ANTLR_JAR)
	cd $(PARSER_DIR) && $(ANTLR) lexer.g
	cd $(PARSER_DIR) && $(ANTLR) base.g
	cd $(PARSER_DIR) && $(ANTLR) -glib base.g types.g
	cd $(PARSER_DIR) && $(ANTLR) -glib base.g actions.g
	@touch .grammar

clean:
	rm -rf build/ .generator_build .grammar $(GRGEN_JAR)
	cd $(PARSER_DIR) && rm -f GRActionsParser.java GRActionsParserTokenTypes.java GRBaseParser.java GRBaseParserTokenTypes.java GRBaseTokenTypes.java GRLexer.java GRTypeParser.java GRTypeParserTokenTypes.java GRActionsParserTokenTypes.txt GRBaseParserTokenTypes.txt  GRTypeParserTokenTypes.txt expandedactions.g expandedtypes.g GRBaseParser.smap GRTypeParser.smap GRActionsParser.smap GRLexer.smap
