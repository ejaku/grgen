// by Claude Code with Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.IO;
using Antlr.Runtime;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.graphViewerAndSequenceDebugger;

namespace de.unika.ipd.grGen.extMSAGLExt
{
    /// <summary>
    /// Second pass of VCG import: walks the VcgAst and drives IBasicGraphViewerClient calls.
    /// Handles realizer deduplication, node/subgraph nesting via MoveNode, attribute parsing from info1/labels.
    /// </summary>
    public class VcgImporter
    {
        // reverse maps from VCG string to enum value (built from VCGDumper's static arrays)
        static readonly Dictionary<string, GrColor> colorByName;
        static readonly Dictionary<string, GrNodeShape> shapeByName;
        static readonly Dictionary<string, GrLineStyle> lineStyleByName;

        // VCG color names in GrColor enum order (matches VCGDumper.colors)
        static readonly string[] colorNames =
        {
            "black", "blue", "green", "cyan", "red", "purple", "khaki", "darkgrey",
            "lightgrey", "lightblue", "lightgreen", "lightcyan", "lightred", "lightmagenta", "yellow", "white",
            "darkblue", "darkred", "darkgreen", "darkyellow", "darkmagenta", "darkcyan", "gold", "lilac",
            "turquoise", "aquamarine", "khaki", "pink", "orange", "orchid", "lightyellow", "yellowgreen"
        };
        // VCG node shape names in GrNodeShape enum order (matches VCGDumper.nodeShapes)
        static readonly string[] shapeNames = { "box", "triangle", "circle", "ellipse", "rhomb", "hexagon", "trapeze", "uptrapeze", "lparallelogram", "rparallelogram" };
        // VCG line style names in GrLineStyle enum order (matches VCGDumper.lineStyles)
        static readonly string[] lineStyleNames = { "continuous", "dotted", "dashed", "invisible" };

        static VcgImporter()
        {
            colorByName = new Dictionary<string, GrColor>(StringComparer.Ordinal);
            for(int i = 0; i < colorNames.Length; i++)
                colorByName[colorNames[i]] = (GrColor)i;

            shapeByName = new Dictionary<string, GrNodeShape>(StringComparer.Ordinal);
            for(int i = 0; i < shapeNames.Length; i++)
                shapeByName[shapeNames[i]] = (GrNodeShape)i;

            lineStyleByName = new Dictionary<string, GrLineStyle>(StringComparer.Ordinal);
            for(int i = 0; i < lineStyleNames.Length; i++)
                lineStyleByName[lineStyleNames[i]] = (GrLineStyle)i;
        }

        // state of one import run
        readonly IBasicGraphViewerClient client;
        readonly Dictionary<string, VcgAst.Node> nodeByName;
        readonly Dictionary<string, string> nodeRealizerMap = new Dictionary<string, string>(StringComparer.Ordinal); // styleKey → realizerName
        readonly Dictionary<string, string> edgeRealizerMap = new Dictionary<string, string>(StringComparer.Ordinal);
        int nodeRealizerCount = 0;
        int edgeRealizerCount = 0;
        int edgeCount = 0;

        VcgImporter(IBasicGraphViewerClient client, Dictionary<string, VcgAst.Node> nodeByName)
        {
            this.client = client;
            this.nodeByName = nodeByName;
        }

        // ---- public entry point ----

        /// <summary>
        /// Imports a VCG file and displays it via the given client.
        /// Returns an error message string on failure, null on success.
        /// </summary>
        public static string Import(string filePath, IBasicGraphViewerClient client)
        {
            Console.Out.WriteLine("VCG import of \"" + filePath + "\".");

            string text; // maybe TODO: memory consumption optimization, reading entire file into memory is costly, but (building AST and) visualizing it later on will require even more memory...
            try { text = File.ReadAllText(filePath); }
            catch(Exception ex) { return "Cannot read file \"" + filePath + "\": " + ex.Message; }

            return ImportFromText(text, client);
        }

