#
# path to grgen dir
GRGENDIR=../..

#
# where JARGS is located
JARGS=$(GRGENDIR)/jars/jargs.jar

#
# where antlr is located
ANTLR=$(GRGENDIR)/jars/antlr.jar

#
# full classpath for executing grgen
CLASSPATH=$(JARGS):$(ANTLR):$(GRGENDIR)/grgen.jar

GMS     = $(wildcard *.gm)
GRGS    = $(wildcard *.grg)

DIRS_CSC= $(GRGS:.grg=_csc) # The directory for the C# compiling backend
BE_CSC  =	de.unika.ipd.grgen.be.Csharp.SearchPlanBackend

DIRS_CSI= $(GRGS:.grg=_csi) # The directory for the C# interperting backend

all: clean $(DIRS_CSC)
	cat success.log
	
%_csc : %.grg $(GMS)
	#create build dir
	test -d || mkdir $@
	#call grgen
	-java -Xmx256M -cp $(CLASSPATH) -ea de.unika.ipd.grgen.Main -n -b $(BE_CSC) -o $@ $< && echo "$< succeeded" >> success.log || echo "$< failed" >> failed.log
	# build the action and graph model for the generated inc/c-files
	#$(MAKE) -C $@ CFLAGS="$(CFLAGS)" LDFLAGS="-L$(FB_LIB) $(LDFLAGS)" LIBDIR=$(LIBDIR) all install
	touch $@

clean:
	rm -rf $(DIRS_CSC) $(DIRS_CSI) success.log failed.log

