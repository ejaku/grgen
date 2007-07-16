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
# where GrGen.NET is located
GRGENNET=$(GRGENDIR)/../grgennet/out/bin/

#
# full classpath for executing grgen
CLASSPATH=$(JARGS):$(ANTLR):$(GRGENDIR)/grgen.jar

GMS     = $(wildcard *.gm)
GRGS    = $(wildcard *.grg)

DIRS_FE = $(GRGS:.grg=_fe)  # The directory for the C# compiling backend

DIRS_CSC= $(GRGS:.grg=_csc) # The directory for the C# compiling backend
BE_CSC  =	de.unika.ipd.grgen.be.Csharp.SearchPlanBackend

DIRS_CSI= $(GRGS:.grg=_csi) # The directory for the C# interperting backend

all: clean $(DIRS_FE)
	cat successFE.log

csc: clean $(DIRS_CSC)
	cat successCSC.log


%_fe : %.grg $(GMS)
	@touch successFE.log
	@touch failedFE.log
	@test -d $@ || mkdir $@
	-java -Xmx256M -cp $(CLASSPATH) -ea de.unika.ipd.grgen.Main -n -b $(BE_CSC) -o $@ $< && echo "$< succeeded" >> successFE.log || echo "$< failed" >> failedFE.log

%_csc : %.grg $(GMS)
	@touch successCSC.log
	@touch failedCSC.log
	@test -d $@ || mkdir $@
	-mono $(GRGENNET)/GrGen.exe -o $@ $< && echo "$< succeeded" >> successCSC.log || echo "$< failed" >> failedCSC.log

clean:
	rm -rf $(DIRS_FE) $(DIRS_CSC) $(DIRS_CSI) successFE.log failedFE.log successCSC.log failedCSC.log

