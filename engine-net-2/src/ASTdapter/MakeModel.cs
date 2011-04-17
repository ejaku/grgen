/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace ASTdapter
{
    class GraphModelGenerator
    {
        private static void TryWrite(String filename, String text)
        {
            bool writeFile = true;
            if(File.Exists(filename))
            {
                Console.Write(filename + " exists! Overwrite? (y/n) ");
                String str;
                do
                {
                    str = Console.ReadLine();
                }
                while(!str.Equals("y", StringComparison.OrdinalIgnoreCase)
                    && !str.Equals("n", StringComparison.OrdinalIgnoreCase));
                writeFile = str.Equals("y", StringComparison.OrdinalIgnoreCase);
            }
            if(writeFile)
            {
                using(StreamWriter writer = new StreamWriter(filename))
                    writer.Write(text);
            }
        }

        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Need one Parameter: The file name of the assembly (usually .dll file) containing the parser classes.");
                return;
            }
            string parserdllname = args[0];
            string result = GenerateFrom(parserdllname);
            string filenameModel = GetBaseName(parserdllname) + "Model.gm";
            TryWrite(filenameModel, result);

            string grg = GenerateGrammarTemplate(parserdllname);
            string filenameGRG = GetBaseName(parserdllname) + ".grg";
            TryWrite(filenameGRG, grg);
        }

        private static string GenerateGrammarTemplate(string parserdllname)
        {
            StringBuilder sb = new StringBuilder();
            string modelname = GetBaseName(parserdllname) + "model";
            sb.AppendLine("using " + modelname + ";");
            sb.AppendLine("/* at least one rule needs to be defined */");
            sb.AppendLine("rule dummy { pattern {} replace {}}");
            return sb.ToString();
        }

        private static string GetBaseName(string parserdllname)
        {
            string s;
            int index = parserdllname.LastIndexOfAny(new char[] { '\\', '/' }) + 1;
            if (parserdllname.EndsWith(".dll", StringComparison.OrdinalIgnoreCase)
                || parserdllname.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
            {
                s = parserdllname.Substring(index, parserdllname.Length - index - 4);
            }
            else
            {
                s = parserdllname.Substring(index);
            }
            return s + "AST";
        }

        public static string GenerateFrom(string parserdllname)
        {
            StringBuilder result = new StringBuilder();

            Type tokentypesclass = GetTokenTypeClass(parserdllname);

            result.AppendLine("node class ASTNode {");
            result.AppendLine("\tvalue:string;");
            result.AppendLine("\tlineNr:int;");
            result.AppendLine("\tcolumn:int;");
            result.AppendLine("\tline:string;");
            result.AppendLine("}");
            result.AppendLine();
            result.AppendLine("edge class child");
            result.AppendLine("\tconnect ASTNode[0:*] -> ASTNode[0:1];");
            result.AppendLine();
            result.AppendLine("edge class next");
            result.AppendLine("\tconnect ASTNode[0:1] -> ASTNode[0:1];");
            result.AppendLine();
            foreach (FieldInfo fi in tokentypesclass.GetFields())
            {
                string name = fi.Name;
                result.AppendLine("node class " + name + " extends ASTNode;");
            }
            return result.ToString();
        }

        private static Type GetTokenTypeClass(String assemblyName)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyName);
            Type tokentypesclass = null;
            foreach (Type type in assembly.GetTypes())
            {
                if (!type.IsClass || type.IsNotPublic) continue;
                if (!type.Name.EndsWith("TokenTypes")) continue;
                tokentypesclass = type;
            }
            return tokentypesclass;
        }

    }
}
