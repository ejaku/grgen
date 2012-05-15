/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * ModelGen.java
 *
 * Generates the model files for the SearchPlanBackend2 backend.
 *
 * @author Moritz Kroll
 * @version $Id: ModelGen.java 26976 2010-10-11 00:11:23Z eja $
 */

package de.unika.ipd.grgen.be.Csharp;

import java.io.File;
import java.util.BitSet;
import java.util.Collection;
import java.util.Comparator;
import java.util.Date;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.List;
import java.util.LinkedList;
import java.util.Map;
import java.util.Set;
import java.util.TreeSet;

import de.unika.ipd.grgen.ir.*;

public class ModelGen extends CSharpBase {
	private final int MAX_OPERATIONS_FOR_ATTRIBUTE_INITIALIZATION_INLINING = 20;
	private final static String ATTR_IMPL_SUFFIX = "_M0no_suXx_h4rD";

	public ModelGen(SearchPlanBackend2 backend, String nodeTypePrefix, String edgeTypePrefix) {
		super(nodeTypePrefix, edgeTypePrefix);
		be = backend;
		rootTypes = new HashSet<String>();
		rootTypes.add("Node");
		rootTypes.add("Edge");
		rootTypes.add("AEdge");
		rootTypes.add("UEdge");
	}

	/**
	 * Generates the model sourcecode for the current unit.
	 */
	public void genModel(Model model) {
		this.model = model;
		sb = new StringBuffer();
		stubsb = null;

		String filename = model.getIdent() + "Model.cs";

		System.out.println("  generating the " + filename + " file...");

		sb.append("// This file has been generated automatically by GrGen (www.grgen.net)\n"
				+ "// Do not modify this file! Any changes will be lost!\n"
				+ "// Generated from \"" + be.unit.getFilename() + "\" on " + new Date() + "\n"
				+ "\n"
				+ "using System;\n"
				+ "using System.Collections.Generic;\n"
                + "using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n"
                + "using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n"
				+ "\n"
				+ "namespace de.unika.ipd.grGen.Model_" + model.getIdent() + "\n"
				+ "{\n"
				+ "\tusing GRGEN_MODEL = de.unika.ipd.grGen.Model_" + model.getIdent() + ";\n");

		System.out.println("    generating enums...");
		genEnums();

		System.out.println("    generating node types...");
		sb.append("\n");
		genTypes(model.getNodeTypes(), true);

		System.out.println("    generating node model...");
		sb.append("\n");
		genModelClass(model.getNodeTypes(), true);

		System.out.println("    generating edge types...");
		sb.append("\n");
		genTypes(model.getEdgeTypes(), false);

		System.out.println("    generating edge model...");
		sb.append("\n");
		genModelClass(model.getEdgeTypes(), false);

		System.out.println("    generating graph model...");
		sb.append("\n");
		genGraphModel();

		sb.append("}\n");

		writeFile(be.path, filename, sb);
		if(stubsb != null) {
			String stubFilename = model.getIdent() + "ModelStub.cs";
			System.out.println("  writing the " + stubFilename + " stub file...");
			writeFile(be.path, stubFilename, stubsb);
		}

		// generate the external functions stub file
		// only if there are external functions required
		if(model.getExternalTypes().isEmpty() && model.getExternalFunctions().isEmpty())
			return;

		filename = model.getIdent() + "ModelExternalFunctions.cs";

		System.out.println("  generating the " + filename + " file...");

		sb = new StringBuffer();

		sb.append("// This file has been generated automatically by GrGen (www.grgen.net)\n"
				+ "// Do not modify this file! Any changes will be lost!\n"
				+ "// Generated from \"" + be.unit.getFilename() + "\" on " + new Date() + "\n"
				+ "\n"
				+ "using System;\n"
				+ "using System.Collections.Generic;\n"
                + "using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n"
                + "using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n");

		if(!model.getExternalTypes().isEmpty())
		{
			sb.append("\n");
			sb.append("namespace de.unika.ipd.grGen.Model_" + model.getIdent() + "\n"
					+ "{\n");

			genExternalClasses();

			sb.append("}\n");
		}

		if(!model.getExternalFunctions().isEmpty())
		{
			sb.append("\n");
			sb.append("namespace de.unika.ipd.grGen.expression\n"
					+ "{\n"
					+ "\tusing GRGEN_MODEL = de.unika.ipd.grGen.Model_" + model.getIdent() + ";\n"
					+ "\n");

			sb.append("\tpublic partial class ExternalFunctions\n");
			sb.append("\t{\n");
			sb.append("\t\t// You must implement the following functions in the same partial class in ./" + model.getIdent() + "ModelExternalFunctionsImpl.cs:\n"
					+ "\n");

			genExternalFunctionHeaders();

			sb.append("\t}\n");

			sb.append("}\n");
		}

		writeFile(new File("."), filename, sb);
	}

	private StringBuffer getStubBuffer() {
		if(stubsb == null) {
			stubsb = new StringBuffer();
			stubsb.append("// This file has been generated automatically by GrGen (www.grgen.net)\n"
					+ "// Do not modify this file! Any changes will be lost!\n"
					+ "// Rename this file or use a copy!\n"
					+ "// Generated from \"" + be.unit.getFilename() + "\" on " + new Date() + "\n"
					+ "\n"
					+ "using System;\n"
					+ "using System.Collections.Generic;\n"
					+ "using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n"
					+ "using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n"
					+ "using GRGEN_MODEL = de.unika.ipd.grGen.Model_" + model.getIdent() + ";\n");
		}
		return stubsb;
	}

	private void genEnums() {
		sb.append("\t//\n");
		sb.append("\t// Enums\n");
		sb.append("\t//\n");
		sb.append("\n");

		for(EnumType enumt : model.getEnumTypes()) {
			sb.append("\tpublic enum ENUM_" + formatIdentifiable(enumt) + " { ");
			for(EnumItem enumi : enumt.getItems()) {
				sb.append("@" + formatIdentifiable(enumi) + " = " + enumi.getValue().getValue() + ", ");
			}
			sb.append("};\n\n");
		}

		sb.append("\tpublic class Enums\n");
		sb.append("\t{\n");
		for(EnumType enumt : model.getEnumTypes()) {
			sb.append("\t\tpublic static GRGEN_LIBGR.EnumAttributeType @" + formatIdentifiable(enumt)
					+ " = new GRGEN_LIBGR.EnumAttributeType(\"" + formatIdentifiable(enumt)
					+ "\", typeof(GRGEN_MODEL.ENUM_" + formatIdentifiable(enumt)
					+ "), new GRGEN_LIBGR.EnumMember[] {\n");
			for(EnumItem enumi : enumt.getItems()) {
				sb.append("\t\t\tnew GRGEN_LIBGR.EnumMember(" + enumi.getValue().getValue()
						+ ", \"" + formatIdentifiable(enumi) + "\"),\n");
			}
			sb.append("\t\t});\n");
		}
		sb.append("\t}\n");
	}

	/**
	 * Generates code for all given element types.
	 */
	private void genTypes(Collection<? extends InheritanceType> types, boolean isNode) {
		sb.append("\t//\n");
		sb.append("\t// " + formatNodeOrEdge(isNode) + " types\n");
		sb.append("\t//\n");
		sb.append("\n");
		sb.append("\tpublic enum " + formatNodeOrEdge(isNode) + "Types ");
		genSet(sb, types, "@", "", true);
		sb.append(";\n");

		for(InheritanceType type : types) {
			genType(types, type);
		}
	}

	/**
	 * Generates all code for a given type.
	 */
	private void genType(Collection<? extends InheritanceType> types, InheritanceType type) {
		sb.append("\n");
		sb.append("\t// *** " + formatNodeOrEdge(type) + " " + formatIdentifiable(type) + " ***\n");
		sb.append("\n");

		if(!rootTypes.contains(type.getIdent().toString()))
			genElementInterface(type);
		if(!type.isAbstract())
			genElementImplementation(type);
		genTypeImplementation(types, type);
	}

	//////////////////////////////////
	// Element interface generation //
	//////////////////////////////////

	/**
	 * Generates the element interface for the given type
	 */
	private void genElementInterface(InheritanceType type) {
		String iname = "I" + getNodeOrEdgeTypePrefix(type) + formatIdentifiable(type);
		sb.append("\tpublic interface " + iname + " : ");
		genDirectSuperTypeList(type);
		sb.append("\n");
		sb.append("\t{\n");
		genAttributeAccess(type, type.getMembers(), "");
		sb.append("\t}\n");
	}

	/**
	 * Generate a list of direct supertypes of the given type.
	 */
	private void genDirectSuperTypeList(InheritanceType type) {
		boolean isNode = type instanceof NodeType;
		String kindStr = isNode ? "Node" : "Edge";

		String iprefix = "I" + getNodeOrEdgeTypePrefix(type);
		Collection<InheritanceType> directSuperTypes = type.getDirectSuperTypes();

		if(directSuperTypes.isEmpty())
		{
			sb.append("GRGEN_LIBGR.I" + formatNodeOrEdge(type));		// INode or IEdge
		}

		boolean hasRootType = false;
		boolean first = true;
		for(Iterator<InheritanceType> i = directSuperTypes.iterator(); i.hasNext(); ) {
			InheritanceType superType = i.next();
			if(rootTypes.contains(superType.getIdent().toString())) {
				// avoid problems with "extends Edge, AEdge" mapping to "IEdge, IEdge"
				if(hasRootType) continue;
				hasRootType = true;

				if(first) first = false;
				else sb.append(", ");
				sb.append("GRGEN_LIBGR.I" + kindStr);
			}
			else {
				if(first) first = false;
				else sb.append(", ");
				sb.append(iprefix + formatIdentifiable(superType));
			}
		}
	}

