/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace ChangeFileHeaders
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 3)
                throw new Exception("At least 3 parameters expected, the file to process, the line to insert the pragma warning disable, and at least one warning code to disable");

            String file = args[0];
            int line = int.Parse(args[1]);
            FileInfo dir = new FileInfo(file);
            List<string> warningsToDisable = new List<string>();
            for(int i = 2; i < args.Length; ++i)
            {
                warningsToDisable.Add(args[i]);
            }

            ProcessFile(dir, line, warningsToDisable);
        }

        private static void ProcessFile(FileInfo file, int line, IList<string> warningsToDisable)
        {
            Encoding encoding = Encoding.Default; //Encoding.UTF8;

            StringBuilder sb = new StringBuilder();
            sb.Append("#pragma warning disable ");
            bool first = true;
            foreach(string warningToDisable in warningsToDisable)
            {
                if(first)
                    first = false;
                else
                    sb.Append(", ");
                sb.Append(warningToDisable);
            }

            List<string> lines = new List<string>(File.ReadAllLines(file.FullName, encoding));

            lines.Insert(line, sb.ToString());
 
            File.WriteAllLines(file.FullName, lines.ToArray(), encoding);

            Console.WriteLine("Added " + sb.ToString() + " to " + file.FullName);
        }
    }
}
