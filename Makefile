all:
	mkdir build
	cd de/unika/ipd/grgen/parser/antlr/ && java -cp /usr/public/tools/grs_tools/antlr.jar antlr.Tool grgen.g
	find . -type f -name "*.java" | xargs javac -d build -classpath /usr/public/tools/grs_tools/antlr.jar:/usr/public/tools/grs_tools/jargs.jar -source 1.4

clean:
	rm -rf build/*