	/**
	 * Generate the attribute accessor declarations of the given members.
	 * @param type The type for which the accessors are to be generated.
	 * @param members A collection of member entities.
	 * @param modifiers A string which may contain modifiers to be applied to the accessors.
	 * 		It must either end with a space or be empty.
	 */
	private void genAttributeAccess(InheritanceType type, Collection<Entity> members,
			String modifiers) {
		for(Entity e : members) {
			sb.append("\t\t" + modifiers);
			if(type.getOverriddenMember(e) != null)
				sb.append("new ");
			if(e.isConst()) {
				sb.append(formatAttributeType(e) + " @" + formatIdentifiable(e) + " { get; }\n");
			} else {
				sb.append(formatAttributeType(e) + " @" + formatIdentifiable(e) + " { get; set; }\n");
			}
		}
	}

	///////////////////////////////////////
	// Element implementation generation //
	///////////////////////////////////////

	/**
	 * Generates the element implementation for the given type
	 */
	private void genElementImplementation(InheritanceType type) {
		boolean isNode = type instanceof NodeType;
		String kindStr = isNode ? "Node" : "Edge";
		String elemname = formatElementClassName(type);
		String elemref = formatElementClassRef(type);
		String extName = type.getExternalName();
		String allocName = extName != null ? "global::" + extName : elemref;
		String typeref = formatTypeClassRef(type);
		String ielemref = formatElementInterfaceRef(type);
		String namespace = null;
		StringBuffer routedSB = sb;
		String routedClassName = elemname;
		String routedDeclName = elemref;

		if(extName == null) {
			sb.append("\n\tpublic sealed class " + elemname + " : GRGEN_LGSP.LGSP"
					+ kindStr + ", " + ielemref + "\n\t{\n");
		}
		else { // what's that?
			routedSB = getStubBuffer();
			int lastDot = extName.lastIndexOf('.');
			String extClassName;
			if(lastDot != -1) {
				namespace = extName.substring(0, lastDot);
				extClassName = extName.substring(lastDot + 1);
				stubsb.append("\n"
						+ "namespace " + namespace + "\n"
						+ "{\n");
			}
			else extClassName = extName;
			routedClassName = extClassName;
			routedDeclName = extClassName;

			stubsb.append("\tpublic class " + extClassName + " : " + elemref + "\n"
					+ "\t{\n"
					+ "\t\tpublic " + extClassName + "() : base() { }\n\n");

			sb.append("\n\tpublic abstract class " + elemname + " : GRGEN_LGSP.LGSP"
					+ kindStr + ", " + ielemref + "\n\t{\n");
		}
		sb.append("\t\tprivate static int poolLevel = 0;\n"
				+ "\t\tprivate static " + elemref + "[] pool = new " + elemref + "[10];\n");

		// Static initialization for constants = static members
		initAllMembersConst(type, elemname, "this", "\t\t\t");

		// Generate constructor
		if(isNode) {
			sb.append("\t\tpublic " + elemname + "() : base("+ typeref + ".typeVar)\n"
					+ "\t\t{\n");
			initAllMembersNonConst(type, "this", "\t\t\t", false, false);
			sb.append("\t\t}\n\n");
		}
		else {
			sb.append("\t\tpublic " + elemname + "(GRGEN_LGSP.LGSPNode source, "
						+ "GRGEN_LGSP.LGSPNode target)\n"
					+ "\t\t\t: base("+ typeref + ".typeVar, source, target)\n"
					+ "\t\t{\n");
			initAllMembersNonConst(type, "this", "\t\t\t", false, false);
			sb.append("\t\t}\n\n");
		}

		// Generate static type getter
		sb.append("\t\tpublic static " + typeref + " TypeInstance { get { return " + typeref + ".typeVar; } }\n\n");

		// Generate the clone and copy constructor
		if(isNode)
			routedSB.append("\t\tpublic override GRGEN_LIBGR.INode Clone() { return new "
						+ routedDeclName + "(this); }\n"
					+ "\n"
					+ "\t\tprivate " + routedClassName + "(" + routedDeclName + " oldElem) : base("
					+ (extName == null ? typeref + ".typeVar" : "") + ")\n");
		else
			routedSB.append("\t\tpublic override GRGEN_LIBGR.IEdge Clone("
						+ "GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)\n"
					+ "\t\t{ return new " + routedDeclName + "(this, (GRGEN_LGSP.LGSPNode) newSource, "
						+ "(GRGEN_LGSP.LGSPNode) newTarget); }\n"
					+ "\n"
					+ "\t\tprivate " + routedClassName + "(" + routedDeclName
						+ " oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)\n"
					+ "\t\t\t: base("
					+ (extName == null ? typeref + ".typeVar, " : "") + "newSource, newTarget)\n");
		routedSB.append("\t\t{\n");
		for(Entity member : type.getAllMembers()) {
			if(member.isConst())
				continue;

			String attrName = formatIdentifiable(member);
			if(member.getType() instanceof MapType || member.getType() instanceof SetType || member.getType() instanceof ArrayType) {
				routedSB.append("\t\t\t" + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = new " + formatAttributeType(member.getType())
						+ "(oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ");\n");
			} else {
				routedSB.append("\t\t\t" + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ";\n");
			}
		}
		routedSB.append("\t\t}\n");

		// Generate the attribute comparison method
		routedSB.append("\n\t\tpublic override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {\n");
		routedSB.append("\t\t\tif(!(that is "+routedClassName+")) return false;\n");
		routedSB.append("\t\t\t"+routedClassName+" that_ = ("+routedClassName+")that;\n");
		routedSB.append("\t\t\treturn true\n");
		for(Entity member : type.getAllMembers()) {
			if(member.isConst())
				continue;

			String attrName = formatIdentifiable(member);
			if(member.getType() instanceof MapType || member.getType() instanceof SetType || member.getType() instanceof ArrayType) {
				routedSB.append("\t\t\t\t&& GRGEN_LIBGR.DictionaryListHelper.Equal(" + attrName + ModelGen.ATTR_IMPL_SUFFIX + ", "
						+ "that_." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ")\n");
			} else {
				routedSB.append("\t\t\t\t&& " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " == that_." + attrName + ModelGen.ATTR_IMPL_SUFFIX + "\n");
			}
		}
		routedSB.append("\t\t\t;\n");
		routedSB.append("\t\t}\n\n");

		// Generate element creators
		if(isNode) {
			sb.append("\t\tpublic static " + elemref + " CreateNode(GRGEN_LGSP.LGSPGraph graph)\n"
					+ "\t\t{\n"
					+ "\t\t\t" + elemref + " node;\n"
					+ "\t\t\tif(poolLevel == 0)\n"
					+ "\t\t\t\tnode = new " + allocName + "();\n"
					+ "\t\t\telse\n"
					+ "\t\t\t{\n"
					+ "\t\t\t\tnode = pool[--poolLevel];\n"
					+ "\t\t\t\tnode.lgspInhead = null;\n"
					+ "\t\t\t\tnode.lgspOuthead = null;\n"
					+ "\t\t\t\tnode.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;\n");
			initAllMembersNonConst(type, "node", "\t\t\t\t", true, false);
			sb.append("\t\t\t}\n"
					+ "\t\t\tgraph.AddNode(node);\n"
					+ "\t\t\treturn node;\n"
					+ "\t\t}\n\n"
					
					+ "\t\tpublic static " + elemref + " CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)\n"
					+ "\t\t{\n"
					+ "\t\t\t" + elemref + " node;\n"
					+ "\t\t\tif(poolLevel == 0)\n"
					+ "\t\t\t\tnode = new " + allocName + "();\n"
					+ "\t\t\telse\n"
					+ "\t\t\t{\n"
					+ "\t\t\t\tnode = pool[--poolLevel];\n"
					+ "\t\t\t\tnode.lgspInhead = null;\n"
					+ "\t\t\t\tnode.lgspOuthead = null;\n"
					+ "\t\t\t\tnode.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;\n");
			initAllMembersNonConst(type, "node", "\t\t\t\t", true, false);
			sb.append("\t\t\t}\n"
					+ "\t\t\tgraph.AddNode(node, nodeName);\n"
					+ "\t\t\treturn node;\n"
					+ "\t\t}\n\n");
		}
		else {
			sb.append("\t\tpublic static " + elemref + " CreateEdge(GRGEN_LGSP.LGSPGraph graph, "
						+ "GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)\n"
					+ "\t\t{\n"
					+ "\t\t\t" + elemref + " edge;\n"
					+ "\t\t\tif(poolLevel == 0)\n"
					+ "\t\t\t\tedge = new " + allocName + "(source, target);\n"
					+ "\t\t\telse\n"
					+ "\t\t\t{\n"
					+ "\t\t\t\tedge = pool[--poolLevel];\n"
					+ "\t\t\t\tedge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;\n"
					+ "\t\t\t\tedge.lgspSource = source;\n"
					+ "\t\t\t\tedge.lgspTarget = target;\n");
			initAllMembersNonConst(type, "edge", "\t\t\t\t", true, false);
			sb.append("\t\t\t}\n"
					+ "\t\t\tgraph.AddEdge(edge);\n"
					+ "\t\t\treturn edge;\n"
					+ "\t\t}\n\n"

					+ "\t\tpublic static " + elemref + " CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, "
						+ "GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)\n"
					+ "\t\t{\n"
					+ "\t\t\t" + elemref + " edge;\n"
					+ "\t\t\tif(poolLevel == 0)\n"
					+ "\t\t\t\tedge = new " + allocName + "(source, target);\n"
					+ "\t\t\telse\n"
					+ "\t\t\t{\n"
					+ "\t\t\t\tedge = pool[--poolLevel];\n"
					+ "\t\t\t\tedge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;\n"
					+ "\t\t\t\tedge.lgspSource = source;\n"
					+ "\t\t\t\tedge.lgspTarget = target;\n");
			initAllMembersNonConst(type, "edge", "\t\t\t\t", true, false);
			sb.append("\t\t\t}\n"
					+ "\t\t\tgraph.AddEdge(edge, edgeName);\n"
					+ "\t\t\treturn edge;\n"
					+ "\t\t}\n\n");
		}
		sb.append("\t\tpublic override void Recycle()\n"
				+ "\t\t{\n"
				+ "\t\t\tif(poolLevel < 10)\n"
				+ "\t\t\t\tpool[poolLevel++] = this;\n"
				+ "\t\t}\n\n");

		genAttributesAndAttributeAccessImpl(type);

		sb.append("\t}\n");

		if(extName != null) {
			stubsb.append(nsIndent + "}\n");		// close class stub
			if(namespace != null)
				stubsb.append("}\n");				// close namespace
		}
	}

