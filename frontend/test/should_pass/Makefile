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
# guess, where GrGen.NET is located, if not already set
GRGENNET?=$(GRGENDIR)/../engine-net-2/out/bin

#
# full classpath for executing grgen
CLASSPATH=$(JARGS):$(ANTLR):$(GRGENNET)/grgen.jar

GMS     = $(wildcard *.gm)
GRGS    = $(wildcard *.grg)

#
# the logfiles
SUCCESSFE   = successFE.log
FAILEDFE    = failedFE.log
SUCCESSCSC  = successCSC.log
FAILEDCSCFE = failedCSC_FE.log
FAILEDCSC   = failedCSC.log


TARGETS_FE= $(GRGS:.grg=_fe)        # The targets for the frontend-only test
SUCCESSFILES_FE= $(GRGS:.grg=_fe)   # The success files for the frontend-only test
DIRS_FE = $(GRGS:.grg=_fe_out)      # The directory for the frontend-only test

TARGETS_CSC= $(GRGS:.grg=_csc_success)      # The targets for the C# compiling backend
SUCCESSFILES_CSC= $(GRGS:.grg=_csc_success) # The success files for the C# compiling backend
DIRS_CSC= $(GRGS:.grg=_csc_out)             # The directory for the C# compiling backend
BE_CSC  = de.unika.ipd.grgen.be.Csharp.SearchPlanBackend2

DIRS_CSI= $(GRGS:.grg=_csi)         # The directory for the C# interpreting backend

all: cleanfelog $(TARGETS_FE)
	cat $(FAILEDFE)

csc: cleancsclog csccontinue ;

csccontinue: $(TARGETS_CSC)
	@echo -e "-------------\nFailed in frontend of GrGen (from $(FAILEDCSCFE)):\n"
	@cat $(FAILEDCSCFE)
	@echo -e "\nFailed in C# part of GrGen (from $(FAILEDCSC)):\n"
	@cat $(FAILEDCSC)

grgenuptodate: $(GRGENDIR)/grgen.jar
	@touch grgenuptodate
	cp $(GRGENDIR)/grgen.jar $(GRGENNET)/

%_fe : %.grg $(GMS)
	@touch $(SUCCESSFE)
	@touch $(FAILEDFE)
	@test -d $@_out || mkdir $@_out
	-java -Xmx256M -cp $(CLASSPATH) -ea de.unika.ipd.grgen.Main -n -t -b $(BE_CSC) -o $@_out $< && echo "$< succeeded" >> $(SUCCESSFE) && touch $@ || echo "$< failed" >> $(FAILEDFE)

%_csc : %.grg $(GMS) grgenuptodate %_csc_success ;

%_csc_success : %.grg $(GMS)
	@echo -e "-------------\nTesting $<...\n"
	@touch $(SUCCESSCSC)
	@touch $(FAILEDCSC)
	@test -d $(@:%_csc_success=%_csc_out) || mkdir $(@:%_csc_success=%_csc_out)
	-mono $(GRGENNET)/GrGen.exe -keep $(@:%_csc_success=%_csc_out) -o $(@:%_csc_success=%_csc_out) $< &> $@_tmp.log && cat $@_tmp.log && echo "$< succeeded" >> $(SUCCESSCSC) && touch $@ || (cat $@_tmp.log && grep "done\!" $@_tmp.log > /dev/null && echo "$< failed" >> $(FAILEDCSC) || echo "$< frontend failed" >> $(FAILEDCSCFE))
	@rm -f $@_tmp.log

clean:
	@rm -rf $(SUCCESSFILES_FE) $(DIRS_FE) $(SUCCESSFILES_CSC) $(DIRS_CSC) $(DIRS_CSI) $(SUCCESSFE) $(FAILEDFE) $(SUCCESSCSC) $(FAILEDCSC) $(FAILEDCSCFE)

cleanfelog:
	@rm -f $(FAILEDFE)

cleancsclog:
	@rm -f $(FAILEDCSC) $(FAILEDCSCFE)
