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
GRGENNET?=$(GRGENDIR)/../engine-net-2/out/bin/

#
# full classpath for executing grgen
CLASSPATH=$(JARGS):$(ANTLR):$(GRGENNET)/grgen.jar

GMS     = $(wildcard *.gm)
GRGS    = $(wildcard *.grg)

DIRS_FE = $(GRGS:.grg=_fe)  # The directory for the C# compiling backend

DIRS_CSC= $(GRGS:.grg=_csc) # The directory for the C# compiling backend
BE_CSC  =	de.unika.ipd.grgen.be.Csharp.SearchPlanBackend2

DIRS_CSI= $(GRGS:.grg=_csi) # The directory for the C# interperting backend

#
# the logfiles
SUCCESSFE   = successFE.log
FAILEDFE    = failedFE.log
ABENDFE     = abendFE.log


all: cleanfelog $(DIRS_FE)
	cat $(SUCCESSFE)
	cat $(ABENDFE)

fe: cleanfelog $(DIRS_FE)
	cat $(SUCCESSFE)

%_fe : %.grg $(GMS)
	@touch $(SUCCESSFE)
	@touch $(FAILEDFE)
	@test -d $@ || mkdir $@
	-(java -Xmx256M -cp $(CLASSPATH) -ea de.unika.ipd.grgen.Main -n -b $(BE_CSC) -o $@ $< && echo "$< succeeded" >> $(SUCCESSFE) || echo "$< failed" >> $(FAILEDFE)) 2>&1 > /dev/null | grep -q -v ERROR && echo "$< abend" >> $(ABENDFE)

clean:
	rm -rf $(DIRS_FE) $(DIRS_CSC) $(DIRS_CSI) $(SUCCESSFE) $(FAILEDFE) $(ABENDFE)

cleanfelog:
	@rm -f $(SUCCESSFE)
