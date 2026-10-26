// by Claude Code with Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.extMSAGLExt
{
    /// <summary>
    /// Parses the syntactic elements of GrGen-yComp protocol command lines:
    /// splits a line into keyword and (unescaped) arguments, and converts argument strings
    /// to attribute keys, colors, shapes and line styles. Stateless; the execution of the commands
    /// is up to the CommandLineInterpreter.
    /// </summary>
    public static class CommandLineParser
    {
        // ---- tokenizer: keyword "arg1" "arg2" ... ----
        // Arguments are enclosed in quotes; waitForElement is the exception with an unquoted bool.
        // Escaping inside quotes (applied by the sending client, undone here): \\ -> backslash, \n -> newline, \" -> quotation mark.
        // Throws a ProtocolSyntaxException on syntax errors.

        // the line is matched as a whole, strictly: no leading or trailing spaces, exactly one space between the parts;
        // keyword, then quoted (with escaped content) or unquoted arguments
        private static readonly Regex commandLine =
            new Regex(@"^(?<keyword>\w+)(?: (?<argument>""(?:[^""\\]|\\.)*""|\w+))*$", RegexOptions.Compiled);

        public static List<string> Tokenize(string line)
        {
            List<string> tokens = new List<string>();
            if(line.Length == 0)
                return tokens;

            Match match = commandLine.Match(line);
            if(!match.Success)
                throw new ProtocolSyntaxException("malformed command line (e.g. unterminated string argument or dangling backslash) \"" + line + "\"");

            tokens.Add(match.Groups["keyword"].Value);
            foreach(Capture argument in match.Groups["argument"].Captures)
            {
                if(argument.Value[0] == '"')
                    tokens.Add(Unescape(argument.Value.Substring(1, argument.Value.Length - 2), line));
                else
                    tokens.Add(argument.Value); // unquoted token (e.g. waitForElement true/false)
            }
            return tokens;
        }

        // the regex guarantees that a backslash is always followed by a character
        private static string Unescape(string content, string line)
        {
            StringBuilder sb = new StringBuilder(content.Length);
            for(int i = 0; i < content.Length; ++i)
            {
                if(content[i] != '\\')
                {
                    sb.Append(content[i]);
                    continue;
                }
                ++i;
                switch(content[i])
                {
                    case 'n': sb.Append('\n'); break;
                    case '\\': sb.Append('\\'); break;
                    case '"': sb.Append('"'); break;
                    default: throw new ProtocolSyntaxException("unknown escape sequence \\" + content[i] + " in the command line \"" + line + "\"");
                }
            }
            return sb.ToString();
        }

        // ---- attribute key parsing: "ownerType::attrName : attrTypeStr" ----

        public static void SplitAttrKey(string attrKey, out string ownerType, out string attrName, out string attrType)
        {
            int dcIdx = attrKey.IndexOf("::", StringComparison.Ordinal);
            if(dcIdx < 0)
                throw new ProtocolSyntaxException("attribute key \"" + attrKey + "\" does not contain ::");
            ownerType = attrKey.Substring(0, dcIdx);
            string rest = attrKey.Substring(dcIdx + 2);
            int csIdx = rest.IndexOf(" : ", StringComparison.Ordinal);
            if(csIdx < 0)
            {
                attrName = rest;
                attrType = "";
                return;
            }
            attrName = rest.Substring(0, csIdx);
            attrType = rest.Substring(csIdx + 3);
        }

        // ---- enum parsing (wire names identical to VCGDumper strings) ----

        public static GrColor ParseColor(string s)
        {
            switch(s)
            {
                case "black": return GrColor.Black;
                case "blue": return GrColor.Blue;
                case "green": return GrColor.Green;
                case "cyan": return GrColor.Cyan;
                case "red": return GrColor.Red;
                case "purple": return GrColor.Purple;
                case "khaki": return GrColor.Khaki;
                case "darkgrey": return GrColor.Grey;
                case "lightgrey": return GrColor.LightGrey;
                case "lightblue": return GrColor.LightBlue;
                case "lightgreen": return GrColor.LightGreen;
                case "lightcyan": return GrColor.LightCyan;
                case "lightred": return GrColor.LightRed;
                case "lightmagenta": return GrColor.LightPurple;
                case "yellow": return GrColor.Yellow;
                case "white": return GrColor.White;
                case "darkblue": return GrColor.DarkBlue;
                case "darkred": return GrColor.DarkRed;
                case "darkgreen": return GrColor.DarkGreen;
                case "darkyellow": return GrColor.DarkYellow;
                case "darkmagenta": return GrColor.DarkMagenta;
                case "darkcyan": return GrColor.DarkCyan;
                case "gold": return GrColor.Gold;
                case "lilac": return GrColor.Lilac;
                case "turquoise": return GrColor.Turquoise;
                case "aquamarine": return GrColor.Aquamarine;
                case "pink": return GrColor.Pink;
                case "orange": return GrColor.Orange;
                case "orchid": return GrColor.Orchid;
                case "lightyellow": return GrColor.LightYellow;
                case "yellowgreen": return GrColor.YellowGreen;
                default: throw new ProtocolSyntaxException("unknown color \"" + s + "\"");
            }
        }

        public static GrNodeShape ParseShape(string s)
        {
            switch(s)
            {
                case "box": return GrNodeShape.Box;
                case "triangle": return GrNodeShape.Triangle;
                case "circle": return GrNodeShape.Circle;
                case "ellipse": return GrNodeShape.Ellipse;
                case "rhomb": return GrNodeShape.Rhomb;
                case "hexagon": return GrNodeShape.Hexagon;
                case "trapeze": return GrNodeShape.Trapeze;
                case "uptrapeze": return GrNodeShape.UpTrapeze;
                case "lparallelogram": return GrNodeShape.LParallelogram;
                case "rparallelogram": return GrNodeShape.RParallelogram;
                default: throw new ProtocolSyntaxException("unknown shape \"" + s + "\"");
            }
        }

        public static GrLineStyle ParseLineStyle(string s)
        {
            switch(s)
            {
                case "continuous": return GrLineStyle.Continuous;
                case "dotted": return GrLineStyle.Dotted;
                case "dashed": return GrLineStyle.Dashed;
                case "invisible": return GrLineStyle.Invisible;
                default: throw new ProtocolSyntaxException("unknown linestyle \"" + s + "\"");
            }
        }

        public static int ParseInt(string s)
        {
            int value;
            if(!int.TryParse(s, out value))
                throw new ProtocolSyntaxException("\"" + s + "\" is not an integer");
            return value;
        }
    }

    /// <summary>
    /// Thrown upon a syntax error in the live graph changes protocol (received from the sending client);
    /// not caught on purpose, errors are symptoms of bugs in the sender and are to be noticed (crash dialog).
    /// </summary>
    public class ProtocolSyntaxException : Exception
    {
        public ProtocolSyntaxException(string message)
            : base("extMSAGLExt protocol syntax error: " + message)
        {
        }
    }
}
