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
}