        /// <summary>
        /// Imports VCG text and displays it via the given client.
        /// Returns an error message string on a syntax error, null on success.
        /// Constraint violations are reported to the console and do not abort the import.
        /// </summary>
        public static string ImportFromText(string text, IBasicGraphViewerClient client)
        {
            VcgLexer lexer = new VcgLexer(new ANTLRStringStream(text));
            VcgParser parser = new VcgParser(new CommonTokenStream(lexer));
            VcgAst.Graph ast;
            try { ast = parser.vcgFile(); }
            catch(RecognitionException ex) { return "VCG parse error: " + ex.Message; }
            if(lexer.Errors.Count > 0) return "VCG parse error: " + lexer.Errors[0];
            if(parser.Errors.Count > 0) return "VCG parse error: " + parser.Errors[0];
            if(ast == null) return "VCG parse error: no graph found";

            new VcgImporter(client, parser.NodeOrSubgraphByName).BuildGraph(ast);
            return null;
        }

        // ---- private implementation ----

        void BuildGraph(VcgAst.Graph ast)
        {
            client.ClearGraph();

            // 1) nodes and subgraphs
            foreach(VcgAst.Entity entity in ast.Children)
            {
                if(entity is VcgAst.Subgraph || entity is VcgAst.Node)
                    AddNodeOrSubgraph(entity, null);
            }

            // 2) node attribute assignment from info1
            foreach(VcgAst.Entity entity in ast.Children)
                ApplyNodeAttributes(entity);

            // 3) edges (including the ones nested in subgraphs)
            AddEdges(ast.Children);

            // 4) layout
            string layoutName = GetLayout(ast.Attributes);
            client.SetLayout(layoutName);
            client.ForceLayout();
        }

        void AddEdges(List<VcgAst.Entity> entities)
        {
            foreach(VcgAst.Entity entity in entities)
            {
                if(entity is VcgAst.Edge)
                    AddEdge((VcgAst.Edge)entity);
                else if(entity is VcgAst.Subgraph)
                    AddEdges(((VcgAst.Subgraph)entity).Children);
            }
        }

        static string GetLayout(Dictionary<string, string> attrs)
        {
            // VCGDumper always writes layoutalgorithm: normal // comment
            // MSAGL layout names: SugiyamaScheme, MDS, Ranking, IcrementalLayout
            return "SugiyamaScheme";
        }

        void AddNodeOrSubgraph(VcgAst.Entity entity, VcgAst.Subgraph parent)
        {
            string title = GetString(entity.Attributes, "title", null);
            if(title == null) return; // already warned during parse

            string label = GetString(entity.Attributes, "label", "");
            GrColor borderColor = GetColor(entity.Attributes, "bordercolor", GrColor.Black);
            GrColor color = GetColor(entity.Attributes, "color", GrColor.Yellow);
            GrColor textColor = GetColor(entity.Attributes, "textcolor", GrColor.Black);
            GrNodeShape shape = GetShape(entity.Attributes, "shape", GrNodeShape.Box);

            string realizerName = GetOrCreateNodeRealizer(borderColor, color, textColor, shape);

            if(entity is VcgAst.Subgraph)
            {
                client.AddSubgraphNode(title, realizerName, label);
                var sg = (VcgAst.Subgraph)entity;
                foreach(VcgAst.Entity child in sg.Children)
                {
                    if(child is VcgAst.Subgraph || child is VcgAst.Node)
                        AddNodeOrSubgraph(child, sg);
                }
                // nest children into this subgraph after all are added
                foreach(VcgAst.Entity child in sg.Children)
                {
                    if(child is VcgAst.Subgraph || child is VcgAst.Node)
                    {
                        string childTitle = GetString(child.Attributes, "title", null);
                        if(childTitle != null)
                            client.MoveNode(childTitle, title);
                    }
                }
            }
            else
            {
                client.AddNode(title, realizerName, label);
            }
        }

