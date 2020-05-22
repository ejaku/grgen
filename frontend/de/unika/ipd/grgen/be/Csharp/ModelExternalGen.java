/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Generates the external part / external functions file for the SearchPlanBackend2 model.
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.be.Csharp;

import java.util.Collection;
import java.util.Date;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.executable.ExternalFunction;
import de.unika.ipd.grgen.ir.executable.ExternalFunctionMethod;
import de.unika.ipd.grgen.ir.executable.ExternalProcedure;
import de.unika.ipd.grgen.ir.executable.ExternalProcedureMethod;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.model.Model;
import de.unika.ipd.grgen.ir.model.type.ExternalType;
import de.unika.ipd.grgen.ir.model.type.InheritanceType;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.util.SourceBuilder;

public class ModelExternalGen extends CSharpBase
{
	public ModelExternalGen(Model model, SourceBuilder sb, String nodeTypePrefix, String edgeTypePrefix)
	{
		super(nodeTypePrefix, edgeTypePrefix);
		this.model = model;
		this.sb = sb;
	}

	public void genExternalFunctionsFile(String filename)
	{
		sb.appendFront("// This file has been generated automatically by GrGen (www.grgen.net)\n"
				+ "// Do not modify this file! Any changes will be lost!\n"
				+ "// Generated from \"" + filename + "\" on " + new Date() + "\n"
				+ "\n"
				+ "using System;\n"
				+ "using System.Collections.Generic;\n"
				+ "using System.IO;\n"
				+ "using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n"
				+ "using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n"
				+ "using GRGEN_MODEL = de.unika.ipd.grGen.Model_" + model.getIdent() + ";\n");

		if(!model.getExternalTypes().isEmpty() || model.isEmitClassDefined() || model.isEmitGraphClassDefined()) {
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

		if(!model.getExternalFunctions().isEmpty()) {
			sb.append("\n");
			sb.appendFront("namespace de.unika.ipd.grGen.expression\n");
			sb.appendFront("{\n");
			sb.indent();

			sb.appendFront("public partial class ExternalFunctions\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("// You must implement the following functions in the same partial class in ./"
					+ model.getIdent() + "ModelExternalFunctionsImpl.cs:\n");
			sb.append("\n");

			genExternalFunctionHeaders();

			sb.unindent();
			sb.appendFront("}\n");

			sb.unindent();
			sb.appendFront("}\n");
		}

		if(!model.getExternalProcedures().isEmpty()) {
			sb.append("\n");
			sb.appendFront("namespace de.unika.ipd.grGen.expression\n");
			sb.appendFront("{\n");
			sb.indent();

			sb.appendFront("public partial class ExternalProcedures\n");
			sb.appendFront("{\n");
			sb.indent();
			sb.appendFront("// You must implement the following procedures in the same partial class in ./"
					+ model.getIdent() + "ModelExternalFunctionsImpl.cs:\n");
			sb.append("\n");

			genExternalProcedureHeaders();

			sb.unindent();
			sb.appendFront("}\n");

			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	///////////////////////////////
	// External stuff generation //
	///////////////////////////////

	/**
	 * Generates the external type implementation
	 */
	public void genExternalType(ExternalType type)
	{
		sb.append("\n");
		sb.appendFront("public sealed class ExternalType_" + type.getIdent() + " : GRGEN_LIBGR.ExternalType\n");
		sb.appendFront("{\n");
		sb.indent();

		sb.appendFront("public ExternalType_" + type.getIdent() + "()\n");
		sb.appendFrontIndented(": base(\"" + type.getIdent() + "\", typeof(" + type.getIdent() + "))\n");
		sb.appendFront("{\n");
		sb.appendFront("}\n");

		sb.appendFront("public override int NumFunctionMethods { get { return "
				+ type.getAllExternalFunctionMethods().size() + "; } }\n");
		genExternalFunctionMethodsEnumerator(type);
		genGetExternalFunctionMethod(type);

		sb.appendFront("public override int NumProcedureMethods { get { return "
				+ type.getAllExternalProcedureMethods().size() + "; } }\n");
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

	private void genExternalFunctionMethodsEnumerator(ExternalType type)
	{
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
				sb.appendFront("yield return " + formatExternalFunctionMethodInfoName(efm, type) + ".Instance;\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genGetExternalFunctionMethod(ExternalType type)
	{
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
				sb.appendFront("case \"" + formatIdentifiable(efm) + "\" : return " +
						formatExternalFunctionMethodInfoName(efm, type) + ".Instance;\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
			sb.appendFront("return null;\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	private void genExternalProcedureMethodsEnumerator(ExternalType type)
	{
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

	private void genGetExternalProcedureMethod(ExternalType type)
	{
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
				sb.appendFront("case \"" + formatIdentifiable(epm) + "\" : return " +
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
	private void genExternalFunctionMethodInfo(ExternalFunctionMethod efm, ExternalType type, String packageName)
	{
		String externalFunctionMethodName = formatIdentifiable(efm);
		String className = formatExternalFunctionMethodInfoName(efm, type);

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
		sb.appendFront("\"" + externalFunctionMethodName + "\",\n");
		sb.appendFront((packageName != null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\""
				+ (packageName != null ? packageName + "::" + externalFunctionMethodName : externalFunctionMethodName)
				+ "\",\n");
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
			if(inParamType instanceof InheritanceType && !(inParamType instanceof ExternalType)) {
				sb.appendFront(formatTypeClassRef(inParamType) + ".typeVar, ");
			} else {
				sb.appendFront("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(inParamType) + ")), ");
			}
		}
		sb.append(" },\n");
		Type outType = efm.getReturnType();
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

		sb.appendFront("public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)\n");
		sb.appendFront("{\n");
		sb.appendFrontIndented("throw new Exception(\"Not implemented, can't call function method without this object!\");\n");
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	/**
	 * Generates the procedure info for the given external procedure method
	 */
	private void genExternalProcedureMethodInfo(ExternalProcedureMethod epm, ExternalType type, String packageName)
	{
		String externalProcedureMethodName = formatIdentifiable(epm);
		String className = formatExternalProcedureMethodInfoName(epm, type);

		sb.appendFront("public class " + className + " : GRGEN_LIBGR.ProcedureInfo\n");
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
		sb.appendFront("\"" + externalProcedureMethodName + "\",\n");
		sb.appendFront((packageName != null ? "\"" + packageName + "\"" : "null") + ", ");
		sb.append("\""
				+ (packageName != null ? packageName + "::" + externalProcedureMethodName : externalProcedureMethodName)
				+ "\",\n");
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
		sb.unindent();
		sb.appendFront(")\n");
		sb.unindent();
		sb.appendFront("{\n");
		sb.appendFront("}\n");

		sb.appendFront("public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)\n");
		sb.appendFront("{\n");
		sb.appendFrontIndented("throw new Exception(\"Not implemented, can't call procedure method without this object!\");\n");
		sb.appendFront("}\n");

		sb.unindent();
		sb.appendFront("}\n");
		sb.append("\n");
	}

	public void genExternalTypeObject()
	{
		sb.append("\n");
		sb.appendFront("public sealed class ExternalType_object : GRGEN_LIBGR.ExternalType\n");
		sb.appendFront("{\n");
		sb.indent();

		sb.appendFront("public ExternalType_object()\n");
		sb.appendFrontIndented(": base(\"object\", typeof(object))\n");
		sb.appendFront("{\n");
		sb.appendFront("}\n");

		sb.appendFront("public override int NumFunctionMethods { get { return 0; } }\n");
		sb.appendFront("public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods "
				+ "{ get { yield break; } }\n");
		sb.appendFront("public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) "
				+ "{ return null; }\n");
		sb.appendFront("public override int NumProcedureMethods { get { return 0; } }\n");
		sb.appendFront("public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods "
				+ "{ get { yield break; } }\n");
		sb.appendFront("public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) "
				+ "{ return null; }\n");

		sb.unindent();
		sb.appendFront("}\n");
	}

	private void genExternalClasses()
	{
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
			sb.appendFront("// You must implement this class in the same partial class in ./" + model.getIdent()
					+ "ModelExternalFunctionsImpl.cs:\n");

			genExtMethods(et);

			sb.unindent();
			sb.appendFront("}\n");
			sb.append("\n");
		}
	}

	private void genExtMethods(ExternalType type)
	{
		if(type.getAllExternalFunctionMethods().size() == 0 && type.getAllExternalProcedureMethods().size() == 0)
			return;

		sb.append("\n");
		sb.appendFront("// You must implement the following methods in the same partial class in ./"
				+ model.getIdent() + "ModelExternalFunctionsImpl.cs:\n");

		for(ExternalFunctionMethod efm : type.getAllExternalFunctionMethods()) {
			sb.appendFront("//public " + formatType(efm.getReturnType()) + " ");
			sb.append(efm.getIdent().toString() + "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph");
			for(Type inParamType : efm.getParameterTypes()) {
				sb.append(", ");
				sb.append(formatType(inParamType));
			}
			sb.append(");\n");

			if(model.areFunctionsParallel()) {
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
			sb.append(epm.getIdent().toString()
					+ "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, GRGEN_LIBGR.IGraphElement");
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

	private void genParameterPassingReturnArray(ExternalType type, ExternalProcedureMethod epm)
	{
		sb.appendFront("private static object[] ReturnArray_" + epm.getIdent().toString() + "_"
				+ type.getIdent().toString() + " = new object[" + epm.getReturnTypes().size()
				+ "]; // helper array for multi-value-returns, to allow for contravariant parameter assignment\n");
	}

	private void genEmitterParserClass()
	{
		sb.appendFront("public partial class AttributeTypeObjectEmitterParser");
		sb.append("\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("// You must implement this class in the same partial class in ./"
				+ model.getIdent() + "ModelExternalFunctionsImpl.cs:\n");
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

	private void genCopierComparerClass()
	{
		sb.appendFront("public partial class AttributeTypeObjectCopierComparer\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("// You must implement the following functions in the same partial class in ./"
				+ model.getIdent() + "ModelExternalFunctionsImpl.cs:\n");
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

	private void genExternalFunctionHeaders()
	{
		for(ExternalFunction ef : model.getExternalFunctions()) {
			Type returnType = ef.getReturnType();
			sb.appendFront("//public static " + formatType(returnType) + " " + ef.getName()
					+ "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph");
			for(Type paramType : ef.getParameterTypes()) {
				sb.append(", ");
				sb.append(formatType(paramType));
			}
			sb.append(");\n");

			if(model.areFunctionsParallel()) {
				sb.appendFront("//public static " + formatType(returnType) + " " + ef.getName()
						+ "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph");
				for(Type paramType : ef.getParameterTypes()) {
					sb.append(", ");
					sb.append(formatType(paramType));
				}
				sb.append(", int threadId");
				sb.append(");\n");
			}
		}
	}

	private void genExternalProcedureHeaders()
	{
		for(ExternalProcedure ep : model.getExternalProcedures()) {
			sb.appendFront("//public static void " + ep.getName()
					+ "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph");
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
