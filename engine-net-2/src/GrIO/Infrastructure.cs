/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using System.IO;

namespace grIO
{
    public class Infrastructure
    {
        public static void Flush(IGraph g)
        {
            FileIO fio = new FileIO(g);
            fio.FlushFiles();
        }

        public static void MsgToConsole(string msg)
        {
            Console.WriteLine(msg);
        }

        public const string GraphModelDefinition =
@"node class grIO_OUTPUT {
    timestamp : int;
}

node class grIO_File {
    path : string;
}

edge class grIO_CreateOrAppend
    connect grIO_OUTPUT [0:1] -> grIO_File [0:*];

edge class grIO_CreateOrOverwrite
    connect grIO_OUTPUT [0:1] -> grIO_File [0:*];

node class grIO_File_Line {
    content : string;
}

edge class grIO_File_ContainsLine
    connect grIO_File [0:1] -> grIO_File_Line [0:*];

edge class grIO_File_NextLine
    connect grIO_File_Line [0:1] -> grIO_File_Line [0:1];
";
    }
}