	private void initAllMembersNonConst(InheritanceType type, String varName,
			String indentString, boolean withDefaultInits, boolean isResetAllAttributes) {
		curMemberOwner = varName;

		// if we don't currently create the method ResetAllAttributes
		// we replace the initialization code by a call to ResetAllAttributes, if it gets to large
		if(!isResetAllAttributes
				&& initializationOperationsCount(type) > MAX_OPERATIONS_FOR_ATTRIBUTE_INITIALIZATION_INLINING)
		{
			sb.append(indentString + varName +  ".ResetAllAttributes();\n");
			curMemberOwner = null;
			return;
		}

		sb.append(indentString + "// implicit initialization, map/set/array creation of " + formatIdentifiable(type) + "\n");

		// default attribute inits need to be generated if code must overwrite old values
		// only in constructor not needed, cause there taken care of by c#
		// if there is explicit initialization code, it's not needed, too,
		// but that's left for the compiler to optimize away
		if(withDefaultInits) {
			for(Entity member : type.getAllMembers()) {
				if(member.isConst())
					continue;

				Type t = member.getType();
				// handled down below, as maps/sets/arrays must be created independent of initialization
				if(t instanceof MapType || t instanceof SetType || t instanceof ArrayType)
					continue;

				String attrName = formatIdentifiable(member);
				sb.append(indentString + varName + ".@" + attrName + " = ");
				if(t instanceof ByteType || t instanceof ShortType || t instanceof IntType 
						|| t instanceof EnumType || t instanceof DoubleType ) {
					sb.append("0;\n");
				} else if(t instanceof FloatType) {
					sb.append("0f;\n");
				} else if(t instanceof LongType) {
					sb.append("0l;\n");
				} else if(t instanceof BooleanType) {
					sb.append("false;\n");
				} else if(t instanceof StringType || t instanceof ObjectType || t instanceof VoidType || t instanceof ExternalType || t instanceof GraphType) {
					sb.append("null;\n");
				} else {
					throw new IllegalArgumentException("Unknown Entity: " + member + "(" + t + ")");
				}
			}
		}

		// create maps and sets and arrays
		for(Entity member : type.getAllMembers()) {
			if(member.isConst())
				continue;

			Type t = member.getType();
			if(!(t instanceof MapType || t instanceof SetType || t instanceof ArrayType))
				continue;

			String attrName = formatIdentifiable(member);
			sb.append(indentString + varName + ".@" + attrName + " = ");
			if(t instanceof MapType) {
				MapType mapType = (MapType) t;
				sb.append("new " + formatAttributeType(mapType) + "();\n");
			} else if(t instanceof SetType) {
				SetType setType = (SetType) t;
				sb.append("new " + formatAttributeType(setType) + "();\n");
			} else if(t instanceof ArrayType) {
				ArrayType arrayType = (ArrayType) t;
				sb.append("new " + formatAttributeType(arrayType) + "();\n");
			}
		}

		// generate the user defined initializations, first for super types
		for(InheritanceType superType : type.getAllSuperTypes())
			genMemberInitNonConst(superType, type, indentString, varName,
					withDefaultInits, isResetAllAttributes);
		// then for current type
		genMemberInitNonConst(type, type, indentString, varName,
				withDefaultInits, isResetAllAttributes);

		curMemberOwner = null;
	}

	private int initializationOperationsCount(InheritanceType targetType) {
		int initializationOperations = 0;

		// attribute initializations from super classes not overridden in target class
		for(InheritanceType superType : targetType.getAllSuperTypes()) {
member_init_loop:
			for(MemberInit memberInit : superType.getMemberInits()) {
				if(memberInit.getMember().isConst())
					continue;
				for(MemberInit tmi : targetType.getMemberInits()) {
					if(memberInit.getMember() == tmi.getMember())
						continue member_init_loop;
				}
				++initializationOperations;
			}
map_init_loop:
			for(MapInit mapInit : superType.getMapInits()) {
				if(mapInit.getMember().isConst())
					continue;
				for(MapInit tmi : targetType.getMapInits()) {
					if(mapInit.getMember() == tmi.getMember())
						continue map_init_loop;
				}
				initializationOperations += mapInit.getMapItems().size();
			}
set_init_loop:
			for(SetInit setInit : superType.getSetInits()) {
				if(setInit.getMember().isConst())
					continue;
				for(SetInit tsi : targetType.getSetInits()) {
					if(setInit.getMember() == tsi.getMember())
						continue set_init_loop;
				}
				initializationOperations += setInit.getSetItems().size();
			}
array_init_loop:
			for(ArrayInit arrayInit : superType.getArrayInits()) {
				if(arrayInit.getMember().isConst())
					continue;
				for(ArrayInit tai : targetType.getArrayInits()) {
					if(arrayInit.getMember() == tai.getMember())
						continue array_init_loop;
				}
				initializationOperations += arrayInit.getArrayItems().size();
			}
		}

		// attribute initializations of target class
		for(MemberInit memberInit : targetType.getMemberInits()) {
			if(!memberInit.getMember().isConst())
				++initializationOperations;
		}

		for(MapInit mapInit : targetType.getMapInits()) {
			if(!mapInit.getMember().isConst())
				initializationOperations += mapInit.getMapItems().size();
		}

		for(SetInit setInit : targetType.getSetInits()) {
			if(!setInit.getMember().isConst())
				initializationOperations += setInit.getSetItems().size();
		}

		for(ArrayInit arrayInit : targetType.getArrayInits()) {
			if(!arrayInit.getMember().isConst())
				initializationOperations += arrayInit.getArrayItems().size();
		}

		return initializationOperations;
	}

	private void initAllMembersConst(InheritanceType type, String className,
			String varName, String indentString) {
		curMemberOwner = varName;

		List<String> staticInitializers = new LinkedList<String>();

		sb.append("\t\t\n");

		// generate the user defined initializations, first for super types
		for(InheritanceType superType : type.getAllSuperTypes())
			genMemberInitConst(superType, type, staticInitializers);
		// then for current type
		genMemberInitConst(type, type, staticInitializers);

		sb.append("\t\tstatic " + className + "() {\n");
		for(String staticInit : staticInitializers) {
			sb.append("\t\t\t" + staticInit + "();\n");
		}
		sb.append("\t\t}\n");

		sb.append("\t\t\n");

		curMemberOwner = null;
	}

