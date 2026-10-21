// by Claude Code with Edgar Jakumeit

using System.Collections.Generic;

/// <summary>
/// AST classes for parsed VCG files.
/// </summary>
namespace de.unika.ipd.grGen.extMSAGLExt.VcgAst
{
    public abstract class Entity
    {
        public Dictionary<string, string> Attributes = new Dictionary<string, string>(System.StringComparer.Ordinal);
    }

    public class Node : Entity
    {
    }

    public class Subgraph : Node
    {
        public List<Entity> Children = new List<Entity>();
    }

    public class Edge : Entity
    {
    }

    public class Graph
    {
        public Dictionary<string, string> Attributes = new Dictionary<string, string>(System.StringComparer.Ordinal);
        public List<Entity> Children = new List<Entity>();
    }

    /// <summary>
    /// The attribute values are stored in the AST as they were written (string literals including their quotation marks),
    /// this allows to check in the second pass whether a string literal or an identifier/number was given.
    /// </summary>
    public static class Literal
    {
        public static bool IsStringLiteral(string raw)
        {
            return raw.Length >= 2 && raw[0] == '"' && raw[raw.Length - 1] == '"';
        }

        /// <summary>
        /// Returns the content of a string literal, with the escaping of backslashes and quotation marks undone.
        /// </summary>
        public static string ToStringValue(string raw)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for(int i = 1; i < raw.Length - 1; ++i)
            {
                if(raw[i] == '\\' && i + 1 < raw.Length - 1 && (raw[i + 1] == '\\' || raw[i + 1] == '"'))
                    ++i;
                sb.Append(raw[i]);
            }
            return sb.ToString();
        }
    }
}
