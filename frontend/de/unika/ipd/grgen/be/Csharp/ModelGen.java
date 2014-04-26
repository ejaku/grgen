/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Generates the model files for the SearchPlanBackend2 backend.
 * @author Moritz Kroll
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
import de.unika.ipd.grgen.ir.exprevals.*;
import de.unika.ipd.grgen.ir.containers.*;

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
		mgFuncComp = new ModifyGen(backend, nodeTypePrefix, edgeTypePrefix);
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
				+ "using System.IO;\n"
                + "using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n"
                + "using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n"
                + "using GRGEN_EXPR = de.unika.ipd.grGen.expression;\n"
				+ "\n"
				+ "namespace de.unika.ipd.grGen.Model_" + model.getIdent() + "\n"
				+ "{\n"
				+ "\tusing GRGEN_MODEL = de.unika.ipd.grGen.Model_" + model.getIdent() + ";\n");

		for(PackageType pt : model.getPackages()) {
			System.out.println("    generating package " + pt.getIdent() + "...");
	
			sb.append("\n");
			sb.append("\t//-----------------------------------------------------------\n");
			sb.append("\tnamespace ");
			sb.append(formatIdentifiable(pt));
			sb.append("\n");
			sb.append("\t//-----------------------------------------------------------\n");
			sb.append("\t{\n");
	
			genBearer(model.getAllNodeTypes(), model.getAllEdgeTypes(),
					pt, pt.getIdent().toString());
	
			sb.append("\n");
			sb.append("\t//-----------------------------------------------------------\n");
			sb.append("\t}\n");
			sb.append("\t//-----------------------------------------------------------\n");
		}

		genBearer(model.getAllNodeTypes(), model.getAllEdgeTypes(),
				model, null);

		System.out.println("    generating indices...");

		sb.append("\n");
		sb.append("\t//\n");
		sb.append("\t// Indices\n");
		sb.append("\t//\n");
		sb.append("\n");

		genIndexTypes();
		genIndexImplementations();
		genIndexSetType();

		System.out.println("    generating node model...");
		sb.append("\n");
		genModelClass(model.getAllNodeTypes(), true);

		System.out.println("    generating edge model...");
		sb.append("\n");
		genModelClass(model.getAllEdgeTypes(), false);

		System.out.println("    generating graph model...");
		sb.append("\n");
		genGraphModel();
		sb.append("\n");
		genGraphIncludingModelClass();
		sb.append("\n");
		genNamedGraphIncludingModelClass();

		sb.append("}\n");

		System.out.println("    writing to " + be.path + " / " + filename);
		writeFile(be.path, filename, sb);

		if(stubsb != null) {
			String stubFilename = model.getIdent() + "ModelStub.cs";
			System.out.println("  writing the " + stubFilename + " stub file...");
			writeFile(be.path, stubFilename, stubsb);
		}

		
		///////////////////////////////////////////////////////////////////////////////////////////
		// generate the external functions and types stub file
		// only if there are external functions or external procedures or external types required 
		// or the emit class is to be generated or the copy class is to be generated
		if(model.getExternalTypes().isEmpty() 
				&& model.getExternalFunctions().isEmpty()
				&& model.getExternalProcedures().isEmpty()
				&& !model.isEmitClassDefined()
				&& !model.isCopyClassDefined()
				&& !model.isEqualClassDefined()
				&& !model.isLowerClassDefined())
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
				+ "using System.IO;\n"
                + "using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n"
                + "using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n");

		if(!model.getExternalTypes().isEmpty() || model.isEmitClassDefined())
		{
			sb.append("\n");
			sb.append("namespace de.unika.ipd.grGen.Model_" + model.getIdent() + "\n"
					+ "{\n");

			genExternalClasses();

			if(model.isEmitClassDefined())
				genEmitterParserClass();

			if(model.isCopyClassDefined() || model.isEqualClassDefined() || model.isLowerClassDefined())
				genCopierComparerClass();

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

		if(!model.getExternalProcedures().isEmpty())
		{
			sb.append("\n");
			sb.append("namespace de.unika.ipd.grGen.expression\n"
					+ "{\n"
					+ "\tusing GRGEN_MODEL = de.unika.ipd.grGen.Model_" + model.getIdent() + ";\n"
					+ "\n");

			sb.append("\tpublic partial class ExternalProcedures\n");
			sb.append("\t{\n");
			sb.append("\t\t// You must implement the following procedures in the same partial class in ./" + model.getIdent() + "ModelExternalFunctionsImpl.cs:\n"
					+ "\n");

			genExternalProcedureHeaders();

			sb.append("\t}\n");

			sb.append("}\n");
		}

		System.out.println("    writing to " + be.path + " / " + filename);
		writeFile(be.path, filename, sb);

		if(be.path.compareTo(new File("."))==0) {
			System.out.println("    no copy needed for " + be.path + " / " + filename);
		} else {
			System.out.println("    copying " + be.path + " / " + filename + " to " + be.path.getAbsoluteFile().getParent() + " / " + filename);
			copyFile(new File(be.path, filename), new File(be.path.getAbsoluteFile().getParent(), filename));
		}
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

	private void genBearer(Collection<? extends InheritanceType> allNodeTypes,
			Collection<? extends InheritanceType> allEdgeTypes,
			NodeEdgeEnumBearer bearer, String packageName) {
		
		System.out.println("    generating enums...");
		sb.append("\n");
		genEnums(bearer);

		System.out.println("    generating node types...");
		sb.append("\n");
		genTypes(allNodeTypes, bearer, packageName, true);

		System.out.println("    generating edge types...");
		sb.append("\n");
		genTypes(allEdgeTypes, bearer, packageName, false);
	}

	private void genEnums(NodeEdgeEnumBearer bearer) {
		sb.append("\t//\n");
		sb.append("\t// Enums\n");
		sb.append("\t//\n");
		sb.append("\n");

		for(EnumType enumt : bearer.getEnumTypes()) {
			sb.append("\tpublic enum ENUM_" + formatIdentifiable(enumt) + " { ");
			for(EnumItem enumi : enumt.getItems()) {
				sb.append("@" + formatIdentifiable(enumi) + " = " + enumi.getValue().getValue() + ", ");
			}
			sb.append("};\n\n");
		}

		sb.append("\tpublic class Enums\n");
		sb.append("\t{\n");
		for(EnumType enumt : bearer.getEnumTypes()) {
			sb.append("\t\tpublic static GRGEN_LIBGR.EnumAttributeType @" + formatIdentifiable(enumt)
					+ " = new GRGEN_LIBGR.EnumAttributeType(\"" + formatIdentifiable(enumt) + "\", "
					+ (!getPackagePrefix(enumt).equals("") ? "\""+getPackagePrefix(enumt)+"\"" : "null") + ", "
					+ "\"" + getPackagePrefixDoubleColon(enumt) + formatIdentifiable(enumt) + "\", "
					+ "typeof(GRGEN_MODEL." + getPackagePrefixDot(enumt) + "ENUM_" + formatIdentifiable(enumt) + "), "
					+ "new GRGEN_LIBGR.EnumMember[] {\n");
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
	private void genTypes(Collection<? extends InheritanceType> allTypes, 
			NodeEdgeEnumBearer bearer, String packageName, boolean isNode) {
		Collection<? extends InheritanceType> curTypes = 
			isNode ? bearer.getNodeTypes() : bearer.getEdgeTypes();
		
		sb.append("\t//\n");
		sb.append("\t// " + formatNodeOrEdge(isNode) + " types\n");
		sb.append("\t//\n");
		sb.append("\n");
		sb.append("\tpublic enum " + formatNodeOrEdge(isNode) + "Types ");

		sb.append("{ ");
		for(Iterator<? extends InheritanceType> iter = curTypes.iterator(); iter.hasNext();) {
			InheritanceType id = iter.next();
			sb.append("@" + formatIdentifiable(id) + "=" + id.getNodeOrEdgeTypeID(isNode));
			if(iter.hasNext())
				sb.append(", ");
		}
		sb.append(" }");

		sb.append(";\n");

		for(InheritanceType type : curTypes) {
			genType(allTypes, type, packageName);
		}
	}

	/**
	 * Generates all code for a given type.
	 */
	private void genType(Collection<? extends InheritanceType> allTypes, InheritanceType type, String packageName) {
		sb.append("\n");
		sb.append("\t// *** " + formatNodeOrEdge(type) + " " + formatIdentifiable(type) + " ***\n");
		sb.append("\n");

		if(!rootTypes.contains(type.getIdent().toString()))
			genElementInterface(type);
		if(!type.isAbstract())
			genElementImplementation(type);
		genTypeImplementation(allTypes, type, packageName);
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
		genMethodInterfaces(type, type.getFunctionMethods(), type.getProcedureMethods(), "");
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
				sb.append(getPackagePrefixDot(superType) + iprefix + formatIdentifiable(superType));
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

	private void genMethodInterfaces(InheritanceType type, Collection<FunctionMethod> functionMethods,
			Collection<ProcedureMethod> procedureMethods, String modifiers) {
		// METHOD-TODO - inheritance?
		for(FunctionMethod fm : functionMethods) {
			sb.append("\t\t" + formatType(fm.getReturnType()) + " ");
			sb.append(fm.getIdent().toString() + "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph");
			for(Entity inParam : fm.getParameters()) {
				sb.append(", ");
				sb.append(formatType(inParam.getType()));
				sb.append(" ");
				sb.append(formatEntity(inParam));
			}
			sb.append(");\n");
			
			if(be.unit.isToBeParallelizedActionExisting()) {
				sb.append("\t\t" + formatType(fm.getReturnType()) + " ");
				sb.append(fm.getIdent().toString() + "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph");
				for(Entity inParam : fm.getParameters()) {
					sb.append(", ");
					sb.append(formatType(inParam.getType()));
					sb.append(" ");
					sb.append(formatEntity(inParam));
				}
				sb.append(", int threadId");
				sb.append(");\n");
			}
		}
		for(ProcedureMethod pm : procedureMethods) {
			sb.append("\t\tvoid ");
			sb.append(pm.getIdent().toString() + "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph");
			for(Entity inParam : pm.getParameters()) {
				sb.append(", ");
				sb.append(formatType(inParam.getType()));
				sb.append(" ");
				sb.append(formatEntity(inParam));
			}
			int i = 0;
			for(Type outType : pm.getReturnTypes()) {
				sb.append(", out ");
				sb.append(formatType(outType));
				sb.append(" ");
				sb.append("_out_param_" + i);
				++i;
			}
			sb.append(");\n");
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
		if(model.isUniqueDefined())
			routedSB.append("\t\t\tuniqueId = oldElem.uniqueId;\n");
		for(Entity member : type.getAllMembers()) {
			if(member.isConst())
				continue;

			String attrName = formatIdentifiable(member);
			if(member.getType() instanceof MapType || member.getType() instanceof SetType 
					|| member.getType() instanceof ArrayType || member.getType() instanceof DequeType) {
				routedSB.append("\t\t\t" + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = new " + formatAttributeType(member.getType())
						+ "(oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ");\n");
			} else if(model.isCopyClassDefined()
					&& (member.getType().classify() == Type.IS_EXTERNAL_TYPE
							|| member.getType().classify() == Type.IS_OBJECT)) {
				routedSB.append("\t\t\tAttributeTypeObjectCopierComparer.Copy(" + "oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ");\n");
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
			if(member.getType() instanceof MapType || member.getType() instanceof SetType 
					|| member.getType() instanceof ArrayType || member.getType() instanceof DequeType) {
				routedSB.append("\t\t\t\t&& GRGEN_LIBGR.ContainerHelper.Equal(" + attrName + ModelGen.ATTR_IMPL_SUFFIX + ", "
						+ "that_." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ")\n");
			} else if(model.isEqualClassDefined()
					&& (member.getType().classify() == Type.IS_EXTERNAL_TYPE
							|| member.getType().classify() == Type.IS_OBJECT)) {
				routedSB.append("\t\t\t\t&& AttributeTypeObjectCopierComparer.IsEqual(" + attrName + ModelGen.ATTR_IMPL_SUFFIX + ", "
						+ "that_." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ")\n");
			} else if(member.getType().classify() == Type.IS_GRAPH) {
				routedSB.append("\t\t\t\t&& GRGEN_LIBGR.GraphHelper.Equal(" + attrName + ModelGen.ATTR_IMPL_SUFFIX + ", "
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

		genMethods(type);

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

		sb.append(indentString + "// implicit initialization, container creation of " + formatIdentifiable(type) + "\n");

		// default attribute inits need to be generated if code must overwrite old values
		// only in constructor not needed, cause there taken care of by c#
		// if there is explicit initialization code, it's not needed, too,
		// but that's left for the compiler to optimize away
		if(withDefaultInits) {
			for(Entity member : type.getAllMembers()) {
				if(member.isConst())
					continue;

				Type t = member.getType();
				// handled down below, as containers must be created independent of initialization
				if(t instanceof MapType || t instanceof SetType
						|| t instanceof ArrayType || t instanceof DequeType)
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
				} else if(t instanceof StringType || t instanceof ObjectType || t instanceof VoidType 
						|| t instanceof ExternalType || t instanceof GraphType || t instanceof InheritanceType) {
					sb.append("null;\n");
				} else {
					throw new IllegalArgumentException("Unknown Entity: " + member + "(" + t + ")");
				}
			}
		}

		// create containers, i.e. maps, sets, arrays, deques
		for(Entity member : type.getAllMembers()) {
			if(member.isConst())
				continue;

			Type t = member.getType();
			if(!(t instanceof MapType || t instanceof SetType
					|| t instanceof ArrayType || t instanceof DequeType))
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
			} else if(t instanceof DequeType) {
				DequeType dequeType = (DequeType) t;
				sb.append("new " + formatAttributeType(dequeType) + "();\n");
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
deque_init_loop:
			for(DequeInit dequeInit : superType.getDequeInits()) {
				if(dequeInit.getMember().isConst())
					continue;
				for(DequeInit tdi : targetType.getDequeInits()) {
					if(dequeInit.getMember() == tdi.getMember())
						continue deque_init_loop;
				}
				initializationOperations += dequeInit.getDequeItems().size();
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

		for(DequeInit dequeInit : targetType.getDequeInits()) {
			if(!dequeInit.getMember().isConst())
				initializationOperations += dequeInit.getDequeItems().size();
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

		// emit all initializations in base classes of members that are used for init'ing other members,
		// i.e. prevent optimization of using only the closest initialization
		// TODO: generalize to all types in between type and target type
		NeededEntities needs = new NeededEntities(false, false, false, false, false, false, false, true);
		for(MemberInit memberInit : type.getMemberInits()) {
			memberInit.getExpression().collectNeededEntities(needs);
		}
		for(MemberInit memberInit : targetType.getMemberInits()) {
			memberInit.getExpression().collectNeededEntities(needs);
		}
		
		// init members of primitive value with explicit initialization
		for(MemberInit memberInit : type.getMemberInits()) {
			Entity member = memberInit.getMember();
			if(memberInit.getMember().isConst())
				continue;
			if(!needs.members.contains(member) 
					&& !generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
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
		
		// init members of deque value with explicit initialization
		for(DequeInit dequeInit : type.getDequeInits()) {
			Entity member = dequeInit.getMember();
			if(dequeInit.getMember().isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrName = formatIdentifiable(dequeInit.getMember());
			for(DequeItem item : dequeInit.getDequeItems()) {
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

		// init const members of deque value with explicit initialization
		for(DequeInit dequeInit : type.getDequeInits()) {
			Entity member = dequeInit.getMember();
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
			for(DequeItem item : dequeInit.getDequeItems()) {
				sb.append("\t\t\t");
				sb.append(attrName + ModelGen.ATTR_IMPL_SUFFIX);
				sb.append(".Enqueue(");
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

			if(memberType instanceof MapType || memberType instanceof SetType
					|| memberType instanceof ArrayType || memberType instanceof DequeType)
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
			for(DequeInit tdi : relevantChildrenOfFocusedType.getDequeInits()) {
				if(member == tdi.getMember())
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

	private void genParameterPassingMethodCall(InheritanceType type, FunctionMethod fm) {
		sb.append("\t\t\t\tcase \"" + fm.getIdent().toString() + "\":\n");
		sb.append("\t\t\t\t\treturn @" + fm.getIdent().toString() + "(actionEnv, graph");
		int i = 0;
		for(Entity inParam : fm.getParameters()) {
			sb.append(", (" + formatType(inParam.getType()) + ")arguments[" + i + "]");
			++i;
		}
		sb.append(");\n");
	}

	private void genParameterPassingMethodCall(InheritanceType type, ProcedureMethod pm) {
		sb.append("\t\t\t\tcase \"" + pm.getIdent().toString() + "\":\n");
		sb.append("\t\t\t\t{\n");
		int i = 0;
		for(Type outType : pm.getReturnTypes()) {
			sb.append("\t\t\t\t\t" + formatType(outType));
			sb.append(" ");
			sb.append("_out_param_" + i + ";\n");
			++i;
		}
		sb.append("\t\t\t\t\t@" + pm.getIdent().toString() + "(actionEnv, graph");
		i = 0;
		for(Entity inParam : pm.getParameters()) {
			sb.append(", (" + formatType(inParam.getType()) + ")arguments[" + i + "]");
			++i;
		}
		for(i=0; i<pm.getReturnTypes().size(); ++i) {
			sb.append(", out ");
			sb.append("_out_param_" + i);
		}
		sb.append(");\n");
		for(i=0; i<pm.getReturnTypes().size(); ++i) {
			sb.append("\t\t\t\t\tReturnArray_" + pm.getIdent().toString() + "_" + type.getIdent().toString() + "[" + i + "] = ");
			sb.append("_out_param_" + i + ";\n");
		}
		sb.append("\t\t\t\t\treturn ReturnArray_" + pm.getIdent().toString() + "_" + type.getIdent().toString() + ";\n");
		sb.append("\t\t\t\t}\n");
	}

	private void genParameterPassingReturnArray(InheritanceType type, ProcedureMethod pm) {
		sb.append("\t\tprivate static object[] ReturnArray_" + pm.getIdent().toString() + "_" + type.getIdent().toString() + " = new object[" + pm.getReturnTypes().size() + "];\n");
	}

	// METHOD-TODO
	private void genMethods(InheritanceType type) {
		sb.append("\n\t\tpublic override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tswitch(name)\n");
		sb.append("\t\t\t{\n");
		for(FunctionMethod fm : type.getAllFunctionMethods()) {
			genParameterPassingMethodCall(type, fm);
		}
		sb.append("\t\t\t\tdefault: throw new NullReferenceException(\"" + formatIdentifiable(type) + " does not have the function method \" + name + \"!\");\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t}\n");

		for(FunctionMethod fm : type.getAllFunctionMethods()) {
			sb.append("\t\tpublic " + formatType(fm.getReturnType()) + " ");
			sb.append(fm.getIdent().toString() + "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_");
			for(Entity inParam : fm.getParameters()) {
				sb.append(", ");
				sb.append(formatType(inParam.getType()));
				sb.append(" ");
				sb.append(formatEntity(inParam));
			}
			sb.append(")\n");
			sb.append("\t\t{\n");
			sb.append("\t\t\tGRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;\n");
			sb.append("\t\t\tGRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;\n");
			ModifyGen.ModifyGenerationState modifyGenState = mgFuncComp.new ModifyGenerationState(model, false, be.system.emitProfilingInstrumentation());
			for(EvalStatement evalStmt : fm.getComputationStatements()) {
				modifyGenState.functionOrProcedureName = fm.getIdent().toString();
				mgFuncComp.genEvalStmt(sb, modifyGenState, evalStmt);
			}
			sb.append("\t\t}\n");

			if(be.unit.isToBeParallelizedActionExisting())
			{
				sb.append("\t\tpublic " + formatType(fm.getReturnType()) + " ");
				sb.append(fm.getIdent().toString() + "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_");
				for(Entity inParam : fm.getParameters()) {
					sb.append(", ");
					sb.append(formatType(inParam.getType()));
					sb.append(" ");
					sb.append(formatEntity(inParam));
				}
				sb.append(", int threadId");
				sb.append(")\n");
				sb.append("\t\t{\n");
				sb.append("\t\t\tGRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;\n");
				sb.append("\t\t\tGRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;\n");
				modifyGenState = mgFuncComp.new ModifyGenerationState(model, true, be.system.emitProfilingInstrumentation());
				for(EvalStatement evalStmt : fm.getComputationStatements()) {
					modifyGenState.functionOrProcedureName = fm.getIdent().toString();
					mgFuncComp.genEvalStmt(sb, modifyGenState, evalStmt);
				}
				sb.append("\t\t}\n");
			}
		}

		//////////////////////////////////////////////////////////////
		
		sb.append("\t\tpublic override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tswitch(name)\n");
		sb.append("\t\t\t{\n");
		for(ProcedureMethod pm : type.getAllProcedureMethods()) {
			genParameterPassingMethodCall(type, pm);
		}
		sb.append("\t\t\t\tdefault: throw new NullReferenceException(\"" + formatIdentifiable(type) + " does not have the procedure method \" + name + \"!\");\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t}\n");
		for(ProcedureMethod pm : type.getAllProcedureMethods()) {
			genParameterPassingReturnArray(type, pm);
		}

		for(ProcedureMethod pm : type.getAllProcedureMethods()) {
			sb.append("\t\tpublic void ");
			sb.append(pm.getIdent().toString() + "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_");
			for(Entity inParam : pm.getParameters()) {
				sb.append(", ");
				sb.append(formatType(inParam.getType()));
				sb.append(" ");
				sb.append(formatEntity(inParam));
			}
			int i = 0;
			for(Type outType : pm.getReturnTypes()) {
				sb.append(", out ");
				sb.append(formatType(outType));
				sb.append(" ");
				sb.append("_out_param_" + i);
				++i;
			}
			sb.append(")\n");
			sb.append("\t\t{\n");
			sb.append("\t\t\tGRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;\n");
			sb.append("\t\t\tGRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;\n");
			ModifyGen.ModifyGenerationState modifyGenState = mgFuncComp.new ModifyGenerationState(model, false, be.system.emitProfilingInstrumentation());
			mgFuncComp.initEvalGen();
			for(EvalStatement evalStmt : pm.getComputationStatements()) {
				modifyGenState.functionOrProcedureName = pm.getIdent().toString();
				mgFuncComp.genEvalStmt(sb, modifyGenState, evalStmt);
			}
			sb.append("\t\t}\n");
		}
	}

	////////////////////////////////////
	// Type implementation generation //
	////////////////////////////////////

	/**
	 * Generates the type implementation
	 */
	private void genTypeImplementation(Collection<? extends InheritanceType> allTypes, InheritanceType type, String packageName) {
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
		genIsA(allTypes, type);
		genIsMyType(allTypes, type);
		genAttributeAttributes(type);

		sb.append("\t\tpublic " + typename + "() : base((int) " + formatNodeOrEdge(type) + "Types.@" + typeident + ")\n");
		sb.append("\t\t{\n");
		genAttributeInit(type);
		addAnnotations(sb, type, "annotations");
		sb.append("\t\t}\n");

		sb.append("\t\tpublic override string Name { get { return \"" + typeident + "\"; } }\n");
		sb.append("\t\tpublic override string Package { get { return " + (!getPackagePrefix(type).equals("") ? "\""+getPackagePrefix(type)+"\"" : "null") + "; } }\n");
		sb.append("\t\tpublic override string PackagePrefixedName { get { return \"" + getPackagePrefixDoubleColon(type) + typeident + "\"; } }\n");
		if(type.getIdent().toString()=="Node") {
				sb.append("\t\tpublic override string "+formatNodeOrEdge(type)+"InterfaceName { get { return "
						+ "\"de.unika.ipd.grGen.libGr.INode\"; } }\n");
		} else if(type.getIdent().toString()=="AEdge" || type.getIdent().toString()=="Edge" || type.getIdent().toString()=="UEdge") {
				sb.append("\t\tpublic override string "+formatNodeOrEdge(type)+"InterfaceName { get { return "
						+ "\"de.unika.ipd.grGen.libGr.IEdge\"; } }\n");
		} else {
			sb.append("\t\tpublic override string "+formatNodeOrEdge(type)+"InterfaceName { get { return "
				+ "\"de.unika.ipd.grGen.Model_" + model.getIdent() + "."
				+ "I" + getNodeOrEdgeTypePrefix(type) + getPackagePrefixDot(type) + formatIdentifiable(type) + "\"; } }\n");
		}
		if(type.isAbstract()) {
			sb.append("\t\tpublic override string "+formatNodeOrEdge(type)+"ClassName { get { return null; } }\n");
		} else {
			sb.append("\t\tpublic override string "+formatNodeOrEdge(type)+"ClassName { get { return \"de.unika.ipd.grGen.Model_"
					+ model.getIdent() + "." + getPackagePrefixDot(type) + formatElementClassName(type) + "\"; } }\n");
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
			sb.append("\t\t}\n\n");
			sb.append("\t\tpublic override void SetSourceAndTarget("
					+ "GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)\n"
				+ "\t\t{\n");
			if(type.isAbstract())
				sb.append("\t\t\tthrow new Exception(\"The abstract edge type "
						+ typeident + " does not support source and target setting!\");\n");
			else
				sb.append("\t\t\t((GRGEN_LGSP.LGSPEdge)edge).SetSourceAndTarget" 
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

		sb.append("\t\tpublic override int NumFunctionMethods { get { return " + type.getAllFunctionMethods().size() + "; } }\n");
		genFunctionMethodsEnumerator(type);
		genGetFunctionMethod(type);

		sb.append("\t\tpublic override int NumProcedureMethods { get { return " + type.getAllProcedureMethods().size() + "; } }\n");
		genProcedureMethodsEnumerator(type);
		genGetProcedureMethod(type);

		sb.append("\t\tpublic override bool IsA(GRGEN_LIBGR.GrGenType other)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\treturn (this == other) || isA[other.TypeID];\n");
		sb.append("\t\t}\n");

		genCreateWithCopyCommons(type);
		sb.append("\t}\n");
		
		// generate function method info classes
		Collection<FunctionMethod> allFunctionMethods = type.getAllFunctionMethods();
		for(FunctionMethod fm : allFunctionMethods) {
			genFunctionMethodInfo(fm, type, packageName);
		}
		
		// generate procedure method info classes
		Collection<ProcedureMethod> allProcedureMethods = type.getAllProcedureMethods();
		for(ProcedureMethod pm : allProcedureMethods) {
			genProcedureMethodInfo(pm, type, packageName);
		}
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
		sb.append("\t\tpublic override bool IsA(int typeID) { return isA[typeID]; }\n");
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
		sb.append("\t\tpublic override bool IsMyType(int typeID) { return isMyType[typeID]; }\n");
	}

	private void genAttributeAttributes(InheritanceType type) {
		for(Entity member : type.getMembers()) { // only for locally defined members
			sb.append("\t\tpublic static GRGEN_LIBGR.AttributeType " + formatAttributeTypeName(member) + ";\n");

			// attribute types T/S of map<T,S>/set<T>/array<T>/deque<T>
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
			if(member.getType() instanceof DequeType) {
				sb.append("\t\tpublic static GRGEN_LIBGR.AttributeType " + formatAttributeTypeName(member)+"_deque_member_type;\n");
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
			else if (t instanceof SetType) {
				SetType st = (SetType)t;

				// attribute type T of set<T>
				sb.append("\t\t\t" + attributeTypeName + "_set_member_type = new GRGEN_LIBGR.AttributeType(");
				sb.append("\"" + formatIdentifiable(e) + "_set_member_type\", this, ");
				genAttributeInitTypeDependentStuff(st.getValueType(), e);
				sb.append(");\n");
			}
			else if (t instanceof ArrayType) {
				ArrayType at = (ArrayType)t;

				// attribute type T of set<T>
				sb.append("\t\t\t" + attributeTypeName + "_array_member_type = new GRGEN_LIBGR.AttributeType(");
				sb.append("\"" + formatIdentifiable(e) + "_array_member_type\", this, ");
				genAttributeInitTypeDependentStuff(at.getValueType(), e);
				sb.append(");\n");
			}
			else if (t instanceof DequeType) {
				DequeType qt = (DequeType)t;

				// attribute type T of deque<T>
				sb.append("\t\t\t" + attributeTypeName + "_deque_member_type = new GRGEN_LIBGR.AttributeType(");
				sb.append("\"" + formatIdentifiable(e) + "_deque_member_type\", this, ");
				genAttributeInitTypeDependentStuff(qt.getValueType(), e);
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
			sb.append(getAttributeKind(t) + ", GRGEN_MODEL." + getPackagePrefixDot(t) + "Enums.@" + formatIdentifiable(t) + ", "
					+ "null, null, "
					+ "null, null, null, typeof(" + formatAttributeType(t) + ")");
		} else if (t instanceof MapType) {
			sb.append(getAttributeKind(t) + ", null, "
					+ formatAttributeTypeName(e) + "_map_range_type" + ", "
					+ formatAttributeTypeName(e) + "_map_domain_type" + ", "
					+ "null, null, null, typeof(" + formatAttributeType(t) + ")");
		} else if (t instanceof SetType) {
			sb.append(getAttributeKind(t) + ", null, "
					+ formatAttributeTypeName(e) + "_set_member_type" + ", null, "
					+ "null, null, null, typeof(" + formatAttributeType(t) + ")");
		} else if (t instanceof ArrayType) {
			sb.append(getAttributeKind(t) + ", null, "
					+ formatAttributeTypeName(e) + "_array_member_type" + ", null, "
					+ "null, null, null, typeof(" + formatAttributeType(t) + ")");
		} else if (t instanceof DequeType) {
			sb.append(getAttributeKind(t) + ", null, "
					+ formatAttributeTypeName(e) + "_deque_member_type" + ", null, "
					+ "null, null, null, typeof(" + formatAttributeType(t) + ")");
		} else if (t instanceof NodeType || t instanceof EdgeType) {
			sb.append(getAttributeKind(t) + ", null, "
					+ "null, null, "
					+ "\"" + formatIdentifiable(t) + "\","
					+ (((ContainedInPackage)t).getPackageContainedIn()!="" ? "\""+((ContainedInPackage)t).getPackageContainedIn()+"\"" : "null") + ","
					+ "\"" + getPackagePrefixDoubleColon(t) + formatIdentifiable(t) + "\","
					+ "typeof(" + formatElementInterfaceRef(t) + ")");
		} else {
			sb.append(getAttributeKind(t) + ", null, "
					+ "null, null, "
					+ "null, null, null, typeof(" + formatAttributeType(t) + ")");
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
		else if (t instanceof DequeType)
			return "GRGEN_LIBGR.AttributeKind.DequeAttr";
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

	private void genFunctionMethodsEnumerator(InheritanceType type) {
		Collection<FunctionMethod> allFunctionMethods = type.getAllFunctionMethods();
		sb.append("\t\tpublic override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods");

		if(allFunctionMethods.isEmpty())
			sb.append(" { get { yield break; } }\n");
		else {
			sb.append("\n\t\t{\n");
			sb.append("\t\t\tget\n");
			sb.append("\t\t\t{\n");
			for(FunctionMethod fm : allFunctionMethods) {
				sb.append("\t\t\t\tyield return " + formatFunctionMethodInfoName(fm, type) + ".Instance;\n");
			}
			sb.append("\t\t\t}\n");
			sb.append("\t\t}\n");
		}
	}

	private void genGetFunctionMethod(InheritanceType type) {
		Collection<FunctionMethod> allFunctionMethods = type.getAllFunctionMethods();
		sb.append("\t\tpublic override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name)");

		if(allFunctionMethods.isEmpty())
			sb.append(" { return null; }\n");
		else {
			sb.append("\n\t\t{\n");
			sb.append("\t\t\tswitch(name)\n");
			sb.append("\t\t\t{\n");
			for(FunctionMethod fm : allFunctionMethods) {
				sb.append("\t\t\t\tcase \"" + formatIdentifiable(fm) + "\" : return " +
						formatFunctionMethodInfoName(fm, type) + ".Instance;\n");
			}
			sb.append("\t\t\t}\n");
			sb.append("\t\t\treturn null;\n");
			sb.append("\t\t}\n");
		}
	}

	private void genProcedureMethodsEnumerator(InheritanceType type) {
		Collection<ProcedureMethod> allProcedureMethods = type.getAllProcedureMethods();
		sb.append("\t\tpublic override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods");

		if(allProcedureMethods.isEmpty())
			sb.append(" { get { yield break; } }\n");
		else {
			sb.append("\n\t\t{\n");
			sb.append("\t\t\tget\n");
			sb.append("\t\t\t{\n");
			for(ProcedureMethod pm : allProcedureMethods) {
				sb.append("\t\t\t\tyield return " + formatProcedureMethodInfoName(pm, type) + ".Instance;\n");
			}
			sb.append("\t\t\t}\n");
			sb.append("\t\t}\n");
		}
	}

	private void genGetProcedureMethod(InheritanceType type) {
		Collection<ProcedureMethod> allProcedureMethods = type.getAllProcedureMethods();
		sb.append("\t\tpublic override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name)");

		if(allProcedureMethods.isEmpty())
			sb.append(" { return null; }\n");
		else {
			sb.append("\n\t\t{\n");
			sb.append("\t\t\tswitch(name)\n");
			sb.append("\t\t\t{\n");
			for(ProcedureMethod pm : allProcedureMethods) {
				sb.append("\t\t\t\tcase \"" + formatIdentifiable(pm) + "\" : return " +
						formatProcedureMethodInfoName(pm, type) + ".Instance;\n");
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
					sb.append("\t\t\t\tcase (int) GRGEN_MODEL." + getPackagePrefixDot(itype) + kindName + "Types.@"
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
								if(member.getType() instanceof MapType || member.getType() instanceof SetType 
										|| member.getType() instanceof ArrayType || member.getType() instanceof DequeType) {
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

	/**
	 * Generates the function info for the given function method
	 */
	private void genFunctionMethodInfo(FunctionMethod fm, InheritanceType type, String packageName) {
		String functionMethodName = formatIdentifiable(fm);
		String className = formatFunctionMethodInfoName(fm, type);

		sb.append("\tpublic class " + className + " : GRGEN_LIBGR.FunctionInfo\n");
		sb.append("\t{\n");
		sb.append("\t\tprivate static " + className + " instance = null;\n");
		sb.append("\t\tpublic static " + className + " Instance { get { if (instance==null) { "
				+ "instance = new " + className + "(); } return instance; } }\n");
		sb.append("\n");

		sb.append("\t\tprivate " + className + "()\n");
		sb.append("\t\t\t\t\t: base(\n");
		sb.append("\t\t\t\t\t\t\"" + functionMethodName + "\",\n");
		sb.append("\t\t\t\t\t\t" + (packageName!=null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\"" + (packageName!=null ? packageName + "::" + functionMethodName : functionMethodName) + "\",\n");
		sb.append("\t\t\t\t\t\tnew String[] { ");
		for(Entity inParam : fm.getParameters()) {
			sb.append("\"" + inParam.getIdent() + "\", ");
		}
		sb.append(" },\n");
		sb.append("\t\t\t\t\t\tnew GRGEN_LIBGR.GrGenType[] { ");
		for(Entity inParam : fm.getParameters()) {
			if(inParam.getType() instanceof InheritanceType) {
				sb.append(formatTypeClassRef(inParam.getType()) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(inParam.getType()) + ")), ");
			}
		}
		sb.append(" },\n");
		Type outType = fm.getReturnType();
		if(outType instanceof InheritanceType) {
			sb.append("\t\t\t\t\t\t" + formatTypeClassRef(outType) + ".typeVar\n");
		} else {
			sb.append("\t\t\t\t\t\tGRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(outType) + "))\n");
		}
		sb.append("\t\t\t\t\t  )\n");
		sb.append("\t\t{\n");
		sb.append("\t\t}\n");
		
		sb.append("\t\tpublic override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tthrow new Exception(\"Not implemented, can't call function method without this object!\");\n");
		sb.append("\t\t}\n");

		sb.append("\t}\n");
		sb.append("\n");
	}

	/**
	 * Generates the procedure info for the given procedure method
	 */
	private void genProcedureMethodInfo(ProcedureMethod pm, InheritanceType type, String packageName) {
		String procedureMethodName = formatIdentifiable(pm);
		String className = formatProcedureMethodInfoName(pm, type);

		sb.append("\tpublic class " + className + " : GRGEN_LIBGR.ProcedureInfo\n");
		sb.append("\t{\n");
		sb.append("\t\tprivate static " + className + " instance = null;\n");
		sb.append("\t\tpublic static " + className + " Instance { get { if (instance==null) { "
				+ "instance = new " + className + "(); } return instance; } }\n");
		sb.append("\n");

		sb.append("\t\tprivate " + className + "()\n");
		sb.append("\t\t\t\t\t: base(\n");
		sb.append("\t\t\t\t\t\t\"" + procedureMethodName + "\",\n");
		sb.append("\t\t\t\t\t\t" + (packageName!=null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\"" + (packageName!=null ? packageName + "::" + procedureMethodName : procedureMethodName) + "\",\n");
		sb.append("\t\t\t\t\t\tnew String[] { ");
		for(Entity inParam : pm.getParameters()) {
			sb.append("\"" + inParam.getIdent() + "\", ");
		}
		sb.append(" },\n");
		sb.append("\t\t\t\t\t\tnew GRGEN_LIBGR.GrGenType[] { ");
		for(Entity inParam : pm.getParameters()) {
			if(inParam.getType() instanceof InheritanceType) {
				sb.append(formatTypeClassRef(inParam.getType()) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(inParam.getType()) + ")), ");
			}
		}
		sb.append(" },\n");
		sb.append("\t\t\t\t\t\tnew GRGEN_LIBGR.GrGenType[] { ");
		for(Type outType : pm.getReturnTypes()) {
			if(outType instanceof InheritanceType) {
				sb.append(formatTypeClassRef(outType) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(outType) + ")), ");
			}
		}
		sb.append(" }\n");
		sb.append("\t\t\t\t\t  )\n");
		sb.append("\t\t{\n");
		sb.append("\t\t}\n");
		
		sb.append("\t\tpublic override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.ProcedureInvocationParameterBindings paramBindings)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tthrow new Exception(\"Not implemented, can't call procedure method without this object!\");\n");
		sb.append("\t\t}\n");

		sb.append("\t}\n");
		sb.append("\n");
	}

	////////////////////////////
	// Index generation //
	////////////////////////////

	void genIndexTypes() {
		for(Index index : model.getIndices()) {
			genIndexType(index);
		}
	}

	void genIndexType(Index index) {
		String indexName = index.getIdent().toString();
		String lookupType = index instanceof AttributeIndex ? formatAttributeType(((AttributeIndex)index).entity) : "int";
		String graphElementType = index instanceof AttributeIndex ? formatElementInterfaceRef(((AttributeIndex)index).type) : formatElementInterfaceRef(((IncidenceIndex)index).getStartNodeType());
		if(index instanceof AttributeIndex) {
			sb.append("\tinterface Index" + indexName + " : GRGEN_LIBGR.IAttributeIndex\n");
		} else if(index instanceof IncidenceIndex) {
			sb.append("\tinterface Index" + indexName + " : GRGEN_LIBGR.IIncidenceIndex\n");
		}
		sb.append("\t{\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> Lookup(" + lookupType + " fromto);\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupAscending();\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupAscendingFromInclusive(" + lookupType + " from);\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupAscendingFromExclusive(" + lookupType + " from);\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupAscendingToInclusive(" + lookupType + " to);\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupAscendingToExclusive(" + lookupType + " to);\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupAscendingFromInclusiveToInclusive(" + lookupType + " from, " + lookupType + " to);\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupAscendingFromInclusiveToExclusive(" + lookupType + " from, " + lookupType + " to);\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupAscendingFromExclusiveToInclusive(" + lookupType + " from, " + lookupType + " to);\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupAscendingFromExclusiveToExclusive(" + lookupType + " from, " + lookupType + " to);\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupDescending();\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupDescendingFromInclusive(" + lookupType + " from);\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupDescendingFromExclusive(" + lookupType + " from);\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupDescendingToInclusive(" + lookupType + " to);\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupDescendingToExclusive(" + lookupType + " to);\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupDescendingFromInclusiveToInclusive(" + lookupType + " from, " + lookupType + " to);\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupDescendingFromInclusiveToExclusive(" + lookupType + " from, " + lookupType + " to);\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupDescendingFromExclusiveToInclusive(" + lookupType + " from, " + lookupType + " to);\n");
		sb.append("\t\tIEnumerable<" + graphElementType + "> LookupDescendingFromExclusiveToExclusive(" + lookupType + " from, " + lookupType + " to);\n");
		if(index instanceof IncidenceIndex) {
			sb.append("\t\tint GetIncidenceCount(" + graphElementType + " element);\n");
		}
		sb.append("\t}\n");
		sb.append("\n");
	}

	void genIndexImplementations() {
		int i=0;
		for(Index index : model.getIndices()) {
			if(index instanceof AttributeIndex) {
				genIndexImplementation((AttributeIndex)index, i);
			} else {
				genIndexImplementation((IncidenceIndex)index, i);
			}
			++i;
		}
	}

	void genIndexImplementation(AttributeIndex index, int indexNum) {
		String indexName = index.getIdent().toString();
		String graphElementType = formatElementInterfaceRef(index.type);
		String modelName = model.getIdent().toString() + "GraphModel";
		sb.append("\tpublic class Index" + indexName + "Impl : Index" + indexName + "\n");
		sb.append("\t{\n");
		
		sb.append("\t\tpublic GRGEN_LIBGR.IndexDescription Description { get { return " + modelName + ".GetIndexDescription(" + indexNum + "); } }\n");
		sb.append("\n");

		sb.append("\t\tprotected class TreeNode\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\t// search tree structure\n");
		sb.append("\t\t\tpublic TreeNode left;\n");
		sb.append("\t\t\tpublic TreeNode right;\n");
		sb.append("\t\t\tpublic int level;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\t// user data\n");
		sb.append("\t\t\tpublic " + graphElementType + " value;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\t// for the bottom node, operating as sentinel\n");
		sb.append("\t\t\tpublic TreeNode()\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tleft = this;\n");
		sb.append("\t\t\t\tright = this;\n");
		sb.append("\t\t\t\tlevel = 0;\n");
	    sb.append("\t\t\t}\n");
	    sb.append("\t\t\t\n");
	    sb.append("\t\t\t// for regular nodes (that are born as leaf nodes)\n");
	    sb.append("\t\t\tpublic TreeNode(" + graphElementType + " value, TreeNode bottom)\n");
	    sb.append("\t\t\t{\n");
	    sb.append("\t\t\t\tleft = bottom;\n");
	    sb.append("\t\t\t\tright = bottom;\n");
	    sb.append("\t\t\t\tlevel = 1;\n");
	    sb.append("\t\t\t\t\n");
	    sb.append("\t\t\t\tthis.value = value;\n");
	    sb.append("\t\t\t}\n");
	    sb.append("\t\t\t\n");
	    sb.append("\t\t\t// for copy constructing from other index\n");
	    sb.append("\t\t\tpublic TreeNode(TreeNode left, TreeNode right, int level, " + graphElementType + " value)\n");
	    sb.append("\t\t\t{\n");
	    sb.append("\t\t\t\tthis.left = left;\n");
	    sb.append("\t\t\t\tthis.right = right;\n");
	    sb.append("\t\t\t\tthis.level = level;\n");
	    sb.append("\t\t\t\t\n");
	    sb.append("\t\t\t\tthis.value = value;\n");
	    sb.append("\t\t\t}\n");
	    sb.append("\t\t}\n");
	    sb.append("\n");

		sb.append("\t\tprotected TreeNode root;\n");
		sb.append("\t\tprotected TreeNode bottom;\n");
		sb.append("\t\tprotected TreeNode deleted;\n");
		sb.append("\t\tprotected TreeNode last;\n");
		sb.append("\t\tprotected int count;\n");
		sb.append("\t\tprotected int version;\n");
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

		sb.append("\t\tpublic Index" + indexName + "Impl(GRGEN_LGSP.LGSPGraph graph)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tthis.graph = graph;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\t// initialize AA tree used to implement the index\n");
		sb.append("\t\t\tbottom = new TreeNode();\n");
		sb.append("\t\t\troot = bottom;\n");
		sb.append("\t\t\tdeleted = bottom;\n");
		sb.append("\t\t\tcount = 0;\n");
		sb.append("\t\t\tversion = 0;\n");
		sb.append("\t\t\t\n");
		if(index.type instanceof NodeType) {
			sb.append("\t\t\tgraph.OnNodeAdded += Added;\n");
			sb.append("\t\t\tgraph.OnRemovingNode += Removing;\n");
			sb.append("\t\t\tgraph.OnChangingNodeAttribute += ChangingAttribute;\n");
			sb.append("\t\t\tgraph.OnRetypingNode += Retyping;\n");
		} else {
			sb.append("\t\t\tgraph.OnEdgeAdded += Added;\n");
			sb.append("\t\t\tgraph.OnRemovingEdge += Removing;\n");
			sb.append("\t\t\tgraph.OnChangingEdgeAttribute += ChangingAttribute;\n");
			sb.append("\t\t\tgraph.OnRetypingEdge += Retyping;\n");
		}
		sb.append("\t\t}\n");
		sb.append("\n");
		
		sb.append("\t\tpublic void FillAsClone(Index" + indexName + "Impl that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\troot = FillAsClone(that.root, that.bottom, oldToNewMap);\n");
		sb.append("\t\t\tcount = that.count;\n");
		sb.append("\t\t}\n");
		sb.append("\n");
		
		sb.append("\t\tprotected TreeNode FillAsClone(TreeNode that, TreeNode otherBottom, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(that == otherBottom)\n");
		sb.append("\t\t\t\treturn bottom;\n");
		sb.append("\t\t\telse\n");
		sb.append("\t\t\t\treturn new TreeNode(\n");
		sb.append("\t\t\t\t\tFillAsClone(that.left, otherBottom, oldToNewMap),\n");
		sb.append("\t\t\t\t\tFillAsClone(that.right, otherBottom, oldToNewMap),\n");
		sb.append("\t\t\t\t\tthat.level,\n");
		sb.append("\t\t\t\t\t(" + graphElementType + ")oldToNewMap[that.value]\n");
        sb.append("\t\t\t\t);\n");
        sb.append("\t\t}\n");
		sb.append("\n");

		genIndexMaintainingEventHandlers(index);

		genIndexAATreeBalancingInsertionDeletion(index);

		sb.append("\t\tprivate GRGEN_LGSP.LGSPGraph graph;\n");

		sb.append("\t}\n");
		sb.append("\n");
	}

	void genEqualElementEntry(Index index)
	{
		String attributeType = index instanceof AttributeIndex ? formatAttributeType(((AttributeIndex)index).entity) : "int";
		
		sb.append("\t\tpublic IEnumerable<GRGEN_LIBGR.IGraphElement> LookupElements(object fromto)\n");
		sb.append("\t\t{\n");

		sb.append("\t\t\tint versionAtIterationBegin = version;\n");
		sb.append("\t\t\tforeach(GRGEN_LIBGR.IGraphElement value in Lookup(root, (" + attributeType + ")fromto))\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t}\n");

		sb.append("\t\t}\n");
		sb.append("\t\t\n");
	}

	void genEqualEntry(Index index)
	{
		String attributeType = index instanceof AttributeIndex ? formatAttributeType(((AttributeIndex)index).entity) : "int";
		String graphElementType = index instanceof AttributeIndex ? formatElementInterfaceRef(((AttributeIndex)index).type) : formatElementInterfaceRef(((IncidenceIndex)index).getStartNodeType());
		
		sb.append("\t\tpublic IEnumerable<" + graphElementType + "> Lookup(" + attributeType + " fromto)\n");
		sb.append("\t\t{\n");

		sb.append("\t\t\tint versionAtIterationBegin = version;\n");
		sb.append("\t\t\tforeach(" + graphElementType + " value in Lookup(root, fromto))\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t}\n");

		sb.append("\t\t}\n");
		sb.append("\t\t\n");
	}

	void genEqual(AttributeIndex index)
	{
		String attributeType = formatAttributeType(index.entity);
		String attributeName = index.entity.getIdent().toString();
		String graphElementType = formatElementInterfaceRef(index.type);
		
		sb.append("\t\tprivate IEnumerable<" + graphElementType + "> Lookup(TreeNode current, " + attributeType + " fromto)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(current == bottom)\n");
		sb.append("\t\t\t\tyield break;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\tint versionAtIterationBegin = version;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\t// don't go left if the value is already lower than fromto\n");
		if(index.entity.getType() instanceof BooleanType)
			sb.append("\t\t\tif(current.value." + attributeName + ".CompareTo(fromto)>=0)\n");
		else if(index.entity.getType() instanceof StringType)
			sb.append("\t\t\tif(String.Compare(current.value." + attributeName + ", fromto, StringComparison.InvariantCulture)>=0)\n");
		else
			sb.append("\t\t\tif(current.value." + attributeName + " >= fromto)\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tforeach(" + graphElementType + " value in Lookup(current.left, fromto))\n");
		sb.append("\t\t\t\t{\n");
		sb.append("\t\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t\t}\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t\n");
		
		sb.append("\t\t\t// (only) yield a value that is equal to fromto\n");
		sb.append("\t\t\tif(current.value." + attributeName + " == fromto)\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\t// the value is within range.\n");
		sb.append("\t\t\t\tyield return current.value;\n");
		sb.append("\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t\n");
		
		sb.append("\t\t\t// don't go right if the value is already higher than fromto\n");
		if(index.entity.getType() instanceof BooleanType)
			sb.append("\t\t\tif(current.value." + attributeName + ".CompareTo(fromto)<=0)\n");
		else if(index.entity.getType() instanceof StringType)
			sb.append("\t\t\tif(String.Compare(current.value." + attributeName + ", fromto, StringComparison.InvariantCulture)<=0)\n");
		else
			sb.append("\t\t\tif(current.value." + attributeName + " <= fromto)\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tforeach(" + graphElementType + " value in Lookup(current.right, fromto))\n");
		sb.append("\t\t\t\t{\n");
		sb.append("\t\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t\t}\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t}\n");
		sb.append("\t\t\n");
	}

	void genAscendingElementEntry(Index index, boolean fromConstrained, boolean fromInclusive, boolean toConstrained, boolean toInclusive)
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
		
		sb.append("\t\tpublic IEnumerable<GRGEN_LIBGR.IGraphElement> LookupElements" + lookupMethodNameAppendix + "(");
		if(fromConstrained)
			sb.append("object from");
		if(fromConstrained && toConstrained)
			sb.append(", ");
		if(toConstrained)
			sb.append("object to");
		sb.append(")\n");
		sb.append("\t\t{\n");

		sb.append("\t\t\tint versionAtIterationBegin = version;\n");
		sb.append("\t\t\tforeach(GRGEN_LIBGR.IGraphElement value in Lookup" + lookupMethodNameAppendix + "(root");
		if(fromConstrained)
			sb.append(", (" + attributeType + ")from");
		if(toConstrained)
			sb.append(", (" + attributeType + ")to");
		sb.append("))\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t}\n");

		sb.append("\t\t}\n");
		sb.append("\t\t\n");
	}

	void genAscendingEntry(Index index, boolean fromConstrained, boolean fromInclusive, boolean toConstrained, boolean toInclusive)
	{
		String attributeType = index instanceof AttributeIndex ? formatAttributeType(((AttributeIndex)index).entity) : "int";
		String graphElementType = index instanceof AttributeIndex ? formatElementInterfaceRef(((AttributeIndex)index).type) : formatElementInterfaceRef(((IncidenceIndex)index).getStartNodeType());

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
		
		sb.append("\t\tpublic IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix + "(");
		if(fromConstrained)
			sb.append(attributeType + " from");
		if(fromConstrained && toConstrained)
			sb.append(", ");
		if(toConstrained)
			sb.append(attributeType + " to");
		sb.append(")\n");
		sb.append("\t\t{\n");

		sb.append("\t\t\tint versionAtIterationBegin = version;\n");
		sb.append("\t\t\tforeach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(root");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t}\n");

		sb.append("\t\t}\n");
		sb.append("\t\t\n");
	}

	void genAscending(AttributeIndex index, boolean fromConstrained, boolean fromInclusive, boolean toConstrained, boolean toInclusive)
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
		
		sb.append("\t\tprivate IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix + "(TreeNode current");
		if(fromConstrained)
			sb.append(", " + attributeType + " from");
		if(toConstrained)
			sb.append(", " + attributeType + " to");
		sb.append(")\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(current == bottom)\n");
		sb.append("\t\t\t\tyield break;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\tint versionAtIterationBegin = version;\n");
		sb.append("\t\t\t\n");
		if(fromConstrained) {
			sb.append("\t\t\t// don't go left if the value is already lower than from\n");
			if(index.entity.getType() instanceof BooleanType)
				sb.append("\t\t\tif(current.value." + attributeName + ".CompareTo(from)" + (fromInclusive ? " >= " : " > ") + "0)\n");
			else if(index.entity.getType() instanceof StringType)
				sb.append("\t\t\tif(String.Compare(current.value." + attributeName + ", from, StringComparison.InvariantCulture)" + (fromInclusive ? " >= " : " > ") + "0)\n");
			else
				sb.append("\t\t\tif(current.value." + attributeName + (fromInclusive ? " >= " : " > ") + "from)\n");
		}
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tforeach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.left");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.append("\t\t\t\t{\n");
		sb.append("\t\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t\t}\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t\n");
		
		if(fromConstrained || toConstrained) {
			sb.append("\t\t\t// (only) yield a value that is within bounds\n");
			sb.append("\t\t\tif(");
			if(fromConstrained) {
				if(index.entity.getType() instanceof BooleanType)
					sb.append("current.value." + attributeName + ".CompareTo(from)" + (fromInclusive ? " >= " : " > ") + "0");
				else if(index.entity.getType() instanceof StringType)
					sb.append("String.Compare(current.value." + attributeName + ", from, StringComparison.InvariantCulture)" + (fromInclusive ? " >= " : " > ") + "0");
				else
					sb.append("current.value." + attributeName + (fromInclusive ? " >= " : " > ") + "from");
			}
			if(fromConstrained && toConstrained)
				sb.append(" && ");
			if(toConstrained) {
				if(index.entity.getType() instanceof BooleanType)
					sb.append("current.value." + attributeName + ".CompareTo(to)" + (toInclusive ? " <= " : " < ") + "0");
				else if(index.entity.getType() instanceof StringType)
					sb.append("String.Compare(current.value." + attributeName + ", to, StringComparison.InvariantCulture)" + (toInclusive ? " <= " : " < ") + "0");
				else
					sb.append("current.value." + attributeName + (toInclusive ? " <= " : " < ") + "to");
			}
			sb.append(")\n");
		}
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\t// the value is within range.\n");
		sb.append("\t\t\t\tyield return current.value;\n");
		sb.append("\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t\n");
		
		if(toConstrained) {
			sb.append("\t\t\t// don't go right if the value is already higher than to\n");
			if(index.entity.getType() instanceof BooleanType)
				sb.append("\t\t\tif(current.value." + attributeName + ".CompareTo(to)" + (toInclusive ? " <= " : " < ") + "0)\n");
			else if(index.entity.getType() instanceof StringType)
				sb.append("\t\t\tif(String.Compare(current.value." + attributeName + ", to, StringComparison.InvariantCulture)" + (toInclusive ? " <= " : " < ") + "0)\n");
			else
				sb.append("\t\t\tif(current.value." + attributeName + (toInclusive ? " <= " : " < ") + "to)\n");
		}
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tforeach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.right");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.append("\t\t\t\t{\n");
		sb.append("\t\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t\t}\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t}\n");
		sb.append("\t\t\n");
	}

	void genDescendingElementEntry(Index index, boolean fromConstrained, boolean fromInclusive, boolean toConstrained, boolean toInclusive)
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
		
		sb.append("\t\tpublic IEnumerable<GRGEN_LIBGR.IGraphElement> LookupElements" + lookupMethodNameAppendix + "(");
		if(fromConstrained)
			sb.append("object from");
		if(fromConstrained && toConstrained)
			sb.append(", ");
		if(toConstrained)
			sb.append("object to");
		sb.append(")\n");
		sb.append("\t\t{\n");

		sb.append("\t\t\tint versionAtIterationBegin = version;\n");
		sb.append("\t\t\tforeach(GRGEN_LIBGR.IGraphElement value in Lookup" + lookupMethodNameAppendix + "(root");
		if(fromConstrained)
			sb.append(", (" + attributeType + ")from");
		if(toConstrained)
			sb.append(", (" + attributeType + ")to");
		sb.append("))\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t}\n");

		sb.append("\t\t}\n");
		sb.append("\t\t\n");
	}

	void genDescendingEntry(Index index, boolean fromConstrained, boolean fromInclusive, boolean toConstrained, boolean toInclusive)
	{
		String attributeType = index instanceof AttributeIndex ? formatAttributeType(((AttributeIndex)index).entity) : "int";
		String graphElementType = index instanceof AttributeIndex ? formatElementInterfaceRef(((AttributeIndex)index).type) : formatElementInterfaceRef(((IncidenceIndex)index).getStartNodeType());

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
		
		sb.append("\t\tpublic IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix + "(");
		if(fromConstrained)
			sb.append(attributeType + " from");
		if(fromConstrained && toConstrained)
			sb.append(", ");
		if(toConstrained)
			sb.append(attributeType + " to");
		sb.append(")\n");
		sb.append("\t\t{\n");

		sb.append("\t\t\tint versionAtIterationBegin = version;\n");
		sb.append("\t\t\tforeach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(root");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t}\n");

		sb.append("\t\t}\n");
		sb.append("\t\t\n");
	}

	void genDescending(AttributeIndex index, boolean fromConstrained, boolean fromInclusive, boolean toConstrained, boolean toInclusive)
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
		
		sb.append("\t\tprivate IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix + "(TreeNode current");
		if(fromConstrained)
			sb.append(", " + attributeType + " from");
		if(toConstrained)
			sb.append(", " + attributeType + " to");
		sb.append(")\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(current == bottom)\n");
		sb.append("\t\t\t\tyield break;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\tint versionAtIterationBegin = version;\n");
		sb.append("\t\t\t\n");
		if(fromConstrained) {
			sb.append("\t\t\t// don't go left if the value is already lower than from\n");
			if(index.entity.getType() instanceof BooleanType)
				sb.append("\t\t\tif(current.value." + attributeName + ".CompareTo(from)" + (fromInclusive ? " <= " : " < ") + "0)\n");
			else if(index.entity.getType() instanceof StringType)
				sb.append("\t\t\tif(String.Compare(current.value." + attributeName + ", from, StringComparison.InvariantCulture)" + (fromInclusive ? " <= " : " < ") + "0)\n");
			else
				sb.append("\t\t\tif(current.value." + attributeName + (fromInclusive ? " <= " : " < ") + "from)\n");
		}
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tforeach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.right");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.append("\t\t\t\t{\n");
		sb.append("\t\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t\t}\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t\n");
		
		if(fromConstrained || toConstrained) {
			sb.append("\t\t\t// (only) yield a value that is within bounds\n");
			sb.append("\t\t\tif(");
			if(fromConstrained) {
				if(index.entity.getType() instanceof BooleanType)
					sb.append("current.value." + attributeName + ".CompareTo(from)" + (fromInclusive ? " <= " : " < ") + "0");
				else if(index.entity.getType() instanceof StringType)
					sb.append("String.Compare(current.value." + attributeName + ", from, StringComparison.InvariantCulture)" + (fromInclusive ? " <= " : " < ") + "0");
				else
					sb.append("current.value." + attributeName + (fromInclusive ? " <= " : " < ") + "from");
			}
			if(fromConstrained && toConstrained)
				sb.append(" && ");
			if(toConstrained) {
				if(index.entity.getType() instanceof BooleanType)
					sb.append("current.value." + attributeName + ".CompareTo(to)" + (toInclusive ? " >= " : " > ") + "0");
				else if(index.entity.getType() instanceof StringType)
					sb.append("String.Compare(current.value." + attributeName + ", to, StringComparison.InvariantCulture)" + (toInclusive ? " >= " : " > ") + "0");
				else
					sb.append("current.value." + attributeName + (toInclusive ? " >= " : " > ") + "to");
			}
			sb.append(")\n");
		}
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\t// the value is within range.\n");
		sb.append("\t\t\t\tyield return current.value;\n");
		sb.append("\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t\n");
		
		if(toConstrained) {
			sb.append("\t\t\t// don't go right if the value is already higher than to\n");
			if(index.entity.getType() instanceof BooleanType)
				sb.append("\t\t\tif(current.value." + attributeName + ".CompareTo(to)" + (toInclusive ? " >= " : " > ") + "0)\n");
			else if(index.entity.getType() instanceof StringType)
				sb.append("\t\t\tif(String.Compare(current.value." + attributeName + ", to, StringComparison.InvariantCulture)" + (toInclusive ? " >= " : " > ") + "0)\n");
			else
				sb.append("\t\t\tif(current.value." + attributeName + (toInclusive ? " >= " : " > ") + "to)\n");
		}
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tforeach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.left");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.append("\t\t\t\t{\n");
		sb.append("\t\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t\t}\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t}\n");
		sb.append("\t\t\n");
	}

	void genIndexMaintainingEventHandlers(AttributeIndex index)
	{
		String attributeType = formatAttributeType(index.entity);
		String attributeName = index.entity.getIdent().toString();
		String graphElementType = formatElementInterfaceRef(index.type);
		
		sb.append("\t\tvoid Added(GRGEN_LIBGR.IGraphElement elem)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(elem is " + graphElementType + ")\n");
		sb.append("\t\t\t\tInsert(ref root, (" + graphElementType + ")elem, ((" + graphElementType + ")elem)." + attributeName + ");\n");
		sb.append("\t\t}\n\n");
		sb.append("\t\tvoid Removing(GRGEN_LIBGR.IGraphElement elem)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(elem is " + graphElementType + ")\n");
		sb.append("\t\t\t\tDelete(ref root, (" + graphElementType + ")elem);\n");
		sb.append("\t\t}\n\n");
		sb.append("\t\tvoid ChangingAttribute(GRGEN_LIBGR.IGraphElement elem, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.AttributeChangeType changeType, Object newValue, Object keyValue)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(elem is " + graphElementType + " && attrType.Name==\"" + attributeName + "\")\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tDelete(ref root, (" + graphElementType + ")elem);\n");
		sb.append("\t\t\t\tInsert(ref root, (" + graphElementType + ")elem, (" + attributeType + ")newValue);\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t}\n\n");
		sb.append("\t\tvoid Retyping(GRGEN_LIBGR.IGraphElement oldElem, GRGEN_LIBGR.IGraphElement newElem)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(oldElem is " + graphElementType + ")\n");
		sb.append("\t\t\t\tDelete(ref root, (" + graphElementType + ")oldElem);\n");
		sb.append("\t\t\tif(newElem is " + graphElementType + ")\n");
		sb.append("\t\t\t\tInsert(ref root, (" + graphElementType + ")newElem, ((" + graphElementType + ")newElem)." + attributeName + ");\n");
		sb.append("\t\t}\n\n");
	}

	void genIndexAATreeBalancingInsertionDeletion(AttributeIndex index) {
		String attributeType = formatAttributeType(index.entity);
		String attributeName = index.entity.getIdent().toString();
		String graphElementType = formatElementInterfaceRef(index.type);
		String castForUnique = index.type instanceof NodeType ? " as GRGEN_LGSP.LGSPNode" : " as GRGEN_LGSP.LGSPEdge";

		sb.append("\t\tprivate void Skew(ref TreeNode current)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(current.level != current.left.level)\n");
		sb.append("\t\t\t\treturn;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\t// rotate right\n");
		sb.append("\t\t\tTreeNode left = current.left;\n");
		sb.append("\t\t\tcurrent.left = left.right;\n");
		sb.append("\t\t\tleft.right = current;\n");
		sb.append("\t\t\tcurrent = left;\n");
		sb.append("\t\t}\n");
		sb.append("\n");

		sb.append("\t\tprivate void Split(ref TreeNode current)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(current.right.right.level != current.level)\n");
		sb.append("\t\t\t\treturn;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\t// rotate left\n");
		sb.append("\t\t\tTreeNode right = current.right;\n");
		sb.append("\t\t\tcurrent.right = right.left;\n");
		sb.append("\t\t\tright.left = current;\n");
		sb.append("\t\t\tcurrent = right;\n");
		sb.append("\t\t\t++current.level;\n");
		sb.append("\t\t}\n");
		sb.append("\n");
		
		sb.append("\t\tprivate void Insert(ref TreeNode current, " + graphElementType + " value, " + attributeType + " attributeValue)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(current == bottom)\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tcurrent = new TreeNode(value, bottom);\n");
		sb.append("\t\t\t\t++count;\n");
		sb.append("\t\t\t\t++version;\n");
		sb.append("\t\t\t\treturn;\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t\n");
		if(index.entity.getType() instanceof BooleanType)
			sb.append("\t\t\tif(attributeValue.CompareTo(current.value." + attributeName + ")<0");
		else if(index.entity.getType() instanceof StringType)
			sb.append("\t\t\tif(String.Compare(attributeValue, current.value." + attributeName + ", StringComparison.InvariantCulture)<0");
		else
			sb.append("\t\t\tif(attributeValue < current.value." + attributeName);
		sb.append(" || ( attributeValue == current.value." + attributeName + " && (value" + castForUnique + ").uniqueId < (current.value" + castForUnique + ").uniqueId ) )\n");
		sb.append("\t\t\t\tInsert(ref current.left, value, attributeValue);\n");
		if(index.entity.getType() instanceof BooleanType)
			sb.append("\t\t\telse if(attributeValue.CompareTo(current.value." + attributeName + ")>0");
		else if(index.entity.getType() instanceof StringType)
			sb.append("\t\t\telse if(String.Compare(attributeValue, current.value." + attributeName + ", StringComparison.InvariantCulture)>0");
		else
			sb.append("\t\t\telse if(attributeValue > current.value." + attributeName);
		sb.append(" || ( attributeValue == current.value." + attributeName + " && (value" + castForUnique + ").uniqueId > (current.value" + castForUnique + ").uniqueId ) )\n");
		sb.append("\t\t\t\tInsert(ref current.right, value, attributeValue);\n");
		sb.append("\t\t\telse\n");
		sb.append("\t\t\t\tthrow new Exception(\"Insertion of already available element\");\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\tSkew(ref current);\n");
		sb.append("\t\t\tSplit(ref current);\n");
		sb.append("\t\t}\n");
		sb.append("\n");
		
		sb.append("\t\tprivate void Delete(ref TreeNode current, " + graphElementType + " value)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(current == bottom)\n");
		sb.append("\t\t\t\treturn;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\t// search down the tree (and set pointer last and deleted)\n");
		sb.append("\t\t\tlast = current;\n");
		if(index.entity.getType() instanceof BooleanType)
			sb.append("\t\t\tif(value." + attributeName + ".CompareTo(current.value." + attributeName + ")<0");
		else if(index.entity.getType() instanceof StringType)
			sb.append("\t\t\tif(String.Compare(value." + attributeName + ", current.value." + attributeName + ", StringComparison.InvariantCulture)<0");
		else
			sb.append("\t\t\tif(value." + attributeName + " < current.value." + attributeName);
		sb.append(" || ( value." + attributeName + " == current.value." + attributeName + " && (value" + castForUnique + ").uniqueId < (current.value" + castForUnique + ").uniqueId ) )\n");
		sb.append("\t\t\t\tDelete(ref current.left, value);\n");
		sb.append("\t\t\telse\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tdeleted = current;\n");
		sb.append("\t\t\t\tDelete(ref current.right, value);\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\t// at the bottom of the tree we remove the element (if present)\n");
		sb.append("\t\t\tif(current == last && deleted != bottom && value." + attributeName + " == deleted.value." + attributeName);
		sb.append(" && (value" + castForUnique + ").uniqueId == (deleted.value" + castForUnique + ").uniqueId )\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tdeleted.value = current.value;\n");
		sb.append("\t\t\t\tdeleted = bottom;\n");
		sb.append("\t\t\t\tcurrent = current.right;\n");
		sb.append("\t\t\t\t--count;\n");
		sb.append("\t\t\t\t++version;\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t// on the way back, we rebalance\n");
		sb.append("\t\t\telse if(current.left.level < current.level - 1\n");
		sb.append("\t\t\t\t|| current.right.level < current.level - 1)\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\t--current.level;\n");
		sb.append("\t\t\t\tif(current.right.level > current.level)\n");
		sb.append("\t\t\t\t\tcurrent.right.level = current.level;\n");
		sb.append("\t\t\t\tSkew(ref current);\n");
		sb.append("\t\t\t\tSkew(ref current.right);\n");
		sb.append("\t\t\t\tSkew(ref current.right.right);\n");
		sb.append("\t\t\t\tSplit(ref current);\n");
		sb.append("\t\t\t\tSplit(ref current.right);\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t}\n");
		sb.append("\n");
	}

	void genIndexSetType() {
		sb.append("\tpublic class " + model.getIdent() + "IndexSet : GRGEN_LIBGR.IIndexSet\n");
		sb.append("\t{\n");
		sb.append("\t\tpublic " + model.getIdent() + "IndexSet(GRGEN_LGSP.LGSPGraph graph)\n");
		sb.append("\t\t{\n");
		for(Index index : model.getIndices()) {
			String indexName = index.getIdent().toString();
			sb.append("\t\t\t" + indexName + " = new Index" + indexName + "Impl(graph);\n");
		}
		sb.append("\t\t}\n");
		sb.append("\n");
				
		for(Index index : model.getIndices()) {
			String indexName = index.getIdent().toString();
			sb.append("\t\tpublic Index" + indexName + "Impl " + indexName +";\n");
		}
		sb.append("\n");

		sb.append("\t\tpublic GRGEN_LIBGR.IIndex GetIndex(string indexName)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tswitch(indexName)\n");
		sb.append("\t\t\t{\n");
		for(Index index : model.getIndices()) {
			String indexName = index.getIdent().toString();
			sb.append("\t\t\t\tcase \"" + indexName + "\": return " + indexName + ";\n");
		}
		sb.append("\t\t\t}\n");
		sb.append("\t\t\treturn null;\n");
		sb.append("\t\t}\n");
		sb.append("\n");
		
		sb.append("\t\tpublic void FillAsClone(GRGEN_LGSP.LGSPGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
		sb.append("\t\t{\n");
		for(Index index : model.getIndices()) {
			String indexName = index.getIdent().toString();
			sb.append("\t\t\t" + indexName + ".FillAsClone((Index" + indexName + "Impl)originalGraph.indices.GetIndex(\"" + indexName + "\"), oldToNewMap);\n");
		}
		sb.append("\t\t}\n");
		
		sb.append("\t}\n");
	}
	
	void genIndexImplementation(IncidenceIndex index, int indexNum) {
		String indexName = index.getIdent().toString();
		String graphElementType = formatElementInterfaceRef(((IncidenceIndex)index).getStartNodeType());
		String modelName = model.getIdent().toString() + "GraphModel";
		sb.append("\tpublic class Index" + indexName + "Impl : Index" + indexName + "\n");
		sb.append("\t{\n");
		
		sb.append("\t\tpublic GRGEN_LIBGR.IndexDescription Description { get { return " + modelName + ".GetIndexDescription(" + indexNum + "); } }\n");
		sb.append("\n");

		sb.append("\t\tprotected class TreeNode\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\t// search tree structure\n");
		sb.append("\t\t\tpublic TreeNode left;\n");
		sb.append("\t\t\tpublic TreeNode right;\n");
		sb.append("\t\t\tpublic int level;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\t// user data\n");
		sb.append("\t\t\tpublic int key;\n");
		sb.append("\t\t\tpublic " + graphElementType + " value;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\t// for the bottom node, operating as sentinel\n");
		sb.append("\t\t\tpublic TreeNode()\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tleft = this;\n");
		sb.append("\t\t\t\tright = this;\n");
		sb.append("\t\t\t\tlevel = 0;\n");
	    sb.append("\t\t\t}\n");
	    sb.append("\t\t\t\n");
	    sb.append("\t\t\t// for regular nodes (that are born as leaf nodes)\n");
	    sb.append("\t\t\tpublic TreeNode(int key, " + graphElementType + " value, TreeNode bottom)\n");
	    sb.append("\t\t\t{\n");
	    sb.append("\t\t\t\tleft = bottom;\n");
	    sb.append("\t\t\t\tright = bottom;\n");
	    sb.append("\t\t\t\tlevel = 1;\n");
	    sb.append("\t\t\t\t\n");
	    sb.append("\t\t\t\tthis.key = key;\n");
	    sb.append("\t\t\t\tthis.value = value;\n");
	    sb.append("\t\t\t}\n");
	    sb.append("\t\t\t\n");
	    sb.append("\t\t\t// for copy constructing from other index\n");
	    sb.append("\t\t\tpublic TreeNode(TreeNode left, TreeNode right, int level, int key, " + graphElementType + " value)\n");
	    sb.append("\t\t\t{\n");
	    sb.append("\t\t\t\tthis.left = left;\n");
	    sb.append("\t\t\t\tthis.right = right;\n");
	    sb.append("\t\t\t\tthis.level = level;\n");
	    sb.append("\t\t\t\t\n");
	    sb.append("\t\t\t\tthis.key = key;\n");
	    sb.append("\t\t\t\tthis.value = value;\n");
	    sb.append("\t\t\t}\n");
	    sb.append("\t\t}\n");
	    sb.append("\n");

		sb.append("\t\tprotected TreeNode root;\n");
		sb.append("\t\tprotected TreeNode bottom;\n");
		sb.append("\t\tprotected TreeNode deleted;\n");
		sb.append("\t\tprotected TreeNode last;\n");
		sb.append("\t\tprotected int count;\n");
		sb.append("\t\tprotected int version;\n");
		sb.append("\n");
		sb.append("\t\tprotected IDictionary<" + graphElementType + ", int> nodeToIncidenceCount = new Dictionary<" + graphElementType + ", int>();\n");
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
		
		sb.append("\t\tpublic Index" + indexName + "Impl(GRGEN_LGSP.LGSPGraph graph)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tthis.graph = graph;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\t// initialize AA tree used to implement the index\n");
		sb.append("\t\t\tbottom = new TreeNode();\n");
		sb.append("\t\t\troot = bottom;\n");
		sb.append("\t\t\tdeleted = bottom;\n");
		sb.append("\t\t\tcount = 0;\n");
		sb.append("\t\t\tversion = 0;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\tgraph.OnEdgeAdded += EdgeAdded;\n");
		sb.append("\t\t\tgraph.OnNodeAdded += NodeAdded;\n");
		sb.append("\t\t\tgraph.OnRemovingEdge += RemovingEdge;\n");
		sb.append("\t\t\tgraph.OnRemovingNode += RemovingNode;\n");
		sb.append("\t\t\tgraph.OnRetypingEdge += RetypingEdge;\n");
		sb.append("\t\t\tgraph.OnRetypingNode += RetypingNode;\n");
		sb.append("\t\t}\n");
		sb.append("\n");

		sb.append("\t\tpublic void FillAsClone(Index" + indexName + "Impl that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\troot = FillAsClone(that.root, that.bottom, oldToNewMap);\n");
		sb.append("\t\t\tcount = that.count;\n");
		sb.append("\t\t\tforeach(KeyValuePair<" + graphElementType + ", int> ntic in that.nodeToIncidenceCount)\n");
		sb.append("\t\t\t\tnodeToIncidenceCount.Add((" + graphElementType + ")oldToNewMap[ntic.Key], ntic.Value);\n");
		sb.append("\t\t}\n");
		sb.append("\n");

		sb.append("\t\tprotected TreeNode FillAsClone(TreeNode that, TreeNode otherBottom, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(that == otherBottom)\n");
		sb.append("\t\t\t\treturn bottom;\n");
		sb.append("\t\t\telse\n");
		sb.append("\t\t\t\treturn new TreeNode(\n");
		sb.append("\t\t\t\t\tFillAsClone(that.left, otherBottom, oldToNewMap),\n");
		sb.append("\t\t\t\t\tFillAsClone(that.right, otherBottom, oldToNewMap),\n");
		sb.append("\t\t\t\t\tthat.level,\n");
		sb.append("\t\t\t\t\tthat.key,\n");
		sb.append("\t\t\t\t\t(" + graphElementType + ")oldToNewMap[that.value]\n");
        sb.append("\t\t\t\t);\n");
        sb.append("\t\t}\n");
		sb.append("\n");

		genIndexMaintainingEventHandlers(index);

		genIndexAATreeBalancingInsertionDeletion(index);

		//genCheckDump(index);

		sb.append("\t\tprivate GRGEN_LGSP.LGSPGraph graph;\n");

		sb.append("\t}\n");
		sb.append("\n");
	}

	void genEqual(IncidenceIndex index)
	{
		String graphElementType = formatElementInterfaceRef(((IncidenceIndex)index).getStartNodeType());
		
		sb.append("\t\tprivate IEnumerable<" + graphElementType + "> Lookup(TreeNode current, int fromto)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(current == bottom)\n");
		sb.append("\t\t\t\tyield break;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\tint versionAtIterationBegin = version;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\t// don't go left if the value is already lower than fromto\n");
		sb.append("\t\t\tif(current.key >= fromto)\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tforeach(" + graphElementType + " value in Lookup(current.left, fromto))\n");
		sb.append("\t\t\t\t{\n");
		sb.append("\t\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t\t}\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t\n");
		
		sb.append("\t\t\t// (only) yield a value that is equal to fromto\n");
		sb.append("\t\t\tif(current.key == fromto)\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\t// the value is within range.\n");
		sb.append("\t\t\t\tyield return current.value;\n");
		sb.append("\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t\n");
		
		sb.append("\t\t\t// don't go right if the value is already higher than fromto\n");
		sb.append("\t\t\tif(current.key <= fromto)\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tforeach(" + graphElementType + " value in Lookup(current.right, fromto))\n");
		sb.append("\t\t\t\t{\n");
		sb.append("\t\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t\t}\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t}\n");
		sb.append("\t\t\n");
	}

	void genAscending(IncidenceIndex index, boolean fromConstrained, boolean fromInclusive, boolean toConstrained, boolean toInclusive)
	{
		String attributeType = "int";
		String graphElementType = formatElementInterfaceRef(((IncidenceIndex)index).getStartNodeType());

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
		
		sb.append("\t\tprivate IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix + "(TreeNode current");
		if(fromConstrained)
			sb.append(", " + attributeType + " from");
		if(toConstrained)
			sb.append(", " + attributeType + " to");
		sb.append(")\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(current == bottom)\n");
		sb.append("\t\t\t\tyield break;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\tint versionAtIterationBegin = version;\n");
		sb.append("\t\t\t\n");
		if(fromConstrained) {
			sb.append("\t\t\t// don't go left if the value is already lower than from\n");
			sb.append("\t\t\tif(current.key" + (fromInclusive ? " >= " : " > ") + "from)\n");
		}
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tforeach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.left");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.append("\t\t\t\t{\n");
		sb.append("\t\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t\t}\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t\n");
		
		if(fromConstrained || toConstrained) {
			sb.append("\t\t\t// (only) yield a value that is within bounds\n");
			sb.append("\t\t\tif(");
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
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\t// the value is within range.\n");
		sb.append("\t\t\t\tyield return current.value;\n");
		sb.append("\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t\n");
		
		if(toConstrained) {
			sb.append("\t\t\t// don't go right if the value is already higher than to\n");
			sb.append("\t\t\tif(current.key" + (toInclusive ? " <= " : " < ") + "to)\n");
		}
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tforeach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.right");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.append("\t\t\t\t{\n");
		sb.append("\t\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t\t}\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t}\n");
		sb.append("\t\t\n");
	}

	void genDescending(IncidenceIndex index, boolean fromConstrained, boolean fromInclusive, boolean toConstrained, boolean toInclusive)
	{
		String attributeType = "int";
		String graphElementType = formatElementInterfaceRef(((IncidenceIndex)index).getStartNodeType());

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
		
		sb.append("\t\tprivate IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix + "(TreeNode current");
		if(fromConstrained)
			sb.append(", " + attributeType + " from");
		if(toConstrained)
			sb.append(", " + attributeType + " to");
		sb.append(")\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(current == bottom)\n");
		sb.append("\t\t\t\tyield break;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\tint versionAtIterationBegin = version;\n");
		sb.append("\t\t\t\n");
		if(fromConstrained) {
			sb.append("\t\t\t// don't go left if the value is already lower than from\n");
			sb.append("\t\t\tif(current.key" + (fromInclusive ? " <= " : " < ") + "from)\n");
		}
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tforeach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.right");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.append("\t\t\t\t{\n");
		sb.append("\t\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t\t}\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t\n");
		
		if(fromConstrained || toConstrained) {
			sb.append("\t\t\t// (only) yield a value that is within bounds\n");
			sb.append("\t\t\tif(");
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
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\t// the value is within range.\n");
		sb.append("\t\t\t\tyield return current.value;\n");
		sb.append("\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t\n");
		
		if(toConstrained) {
			sb.append("\t\t\t// don't go right if the value is already higher than to\n");
			sb.append("\t\t\tif(current.key" + (toInclusive ? " >= " : " > ") + "to)\n");
		}
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tforeach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.left");
		if(fromConstrained)
			sb.append(", from");
		if(toConstrained)
			sb.append(", to");
		sb.append("))\n");
		sb.append("\t\t\t\t{\n");
		sb.append("\t\t\t\t\tyield return value;\n");
		sb.append("\t\t\t\t\tif(version != versionAtIterationBegin)\n");
		sb.append("\t\t\t\t\t\tthrow new InvalidOperationException(\"Index changed during enumeration\");\n");
		sb.append("\t\t\t\t}\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t}\n");
		sb.append("\t\t\n");
	}

	void genGetIncidenceCount(IncidenceIndex index)
	{
		String graphElementType = formatElementInterfaceRef(index.getStartNodeType());
		sb.append("\t\tpublic int GetIncidenceCount(GRGEN_LIBGR.IGraphElement element)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\treturn GetIncidenceCount((" + graphElementType + ") element);\n");
		sb.append("\t\t}\n\n");

		sb.append("\t\tpublic int GetIncidenceCount(" + graphElementType + " element)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\treturn nodeToIncidenceCount[element];\n");
		sb.append("\t\t}\n");
	}
	
	void genCheckDump(IncidenceIndex index)
	{
		String startNodeType = formatElementInterfaceRef(((IncidenceIndex)index).getStartNodeType());

		sb.append("\t\tprotected void Check(TreeNode current)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(current == bottom)\n");
		sb.append("\t\t\t\treturn;\n");
		sb.append("\t\t\tCheck(current.left);\n");
		sb.append("\t\t\tif(!nodeToIncidenceCount.ContainsKey(current.value)) {\n");
		sb.append("\t\t\t\tDump(root);\n");		
		sb.append("\t\t\t\tDump();\n");
		sb.append("\t\t\t\tthrow new Exception(\"Missing node\");\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\tif(nodeToIncidenceCount[current.value]!=current.key) {\n");
		sb.append("\t\t\t\tDump(root);\n");		
		sb.append("\t\t\t\tDump();\n");
		sb.append("\t\t\t\tthrow new Exception(\"Incidence values differ\");\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\tCheck(current.right);\n");				
		sb.append("\t\t}\n");
		sb.append("\t\t\n");

		sb.append("\t\tprotected void Dump(TreeNode current)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(current == bottom)\n");
		sb.append("\t\t\t\treturn;\n");
		sb.append("\t\t\tDump(current.left);\n");
		sb.append("\t\t\tConsole.Write(current.key);\n");
		sb.append("\t\t\tConsole.Write(\" -> \");\n");
		sb.append("\t\t\tConsole.WriteLine(graph.GetElementName(current.value));\n");
		sb.append("\t\t\tDump(current.right);\n");				
		sb.append("\t\t}\n");
		sb.append("\t\t\n");
		
		sb.append("\t\tprotected void Dump()\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tforeach(KeyValuePair<" + startNodeType + ",int> kvp in nodeToIncidenceCount) {\n");
		sb.append("\t\t\t\tConsole.Write(graph.GetElementName(kvp.Key));\n");
		sb.append("\t\t\t\tConsole.Write(\" => \");\n");
		sb.append("\t\t\t\tConsole.WriteLine(kvp.Value);\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t}\n");
		sb.append("\t\t\n");
	}

	void genIndexMaintainingEventHandlers(IncidenceIndex index)
	{
		String startNodeType = formatElementInterfaceRef(((IncidenceIndex)index).getStartNodeType());
		String incidentEdgeType = formatElementInterfaceRef(((IncidenceIndex)index).getIncidentEdgeType());
		String incidentEdgeTypeType = formatTypeClassRefInstance(((IncidenceIndex)index).getIncidentEdgeType());

		sb.append("\t\tvoid EdgeAdded(GRGEN_LIBGR.IEdge edge)\n");
		sb.append("\t\t{\n");
		//sb.append("Check(root);\n");
		sb.append("\t\t\tif(!(edge is " + incidentEdgeType + "))\n");
		sb.append("\t\t\t\treturn;\n");
		sb.append("\t\t\tGRGEN_LIBGR.INode source = edge.Source;\n");
		sb.append("\t\t\tGRGEN_LIBGR.INode target = edge.Target;\n");
		genIndexMaintainingEdgeAdded(index);
		//sb.append("Check(root);\n");
		sb.append("\t\t}\n\n");
		
		sb.append("\t\tvoid NodeAdded(GRGEN_LIBGR.INode node)\n");
		sb.append("\t\t{\n");
		//sb.append("Check(root);\n");
		sb.append("\t\t\tif(node is " + startNodeType + ") {\n");
		sb.append("\t\t\t\tnodeToIncidenceCount.Add((" + startNodeType + ")node, 0);\n");
		sb.append("\t\t\t\tInsert(ref root, 0, (" + startNodeType + ")node);\n");
		sb.append("\t\t\t}\n");
		//sb.append("Check(root);\n");
		sb.append("\t\t}\n\n");
	
		sb.append("\t\tvoid RemovingEdge(GRGEN_LIBGR.IEdge edge)\n");
		sb.append("\t\t{\n");
		//sb.append("Check(root);\n");
		sb.append("\t\t\tif(!(edge is " + incidentEdgeType + "))\n");
		sb.append("\t\t\t\treturn;\n");
		sb.append("\t\t\tGRGEN_LIBGR.INode source = edge.Source;\n");
		sb.append("\t\t\tGRGEN_LIBGR.INode target = edge.Target;\n");
		genIndexMaintainingRemovingEdge(index);
		//sb.append("Check(root);\n");
		sb.append("\t\t}\n\n");
		
		sb.append("\t\tvoid RemovingNode(GRGEN_LIBGR.INode node)\n");
		sb.append("\t\t{\n");
		//sb.append("Check(root);\n");
		sb.append("\t\t\tif(node is " + startNodeType + ") {\n");
		sb.append("\t\t\t\tnodeToIncidenceCount.Remove((" + startNodeType + ")node);\n");
		sb.append("\t\t\t\tDelete(ref root, 0, (" + startNodeType + ")node);\n");
		sb.append("\t\t\t}\n");
		//sb.append("Check(root);\n");
		sb.append("\t\t}\n\n");
	
		sb.append("\t\tvoid RetypingEdge(GRGEN_LIBGR.IEdge oldEdge, GRGEN_LIBGR.IEdge newEdge)\n");
		sb.append("\t\t{\n");
		//sb.append("Check(root);\n");
		sb.append("\t\t\tRemovingEdge(oldEdge);\n");
		sb.append("\t\t\tEdgeAdded(newEdge);\n");
		//sb.append("Check(root);\n");
		sb.append("\t\t}\n\n");
	
		sb.append("\t\tvoid RetypingNode(GRGEN_LIBGR.INode oldNode, GRGEN_LIBGR.INode newNode)\n");
		sb.append("\t\t{\n");
		//sb.append("Check(root);\n");
		sb.append("\t\t\tIDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType> incidentEdges = GRGEN_LIBGR.GraphHelper.Incident(oldNode, " + incidentEdgeTypeType + ", graph.Model.NodeModel.RootType);\n");
		sb.append("\t\t\tforeach(KeyValuePair<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType> edgeKVP in incidentEdges)\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tGRGEN_LIBGR.IEdge edge = edgeKVP.Key;\n");
		sb.append("\t\t\t\tGRGEN_LIBGR.INode source = edge.Source;\n");
		sb.append("\t\t\t\tGRGEN_LIBGR.INode target = edge.Target;\n");
		genIndexMaintainingRemovingEdge(index);
		sb.append("\t\t\t}\n");

		sb.append("\t\t\tif(oldNode is " + startNodeType + ") {\n");
		sb.append("\t\t\t\tnodeToIncidenceCount.Remove((" + startNodeType + ")oldNode);\n");
		sb.append("\t\t\t\tDelete(ref root, 0, (" + startNodeType + ")oldNode);\n");
		sb.append("\t\t\t}\n");

		sb.append("\t\t\tif(newNode is " + startNodeType + ") {\n");
		sb.append("\t\t\t\tnodeToIncidenceCount.Add((" + startNodeType + ")newNode, 0);\n");
		sb.append("\t\t\t\tInsert(ref root, 0, (" + startNodeType + ")newNode);\n");
		sb.append("\t\t\t}\n");

		sb.append("\t\t\tforeach(KeyValuePair<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType> edgeKVP in incidentEdges)\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tGRGEN_LIBGR.IEdge edge = edgeKVP.Key;\n");
		sb.append("\t\t\t\tGRGEN_LIBGR.INode source = edge.Source==oldNode ? newNode : edge.Source;\n");
		sb.append("\t\t\t\tGRGEN_LIBGR.INode target = edge.Target==oldNode ? newNode : edge.Target;\n");
		genIndexMaintainingEdgeAdded(index);
		sb.append("\t\t\t}\n");
		//sb.append("Check(root);\n");
		sb.append("\t\t}\n\n");
	}

	void genIndexMaintainingEdgeAdded(IncidenceIndex index)
	{
		String startNodeType = formatElementInterfaceRef(((IncidenceIndex)index).getStartNodeType());
		String adjacentNodeType = formatElementInterfaceRef(((IncidenceIndex)index).getAdjacentNodeType());

		if(index.Direction()==IncidentEdgeExpr.OUTGOING) {
			sb.append("\t\t\t\tif(source is " + startNodeType + " && target is " + adjacentNodeType + ") {\n");
			sb.append("\t\t\t\t\tDelete(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], (" + startNodeType + ")source);\n");
			sb.append("\t\t\t\t\tnodeToIncidenceCount[(" + startNodeType + ")source] = nodeToIncidenceCount[(" + startNodeType + ")source] + 1;\n");
			sb.append("\t\t\t\t\tInsert(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], (" + startNodeType + ")source);\n");
			sb.append("\t\t\t\t}\n");
		} else if(index.Direction()==IncidentEdgeExpr.INCOMING) {
			sb.append("\t\t\t\tif(target is " + startNodeType + " && source is " + adjacentNodeType + ") {\n");
			sb.append("\t\t\t\t\tDelete(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], (" + startNodeType + ")target);\n");
			sb.append("\t\t\t\t\tnodeToIncidenceCount[(" + startNodeType + ")target] = nodeToIncidenceCount[(" + startNodeType + ")target] + 1;\n");
			sb.append("\t\t\t\t\tInsert(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], (" + startNodeType + ")target);\n");
			sb.append("\t\t\t\t}\n");
		} else {
			sb.append("\t\t\t\tif(source is " + startNodeType + " && target is " + adjacentNodeType + ") {\n");
			sb.append("\t\t\t\t\tDelete(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], (" + startNodeType + ")source);\n");
			sb.append("\t\t\t\t\tnodeToIncidenceCount[(" + startNodeType + ")source] = nodeToIncidenceCount[(" + startNodeType + ")source] + 1;\n");
			sb.append("\t\t\t\t\tInsert(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], (" + startNodeType + ")source);\n");
			sb.append("\t\t\t\t}\n");
			sb.append("\t\t\t\tif(target is " + startNodeType + " && source is " + adjacentNodeType + " && source!=target) {\n");
			sb.append("\t\t\t\t\tDelete(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], (" + startNodeType + ")target);\n");
			sb.append("\t\t\t\t\tnodeToIncidenceCount[(" + startNodeType + ")target] = nodeToIncidenceCount[(" + startNodeType + ")target] + 1;\n");
			sb.append("\t\t\t\t\tInsert(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], (" + startNodeType + ")target);\n");
			sb.append("\t\t\t\t}\n");
		}		
	}

	void genIndexMaintainingRemovingEdge(IncidenceIndex index)
	{
		String startNodeType = formatElementInterfaceRef(((IncidenceIndex)index).getStartNodeType());
		String adjacentNodeType = formatElementInterfaceRef(((IncidenceIndex)index).getAdjacentNodeType());
	
		if(index.Direction()==IncidentEdgeExpr.OUTGOING) {
			sb.append("\t\t\t\tif(source is " + startNodeType + " && target is " + adjacentNodeType + ") {\n");
			sb.append("\t\t\t\t\tDelete(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], (" + startNodeType + ")source);\n");
			sb.append("\t\t\t\t\tnodeToIncidenceCount[(" + startNodeType + ")source] = nodeToIncidenceCount[(" + startNodeType + ")source] - 1;\n");
			sb.append("\t\t\t\t\tInsert(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], (" + startNodeType + ")source);\n");
			sb.append("\t\t\t\t}\n");
		} else if(index.Direction()==IncidentEdgeExpr.INCOMING) {
			sb.append("\t\t\t\tif(target is " + startNodeType + " && source is " + adjacentNodeType + ") {\n");
			sb.append("\t\t\t\t\tDelete(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], (" + startNodeType + ")target);\n");
			sb.append("\t\t\t\t\tnodeToIncidenceCount[(" + startNodeType + ")target] = nodeToIncidenceCount[(" + startNodeType + ")target] - 1;\n");
			sb.append("\t\t\t\t\tInsert(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], (" + startNodeType + ")target);\n");
			sb.append("\t\t\t\t}\n");
		} else {
			sb.append("\t\t\t\tif(source is " + startNodeType + " && target is " + adjacentNodeType + ") {\n");
			sb.append("\t\t\t\t\tDelete(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], (" + startNodeType + ")source);\n");
			sb.append("\t\t\t\t\tnodeToIncidenceCount[(" + startNodeType + ")source] = nodeToIncidenceCount[(" + startNodeType + ")source] - 1;\n");
			sb.append("\t\t\t\t\tInsert(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], (" + startNodeType + ")source);\n");
			sb.append("\t\t\t\t}\n");
			sb.append("\t\t\t\tif(target is " + startNodeType + " && source is " + adjacentNodeType + " && source!=target) {\n");
			sb.append("\t\t\t\t\tDelete(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], (" + startNodeType + ")target);\n");
			sb.append("\t\t\t\t\tnodeToIncidenceCount[(" + startNodeType + ")target] = nodeToIncidenceCount[(" + startNodeType + ")target] - 1;\n");
			sb.append("\t\t\t\t\tInsert(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], (" + startNodeType + ")target);\n");
			sb.append("\t\t\t\t}\n");
		}
	}

	void genIndexAATreeBalancingInsertionDeletion(IncidenceIndex index) {
		String graphElementType = formatElementInterfaceRef(((IncidenceIndex)index).getStartNodeType());
		String castForUnique = " as GRGEN_LGSP.LGSPNode";

		sb.append("\t\tprivate void Skew(ref TreeNode current)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(current.level != current.left.level)\n");
		sb.append("\t\t\t\treturn;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\t// rotate right\n");
		sb.append("\t\t\tTreeNode left = current.left;\n");
		sb.append("\t\t\tcurrent.left = left.right;\n");
		sb.append("\t\t\tleft.right = current;\n");
		sb.append("\t\t\tcurrent = left;\n");
		sb.append("\t\t}\n");
		sb.append("\n");

		sb.append("\t\tprivate void Split(ref TreeNode current)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(current.right.right.level != current.level)\n");
		sb.append("\t\t\t\treturn;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\t// rotate left\n");
		sb.append("\t\t\tTreeNode right = current.right;\n");
		sb.append("\t\t\tcurrent.right = right.left;\n");
		sb.append("\t\t\tright.left = current;\n");
		sb.append("\t\t\tcurrent = right;\n");
		sb.append("\t\t\t++current.level;\n");
		sb.append("\t\t}\n");
		sb.append("\n");
		
		sb.append("\t\tprivate void Insert(ref TreeNode current, int key, " + graphElementType + " value)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(current == bottom)\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tcurrent = new TreeNode(key, value, bottom);\n");
		sb.append("\t\t\t\t++count;\n");
		sb.append("\t\t\t\t++version;\n");
		sb.append("\t\t\t\treturn;\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\tif(key < current.key");
		sb.append(" || ( key == current.key && (value" + castForUnique + ").uniqueId < (current.value" + castForUnique + ").uniqueId ) )\n");
		sb.append("\t\t\t\tInsert(ref current.left, key, value);\n");
		sb.append("\t\t\telse if(key > current.key");
		sb.append(" || ( key == current.key && (value" + castForUnique + ").uniqueId > (current.value" + castForUnique + ").uniqueId ) )\n");
		sb.append("\t\t\t\tInsert(ref current.right, key, value);\n");
		sb.append("\t\t\telse\n");
		sb.append("\t\t\t\tthrow new Exception(\"Insertion of already available element\");\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\tSkew(ref current);\n");
		sb.append("\t\t\tSplit(ref current);\n");
		sb.append("\t\t}\n");
		sb.append("\n");
		
		sb.append("\t\tprivate void Delete(ref TreeNode current, int key, " + graphElementType + " value)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tif(current == bottom)\n");
		sb.append("\t\t\t\treturn;\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\t// search down the tree (and set pointer last and deleted)\n");
		sb.append("\t\t\tlast = current;\n");
		sb.append("\t\t\tif(key < current.key");
		sb.append(" || ( key == current.key && (value" + castForUnique + ").uniqueId < (current.value" + castForUnique + ").uniqueId ) )\n");
		sb.append("\t\t\t\tDelete(ref current.left, key, value);\n");
		sb.append("\t\t\telse\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tdeleted = current;\n");
		sb.append("\t\t\t\tDelete(ref current.right, key, value);\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t\n");
		sb.append("\t\t\t// at the bottom of the tree we remove the element (if present)\n");
		sb.append("\t\t\tif(current == last && deleted != bottom && key == deleted.key");
		sb.append(" && (value" + castForUnique + ").uniqueId == (deleted.value" + castForUnique + ").uniqueId )\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\tdeleted.value = current.value;\n");
		sb.append("\t\t\t\tdeleted.key = current.key;\n");
		sb.append("\t\t\t\tdeleted = bottom;\n");
		sb.append("\t\t\t\tcurrent = current.right;\n");
		sb.append("\t\t\t\t--count;\n");
		sb.append("\t\t\t\t++version;\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t\t// on the way back, we rebalance\n");
		sb.append("\t\t\telse if(current.left.level < current.level - 1\n");
		sb.append("\t\t\t\t|| current.right.level < current.level - 1)\n");
		sb.append("\t\t\t{\n");
		sb.append("\t\t\t\t--current.level;\n");
		sb.append("\t\t\t\tif(current.right.level > current.level)\n");
		sb.append("\t\t\t\t\tcurrent.right.level = current.level;\n");
		sb.append("\t\t\t\tSkew(ref current);\n");
		sb.append("\t\t\t\tSkew(ref current.right);\n");
		sb.append("\t\t\t\tSkew(ref current.right.right);\n");
		sb.append("\t\t\t\tSplit(ref current);\n");
		sb.append("\t\t\t\tSplit(ref current.right);\n");
		sb.append("\t\t\t}\n");
		sb.append("\t\t}\n");
		sb.append("\n");
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
		for(InheritanceType type : types) {
			sb.append("\t\t\t\tcase \"" + getPackagePrefixDoubleColon(type) + formatIdentifiable(type) + "\" : return " + formatTypeClassRef(type) + ".typeVar;\n");
		}
		sb.append("\t\t\t}\n");
		sb.append("\t\t\treturn null;\n");
		sb.append("\t\t}\n");
		sb.append("\t\tGRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(string name)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\treturn GetType(name);\n");
		sb.append("\t\t}\n");

		sb.append("\t\tprivate GRGEN_LIBGR." + kindStr + "Type[] types = {\n");
		for(InheritanceType type : types) {
			sb.append("\t\t\t" + formatTypeClassRef(type) + ".typeVar,\n");
		}
		sb.append("\t\t};\n");
		sb.append("\t\tpublic GRGEN_LIBGR." + kindStr + "Type[] Types { get { return types; } }\n");
		sb.append("\t\tGRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types "
				+ "{ get { return types; } }\n");

		sb.append("\t\tprivate System.Type[] typeTypes = {\n");
		for(InheritanceType type : types) {
			sb.append("\t\t\ttypeof(" + formatTypeClassRef(type) + "),\n");
		}
		sb.append("\t\t};\n");
		sb.append("\t\tpublic System.Type[] TypeTypes { get { return typeTypes; } }\n");

		sb.append("\t\tprivate GRGEN_LIBGR.AttributeType[] attributeTypes = {\n");
		for(InheritanceType type : types) {
			String ctype = formatTypeClassRef(type);
			for(Entity member : type.getMembers()) {
				sb.append("\t\t\t" + ctype + "." + formatAttributeTypeName(member) + ",\n");
			}
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
		sb.append("\t// IGraphModel (LGSPGraphModel) implementation\n");
		sb.append("\t//\n");

		sb.append("\tpublic sealed class " + modelName + "GraphModel : GRGEN_LGSP.LGSPGraphModel\n");
		sb.append("\t{\n");
		sb.append("\t\tpublic " + modelName + "GraphModel()\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\tFullyInitializeExternalTypes();\n");
		sb.append("\t\t}\n\n");

		genGraphModelBody(modelName, true);

		sb.append("\t}\n");
	}

	private void genGraphIncludingModelClass() {
		String modelName = model.getIdent().toString();

		sb.append("\t//\n");
		sb.append("\t// IGraph (LGSPGraph) / IGraphModel implementation\n");
		sb.append("\t//\n");

		sb.append(
			  "\tpublic class " + modelName + "Graph : GRGEN_LGSP.LGSPGraph, GRGEN_LIBGR.IGraphModel\n"
			+ "\t{\n"
			+ "\t\tpublic " + modelName + "Graph() : base(GetNextGraphName())\n"
			+ "\t\t{\n"
			+ "\t\t\tFullyInitializeExternalTypes();\n"
			+ "\t\t\tInitializeGraph(this);\n"
			+ "\t\t}\n\n"
		);

		for(NodeType nt : model.getNodeTypes()) {
			genCreateNodeConvenienceHelper(nt, false);
		}
		for(PackageType pt : model.getPackages()) {
			for(NodeType nt : pt.getNodeTypes()) {
				genCreateNodeConvenienceHelper(nt, false);
			}
		}

		for(EdgeType et : model.getEdgeTypes()) {
			genCreateEdgeConvenienceHelper(et, false);
		}
		for(PackageType pt : model.getPackages()) {
			for(EdgeType et : pt.getEdgeTypes()) {
				genCreateEdgeConvenienceHelper(et, false);
			}
		}

		genGraphModelBody(modelName, false);
		sb.append("\t}\n");
	}

	private void genNamedGraphIncludingModelClass() {
		String modelName = model.getIdent().toString();

		sb.append("\t//\n");
		sb.append("\t// INamedGraph (LGSPNamedGraph) / IGraphModel implementation\n");
		sb.append("\t//\n");

		sb.append(
			  "\tpublic class " + modelName + "NamedGraph : GRGEN_LGSP.LGSPNamedGraph, GRGEN_LIBGR.IGraphModel\n"
			+ "\t{\n"
			+ "\t\tpublic " + modelName + "NamedGraph() : base(GetNextGraphName())\n"
			+ "\t\t{\n"
			+ "\t\t\tFullyInitializeExternalTypes();\n"
			+ "\t\t\tInitializeGraph(this);\n"
			+ "\t\t}\n\n"
		);

		for(NodeType nodeType : model.getNodeTypes()) {
			genCreateNodeConvenienceHelper(nodeType, true);
		}
		for(PackageType pt : model.getPackages()) {
			for(NodeType nt : pt.getNodeTypes()) {
				genCreateNodeConvenienceHelper(nt, true);
			}
		}

		for(EdgeType et : model.getEdgeTypes()) {
			genCreateEdgeConvenienceHelper(et, true);
		}
		for(PackageType pt : model.getPackages()) {
			for(EdgeType et : pt.getEdgeTypes()) {
				genCreateEdgeConvenienceHelper(et, true);
			}
		}

		genGraphModelBody(modelName, false);
		sb.append("\t}\n");
	}

	private void genCreateNodeConvenienceHelper(NodeType nodeType, boolean isNamed) {
		if(nodeType.isAbstract())
			return;

		String name = getPackagePrefix(nodeType) + formatIdentifiable(nodeType);
		String elemref = formatElementClassRef(nodeType);
		sb.append(
			  "\t\tpublic " + elemref + " CreateNode" + name + "()\n"
			+ "\t\t{\n"
			+ "\t\t\treturn " + elemref + ".CreateNode(this);\n"
			+ "\t\t}\n\n"
		);
		
		if(!isNamed)
			return;

		sb.append(
			"\t\tpublic " + elemref + " CreateNode" + name + "(string nodeName)\n"
			+ "\t\t{\n"
			+ "\t\t\treturn " + elemref + ".CreateNode(this, nodeName);\n"
			+ "\t\t}\n\n"
		);
	}

	private void genCreateEdgeConvenienceHelper(EdgeType edgeType, boolean isNamed) {
		if(edgeType.isAbstract())
			return;
		
		String name = getPackagePrefix(edgeType) + formatIdentifiable(edgeType);
		String elemref = formatElementClassRef(edgeType);
		sb.append(
			  "\t\tpublic @" + elemref + " CreateEdge" + name
			+ "(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)\n"
			+ "\t\t{\n"
			+ "\t\t\treturn @" + elemref + ".CreateEdge(this, source, target);\n"
			+ "\t\t}\n\n"
		);

		if(!isNamed)
			return;

		sb.append(
			  "\t\tpublic @" + elemref + " CreateEdge" + name
			+ "(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)\n"
			+ "\t\t{\n"
			+ "\t\t\treturn @" + elemref + ".CreateEdge(this, source, target, edgeName);\n"
			+ "\t\t}\n\n"
		);
	}

	private void genGraphModelBody(String modelName, boolean inPureGraphModel) {
		sb.append("\t\tprivate " + modelName + "NodeModel nodeModel = new " + modelName + "NodeModel();\n");
		sb.append("\t\tprivate " + modelName + "EdgeModel edgeModel = new " + modelName + "EdgeModel();\n");

		genPackages();
		genEnumAttributeTypes();
		genValidates();
		genIndexDescriptions();
		genIndicesGraphBinding(inPureGraphModel);
		sb.append("\n");
		
		String override = inPureGraphModel ? "override " : "";

		sb.append("\t\tpublic " + override + "string ModelName { get { return \"" + modelName + "\"; } }\n");
		
		sb.append("\t\tpublic " + override + "GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }\n");
		sb.append("\t\tpublic " + override + "GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }\n");
		
		sb.append("\t\tpublic " + override + "IEnumerable<string> Packages "
				+ "{ get { return packages; } }\n");
		sb.append("\t\tpublic " + override + "IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes "
				+ "{ get { return enumAttributeTypes; } }\n");
		sb.append("\t\tpublic " + override + "IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo "
				+ "{ get { return validateInfos; } }\n");
		sb.append("\t\tpublic " + override + "IEnumerable<GRGEN_LIBGR.IndexDescription> IndexDescriptions "
				+ "{ get { return indexDescriptions; } }\n");
		sb.append("\t\tpublic static GRGEN_LIBGR.IndexDescription GetIndexDescription(int i) "
				+ "{ return indexDescriptions[i]; }\n");
		sb.append("\t\tpublic static GRGEN_LIBGR.IndexDescription GetIndexDescription(string indexName)\n ");
		sb.append("\t\t{\n");
		sb.append("\t\t\tfor(int i=0; i<indexDescriptions.Length; ++i)\n");
		sb.append("\t\t\t\tif(indexDescriptions[i].Name==indexName)\n");
		sb.append("\t\t\t\t\treturn indexDescriptions[i];\n");
		sb.append("\t\t\treturn null;\n");
		sb.append("\t\t}\n");
		sb.append("\t\tpublic " + override + "bool GraphElementUniquenessIsEnsured { get { return " + (model.isUniqueDefined() ? "true" : "false") + "; } }\n");
		sb.append("\t\tpublic " + override + "bool GraphElementsAreAccessibleByUniqueId { get { return " + (model.isUniqueIndexDefined() ? "true" : "false") + "; } }\n");
		sb.append("\n");
        
		if(model.isEmitClassDefined()) {
			sb.append("\t\tpublic " + override + "object Parse(TextReader reader, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
			sb.append("\t\t{\n");
			sb.append("\t\t\treturn AttributeTypeObjectEmitterParser.Parse(reader, attrType, graph);\n");
			sb.append("\t\t}\n");
			sb.append("\t\tpublic " + override + "string Serialize(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
			sb.append("\t\t{\n");
			sb.append("\t\t\treturn AttributeTypeObjectEmitterParser.Serialize(attribute, attrType, graph);\n");
			sb.append("\t\t}\n");
			sb.append("\t\tpublic " + override + "string Emit(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
			sb.append("\t\t{\n");
			sb.append("\t\t\treturn AttributeTypeObjectEmitterParser.Emit(attribute, attrType, graph);\n");
			sb.append("\t\t}\n\n");
		} else {
			if(!inPureGraphModel) { // the functions are inherited from LGSPGraphModel, which is not available in the graph'n'graph-model-in-one classes (because of the lack of multiple inheritance)
				sb.append("\t\tpublic object Parse(TextReader reader, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
				sb.append("\t\t{\n");
				sb.append("\t\t\treader.Read(); reader.Read(); reader.Read(); reader.Read(); // eat 'n' 'u' 'l' 'l'\n");
				sb.append("\t\t\treturn null;\n");
				sb.append("\t\t}\n");
				sb.append("\t\tpublic string Serialize(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
				sb.append("\t\t{\n");
				sb.append("\t\t\tConsole.WriteLine(\"Warning: Exporting attribute of object type to null\");\n");
				sb.append("\t\t\treturn \"null\";\n");
				sb.append("\t\t}\n");
				sb.append("\t\tpublic string Emit(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
				sb.append("\t\t{\n");
				sb.append("\t\t\treturn attribute!=null ? attribute.ToString() : \"null\";\n");
				sb.append("\t\t}\n\n");
			}
		}
		
		genExternalTypes();
		sb.append("\t\tpublic " + override + "GRGEN_LIBGR.ExternalType[] ExternalTypes { get { return externalTypes; } }\n\n");

		sb.append("\t\tprivate void FullyInitializeExternalTypes()\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\texternalType_object.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { } );\n");
		for(ExternalType et : model.getExternalTypes()) {
			sb.append("\t\t\texternalType_" + et.getIdent() + ".InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { ");
			boolean directSupertypeAvailable = false;
			for(InheritanceType superType : et.getDirectSuperTypes()) {
				sb.append("externalType_" + superType.getIdent() + ", ");
				directSupertypeAvailable = true;
			}
			if(!directSupertypeAvailable)
				sb.append("externalType_object ");
			sb.append("} );\n");
		}
		sb.append("\t\t}\n\n");

		if(model.isEqualClassDefined() && model.isLowerClassDefined()) {
			sb.append("\t\tpublic " + override + "bool IsEqualClassDefined { get { return true; } }\n");
			sb.append("\t\tpublic " + override + "bool IsLowerClassDefined { get { return true; } }\n");
			sb.append("\t\tpublic " + override + "bool IsEqual(object this_, object that)\n");
			sb.append("\t\t{\n");
			sb.append("\t\t\treturn AttributeTypeObjectCopierComparer.IsEqual(this_, that);\n");
			sb.append("\t\t}\n");
			sb.append("\t\tpublic " + override + "bool IsLower(object this_, object that)\n");
			sb.append("\t\t{\n");
			sb.append("\t\t\treturn AttributeTypeObjectCopierComparer.IsLower(this_, that);\n");
			sb.append("\t\t}\n\n");
		} else if(model.isEqualClassDefined()) {
			sb.append("\t\tpublic " + override + "bool IsEqualClassDefined { get { return true; } }\n");
			sb.append("\t\tpublic " + override + "bool IsEqual(object this_, object that)\n");
			sb.append("\t\t{\n");
			sb.append("\t\t\treturn AttributeTypeObjectCopierComparer.IsEqual(this_, that);\n");
			sb.append("\t\t}\n");
			if(!inPureGraphModel) {
				sb.append("\t\tpublic bool IsLowerClassDefined { get { return false; } }\n");
				sb.append("\t\tpublic bool IsLower(object this_, object that)\n");
				sb.append("\t\t{\n");
				sb.append("\t\t\treturn this_ == that;\n");
				sb.append("\t\t}\n\n");
			}
		} else {
			if(!inPureGraphModel) {
				sb.append("\t\tpublic bool IsEqualClassDefined { get { return false; } }\n");
				sb.append("\t\tpublic bool IsLowerClassDefined { get { return false; } }\n");
				sb.append("\t\tpublic bool IsEqual(object this_, object that)\n");
				sb.append("\t\t{\n");
				sb.append("\t\t\treturn this_ == that;\n");
				sb.append("\t\t}\n");
				sb.append("\t\tpublic bool IsLower(object this_, object that)\n");
				sb.append("\t\t{\n");
				sb.append("\t\t\treturn this_ == that;\n");
				sb.append("\t\t}\n\n");
			}
		}
		
		sb.append("\t\tpublic " + override + "string MD5Hash { get { return \"" + be.unit.getTypeDigest() + "\"; } }\n");
	}

	private void genPackages() {
		sb.append("\t\tprivate string[] packages = {\n");
		for(PackageType pt : model.getPackages()) {
			sb.append("\t\t\t\"" + pt.getIdent() + "\",\n");
		}
		sb.append("\t\t};\n");
	}

	private void genValidates() {
		sb.append("\t\tprivate GRGEN_LIBGR.ValidateInfo[] validateInfos = {\n");

		for(EdgeType edgeType : model.getEdgeTypes()) {
			genValidate(edgeType);
		}
		
		for(PackageType pt : model.getPackages()) {
			for(EdgeType edgeType : pt.getEdgeTypes()) {
				genValidate(edgeType);
			}
		}

		sb.append("\t\t};\n");
	}

	private void genValidate(EdgeType edgeType) {
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

	private void genIndexDescriptions() {
		sb.append("\t\tprivate static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {\n");

		for(Index index : model.getIndices()) {
			if(index instanceof AttributeIndex)
				genIndexDescription((AttributeIndex)index);
			else
				genIndexDescription((IncidenceIndex)index);
		}
		
		/*for(PackageType pt : model.getPackages()) {
			for(AttributeIndex index : pt.getIndices()) {
				genIndexDescription(index);
			}
		}*/

		sb.append("\t\t};\n");
	}

	private void genIndexDescription(AttributeIndex index) {
		sb.append("\t\t\tnew GRGEN_LIBGR.AttributeIndexDescription(");
		sb.append("\"" + index.getIdent() + "\", ");
		sb.append(formatTypeClassName(index.type) + ".typeVar, ");
		sb.append(formatTypeClassName(index.type) + "." + formatAttributeTypeName(index.entity));
		sb.append("),\n");
	}

	private void genIndexDescription(IncidenceIndex index) {
		sb.append("\t\t\tnew GRGEN_LIBGR.IncidenceIndexDescription(");
		sb.append("\"" + index.getIdent() + "\", ");
		switch(index.Direction()) {
		case IncidentEdgeExpr.OUTGOING:
			sb.append("GRGEN_LIBGR.IncidenceDirection.OUTGOING, ");
			break;
		case IncidentEdgeExpr.INCOMING:
			sb.append("GRGEN_LIBGR.IncidenceDirection.INCOMING, ");
			break;
		case IncidentEdgeExpr.INCIDENT:
			sb.append("GRGEN_LIBGR.IncidenceDirection.INCIDENT, ");
			break;
		}
		sb.append(formatTypeClassRefInstance(((IncidenceIndex)index).getStartNodeType()) + ", ");
		sb.append(formatTypeClassRefInstance(((IncidenceIndex)index).getIncidentEdgeType()) + ", ");
		sb.append(formatTypeClassRefInstance(((IncidenceIndex)index).getAdjacentNodeType()));
		sb.append("),\n");
	}

	private void genIndicesGraphBinding(boolean inPureGraphModel) {
		String override = inPureGraphModel ? "override " : "";
		sb.append("\t\tpublic " + override + "void CreateAndBindIndexSet(GRGEN_LGSP.LGSPGraph graph) {\n");
		if(model.isUniqueIndexDefined())
			sb.append("\t\t\tnew GRGEN_LGSP.LGSPUniquenessIndex(graph); // must be called before the indices so that its event handler is registered first, doing the unique id computation the indices depend upon\n");
		else if(model.isUniqueDefined())
			sb.append("\t\t\tnew GRGEN_LGSP.LGSPUniquenessEnsurer(graph); // must be called before the indices so that its event handler is registered first, doing the unique id computation the indices depend upon\n");
		sb.append("\t\t\tgraph.indices = new " + model.getIdent() + "IndexSet(graph);\n");
		sb.append("\t\t}\n");
		
		sb.append("\t\tpublic " + override + "void FillIndexSetAsClone(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {\n");
		if(model.isUniqueDefined())
			sb.append("\t\t\tgraph.uniquenessEnsurer.FillAsClone(originalGraph, oldToNewMap);\n");
		sb.append("\t\t\t((" + model.getIdent() + "IndexSet)graph.indices).FillAsClone(originalGraph, oldToNewMap);\n");
		sb.append("\t\t}\n");
	}
	
	private void genEnumAttributeTypes() {
		sb.append("\t\tprivate GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {\n");
		for(EnumType enumt : model.getEnumTypes()) {
			genEnumAttributeType(enumt);
		}
		for(PackageType pt : model.getPackages()) {
			for(EnumType enumt : pt.getEnumTypes()) {
				genEnumAttributeType(enumt);
			}
		}
		sb.append("\t\t};\n");
	}

	private void genEnumAttributeType(EnumType enumt) {
		sb.append("\t\t\tGRGEN_MODEL." + getPackagePrefixDot(enumt) + "Enums.@" + formatIdentifiable(enumt) + ",\n");
	}

	private void genExternalTypes() {
		sb.append("\t\tpublic static GRGEN_LIBGR.ExternalType externalType_object = new GRGEN_LIBGR.ExternalType(");
		sb.append("\"object\", ");
		sb.append("typeof(object)");
		sb.append(");\n");
		for(ExternalType et : model.getExternalTypes()) {
			sb.append("\t\tpublic static GRGEN_LIBGR.ExternalType externalType_" + et.getIdent() + " = new GRGEN_LIBGR.ExternalType(");
			sb.append("\"" + et.getIdent() + "\", ");
			sb.append("typeof(" + et.getIdent() + ")");
			sb.append(");\n");
		}

		sb.append("\t\tprivate GRGEN_LIBGR.ExternalType[] externalTypes = { ");
		sb.append("externalType_object");
		for(ExternalType et : model.getExternalTypes()) {
			sb.append(", externalType_" + et.getIdent());
		}
		sb.append(" };\n");
	}

	///////////////////////////////
	// External stuff generation //
	///////////////////////////////

	private void genExternalClasses() {
		for(ExternalType et : model.getExternalTypes()) {
			sb.append("\tpublic partial class " + et.getIdent());
			boolean first = true;
			for(InheritanceType superType : et.getDirectSuperTypes()) {
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
			sb.append("\n");
		}
	}

	private void genEmitterParserClass() {
		sb.append("\tpublic partial class AttributeTypeObjectEmitterParser");
		sb.append("\n");
		sb.append("\t{\n");
		sb.append("\t\t// You must implement this class in the same partial class in ./" + model.getIdent() + "ModelExternalFunctionsImpl.cs:\n");
		sb.append("\t\t// You must implement the functions called by the following functions inside that class (same name plus suffix Impl):\n");
		sb.append("\n");
		sb.append("\t\t// Called during .grs import, at exactly the position in the text reader where the attribute begins.\n");
		sb.append("\t\t// For attribute type object or a user defined type, which is treated as object.\n");
		sb.append("\t\t// The implementation must parse from there on the attribute type requested.\n");
		sb.append("\t\t// It must not parse beyond the serialized representation of the attribute, \n");
		sb.append("\t\t// i.e. Peek() must return the first character not belonging to the attribute type any more.\n");
		sb.append("\t\t// Returns the parsed object.\n");
		sb.append("\t\tpublic static object Parse(TextReader reader, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\treturn ParseImpl(reader, attrType, graph);\n");
		sb.append("\t\t\t//reader.Read(); reader.Read(); reader.Read(); reader.Read(); // eat 'n' 'u' 'l' 'l' // default implementation\n");
		sb.append("\t\t\t//return null; // default implementation\n");
		sb.append("\t\t}\n");
		sb.append("\n");
		sb.append("\t\t// Called during .grs export, the implementation must return a string representation for the attribute.\n");
		sb.append("\t\t// For attribute type object or a user defined type, which is treated as object.\n");
		sb.append("\t\t// The serialized string must be parseable by Parse.\n");
		sb.append("\t\tpublic static string Serialize(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\treturn SerializeImpl(attribute, attrType, graph);\n");
		sb.append("\t\t\t//Console.WriteLine(\"Warning: Exporting attribute of object type to null\"); // default implementation\n");
		sb.append("\t\t\t//return \"null\"; // default implementation\n");
		sb.append("\t\t}\n");
		sb.append("\n");
		sb.append("\t\t// Called during debugging or emit writing, the implementation must return a string representation for the attribute.\n");
		sb.append("\t\t// For attribute type object or a user defined type, which is treated as object.\n");
		sb.append("\t\t// The attribute type may be null.\n");
		sb.append("\t\t// The string is meant for consumption by humans, it does not need to be parseable.\n");
		sb.append("\t\tpublic static string Emit(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
		sb.append("\t\t{\n");
		sb.append("\t\t\treturn EmitImpl(attribute, attrType, graph);\n");
		sb.append("\t\t\t//return \"null\"; // default implementation\n");
		sb.append("\t\t}\n");
		sb.append("\t}\n");
		sb.append("\n");
	}

	private void genCopierComparerClass() {
		sb.append("\tpublic partial class AttributeTypeObjectCopierComparer");
		sb.append("\n");
		sb.append("\t{\n");
		sb.append("\t\t// You must implement the following functions in the same partial class in ./" + model.getIdent() + "ModelExternalFunctionsImpl.cs:\n");
		sb.append("\n");
		if(model.isCopyClassDefined()) {
			sb.append("\t\t// Called when a graph element is cloned/copied.\n");
			sb.append("\t\t// For attribute type object.\n");
			sb.append("\t\t// If \"copy class\" is not specified, objects are copied by copying the reference, i.e. they are identical afterwards.\n");
			sb.append("\t\t// All other attribute types are copied by-value (so changing one later on has no effect on the other).\n");
			sb.append("\t\t//public static object Copy(object);\n");
			sb.append("\n");
		}
		if(model.isEqualClassDefined()) {
			sb.append("\t\t// Called during comparison of graph elements from graph isomorphy comparison, or attribute comparison.\n");
			sb.append("\t\t// For attribute type object.\n");
			sb.append("\t\t// If \"== class\" is not specified, objects are equal if they are identical,\n");
			sb.append("\t\t// i.e. by-reference-equality (same pointer); all other attribute types are compared by-value.\n");
			sb.append("\t\t//public static bool IsEqual(object, object);\n");
			sb.append("\n");
		}
		if(model.isLowerClassDefined()) {
			sb.append("\t\t// Called during attribute comparison.\n");
			sb.append("\t\t// For attribute type object.\n");
			sb.append("\t\t// If \"< class\" is not specified, objects can't be compared for ordering, only for equality.\n");
			sb.append("\t\t//public static bool IsLower(object, object);\n");
			sb.append("\n");
		}
		if(model.getExternalTypes().size() > 0) {
			sb.append("\n");
			sb.append("\t\t// The same functions, just for each user defined type.\n");
			sb.append("\t\t// Those are normally treated as object (if no \"copy class or == class or < class\" is specified),\n");
			sb.append("\t\t// i.e. equal if identical references, no ordered comparisons available, and copy just copies the reference (making them identical).\n");
			sb.append("\t\t// Here you can overwrite the default reference semantics with value semantics, fitting better to the other attribute types.\n");
			for(ExternalType et : model.getExternalTypes()) {
				String typeName = et.getIdent().toString();
				sb.append("\n");
				if(model.isCopyClassDefined())
					sb.append("\t\t//public static " + typeName + " Copy(" + typeName + ");\n");
				if(model.isEqualClassDefined())
					sb.append("\t\t//public static bool IsEqual(" + typeName + ", " + typeName + ");\n");
				if(model.isLowerClassDefined())
					sb.append("\t\t//public static bool IsLower(" + typeName + ", " + typeName + ");\n");
			}
		}
		sb.append("\t}\n");
		sb.append("\n");
	}

	private void genExternalFunctionHeaders() {
		for(ExternalFunction ef : model.getExternalFunctions()) {
			Type returnType = ef.getReturnType();
			sb.append("\t\t//public static " + formatType(returnType) + " " + ef.getName() + "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph");
			for(Type paramType : ef.getParameterTypes()) {
				sb.append(", ");
				sb.append(formatType(paramType));
			}
			sb.append(");\n");

			if(be.unit.isToBeParallelizedActionExisting())
			{
				sb.append("\t\t//public static " + formatType(returnType) + " " + ef.getName() + "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph");
				for(Type paramType : ef.getParameterTypes()) {
					sb.append(", ");
					sb.append(formatType(paramType));
				}
				sb.append(", int threadId");
				sb.append(");\n");
			}
		}
	}

	private void genExternalProcedureHeaders() {
		for(ExternalProcedure ep : model.getExternalProcedures()) {
			sb.append("\t\t//public static void " + ep.getName() + "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph");
			for(Type paramType : ep.getParameterTypes()) {
				sb.append(", ");
				sb.append(formatType(paramType));
			}
			for(Type retType : ep.getReturnTypes()) {
				sb.append(", ");
				sb.append("out ");
				sb.append(formatType(retType));
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
	private ModifyGen mgFuncComp;
}

