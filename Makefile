PARSER_DIR = de/unika/ipd/grgen/parser/antlr

all:
	test -d build || mkdir build
	cd $(PARSER_DIR) && java -cp /usr/public/tools/grs_tools/antlr.jar antlr.Tool lexer.g
	cd $(PARSER_DIR) && java -cp /usr/public/tools/grs_tools/antlr.jar antlr.Tool base.g
	cd $(PARSER_DIR) && java -cp /usr/public/tools/grs_tools/antlr.jar antlr.Tool -glib base.g types.g
	cd $(PARSER_DIR) && java -cp /usr/public/tools/grs_tools/antlr.jar antlr.Tool -glib base.g actions.g
	find . -type f -name "*.java" | xargs javac -d build -classpath /usr/public/tools/grs_tools/antlr.jar:/usr/public/tools/grs_tools/jargs.jar -source 1.4
	touch .generator_build

clean:
	rm -rf build/*
