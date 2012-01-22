/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

//#define LOG_VARIABLE_OPERATIONS

using System;
using System.Collections;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A variable declared/used within a sequence,
    /// might be a sequence-local variable or a reference to a graph-global variable.
    /// It is first stored within the symbol table belonging to the sequence on sequence parsing,
    /// after parsing only on the heap, with references from the sequence AST pointing to it.
    /// </summary>
    public class SequenceVariable
    {
        public SequenceVariable(String name, String prefix, String type)
        {
            this.name = name;
            this.prefix = prefix;
            this.type = type;
            value = null;
            visited = false;
        }

        public SequenceVariable Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            // ensure that every instance of a variable is mapped to the same copy
            if(originalToCopy.ContainsKey(this))
                return originalToCopy[this];

            // local variables must be cloned when a defined sequence gets copied
            // global variables stay the same
            if(Type == "")
            {
                originalToCopy.Add(this, this);
                return this;
            }
            else
            {
                originalToCopy.Add(this, new SequenceVariable(name, prefix, type));
#if LOG_VARIABLE_OPERATIONS
                procEnv.Recorder.Write(name + " = " + name + "; " + name + "==" + DictionaryListHelper.ToStringAutomatic(value, procEnv.Graph) + "\n");
#endif
                return originalToCopy[this];
            }
        }

        // the pure name of the variable, used in code generation
        public String PureName { get { return name; } }
        // the name of the variable, for debug printing to the user (globals prefixed with :: in contrast to pure name)
        public String Name { get { return type=="" ? "::"+name : name; } }
        // the nesting prefix of the variable
        public String Prefix { get { return prefix; } }
        // the type of the variable (a graph-global variable maps to "", a sequence-local to its type)
        public String Type { get { return type; } }

        // the variable value if the xgrs gets interpreted/executed
        // TODO: cast the value to the declared type on write, error check throwing exception
        // TODO: sequence can be used multiple times: sequence re-initialization is needed
        // davor? danach? dazwischen? dazwischen am besten, aber muss von hand gemacht werden.
        // davor/danach könnten automatisch vor/nach Apply laufen
        public object Value { get { return value; } set { this.value = value; } }

        // gets the variable value, decides whether to query the graph-global or the sequence-lokal variables
        public object GetVariableValue(IGraphProcessingEnvironment procEnv)
        {
            if(Type == "") {
                return procEnv.GetVariableValue(name);
            } else {
#if LOG_VARIABLE_OPERATIONS
                procEnv.Recorder.Write(name + "==" + DictionaryListHelper.ToStringAutomatic(value, procEnv.Graph) + "\n");
#endif
                return value;
            }
        }

        // sets the variable value, decides whether to update the graph-global or the sequence-lokal variables
        public void SetVariableValue(object value, IGraphProcessingEnvironment procEnv)
        {
            if(Type == "") {
                procEnv.SetVariableValue(name, value);
            } else {
#if LOG_VARIABLE_OPERATIONS
                procEnv.Recorder.Write(name + " = " + DictionaryListHelper.ToStringAutomatic(value, procEnv.Graph) + "\n");
#endif
                this.value = value;
            }
        }

        // add ourselves to the variables set if we are a local variable
        public void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            if(Type != "" && !variables.ContainsKey(this))
                variables.Add(this, null);
        }

        // visited flag used in xgrs code generation
        public bool Visited { get { return visited; } set { this.visited = value; } }


        private String name;
        private String prefix;
        private String type;

        private object value;

        private bool visited;
    }
}
