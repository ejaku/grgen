/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Generates the model files for the SearchPlanBackend2 backend.
 * @author Moritz Kroll, Edgar Jakumeit
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
import de.unika.ipd.grgen.ir.executable.FunctionMethod;
import de.unika.ipd.grgen.ir.executable.ProcedureMethod;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.basic.BooleanType;
import de.unika.ipd.grgen.ir.type.basic.ByteType;
import de.unika.ipd.grgen.ir.type.basic.DoubleType;
import de.unika.ipd.grgen.ir.type.basic.FloatType;
import de.unika.ipd.grgen.ir.type.basic.GraphType;
import de.unika.ipd.grgen.ir.type.basic.IntType;
import de.unika.ipd.grgen.ir.type.basic.LongType;
import de.unika.ipd.grgen.ir.type.basic.ObjectType;
import de.unika.ipd.grgen.ir.type.basic.ShortType;
import de.unika.ipd.grgen.ir.type.basic.StringType;
import de.unika.ipd.grgen.ir.type.basic.VoidType;
import de.unika.ipd.grgen.ir.type.container.ArrayType;
import de.unika.ipd.grgen.ir.type.container.DequeType;
import de.unika.ipd.grgen.ir.type.container.MapType;
import de.unika.ipd.grgen.ir.type.container.SetType;
import de.unika.ipd.grgen.util.SourceBuilder;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.ExpressionPair;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.expr.array.ArrayInit;
import de.unika.ipd.grgen.ir.expr.deque.DequeInit;
import de.unika.ipd.grgen.ir.expr.graph.IncidentEdgeExpr;
import de.unika.ipd.grgen.ir.expr.map.MapInit;
import de.unika.ipd.grgen.ir.expr.set.SetInit;
import de.unika.ipd.grgen.ir.model.AttributeIndex;
import de.unika.ipd.grgen.ir.model.ConnAssert;
import de.unika.ipd.grgen.ir.model.EnumItem;
import de.unika.ipd.grgen.ir.model.IncidenceCountIndex;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.model.MemberInit;
import de.unika.ipd.grgen.ir.model.Model;
import de.unika.ipd.grgen.ir.model.NodeEdgeEnumBearer;
import de.unika.ipd.grgen.ir.model.type.EdgeType;
import de.unika.ipd.grgen.ir.model.type.EnumType;
import de.unika.ipd.grgen.ir.model.type.ExternalType;
import de.unika.ipd.grgen.ir.model.type.InheritanceType;
import de.unika.ipd.grgen.ir.model.type.NodeType;
import de.unika.ipd.grgen.ir.model.type.PackageType;

public class ModelGen extends CSharpBase
{
	private final int MAX_OPERATIONS_FOR_ATTRIBUTE_INITIALIZATION_INLINING = 20;
	private final static String ATTR_IMPL_SUFFIX = "_M0no_suXx_h4rD";