        void ApplyNodeAttributes(VcgAst.Entity entity)
        {
            string title = GetString(entity.Attributes, "title", null);
            if(title == null) return;

            string info1 = GetString(entity.Attributes, "info1", null);
            if(info1 != null)
            {
                foreach(string line in info1.Split('\n'))
                {
                    string trimmed = line.Trim();
                    if(trimmed.Length == 0) continue;
                    if(!trimmed.Contains("::")) continue; // skip non-attribute lines like "name : type"
                    SetAttributeFromLine(title, trimmed, false);
                }
            }

            if(entity is VcgAst.Subgraph)
            {
                foreach(VcgAst.Entity child in ((VcgAst.Subgraph)entity).Children)
                    ApplyNodeAttributes(child);
            }
        }

        void AddEdge(VcgAst.Edge edge)
        {
            string src = GetString(edge.Attributes, "sourcename", null);
            string tgt = GetString(edge.Attributes, "targetname", null);
            if(src == null || tgt == null) return;

            if(!nodeByName.ContainsKey(src)) { Console.Error.WriteLine("VCG error: edge source \"" + src + "\" not found"); return; }
            if(!nodeByName.ContainsKey(tgt)) { Console.Error.WriteLine("VCG error: edge target \"" + tgt + "\" not found"); return; }

            GrColor color = GetColor(edge.Attributes, "color", GrColor.Khaki);
            GrColor textColor = GetColor(edge.Attributes, "textcolor", GrColor.Black);
            GrLineStyle lineStyle = GetLineStyle(edge.Attributes, "linestyle", GrLineStyle.Continuous);
            int thickness = GetInt(edge.Attributes, "thickness", 1, 1, 5);

            string realizerName = GetOrCreateEdgeRealizer(color, textColor, thickness, lineStyle);

            string edgeName = "e" + edgeCount++;
            string fullLabel = GetString(edge.Attributes, "label", "");

            // split label into visual part and attribute section
            string visualLabel = fullLabel;
            string attrSection = null;
            int attrMarker = fullLabel.IndexOf("\nAttributes:", StringComparison.Ordinal);
            if(attrMarker >= 0)
            {
                visualLabel = fullLabel.Substring(0, attrMarker);
                attrSection = fullLabel.Substring(attrMarker + 12); // skip "\nAttributes:"
            }

            client.AddEdge(edgeName, src, tgt, realizerName, visualLabel);

            if(attrSection != null)
            {
                foreach(string line in attrSection.Split('\n'))
                {
                    string trimmed = line.Trim();
                    if(trimmed.Length == 0) continue;
                    if(!trimmed.Contains("::")) continue;
                    SetAttributeFromLine(edgeName, trimmed, true);
                }
            }
        }

        void SetAttributeFromLine(string entityName, string line, bool isEdge)
        {
            // format: ownerType::attrName : attrTypeStr = value
            int dcIdx = line.IndexOf("::", StringComparison.Ordinal);
            if(dcIdx < 0) return;
            string ownerType = line.Substring(0, dcIdx);

            string rest = line.Substring(dcIdx + 2);
            int csIdx = rest.IndexOf(" : ", StringComparison.Ordinal);
            if(csIdx < 0) return;
            string attrName = rest.Substring(0, csIdx);

            string rest2 = rest.Substring(csIdx + 3);
            int eqIdx = rest2.IndexOf(" = ", StringComparison.Ordinal);
            if(eqIdx < 0) return;
            string attrTypeStr = rest2.Substring(0, eqIdx);
            string value = rest2.Substring(eqIdx + 3);

            if(isEdge)
                client.SetEdgeAttribute(entityName, ownerType, attrName, attrTypeStr, value);
            else
                client.SetNodeAttribute(entityName, ownerType, attrName, attrTypeStr, value);
        }

        // ---- realizer deduplication ----

        string GetOrCreateNodeRealizer(GrColor border, GrColor fill, GrColor text, GrNodeShape shape)
        {
            string key = (int)border + "_" + (int)fill + "_" + (int)text + "_" + (int)shape;
            string name;
            if(!nodeRealizerMap.TryGetValue(key, out name))
            {
                name = "nr" + nodeRealizerCount++;
                nodeRealizerMap[key] = name;
                client.AddNodeRealizer(name, border, fill, text, shape);
            }
            return name;
        }

