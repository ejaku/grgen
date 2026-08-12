/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Generates the pattern match objects for the SearchPlanBackend2 backend.
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.be.Csharp
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using de.unika.ipd.grgen.ir;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
	using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;
	using Alternative = de.unika.ipd.grgen.ir.pattern.Alternative;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
	using SubpatternUsage = de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using ContainerType = de.unika.ipd.grgen.ir.type.container.ContainerType;
	using SourceBuilder = de.unika.ipd.grgen.util.SourceBuilder;

	public class ActionsMatchGen : CSharpBase
	{
		// constants encoding different types of match parts
		// must be consecutive, beginning with MATCH_PART_NODES, ending with terminating dummy-element MATCH_PART_END
		internal const int MATCH_PART_NODES = 0;
		internal const int MATCH_PART_EDGES = 1;
		internal const int MATCH_PART_VARIABLES = 2;
		internal const int MATCH_PART_EMBEDDED_GRAPHS = 3;
		internal const int MATCH_PART_ALTERNATIVES = 4;
		internal const int MATCH_PART_ITERATEDS = 5;
		internal const int MATCH_PART_INDEPENDENTS = 6;
		internal const int MATCH_PART_END = 7;

		public ActionsMatchGen(string nodeTypePrefix, string edgeTypePrefix, string objectTypePrefix, string transientObjectTypePrefix)
			: base(nodeTypePrefix, edgeTypePrefix, objectTypePrefix, transientObjectTypePrefix)
		{
		}

		//////////////////////////////
		// Match objects generation //
		//////////////////////////////

		public virtual void GenPatternMatchInterface(SourceBuilder sb, PatternGraphLhs pattern, string name,
				string @base, string pathPrefixForElements, bool iterated, bool alternativeCase,
				bool matchClass, HashSet<string> elementsAlreadyDeclared)
		{
			GenMatchInterface(sb, pattern, name,
					@base, pathPrefixForElements, iterated, alternativeCase,
					matchClass, elementsAlreadyDeclared);

			foreach(PatternGraphLhs neg in pattern.Negs)
			{
				string negName = neg.NameOfGraph;
				GenPatternMatchInterface(sb, neg, pathPrefixForElements + negName,
						"GRGEN_LIBGR.IMatch", pathPrefixForElements + negName + "_",
						false, false, false, elementsAlreadyDeclared);
			}

			foreach(PatternGraphLhs idpt in pattern.Idpts)
			{
				string idptName = idpt.NameOfGraph;
				GenPatternMatchInterface(sb, idpt, pathPrefixForElements + idptName,
						"GRGEN_LIBGR.IMatch", pathPrefixForElements + idptName + "_",
						false, false, false, elementsAlreadyDeclared);
			}

			foreach(Alternative alt in pattern.Alts)
			{
				string altName = alt.NameOfGraph;
				GenAlternativeMatchInterface(sb, pathPrefixForElements + altName);
				foreach(Rule altCase in alt.AlternativeCases)
				{
					PatternGraphLhs altCasePattern = altCase.Left;
					string altPatName = pathPrefixForElements + altName + "_" + altCasePattern.NameOfGraph;
					GenPatternMatchInterface(sb, altCasePattern, altPatName,
							"IMatch_" + pathPrefixForElements + altName,
							pathPrefixForElements + altName + "_" + altCasePattern.NameOfGraph + "_",
							false, true, false, elementsAlreadyDeclared);
				}
			}

			foreach(Rule iter in pattern.Iters)
			{
				PatternGraphLhs iterPattern = iter.Left;
				string iterName = iterPattern.NameOfGraph;
				GenPatternMatchInterface(sb, iterPattern, pathPrefixForElements + iterName,
						"GRGEN_LIBGR.IMatch", pathPrefixForElements + iterName + "_",
						true, false, false, elementsAlreadyDeclared);
			}
		}

		public virtual void GenPatternMatchImplementation(SourceBuilder sb, PatternGraphLhs pattern, string name,
				string patGraphVarName, string className,
				string pathPrefixForElements,
				bool iterated, bool independent, bool parallelized, bool matchClass)
		{
			GenMatchImplementation(sb, pattern, name,
					patGraphVarName, className, pathPrefixForElements,
					iterated, independent, parallelized);

			foreach(PatternGraphLhs neg in pattern.Negs)
			{
				string negName = neg.NameOfGraph;
				GenPatternMatchImplementation(sb, neg, pathPrefixForElements + negName,
						pathPrefixForElements + negName, className,
						pathPrefixForElements + negName + "_", false, false, false, false);
			}

			foreach(PatternGraphLhs idpt in pattern.Idpts)
			{
				string idptName = idpt.NameOfGraph;
				GenPatternMatchImplementation(sb, idpt, pathPrefixForElements + idptName,
						pathPrefixForElements + idptName, className,
						pathPrefixForElements + idptName + "_", false, true, false, false);
			}

			foreach(Alternative alt in pattern.Alts)
			{
				string altName = alt.NameOfGraph;
				foreach(Rule altCase in alt.AlternativeCases)
				{
					PatternGraphLhs altCasePattern = altCase.Left;
					string altPatName = pathPrefixForElements + altName + "_" + altCasePattern.NameOfGraph;
					GenPatternMatchImplementation(sb, altCasePattern, altPatName,
							altPatName, className,
							pathPrefixForElements + altName + "_" + altCasePattern.NameOfGraph + "_",
							false, false, false, false);
				}
			}

			foreach(Rule iter in pattern.Iters)
			{
				PatternGraphLhs iterPattern = iter.Left;
				string iterName = iterPattern.NameOfGraph;
				GenPatternMatchImplementation(sb, iterPattern, pathPrefixForElements + iterName,
						pathPrefixForElements + iterName, className,
						pathPrefixForElements + iterName + "_", true, false, false, false);
			}
		}

		private void GenMatchInterface(SourceBuilder sb, PatternGraphLhs pattern,
				string name, string @base,
				string pathPrefixForElements, bool iterated, bool alternativeCase,
				bool matchClass, HashSet<string> elementsAlreadyDeclared)
		{
			string interfaceName = "IMatch_" + name;
			sb.AppendFront("public interface " + interfaceName + " : " + @base + "\n");
			sb.AppendFront("{\n");
			sb.Indent();

			for(int i = MATCH_PART_NODES; i < MATCH_PART_END; ++i)
				GenMatchedEntitiesInterface(sb, pattern, elementsAlreadyDeclared, name, i, pathPrefixForElements);

			sb.AppendFront("// further match object stuff\n");

			if(iterated)
				sb.AppendFront("bool IsNullMatch { get; }\n");

			if(alternativeCase)
				sb.AppendFront("new void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);\n");
			else
			{
				if(!matchClass)
					sb.AppendFront("void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);\n");
			}

			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Append("\n");
		}

		private static void GenAlternativeMatchInterface(SourceBuilder sb, string name)
		{
			string interfaceName = "IMatch_" + name;
			sb.AppendFront("public interface " + interfaceName + " : GRGEN_LIBGR.IMatch\n");
			sb.AppendFront("{\n");

			sb.AppendFrontIndented("void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);\n");

			sb.AppendFront("}\n");
			sb.Append("\n");
		}

		public virtual void GenMatchClassImplementation(SourceBuilder sb, PatternGraphLhs pattern, string name,
				string pathPrefixForElements)
		{
			string interfaceName = "IMatch_" + name;
			string className = "Match_" + name;
			sb.AppendFront("public class " + className + " : GRGEN_LGSP.MatchListElement<" + className + ">, "
					+ interfaceName + "\n");
			sb.AppendFront("{\n");
			sb.Indent();

			for(int i = MATCH_PART_NODES; i < MATCH_PART_END; ++i)
			{
				GenMatchedEntitiesImplementation(sb, pattern, name, i, pathPrefixForElements);
				GenMatchEnum(sb, pattern, name, i, pathPrefixForElements);
				GenIMatchImplementation(sb, pattern, name, i, pathPrefixForElements);
				sb.Append("\n");
			}

			sb.AppendFront("public override GRGEN_LIBGR.IPatternGraph Pattern { get { return null; } }\n");
			sb.AppendFront("public override GRGEN_LIBGR.IMatchClass MatchClass { get { return MatchClassInfo_" + name + ".Instance; } }\n");
			sb.AppendFront("public override GRGEN_LIBGR.IMatch Clone() { return new " + className + "(this); }\n");
			sb.AppendFront("public override GRGEN_LIBGR.IMatch Clone(IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) { return new " + className + "(this, oldToNewMap); }\n");
			sb.AppendFront("public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) "
					+ "{ _matchOfEnclosingPattern = matchOfEnclosingPattern; }\n");

			GenCopyConstructorAndContentAssignment(sb, pattern, false, name, pathPrefixForElements, className);
			GenCopyConstructorAndContentAssignment(sb, pattern, true, name, pathPrefixForElements, className);

			sb.AppendFront("public " + className + "()\n");
			sb.AppendFront("{\n");
			sb.AppendFront("}\n");

			sb.Append("\n");

			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Append("\n");
		}

		private void GenMatchImplementation(SourceBuilder sb, PatternGraphLhs pattern, string name,
				string patGraphVarName, string ruleClassName,
				string pathPrefixForElements,
				bool iterated, bool independent, bool parallelized)
		{
			string interfaceName = "IMatch_" + name;
			string className = "Match_" + name;
			sb.AppendFront("public class " + className + " : GRGEN_LGSP.MatchListElement<" + className + ">, "
					+ interfaceName + "\n");
			sb.AppendFront("{\n");
			sb.Indent();

			for(int i = MATCH_PART_NODES; i < MATCH_PART_END; ++i)
			{
				GenMatchedEntitiesImplementation(sb, pattern, name, i, pathPrefixForElements);
				GenMatchEnum(sb, pattern, name, i, pathPrefixForElements);
				GenIMatchImplementation(sb, pattern, name, i, pathPrefixForElements);
				sb.Append("\n");
			}

			sb.AppendFront("public override GRGEN_LIBGR.IPatternGraph Pattern { get { return " + ruleClassName
					+ ".instance." + patGraphVarName + "; } }\n");
			sb.AppendFront("public override GRGEN_LIBGR.IMatchClass MatchClass { get { return null; } }\n");
			if(iterated)
			{
				sb.AppendFront("public bool IsNullMatch { get { return _isNullMatch; } }\n");
				sb.AppendFront("public bool _isNullMatch;\n");
			}
			sb.AppendFront("public override GRGEN_LIBGR.IMatch Clone() { return new " + className + "(this); }\n");
			sb.AppendFront("public override GRGEN_LIBGR.IMatch Clone(IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) { return new " + className + "(this, oldToNewMap); }\n");
			sb.AppendFront("public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) "
					+ "{ _matchOfEnclosingPattern = matchOfEnclosingPattern; }\n");

			sb.AppendFront("public " + className + " nextWithSameHash;\n");

			GenCleanNextWithSameHash(sb, className);

			if(parallelized)
				sb.AppendFront("public int duplicateMatchHash;\n");

			GenCopyConstructorAndContentAssignment(sb, pattern, false, name, pathPrefixForElements, className);
			GenCopyConstructorAndContentAssignment(sb, pattern, true, name, pathPrefixForElements, className);

			sb.AppendFront("public " + className + "()\n");
			sb.AppendFront("{\n");
			sb.AppendFront("}\n");

			sb.Append("\n");

			GenIsEqualMethod(sb, pattern, name, pathPrefixForElements, className);

			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Append("\n");
		}

		private static void GenCleanNextWithSameHash(SourceBuilder sb, string className)
		{
			sb.AppendFront("public void CleanNextWithSameHash() {\n");
			sb.Indent();
			sb.AppendFront(className + " cur = this;\n");
			sb.AppendFront("while(cur != null) {\n");
			sb.Indent();
			sb.AppendFront(className + " next = cur.nextWithSameHash;\n");
			sb.AppendFront("cur.nextWithSameHash = null;\n");
			sb.AppendFront("cur = next;\n");
			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Append("\n");
		}

		private void GenCopyConstructorAndContentAssignment(SourceBuilder sb, PatternGraphLhs pattern, bool mapping,
				string name, string pathPrefixForElements, string className)
		{
			string mappingParameter = mapping ? ", IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap" : "";
			string mappingArgument = mapping ? ", oldToNewMap" : "";

			sb.AppendFront("public void AssignContent(" + className + " that" + mappingParameter + ")\n");
			sb.AppendFront("{\n");
			sb.Indent();
			for(int i = MATCH_PART_NODES; i < MATCH_PART_END; ++i)
				GenCopyMatchedEntities(sb, pattern, mapping, name, i, pathPrefixForElements);
			sb.Unindent();
			sb.AppendFront("}\n");

			sb.Append("\n");

			sb.AppendFront("public " + className + "(" + className + " that" + mappingParameter + ")\n");
			sb.AppendFront("{\n");
			sb.AppendFrontIndented("AssignContent(that" + mappingArgument + ");\n");
			sb.AppendFront("}\n");
		}

		private void GenIsEqualMethod(SourceBuilder sb, PatternGraphLhs pattern, string name, string pathPrefixForElements,
				string className)
		{
			sb.AppendFront("public bool IsEqual(" + className + " that)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("if(that==null) return false;\n");
			for(int i = MATCH_PART_NODES; i < MATCH_PART_END; ++i)
				GenEqualMatch(sb, pattern, name, i, pathPrefixForElements);
			sb.AppendFront("return true;\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenMatchedEntitiesInterface(SourceBuilder sb, PatternGraphLhs pattern,
				HashSet<string> elementsAlreadyDeclared,
				string name, int which, string pathPrefixForElements)
		{
			// the getters for the elements
			sb.AppendFront("//" + MatchedEntitiesNamePlural(which) + "\n");
			switch(which)
			{
			case MATCH_PART_NODES:
				foreach(Node node in pattern.Nodes)
				{
					string newPrefix = elementsAlreadyDeclared.Contains(FormatEntity(node)) ? "new " : "";
					sb.AppendFront(newPrefix + FormatElementInterfaceRef(node.Type) + " " + FormatEntity(node)
							+ " { get; set; }\n");
				}
				break;
			case MATCH_PART_EDGES:
				foreach(Edge edge in pattern.Edges)
				{
					string newPrefix = elementsAlreadyDeclared.Contains(FormatEntity(edge)) ? "new " : "";
					sb.AppendFront(newPrefix + FormatElementInterfaceRef(edge.Type) + " " + FormatEntity(edge)
							+ " { get; set; }\n");
				}
				break;
			case MATCH_PART_VARIABLES:
				foreach(Variable var in pattern.Vars)
				{
					string newPrefix = elementsAlreadyDeclared.Contains(FormatEntity(var)) ? "new " : "";
					sb.AppendFront(newPrefix + FormatAttributeType(var.Type) + " @" + FormatEntity(var)
							+ " { get; set; }\n");
				}
				break;
			case MATCH_PART_EMBEDDED_GRAPHS:
				foreach(SubpatternUsage sub in pattern.SubpatternUsages)
					sb.AppendFront("@" + MatchType(sub.SubpatternAction.Pattern, sub.SubpatternAction, true, "")
									+ " @" + FormatIdentifiable(sub) + " { get; }\n");
				break;
			case MATCH_PART_ALTERNATIVES:
				foreach(Alternative alt in pattern.Alts)
				{
					string altName = alt.NameOfGraph;
					sb.AppendFront("IMatch_" + pathPrefixForElements + altName + " " + altName + " { get; }\n");
				}
				break;
			case MATCH_PART_ITERATEDS:
				foreach(Rule iter in pattern.Iters)
				{
					string iterName = iter.Left.NameOfGraph;
					sb.AppendFront("GRGEN_LIBGR.IMatchesExact<IMatch_" + pathPrefixForElements + iterName + "> " + iterName + " { get; }\n");
				}
				break;
			case MATCH_PART_INDEPENDENTS:
				foreach(PatternGraphLhs idpt in pattern.Idpts)
				{
					string idptName = idpt.NameOfGraph;
					sb.AppendFront("IMatch_" + pathPrefixForElements + idptName + " " + idptName + " { get; }\n");
				}
				break;
			default:
				Debug.Assert((false));
			break;
			}
		}

		private void GenMatchedEntitiesImplementation(SourceBuilder sb, PatternGraphLhs pattern,
				string name, int which, string pathPrefixForElements)
		{
			// the element itself and the getter for it
			switch(which)
			{
			case MATCH_PART_NODES:
				foreach(Node node in pattern.Nodes)
				{
					sb.AppendFront("public " + FormatElementInterfaceRef(node.Type) + " " + FormatEntity(node)
							+ " { "
							+ "get { return (" + FormatElementInterfaceRef(node.Type) + ")" + FormatEntity(node, "_") + "; } "
							+ "set { " + FormatEntity(node, "_") + " = (GRGEN_LGSP.LGSPNode)value; }"
							+ " }\n");
				}
				foreach(Node node in pattern.Nodes)
					sb.AppendFront("public GRGEN_LGSP.LGSPNode " + FormatEntity(node, "_") + ";\n");
				break;
			case MATCH_PART_EDGES:
				foreach(Edge edge in pattern.Edges)
				{
					sb.AppendFront("public " + FormatElementInterfaceRef(edge.Type) + " " + FormatEntity(edge)
							+ " { "
							+ "get { return (" + FormatElementInterfaceRef(edge.Type) + ")" + FormatEntity(edge, "_") + "; } "
							+ "set { " + FormatEntity(edge, "_") + " = (GRGEN_LGSP.LGSPEdge)value; }"
							+ " }\n");
				}
				foreach(Edge edge in pattern.Edges)
					sb.AppendFront("public GRGEN_LGSP.LGSPEdge " + FormatEntity(edge, "_") + ";\n");
				break;
			case MATCH_PART_VARIABLES:
				foreach(Variable var in pattern.Vars)
				{
					sb.AppendFront("public " + FormatAttributeType(var.Type) + " " + FormatEntity(var)
							+ " { " + "get { return " + FormatEntity(var, "_") + "; } "
							+ "set { " + FormatEntity(var, "_") + " = value; }" 
							+ " }\n");
				}
				foreach(Variable var in pattern.Vars)
					sb.AppendFront("public " + FormatAttributeType(var.Type) + " " + FormatEntity(var, "_") + ";\n");
				break;
			case MATCH_PART_EMBEDDED_GRAPHS:
				foreach(SubpatternUsage sub in pattern.SubpatternUsages)
				{
					sb.AppendFront("public @"
							+ MatchType(sub.SubpatternAction.Pattern, sub.SubpatternAction, true, "") + " @"
							+ FormatIdentifiable(sub) + " { get { return @" + FormatIdentifiable(sub, "_") + "; } }\n");
				}
				foreach(SubpatternUsage sub in pattern.SubpatternUsages)
					sb.AppendFront("public @"
							+ MatchType(sub.SubpatternAction.Pattern, sub.SubpatternAction, true, "") + " @"
							+ FormatIdentifiable(sub, "_") + ";\n");
				break;
			case MATCH_PART_ALTERNATIVES:
				foreach(Alternative alt in pattern.Alts)
				{
					string altName = alt.NameOfGraph;
					sb.AppendFront("public IMatch_" + pathPrefixForElements + altName + " " + altName
							+ " { get { return _" + altName + "; } }\n");
				}
				foreach(Alternative alt in pattern.Alts)
				{
					string altName = alt.NameOfGraph;
					sb.AppendFront("public IMatch_" + pathPrefixForElements + altName + " _" + altName + ";\n");
				}
				break;
			case MATCH_PART_ITERATEDS:
				foreach(Rule iter in pattern.Iters)
				{
					string iterName = iter.Left.NameOfGraph;
					sb.AppendFront("public GRGEN_LIBGR.IMatchesExact<IMatch_" + pathPrefixForElements + iterName + "> "
							+ iterName + " { get { return _" + iterName + "; } }\n");
				}
				foreach(Rule iter in pattern.Iters)
				{
					string iterName = iter.Left.NameOfGraph;
					sb.AppendFront("public GRGEN_LGSP.LGSPMatchesList<Match_" + pathPrefixForElements + iterName
							+ ", IMatch_" + pathPrefixForElements + iterName + "> _" + iterName + ";\n");
				}
				break;
			case MATCH_PART_INDEPENDENTS:
				foreach(PatternGraphLhs idpt in pattern.Idpts)
				{
					string idptName = idpt.NameOfGraph;
					sb.AppendFront("public IMatch_" + pathPrefixForElements + idptName + " " + idptName
							+ " { get { return _" + idptName + "; } }\n");
				}
				foreach(PatternGraphLhs idpt in pattern.Idpts)
				{
					string idptName = idpt.NameOfGraph;
					sb.AppendFront("public IMatch_" + pathPrefixForElements + idptName + " _" + idptName + ";\n");
				}
				break;
			default:
				Debug.Assert((false));
			break;
			}
		}

		private void GenCopyMatchedEntities(SourceBuilder sb, PatternGraphLhs pattern, bool mapping,
				string name, int which, string pathPrefixForElements)
		{
			switch(which)
			{
			case MATCH_PART_NODES:
				foreach(Node node in pattern.Nodes)
				{
					string nodeName = FormatEntity(node, "_");
					if(mapping)
						sb.AppendFront(nodeName + " = (GRGEN_LGSP.LGSPNode)oldToNewMap[that." + nodeName + "];\n");
					else
						sb.AppendFront(nodeName + " = that." + nodeName + ";\n");
				}
				break;
			case MATCH_PART_EDGES:
				foreach(Edge edge in pattern.Edges)
				{
					string edgeName = FormatEntity(edge, "_");
					if(mapping)
						sb.AppendFront(edgeName + " = (GRGEN_LGSP.LGSPEdge)oldToNewMap[that." + edgeName + "];\n");
					else
						sb.AppendFront(edgeName + " = that." + edgeName + ";\n");
				}
				break;
			case MATCH_PART_VARIABLES:
				foreach(Variable var in pattern.Vars)
				{
					string varName = FormatEntity(var, "_");
					if(mapping)
					{
						if(var.Type is ContainerType)
							sb.AppendFront(varName + " = (" + FormatType(var.Type) + ")GRGEN_LIBGR.ContainerHelper.MappingClone(that." + varName + ", oldToNewMap);\n");
						else if(var.Type is NodeType || var.Type is EdgeType)
							sb.AppendFront(varName + " = (" + FormatType(var.Type) + ")oldToNewMap[that." + varName + "];\n");
						else
							sb.AppendFront(varName + " = that." + varName + ";\n");
					}
					else
						sb.AppendFront(varName + " = that." + varName + ";\n");
				}
				break;
			case MATCH_PART_EMBEDDED_GRAPHS:
				foreach(SubpatternUsage sub in pattern.SubpatternUsages)
				{
					string subName = "@" + FormatIdentifiable(sub, "_");
					string subpatternMatchName = MatchType(sub.SubpatternAction.Pattern, sub.SubpatternAction, true, "");
					if(mapping)
						sb.AppendFront(subName + " = new " + subpatternMatchName + "(that." + subName + ", oldToNewMap);\n");
					else
						sb.AppendFront(subName + " = that." + subName + ";\n");
				}
				break;
			case MATCH_PART_ALTERNATIVES:
				foreach(Alternative alt in pattern.Alts)
				{
					string altName = "_" + alt.NameOfGraph;
					if(mapping)
					{
						bool first = true;
						foreach(Rule altCase in alt.AlternativeCases)
						{
							string altCaseMatchName = "Match_" + pathPrefixForElements + alt.NameOfGraph + "_" + altCase.Pattern.NameOfGraph;
							if(first)
							{
								first = false;
								sb.AppendFront("if(that." + altName + " is " + altCaseMatchName + ")\n");
							}
							else
								sb.AppendFront("else if(that." + altName + " is " + altCaseMatchName + ")\n");
							sb.AppendFrontIndented(altName + " = new " + altCaseMatchName + "((" + altCaseMatchName + ")that." + altName + ", oldToNewMap);\n");
						}
					}
					else
						sb.AppendFront(altName + " = that." + altName + ";\n");
				}
				break;
			case MATCH_PART_ITERATEDS:
				foreach(Rule iter in pattern.Iters)
				{
					string iterName = "_" + iter.Left.NameOfGraph;
					string iteratedMatchName = "Match_" + pathPrefixForElements + iter.Left.NameOfGraph;
					string matchesListTypeName = "GRGEN_LGSP.LGSPMatchesList<" + iteratedMatchName + ", I" + iteratedMatchName + ">";
					if(mapping)
						sb.AppendFront(iterName + " = new " + matchesListTypeName + "(that." + iterName + ", oldToNewMap);\n");
					else
						sb.AppendFront(iterName + " = that." + iterName + ";\n");
				}
				break;
			case MATCH_PART_INDEPENDENTS:
				foreach(PatternGraphLhs idpt in pattern.Idpts)
				{
					string idptName = "_" + idpt.NameOfGraph;
					string idptMatchName = "Match_" + pathPrefixForElements + idpt.NameOfGraph;
					if(mapping)
						sb.AppendFront(idptName + " = new " + idptMatchName + "((" + idptMatchName + ")that." + idptName + ", oldToNewMap);\n");
					else
						sb.AppendFront(idptName + " = that." + idptName + ";\n");
				}
				break;
			default:
				Debug.Assert((false));
			break;
			}
		}

		private void GenEqualMatch(SourceBuilder sb, PatternGraphLhs pattern,
				string name, int which, string pathPrefixForElements)
		{
			switch(which)
			{
			case MATCH_PART_NODES:
				foreach(Node node in pattern.Nodes)
				{
					if(node.IsDefToBeYieldedTo())
						continue;
					string nodeName = FormatEntity(node, "_");
					sb.AppendFront("if(" + nodeName + " != that." + nodeName + ") return false;\n");
				}
				break;
			case MATCH_PART_EDGES:
				foreach(Edge edge in pattern.Edges)
				{
					if(edge.IsDefToBeYieldedTo())
						continue;
					string edgeName = FormatEntity(edge, "_");
					sb.AppendFront("if(" + edgeName + " != that." + edgeName + ") return false;\n");
				}
				break;
			case MATCH_PART_VARIABLES:
				foreach(Variable var in pattern.Vars)
				{
					if(var.IsDefToBeYieldedTo())
						continue;
					string varName = FormatEntity(var, "_");
					sb.AppendFront("if(" + varName + " != that." + varName + ") return false;\n");
				}
				break;
			case MATCH_PART_EMBEDDED_GRAPHS:
				foreach(SubpatternUsage sub in pattern.SubpatternUsages)
				{
					string subName = "@" + FormatIdentifiable(sub, "_");
					sb.AppendFront("if(!" + subName + ".IsEqual(that." + subName + ")) return false;\n");
				}
				break;
			case MATCH_PART_ALTERNATIVES:
				foreach(Alternative alt in pattern.Alts)
				{
					string altName = "_" + alt.NameOfGraph;
					foreach(Rule altCase in alt.AlternativeCases)
					{
						PatternGraphLhs altCasePattern = altCase.Left;
						sb.AppendFront("if(" + altName + " is Match_" + name + altName + "_" + altCasePattern.NameOfGraph
								+ " && !(" + altName + " as Match_" + name + altName + "_" + altCasePattern.NameOfGraph
								+ ").IsEqual(that." + altName + " as Match_" + name + altName + "_" + altCasePattern.NameOfGraph
								+ ")) return false;\n");
					}
				}
				break;
			case MATCH_PART_ITERATEDS:
				foreach(Rule iter in pattern.Iters)
				{
					string iterName = "_" + iter.Left.NameOfGraph;
					sb.AppendFront("if(" + iterName + ".Count != that." + iterName + ".Count) return false;\n");
					sb.AppendFront("IEnumerator<GRGEN_LIBGR.IMatch> " + iterName + "_thisEnumerator = " + iterName + ".GetEnumerator();\n");
					sb.AppendFront("IEnumerator<GRGEN_LIBGR.IMatch> " + iterName + "_thatEnumerator = that." + iterName + ".GetEnumerator();\n");
					sb.AppendFront("while(" + iterName + "_thisEnumerator.MoveNext())\n");
					sb.AppendFront("{\n");
					sb.Indent();
					sb.AppendFront(iterName + "_thatEnumerator.MoveNext();\n");
					sb.AppendFront("if(!(" + iterName + "_thisEnumerator.Current as Match_" + name + iterName + ").IsEqual("
							+ iterName + "_thatEnumerator.Current as Match_" + name + iterName + ")) return false;\n");
					sb.Unindent();
					sb.AppendFront("}\n");
				}
				break;
			case MATCH_PART_INDEPENDENTS:
				// for independents, the existence counts, the exact elements are irrelevant
				break;
			default:
				Debug.Assert((false));
			break;
			}
		}

		private void GenIMatchImplementation(SourceBuilder sb, PatternGraphLhs pattern,
				string name, int which, string pathPrefixForElements)
		{
			// the various match part getters

			string enumerableName = "GRGEN_LGSP." + MatchedEntitiesNamePlural(which) + "_Enumerable";
			string enumeratorName = "GRGEN_LGSP." + MatchedEntitiesNamePlural(which) + "_Enumerator";
			string typeOfMatchedEntities = TypeOfMatchedEntities(which);
			int numberOfMatchedEntities = NumOfMatchedEntities(which, pattern);
			string matchedEntitiesNameSingular = MatchedEntitiesNameSingular(which);
			string matchedEntitiesNamePlural = MatchedEntitiesNamePlural(which);

			sb.AppendFront("public override IEnumerable<" + typeOfMatchedEntities + "> " + matchedEntitiesNamePlural
					+ " { get { return new " + enumerableName + "(this); } }\n");
			sb.AppendFront("public override IEnumerator<" + typeOfMatchedEntities + "> " + matchedEntitiesNamePlural
					+ "Enumerator { get { return new " + enumeratorName + "(this); } }\n");
			sb.AppendFront("public override int NumberOf" + matchedEntitiesNamePlural
					+ " { get { return " + numberOfMatchedEntities + "; } }\n");

			// -----------------------------

			sb.AppendFront("public override " + typeOfMatchedEntities
					+ " get" + matchedEntitiesNameSingular + "At(int index)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("switch(index) {\n");

			switch(which)
			{
			case MATCH_PART_NODES:
				foreach(Node node in pattern.Nodes)
					sb.AppendFront("case (int)" + EntitiesEnumName(which, pathPrefixForElements) + ".@"
							+ FormatIdentifiable(node) + ": return " + FormatEntity(node, "_") + ";\n");
				break;
			case MATCH_PART_EDGES:
				foreach(Edge edge in pattern.Edges)
					sb.AppendFront("case (int)" + EntitiesEnumName(which, pathPrefixForElements) + ".@"
							+ FormatIdentifiable(edge) + ": return " + FormatEntity(edge, "_") + ";\n");
				break;
			case MATCH_PART_VARIABLES:
				foreach(Variable var in pattern.Vars)
					sb.AppendFront("case (int)" + EntitiesEnumName(which, pathPrefixForElements) + ".@"
							+ FormatIdentifiable(var) + ": return " + FormatEntity(var, "_") + ";\n");
				break;
			case MATCH_PART_EMBEDDED_GRAPHS:
				foreach(SubpatternUsage sub in pattern.SubpatternUsages)
					sb.AppendFront("case (int)" + EntitiesEnumName(which, pathPrefixForElements) + ".@"
							+ FormatIdentifiable(sub) + ": return " + FormatIdentifiable(sub, "_") + ";\n");
				break;
			case MATCH_PART_ALTERNATIVES:
				foreach(Alternative alt in pattern.Alts)
				{
					string altName = alt.NameOfGraph;
					sb.AppendFront("case (int)" + EntitiesEnumName(which, pathPrefixForElements) + ".@" + altName
							+ ": return _" + altName + ";\n");
				}
				break;
			case MATCH_PART_ITERATEDS:
				foreach(Rule iter in pattern.Iters)
				{
					string iterName = iter.Left.NameOfGraph;
					sb.AppendFront("case (int)" + EntitiesEnumName(which, pathPrefixForElements) + ".@" + iterName
							+ ": return _" + iterName + ";\n");
				}
				break;
			case MATCH_PART_INDEPENDENTS:
				foreach(PatternGraphLhs idpt in pattern.Idpts)
				{
					string idptName = idpt.NameOfGraph;
					sb.AppendFront("case (int)" + EntitiesEnumName(which, pathPrefixForElements) + ".@" + idptName
							+ ": return _" + idptName + ";\n");
				}
				break;
			default:
				Debug.Assert((false));
				break;
			}

			sb.AppendFront("default: return null;\n");
			sb.AppendFront("}\n");
			sb.Unindent();
			sb.AppendFront("}\n");

			// -----------------------------

			sb.AppendFront("public override " + typeOfMatchedEntities
					+ " get" + matchedEntitiesNameSingular + "(string name)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("switch(name) {\n");

			switch(which)
			{
			case MATCH_PART_NODES:
				foreach(Node node in pattern.Nodes)
					sb.AppendFront("case \"" + FormatIdentifiable(node) + "\": return " + FormatEntity(node, "_") + ";\n");
				break;
			case MATCH_PART_EDGES:
				foreach(Edge edge in pattern.Edges)
					sb.AppendFront("case \"" + FormatIdentifiable(edge) + "\": return " + FormatEntity(edge, "_") + ";\n");
				break;
			case MATCH_PART_VARIABLES:
				foreach(Variable var in pattern.Vars)
					sb.AppendFront("case \"" + FormatIdentifiable(var) + "\": return " + FormatEntity(var, "_") + ";\n");
				break;
			case MATCH_PART_EMBEDDED_GRAPHS:
				foreach(SubpatternUsage sub in pattern.SubpatternUsages)
					sb.AppendFront("case \"" + FormatIdentifiable(sub) + "\": return " + FormatIdentifiable(sub, "_") + ";\n");
				break;
			case MATCH_PART_ALTERNATIVES:
				foreach(Alternative alt in pattern.Alts)
				{
					string altName = alt.NameOfGraph;
					sb.AppendFront("case \"" + altName + "\": return _" + altName + ";\n");
				}
				break;
			case MATCH_PART_ITERATEDS:
				foreach(Rule iter in pattern.Iters)
				{
					string iterName = iter.Left.NameOfGraph;
					sb.AppendFront("case \"" + iterName + "\": return _" + iterName + ";\n");
				}
				break;
			case MATCH_PART_INDEPENDENTS:
				foreach(PatternGraphLhs idpt in pattern.Idpts)
				{
					string idptName = idpt.NameOfGraph;
					sb.AppendFront("case \"" + idptName + "\": return _" + idptName + ";\n");
				}
				break;
			default:
				Debug.Assert((false));
				break;
			}

			// -----------------------------

			sb.AppendFront("default: return null;\n");
			sb.AppendFront("}\n");
			sb.Unindent();
			sb.AppendFront("}\n");

			if(which != MATCH_PART_NODES && which != MATCH_PART_EDGES && which != MATCH_PART_VARIABLES)
				return;

			string type;
			if(which == MATCH_PART_NODES)
				type = "GRGEN_LIBGR.INode";
			else if(which == MATCH_PART_EDGES)
				type = "GRGEN_LIBGR.IEdge";
			else
				type = "object";

			sb.AppendFront("public override void Set" + matchedEntitiesNameSingular + "(string name, " + type + " value)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("switch(name) {\n");

			switch(which)
			{
			case MATCH_PART_NODES:
				foreach(Node node in pattern.Nodes)
					sb.AppendFront("case \"" + FormatIdentifiable(node) + "\": " + FormatEntity(node, "_") + " = (GRGEN_LGSP.LGSPNode)value; break;\n");
				break;
			case MATCH_PART_EDGES:
				foreach(Edge edge in pattern.Edges)
					sb.AppendFront("case \"" + FormatIdentifiable(edge) + "\": " + FormatEntity(edge, "_") + " = (GRGEN_LGSP.LGSPEdge)value; break;\n");
				break;
			case MATCH_PART_VARIABLES:
				foreach(Variable var in pattern.Vars)
					sb.AppendFront("case \"" + FormatIdentifiable(var) + "\": " + FormatEntity(var, "_") + " = (" + FormatType(var.Type) + ")value; break;\n");
				break;
			default:
				Debug.Assert((false));
				break;
			}

			sb.AppendFront("default: break;\n");
			sb.AppendFront("}\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenMatchEnum(SourceBuilder sb, PatternGraphLhs pattern,
				string name, int which, string pathPrefixForElements)
		{
			// generate enum mapping entity names to consecutive integers
			sb.AppendFront("public enum " + EntitiesEnumName(which, pathPrefixForElements) + " { ");
			switch(which)
			{
			case MATCH_PART_NODES:
				foreach(Node node in pattern.Nodes)
					sb.Append("@" + FormatIdentifiable(node) + ", ");
				break;
			case MATCH_PART_EDGES:
				foreach(Edge edge in pattern.Edges)
					sb.Append("@" + FormatIdentifiable(edge) + ", ");
				break;
			case MATCH_PART_VARIABLES:
				foreach(Variable var in pattern.Vars)
					sb.Append("@" + FormatIdentifiable(var) + ", ");
				break;
			case MATCH_PART_EMBEDDED_GRAPHS:
				foreach(SubpatternUsage sub in pattern.SubpatternUsages)
					sb.Append("@" + FormatIdentifiable(sub) + ", ");
				break;
			case MATCH_PART_ALTERNATIVES:
				foreach(Alternative alt in pattern.Alts)
					sb.Append("@" + alt.NameOfGraph + ", ");
				break;
			case MATCH_PART_ITERATEDS:
				foreach(Rule iter in pattern.Iters)
					sb.Append("@" + iter.Left.NameOfGraph + ", ");
				break;
			case MATCH_PART_INDEPENDENTS:
				foreach(PatternGraphLhs idpt in pattern.Idpts)
					sb.Append("@" + idpt.NameOfGraph + ", ");
				break;
			default:
				Debug.Assert((false));
				break;
			}
			sb.Append("END_OF_ENUM };\n");
		}

		private string MatchedEntitiesNameSingular(int which)
		{
			switch(which)
			{
			case MATCH_PART_NODES:
				return "Node";
			case MATCH_PART_EDGES:
				return "Edge";
			case MATCH_PART_VARIABLES:
				return "Variable";
			case MATCH_PART_EMBEDDED_GRAPHS:
				return "EmbeddedGraph";
			case MATCH_PART_ALTERNATIVES:
				return "Alternative";
			case MATCH_PART_ITERATEDS:
				return "Iterated";
			case MATCH_PART_INDEPENDENTS:
				return "Independent";
			default:
				Debug.Assert((false));
				return "";
			}
		}

		private string MatchedEntitiesNamePlural(int which)
		{
			return MatchedEntitiesNameSingular(which) + "s";
		}

		private string EntitiesEnumName(int which, string pathPrefixForElements)
		{
			switch(which)
			{
			case MATCH_PART_NODES:
				return pathPrefixForElements + "NodeNums";
			case MATCH_PART_EDGES:
				return pathPrefixForElements + "EdgeNums";
			case MATCH_PART_VARIABLES:
				return pathPrefixForElements + "VariableNums";
			case MATCH_PART_EMBEDDED_GRAPHS:
				return pathPrefixForElements + "SubNums";
			case MATCH_PART_ALTERNATIVES:
				return pathPrefixForElements + "AltNums";
			case MATCH_PART_ITERATEDS:
				return pathPrefixForElements + "IterNums";
			case MATCH_PART_INDEPENDENTS:
				return pathPrefixForElements + "IdptNums";
			default:
				Debug.Assert((false));
				return "";
			}
		}

		private string TypeOfMatchedEntities(int which)
		{
			switch(which)
			{
			case MATCH_PART_NODES:
				return "GRGEN_LIBGR.INode";
			case MATCH_PART_EDGES:
				return "GRGEN_LIBGR.IEdge";
			case MATCH_PART_VARIABLES:
				return "object";
			case MATCH_PART_EMBEDDED_GRAPHS:
				return "GRGEN_LIBGR.IMatch";
			case MATCH_PART_ALTERNATIVES:
				return "GRGEN_LIBGR.IMatch";
			case MATCH_PART_ITERATEDS:
				return "GRGEN_LIBGR.IMatches";
			case MATCH_PART_INDEPENDENTS:
				return "GRGEN_LIBGR.IMatch";
			default:
				Debug.Assert((false));
				return "";
			}
		}

		private int NumOfMatchedEntities(int which, PatternGraphLhs pattern)
		{
			switch(which)
			{
			case MATCH_PART_NODES:
				return pattern.Nodes.Count;
			case MATCH_PART_EDGES:
				return pattern.Edges.Count;
			case MATCH_PART_VARIABLES:
				return pattern.Vars.Count;
			case MATCH_PART_EMBEDDED_GRAPHS:
				return pattern.SubpatternUsages.Count;
			case MATCH_PART_ALTERNATIVES:
				return pattern.Alts.Count;
			case MATCH_PART_ITERATEDS:
				return pattern.Iters.Count;
			case MATCH_PART_INDEPENDENTS:
				return pattern.Idpts.Count;
			default:
				Debug.Assert((false));
				return 0;
			}
		}

		protected internal override void GenQualAccess(SourceBuilder sb, Qualification qual, object modifyGenerationState)
		{
			// needed because of inheritance, maybe todo: remove
		}

		protected internal override void GenMemberAccess(SourceBuilder sb, Entity member)
		{
			// needed because of inheritance, maybe todo: remove
		}
	}

}
