/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    // ConsoleUI for user dialog
    public interface IDebuggerConsoleUI
    {
        // outWriter (errorOutWriter not used in interactive debugger)
        void Write(string value);
        void Write(string format, params object[] arg);
        void WriteLine(string value);
        void WriteLine(string format, params object[] arg);
        void WriteLine();

        // consoleOut for user dialog
        void PrintHighlightedUserDialog(String text, HighlightingMode mode);

        // inReader
        string ReadLine();

        // consoleIn
        ConsoleKeyInfo ReadKey(bool intercept);
        ConsoleKeyInfo ReadKeyWithControlCAsInput();
        bool KeyAvailable { get; }
    }

    public interface IDebuggerUIForDataRendering
    {
        // base version of outWriter for data rendering, TODO: also implemented by the graph GUI version, resulting in a list of lines
        void WriteLineDataRendering(string value);
        void WriteLineDataRendering(string format, params object[] arg);
        void WriteLineDataRendering();

        void Clear();

        // flicker prevention/performance optimization
        void SuspendImmediateExecution();
        void RestartImmediateExecution();
    }

    // ConsoleUI for data rendering (of the main work object)
    public interface IDebuggerConsoleUIForDataRendering : IDebuggerUIForDataRendering
    {
        // outWriter for data rendering (errorOutWriter not used in interactive debugger and even less in data rendering)
        void WriteDataRendering(string value);
        void WriteDataRendering(string format, params object[] arg);
        //void WriteLineDataRendering(string value);
        //void WriteLineDataRendering(string format, params object[] arg);
        //void WriteLineDataRendering();

        // consoleOut
        void PrintHighlighted(String text, HighlightingMode mode);

        //void Clear();

        // inReader and consoleIn are not used in data rendering

        // flicker prevention/performance optimization
        //void SuspendImmediateExecution();
        //void RestartImmediateExecution();
    }

    // interface extension of the GUI debugger compared to the two pane mode debugger consoles / GUI for data rendering (of the main work object as graph)
    public interface IDebuggerGUIForDataRendering : IDebuggerUIForDataRendering
    {
        void SetContext(UserChoiceMenu userChoiceMenu, UserChoiceMenu additionalGuiUserChoiceMenu); // GUI TODO: move out to the IDebuggerEnvironment(?) (and the IGuiDebuggerHost(?))

        IBasicGraphViewerClient graphViewer { get; } // for graphical rendering of the sequence AST better ASG; GUI TODO: maybe slighthly reduced interface for internal use in DLL form only

        // GUI TODO: maybe also some list/table interface for non-textual display
    }

    public interface IDebuggerConsoleUICombined : IDebuggerConsoleUI, IDebuggerConsoleUIForDataRendering
    {
    }

    public class DebuggerConsoleUI : IDebuggerConsoleUICombined
    {
        public static DebuggerConsoleUI Instance
        {
            get
            {
                if(instance == null)
                    instance = new DebuggerConsoleUI();
                return instance;
            }
        }
        static DebuggerConsoleUI instance;

        public bool EnableClear
        {
            get { return enableClear; }
            set { enableClear = value; }
        }
        bool enableClear = false;

        private DebuggerConsoleUI()
        {
        }

        public void Write(string value)
        {
            ConsoleUI.outWriter.Write(value);
        }

        public void Write(string format, params object[] arg)
        {
            ConsoleUI.outWriter.Write(format, arg);
        }

        public void WriteLine(string value)
        {
            ConsoleUI.outWriter.WriteLine(value);
        }

        public void WriteLine(string format, params object[] arg)
        {
            ConsoleUI.outWriter.WriteLine(format, arg);
        }

        public void WriteLine()
        {
            ConsoleUI.outWriter.WriteLine();
        }

        public void ErrorWrite(string value)
        {
            ConsoleUI.errorOutWriter.Write(value);
        }

        public void ErrorWrite(string format, params object[] arg)
        {
            ConsoleUI.errorOutWriter.Write(format, arg);
        }

        public void ErrorWriteLine(string value)
        {
            ConsoleUI.errorOutWriter.WriteLine(value);
        }

        public void ErrorWriteLine(string format, params object[] arg)
        {
            ConsoleUI.errorOutWriter.WriteLine(format, arg);
        }

        public void ErrorWriteLine()
        {
            ConsoleUI.errorOutWriter.WriteLine();
        }

        public void PrintHighlightedUserDialog(String text, HighlightingMode mode)
        {
            ConsoleUI.consoleOut.PrintHighlighted(text, mode);
        }

        public string ReadLine()
        {
            return ConsoleUI.inReader.ReadLine();
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return ConsoleUI.consoleIn.ReadKey(intercept);
        }

        public ConsoleKeyInfo ReadKeyWithControlCAsInput()
        {
            return ConsoleUI.consoleIn.ReadKeyWithControlCAsInput();
        }

        public bool KeyAvailable
        {
            get { return ConsoleUI.consoleIn.KeyAvailable; }
        }

        // -----------------------------------------------------------------

        public void WriteDataRendering(string value)
        {
            ConsoleUI.outWriter.Write(value);
        }

        public void WriteDataRendering(string format, params object[] arg)
        {
            ConsoleUI.outWriter.Write(format, arg);
        }

        public void WriteLineDataRendering(string value)
        {
            ConsoleUI.outWriter.WriteLine(value);
        }

        public void WriteLineDataRendering(string format, params object[] arg)
        {
            ConsoleUI.outWriter.WriteLine(format, arg);
        }

        public void WriteLineDataRendering()
        {
            ConsoleUI.outWriter.WriteLine();
        }

        public void PrintHighlighted(String text, HighlightingMode mode)
        {
            ConsoleUI.consoleOut.PrintHighlighted(text, mode);
        }

        public void Clear()
        {
            if(enableClear)
                ConsoleUI.consoleOut.Clear();
        }

        public void SuspendImmediateExecution()
        {
            // nop
        }

        public void RestartImmediateExecution()
        {
            // nop
        }
    }

    public interface IDebuggerEnvironment : IDebuggerConsoleUI, IDebuggerConsoleUIForDataRendering
    {
        ConsoleKeyInfo ReadKeyWithCancel();

        GrGenType GetGraphElementType(String typeName);
        IGraphElement GetElemByName(String elemName);

        object Askfor(String typeName, INamedGraph graph);

        void PauseUntilEnterPressed(string msg);
        void PauseUntilAnyKeyPressedToResumeDebugging(string msg);
        void PauseUntilAnyKeyPressedToContinueDialog(string msg);
        bool ShowMsgAskForYesNo(string msg);
        int ShowMsgAskForIntegerNumber(string msg);
        int ShowMsgAskForIntegerNumber(string msg, int defaultOnEmptyInput);
        double ShowMsgAskForFloatingPointNumber(string msg);
        double ShowMsgAskForFloatingPointNumber(string msg, double defaultOnEmptyInput);
        string ShowMsgAskForString(string msg);

        // lets the user press a key, searches the options from the choiceMenu for the key character in parenthesis, if one is found it is returned
        // if a key was pressed that is not available in the list of keys, an error message with the available options is printed,
        // and the user choice is repeated, unless the (any key) choice is admissible (in this case ' ' is returned)
        // the additional GUI choice menu is not printed to the user, but allows for additional user input, it is intended for extra options in the gui debugger
        ConsoleKeyInfo LetUserChoose(UserChoiceMenu choiceMenu, UserChoiceMenu additionalGuiChoiceMenu);
        char LetUserChoose(UserChoiceMenu choiceMenu);

        // prints the available menu options to the user, separated by comma, prints the prefix before the instructions and the suffix after the instructions
        void PrintInstructions(UserChoiceMenu choiceMenu, string prefix, string suffix);
        // prints the available options to the user, separated by newline, prints the prefix before the instructions and the suffix after the instructions
        void PrintInstructionsSeparateByNewline(UserChoiceMenu choiceMenu, string prefix, string suffix);

        // -----------------------------------------------------------------

        string ShowGraphWith(String programName, String arguments, bool keep);
        void Cancel();
        //void HandleSequenceParserException(SequenceParserException ex); is now a static method in DebuggerEnvironment

        bool TwoPane { get; }
        bool Gui { get; }

        IDebuggerGUIForDataRendering guiForDataRendering { get; } // TODO: maybe better place existing
    }


    public class EOFException : IOException
    {
    }

    public class DebuggerEnvironment : IDebuggerEnvironment
    {
        public DebuggerEnvironment(IDebuggerConsoleUI debuggerConsoleUI, IDebuggerConsoleUIForDataRendering debuggerConsoleUIForDataRendering, IDebuggerGUIForDataRendering debuggerGUIForDataRendering)
        {
            this.theDebuggerConsoleUI = debuggerConsoleUI;
            this.theDebuggerConsoleUIForDataRendering = debuggerConsoleUIForDataRendering;
            this.theDebuggerGUIForDataRendering = debuggerGUIForDataRendering;
        }

        public IDebuggerConsoleUI TheDebuggerConsoleUI // debugger console UI operations are delegated to this object, can be switched in between a gui console or a console
        {
            get { return theDebuggerConsoleUI; }
            set { theDebuggerConsoleUI = value; }
        }
        private IDebuggerConsoleUI theDebuggerConsoleUI;

        public IDebuggerConsoleUIForDataRendering TheDebuggerConsoleUIForDataRendering // debugger console UI operations for data rendering are delegated to this object, can be switched in between a gui console or a console (in case of a console it is typically mapped to the normal debugger console)
        {
            get { return theDebuggerConsoleUIForDataRendering; }
            set { theDebuggerConsoleUIForDataRendering = value; }
        }
        private IDebuggerConsoleUIForDataRendering theDebuggerConsoleUIForDataRendering;

        public IDebuggerGUIForDataRendering TheDebuggerGUIForDataRendering // debugger GUI operations for data rendering are delegated to this object
        {
            get { return theDebuggerGUIForDataRendering; }
            set { theDebuggerGUIForDataRendering = value; }
        }
        private IDebuggerGUIForDataRendering theDebuggerGUIForDataRendering;

        public IDebuggerGUIForDataRendering guiForDataRendering
        {
            get { return theDebuggerGUIForDataRendering; }
        }

        public Debugger Debugger // has to be set after debugger was constructed
        {
            get { return debugger; }
            set { debugger = value; }
        }
        protected Debugger debugger;

        private UserChoiceMenu choiceMenuContinueAnyKey = new UserChoiceMenu(UserChoiceMenuNames.PauseContinueMenu, new string[] { "commandContinueAnyKey" });
        private UserChoiceMenu choiceMenuEnterLineCancel = new UserChoiceMenu(UserChoiceMenuNames.EnterLineCancel, new string[] { "enterLineCancel" });

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
                String inputValue = ShowMsgAskForString("Enter a value of type " + typeName);
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

        // TODO: no idea what this is good for or what's the best way to cope with it, under Mono-LINUX it throws, maybe some issue in the MonoWorkaroundConsoleTextReader
        public string ReadOrEofErr()
        {
            string result = ReadLine();
            if(result == null)
                throw new EOFException();
            return result;
        }

        public void PauseUntilEnterPressed(string msg)
        {
            Write(msg);
            ReadOrEofErr();
        }

        public void PauseUntilAnyKeyPressedToResumeDebugging(string msg)
        {
            WriteLine(msg);

            if(theDebuggerGUIForDataRendering != null)
                theDebuggerGUIForDataRendering.SetContext(choiceMenuContinueAnyKey, null);

            ReadKey(true);
        }

        public void PauseUntilAnyKeyPressedToContinueDialog(string msg)
        {
            WriteLine(msg);

            if(theDebuggerGUIForDataRendering != null)
                theDebuggerGUIForDataRendering.SetContext(choiceMenuContinueAnyKey, null);

            ReadKey(true);
        }

        public bool ShowMsgAskForYesNo(string msg)
        {
            Write(msg + " Enter (y)es or (n)o: ");
            while(true)
            {
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
                Write("You must enter (y)es or (n)o: ");
            }
        }

        public int ShowMsgAskForIntegerNumber(string msg)
        {
            Write(msg + ": ");
            while(true)
            {
                String numStr = ReadOrEofErr();
                int num;
                if(int.TryParse(numStr, out num))
                {
                    return num;
                }
                Write("You must enter a valid integer number, please try again: ");
            }
        }

        public int ShowMsgAskForIntegerNumber(string msg, int defaultOnEmptyInput)
        {
            Write(msg + ": ");
            while(true)
            {
                String numStr = ReadOrEofErr();
                if(numStr == "")
                {
                    return defaultOnEmptyInput;
                }
                int num;
                if(int.TryParse(numStr, out num))
                {
                    return num;
                }
                Write("You must enter a valid integer number, please try again: ");
            }
        }

        public double ShowMsgAskForFloatingPointNumber(string msg)
        {
            Write(msg + ": ");
            while(true)
            {
                String numStr = ReadOrEofErr();
                double num;
                if(double.TryParse(numStr, System.Globalization.NumberStyles.Float,
                                System.Globalization.CultureInfo.InvariantCulture, out num))
                {
                    return num;
                }
                Write("You must enter a valid floating point number, please try again: ");
            }
        }

        public double ShowMsgAskForFloatingPointNumber(string msg, double defaultOnEmptyInput)
        {
            Write(msg + ": ");
            while(true)
            {
                String numStr = ReadOrEofErr();
                if(numStr == "")
                {
                    return defaultOnEmptyInput;
                }
                double num;
                if(double.TryParse(numStr, System.Globalization.NumberStyles.Float,
                                System.Globalization.CultureInfo.InvariantCulture, out num))
                {
                    return num;
                }
                Write("You must enter a valid floating point number, please try again: ");
            }
        }

        public string ShowMsgAskForString(string msg)
        {
            Write(msg + ": ");
            return ReadOrEofErr();
        }

        public ConsoleKeyInfo LetUserChoose(UserChoiceMenu choiceMenu, UserChoiceMenu additionalGuiChoiceMenu)
        {
            if(theDebuggerGUIForDataRendering != null)
                theDebuggerGUIForDataRendering.SetContext(choiceMenu, additionalGuiChoiceMenu);

            while(true)
            {
                ConsoleKeyInfo key = ReadKeyWithCancel();

                if(choiceMenu.ContainsKey(ref key))
                    return key;
                if(additionalGuiChoiceMenu != null && additionalGuiChoiceMenu.ContainsKey(ref key))
                    return key;

                theDebuggerConsoleUI.WriteLine("Illegal choice (" + key.KeyChar + "; key = " + key.Key + ")!"
                        + " Only " + choiceMenu.ToOptionsString(false) + " are allowed!");
            }
        }

        public char LetUserChoose(UserChoiceMenu choiceMenu)
        {
            return LetUserChoose(choiceMenu, null).KeyChar;
        }

        public void PrintInstructions(UserChoiceMenu choiceMenu, string prefix, string suffix)
        {
            theDebuggerConsoleUI.WriteLine(prefix + choiceMenu.ToOptionsString(false) + suffix);
        }

        public void PrintInstructionsSeparateByNewline(UserChoiceMenu choiceMenu, string prefix, string suffix)
        {
            theDebuggerConsoleUI.WriteLine(prefix + choiceMenu.ToOptionsString(true) + suffix);
        }

        public bool TwoPane
        {
            get { return TheDebuggerConsoleUI != TheDebuggerConsoleUIForDataRendering; }
        }

        public bool Gui
        {
            get { return TheDebuggerGUIForDataRendering != null; }
        }

        // IDebuggerEnvironment consisting of -----------------------------------------------------------------------------------
        // IDebuggerConsoleUI extended by errorOutWriter methods ----------------------------------

        // outWriter
        public void Write(string value)
        {
            theDebuggerConsoleUI.Write(value);
        }

        public void Write(string format, params object[] arg)
        {
            theDebuggerConsoleUI.Write(format, arg);
        }

        public void WriteLine(string value)
        {
            theDebuggerConsoleUI.WriteLine(value);
        }

        public void WriteLine(string format, params object[] arg)
        {
            theDebuggerConsoleUI.WriteLine(format, arg);
        }

        public void WriteLine()
        {
            theDebuggerConsoleUI.WriteLine();
        }

        // errorOutWriter (not used in the interactive debugger, but some methods from the environment are also called from the shell (shared code))
        public void ErrorWrite(string value)
        {
            theDebuggerConsoleUI.Write(value);
        }

        public void ErrorWrite(string format, params object[] arg)
        {
            theDebuggerConsoleUI.Write(format, arg);
        }

        public void ErrorWriteLine(string value)
        {
            theDebuggerConsoleUI.WriteLine(value);
        }

        public void ErrorWriteLine(string format, params object[] arg)
        {
            theDebuggerConsoleUI.WriteLine(format, arg);
        }

        public void ErrorWriteLine()
        {
            theDebuggerConsoleUI.WriteLine();
        }
        
        // consoleOut
        public void PrintHighlightedUserDialog(String text, HighlightingMode mode)
        {
            theDebuggerConsoleUI.PrintHighlightedUserDialog(text, mode);
        }

        // inReader
        public string ReadLine()
        {
            // GUI TODO: fits here or better introduce another ReadLine with maybe more semantic meaning/no GUI disabling?
            if(theDebuggerGUIForDataRendering != null)
                theDebuggerGUIForDataRendering.SetContext(choiceMenuEnterLineCancel, null);

            return theDebuggerConsoleUI.ReadLine();
        }
        
        // consoleIn
        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return theDebuggerConsoleUI.ReadKey(intercept);
        }

        public ConsoleKeyInfo ReadKeyWithControlCAsInput()
        {
            return theDebuggerConsoleUI.ReadKeyWithControlCAsInput();
        }

        public bool KeyAvailable
        {
            get { return theDebuggerConsoleUI.KeyAvailable; }
        }

        // IDebuggerConsoleUIForDataRendering -----------------------------------------------------

        public void WriteDataRendering(string value)
        {
            theDebuggerConsoleUIForDataRendering.WriteDataRendering(value);
        }

        public void WriteDataRendering(string format, params object[] arg)
        {
            theDebuggerConsoleUIForDataRendering.WriteDataRendering(format, arg);
        }

        public void WriteLineDataRendering(string value)
        {
            theDebuggerConsoleUIForDataRendering.WriteLineDataRendering(value);
        }

        public void WriteLineDataRendering(string format, params object[] arg)
        {
            theDebuggerConsoleUIForDataRendering.WriteLineDataRendering(format, arg);
        }

        public void WriteLineDataRendering()
        {
            theDebuggerConsoleUIForDataRendering.WriteLineDataRendering();
        }

        public void PrintHighlighted(String text, HighlightingMode mode)
        {
            theDebuggerConsoleUIForDataRendering.PrintHighlighted(text, mode);
        }

        public void Clear()
        {
            theDebuggerConsoleUIForDataRendering.Clear();
        }

        public void SuspendImmediateExecution()
        {
            theDebuggerConsoleUIForDataRendering.SuspendImmediateExecution();
        }

        public void RestartImmediateExecution()
        {
            theDebuggerConsoleUIForDataRendering.RestartImmediateExecution();
        }
    }
}
