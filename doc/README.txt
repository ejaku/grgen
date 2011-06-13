GrGen.NET v2.6 (2010-09-05)
---------------------------

This is the GrGen.NET system for graph rewriting.
It consists of two parts of components:
- the Graph Rewrite Generator GrGen, which turns declarative rewrite rule
  specifications into efficient .NET assemblies performing the rewrites,
  which are supported by the runtime environment LibGr.
- the rapid prototyping environment offered by GrShell and yComp

INSTALL
-------

You need the following system setup for GrGen.NET:
  - Microsoft .NET 2.0 or above
    OR Mono 1.2.5 or above
  - Java 1.5 or above

For Linux:
  - Unpack GrGenNET.tar.bz2 to a folder of your choice (referred to
    as <GrGenNETPath> in the following)
    Example (extracts files into GrGen.NET in your current directory):

      tar -xjf GrGenNET.tar.bz2

  - Add the <GrGenNETPath>/bin directory to your search path,
    otherwise you must specify it together with the executable when running mono

For Windows:
  - Unpack GrGenNET.zip to a folder of your choice (referred to as
    <GrGenNETPath> in the following)

  - Either set your computer path environment to include <GrGenNETPath>\bin
    or enter the following command in the command prompt:

      set PATH=<GrGenNETPath>\bin;%PATH%

    Of course you have to replace <GrGenNETPath> by your chosen path.

    ATTENTION: Do NOT add ""s around any part of the path, even if it
      contains spaces! Otherwise yComp, a graph visualisation tool, will not
      run out of GrShell!


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

The tests subdirectory contains an automated testbench used to check the
consistency of our GrGen.NET releases. You can run the testbench by executing
the "test.sh" shell script (for Windows you must use Cygwin).
The source distribution contains further frontend/generation only tests,
regarding exection the same applies as for the backend/shell execution tests.


HELP
----

For further information refer to the "GrGen.NET User Manual" available
at http://www.grgen.net.

If you find any bugs or have a suggestion, please send an email to:
    grgen@ipd.info.uni-karlsruhe.de


AUTHORS
-------

Veit Batz
Michael Beck
Jakob Blomer
Sebastian Buchwald
Rubino Geiss
Daniel Grund
Sebastian Hack
Enno Hofmann
Edgar Jakumeit
Moritz Kroll
Christoph Mallon
Jens Mueller
Andreas Schoesser
Adam Szalkowski
Nicholas Tung


COPYING
-------

Copyright (C) 2003-20010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos

This file is part of GrGen, Version GrGen.NET 2.6

GrGen is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

GrGen is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with GrGen.  If not, see <http://www.gnu.org/licenses/>.

