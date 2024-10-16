/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.IO;

using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public interface IDebuggerEnvironment
    {
        void Cancel();
        ConsoleKeyInfo ReadKeyWithCancel();
        object Askfor(String typeName, INamedGraph graph);
        GrGenType GetGraphElementType(String typeName);
        //void HandleSequenceParserException(SequenceParserException ex); is now a static method in DebuggerEnvironment
        string ShowGraphWith(String programName, String arguments, bool keep);
        IGraphElement GetElemByName(String elemName);
        void ShowMsgAskForEnter(string msg);
        bool ShowMsgAskForYesNo(string msg);
        string ShowMsgAskForString(string msg);

        // ConsoleUI -------------------------------------------------------------------------------

        // outWriter (errorOutWriter not used in interactive debugger)
        void Write(string value);
        void Write(string format, params object[] arg);
        void WriteLine(string value);
        void WriteLine(string format, params object[] arg);
        void WriteLine();

        // consoleOut
        void PrintHighlighted(String text, HighlightingMode mode);

        // inReader
        string ReadLine();

        // consoleIn
        ConsoleKeyInfo ReadKey(bool intercept);
        ConsoleKeyInfo ReadKeyWithControlCAsInput();
        bool KeyAvailable { get; }
    }

    public class EOFException : IOException
    {
    }

    public abstract class BaseDebuggerEnvironment : IDebuggerEnvironment
    {
        public ConsoleDebugger Debugger // has to be set after debugger was constructed
        {
            get { return debugger; }
            set { debugger = value; }
        }
        private ConsoleDebugger debugger;

        public virtual void Cancel()
        {
            debugger.AbortRewriteSequence();
            throw new OperationCanceledException();                 // abort rewrite sequence
        }

        public virtual ConsoleKeyInfo ReadKeyWithCancel()
        {
            ConsoleKeyInfo key = ReadKeyWithControlCAsInput();

            if(key.Key == ConsoleKey.C && (key.Modifiers & ConsoleModifiers.Control) != 0)
                Cancel();

            return key;
        }

        public object Askfor(String typeName, INamedGraph graph)
        {
            if(TypesHelper.GetGraphElementType(typeName, graph.Model) != null) // if type is node/edge type let the user select the element in yComp
            {
                if(!CheckDebuggerAlive())
                {
                    ErrorWriteLine("debug mode must be enabled (yComp available) for asking for a node/edge type");
                    return null;
                }

                WriteLine("Select an element of type " + typeName + " by double clicking in yComp (ESC for abort)...");

                String id = debugger.ChooseGraphElement();
                if(id == null)
                    return null;

                WriteLine("Received @(\"" + id + "\")");

                IGraphElement elem = graph.GetGraphElement(id);
                if(elem == null)
                {
                    ErrorWriteLine("Graph element does not exist (anymore?).");
                    return null;
                }
                if(!TypesHelper.IsSameOrSubtype(elem.Type.PackagePrefixedName, typeName, graph.Model))
                {
                    ErrorWriteLine(elem.Type.PackagePrefixedName + " is not the same type as/a subtype of " + typeName + ".");
                    return null;
                }
                return elem;
            }
            else // else let the user type in the value
            {
                String inputValue = ShowMsgAskForString("Enter a value of type " + typeName + ": ");
                StringReader reader = new StringReader(inputValue);
                ConstantParser shellForParsing = new ConstantParser(reader);
                shellForParsing.SetImpl(new ConstantParserHelper(debugger.DebuggerProcEnv));
                object val = shellForParsing.Constant();
                String valTypeName = TypesHelper.XgrsTypeOfConstant(val, graph.Model);
                if(!TypesHelper.IsSameOrSubtype(valTypeName, typeName, graph.Model))
                {
                    ErrorWriteLine(valTypeName + " is not the same type as/a subtype of " + typeName + ".");
                    return null;
                }
                return val;
            }
        }

        protected virtual bool CheckDebuggerAlive()
        {
            return debugger.GraphViewerClient.Sync();
        }

        public GrGenType GetGraphElementType(String typeName)
        {
            //if(!GraphExists())
            //    return null;
            GrGenType type = debugger.DebuggerProcEnv.ProcEnv.NamedGraph.Model.NodeModel.GetType(typeName);
            if(type != null)
                return type;
            type = debugger.DebuggerProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.GetType(typeName);
            if(type != null)
                return type;
            ErrorWriteLine("Unknown graph element type: \"{0}\"", typeName);
            return null;
        }

        public static void HandleSequenceParserException(SequenceParserException ex)
        {
            ConsoleUI.errorOutWriter.WriteLine(ex.Message);

            if(!(ex is SequenceParserExceptionCallParameterIssue))
                return;

            PrintPrototype((SequenceParserExceptionCallParameterIssue)ex);
        }

        private static void PrintPrototype(SequenceParserExceptionCallParameterIssue ex)
        {
            if(ex.Action != null)
            {
                IAction action = ex.Action;
                ConsoleUI.outWriter.Write("Prototype: {0}", ex.Action.RulePattern.PatternGraph.Name);
                if(action.RulePattern.Inputs.Length != 0)
                {
                    ConsoleUI.outWriter.Write("(");
                    for(int i = 0; i < action.RulePattern.Inputs.Length; ++i)
                    {
                        if(i > 0)
                            ConsoleUI.outWriter.Write(",");
                        GrGenType type = action.RulePattern.Inputs[i];
                        ConsoleUI.outWriter.Write("{0}:{1}", action.RulePattern.InputNames[i], type.PackagePrefixedName);
                    }
                    ConsoleUI.outWriter.Write(")");
                }
                if(action.RulePattern.Outputs.Length != 0)
                {
                    ConsoleUI.outWriter.Write(" : (");
                    bool first = true;
                    foreach(GrGenType type in action.RulePattern.Outputs)
                    {
                        if(first)
                            first = false;
                        else
                            ConsoleUI.outWriter.Write(",");
                        ConsoleUI.outWriter.Write("{0}", type.PackagePrefixedName);
                    }
                    ConsoleUI.outWriter.Write(")");
                }
            }
            else if(ex.Sequence != null)
            {
                ISequenceDefinition sequence = ex.Sequence;
                SequenceDefinition seqDef = (SequenceDefinition)sequence;
                ConsoleUI.outWriter.Write("Prototype: ");
                ConsoleUI.outWriter.Write(seqDef.Symbol);
            }
            else if(ex.Procedure != null)
            {
                IProcedureDefinition procedure = ex.Procedure;
                ConsoleUI.outWriter.Write("Prototype: {0}", ex.Procedure.Name);
                if(procedure.Inputs.Length != 0)
                {
                    ConsoleUI.outWriter.Write("(");
                    for(int i = 0; i < procedure.Inputs.Length; ++i)
                    {
                        if(i > 0)
                            ConsoleUI.outWriter.Write(",");
                        GrGenType type = procedure.Inputs[i];
                        ConsoleUI.outWriter.Write("{0}:{1}", procedure.InputNames[i], type.PackagePrefixedName);
                    }
                    ConsoleUI.outWriter.Write(")");
                }
                if(procedure.Outputs.Length != 0)
                {
                    ConsoleUI.outWriter.Write(" : (");
                    bool first = true;
                    foreach(GrGenType type in procedure.Outputs)
                    {
                        if(first)
                            first = false;
                        else
                            ConsoleUI.outWriter.Write(",");
                        ConsoleUI.outWriter.Write("{0}", type.PackagePrefixedName);
                    }
                    ConsoleUI.outWriter.Write(")");
                }
            }
            else if(ex.Function != null)
            {
                IFunctionDefinition function = ex.Function;
                ConsoleUI.outWriter.Write("Prototype: {0}", ex.Function.Name);
                if(function.Inputs.Length != 0)
                {
                    ConsoleUI.outWriter.Write("(");
                    for(int i = 0; i < function.Inputs.Length; ++i)
                    {
                        if(i > 0)
                            ConsoleUI.outWriter.Write(",");
                        GrGenType type = function.Inputs[i];
                        ConsoleUI.outWriter.Write("{0}:{1}", function.InputNames[i], type.PackagePrefixedName);
                    }
                    ConsoleUI.outWriter.Write(")");
                }
                ConsoleUI.outWriter.Write(" : {0}", function.Output.PackagePrefixedName);
            }
            ConsoleUI.outWriter.WriteLine();
        }

        public virtual string ShowGraphWith(String programName, String arguments, bool keep)
        {
            if(GraphViewer.IsDotExecutable(programName))
                return GraphViewer.ShowGraphWithDot(debugger.DebuggerProcEnv, programName, arguments, keep);
            else
                return GraphViewer.ShowVcgGraph(debugger.DebuggerProcEnv, debugger.debugLayout, programName, arguments, keep);
        }

        public IGraphElement GetElemByName(String elemName)
        {
            //if(!GraphExists())
            //    return null;
            IGraphElement elem = debugger.DebuggerProcEnv.ProcEnv.NamedGraph.GetGraphElement(elemName);
            if(elem == null)
            {
                ErrorWriteLine("Unknown graph element: \"{0}\"", elemName);
                return null;
            }
            return elem;
        }

        public string ReadOrEofErr()
        {
            string result = ReadLine();
            if(result == null)
                throw new EOFException();
            return result;
        }

        public void ShowMsgAskForEnter(string msg)
        {
            Write(msg + " [enter] ");
            ReadOrEofErr();
        }

        public bool ShowMsgAskForYesNo(string msg)
        {
            while(true)
            {
                Write(msg + " [y(es)/n(o)] ");
                string result = ReadOrEofErr();
                if(result.Equals("y", StringComparison.InvariantCultureIgnoreCase) ||
                    result.Equals("yes", StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
                else if(result.Equals("n", StringComparison.InvariantCultureIgnoreCase) ||
                    result.Equals("no", StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }
            }
        }

        public string ShowMsgAskForString(string msg)
        {
            Write(msg);
            return ReadOrEofErr();
        }

        // ConsoleUI -------------------------------------------------------------------------------

        // outWriter
        public abstract void Write(string value);
        public abstract void Write(string format, params object[] arg);
        public abstract void WriteLine(string value);
        public abstract void WriteLine(string format, params object[] arg);
        public abstract void WriteLine();

        // errorOutWriter (not used in the interactive debugger, but some methods from the environment are also called from the shell (shared code))
        public abstract void ErrorWrite(string value);
        public abstract void ErrorWrite(string format, params object[] arg);
        public abstract void ErrorWriteLine(string value);
        public abstract void ErrorWriteLine(string format, params object[] arg);
        public abstract void ErrorWriteLine();
        
        // consoleOut
        public abstract void PrintHighlighted(String text, HighlightingMode mode);
        
        // inReader
        public abstract string ReadLine();
        
        // consoleIn
        public abstract ConsoleKeyInfo ReadKey(bool intercept);
        public abstract ConsoleKeyInfo ReadKeyWithControlCAsInput();
        public abstract bool KeyAvailable { get; }
    }

    public class DebuggerEnvironment : BaseDebuggerEnvironment
    {
        public override void Write(string value)
        {
            ConsoleUI.outWriter.Write(value);
        }

        public override void Write(string format, params object[] arg)
        {
            ConsoleUI.outWriter.Write(format, arg);
        }

        public override void WriteLine(string value)
        {
            ConsoleUI.outWriter.WriteLine(value);
        }

        public override void WriteLine(string format, params object[] arg)
        {
            ConsoleUI.outWriter.WriteLine(format, arg);
        }

        public override void WriteLine()
        {
            ConsoleUI.outWriter.WriteLine();
        }

        public override void ErrorWrite(string value)
        {
            ConsoleUI.errorOutWriter.Write(value);
        }

        public override void ErrorWrite(string format, params object[] arg)
        {
            ConsoleUI.errorOutWriter.Write(format, arg);
        }

        public override void ErrorWriteLine(string value)
        {
            ConsoleUI.errorOutWriter.WriteLine(value);
        }

        public override void ErrorWriteLine(string format, params object[] arg)
        {
            ConsoleUI.errorOutWriter.WriteLine(format, arg);
        }

        public override void ErrorWriteLine()
        {
            ConsoleUI.errorOutWriter.WriteLine();
        }

        public override void PrintHighlighted(String text, HighlightingMode mode)
        {
            ConsoleUI.consoleOut.PrintHighlighted(text, mode);
        }

        public override string ReadLine()
        {
            return ConsoleUI.inReader.ReadLine();
        }

        public override ConsoleKeyInfo ReadKey(bool intercept)
        {
            return ConsoleUI.consoleIn.ReadKey(intercept);
        }

        public override ConsoleKeyInfo ReadKeyWithControlCAsInput()
        {
            return ConsoleUI.consoleIn.ReadKeyWithControlCAsInput();
        }

        public override bool KeyAvailable
        {
            get { return ConsoleUI.consoleIn.KeyAvailable; }
        }
    }
}
