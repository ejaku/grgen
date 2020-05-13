/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Generates the index part of the SearchPlanBackend2 model.
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.be.Csharp;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.exprevals.*;
import de.unika.ipd.grgen.util.SourceBuilder;

public class ModelIndexGen extends CSharpBase
{
	public ModelIndexGen(Model model, SourceBuilder sb, String nodeTypePrefix, String edgeTypePrefix)
	{
		super(nodeTypePrefix, edgeTypePrefix);
		this.model = model;
		this.sb = sb;
	}

	////////////////////////////
	// Index generation //
	////////////////////////////

	void genIndexTypes()
	{
		for(Index index : model.getIndices()) {
			genIndexType(index);
		}
	}

	void genIndexType(Index index)
	{
		String indexName = index.getIdent().toString();
		String lookupType = index instanceof AttributeIndex ? formatAttributeType(((AttributeIndex)index).entity) : "int";
		String graphElementType = index instanceof AttributeIndex
				? formatElementInterfaceRef(((AttributeIndex)index).type)
				: formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());
		if(index instanceof AttributeIndex) {
			sb.appendFront("interface Index" + indexName + " : GRGEN_LIBGR.IAttributeIndex\n");
		} else if(index instanceof IncidenceCountIndex) {
			sb.appendFront("interface Index" + indexName + " : GRGEN_LIBGR.IIncidenceCountIndex\n");
		}
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("IEnumerable<" + graphElementType + "> Lookup("
				+ lookupType + " fromto);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscending();\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromInclusive("
				+ lookupType + " from);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromExclusive("
				+ lookupType + " from);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscendingToInclusive("
				+ lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscendingToExclusive("
				+ lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromInclusiveToInclusive("
				+ lookupType + " from, " + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromInclusiveToExclusive("
				+ lookupType + " from, " + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromExclusiveToInclusive("
				+ lookupType + " from, " + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromExclusiveToExclusive("
				+ lookupType + " from, " + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescending();\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromInclusive("
				+ lookupType + " from);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromExclusive("
				+ lookupType + " from);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescendingToInclusive("
				+ lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescendingToExclusive("
				+ lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromInclusiveToInclusive("
				+ lookupType + " from, " + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromInclusiveToExclusive("
				+ lookupType + " from, " + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromExclusiveToInclusive("
				+ lookupType + " from, " + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromExclusiveToExclusive("
				+ lookupType + " from, " + lookupType + " to);\n");
		if(index instanceof IncidenceCountIndex) {
			sb.appendFront("int GetIncidenceCount(" + graphElementType + " element);\n");
		}
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genIndexImplementations()
	{
		int i = 0;
		for(Index index : model.getIndices()) {
			if(index instanceof AttributeIndex) {
				genIndexImplementation((AttributeIndex)index, i);
			} else {
				genIndexImplementation((IncidenceCountIndex)index, i);
			}
			++i;
		}
	}

	void genIndexImplementation(AttributeIndex index, int indexNum)
	{
		String indexName = index.getIdent().toString();
		String graphElementType = formatElementInterfaceRef(index.type);
		String modelName = model.getIdent().toString() + "GraphModel";
		sb.appendFront("public class Index" + indexName + "Impl : Index" + indexName + "\n");
		sb.appendFront("{\n");
		sb.indent();

		sb.appendFront("public GRGEN_LIBGR.IndexDescription Description { get { return "
				+ modelName + ".GetIndexDescription(" + indexNum + "); } }\n");
		sb.append("\n");

		sb.appendFront("protected class TreeNode\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("// search tree structure\n");
		sb.appendFront("public TreeNode left;\n");
		sb.appendFront("public TreeNode right;\n");
		sb.appendFront("public int level;\n");
		sb.append("\n");
		sb.appendFront("// user data\n");
		sb.appendFront("public " + graphElementType + " value;\n");
		sb.append("\n");
		sb.appendFront("// for the bottom node, operating as sentinel\n");
		sb.appendFront("public TreeNode()\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("left = this;\n");
		sb.appendFront("right = this;\n");
		sb.appendFront("level = 0;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
		sb.appendFront("// for regular nodes (that are born as leaf nodes)\n");
		sb.appendFront("public TreeNode(" + graphElementType + " value, TreeNode bottom)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("left = bottom;\n");
		sb.appendFront("right = bottom;\n");
		sb.appendFront("level = 1;\n");
		sb.append("\n");
		sb.appendFront("this.value = value;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
		sb.appendFront("// for copy constructing from other index\n");
		sb.appendFront("public TreeNode(TreeNode left, TreeNode right, int level, " + graphElementType + " value)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("this.left = left;\n");
		sb.appendFront("this.right = right;\n");
		sb.appendFront("this.level = level;\n");
		sb.append("\n");
		sb.appendFront("this.value = value;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("protected TreeNode root;\n");
		sb.appendFront("protected TreeNode bottom;\n");
		sb.appendFront("protected TreeNode deleted;\n");
		sb.appendFront("protected TreeNode last;\n");
		sb.appendFront("protected int count;\n");
		sb.appendFront("protected int version;\n");
		sb.append("\n");

		genEqualElementEntry(index);
		genEqualEntry(index);

		genAscendingElementEntry(index, false, true, false, true);
		genAscendingEntry(index, false, true, false, true);
		genAscendingElementEntry(index, true, true, false, true);
		genAscendingEntry(index, true, true, false, true);
		genAscendingElementEntry(index, true, false, false, true);
		genAscendingEntry(index, true, false, false, true);
		genAscendingElementEntry(index, false, true, true, true);
		genAscendingEntry(index, false, true, true, true);
		genAscendingElementEntry(index, false, true, true, false);
		genAscendingEntry(index, false, true, true, false);
		genAscendingElementEntry(index, true, true, true, true);
		genAscendingEntry(index, true, true, true, true);
		genAscendingElementEntry(index, true, true, true, false);
		genAscendingEntry(index, true, true, true, false);
		genAscendingElementEntry(index, true, false, true, true);
		genAscendingEntry(index, true, false, true, true);
		genAscendingElementEntry(index, true, false, true, false);
		genAscendingEntry(index, true, false, true, false);

		genDescendingElementEntry(index, false, true, false, true);
		genDescendingEntry(index, false, true, false, true);
		genDescendingElementEntry(index, true, true, false, true);
		genDescendingEntry(index, true, true, false, true);
		genDescendingElementEntry(index, true, false, false, true);
		genDescendingEntry(index, true, false, false, true);
		genDescendingElementEntry(index, false, true, true, true);
		genDescendingEntry(index, false, true, true, true);
		genDescendingElementEntry(index, false, true, true, false);
		genDescendingEntry(index, false, true, true, false);
		genDescendingElementEntry(index, true, true, true, true);
		genDescendingEntry(index, true, true, true, true);
		genDescendingElementEntry(index, true, true, true, false);
		genDescendingEntry(index, true, true, true, false);
		genDescendingElementEntry(index, true, false, true, true);
		genDescendingEntry(index, true, false, true, true);
		genDescendingElementEntry(index, true, false, true, false);
		genDescendingEntry(index, true, false, true, false);

		genEqual(index);

		genAscending(index, false, true, false, true);
		genAscending(index, true, true, false, true);
		genAscending(index, true, false, false, true);
		genAscending(index, false, true, true, true);
		genAscending(index, false, true, true, false);
		genAscending(index, true, true, true, true);
		genAscending(index, true, true, true, false);
		genAscending(index, true, false, true, true);
		genAscending(index, true, false, true, false);

		genDescending(index, false, true, false, true);
		genDescending(index, true, true, false, true);
		genDescending(index, true, false, false, true);
		genDescending(index, false, true, true, true);
		genDescending(index, false, true, true, false);
		genDescending(index, true, true, true, true);
		genDescending(index, true, true, true, false);
		genDescending(index, true, false, true, true);
		genDescending(index, true, false, true, false);

		sb.appendFront("public Index" + indexName + "Impl(GRGEN_LGSP.LGSPGraph graph)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("this.graph = graph;\n");
		sb.append("\n");
		sb.appendFront("// initialize AA tree used to implement the index\n");
		sb.appendFront("bottom = new TreeNode();\n");
		sb.appendFront("root = bottom;\n");
		sb.appendFront("deleted = bottom;\n");
		sb.appendFront("count = 0;\n");
		sb.appendFront("version = 0;\n");
		sb.append("\n");
		sb.appendFront("graph.OnClearingGraph += ClearingGraph;\n");
		if(index.type instanceof NodeType) {
			sb.appendFront("graph.OnNodeAdded += Added;\n");
			sb.appendFront("graph.OnRemovingNode += Removing;\n");
			sb.appendFront("graph.OnChangingNodeAttribute += ChangingAttribute;\n");
			sb.appendFront("graph.OnRetypingNode += Retyping;\n");
		} else {
			sb.appendFront("graph.OnEdgeAdded += Added;\n");
			sb.appendFront("graph.OnRemovingEdge += Removing;\n");
			sb.appendFront("graph.OnChangingEdgeAttribute += ChangingAttribute;\n");
			sb.appendFront("graph.OnRetypingEdge += Retyping;\n");
		}
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("public void FillAsClone(Index" + indexName + "Impl that, "
				+ "IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("root = FillAsClone(that.root, that.bottom, oldToNewMap);\n");
		sb.appendFront("count = that.count;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("protected TreeNode FillAsClone(TreeNode that, TreeNode otherBottom, "
				+ "IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(that == otherBottom)\n");
		sb.appendFront("\treturn bottom;\n");
		sb.appendFront("else\n");
		sb.indent();
		sb.appendFront("return new TreeNode(\n");
		sb.indent();
		sb.appendFront("FillAsClone(that.left, otherBottom, oldToNewMap),\n");
		sb.appendFront("FillAsClone(that.right, otherBottom, oldToNewMap),\n");
		sb.appendFront("that.level,\n");
		sb.appendFront("(" + graphElementType + ")oldToNewMap[that.value]\n");
		sb.unindent();
		sb.appendFront(");\n");
		sb.unindent();
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		genIndexMaintainingEventHandlers(index);

		genIndexAATreeBalancingInsertionDeletion(index);

		sb.appendFront("private GRGEN_LGSP.LGSPGraph graph;\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genEqualElementEntry(Index index)
	{
		String attributeType = index instanceof AttributeIndex ? formatAttributeType(((AttributeIndex)index).entity) : "int";

		sb.appendFront("public IEnumerable<GRGEN_LIBGR.IGraphElement> LookupElements(object fromto)\n");
		sb.appendFront("{\n");
		sb.indent();

		sb.appendFront("int versionAtIterationBegin = version;\n");
		sb.appendFront("foreach(GRGEN_LIBGR.IGraphElement value in Lookup(root, (" + attributeType + ")fromto))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genEqualEntry(Index index)
	{
		String attributeType = index instanceof AttributeIndex ? formatAttributeType(((AttributeIndex)index).entity) : "int";
		String graphElementType = index instanceof AttributeIndex
				? formatElementInterfaceRef(((AttributeIndex)index).type)
				: formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());

		sb.appendFront("public IEnumerable<" + graphElementType + "> Lookup(" + attributeType + " fromto)\n");
		sb.appendFront("{\n");
		sb.indent();

		sb.appendFront("int versionAtIterationBegin = version;\n");
		sb.appendFront("foreach(" + graphElementType + " value in Lookup(root, fromto))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genEqual(AttributeIndex index)
	{
		String attributeType = formatAttributeType(index.entity);
		String attributeName = index.entity.getIdent().toString();
		String graphElementType = formatElementInterfaceRef(index.type);

		sb.appendFront("private IEnumerable<" + graphElementType + "> Lookup(TreeNode current, "
				 + attributeType + " fromto)\n");
		sb.appendFront("{\n");
		sb.indent();

		sb.appendFront("if(current == bottom)\n");
		sb.appendFront("\tyield break;\n");
		sb.append("\n");
		sb.appendFront("int versionAtIterationBegin = version;\n");
		sb.append("\n");
		sb.appendFront("// don't go left if the value is already lower than fromto\n");
		if(index.entity.getType() instanceof BooleanType)
			sb.appendFront("if(current.value." + attributeName + ".CompareTo(fromto)>=0)\n");
		else if(index.entity.getType() instanceof StringType) {
			sb.appendFront("if(String.Compare(current.value." + attributeName
					+ ", fromto, StringComparison.InvariantCulture)>=0)\n");
		} else
			sb.appendFront("if(current.value." + attributeName + " >= fromto)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("foreach(" + graphElementType + " value in Lookup(current.left, fromto))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("// (only) yield a value that is equal to fromto\n");
		sb.appendFront("if(current.value." + attributeName + " == fromto)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("// the value is within range.\n");
		sb.appendFront("yield return current.value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("// don't go right if the value is already higher than fromto\n");
		if(index.entity.getType() instanceof BooleanType)
			sb.appendFront("if(current.value." + attributeName + ".CompareTo(fromto)<=0)\n");
		else if(index.entity.getType() instanceof StringType) {
			sb.appendFront("if(String.Compare(current.value." + attributeName
					+ ", fromto, StringComparison.InvariantCulture)<=0)\n");
		} else
			sb.appendFront("if(current.value." + attributeName + " <= fromto)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("foreach(" + graphElementType + " value in Lookup(current.right, fromto))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genAscendingElementEntry(Index index, boolean fromConstrained, boolean fromInclusive,
			boolean toConstrained, boolean toInclusive)
	{
		String attributeType = index instanceof AttributeIndex ? formatAttributeType(((AttributeIndex)index).entity) : "int";

		String lookupMethodNameAppendix = "Ascending";
		if(fromConstrained) {
			lookupMethodNameAppendix += "From";
			if(fromInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}
		if(toConstrained) {
			lookupMethodNameAppendix += "To";
			if(toInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}

		sb.appendFront("public IEnumerable<GRGEN_LIBGR.IGraphElement> LookupElements" + lookupMethodNameAppendix + "(");
		if(fromConstrained)
			sb.append("object from");
		if(fromConstrained && toConstrained)
			sb.append(", ");
		if(toConstrained)
			sb.append("object to");
		sb.append(")\n");
		sb.appendFront("{\n");
		sb.indent();

		sb.appendFront("int versionAtIterationBegin = version;\n");
		sb.appendFront("foreach(GRGEN_LIBGR.IGraphElement value in Lookup" + lookupMethodNameAppendix + "(root");
		if(fromConstrained)
			sb.append(", (" + attributeType + ")from");
		if(toConstrained)
			sb.append(", (" + attributeType + ")to");
		sb.append("))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genAscendingEntry(Index index, boolean fromConstrained, boolean fromInclusive,
			boolean toConstrained, boolean toInclusive)
	{
		String attributeType = index instanceof AttributeIndex ? formatAttributeType(((AttributeIndex)index).entity) : "int";
		String graphElementType = index instanceof AttributeIndex
				? formatElementInterfaceRef(((AttributeIndex)index).type)
				: formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());

		String lookupMethodNameAppendix = "Ascending";
		if(fromConstrained) {
			lookupMethodNameAppendix += "From";
			if(fromInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}
		if(toConstrained) {
			lookupMethodNameAppendix += "To";
			if(toInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}

		sb.appendFront("public IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix + "(");
		if(fromConstrained)
			sb.append(attributeType + " from");
		if(fromConstrained && toConstrained)
			sb.append(", ");
		if(toConstrained)
			sb.append(attributeType + " to");
		sb.append(")\n");
		sb.appendFront("{\n");
		sb.indent();

		sb.appendFront("int versionAtIterationBegin = version;\n");
		sb.appendFront("foreach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(root");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genAscending(AttributeIndex index, boolean fromConstrained, boolean fromInclusive,
			boolean toConstrained, boolean toInclusive)
	{
		String attributeType = formatAttributeType(index.entity);
		String attributeName = index.entity.getIdent().toString();
		String graphElementType = formatElementInterfaceRef(index.type);

		String lookupMethodNameAppendix = "Ascending";
		if(fromConstrained) {
			lookupMethodNameAppendix += "From";
			if(fromInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}
		if(toConstrained) {
			lookupMethodNameAppendix += "To";
			if(toInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}

		sb.appendFront("private IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix
				+ "(TreeNode current");
		if(fromConstrained)
			sb.append(", " + attributeType + " from");
		if(toConstrained)
			sb.append(", " + attributeType + " to");
		sb.append(")\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(current == bottom)\n");
		sb.appendFront("\tyield break;\n");
		sb.append("\n");
		sb.appendFront("int versionAtIterationBegin = version;\n");
		sb.append("\n");
		if(fromConstrained) {
			sb.appendFront("// don't go left if the value is already lower than from\n");
			if(index.entity.getType() instanceof BooleanType) {
				sb.appendFront("if(current.value." + attributeName + ".CompareTo(from)"
						+ (fromInclusive ? " >= " : " > ") + "0)\n");
			} else if(index.entity.getType() instanceof StringType) {
				sb.appendFront("if(String.Compare(current.value." + attributeName
						+ ", from, StringComparison.InvariantCulture)" + (fromInclusive ? " >= " : " > ") + "0)\n");
			} else
				sb.appendFront("if(current.value." + attributeName + (fromInclusive ? " >= " : " > ") + "from)\n");
		}
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("foreach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.left");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		if(fromConstrained || toConstrained) {
			sb.appendFront("// (only) yield a value that is within bounds\n");
			sb.appendFront("if(");
			if(fromConstrained) {
				if(index.entity.getType() instanceof BooleanType) {
					sb.append("current.value." + attributeName + ".CompareTo(from)"
							+ (fromInclusive ? " >= " : " > ") + "0");
				} else if(index.entity.getType() instanceof StringType) {
					sb.append("String.Compare(current.value." + attributeName
							+ ", from, StringComparison.InvariantCulture)" + (fromInclusive ? " >= " : " > ") + "0");
				} else
					sb.append("current.value." + attributeName + (fromInclusive ? " >= " : " > ") + "from");
			}
			if(fromConstrained && toConstrained)
				sb.append(" && ");
			if(toConstrained) {
				if(index.entity.getType() instanceof BooleanType) {
					sb.append("current.value." + attributeName + ".CompareTo(to)"
							 + (toInclusive ? " <= " : " < ") + "0");
				} else if(index.entity.getType() instanceof StringType)
					sb.append("String.Compare(current.value." + attributeName
							+ ", to, StringComparison.InvariantCulture)" + (toInclusive ? " <= " : " < ") + "0");
				else
					sb.append("current.value." + attributeName + (toInclusive ? " <= " : " < ") + "to");
			}
			sb.append(")\n");
		}
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("// the value is within range.\n");
		sb.appendFront("yield return current.value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		if(toConstrained) {
			sb.appendFront("// don't go right if the value is already higher than to\n");
			if(index.entity.getType() instanceof BooleanType) {
				sb.appendFront("if(current.value." + attributeName + ".CompareTo(to)"
						+ (toInclusive ? " <= " : " < ") + "0)\n");
			} else if(index.entity.getType() instanceof StringType) {
				sb.appendFront("if(String.Compare(current.value." + attributeName
						+ ", to, StringComparison.InvariantCulture)" + (toInclusive ? " <= " : " < ") + "0)\n");
			} else
				sb.appendFront("if(current.value." + attributeName + (toInclusive ? " <= " : " < ") + "to)\n");
		}
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("foreach(" + graphElementType + " value in Lookup"
				+ lookupMethodNameAppendix + "(current.right");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genDescendingElementEntry(Index index, boolean fromConstrained, boolean fromInclusive,
			boolean toConstrained, boolean toInclusive)
	{
		String attributeType = index instanceof AttributeIndex ? formatAttributeType(((AttributeIndex)index).entity) : "int";

		String lookupMethodNameAppendix = "Descending";
		if(fromConstrained) {
			lookupMethodNameAppendix += "From";
			if(fromInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}
		if(toConstrained) {
			lookupMethodNameAppendix += "To";
			if(toInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}

		sb.appendFront("public IEnumerable<GRGEN_LIBGR.IGraphElement> LookupElements" + lookupMethodNameAppendix + "(");
		if(fromConstrained)
			sb.append("object from");
		if(fromConstrained && toConstrained)
			sb.append(", ");
		if(toConstrained)
			sb.append("object to");
		sb.append(")\n");
		sb.appendFront("{\n");
		sb.indent();

		sb.appendFront("int versionAtIterationBegin = version;\n");
		sb.appendFront("foreach(GRGEN_LIBGR.IGraphElement value in Lookup" + lookupMethodNameAppendix + "(root");
		if(fromConstrained)
			sb.append(", (" + attributeType + ")from");
		if(toConstrained)
			sb.append(", (" + attributeType + ")to");
		sb.append("))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genDescendingEntry(Index index, boolean fromConstrained, boolean fromInclusive,
			boolean toConstrained, boolean toInclusive)
	{
		String attributeType = index instanceof AttributeIndex ? formatAttributeType(((AttributeIndex)index).entity) : "int";
		String graphElementType = index instanceof AttributeIndex
				? formatElementInterfaceRef(((AttributeIndex)index).type)
				: formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());

		String lookupMethodNameAppendix = "Descending";
		if(fromConstrained) {
			lookupMethodNameAppendix += "From";
			if(fromInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}
		if(toConstrained) {
			lookupMethodNameAppendix += "To";
			if(toInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}

		sb.appendFront("public IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix + "(");
		if(fromConstrained)
			sb.append(attributeType + " from");
		if(fromConstrained && toConstrained)
			sb.append(", ");
		if(toConstrained)
			sb.append(attributeType + " to");
		sb.append(")\n");
		sb.appendFront("{\n");

		sb.indent();
		sb.appendFront("int versionAtIterationBegin = version;\n");
		sb.appendFront("foreach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(root");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genDescending(AttributeIndex index, boolean fromConstrained, boolean fromInclusive,
			boolean toConstrained, boolean toInclusive)
	{
		String attributeType = formatAttributeType(index.entity);
		String attributeName = index.entity.getIdent().toString();
		String graphElementType = formatElementInterfaceRef(index.type);

		String lookupMethodNameAppendix = "Descending";
		if(fromConstrained) {
			lookupMethodNameAppendix += "From";
			if(fromInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}
		if(toConstrained) {
			lookupMethodNameAppendix += "To";
			if(toInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}

		sb.appendFront("private IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix
				+ "(TreeNode current");
		if(fromConstrained)
			sb.append(", " + attributeType + " from");
		if(toConstrained)
			sb.append(", " + attributeType + " to");
		sb.append(")\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(current == bottom)\n");
		sb.appendFront("\tyield break;\n");
		sb.append("\n");
		sb.appendFront("int versionAtIterationBegin = version;\n");
		sb.append("\n");
		if(fromConstrained) {
			sb.appendFront("// don't go left if the value is already lower than from\n");
			if(index.entity.getType() instanceof BooleanType) {
				sb.appendFront("if(current.value." + attributeName + ".CompareTo(from)"
						+ (fromInclusive ? " <= " : " < ") + "0)\n");
			} else if(index.entity.getType() instanceof StringType) {
				sb.appendFront("if(String.Compare(current.value." + attributeName
						+ ", from, StringComparison.InvariantCulture)" + (fromInclusive ? " <= " : " < ") + "0)\n");
			} else
				sb.appendFront("if(current.value." + attributeName + (fromInclusive ? " <= " : " < ") + "from)\n");
		}
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("foreach(" + graphElementType + " value in Lookup"
				+ lookupMethodNameAppendix + "(current.right");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		if(fromConstrained || toConstrained) {
			sb.appendFront("// (only) yield a value that is within bounds\n");
			sb.appendFront("if(");
			if(fromConstrained) {
				if(index.entity.getType() instanceof BooleanType) {
					sb.append("current.value." + attributeName + ".CompareTo(from)"
							+ (fromInclusive ? " <= " : " < ") + "0");
				} else if(index.entity.getType() instanceof StringType) {
					sb.append("String.Compare(current.value." + attributeName
							+ ", from, StringComparison.InvariantCulture)" + (fromInclusive ? " <= " : " < ") + "0");
				} else
					sb.append("current.value." + attributeName + (fromInclusive ? " <= " : " < ") + "from");
			}
			if(fromConstrained && toConstrained)
				sb.append(" && ");
			if(toConstrained) {
				if(index.entity.getType() instanceof BooleanType) {
					sb.append("current.value." + attributeName + ".CompareTo(to)"
							+ (toInclusive ? " >= " : " > ") + "0");
				} else if(index.entity.getType() instanceof StringType) {
					sb.append("String.Compare(current.value." + attributeName
							+ ", to, StringComparison.InvariantCulture)" + (toInclusive ? " >= " : " > ") + "0");
				} else
					sb.append("current.value." + attributeName + (toInclusive ? " >= " : " > ") + "to");
			}
			sb.append(")\n");
		}
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("// the value is within range.\n");
		sb.appendFront("yield return current.value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		if(toConstrained) {
			sb.appendFront("// don't go right if the value is already higher than to\n");
			if(index.entity.getType() instanceof BooleanType) {
				sb.appendFront("if(current.value." + attributeName + ".CompareTo(to)"
						+ (toInclusive ? " >= " : " > ") + "0)\n");
			} else if(index.entity.getType() instanceof StringType) {
				sb.appendFront("if(String.Compare(current.value." + attributeName
						+ ", to, StringComparison.InvariantCulture)" + (toInclusive ? " >= " : " > ") + "0)\n");
			} else
				sb.appendFront("if(current.value." + attributeName + (toInclusive ? " >= " : " > ") + "to)\n");
		}
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("foreach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.left");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genIndexMaintainingEventHandlers(AttributeIndex index)
	{
		String attributeType = formatAttributeType(index.entity);
		String attributeName = index.entity.getIdent().toString();
		String graphElementType = formatElementInterfaceRef(index.type);

		sb.appendFront("void ClearingGraph()\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("// ReInitialize AA tree to clear the index\n");
		sb.appendFront("bottom = new TreeNode();\n");
		sb.appendFront("root = bottom;\n");
		sb.appendFront("deleted = bottom;\n");
		sb.appendFront("last = null;\n");
		sb.appendFront("count = 0;\n");
		sb.appendFront("version = 0;\n");
		sb.unindent();
		sb.appendFront("}\n\n");

		sb.appendFront("void Added(GRGEN_LIBGR.IGraphElement elem)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(elem is " + graphElementType + ")\n");
		sb.appendFront("\tInsert(ref root, (" + graphElementType + ")elem, "
				+"((" + graphElementType + ")elem)." + attributeName + ");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
		sb.appendFront("void Removing(GRGEN_LIBGR.IGraphElement elem)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(elem is " + graphElementType + ")\n");
		sb.appendFront("\tDelete(ref root, (" + graphElementType + ")elem);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
		sb.appendFront("void ChangingAttribute(GRGEN_LIBGR.IGraphElement elem, "
				+ "GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.AttributeChangeType changeType, Object newValue, Object keyValue)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(elem is " + graphElementType + " && attrType.Name==\"" + attributeName + "\")\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("Delete(ref root, (" + graphElementType + ")elem);\n");
		sb.appendFront("Insert(ref root, (" + graphElementType + ")elem, (" + attributeType + ")newValue);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n\n");
		sb.append("\n");
		sb.appendFront("void Retyping(GRGEN_LIBGR.IGraphElement oldElem, GRGEN_LIBGR.IGraphElement newElem)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(oldElem is " + graphElementType + ")\n");
		sb.appendFront("\tDelete(ref root, (" + graphElementType + ")oldElem);\n");
		sb.appendFront("if(newElem is " + graphElementType + ")\n");
		sb.appendFront("\tInsert(ref root, (" + graphElementType + ")newElem, "
				+ "((" + graphElementType + ")newElem)." + attributeName + ");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genIndexAATreeBalancingInsertionDeletion(AttributeIndex index)
	{
		String attributeType = formatAttributeType(index.entity);
		String attributeName = index.entity.getIdent().toString();
		String graphElementType = formatElementInterfaceRef(index.type);
		String castForUnique = index.type instanceof NodeType ? " as GRGEN_LGSP.LGSPNode" : " as GRGEN_LGSP.LGSPEdge";

		sb.appendFront("private void Skew(ref TreeNode current)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(current.level != current.left.level)\n");
		sb.appendFront("\treturn;\n");
		sb.append("\n");
		sb.appendFront("// rotate right\n");
		sb.appendFront("TreeNode left = current.left;\n");
		sb.appendFront("current.left = left.right;\n");
		sb.appendFront("left.right = current;\n");
		sb.appendFront("current = left;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("private void Split(ref TreeNode current)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(current.right.right.level != current.level)\n");
		sb.appendFront("\treturn;\n");
		sb.append("\n");
		sb.appendFront("// rotate left\n");
		sb.appendFront("TreeNode right = current.right;\n");
		sb.appendFront("current.right = right.left;\n");
		sb.appendFront("right.left = current;\n");
		sb.appendFront("current = right;\n");
		sb.appendFront("++current.level;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("private void Insert(ref TreeNode current, " + graphElementType + " value, "
				+ attributeType + " attributeValue)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(current == bottom)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("current = new TreeNode(value, bottom);\n");
		sb.appendFront("++count;\n");
		sb.appendFront("++version;\n");
		sb.appendFront("return;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
		if(index.entity.getType() instanceof BooleanType)
			sb.appendFront("if(attributeValue.CompareTo(current.value." + attributeName + ")<0");
		else if(index.entity.getType() instanceof StringType) {
			sb.appendFront("if(String.Compare(attributeValue, current.value." + attributeName
					+ ", StringComparison.InvariantCulture)<0");
		} else
			sb.appendFront("if(attributeValue < current.value." + attributeName);
		sb.append(" || ( attributeValue == current.value." + attributeName + " && (value" + castForUnique
				+ ").uniqueId < (current.value" + castForUnique + ").uniqueId ) )\n");
		sb.appendFront("\tInsert(ref current.left, value, attributeValue);\n");
		if(index.entity.getType() instanceof BooleanType)
			sb.appendFront("else if(attributeValue.CompareTo(current.value." + attributeName + ")>0");
		else if(index.entity.getType() instanceof StringType) {
			sb.appendFront("else if(String.Compare(attributeValue, current.value." + attributeName
					+ ", StringComparison.InvariantCulture)>0");
		} else
			sb.appendFront("else if(attributeValue > current.value." + attributeName);
		sb.append(" || ( attributeValue == current.value." + attributeName + " && (value" + castForUnique
				+ ").uniqueId > (current.value" + castForUnique + ").uniqueId ) )\n");
		sb.appendFront("\tInsert(ref current.right, value, attributeValue);\n");
		sb.appendFront("else\n");
		sb.appendFront("\tthrow new Exception(\"Insertion of already available element\");\n");
		sb.append("\n");
		sb.appendFront("Skew(ref current);\n");
		sb.appendFront("Split(ref current);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("private void Delete(ref TreeNode current, " + graphElementType + " value)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(current == bottom)\n");
		sb.appendFront("\treturn;\n");
		sb.append("\n");
		sb.appendFront("// search down the tree (and set pointer last and deleted)\n");
		sb.appendFront("last = current;\n");
		if(index.entity.getType() instanceof BooleanType)
			sb.appendFront("if(value." + attributeName + ".CompareTo(current.value." + attributeName + ")<0");
		else if(index.entity.getType() instanceof StringType) {
			sb.appendFront("if(String.Compare(value." + attributeName + ", current.value." + attributeName
					+ ", StringComparison.InvariantCulture)<0");
		} else
			sb.appendFront("if(value." + attributeName + " < current.value." + attributeName);
		sb.append(" || ( value." + attributeName + " == current.value." + attributeName + " && (value" + castForUnique
				+ ").uniqueId < (current.value" + castForUnique + ").uniqueId ) )\n");
		sb.appendFront("\tDelete(ref current.left, value);\n");
		sb.appendFront("else\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("deleted = current;\n");
		sb.appendFront("Delete(ref current.right, value);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
		sb.appendFront("// at the bottom of the tree we remove the element (if present)\n");
		sb.appendFront("if(current == last && deleted != bottom && "
				+"value." + attributeName + " == deleted.value." + attributeName);
		sb.append(" && (value" + castForUnique + ").uniqueId == (deleted.value" + castForUnique + ").uniqueId )\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("deleted.value = current.value;\n");
		sb.appendFront("deleted = bottom;\n");
		sb.appendFront("current = current.right;\n");
		sb.appendFront("--count;\n");
		sb.appendFront("++version;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.appendFront("// on the way back, we rebalance\n");
		sb.appendFront("else if(current.left.level < current.level - 1\n");
		sb.appendFront("\t|| current.right.level < current.level - 1)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("--current.level;\n");
		sb.appendFront("if(current.right.level > current.level)\n");
		sb.appendFront("\tcurrent.right.level = current.level;\n");
		sb.appendFront("Skew(ref current);\n");
		sb.appendFront("Skew(ref current.right);\n");
		sb.appendFront("Skew(ref current.right.right);\n");
		sb.appendFront("Split(ref current);\n");
		sb.appendFront("Split(ref current.right);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genIndexSetType()
	{
		sb.appendFront("public class " + model.getIdent() + "IndexSet : GRGEN_LIBGR.IIndexSet\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("public " + model.getIdent() + "IndexSet(GRGEN_LGSP.LGSPGraph graph)\n");
		sb.appendFront("{\n");
		sb.indent();
		for(Index index : model.getIndices()) {
			String indexName = index.getIdent().toString();
			sb.appendFront(indexName + " = new Index" + indexName + "Impl(graph);\n");
		}
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		for(Index index : model.getIndices()) {
			String indexName = index.getIdent().toString();
			sb.appendFront("public Index" + indexName + "Impl " + indexName + ";\n");
		}
		sb.append("\n");

		sb.appendFront("public GRGEN_LIBGR.IIndex GetIndex(string indexName)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("switch(indexName)\n");
		sb.appendFront("{\n");
		sb.indent();
		for(Index index : model.getIndices()) {
			String indexName = index.getIdent().toString();
			sb.appendFront("case \"" + indexName + "\": return " + indexName + ";\n");
		}
		sb.appendFront("default: return null;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("public void FillAsClone(GRGEN_LGSP.LGSPGraph originalGraph, "
				+ "IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
		sb.appendFront("{\n");
		sb.indent();
		for(Index index : model.getIndices()) {
			String indexName = index.getIdent().toString();
			sb.appendFront(indexName + ".FillAsClone((Index" + indexName + "Impl)originalGraph.Indices.GetIndex(\""
					+ indexName + "\"), oldToNewMap);\n");
		}
		sb.unindent();
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
	}

	void genIndexImplementation(IncidenceCountIndex index, int indexNum)
	{
		String indexName = index.getIdent().toString();
		String graphElementType = formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());
		String modelName = model.getIdent().toString() + "GraphModel";
		sb.appendFront("public class Index" + indexName + "Impl : Index" + indexName + "\n");
		sb.appendFront("{\n");
		sb.indent();

		sb.appendFront("public GRGEN_LIBGR.IndexDescription Description { get { return "
				+ modelName + ".GetIndexDescription(" + indexNum + "); } }\n");
		sb.append("\n");

		sb.appendFront("protected class TreeNode\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("// search tree structure\n");
		sb.appendFront("public TreeNode left;\n");
		sb.appendFront("public TreeNode right;\n");
		sb.appendFront("public int level;\n");
		sb.append("\n");
		sb.appendFront("// user data\n");
		sb.appendFront("public int key;\n");
		sb.appendFront("public " + graphElementType + " value;\n");
		sb.append("\n");
		sb.appendFront("// for the bottom node, operating as sentinel\n");
		sb.appendFront("public TreeNode()\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("left = this;\n");
		sb.appendFront("right = this;\n");
		sb.appendFront("level = 0;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
		sb.appendFront("// for regular nodes (that are born as leaf nodes)\n");
		sb.appendFront("public TreeNode(int key, " + graphElementType + " value, TreeNode bottom)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("left = bottom;\n");
		sb.appendFront("right = bottom;\n");
		sb.appendFront("level = 1;\n");
		sb.append("\n");
		sb.appendFront("this.key = key;\n");
		sb.appendFront("this.value = value;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
		sb.appendFront("// for copy constructing from other index\n");
		sb.appendFront("public TreeNode(TreeNode left, TreeNode right, int level, int key, " + graphElementType + " value)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("this.left = left;\n");
		sb.appendFront("this.right = right;\n");
		sb.appendFront("this.level = level;\n");
		sb.append("\n");
		sb.appendFront("this.key = key;\n");
		sb.appendFront("this.value = value;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("protected TreeNode root;\n");
		sb.appendFront("protected TreeNode bottom;\n");
		sb.appendFront("protected TreeNode deleted;\n");
		sb.appendFront("protected TreeNode last;\n");
		sb.appendFront("protected int count;\n");
		sb.appendFront("protected int version;\n");
		sb.append("\n");
		sb.appendFront("protected IDictionary<" + graphElementType + ", int> nodeToIncidenceCount = "
				+ "new Dictionary<" + graphElementType + ", int>();\n");
		sb.append("\n");

		genEqualElementEntry(index);
		genEqualEntry(index);

		genAscendingElementEntry(index, false, true, false, true);
		genAscendingEntry(index, false, true, false, true);
		genAscendingElementEntry(index, true, true, false, true);
		genAscendingEntry(index, true, true, false, true);
		genAscendingElementEntry(index, true, false, false, true);
		genAscendingEntry(index, true, false, false, true);
		genAscendingElementEntry(index, false, true, true, true);
		genAscendingEntry(index, false, true, true, true);
		genAscendingElementEntry(index, false, true, true, false);
		genAscendingEntry(index, false, true, true, false);
		genAscendingElementEntry(index, true, true, true, true);
		genAscendingEntry(index, true, true, true, true);
		genAscendingElementEntry(index, true, true, true, false);
		genAscendingEntry(index, true, true, true, false);
		genAscendingElementEntry(index, true, false, true, true);
		genAscendingEntry(index, true, false, true, true);
		genAscendingElementEntry(index, true, false, true, false);
		genAscendingEntry(index, true, false, true, false);

		genDescendingElementEntry(index, false, true, false, true);
		genDescendingEntry(index, false, true, false, true);
		genDescendingElementEntry(index, true, true, false, true);
		genDescendingEntry(index, true, true, false, true);
		genDescendingElementEntry(index, true, false, false, true);
		genDescendingEntry(index, true, false, false, true);
		genDescendingElementEntry(index, false, true, true, true);
		genDescendingEntry(index, false, true, true, true);
		genDescendingElementEntry(index, false, true, true, false);
		genDescendingEntry(index, false, true, true, false);
		genDescendingElementEntry(index, true, true, true, true);
		genDescendingEntry(index, true, true, true, true);
		genDescendingElementEntry(index, true, true, true, false);
		genDescendingEntry(index, true, true, true, false);
		genDescendingElementEntry(index, true, false, true, true);
		genDescendingEntry(index, true, false, true, true);
		genDescendingElementEntry(index, true, false, true, false);
		genDescendingEntry(index, true, false, true, false);

		genEqual(index);

		genAscending(index, false, true, false, true);
		genAscending(index, true, true, false, true);
		genAscending(index, true, false, false, true);
		genAscending(index, false, true, true, true);
		genAscending(index, false, true, true, false);
		genAscending(index, true, true, true, true);
		genAscending(index, true, true, true, false);
		genAscending(index, true, false, true, true);
		genAscending(index, true, false, true, false);

		genDescending(index, false, true, false, true);
		genDescending(index, true, true, false, true);
		genDescending(index, true, false, false, true);
		genDescending(index, false, true, true, true);
		genDescending(index, false, true, true, false);
		genDescending(index, true, true, true, true);
		genDescending(index, true, true, true, false);
		genDescending(index, true, false, true, true);
		genDescending(index, true, false, true, false);

		genGetIncidenceCount(index);

		sb.appendFront("public Index" + indexName + "Impl(GRGEN_LGSP.LGSPGraph graph)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("this.graph = graph;\n");
		sb.append("\n");
		sb.appendFront("// initialize AA tree used to implement the index\n");
		sb.appendFront("bottom = new TreeNode();\n");
		sb.appendFront("root = bottom;\n");
		sb.appendFront("deleted = bottom;\n");
		sb.appendFront("count = 0;\n");
		sb.appendFront("version = 0;\n");
		sb.append("\n");
		sb.appendFront("graph.OnClearingGraph += ClearingGraph;\n");
		sb.appendFront("graph.OnEdgeAdded += EdgeAdded;\n");
		sb.appendFront("graph.OnNodeAdded += NodeAdded;\n");
		sb.appendFront("graph.OnRemovingEdge += RemovingEdge;\n");
		sb.appendFront("graph.OnRemovingNode += RemovingNode;\n");
		sb.appendFront("graph.OnRetypingEdge += RetypingEdge;\n");
		sb.appendFront("graph.OnRetypingNode += RetypingNode;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("public void FillAsClone(Index" + indexName + "Impl that, "
				+ "IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("root = FillAsClone(that.root, that.bottom, oldToNewMap);\n");
		sb.appendFront("count = that.count;\n");
		sb.appendFront("foreach(KeyValuePair<" + graphElementType + ", int> ntic in that.nodeToIncidenceCount)\n");
		sb.appendFront("\tnodeToIncidenceCount.Add((" + graphElementType + ")oldToNewMap[ntic.Key], ntic.Value);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.append("\n");

		sb.appendFront("protected TreeNode FillAsClone(TreeNode that, TreeNode otherBottom, "
				+ "IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(that == otherBottom)\n");
		sb.appendFront("\treturn bottom;\n");
		sb.appendFront("else\n");
		sb.indent();
		sb.appendFront("return new TreeNode(\n");
		sb.indent();
		sb.appendFront("FillAsClone(that.left, otherBottom, oldToNewMap),\n");
		sb.appendFront("FillAsClone(that.right, otherBottom, oldToNewMap),\n");
		sb.appendFront("that.level,\n");
		sb.appendFront("that.key,\n");
		sb.appendFront("(" + graphElementType + ")oldToNewMap[that.value]\n");
		sb.unindent();
		sb.unindent();
		sb.append(");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		genIndexMaintainingEventHandlers(index);

		genIndexAATreeBalancingInsertionDeletion(index);

		//genCheckDump(index);

		sb.appendFront("private GRGEN_LGSP.LGSPGraph graph;\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genEqual(IncidenceCountIndex index)
	{
		String graphElementType = formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());

		sb.appendFront("private IEnumerable<" + graphElementType + "> Lookup(TreeNode current, int fromto)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(current == bottom)\n");
		sb.appendFront("\tyield break;\n");
		sb.append("\n");
		sb.appendFront("int versionAtIterationBegin = version;\n");
		sb.append("\n");
		sb.appendFront("// don't go left if the value is already lower than fromto\n");
		sb.appendFront("if(current.key >= fromto)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("foreach(" + graphElementType + " value in Lookup(current.left, fromto))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("// (only) yield a value that is equal to fromto\n");
		sb.appendFront("if(current.key == fromto)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("// the value is within range.\n");
		sb.appendFront("yield return current.value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("// don't go right if the value is already higher than fromto\n");
		sb.appendFront("if(current.key <= fromto)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("foreach(" + graphElementType + " value in Lookup(current.right, fromto))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genAscending(IncidenceCountIndex index, boolean fromConstrained, boolean fromInclusive,
			boolean toConstrained, boolean toInclusive)
	{
		String attributeType = "int";
		String graphElementType = formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());

		String lookupMethodNameAppendix = "Ascending";
		if(fromConstrained) {
			lookupMethodNameAppendix += "From";
			if(fromInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}
		if(toConstrained) {
			lookupMethodNameAppendix += "To";
			if(toInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}

		sb.appendFront("private IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix
				+ "(TreeNode current");
		if(fromConstrained)
			sb.append(", " + attributeType + " from");
		if(toConstrained)
			sb.append(", " + attributeType + " to");
		sb.append(")\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(current == bottom)\n");
		sb.appendFront("\tyield break;\n");
		sb.append("\n");
		sb.appendFront("int versionAtIterationBegin = version;\n");
		sb.append("\n");
		if(fromConstrained) {
			sb.appendFront("// don't go left if the value is already lower than from\n");
			sb.appendFront("if(current.key" + (fromInclusive ? " >= " : " > ") + "from)\n");
		}
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("foreach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.left");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		if(fromConstrained || toConstrained) {
			sb.appendFront("// (only) yield a value that is within bounds\n");
			sb.appendFront("if(");
			if(fromConstrained) {
				sb.append("current.key" + (fromInclusive ? " >= " : " > ") + "from");
			}
			if(fromConstrained && toConstrained)
				sb.append(" && ");
			if(toConstrained) {
				sb.append("current.key" + (toInclusive ? " <= " : " < ") + "to");
			}
			sb.append(")\n");
		}
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("// the value is within range.\n");
		sb.appendFront("yield return current.value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		if(toConstrained) {
			sb.appendFront("// don't go right if the value is already higher than to\n");
			sb.appendFront("if(current.key" + (toInclusive ? " <= " : " < ") + "to)\n");
		}
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("foreach(" + graphElementType + " value in Lookup"
				+ lookupMethodNameAppendix + "(current.right");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genDescending(IncidenceCountIndex index, boolean fromConstrained, boolean fromInclusive,
			boolean toConstrained, boolean toInclusive)
	{
		String attributeType = "int";
		String graphElementType = formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());

		String lookupMethodNameAppendix = "Descending";
		if(fromConstrained) {
			lookupMethodNameAppendix += "From";
			if(fromInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}
		if(toConstrained) {
			lookupMethodNameAppendix += "To";
			if(toInclusive)
				lookupMethodNameAppendix += "Inclusive";
			else
				lookupMethodNameAppendix += "Exclusive";
		}

		sb.appendFront("private IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix
				+ "(TreeNode current");
		if(fromConstrained)
			sb.append(", " + attributeType + " from");
		if(toConstrained)
			sb.append(", " + attributeType + " to");
		sb.append(")\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(current == bottom)\n");
		sb.appendFront("\tyield break;\n");
		sb.append("\n");
		sb.appendFront("int versionAtIterationBegin = version;\n");
		sb.append("\n");
		if(fromConstrained) {
			sb.appendFront("// don't go left if the value is already lower than from\n");
			sb.appendFront("if(current.key" + (fromInclusive ? " <= " : " < ") + "from)\n");
		}
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("foreach(" + graphElementType + " value in Lookup"
				+ lookupMethodNameAppendix + "(current.right");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		if(fromConstrained || toConstrained) {
			sb.appendFront("// (only) yield a value that is within bounds\n");
			sb.appendFront("if(");
			if(fromConstrained) {
				sb.append("current.key" + (fromInclusive ? " <= " : " < ") + "from");
			}
			if(fromConstrained && toConstrained)
				sb.append(" && ");
			if(toConstrained) {
				sb.append("current.key" + (toInclusive ? " >= " : " > ") + "to");
			}
			sb.append(")\n");
		}
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("// the value is within range.\n");
		sb.appendFront("yield return current.value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		if(toConstrained) {
			sb.appendFront("// don't go right if the value is already higher than to\n");
			sb.appendFront("if(current.key" + (toInclusive ? " >= " : " > ") + "to)\n");
		}
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("foreach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.left");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("yield return value;\n");
		sb.appendFront("if(version != versionAtIterationBegin)\n");
		sb.appendFront("\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genGetIncidenceCount(IncidenceCountIndex index)
	{
		String graphElementType = formatElementInterfaceRef(index.getStartNodeType());
		sb.appendFront("public int GetIncidenceCount(GRGEN_LIBGR.IGraphElement element)\n");
		sb.appendFront("{\n");
		sb.appendFront("\treturn GetIncidenceCount((" + graphElementType + ") element);\n");
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("public int GetIncidenceCount(" + graphElementType + " element)\n");
		sb.appendFront("{\n");
		sb.appendFront("\treturn nodeToIncidenceCount[element];\n");
		sb.appendFront("}\n");
	}

	void genCheckDump(IncidenceCountIndex index)
	{
		String startNodeType = formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());

		sb.appendFront("protected void Check(TreeNode current)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(current == bottom)\n");
		sb.appendFront("\treturn;\n");
		sb.appendFront("Check(current.left);\n");
		sb.appendFront("if(!nodeToIncidenceCount.ContainsKey(current.value)) {\n");
		sb.indent();
		sb.appendFront("Dump(root);\n");
		sb.appendFront("Dump();\n");
		sb.appendFront("throw new Exception(\"Missing node\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.appendFront("if(nodeToIncidenceCount[current.value]!=current.key) {\n");
		sb.indent();
		sb.appendFront("Dump(root);\n");
		sb.appendFront("Dump();\n");
		sb.appendFront("throw new Exception(\"Incidence values differ\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.appendFront("Check(current.right);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("protected void Dump(TreeNode current)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(current == bottom)\n");
		sb.appendFront("\treturn;\n");
		sb.appendFront("Dump(current.left);\n");
		sb.appendFront("Console.Write(current.key);\n");
		sb.appendFront("Console.Write(\" -> \");\n");
		sb.appendFront("Console.WriteLine(graph.GetElementName(current.value));\n");
		sb.appendFront("Dump(current.right);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("protected void Dump()\n");
		sb.appendFront("{\n");
		sb.appendFront("foreach(KeyValuePair<" + startNodeType + ",int> kvp in nodeToIncidenceCount) {\n");
		sb.indent();
		sb.appendFront("Console.Write(graph.GetElementName(kvp.Key));\n");
		sb.appendFront("Console.Write(\" => \");\n");
		sb.appendFront("Console.WriteLine(kvp.Value);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genIndexMaintainingEventHandlers(IncidenceCountIndex index)
	{
		String startNodeType = formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());
		String incidentEdgeType = formatElementInterfaceRef(((IncidenceCountIndex)index).getIncidentEdgeType());
		String incidentEdgeTypeType = formatTypeClassRefInstance(((IncidenceCountIndex)index).getIncidentEdgeType());

		sb.appendFront("void ClearingGraph()\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("// ReInitialize AA tree to clear the index\n");
		sb.appendFront("bottom = new TreeNode();\n");
		sb.appendFront("root = bottom;\n");
		sb.appendFront("deleted = bottom;\n");
		sb.appendFront("last = null;\n");
		sb.appendFront("count = 0;\n");
		sb.appendFront("version = 0;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("void EdgeAdded(GRGEN_LIBGR.IEdge edge)\n");
		sb.appendFront("{\n");
		sb.indent();
		//sb.append("Check(root);\n");
		sb.appendFront("if(!(edge is " + incidentEdgeType + "))\n");
		sb.appendFront("\treturn;\n");
		sb.appendFront("GRGEN_LIBGR.INode source = edge.Source;\n");
		sb.appendFront("GRGEN_LIBGR.INode target = edge.Target;\n");
		genIndexMaintainingEdgeAdded(index);
		//sb.append("Check(root);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("void NodeAdded(GRGEN_LIBGR.INode node)\n");
		sb.appendFront("{\n");
		sb.indent();
		//sb.append("Check(root);\n");
		sb.appendFront("if(node is " + startNodeType + ") {\n");
		sb.indent();
		sb.appendFront("nodeToIncidenceCount.Add((" + startNodeType + ")node, 0);\n");
		sb.appendFront("Insert(ref root, 0, (" + startNodeType + ")node);\n");
		sb.unindent();
		sb.appendFront("}\n");
		//sb.append("Check(root);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("void RemovingEdge(GRGEN_LIBGR.IEdge edge)\n");
		sb.appendFront("{\n");
		sb.indent();
		//sb.append("Check(root);\n");
		sb.appendFront("if(!(edge is " + incidentEdgeType + "))\n");
		sb.appendFront("\treturn;\n");
		sb.appendFront("GRGEN_LIBGR.INode source = edge.Source;\n");
		sb.appendFront("GRGEN_LIBGR.INode target = edge.Target;\n");
		genIndexMaintainingRemovingEdge(index);
		//sb.append("Check(root);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("void RemovingNode(GRGEN_LIBGR.INode node)\n");
		sb.appendFront("{\n");
		sb.indent();
		//sb.append("Check(root);\n");
		sb.appendFront("if(node is " + startNodeType + ") {\n");
		sb.indent();
		sb.appendFront("nodeToIncidenceCount.Remove((" + startNodeType + ")node);\n");
		sb.appendFront("Delete(ref root, 0, (" + startNodeType + ")node);\n");
		sb.unindent();
		sb.appendFront("}\n");
		//sb.append("Check(root);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("void RetypingEdge(GRGEN_LIBGR.IEdge oldEdge, GRGEN_LIBGR.IEdge newEdge)\n");
		sb.appendFront("{\n");
		sb.indent();
		//sb.append("Check(root);\n");
		sb.appendFront("RemovingEdge(oldEdge);\n");
		sb.appendFront("EdgeAdded(newEdge);\n");
		//sb.append("Check(root);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("void RetypingNode(GRGEN_LIBGR.INode oldNode, GRGEN_LIBGR.INode newNode)\n");
		sb.appendFront("{\n");
		sb.indent();
		//sb.append("Check(root);\n");
		sb.appendFront("IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType> incidentEdges = "
				+ "GRGEN_LIBGR.GraphHelper.Incident(oldNode, " + incidentEdgeTypeType + ", graph.Model.NodeModel.RootType);\n");
		sb.appendFront("foreach(KeyValuePair<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType> edgeKVP in incidentEdges)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("GRGEN_LIBGR.IEdge edge = edgeKVP.Key;\n");
		sb.appendFront("GRGEN_LIBGR.INode source = edge.Source;\n");
		sb.appendFront("GRGEN_LIBGR.INode target = edge.Target;\n");
		genIndexMaintainingRemovingEdge(index);
		sb.unindent();
		sb.appendFront("}\n");

		sb.appendFront("if(oldNode is " + startNodeType + ") {\n");
		sb.indent();
		sb.appendFront("nodeToIncidenceCount.Remove((" + startNodeType + ")oldNode);\n");
		sb.appendFront("Delete(ref root, 0, (" + startNodeType + ")oldNode);\n");
		sb.unindent();
		sb.appendFront("}\n");

		sb.appendFront("if(newNode is " + startNodeType + ") {\n");
		sb.indent();
		sb.appendFront("nodeToIncidenceCount.Add((" + startNodeType + ")newNode, 0);\n");
		sb.appendFront("Insert(ref root, 0, (" + startNodeType + ")newNode);\n");
		sb.unindent();
		sb.appendFront("}\n");

		sb.appendFront("foreach(KeyValuePair<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType> edgeKVP in incidentEdges)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("GRGEN_LIBGR.IEdge edge = edgeKVP.Key;\n");
		sb.appendFront("GRGEN_LIBGR.INode source = edge.Source==oldNode ? newNode : edge.Source;\n");
		sb.appendFront("GRGEN_LIBGR.INode target = edge.Target==oldNode ? newNode : edge.Target;\n");
		genIndexMaintainingEdgeAdded(index);
		sb.unindent();
		sb.appendFront("}\n");
		//sb.append("Check(root);\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	void genIndexMaintainingEdgeAdded(IncidenceCountIndex index)
	{
		String startNodeType = formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());
		String adjacentNodeType = formatElementInterfaceRef(((IncidenceCountIndex)index).getAdjacentNodeType());

		if(index.Direction() == IncidentEdgeExpr.OUTGOING) {
			sb.appendFront("if(source is " + startNodeType + " && target is " + adjacentNodeType + ") {\n");
			sb.indent();
			sb.appendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], "
					+ "(" + startNodeType + ")source);\n");
			sb.appendFront("nodeToIncidenceCount[(" + startNodeType + ")source] = "
					+"nodeToIncidenceCount[(" + startNodeType + ")source] + 1;\n");
			sb.appendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], "
					+"(" + startNodeType + ")source);\n");
			sb.unindent();
			sb.appendFront("}\n");
		} else if(index.Direction() == IncidentEdgeExpr.INCOMING) {
			sb.appendFront("if(target is " + startNodeType + " && source is " + adjacentNodeType + ") {\n");
			sb.indent();
			sb.appendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], "
					+"(" + startNodeType + ")target);\n");
			sb.appendFront("nodeToIncidenceCount[(" + startNodeType + ")target] = "
					+ "nodeToIncidenceCount[(" + startNodeType + ")target] + 1;\n");
			sb.appendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], "
					+"(" + startNodeType + ")target);\n");
			sb.unindent();
			sb.appendFront("}\n");
		} else {
			sb.appendFront("if(source is " + startNodeType + " && target is " + adjacentNodeType + ") {\n");
			sb.indent();
			sb.appendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], "
					+"(" + startNodeType + ")source);\n");
			sb.appendFront("nodeToIncidenceCount[(" + startNodeType + ")source] = "
					+ "nodeToIncidenceCount[(" + startNodeType + ")source] + 1;\n");
			sb.appendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], "
					+"(" + startNodeType + ")source);\n");
			sb.unindent();
			sb.appendFront("}\n");
			sb.appendFront("if(target is " + startNodeType + " && source is " + adjacentNodeType + " && source!=target) {\n");
			sb.indent();
			sb.appendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], "
					+"(" + startNodeType + ")target);\n");
			sb.appendFront("nodeToIncidenceCount[(" + startNodeType + ")target] = "
					+"nodeToIncidenceCount[(" + startNodeType + ")target] + 1;\n");
			sb.appendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], "
					+"(" + startNodeType + ")target);\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	void genIndexMaintainingRemovingEdge(IncidenceCountIndex index)
	{
		String startNodeType = formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());
		String adjacentNodeType = formatElementInterfaceRef(((IncidenceCountIndex)index).getAdjacentNodeType());

		if(index.Direction() == IncidentEdgeExpr.OUTGOING) {
			sb.appendFront("if(source is " + startNodeType + " && target is " + adjacentNodeType + ") {\n");
			sb.indent();
			sb.appendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], "
					+ "(" + startNodeType + ")source);\n");
			sb.appendFront("nodeToIncidenceCount[(" + startNodeType + ")source] = "
					+ "nodeToIncidenceCount[(" + startNodeType + ")source] - 1;\n");
			sb.appendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], "
					+ "(" + startNodeType + ")source);\n");
			sb.unindent();
			sb.appendFront("}\n");
		} else if(index.Direction() == IncidentEdgeExpr.INCOMING) {
			sb.appendFront("if(target is " + startNodeType + " && source is " + adjacentNodeType + ") {\n");
			sb.indent();
			sb.appendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], "
					+ "(" + startNodeType + ")target);\n");
			sb.appendFront("nodeToIncidenceCount[(" + startNodeType + ")target] = "
					+ "nodeToIncidenceCount[("+ startNodeType + ")target] - 1;\n");
			sb.appendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], "
					+ "(" + startNodeType + ")target);\n");
			sb.unindent();
			sb.appendFront("}\n");
		} else {
			sb.appendFront("if(source is " + startNodeType + " && target is " + adjacentNodeType + ") {\n");
			sb.indent();
			sb.appendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], "
					+ "(" + startNodeType + ")source);\n");
			sb.appendFront("nodeToIncidenceCount[(" + startNodeType + ")source] = "
					+ "nodeToIncidenceCount[(" + startNodeType + ")source] - 1;\n");
			sb.appendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], "
					+ "(" + startNodeType + ")source);\n");
			sb.unindent();
			sb.appendFront("}\n");
			sb.appendFront("if(target is " + startNodeType + " && source is " + adjacentNodeType + " && source!=target) {\n");
			sb.indent();
			sb.appendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], "
					+ "(" + startNodeType + ")target);\n");
			sb.appendFront("nodeToIncidenceCount[(" + startNodeType + ")target] = "
					+ "nodeToIncidenceCount[(" + startNodeType + ")target] - 1;\n");
			sb.appendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], "
					+ "(" + startNodeType + ")target);\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	void genIndexAATreeBalancingInsertionDeletion(IncidenceCountIndex index)
	{
		String graphElementType = formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());
		String castForUnique = " as GRGEN_LGSP.LGSPNode";

		sb.appendFront("private void Skew(ref TreeNode current)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(current.level != current.left.level)\n");
		sb.appendFront("\treturn;\n");
		sb.append("\n");
		sb.appendFront("// rotate right\n");
		sb.appendFront("TreeNode left = current.left;\n");
		sb.appendFront("current.left = left.right;\n");
		sb.appendFront("left.right = current;\n");
		sb.appendFront("current = left;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("private void Split(ref TreeNode current)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(current.right.right.level != current.level)\n");
		sb.appendFront("\treturn;\n");
		sb.append("\n");
		sb.appendFront("// rotate left\n");
		sb.appendFront("TreeNode right = current.right;\n");
		sb.appendFront("current.right = right.left;\n");
		sb.appendFront("right.left = current;\n");
		sb.appendFront("current = right;\n");
		sb.appendFront("++current.level;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("private void Insert(ref TreeNode current, int key, " + graphElementType + " value)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(current == bottom)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("current = new TreeNode(key, value, bottom);\n");
		sb.appendFront("++count;\n");
		sb.appendFront("++version;\n");
		sb.appendFront("return;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
		sb.appendFront("if(key < current.key");
		sb.appendFront(" || ( key == current.key && (value" + castForUnique + ").uniqueId < "
				+ "(current.value" + castForUnique + ").uniqueId ) )\n");
		sb.appendFront("\tInsert(ref current.left, key, value);\n");
		sb.appendFront("else if(key > current.key");
		sb.appendFront(" || ( key == current.key && (value" + castForUnique + ").uniqueId > "
				+ "(current.value" + castForUnique + ").uniqueId ) )\n");
		sb.appendFront("\tInsert(ref current.right, key, value);\n");
		sb.appendFront("else\n");
		sb.appendFront("\tthrow new Exception(\"Insertion of already available element\");\n");
		sb.append("\n");
		sb.appendFront("Skew(ref current);\n");
		sb.appendFront("Split(ref current);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		sb.appendFront("private void Delete(ref TreeNode current, int key, " + graphElementType + " value)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(current == bottom)\n");
		sb.appendFront("\treturn;\n");
		sb.append("\n");
		sb.appendFront("// search down the tree (and set pointer last and deleted)\n");
		sb.appendFront("last = current;\n");
		sb.appendFront("if(key < current.key");
		sb.append(" || ( key == current.key && (value" + castForUnique + ").uniqueId < "
				+ "(current.value" + castForUnique + ").uniqueId ) )\n");
		sb.appendFront("\tDelete(ref current.left, key, value);\n");
		sb.appendFront("else\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("deleted = current;\n");
		sb.appendFront("Delete(ref current.right, key, value);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
		sb.appendFront("// at the bottom of the tree we remove the element (if present)\n");
		sb.appendFront("if(current == last && deleted != bottom && key == deleted.key");
		sb.appendFront(" && (value" + castForUnique + ").uniqueId"
				+ " == (deleted.value" + castForUnique + ").uniqueId )\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("deleted.value = current.value;\n");
		sb.appendFront("deleted.key = current.key;\n");
		sb.appendFront("deleted = bottom;\n");
		sb.appendFront("current = current.right;\n");
		sb.appendFront("--count;\n");
		sb.appendFront("++version;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.appendFront("// on the way back, we rebalance\n");
		sb.appendFront("else if(current.left.level < current.level - 1\n");
		sb.appendFront("\t|| current.right.level < current.level - 1)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("--current.level;\n");
		sb.appendFront("if(current.right.level > current.level)\n");
		sb.appendFront("\tcurrent.right.level = current.level;\n");
		sb.appendFront("Skew(ref current);\n");
		sb.appendFront("Skew(ref current.right);\n");
		sb.appendFront("Skew(ref current.right.right);\n");
		sb.appendFront("Split(ref current);\n");
		sb.appendFront("Split(ref current.right);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	@Override
	protected void genQualAccess(SourceBuilder sb, Qualification qual, Object modifyGenerationState)
	{
		// needed because of inheritance, maybe todo: remove
	}

	@Override
	protected void genMemberAccess(SourceBuilder sb, Entity member)
	{
		// needed because of inheritance, maybe todo: remove
	}

	///////////////////////
	// Private variables //
	///////////////////////

	private Model model;
	private SourceBuilder sb = null;
}
