/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Generates the index part of the SearchPlanBackend2 model.
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.be.Csharp
{
using de.unika.ipd.grgen.ir;
using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
using AttributeIndex = de.unika.ipd.grgen.ir.model.AttributeIndex;
using IncidenceCountIndex = de.unika.ipd.grgen.ir.model.IncidenceCountIndex;
using Index = de.unika.ipd.grgen.ir.model.Index;
using Model = de.unika.ipd.grgen.ir.model.Model;
using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;
using BooleanType = de.unika.ipd.grgen.ir.type.basic.BooleanType;
using StringType = de.unika.ipd.grgen.ir.type.basic.StringType;
using Direction = de.unika.ipd.grgen.util.Direction;
using SourceBuilder = de.unika.ipd.grgen.util.SourceBuilder;

public class ModelIndexGen : CSharpBase
{
	public ModelIndexGen(Model model, SourceBuilder sb, string nodeTypePrefix, string edgeTypePrefix,
			string objectTypePrefix, string transientObjectTypePrefix)
		: base(nodeTypePrefix, edgeTypePrefix, objectTypePrefix, transientObjectTypePrefix)
	{
		this.model = model;
		this.sb = sb;
	}

	////////////////////////////
	// Index generation //
	////////////////////////////

	internal virtual void GenIndexTypes()
	{
		foreach(Index index in model.Indices)
			GenIndexType(index);
	}

	internal virtual void GenIndexType(Index index)
	{
		string indexName = index.Ident.ToString();
		string lookupType = index is AttributeIndex ? FormatAttributeType(((AttributeIndex)index).entity) : "int";
		string graphElementType = index is AttributeIndex
				? FormatElementInterfaceRef(((AttributeIndex)index).type)
				: FormatElementInterfaceRef(((IncidenceCountIndex)index).StartNodeType);
		if(index is AttributeIndex)
			sb.AppendFront("interface Index" + indexName + " : GRGEN_LIBGR.IAttributeIndex\n");
		else if(index is IncidenceCountIndex)
			sb.AppendFront("interface Index" + indexName + " : GRGEN_LIBGR.IIncidenceCountIndex\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("IEnumerable<" + graphElementType + "> Lookup("
				+ lookupType + " fromto);\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupAscending();\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromInclusive("
				+ lookupType + " from);\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromExclusive("
				+ lookupType + " from);\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupAscendingToInclusive("
				+ lookupType + " to);\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupAscendingToExclusive("
				+ lookupType + " to);\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromInclusiveToInclusive("
				+ lookupType + " from, " + lookupType + " to);\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromInclusiveToExclusive("
				+ lookupType + " from, " + lookupType + " to);\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromExclusiveToInclusive("
				+ lookupType + " from, " + lookupType + " to);\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromExclusiveToExclusive("
				+ lookupType + " from, " + lookupType + " to);\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupDescending();\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromInclusive("
				+ lookupType + " from);\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromExclusive("
				+ lookupType + " from);\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupDescendingToInclusive("
				+ lookupType + " to);\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupDescendingToExclusive("
				+ lookupType + " to);\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromInclusiveToInclusive("
				+ lookupType + " from, " + lookupType + " to);\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromInclusiveToExclusive("
				+ lookupType + " from, " + lookupType + " to);\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromExclusiveToInclusive("
				+ lookupType + " from, " + lookupType + " to);\n");
		sb.AppendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromExclusiveToExclusive("
				+ lookupType + " from, " + lookupType + " to);\n");
		if(index is IncidenceCountIndex)
			sb.AppendFront("int GetIncidenceCount(" + graphElementType + " element);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenIndexImplementations()
	{
		int i = 0;
		foreach(Index index in model.Indices)
		{
			if(index is AttributeIndex)
				GenIndexImplementation((AttributeIndex)index, i);
			else
				GenIndexImplementation((IncidenceCountIndex)index, i);
			++i;
		}
	}

	internal virtual void GenIndexImplementation(AttributeIndex index, int indexNum)
	{
		string indexName = index.Ident.ToString();
		string graphElementType = FormatElementInterfaceRef(index.type);
		string modelName = model.Ident.ToString() + "GraphModel";
		sb.AppendFront("public class Index" + indexName + "Impl : Index" + indexName + "\n");
		sb.AppendFront("{\n");
		sb.Indent();

		sb.AppendFront("public GRGEN_LIBGR.IndexDescription Description { get { return "
				+ modelName + ".GetIndexDescription(" + indexNum + "); } }\n");
		sb.Append("\n");

		sb.AppendFront("public int Size { get { return count; } }\n");
		sb.Append("\n");

		sb.AppendFront("protected class TreeNode\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("// search tree structure\n");
		sb.AppendFront("public TreeNode left;\n");
		sb.AppendFront("public TreeNode right;\n");
		sb.AppendFront("public int level;\n");
		sb.Append("\n");
		sb.AppendFront("// user data\n");
		sb.AppendFront("public " + graphElementType + " value;\n");
		sb.Append("\n");
		sb.AppendFront("// for the bottom node, operating as sentinel\n");
		sb.AppendFront("public TreeNode()\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("left = this;\n");
		sb.AppendFront("right = this;\n");
		sb.AppendFront("level = 0;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
		sb.AppendFront("// for regular nodes (that are born as leaf nodes)\n");
		sb.AppendFront("public TreeNode(" + graphElementType + " value, TreeNode bottom)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("left = bottom;\n");
		sb.AppendFront("right = bottom;\n");
		sb.AppendFront("level = 1;\n");
		sb.Append("\n");
		sb.AppendFront("this.value = value;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
		sb.AppendFront("// for copy constructing from other index\n");
		sb.AppendFront("public TreeNode(TreeNode left, TreeNode right, int level, " + graphElementType + " value)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("this.left = left;\n");
		sb.AppendFront("this.right = right;\n");
		sb.AppendFront("this.level = level;\n");
		sb.Append("\n");
		sb.AppendFront("this.value = value;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("protected TreeNode root;\n");
		sb.AppendFront("protected TreeNode bottom;\n");
		sb.AppendFront("protected TreeNode deleted;\n");
		sb.AppendFront("protected TreeNode last;\n");
		sb.AppendFront("protected int count;\n");
		sb.AppendFront("protected int version;\n");
		sb.Append("\n");

		GenEqualElementEntry(index);
		GenEqualEntry(index);

		GenAscendingElementEntry(index, false, true, false, true);
		GenAscendingEntry(index, false, true, false, true);
		GenAscendingElementEntry(index, true, true, false, true);
		GenAscendingEntry(index, true, true, false, true);
		GenAscendingElementEntry(index, true, false, false, true);
		GenAscendingEntry(index, true, false, false, true);
		GenAscendingElementEntry(index, false, true, true, true);
		GenAscendingEntry(index, false, true, true, true);
		GenAscendingElementEntry(index, false, true, true, false);
		GenAscendingEntry(index, false, true, true, false);
		GenAscendingElementEntry(index, true, true, true, true);
		GenAscendingEntry(index, true, true, true, true);
		GenAscendingElementEntry(index, true, true, true, false);
		GenAscendingEntry(index, true, true, true, false);
		GenAscendingElementEntry(index, true, false, true, true);
		GenAscendingEntry(index, true, false, true, true);
		GenAscendingElementEntry(index, true, false, true, false);
		GenAscendingEntry(index, true, false, true, false);

		GenDescendingElementEntry(index, false, true, false, true);
		GenDescendingEntry(index, false, true, false, true);
		GenDescendingElementEntry(index, true, true, false, true);
		GenDescendingEntry(index, true, true, false, true);
		GenDescendingElementEntry(index, true, false, false, true);
		GenDescendingEntry(index, true, false, false, true);
		GenDescendingElementEntry(index, false, true, true, true);
		GenDescendingEntry(index, false, true, true, true);
		GenDescendingElementEntry(index, false, true, true, false);
		GenDescendingEntry(index, false, true, true, false);
		GenDescendingElementEntry(index, true, true, true, true);
		GenDescendingEntry(index, true, true, true, true);
		GenDescendingElementEntry(index, true, true, true, false);
		GenDescendingEntry(index, true, true, true, false);
		GenDescendingElementEntry(index, true, false, true, true);
		GenDescendingEntry(index, true, false, true, true);
		GenDescendingElementEntry(index, true, false, true, false);
		GenDescendingEntry(index, true, false, true, false);

		GenEqual(index);

		GenAscending(index, false, true, false, true);
		GenAscending(index, true, true, false, true);
		GenAscending(index, true, false, false, true);
		GenAscending(index, false, true, true, true);
		GenAscending(index, false, true, true, false);
		GenAscending(index, true, true, true, true);
		GenAscending(index, true, true, true, false);
		GenAscending(index, true, false, true, true);
		GenAscending(index, true, false, true, false);

		GenDescending(index, false, true, false, true);
		GenDescending(index, true, true, false, true);
		GenDescending(index, true, false, false, true);
		GenDescending(index, false, true, true, true);
		GenDescending(index, false, true, true, false);
		GenDescending(index, true, true, true, true);
		GenDescending(index, true, true, true, false);
		GenDescending(index, true, false, true, true);
		GenDescending(index, true, false, true, false);

		sb.AppendFront("public Index" + indexName + "Impl(GRGEN_LGSP.LGSPGraph graph)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("this.graph = graph;\n");
		sb.Append("\n");
		sb.AppendFront("// initialize AA tree used to implement the index\n");
		sb.AppendFront("bottom = new TreeNode();\n");
		sb.AppendFront("root = bottom;\n");
		sb.AppendFront("deleted = bottom;\n");
		sb.AppendFront("count = 0;\n");
		sb.AppendFront("version = 0;\n");
		sb.Append("\n");
		sb.AppendFront("graph.OnClearingGraph += ClearingGraph;\n");
		if(index.type is NodeType)
		{
			sb.AppendFront("graph.OnNodeAdded += Added;\n");
			sb.AppendFront("graph.OnRemovingNode += Removing;\n");
			sb.AppendFront("graph.OnChangingNodeAttribute += ChangingAttribute;\n");
			sb.AppendFront("graph.OnRetypingNode += Retyping;\n");
		}
		else
		{
			sb.AppendFront("graph.OnEdgeAdded += Added;\n");
			sb.AppendFront("graph.OnRemovingEdge += Removing;\n");
			sb.AppendFront("graph.OnChangingEdgeAttribute += ChangingAttribute;\n");
			sb.AppendFront("graph.OnRetypingEdge += Retyping;\n");
		}
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("public void FillAsClone(Index" + indexName + "Impl that, "
				+ "IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("root = FillAsClone(that.root, that.bottom, oldToNewMap);\n");
		sb.AppendFront("count = that.count;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("protected TreeNode FillAsClone(TreeNode that, TreeNode otherBottom, "
				+ "IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(that == otherBottom)\n");
		sb.AppendFrontIndented("return bottom;\n");
		sb.AppendFront("else\n");
		sb.Indent();
		sb.AppendFront("return new TreeNode(\n");
		sb.Indent();
		sb.AppendFront("FillAsClone(that.left, otherBottom, oldToNewMap),\n");
		sb.AppendFront("FillAsClone(that.right, otherBottom, oldToNewMap),\n");
		sb.AppendFront("that.level,\n");
		sb.AppendFront("(" + graphElementType + ")oldToNewMap[that.value]\n");
		sb.Unindent();
		sb.AppendFront(");\n");
		sb.Unindent();
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		GenIndexMaintainingEventHandlers(index);

		GenIndexAATreeBalancingInsertionDeletion(index);

		sb.AppendFront("private GRGEN_LGSP.LGSPGraph graph;\n");

		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenEqualElementEntry(Index index)
	{
		string attributeType = index is AttributeIndex ? FormatAttributeType(((AttributeIndex)index).entity) : "int";

		sb.AppendFront("public IEnumerable<GRGEN_LIBGR.IGraphElement> LookupElements(object fromto)\n");
		sb.AppendFront("{\n");
		sb.Indent();

		sb.AppendFront("int versionAtIterationBegin = version;\n");
		sb.AppendFront("foreach(GRGEN_LIBGR.IGraphElement value in Lookup(root, (" + attributeType + ")fromto))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");

		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenEqualEntry(Index index)
	{
		string attributeType = index is AttributeIndex ? FormatAttributeType(((AttributeIndex)index).entity) : "int";
		string graphElementType = index is AttributeIndex
				? FormatElementInterfaceRef(((AttributeIndex)index).type)
				: FormatElementInterfaceRef(((IncidenceCountIndex)index).StartNodeType);

		sb.AppendFront("public IEnumerable<" + graphElementType + "> Lookup(" + attributeType + " fromto)\n");
		sb.AppendFront("{\n");
		sb.Indent();

		sb.AppendFront("int versionAtIterationBegin = version;\n");
		sb.AppendFront("foreach(" + graphElementType + " value in Lookup(root, fromto))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");

		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenEqual(AttributeIndex index)
	{
		string attributeType = FormatAttributeType(index.entity);
		string attributeName = index.entity.Ident.ToString();
		string graphElementType = FormatElementInterfaceRef(index.type);

		sb.AppendFront("private IEnumerable<" + graphElementType + "> Lookup(TreeNode current, "
				+ attributeType + " fromto)\n");
		sb.AppendFront("{\n");
		sb.Indent();

		sb.AppendFront("if(current == bottom)\n");
		sb.AppendFrontIndented("yield break;\n");
		sb.Append("\n");
		sb.AppendFront("int versionAtIterationBegin = version;\n");
		sb.Append("\n");
		sb.AppendFront("// don't go left if the value is already lower than fromto\n");
		if(index.entity.Type is BooleanType)
			sb.AppendFront("if(current.value." + attributeName + ".CompareTo(fromto)>=0)\n");
		else if(index.entity.Type is StringType)
		{
			sb.AppendFront("if(String.Compare(current.value." + attributeName
					+ ", fromto, StringComparison.InvariantCulture)>=0)\n");
		}
		else
			sb.AppendFront("if(current.value." + attributeName + " >= fromto)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("foreach(" + graphElementType + " value in Lookup(current.left, fromto))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("// (only) yield a value that is equal to fromto\n");
		sb.AppendFront("if(current.value." + attributeName + " == fromto)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("// the value is within range.\n");
		sb.AppendFront("yield return current.value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("// don't go right if the value is already higher than fromto\n");
		if(index.entity.Type is BooleanType)
			sb.AppendFront("if(current.value." + attributeName + ".CompareTo(fromto)<=0)\n");
		else if(index.entity.Type is StringType)
		{
			sb.AppendFront("if(String.Compare(current.value." + attributeName
					+ ", fromto, StringComparison.InvariantCulture)<=0)\n");
		}
		else
			sb.AppendFront("if(current.value." + attributeName + " <= fromto)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("foreach(" + graphElementType + " value in Lookup(current.right, fromto))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");

		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenAscendingElementEntry(Index index, bool fromConstrained, bool fromInclusive,
			bool toConstrained, bool toInclusive)
	{
		string attributeType = index is AttributeIndex ? FormatAttributeType(((AttributeIndex)index).entity) : "int";

		string lookupMethodNameAppendix = "Ascending";
		if(fromConstrained)
		{
			lookupMethodNameAppendix += "From";
			if(fromInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}
		if(toConstrained)
		{
			lookupMethodNameAppendix += "To";
			if(toInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}

		sb.AppendFront("public IEnumerable<GRGEN_LIBGR.IGraphElement> LookupElements" + lookupMethodNameAppendix + "(");
		if(fromConstrained)
			sb.Append("object from");
		if(fromConstrained && toConstrained)
			sb.Append(", ");
		if(toConstrained)
			sb.Append("object to");
		sb.Append(")\n");
		sb.AppendFront("{\n");
		sb.Indent();

		sb.AppendFront("int versionAtIterationBegin = version;\n");
		sb.AppendFront("foreach(GRGEN_LIBGR.IGraphElement value in Lookup" + lookupMethodNameAppendix + "(root");
		if(fromConstrained)
			sb.Append(", (" + attributeType + ")from");
		if(toConstrained)
			sb.Append(", (" + attributeType + ")to");
		sb.Append("))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");

		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenAscendingEntry(Index index, bool fromConstrained, bool fromInclusive,
			bool toConstrained, bool toInclusive)
	{
		string attributeType = index is AttributeIndex ? FormatAttributeType(((AttributeIndex)index).entity) : "int";
		string graphElementType = index is AttributeIndex
				? FormatElementInterfaceRef(((AttributeIndex)index).type)
				: FormatElementInterfaceRef(((IncidenceCountIndex)index).StartNodeType);

		string lookupMethodNameAppendix = "Ascending";
		if(fromConstrained)
		{
			lookupMethodNameAppendix += "From";
			if(fromInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}
		if(toConstrained)
		{
			lookupMethodNameAppendix += "To";
			if(toInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}

		sb.AppendFront("public IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix + "(");
		if(fromConstrained)
			sb.Append(attributeType + " from");
		if(fromConstrained && toConstrained)
			sb.Append(", ");
		if(toConstrained)
			sb.Append(attributeType + " to");
		sb.Append(")\n");
		sb.AppendFront("{\n");
		sb.Indent();

		sb.AppendFront("int versionAtIterationBegin = version;\n");
		sb.AppendFront("foreach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(root");
		if(fromConstrained)
			sb.Append(", from");
		if(toConstrained)
			sb.Append(", to");
		sb.Append("))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");

		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenAscending(AttributeIndex index, bool fromConstrained, bool fromInclusive,
			bool toConstrained, bool toInclusive)
	{
		string attributeType = FormatAttributeType(index.entity);
		string attributeName = index.entity.Ident.ToString();
		string graphElementType = FormatElementInterfaceRef(index.type);

		string lookupMethodNameAppendix = "Ascending";
		if(fromConstrained)
		{
			lookupMethodNameAppendix += "From";
			if(fromInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}
		if(toConstrained)
		{
			lookupMethodNameAppendix += "To";
			if(toInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}

		sb.AppendFront("private IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix
				+ "(TreeNode current");
		if(fromConstrained)
			sb.Append(", " + attributeType + " from");
		if(toConstrained)
			sb.Append(", " + attributeType + " to");
		sb.Append(")\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(current == bottom)\n");
		sb.AppendFrontIndented("yield break;\n");
		sb.Append("\n");
		sb.AppendFront("int versionAtIterationBegin = version;\n");
		sb.Append("\n");
		if(fromConstrained)
		{
			sb.AppendFront("// don't go left if the value is already lower than from\n");
			if(index.entity.Type is BooleanType)
			{
				sb.AppendFront("if(current.value." + attributeName + ".CompareTo(from)"
						+ (fromInclusive ? " >= " : " > ") + "0)\n");
			}
			else if(index.entity.Type is StringType)
			{
				sb.AppendFront("if(String.Compare(current.value." + attributeName
						+ ", from, StringComparison.InvariantCulture)" + (fromInclusive ? " >= " : " > ") + "0)\n");
			}
			else
				sb.AppendFront("if(current.value." + attributeName + (fromInclusive ? " >= " : " > ") + "from)\n");
		}
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("foreach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.left");
		if(fromConstrained)
			sb.Append(", from");
		if(toConstrained)
			sb.Append(", to");
		sb.Append("))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		if(fromConstrained || toConstrained)
		{
			sb.AppendFront("// (only) yield a value that is within bounds\n");
			sb.AppendFront("if(");
			if(fromConstrained)
			{
				if(index.entity.Type is BooleanType)
				{
					sb.Append("current.value." + attributeName + ".CompareTo(from)"
							+ (fromInclusive ? " >= " : " > ") + "0");
				}
				else if(index.entity.Type is StringType)
				{
					sb.Append("String.Compare(current.value." + attributeName
							+ ", from, StringComparison.InvariantCulture)" + (fromInclusive ? " >= " : " > ") + "0");
				}
				else
					sb.Append("current.value." + attributeName + (fromInclusive ? " >= " : " > ") + "from");
			}
			if(fromConstrained && toConstrained)
				sb.Append(" && ");
			if(toConstrained)
			{
				if(index.entity.Type is BooleanType)
				{
					sb.Append("current.value." + attributeName + ".CompareTo(to)"
							+ (toInclusive ? " <= " : " < ") + "0");
				}
				else if(index.entity.Type is StringType)
				{
					sb.Append("String.Compare(current.value." + attributeName
							+ ", to, StringComparison.InvariantCulture)" + (toInclusive ? " <= " : " < ") + "0");
				}
				else
					sb.Append("current.value." + attributeName + (toInclusive ? " <= " : " < ") + "to");
			}
			sb.Append(")\n");
		}
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("// the value is within range.\n");
		sb.AppendFront("yield return current.value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		if(toConstrained)
		{
			sb.AppendFront("// don't go right if the value is already higher than to\n");
			if(index.entity.Type is BooleanType)
			{
				sb.AppendFront("if(current.value." + attributeName + ".CompareTo(to)"
						+ (toInclusive ? " <= " : " < ") + "0)\n");
			}
			else if(index.entity.Type is StringType)
			{
				sb.AppendFront("if(String.Compare(current.value." + attributeName
						+ ", to, StringComparison.InvariantCulture)" + (toInclusive ? " <= " : " < ") + "0)\n");
			}
			else
				sb.AppendFront("if(current.value." + attributeName + (toInclusive ? " <= " : " < ") + "to)\n");
		}
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("foreach(" + graphElementType + " value in Lookup"
				+ lookupMethodNameAppendix + "(current.right");
		if(fromConstrained)
			sb.Append(", from");
		if(toConstrained)
			sb.Append(", to");
		sb.Append("))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenDescendingElementEntry(Index index, bool fromConstrained, bool fromInclusive,
			bool toConstrained, bool toInclusive)
	{
		string attributeType = index is AttributeIndex ? FormatAttributeType(((AttributeIndex)index).entity) : "int";

		string lookupMethodNameAppendix = "Descending";
		if(fromConstrained)
		{
			lookupMethodNameAppendix += "From";
			if(fromInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}
		if(toConstrained)
		{
			lookupMethodNameAppendix += "To";
			if(toInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}

		sb.AppendFront("public IEnumerable<GRGEN_LIBGR.IGraphElement> LookupElements" + lookupMethodNameAppendix + "(");
		if(fromConstrained)
			sb.Append("object from");
		if(fromConstrained && toConstrained)
			sb.Append(", ");
		if(toConstrained)
			sb.Append("object to");
		sb.Append(")\n");
		sb.AppendFront("{\n");
		sb.Indent();

		sb.AppendFront("int versionAtIterationBegin = version;\n");
		sb.AppendFront("foreach(GRGEN_LIBGR.IGraphElement value in Lookup" + lookupMethodNameAppendix + "(root");
		if(fromConstrained)
			sb.Append(", (" + attributeType + ")from");
		if(toConstrained)
			sb.Append(", (" + attributeType + ")to");
		sb.Append("))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");

		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenDescendingEntry(Index index, bool fromConstrained, bool fromInclusive,
			bool toConstrained, bool toInclusive)
	{
		string attributeType = index is AttributeIndex ? FormatAttributeType(((AttributeIndex)index).entity) : "int";
		string graphElementType = index is AttributeIndex
				? FormatElementInterfaceRef(((AttributeIndex)index).type)
				: FormatElementInterfaceRef(((IncidenceCountIndex)index).StartNodeType);

		string lookupMethodNameAppendix = "Descending";
		if(fromConstrained)
		{
			lookupMethodNameAppendix += "From";
			if(fromInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}
		if(toConstrained)
		{
			lookupMethodNameAppendix += "To";
			if(toInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}

		sb.AppendFront("public IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix + "(");
		if(fromConstrained)
			sb.Append(attributeType + " from");
		if(fromConstrained && toConstrained)
			sb.Append(", ");
		if(toConstrained)
			sb.Append(attributeType + " to");
		sb.Append(")\n");
		sb.AppendFront("{\n");

		sb.Indent();
		sb.AppendFront("int versionAtIterationBegin = version;\n");
		sb.AppendFront("foreach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(root");
		if(fromConstrained)
			sb.Append(", from");
		if(toConstrained)
			sb.Append(", to");
		sb.Append("))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");

		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenDescending(AttributeIndex index, bool fromConstrained, bool fromInclusive,
			bool toConstrained, bool toInclusive)
	{
		string attributeType = FormatAttributeType(index.entity);
		string attributeName = index.entity.Ident.ToString();
		string graphElementType = FormatElementInterfaceRef(index.type);

		string lookupMethodNameAppendix = "Descending";
		if(fromConstrained)
		{
			lookupMethodNameAppendix += "From";
			if(fromInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}
		if(toConstrained)
		{
			lookupMethodNameAppendix += "To";
			if(toInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}

		sb.AppendFront("private IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix
				+ "(TreeNode current");
		if(fromConstrained)
			sb.Append(", " + attributeType + " from");
		if(toConstrained)
			sb.Append(", " + attributeType + " to");
		sb.Append(")\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(current == bottom)\n");
		sb.AppendFrontIndented("yield break;\n");
		sb.Append("\n");
		sb.AppendFront("int versionAtIterationBegin = version;\n");
		sb.Append("\n");
		if(fromConstrained)
		{
			sb.AppendFront("// don't go left if the value is already lower than from\n");
			if(index.entity.Type is BooleanType)
			{
				sb.AppendFront("if(current.value." + attributeName + ".CompareTo(from)"
						+ (fromInclusive ? " <= " : " < ") + "0)\n");
			}
			else if(index.entity.Type is StringType)
			{
				sb.AppendFront("if(String.Compare(current.value." + attributeName
						+ ", from, StringComparison.InvariantCulture)" + (fromInclusive ? " <= " : " < ") + "0)\n");
			}
			else
				sb.AppendFront("if(current.value." + attributeName + (fromInclusive ? " <= " : " < ") + "from)\n");
		}
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("foreach(" + graphElementType + " value in Lookup"
				+ lookupMethodNameAppendix + "(current.right");
		if(fromConstrained)
			sb.Append(", from");
		if(toConstrained)
			sb.Append(", to");
		sb.Append("))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		if(fromConstrained || toConstrained)
		{
			sb.AppendFront("// (only) yield a value that is within bounds\n");
			sb.AppendFront("if(");
			if(fromConstrained)
			{
				if(index.entity.Type is BooleanType)
				{
					sb.Append("current.value." + attributeName + ".CompareTo(from)"
							+ (fromInclusive ? " <= " : " < ") + "0");
				}
				else if(index.entity.Type is StringType)
				{
					sb.Append("String.Compare(current.value." + attributeName
							+ ", from, StringComparison.InvariantCulture)" + (fromInclusive ? " <= " : " < ") + "0");
				}
				else
					sb.Append("current.value." + attributeName + (fromInclusive ? " <= " : " < ") + "from");
			}
			if(fromConstrained && toConstrained)
				sb.Append(" && ");
			if(toConstrained)
			{
				if(index.entity.Type is BooleanType)
				{
					sb.Append("current.value." + attributeName + ".CompareTo(to)"
							+ (toInclusive ? " >= " : " > ") + "0");
				}
				else if(index.entity.Type is StringType)
				{
					sb.Append("String.Compare(current.value." + attributeName
							+ ", to, StringComparison.InvariantCulture)" + (toInclusive ? " >= " : " > ") + "0");
				}
				else
					sb.Append("current.value." + attributeName + (toInclusive ? " >= " : " > ") + "to");
			}
			sb.Append(")\n");
		}
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("// the value is within range.\n");
		sb.AppendFront("yield return current.value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		if(toConstrained)
		{
			sb.AppendFront("// don't go right if the value is already higher than to\n");
			if(index.entity.Type is BooleanType)
			{
				sb.AppendFront("if(current.value." + attributeName + ".CompareTo(to)"
						+ (toInclusive ? " >= " : " > ") + "0)\n");
			}
			else if(index.entity.Type is StringType)
			{
				sb.AppendFront("if(String.Compare(current.value." + attributeName
						+ ", to, StringComparison.InvariantCulture)" + (toInclusive ? " >= " : " > ") + "0)\n");
			}
			else
				sb.AppendFront("if(current.value." + attributeName + (toInclusive ? " >= " : " > ") + "to)\n");
		}
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("foreach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.left");
		if(fromConstrained)
			sb.Append(", from");
		if(toConstrained)
			sb.Append(", to");
		sb.Append("))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenIndexMaintainingEventHandlers(AttributeIndex index)
	{
		string attributeType = FormatAttributeType(index.entity);
		string attributeName = index.entity.Ident.ToString();
		string graphElementType = FormatElementInterfaceRef(index.type);

		sb.AppendFront("void ClearingGraph(GRGEN_LIBGR.IGraph graph)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("// ReInitialize AA tree to clear the index\n");
		sb.AppendFront("bottom = new TreeNode();\n");
		sb.AppendFront("root = bottom;\n");
		sb.AppendFront("deleted = bottom;\n");
		sb.AppendFront("last = null;\n");
		sb.AppendFront("count = 0;\n");
		sb.AppendFront("version = 0;\n");
		sb.Unindent();
		sb.AppendFront("}\n\n");

		sb.AppendFront("void Added(GRGEN_LIBGR.IGraphElement elem)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(elem is " + graphElementType + ")\n");
		sb.AppendFrontIndented("Insert(ref root, (" + graphElementType + ")elem, "
				+ "((" + graphElementType + ")elem)." + attributeName + ");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
		sb.AppendFront("void Removing(GRGEN_LIBGR.IGraphElement elem)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(elem is " + graphElementType + ")\n");
		sb.AppendFrontIndented("Delete(ref root, (" + graphElementType + ")elem);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
		sb.AppendFront("void ChangingAttribute(GRGEN_LIBGR.IGraphElement elem, "
				+ "GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.AttributeChangeType changeType, object newValue, object keyValue)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(elem is " + graphElementType + " && attrType.Name==\"" + attributeName + "\")\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("Delete(ref root, (" + graphElementType + ")elem);\n");
		sb.AppendFront("Insert(ref root, (" + graphElementType + ")elem, (" + attributeType + ")newValue);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n\n");
		sb.Append("\n");
		sb.AppendFront("void Retyping(GRGEN_LIBGR.IGraphElement oldElem, GRGEN_LIBGR.IGraphElement newElem)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(oldElem is " + graphElementType + ")\n");
		sb.AppendFrontIndented("Delete(ref root, (" + graphElementType + ")oldElem);\n");
		sb.AppendFront("if(newElem is " + graphElementType + ")\n");
		sb.AppendFrontIndented("Insert(ref root, (" + graphElementType + ")newElem, "
				+ "((" + graphElementType + ")newElem)." + attributeName + ");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenIndexAATreeBalancingInsertionDeletion(AttributeIndex index)
	{
		string attributeType = FormatAttributeType(index.entity);
		string attributeName = index.entity.Ident.ToString();
		string graphElementType = FormatElementInterfaceRef(index.type);
		string castForUnique = index.type is NodeType ? " as GRGEN_LGSP.LGSPNodeWithUniqueId" : " as GRGEN_LGSP.LGSPEdgeWithUniqueId";

		sb.AppendFront("private void Skew(ref TreeNode current)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(current.level != current.left.level)\n");
		sb.AppendFrontIndented("return;\n");
		sb.Append("\n");
		sb.AppendFront("// rotate right\n");
		sb.AppendFront("TreeNode left = current.left;\n");
		sb.AppendFront("current.left = left.right;\n");
		sb.AppendFront("left.right = current;\n");
		sb.AppendFront("current = left;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("private void Split(ref TreeNode current)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(current.right.right.level != current.level)\n");
		sb.AppendFrontIndented("return;\n");
		sb.Append("\n");
		sb.AppendFront("// rotate left\n");
		sb.AppendFront("TreeNode right = current.right;\n");
		sb.AppendFront("current.right = right.left;\n");
		sb.AppendFront("right.left = current;\n");
		sb.AppendFront("current = right;\n");
		sb.AppendFront("++current.level;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("private void Insert(ref TreeNode current, " + graphElementType + " value, "
				+ attributeType + " attributeValue)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(current == bottom)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("current = new TreeNode(value, bottom);\n");
		sb.AppendFront("++count;\n");
		sb.AppendFront("++version;\n");
		sb.AppendFront("return;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
		if(index.entity.Type is BooleanType)
			sb.AppendFront("if(attributeValue.CompareTo(current.value." + attributeName + ")<0");
		else if(index.entity.Type is StringType)
		{
			sb.AppendFront("if(String.Compare(attributeValue, current.value." + attributeName
					+ ", StringComparison.InvariantCulture)<0");
		}
		else
			sb.AppendFront("if(attributeValue < current.value." + attributeName);
		sb.Append(" || ( attributeValue == current.value." + attributeName + " && (value" + castForUnique
				+ ").uniqueId < (current.value" + castForUnique + ").uniqueId ) )\n");
		sb.AppendFrontIndented("Insert(ref current.left, value, attributeValue);\n");
		if(index.entity.Type is BooleanType)
			sb.AppendFront("else if(attributeValue.CompareTo(current.value." + attributeName + ")>0");
		else if(index.entity.Type is StringType)
			sb.AppendFront("else if(String.Compare(attributeValue, current.value." + attributeName
					+ ", StringComparison.InvariantCulture)>0");
		else
			sb.AppendFront("else if(attributeValue > current.value." + attributeName);
		sb.Append(" || ( attributeValue == current.value." + attributeName + " && (value" + castForUnique
				+ ").uniqueId > (current.value" + castForUnique + ").uniqueId ) )\n");
		sb.AppendFrontIndented("Insert(ref current.right, value, attributeValue);\n");
		sb.AppendFront("else\n");
		sb.AppendFrontIndented("throw new Exception(\"Insertion of already available element\");\n");
		sb.Append("\n");
		sb.AppendFront("Skew(ref current);\n");
		sb.AppendFront("Split(ref current);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("private void Delete(ref TreeNode current, " + graphElementType + " value)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(current == bottom)\n");
		sb.AppendFrontIndented("return;\n");
		sb.Append("\n");
		sb.AppendFront("// search down the tree (and set pointer last and deleted)\n");
		sb.AppendFront("last = current;\n");
		if(index.entity.Type is BooleanType)
			sb.AppendFront("if(value." + attributeName + ".CompareTo(current.value." + attributeName + ")<0");
		else if(index.entity.Type is StringType)
		{
			sb.AppendFront("if(String.Compare(value." + attributeName + ", current.value." + attributeName
					+ ", StringComparison.InvariantCulture)<0");
		}
		else
			sb.AppendFront("if(value." + attributeName + " < current.value." + attributeName);
		sb.Append(" || ( value." + attributeName + " == current.value." + attributeName + " && (value" + castForUnique
				+ ").uniqueId < (current.value" + castForUnique + ").uniqueId ) )\n");
		sb.AppendFrontIndented("Delete(ref current.left, value);\n");
		sb.AppendFront("else\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("deleted = current;\n");
		sb.AppendFront("Delete(ref current.right, value);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
		sb.AppendFront("// at the bottom of the tree we remove the element (if present)\n");
		sb.AppendFront("if(current == last && deleted != bottom && "
				+ "value." + attributeName + " == deleted.value." + attributeName);
		sb.Append(" && (value" + castForUnique + ").uniqueId == (deleted.value" + castForUnique + ").uniqueId )\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("deleted.value = current.value;\n");
		sb.AppendFront("deleted = bottom;\n");
		sb.AppendFront("current = current.right;\n");
		sb.AppendFront("--count;\n");
		sb.AppendFront("++version;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.AppendFront("// on the way back, we rebalance\n");
		sb.AppendFront("else if(current.left.level < current.level - 1\n");
		sb.AppendFrontIndented("|| current.right.level < current.level - 1)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("--current.level;\n");
		sb.AppendFront("if(current.right.level > current.level)\n");
		sb.AppendFrontIndented("current.right.level = current.level;\n");
		sb.AppendFront("Skew(ref current);\n");
		sb.AppendFront("Skew(ref current.right);\n");
		sb.AppendFront("Skew(ref current.right.right);\n");
		sb.AppendFront("Split(ref current);\n");
		sb.AppendFront("Split(ref current.right);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenIndexSetType()
	{
		sb.AppendFront("public class " + model.Ident + "IndexSet : GRGEN_LIBGR.IIndexSet\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("public " + model.Ident + "IndexSet(GRGEN_LGSP.LGSPGraph graph)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		foreach(Index index in model.Indices)
		{
			string indexName = index.Ident.ToString();
			sb.AppendFront(indexName + " = new Index" + indexName + "Impl(graph);\n");
		}
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		foreach(Index index in model.Indices)
		{
			string indexName = index.Ident.ToString();
			sb.AppendFront("public Index" + indexName + "Impl " + indexName + ";\n");
		}
		sb.Append("\n");

		sb.AppendFront("public GRGEN_LIBGR.IIndex GetIndex(string indexName)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("switch(indexName)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		foreach(Index index in model.Indices)
		{
			string indexName = index.Ident.ToString();
			sb.AppendFront("case \"" + indexName + "\": return " + indexName + ";\n");
		}
		sb.AppendFront("default: return null;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("public void FillAsClone(GRGEN_LGSP.LGSPGraph originalGraph, "
				+ "IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		foreach(Index index in model.Indices)
		{
			string indexName = index.Ident.ToString();
			sb.AppendFront(indexName + ".FillAsClone((Index" + indexName + "Impl)originalGraph.Indices.GetIndex(\""
					+ indexName + "\"), oldToNewMap);\n");
		}
		sb.Unindent();
		sb.AppendFront("}\n");

		sb.Unindent();
		sb.AppendFront("}\n");
	}

	internal virtual void GenIndexImplementation(IncidenceCountIndex index, int indexNum)
	{
		string indexName = index.Ident.ToString();
		string graphElementType = FormatElementInterfaceRef(index.StartNodeType);
		string modelName = model.Ident.ToString() + "GraphModel";
		sb.AppendFront("public class Index" + indexName + "Impl : Index" + indexName + "\n");
		sb.AppendFront("{\n");
		sb.Indent();

		sb.AppendFront("public GRGEN_LIBGR.IndexDescription Description { get { return "
				+ modelName + ".GetIndexDescription(" + indexNum + "); } }\n");
		sb.Append("\n");

		sb.AppendFront("public int Size { get { return count; } }\n");
		sb.Append("\n");

		sb.AppendFront("protected class TreeNode\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("// search tree structure\n");
		sb.AppendFront("public TreeNode left;\n");
		sb.AppendFront("public TreeNode right;\n");
		sb.AppendFront("public int level;\n");
		sb.Append("\n");
		sb.AppendFront("// user data\n");
		sb.AppendFront("public int key;\n");
		sb.AppendFront("public " + graphElementType + " value;\n");
		sb.Append("\n");
		sb.AppendFront("// for the bottom node, operating as sentinel\n");
		sb.AppendFront("public TreeNode()\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("left = this;\n");
		sb.AppendFront("right = this;\n");
		sb.AppendFront("level = 0;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
		sb.AppendFront("// for regular nodes (that are born as leaf nodes)\n");
		sb.AppendFront("public TreeNode(int key, " + graphElementType + " value, TreeNode bottom)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("left = bottom;\n");
		sb.AppendFront("right = bottom;\n");
		sb.AppendFront("level = 1;\n");
		sb.Append("\n");
		sb.AppendFront("this.key = key;\n");
		sb.AppendFront("this.value = value;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
		sb.AppendFront("// for copy constructing from other index\n");
		sb.AppendFront("public TreeNode(TreeNode left, TreeNode right, int level, int key, " + graphElementType + " value)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("this.left = left;\n");
		sb.AppendFront("this.right = right;\n");
		sb.AppendFront("this.level = level;\n");
		sb.Append("\n");
		sb.AppendFront("this.key = key;\n");
		sb.AppendFront("this.value = value;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("protected TreeNode root;\n");
		sb.AppendFront("protected TreeNode bottom;\n");
		sb.AppendFront("protected TreeNode deleted;\n");
		sb.AppendFront("protected TreeNode last;\n");
		sb.AppendFront("protected int count;\n");
		sb.AppendFront("protected int version;\n");
		sb.Append("\n");
		sb.AppendFront("protected IDictionary<" + graphElementType + ", int> nodeToIncidenceCount = "
				+ "new Dictionary<" + graphElementType + ", int>();\n");
		sb.Append("\n");

		GenEqualElementEntry(index);
		GenEqualEntry(index);

		GenAscendingElementEntry(index, false, true, false, true);
		GenAscendingEntry(index, false, true, false, true);
		GenAscendingElementEntry(index, true, true, false, true);
		GenAscendingEntry(index, true, true, false, true);
		GenAscendingElementEntry(index, true, false, false, true);
		GenAscendingEntry(index, true, false, false, true);
		GenAscendingElementEntry(index, false, true, true, true);
		GenAscendingEntry(index, false, true, true, true);
		GenAscendingElementEntry(index, false, true, true, false);
		GenAscendingEntry(index, false, true, true, false);
		GenAscendingElementEntry(index, true, true, true, true);
		GenAscendingEntry(index, true, true, true, true);
		GenAscendingElementEntry(index, true, true, true, false);
		GenAscendingEntry(index, true, true, true, false);
		GenAscendingElementEntry(index, true, false, true, true);
		GenAscendingEntry(index, true, false, true, true);
		GenAscendingElementEntry(index, true, false, true, false);
		GenAscendingEntry(index, true, false, true, false);

		GenDescendingElementEntry(index, false, true, false, true);
		GenDescendingEntry(index, false, true, false, true);
		GenDescendingElementEntry(index, true, true, false, true);
		GenDescendingEntry(index, true, true, false, true);
		GenDescendingElementEntry(index, true, false, false, true);
		GenDescendingEntry(index, true, false, false, true);
		GenDescendingElementEntry(index, false, true, true, true);
		GenDescendingEntry(index, false, true, true, true);
		GenDescendingElementEntry(index, false, true, true, false);
		GenDescendingEntry(index, false, true, true, false);
		GenDescendingElementEntry(index, true, true, true, true);
		GenDescendingEntry(index, true, true, true, true);
		GenDescendingElementEntry(index, true, true, true, false);
		GenDescendingEntry(index, true, true, true, false);
		GenDescendingElementEntry(index, true, false, true, true);
		GenDescendingEntry(index, true, false, true, true);
		GenDescendingElementEntry(index, true, false, true, false);
		GenDescendingEntry(index, true, false, true, false);

		GenEqual(index);

		GenAscending(index, false, true, false, true);
		GenAscending(index, true, true, false, true);
		GenAscending(index, true, false, false, true);
		GenAscending(index, false, true, true, true);
		GenAscending(index, false, true, true, false);
		GenAscending(index, true, true, true, true);
		GenAscending(index, true, true, true, false);
		GenAscending(index, true, false, true, true);
		GenAscending(index, true, false, true, false);

		GenDescending(index, false, true, false, true);
		GenDescending(index, true, true, false, true);
		GenDescending(index, true, false, false, true);
		GenDescending(index, false, true, true, true);
		GenDescending(index, false, true, true, false);
		GenDescending(index, true, true, true, true);
		GenDescending(index, true, true, true, false);
		GenDescending(index, true, false, true, true);
		GenDescending(index, true, false, true, false);

		GenGetIncidenceCount(index);

		sb.AppendFront("public Index" + indexName + "Impl(GRGEN_LGSP.LGSPGraph graph)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("this.graph = graph;\n");
		sb.Append("\n");
		sb.AppendFront("// initialize AA tree used to implement the index\n");
		sb.AppendFront("bottom = new TreeNode();\n");
		sb.AppendFront("root = bottom;\n");
		sb.AppendFront("deleted = bottom;\n");
		sb.AppendFront("count = 0;\n");
		sb.AppendFront("version = 0;\n");
		sb.Append("\n");
		sb.AppendFront("graph.OnClearingGraph += ClearingGraph;\n");
		sb.AppendFront("graph.OnEdgeAdded += EdgeAdded;\n");
		sb.AppendFront("graph.OnNodeAdded += NodeAdded;\n");
		sb.AppendFront("graph.OnRemovingEdge += RemovingEdge;\n");
		sb.AppendFront("graph.OnRemovingNode += RemovingNode;\n");
		sb.AppendFront("graph.OnRetypingEdge += RetypingEdge;\n");
		sb.AppendFront("graph.OnRetypingNode += RetypingNode;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("public void FillAsClone(Index" + indexName + "Impl that, "
				+ "IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("root = FillAsClone(that.root, that.bottom, oldToNewMap);\n");
		sb.AppendFront("count = that.count;\n");
		sb.AppendFront("foreach(KeyValuePair<" + graphElementType + ", int> ntic in that.nodeToIncidenceCount)\n");
		sb.AppendFrontIndented("nodeToIncidenceCount.Add((" + graphElementType + ")oldToNewMap[ntic.Key], ntic.Value);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.Append("\n");

		sb.AppendFront("protected TreeNode FillAsClone(TreeNode that, TreeNode otherBottom, "
				+ "IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(that == otherBottom)\n");
		sb.AppendFrontIndented("return bottom;\n");
		sb.AppendFront("else\n");
		sb.Indent();
		sb.AppendFront("return new TreeNode(\n");
		sb.Indent();
		sb.AppendFront("FillAsClone(that.left, otherBottom, oldToNewMap),\n");
		sb.AppendFront("FillAsClone(that.right, otherBottom, oldToNewMap),\n");
		sb.AppendFront("that.level,\n");
		sb.AppendFront("that.key,\n");
		sb.AppendFront("(" + graphElementType + ")oldToNewMap[that.value]\n");
		sb.Unindent();
		sb.Unindent();
		sb.Append(");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		GenIndexMaintainingEventHandlers(index);

		GenIndexAATreeBalancingInsertionDeletion(index);

		//genCheckDump(index);

		sb.AppendFront("private GRGEN_LGSP.LGSPGraph graph;\n");

		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenEqual(IncidenceCountIndex index)
	{
		string graphElementType = FormatElementInterfaceRef(index.StartNodeType);

		sb.AppendFront("private IEnumerable<" + graphElementType + "> Lookup(TreeNode current, int fromto)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(current == bottom)\n");
		sb.AppendFrontIndented("yield break;\n");
		sb.Append("\n");
		sb.AppendFront("int versionAtIterationBegin = version;\n");
		sb.Append("\n");
		sb.AppendFront("// don't go left if the value is already lower than fromto\n");
		sb.AppendFront("if(current.key >= fromto)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("foreach(" + graphElementType + " value in Lookup(current.left, fromto))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("// (only) yield a value that is equal to fromto\n");
		sb.AppendFront("if(current.key == fromto)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("// the value is within range.\n");
		sb.AppendFront("yield return current.value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("// don't go right if the value is already higher than fromto\n");
		sb.AppendFront("if(current.key <= fromto)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("foreach(" + graphElementType + " value in Lookup(current.right, fromto))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenAscending(IncidenceCountIndex index, bool fromConstrained, bool fromInclusive,
			bool toConstrained, bool toInclusive)
	{
		string attributeType = "int";
		string graphElementType = FormatElementInterfaceRef(index.StartNodeType);

		string lookupMethodNameAppendix = "Ascending";
		if(fromConstrained)
		{
			lookupMethodNameAppendix += "From";
			if(fromInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}
		if(toConstrained)
		{
			lookupMethodNameAppendix += "To";
			if(toInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}

		sb.AppendFront("private IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix
				+ "(TreeNode current");
		if(fromConstrained)
			sb.Append(", " + attributeType + " from");
		if(toConstrained)
			sb.Append(", " + attributeType + " to");
		sb.Append(")\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(current == bottom)\n");
		sb.AppendFrontIndented("yield break;\n");
		sb.Append("\n");
		sb.AppendFront("int versionAtIterationBegin = version;\n");
		sb.Append("\n");
		if(fromConstrained)
		{
			sb.AppendFront("// don't go left if the value is already lower than from\n");
			sb.AppendFront("if(current.key" + (fromInclusive ? " >= " : " > ") + "from)\n");
		}
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("foreach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.left");
		if(fromConstrained)
			sb.Append(", from");
		if(toConstrained)
			sb.Append(", to");
		sb.Append("))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		if(fromConstrained || toConstrained)
		{
			sb.AppendFront("// (only) yield a value that is within bounds\n");
			sb.AppendFront("if(");
			if(fromConstrained)
				sb.Append("current.key" + (fromInclusive ? " >= " : " > ") + "from");
			if(fromConstrained && toConstrained)
				sb.Append(" && ");
			if(toConstrained)
				sb.Append("current.key" + (toInclusive ? " <= " : " < ") + "to");
			sb.Append(")\n");
		}
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("// the value is within range.\n");
		sb.AppendFront("yield return current.value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		if(toConstrained)
		{
			sb.AppendFront("// don't go right if the value is already higher than to\n");
			sb.AppendFront("if(current.key" + (toInclusive ? " <= " : " < ") + "to)\n");
		}
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("foreach(" + graphElementType + " value in Lookup"
				+ lookupMethodNameAppendix + "(current.right");
		if(fromConstrained)
			sb.Append(", from");
		if(toConstrained)
			sb.Append(", to");
		sb.Append("))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenDescending(IncidenceCountIndex index, bool fromConstrained, bool fromInclusive,
			bool toConstrained, bool toInclusive)
	{
		string attributeType = "int";
		string graphElementType = FormatElementInterfaceRef(index.StartNodeType);

		string lookupMethodNameAppendix = "Descending";
		if(fromConstrained)
		{
			lookupMethodNameAppendix += "From";
			if(fromInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}
		if(toConstrained)
		{
			lookupMethodNameAppendix += "To";
			if(toInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}

		sb.AppendFront("private IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix
				+ "(TreeNode current");
		if(fromConstrained)
			sb.Append(", " + attributeType + " from");
		if(toConstrained)
			sb.Append(", " + attributeType + " to");
		sb.Append(")\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(current == bottom)\n");
		sb.AppendFrontIndented("yield break;\n");
		sb.Append("\n");
		sb.AppendFront("int versionAtIterationBegin = version;\n");
		sb.Append("\n");
		if(fromConstrained)
		{
			sb.AppendFront("// don't go left if the value is already lower than from\n");
			sb.AppendFront("if(current.key" + (fromInclusive ? " <= " : " < ") + "from)\n");
		}
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("foreach(" + graphElementType + " value in Lookup"
				+ lookupMethodNameAppendix + "(current.right");
		if(fromConstrained)
			sb.Append(", from");
		if(toConstrained)
			sb.Append(", to");
		sb.Append("))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		if(fromConstrained || toConstrained)
		{
			sb.AppendFront("// (only) yield a value that is within bounds\n");
			sb.AppendFront("if(");
			if(fromConstrained)
				sb.Append("current.key" + (fromInclusive ? " <= " : " < ") + "from");
			if(fromConstrained && toConstrained)
				sb.Append(" && ");
			if(toConstrained)
				sb.Append("current.key" + (toInclusive ? " >= " : " > ") + "to");
			sb.Append(")\n");
		}
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("// the value is within range.\n");
		sb.AppendFront("yield return current.value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		if(toConstrained)
		{
			sb.AppendFront("// don't go right if the value is already higher than to\n");
			sb.AppendFront("if(current.key" + (toInclusive ? " >= " : " > ") + "to)\n");
		}
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("foreach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.left");
		if(fromConstrained)
			sb.Append(", from");
		if(toConstrained)
			sb.Append(", to");
		sb.Append("))\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("yield return value;\n");
		sb.AppendFront("if(version != versionAtIterationBegin)\n");
		sb.AppendFrontIndented("throw new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenGetIncidenceCount(IncidenceCountIndex index)
	{
		string graphElementType = FormatElementInterfaceRef(index.StartNodeType);
		sb.AppendFront("public int GetIncidenceCount(GRGEN_LIBGR.IGraphElement element)\n");
		sb.AppendFront("{\n");
		sb.AppendFrontIndented("return GetIncidenceCount((" + graphElementType + ") element);\n");
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("public int GetIncidenceCount(" + graphElementType + " element)\n");
		sb.AppendFront("{\n");
		sb.AppendFrontIndented("return nodeToIncidenceCount[element];\n");
		sb.AppendFront("}\n");
	}

	internal virtual void GenCheckDump(IncidenceCountIndex index)
	{
		string startNodeType = FormatElementInterfaceRef(index.StartNodeType);

		sb.AppendFront("protected void Check(TreeNode current)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(current == bottom)\n");
		sb.AppendFrontIndented("return;\n");
		sb.AppendFront("Check(current.left);\n");
		sb.AppendFront("if(!nodeToIncidenceCount.ContainsKey(current.value)) {\n");
		sb.Indent();
		sb.AppendFront("Dump(root);\n");
		sb.AppendFront("Dump();\n");
		sb.AppendFront("throw new Exception(\"Missing node\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.AppendFront("if(nodeToIncidenceCount[current.value]!=current.key) {\n");
		sb.Indent();
		sb.AppendFront("Dump(root);\n");
		sb.AppendFront("Dump();\n");
		sb.AppendFront("throw new Exception(\"Incidence values differ\");\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.AppendFront("Check(current.right);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("protected void Dump(TreeNode current)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(current == bottom)\n");
		sb.AppendFrontIndented("return;\n");
		sb.AppendFront("Dump(current.left);\n");
		sb.AppendFront("Console.Write(current.key);\n");
		sb.AppendFront("Console.Write(\" -> \");\n");
		sb.AppendFront("Console.WriteLine(graph.GetElementName(current.value));\n");
		sb.AppendFront("Dump(current.right);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("protected void Dump()\n");
		sb.AppendFront("{\n");
		sb.AppendFront("foreach(KeyValuePair<" + startNodeType + ",int> kvp in nodeToIncidenceCount) {\n");
		sb.Indent();
		sb.AppendFront("Console.Write(graph.GetElementName(kvp.Key));\n");
		sb.AppendFront("Console.Write(\" => \");\n");
		sb.AppendFront("Console.WriteLine(kvp.Value);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	internal virtual void GenIndexMaintainingEventHandlers(IncidenceCountIndex index)
	{
		string startNodeType = FormatElementInterfaceRef(index.StartNodeType);
		string incidentEdgeType = FormatElementInterfaceRef(index.IncidentEdgeType);
		string incidentEdgeTypeType = FormatTypeClassRefInstance(index.IncidentEdgeType);

		sb.AppendFront("void ClearingGraph(GRGEN_LIBGR.IGraph graph)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("// ReInitialize AA tree to clear the index\n");
		sb.AppendFront("bottom = new TreeNode();\n");
		sb.AppendFront("root = bottom;\n");
		sb.AppendFront("deleted = bottom;\n");
		sb.AppendFront("last = null;\n");
		sb.AppendFront("count = 0;\n");
		sb.AppendFront("version = 0;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("void EdgeAdded(GRGEN_LIBGR.IEdge edge)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		//sb.append("Check(root);\n");
		sb.AppendFront("if(!(edge is " + incidentEdgeType + "))\n");
		sb.AppendFrontIndented("return;\n");
		sb.AppendFront("GRGEN_LIBGR.INode source = edge.Source;\n");
		sb.AppendFront("GRGEN_LIBGR.INode target = edge.Target;\n");
		GenIndexMaintainingEdgeAdded(index);
		//sb.append("Check(root);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("void NodeAdded(GRGEN_LIBGR.INode node)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		//sb.append("Check(root);\n");
		sb.AppendFront("if(node is " + startNodeType + ") {\n");
		sb.Indent();
		sb.AppendFront("nodeToIncidenceCount.Add((" + startNodeType + ")node, 0);\n");
		sb.AppendFront("Insert(ref root, 0, (" + startNodeType + ")node);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		//sb.append("Check(root);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("void RemovingEdge(GRGEN_LIBGR.IEdge edge)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		//sb.append("Check(root);\n");
		sb.AppendFront("if(!(edge is " + incidentEdgeType + "))\n");
		sb.AppendFrontIndented("return;\n");
		sb.AppendFront("GRGEN_LIBGR.INode source = edge.Source;\n");
		sb.AppendFront("GRGEN_LIBGR.INode target = edge.Target;\n");
		GenIndexMaintainingRemovingEdge(index);
		//sb.append("Check(root);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("void RemovingNode(GRGEN_LIBGR.INode node)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		//sb.append("Check(root);\n");
		sb.AppendFront("if(node is " + startNodeType + ") {\n");
		sb.Indent();
		sb.AppendFront("nodeToIncidenceCount.Remove((" + startNodeType + ")node);\n");
		sb.AppendFront("Delete(ref root, 0, (" + startNodeType + ")node);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		//sb.append("Check(root);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("void RetypingEdge(GRGEN_LIBGR.IEdge oldEdge, GRGEN_LIBGR.IEdge newEdge)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		//sb.append("Check(root);\n");
		sb.AppendFront("RemovingEdge(oldEdge);\n");
		sb.AppendFront("EdgeAdded(newEdge);\n");
		//sb.append("Check(root);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("void RetypingNode(GRGEN_LIBGR.INode oldNode, GRGEN_LIBGR.INode newNode)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		//sb.append("Check(root);\n");
		sb.AppendFront("IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType> incidentEdges = "
				+ "GRGEN_LIBGR.GraphHelper.Incident(oldNode, " + incidentEdgeTypeType + ", graph.Model.NodeModel.RootType);\n");
		sb.AppendFront("foreach(KeyValuePair<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType> edgeKVP in incidentEdges)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("GRGEN_LIBGR.IEdge edge = edgeKVP.Key;\n");
		sb.AppendFront("GRGEN_LIBGR.INode source = edge.Source;\n");
		sb.AppendFront("GRGEN_LIBGR.INode target = edge.Target;\n");
		GenIndexMaintainingRemovingEdge(index);
		sb.Unindent();
		sb.AppendFront("}\n");

		sb.AppendFront("if(oldNode is " + startNodeType + ") {\n");
		sb.Indent();
		sb.AppendFront("nodeToIncidenceCount.Remove((" + startNodeType + ")oldNode);\n");
		sb.AppendFront("Delete(ref root, 0, (" + startNodeType + ")oldNode);\n");
		sb.Unindent();
		sb.AppendFront("}\n");

		sb.AppendFront("if(newNode is " + startNodeType + ") {\n");
		sb.Indent();
		sb.AppendFront("nodeToIncidenceCount.Add((" + startNodeType + ")newNode, 0);\n");
		sb.AppendFront("Insert(ref root, 0, (" + startNodeType + ")newNode);\n");
		sb.Unindent();
		sb.AppendFront("}\n");

		sb.AppendFront("foreach(KeyValuePair<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType> edgeKVP in incidentEdges)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("GRGEN_LIBGR.IEdge edge = edgeKVP.Key;\n");
		sb.AppendFront("GRGEN_LIBGR.INode source = edge.Source==oldNode ? newNode : edge.Source;\n");
		sb.AppendFront("GRGEN_LIBGR.INode target = edge.Target==oldNode ? newNode : edge.Target;\n");
		GenIndexMaintainingEdgeAdded(index);
		sb.Unindent();
		sb.AppendFront("}\n");
		//sb.append("Check(root);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
	}

	internal virtual void GenIndexMaintainingEdgeAdded(IncidenceCountIndex index)
	{
		string startNodeType = FormatElementInterfaceRef(index.StartNodeType);
		string adjacentNodeType = FormatElementInterfaceRef(index.AdjacentNodeType);

		if(index.Direction() == Direction.OUTGOING)
		{
			sb.AppendFront("if(source is " + startNodeType + " && target is " + adjacentNodeType + ") {\n");
			sb.Indent();
			sb.AppendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], "
					+ "(" + startNodeType + ")source);\n");
			sb.AppendFront("nodeToIncidenceCount[(" + startNodeType + ")source] = "
					+ "nodeToIncidenceCount[(" + startNodeType + ")source] + 1;\n");
			sb.AppendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], "
					+ "(" + startNodeType + ")source);\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}
		else if(index.Direction() == Direction.INCOMING)
		{
			sb.AppendFront("if(target is " + startNodeType + " && source is " + adjacentNodeType + ") {\n");
			sb.Indent();
			sb.AppendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], "
					+ "(" + startNodeType + ")target);\n");
			sb.AppendFront("nodeToIncidenceCount[(" + startNodeType + ")target] = "
					+ "nodeToIncidenceCount[(" + startNodeType + ")target] + 1;\n");
			sb.AppendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], "
					+ "(" + startNodeType + ")target);\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}
		else
		{
			sb.AppendFront("if(source is " + startNodeType + " && target is " + adjacentNodeType + ") {\n");
			sb.Indent();
			sb.AppendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], "
					+ "(" + startNodeType + ")source);\n");
			sb.AppendFront("nodeToIncidenceCount[(" + startNodeType + ")source] = "
					+ "nodeToIncidenceCount[(" + startNodeType + ")source] + 1;\n");
			sb.AppendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], "
					+ "(" + startNodeType + ")source);\n");
			sb.Unindent();
			sb.AppendFront("}\n");
			sb.AppendFront("if(target is " + startNodeType + " && source is " + adjacentNodeType + " && source!=target) {\n");
			sb.Indent();
			sb.AppendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], "
					+ "(" + startNodeType + ")target);\n");
			sb.AppendFront("nodeToIncidenceCount[(" + startNodeType + ")target] = "
					+ "nodeToIncidenceCount[(" + startNodeType + ")target] + 1;\n");
			sb.AppendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], "
					+ "(" + startNodeType + ")target);\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}
	}

	internal virtual void GenIndexMaintainingRemovingEdge(IncidenceCountIndex index)
	{
		string startNodeType = FormatElementInterfaceRef(index.StartNodeType);
		string adjacentNodeType = FormatElementInterfaceRef(index.AdjacentNodeType);

		if(index.Direction() == Direction.OUTGOING)
		{
			sb.AppendFront("if(source is " + startNodeType + " && target is " + adjacentNodeType + ") {\n");
			sb.Indent();
			sb.AppendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], "
					+ "(" + startNodeType + ")source);\n");
			sb.AppendFront("nodeToIncidenceCount[(" + startNodeType + ")source] = "
					+ "nodeToIncidenceCount[(" + startNodeType + ")source] - 1;\n");
			sb.AppendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], "
					+ "(" + startNodeType + ")source);\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}
		else if(index.Direction() == Direction.INCOMING)
		{
			sb.AppendFront("if(target is " + startNodeType + " && source is " + adjacentNodeType + ") {\n");
			sb.Indent();
			sb.AppendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], "
					+ "(" + startNodeType + ")target);\n");
			sb.AppendFront("nodeToIncidenceCount[(" + startNodeType + ")target] = "
					+ "nodeToIncidenceCount[("+ startNodeType + ")target] - 1;\n");
			sb.AppendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], "
					+ "(" + startNodeType + ")target);\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}
		else
		{
			sb.AppendFront("if(source is " + startNodeType + " && target is " + adjacentNodeType + ") {\n");
			sb.Indent();
			sb.AppendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], "
					+ "(" + startNodeType + ")source);\n");
			sb.AppendFront("nodeToIncidenceCount[(" + startNodeType + ")source] = "
					+ "nodeToIncidenceCount[(" + startNodeType + ")source] - 1;\n");
			sb.AppendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], "
					+ "(" + startNodeType + ")source);\n");
			sb.Unindent();
			sb.AppendFront("}\n");
			sb.AppendFront("if(target is " + startNodeType + " && source is " + adjacentNodeType + " && source!=target) {\n");
			sb.Indent();
			sb.AppendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], "
					+ "(" + startNodeType + ")target);\n");
			sb.AppendFront("nodeToIncidenceCount[(" + startNodeType + ")target] = "
					+ "nodeToIncidenceCount[(" + startNodeType + ")target] - 1;\n");
			sb.AppendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], "
					+ "(" + startNodeType + ")target);\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}
	}

	internal virtual void GenIndexAATreeBalancingInsertionDeletion(IncidenceCountIndex index)
	{
		string graphElementType = FormatElementInterfaceRef(index.StartNodeType);
		string castForUnique = " as GRGEN_LGSP.LGSPNodeWithUniqueId";

		sb.AppendFront("private void Skew(ref TreeNode current)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(current.level != current.left.level)\n");
		sb.AppendFrontIndented("return;\n");
		sb.Append("\n");
		sb.AppendFront("// rotate right\n");
		sb.AppendFront("TreeNode left = current.left;\n");
		sb.AppendFront("current.left = left.right;\n");
		sb.AppendFront("left.right = current;\n");
		sb.AppendFront("current = left;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("private void Split(ref TreeNode current)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(current.right.right.level != current.level)\n");
		sb.AppendFrontIndented("return;\n");
		sb.Append("\n");
		sb.AppendFront("// rotate left\n");
		sb.AppendFront("TreeNode right = current.right;\n");
		sb.AppendFront("current.right = right.left;\n");
		sb.AppendFront("right.left = current;\n");
		sb.AppendFront("current = right;\n");
		sb.AppendFront("++current.level;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("private void Insert(ref TreeNode current, int key, " + graphElementType + " value)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(current == bottom)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("current = new TreeNode(key, value, bottom);\n");
		sb.AppendFront("++count;\n");
		sb.AppendFront("++version;\n");
		sb.AppendFront("return;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
		sb.AppendFront("if(key < current.key");
		sb.AppendFront(" || ( key == current.key && (value" + castForUnique + ").uniqueId < "
				+ "(current.value" + castForUnique + ").uniqueId ) )\n");
		sb.AppendFrontIndented("Insert(ref current.left, key, value);\n");
		sb.AppendFront("else if(key > current.key");
		sb.AppendFront(" || ( key == current.key && (value" + castForUnique + ").uniqueId > "
				+ "(current.value" + castForUnique + ").uniqueId ) )\n");
		sb.AppendFrontIndented("Insert(ref current.right, key, value);\n");
		sb.AppendFront("else\n");
		sb.AppendFrontIndented("throw new Exception(\"Insertion of already available element\");\n");
		sb.Append("\n");
		sb.AppendFront("Skew(ref current);\n");
		sb.AppendFront("Split(ref current);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");

		sb.AppendFront("private void Delete(ref TreeNode current, int key, " + graphElementType + " value)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("if(current == bottom)\n");
		sb.AppendFrontIndented("return;\n");
		sb.Append("\n");
		sb.AppendFront("// search down the tree (and set pointer last and deleted)\n");
		sb.AppendFront("last = current;\n");
		sb.AppendFront("if(key < current.key");
		sb.Append(" || ( key == current.key && (value" + castForUnique + ").uniqueId < "
				+ "(current.value" + castForUnique + ").uniqueId ) )\n");
		sb.AppendFrontIndented("Delete(ref current.left, key, value);\n");
		sb.AppendFront("else\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("deleted = current;\n");
		sb.AppendFront("Delete(ref current.right, key, value);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
		sb.AppendFront("// at the bottom of the tree we remove the element (if present)\n");
		sb.AppendFront("if(current == last && deleted != bottom && key == deleted.key");
		sb.AppendFront(" && (value" + castForUnique + ").uniqueId"
				+ " == (deleted.value" + castForUnique + ").uniqueId )\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("deleted.value = current.value;\n");
		sb.AppendFront("deleted.key = current.key;\n");
		sb.AppendFront("deleted = bottom;\n");
		sb.AppendFront("current = current.right;\n");
		sb.AppendFront("--count;\n");
		sb.AppendFront("++version;\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.AppendFront("// on the way back, we rebalance\n");
		sb.AppendFront("else if(current.left.level < current.level - 1\n");
		sb.AppendFrontIndented("|| current.right.level < current.level - 1)\n");
		sb.AppendFront("{\n");
		sb.Indent();
		sb.AppendFront("--current.level;\n");
		sb.AppendFront("if(current.right.level > current.level)\n");
		sb.AppendFrontIndented("current.right.level = current.level;\n");
		sb.AppendFront("Skew(ref current);\n");
		sb.AppendFront("Skew(ref current.right);\n");
		sb.AppendFront("Skew(ref current.right.right);\n");
		sb.AppendFront("Split(ref current);\n");
		sb.AppendFront("Split(ref current.right);\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Unindent();
		sb.AppendFront("}\n");
		sb.Append("\n");
	}

	protected internal override void GenQualAccess(SourceBuilder sb, Qualification qual, object modifyGenerationState)
	{
		// needed because of inheritance, maybe todo: remove
	}

	protected internal override void GenMemberAccess(SourceBuilder sb, Entity member)
	{
		// needed because of inheritance, maybe todo: remove
	}

	///////////////////////
	// Private variables //
	///////////////////////

	private Model model;
	private SourceBuilder sb = null;
}

}
