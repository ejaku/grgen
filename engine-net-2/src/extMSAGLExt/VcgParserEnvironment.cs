// by Claude Code with Edgar Jakumeit

using System.Collections.Generic;
using de.unika.ipd.grGen.extMSAGLExt.VcgAst;

namespace de.unika.ipd.grGen.extMSAGLExt
{
    /// <summary>
    /// Environment of the vcg parser (generated from Vcg.g): the definition table filled during parsing,
    /// plus the helpers used by the grammar actions for filling it.
    /// </summary>
    public class VcgParserEnvironment
    {
        // Populated during parsing; edges use this to resolve source/target names.
        public readonly Dictionary<string, Node> NodeOrSubgraphByName = new Dictionary<string, Node>();

        // enters the node or subgraph into the definition table, returns false (after emitting an error) in case the title is already in use
        public bool AddNodeOrSubgraph(string title, Node nodeOrSubgraph, string context)
        {
            if(NodeOrSubgraphByName.ContainsKey(title))
            {
                System.Console.Error.WriteLine("VCG error: duplicate " + context + " title \"" + title + "\" -- skipped");
                return false;
            }
            NodeOrSubgraphByName[title] = nodeOrSubgraph;
            return true;
        }

        // used to fetch the mandatory title attribute (needed for entering the node or subgraph into the definition table; used to post-filter entities with a missing title when their block ends)
        // the title attribute must be a string literal, returns its content or null (after emitting an error)
        public string GetNameOrNull(Entity entity, string attributeName, string context)
        {
            string raw;
            if(!entity.Attributes.TryGetValue(attributeName, out raw))
            {
                System.Console.Error.WriteLine("VCG error: " + context + " missing mandatory " + attributeName + " attribute -- skipped");
                return null;
            }
            if(!Literal.IsStringLiteral(raw))
            {
                System.Console.Error.WriteLine("VCG error: " + context + " attribute " + attributeName + " is not a string literal (" + raw + ") -- skipped");
                return null;
            }
            return Literal.ToStringValue(raw);
        }
    }
}