        string GetOrCreateEdgeRealizer(GrColor color, GrColor text, int lineWidth, GrLineStyle style)
        {
            string key = (int)color + "_" + (int)text + "_" + lineWidth + "_" + (int)style;
            string name;
            if(!edgeRealizerMap.TryGetValue(key, out name))
            {
                name = "er" + edgeRealizerCount++;
                edgeRealizerMap[key] = name;
                client.AddEdgeRealizer(name, color, text, lineWidth, style);
            }
            return name;
        }

        // ---- attribute helpers (the AST contains the attribute values as written, they are checked here) ----

        // the raw value of a string literal attribute is turned into its content; for a non-string-literal value an error is reported
        static string GetString(Dictionary<string, string> attrs, string key, string defaultValue)
        {
            string raw;
            if(!attrs.TryGetValue(key, out raw))
                return defaultValue;
            if(!VcgAst.Literal.IsStringLiteral(raw))
            {
                Console.Error.WriteLine("VCG error: attribute \"" + key + "\" requires a string literal but is " + raw + " -- using default");
                return defaultValue;
            }
            return VcgAst.Literal.ToStringValue(raw);
        }

        // the raw value of an enum attribute must be an identifier of the enum
        static bool TryGetEnumIdentifier(Dictionary<string, string> attrs, string key, out string identifier)
        {
            if(!attrs.TryGetValue(key, out identifier))
                return false;
            if(VcgAst.Literal.IsStringLiteral(identifier) || !(char.IsLetter(identifier[0]) || identifier[0] == '_'))
            {
                Console.Error.WriteLine("VCG error: attribute \"" + key + "\" requires an identifier but is " + identifier + " -- using default");
                return false;
            }
            return true;
        }

        static GrColor GetColor(Dictionary<string, string> attrs, string key, GrColor defaultValue)
        {
            string s;
            if(!TryGetEnumIdentifier(attrs, key, out s)) return defaultValue;
            GrColor c;
            if(!colorByName.TryGetValue(s, out c))
            {
                Console.Error.WriteLine("VCG error: unknown color \"" + s + "\" for attribute \"" + key + "\" -- using default");
                return defaultValue;
            }
            return c;
        }

        static GrNodeShape GetShape(Dictionary<string, string> attrs, string key, GrNodeShape defaultValue)
        {
            string s;
            if(!TryGetEnumIdentifier(attrs, key, out s)) return defaultValue;
            GrNodeShape shape;
            if(!shapeByName.TryGetValue(s, out shape))
            {
                Console.Error.WriteLine("VCG error: unknown shape \"" + s + "\" for attribute \"" + key + "\" -- using default");
                return defaultValue;
            }
            return shape;
        }

        static GrLineStyle GetLineStyle(Dictionary<string, string> attrs, string key, GrLineStyle defaultValue)
        {
            string s;
            if(!TryGetEnumIdentifier(attrs, key, out s)) return defaultValue;
            GrLineStyle ls;
            if(!lineStyleByName.TryGetValue(s, out ls))
            {
                Console.Error.WriteLine("VCG error: unknown linestyle \"" + s + "\" for attribute \"" + key + "\" -- using default");
                return defaultValue;
            }
            return ls;
        }

        // an integer attribute within the given range (inclusive)
        static int GetInt(Dictionary<string, string> attrs, string key, int defaultValue, int minValue, int maxValue)
        {
            string s;
            if(!attrs.TryGetValue(key, out s)) return defaultValue;
            int v;
            if(!int.TryParse(s, out v))
            {
                Console.Error.WriteLine("VCG error: attribute \"" + key + "\" requires an integer number but is " + s + " -- using default");
                return defaultValue;
            }
            if(v < minValue || v > maxValue)
            {
                Console.Error.WriteLine("VCG error: attribute \"" + key + "\" must be in the range " + minValue + "-" + maxValue + " but is " + v + " -- using default");
                return defaultValue;
            }
            return v;
        }
    }
}
