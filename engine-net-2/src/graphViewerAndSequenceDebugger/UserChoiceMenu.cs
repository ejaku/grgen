/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Text;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    // ConsoleUI for user dialog
    public enum UserChoiceMenuNames
    {
        DebuggerMainSequenceEnteringMenu,
        SubruleDebuggingMenu,
        ContinueOnAssertionMenu,
        SkipAsRequiredInMatchByMatchProcessingMenu,
        SkipAsRequiredMenu,
        QueryContinueWhenShowPostDisabledMenu,
        QueryContinueOrTraceMenu,
        ChooseDirectionMenu,
        ChooseSequenceMenu,
        ChooseSequenceParallelMenu,
        ChoosePointMenu,
        ChooseMatchSomeFromSetMenu,
        ChooseMatchMenu,
        ChooseValueMenu,
        WhichBreakpointToToggleMenu,
        WhichChoicepointToToggleMenu,
        HandleWatchpointsMenu,
        WatchpointDetermineEventTypeToConfigureMenu,
        WatchpointMatchSubruleMessageMenu,
        WatchpointDetermineMatchGraphElementModeMenu,
        WatchpointDetermineMatchGraphElementByTypeModeMenu,
        WatchpointDetermineDecisionActionMenu,
        PauseContinueMenu,
        SwitchRefreshViewMenu
    }

    /// <summary>
    /// A description of a user choice in the user interface,
    /// built from an array of options, describing the option/command, which must be a unique name from the resources, mapping to the command string, giving its command key/character shortcut as a char in parenthesis,
    /// special cases: (0-9) to allow any keys from 0 ... 9, (any key) to allow any key not listed in the choices, (()) to render a text in simple parenthesis not getting interpreted as key char.
    /// The name should distinguish the different available choice menus.
    /// </summary>
    public class UserChoiceMenu
    {
        public UserChoiceMenu(UserChoiceMenuNames name, string[] optionNames)
        {
            this.name = name;
            this.optionNames = optionNames;
            List<string> arrayBuilder = new List<string>();
            foreach(String optionName in optionNames)
            {
                String option = global::graphViewerAndSequenceDebugger.Properties.Resources.ResourceManager.GetString(optionName);
                arrayBuilder.Add(option);
            }
            this.options = arrayBuilder.ToArray();
        }

        public UserChoiceMenuNames name;
        public string[] optionNames;
        public string[] options;

        public string ToOptionsString(bool separateByNewline)
        {
            StringBuilder sb = new StringBuilder(OptionsOverallLength(options));
            bool first = true;
            foreach(string option in options)
            {
                if(first)
                    first = false;
                else
                    sb.Append(separateByNewline ? "\n" : ", ");
                AppendDoubleParenthesisReplaced(sb, option);
            }
            return sb.ToString();
        }

        private int OptionsOverallLength(string[] options)
        {
            int length = 0;
            for(int i = 0; i < options.Length; ++i)
            {
                length += options[i].Length;
            }
            return length;
        }

        // (( and )) are escaped parenthesis that prevent content from getting interpreted but must be pretty printed to simple parenthesis
        private void AppendDoubleParenthesisReplaced(StringBuilder sb, string text)
        {
            bool openingParenthesisBefore = false;
            bool closingParenthesisBefore = false;
            foreach(char c in text)
            {
                if(c == '(' && openingParenthesisBefore)
                {
                    openingParenthesisBefore = false;
                    continue;
                }
                else if(c == ')' && closingParenthesisBefore)
                {
                    closingParenthesisBefore = false;
                    continue;
                }
                if(c == '(')
                {
                    openingParenthesisBefore = true;
                    closingParenthesisBefore = false;
                }
                else if(c == ')')
                {
                    closingParenthesisBefore = true;
                    openingParenthesisBefore = false;
                }
                else
                {
                    openingParenthesisBefore = false;
                    closingParenthesisBefore = false;
                }
                sb.Append(c);
            }
        }

        public bool IsCurrentlyAvailable(string command)
        {
            for(int i = 0; i < optionNames.Length; ++i)
            {
                if(optionNames[i] == command)
                {
                    return true;
                }
            }
            return false;
        }

        public static KeyValuePair<char, ConsoleKey> GetKey(string commandOption)
        {
            int indexOfOpeningParenthesis = -1;
            int indexOfClosingParenthesis = 0;

            while(true)
            {
                indexOfOpeningParenthesis = commandOption.IndexOf('(', indexOfOpeningParenthesis + 1);
                if(indexOfOpeningParenthesis == -1)
                    break; // may happen when only placeholder keys that are skipped are contained in the command (or none at all, but that would be illegal)
                indexOfClosingParenthesis = commandOption.IndexOf(')', indexOfOpeningParenthesis);
                if(indexOfClosingParenthesis == -1)
                    break; // may happen when only placeholder keys that are skipped are contained in the command (or none at all, but that would be illegal)
                if(commandOption[indexOfOpeningParenthesis + 1] == 'F') // return the first contained function key (F1..F24)
                {
                    return MenuPlaceholderToFunctionKey(commandOption.Substring(indexOfOpeningParenthesis, indexOfClosingParenthesis - indexOfOpeningParenthesis + 1));
                }
                else if(commandOption[indexOfOpeningParenthesis + 1] == '(' // skip escaped parenthesis in the form of (())
                    || indexOfClosingParenthesis > indexOfOpeningParenthesis + 2) // skip number special (0-9), also skipping (any key)
                {
                    indexOfOpeningParenthesis = indexOfClosingParenthesis;
                }
                else
                    return new KeyValuePair<char, ConsoleKey>(commandOption[indexOfOpeningParenthesis + 1], ConsoleKey.NoName); // return first matching key
            }

            if(commandOption.Contains("(any key)"))
                return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.NoName); // return space in case the option contains only (any key)

            throw new Exception("Internal error - no command option that can generate a key");
        }

        private static KeyValuePair<char, ConsoleKey> MenuPlaceholderToFunctionKey(string commandOption)
        {
            switch(commandOption)
            {
                case "(F1)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F1);
                case "(F2)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F2);
                case "(F3)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F3);
                case "(F4)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F4);
                case "(F5)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F5);
                case "(F6)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F6);
                case "(F7)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F7);
                case "(F8)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F8);
                case "(F9)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F9);
                case "(F10)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F10);
                case "(F11)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F11);
                case "(F12)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F12);
                case "(F13)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F13);
                case "(F14)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F14);
                case "(F15)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F15);
                case "(F16)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F16);
                case "(F17)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F17);
                case "(F18)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F18);
                case "(F19)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F19);
                case "(F20)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F20);
                case "(F21)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F21);
                case "(F22)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F22);
                case "(F23)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F23);
                case "(F24)": return new KeyValuePair<char, ConsoleKey>(' ', ConsoleKey.F24);
                default: throw new Exception("Internal error - unknown function key in command options");
            }
        }
    }
}
