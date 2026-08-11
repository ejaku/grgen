/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Generates the external part / external functions file for the SearchPlanBackend2 model.
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.be.Csharp
{

	using System;
	using System.Collections.Generic;

	using de.unika.ipd.grgen.ir;
	using ExternalFunction = de.unika.ipd.grgen.ir.executable.ExternalFunction;
	using ExternalFunctionMethod = de.unika.ipd.grgen.ir.executable.ExternalFunctionMethod;
	using ExternalProcedure = de.unika.ipd.grgen.ir.executable.ExternalProcedure;
	using ExternalProcedureMethod = de.unika.ipd.grgen.ir.executable.ExternalProcedureMethod;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using Model = de.unika.ipd.grgen.ir.model.Model;
	using ExternalObjectType = de.unika.ipd.grgen.ir.model.type.ExternalObjectType;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using SourceBuilder = de.unika.ipd.grgen.util.SourceBuilder;

	public class ModelExternalGen : CSharpBase
	{
		public ModelExternalGen(Model model, SourceBuilder sb, string nodeTypePrefix, string edgeTypePrefix,
				string objectTypePrefix, string transientObjectTypePrefix)
			: base(nodeTypePrefix, edgeTypePrefix, objectTypePrefix, transientObjectTypePrefix)
		{
			this.model = model;
			this.sb = sb;
		}

		public virtual void GenExternalFunctionsFile(string filename)
		{
			sb.AppendFront("// This file has been generated automatically by GrGen (www.grgen.net)\n"
					+ "// Do not modify this file! Any changes will be lost!\n"
					+ "// Generated from \"" + filename + "\" on " + DateTime.Now + "\n"
					+ "\n"
					+ "using System;\n"
					+ "using System.Collections.Generic;\n"
					+ "using System.IO;\n"
					+ "using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n"
					+ "using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n"
					+ "using GRGEN_MODEL = de.unika.ipd.grGen.Model_" + model.Ident + ";\n");

			if(model.ExternalObjectTypes.Count > 0 || model.IsEmitClassDefined() || model.IsEmitGraphClassDefined())
			{
				sb.Append("\n");
				sb.AppendFront("namespace de.unika.ipd.grGen.Model_" + model.Ident + "\n"
						+ "{\n");
				sb.Indent();

				GenExternalClasses();

				if(model.IsEmitClassDefined() || model.IsEmitGraphClassDefined())
					GenEmitterParserClass();

				if(model.IsCopyClassDefined() || model.IsEqualClassDefined() || model.IsLowerClassDefined())
					GenCopierComparerClass();

				sb.Unindent();
				sb.AppendFront("}\n");
			}

			if(model.ExternalFunctions.Count > 0)
			{
				sb.Append("\n");
				sb.AppendFront("namespace de.unika.ipd.grGen.expression\n");
				sb.AppendFront("{\n");
				sb.Indent();

				sb.AppendFront("public partial class ExternalFunctions\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("// You must implement the following functions in the same partial class in ./"
						+ model.Ident + "ModelExternalFunctionsImpl.cs:\n");
				sb.Append("\n");

				GenExternalFunctionHeaders();

				sb.Unindent();
				sb.AppendFront("}\n");

				sb.Unindent();
				sb.AppendFront("}\n");
			}

			if(model.ExternalProcedures.Count > 0)
			{
				sb.Append("\n");
				sb.AppendFront("namespace de.unika.ipd.grGen.expression\n");
				sb.AppendFront("{\n");
				sb.Indent();

				sb.AppendFront("public partial class ExternalProcedures\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("// You must implement the following procedures in the same partial class in ./"
						+ model.Ident + "ModelExternalFunctionsImpl.cs:\n");
				sb.Append("\n");

				GenExternalProcedureHeaders();

				sb.Unindent();
				sb.AppendFront("}\n");

				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		///////////////////////////////
		// External stuff generation //
		///////////////////////////////

		/// <summary>
		/// Generates the external object type implementation
		/// </summary>
		public virtual void GenExternalObjectType(ExternalObjectType type)
		{
			sb.Append("\n");
			sb.AppendFront("public sealed class ExternalObjectType_" + type.Ident + " : GRGEN_LIBGR.ExternalObjectType\n");
			sb.AppendFront("{\n");
			sb.Indent();

			sb.AppendFront("public ExternalObjectType_" + type.Ident + "()\n");
			sb.AppendFrontIndented(": base(\"" + type.Ident + "\", typeof(" + type.Ident + "))\n");
			sb.AppendFront("{\n");
			sb.AppendFront("}\n");

			sb.AppendFront("public override int NumFunctionMethods { get { return "
					+ type.AllExternalFunctionMethods.Count + "; } }\n");
			GenExternalFunctionMethodsEnumerator(type);
			GenGetExternalFunctionMethod(type);

			sb.AppendFront("public override int NumProcedureMethods { get { return "
					+ type.AllExternalProcedureMethods.Count + "; } }\n");
			GenExternalProcedureMethodsEnumerator(type);
			GenGetExternalProcedureMethod(type);

			sb.Unindent();
			sb.Append("}\n");

			// generate function method info classes
			ICollection<ExternalFunctionMethod> allExternalFunctionMethods = type.AllExternalFunctionMethods;
			foreach(ExternalFunctionMethod efm in allExternalFunctionMethods)
				GenExternalFunctionMethodInfo(efm, type, null);

			// generate procedure method info classes
			ICollection<ExternalProcedureMethod> allExternalProcedureMethods = type.AllExternalProcedureMethods;
			foreach(ExternalProcedureMethod epm in allExternalProcedureMethods)
				GenExternalProcedureMethodInfo(epm, type, null);
		}

		private void GenExternalFunctionMethodsEnumerator(ExternalObjectType type)
		{
			ICollection<ExternalFunctionMethod> allExternalFunctionMethods = type.AllExternalFunctionMethods;
			sb.AppendFront("public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods");

			if(allExternalFunctionMethods.Count == 0)
				sb.Append(" { get { yield break; } }\n");
			else
			{
				sb.Append("\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("get\n");
				sb.AppendFront("{\n");
				sb.Indent();
				foreach(ExternalFunctionMethod efm in allExternalFunctionMethods)
					sb.AppendFront("yield return " + FormatExternalFunctionMethodInfoName(efm, type) + ".Instance;\n");
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		private void GenGetExternalFunctionMethod(ExternalObjectType type)
		{
			ICollection<ExternalFunctionMethod> allExternalFunctionMethods = type.AllExternalFunctionMethods;
			sb.AppendFront("public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name)");

			if(allExternalFunctionMethods.Count == 0)
				sb.Append(" { return null; }\n");
			else
			{
				sb.Append("\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("switch(name)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				foreach(ExternalFunctionMethod efm in allExternalFunctionMethods)
				{
					sb.AppendFront("case \"" + FormatIdentifiable(efm) + "\" : return " +
							FormatExternalFunctionMethodInfoName(efm, type) + ".Instance;\n");
				}
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.AppendFront("return null;\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		private void GenExternalProcedureMethodsEnumerator(ExternalObjectType type)
		{
			ICollection<ExternalProcedureMethod> allExternalProcedureMethods = type.AllExternalProcedureMethods;
			sb.AppendFront("public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods");

			if(allExternalProcedureMethods.Count == 0)
				sb.Append(" { get { yield break; } }\n");
			else
			{
				sb.Append("\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("get\n");
				sb.AppendFront("{\n");
				sb.Indent();
				foreach(ExternalProcedureMethod epm in allExternalProcedureMethods)
				{
					sb.AppendFront("yield return " + FormatExternalProcedureMethodInfoName(epm, type) + ".Instance;\n");
				}
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		private void GenGetExternalProcedureMethod(ExternalObjectType type)
		{
			ICollection<ExternalProcedureMethod> allExternalProcedureMethods = type.AllExternalProcedureMethods;
			sb.AppendFront("public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name)");

			if(allExternalProcedureMethods.Count == 0)
				sb.Append(" { return null; }\n");
			else
			{
				sb.Append("\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("switch(name)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				foreach(ExternalProcedureMethod epm in allExternalProcedureMethods)
				{
					sb.AppendFront("case \"" + FormatIdentifiable(epm) + "\" : return " +
							FormatExternalProcedureMethodInfoName(epm, type) + ".Instance;\n");
				}
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.AppendFront("return null;\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		/// <summary>
		/// Generates the function info for the given external function method
		/// </summary>
		private void GenExternalFunctionMethodInfo(ExternalFunctionMethod efm, ExternalObjectType type, string packageName)
		{
			string externalFunctionMethodName = FormatIdentifiable(efm);
			string className = FormatExternalFunctionMethodInfoName(efm, type);

			sb.AppendFront("public class " + className + " : GRGEN_LIBGR.FunctionInfo\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("private static " + className + " instance = null;\n");
			sb.AppendFront("public static " + className + " Instance { get { if(instance==null) { "
					+ "instance = new " + className + "(); } return instance; } }\n");
			sb.Append("\n");

			sb.AppendFront("private " + className + "()\n");
			sb.Indent();
			sb.AppendFront(": base(\n");
			sb.Indent();
			sb.AppendFront("\"" + externalFunctionMethodName + "\",\n");
			sb.AppendFront((!string.ReferenceEquals(packageName, null) ? "\"" + packageName + "\"" : "null") + ", ");
			sb.Append("\""
					+ (!string.ReferenceEquals(packageName, null) ? packageName + "::" + externalFunctionMethodName : externalFunctionMethodName)
					+ "\",\n");
			sb.AppendFront("true,\n");
			sb.AppendFront("new String[] { ");
			int i = 0;
	// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
	// ORIGINAL LINE: for(@SuppressWarnings("unused") de.unika.ipd.grgen.ir.type.Type inParamType : efm.getParameterTypes())
			foreach(Type inParamType in efm.ParameterTypes)
			{
				sb.Append("\"in_" + i + "\", ");
				++i;
			}
			sb.Append(" },\n");
			sb.AppendFront("new GRGEN_LIBGR.GrGenType[] { ");
			foreach(Type inParamType in efm.ParameterTypes)
			{
				if(inParamType is InheritanceType && !(inParamType is ExternalObjectType))
					sb.AppendFront(FormatTypeClassRef(inParamType) + ".typeVar, ");
				else
					sb.AppendFront("GRGEN_LIBGR.VarType.GetVarType(typeof(" + FormatAttributeType(inParamType) + ")), ");
			}
			sb.Append(" },\n");
			Type outType = efm.ReturnType;
			if(outType is InheritanceType && !(outType is ExternalObjectType))
				sb.AppendFront(FormatTypeClassRef(outType) + ".typeVar\n");
			else
				sb.AppendFront("GRGEN_LIBGR.VarType.GetVarType(typeof(" + FormatAttributeType(outType) + "))\n");
			sb.Unindent();
			sb.AppendFront(")\n");
			sb.Unindent();
			sb.AppendFront("{\n");
			sb.AppendFront("}\n");

			sb.AppendFront("public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)\n");
			sb.AppendFront("{\n");
			sb.AppendFrontIndented("throw new Exception(\"Not implemented, can't call function method without this object!\");\n");
			sb.AppendFront("}\n");

			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Append("\n");
		}

		/// <summary>
		/// Generates the procedure info for the given external procedure method
		/// </summary>
		private void GenExternalProcedureMethodInfo(ExternalProcedureMethod epm, ExternalObjectType type, string packageName)
		{
			string externalProcedureMethodName = FormatIdentifiable(epm);
			string className = FormatExternalProcedureMethodInfoName(epm, type);

			sb.AppendFront("public class " + className + " : GRGEN_LIBGR.ProcedureInfo\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("private static " + className + " instance = null;\n");
			sb.AppendFront("public static " + className + " Instance { get { if(instance==null) { "
					+ "instance = new " + className + "(); } return instance; } }\n");
			sb.Append("\n");

			sb.AppendFront("private " + className + "()\n");
			sb.Indent();
			sb.AppendFront(": base(\n");
			sb.Indent();
			sb.AppendFront("\"" + externalProcedureMethodName + "\",\n");
			sb.AppendFront((!string.ReferenceEquals(packageName, null) ? "\"" + packageName + "\"" : "null") + ", ");
			sb.Append("\""
					+ (!string.ReferenceEquals(packageName, null) ? packageName + "::" + externalProcedureMethodName : externalProcedureMethodName)
					+ "\",\n");
			sb.AppendFront("true,\n");
			sb.AppendFront("new String[] { ");
			int i = 0;
	// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
	// ORIGINAL LINE: for(@SuppressWarnings("unused") de.unika.ipd.grgen.ir.type.Type inParamType : epm.getParameterTypes())
			foreach(Type inParamType in epm.ParameterTypes)
			{
				sb.Append("\"in_" + i + "\", ");
				++i;
			}
			sb.Append(" },\n");
			sb.AppendFront("new GRGEN_LIBGR.GrGenType[] { ");
			foreach(Type inParamType in epm.ParameterTypes)
			{
				if(inParamType is InheritanceType && !(inParamType is ExternalObjectType))
					sb.Append(FormatTypeClassRef(inParamType) + ".typeVar, ");
				else
					sb.Append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + FormatAttributeType(inParamType) + ")), ");
			}
			sb.Append(" },\n");
			sb.AppendFront("new GRGEN_LIBGR.GrGenType[] { ");
			foreach(Type outType in epm.ReturnTypes)
			{
				if(outType is InheritanceType && !(outType is ExternalObjectType))
					sb.Append(FormatTypeClassRef(outType) + ".typeVar, ");
				else
					sb.Append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + FormatAttributeType(outType) + ")), ");
			}
			sb.Append(" }\n");
			sb.Unindent();
			sb.AppendFront(")\n");
			sb.Unindent();
			sb.AppendFront("{\n");
			sb.AppendFront("}\n");

			sb.AppendFront("public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)\n");
			sb.AppendFront("{\n");
			sb.AppendFrontIndented("throw new Exception(\"Not implemented, can't call procedure method without this object!\");\n");
			sb.AppendFront("}\n");

			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Append("\n");
		}

		public virtual void GenExternalObjectTypeObject()
		{
			sb.Append("\n");
			sb.AppendFront("public sealed class ExternalObjectType_object : GRGEN_LIBGR.ExternalObjectType\n");
			sb.AppendFront("{\n");
			sb.Indent();

			sb.AppendFront("public ExternalObjectType_object()\n");
			sb.AppendFrontIndented(": base(\"object\", typeof(object))\n");
			sb.AppendFront("{\n");
			sb.AppendFront("}\n");

			sb.AppendFront("public override int NumFunctionMethods { get { return 0; } }\n");
			sb.AppendFront("public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods " + "{ get { yield break; } }\n");
			sb.AppendFront("public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) " + "{ return null; }\n");
			sb.AppendFront("public override int NumProcedureMethods { get { return 0; } }\n");
			sb.AppendFront("public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods " + "{ get { yield break; } }\n");
			sb.AppendFront("public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) " + "{ return null; }\n");

			sb.Append("\n");
			sb.AppendFront("public static object ThrowCopyClassMissingException() { throw new Exception(\"Cannot copy/clone external object, copy class specification is missing in the model.\"); }\n");

			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenExternalClasses()
		{
			foreach(ExternalObjectType eot in model.ExternalObjectTypes)
			{
				sb.AppendFront("public partial class " + eot.Ident);
				bool first = true;
				foreach(InheritanceType superType in eot.DirectSuperTypes)
				{
					if(first)
						sb.Append(" : ");
					else
						sb.Append(", ");
					sb.Append(superType.Ident.ToString());
					first = false;
				}
				sb.Append("\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("// You must implement this class in the same partial class in ./"
						+ model.Ident + "ModelExternalFunctionsImpl.cs:\n");

				GenExternalMethods(eot);

				sb.Unindent();
				sb.AppendFront("}\n");
				sb.Append("\n");
			}
		}

		private void GenExternalMethods(ExternalObjectType type)
		{
			if(type.AllExternalFunctionMethods.Count == 0 && type.AllExternalProcedureMethods.Count == 0)
				return;

			sb.Append("\n");
			sb.AppendFront("// You must implement the following methods in the same partial class in ./"
					+ model.Ident + "ModelExternalFunctionsImpl.cs:\n");

			foreach(ExternalFunctionMethod efm in type.AllExternalFunctionMethods)
			{
				sb.AppendFront("//public " + FormatType(efm.ReturnType) + " ");
				sb.Append(efm.Ident.ToString() + "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph");
				foreach(Type inParamType in efm.ParameterTypes)
				{
					sb.Append(", ");
					sb.Append(FormatType(inParamType));
				}
				sb.Append(");\n");

				if(model.AreFunctionsParallel())
				{
					sb.AppendFront("//public " + FormatType(efm.ReturnType) + " ");
					sb.Append(efm.Ident.ToString() + "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph");
					foreach(Type inParamType in efm.ParameterTypes)
					{
						sb.Append(", ");
						sb.Append(FormatType(inParamType));
					}
					sb.Append(", int threadId");
					sb.Append(");\n");
				}
			}

			//////////////////////////////////////////////////////////////

			foreach(ExternalProcedureMethod epm in type.AllExternalProcedureMethods)
				GenParameterPassingReturnArray(type, epm);

			foreach(ExternalProcedureMethod epm in type.AllExternalProcedureMethods)
			{
				sb.AppendFront("//public void ");
				sb.Append(epm.Ident.ToString() 
						+ "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph, GRGEN_LIBGR.IGraphElement");
				foreach(Type inParamType in epm.ParameterTypes)
				{
					sb.Append(", ");
					sb.Append(FormatType(inParamType));
				}
				foreach(Type outType in epm.ReturnTypes)
				{
					sb.Append(", out ");
					sb.Append(FormatType(outType));
				}
				sb.Append(");\n");
			}
		}

		private void GenParameterPassingReturnArray(ExternalObjectType type, ExternalProcedureMethod epm)
		{
			sb.AppendFront("private static object[] ReturnArray_" + epm.Ident.ToString() + "_"
					+ type.Ident.ToString() + " = new object[" + epm.ReturnTypes.Count
					+ "]; // helper array for multi-value-returns, to allow for contravariant parameter assignment\n");
		}

		private void GenEmitterParserClass()
		{
			sb.AppendFront("public partial class AttributeTypeObjectEmitterParser");
			sb.Append("\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("// You must implement this class in the same partial class in ./"
					+ model.Ident + "ModelExternalFunctionsImpl.cs:\n");
			sb.AppendFront("// You must implement the functions called by the following functions inside that class (same name plus suffix Impl):\n");
			sb.Append("\n");
			if(model.IsEmitClassDefined())
			{
				sb.AppendFront("// Called during .grs import, at exactly the position in the text reader where the attribute begins.\n");
				sb.AppendFront("// For attribute type object or a user defined type, which is treated as object.\n");
				sb.AppendFront("// The implementation must parse from there on the attribute type requested.\n");
				sb.AppendFront("// It must not parse beyond the serialized representation of the attribute, \n");
				sb.AppendFront("// i.e. Peek() must return the first character not belonging to the attribute type any more.\n");
				sb.AppendFront("// Returns the parsed object.\n");
				sb.AppendFront("public static object Parse(TextReader reader, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("return ParseImpl(reader, attrType, graph);\n");
				sb.AppendFront("//reader.Read(); reader.Read(); reader.Read(); reader.Read(); // eat 'n' 'u' 'l' 'l' // default implementation\n");
				sb.AppendFront("//return null; // default implementation\n");
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.Append("\n");
				sb.AppendFront("// Called during .grs export, the implementation must return a string representation for the attribute.\n");
				sb.AppendFront("// For attribute type object or a user defined type, which is treated as object.\n");
				sb.AppendFront("// The serialized string must be parseable by Parse.\n");
				sb.AppendFront("public static string Serialize(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("return SerializeImpl(attribute, attrType, graph);\n");
				sb.AppendFront("//Console.WriteLine(\"Warning: Exporting attribute of object type to null\"); // default implementation\n");
				sb.AppendFront("//return \"null\"; // default implementation\n");
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.Append("\n");
				sb.AppendFront("// Called during debugging or emit writing, the implementation must return a string representation for the attribute.\n");
				sb.AppendFront("// For attribute type object or a user defined type, which is treated as object.\n");
				sb.AppendFront("// The attribute type may be null.\n");
				sb.AppendFront("// The string is meant for consumption by humans, it does not need to be parseable.\n");
				sb.AppendFront("public static string Emit(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("return EmitImpl(attribute, attrType, graph);\n");
				sb.AppendFront("//return \"null\"; // default implementation\n");
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.Append("\n");
				sb.AppendFront("// Called when the shell hits a line starting with \"external\".\n");
				sb.AppendFront("// The content of that line is handed in.\n");
				sb.AppendFront("// This is typically used while replaying changes containing a method call of an external type\n");
				sb.AppendFront("// -- after such a line was recorded, by the method called, by writing to the recorder.\n");
				sb.AppendFront("// This is meant to replay fine-grain changes of graph attributes of external type,\n");
				sb.AppendFront("// in contrast to full assignments handled by Parse and Serialize.\n");
				sb.AppendFront("public static void External(string line, GRGEN_LIBGR.IGraph graph)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("ExternalImpl(line, graph);\n");
				sb.AppendFront("//Console.Write(\"Ignoring: \"); // default implementation\n");
				sb.AppendFront("//Console.WriteLine(line); // default implementation\n");
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.Append("\n");
			}
			if(model.IsEmitGraphClassDefined())
			{
				sb.AppendFront("// Called during debugging on user request, the implementation must return a named graph representation for the attribute.\n");
				sb.AppendFront("// For attribute type object or a user defined type, which is treated as object.\n");
				sb.AppendFront("// The attribute type may be null. The return graph must be of the same model as the graph handed in.\n");
				sb.AppendFront("// The named graph is meant for display in the debugger, to visualize the internal structure of some attribute type.\n");
				sb.AppendFront("// This way you can graphically inspect your own data types which are opaque to GrGen with its debugger.\n");
				sb.AppendFront("public static GRGEN_LIBGR.INamedGraph AsGraph(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("return AsGraphImpl(attribute, attrType, graph);\n");
				sb.AppendFront("//return null; // default implementation\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Append("\n");
		}

		private void GenCopierComparerClass()
		{
			sb.AppendFront("public partial class AttributeTypeObjectCopierComparer\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("// You must implement the following functions in the same partial class in ./"
					+ model.Ident + "ModelExternalFunctionsImpl.cs:\n");
			sb.Append("\n");
			if(model.IsCopyClassDefined())
			{
				sb.AppendFront("// Called when a graph element or internal (transient) object bearing attributes of external object type is to be copied.\n");
				sb.AppendFront("// Also called when a top-level external object is to be cloned or copied.\n");
				sb.AppendFront("// If \"copy class\" is not specified, object attributes are copied by copying the reference, i.e. they are identical afterwards (top-level objects cannot be copied/cloned in this case).\n");
				sb.AppendFront("// If \"copy class\" is specified:\n");
				sb.AppendFront("// If the old to new element dictionary is null, objects are to be cloned, i.e. top-level object (of the very call) is to be cloned and others are just assigned by reference.\n");
				sb.AppendFront("// Otherwise, they are to be copied by-value (so changing one attribute later on has no effect on the other).\n");
				sb.AppendFront("//public static object Copy(object, IGraph, IDictionary<object, object>);\n");
				sb.Append("\n");
			}
			if(model.IsEqualClassDefined())
			{
				sb.AppendFront("// Called during comparison of graph elements from graph isomorphy comparison, or during deeply equal attribute comparisons.\n");
				sb.AppendFront("// For attribute type object.\n");
				sb.AppendFront("// If \"~~ class\" is not specified, objects are equal if their references are identical.\n");
				sb.AppendFront("// The visited objects dictionary contains the already visited objects, insert your object here to detect multiple appearances/cycles (and check against it).\n");
				sb.AppendFront("//public static bool IsEqual(object, object, IDictionary<object, object>);\n");
				sb.Append("\n");
			}
			if(model.IsLowerClassDefined())
			{
				sb.AppendFront("// Called during attribute comparison.\n");
				sb.AppendFront("// For attribute type object.\n");
				sb.AppendFront("// If \"< class\" is not specified, objects can't be compared for ordering, only for equality.\n");
				sb.AppendFront("//public static bool IsLower(object, object, IDictionary<object, object>);\n");
				sb.Append("\n");
			}
			if(model.ExternalObjectTypes.Count > 0)
			{
				sb.Append("\n");
				sb.AppendFront("// The same functions, just for each user defined type.\n");
				sb.AppendFront("// Those are normally treated as object (if no \"copy class or ~~ class or < class\" is specified),\n");
				sb.AppendFront("// i.e. equal if identical references, no ordered comparisons available, and copy just copies the reference (making them identical).\n");
				sb.AppendFront("// Here you can overwrite the default reference semantics with value semantics, fitting better to the other attribute types.\n");
				foreach(ExternalObjectType et in model.ExternalObjectTypes)
				{
					string typeName = et.Ident.ToString();
					sb.Append("\n");
					if(model.IsCopyClassDefined())
						sb.AppendFront("//public static " + typeName + " Copy(" + typeName + ");\n");
					if(model.IsEqualClassDefined())
						sb.AppendFront("//public static bool IsEqual(" + typeName + ", " + typeName + ", IDictionary<object, object>);\n");
					if(model.IsLowerClassDefined())
						sb.AppendFront("//public static bool IsLower(" + typeName + ", " + typeName + ", IDictionary<object, object>);\n");
				}
			}
			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Append("\n");
		}

		private void GenExternalFunctionHeaders()
		{
			foreach(ExternalFunction ef in model.ExternalFunctions)
			{
				Type returnType = ef.ReturnType;
				sb.AppendFront("//public static " + FormatType(returnType) + " " + ef.Name
						+ "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph");
				foreach(Type paramType in ef.ParameterTypes)
				{
					sb.Append(", ");
					sb.Append(FormatType(paramType));
				}
				sb.Append(");\n");

				if(model.AreFunctionsParallel())
				{
					sb.AppendFront("//public static " + FormatType(returnType) + " " + ef.Name
							+ "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph");
					foreach(Type paramType in ef.ParameterTypes)
					{
						sb.Append(", ");
						sb.Append(FormatType(paramType));
					}
					sb.Append(", int threadId");
					sb.Append(");\n");
				}
			}
		}

		private void GenExternalProcedureHeaders()
		{
			foreach(ExternalProcedure ep in model.ExternalProcedures)
			{
				sb.AppendFront("//public static void " + ep.Name
						+ "(GRGEN_LIBGR.IActionExecutionEnvironment, GRGEN_LIBGR.IGraph");
				foreach(Type paramType in ep.ParameterTypes)
				{
					sb.Append(", ");
					sb.Append(FormatType(paramType));
				}
				foreach(Type retType in ep.ReturnTypes)
				{
					sb.Append(", ");
					sb.Append("out ");
					sb.Append(FormatType(retType));
				}
				sb.Append(");\n");
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

		///////////////////////
		// Private variables //
		///////////////////////

		private Model model;
		private SourceBuilder sb = null;
	}

}
