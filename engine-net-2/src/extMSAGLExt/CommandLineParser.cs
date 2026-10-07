// by Claude Code with Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Text;
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
        // Backslash escaping inside quotes: \\n -> newline, \\\\ -> backslash.

        public static List<string> Tokenize(string line)
        {
            var tokens = new List<string>();
            int i = 0;
            // skip leading whitespace
            while(i < line.Length && line[i] == ' ') i++;
            // read command keyword (unquoted, up to first space)
            int start = i;
            while(i < line.Length && line[i] != ' ') i++;
            if(i > start) tokens.Add(line.Substring(start, i - start));
            // read remaining arguments
            while(i < line.Length)
            {
                while(i < line.Length && line[i] == ' ') i++;
                if(i >= line.Length) break;
                if(line[i] == '"')
                {
                    i++; // skip opening quote
                    var sb = new StringBuilder();
                    while(i < line.Length && line[i] != '"')
                    {
                        if(line[i] == '\\' && i + 1 < line.Length)
                        {
                            char next = line[i + 1];
                            if(next == 'n') { sb.Append('\n'); i += 2; }
                            else if(next == '\\') { sb.Append('\\'); i += 2; }
                            else { sb.Append(line[i]); i++; }
                        }
                        else { sb.Append(line[i]); i++; }
                    }
                    if(i < line.Length) i++; // skip closing quote
                    tokens.Add(sb.ToString());
                }
                else
                {
                    // unquoted token (e.g. waitForElement true/false)
                    start = i;
                    while(i < line.Length && line[i] != ' ') i++;
                    tokens.Add(line.Substring(start, i - start));
                }
            }
            return tokens;
        }

        // ---- attribute key parsing: "ownerType::attrName : attrTypeStr" ----

        public static bool SplitAttrKey(string attrKey, out string ownerType, out string attrName, out string attrType)
        {
            ownerType = attrName = attrType = "";
            int dcIdx = attrKey.IndexOf("::", StringComparison.Ordinal);
            if(dcIdx < 0) return false;
            ownerType = attrKey.Substring(0, dcIdx);
            string rest = attrKey.Substring(dcIdx + 2);
            int csIdx = rest.IndexOf(" : ", StringComparison.Ordinal);
            if(csIdx < 0) { attrName = rest; return true; }
            attrName = rest.Substring(0, csIdx);
            attrType = rest.Substring(csIdx + 3);
            return true;
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
                default: Console.Error.WriteLine("extMSAGLExt: unknown color \"" + s + "\""); return GrColor.Black;
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
                default: Console.Error.WriteLine("extMSAGLExt: unknown shape \"" + s + "\""); return GrNodeShape.Box;
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
                default: Console.Error.WriteLine("extMSAGLExt: unknown linestyle \"" + s + "\""); return GrLineStyle.Continuous;
            }
        }

        // Reverses GraphViewerApplicationProxy.Encode: "\\n" -> newline, " &nbsp;" -> "  ", "&quot;" -> "\""
        public static string Decode(string s)
        {
            if(s == null) return "";
            return s.Replace("\\n", "\n").Replace(" &nbsp;", "  ").Replace("&quot;", "\"");
        }
    }
}
