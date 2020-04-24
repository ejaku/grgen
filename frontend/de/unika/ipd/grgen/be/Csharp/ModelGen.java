/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import java.util.HashMap;
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
import de.unika.ipd.grgen.util.SourceBuilder;
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
		sb = new SourceBuilder();
		stubsb = null;

		String filename = model.getIdent() + "Model.cs";

		System.out.println("  generating the " + filename + " file...");

		sb.appendFront("// This file has been generated automatically by GrGen (www.grgen.net)\n"
				+ "// Do not modify this file! Any changes will be lost!\n"
				+ "// Generated from \"" + be.unit.getFilename() + "\" on " + new Date() + "\n"
				+ "\n"
				+ "using System;\n"
				+ "using System.Collections.Generic;\n"
				+ "using System.IO;\n"
				+ "using System.Diagnostics;\n"
				+ "using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n"
				+ "using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n"
				+ "using GRGEN_EXPR = de.unika.ipd.grGen.expression;\n"
				+ "using GRGEN_MODEL = de.unika.ipd.grGen.Model_" + model.getIdent() + ";\n"
				+ "\n"
				+ "namespace de.unika.ipd.grGen.Model_" + model.getIdent() + "\n"
				+ "{\n");
		sb.indent();

		for(PackageType pt : model.getPackages()) {
			System.out.println("    generating package " + pt.getIdent() + "...");
	
			sb.append("\n");
			sb.appendFront("//-----------------------------------------------------------\n");
			sb.appendFront("namespace ");
			sb.append(formatIdentifiable(pt));
			sb.append("\n");
			sb.appendFront("//-----------------------------------------------------------\n");
			sb.appendFront("{\n");
			sb.indent();
	
			genBearer(model.getAllNodeTypes(), model.getAllEdgeTypes(),
					pt, pt.getIdent().toString());
	
			sb.append("\n");
			sb.appendFront("//-----------------------------------------------------------\n");
			sb.unindent();
			sb.appendFront("}\n");
			sb.appendFront("//-----------------------------------------------------------\n");
		}

		genBearer(model.getAllNodeTypes(), model.getAllEdgeTypes(),
				model, null);
		
		genExternalTypeObject();
		for(ExternalType et : model.getExternalTypes()) {
			genExternalType(et);
		}

		System.out.println("    generating indices...");

		sb.append("\n");
		sb.appendFront("//\n");
		sb.appendFront("// Indices\n");
		sb.appendFront("//\n");
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
		genGraphClass();
		sb.append("\n");
		genNamedGraphClass();

		sb.unindent();
		sb.appendFront("}\n");

		System.out.println("    writing to " + be.path + " / " + filename);
		writeFile(be.path, filename, sb.toString());

		if(stubsb != null) {
			String stubFilename = model.getIdent() + "ModelStub.cs";
			System.out.println("  writing the " + stubFilename + " stub file...");
			writeFile(be.path, stubFilename, stubsb.toString());
		}

		
		///////////////////////////////////////////////////////////////////////////////////////////
		// generate the external functions and types stub file
		// only if there are external functions or external procedures or external types required 
		// or the emit class is to be generated or the copy class is to be generated
		if(model.getExternalTypes().isEmpty() 
				&& model.getExternalFunctions().isEmpty()
				&& model.getExternalProcedures().isEmpty()
				&& !model.isEmitClassDefined()
				&& !model.isEmitGraphClassDefined()
				&& !model.isCopyClassDefined()
				&& !model.isEqualClassDefined()
				&& !model.isLowerClassDefined())
			return;

		filename = model.getIdent() + "ModelExternalFunctions.cs";

		System.out.println("  generating the " + filename + " file...");

		sb = new SourceBuilder();

		sb.appendFront("// This file has been generated automatically by GrGen (www.grgen.net)\n"
				+ "// Do not modify this file! Any changes will be lost!\n"
				+ "// Generated from \"" + be.unit.getFilename() + "\" on " + new Date() + "\n"
				+ "\n"
				+ "using System;\n"
				+ "using System.Collections.Generic;\n"
				+ "using System.IO;\n"
                + "using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n"
                + "using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n"
				+ "using GRGEN_MODEL = de.unika.ipd.grGen.Model_" + model.getIdent() + ";\n");

		if(!model.getExternalTypes().isEmpty() || model.isEmitClassDefined() || model.isEmitGraphClassDefined())
		{
			sb.append("\n");
			sb.appendFront("namespace de.unika.ipd.grGen.Model_" + model.getIdent() + "\n"
					+ "{\n");
			sb.indent();

			genExternalClasses();

			if(model.isEmitClassDefined() || model.isEmitGraphClassDefined())
				genEmitterParserClass();

			if(model.isCopyClassDefined() || model.isEqualClassDefined() || model.isLowerClassDefined())
				genCopierComparerClass();

			sb.unindent();
			sb.appendFront("}\n");
		}

		if(!model.getExternalFunctions().isEmpty())
		{
			sb.append("\n");
			sb.appendFront("namespace de.unika.ipd.grGen.expression\n");
			sb.appendFront("{\n");
			sb.indent();

			sb.appendFront("public partial class ExternalFunctions\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("// You must implement the following functions in the same partial class in ./" + model.getIdent() + "ModelExternalFunctionsImpl.cs:\n");
			sb.append("\n");

			genExternalFunctionHeaders();

			sb.appendFront("}\n");

			sb.unindent();
			sb.appendFront("}\n");
		}

		if(!model.getExternalProcedures().isEmpty())
		{
			sb.append("\n");
			sb.appendFront("namespace de.unika.ipd.grGen.expression\n");
			sb.appendFront("{\n");
			sb.indent();

			sb.appendFront("public partial class ExternalProcedures\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("// You must implement the following procedures in the same partial class in ./" + model.getIdent() + "ModelExternalFunctionsImpl.cs:\n");
			sb.append("\n");

			genExternalProcedureHeaders();

			sb.unindent();
			sb.appendFront("}\n");

			sb.unindent();
			sb.appendFront("}\n");
		}

		System.out.println("    writing to " + be.path + " / " + filename);
		writeFile(be.path, filename, sb.toString());

		if(be.path.compareTo(new File("."))==0) {
			System.out.println("    no copy needed for " + be.path + " / " + filename);
		} else {
			System.out.println("    copying " + be.path + " / " + filename + " to " + be.path.getAbsoluteFile().getParent() + " / " + filename);
			copyFile(new File(be.path, filename), new File(be.path.getAbsoluteFile().getParent(), filename));
		}
	}

	private SourceBuilder getStubBuffer() {
		if(stubsb == null) {
			stubsb = new SourceBuilder();
			stubsb.appendFront("// This file has been generated automatically by GrGen (www.grgen.net)\n"
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
		sb.appendFront("//\n");
		sb.appendFront("// Enums\n");
		sb.appendFront("//\n");
		sb.append("\n");

		for(EnumType enumt : bearer.getEnumTypes()) {
			sb.appendFront("public enum ENUM_" + formatIdentifiable(enumt) + " { ");
			for(EnumItem enumi : enumt.getItems()) {
				sb.append("@" + formatIdentifiable(enumi) + " = " + enumi.getValue().getValue() + ", ");
			}
			sb.append("};\n\n");
		}

		sb.appendFront("public class Enums\n");
		sb.appendFront("{\n");
		sb.indent();
		for(EnumType enumt : bearer.getEnumTypes()) {
			sb.appendFront("public static GRGEN_LIBGR.EnumAttributeType @" + formatIdentifiable(enumt)
					+ " = new GRGEN_LIBGR.EnumAttributeType(\"" + formatIdentifiable(enumt) + "\", "
					+ (!getPackagePrefix(enumt).equals("") ? "\""+getPackagePrefix(enumt)+"\"" : "null") + ", "
					+ "\"" + getPackagePrefixDoubleColon(enumt) + formatIdentifiable(enumt) + "\", "
					+ "typeof(GRGEN_MODEL." + getPackagePrefixDot(enumt) + "ENUM_" + formatIdentifiable(enumt) + "), "
					+ "new GRGEN_LIBGR.EnumMember[] {\n");
			sb.indent();
			for(EnumItem enumi : enumt.getItems()) {
				sb.appendFront("new GRGEN_LIBGR.EnumMember(" + enumi.getValue().getValue()
						+ ", \"" + formatIdentifiable(enumi) + "\"),\n");
			}
			sb.unindent();
			sb.appendFront("});\n");
		}
		sb.unindent();
		sb.appendFront("}\n");
	}

	/**
	 * Generates code for all given element types.
	 */
	private void genTypes(Collection<? extends InheritanceType> allTypes, 
			NodeEdgeEnumBearer bearer, String packageName, boolean isNode) {
		Collection<? extends InheritanceType> curTypes = 
			isNode ? bearer.getNodeTypes() : bearer.getEdgeTypes();
		
		sb.appendFront("//\n");
		sb.appendFront("// " + formatNodeOrEdge(isNode) + " types\n");
		sb.appendFront("//\n");
		sb.append("\n");
		sb.appendFront("public enum " + formatNodeOrEdge(isNode) + "Types ");

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
		sb.appendFront("// *** " + formatNodeOrEdge(type) + " " + formatIdentifiable(type) + " ***\n");
		sb.append("\n");

		if(!rootTypes.contains(type.getIdent().toString()))
			genElementInterface(type);
		if(!type.isAbstract())
			genElementImplementation(type);
		genTypeImplementation(allTypes, type, packageName);
		genAttributeArrayHelpersAndComparers(type);
	}

	//////////////////////////////////
	// Element interface generation //
	//////////////////////////////////

	/**
	 * Generates the element interface for the given type
	 */
	private void genElementInterface(InheritanceType type) {
		String iname = "I" + getNodeOrEdgeTypePrefix(type) + formatIdentifiable(type);
		sb.appendFront("public interface " + iname + " : ");
		genDirectSuperTypeList(type);
		sb.append("\n");
		sb.appendFront("{\n");
		sb.indent();
		for(Entity e : type.getMembers()) {
			genAttributeAccess(type, e, "");
		}
		genMethodInterfaces(type, type.getFunctionMethods(), type.getProcedureMethods(), "");
		sb.unindent();
		sb.appendFront("}\n");
	}

	/**
	 * Generate a list of direct supertypes of the given type.
	 */
	private void genDirectSuperTypeList(InheritanceType type) {
		String iprefix = "I" + getNodeOrEdgeTypePrefix(type);
		Collection<InheritanceType> directSuperTypes = type.getDirectSuperTypes();

		boolean first = true;
		for(Iterator<InheritanceType> i = directSuperTypes.iterator(); i.hasNext(); ) {
			InheritanceType superType = i.next();
			if(rootTypes.contains(superType.getIdent().toString())) {
				if(first) first = false;
				else sb.append(", ");
				sb.append(getRootElementInterfaceRef(superType));
			}
			else {
				if(first) first = false;
				else sb.append(", ");
				sb.append(getPackagePrefixDot(superType) + iprefix + formatIdentifiable(superType));
			}
		}
	}

	/**
	 * Generate the attribute accessor declarations of the given member.
	 * @param type The type for which the accessors are to be generated.
	 * @param member The member entity.
	 * @param modifiers A string which may contain modifiers to be applied to the accessor.
	 * 		It must either end with a space or be empty.
	 */
	private void genAttributeAccess(InheritanceType type, Entity member, String modifiers) {
		sb.appendFront(modifiers);
		if(type.getOverriddenMember(member) != null)
			sb.append("new ");
		if(member.isConst()) {
			sb.append(formatAttributeType(member) + " @" + formatIdentifiable(member) + " { get; }\n");
		} else {
			sb.append(formatAttributeType(member) + " @" + formatIdentifiable(member) + " { get; set; }\n");
		}
	}

	private void genMethodInterfaces(InheritanceType type, Collection<FunctionMethod> functionMethods,
			Collection<ProcedureMethod> procedureMethods, String modifiers) {
		// METHOD-TODO - inheritance?
		for(FunctionMethod fm : functionMethods) {
			sb.appendFront(formatType(fm.getReturnType()) + " ");
			sb.append(fm.getIdent().toString() + "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph");
			for(Entity inParam : fm.getParameters()) {
				sb.append(", ");
				sb.append(formatType(inParam.getType()));
				sb.append(" ");
				sb.append(formatEntity(inParam));
			}
			sb.append(");\n");
			
			if(model.areFunctionsParallel()) {
				sb.appendFront(formatType(fm.getReturnType()) + " ");
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
			sb.appendFront("void ");
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
		SourceBuilder routedSB = sb;
		String routedClassName = elemname;
		String routedDeclName = elemref;

		if(extName == null) {
			sb.append("\n");
			sb.appendFront("public sealed partial class " + elemname + " : GRGEN_LGSP.LGSP"
					+ kindStr + ", " + ielemref + "\n");
			sb.appendFront("{\n");
			sb.indent();
		}
		else { // what's that? = for "Embedding the graph rewrite system GrGen.NET into C#" (see corresponding master thesis, mono c# compiler extension)
			routedSB = getStubBuffer();
			int lastDot = extName.lastIndexOf('.');
			String extClassName;
			if(lastDot != -1) {
				namespace = extName.substring(0, lastDot);
				extClassName = extName.substring(lastDot + 1);
				stubsb.append("\n");
				stubsb.appendFront("namespace " + namespace + "\n");
				stubsb.appendFront("{\n");
				stubsb.indent();
			}
			else
				extClassName = extName;
			routedClassName = extClassName;
			routedDeclName = extClassName;

			stubsb.appendFront("public class " + extClassName + " : " + elemref + "\n");
			stubsb.appendFront("{\n");
			stubsb.indent();
			stubsb.appendFront("public " + extClassName + "() : base() { }\n");
			stubsb.append("\n");

			sb.append("\n");
			sb.appendFront("\tpublic abstract class " + elemname + " : GRGEN_LGSP.LGSP"
					+ kindStr + ", " + ielemref + "\n");
			sb.appendFront("{\n");
			sb.indent();
		}
		sb.appendFront("private static int poolLevel = 0;\n");
		sb.appendFront("private static " + elemref + "[] pool = new " + elemref + "[10];\n");

		// Static initialization for constants = static members
		initAllMembersConst(type, elemname, "this");
		sb.append("\n");

		genElementConstructor(type, elemname, typeref);
		sb.append("\n");

		genElementStaticTypeGetter(typeref);
		sb.append("\n");
		
		genElementCloneMethod(type, routedSB, routedDeclName);
		routedSB.append("\n");
		genElementCopyConstructor(type, extName, typeref, routedSB, routedClassName, routedDeclName);
		routedSB.append("\n");

		genElementAttributeComparisonMethod(type, routedSB, routedClassName);
		routedSB.append("\n");

		genElementCreateMethods(type, isNode, elemref, allocName);
		sb.append("\n");
		
		genElementRecycleMethod();
		sb.append("\n");

		genAttributesAndAttributeAccessImpl(type);

		genMethods(type);

		sb.unindent();
		sb.appendFront("}\n");

		if(extName != null) {
			stubsb.unindent();
			stubsb.appendFront("}\n");		// close class stub
			if(namespace != null) {
				stubsb.unindent();
				stubsb.appendFront("}\n");				// close namespace
			}
		}
	}

	private void genElementConstructor(InheritanceType type, String elemname, String typeref) {
		boolean isNode = type instanceof NodeType;
		if(isNode) {
			sb.appendFront("public " + elemname + "() : base("+ typeref + ".typeVar)\n");
			sb.appendFront("{\n");
			sb.indent();
			initAllMembersNonConst(type, "this", false, false);
			sb.unindent();
			sb.appendFront("}\n");
		}
		else {
			sb.appendFront("public " + elemname + "(GRGEN_LGSP.LGSPNode source, "
						+ "GRGEN_LGSP.LGSPNode target)\n");
			sb.appendFront("\t: base("+ typeref + ".typeVar, source, target)\n");
			sb.appendFront("{\n");
			sb.indent();
			initAllMembersNonConst(type, "this", false, false);
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genElementStaticTypeGetter(String typeref) {
		sb.appendFront("public static " + typeref + " TypeInstance { get { return " + typeref + ".typeVar; } }\n");
	}

	private void genElementCloneMethod(InheritanceType type, SourceBuilder routedSB, String routedDeclName) {
		boolean isNode = type instanceof NodeType;
		if(isNode) {
			routedSB.appendFront("public override GRGEN_LIBGR.INode Clone() {\n");
			routedSB.appendFront("\treturn new " + routedDeclName + "(this);\n");
			routedSB.appendFront("}\n");
		} else {
			routedSB.appendFront("public override GRGEN_LIBGR.IEdge Clone("
						+ "GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget) {\n");
			routedSB.appendFront("\treturn new " + routedDeclName + "(this, (GRGEN_LGSP.LGSPNode) newSource, "
						+ "(GRGEN_LGSP.LGSPNode) newTarget);\n");
			routedSB.appendFront("}\n");
		}
	}

	private void genElementCopyConstructor(InheritanceType type, String extName, String typeref,
			SourceBuilder routedSB, String routedClassName, String routedDeclName) {
		boolean isNode = type instanceof NodeType;
		if(isNode) {
			routedSB.appendFront("private " + routedClassName + "(" + routedDeclName + " oldElem) : base("
					+ (extName == null ? typeref + ".typeVar" : "") + ")\n");
		} else {
			routedSB.appendFront("private " + routedClassName + "(" + routedDeclName
						+ " oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)\n");
			routedSB.appendFront("\t: base("
					+ (extName == null ? typeref + ".typeVar, " : "") + "newSource, newTarget)\n");
		}
		routedSB.appendFront("{\n");
		routedSB.indent();
		if(model.isUniqueDefined())
			routedSB.appendFront("uniqueId = oldElem.uniqueId;\n");
		for(Entity member : type.getAllMembers()) {
			if(member.isConst())
				continue;

			String attrName = formatIdentifiable(member);
			if(member.getType() instanceof MapType || member.getType() instanceof SetType 
					|| member.getType() instanceof ArrayType || member.getType() instanceof DequeType) {
				routedSB.appendFront(attrName + ModelGen.ATTR_IMPL_SUFFIX + " = new " + formatAttributeType(member.getType())
						+ "(oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ");\n");
			} else if(model.isCopyClassDefined()
					&& (member.getType().classify() == Type.IS_EXTERNAL_TYPE
							|| member.getType().classify() == Type.IS_OBJECT)) {
				routedSB.appendFront("AttributeTypeObjectCopierComparer.Copy(" + "oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ");\n");
			} else {
				routedSB.appendFront(attrName + ModelGen.ATTR_IMPL_SUFFIX + " = oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ";\n");
			}
		}
		routedSB.unindent();
		routedSB.appendFront("}\n");
	}

	private void genElementAttributeComparisonMethod(InheritanceType type, SourceBuilder routedSB,
			String routedClassName) {
		routedSB.appendFront("public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {\n");
		routedSB.indent();
		routedSB.appendFront("if(!(that is "+routedClassName+")) return false;\n");
		routedSB.appendFront(routedClassName+" that_ = ("+routedClassName+")that;\n");
		routedSB.appendFront("return true\n");
		routedSB.indent();
		for(Entity member : type.getAllMembers()) {
			if(member.isConst())
				continue;

			String attrName = formatIdentifiable(member);
			if(member.getType() instanceof MapType || member.getType() instanceof SetType 
					|| member.getType() instanceof ArrayType || member.getType() instanceof DequeType) {
				routedSB.appendFront("&& GRGEN_LIBGR.ContainerHelper.Equal(" + attrName + ModelGen.ATTR_IMPL_SUFFIX + ", "
						+ "that_." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ")\n");
			} else if(model.isEqualClassDefined()
					&& (member.getType().classify() == Type.IS_EXTERNAL_TYPE
							|| member.getType().classify() == Type.IS_OBJECT)) {
				routedSB.appendFront("&& AttributeTypeObjectCopierComparer.IsEqual(" + attrName + ModelGen.ATTR_IMPL_SUFFIX + ", "
						+ "that_." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ")\n");
			} else if(member.getType().classify() == Type.IS_GRAPH) {
				routedSB.appendFront("&& GRGEN_LIBGR.GraphHelper.Equal(" + attrName + ModelGen.ATTR_IMPL_SUFFIX + ", "
						+ "that_." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ")\n");
			} else {
				routedSB.appendFront("&& " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " == that_." + attrName + ModelGen.ATTR_IMPL_SUFFIX + "\n");
			}
		}
		routedSB.unindent();
		routedSB.appendFront(";\n");
		routedSB.unindent();
		routedSB.appendFront("}\n");
	}

	private void genElementCreateMethods(InheritanceType type, boolean isNode, String elemref, String allocName) {
		if(isNode) {
			sb.appendFront("public static " + elemref + " CreateNode(GRGEN_LGSP.LGSPGraph graph)\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront(elemref + " node;\n");
			sb.appendFront("if(poolLevel == 0)\n");
			sb.appendFront("\tnode = new " + allocName + "();\n");
			sb.appendFront("else\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("node = pool[--poolLevel];\n");
			sb.appendFront("node.lgspInhead = null;\n");
			sb.appendFront("node.lgspOuthead = null;\n");
			sb.appendFront("node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;\n");
			initAllMembersNonConst(type, "node", true, false);
			sb.unindent();
			sb.appendFront("}\n");
			sb.appendFront("graph.AddNode(node);\n");
			sb.appendFront("return node;\n");
			sb.unindent();
			sb.appendFront("}\n");
			sb.append("\n");
					
			sb.appendFront("public static " + elemref + " CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront(elemref + " node;\n");
			sb.appendFront("if(poolLevel == 0)\n");
			sb.appendFront("\tnode = new " + allocName + "();\n");
			sb.appendFront("else\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("node = pool[--poolLevel];\n");
			sb.appendFront("node.lgspInhead = null;\n");
			sb.appendFront("node.lgspOuthead = null;\n");
			sb.appendFront("node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;\n");
			initAllMembersNonConst(type, "node", true, false);
			sb.unindent();
			sb.appendFront("}\n");
			sb.appendFront("graph.AddNode(node, nodeName);\n");
			sb.appendFront("return node;\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
		else {
			sb.appendFront("public static " + elemref + " CreateEdge(GRGEN_LGSP.LGSPGraph graph, "
						+ "GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront(elemref + " edge;\n");
			sb.appendFront("if(poolLevel == 0)\n");
			sb.appendFront("\tedge = new " + allocName + "(source, target);\n");
			sb.appendFront("else\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("edge = pool[--poolLevel];\n");
			sb.appendFront("edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;\n");
			sb.appendFront("edge.lgspSource = source;\n");
			sb.appendFront("edge.lgspTarget = target;\n");
			initAllMembersNonConst(type, "edge", true, false);
			sb.unindent();
			sb.appendFront("}\n");
			sb.appendFront("graph.AddEdge(edge);\n");
			sb.appendFront("return edge;\n");
			sb.unindent();
			sb.appendFront("}\n");
			sb.append("\n");

			sb.appendFront("public static " + elemref + " CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, "
						+ "GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront(elemref + " edge;\n");
			sb.appendFront("if(poolLevel == 0)\n");
			sb.appendFront("\tedge = new " + allocName + "(source, target);\n");
			sb.appendFront("else\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("edge = pool[--poolLevel];\n");
			sb.appendFront("edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;\n");
			sb.appendFront("edge.lgspSource = source;\n");
			sb.appendFront("edge.lgspTarget = target;\n");
			initAllMembersNonConst(type, "edge", true, false);
			sb.unindent();
			sb.appendFront("}\n");
			sb.appendFront("graph.AddEdge(edge, edgeName);\n");
			sb.appendFront("return edge;\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genElementRecycleMethod() {
		sb.appendFront("public override void Recycle()\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(poolLevel < 10)\n");
		sb.appendFront("\tpool[poolLevel++] = this;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void initAllMembersNonConst(InheritanceType type, String varName,
			boolean withDefaultInits, boolean isResetAllAttributes) {
		curMemberOwner = varName;

		// if we don't currently create the method ResetAllAttributes
		// we replace the initialization code by a call to ResetAllAttributes, if it gets to large
		if(!isResetAllAttributes
				&& initializationOperationsCount(type) > MAX_OPERATIONS_FOR_ATTRIBUTE_INITIALIZATION_INLINING)
		{
			sb.appendFront(varName +  ".ResetAllAttributes();\n");
			curMemberOwner = null;
			return;
		}

		sb.appendFront("// implicit initialization, container creation of " + formatIdentifiable(type) + "\n");

		// default attribute inits need to be generated if code must overwrite old values
		// only in constructor not needed, cause there taken care of by c#
		// if there is explicit initialization code, it's not needed, too,
		// but that's left for the compiler to optimize away
		if(withDefaultInits) {
			genDefaultInits(type, varName);
		}

		// create containers, i.e. maps, sets, arrays, deques
		genContainerInits(type, varName);

		// generate the user defined initializations, first for super types
		for(InheritanceType superType : type.getAllSuperTypes()) {
			genMemberInitsNonConst(superType, type, varName, withDefaultInits, isResetAllAttributes);
		}
		// then for current type
		genMemberInitsNonConst(type, type, varName, withDefaultInits, isResetAllAttributes);

		curMemberOwner = null;
	}

	private void genDefaultInits(InheritanceType type, String varName) {
		for(Entity member : type.getAllMembers()) {
			if(member.isConst())
				continue;

			Type t = member.getType();
			// handled down below, as containers must be created independent of initialization
			if(t instanceof MapType || t instanceof SetType
					|| t instanceof ArrayType || t instanceof DequeType)
				continue;

			String attrName = formatIdentifiable(member);
			sb.appendFront(varName + ".@" + attrName + " = ");
			if(t instanceof ByteType || t instanceof ShortType || t instanceof IntType 
					|| t instanceof EnumType || t instanceof DoubleType ) {
				sb.append("0;\n");
			} else if(t instanceof FloatType) {
				sb.append("0f;\n");
			} else if(t instanceof LongType) {
				sb.append("0L;\n");
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

	private void genContainerInits(InheritanceType type, String varName) {
		for(Entity member : type.getAllMembers()) {
			if(member.isConst())
				continue;

			Type t = member.getType();
			if(!(t instanceof MapType || t instanceof SetType
					|| t instanceof ArrayType || t instanceof DequeType))
				continue;

			String attrName = formatIdentifiable(member);
			sb.appendFront(varName + ".@" + attrName + " = ");
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
			String varName) {
		curMemberOwner = varName;

		List<String> staticInitializers = new LinkedList<String>();

		sb.append("\n");

		// generate the user defined initializations, first for super types
		for(InheritanceType superType : type.getAllSuperTypes())
			genMemberInitsConst(superType, type, staticInitializers);
		// then for current type
		genMemberInitsConst(type, type, staticInitializers);

		sb.appendFront("static " + className + "() {\n");
		sb.indent();
		for(String staticInit : staticInitializers) {
			sb.appendFront(staticInit + "();\n");
		}
		sb.unindent();
		sb.appendFront("}\n");

		curMemberOwner = null;
	}

	private void genMemberInitsNonConst(InheritanceType type, InheritanceType targetType,
			String varName, boolean withDefaultInits, boolean isResetAllAttributes) {
		if(rootTypes.contains(type.getIdent().toString())) // skip root types, they don't possess attributes
			return;
		sb.appendFront("// explicit initializations of " + formatIdentifiable(type) + " for target " + formatIdentifiable(targetType) + "\n");

		// emit all initializations in base classes of members that are used for init'ing other members,
		// i.e. prevent optimization of using only the closest initialization
		// TODO: generalize to all types in between type and target type
		
		// init members of primitive value with explicit initialization
		genMemberInitsNonConstPrimitiveType(type, targetType, varName);

		// init members of map value with explicit initialization
		genMemberInitsNonConstMapType(type, targetType, varName);

		// init members of set value with explicit initialization
		genMemberInitsNonConstSetType(type, targetType, varName);

		// init members of array value with explicit initialization
		genMemberInitsNonConstArrayType(type, targetType, varName);
		
		// init members of deque value with explicit initialization
		genMemberInitsNonConstDequeType(type, targetType, varName);
	}

	private void genMemberInitsNonConstPrimitiveType(InheritanceType type, InheritanceType targetType, String varName) {
		NeededEntities needs = new NeededEntities(false, false, false, false, false, false, false, true);
		for(MemberInit memberInit : type.getMemberInits()) {
			memberInit.getExpression().collectNeededEntities(needs);
		}
		for(MemberInit memberInit : targetType.getMemberInits()) {
			memberInit.getExpression().collectNeededEntities(needs);
		}

		for(MemberInit memberInit : type.getMemberInits()) {
			Entity member = memberInit.getMember();
			if(memberInit.getMember().isConst())
				continue;
			if(!needs.members.contains(member) 
					&& !generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrName = formatIdentifiable(memberInit.getMember());
			sb.appendFront(varName + ".@" + attrName + " = ");
			genExpression(sb, memberInit.getExpression(), null);
			sb.append(";\n");
		}
	}

	private void genMemberInitsNonConstMapType(InheritanceType type, InheritanceType targetType, String varName) {
		for(MapInit mapInit : type.getMapInits()) {
			Entity member = mapInit.getMember();
			if(mapInit.getMember().isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrName = formatIdentifiable(mapInit.getMember());
			for(MapItem item : mapInit.getMapItems()) {
				sb.appendFront(varName + ".@" + attrName + "[");
				genExpression(sb, item.getKeyExpr(), null);
				sb.append("] = ");
				genExpression(sb, item.getValueExpr(), null);
				sb.append(";\n");
			}
		}
	}

	private void genMemberInitsNonConstSetType(InheritanceType type, InheritanceType targetType, String varName) {
		for(SetInit setInit : type.getSetInits()) {
			Entity member = setInit.getMember();
			if(setInit.getMember().isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrName = formatIdentifiable(setInit.getMember());
			for(SetItem item : setInit.getSetItems()) {
				sb.appendFront(varName + ".@" + attrName + "[");
				genExpression(sb, item.getValueExpr(), null);
				sb.append("] = null;\n");
			}
		}
	}

	private void genMemberInitsNonConstArrayType(InheritanceType type, InheritanceType targetType, String varName) {
		for(ArrayInit arrayInit : type.getArrayInits()) {
			Entity member = arrayInit.getMember();
			if(arrayInit.getMember().isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrName = formatIdentifiable(arrayInit.getMember());
			for(ArrayItem item : arrayInit.getArrayItems()) {
				sb.appendFront(varName + ".@" + attrName + ".Add(");
				genExpression(sb, item.getValueExpr(), null);
				sb.append(");\n");
			}
		}
	}

	private void genMemberInitsNonConstDequeType(InheritanceType type, InheritanceType targetType, String varName) {
		for(DequeInit dequeInit : type.getDequeInits()) {
			Entity member = dequeInit.getMember();
			if(dequeInit.getMember().isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrName = formatIdentifiable(dequeInit.getMember());
			for(DequeItem item : dequeInit.getDequeItems()) {
				sb.appendFront(varName + ".@" + attrName + ".Add(");
				genExpression(sb, item.getValueExpr(), null);
				sb.append(");\n");
			}
		}
	}

	private void genMemberInitsConst(InheritanceType type, InheritanceType targetType,
			List<String> staticInitializers) {
		if(rootTypes.contains(type.getIdent().toString())) // skip root types, they don't possess attributes
			return;
		sb.appendFront("// explicit initializations of " + formatIdentifiable(type) + " for target " + formatIdentifiable(targetType) + "\n");

		HashSet<Entity> initializedConstMembers = new HashSet<Entity>();

		// init const members of primitive value with explicit initialization
		genMemberInitsConstPrimitiveType(type, targetType, initializedConstMembers);

		// init const members of map value with explicit initialization
		genMemberInitsConstMapType(type, targetType, staticInitializers, initializedConstMembers);

		// init const members of set value with explicit initialization
		genMemberInitsConstSetType(type, targetType, staticInitializers, initializedConstMembers);

		// init const members of array value with explicit initialization
		genMemberInitsConstArrayType(type, targetType, staticInitializers, initializedConstMembers);

		// init const members of deque value with explicit initialization
		genMemberInitsConstDequeType(type, targetType, staticInitializers, initializedConstMembers);

		sb.append("\t\t// implicit initializations of " + formatIdentifiable(type) + " for target " + formatIdentifiable(targetType) + "\n");

		genMemberImplicitInitsNonConst(type, targetType, initializedConstMembers);
	}

	private void genMemberInitsConstPrimitiveType(InheritanceType type, InheritanceType targetType,
			HashSet<Entity> initializedConstMembers) {
		for(MemberInit memberInit : type.getMemberInits()) {
			Entity member = memberInit.getMember();
			if(!member.isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrType = formatAttributeType(member);
			String attrName = formatIdentifiable(member);
			sb.appendFront("private static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = ");
			genExpression(sb, memberInit.getExpression(), null);
			sb.append(";\n");

			initializedConstMembers.add(member);
		}
	}

	private void genMemberInitsConstMapType(InheritanceType type, InheritanceType targetType,
			List<String> staticInitializers, HashSet<Entity> initializedConstMembers) {
		for(MapInit mapInit : type.getMapInits()) {
			Entity member = mapInit.getMember();
			if(!member.isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrType = formatAttributeType(member);
			String attrName = formatIdentifiable(member);
			sb.appendFront("private static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = " +
					"new " + attrType + "();\n");
			staticInitializers.add("init_" + attrName);
			sb.appendFront("static void init_" + attrName + "() {\n");
			sb.indent();
			for(MapItem item : mapInit.getMapItems()) {
				sb.appendFront("");
				sb.append(attrName + ModelGen.ATTR_IMPL_SUFFIX);
				sb.append("[");
				genExpression(sb, item.getKeyExpr(), null);
				sb.append("] = ");
				genExpression(sb, item.getValueExpr(), null);
				sb.append(";\n");
			}
			sb.unindent();
			sb.appendFront("}\n");

			initializedConstMembers.add(member);
		}
	}

	private void genMemberInitsConstSetType(InheritanceType type, InheritanceType targetType,
			List<String> staticInitializers, HashSet<Entity> initializedConstMembers) {
		for(SetInit setInit : type.getSetInits()) {
			Entity member = setInit.getMember();
			if(!member.isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrType = formatAttributeType(member);
			String attrName = formatIdentifiable(member);
			sb.appendFront("private static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = " +
					"new " + attrType + "();\n");
			staticInitializers.add("init_" + attrName);
			sb.appendFront("static void init_" + attrName + "() {\n");
			sb.indent();
			for(SetItem item : setInit.getSetItems()) {
				sb.appendFront("");
				sb.append(attrName + ModelGen.ATTR_IMPL_SUFFIX);
				sb.append("[");
				genExpression(sb, item.getValueExpr(), null);
				sb.append("] = null;\n");
			}
			sb.unindent();
			sb.appendFront("}\n");

			initializedConstMembers.add(member);
		}
	}

	private void genMemberInitsConstArrayType(InheritanceType type, InheritanceType targetType,
			List<String> staticInitializers, HashSet<Entity> initializedConstMembers) {
		for(ArrayInit arrayInit : type.getArrayInits()) {
			Entity member = arrayInit.getMember();
			if(!member.isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrType = formatAttributeType(member);
			String attrName = formatIdentifiable(member);
			sb.appendFront("private static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = " +
					"new " + attrType + "();\n");
			staticInitializers.add("init_" + attrName);
			sb.appendFront("static void init_" + attrName + "() {\n");
			sb.indent();
			for(ArrayItem item : arrayInit.getArrayItems()) {
				sb.appendFront("");
				sb.append(attrName + ModelGen.ATTR_IMPL_SUFFIX);
				sb.append(".Add(");
				genExpression(sb, item.getValueExpr(), null);
				sb.append(");\n");
			}
			sb.unindent();
			sb.appendFront("}\n");

			initializedConstMembers.add(member);
		}
	}

	private void genMemberInitsConstDequeType(InheritanceType type, InheritanceType targetType,
			List<String> staticInitializers, HashSet<Entity> initializedConstMembers) {
		for(DequeInit dequeInit : type.getDequeInits()) {
			Entity member = dequeInit.getMember();
			if(!member.isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrType = formatAttributeType(member);
			String attrName = formatIdentifiable(member);
			sb.appendFront("private static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = " +
					"new " + attrType + "();\n");
			staticInitializers.add("init_" + attrName);
			sb.appendFront("static void init_" + attrName + "() {\n");
			sb.indent();
			for(DequeItem item : dequeInit.getDequeItems()) {
				sb.appendFront("");
				sb.append(attrName + ModelGen.ATTR_IMPL_SUFFIX);
				sb.append(".Enqueue(");
				genExpression(sb, item.getValueExpr(), null);
				sb.append(");\n");
			}
			sb.unindent();
			sb.appendFront("}\n");

			initializedConstMembers.add(member);
		}
	}

	private void genMemberImplicitInitsNonConst(InheritanceType type, InheritanceType targetType,
			HashSet<Entity> initializedConstMembers) {
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
					|| memberType instanceof ArrayType || memberType instanceof DequeType) {
				sb.append("\t\tprivate static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = " +
						"new " + attrType + "();\n");
			} else
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

	protected void genQualAccess(SourceBuilder sb, Qualification qual, Object modifyGenerationState) {
		Entity owner = qual.getOwner();
		sb.append("((I" + getNodeOrEdgeTypePrefix(owner) +
				formatIdentifiable(owner.getType()) + ") ");
		sb.append(formatEntity(owner) + ").@" + formatIdentifiable(qual.getMember()));
	}

	protected void genMemberAccess(SourceBuilder sb, Entity member) {
		if(curMemberOwner != null)
			sb.append(curMemberOwner + ".");
		sb.append("@" + formatIdentifiable(member));
	}


	/**
	 * Generate the attribute accessor implementations of the given type
	 */
	private void genAttributesAndAttributeAccessImpl(InheritanceType type) {
		SourceBuilder routedSB = sb;
		String extName = type.getExternalName();
		String extModifier = "";

		// what's that?
		if(extName != null) {
			routedSB = getStubBuffer();
			extModifier = "override ";

			for(Entity e : type.getAllMembers()) {
				genAttributeAccess(type, e, "public abstract ");
			}
		}

		// Create the implementation of the attributes.
		// If an external name is given for this type, this is written
		// into the stub file with an "override" modifier on the accessors.
		for(Entity member : type.getAllMembers()) {
			genAttributeGetterSetterAndMember(type, routedSB, extModifier, member);
		}

		genGetAttributeByName(type);

		genSetAttributeByName(type);

		genResetAllAttributes(type);
	}

	private void genAttributeGetterSetterAndMember(InheritanceType type, SourceBuilder routedSB, String extModifier,
			Entity member) {
		String attrType = formatAttributeType(member);
		String attrName = formatIdentifiable(member);

		if(member.isConst()) {
			// no member for const attributes, no setter for const attributes
			// they are class static, the member is created at the point of initialization
			routedSB.appendFront("public " + extModifier + attrType + " @" + attrName + "\n");
			routedSB.appendFront("{\n");
			routedSB.appendFront("\tget { return " + attrName + ModelGen.ATTR_IMPL_SUFFIX + "; }\n");
			routedSB.appendFront("}\n");
		} else {
			// member, getter, setter for non-const attributes
			routedSB.append("\n");
			routedSB.appendFront("private " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + ";\n");
			routedSB.appendFront("public " + extModifier + attrType + " @" + attrName + "\n");
			routedSB.appendFront("{\n");
			routedSB.indent();
			routedSB.appendFront("get { return " + attrName + ModelGen.ATTR_IMPL_SUFFIX + "; }\n");
			routedSB.appendFront("set { " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = value; }\n");
			routedSB.unindent();
			routedSB.appendFront("}\n");
		}

		// what's that?
		Entity overriddenMember = type.getOverriddenMember(member);
		if(overriddenMember != null) {
			routedSB.append("\n");
			routedSB.appendFront("object "
					+ formatElementInterfaceRef(overriddenMember.getOwner())
					+ ".@" + attrName + "\n");
			routedSB.appendFront("{\n");
			routedSB.indent();
			routedSB.appendFront("get { return " + attrName + ModelGen.ATTR_IMPL_SUFFIX + "; }\n");
			routedSB.appendFront("set { " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = (" + attrType + ") value; }\n");
			routedSB.unindent();
			routedSB.appendFront("}\n");
		}
	}

	private void genGetAttributeByName(InheritanceType type) {
		sb.appendFront("public override object GetAttribute(string attrName)\n");
		sb.appendFront("{\n");
		sb.indent();
		if(type.getAllMembers().size() != 0) {
			sb.appendFront("switch(attrName)\n");
			sb.appendFront("{\n");
			sb.indent();
			for(Entity member : type.getAllMembers()) {
				String name = formatIdentifiable(member);
				sb.appendFront("case \"" + name + "\": return this.@" + name + ";\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
		}
		sb.appendFront("throw new NullReferenceException(\n");
		sb.appendFront("\t\"The " + (type instanceof NodeType ? "node" : "edge")
				+ " type \\\"" + formatIdentifiable(type)
				+ "\\\" does not have the attribute \\\"\" + attrName + \"\\\"!\");\n");

		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genSetAttributeByName(InheritanceType type) {
		sb.appendFront("public override void SetAttribute(string attrName, object value)\n");
		sb.appendFront("{\n");
		sb.indent();
		if(type.getAllMembers().size() != 0) {
			sb.appendFront("switch(attrName)\n");
			sb.appendFront("{\n");
			sb.indent();
			for(Entity member : type.getAllMembers()) {
				String name = formatIdentifiable(member);
				if(member.isConst()) {
					sb.appendFront("case \"" + name + "\": ");
					sb.append("throw new NullReferenceException(");
					sb.append("\"The attribute " + name + " of the " + (type instanceof NodeType ? "node" : "edge")
							+ " type \\\"" + formatIdentifiable(type)
							+ "\\\" is read only!\");\n");
				} else {
					sb.appendFront("case \"" + name + "\": this.@" + name + " = ("
							+ formatAttributeType(member) + ") value; return;\n");
				}
			}
			sb.unindent();
			sb.appendFront("}\n");
		}
		sb.appendFront("throw new NullReferenceException(\n");
		sb.appendFront("\t\"The " + (type instanceof NodeType ? "node" : "edge")
				+ " type \\\"" + formatIdentifiable(type)
				+ "\\\" does not have the attribute \\\"\" + attrName + \"\\\"!\");\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genResetAllAttributes(InheritanceType type) {
		sb.appendFront("public override void ResetAllAttributes()\n");
		sb.appendFront("{\n");
		sb.indent();
		initAllMembersNonConst(type, "this", true, true);
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genParameterPassingMethodCall(InheritanceType type, FunctionMethod fm) {
		sb.appendFront("case \"" + fm.getIdent().toString() + "\":\n");
		sb.appendFront("\treturn @" + fm.getIdent().toString() + "(actionEnv, graph");
		int i = 0;
		for(Entity inParam : fm.getParameters()) {
			sb.append(", (" + formatType(inParam.getType()) + ")arguments[" + i + "]");
			++i;
		}
		sb.append(");\n");
	}

	private void genParameterPassingMethodCall(InheritanceType type, ProcedureMethod pm) {
		sb.appendFront("case \"" + pm.getIdent().toString() + "\":\n");
		sb.appendFront("{\n");
		sb.indent();
		int i = 0;
		for(Type outType : pm.getReturnTypes()) {
			sb.appendFront(formatType(outType));
			sb.append(" ");
			sb.append("_out_param_" + i + ";\n");
			++i;
		}
		sb.appendFront("@" + pm.getIdent().toString() + "(actionEnv, graph");
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
			sb.appendFront("ReturnArray_" + pm.getIdent().toString() + "_" + type.getIdent().toString() + "[" + i + "] = ");
			sb.append("_out_param_" + i + ";\n");
		}
		sb.appendFront("return ReturnArray_" + pm.getIdent().toString() + "_" + type.getIdent().toString() + ";\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genParameterPassingReturnArray(InheritanceType type, ProcedureMethod pm) {
		sb.appendFront("private static object[] ReturnArray_" + pm.getIdent().toString() + "_" + type.getIdent().toString() + " = new object[" + pm.getReturnTypes().size() + "]; // helper array for multi-value-returns, to allow for contravariant parameter assignment\n");
	}

	private void genMethods(InheritanceType type) {
		sb.append("\n");
		
		genApplyFunctionMethodDispatcher(type);

		for(FunctionMethod fm : type.getAllFunctionMethods()) {
			genFunctionMethod(fm);
		}

		//////////////////////////////////////////////////////////////
		
		genApplyProcedureMethodDispatcher(type);
		
		for(ProcedureMethod pm : type.getAllProcedureMethods()) {
			forceNotConstant(pm.getComputationStatements());
			genParameterPassingReturnArray(type, pm);
		}

		for(ProcedureMethod pm : type.getAllProcedureMethods()) {
			genProcedureMethod(pm);
		}
	}

	private void genApplyFunctionMethodDispatcher(InheritanceType type) {
		sb.appendFront("public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("switch(name)\n");
		sb.appendFront("{\n");
		sb.indent();
		for(FunctionMethod fm : type.getAllFunctionMethods()) {
			forceNotConstant(fm.getComputationStatements());
			genParameterPassingMethodCall(type, fm);
		}
		sb.appendFront("default: throw new NullReferenceException(\"" + formatIdentifiable(type) + " does not have the function method \" + name + \"!\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genFunctionMethod(FunctionMethod fm) {
		List<String> staticInitializers = new LinkedList<String>();
		String pathPrefixForElements = "";
		HashMap<Entity, String> alreadyDefinedEntityToName = new HashMap<Entity, String>();
		genLocalContainersEvals(sb, fm.getComputationStatements(), staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);

		sb.appendFront("public " + formatType(fm.getReturnType()) + " ");
		sb.append(fm.getIdent().toString() + "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_");
		for(Entity inParam : fm.getParameters()) {
			sb.append(", ");
			sb.append(formatType(inParam.getType()));
			sb.append(" ");
			sb.append(formatEntity(inParam));
		}
		sb.append(")\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;\n");
		sb.appendFront("GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;\n");
		ModifyGen.ModifyGenerationState modifyGenState = mgFuncComp.new ModifyGenerationState(model, null, "", false, be.system.emitProfilingInstrumentation());
		for(EvalStatement evalStmt : fm.getComputationStatements()) {
			modifyGenState.functionOrProcedureName = fm.getIdent().toString();
			mgFuncComp.genEvalStmt(sb, modifyGenState, evalStmt);
		}
		sb.unindent();
		sb.appendFront("}\n");

		if(model.areFunctionsParallel())
		{
			sb.appendFront("public " + formatType(fm.getReturnType()) + " ");
			sb.append(fm.getIdent().toString() + "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_");
			for(Entity inParam : fm.getParameters()) {
				sb.append(", ");
				sb.append(formatType(inParam.getType()));
				sb.append(" ");
				sb.append(formatEntity(inParam));
			}
			sb.append(", int threadId");
			sb.append(")\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;\n");
			sb.appendFront("GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;\n");
			modifyGenState = mgFuncComp.new ModifyGenerationState(model, null, "", true, be.system.emitProfilingInstrumentation());
			for(EvalStatement evalStmt : fm.getComputationStatements()) {
				modifyGenState.functionOrProcedureName = fm.getIdent().toString();
				mgFuncComp.genEvalStmt(sb, modifyGenState, evalStmt);
			}
			sb.unindent();
			sb.append("}\n");
		}
	}

	private void genApplyProcedureMethodDispatcher(InheritanceType type) {
		sb.appendFront("public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("switch(name)\n");
		sb.appendFront("{\n");
		sb.indent();
		for(ProcedureMethod pm : type.getAllProcedureMethods()) {
			genParameterPassingMethodCall(type, pm);
		}
		sb.appendFront("default: throw new NullReferenceException(\"" + formatIdentifiable(type) + " does not have the procedure method \" + name + \"!\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genProcedureMethod(ProcedureMethod pm) {
		List<String> staticInitializers = new LinkedList<String>();
		String pathPrefixForElements = "";
		HashMap<Entity, String> alreadyDefinedEntityToName = new HashMap<Entity, String>();
		genLocalContainersEvals(sb, pm.getComputationStatements(), staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);

		sb.appendFront("public void ");
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
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;\n");
		sb.appendFront("GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;\n");
		ModifyGen.ModifyGenerationState modifyGenState = mgFuncComp.new ModifyGenerationState(model, null, "", false, be.system.emitProfilingInstrumentation());
		mgFuncComp.initEvalGen();
		
		if(be.system.mayFireDebugEvents()) {
			sb.appendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering(");
			sb.append("\"" + pm.getIdent().toString() + "\"");
			for(Entity inParam : pm.getParameters()) {
				sb.append(", ");
				sb.append(formatEntity(inParam));
			}
			sb.append(");\n");
		}

		for(EvalStatement evalStmt : pm.getComputationStatements()) {
			modifyGenState.functionOrProcedureName = pm.getIdent().toString();
			mgFuncComp.genEvalStmt(sb, modifyGenState, evalStmt);
		}
		sb.unindent();
		sb.appendFront("}\n");
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
		sb.appendFront("public sealed partial class " + typename + " : GRGEN_LIBGR." + kindStr + "Type\n");
		sb.appendFront("{\n");
		sb.indent();

		sb.appendFront("public static " + typeref + " typeVar = new " + typeref + "();\n");
		genIsA(allTypes, type);
		genIsMyType(allTypes, type);
		genAttributeAttributes(type);

		sb.appendFront("public " + typename + "() : base((int) " + formatNodeOrEdge(type) + "Types.@" + typeident + ")\n");
		sb.appendFront("{\n");
		sb.indent();
		genAttributeInit(type);
		addAnnotations(sb, type, "annotations");
		sb.unindent();
		sb.appendFront("}\n");

		sb.appendFront("public override string Name { get { return \"" + typeident + "\"; } }\n");
		sb.appendFront("public override string Package { get { return " + (!getPackagePrefix(type).equals("") ? "\""+getPackagePrefix(type)+"\"" : "null") + "; } }\n");
		sb.appendFront("public override string PackagePrefixedName { get { return \"" + getPackagePrefixDoubleColon(type) + typeident + "\"; } }\n");
		if(type.getIdent().toString().equals("Node")) {
				sb.appendFront("public override string "+formatNodeOrEdge(type)+"InterfaceName { get { return "
						+ "\"de.unika.ipd.grGen.libGr.INode\"; } }\n");
		} else if(type.getIdent().toString().equals("AEdge")) {
				sb.appendFront("public override string "+formatNodeOrEdge(type)+"InterfaceName { get { return "
						+ "\"de.unika.ipd.grGen.libGr.IEdge\"; } }\n");
		} else if(type.getIdent().toString().equals("Edge")) {
			sb.appendFront("public override string "+formatNodeOrEdge(type)+"InterfaceName { get { return "
					+ "\"de.unika.ipd.grGen.libGr.IDEdge\"; } }\n");
		} else if(type.getIdent().toString().equals("UEdge")) {
			sb.appendFront("public override string "+formatNodeOrEdge(type)+"InterfaceName { get { return "
					+ "\"de.unika.ipd.grGen.libGr.IUEdge\"; } }\n");
		} else {
			sb.appendFront("public override string "+formatNodeOrEdge(type)+"InterfaceName { get { return "
				+ "\"de.unika.ipd.grGen.Model_" + model.getIdent() + "."
				+ getPackagePrefixDot(type) + "I" + getNodeOrEdgeTypePrefix(type) + formatIdentifiable(type) + "\"; } }\n");
		}
		if(type.isAbstract()) {
			sb.appendFront("public override string "+formatNodeOrEdge(type)+"ClassName { get { return null; } }\n");
		} else {
			sb.appendFront("public override string "+formatNodeOrEdge(type)+"ClassName { get { return \"de.unika.ipd.grGen.Model_"
					+ model.getIdent() + "." + getPackagePrefixDot(type) + formatElementClassName(type) + "\"; } }\n");
		}

		if(isNode) {
			sb.appendFront("public override GRGEN_LIBGR.INode CreateNode()\n");
			sb.appendFront("{\n");
			sb.indent();
			if(type.isAbstract())
				sb.appendFront("throw new Exception(\"The abstract node type "
						+ typeident + " cannot be instantiated!\");\n");
			else
				sb.appendFront("return new " + allocName + "();\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
		else {
			EdgeType edgeType = (EdgeType) type;
			sb.appendFront("public override GRGEN_LIBGR.Directedness Directedness "
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
			sb.appendFront("public override GRGEN_LIBGR.IEdge CreateEdge("
						+ "GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)\n");
			sb.appendFront("{\n");
			sb.indent();
			if(type.isAbstract())
				sb.appendFront("throw new Exception(\"The abstract edge type "
						+ typeident + " cannot be instantiated!\");\n");
			else
				sb.appendFront("return new " + allocName
						+ "((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);\n");
			sb.unindent();
			sb.appendFront("}\n\n");
			sb.append("\n");
			sb.appendFront("public override void SetSourceAndTarget("
					+ "GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)\n");
			sb.appendFront("{\n");
			sb.indent();
			if(type.isAbstract())
				sb.appendFront("throw new Exception(\"The abstract edge type "
						+ typeident + " does not support source and target setting!\");\n");
			else
				sb.appendFront("((GRGEN_LGSP.LGSPEdge)edge).SetSourceAndTarget" 
						+ "((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);\n");
			sb.unindent();
			sb.appendFront("}\n");
		}

		sb.appendFront("public override bool IsAbstract { get { return " + (type.isAbstract() ? "true" : "false") + "; } }\n");
		sb.appendFront("public override bool IsConst { get { return " + (type.isConst() ? "true" : "false") + "; } }\n");

		sb.appendFront("public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }\n");
		sb.appendFront("public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();\n");

		sb.appendFront("public override int NumAttributes { get { return " + type.getAllMembers().size() + "; } }\n");
		genAttributeTypesEnumerator(type);
		genGetAttributeType(type);

		sb.appendFront("public override int NumFunctionMethods { get { return " + type.getAllFunctionMethods().size() + "; } }\n");
		genFunctionMethodsEnumerator(type);
		genGetFunctionMethod(type);

		sb.appendFront("public override int NumProcedureMethods { get { return " + type.getAllProcedureMethods().size() + "; } }\n");
		genProcedureMethodsEnumerator(type);
		genGetProcedureMethod(type);

		sb.appendFront("public override bool IsA(GRGEN_LIBGR.GrGenType other)\n");
		sb.appendFront("{\n");
		sb.appendFront("\treturn (this == other) || isA[other.TypeID];\n");
		sb.appendFront("}\n");

		genCreateWithCopyCommons(type);
		sb.unindent();
		sb.appendFront("}\n");
		
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
		sb.appendFront("public static bool[] isA = new bool[] { ");
		for(InheritanceType nt : types) {
			if(type.isCastableTo(nt))
				sb.append("true, ");
			else
				sb.append("false, ");
		}
		sb.append("};\n");
		sb.appendFront("public override bool IsA(int typeID) { return isA[typeID]; }\n");
	}

	private void genIsMyType(Collection<? extends InheritanceType> types, InheritanceType type) {
		sb.appendFront("public static bool[] isMyType = new bool[] { ");
		for(InheritanceType nt : types) {
			if(nt.isCastableTo(type))
				sb.append("true, ");
			else
				sb.append("false, ");
		}
		sb.append("};\n");
		sb.appendFront("public override bool IsMyType(int typeID) { return isMyType[typeID]; }\n");
	}

	private void genAttributeAttributes(InheritanceType type) {
		for(Entity member : type.getMembers()) { // only for locally defined members
			sb.appendFront("public static GRGEN_LIBGR.AttributeType " + formatAttributeTypeName(member) + ";\n");

			// attribute types T/S of map<T,S>/set<T>/array<T>/deque<T>
			if(member.getType() instanceof MapType) {
				sb.appendFront("public static GRGEN_LIBGR.AttributeType " + formatAttributeTypeName(member)+"_map_domain_type;\n");
				sb.appendFront("public static GRGEN_LIBGR.AttributeType " + formatAttributeTypeName(member)+"_map_range_type;\n");
			}
			if(member.getType() instanceof SetType) {
				sb.appendFront("public static GRGEN_LIBGR.AttributeType " + formatAttributeTypeName(member)+"_set_member_type;\n");
			}
			if(member.getType() instanceof ArrayType) {
				sb.appendFront("public static GRGEN_LIBGR.AttributeType " + formatAttributeTypeName(member)+"_array_member_type;\n");
			}
			if(member.getType() instanceof DequeType) {
				sb.appendFront("public static GRGEN_LIBGR.AttributeType " + formatAttributeTypeName(member)+"_deque_member_type;\n");
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
				sb.appendFront(attributeTypeName + "_map_domain_type = new GRGEN_LIBGR.AttributeType(");
				sb.append("\"" + formatIdentifiable(e) + "_map_domain_type\", this, ");
				genAttributeInitTypeDependentStuff(mt.getKeyType(), e);
				sb.append(");\n");

				// attribute types S of map<T,S>
				sb.appendFront(attributeTypeName + "_map_range_type = new GRGEN_LIBGR.AttributeType(");
				sb.append("\"" + formatIdentifiable(e) + "_map_range_type\", this, ");
				genAttributeInitTypeDependentStuff(mt.getValueType(), e);
				sb.append(");\n");
			}
			else if (t instanceof SetType) {
				SetType st = (SetType)t;

				// attribute type T of set<T>
				sb.appendFront(attributeTypeName + "_set_member_type = new GRGEN_LIBGR.AttributeType(");
				sb.append("\"" + formatIdentifiable(e) + "_set_member_type\", this, ");
				genAttributeInitTypeDependentStuff(st.getValueType(), e);
				sb.append(");\n");
			}
			else if (t instanceof ArrayType) {
				ArrayType at = (ArrayType)t;

				// attribute type T of set<T>
				sb.appendFront(attributeTypeName + "_array_member_type = new GRGEN_LIBGR.AttributeType(");
				sb.append("\"" + formatIdentifiable(e) + "_array_member_type\", this, ");
				genAttributeInitTypeDependentStuff(at.getValueType(), e);
				sb.append(");\n");
			}
			else if (t instanceof DequeType) {
				DequeType qt = (DequeType)t;

				// attribute type T of deque<T>
				sb.appendFront(attributeTypeName + "_deque_member_type = new GRGEN_LIBGR.AttributeType(");
				sb.append("\"" + formatIdentifiable(e) + "_deque_member_type\", this, ");
				genAttributeInitTypeDependentStuff(qt.getValueType(), e);
				sb.append(");\n");
			}

			sb.appendFront(attributeTypeName + " = new GRGEN_LIBGR.AttributeType(");
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
		sb.appendFront("public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes");

		if(allMembers.isEmpty())
			sb.append(" { get { yield break; } }\n");
		else {
			sb.append("\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("get\n");
			sb.appendFront("{\n");
			for(Entity e : allMembers) {
				Type ownerType = e.getOwner();
				if(ownerType == type)
					sb.appendFront("\tyield return " + formatAttributeTypeName(e) + ";\n");
				else
					sb.appendFront("\tyield return " + formatTypeClassRef(ownerType) + "." + formatAttributeTypeName(e) + ";\n");
			}
			sb.appendFront("}\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genGetAttributeType(InheritanceType type) {
		Collection<Entity> allMembers = type.getAllMembers();
		sb.appendFront("public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)");

		if(allMembers.isEmpty())
			sb.append(" { return null; }\n");
		else {
			sb.append("\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("switch(name)\n");
			sb.appendFront("{\n");
			for(Entity e : allMembers) {
				Type ownerType = e.getOwner();
				if(ownerType == type)
					sb.appendFront("\tcase \"" + formatIdentifiable(e) + "\" : return " +
							formatAttributeTypeName(e) + ";\n");
				else
					sb.append("\tcase \"" + formatIdentifiable(e) + "\" : return " +
							formatTypeClassRef(ownerType) + "." + formatAttributeTypeName(e) + ";\n");
			}
			sb.appendFront("}\n");
			sb.appendFront("return null;\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genFunctionMethodsEnumerator(InheritanceType type) {
		Collection<FunctionMethod> allFunctionMethods = type.getAllFunctionMethods();
		sb.appendFront("public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods");

		if(allFunctionMethods.isEmpty())
			sb.append(" { get { yield break; } }\n");
		else {
			sb.append("\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("get\n");
			sb.appendFront("{\n");
			for(FunctionMethod fm : allFunctionMethods) {
				sb.append("\tyield return " + formatFunctionMethodInfoName(fm, type) + ".Instance;\n");
			}
			sb.appendFront("}\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genGetFunctionMethod(InheritanceType type) {
		Collection<FunctionMethod> allFunctionMethods = type.getAllFunctionMethods();
		sb.appendFront("public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name)");

		if(allFunctionMethods.isEmpty())
			sb.append(" { return null; }\n");
		else {
			sb.append("\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("switch(name)\n");
			sb.appendFront("{\n");
			for(FunctionMethod fm : allFunctionMethods) {
				sb.append("\tcase \"" + formatIdentifiable(fm) + "\" : return " +
						formatFunctionMethodInfoName(fm, type) + ".Instance;\n");
			}
			sb.appendFront("}\n");
			sb.appendFront("return null;\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genProcedureMethodsEnumerator(InheritanceType type) {
		Collection<ProcedureMethod> allProcedureMethods = type.getAllProcedureMethods();
		sb.appendFront("public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods");

		if(allProcedureMethods.isEmpty())
			sb.append(" { get { yield break; } }\n");
		else {
			sb.append("\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("get\n");
			sb.appendFront("{\n");
			for(ProcedureMethod pm : allProcedureMethods) {
				sb.append("\tyield return " + formatProcedureMethodInfoName(pm, type) + ".Instance;\n");
			}
			sb.appendFront("}\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genGetProcedureMethod(InheritanceType type) {
		Collection<ProcedureMethod> allProcedureMethods = type.getAllProcedureMethods();
		sb.appendFront("public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name)");

		if(allProcedureMethods.isEmpty())
			sb.append(" { return null; }\n");
		else {
			sb.append("\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("switch(name)\n");
			sb.appendFront("{\n");
			for(ProcedureMethod pm : allProcedureMethods) {
				sb.append("\tcase \"" + formatIdentifiable(pm) + "\" : return " +
						formatProcedureMethodInfoName(pm, type) + ".Instance;\n");
			}
			sb.appendFront("}\n");
			sb.appendFront("return null;\n");
			sb.unindent();
			sb.append("}\n");
		}
	}

	private void getFirstCommonAncestors(InheritanceType curType,
			InheritanceType type, Set<InheritanceType> resTypes) {
		if(type.isCastableTo(curType))
			resTypes.add(curType);
		else {
			for(InheritanceType superType : curType.getDirectSuperTypes()) {
				getFirstCommonAncestors(superType, type, resTypes);
			}
		}
	}

	private void genCreateWithCopyCommons(InheritanceType type) {
		boolean isNode = type instanceof NodeType;
		String elemref = formatElementClassRef(type);
		String extName = type.getExternalName();
		String allocName = extName != null ? "global::" + extName : elemref;
		String kindName = isNode ? "Node" : "Edge";

		if(isNode) {
			sb.appendFront("public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons("
						+ "GRGEN_LIBGR.INode oldINode)\n");
			sb.appendFront("{\n");
			sb.indent();
		}
		else {
			sb.appendFront("public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons("
						+ "GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, "
						+ "GRGEN_LIBGR.IEdge oldIEdge)\n");
			sb.appendFront("{\n");
			sb.indent();
		}

		if(type.isAbstract()) {
			sb.appendFront("throw new Exception(\"Cannot retype to the abstract type "
					+ formatIdentifiable(type) + "!\");\n");
			sb.unindent();
			sb.appendFront("}\n");
			return;
		}

		Map<BitSet, LinkedList<InheritanceType>> commonGroups = getCommonGroups(type);

		if(commonGroups.size() != 0) {
			if(isNode) {
				sb.appendFront("GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;\n");
				sb.appendFront(elemref + " newNode = new " + allocName + "();\n");
			} else {
				sb.appendFront("GRGEN_LGSP.LGSPEdge oldEdge = (GRGEN_LGSP.LGSPEdge) oldIEdge;\n");
				sb.appendFront(elemref + " newEdge = new " + allocName
						+ "((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);\n");
			}
			sb.appendFront("switch(old" + kindName + ".Type.TypeID)\n");
			sb.appendFront("{\n");
			sb.indent();
			for(Map.Entry<BitSet, LinkedList<InheritanceType>> entry : commonGroups.entrySet()) {
				emitCommonGroup(type, kindName, entry);
			}
			sb.unindent();
			sb.appendFront("}\n");
			sb.appendFront("return new" + kindName + ";\n");
			sb.unindent();
			sb.appendFront("}\n");
			sb.append("\n");
		}
		else {
			if(isNode)
				sb.appendFront("return new " + allocName + "();\n");
			else {
				sb.appendFront("return new " + allocName
						+ "((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
			sb.append("\n");
		}
	}

	private Map<BitSet, LinkedList<InheritanceType>> getCommonGroups(InheritanceType type) {
		boolean isNode = type instanceof NodeType;
		
		Map<BitSet, LinkedList<InheritanceType>> commonGroups = new LinkedHashMap<BitSet, LinkedList<InheritanceType>>();

		Collection<? extends InheritanceType> typeSet =
			isNode ? (Collection<? extends InheritanceType>) model.getAllNodeTypes()
			: (Collection<? extends InheritanceType>) model.getAllEdgeTypes();
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
		return commonGroups;
	}

	private void emitCommonGroup(InheritanceType type, String kindName,
			Map.Entry<BitSet, LinkedList<InheritanceType>> entry) {
		for(InheritanceType itype : entry.getValue()) {
			sb.appendFront("case (int) GRGEN_MODEL." + getPackagePrefixDot(itype) + kindName + "Types.@"
					+ formatIdentifiable(itype) + ":\n");
		}
		sb.indent();
		BitSet bitset = entry.getKey();
		HashSet<Entity> copiedAttribs = new HashSet<Entity>();
		for(int i = bitset.nextSetBit(0); i >= 0; i = bitset.nextSetBit(i+1)) {
			InheritanceType commonType = InheritanceType.getByTypeID(i);
			Collection<Entity> members = commonType.getAllMembers();
			if(members.size() != 0) {
				sb.appendFront("// copy attributes for: "
						+ formatIdentifiable(commonType) + "\n");
				boolean alreadyCasted = false;
				for(Entity member : members) {
					if(member.isConst()) {
						sb.appendFront("// is const: " + formatIdentifiable(member) + "\n");
						continue;
					}
					if(member.getType().isVoid()) {
						sb.appendFront("// is abstract: " + formatIdentifiable(member) + "\n");
						continue;
					}
					if(copiedAttribs.contains(member)) {
						sb.appendFront("// already copied: " + formatIdentifiable(member) + "\n");
						continue;
					}
					if(!alreadyCasted) {
						alreadyCasted = true;
						sb.appendFront("{\n");
						sb.indent();
						sb.appendFront(formatVarDeclWithCast(formatElementInterfaceRef(commonType), "old")
								+ "old" + kindName + ";\n");
					}
					copiedAttribs.add(member);
					String memberName = formatIdentifiable(member);
					// what's that?
					if(type.getOverriddenMember(member) != null) {
						// Workaround for Mono Bug 357287
						// "Access to hiding properties of interfaces resolves wrong member"
						// https://bugzilla.novell.com/show_bug.cgi?id=357287
						sb.appendFront("new" + kindName + ".@" + memberName
								+ " = (" + formatAttributeType(member) + ") old.@" + memberName
								+ ";   // Mono workaround (bug #357287)\n");
					} else {
						if(member.getType() instanceof MapType || member.getType() instanceof SetType 
								|| member.getType() instanceof ArrayType || member.getType() instanceof DequeType) {
							sb.appendFront("new" + kindName + ".@" + memberName
									+ " = new " + formatAttributeType(member.getType()) + "(old.@" + memberName + ");\n");
						} else {
							sb.appendFront("new" + kindName + ".@" + memberName
									+ " = old.@" + memberName + ";\n");
						}
					}
				}
				if(alreadyCasted) {
					sb.unindent();
					sb.appendFront("}\n");
				}
			}
		}
		sb.appendFront("break;\n");
		sb.unindent();
	}

	/**
	 * Generates the function info for the given function method
	 */
	private void genFunctionMethodInfo(FunctionMethod fm, InheritanceType type, String packageName) {
		String functionMethodName = formatIdentifiable(fm);
		String className = formatFunctionMethodInfoName(fm, type);

		sb.appendFront("public class " + className + " : GRGEN_LIBGR.FunctionInfo\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if (instance==null) { "
				+ "instance = new " + className + "(); } return instance; } }\n");
		sb.append("\n");

		sb.appendFront("private " + className + "()\n");
		sb.appendFront("\t\t\t: base(\n");
		sb.appendFront("\t\t\t\t\"" + functionMethodName + "\",\n");
		sb.appendFront("\t\t\t\t" + (packageName!=null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\"" + (packageName!=null ? packageName + "::" + functionMethodName : functionMethodName) + "\",\n");
		sb.appendFront("\t\t\t\tfalse,\n");
		sb.appendFront("\t\t\t\tnew String[] { ");
		for(Entity inParam : fm.getParameters()) {
			sb.append("\"" + inParam.getIdent() + "\", ");
		}
		sb.append(" },\n");
		sb.appendFront("\t\t\t\tnew GRGEN_LIBGR.GrGenType[] { ");
		for(Entity inParam : fm.getParameters()) {
			if(inParam.getType() instanceof InheritanceType  && !(inParam.getType() instanceof ExternalType)) {
				sb.append(formatTypeClassRef(inParam.getType()) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(inParam.getType()) + ")), ");
			}
		}
		sb.append(" },\n");
		Type outType = fm.getReturnType();
		if(outType instanceof InheritanceType && !(outType instanceof ExternalType)) {
			sb.appendFront("\t\t\t\t" + formatTypeClassRef(outType) + ".typeVar\n");
		} else {
			sb.appendFront("\t\t\t\tGRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(outType) + "))\n");
		}
		sb.appendFront("\t\t\t  )\n");
		sb.appendFront("{\n");
		sb.appendFront("}\n");
		
		sb.appendFront("public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)\n");
		sb.appendFront("{\n");
		sb.appendFront("\tthrow new Exception(\"Not implemented, can't call function method without this object!\");\n");
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	/**
	 * Generates the procedure info for the given procedure method
	 */
	private void genProcedureMethodInfo(ProcedureMethod pm, InheritanceType type, String packageName) {
		String procedureMethodName = formatIdentifiable(pm);
		String className = formatProcedureMethodInfoName(pm, type);

		sb.appendFront("public class " + className + " : GRGEN_LIBGR.ProcedureInfo\n");
		sb.appendFront("{\n");
		sb.unindent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.append("public static " + className + " Instance { get { if (instance==null) { "
				+ "instance = new " + className + "(); } return instance; } }\n");
		sb.append("\n");

		sb.appendFront("private " + className + "()\n");
		sb.appendFront("\t\t\t: base(\n");
		sb.appendFront("\t\t\t\t\"" + procedureMethodName + "\",\n");
		sb.appendFront("\t\t\t\t" + (packageName!=null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\"" + (packageName!=null ? packageName + "::" + procedureMethodName : procedureMethodName) + "\",\n");
		sb.appendFront("\t\t\t\tfalse,\n");
		sb.appendFront("\t\t\t\tnew String[] { ");
		for(Entity inParam : pm.getParameters()) {
			sb.append("\"" + inParam.getIdent() + "\", ");
		}
		sb.append(" },\n");
		sb.appendFront("\t\t\t\tnew GRGEN_LIBGR.GrGenType[] { ");
		for(Entity inParam : pm.getParameters()) {
			if(inParam.getType() instanceof InheritanceType && !(inParam.getType() instanceof ExternalType)) {
				sb.append(formatTypeClassRef(inParam.getType()) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(inParam.getType()) + ")), ");
			}
		}
		sb.append(" },\n");
		sb.appendFront("\t\t\t\tnew GRGEN_LIBGR.GrGenType[] { ");
		for(Type outType : pm.getReturnTypes()) {
			if(outType instanceof InheritanceType && !(outType instanceof ExternalType)) {
				sb.append(formatTypeClassRef(outType) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(outType) + ")), ");
			}
		}
		sb.append(" }\n");
		sb.appendFront("\t\t\t  )\n");
		sb.appendFront("{\n");
		sb.appendFront("}\n");
		
		sb.appendFront("public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)\n");
		sb.appendFront("{\n");
		sb.appendFront("\tthrow new Exception(\"Not implemented, can't call procedure method without this object!\");\n");
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genAttributeArrayHelpersAndComparers(InheritanceType type)
	{
		for(Entity entity : type.getAllMembers()) {
			if(entity.getType().classify() == Type.IS_BYTE
				|| entity.getType().classify() == Type.IS_SHORT
				|| entity.getType().classify() == Type.IS_INTEGER
				|| entity.getType().classify() == Type.IS_LONG
				|| entity.getType().classify() == Type.IS_FLOAT
				|| entity.getType().classify() == Type.IS_DOUBLE
				|| entity.getType().classify() == Type.IS_BOOLEAN
				|| entity.getType().classify() == Type.IS_STRING
				|| entity.getType().classify() == Type.IS_EXTERNAL_TYPE
				|| entity.getType().classify() == Type.IS_OBJECT)
			{
				if((entity.getType().classify() == Type.IS_EXTERNAL_TYPE || entity.getType().classify() == Type.IS_OBJECT)
						&& !(model.isEqualClassDefined() && model.isLowerClassDefined()))
					continue;
				if(entity.isConst())
					continue;
				genAttributeArrayHelpersAndComparers(type, entity);
			}
		}
	}

	void genAttributeArrayHelpersAndComparers(InheritanceType type, Entity entity)
	{
		String typeName = formatElementInterfaceRef(type);
		String attributeName = formatIdentifiable(entity);
		String attributeTypeName = formatAttributeType(entity.getType());
		String comparerClassName = "Comparer_" + type.getIdent().toString() + "_" + attributeName;
		String reverseComparerClassName = "ReverseComparer_" + type.getIdent().toString() + "_" + attributeName;
		
		InheritanceType nonAbstractTypeOrSubtype = null;
		if(!type.isAbstract() && type.getExternalName() == null) 
			nonAbstractTypeOrSubtype = type;
		else
		{
			for(InheritanceType subtype : type.getAllSubTypes()) {
				if(!subtype.isAbstract() && type.getExternalName() == null)
				{
					nonAbstractTypeOrSubtype = subtype;
					break;
				}
			}
		}
		
		if(nonAbstractTypeOrSubtype == null)
			return; // can't generate comparer for abstract types that have no concrete subtype

		genAttributeArrayReverseComparer(type, entity);

		sb.append("\n");
		sb.appendFront("public class " + comparerClassName + " : Comparer<" + typeName + ">\n");
		sb.appendFront("{\n");
		sb.indent();

		if(type instanceof EdgeType)
			sb.appendFront("private static " + formatElementInterfaceRef(type) + " nodeBearingAttributeForSearch = new " + formatElementClassRef(nonAbstractTypeOrSubtype) + "(null, null);\n");
		else
			sb.appendFront("private static " + formatElementInterfaceRef(type) + " nodeBearingAttributeForSearch = new " + formatElementClassRef(nonAbstractTypeOrSubtype) + "();\n");
		
		sb.appendFront("private static " + comparerClassName + " thisComparer = new " + comparerClassName + "();\n");
		
		genCompareMethod(typeName, entity);

		genIndexOfByMethod(typeName, attributeName, attributeTypeName);
		genIndexOfByWithStartMethod(typeName, attributeName, attributeTypeName);

		genLastIndexOfByMethod(typeName, attributeName, attributeTypeName);
		genLastIndexOfByWithStartMethod(typeName, attributeName, attributeTypeName);

		genIndexOfOrderedByMethod(typeName, attributeName, attributeTypeName);

		genArrayOrderAscendingByMethod(typeName);
		genArrayOrderDescendingByMethod(typeName, reverseComparerClassName);

		generateArrayKeepOneForEach(sb, "ArrayKeepOneForEachBy", typeName, attributeName, attributeTypeName);

		genArrayExtractMethod(typeName, attributeName, attributeTypeName);
		
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genCompareMethod(String typeName, Entity entity)
	{
		String attributeName = formatIdentifiable(entity);
			
		sb.appendFront("public override int Compare(" + typeName + " a, " + typeName + " b)\n");
		sb.appendFront("{\n");
		sb.indent();
		if(entity.getType().classify()==Type.IS_EXTERNAL_TYPE || entity.getType().classify()==Type.IS_OBJECT) {
			sb.appendFront("if(AttributeTypeObjectCopierComparer.IsEqual(a, b)) return 0;\n");
			sb.appendFront("if(AttributeTypeObjectCopierComparer.IsLower(a, b)) return -1;\n");
			sb.appendFront("return 1;\n");
		}
		else if(entity.getType() instanceof StringType)
			sb.appendFront("return StringComparer.InvariantCulture.Compare(a.@" + attributeName + ", b.@" + attributeName + ");\n");
		else
			sb.appendFront("return a.@" + attributeName + ".CompareTo(b.@" + attributeName + ");\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	void genIndexOfByMethod(String typeName, String attributeName, String attributeTypeName)
	{
		sb.appendFront("public static int IndexOfBy(IList<" + typeName + "> list, " + attributeTypeName + " entry)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("for(int i = 0; i < list.Count; ++i)\n");
		sb.appendFront("\tif(list[i].@" + attributeName + ".Equals(entry))\n");
		sb.appendFront("\t\treturn i;\n");
		sb.appendFront("return -1;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	void genIndexOfByWithStartMethod(String typeName, String attributeName, String attributeTypeName)
	{
		sb.appendFront("public static int IndexOfBy(IList<" + typeName + "> list, " + attributeTypeName + " entry, int startIndex)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("for(int i = startIndex; i < list.Count; ++i)\n");
		sb.appendFront("\tif(list[i].@" + attributeName + ".Equals(entry))\n");
		sb.appendFront("\t\treturn i;\n");
		sb.appendFront("return -1;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	void genLastIndexOfByMethod(String typeName, String attributeName, String attributeTypeName)
	{
		sb.appendFront("public static int LastIndexOfBy(IList<" + typeName + "> list, " + attributeTypeName + " entry)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("for(int i = list.Count - 1; i >= 0; --i)\n");
		sb.appendFront("\tif(list[i].@" + attributeName + ".Equals(entry))\n");
		sb.appendFront("\t\treturn i;\n");
		sb.appendFront("return -1;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	void genLastIndexOfByWithStartMethod(String typeName, String attributeName, String attributeTypeName)
	{
		sb.appendFront("public static int LastIndexOfBy(IList<" + typeName + "> list, " + attributeTypeName + " entry, int startIndex)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("for(int i = startIndex; i >= 0; --i)\n");
		sb.appendFront("\tif(list[i].@" + attributeName + ".Equals(entry))\n");
		sb.appendFront("\t\treturn i;\n");
		sb.appendFront("return -1;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	void genIndexOfOrderedByMethod(String typeName, String attributeName, String attributeTypeName)
	{
		sb.appendFront("public static int IndexOfOrderedBy(List<" + typeName + "> list, " + attributeTypeName + " entry)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("nodeBearingAttributeForSearch.@" + attributeName + " = entry;\n");
		sb.appendFront("return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	void genArrayOrderAscendingByMethod(String typeName)
	{
		sb.appendFront("public static List<" + typeName + "> ArrayOrderAscendingBy(List<" + typeName + "> list)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("List<" + typeName + "> newList = new List<" + typeName + ">(list);\n");
		sb.appendFront("newList.Sort(thisComparer);\n");
		sb.appendFront("return newList;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	void genArrayOrderDescendingByMethod(String typeName, String reverseComparerClassName)
	{
		sb.appendFront("public static List<" + typeName + "> ArrayOrderDescendingBy(List<" + typeName + "> list)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("List<" + typeName + "> newList = new List<" + typeName + ">(list);\n");
		sb.appendFront("newList.Sort(" + reverseComparerClassName + ".thisComparer);\n");
		sb.appendFront("return newList;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	void genArrayExtractMethod(String typeName, String attributeName, String attributeTypeName)
	{
		sb.appendFront("public static List<" + attributeTypeName + "> Extract(List<" + typeName + "> list)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("List<" + attributeTypeName + "> resultList = new List<" + attributeTypeName + ">(list.Count);\n");
		sb.appendFront("foreach(" + typeName + " entry in list)\n");
		sb.appendFront("\tresultList.Add(entry.@" + attributeName + ");\n");
		sb.appendFront("return resultList;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	void genAttributeArrayReverseComparer(InheritanceType type, Entity entity)
	{
		String typeName = formatElementInterfaceRef(type);
		String attributeName = formatIdentifiable(entity);
		String reverseComparerClassName = "ReverseComparer_" + type.getIdent().toString() + "_" + attributeName;

		sb.append("\n");
		sb.appendFront("public class " + reverseComparerClassName + " : Comparer<" + typeName + ">\n");
		sb.appendFront("{\n");
		sb.indent();
		
		sb.appendFront("public static " + reverseComparerClassName + " thisComparer = new " + reverseComparerClassName + "();\n");
		
		genCompareMethodReverse(typeName, entity);
		
		sb.unindent();
		sb.appendFront("}\n");
	}

	void genCompareMethodReverse(String typeName, Entity entity)
	{
		String attributeName = formatIdentifiable(entity);

		sb.appendFront("public override int Compare(" + typeName + " a, " + typeName + " b)\n");
		sb.appendFront("{\n");
		sb.indent();
		if(entity.getType().classify()==Type.IS_EXTERNAL_TYPE || entity.getType().classify()==Type.IS_OBJECT) {
			sb.appendFront("if(AttributeTypeObjectCopierComparer.IsEqual(a, b)) return 0;\n");
			sb.appendFront("if(AttributeTypeObjectCopierComparer.IsLower(a, b)) return 1;\n");
			sb.appendFront("return -1;\n");
		}
		else if(entity.getType() instanceof StringType)
			sb.appendFront("return -StringComparer.InvariantCulture.Compare(a.@" + attributeName + ", b.@" + attributeName + ");\n");
		else
			sb.appendFront("return -a.@" + attributeName + ".CompareTo(b.@" + attributeName + ");\n");
		sb.unindent();
		sb.appendFront("}\n");
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
		String graphElementType = index instanceof AttributeIndex ? formatElementInterfaceRef(((AttributeIndex)index).type) : formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());
		if(index instanceof AttributeIndex) {
			sb.appendFront("interface Index" + indexName + " : GRGEN_LIBGR.IAttributeIndex\n");
		} else if(index instanceof IncidenceCountIndex) {
			sb.appendFront("interface Index" + indexName + " : GRGEN_LIBGR.IIncidenceCountIndex\n");
		}
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("IEnumerable<" + graphElementType + "> Lookup(" + lookupType + " fromto);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscending();\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromInclusive(" + lookupType + " from);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromExclusive(" + lookupType + " from);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscendingToInclusive(" + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscendingToExclusive(" + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromInclusiveToInclusive(" + lookupType + " from, " + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromInclusiveToExclusive(" + lookupType + " from, " + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromExclusiveToInclusive(" + lookupType + " from, " + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupAscendingFromExclusiveToExclusive(" + lookupType + " from, " + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescending();\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromInclusive(" + lookupType + " from);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromExclusive(" + lookupType + " from);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescendingToInclusive(" + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescendingToExclusive(" + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromInclusiveToInclusive(" + lookupType + " from, " + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromInclusiveToExclusive(" + lookupType + " from, " + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromExclusiveToInclusive(" + lookupType + " from, " + lookupType + " to);\n");
		sb.appendFront("IEnumerable<" + graphElementType + "> LookupDescendingFromExclusiveToExclusive(" + lookupType + " from, " + lookupType + " to);\n");
		if(index instanceof IncidenceCountIndex) {
			sb.appendFront("int GetIncidenceCount(" + graphElementType + " element);\n");
		}
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genIndexImplementations() {
		int i=0;
		for(Index index : model.getIndices()) {
			if(index instanceof AttributeIndex) {
				genIndexImplementation((AttributeIndex)index, i);
			} else {
				genIndexImplementation((IncidenceCountIndex)index, i);
			}
			++i;
		}
	}

	void genIndexImplementation(AttributeIndex index, int indexNum) {
		String indexName = index.getIdent().toString();
		String graphElementType = formatElementInterfaceRef(index.type);
		String modelName = model.getIdent().toString() + "GraphModel";
		sb.appendFront("public class Index" + indexName + "Impl : Index" + indexName + "\n");
		sb.appendFront("{\n");
		sb.indent();
		
		sb.appendFront("public GRGEN_LIBGR.IndexDescription Description { get { return " + modelName + ".GetIndexDescription(" + indexNum + "); } }\n");
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
		
		sb.appendFront("public void FillAsClone(Index" + indexName + "Impl that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("root = FillAsClone(that.root, that.bottom, oldToNewMap);\n");
		sb.appendFront("count = that.count;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
		
		sb.appendFront("protected TreeNode FillAsClone(TreeNode that, TreeNode otherBottom, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
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
		String graphElementType = index instanceof AttributeIndex ? formatElementInterfaceRef(((AttributeIndex)index).type) : formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());
		
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
		
		sb.appendFront("private IEnumerable<" + graphElementType + "> Lookup(TreeNode current, " + attributeType + " fromto)\n");
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
		else if(index.entity.getType() instanceof StringType)
			sb.appendFront("if(String.Compare(current.value." + attributeName + ", fromto, StringComparison.InvariantCulture)>=0)\n");
		else
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
		else if(index.entity.getType() instanceof StringType)
			sb.appendFront("if(String.Compare(current.value." + attributeName + ", fromto, StringComparison.InvariantCulture)<=0)\n");
		else
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

	void genAscendingEntry(Index index, boolean fromConstrained, boolean fromInclusive, boolean toConstrained, boolean toInclusive)
	{
		String attributeType = index instanceof AttributeIndex ? formatAttributeType(((AttributeIndex)index).entity) : "int";
		String graphElementType = index instanceof AttributeIndex ? formatElementInterfaceRef(((AttributeIndex)index).type) : formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());

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
		
		sb.appendFront("private IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix + "(TreeNode current");
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
			if(index.entity.getType() instanceof BooleanType)
				sb.appendFront("if(current.value." + attributeName + ".CompareTo(from)" + (fromInclusive ? " >= " : " > ") + "0)\n");
			else if(index.entity.getType() instanceof StringType)
				sb.appendFront("if(String.Compare(current.value." + attributeName + ", from, StringComparison.InvariantCulture)" + (fromInclusive ? " >= " : " > ") + "0)\n");
			else
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
			if(index.entity.getType() instanceof BooleanType)
				sb.appendFront("if(current.value." + attributeName + ".CompareTo(to)" + (toInclusive ? " <= " : " < ") + "0)\n");
			else if(index.entity.getType() instanceof StringType)
				sb.appendFront("if(String.Compare(current.value." + attributeName + ", to, StringComparison.InvariantCulture)" + (toInclusive ? " <= " : " < ") + "0)\n");
			else
				sb.appendFront("if(current.value." + attributeName + (toInclusive ? " <= " : " < ") + "to)\n");
		}
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("foreach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.right");
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

	void genDescendingEntry(Index index, boolean fromConstrained, boolean fromInclusive, boolean toConstrained, boolean toInclusive)
	{
		String attributeType = index instanceof AttributeIndex ? formatAttributeType(((AttributeIndex)index).entity) : "int";
		String graphElementType = index instanceof AttributeIndex ? formatElementInterfaceRef(((AttributeIndex)index).type) : formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());

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
		
		sb.appendFront("private IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix + "(TreeNode current");
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
			if(index.entity.getType() instanceof BooleanType)
				sb.appendFront("if(current.value." + attributeName + ".CompareTo(from)" + (fromInclusive ? " <= " : " < ") + "0)\n");
			else if(index.entity.getType() instanceof StringType)
				sb.appendFront("if(String.Compare(current.value." + attributeName + ", from, StringComparison.InvariantCulture)" + (fromInclusive ? " <= " : " < ") + "0)\n");
			else
				sb.appendFront("if(current.value." + attributeName + (fromInclusive ? " <= " : " < ") + "from)\n");
		}
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("foreach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.right");
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
			if(index.entity.getType() instanceof BooleanType)
				sb.appendFront("if(current.value." + attributeName + ".CompareTo(to)" + (toInclusive ? " >= " : " > ") + "0)\n");
			else if(index.entity.getType() instanceof StringType)
				sb.appendFront("if(String.Compare(current.value." + attributeName + ", to, StringComparison.InvariantCulture)" + (toInclusive ? " >= " : " > ") + "0)\n");
			else
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
		sb.appendFront("\tInsert(ref root, (" + graphElementType + ")elem, ((" + graphElementType + ")elem)." + attributeName + ");\n");
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
		sb.appendFront("void ChangingAttribute(GRGEN_LIBGR.IGraphElement elem, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.AttributeChangeType changeType, Object newValue, Object keyValue)\n");
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
		sb.appendFront("\tInsert(ref root, (" + graphElementType + ")newElem, ((" + graphElementType + ")newElem)." + attributeName + ");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genIndexAATreeBalancingInsertionDeletion(AttributeIndex index) {
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
		
		sb.appendFront("private void Insert(ref TreeNode current, " + graphElementType + " value, " + attributeType + " attributeValue)\n");
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
		else if(index.entity.getType() instanceof StringType)
			sb.appendFront("if(String.Compare(attributeValue, current.value." + attributeName + ", StringComparison.InvariantCulture)<0");
		else
			sb.appendFront("if(attributeValue < current.value." + attributeName);
		sb.append(" || ( attributeValue == current.value." + attributeName + " && (value" + castForUnique + ").uniqueId < (current.value" + castForUnique + ").uniqueId ) )\n");
		sb.appendFront("\tInsert(ref current.left, value, attributeValue);\n");
		if(index.entity.getType() instanceof BooleanType)
			sb.appendFront("else if(attributeValue.CompareTo(current.value." + attributeName + ")>0");
		else if(index.entity.getType() instanceof StringType)
			sb.appendFront("else if(String.Compare(attributeValue, current.value." + attributeName + ", StringComparison.InvariantCulture)>0");
		else
			sb.appendFront("else if(attributeValue > current.value." + attributeName);
		sb.append(" || ( attributeValue == current.value." + attributeName + " && (value" + castForUnique + ").uniqueId > (current.value" + castForUnique + ").uniqueId ) )\n");
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
		else if(index.entity.getType() instanceof StringType)
			sb.appendFront("if(String.Compare(value." + attributeName + ", current.value." + attributeName + ", StringComparison.InvariantCulture)<0");
		else
			sb.appendFront("if(value." + attributeName + " < current.value." + attributeName);
		sb.append(" || ( value." + attributeName + " == current.value." + attributeName + " && (value" + castForUnique + ").uniqueId < (current.value" + castForUnique + ").uniqueId ) )\n");
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
		sb.appendFront("if(current == last && deleted != bottom && value." + attributeName + " == deleted.value." + attributeName);
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

	void genIndexSetType() {
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
			sb.appendFront("public Index" + indexName + "Impl " + indexName +";\n");
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
		
		sb.appendFront("public void FillAsClone(GRGEN_LGSP.LGSPGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
		sb.appendFront("{\n");
		sb.indent();
		for(Index index : model.getIndices()) {
			String indexName = index.getIdent().toString();
			sb.appendFront(indexName + ".FillAsClone((Index" + indexName + "Impl)originalGraph.Indices.GetIndex(\"" + indexName + "\"), oldToNewMap);\n");
		}
		sb.unindent();
		sb.appendFront("}\n");
		
		sb.unindent();
		sb.appendFront("}\n");
	}
	
	void genIndexImplementation(IncidenceCountIndex index, int indexNum) {
		String indexName = index.getIdent().toString();
		String graphElementType = formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());
		String modelName = model.getIdent().toString() + "GraphModel";
		sb.appendFront("public class Index" + indexName + "Impl : Index" + indexName + "\n");
		sb.appendFront("{\n");
		sb.indent();
		
		sb.appendFront("public GRGEN_LIBGR.IndexDescription Description { get { return " + modelName + ".GetIndexDescription(" + indexNum + "); } }\n");
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
		sb.appendFront("protected IDictionary<" + graphElementType + ", int> nodeToIncidenceCount = new Dictionary<" + graphElementType + ", int>();\n");
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

		sb.appendFront("public void FillAsClone(Index" + indexName + "Impl that, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
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

		sb.appendFront("protected TreeNode FillAsClone(TreeNode that, TreeNode otherBottom, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)\n");
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

	void genAscending(IncidenceCountIndex index, boolean fromConstrained, boolean fromInclusive, boolean toConstrained, boolean toInclusive)
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
		
		sb.appendFront("private IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix + "(TreeNode current");
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
		sb.appendFront("foreach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.right");
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

	void genDescending(IncidenceCountIndex index, boolean fromConstrained, boolean fromInclusive, boolean toConstrained, boolean toInclusive)
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
		
		sb.appendFront("private IEnumerable<" + graphElementType + "> Lookup" + lookupMethodNameAppendix + "(TreeNode current");
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
		sb.appendFront("foreach(" + graphElementType + " value in Lookup" + lookupMethodNameAppendix + "(current.right");
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
		sb.appendFront("IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType> incidentEdges = GRGEN_LIBGR.GraphHelper.Incident(oldNode, " + incidentEdgeTypeType + ", graph.Model.NodeModel.RootType);\n");
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

		if(index.Direction()==IncidentEdgeExpr.OUTGOING) {
			sb.appendFront("if(source is " + startNodeType + " && target is " + adjacentNodeType + ") {\n");
			sb.indent();
			sb.appendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], (" + startNodeType + ")source);\n");
			sb.appendFront("nodeToIncidenceCount[(" + startNodeType + ")source] = nodeToIncidenceCount[(" + startNodeType + ")source] + 1;\n");
			sb.appendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], (" + startNodeType + ")source);\n");
			sb.unindent();
			sb.appendFront("}\n");
		} else if(index.Direction()==IncidentEdgeExpr.INCOMING) {
			sb.appendFront("if(target is " + startNodeType + " && source is " + adjacentNodeType + ") {\n");
			sb.indent();
			sb.appendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], (" + startNodeType + ")target);\n");
			sb.appendFront("nodeToIncidenceCount[(" + startNodeType + ")target] = nodeToIncidenceCount[(" + startNodeType + ")target] + 1;\n");
			sb.appendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], (" + startNodeType + ")target);\n");
			sb.unindent();
			sb.appendFront("}\n");
		} else {
			sb.appendFront("if(source is " + startNodeType + " && target is " + adjacentNodeType + ") {\n");
			sb.indent();
			sb.appendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], (" + startNodeType + ")source);\n");
			sb.appendFront("nodeToIncidenceCount[(" + startNodeType + ")source] = nodeToIncidenceCount[(" + startNodeType + ")source] + 1;\n");
			sb.appendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], (" + startNodeType + ")source);\n");
			sb.unindent();
			sb.appendFront("}\n");
			sb.appendFront("if(target is " + startNodeType + " && source is " + adjacentNodeType + " && source!=target) {\n");
			sb.indent();
			sb.appendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], (" + startNodeType + ")target);\n");
			sb.appendFront("nodeToIncidenceCount[(" + startNodeType + ")target] = nodeToIncidenceCount[(" + startNodeType + ")target] + 1;\n");
			sb.appendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], (" + startNodeType + ")target);\n");
			sb.unindent();
			sb.appendFront("}\n");
		}		
	}

	void genIndexMaintainingRemovingEdge(IncidenceCountIndex index)
	{
		String startNodeType = formatElementInterfaceRef(((IncidenceCountIndex)index).getStartNodeType());
		String adjacentNodeType = formatElementInterfaceRef(((IncidenceCountIndex)index).getAdjacentNodeType());
	
		if(index.Direction()==IncidentEdgeExpr.OUTGOING) {
			sb.appendFront("if(source is " + startNodeType + " && target is " + adjacentNodeType + ") {\n");
			sb.indent();
			sb.appendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], (" + startNodeType + ")source);\n");
			sb.appendFront("nodeToIncidenceCount[(" + startNodeType + ")source] = nodeToIncidenceCount[(" + startNodeType + ")source] - 1;\n");
			sb.appendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], (" + startNodeType + ")source);\n");
			sb.unindent();
			sb.appendFront("}\n");
		} else if(index.Direction()==IncidentEdgeExpr.INCOMING) {
			sb.appendFront("if(target is " + startNodeType + " && source is " + adjacentNodeType + ") {\n");
			sb.indent();
			sb.appendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], (" + startNodeType + ")target);\n");
			sb.appendFront("nodeToIncidenceCount[(" + startNodeType + ")target] = nodeToIncidenceCount[(" + startNodeType + ")target] - 1;\n");
			sb.appendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], (" + startNodeType + ")target);\n");
			sb.unindent();
			sb.appendFront("}\n");
		} else {
			sb.appendFront("if(source is " + startNodeType + " && target is " + adjacentNodeType + ") {\n");
			sb.indent();
			sb.appendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], (" + startNodeType + ")source);\n");
			sb.appendFront("nodeToIncidenceCount[(" + startNodeType + ")source] = nodeToIncidenceCount[(" + startNodeType + ")source] - 1;\n");
			sb.appendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")source], (" + startNodeType + ")source);\n");
			sb.unindent();
			sb.appendFront("}\n");
			sb.appendFront("if(target is " + startNodeType + " && source is " + adjacentNodeType + " && source!=target) {\n");
			sb.indent();
			sb.appendFront("Delete(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], (" + startNodeType + ")target);\n");
			sb.appendFront("nodeToIncidenceCount[(" + startNodeType + ")target] = nodeToIncidenceCount[(" + startNodeType + ")target] - 1;\n");
			sb.appendFront("Insert(ref root, nodeToIncidenceCount[(" + startNodeType + ")target], (" + startNodeType + ")target);\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	void genIndexAATreeBalancingInsertionDeletion(IncidenceCountIndex index) {
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
		sb.appendFront(" || ( key == current.key && (value" + castForUnique + ").uniqueId < (current.value" + castForUnique + ").uniqueId ) )\n");
		sb.appendFront("\tInsert(ref current.left, key, value);\n");
		sb.appendFront("else if(key > current.key");
		sb.appendFront(" || ( key == current.key && (value" + castForUnique + ").uniqueId > (current.value" + castForUnique + ").uniqueId ) )\n");
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
		sb.append(" || ( key == current.key && (value" + castForUnique + ").uniqueId < (current.value" + castForUnique + ").uniqueId ) )\n");
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
		sb.appendFront(" && (value" + castForUnique + ").uniqueId == (deleted.value" + castForUnique + ").uniqueId )\n");
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
	
	////////////////////////////
	// Model class generation //
	////////////////////////////

	/**
	 * Generates the model class for the edge or node types.
	 */
	private void genModelClass(Collection<? extends InheritanceType> types, boolean isNode) {
		String kindStr = isNode ? "Node" : "Edge";

		sb.appendFront("//\n");
		sb.appendFront("// " + formatNodeOrEdge(isNode) + " model\n");
		sb.appendFront("//\n");
		sb.append("\n");
		sb.appendFront("public sealed class " + model.getIdent() + formatNodeOrEdge(isNode)
				+ "Model : GRGEN_LIBGR.I" + kindStr + "Model\n");
		sb.appendFront("{\n");
		sb.indent();

		InheritanceType rootType = genModelConstructor(isNode, types);

		sb.appendFront("public bool IsNodeModel { get { return " + (isNode?"true":"false") +"; } }\n");
		sb.appendFront("public GRGEN_LIBGR." + kindStr + "Type RootType { get { return "
				+ formatTypeClassRef(rootType) + ".typeVar; } }\n");
		sb.appendFront("GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.RootType { get { return "
				+ formatTypeClassRef(rootType) + ".typeVar; } }\n");
		sb.appendFront("public GRGEN_LIBGR." + kindStr + "Type GetType(string name)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("switch(name)\n");
		sb.appendFront("{\n");
		sb.indent();
		for(InheritanceType type : types) {
			sb.appendFront("case \"" + getPackagePrefixDoubleColon(type) + formatIdentifiable(type) + "\" : return " + formatTypeClassRef(type) + ".typeVar;\n");
		}
		sb.unindent();
		sb.appendFront("}\n");
		sb.appendFront("return null;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.appendFront("GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(string name)\n");
		sb.appendFront("{\n");
		sb.appendFront("\treturn GetType(name);\n");
		sb.appendFront("}\n");

		sb.appendFront("private GRGEN_LIBGR." + kindStr + "Type[] types = {\n");
		sb.indent();
		for(InheritanceType type : types) {
			sb.appendFront(formatTypeClassRef(type) + ".typeVar,\n");
		}
		sb.unindent();
		sb.appendFront("};\n");
		sb.appendFront("public GRGEN_LIBGR." + kindStr + "Type[] Types { get { return types; } }\n");
		sb.appendFront("GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types "
				+ "{ get { return types; } }\n");

		sb.appendFront("private System.Type[] typeTypes = {\n");
		sb.indent();
		for(InheritanceType type : types) {
			sb.appendFront("typeof(" + formatTypeClassRef(type) + "),\n");
		}
		sb.unindent();
		sb.appendFront("};\n");
		sb.appendFront("public System.Type[] TypeTypes { get { return typeTypes; } }\n");

		sb.appendFront("private GRGEN_LIBGR.AttributeType[] attributeTypes = {\n");
		sb.indent();
		for(InheritanceType type : types) {
			String ctype = formatTypeClassRef(type);
			for(Entity member : type.getMembers()) {
				sb.appendFront(ctype + "." + formatAttributeTypeName(member) + ",\n");
			}
		}
		sb.unindent();
		sb.appendFront("};\n");
		sb.appendFront("public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes "
				+ "{ get { return attributeTypes; } }\n");

		sb.unindent();
		sb.appendFront("}\n");
	}

	private InheritanceType genModelConstructor(boolean isNode, Collection<? extends InheritanceType> types) {
		String kindStr = (isNode ? "Node" : "Edge");
		InheritanceType rootType = null;

		sb.appendFront("public " + model.getIdent() + formatNodeOrEdge(isNode) + "Model()\n");
		sb.appendFront("{\n");
		sb.indent();
		for(InheritanceType type : types) {
			String ctype = formatTypeClassRef(type);
			sb.appendFront(ctype + ".typeVar.subOrSameGrGenTypes = "
					+ ctype + ".typeVar.subOrSameTypes = new GRGEN_LIBGR."
					+ kindStr + "Type[] {\n");
			sb.indent();
			sb.appendFront(ctype + ".typeVar,\n");
			for(InheritanceType otherType : types) {
				if(type != otherType && otherType.isCastableTo(type))
					sb.appendFront(formatTypeClassRef(otherType) + ".typeVar,\n");
			}
			sb.unindent();
			sb.appendFront("};\n");

			sb.appendFront(ctype + ".typeVar.directSubGrGenTypes = "
					+ ctype + ".typeVar.directSubTypes = new GRGEN_LIBGR."
					+ kindStr + "Type[] {\n");
			sb.indent();
			for(InheritanceType subType : type.getDirectSubTypes()) {
				// TODO: HACK, because direct sub types may also contain types from other models...
				if(!types.contains(subType))
					continue;
				sb.appendFront(formatTypeClassRef(subType) + ".typeVar,\n");
			}
			sb.unindent();
			sb.appendFront("};\n");

			sb.appendFront(ctype + ".typeVar.superOrSameGrGenTypes = "
					+ ctype + ".typeVar.superOrSameTypes = new GRGEN_LIBGR."
					+ kindStr + "Type[] {\n");
			sb.indent();
			sb.appendFront(ctype + ".typeVar,\n");
			for(InheritanceType otherType : types) {
				if(type != otherType && type.isCastableTo(otherType))
					sb.appendFront(formatTypeClassRef(otherType) + ".typeVar,\n");
			}
			sb.unindent();
			sb.appendFront("};\n");

			sb.appendFront(ctype + ".typeVar.directSuperGrGenTypes = "
					+ ctype + ".typeVar.directSuperTypes = new GRGEN_LIBGR."
					+ kindStr + "Type[] {\n");
			sb.indent();
			for(InheritanceType superType : type.getDirectSuperTypes()) {
				sb.appendFront(formatTypeClassRef(superType) + ".typeVar,\n");
			}
			sb.unindent();
			sb.appendFront("};\n");

			if(type.isRoot())
				rootType = type;
		}
		sb.unindent();
		sb.appendFront("}\n");

		return rootType;
	}

	/**
	 * Generates the graph model class.
	 */
	private void genGraphModel() {
		String modelName = model.getIdent().toString();
		sb.appendFront("//\n");
		sb.appendFront("// IGraphModel (LGSPGraphModel) implementation\n");
		sb.appendFront("//\n");

		sb.appendFront("public sealed class " + modelName + "GraphModel : GRGEN_LGSP.LGSPGraphModel\n");
		sb.appendFront("{\n");
		sb.indent();
		
		sb.appendFront("public " + modelName + "GraphModel()\n");
		sb.appendFront("{\n");
		sb.appendFront("\tFullyInitializeExternalTypes();\n");
		sb.appendFront("}\n");
		sb.append("\n");

		genGraphModelBody(modelName);

		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genGraphClass() {
		String modelName = model.getIdent().toString();

		sb.appendFront("//\n");
		sb.appendFront("// IGraph (LGSPGraph) implementation\n");
		sb.appendFront("//\n");

		sb.appendFront("public class " + modelName + "Graph : GRGEN_LGSP.LGSPGraph\n");
		sb.appendFront("{\n");
		sb.indent();
		
		sb.appendFront("public " + modelName + "Graph() : base(new " + modelName + "GraphModel(), GetGraphName())\n");
		sb.appendFront("{\n");
		sb.appendFront("}\n");
		sb.append("\n");

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

		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genNamedGraphClass() {
		String modelName = model.getIdent().toString();

		sb.appendFront("//\n");
		sb.appendFront("// INamedGraph (LGSPNamedGraph) implementation\n");
		sb.appendFront("//\n");

		sb.appendFront("public class " + modelName + "NamedGraph : GRGEN_LGSP.LGSPNamedGraph\n");
		sb.appendFront("{\n");
		sb.indent();
		
		sb.appendFront("public " + modelName + "NamedGraph() : base(new " + modelName + "GraphModel(), GetGraphName(), 0)\n");
		sb.appendFront("{\n");
		sb.appendFront("}\n");
		sb.append("\n");

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

		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genCreateNodeConvenienceHelper(NodeType nodeType, boolean isNamed) {
		if(nodeType.isAbstract())
			return;

		String name = getPackagePrefix(nodeType) + formatIdentifiable(nodeType);
		String elemref = formatElementClassRef(nodeType);
		sb.appendFront("public " + elemref + " CreateNode" + name + "()\n");
		sb.appendFront("{\n");
		sb.appendFront("\treturn " + elemref + ".CreateNode(this);\n");
		sb.appendFront("}\n");
		sb.append("\n");
		
		if(!isNamed)
			return;

		sb.appendFront("public " + elemref + " CreateNode" + name + "(string nodeName)\n");
		sb.appendFront("{\n");
		sb.appendFront("\treturn " + elemref + ".CreateNode(this, nodeName);\n");
		sb.appendFront("}\n");
		sb.append("\n");
	}

	private void genCreateEdgeConvenienceHelper(EdgeType edgeType, boolean isNamed) {
		if(edgeType.isAbstract())
			return;
		
		String name = getPackagePrefix(edgeType) + formatIdentifiable(edgeType);
		String elemref = formatElementClassRef(edgeType);
		sb.appendFront("public @" + elemref + " CreateEdge" + name);
		sb.append("(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)\n");
		sb.appendFront("{\n");
		sb.appendFront("\treturn @" + elemref + ".CreateEdge(this, source, target);\n");
		sb.appendFront("}\n");
		sb.append("\n");

		if(!isNamed)
			return;

		sb.appendFront("public @" + elemref + " CreateEdge" + name);
		sb.append("(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)\n");
		sb.appendFront("{\n");
		sb.appendFront("\treturn @" + elemref + ".CreateEdge(this, source, target, edgeName);\n");
		sb.appendFront("}\n");
		sb.append("\n");
	}

	private void genGraphModelBody(String modelName) {
		sb.appendFront("private " + modelName + "NodeModel nodeModel = new " + modelName + "NodeModel();\n");
		sb.appendFront("private " + modelName + "EdgeModel edgeModel = new " + modelName + "EdgeModel();\n");

		genPackages();
		genEnumAttributeTypes();
		genValidates();
		genIndexDescriptions();
		genIndicesGraphBinding();
		sb.append("\n");
		
		sb.appendFront("public override string ModelName { get { return \"" + modelName + "\"; } }\n");
		
		sb.appendFront("public override GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }\n");
		sb.appendFront("public override GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }\n");
		
		sb.appendFront("public override IEnumerable<string> Packages "
				+ "{ get { return packages; } }\n");
		sb.appendFront("public override IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes "
				+ "{ get { return enumAttributeTypes; } }\n");
		sb.appendFront("public override IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo "
				+ "{ get { return validateInfos; } }\n");
		sb.appendFront("public override IEnumerable<GRGEN_LIBGR.IndexDescription> IndexDescriptions "
				+ "{ get { return indexDescriptions; } }\n");
		sb.appendFront("public static GRGEN_LIBGR.IndexDescription GetIndexDescription(int i) "
				+ "{ return indexDescriptions[i]; }\n");
		sb.appendFront("public static GRGEN_LIBGR.IndexDescription GetIndexDescription(string indexName)\n ");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("for(int i=0; i<indexDescriptions.Length; ++i)\n");
		sb.appendFront("\tif(indexDescriptions[i].Name==indexName)\n");
		sb.appendFront("\t\treturn indexDescriptions[i];\n");
		sb.appendFront("return null;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.appendFront("public override bool GraphElementUniquenessIsEnsured { get { return " + (model.isUniqueDefined() ? "true" : "false") + "; } }\n");
		sb.appendFront("public override bool GraphElementsAreAccessibleByUniqueId { get { return " + (model.isUniqueIndexDefined() ? "true" : "false") + "; } }\n");
		sb.appendFront("public override bool AreFunctionsParallelized { get { return " + model.areFunctionsParallel() + "; } }\n");
		sb.appendFront("public override int BranchingFactorForEqualsAny { get { return " + model.isoParallel() + "; } }\n");
		sb.append("\n");
        
		if(model.isEmitClassDefined()) {
			sb.appendFront("public override object Parse(TextReader reader, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
			sb.appendFront("{\n");
			sb.appendFront("\treturn AttributeTypeObjectEmitterParser.Parse(reader, attrType, graph);\n");
			sb.appendFront("}\n");
			sb.appendFront("public override string Serialize(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
			sb.appendFront("{\n");
			sb.appendFront("\treturn AttributeTypeObjectEmitterParser.Serialize(attribute, attrType, graph);\n");
			sb.appendFront("}\n");
			sb.appendFront("public override string Emit(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
			sb.appendFront("{\n");
			sb.appendFront("\treturn AttributeTypeObjectEmitterParser.Emit(attribute, attrType, graph);\n");
			sb.appendFront("}\n");
			sb.appendFront("public override void External(string line, GRGEN_LIBGR.IGraph graph)\n");
			sb.appendFront("{\n");
			sb.appendFront("\tAttributeTypeObjectEmitterParser.External(line, graph);\n");
			sb.appendFront("}\n");
		}
		if(model.isEmitGraphClassDefined()) {
			sb.appendFront("public override GRGEN_LIBGR.INamedGraph AsGraph(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
			sb.appendFront("{\n");
			sb.appendFront("\treturn AttributeTypeObjectEmitterParser.AsGraph(attribute, attrType, graph);\n");
			sb.appendFront("}\n\n");
		}
			
		genExternalTypes();
		sb.appendFront("public override GRGEN_LIBGR.ExternalType[] ExternalTypes { get { return externalTypes; } }\n\n");

		sb.appendFront("private void FullyInitializeExternalTypes()\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("externalType_object.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { } );\n");
		for(ExternalType et : model.getExternalTypes()) {
			sb.appendFront("externalType_" + et.getIdent() + ".InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { ");
			boolean directSupertypeAvailable = false;
			for(InheritanceType superType : et.getDirectSuperTypes()) {
				sb.append("externalType_" + superType.getIdent() + ", ");
				directSupertypeAvailable = true;
			}
			if(!directSupertypeAvailable)
				sb.append("externalType_object ");
			sb.append("} );\n");
		}
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");

		if(model.isEqualClassDefined() && model.isLowerClassDefined()) {
			sb.appendFront("public override bool IsEqualClassDefined { get { return true; } }\n");
			sb.appendFront("public override bool IsLowerClassDefined { get { return true; } }\n");
			sb.appendFront("public override bool IsEqual(object this_, object that)\n");
			sb.appendFront("{\n");
			sb.appendFront("\treturn AttributeTypeObjectCopierComparer.IsEqual(this_, that);\n");
			sb.appendFront("}\n");
			sb.appendFront("public override bool IsLower(object this_, object that)\n");
			sb.appendFront("{\n");
			sb.appendFront("\treturn AttributeTypeObjectCopierComparer.IsLower(this_, that);\n");
			sb.appendFront("}\n\n");
		} else if(model.isEqualClassDefined()) {
			sb.appendFront("public override bool IsEqualClassDefined { get { return true; } }\n");
			sb.appendFront("public override bool IsEqual(object this_, object that)\n");
			sb.appendFront("{\n");
			sb.appendFront("\treturn AttributeTypeObjectCopierComparer.IsEqual(this_, that);\n");
			sb.appendFront("}\n");
		}
		
		sb.appendFront("public override void FailAssertion() { Debug.Assert(false); }\n");
		sb.appendFront("public override string MD5Hash { get { return \"" + be.unit.getTypeDigest() + "\"; } }\n");
	}

	private void genPackages() {
		sb.appendFront("private string[] packages = {\n");
		sb.indent();
		for(PackageType pt : model.getPackages()) {
			sb.appendFront("\"" + pt.getIdent() + "\",\n");
		}
		sb.unindent();
		sb.appendFront("};\n");
	}

	private void genValidates() {
		sb.appendFront("private GRGEN_LIBGR.ValidateInfo[] validateInfos = {\n");

		for(EdgeType edgeType : model.getEdgeTypes()) {
			genValidate(edgeType);
		}
		
		for(PackageType pt : model.getPackages()) {
			for(EdgeType edgeType : pt.getEdgeTypes()) {
				genValidate(edgeType);
			}
		}

		sb.appendFront("};\n");
	}

	private void genValidate(EdgeType edgeType) {
		for(ConnAssert ca : edgeType.getConnAsserts()) {
			sb.appendFront("new GRGEN_LIBGR.ValidateInfo(");
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
		sb.appendFront("private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {\n");

		for(Index index : model.getIndices()) {
			if(index instanceof AttributeIndex)
				genIndexDescription((AttributeIndex)index);
			else
				genIndexDescription((IncidenceCountIndex)index);
		}
		
		/*for(PackageType pt : model.getPackages()) {
			for(AttributeIndex index : pt.getIndices()) {
				genIndexDescription(index);
			}
		}*/

		sb.appendFront("};\n");
	}

	private void genIndexDescription(AttributeIndex index) {
		sb.appendFront("new GRGEN_LIBGR.AttributeIndexDescription(");
		sb.append("\"" + index.getIdent() + "\", ");
		sb.append(formatTypeClassName(index.type) + ".typeVar, ");
		sb.append(formatTypeClassName(index.type) + "." + formatAttributeTypeName(index.entity));
		sb.append("),\n");
	}

	private void genIndexDescription(IncidenceCountIndex index) {
		sb.appendFront("new GRGEN_LIBGR.IncidenceCountIndexDescription(");
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
		sb.append(formatTypeClassRefInstance(((IncidenceCountIndex)index).getStartNodeType()) + ", ");
		sb.append(formatTypeClassRefInstance(((IncidenceCountIndex)index).getIncidentEdgeType()) + ", ");
		sb.append(formatTypeClassRefInstance(((IncidenceCountIndex)index).getAdjacentNodeType()));
		sb.append("),\n");
	}

	private void genIndicesGraphBinding() {
		sb.appendFront("public override GRGEN_LIBGR.IUniquenessHandler CreateUniquenessHandler(GRGEN_LIBGR.IGraph graph) {\n");
		sb.indent();
		if(model.isUniqueIndexDefined())
			sb.appendFront("return new GRGEN_LGSP.LGSPUniquenessIndex((GRGEN_LGSP.LGSPGraph)graph); // must be called before the indices so that its event handler is registered first, doing the unique id computation the indices depend upon\n");
		else if(model.isUniqueDefined())
			sb.appendFront("return new GRGEN_LGSP.LGSPUniquenessEnsurer((GRGEN_LGSP.LGSPGraph)graph); // must be called before the indices so that its event handler is registered first, doing the unique id computation the indices depend upon\n");
		else
			sb.appendFront("return null;\n");
		sb.unindent();
		sb.appendFront("}\n");

		sb.appendFront("public override GRGEN_LIBGR.IIndexSet CreateIndexSet(GRGEN_LIBGR.IGraph graph) {\n");
		sb.appendFront("\treturn new " + model.getIdent() + "IndexSet((GRGEN_LGSP.LGSPGraph)graph);\n");
		sb.appendFront("}\n");
		
		sb.appendFront("public override void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {\n");
		sb.indent();
		if(model.isUniqueDefined())
			sb.appendFront("((GRGEN_LGSP.LGSPUniquenessEnsurer)graph.UniquenessHandler).FillAsClone((GRGEN_LGSP.LGSPUniquenessEnsurer)originalGraph.UniquenessHandler, oldToNewMap);\n");
		sb.appendFront("((" + model.getIdent() + "IndexSet)graph.Indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);\n");
		sb.unindent();
		sb.appendFront("}\n");
	}
	
	private void genEnumAttributeTypes() {
		sb.appendFront("private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {\n");
		sb.indent();
		for(EnumType enumt : model.getEnumTypes()) {
			genEnumAttributeType(enumt);
		}
		for(PackageType pt : model.getPackages()) {
			for(EnumType enumt : pt.getEnumTypes()) {
				genEnumAttributeType(enumt);
			}
		}
		sb.unindent();
		sb.appendFront("};\n");
	}

	private void genEnumAttributeType(EnumType enumt) {
		sb.appendFront("GRGEN_MODEL." + getPackagePrefixDot(enumt) + "Enums.@" + formatIdentifiable(enumt) + ",\n");
	}

	///////////////////////////////
	// External stuff generation //
	///////////////////////////////

	private void genExternalTypes() {
		sb.appendFront("public static GRGEN_LIBGR.ExternalType externalType_object = new ExternalType_object();\n");
		for(ExternalType et : model.getExternalTypes()) {
			sb.appendFront("public static GRGEN_LIBGR.ExternalType externalType_" + et.getIdent() + " = new ExternalType_" + et.getIdent() + "();\n");
		}

		sb.appendFront("private GRGEN_LIBGR.ExternalType[] externalTypes = { ");
		sb.append("externalType_object");
		for(ExternalType et : model.getExternalTypes()) {
			sb.append(", externalType_" + et.getIdent());
		}
		sb.append(" };\n");
	}

	/**
	 * Generates the external type implementation
	 */
	private void genExternalType(ExternalType type) {
		sb.append("\n");
		sb.appendFront("public sealed class ExternalType_" + type.getIdent() + " : GRGEN_LIBGR.ExternalType\n");
		sb.appendFront("{\n");
		sb.indent();

		sb.appendFront("public ExternalType_" + type.getIdent() + "()\n");
		sb.appendFront("\t: base(\"" + type.getIdent() + "\", typeof(" + type.getIdent() + "))\n");
		sb.appendFront("{\n");
		sb.appendFront("}\n");

		sb.appendFront("public override int NumFunctionMethods { get { return " + type.getAllExternalFunctionMethods().size() + "; } }\n");
		genExternalFunctionMethodsEnumerator(type);
		genGetExternalFunctionMethod(type);

		sb.appendFront("public override int NumProcedureMethods { get { return " + type.getAllExternalProcedureMethods().size() + "; } }\n");
		genExternalProcedureMethodsEnumerator(type);
		genGetExternalProcedureMethod(type);

		sb.unindent();
		sb.append("}\n");
		
		// generate function method info classes
		Collection<ExternalFunctionMethod> allExternalFunctionMethods = type.getAllExternalFunctionMethods();
		for(ExternalFunctionMethod efm : allExternalFunctionMethods) {
			genExternalFunctionMethodInfo(efm, type, null);
		}
		
		// generate procedure method info classes
		Collection<ExternalProcedureMethod> allExternalProcedureMethods = type.getAllExternalProcedureMethods();
		for(ExternalProcedureMethod epm : allExternalProcedureMethods) {
			genExternalProcedureMethodInfo(epm, type, null);
		}
	}

	private void genExternalFunctionMethodsEnumerator(ExternalType type) {
		Collection<ExternalFunctionMethod> allExternalFunctionMethods = type.getAllExternalFunctionMethods();
		sb.appendFront("public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods");

		if(allExternalFunctionMethods.isEmpty())
			sb.append(" { get { yield break; } }\n");
		else {
			sb.append("\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("get\n");
			sb.appendFront("{\n");
			sb.indent();
			for(ExternalFunctionMethod efm : allExternalFunctionMethods) {
				sb.append("yield return " + formatExternalFunctionMethodInfoName(efm, type) + ".Instance;\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genGetExternalFunctionMethod(ExternalType type) {
		Collection<ExternalFunctionMethod> allExternalFunctionMethods = type.getAllExternalFunctionMethods();
		sb.appendFront("public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name)");

		if(allExternalFunctionMethods.isEmpty())
			sb.append(" { return null; }\n");
		else {
			sb.append("\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("switch(name)\n");
			sb.appendFront("{\n");
			sb.indent();
			for(ExternalFunctionMethod efm : allExternalFunctionMethods) {
				sb.append("case \"" + formatIdentifiable(efm) + "\" : return " +
						formatExternalFunctionMethodInfoName(efm, type) + ".Instance;\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
			sb.appendFront("return null;\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genExternalProcedureMethodsEnumerator(ExternalType type) {
		Collection<ExternalProcedureMethod> allExternalProcedureMethods = type.getAllExternalProcedureMethods();
		sb.appendFront("public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods");

		if(allExternalProcedureMethods.isEmpty())
			sb.append(" { get { yield break; } }\n");
		else {
			sb.append("\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("get\n");
			sb.appendFront("{\n");
			sb.indent();
			for(ExternalProcedureMethod epm : allExternalProcedureMethods) {
				sb.appendFront("yield return " + formatExternalProcedureMethodInfoName(epm, type) + ".Instance;\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genGetExternalProcedureMethod(ExternalType type) {
		Collection<ExternalProcedureMethod> allExternalProcedureMethods = type.getAllExternalProcedureMethods();
		sb.appendFront("public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name)");

		if(allExternalProcedureMethods.isEmpty())
			sb.append(" { return null; }\n");
		else {
			sb.append("\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("switch(name)\n");
			sb.appendFront("{\n");
			sb.indent();
			for(ExternalProcedureMethod epm : allExternalProcedureMethods) {
				sb.append("case \"" + formatIdentifiable(epm) + "\" : return " +
						formatExternalProcedureMethodInfoName(epm, type) + ".Instance;\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
			sb.appendFront("return null;\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	/**
	 * Generates the function info for the given external function method
	 */
	private void genExternalFunctionMethodInfo(ExternalFunctionMethod efm, ExternalType type, String packageName) {
		String externalFunctionMethodName = formatIdentifiable(efm);
		String className = formatExternalFunctionMethodInfoName(efm, type);

		sb.appendFront("public class " + className + " : GRGEN_LIBGR.FunctionInfo\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if (instance==null) { "
				+ "instance = new " + className + "(); } return instance; } }\n");
		sb.append("\n");

		sb.appendFront("private " + className + "()\n");
		sb.indent().indent().indent();
		sb.appendFront(": base(\n");
		sb.indent();
		sb.appendFront("\"" + externalFunctionMethodName + "\",\n");
		sb.appendFront((packageName!=null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\"" + (packageName!=null ? packageName + "::" + externalFunctionMethodName : externalFunctionMethodName) + "\",\n");
		sb.appendFront("true,\n");
		sb.appendFront("new String[] { ");
		int i = 0;
		for(@SuppressWarnings("unused") Type inParamType : efm.getParameterTypes()) {
			sb.append("\"in_" + i + "\", ");
			++i;
		}
		sb.append(" },\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Type inParamType : efm.getParameterTypes()) {
			if(inParamType instanceof InheritanceType  && !(inParamType instanceof ExternalType)) {
				sb.append(formatTypeClassRef(inParamType) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(inParamType) + ")), ");
			}
		}
		sb.append(" },\n");
		Type outType = efm.getReturnType();
		if(outType instanceof InheritanceType && !(outType instanceof ExternalType)) {
			sb.append(formatTypeClassRef(outType) + ".typeVar\n");
		} else {
			sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(outType) + "))\n");
		}
		sb.unindent();
		sb.append(")\n");
		sb.unindent().unindent().unindent();
		sb.appendFront("{\n");
		sb.appendFront("}\n");
		
		sb.appendFront("public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)\n");
		sb.appendFront("{\n");
		sb.appendFront("\tthrow new Exception(\"Not implemented, can't call function method without this object!\");\n");
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	/**
	 * Generates the procedure info for the given external procedure method
	 */
	private void genExternalProcedureMethodInfo(ExternalProcedureMethod epm, ExternalType type, String packageName) {
		String externalProcedureMethodName = formatIdentifiable(epm);
		String className = formatExternalProcedureMethodInfoName(epm, type);

		sb.appendFront("public class " + className + " : GRGEN_LIBGR.ProcedureInfo\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if (instance==null) { "
				+ "instance = new " + className + "(); } return instance; } }\n");
		sb.append("\n");

		sb.appendFront("private " + className + "()\n");
		sb.indent().indent().indent();
		sb.appendFront(": base(\n");
		sb.indent();
		sb.appendFront("\"" + externalProcedureMethodName + "\",\n");
		sb.appendFront((packageName!=null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\"" + (packageName!=null ? packageName + "::" + externalProcedureMethodName : externalProcedureMethodName) + "\",\n");
		sb.appendFront("true,\n");
		sb.appendFront("new String[] { ");
		int i = 0;
		for(@SuppressWarnings("unused") Type inParamType : epm.getParameterTypes()) {
			sb.append("\"in_" + i + "\", ");
			++i;
		}
		sb.append(" },\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Type inParamType : epm.getParameterTypes()) {
			if(inParamType instanceof InheritanceType && !(inParamType instanceof ExternalType)) {
				sb.append(formatTypeClassRef(inParamType) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(inParamType) + ")), ");
			}
		}
		sb.append(" },\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Type outType : epm.getReturnTypes()) {
			if(outType instanceof InheritanceType && !(outType instanceof ExternalType)) {
				sb.append(formatTypeClassRef(outType) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(outType) + ")), ");
			}
		}
		sb.append(" }\n");
		sb.indent();
		sb.appendFront(")\n");
		sb.indent().indent().indent();
		sb.appendFront("{\n");
		sb.appendFront("}\n");
		
		sb.appendFront("public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)\n");
		sb.appendFront("{\n");
		sb.appendFront("\tthrow new Exception(\"Not implemented, can't call procedure method without this object!\");\n");
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	private void genExternalTypeObject() {
		sb.append("\n");
		sb.appendFront("public sealed class ExternalType_object : GRGEN_LIBGR.ExternalType\n");
		sb.appendFront("{\n");
		sb.indent();

		sb.appendFront("public ExternalType_object()\n");
		sb.appendFront("\t: base(\"object\", typeof(object))\n");
		sb.appendFront("{\n");
		sb.appendFront("}\n");

		sb.appendFront("public override int NumFunctionMethods { get { return 0; } }\n");
		sb.appendFront("public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }\n");
		sb.appendFront("public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }\n");
		sb.appendFront("public override int NumProcedureMethods { get { return 0; } }\n");
		sb.appendFront("public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }\n");
		sb.appendFront("public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }\n");

		sb.unindent();
		sb.appendFront("}\n");		
	}

	private void genExternalClasses() {
		for(ExternalType et : model.getExternalTypes()) {
			sb.appendFront("public partial class " + et.getIdent());
			boolean first = true;
			for(InheritanceType superType : et.getDirectSuperTypes()) {
				if(first) {
					sb.append(" : ");
				} else {
					sb.append(", ");
				}
				sb.append(superType.getIdent().toString());
				first = false;
			}
			sb.append("\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("// You must implement this class in the same partial class in ./" + model.getIdent() + "ModelExternalFunctionsImpl.cs:\n");
			
			genExtMethods(et);
			
			sb.unindent();
			sb.appendFront("}\n");
			sb.append("\n");
		}
	}

	private void genExtMethods(ExternalType type) {
		if(type.getAllExternalFunctionMethods().size()==0 && type.getAllExternalProcedureMethods().size()==0)
			return;

		sb.append("\n");
		sb.appendFront("// You must implement the following methods in the same partial class in ./" + model.getIdent() + "ModelExternalFunctionsImpl.cs:\n");
		
		for(ExternalFunctionMethod efm : type.getAllExternalFunctionMethods()) {
			sb.appendFront("//public " + formatType(efm.getReturnType()) + " ");
			sb.append(efm.getIdent().toString() + "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph");
			for(Type inParamType : efm.getParameterTypes()) {
				sb.append(", ");
				sb.append(formatType(inParamType));
			}
			sb.append(");\n");

			if(model.areFunctionsParallel())
			{
				sb.appendFront("//public " + formatType(efm.getReturnType()) + " ");
				sb.append(efm.getIdent().toString() + "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph");
				for(Type inParamType : efm.getParameterTypes()) {
					sb.append(", ");
					sb.append(formatType(inParamType));
				}
				sb.append(", int threadId");
				sb.append(");\n");
			}
		}

		//////////////////////////////////////////////////////////////
		
		for(ExternalProcedureMethod epm : type.getAllExternalProcedureMethods()) {
			genParameterPassingReturnArray(type, epm);
		}

		for(ExternalProcedureMethod epm : type.getAllExternalProcedureMethods()) {
			sb.appendFront("//public void ");
			sb.append(epm.getIdent().toString() + "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, GRGEN_LIBGR.IGraphElement");
			for(Type inParamType : epm.getParameterTypes()) {
				sb.append(", ");
				sb.append(formatType(inParamType));
			}
			for(Type outType : epm.getReturnTypes()) {
				sb.append(", out ");
				sb.append(formatType(outType));
			}
			sb.append(");\n");
		}
	}

	private void genParameterPassingReturnArray(ExternalType type, ExternalProcedureMethod epm) {
		sb.appendFront("private static object[] ReturnArray_" + epm.getIdent().toString() + "_" + type.getIdent().toString() + " = new object[" + epm.getReturnTypes().size() + "]; // helper array for multi-value-returns, to allow for contravariant parameter assignment\n");
	}

	private void genEmitterParserClass() {
		sb.appendFront("public partial class AttributeTypeObjectEmitterParser");
		sb.append("\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("// You must implement this class in the same partial class in ./" + model.getIdent() + "ModelExternalFunctionsImpl.cs:\n");
		sb.appendFront("// You must implement the functions called by the following functions inside that class (same name plus suffix Impl):\n");
		sb.append("\n");
		if(model.isEmitClassDefined()) {
			sb.appendFront("// Called during .grs import, at exactly the position in the text reader where the attribute begins.\n");
			sb.appendFront("// For attribute type object or a user defined type, which is treated as object.\n");
			sb.appendFront("// The implementation must parse from there on the attribute type requested.\n");
			sb.appendFront("// It must not parse beyond the serialized representation of the attribute, \n");
			sb.appendFront("// i.e. Peek() must return the first character not belonging to the attribute type any more.\n");
			sb.appendFront("// Returns the parsed object.\n");
			sb.appendFront("public static object Parse(TextReader reader, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("return ParseImpl(reader, attrType, graph);\n");
			sb.appendFront("//reader.Read(); reader.Read(); reader.Read(); reader.Read(); // eat 'n' 'u' 'l' 'l' // default implementation\n");
			sb.appendFront("//return null; // default implementation\n");
			sb.unindent();
			sb.appendFront("}\n");
			sb.append("\n");
			sb.appendFront("// Called during .grs export, the implementation must return a string representation for the attribute.\n");
			sb.appendFront("// For attribute type object or a user defined type, which is treated as object.\n");
			sb.appendFront("// The serialized string must be parseable by Parse.\n");
			sb.appendFront("public static string Serialize(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("return SerializeImpl(attribute, attrType, graph);\n");
			sb.appendFront("//Console.WriteLine(\"Warning: Exporting attribute of object type to null\"); // default implementation\n");
			sb.appendFront("//return \"null\"; // default implementation\n");
			sb.unindent();
			sb.appendFront("}\n");
			sb.append("\n");
			sb.appendFront("// Called during debugging or emit writing, the implementation must return a string representation for the attribute.\n");
			sb.appendFront("// For attribute type object or a user defined type, which is treated as object.\n");
			sb.appendFront("// The attribute type may be null.\n");
			sb.appendFront("// The string is meant for consumption by humans, it does not need to be parseable.\n");
			sb.appendFront("public static string Emit(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("return EmitImpl(attribute, attrType, graph);\n");
			sb.appendFront("//return \"null\"; // default implementation\n");
			sb.unindent();
			sb.appendFront("}\n");
			sb.append("\n");
			sb.appendFront("// Called when the shell hits a line starting with \"external\".\n");
			sb.appendFront("// The content of that line is handed in.\n");
			sb.appendFront("// This is typically used while replaying changes containing a method call of an external type\n");
			sb.appendFront("// -- after such a line was recorded, by the method called, by writing to the recorder.\n");
			sb.appendFront("// This is meant to replay fine-grain changes of graph attributes of external type,\n");
			sb.appendFront("// in contrast to full assignments handled by Parse and Serialize.\n");
			sb.appendFront("public static void External(string line, GRGEN_LIBGR.IGraph graph)\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("ExternalImpl(line, graph);\n");
			sb.appendFront("//Console.Write(\"Ignoring: \"); // default implementation\n");
			sb.appendFront("//Console.WriteLine(line); // default implementation\n");
			sb.unindent();
			sb.appendFront("}\n");
			sb.append("\n");
		}
		if(model.isEmitGraphClassDefined()) {
			sb.appendFront("// Called during debugging on user request, the implementation must return a named graph representation for the attribute.\n");
			sb.appendFront("// For attribute type object or a user defined type, which is treated as object.\n");
			sb.appendFront("// The attribute type may be null. The return graph must be of the same model as the graph handed in.\n");
			sb.appendFront("// The named graph is meant for display in the debugger, to visualize the internal structure of some attribute type.\n");
			sb.appendFront("// This way you can graphically inspect your own data types which are opaque to GrGen with its debugger.\n");
			sb.appendFront("public static GRGEN_LIBGR.INamedGraph AsGraph(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("return AsGraphImpl(attribute, attrType, graph);\n");
			sb.appendFront("//return null; // default implementation\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	private void genCopierComparerClass() {
		sb.appendFront("public partial class AttributeTypeObjectCopierComparer\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("// You must implement the following functions in the same partial class in ./" + model.getIdent() + "ModelExternalFunctionsImpl.cs:\n");
		sb.append("\n");
		if(model.isCopyClassDefined()) {
			sb.appendFront("// Called when a graph element is cloned/copied.\n");
			sb.appendFront("// For attribute type object.\n");
			sb.appendFront("// If \"copy class\" is not specified, objects are copied by copying the reference, i.e. they are identical afterwards.\n");
			sb.appendFront("// All other attribute types are copied by-value (so changing one later on has no effect on the other).\n");
			sb.appendFront("//public static object Copy(object);\n");
			sb.append("\n");
		}
		if(model.isEqualClassDefined()) {
			sb.appendFront("// Called during comparison of graph elements from graph isomorphy comparison, or attribute comparison.\n");
			sb.appendFront("// For attribute type object.\n");
			sb.appendFront("// If \"== class\" is not specified, objects are equal if they are identical,\n");
			sb.appendFront("// i.e. by-reference-equality (same pointer); all other attribute types are compared by-value.\n");
			sb.appendFront("//public static bool IsEqual(object, object);\n");
			sb.append("\n");
		}
		if(model.isLowerClassDefined()) {
			sb.appendFront("// Called during attribute comparison.\n");
			sb.appendFront("// For attribute type object.\n");
			sb.appendFront("// If \"< class\" is not specified, objects can't be compared for ordering, only for equality.\n");
			sb.appendFront("//public static bool IsLower(object, object);\n");
			sb.append("\n");
		}
		if(model.getExternalTypes().size() > 0) {
			sb.append("\n");
			sb.appendFront("// The same functions, just for each user defined type.\n");
			sb.appendFront("// Those are normally treated as object (if no \"copy class or == class or < class\" is specified),\n");
			sb.appendFront("// i.e. equal if identical references, no ordered comparisons available, and copy just copies the reference (making them identical).\n");
			sb.appendFront("// Here you can overwrite the default reference semantics with value semantics, fitting better to the other attribute types.\n");
			for(ExternalType et : model.getExternalTypes()) {
				String typeName = et.getIdent().toString();
				sb.append("\n");
				if(model.isCopyClassDefined())
					sb.appendFront("//public static " + typeName + " Copy(" + typeName + ");\n");
				if(model.isEqualClassDefined())
					sb.appendFront("//public static bool IsEqual(" + typeName + ", " + typeName + ");\n");
				if(model.isLowerClassDefined())
					sb.appendFront("//public static bool IsLower(" + typeName + ", " + typeName + ");\n");
			}
		}
		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	private void genExternalFunctionHeaders() {
		for(ExternalFunction ef : model.getExternalFunctions()) {
			Type returnType = ef.getReturnType();
			sb.appendFront("//public static " + formatType(returnType) + " " + ef.getName() + "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph");
			for(Type paramType : ef.getParameterTypes()) {
				sb.append(", ");
				sb.append(formatType(paramType));
			}
			sb.append(");\n");

			if(model.areFunctionsParallel())
			{
				sb.appendFront("//public static " + formatType(returnType) + " " + ef.getName() + "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph");
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
			sb.appendFront("//public static void " + ep.getName() + "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph");
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
	private SourceBuilder sb = null;
	private SourceBuilder stubsb = null;
	private String curMemberOwner = null;
	private HashSet<String> rootTypes;
	private ModifyGen mgFuncComp;
}

