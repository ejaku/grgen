GrGen.NET v8.0 (2025-12-24)
---------------------------

This is the GrGen.NET system for graph rewriting.
It consists of two parts of components:
- the Graph Rewrite Generator GrGen, which turns 
  declarative rewrite rule specifications into efficient .NET assemblies
  carrying out the pattern matching and performing the rewrites;
  they are supported by the runtime environment implemented by the
  libGr and the lgspBackend dlls (plus some helper dlls).
- the rapid prototyping environment offered by the (G)GrShell with the 
  console or the GUI debugger, and the yComp or MSAGL graph viewers
  (MSAGL and the debugger can be also used from your own .NET application;
  note that graphs may also be auto-persisted to a database
  or exported to/imported from line-based text files)


INSTALL
-------

You need the following system setup for GrGen.NET:
  - Microsoft .NET Framework 4.7.2 or above
    OR Mono 5.10.0 or above
  - Java 1.8 (RE) or above (during development of your graph rewrite system)

For Linux:
  - Unpack GrGenNET.tar.bz2 to a folder of your choice (referred to
    as <GrGenNETPath> in the following)
    Example (extracts files into GrGen.NET in your current directory):

      tar -xjf GrGenNET.tar.bz2

  - Add the <GrGenNETPath>/bin directory to your search path,
    otherwise you must specify it together with the executable when running mono

    ( by adding in .profile or .bashrc: export PATH=<GrGenNETPath>/bin:$PATH )

For Windows:
  - Unpack GrGenNET.zip to a folder of your choice (referred to as
    <GrGenNETPath> in the following)

  - Either set your computer path environment to include <GrGenNETPath>\bin
    or enter the following command in the command prompt:

      set PATH=<GrGenNETPath>\bin;%PATH%

    Of course you have to replace <GrGenNETPath> by your chosen path.

    ATTENTION: Do NOT add ""s around any part of the path, even if it
      contains spaces! Otherwise yComp, our graph visualisation tool, will not
      run out of GrShell!

    If you run into "Unable to process specification: 
    The system cannot find the file specified" errors, 
    you may need to install a JDK to a non system path
    and add the bin folder of the JDK to the path variable.
    (Normally just installing a JRE is sufficient.)
    Or you may need to remove a wrong java folder from the system path.


RUN
---

To run an example change into the appropriate example directory and execute
the GrShell with a .grs-file. Libraries are automatically generated from
the rule (.grg) and model (.gm) specification files if the
"new graph <grg-file>" command is used and any of these files changed or
the libraries do not exist, yet.

Example:
  - cd into <GrGenNETPath>/examples/Mutex
  - .NET: Run "GrShell Mutex10.grs"
    Mono: Run "mono <GrGenNETPath>/bin/GrShell.exe Mutex10.grs"

If you just need the libraries execute GrGen with a .grg-file.
Example:
  - .NET: Run "GrGen <GrGenNETPath>/examples/Mutex/Mutex.grg"
    Mono: Run "mono <GrGenNETPath>/bin/GrGen.exe
               <GrGenNETPath>/examples/Mutex/Mutex.grg"

You can also generate source code, which you can include into your C#
projects directly. Call GrGen with the -keep option, which saves the
generated source code in a subfolder "tmpgrgen<number>".
Example:
  - .NET: Run "GrGen -keep <GrGenNETPath>/examples/Mutex/Mutex.grg"
    Mono: Run "mono <GrGenNETPath>/bin/GrGen.exe -keep
               <GrGenNETPath>/examples/Mutex/Mutex.grg"


TESTS
-----

The consistency of GrGen.NET releases is checked with an automated testbench.
In the tests directory do you find the semantic/execution tests,
in the test directory do you find the syntax/compiler tests,
and in the examples directory do you find the examples that are also used as additional smoke tests.
You can run the testbench by executing the "test.sh" shell scripts (for Windows you must use Cygwin).


HELP
----

For further information refer to the "GrGen.NET User Manual" contained in this package,
and also available from https://www.grgen.de (or http://www.grgen.net).

If you find any bugs or have a suggestion, please send an email to:
    grgen@grgen.de


MAIN AUTHORS FROM IPD
---------------------

Veit Batz
Jakob Blomer
Sebastian Buchwald
Rubino Geiß
Daniel Grund
Sebastian Hack
Edgar Jakumeit
Moritz Kroll
Adam Szalkowski


FURTHER AUTHORS FROM IPD
------------------------

Michael Beck
Christoph Mallon
Jens Müller
Andreas Schösser
Andreas Zwinkau


FURTHER EXTERNAL CODE/BUGFIX CONTRIBUTIONS
------------------------------------------

Paolo Bonzini
Philip Daubmeier
Peter Grüner
Philip Lorenz
Carl Mai
Julian Reich
Nicholas Tung
Maneesh Yadav


BUGREPORTS AND OTHER CONTRIBUTIONS
----------------------------------

Nadeem Ahmed
Bill Alexander
Serge Autexier
Paul Bedaride
Maxim Bendiks
Paul Buschmann
Catherine Cocu
Bugra Derre
Dominik Dietrich
Gerhard Goos
Pieter van Gorp
Thomas Grasl
Enno Hofmann
Tassilo Horn
Yuan Jochen Kang
Mathias Landhäußer
Wei Le
Robert C. Metzger
Normen Müller
Clemens Münzer
Laura Perez
J-S-Robinson
Philipp Rosenberger
Caroline von Totth


COPYING
-------

Copyright (C) 2003-2025 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation (IPD), LS Goos; and free programmers

For most of GrGen.NET, the LGPL holds, 
for yComp, an academic license granted by yWorks holds,
for the MSAGL component, the MIT license holds,
the SQLite used by the persistent graph is in the public domain;
you find more on this topic in the LICENSE.txt and the licenses folder.