	private void genMemberInitNonConst(InheritanceType type, InheritanceType targetType,
			String indentString, String varName,
			boolean withDefaultInits, boolean isResetAllAttributes) {
		if(rootTypes.contains(type.getIdent().toString())) // skip root types, they don't possess attributes
			return;
		sb.append(indentString + "// explicit initializations of " + formatIdentifiable(type) + " for target " + formatIdentifiable(targetType) + "\n");

		// init members of primitive value with explicit initialization
		for(MemberInit memberInit : type.getMemberInits()) {
			Entity member = memberInit.getMember();
			if(memberInit.getMember().isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrName = formatIdentifiable(memberInit.getMember());
			sb.append(indentString + varName + ".@" + attrName + " = ");
			genExpression(sb, memberInit.getExpression(), null);
			sb.append(";\n");
		}

		// init members of map value with explicit initialization
		for(MapInit mapInit : type.getMapInits()) {
			Entity member = mapInit.getMember();
			if(mapInit.getMember().isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrName = formatIdentifiable(mapInit.getMember());
			for(MapItem item : mapInit.getMapItems()) {
				sb.append(indentString + varName + ".@" + attrName + "[");
				genExpression(sb, item.getKeyExpr(), null);
				sb.append("] = ");
				genExpression(sb, item.getValueExpr(), null);
				sb.append(";\n");
			}
		}

		// init members of set value with explicit initialization
		for(SetInit setInit : type.getSetInits()) {
			Entity member = setInit.getMember();
			if(setInit.getMember().isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrName = formatIdentifiable(setInit.getMember());
			for(SetItem item : setInit.getSetItems()) {
				sb.append(indentString + varName + ".@" + attrName + "[");
				genExpression(sb, item.getValueExpr(), null);
				sb.append("] = null;\n");
			}
		}

		// init members of array value with explicit initialization
		for(ArrayInit arrayInit : type.getArrayInits()) {
			Entity member = arrayInit.getMember();
			if(arrayInit.getMember().isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrName = formatIdentifiable(arrayInit.getMember());
			for(ArrayItem item : arrayInit.getArrayItems()) {
				sb.append(indentString + varName + ".@" + attrName + ".Add(");
				genExpression(sb, item.getValueExpr(), null);
				sb.append(");\n");
			}
		}
	}

	private void genMemberInitConst(InheritanceType type, InheritanceType targetType,
			List<String> staticInitializers) {
		if(rootTypes.contains(type.getIdent().toString())) // skip root types, they don't possess attributes
			return;
		sb.append("\t\t// explicit initializations of " + formatIdentifiable(type) + " for target " + formatIdentifiable(targetType) + "\n");

		HashSet<Entity> initializedConstMembers = new HashSet<Entity>();

		// init const members of primitive value with explicit initialization
		for(MemberInit memberInit : type.getMemberInits()) {
			Entity member = memberInit.getMember();
			if(!member.isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrType = formatAttributeType(member);
			String attrName = formatIdentifiable(member);
			sb.append("\t\tprivate static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = ");
			genExpression(sb, memberInit.getExpression(), null);
			sb.append(";\n");

			initializedConstMembers.add(member);
		}

		// init const members of map value with explicit initialization
		for(MapInit mapInit : type.getMapInits()) {
			Entity member = mapInit.getMember();
			if(!member.isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrType = formatAttributeType(member);
			String attrName = formatIdentifiable(member);
			sb.append("\t\tprivate static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = " +
					"new " + attrType + "();\n");
			staticInitializers.add("init_" + attrName);
			sb.append("\t\tstatic void init_" + attrName + "() {\n");
			for(MapItem item : mapInit.getMapItems()) {
				sb.append("\t\t\t");
				sb.append(attrName + ModelGen.ATTR_IMPL_SUFFIX);
				sb.append("[");
				genExpression(sb, item.getKeyExpr(), null);
				sb.append("] = ");
				genExpression(sb, item.getValueExpr(), null);
				sb.append(";\n");
			}
			sb.append("\t\t}\n");

			initializedConstMembers.add(member);
		}

		// init const members of set value with explicit initialization
		for(SetInit setInit : type.getSetInits()) {
			Entity member = setInit.getMember();
			if(!member.isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrType = formatAttributeType(member);
			String attrName = formatIdentifiable(member);
			sb.append("\t\tprivate static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = " +
					"new " + attrType + "();\n");
			staticInitializers.add("init_" + attrName);
			sb.append("\t\tstatic void init_" + attrName + "() {\n");
			for(SetItem item : setInit.getSetItems()) {
				sb.append("\t\t\t");
				sb.append(attrName + ModelGen.ATTR_IMPL_SUFFIX);
				sb.append("[");
				genExpression(sb, item.getValueExpr(), null);
				sb.append("] = null;\n");
			}
			sb.append("\t\t}\n");

			initializedConstMembers.add(member);
		}

		// init const members of array value with explicit initialization
		for(ArrayInit arrayInit : type.getArrayInits()) {
			Entity member = arrayInit.getMember();
			if(!member.isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrType = formatAttributeType(member);
			String attrName = formatIdentifiable(member);
			sb.append("\t\tprivate static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = " +
					"new " + attrType + "();\n");
			staticInitializers.add("init_" + attrName);
			sb.append("\t\tstatic void init_" + attrName + "() {\n");
			for(ArrayItem item : arrayInit.getArrayItems()) {
				sb.append("\t\t\t");
				sb.append(attrName + ModelGen.ATTR_IMPL_SUFFIX);
				sb.append(".Add(");
				genExpression(sb, item.getValueExpr(), null);
				sb.append(");\n");
			}
			sb.append("\t\t}\n");

			initializedConstMembers.add(member);
		}

		sb.append("\t\t// implicit initializations of " + formatIdentifiable(type) + " for target " + formatIdentifiable(targetType) + "\n");

		for(Entity member : type.getMembers()) {
			if(!member.isConst())
				continue;
			if(initializedConstMembers.contains(member))
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			Type memberType = member.getType();
			String attrType = formatAttributeType(member);
			String attrName = formatIdentifiable(member);

			if(memberType instanceof MapType || memberType instanceof SetType || memberType instanceof ArrayType)
				sb.append("\t\tprivate static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = " +
						"new " + attrType + "();\n");
			else
				sb.append("\t\tprivate static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + ";\n");
		}
	}

	boolean generateInitializationOfTypeAtCreatingTargetTypeInitialization(
			Entity member, InheritanceType type, InheritanceType targetType)
	{
		// to decide on generating targetType initialization:
		//  - generate initialization of currently focused supertype type?
		// goal: only generate the initialization closest to the target type
		// -> don't generate initialization of type if there exists a subtype of type,
		// which is a supertype of the target type, and which contains an initialization

		Set<InheritanceType> childrenOfFocusedType =
			new LinkedHashSet<InheritanceType>(type.getAllSubTypes());
		childrenOfFocusedType.remove(type); // we want children only, comes with type itself included

		Set<InheritanceType> targetTypeAndParents =
			new LinkedHashSet<InheritanceType>(targetType.getAllSuperTypes());
		targetTypeAndParents.add(targetType); // we want it inclusive target, comes exclusive

		Set<InheritanceType> intersection =
			new LinkedHashSet<InheritanceType>(childrenOfFocusedType);
		intersection.retainAll(targetTypeAndParents); // the set is empty if type==targetType

		for(InheritanceType relevantChildrenOfFocusedType : intersection)
		{
			// if a type below focused type contains an initialization for current member
			// then we skip the initialization of the focused type
			for(MemberInit tmi : relevantChildrenOfFocusedType.getMemberInits()) {
				if(member == tmi.getMember())
					return false;
			}
			for(MapInit tmi : relevantChildrenOfFocusedType.getMapInits()) {
				if(member == tmi.getMember())
					return false;
			}
			for(SetInit tsi : relevantChildrenOfFocusedType.getSetInits()) {
				if(member == tsi.getMember())
					return false;
			}
			for(ArrayInit tai : relevantChildrenOfFocusedType.getArrayInits()) {
				if(member == tai.getMember())
					return false;
			}
		}

		return true;
	}

	protected void genQualAccess(StringBuffer sb, Qualification qual, Object modifyGenerationState) {
		Entity owner = qual.getOwner();
		sb.append("((I" + getNodeOrEdgeTypePrefix(owner) +
				formatIdentifiable(owner.getType()) + ") ");
		sb.append(formatEntity(owner) + ").@" + formatIdentifiable(qual.getMember()));
	}

	protected void genMemberAccess(StringBuffer sb, Entity member) {
		if(curMemberOwner != null)
			sb.append(curMemberOwner + ".");
		sb.append("@" + formatIdentifiable(member));
	}


	/**
	 * Generate the attribute accessor implementations of the given type
	 */
	private void genAttributesAndAttributeAccessImpl(InheritanceType type) {
		StringBuffer routedSB = sb;
		String extName = type.getExternalName();
		String extModifier = "";

		// what's that?
		if(extName != null) {
			routedSB = getStubBuffer();
			extModifier = "override ";

			genAttributeAccess(type, type.getAllMembers(), "public abstract ");
		}

		// Create the implementation of the attributes.
		// If an external name is given for this type, this is written
		// into the stub file with an "override" modifier on the accessors.

		// member, getter, setter for attributes
		for(Entity e : type.getAllMembers()) {
			String attrType = formatAttributeType(e);
			String attrName = formatIdentifiable(e);

			if(e.isConst()) {
				// no member for const attributes, no setter for const attributes
				// they are class static, the member is created at the point of initialization
				routedSB.append("\t\tpublic " + extModifier + attrType + " @" + attrName + "\n");
				routedSB.append("\t\t{\n");
				routedSB.append("\t\t\tget { return " + attrName + ModelGen.ATTR_IMPL_SUFFIX + "; }\n");
				routedSB.append("\t\t}\n");
			} else {
				// member, getter, setter for non-const attributes
				routedSB.append("\n\t\tprivate " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + ";\n");
				routedSB.append("\t\tpublic " + extModifier + attrType + " @" + attrName + "\n");
				routedSB.append("\t\t{\n");
				routedSB.append("\t\t\tget { return " + attrName + ModelGen.ATTR_IMPL_SUFFIX + "; }\n");
				routedSB.append("\t\t\tset { " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = value; }\n");
				routedSB.append("\t\t}\n");
			}

			// what's that?
			Entity overriddenMember = type.getOverriddenMember(e);
			if(overriddenMember != null) {
				routedSB.append("\n\t\tobject "
						+ formatElementInterfaceRef(overriddenMember.getOwner())
						+ ".@" + attrName + "\n"
						+ "\t\t{\n"
						+ "\t\t\tget { return " + attrName + ModelGen.ATTR_IMPL_SUFFIX + "; }\n"
						+ "\t\t\tset { " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = (" + attrType + ") value; }\n"
						+ "\t\t}\n");
			}
		}

		// get attribute by name
		sb.append("\t\tpublic override object GetAttribute(string attrName)\n");
		sb.append("\t\t{\n");
		if(type.getAllMembers().size() != 0) {
			sb.append("\t\t\tswitch(attrName)\n");
			sb.append("\t\t\t{\n");
			for(Entity e : type.getAllMembers()) {
				String name = formatIdentifiable(e);
				sb.append("\t\t\t\tcase \"" + name + "\": return this.@" + name + ";\n");
			}
			sb.append("\t\t\t}\n");
		}
		sb.append("\t\t\tthrow new NullReferenceException(\n");
		sb.append("\t\t\t\t\"The " + (type instanceof NodeType ? "node" : "edge")
				+ " type \\\"" + formatIdentifiable(type)
				+ "\\\" does not have the attribute \\\"\" + attrName + \"\\\"!\");\n");
		sb.append("\t\t}\n");

		// set attribute by name
		sb.append("\t\tpublic override void SetAttribute(string attrName, object value)\n");
		sb.append("\t\t{\n");
		if(type.getAllMembers().size() != 0) {
			sb.append("\t\t\tswitch(attrName)\n");
			sb.append("\t\t\t{\n");
			for(Entity e : type.getAllMembers()) {
				String name = formatIdentifiable(e);
				if(e.isConst()) {
					sb.append("\t\t\t\tcase \"" + name + "\": ");
					sb.append("throw new NullReferenceException(");
					sb.append("\"The attribute " + name + " of the " + (type instanceof NodeType ? "node" : "edge")
							+ " type \\\"" + formatIdentifiable(type)
							+ "\\\" is read only!\");\n");
				} else {
					sb.append("\t\t\t\tcase \"" + name + "\": this.@" + name + " = ("
							+ formatAttributeType(e) + ") value; return;\n");
				}
			}
			sb.append("\t\t\t}\n");
		}
		sb.append("\t\t\tthrow new NullReferenceException(\n");
		sb.append("\t\t\t\t\"The " + (type instanceof NodeType ? "node" : "edge")
				+ " type \\\"" + formatIdentifiable(type)
				+ "\\\" does not have the attribute \\\"\" + attrName + \"\\\"!\");\n");
		sb.append("\t\t}\n");

		// reset all attributes
		sb.append("\t\tpublic override void ResetAllAttributes()\n");
		sb.append("\t\t{\n");
		initAllMembersNonConst(type, "this", "\t\t\t", true, true);
		sb.append("\t\t}\n");
	}

	////////////////////////////////////
	// Type implementation generation //
	////////////////////////////////////

	/**
	 * Generates the type implementation
	 */
	private void genTypeImplementation(Collection<? extends InheritanceType> types, InheritanceType type) {
		String typeident = formatIdentifiable(type);
		String typename = formatTypeClassName(type);
		String typeref = formatTypeClassRef(type);
		String elemref = formatElementClassRef(type);
		String extName = type.getExternalName();
		String allocName = extName != null ? "global::" + extName : elemref;
		boolean isNode = type instanceof NodeType;
		String kindStr = isNode ? "Node" : "Edge";

		sb.append("\n");
		sb.append("\tpublic sealed class " + typename + " : GRGEN_LIBGR." + kindStr + "Type\n");
		sb.append("\t{\n");

		sb.append("\t\tpublic static " + typeref + " typeVar = new " + typeref + "();\n");
		genIsA(types, type);
		genIsMyType(types, type);
		genAttributeAttributes(type);

		sb.append("\t\tpublic " + typename + "() : base((int) " + formatNodeOrEdge(type) + "Types.@" + typeident + ")\n");
		sb.append("\t\t{\n");
		genAttributeInit(type);
		addAnnotations(sb, type, "annotations");
		sb.append("\t\t}\n");

		sb.append("\t\tpublic override string Name { get { return \"" + typeident + "\"; } }\n");
		if(type.getIdent().toString()=="Node") {
				sb.append("\t\tpublic override string "+formatNodeOrEdge(type)+"InterfaceName { get { return "
						+ "\"de.unika.ipd.grGen.libGr.INode\"; } }\n");
		} else if(type.getIdent().toString()=="AEdge" || type.getIdent().toString()=="Edge" || type.getIdent().toString()=="UEdge") {
				sb.append("\t\tpublic override string "+formatNodeOrEdge(type)+"InterfaceName { get { return "
						+ "\"de.unika.ipd.grGen.libGr.IEdge\"; } }\n");
		} else {
			sb.append("\t\tpublic override string "+formatNodeOrEdge(type)+"InterfaceName { get { return "
				+ "\"de.unika.ipd.grGen.Model_" + model.getIdent() + "."
				+ "I" + getNodeOrEdgeTypePrefix(type) + formatIdentifiable(type) + "\"; } }\n");
		}
		if(type.isAbstract()) {
			sb.append("\t\tpublic override string "+formatNodeOrEdge(type)+"ClassName { get { return null; } }\n");
		} else {
			sb.append("\t\tpublic override string "+formatNodeOrEdge(type)+"ClassName { get { return \"de.unika.ipd.grGen.Model_"
					+ model.getIdent() + "." + formatElementClassName(type) + "\"; } }\n");
		}

		if(isNode) {
			sb.append("\t\tpublic override GRGEN_LIBGR.INode CreateNode()\n"
					+ "\t\t{\n");
			if(type.isAbstract())
				sb.append("\t\t\tthrow new Exception(\"The abstract node type "
						+ typeident + " cannot be instantiated!\");\n");
			else
				sb.append("\t\t\treturn new " + allocName + "();\n");
			sb.append("\t\t}\n");
		}
		else {
			EdgeType edgeType = (EdgeType) type;
			sb.append("\t\tpublic override GRGEN_LIBGR.Directedness Directedness "
					+ "{ get { return GRGEN_LIBGR.Directedness.");
			switch(edgeType.getDirectedness()) {
				case Arbitrary:
					sb.append("Arbitrary; } }\n");
					break;
				case Directed:
					sb.append("Directed; } }\n");
					break;
				case Undirected:
					sb.append("Undirected; } }\n");
					break;
				default:
					throw new UnsupportedOperationException("Illegal directedness of edge type \""
							+ formatIdentifiable(type) + "\"");
			}
			sb.append("\t\tpublic override GRGEN_LIBGR.IEdge CreateEdge("
						+ "GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)\n"
					+ "\t\t{\n");
			if(type.isAbstract())
				sb.append("\t\t\tthrow new Exception(\"The abstract edge type "
						+ typeident + " cannot be instantiated!\");\n");
			else
				sb.append("\t\t\treturn new " + allocName
						+ "((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);\n");
			sb.append("\t\t}\n");
		}

		sb.append("\t\tpublic override bool IsAbstract { get { return " + (type.isAbstract() ? "true" : "false") + "; } }\n");
		sb.append("\t\tpublic override bool IsConst { get { return " + (type.isConst() ? "true" : "false") + "; } }\n");

		sb.append("\t\tpublic override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }\n");
		sb.append("\t\tpublic IDictionary<string, string> annotations = new Dictionary<string, string>();\n");

		sb.append("\t\tpublic override int NumAttributes { get { return " + type.getAllMembers().size() + "; } }\n");
		genAttributeTypesEnumerator(type);
		genGetAttributeType(type);

		sb.append("\t\tpublic override bool IsA(GRGEN_LIBGR.GrGenType other)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\treturn (this == other) || isA[other.TypeID];\n");
		sb.append("\t\t}\n");

		genCreateWithCopyCommons(type);
		sb.append("\t}\n");
	}

	private void genIsA(Collection<? extends InheritanceType> types, InheritanceType type) {
		sb.append("\t\tpublic static bool[] isA = new bool[] { ");
		for(InheritanceType nt : types) {
			if(type.isCastableTo(nt))
				sb.append("true, ");
			else
				sb.append("false, ");
		}
		sb.append("};\n");
	}

	private void genIsMyType(Collection<? extends InheritanceType> types, InheritanceType type) {
		sb.append("\t\tpublic static bool[] isMyType = new bool[] { ");
		for(InheritanceType nt : types) {
			if(nt.isCastableTo(type))
				sb.append("true, ");
			else
				sb.append("false, ");
		}
		sb.append("};\n");
	}

	private void genAttributeAttributes(InheritanceType type) {
		for(Entity member : type.getMembers()) { // only for locally defined members
			sb.append("\t\tpublic static GRGEN_LIBGR.AttributeType " + formatAttributeTypeName(member) + ";\n");

			// attribute types T/S of map<T,S>/set<T>/array<T>
			if(member.getType() instanceof MapType) {
				sb.append("\t\tpublic static GRGEN_LIBGR.AttributeType " + formatAttributeTypeName(member)+"_map_domain_type;\n");
				sb.append("\t\tpublic static GRGEN_LIBGR.AttributeType " + formatAttributeTypeName(member)+"_map_range_type;\n");
			}
			if(member.getType() instanceof SetType) {
				sb.append("\t\tpublic static GRGEN_LIBGR.AttributeType " + formatAttributeTypeName(member)+"_set_member_type;\n");
			}
			if(member.getType() instanceof ArrayType) {
				sb.append("\t\tpublic static GRGEN_LIBGR.AttributeType " + formatAttributeTypeName(member)+"_array_member_type;\n");
			}
		}
	}

	private void genAttributeInit(InheritanceType type) {
		for(Entity e : type.getMembers()) {
			String attributeTypeName = formatAttributeTypeName(e);
			Type t = e.getType();

			if (t instanceof MapType) {
				MapType mt = (MapType)t;

				// attribute types T of map<T,S>
				sb.append("\t\t\t" + attributeTypeName + "_map_domain_type = new GRGEN_LIBGR.AttributeType(");
				sb.append("\"" + formatIdentifiable(e) + "_map_domain_type\", this, ");
				genAttributeInitTypeDependentStuff(mt.getKeyType(), e);
				sb.append(");\n");

				// attribute types S of map<T,S>
				sb.append("\t\t\t" + attributeTypeName + "_map_range_type = new GRGEN_LIBGR.AttributeType(");
				sb.append("\"" + formatIdentifiable(e) + "_map_range_type\", this, ");
				genAttributeInitTypeDependentStuff(mt.getValueType(), e);
				sb.append(");\n");
			}
			if (t instanceof SetType) {
				SetType st = (SetType)t;

				// attribute type T of set<T>
				sb.append("\t\t\t" + attributeTypeName + "_set_member_type = new GRGEN_LIBGR.AttributeType(");
				sb.append("\"" + formatIdentifiable(e) + "_set_member_type\", this, ");
				genAttributeInitTypeDependentStuff(st.getValueType(), e);
				sb.append(");\n");
			}
			if (t instanceof ArrayType) {
				ArrayType at = (ArrayType)t;

				// attribute type T of set<T>
				sb.append("\t\t\t" + attributeTypeName + "_array_member_type = new GRGEN_LIBGR.AttributeType(");
				sb.append("\"" + formatIdentifiable(e) + "_array_member_type\", this, ");
				genAttributeInitTypeDependentStuff(at.getValueType(), e);
				sb.append(");\n");
			}

			sb.append("\t\t\t" + attributeTypeName + " = new GRGEN_LIBGR.AttributeType(");
			sb.append("\"" + formatIdentifiable(e) + "\", this, ");
			genAttributeInitTypeDependentStuff(t, e);
			sb.append(");\n");

			addAnnotations(sb, e, attributeTypeName+".annotations");
		}
	}

	private void genAttributeInitTypeDependentStuff(Type t, Entity e) {
		if (t instanceof EnumType) {
			sb.append(getAttributeKind(t) + ", GRGEN_MODEL.Enums.@" + formatIdentifiable(t) + ", "
					+ "null, null, "
					+ "null");
		} else if (t instanceof MapType) {
			sb.append(getAttributeKind(t) + ", null, "
					+ formatAttributeTypeName(e) + "_map_range_type" + ", "
					+ formatAttributeTypeName(e) + "_map_domain_type" + ", "
					+ "null");
		} else if (t instanceof SetType) {
			sb.append(getAttributeKind(t) + ", null, "
					+ formatAttributeTypeName(e) + "_set_member_type" + ", null, "
					+ "null");
		} else if (t instanceof ArrayType) {
			sb.append(getAttributeKind(t) + ", null, "
					+ formatAttributeTypeName(e) + "_array_member_type" + ", null, "
					+ "null");
		} else if (t instanceof NodeType || t instanceof EdgeType) {
			sb.append(getAttributeKind(t) + ", null, "
					+ "null, null, "
					+ "\"" + formatIdentifiable(t) + "\"");
		} else {
			sb.append(getAttributeKind(t) + ", null, "
					+ "null, null, "
					+ "null");
		}
	}

	private String getAttributeKind(Type t) {
		if (t instanceof ByteType)
			return "GRGEN_LIBGR.AttributeKind.ByteAttr";
		else if (t instanceof ShortType)
			return "GRGEN_LIBGR.AttributeKind.ShortAttr";
		else if (t instanceof IntType)
			return "GRGEN_LIBGR.AttributeKind.IntegerAttr";
		else if (t instanceof LongType)
			return "GRGEN_LIBGR.AttributeKind.LongAttr";
		else if (t instanceof FloatType)
			return "GRGEN_LIBGR.AttributeKind.FloatAttr";
		else if (t instanceof DoubleType)
			return "GRGEN_LIBGR.AttributeKind.DoubleAttr";
		else if (t instanceof BooleanType)
			return "GRGEN_LIBGR.AttributeKind.BooleanAttr";
		else if (t instanceof StringType)
			return "GRGEN_LIBGR.AttributeKind.StringAttr";
		else if (t instanceof EnumType)
			return "GRGEN_LIBGR.AttributeKind.EnumAttr";
		else if (t instanceof ObjectType || t instanceof VoidType || t instanceof ExternalType)
			return "GRGEN_LIBGR.AttributeKind.ObjectAttr";
		else if (t instanceof MapType)
			return "GRGEN_LIBGR.AttributeKind.MapAttr";
		else if (t instanceof SetType)
			return "GRGEN_LIBGR.AttributeKind.SetAttr";
		else if (t instanceof ArrayType)
			return "GRGEN_LIBGR.AttributeKind.ArrayAttr";
		else if (t instanceof NodeType)
			return "GRGEN_LIBGR.AttributeKind.NodeAttr";
		else if (t instanceof EdgeType)
			return "GRGEN_LIBGR.AttributeKind.EdgeAttr";
		else if (t instanceof GraphType)
			return "GRGEN_LIBGR.AttributeKind.GraphAttr";
		else throw new IllegalArgumentException("Unknown Type: " + t);
	}

	private void genAttributeTypesEnumerator(InheritanceType type) {
		Collection<Entity> allMembers = type.getAllMembers();
		sb.append("\t\tpublic override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes");

		if(allMembers.isEmpty())
			sb.append(" { get { yield break; } }\n");
		else {
			sb.append("\n\t\t{\n");
			sb.append("\t\t\tget\n");
			sb.append("\t\t\t{\n");
			for(Entity e : allMembers) {
				Type ownerType = e.getOwner();
				if(ownerType == type)
					sb.append("\t\t\t\tyield return " + formatAttributeTypeName(e) + ";\n");
				else
					sb.append("\t\t\t\tyield return " + formatTypeClassRef(ownerType) + "." + formatAttributeTypeName(e) + ";\n");
			}
			sb.append("\t\t\t}\n");
			sb.append("\t\t}\n");
		}
	}

	private void genGetAttributeType(InheritanceType type) {
		Collection<Entity> allMembers = type.getAllMembers();
		sb.append("\t\tpublic override GRGEN_LIBGR.AttributeType GetAttributeType(string name)");

		if(allMembers.isEmpty())
			sb.append(" { return null; }\n");
		else {
			sb.append("\n\t\t{\n");
			sb.append("\t\t\tswitch(name)\n");
			sb.append("\t\t\t{\n");
			for(Entity e : allMembers) {
				Type ownerType = e.getOwner();
				if(ownerType == type)
					sb.append("\t\t\t\tcase \"" + formatIdentifiable(e) + "\" : return " +
							formatAttributeTypeName(e) + ";\n");
				else
					sb.append("\t\t\t\tcase \"" + formatIdentifiable(e) + "\" : return " +
							formatTypeClassRef(ownerType) + "." + formatAttributeTypeName(e) + ";\n");
			}
			sb.append("\t\t\t}\n");
			sb.append("\t\t\treturn null;\n");
			sb.append("\t\t}\n");
		}
	}

	private void getFirstCommonAncestors(InheritanceType curType,
			InheritanceType type, Set<InheritanceType> resTypes) {
		if(type.isCastableTo(curType))
			resTypes.add(curType);
		else
			for(InheritanceType superType : curType.getDirectSuperTypes())
				getFirstCommonAncestors(superType, type, resTypes);
	}

	private void genCreateWithCopyCommons(InheritanceType type) {
		boolean isNode = type instanceof NodeType;
		String elemref = formatElementClassRef(type);
		String extName = type.getExternalName();
		String allocName = extName != null ? "global::" + extName : elemref;
		String kindName = isNode ? "Node" : "Edge";

		if(isNode) {
			sb.append("\t\tpublic override GRGEN_LIBGR.INode CreateNodeWithCopyCommons("
						+ "GRGEN_LIBGR.INode oldINode)\n"
					+ "\t\t{\n");
		}
		else {
			sb.append("\t\tpublic override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons("
						+ "GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, "
						+ "GRGEN_LIBGR.IEdge oldIEdge)\n"
					+ "\t\t{\n");
		}

		if(type.isAbstract()) {
			sb.append("\t\t\tthrow new Exception(\"Cannot retype to the abstract type "
					+ formatIdentifiable(type) + "!\");\n"
					+ "\t\t}\n");
			return;
		}

		Map<BitSet, LinkedList<InheritanceType>> commonGroups = new LinkedHashMap<BitSet, LinkedList<InheritanceType>>();

		Collection<? extends InheritanceType> typeSet =
			isNode ? (Collection<? extends InheritanceType>) model.getNodeTypes()
			: (Collection<? extends InheritanceType>) model.getEdgeTypes();
		for(InheritanceType itype : typeSet) {
			if(itype.isAbstract()) continue;

			Set<InheritanceType> firstCommonAncestors = new LinkedHashSet<InheritanceType>();
			getFirstCommonAncestors(itype, type, firstCommonAncestors);

			TreeSet<InheritanceType> sortedCommonTypes = new TreeSet<InheritanceType>(
				new Comparator<InheritanceType>() {
					public int compare(InheritanceType o1, InheritanceType o2) {
						return o2.getMaxDist() - o1.getMaxDist();
					}
				});

			sortedCommonTypes.addAll(firstCommonAncestors);
			Iterator<InheritanceType> iter = sortedCommonTypes.iterator();
			while(iter.hasNext()) {
				InheritanceType commonType = iter.next();
				if(!firstCommonAncestors.contains(commonType)) continue;
				for(InheritanceType superType : commonType.getAllSuperTypes()) {
					firstCommonAncestors.remove(superType);
				}
			}

			boolean mustCopyAttribs = false;
commonLoop:	for(InheritanceType commonType : firstCommonAncestors) {
				for(Entity member : commonType.getAllMembers()) {
					if(member.getType().isVoid())   // is it an abstract member?
						continue;
					mustCopyAttribs = true;
					break commonLoop;
				}
			}

			if(!mustCopyAttribs) continue;

			BitSet commonTypesBitset = new BitSet();
			for(InheritanceType commonType : firstCommonAncestors) {
				commonTypesBitset.set(commonType.getTypeID());
			}
			LinkedList<InheritanceType> commonList = commonGroups.get(commonTypesBitset);
			if(commonList == null) {
				commonList = new LinkedList<InheritanceType>();
				commonGroups.put(commonTypesBitset, commonList);
			}
			commonList.add(itype);
		}

		if(commonGroups.size() != 0) {
			if(isNode)
				sb.append("\t\t\tGRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;\n"
						+ "\t\t\t" + elemref + " newNode = new " + allocName + "();\n");
			else
				sb.append("\t\t\tGRGEN_LGSP.LGSPEdge oldEdge = (GRGEN_LGSP.LGSPEdge) oldIEdge;\n"
						+ "\t\t\t" + elemref + " newEdge = new " + allocName
						+ "((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);\n");
			sb.append("\t\t\tswitch(old" + kindName + ".Type.TypeID)\n"
					+ "\t\t\t{\n");
			for(Map.Entry<BitSet, LinkedList<InheritanceType>> entry : commonGroups.entrySet()) {
				for(InheritanceType itype : entry.getValue()) {
					sb.append("\t\t\t\tcase (int) " + kindName + "Types.@"
							+ formatIdentifiable(itype) + ":\n");
				}
				BitSet bitset = entry.getKey();
				HashSet<Entity> copiedAttribs = new HashSet<Entity>();
				for(int i = bitset.nextSetBit(0); i >= 0; i = bitset.nextSetBit(i+1)) {
					InheritanceType commonType = InheritanceType.getByTypeID(i);
					Collection<Entity> members = commonType.getAllMembers();
					if(members.size() != 0) {
						sb.append("\t\t\t\t\t// copy attributes for: "
								+ formatIdentifiable(commonType) + "\n");
						boolean alreadyCasted = false;
						for(Entity member : members) {
							if(member.isConst()) {
								sb.append("\t\t\t\t\t\t// is const: " + formatIdentifiable(member) + "\n");
								continue;
							}
							if(member.getType().isVoid()) {
								sb.append("\t\t\t\t\t\t// is abstract: " + formatIdentifiable(member) + "\n");
								continue;
							}
							if(copiedAttribs.contains(member)) {
								sb.append("\t\t\t\t\t\t// already copied: " + formatIdentifiable(member) + "\n");
								continue;
							}
							if(!alreadyCasted) {
								alreadyCasted = true;
								sb.append("\t\t\t\t\t{\n\t\t\t\t\t\t"
										+ formatVarDeclWithCast(formatElementInterfaceRef(commonType), "old")
										+ "old" + kindName + ";\n");
							}
							copiedAttribs.add(member);
							String memberName = formatIdentifiable(member);
							// what's that?
							if(type.getOverriddenMember(member) != null) {
								// Workaround for Mono Bug 357287
								// "Access to hiding properties of interfaces resolves wrong member"
								// https://bugzilla.novell.com/show_bug.cgi?id=357287
								sb.append("\t\t\t\t\t\tnew" + kindName + ".@" + memberName
										+ " = (" + formatAttributeType(member) + ") old.@" + memberName
										+ ";   // Mono workaround (bug #357287)\n");
							} else {
								if(member.getType() instanceof MapType || member.getType() instanceof SetType || member.getType() instanceof ArrayType) {
									sb.append("\t\t\t\t\t\tnew" + kindName + ".@" + memberName
											+ " = new " + formatAttributeType(member.getType()) + "(old.@" + memberName + ");\n");
								} else {
									sb.append("\t\t\t\t\t\tnew" + kindName + ".@" + memberName
											+ " = old.@" + memberName + ";\n");
								}
							}
						}
						if(alreadyCasted)
							sb.append("\t\t\t\t\t}\n");
					}
				}
				sb.append("\t\t\t\t\tbreak;\n");
			}
			sb.append("\t\t\t}\n"
					+ "\t\t\treturn new" + kindName + ";\n"
					+ "\t\t}\n\n");
		}
		else {
			if(isNode) {
				sb.append("\t\t\treturn new " + allocName + "();\n"
					+ "\t\t}\n\n");
			} else {
				sb.append("\t\t\treturn new " + allocName
						+ "((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);\n"
					+ "\t\t}\n\n");
			}
		}
	}

	////////////////////////////
	// Model class generation //
	////////////////////////////

	/**
	 * Generates the model class for the edge or node types.
	 */
	private void genModelClass(Collection<? extends InheritanceType> types, boolean isNode) {
		String kindStr = isNode ? "Node" : "Edge";

		sb.append("\t//\n");
		sb.append("\t// " + formatNodeOrEdge(isNode) + " model\n");
		sb.append("\t//\n");
		sb.append("\n");
		sb.append("\tpublic sealed class " + model.getIdent() + formatNodeOrEdge(isNode)
				+ "Model : GRGEN_LIBGR.I" + kindStr + "Model\n");
		sb.append("\t{\n");

		InheritanceType rootType = genModelConstructor(isNode, types);

		sb.append("\t\tpublic bool IsNodeModel { get { return " + (isNode?"true":"false") +"; } }\n");
		sb.append("\t\tpublic GRGEN_LIBGR." + kindStr + "Type RootType { get { return "
				+ formatTypeClassRef(rootType) + ".typeVar; } }\n");
		sb.append("\t\tGRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.RootType { get { return "
				+ formatTypeClassRef(rootType) + ".typeVar; } }\n");
		sb.append("\t\tpublic GRGEN_LIBGR." + kindStr + "Type GetType(string name)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tswitch(name)\n");
		sb.append("\t\t\t{\n");
		for(InheritanceType type : types)
			sb.append("\t\t\t\tcase \"" + formatIdentifiable(type) + "\" : return " + formatTypeClassRef(type) + ".typeVar;\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\treturn null;\n");
		sb.append("\t\t}\n");
		sb.append("\t\tGRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(string name)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\treturn GetType(name);\n");
		sb.append("\t\t}\n");

		sb.append("\t\tprivate GRGEN_LIBGR." + kindStr + "Type[] types = {\n");
		for(InheritanceType type : types)
			sb.append("\t\t\t" + formatTypeClassRef(type) + ".typeVar,\n");
		sb.append("\t\t};\n");
		sb.append("\t\tpublic GRGEN_LIBGR." + kindStr + "Type[] Types { get { return types; } }\n");
		sb.append("\t\tGRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types "
				+ "{ get { return types; } }\n");

		sb.append("\t\tprivate System.Type[] typeTypes = {\n");
		for(InheritanceType type : types)
			sb.append("\t\t\ttypeof(" + formatTypeClassRef(type) + "),\n");
		sb.append("\t\t};\n");
		sb.append("\t\tpublic System.Type[] TypeTypes { get { return typeTypes; } }\n");

		sb.append("\t\tprivate GRGEN_LIBGR.AttributeType[] attributeTypes = {\n");
		for(InheritanceType type : types) {
			String ctype = formatTypeClassRef(type);
			for(Entity member : type.getMembers())
				sb.append("\t\t\t" + ctype + "." + formatAttributeTypeName(member) + ",\n");
		}
		sb.append("\t\t};\n");
		sb.append("\t\tpublic IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes "
				+ "{ get { return attributeTypes; } }\n");

		sb.append("\t}\n");
	}

	private InheritanceType genModelConstructor(boolean isNode, Collection<? extends InheritanceType> types) {
		String kindStr = (isNode ? "Node" : "Edge");
		InheritanceType rootType = null;

		sb.append("\t\tpublic " + model.getIdent() + formatNodeOrEdge(isNode) + "Model()\n");
		sb.append("\t\t{\n");
		for(InheritanceType type : types) {
			String ctype = formatTypeClassRef(type);
			sb.append("\t\t\t" + ctype + ".typeVar.subOrSameGrGenTypes = "
					+ ctype + ".typeVar.subOrSameTypes = new GRGEN_LIBGR."
					+ kindStr + "Type[] {\n");
			sb.append("\t\t\t\t" + ctype + ".typeVar,\n");
			for(InheritanceType otherType : types) {
				if(type != otherType && otherType.isCastableTo(type))
					sb.append("\t\t\t\t" + formatTypeClassRef(otherType) + ".typeVar,\n");
			}
			sb.append("\t\t\t};\n");

			sb.append("\t\t\t" + ctype + ".typeVar.directSubGrGenTypes = "
					+ ctype + ".typeVar.directSubTypes = new GRGEN_LIBGR."
					+ kindStr + "Type[] {\n");
			for(InheritanceType subType : type.getDirectSubTypes()) {
				// TODO: HACK, because direct sub types may also contain types from other models...
				if(!types.contains(subType))
					continue;
				sb.append("\t\t\t\t" + formatTypeClassRef(subType) + ".typeVar,\n");
			}
			sb.append("\t\t\t};\n");

			sb.append("\t\t\t" + ctype + ".typeVar.superOrSameGrGenTypes = "
					+ ctype + ".typeVar.superOrSameTypes = new GRGEN_LIBGR."
					+ kindStr + "Type[] {\n");
			sb.append("\t\t\t\t" + ctype + ".typeVar,\n");
			for(InheritanceType otherType : types) {
				if(type != otherType && type.isCastableTo(otherType))
					sb.append("\t\t\t\t" + formatTypeClassRef(otherType) + ".typeVar,\n");
			}
			sb.append("\t\t\t};\n");

			sb.append("\t\t\t" + ctype + ".typeVar.directSuperGrGenTypes = "
					+ ctype + ".typeVar.directSuperTypes = new GRGEN_LIBGR."
					+ kindStr + "Type[] {\n");
			for(InheritanceType superType : type.getDirectSuperTypes()) {
				sb.append("\t\t\t\t" + formatTypeClassRef(superType) + ".typeVar,\n");
			}
			sb.append("\t\t\t};\n");

			if(type.isRoot())
				rootType = type;
		}
		sb.append("\t\t}\n");

		return rootType;
	}

	/**
	 * Generates the graph model class.
	 */
	private void genGraphModel() {
		String modelName = model.getIdent().toString();
		sb.append("\t//\n");
		sb.append("\t// IGraphModel implementation\n");
		sb.append("\t//\n");

		sb.append("\tpublic sealed class " + modelName + "GraphModel : GRGEN_LIBGR.IGraphModel\n");
		sb.append("\t{\n");
		genGraphModelBody(modelName);
		sb.append("\t}\n");
		sb.append("\n");

		genGraphIncludingModelClass();

		sb.append("\n");

		genNamedGraphIncludingModelClass();
	}

	private void genGraphIncludingModelClass() {
		String modelName = model.getIdent().toString();

		sb.append("\t//\n");
		sb.append("\t// IGraph/IGraphModel implementation\n");
		sb.append("\t//\n");

		sb.append(
			  "\tpublic class " + modelName + "Graph : GRGEN_LGSP.LGSPGraph, GRGEN_LIBGR.IGraphModel\n"
			+ "\t{\n"
			+ "\t\tpublic " + modelName + "Graph() : base(GetNextGraphName())\n"
			+ "\t\t{\n"
			+ "\t\t\tInitializeGraph(this);\n"
			+ "\t\t}\n\n"
		);

		for(NodeType nodeType : model.getNodeTypes()) {
			if(nodeType.isAbstract()) continue;
			String name = formatIdentifiable(nodeType);
			String elemref = formatElementClassRef(nodeType);
			sb.append(
				  "\t\tpublic " + elemref + " CreateNode" + name + "()\n"
				+ "\t\t{\n"
				+ "\t\t\treturn " + elemref + ".CreateNode(this);\n"
				+ "\t\t}\n\n"
			);
		}

		for(EdgeType edgeType : model.getEdgeTypes()) {
			if(edgeType.isAbstract()) continue;
			String name = formatIdentifiable(edgeType);
			String elemref = formatElementClassRef(edgeType);
			sb.append(
				  "\t\tpublic @" + elemref + " CreateEdge" + name
					+ "(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)\n"
				+ "\t\t{\n"
				+ "\t\t\treturn @" + elemref + ".CreateEdge(this, source, target);\n"
				+ "\t\t}\n\n"
			);
		}

		genGraphModelBody(modelName);
		sb.append("\t}\n");
	}

	private void genNamedGraphIncludingModelClass() {
		String modelName = model.getIdent().toString();

		sb.append("\t//\n");
		sb.append("\t// INamedGraph/IGraphModel implementation\n");
		sb.append("\t//\n");

		sb.append(
			  "\tpublic class " + modelName + "NamedGraph : GRGEN_LGSP.LGSPNamedGraph, GRGEN_LIBGR.IGraphModel\n"
			+ "\t{\n"
			+ "\t\tpublic " + modelName + "NamedGraph() : base(GetNextGraphName())\n"
			+ "\t\t{\n"
			+ "\t\t\tInitializeGraph(this);\n"
			+ "\t\t}\n\n"
		);

		for(NodeType nodeType : model.getNodeTypes()) {
			if(nodeType.isAbstract()) continue;
			String name = formatIdentifiable(nodeType);
			String elemref = formatElementClassRef(nodeType);
			sb.append(
				  "\t\tpublic " + elemref + " CreateNode" + name + "()\n"
				+ "\t\t{\n"
				+ "\t\t\treturn " + elemref + ".CreateNode(this);\n"
				+ "\t\t}\n\n"
				+ "\t\tpublic " + elemref + " CreateNode" + name + "(string nodeName)\n"
				+ "\t\t{\n"
				+ "\t\t\treturn " + elemref + ".CreateNode(this, nodeName);\n"
				+ "\t\t}\n\n"
			);
		}

		for(EdgeType edgeType : model.getEdgeTypes()) {
			if(edgeType.isAbstract()) continue;
			String name = formatIdentifiable(edgeType);
			String elemref = formatElementClassRef(edgeType);
			sb.append(
				  "\t\tpublic @" + elemref + " CreateEdge" + name
					+ "(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)\n"
				+ "\t\t{\n"
				+ "\t\t\treturn @" + elemref + ".CreateEdge(this, source, target);\n"
				+ "\t\t}\n\n"
				+ "\t\tpublic @" + elemref + " CreateEdge" + name
					+ "(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)\n"
				+ "\t\t{\n"
				+ "\t\t\treturn @" + elemref + ".CreateEdge(this, source, target, edgeName);\n"
				+ "\t\t}\n\n"
			);
		}

		genGraphModelBody(modelName);
		sb.append("\t}\n");
	}

	private void genGraphModelBody(String modelName) {
		sb.append("\t\tprivate " + modelName + "NodeModel nodeModel = new " + modelName + "NodeModel();\n");
		sb.append("\t\tprivate " + modelName + "EdgeModel edgeModel = new " + modelName + "EdgeModel();\n");
		genValidate();
		genEnumAttributeTypeArray();
		sb.append("\n");

		sb.append("\t\tpublic string ModelName { get { return \"" + modelName + "\"; } }\n");
		sb.append("\t\tpublic GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }\n");
		sb.append("\t\tpublic GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }\n");
		sb.append("\t\tpublic IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo "
				+ "{ get { return validateInfos; } }\n");
		sb.append("\t\tpublic IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes "
				+ "{ get { return enumAttributeTypes; } }\n");
		sb.append("\t\tpublic string MD5Hash { get { return \"" + be.unit.getTypeDigest() + "\"; } }\n");
	}

	private void genValidate() {
		sb.append("\t\tprivate GRGEN_LIBGR.ValidateInfo[] validateInfos = {\n");

		for(EdgeType edgeType : model.getEdgeTypes()) {
			for(ConnAssert ca : edgeType.getConnAsserts()) {
				sb.append("\t\t\tnew GRGEN_LIBGR.ValidateInfo(");
				sb.append(formatTypeClassRef(edgeType) + ".typeVar, ");
				sb.append(formatTypeClassRef(ca.getSrcType()) + ".typeVar, ");
				sb.append(formatTypeClassRef(ca.getTgtType()) + ".typeVar, ");
				sb.append(formatLong(ca.getSrcLower()) + ", ");
				sb.append(formatLong(ca.getSrcUpper()) + ", ");
				sb.append(formatLong(ca.getTgtLower()) + ", ");
				sb.append(formatLong(ca.getTgtUpper()) + ", ");
				sb.append(ca.getBothDirections());
				sb.append("),\n");
			}
		}

		sb.append("\t\t};\n");
	}

	private void genEnumAttributeTypeArray() {
		sb.append("\t\tprivate GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {\n");
		for(EnumType enumt : model.getEnumTypes()) {
			sb.append("\t\t\tGRGEN_MODEL.Enums.@" + formatIdentifiable(enumt) + ",\n");
		}
		sb.append("\t\t};\n");
	}

	///////////////////////////////
	// External stuff generation //
	///////////////////////////////

	private void genExternalClasses() {
		for(ExternalType et : model.getExternalTypes()) {
			sb.append("\tpublic partial class " + et.getIdent());
			boolean first = true;
			for(InheritanceType superType : et.getAllSuperTypes()) {
				if(first) {
					sb.append(" : ");
				} else {
					sb.append(", ");
				}
				sb.append(superType.getIdent());
				first = false;
			}
			sb.append("\n");
			sb.append("\t{\n");
			sb.append("\t\t// You must implement this class in the same partial class in ./" + model.getIdent() + "ModelExternalFunctionsImpl.cs:\n");
			sb.append("\t}\n");
		}
	}

	private void genExternalFunctionHeaders() {
		for(ExternalFunction ef : model.getExternalFunctions()) {
			Type returnType = ef.getReturnType();
			sb.append("\t\t//public static " + formatType(returnType) + " " + ef.getName() + "(");
			boolean first = true;
			for(Type paramType : ef.getParameterTypes()) {
				if(!first) sb.append(", ");
				sb.append(formatType(paramType));
				first = false;
			}
			sb.append(");\n");
		}
	}

	///////////////////////
	// Private variables //
	///////////////////////

	private SearchPlanBackend2 be;
	private Model model;
	private StringBuffer sb = null;
	private StringBuffer stubsb = null;
	private String curMemberOwner = null;
	private String nsIndent = "\t";
	private HashSet<String> rootTypes;
}

