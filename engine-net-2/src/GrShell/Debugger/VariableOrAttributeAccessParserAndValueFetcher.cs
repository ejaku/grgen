/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;
using System.Text;

namespace de.unika.ipd.grGen.grShell
{
    class VariableOrAttributeAccessParserAndValueFetcher
    {
        readonly IDebuggerEnvironment env;
        readonly ShellGraphProcessingEnvironment shellProcEnv;

        readonly Stack<SequenceBase> debugSequences;

        public VariableOrAttributeAccessParserAndValueFetcher(IDebuggerEnvironment env,
            ShellGraphProcessingEnvironment shellProcEnv,
            Stack<SequenceBase> debugSequences)
        {
            this.env = env;
            this.shellProcEnv = shellProcEnv;
            this.debugSequences = debugSequences;
        }

        public bool FetchObjectToBeShownAsGraph(SequenceBase seq, out object toBeShownAsGraph, out AttributeType attrType)
        {
            do
            {
                Console.WriteLine("Enter name of variable or attribute access to show as graph (just enter for abort): ");
                Console.WriteLine("Examples: \"v\", \"v.a\", \"@(\"$0\").a\" ");
                String str = Console.ReadLine();
                if(str.Length == 0)
                {
                    toBeShownAsGraph = null;
                    attrType = null;
                    return true;
                }

                if(str.StartsWith("@"))
                {
                    // graph element by name
                    string attributeName;
                    IGraphElement elem = ParseAccessByName(str, out attributeName);
                    if(elem == null)
                    {
                        Console.WriteLine("Can't parse graph access / unknown graph element: " + str);
                        continue;
                    }
                    if(attributeName == null)
                    {
                        Console.WriteLine("The result of a graph access is a node or edge, you must access an attribute: " + str);
                        continue;
                    }
                    attrType = elem.Type.GetAttributeType(attributeName);
                    if(attrType == null)
                    {
                        Console.WriteLine("Unknown attribute: " + attributeName);
                        continue;
                    }
                    object attribute = elem.GetAttribute(attributeName);
                    if(attribute == null)
                    {
                        Console.WriteLine("Null-valued attribute: " + attributeName);
                        continue;
                    }

                    toBeShownAsGraph = attribute;
                    return false;
                }
                else
                {
                    // variable
                    string attributeName;
                    object value = ParseVariable(str, seq, out attributeName);
                    if(value == null)
                    {
                        Console.WriteLine("Can't parse variable / unknown variable / null-valued variable: " + str);
                        continue;
                    }

                    if(attributeName != null)
                    {
                        if(!(value is IGraphElement))
                        {
                            Console.WriteLine("Can't access attribute, the variable value is not a graph element: " + str);
                            continue;
                        }
                        IGraphElement elem = (IGraphElement)value;
                        attrType = elem.Type.GetAttributeType(attributeName);
                        if(attrType == null)
                        {
                            Console.WriteLine("Unknown attribute: " + attributeName);
                            continue;
                        }
                        object attribute = elem.GetAttribute(attributeName);
                        if(attribute == null)
                        {
                            Console.WriteLine("Null-valued attribute: " + attributeName);
                            continue;
                        }

                        toBeShownAsGraph = attribute;
                        return false;
                    }
                    else
                    {
                        attrType = null;
                        toBeShownAsGraph = value;
                        return false;
                    }
                }
            }
            while(true);
        }

        private IGraphElement ParseAccessByName(string str, out string attribute)
        {
            attribute = null;

            int pos = 0;
            if(str[pos++] != '@')
                return null;
            if(str[pos++] != '(')
                return null;
            if(str[pos++] != '"')
                return null;
            StringBuilder sb = new StringBuilder();
            while(str[pos] != '"')
            {
                sb.Append(str[pos++]);
            }
            if(str[pos++] != '"')
                return null;
            if(str[pos++] != ')')
                return null;
            if(pos == str.Length)
                return env.GetElemByName(sb.ToString());
            if(str[pos++] != '.')
                return null;
            attribute = str.Substring(pos);
            return env.GetElemByName(sb.ToString());
        }

        private object ParseVariable(string str, SequenceBase seq, out string attribute)
        {
            string varName;
            if(str.Contains("."))
            {
                varName = str.Substring(0, str.LastIndexOf('.'));
                attribute = str.Substring(str.LastIndexOf('.') + 1);
            }
            else
            {
                varName = str;
                attribute = null;
            }

            Dictionary<SequenceVariable, SetValueType> seqVars = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            (debugSequences.Peek()).GetLocalVariables(seqVars, constructors, seq);
            foreach(SequenceVariable var in seqVars.Keys)
            {
                if(var.Name == varName)
                    return var.Value;
            }
            foreach(Variable var in shellProcEnv.ProcEnv.Variables)
            {
                if(var.Name == varName)
                    return var.Value;
            }
            return null;
        }
    }
}