	public ModelGen(SearchPlanBackend2 backend, String nodeTypePrefix, String edgeTypePrefix)
	{
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
	public void genModel(Model model)
	{
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

		ModelExternalGen modelExternalGen = new ModelExternalGen(model, sb, nodeTypePrefix, edgeTypePrefix);
		modelExternalGen.genExternalTypeObject();
		for(ExternalType et : model.getExternalTypes()) {
			modelExternalGen.genExternalType(et);
		}

		System.out.println("    generating indices...");

		sb.append("\n");
		sb.appendFront("//\n");
		sb.appendFront("// Indices\n");
		sb.appendFront("//\n");
		sb.append("\n");

		ModelIndexGen indexGen = new ModelIndexGen(model, sb, nodeTypePrefix, edgeTypePrefix);
		indexGen.genIndexTypes();
		indexGen.genIndexImplementations();
		indexGen.genIndexSetType();

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

		modelExternalGen = new ModelExternalGen(model, sb, nodeTypePrefix, edgeTypePrefix);

		modelExternalGen.genExternalFunctionsFile(be.unit.getFilename());

		System.out.println("    writing to " + be.path + " / " + filename);
		writeFile(be.path, filename, sb.toString());

		if(be.path.compareTo(new File(".")) == 0) {
			System.out.println("    no copy needed for " + be.path + " / " + filename);
		} else {
			System.out.println("    copying " + be.path + " / " + filename + " to "
					+ be.path.getAbsoluteFile().getParent() + " / " + filename);
			copyFile(new File(be.path, filename), new File(be.path.getAbsoluteFile().getParent(), filename));
		}
	}

	private SourceBuilder getStubBuffer()
	{
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
			NodeEdgeEnumBearer bearer, String packageName)
	{

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

	private void genEnums(NodeEdgeEnumBearer bearer)
	{
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
					+ (!getPackagePrefix(enumt).equals("") ? "\"" + getPackagePrefix(enumt) + "\"" : "null") + ", "
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
			NodeEdgeEnumBearer bearer, String packageName, boolean isNode)
	{
		Collection<? extends InheritanceType> curTypes = isNode ? bearer.getNodeTypes() : bearer.getEdgeTypes();

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
	private void genType(Collection<? extends InheritanceType> allTypes, InheritanceType type, String packageName)
	{
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
	private void genElementInterface(InheritanceType type)
	{
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
	private void genDirectSuperTypeList(InheritanceType type)
	{
		String iprefix = "I" + getNodeOrEdgeTypePrefix(type);
		Collection<InheritanceType> directSuperTypes = type.getDirectSuperTypes();

		boolean first = true;
		for(Iterator<InheritanceType> i = directSuperTypes.iterator(); i.hasNext();) {
			InheritanceType superType = i.next();
			if(rootTypes.contains(superType.getIdent().toString())) {
				if(first)
					first = false;
				else
					sb.append(", ");
				sb.append(getRootElementInterfaceRef(superType));
			} else {
				if(first)
					first = false;
				else
					sb.append(", ");
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
	private void genAttributeAccess(InheritanceType type, Entity member, String modifiers)
	{
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
			Collection<ProcedureMethod> procedureMethods, String modifiers)
	{
		// METHOD-TODO - inheritance?
		for(FunctionMethod fm : functionMethods) {
			if(type.superTypeDefinesFunctionMethod(fm))
				continue; // skip methods which were already declared in a base interface

			sb.appendFront(formatType(fm.getReturnType()) + " ");
			sb.append(fm.getIdent().toString()
					+ "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph");
			for(Entity inParam : fm.getParameters()) {
				sb.append(", ");
				sb.append(formatType(inParam.getType()));
				sb.append(" ");
				sb.append(formatEntity(inParam));
			}
			sb.append(");\n");

			if(model.areFunctionsParallel()) {
				sb.appendFront(formatType(fm.getReturnType()) + " ");
				sb.append(fm.getIdent().toString()
						+ "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph");
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
			if(type.superTypeDefinesProcedureMethod(pm))
				continue; // skip methods which were already declared in a base interface

			sb.appendFront("void ");
			sb.append(pm.getIdent().toString()
					+ "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph");
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
	private void genElementImplementation(InheritanceType type)
	{
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
		} else { // what's that? = for "Embedding the graph rewrite system GrGen.NET into C#" (see corresponding master thesis, mono c# compiler extension)
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
			} else
				extClassName = extName;
			routedClassName = extClassName;
			routedDeclName = extClassName;

			stubsb.appendFront("public class " + extClassName + " : " + elemref + "\n");
			stubsb.appendFront("{\n");
			stubsb.indent();
			stubsb.appendFront("public " + extClassName + "() : base() { }\n");
			stubsb.append("\n");

			sb.append("\n");
			sb.appendFront("public abstract class " + elemname + " : GRGEN_LGSP.LGSP"
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
			stubsb.appendFront("}\n"); // close class stub
			if(namespace != null) {
				stubsb.unindent();
				stubsb.appendFront("}\n"); // close namespace
			}
		}
	}

	private void genElementConstructor(InheritanceType type, String elemname, String typeref)
	{
		boolean isNode = type instanceof NodeType;
		if(isNode) {
			sb.appendFront("public " + elemname + "() : base(" + typeref + ".typeVar)\n");
			sb.appendFront("{\n");
			sb.indent();
			initAllMembersNonConst(type, "this", false, false);
			sb.unindent();
			sb.appendFront("}\n");
		} else {
			sb.appendFront("public " + elemname + "(GRGEN_LGSP.LGSPNode source, "
					+ "GRGEN_LGSP.LGSPNode target)\n");
			sb.appendFrontIndented(": base(" + typeref + ".typeVar, source, target)\n");
			sb.appendFront("{\n");
			sb.indent();
			initAllMembersNonConst(type, "this", false, false);
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genElementStaticTypeGetter(String typeref)
	{
		sb.appendFront("public static " + typeref + " TypeInstance { get { return " + typeref + ".typeVar; } }\n");
	}

	private void genElementCloneMethod(InheritanceType type, SourceBuilder routedSB, String routedDeclName)
	{
		boolean isNode = type instanceof NodeType;
		if(isNode) {
			routedSB.appendFront("public override GRGEN_LIBGR.INode Clone() {\n");
			routedSB.appendFrontIndented("return new " + routedDeclName + "(this);\n");
			routedSB.appendFront("}\n");
		} else {
			routedSB.appendFront("public override GRGEN_LIBGR.IEdge Clone("
					+ "GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget) {\n");
			routedSB.appendFrontIndented("return new " + routedDeclName + "(this, (GRGEN_LGSP.LGSPNode) newSource, "
					+ "(GRGEN_LGSP.LGSPNode) newTarget);\n");
			routedSB.appendFront("}\n");
		}
	}

	private void genElementCopyConstructor(InheritanceType type, String extName, String typeref,
			SourceBuilder routedSB, String routedClassName, String routedDeclName)
	{
		boolean isNode = type instanceof NodeType;
		if(isNode) {
			routedSB.appendFront("private " + routedClassName + "(" + routedDeclName + " oldElem) : base("
					+ (extName == null ? typeref + ".typeVar" : "") + ")\n");
		} else {
			routedSB.appendFront("private " + routedClassName + "(" + routedDeclName
					+ " oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)\n");
			routedSB.appendFrontIndented(": base("
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
				routedSB.appendFront("AttributeTypeObjectCopierComparer.Copy("
						+ "oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ");\n");
			} else {
				routedSB.appendFront(attrName + ModelGen.ATTR_IMPL_SUFFIX + " = "
						+ "oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ";\n");
			}
		}
		routedSB.unindent();
		routedSB.appendFront("}\n");
	}

	private void genElementAttributeComparisonMethod(InheritanceType type, SourceBuilder routedSB,
			String routedClassName)
	{
		routedSB.appendFront("public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {\n");
		routedSB.indent();
		routedSB.appendFront("if(!(that is " + routedClassName + ")) return false;\n");
		routedSB.appendFront(routedClassName + " that_ = (" + routedClassName + ")that;\n");
		routedSB.appendFront("return true\n");
		routedSB.indent();
		for(Entity member : type.getAllMembers()) {
			if(member.isConst())
				continue;

			String attrName = formatIdentifiable(member);
			if(member.getType() instanceof MapType || member.getType() instanceof SetType
					|| member.getType() instanceof ArrayType || member.getType() instanceof DequeType) {
				routedSB.appendFront("&& GRGEN_LIBGR.ContainerHelper.Equal("
								+ attrName + ModelGen.ATTR_IMPL_SUFFIX + ", "
								+ "that_." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ")\n");
			} else if(model.isEqualClassDefined()
					&& (member.getType().classify() == Type.IS_EXTERNAL_TYPE
							|| member.getType().classify() == Type.IS_OBJECT)) {
				routedSB.appendFront("&& AttributeTypeObjectCopierComparer.IsEqual("
								+ attrName + ModelGen.ATTR_IMPL_SUFFIX + ", "
								+ "that_." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ")\n");
			} else if(member.getType().classify() == Type.IS_GRAPH) {
				routedSB.appendFront("&& GRGEN_LIBGR.GraphHelper.Equal(" + attrName + ModelGen.ATTR_IMPL_SUFFIX + ", "
						+ "that_." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ")\n");
			} else {
				routedSB.appendFront("&& " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " == "
						+ "that_." + attrName + ModelGen.ATTR_IMPL_SUFFIX + "\n");
			}
		}
		routedSB.unindent();
		routedSB.appendFront(";\n");
		routedSB.unindent();
		routedSB.appendFront("}\n");
	}

	private void genElementCreateMethods(InheritanceType type, boolean isNode, String elemref, String allocName)
	{
		if(isNode) {
			sb.appendFront("public static " + elemref + " CreateNode(GRGEN_LGSP.LGSPGraph graph)\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront(elemref + " node;\n");
			sb.appendFront("if(poolLevel == 0)\n");
			sb.appendFrontIndented("node = new " + allocName + "();\n");
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

			sb.appendFront("public static " + elemref + " CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, "
					+ "string nodeName)\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront(elemref + " node;\n");
			sb.appendFront("if(poolLevel == 0)\n");
			sb.appendFrontIndented("node = new " + allocName + "();\n");
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
		} else {
			sb.appendFront("public static " + elemref + " CreateEdge(GRGEN_LGSP.LGSPGraph graph, "
					+ "GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront(elemref + " edge;\n");
			sb.appendFront("if(poolLevel == 0)\n");
			sb.appendFrontIndented("edge = new " + allocName + "(source, target);\n");
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
			sb.appendFrontIndented("edge = new " + allocName + "(source, target);\n");
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

	private void genElementRecycleMethod()
	{
		sb.appendFront("public override void Recycle()\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(poolLevel < 10)\n");
		sb.appendFrontIndented("pool[poolLevel++] = this;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void initAllMembersNonConst(InheritanceType type, String varName,
			boolean withDefaultInits, boolean isResetAllAttributes)
	{
		curMemberOwner = varName;

		// if we don't currently create the method ResetAllAttributes
		// we replace the initialization code by a call to ResetAllAttributes, if it gets to large
		if(!isResetAllAttributes
				&& initializationOperationsCount(type) > MAX_OPERATIONS_FOR_ATTRIBUTE_INITIALIZATION_INLINING) {
			sb.appendFront(varName + ".ResetAllAttributes();\n");
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

	private void genDefaultInits(InheritanceType type, String varName)
	{
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
					|| t instanceof EnumType || t instanceof DoubleType) {
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

	private void genContainerInits(InheritanceType type, String varName)
	{
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
				MapType mapType = (MapType)t;
				sb.append("new " + formatAttributeType(mapType) + "();\n");
			} else if(t instanceof SetType) {
				SetType setType = (SetType)t;
				sb.append("new " + formatAttributeType(setType) + "();\n");
			} else if(t instanceof ArrayType) {
				ArrayType arrayType = (ArrayType)t;
				sb.append("new " + formatAttributeType(arrayType) + "();\n");
			} else if(t instanceof DequeType) {
				DequeType dequeType = (DequeType)t;
				sb.append("new " + formatAttributeType(dequeType) + "();\n");
			}
		}
	}

	private int initializationOperationsCount(InheritanceType targetType)
	{
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
			String varName)
	{
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
			String varName, boolean withDefaultInits, boolean isResetAllAttributes)
	{
		if(rootTypes.contains(type.getIdent().toString())) // skip root types, they don't possess attributes
			return;
		sb.appendFront("// explicit initializations of " + formatIdentifiable(type)
				+ " for target " + formatIdentifiable(targetType) + "\n");

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

	private void genMemberInitsNonConstPrimitiveType(InheritanceType type, InheritanceType targetType, String varName)
	{
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

	private void genMemberInitsNonConstMapType(InheritanceType type, InheritanceType targetType, String varName)
	{
		for(MapInit mapInit : type.getMapInits()) {
			Entity member = mapInit.getMember();
			if(mapInit.getMember().isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrName = formatIdentifiable(mapInit.getMember());
			for(ExpressionPair item : mapInit.getMapItems()) {
				sb.appendFront(varName + ".@" + attrName + "[");
				genExpression(sb, item.getKeyExpr(), null);
				sb.append("] = ");
				genExpression(sb, item.getValueExpr(), null);
				sb.append(";\n");
			}
		}
	}

	private void genMemberInitsNonConstSetType(InheritanceType type, InheritanceType targetType, String varName)
	{
		for(SetInit setInit : type.getSetInits()) {
			Entity member = setInit.getMember();
			if(setInit.getMember().isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrName = formatIdentifiable(setInit.getMember());
			for(Expression item : setInit.getSetItems()) {
				sb.appendFront(varName + ".@" + attrName + "[");
				genExpression(sb, item, null);
				sb.append("] = null;\n");
			}
		}
	}

	private void genMemberInitsNonConstArrayType(InheritanceType type, InheritanceType targetType, String varName)
	{
		for(ArrayInit arrayInit : type.getArrayInits()) {
			Entity member = arrayInit.getMember();
			if(arrayInit.getMember().isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrName = formatIdentifiable(arrayInit.getMember());
			for(Expression item : arrayInit.getArrayItems()) {
				sb.appendFront(varName + ".@" + attrName + ".Add(");
				genExpression(sb, item, null);
				sb.append(");\n");
			}
		}
	}

	private void genMemberInitsNonConstDequeType(InheritanceType type, InheritanceType targetType, String varName)
	{
		for(DequeInit dequeInit : type.getDequeInits()) {
			Entity member = dequeInit.getMember();
			if(dequeInit.getMember().isConst())
				continue;
			if(!generateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
				continue;

			String attrName = formatIdentifiable(dequeInit.getMember());
			for(Expression item : dequeInit.getDequeItems()) {
				sb.appendFront(varName + ".@" + attrName + ".Add(");
				genExpression(sb, item, null);
				sb.append(");\n");
			}
		}
	}

	private void genMemberInitsConst(InheritanceType type, InheritanceType targetType,
			List<String> staticInitializers)
	{
		if(rootTypes.contains(type.getIdent().toString())) // skip root types, they don't possess attributes
			return;
		sb.appendFront("// explicit initializations of " + formatIdentifiable(type)
				+ " for target " + formatIdentifiable(targetType) + "\n");

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

		sb.appendFront("// implicit initializations of " + formatIdentifiable(type)
				+ " for target " + formatIdentifiable(targetType) + "\n");

		genMemberImplicitInitsNonConst(type, targetType, initializedConstMembers);
	}

	private void genMemberInitsConstPrimitiveType(InheritanceType type, InheritanceType targetType,
			HashSet<Entity> initializedConstMembers)
	{
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
			List<String> staticInitializers, HashSet<Entity> initializedConstMembers)
	{
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
			for(ExpressionPair item : mapInit.getMapItems()) {
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
			List<String> staticInitializers, HashSet<Entity> initializedConstMembers)
	{
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
			for(Expression item : setInit.getSetItems()) {
				sb.appendFront("");
				sb.append(attrName + ModelGen.ATTR_IMPL_SUFFIX);
				sb.append("[");
				genExpression(sb, item, null);
				sb.append("] = null;\n");
			}
			sb.unindent();
			sb.appendFront("}\n");

			initializedConstMembers.add(member);
		}
	}

	private void genMemberInitsConstArrayType(InheritanceType type, InheritanceType targetType,
			List<String> staticInitializers, HashSet<Entity> initializedConstMembers)
	{
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
			for(Expression item : arrayInit.getArrayItems()) {
				sb.appendFront("");
				sb.append(attrName + ModelGen.ATTR_IMPL_SUFFIX);
				sb.append(".Add(");
				genExpression(sb, item, null);
				sb.append(");\n");
			}
			sb.unindent();
			sb.appendFront("}\n");

			initializedConstMembers.add(member);
		}
	}

	private void genMemberInitsConstDequeType(InheritanceType type, InheritanceType targetType,
			List<String> staticInitializers, HashSet<Entity> initializedConstMembers)
	{
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
			for(Expression item : dequeInit.getDequeItems()) {
				sb.appendFront("");
				sb.append(attrName + ModelGen.ATTR_IMPL_SUFFIX);
				sb.append(".Enqueue(");
				genExpression(sb, item, null);
				sb.append(");\n");
			}
			sb.unindent();
			sb.appendFront("}\n");

			initializedConstMembers.add(member);
		}
	}

	private void genMemberImplicitInitsNonConst(InheritanceType type, InheritanceType targetType,
			HashSet<Entity> initializedConstMembers)
	{
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
				sb.appendFront("private static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX
						+ " = " + "new " + attrType + "();\n");
			} else
				sb.appendFront("private static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + ";\n");
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

		Set<InheritanceType> childrenOfFocusedType = new LinkedHashSet<InheritanceType>(type.getAllSubTypes());
		childrenOfFocusedType.remove(type); // we want children only, comes with type itself included

		Set<InheritanceType> targetTypeAndParents = new LinkedHashSet<InheritanceType>(targetType.getAllSuperTypes());
		targetTypeAndParents.add(targetType); // we want it inclusive target, comes exclusive

		Set<InheritanceType> intersection = new LinkedHashSet<InheritanceType>(childrenOfFocusedType);
		intersection.retainAll(targetTypeAndParents); // the set is empty if type==targetType

		for(InheritanceType relevantChildrenOfFocusedType : intersection) {
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

	protected void genQualAccess(SourceBuilder sb, Qualification qual, Object modifyGenerationState)
	{
		Entity owner = qual.getOwner();
		sb.append("((I" + getNodeOrEdgeTypePrefix(owner) +
				formatIdentifiable(owner.getType()) + ") ");
		sb.append(formatEntity(owner) + ").@" + formatIdentifiable(qual.getMember()));
	}

	protected void genMemberAccess(SourceBuilder sb, Entity member)
	{
		if(curMemberOwner != null)
			sb.append(curMemberOwner + ".");
		sb.append("@" + formatIdentifiable(member));
	}

	/**
	 * Generate the attribute accessor implementations of the given type
	 */
	private void genAttributesAndAttributeAccessImpl(InheritanceType type)
	{
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
			Entity member)
	{
		String attrType = formatAttributeType(member);
		String attrName = formatIdentifiable(member);

		if(member.isConst()) {
			// no member for const attributes, no setter for const attributes
			// they are class static, the member is created at the point of initialization
			routedSB.appendFront("public " + extModifier + attrType + " @" + attrName + "\n");
			routedSB.appendFront("{\n");
			routedSB.appendFrontIndented("get { return " + attrName + ModelGen.ATTR_IMPL_SUFFIX + "; }\n");
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

	private void genGetAttributeByName(InheritanceType type)
	{
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
		sb.appendFrontIndented("\"The " + (type instanceof NodeType ? "node" : "edge")
				+ " type \\\"" + formatIdentifiable(type)
				+ "\\\" does not have the attribute \\\"\" + attrName + \"\\\"!\");\n");

		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genSetAttributeByName(InheritanceType type)
	{
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
		sb.appendFrontIndented("\"The " + (type instanceof NodeType ? "node" : "edge")
				+ " type \\\"" + formatIdentifiable(type)
				+ "\\\" does not have the attribute \\\"\" + attrName + \"\\\"!\");\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genResetAllAttributes(InheritanceType type)
	{
		sb.appendFront("public override void ResetAllAttributes()\n");
		sb.appendFront("{\n");
		sb.indent();
		initAllMembersNonConst(type, "this", true, true);
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genParameterPassingMethodCall(InheritanceType type, FunctionMethod fm)
	{
		sb.appendFront("case \"" + fm.getIdent().toString() + "\":\n");
		sb.appendFrontIndented("return @" + fm.getIdent().toString() + "(actionEnv, graph");
		int i = 0;
		for(Entity inParam : fm.getParameters()) {
			sb.append(", (" + formatType(inParam.getType()) + ")arguments[" + i + "]");
			++i;
		}
		sb.append(");\n");
	}

	private void genParameterPassingMethodCall(InheritanceType type, ProcedureMethod pm)
	{
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
		for(i = 0; i < pm.getReturnTypes().size(); ++i) {
			sb.append(", out ");
			sb.append("_out_param_" + i);
		}
		sb.append(");\n");
		for(i = 0; i < pm.getReturnTypes().size(); ++i) {
			sb.appendFront("ReturnArray_" + pm.getIdent().toString() + "_" + type.getIdent().toString()
					+ "[" + i + "] = ");
			sb.append("_out_param_" + i + ";\n");
		}
		sb.appendFront("return ReturnArray_" + pm.getIdent().toString() + "_" + type.getIdent().toString() + ";\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genParameterPassingReturnArray(InheritanceType type, ProcedureMethod pm)
	{
		sb.appendFront("private static object[] ReturnArray_" + pm.getIdent().toString() + "_"
				+ type.getIdent().toString() + " = new object[" + pm.getReturnTypes().size()
				+ "]; // helper array for multi-value-returns, to allow for contravariant parameter assignment\n");
	}

	private void genMethods(InheritanceType type)
	{
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

	private void genApplyFunctionMethodDispatcher(InheritanceType type)
	{
		sb.appendFront("public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, "
				+ "string name, object[] arguments)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("switch(name)\n");
		sb.appendFront("{\n");
		sb.indent();
		for(FunctionMethod fm : type.getAllFunctionMethods()) {
			forceNotConstant(fm.getComputationStatements());
			genParameterPassingMethodCall(type, fm);
		}
		sb.appendFront("default: throw new NullReferenceException(\"" + formatIdentifiable(type)
				+ " does not have the function method \" + name + \"!\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genFunctionMethod(FunctionMethod fm)
	{
		List<String> staticInitializers = new LinkedList<String>();
		String pathPrefixForElements = "";
		HashMap<Entity, String> alreadyDefinedEntityToName = new HashMap<Entity, String>();
		genLocalContainersEvals(sb, fm.getComputationStatements(), staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);

		sb.appendFront("public " + formatType(fm.getReturnType()) + " ");
		sb.append(fm.getIdent().toString()
				+ "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_");
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
		ModifyGenerationState modifyGenState = new ModifyGenerationState(model, null, "", false,
				be.system.emitProfilingInstrumentation());
		ModifyEvalGen evalGen = new ModifyEvalGen(be, null, nodeTypePrefix, edgeTypePrefix);
		for(EvalStatement evalStmt : fm.getComputationStatements()) {
			modifyGenState.functionOrProcedureName = fm.getIdent().toString();
			evalGen.genEvalStmt(sb, modifyGenState, evalStmt);
		}
		sb.unindent();
		sb.appendFront("}\n");

		if(model.areFunctionsParallel()) {
			sb.appendFront("public " + formatType(fm.getReturnType()) + " ");
			sb.append(fm.getIdent().toString()
					+ "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_");
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
			modifyGenState = new ModifyGenerationState(model, null, "", true, be.system.emitProfilingInstrumentation());
			for(EvalStatement evalStmt : fm.getComputationStatements()) {
				modifyGenState.functionOrProcedureName = fm.getIdent().toString();
				evalGen.genEvalStmt(sb, modifyGenState, evalStmt);
			}
			sb.unindent();
			sb.append("}\n");
		}
	}

	private void genApplyProcedureMethodDispatcher(InheritanceType type)
	{
		sb.appendFront("public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph,"
				+ " string name, object[] arguments)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("switch(name)\n");
		sb.appendFront("{\n");
		sb.indent();
		for(ProcedureMethod pm : type.getAllProcedureMethods()) {
			genParameterPassingMethodCall(type, pm);
		}
		sb.appendFront("default: throw new NullReferenceException(\"" + formatIdentifiable(type)
				+ " does not have the procedure method \" + name + \"!\");\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genProcedureMethod(ProcedureMethod pm)
	{
		List<String> staticInitializers = new LinkedList<String>();
		String pathPrefixForElements = "";
		HashMap<Entity, String> alreadyDefinedEntityToName = new HashMap<Entity, String>();
		genLocalContainersEvals(sb, pm.getComputationStatements(), staticInitializers,
				pathPrefixForElements, alreadyDefinedEntityToName);

		sb.appendFront("public void ");
		sb.append(pm.getIdent().toString()
				+ "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_");
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
		ModifyGenerationState modifyGenState = new ModifyGenerationState(model, null, "", false,
				be.system.emitProfilingInstrumentation());
		ModifyExecGen execGen = new ModifyExecGen(be, nodeTypePrefix, edgeTypePrefix);
		ModifyEvalGen evalGen = new ModifyEvalGen(be, execGen, nodeTypePrefix, edgeTypePrefix);

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
			evalGen.genEvalStmt(sb, modifyGenState, evalStmt);
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
	private void genTypeImplementation(Collection<? extends InheritanceType> allTypes, InheritanceType type,
			String packageName)
	{
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

		sb.appendFront("public " + typename + "() "
				+ ": base((int) " + formatNodeOrEdge(type) + "Types.@" + typeident + ")\n");
		sb.appendFront("{\n");
		sb.indent();
		genAttributeInit(type);
		addAnnotations(sb, type, "annotations");
		sb.unindent();
		sb.appendFront("}\n");

		sb.appendFront("public override string Name { get { return \"" + typeident + "\"; } }\n");
		sb.appendFront("public override string Package { get { return "
				+ (!getPackagePrefix(type).equals("") ? "\"" + getPackagePrefix(type) + "\"" : "null") + "; } }\n");
		sb.appendFront("public override string PackagePrefixedName { get { return \""
				+ getPackagePrefixDoubleColon(type) + typeident + "\"; } }\n");
		switch(type.getIdent().toString()) {
		case "Node":
			sb.appendFront("public override string " + formatNodeOrEdge(type) + "InterfaceName { get { return "
					+ "\"de.unika.ipd.grGen.libGr.INode\"; } }\n");
			break;
		case "AEdge":
			sb.appendFront("public override string " + formatNodeOrEdge(type) + "InterfaceName { get { return "
					+ "\"de.unika.ipd.grGen.libGr.IEdge\"; } }\n");
			break;
		case "Edge":
			sb.appendFront("public override string " + formatNodeOrEdge(type) + "InterfaceName { get { return "
					+ "\"de.unika.ipd.grGen.libGr.IDEdge\"; } }\n");
			break;
		case "UEdge":
			sb.appendFront("public override string " + formatNodeOrEdge(type) + "InterfaceName { get { return "
					+ "\"de.unika.ipd.grGen.libGr.IUEdge\"; } }\n");
			break;
		default:
			sb.appendFront("public override string " + formatNodeOrEdge(type) + "InterfaceName { get { return "
					+ "\"de.unika.ipd.grGen.Model_" + model.getIdent() + "."
					+ getPackagePrefixDot(type) + "I" + getNodeOrEdgeTypePrefix(type) + formatIdentifiable(type)
					+ "\"; } }\n");
		}
		if(type.isAbstract()) {
			sb.appendFront("public override string " + formatNodeOrEdge(type) + "ClassName { get { return null; } }\n");
		} else {
			sb.appendFront("public override string " + formatNodeOrEdge(type)
					+ "ClassName { get { return \"de.unika.ipd.grGen.Model_"
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
		} else {
			EdgeType edgeType = (EdgeType)type;
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

		sb.appendFront("public override bool IsAbstract { get { return "
				+ (type.isAbstract() ? "true" : "false") + "; } }\n");
		sb.appendFront("public override bool IsConst { get { return "
				+ (type.isConst() ? "true" : "false") + "; } }\n");

		sb.appendFront("public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }\n");
		sb.appendFront("public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();\n");

		sb.appendFront("public override int NumAttributes { get { return " + type.getAllMembers().size() + "; } }\n");
		genAttributeTypesEnumerator(type);
		genGetAttributeType(type);

		sb.appendFront("public override int NumFunctionMethods { get { return "
				+ type.getAllFunctionMethods().size() + "; } }\n");
		genFunctionMethodsEnumerator(type);
		genGetFunctionMethod(type);

		sb.appendFront("public override int NumProcedureMethods { get { return "
				+ type.getAllProcedureMethods().size() + "; } }\n");
		genProcedureMethodsEnumerator(type);
		genGetProcedureMethod(type);

		sb.appendFront("public override bool IsA(GRGEN_LIBGR.GrGenType other)\n");
		sb.appendFront("{\n");
		sb.appendFrontIndented("return (this == other) || isA[other.TypeID];\n");
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

	private void genIsA(Collection<? extends InheritanceType> types, InheritanceType type)
	{
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

	private void genIsMyType(Collection<? extends InheritanceType> types, InheritanceType type)
	{
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

	private void genAttributeAttributes(InheritanceType type)
	{
		for(Entity member : type.getMembers()) { // only for locally defined members
			sb.appendFront("public static GRGEN_LIBGR.AttributeType " + formatAttributeTypeName(member) + ";\n");

			// attribute types T/S of map<T,S>/set<T>/array<T>/deque<T>
			if(member.getType() instanceof MapType) {
				sb.appendFront("public static GRGEN_LIBGR.AttributeType "
						+ formatAttributeTypeName(member) + "_map_domain_type;\n");
				sb.appendFront("public static GRGEN_LIBGR.AttributeType "
						+ formatAttributeTypeName(member) + "_map_range_type;\n");
			}
			if(member.getType() instanceof SetType) {
				sb.appendFront("public static GRGEN_LIBGR.AttributeType "
						+ formatAttributeTypeName(member) + "_set_member_type;\n");
			}
			if(member.getType() instanceof ArrayType) {
				sb.appendFront("public static GRGEN_LIBGR.AttributeType "
						+ formatAttributeTypeName(member) + "_array_member_type;\n");
			}
			if(member.getType() instanceof DequeType) {
				sb.appendFront("public static GRGEN_LIBGR.AttributeType "
						+ formatAttributeTypeName(member) + "_deque_member_type;\n");
			}
		}
	}

	private void genAttributeInit(InheritanceType type)
	{
		for(Entity e : type.getMembers()) {
			String attributeTypeName = formatAttributeTypeName(e);
			Type t = e.getType();

			if(t instanceof MapType) {
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
			} else if(t instanceof SetType) {
				SetType st = (SetType)t;

				// attribute type T of set<T>
				sb.appendFront(attributeTypeName + "_set_member_type = new GRGEN_LIBGR.AttributeType(");
				sb.append("\"" + formatIdentifiable(e) + "_set_member_type\", this, ");
				genAttributeInitTypeDependentStuff(st.getValueType(), e);
				sb.append(");\n");
			} else if(t instanceof ArrayType) {
				ArrayType at = (ArrayType)t;

				// attribute type T of set<T>
				sb.appendFront(attributeTypeName + "_array_member_type = new GRGEN_LIBGR.AttributeType(");
				sb.append("\"" + formatIdentifiable(e) + "_array_member_type\", this, ");
				genAttributeInitTypeDependentStuff(at.getValueType(), e);
				sb.append(");\n");
			} else if(t instanceof DequeType) {
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

			addAnnotations(sb, e, attributeTypeName + ".annotations");
		}
	}

	private void genAttributeInitTypeDependentStuff(Type t, Entity e)
	{
		if(t instanceof EnumType) {
			sb.append(getAttributeKind(t)
					+ ", GRGEN_MODEL." + getPackagePrefixDot(t) + "Enums.@" + formatIdentifiable(t) + ", "
					+ "null, null, "
					+ "null, null, null, typeof(" + formatAttributeType(t) + ")");
		} else if(t instanceof MapType) {
			sb.append(getAttributeKind(t) + ", null, "
					+ formatAttributeTypeName(e) + "_map_range_type" + ", "
					+ formatAttributeTypeName(e) + "_map_domain_type" + ", "
					+ "null, null, null, typeof(" + formatAttributeType(t) + ")");
		} else if(t instanceof SetType) {
			sb.append(getAttributeKind(t) + ", null, "
					+ formatAttributeTypeName(e) + "_set_member_type" + ", null, "
					+ "null, null, null, typeof(" + formatAttributeType(t) + ")");
		} else if(t instanceof ArrayType) {
			sb.append(getAttributeKind(t) + ", null, "
					+ formatAttributeTypeName(e) + "_array_member_type" + ", null, "
					+ "null, null, null, typeof(" + formatAttributeType(t) + ")");
		} else if(t instanceof DequeType) {
			sb.append(getAttributeKind(t) + ", null, "
					+ formatAttributeTypeName(e) + "_deque_member_type" + ", null, "
					+ "null, null, null, typeof(" + formatAttributeType(t) + ")");
		} else if(t instanceof NodeType || t instanceof EdgeType) {
			sb.append(getAttributeKind(t) + ", null, "
					+ "null, null, "
					+ "\"" + formatIdentifiable(t) + "\","
					+ (((ContainedInPackage)t).getPackageContainedIn() != ""
							? "\"" + ((ContainedInPackage)t).getPackageContainedIn() + "\""
							: "null")
					+ ","
					+ "\"" + getPackagePrefixDoubleColon(t) + formatIdentifiable(t) + "\","
					+ "typeof(" + formatElementInterfaceRef(t) + ")");
		} else {
			sb.append(getAttributeKind(t) + ", null, "
					+ "null, null, "
					+ "null, null, null, typeof(" + formatAttributeType(t) + ")");
		}
	}

	private String getAttributeKind(Type t)
	{
		if(t instanceof ByteType)
			return "GRGEN_LIBGR.AttributeKind.ByteAttr";
		else if(t instanceof ShortType)
			return "GRGEN_LIBGR.AttributeKind.ShortAttr";
		else if(t instanceof IntType)
			return "GRGEN_LIBGR.AttributeKind.IntegerAttr";
		else if(t instanceof LongType)
			return "GRGEN_LIBGR.AttributeKind.LongAttr";
		else if(t instanceof FloatType)
			return "GRGEN_LIBGR.AttributeKind.FloatAttr";
		else if(t instanceof DoubleType)
			return "GRGEN_LIBGR.AttributeKind.DoubleAttr";
		else if(t instanceof BooleanType)
			return "GRGEN_LIBGR.AttributeKind.BooleanAttr";
		else if(t instanceof StringType)
			return "GRGEN_LIBGR.AttributeKind.StringAttr";
		else if(t instanceof EnumType)
			return "GRGEN_LIBGR.AttributeKind.EnumAttr";
		else if(t instanceof ObjectType || t instanceof VoidType || t instanceof ExternalType)
			return "GRGEN_LIBGR.AttributeKind.ObjectAttr";
		else if(t instanceof MapType)
			return "GRGEN_LIBGR.AttributeKind.MapAttr";
		else if(t instanceof SetType)
			return "GRGEN_LIBGR.AttributeKind.SetAttr";
		else if(t instanceof ArrayType)
			return "GRGEN_LIBGR.AttributeKind.ArrayAttr";
		else if(t instanceof DequeType)
			return "GRGEN_LIBGR.AttributeKind.DequeAttr";
		else if(t instanceof NodeType)
			return "GRGEN_LIBGR.AttributeKind.NodeAttr";
		else if(t instanceof EdgeType)
			return "GRGEN_LIBGR.AttributeKind.EdgeAttr";
		else if(t instanceof GraphType)
			return "GRGEN_LIBGR.AttributeKind.GraphAttr";
		else
			throw new IllegalArgumentException("Unknown Type: " + t);
	}

	private void genAttributeTypesEnumerator(InheritanceType type)
	{
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
			sb.indent();
			for(Entity e : allMembers) {
				Type ownerType = e.getOwner();
				if(ownerType == type)
					sb.appendFront("yield return " + formatAttributeTypeName(e) + ";\n");
				else
					sb.appendFront("yield return " + formatTypeClassRef(ownerType) + "." + formatAttributeTypeName(e) + ";\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genGetAttributeType(InheritanceType type)
	{
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
			sb.indent();
			for(Entity e : allMembers) {
				Type ownerType = e.getOwner();
				if(ownerType == type)
					sb.appendFront("case \"" + formatIdentifiable(e) + "\" : return " +
							formatAttributeTypeName(e) + ";\n");
				else
					sb.appendFront("case \"" + formatIdentifiable(e) + "\" : return " +
							formatTypeClassRef(ownerType) + "." + formatAttributeTypeName(e) + ";\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
			sb.appendFront("return null;\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genFunctionMethodsEnumerator(InheritanceType type)
	{
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
			sb.indent();
			for(FunctionMethod fm : allFunctionMethods) {
				sb.appendFront("yield return " + formatFunctionMethodInfoName(fm, type) + ".Instance;\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genGetFunctionMethod(InheritanceType type)
	{
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
			sb.indent();
			for(FunctionMethod fm : allFunctionMethods) {
				sb.appendFront("case \"" + formatIdentifiable(fm) + "\" : return " +
						formatFunctionMethodInfoName(fm, type) + ".Instance;\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
			sb.appendFront("return null;\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genProcedureMethodsEnumerator(InheritanceType type)
	{
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
			sb.indent();
			for(ProcedureMethod pm : allProcedureMethods) {
				sb.appendFront("yield return " + formatProcedureMethodInfoName(pm, type) + ".Instance;\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genGetProcedureMethod(InheritanceType type)
	{
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
			sb.indent();
			for(ProcedureMethod pm : allProcedureMethods) {
				sb.appendFront("case \"" + formatIdentifiable(pm) + "\" : return " +
						formatProcedureMethodInfoName(pm, type) + ".Instance;\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
			sb.appendFront("return null;\n");
			sb.unindent();
			sb.append("}\n");
		}
	}

	private void getFirstCommonAncestors(InheritanceType curType,
			InheritanceType type, Set<InheritanceType> resTypes)
	{
		if(type.isCastableTo(curType))
			resTypes.add(curType);
		else {
			for(InheritanceType superType : curType.getDirectSuperTypes()) {
				getFirstCommonAncestors(superType, type, resTypes);
			}
		}
	}

	private void genCreateWithCopyCommons(InheritanceType type)
	{
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
		} else {
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
		} else {
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

	private Map<BitSet, LinkedList<InheritanceType>> getCommonGroups(InheritanceType type)
	{
		boolean isNode = type instanceof NodeType;

		Map<BitSet, LinkedList<InheritanceType>> commonGroups = new LinkedHashMap<BitSet, LinkedList<InheritanceType>>();

		Collection<? extends InheritanceType> typeSet = isNode
				? (Collection<? extends InheritanceType>)model.getAllNodeTypes()
				: (Collection<? extends InheritanceType>)model.getAllEdgeTypes();
		for(InheritanceType itype : typeSet) {
			if(itype.isAbstract())
				continue;

			Set<InheritanceType> firstCommonAncestors = new LinkedHashSet<InheritanceType>();
			getFirstCommonAncestors(itype, type, firstCommonAncestors);

			TreeSet<InheritanceType> sortedCommonTypes = new TreeSet<InheritanceType>(
					new Comparator<InheritanceType>() {
						public int compare(InheritanceType o1, InheritanceType o2)
						{
							return o2.getMaxDist() - o1.getMaxDist();
						}
					});

			sortedCommonTypes.addAll(firstCommonAncestors);
			Iterator<InheritanceType> iter = sortedCommonTypes.iterator();
			while(iter.hasNext()) {
				InheritanceType commonType = iter.next();
				if(!firstCommonAncestors.contains(commonType))
					continue;
				for(InheritanceType superType : commonType.getAllSuperTypes()) {
					firstCommonAncestors.remove(superType);
				}
			}

			boolean mustCopyAttribs = false;
commonLoop:
			for(InheritanceType commonType : firstCommonAncestors) {
				for(Entity member : commonType.getAllMembers()) {
					if(member.getType().isVoid()) // is it an abstract member?
						continue;
					mustCopyAttribs = true;
					break commonLoop;
				}
			}

			if(!mustCopyAttribs)
				continue;

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
			Map.Entry<BitSet, LinkedList<InheritanceType>> entry)
	{
		for(InheritanceType itype : entry.getValue()) {
			sb.appendFront("case (int) GRGEN_MODEL." + getPackagePrefixDot(itype) + kindName + "Types.@"
					+ formatIdentifiable(itype) + ":\n");
		}
		sb.indent();
		BitSet bitset = entry.getKey();
		HashSet<Entity> copiedAttribs = new HashSet<Entity>();
		for(int i = bitset.nextSetBit(0); i >= 0; i = bitset.nextSetBit(i + 1)) {
			InheritanceType commonType = InheritanceType.getByTypeID(i);
			Collection<Entity> members = commonType.getAllMembers();
			if(members.size() != 0) {
				sb.appendFront("// copy attributes for: "
						+ formatIdentifiable(commonType) + "\n");
				boolean alreadyCasted = false;
				for(Entity member : members) {
					if(member.isConst()) {
						sb.appendFrontIndented("// is const: " + formatIdentifiable(member) + "\n");
						continue;
					}
					if(member.getType().isVoid()) {
						sb.appendFrontIndented("// is abstract: " + formatIdentifiable(member) + "\n");
						continue;
					}
					if(copiedAttribs.contains(member)) {
						sb.appendFrontIndented("// already copied: " + formatIdentifiable(member) + "\n");
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
									+ " = new " + formatAttributeType(member.getType())
									+ "(old.@" + memberName + ");\n");
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
	private void genFunctionMethodInfo(FunctionMethod fm, InheritanceType type, String packageName)
	{
		String functionMethodName = formatIdentifiable(fm);
		String className = formatFunctionMethodInfoName(fm, type);

		sb.appendFront("public class " + className + " : GRGEN_LIBGR.FunctionInfo\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.appendFront("public static " + className + " Instance { get { if(instance==null) { "
				+ "instance = new " + className + "(); } return instance; } }\n");
		sb.append("\n");

		sb.appendFront("private " + className + "()\n");
		sb.indent();
		sb.appendFront(": base(\n");
		sb.indent();
		sb.appendFront("\"" + functionMethodName + "\",\n");
		sb.appendFront((packageName != null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\"" + (packageName != null
				? packageName + "::" + functionMethodName
				: functionMethodName) + "\",\n");
		sb.appendFront("false,\n");
		sb.appendFront("new String[] { ");
		for(Entity inParam : fm.getParameters()) {
			sb.append("\"" + inParam.getIdent() + "\", ");
		}
		sb.append(" },\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Entity inParam : fm.getParameters()) {
			if(inParam.getType() instanceof InheritanceType && !(inParam.getType() instanceof ExternalType)) {
				sb.append(formatTypeClassRef(inParam.getType()) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(inParam.getType()) + ")), ");
			}
		}
		sb.append(" },\n");
		Type outType = fm.getReturnType();
		if(outType instanceof InheritanceType && !(outType instanceof ExternalType)) {
			sb.appendFront(formatTypeClassRef(outType) + ".typeVar\n");
		} else {
			sb.appendFront("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(outType) + "))\n");
		}
		sb.unindent();
		sb.appendFront(")\n");
		sb.unindent();
		sb.appendFront("{\n");
		sb.appendFront("}\n");

		sb.appendFront("public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, "
				+ "object[] arguments)\n");
		sb.appendFront("{\n");
		sb.appendFrontIndented("throw new Exception(\"Not implemented, can't call function method without this object!\");\n");
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	/**
	 * Generates the procedure info for the given procedure method
	 */
	private void genProcedureMethodInfo(ProcedureMethod pm, InheritanceType type, String packageName)
	{
		String procedureMethodName = formatIdentifiable(pm);
		String className = formatProcedureMethodInfoName(pm, type);

		sb.appendFront("public class " + className + " : GRGEN_LIBGR.ProcedureInfo\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("private static " + className + " instance = null;\n");
		sb.append("public static " + className + " Instance { get { if(instance==null) { "
				+ "instance = new " + className + "(); } return instance; } }\n");
		sb.append("\n");

		sb.appendFront("private " + className + "()\n");
		sb.indent();
		sb.appendFront(": base(\n");
		sb.indent();
		sb.appendFront("\"" + procedureMethodName + "\",\n");
		sb.appendFront((packageName != null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\"" + (packageName != null
				? packageName + "::" + procedureMethodName
				: procedureMethodName) + "\",\n");
		sb.appendFront("false,\n");
		sb.appendFront("new String[] { ");
		for(Entity inParam : pm.getParameters()) {
			sb.append("\"" + inParam.getIdent() + "\", ");
		}
		sb.append(" },\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Entity inParam : pm.getParameters()) {
			if(inParam.getType() instanceof InheritanceType && !(inParam.getType() instanceof ExternalType)) {
				sb.append(formatTypeClassRef(inParam.getType()) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(inParam.getType()) + ")), ");
			}
		}
		sb.append(" },\n");
		sb.appendFront("new GRGEN_LIBGR.GrGenType[] { ");
		for(Type outType : pm.getReturnTypes()) {
			if(outType instanceof InheritanceType && !(outType instanceof ExternalType)) {
				sb.append(formatTypeClassRef(outType) + ".typeVar, ");
			} else {
				sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(outType) + ")), ");
			}
		}
		sb.append(" }\n");
		sb.unindent();
		sb.appendFront(")\n");
		sb.unindent();
		sb.appendFront("{\n");
		sb.appendFront("}\n");

		sb.appendFront("public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph,"
				+ " object[] arguments)\n");
		sb.appendFront("{\n");
		sb.appendFrontIndented("throw new Exception(\"Not implemented, can't call procedure method without this object!\");\n");
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genAttributeArrayHelpersAndComparers(InheritanceType type)
	{
		for(Entity entity : type.getAllMembers()) {
			if(entity.getType().isFilterableType()
					|| entity.getType().classify() == Type.IS_EXTERNAL_TYPE
					|| entity.getType().classify() == Type.IS_OBJECT) {
				if((entity.getType().classify() == Type.IS_EXTERNAL_TYPE
						|| entity.getType().classify() == Type.IS_OBJECT)
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
		else {
			for(InheritanceType subtype : type.getAllSubTypes()) {
				if(!subtype.isAbstract() && type.getExternalName() == null) {
					nonAbstractTypeOrSubtype = subtype;
					break;
				}
			}
		}

		if(nonAbstractTypeOrSubtype == null)
			return; // can't generate comparer for abstract types that have no concrete subtype

		if(entity.getType().isOrderableType())
			genAttributeArrayReverseComparer(type, entity);

		sb.append("\n");
		if(entity.getType().isOrderableType())
			sb.appendFront("public class " + comparerClassName + " : Comparer<" + typeName + ">\n");
		else
			sb.appendFront("public class " + comparerClassName + "\n");
		sb.appendFront("{\n");
		sb.indent();

		if(type instanceof EdgeType) {
			sb.appendFront("private static " + formatElementInterfaceRef(type) + " nodeBearingAttributeForSearch = "
					+ "new " + formatElementClassRef(nonAbstractTypeOrSubtype) + "(null, null);\n");
		} else {
			sb.appendFront("private static " + formatElementInterfaceRef(type) + " nodeBearingAttributeForSearch = "
					+ "new " + formatElementClassRef(nonAbstractTypeOrSubtype) + "();\n");
		}

		sb.appendFront("private static " + comparerClassName + " thisComparer = new " + comparerClassName + "();\n");

		if(entity.getType().isOrderableType())
			genCompareMethod(sb, typeName, formatIdentifiable(entity), entity.getType(), true);

		genIndexOfByMethod(typeName, attributeName, attributeTypeName);
		genIndexOfByWithStartMethod(typeName, attributeName, attributeTypeName);

		genLastIndexOfByMethod(typeName, attributeName, attributeTypeName);
		genLastIndexOfByWithStartMethod(typeName, attributeName, attributeTypeName);

		if(entity.getType().isOrderableType()) {
			genIndexOfOrderedByMethod(typeName, attributeName, attributeTypeName);

			genArrayOrderAscendingByMethod(typeName);
			genArrayOrderDescendingByMethod(typeName, reverseComparerClassName);
		}

		generateArrayKeepOneForEach(sb, "ArrayKeepOneForEachBy", typeName, attributeName, attributeTypeName);

		genArrayExtractMethod(typeName, attributeName, attributeTypeName);

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	void genIndexOfByMethod(String typeName, String attributeName, String attributeTypeName)
	{
		sb.appendFront("public static int IndexOfBy(IList<" + typeName + "> list, " + attributeTypeName + " entry)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("for(int i = 0; i < list.Count; ++i)\n");
		sb.indent();
		sb.appendFront("if(list[i].@" + attributeName + ".Equals(entry))\n");
		sb.appendFrontIndented("return i;\n");
		sb.unindent();
		sb.appendFront("return -1;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	void genIndexOfByWithStartMethod(String typeName, String attributeName, String attributeTypeName)
	{
		sb.appendFront("public static int IndexOfBy(IList<" + typeName + "> list, "
				+ attributeTypeName + " entry, int startIndex)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("for(int i = startIndex; i < list.Count; ++i)\n");
		sb.indent();
		sb.appendFront("if(list[i].@" + attributeName + ".Equals(entry))\n");
		sb.appendFrontIndented("return i;\n");
		sb.unindent();
		sb.appendFront("return -1;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	void genLastIndexOfByMethod(String typeName, String attributeName, String attributeTypeName)
	{
		sb.appendFront("public static int LastIndexOfBy(IList<" + typeName + "> list, "
				+ attributeTypeName + " entry)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("for(int i = list.Count - 1; i >= 0; --i)\n");
		sb.indent();
		sb.appendFront("if(list[i].@" + attributeName + ".Equals(entry))\n");
		sb.appendFrontIndented("return i;\n");
		sb.unindent();
		sb.appendFront("return -1;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	void genLastIndexOfByWithStartMethod(String typeName, String attributeName, String attributeTypeName)
	{
		sb.appendFront("public static int LastIndexOfBy(IList<" + typeName + "> list, "
				+ attributeTypeName + " entry, int startIndex)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("for(int i = startIndex; i >= 0; --i)\n");
		sb.indent();
		sb.appendFront("if(list[i].@" + attributeName + ".Equals(entry))\n");
		sb.appendFrontIndented("return i;\n");
		sb.unindent();
		sb.appendFront("return -1;\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	void genIndexOfOrderedByMethod(String typeName, String attributeName, String attributeTypeName)
	{
		sb.appendFront("public static int IndexOfOrderedBy(List<" + typeName + "> list, "
				+ attributeTypeName + " entry)\n");
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
		sb.appendFront("List<" + attributeTypeName + "> resultList = new List<"
				+ attributeTypeName + ">(list.Count);\n");
		sb.appendFront("foreach(" + typeName + " entry in list)\n");
		sb.appendFrontIndented("resultList.Add(entry.@" + attributeName + ");\n");
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

		sb.appendFront("public static " + reverseComparerClassName + " thisComparer = "
				+ "new " + reverseComparerClassName + "();\n");

		genCompareMethod(sb, typeName, formatIdentifiable(entity), entity.getType(), false);

		sb.unindent();
		sb.appendFront("}\n");
	}

	////////////////////////////
	// Model class generation //
	////////////////////////////

	/**
	 * Generates the model class for the edge or node types.
	 */
	private void genModelClass(Collection<? extends InheritanceType> types, boolean isNode)
	{
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

		sb.appendFront("public bool IsNodeModel { get { return " + (isNode ? "true" : "false") + "; } }\n");
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
			sb.appendFront("case \"" + getPackagePrefixDoubleColon(type) + formatIdentifiable(type) + "\" : "
					+ "return " + formatTypeClassRef(type) + ".typeVar;\n");
		}
		sb.unindent();
		sb.appendFront("}\n");
		sb.appendFront("return null;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.appendFront("GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(string name)\n");
		sb.appendFront("{\n");
		sb.appendFrontIndented("return GetType(name);\n");
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

	private InheritanceType genModelConstructor(boolean isNode, Collection<? extends InheritanceType> types)
	{
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
	private void genGraphModel()
	{
		String modelName = model.getIdent().toString();
		sb.appendFront("//\n");
		sb.appendFront("// IGraphModel (LGSPGraphModel) implementation\n");
		sb.appendFront("//\n");

		sb.appendFront("public sealed class " + modelName + "GraphModel : GRGEN_LGSP.LGSPGraphModel\n");
		sb.appendFront("{\n");
		sb.indent();

		sb.appendFront("public " + modelName + "GraphModel()\n");
		sb.appendFront("{\n");
		sb.appendFrontIndented("FullyInitializeExternalTypes();\n");
		sb.appendFront("}\n");
		sb.append("\n");

		genGraphModelBody(modelName);

		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genGraphClass()
	{
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

	private void genNamedGraphClass()
	{
		String modelName = model.getIdent().toString();

		sb.appendFront("//\n");
		sb.appendFront("// INamedGraph (LGSPNamedGraph) implementation\n");
		sb.appendFront("//\n");

		sb.appendFront("public class " + modelName + "NamedGraph : GRGEN_LGSP.LGSPNamedGraph\n");
		sb.appendFront("{\n");
		sb.indent();

		sb.appendFront("public " + modelName + "NamedGraph() "
				+ ": base(new " + modelName + "GraphModel(), GetGraphName(), 0)\n");
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

	private void genCreateNodeConvenienceHelper(NodeType nodeType, boolean isNamed)
	{
		if(nodeType.isAbstract())
			return;

		String name = getPackagePrefix(nodeType) + formatIdentifiable(nodeType);
		String elemref = formatElementClassRef(nodeType);
		sb.appendFront("public " + elemref + " CreateNode" + name + "()\n");
		sb.appendFront("{\n");
		sb.appendFrontIndented("return " + elemref + ".CreateNode(this);\n");
		sb.appendFront("}\n");
		sb.append("\n");

		if(!isNamed)
			return;

		sb.appendFront("public " + elemref + " CreateNode" + name + "(string nodeName)\n");
		sb.appendFront("{\n");
		sb.appendFrontIndented("return " + elemref + ".CreateNode(this, nodeName);\n");
		sb.appendFront("}\n");
		sb.append("\n");
	}

	private void genCreateEdgeConvenienceHelper(EdgeType edgeType, boolean isNamed)
	{
		if(edgeType.isAbstract())
			return;

		String name = getPackagePrefix(edgeType) + formatIdentifiable(edgeType);
		String elemref = formatElementClassRef(edgeType);
		sb.appendFront("public @" + elemref + " CreateEdge" + name);
		sb.append("(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)\n");
		sb.appendFront("{\n");
		sb.appendFrontIndented("return @" + elemref + ".CreateEdge(this, source, target);\n");
		sb.appendFront("}\n");
		sb.append("\n");

		if(!isNamed)
			return;

		sb.appendFront("public @" + elemref + " CreateEdge" + name);
		sb.append("(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)\n");
		sb.appendFront("{\n");
		sb.appendFrontIndented("return @" + elemref + ".CreateEdge(this, source, target, edgeName);\n");
		sb.appendFront("}\n");
		sb.append("\n");
	}

	private void genGraphModelBody(String modelName)
	{
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
		sb.indent();
		sb.appendFront("if(indexDescriptions[i].Name==indexName)\n");
		sb.appendFrontIndented("return indexDescriptions[i];\n");
		sb.unindent();
		sb.appendFront("return null;\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.appendFront("public override bool GraphElementUniquenessIsEnsured { get { return "
				+ (model.isUniqueDefined() ? "true" : "false") + "; } }\n");
		sb.appendFront("public override bool GraphElementsAreAccessibleByUniqueId { get { return "
				+ (model.isUniqueIndexDefined() ? "true" : "false") + "; } }\n");
		sb.appendFront("public override bool AreFunctionsParallelized { get { return "
				+ model.areFunctionsParallel() + "; } }\n");
		sb.appendFront("public override int BranchingFactorForEqualsAny { get { return "
				+ model.isoParallel() + "; } }\n");

		genGraphModelBodyAccessToExternalParts();

		sb.append("\n");
		sb.appendFront("public override void FailAssertion() { Debug.Assert(false); }\n");
		sb.appendFront("public override string MD5Hash { get { return \"" + be.unit.getTypeDigest() + "\"; } }\n");
	}

	private void genGraphModelBodyAccessToExternalParts()
	{
		if(model.isEmitClassDefined()) {
			sb.append("\n");
			sb.appendFront("public override object Parse(TextReader reader, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
			sb.appendFront("{\n");
			sb.appendFrontIndented("return AttributeTypeObjectEmitterParser.Parse(reader, attrType, graph);\n");
			sb.appendFront("}\n");
			sb.appendFront("public override string Serialize(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
			sb.appendFront("{\n");
			sb.appendFrontIndented("return AttributeTypeObjectEmitterParser.Serialize(attribute, attrType, graph);\n");
			sb.appendFront("}\n");
			sb.appendFront("public override string Emit(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
			sb.appendFront("{\n");
			sb.appendFrontIndented("return AttributeTypeObjectEmitterParser.Emit(attribute, attrType, graph);\n");
			sb.appendFront("}\n");
			sb.appendFront("public override void External(string line, GRGEN_LIBGR.IGraph graph)\n");
			sb.appendFront("{\n");
			sb.appendFrontIndented("AttributeTypeObjectEmitterParser.External(line, graph);\n");
			sb.appendFront("}\n");
		}
		if(model.isEmitGraphClassDefined()) {
			sb.append("\n");
			sb.appendFront("public override GRGEN_LIBGR.INamedGraph AsGraph(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
			sb.appendFront("{\n");
			sb.appendFrontIndented("return AttributeTypeObjectEmitterParser.AsGraph(attribute, attrType, graph);\n");
			sb.appendFront("}\n");
		}

		genExternalTypes();
		sb.appendFront("public override GRGEN_LIBGR.ExternalType[] ExternalTypes { get { return externalTypes; } }\n");

		sb.append("\n");
		sb.appendFront("private void FullyInitializeExternalTypes()\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("externalType_object.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { } );\n");
		for(ExternalType et : model.getExternalTypes()) {
			sb.appendFront("externalType_" + et.getIdent() + ".InitDirectSupertypes( "
					+ "new GRGEN_LIBGR.ExternalType[] { ");
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

		if(model.isEqualClassDefined() && model.isLowerClassDefined()) {
			sb.append("\n");
			sb.appendFront("public override bool IsEqualClassDefined { get { return true; } }\n");
			sb.appendFront("public override bool IsLowerClassDefined { get { return true; } }\n");
			sb.appendFront("public override bool IsEqual(object this_, object that)\n");
			sb.appendFront("{\n");
			sb.appendFrontIndented("return AttributeTypeObjectCopierComparer.IsEqual(this_, that);\n");
			sb.appendFront("}\n");
			sb.appendFront("public override bool IsLower(object this_, object that)\n");
			sb.appendFront("{\n");
			sb.appendFrontIndented("return AttributeTypeObjectCopierComparer.IsLower(this_, that);\n");
			sb.appendFront("}\n");
		} else if(model.isEqualClassDefined()) {
			sb.append("\n");
			sb.appendFront("public override bool IsEqualClassDefined { get { return true; } }\n");
			sb.appendFront("public override bool IsEqual(object this_, object that)\n");
			sb.appendFront("{\n");
			sb.appendFrontIndented("return AttributeTypeObjectCopierComparer.IsEqual(this_, that);\n");
			sb.appendFront("}\n");
		}
	}

	private void genExternalTypes()
	{
		sb.append("\n");
		sb.appendFront("public static GRGEN_LIBGR.ExternalType externalType_object = new ExternalType_object();\n");
		for(ExternalType et : model.getExternalTypes()) {
			sb.appendFront("public static GRGEN_LIBGR.ExternalType externalType_" + et.getIdent()
					+ " = new ExternalType_" + et.getIdent() + "();\n");
		}

		sb.appendFront("private GRGEN_LIBGR.ExternalType[] externalTypes = { ");
		sb.append("externalType_object");
		for(ExternalType et : model.getExternalTypes()) {
			sb.append(", externalType_" + et.getIdent());
		}
		sb.append(" };\n");
	}

	private void genPackages()
	{
		sb.appendFront("private string[] packages = {\n");
		sb.indent();
		for(PackageType pt : model.getPackages()) {
			sb.appendFront("\"" + pt.getIdent() + "\",\n");
		}
		sb.unindent();
		sb.appendFront("};\n");
	}

	private void genValidates()
	{
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

	private void genValidate(EdgeType edgeType)
	{
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

	private void genIndexDescriptions()
	{
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

	private void genIndexDescription(AttributeIndex index)
	{
		sb.appendFront("new GRGEN_LIBGR.AttributeIndexDescription(");
		sb.append("\"" + index.getIdent() + "\", ");
		sb.append(formatTypeClassName(index.type) + ".typeVar, ");
		sb.append(formatTypeClassName(index.type) + "." + formatAttributeTypeName(index.entity));
		sb.append("),\n");
	}

	private void genIndexDescription(IncidenceCountIndex index)
	{
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

	private void genIndicesGraphBinding()
	{
		sb.appendFront("public override GRGEN_LIBGR.IUniquenessHandler CreateUniquenessHandler(GRGEN_LIBGR.IGraph graph) {\n");
		sb.indent();
		if(model.isUniqueIndexDefined())
			sb.appendFront("return new GRGEN_LGSP.LGSPUniquenessIndex((GRGEN_LGSP.LGSPGraph)graph); "
					+ "// must be called before the indices so that its event handler is registered first, doing the unique id computation the indices depend upon\n");
		else if(model.isUniqueDefined())
			sb.appendFront("return new GRGEN_LGSP.LGSPUniquenessEnsurer((GRGEN_LGSP.LGSPGraph)graph); "
					+ "// must be called before the indices so that its event handler is registered first, doing the unique id computation the indices depend upon\n");
		else
			sb.appendFront("return null;\n");
		sb.unindent();
		sb.appendFront("}\n");

		sb.appendFront("public override GRGEN_LIBGR.IIndexSet CreateIndexSet(GRGEN_LIBGR.IGraph graph) {\n");
		sb.appendFrontIndented("return new " + model.getIdent() + "IndexSet((GRGEN_LGSP.LGSPGraph)graph);\n");
		sb.appendFront("}\n");

		sb.appendFront("public override void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, "
				+ "IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {\n");
		sb.indent();
		if(model.isUniqueDefined()) {
			sb.appendFront("((GRGEN_LGSP.LGSPUniquenessEnsurer)graph.UniquenessHandler).FillAsClone("
					+ "(GRGEN_LGSP.LGSPUniquenessEnsurer)originalGraph.UniquenessHandler, oldToNewMap);\n");
		}
		sb.appendFront("((" + model.getIdent()+ "IndexSet)graph.Indices).FillAsClone("
				+ "(GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genEnumAttributeTypes()
	{
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

	private void genEnumAttributeType(EnumType enumt)
	{
		sb.appendFront("GRGEN_MODEL." + getPackagePrefixDot(enumt) + "Enums.@" + formatIdentifiable(enumt) + ",\n");
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
}
