/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Generates the model files for the SearchPlanBackend2 backend.
/// @author Moritz Kroll, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.be.Csharp
{

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Diagnostics;

	using de.unika.ipd.grgen.ir;
	using Needs = de.unika.ipd.grgen.ir.NeededEntities.Needs;
	using FunctionMethod = de.unika.ipd.grgen.ir.executable.FunctionMethod;
	using ProcedureMethod = de.unika.ipd.grgen.ir.executable.ProcedureMethod;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using TypeClass = de.unika.ipd.grgen.ir.type.Type.TypeClass;
	using BooleanType = de.unika.ipd.grgen.ir.type.basic.BooleanType;
	using ByteType = de.unika.ipd.grgen.ir.type.basic.ByteType;
	using DoubleType = de.unika.ipd.grgen.ir.type.basic.DoubleType;
	using FloatType = de.unika.ipd.grgen.ir.type.basic.FloatType;
	using GraphType = de.unika.ipd.grgen.ir.type.basic.GraphType;
	using IntType = de.unika.ipd.grgen.ir.type.basic.IntType;
	using LongType = de.unika.ipd.grgen.ir.type.basic.LongType;
	using ObjectType = de.unika.ipd.grgen.ir.type.basic.ObjectType;
	using ShortType = de.unika.ipd.grgen.ir.type.basic.ShortType;
	using StringType = de.unika.ipd.grgen.ir.type.basic.StringType;
	using VoidType = de.unika.ipd.grgen.ir.type.basic.VoidType;
	using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;
	using ContainerType = de.unika.ipd.grgen.ir.type.container.ContainerType;
	using DequeType = de.unika.ipd.grgen.ir.type.container.DequeType;
	using MapType = de.unika.ipd.grgen.ir.type.container.MapType;
	using SetType = de.unika.ipd.grgen.ir.type.container.SetType;
	using SourceBuilder = de.unika.ipd.grgen.util.SourceBuilder;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ExpressionPair = de.unika.ipd.grgen.ir.expr.ExpressionPair;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using ArrayInit = de.unika.ipd.grgen.ir.expr.array.ArrayInit;
	using DequeInit = de.unika.ipd.grgen.ir.expr.deque.DequeInit;
	using MapInit = de.unika.ipd.grgen.ir.expr.map.MapInit;
	using SetInit = de.unika.ipd.grgen.ir.expr.set.SetInit;
	using AttributeIndex = de.unika.ipd.grgen.ir.model.AttributeIndex;
	using ConnAssert = de.unika.ipd.grgen.ir.model.ConnAssert;
	using EnumItem = de.unika.ipd.grgen.ir.model.EnumItem;
	using IncidenceCountIndex = de.unika.ipd.grgen.ir.model.IncidenceCountIndex;
	using Index = de.unika.ipd.grgen.ir.model.Index;
	using MemberInit = de.unika.ipd.grgen.ir.model.MemberInit;
	using Model = de.unika.ipd.grgen.ir.model.Model;
	using NodeEdgeEnumBearer = de.unika.ipd.grgen.ir.model.NodeEdgeEnumBearer;
	using BaseInternalObjectType = de.unika.ipd.grgen.ir.model.type.BaseInternalObjectType;
	using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
	using EnumType = de.unika.ipd.grgen.ir.model.type.EnumType;
	using ExternalObjectType = de.unika.ipd.grgen.ir.model.type.ExternalObjectType;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;
	using InternalObjectType = de.unika.ipd.grgen.ir.model.type.InternalObjectType;
	using InternalTransientObjectType = de.unika.ipd.grgen.ir.model.type.InternalTransientObjectType;
	using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;
	using PackageType = de.unika.ipd.grgen.ir.model.type.PackageType;

	public class ModelGen : CSharpBase
	{
		internal enum InheritanceTypeType
		{
			Node,
			Edge,
			Object,
			TransientObject
		}

		private readonly int MAX_OPERATIONS_FOR_ATTRIBUTE_INITIALIZATION_INLINING = 20;
		private const string ATTR_IMPL_SUFFIX = "_M0no_suXx_h4rD";

		public ModelGen(SearchPlanBackend2 backend, string nodeTypePrefix, string edgeTypePrefix, string objectTypePrefix, string transientObjectTypePrefix)
			: base(nodeTypePrefix, edgeTypePrefix, objectTypePrefix, transientObjectTypePrefix)
		{
			be = backend;
			rootTypes = new HashSet<string>();
			rootTypes.Add("Node");
			rootTypes.Add("Edge");
			rootTypes.Add("AEdge");
			rootTypes.Add("UEdge");
			rootTypes.Add("Object");
			rootTypes.Add("TransientObject");
		}

		/// <summary>
		/// Generates the model sourcecode for the current unit.
		/// </summary>
		public virtual void GenModel(Model model)
		{
			this.model = model;
			sb = new SourceBuilder();
			stubsb = null;

			string filename = model.Ident + "Model.cs";

			Console.WriteLine("  generating the " + filename + " file...");

			sb.AppendFront("// This file has been generated automatically by GrGen (www.grgen.net)\n"
					+ "// Do not modify this file! Any changes will be lost!\n"
					+ "// Generated from \"" + be.unit.Filename + "\" on " + DateTime.Now + "\n"
					+ "\n"
					+ "using System;\n"
					+ "using System.Collections.Generic;\n"
					+ "using System.Collections;\n"
					+ "using System.IO;\n"
					+ "using System.Diagnostics;\n"
					+ "using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n"
					+ "using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n"
					+ "using GRGEN_EXPR = de.unika.ipd.grGen.expression;\n"
					+ "using GRGEN_MODEL = de.unika.ipd.grGen.Model_" + model.Ident + ";\n"
					+ "\n"
					+ "namespace de.unika.ipd.grGen.Model_" + model.Ident + "\n"
					+ "{\n");
			sb.Indent();

			foreach(PackageType pt in model.Packages)
			{
				Console.WriteLine("    generating package " + pt.Ident + "...");

				sb.Append("\n");
				sb.AppendFront("//-----------------------------------------------------------\n");
				sb.AppendFront("namespace ");
				sb.Append(FormatIdentifiable(pt));
				sb.Append("\n");
				sb.AppendFront("//-----------------------------------------------------------\n");
				sb.AppendFront("{\n");
				sb.Indent();

				GenBearer(model.AllNodeTypes, model.AllEdgeTypes,
						model.AllObjectTypes, model.AllTransientObjectTypes,
						pt, pt.Ident.ToString());

				sb.Append("\n");
				sb.AppendFront("//-----------------------------------------------------------\n");
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.AppendFront("//-----------------------------------------------------------\n");
			}

			GenBearer(model.AllNodeTypes, model.AllEdgeTypes,
					model.AllObjectTypes, model.AllTransientObjectTypes,
					model, null);

			ModelExternalGen modelExternalGen = new ModelExternalGen(model, sb, nodeTypePrefix, edgeTypePrefix, objectTypePrefix, transientObjectTypePrefix);
			modelExternalGen.GenExternalObjectTypeObject();
			foreach(ExternalObjectType et in model.ExternalObjectTypes)
				modelExternalGen.GenExternalObjectType(et);

			Console.WriteLine("    generating indices...");

			sb.Append("\n");
			sb.AppendFront("//\n");
			sb.AppendFront("// Indices\n");
			sb.AppendFront("//\n");
			sb.Append("\n");

			ModelIndexGen indexGen = new ModelIndexGen(model, sb, nodeTypePrefix, edgeTypePrefix, objectTypePrefix, transientObjectTypePrefix);
			indexGen.GenIndexTypes();
			indexGen.GenIndexImplementations();
			indexGen.GenIndexSetType();

			Console.WriteLine("    generating node model...");
			sb.Append("\n");
			GenModelClass(model.AllNodeTypes, InheritanceTypeType.Node);

			Console.WriteLine("    generating edge model...");
			sb.Append("\n");
			GenModelClass(model.AllEdgeTypes, InheritanceTypeType.Edge);

			Console.WriteLine("    generating class model...");
			sb.Append("\n");
			GenModelClass(model.AllObjectTypes, InheritanceTypeType.Object);

			Console.WriteLine("    generating transient class model...");
			sb.Append("\n");
			GenModelClass(model.AllTransientObjectTypes, InheritanceTypeType.TransientObject);

			Console.WriteLine("    generating graph model...");
			sb.Append("\n");
			GenGraphModel();
			sb.Append("\n");
			GenGraphClass();
			sb.Append("\n");
			GenNamedGraphClass();

			sb.Unindent();
			sb.AppendFront("}\n");

			Console.WriteLine("    writing to " + be.path + " / " + filename);
			WriteFile(be.path, filename, sb.ToString());

			if(stubsb != null)
			{
				string stubFilename = model.Ident + "ModelStub.cs";
				Console.WriteLine("  writing the " + stubFilename + " stub file...");
				WriteFile(be.path, stubFilename, stubsb.ToString());
			}

			///////////////////////////////////////////////////////////////////////////////////////////
			// generate the external functions and types stub file
			// only if there are external functions or external procedures or external types required 
			// or the emit class is to be generated or the copy class is to be generated
			if(model.ExternalObjectTypes.Count == 0
					&& model.ExternalFunctions.Count == 0
					&& model.ExternalProcedures.Count == 0
					&& !model.IsEmitClassDefined()
					&& !model.IsEmitGraphClassDefined()
					&& !model.IsCopyClassDefined()
					&& !model.IsEqualClassDefined()
					&& !model.IsLowerClassDefined())
				return;

			filename = model.Ident + "ModelExternalFunctions.cs";

			Console.WriteLine("  generating the " + filename + " file...");

			sb = new SourceBuilder();

			modelExternalGen = new ModelExternalGen(model, sb, nodeTypePrefix, edgeTypePrefix, objectTypePrefix, transientObjectTypePrefix);

			modelExternalGen.GenExternalFunctionsFile(be.unit.Filename);

			Console.WriteLine("    writing to " + be.path + " / " + filename);
			WriteFile(be.path, filename, sb.ToString());

			if(be.path.CompareTo(new File(".")) == 0)
				Console.WriteLine("    no copy needed for " + be.path + " / " + filename);
			else
			{
				Console.WriteLine("    copying " + be.path + " / " + filename + " to "
						+ be.path.GetAbsoluteFile().GetParent() + " / " + filename);
				CopyFile(new File(be.path, filename), new File(be.path.GetAbsoluteFile().GetParent(), filename));
			}
		}

		private SourceBuilder StubBuffer
		{
			get
			{
				if(stubsb == null)
				{
					stubsb = new SourceBuilder();
					stubsb.AppendFront("// This file has been generated automatically by GrGen (www.grgen.net)\n"
							+ "// Do not modify this file! Any changes will be lost!\n"
							+ "// Rename this file or use a copy!\n"
							+ "// Generated from \"" + be.unit.Filename + "\" on " + DateTime.Now + "\n"
							+ "\n"
							+ "using System;\n"
							+ "using System.Collections.Generic;\n"
							+ "using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;\n"
							+ "using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;\n"
							+ "using GRGEN_MODEL = de.unika.ipd.grGen.Model_" + model.Ident + ";\n");
				}
				return stubsb;
			}
		}

		private void GenBearer<T1, T2, T3, T4>(ICollection<T1> allNodeTypes,
				ICollection<T2> allEdgeTypes,
				ICollection<T3> allObjectTypes,
				ICollection<T4> allTransientObjectTypes,
				NodeEdgeEnumBearer bearer, string packageName) where T1 : de.unika.ipd.grgen.ir.model.type.InheritanceType where T2 : de.unika.ipd.grgen.ir.model.type.InheritanceType where T3 : de.unika.ipd.grgen.ir.model.type.InheritanceType where T4 : de.unika.ipd.grgen.ir.model.type.InheritanceType
		{

			Console.WriteLine("    generating enums...");
			sb.Append("\n");
			GenEnums(bearer);

			Console.WriteLine("    generating node types...");
			sb.Append("\n");
			GenInheritanceTypes(allNodeTypes, bearer, packageName, InheritanceTypeType.Node);

			Console.WriteLine("    generating edge types...");
			sb.Append("\n");
			GenInheritanceTypes(allEdgeTypes, bearer, packageName, InheritanceTypeType.Edge);

			Console.WriteLine("    generating object types...");
			sb.Append("\n");
			GenInheritanceTypes(allObjectTypes, bearer, packageName, InheritanceTypeType.Object);

			Console.WriteLine("    generating transient object types...");
			sb.Append("\n");
			GenInheritanceTypes(allTransientObjectTypes, bearer, packageName, InheritanceTypeType.TransientObject);
		}

		private void GenEnums(NodeEdgeEnumBearer bearer)
		{
			sb.AppendFront("//\n");
			sb.AppendFront("// Enums\n");
			sb.AppendFront("//\n");
			sb.Append("\n");

			foreach(EnumType enumt in bearer.EnumTypes)
			{
				sb.AppendFront("public enum ENUM_" + FormatIdentifiable(enumt) + " { ");
				foreach(EnumItem enumi in enumt.Items)
					sb.Append("@" + FormatIdentifiable(enumi) + " = " + enumi.Value.Value + ", ");
				sb.Append("};\n\n");
			}

			sb.AppendFront("public class Enums\n");
			sb.AppendFront("{\n");
			sb.Indent();
			foreach(EnumType enumt in bearer.EnumTypes)
			{
				sb.AppendFront("public static GRGEN_LIBGR.EnumAttributeType @" + FormatIdentifiable(enumt)
						+ " = new GRGEN_LIBGR.EnumAttributeType(\"" + FormatIdentifiable(enumt) + "\", "
						+ (!GetPackagePrefix(enumt).Equals("") ? "\"" + GetPackagePrefix(enumt) + "\"" : "null") + ", "
						+ "\"" + GetPackagePrefixDoubleColon(enumt) + FormatIdentifiable(enumt) + "\", "
						+ "typeof(GRGEN_MODEL." + GetPackagePrefixDot(enumt) + "ENUM_" + FormatIdentifiable(enumt) + "), "
						+ "new GRGEN_LIBGR.EnumMember[] {\n");
				sb.Indent();
				foreach(EnumItem enumi in enumt.Items)
				{
					sb.AppendFront("new GRGEN_LIBGR.EnumMember(" + enumi.Value.Value
							+ ", \"" + FormatIdentifiable(enumi) + "\"),\n");
				}
				sb.Unindent();
				sb.AppendFront("});\n");
			}
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		/// <summary>
		/// Generates code for all given inheritance types.
		/// </summary>
		private void GenInheritanceTypes<T1>(ICollection<T1> allTypes,
				NodeEdgeEnumBearer bearer, string packageName, InheritanceTypeType inhType) where T1 : de.unika.ipd.grgen.ir.model.type.InheritanceType
		{
			ICollection<InheritanceType> curTypes;
			if(inhType == InheritanceTypeType.Node)
			{
				curTypes = GetInheritanceTypes(bearer.NodeTypes);

				sb.AppendFront("//\n");
				sb.AppendFront("// Node types\n");
				sb.AppendFront("//\n");
				sb.Append("\n");

				sb.AppendFront("public enum NodeTypes ");
			}
			else if(inhType == InheritanceTypeType.Edge)
			{
				curTypes = GetInheritanceTypes(bearer.EdgeTypes);

				sb.AppendFront("//\n");
				sb.AppendFront("// Edge types\n");
				sb.AppendFront("//\n");
				sb.Append("\n");

				sb.AppendFront("public enum EdgeTypes ");
			}
			else if(inhType == InheritanceTypeType.Object)
			{
				curTypes = GetInheritanceTypes(bearer.ObjectTypes);

				sb.AppendFront("//\n");
				sb.AppendFront("// Object types\n");
				sb.AppendFront("//\n");
				sb.Append("\n");

				sb.AppendFront("public enum ObjectTypes ");
			}
			else
			{
				curTypes = GetInheritanceTypes(bearer.TransientObjectTypes);

				sb.AppendFront("//\n");
				sb.AppendFront("// Transient object types\n");
				sb.AppendFront("//\n");
				sb.Append("\n");

				sb.AppendFront("public enum TransientObjectTypes ");
			}

			sb.Append("{ ");
			bool first = true;
			foreach(InheritanceType id in curTypes)
			{
				if(first)
					first = false;
				else
					sb.Append(", ");
				sb.Append("@" + FormatIdentifiable(id) + "=" + id.InheritanceTypeID);
			}
			sb.Append(" }");

			sb.Append(";\n");

			foreach(InheritanceType type in curTypes)
				GenInheritanceType(allTypes, type, packageName, inhType);
		}

		/// <summary>
		/// Generates all code for a given inheritance type.
		/// </summary>
		private void GenInheritanceType<T1>(ICollection<T1> allTypes, InheritanceType type, string packageName, InheritanceTypeType inhType) where T1 : de.unika.ipd.grgen.ir.model.type.InheritanceType
		{
			sb.Append("\n");
			sb.AppendFront("// *** " + FormatInheritanceTypeValue(type) + " " + FormatIdentifiable(type) + " ***\n");
			sb.Append("\n");

			if(!rootTypes.Contains(type.Ident.ToString()))
				GenInheritanceTypeInterface(type);
			if(!type.IsAbstract())
			{
				if(inhType == InheritanceTypeType.Node || inhType == InheritanceTypeType.Edge)
					GenGraphElementImplementation(type);
				else
					GenObjectImplementation(type);
			}
			GenTypeImplementation(allTypes, type, packageName);
			GenAttributeArrayHelpersAndComparers(type);
		}

		//////////////////////////////////
		// Element interface generation //
		//////////////////////////////////

		/// <summary>
		/// Generates the inheritance type interface for the given type
		/// </summary>
		private void GenInheritanceTypeInterface(InheritanceType type)
		{
			string iname = "I" + getInheritanceTypePrefix(type) + FormatIdentifiable(type);
			sb.AppendFront("public interface " + iname + " : ");
			GenDirectSuperTypeList(type);
			sb.Append("\n");
			sb.AppendFront("{\n");
			sb.Indent();
			foreach(Entity e in type.Members)
				GenAttributeAccess(type, e, "");
			GenMethodInterfaces(type, type.FunctionMethods, type.ProcedureMethods, "");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		/// <summary>
		/// Generate a list of direct supertypes of the given type.
		/// </summary>
		private void GenDirectSuperTypeList(InheritanceType type)
		{
			string iprefix = "I" + getInheritanceTypePrefix(type);

			bool first = true;
			foreach(InheritanceType superType in type.DirectSuperTypes)
			{
				if(first)
					first = false;
				else
					sb.Append(", ");
				if(rootTypes.Contains(superType.Ident.ToString()))
					sb.Append(GetRootElementInterfaceRef(superType));
				else
					sb.Append(GetPackagePrefixDot(superType) + iprefix + FormatIdentifiable(superType));
			}
		}

		/// <summary>
		/// Generate the attribute accessor declarations of the given member. </summary>
		/// <param name="type"> The type for which the accessors are to be generated. </param>
		/// <param name="member"> The member entity. </param>
		/// <param name="modifiers"> A string which may contain modifiers to be applied to the accessor.
		/// 		It must either end with a space or be empty. </param>
		private void GenAttributeAccess(InheritanceType type, Entity member, string modifiers)
		{
			sb.AppendFront(modifiers);
			if(type.GetOverriddenMember(member) != null)
				sb.Append("new ");
			if(member.IsConst())
				sb.Append(FormatAttributeType(member) + " @" + FormatIdentifiable(member) + " { get; }\n");
			else
				sb.Append(FormatAttributeType(member) + " @" + FormatIdentifiable(member) + " { get; set; }\n");
		}

		private void GenMethodInterfaces(InheritanceType type, ICollection<FunctionMethod> functionMethods,
				ICollection<ProcedureMethod> procedureMethods, string modifiers)
		{
			foreach(FunctionMethod fm in functionMethods)
			{
				if(type.SuperTypeDefinesFunctionMethod(fm))
					continue; // skip methods which were already declared in a base interface

				sb.AppendFront(FormatType(fm.ReturnType) + " ");
				sb.Append(fm.Ident.ToString()
						+ "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph");
				foreach(Entity inParam in fm.Parameters)
				{
					sb.Append(", ");
					sb.Append(FormatType(inParam.Type));
					sb.Append(" ");
					sb.Append(FormatEntity(inParam));
				}
				sb.Append(");\n");

				if(model.AreFunctionsParallel())
				{
					sb.AppendFront(FormatType(fm.ReturnType) + " ");
					sb.Append(fm.Ident.ToString()
							+ "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph");
					foreach(Entity inParam in fm.Parameters)
					{
						sb.Append(", ");
						sb.Append(FormatType(inParam.Type));
						sb.Append(" ");
						sb.Append(FormatEntity(inParam));
					}
					sb.Append(", int threadId");
					sb.Append(");\n");
				}
			}
			foreach(ProcedureMethod pm in procedureMethods)
			{
				if(type.SuperTypeDefinesProcedureMethod(pm))
					continue; // skip methods which were already declared in a base interface

				sb.AppendFront("void ");
				sb.Append(pm.Ident.ToString()
						+ "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph");
				foreach(Entity inParam in pm.Parameters)
				{
					sb.Append(", ");
					sb.Append(FormatType(inParam.Type));
					sb.Append(" ");
					sb.Append(FormatEntity(inParam));
				}
				int i = 0;
				foreach(Type outType in pm.ReturnTypes)
				{
					sb.Append(", out ");
					sb.Append(FormatType(outType));
					sb.Append(" ");
					sb.Append("_out_param_" + i);
					++i;
				}
				sb.Append(");\n");
			}
		}

		///////////////////////////////////////
		// Element implementation generation //
		///////////////////////////////////////

		/// <summary>
		/// Generates the element implementation for the given type
		/// </summary>
		private void GenGraphElementImplementation(InheritanceType type)
		{
			bool isNode = type is NodeType;
			string kindStr = isNode ? "Node" : "Edge";
			string baseClass = "GRGEN_LGSP.LGSP" + kindStr;
			if(model.IsUniqueResulting())
				baseClass += "WithUniqueId";
			if(model.IsGraphofDefined())
				baseClass += "WithReferenceToContainingGraph";
			string elemname = FormatInheritanceClassName(type);
			string elemref = FormatInheritanceClassRef(type);
			string extName = type.ExternalName;
			string allocName = !string.ReferenceEquals(extName, null) ? "global::" + extName : elemref;
			string typeref = FormatTypeClassRef(type);
			string ielemref = FormatElementInterfaceRef(type);
			string @namespace = null;
			SourceBuilder routedSB = sb;
			string routedClassName = elemname;
			string routedDeclName = elemref;

			if(string.ReferenceEquals(extName, null))
			{
				sb.Append("\n");
				sb.AppendFront("public sealed partial class " + elemname + " : "
						+ baseClass + ", " + ielemref + "\n");
				sb.AppendFront("{\n");
				sb.Indent();
			}
			else
			{ // what's that? = for "Embedding the graph rewrite system GrGen.NET into C#" (see corresponding master thesis, mono c# compiler extension)
				routedSB = StubBuffer;
				int lastDot = extName.LastIndexOf('.');
				string extClassName;
				if(lastDot != -1)
				{
					@namespace = extName.Substring(0, lastDot);
					extClassName = extName.Substring(lastDot + 1);
					stubsb.Append("\n");
					stubsb.AppendFront("namespace " + @namespace + "\n");
					stubsb.AppendFront("{\n");
					stubsb.Indent();
				}
				else
					extClassName = extName;
				routedClassName = extClassName;
				routedDeclName = extClassName;

				stubsb.AppendFront("public class " + extClassName + " : " + elemref + "\n");
				stubsb.AppendFront("{\n");
				stubsb.Indent();
				stubsb.AppendFront("public " + extClassName + "() : base() { }\n");
				stubsb.Append("\n");

				sb.Append("\n");
				sb.AppendFront("public abstract class " + elemname + " : "
						+ baseClass + ", " + ielemref + "\n");
				sb.AppendFront("{\n");
				sb.Indent();
			}
			sb.AppendFront("[ThreadStatic] private static int poolLevel;\n");
			sb.AppendFront("[ThreadStatic] private static " + elemref + "[] pool;\n");

			// Static initialization for constants = static members
			InitAllMembersConst(type, elemname, "this");
			sb.Append("\n");

			GenElementConstructor(type, elemname, typeref);
			sb.Append("\n");

			GenElementStaticTypeGetter(typeref);
			sb.Append("\n");

			GenElementCloneMethod(type, routedSB, routedDeclName);
			routedSB.Append("\n");
			GenElementCopyMethod(type, routedSB, routedDeclName);
			routedSB.Append("\n");
			GenElementCopyConstructor(type, extName, typeref, routedSB, routedClassName, routedDeclName);
			routedSB.Append("\n");

			GenElementAttributeComparisonMethod(type, routedSB, routedClassName);
			routedSB.Append("\n");

			GenElementCreateMethods(type, isNode, elemref, allocName);
			sb.Append("\n");

			GenElementRecycleMethod(elemref);
			sb.Append("\n");

			GenAttributesAndAttributeAccessImpl(type);

			GenMethods(type);

			sb.Unindent();
			sb.AppendFront("}\n");

			if(!string.ReferenceEquals(extName, null))
			{
				stubsb.Unindent();
				stubsb.AppendFront("}\n"); // close class stub
				if(!string.ReferenceEquals(@namespace, null))
				{
					stubsb.Unindent();
					stubsb.AppendFront("}\n"); // close namespace
				}
			}
		}

		/// <summary>
		/// Generates the element implementation for the given type
		/// </summary>
		private void GenObjectImplementation(InheritanceType type)
		{
			string kindStr = type is InternalTransientObjectType ? "TransientObject" : "Object";
			string elemname = FormatInheritanceClassName(type);
			string elemref = FormatInheritanceClassRef(type);
			Debug.Assert((string.ReferenceEquals(type.ExternalName, null)));
			string typeref = FormatTypeClassRef(type);
			string ielemref = FormatElementInterfaceRef(type);
			SourceBuilder routedSB = sb;
			string routedClassName = elemname;
			string routedDeclName = elemref;

			sb.Append("\n");
			sb.AppendFront("public sealed partial class " + elemname + " : GRGEN_LGSP.LGSP"
					+ kindStr + ", " + ielemref + "\n");
			sb.AppendFront("{\n");
			sb.Indent();

			// Static initialization for constants = static members
			InitAllMembersConst(type, elemname, "this");
			sb.Append("\n");

			GenElementConstructor(type, elemname, typeref);
			sb.Append("\n");

			GenElementStaticTypeGetter(typeref);
			sb.Append("\n");

			GenElementCloneMethod(type, routedSB, routedDeclName);
			routedSB.Append("\n");
			GenElementCopyMethod(type, routedSB, routedDeclName);
			routedSB.Append("\n");
			GenElementCopyConstructor(type, null, typeref, routedSB, routedClassName, routedDeclName);
			routedSB.Append("\n");

			GenElementAttributeComparisonMethod(type, routedSB, routedClassName);
			routedSB.Append("\n");

			GenAttributesAndAttributeAccessImpl(type);

			GenMethods(type);

			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenElementConstructor(InheritanceType type, string elemname, string typeref)
		{
			if(type is NodeType)
			{
				sb.AppendFront("public " + elemname + "() : base(" + typeref + ".typeVar)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				InitAllMembersNonConst(type, "this", false, false);
				sb.Unindent();
				sb.AppendFront("}\n");
			}
			else if(type is EdgeType)
			{
				sb.AppendFront("public " + elemname + "(GRGEN_LGSP.LGSPNode source, "
						+ "GRGEN_LGSP.LGSPNode target)\n");
				sb.AppendFrontIndented(": base(" + typeref + ".typeVar, source, target)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				InitAllMembersNonConst(type, "this", false, false);
				sb.Unindent();
				sb.AppendFront("}\n");
			}
			else if(type is InternalObjectType)
			{
				sb.AppendFront("//create object by CreateObject of the type class, not this internal-use constructor\n");
				sb.AppendFront("public " + elemname + "(long uniqueId) : base(" + typeref + ".typeVar, uniqueId)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				InitAllMembersNonConst(type, "this", false, false);
				sb.Unindent();
				sb.AppendFront("}\n");
			}
			else
			{
				sb.AppendFront("//create object by CreateTransientObject of the type class, not this internal-use constructor\n");
				sb.AppendFront("public " + elemname + "() : base(" + typeref + ".typeVar)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				InitAllMembersNonConst(type, "this", false, false);
				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		private void GenElementStaticTypeGetter(string typeref)
		{
			sb.AppendFront("public static " + typeref + " TypeInstance { get { return " + typeref + ".typeVar; } }\n");
		}

		private static void GenElementCloneMethod(InheritanceType type, SourceBuilder routedSB, string routedDeclName)
		{
			if(type is NodeType)
			{
				routedSB.AppendFront("public override GRGEN_LIBGR.INode Clone() {\n");
				routedSB.AppendFrontIndented("return new " + routedDeclName + "(this, null, null);\n");
				routedSB.AppendFront("}\n");
			}
			else if(type is EdgeType)
			{
				routedSB.AppendFront("public override GRGEN_LIBGR.IEdge Clone("
						+ "GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget) {\n");
				routedSB.AppendFrontIndented("return new " + routedDeclName + "(this,"
						+ "(GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget, null, null);\n");
				routedSB.AppendFront("}\n");
			}
			else if(type is InternalTransientObjectType)
			{
				routedSB.AppendFront("public override GRGEN_LIBGR.ITransientObject Clone() {\n");
				routedSB.AppendFrontIndented("return new " + routedDeclName + "(this, null, null);\n");
				routedSB.AppendFront("}\n");
			}
			else
			{
				routedSB.AppendFront("public override GRGEN_LIBGR.IObject Clone(GRGEN_LIBGR.IGraph graph) {\n");
				routedSB.Indent();
				routedSB.AppendFront(routedDeclName + " newObject = new " + routedDeclName + "(this, graph, null);\n");
				routedSB.AppendFront("((GRGEN_LIBGR.BaseGraph)graph).ObjectCreated(newObject);\n");
				routedSB.AppendFront("return newObject;\n");
				routedSB.Unindent();
				routedSB.AppendFront("}\n");
			}
		}

		private static void GenElementCopyMethod(InheritanceType type, SourceBuilder routedSB, string routedDeclName)
		{
			if(type is NodeType)
			{
				routedSB.AppendFront("public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {\n");
				routedSB.AppendFrontIndented("return new " + routedDeclName + "(this, graph, oldToNewObjectMap);\n");
				routedSB.AppendFront("}\n");
			}
			else if(type is EdgeType)
			{
				routedSB.AppendFront("public override GRGEN_LIBGR.IEdge Copy("
						+ "GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {\n");
				routedSB.AppendFrontIndented("return new " + routedDeclName + "(this, "
						+ "(GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget, graph, oldToNewObjectMap);\n");
				routedSB.AppendFront("}\n");
			}
			else if(type is InternalTransientObjectType)
			{
				routedSB.AppendFront("public override GRGEN_LIBGR.ITransientObject Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {\n");
				routedSB.AppendFrontIndented("return new " + routedDeclName + "(this, graph, oldToNewObjectMap);\n");
				routedSB.AppendFront("}\n");
			}
			else
			{
				routedSB.AppendFront("public override GRGEN_LIBGR.IObject Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {\n");
				routedSB.Indent();
				routedSB.AppendFront(routedDeclName + " newObject = new " + routedDeclName + "(this, graph, oldToNewObjectMap);\n");
				routedSB.AppendFront("((GRGEN_LIBGR.BaseGraph)graph).ObjectCreated(newObject);\n");
				routedSB.AppendFront("return newObject;\n");
				routedSB.Unindent();
				routedSB.AppendFront("}\n");
			}
		}

		private void GenContainerCopying(SourceBuilder sb, Entity member, string attrName)
		{
			if(member.Type is ArrayType
					|| member.Type is DequeType)
			{
				string valueType = member.Type is ArrayType ?
						FormatAttributeType(((ArrayType)member.Type).ValueType) :
						FormatAttributeType(((DequeType)member.Type).ValueType);
				sb.AppendFront("for(int i = 0; i < oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ".Count; ++i)\n");
				sb.AppendFront("{\n");
				sb.AppendFrontIndented(attrName + ModelGen.ATTR_IMPL_SUFFIX + ".Add((" + valueType + ")Copy(oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + "[i], graph, oldToNewObjectMap));\n");
				sb.AppendFront("}\n");
			}
			else if(member.Type is SetType)
			{
				SetType setType = (SetType)member.Type;
				string valueType = FormatAttributeType(setType.ValueType);
				sb.AppendFront("foreach(KeyValuePair<" + valueType + ", GRGEN_LIBGR.SetValueType> kvp in oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ")\n");
				sb.AppendFront("{\n");
				string key = "(" + valueType + ")Copy(kvp.Key, graph, oldToNewObjectMap)";
				sb.AppendFrontIndented(attrName + ModelGen.ATTR_IMPL_SUFFIX + "[" + key + "] = null;\n");
				sb.AppendFront("}\n");
			}
			else if(member.Type is MapType)
			{
				MapType mapType = (MapType)member.Type;
				string keyType = FormatAttributeType(mapType.KeyType);
				string valueType = FormatAttributeType(mapType.ValueType);
				sb.AppendFront("foreach(KeyValuePair<" + keyType + "," + valueType + "> kvp in oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ")\n");
				sb.AppendFront("{\n");
				string key = "kvp.Key";
				if(mapType.KeyType is BaseInternalObjectType)
					key = "(" + keyType + ")Copy(kvp.Key, graph, oldToNewObjectMap)";
				string value = "kvp.Value";
				if(mapType.ValueType is BaseInternalObjectType)
					value = "(" + valueType + ")Copy(kvp.Value, graph, oldToNewObjectMap)";
				sb.AppendFrontIndented(attrName + ModelGen.ATTR_IMPL_SUFFIX + "[" + key + "] = " + value + ";\n");
				sb.AppendFront("}\n");
			}
		}

		private void GenElementCopyConstructor(InheritanceType type, string extName, string typeref,
				SourceBuilder routedSB, string routedClassName, string routedDeclName)
		{
			if(type is NodeType)
			{
				routedSB.AppendFront("private " + routedClassName + "(" + routedDeclName + " oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base("
						+ (string.ReferenceEquals(extName, null) ? typeref + ".typeVar" : "") + ")\n");
			}
			else if(type is EdgeType)
			{
				routedSB.AppendFront("private " + routedClassName + "(" + routedDeclName
						+ " oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)\n");
				routedSB.AppendFrontIndented(": base("
						+ (string.ReferenceEquals(extName, null) ? typeref + ".typeVar, " : "") + "newSource, newTarget)\n");
			}
			else if(type is InternalObjectType)
			{
				routedSB.AppendFront("private " + routedClassName + "(" + routedDeclName + " oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base("
						+ (string.ReferenceEquals(extName, null) ? typeref + ".typeVar, " : "") + (model.IsUniqueClassDefined() ? "graph.GlobalVariables.FetchObjectUniqueId()" : "-1") + ")\n");
			}
			else
			{
				routedSB.AppendFront("private " + routedClassName + "(" + routedDeclName + " oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base("
						+ (string.ReferenceEquals(extName, null) ? typeref + ".typeVar" : "") + ")\n");
			}
			routedSB.AppendFront("{\n");
			routedSB.Indent();
			if(model.IsUniqueResulting() && (type is NodeType || type is EdgeType))
				routedSB.AppendFront("uniqueId = oldElem.uniqueId;\n");

			if(type is InternalObjectType || type is InternalTransientObjectType)
			{
				routedSB.AppendFront("if(oldToNewObjectMap != null)\n");
				routedSB.AppendFrontIndented("oldToNewObjectMap.Add(oldElem, this);\n");
			}

			foreach(Entity member in type.AllMembers)
			{
				if(member.IsConst())
					continue;

				string attrName = FormatIdentifiable(member);
				if(member.Type is ContainerType)
				{
					if(type is InternalTransientObjectType)
					{
						routedSB.AppendFront("if(oldToNewObjectMap != null) {\n");
						routedSB.Indent();

						if(((ContainerType)member.Type).ContainsBaseInternalObjectType())
						{
							sb.AppendFront(attrName + ModelGen.ATTR_IMPL_SUFFIX + " = new " + FormatAttributeType(member.Type) + "();\n");
							GenContainerCopying(routedSB, member, attrName);
						}
						else
						{
							sb.AppendFront(attrName + ModelGen.ATTR_IMPL_SUFFIX + " = new " + FormatAttributeType(member.Type)
									+ "(oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ");\n");
						}

						routedSB.Unindent();
						routedSB.AppendFront("} else\n");
						routedSB.AppendFrontIndented(attrName + ModelGen.ATTR_IMPL_SUFFIX + " = " + "oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ";\n");
					}
					else
					{
						if(((ContainerType)member.Type).ContainsBaseInternalObjectType())
						{
							routedSB.AppendFront("if(oldToNewObjectMap != null) {\n");
							routedSB.Indent();

							sb.AppendFront(attrName + ModelGen.ATTR_IMPL_SUFFIX + " = new " + FormatAttributeType(member.Type) + "();\n");

							GenContainerCopying(routedSB, member, attrName);

							routedSB.Unindent();
							routedSB.AppendFront("} else\n");
							sb.AppendFrontIndented(attrName + ModelGen.ATTR_IMPL_SUFFIX + " = new " + FormatAttributeType(member.Type)
									+ "(oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ");\n");
						}
						else
						{
							sb.AppendFront(attrName + ModelGen.ATTR_IMPL_SUFFIX + " = new " + FormatAttributeType(member.Type)
									+ "(oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ");\n");
						}
					}
				}
				else if(model.IsCopyClassDefined()
						&& (member.Type.Classify() == Type.TypeClass.IS_EXTERNAL_CLASS_OBJECT
								|| member.Type.Classify() == Type.TypeClass.IS_OBJECT))
				{
					routedSB.AppendFront("if(oldToNewObjectMap != null) {\n");
					routedSB.Indent();
					routedSB.AppendFront("AttributeTypeObjectCopierComparer.Copy("
							+ "oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ", graph, oldToNewObjectMap);\n");
					routedSB.Unindent();
					routedSB.AppendFront("} else\n");
					routedSB.AppendFrontIndented(attrName + ModelGen.ATTR_IMPL_SUFFIX + " = "
							+ "oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ";\n");
				}
				else if(member.Type is BaseInternalObjectType)
				{
					routedSB.AppendFront("if(oldToNewObjectMap != null) {\n");
					routedSB.AppendFrontIndented(attrName + ModelGen.ATTR_IMPL_SUFFIX + " = "
							+ "(" + FormatAttributeType(member.Type) + ")"
							+ "Copy(oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ", graph, oldToNewObjectMap);\n");
					routedSB.AppendFront("} else\n");
					routedSB.AppendFrontIndented(attrName + ModelGen.ATTR_IMPL_SUFFIX + " = "
							+ "oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ";\n");
				}
				else
				{
					routedSB.AppendFront(attrName + ModelGen.ATTR_IMPL_SUFFIX + " = "
							+ "oldElem." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ";\n");
				}
			}
			routedSB.Unindent();
			routedSB.AppendFront("}\n");

			routedSB.AppendFront("\n");
			GenCopyHelper(routedSB, routedClassName);
		}

		private static void GenCopyHelper(SourceBuilder sb, string type)
		{
			sb.AppendFront("private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("if(oldObj == null)\n");
			sb.AppendFrontIndented("return null;\n");
			sb.AppendFront("if(oldToNewObjectMap.ContainsKey(oldObj))\n");
			sb.AppendFrontIndented("return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];\n");
			sb.AppendFront("else {\n");
			sb.Indent();
			sb.AppendFront("if(oldObj is GRGEN_LIBGR.IObject) {\n");
			sb.Indent();
			sb.AppendFront("GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);\n");
			//sb.appendFront("oldToNewObjectMap[oldObj] = newObj;\n");
			sb.AppendFront("return newObj;\n");
			sb.Unindent();
			sb.AppendFront("} else {\n");
			sb.Indent();
			sb.AppendFront("GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);\n");
			//sb.appendFront("oldToNewObjectMap[oldObj] = newObj;\n");
			sb.AppendFront("return newObj;\n");
			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenElementAttributeComparisonMethod(InheritanceType type, SourceBuilder routedSB,
				string routedClassName)
		{
			routedSB.AppendFront("public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {\n");
			routedSB.Indent();
			routedSB.AppendFront("if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))\n");
			routedSB.AppendFrontIndented("throw new Exception(\"Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!\");\n");

			routedSB.AppendFront("if(this == that)\n");
			routedSB.AppendFrontIndented("return true;\n");
			routedSB.AppendFront("if(!(that is " + routedClassName + "))\n");
			routedSB.AppendFrontIndented("return false;\n");
			routedSB.AppendFront(routedClassName + " that_ = (" + routedClassName + ")that;\n");

			routedSB.AppendFront("visitedObjects.Add(this, null);\n");
			routedSB.AppendFront("if(that != this)\n");
			routedSB.AppendFrontIndented("visitedObjects.Add(that, null);\n");

			routedSB.AppendFront("bool result = true\n");
			routedSB.Indent();
			foreach(Entity member in type.AllMembers)
			{
				if(member.IsConst())
					continue;

				string attrName = FormatIdentifiable(member);
				if(member.Type is MapType || member.Type is SetType
						|| member.Type is ArrayType || member.Type is DequeType)
				{
					routedSB.AppendFront("&& GRGEN_LIBGR.ContainerHelper.DeeplyEqual("
									+ attrName + ModelGen.ATTR_IMPL_SUFFIX + ", "
									+ "that_." + attrName + ModelGen.ATTR_IMPL_SUFFIX
									+ ", visitedObjects)\n");
				}
				else if(model.IsEqualClassDefined()
						&& (member.Type.Classify() == Type.TypeClass.IS_EXTERNAL_CLASS_OBJECT
								|| member.Type.Classify() == Type.TypeClass.IS_OBJECT))
				{
					routedSB.AppendFront("&& AttributeTypeObjectCopierComparer.IsEqual("
									+ attrName + ModelGen.ATTR_IMPL_SUFFIX + ", "
									+ "that_." + attrName + ModelGen.ATTR_IMPL_SUFFIX
									+ ", visitedObjects)\n");
				}
				else if(member.Type.Classify() == Type.TypeClass.IS_GRAPH)
				{
					routedSB.AppendFront("&& GRGEN_LIBGR.GraphHelper.Equal(" + attrName + ModelGen.ATTR_IMPL_SUFFIX + ", "
							+ "that_." + attrName + ModelGen.ATTR_IMPL_SUFFIX + ")\n");
				}
				else if(member.Type.Classify() == Type.TypeClass.IS_INTERNAL_CLASS_OBJECT
						|| member.Type.Classify() == Type.TypeClass.IS_INTERNAL_TRANSIENT_CLASS_OBJECT
						|| member.Type.Classify() == Type.TypeClass.IS_NODE
						|| member.Type.Classify() == Type.TypeClass.IS_EDGE)
				{
					routedSB.AppendFront("&& GRGEN_LIBGR.ContainerHelper.DeeplyEqual("
							+ attrName + ModelGen.ATTR_IMPL_SUFFIX + ", "
							+ "that_." + attrName + ModelGen.ATTR_IMPL_SUFFIX
							+ ", visitedObjects)\n");
				}
				else
					routedSB.AppendFront("&& " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " == " + "that_." + attrName + ModelGen.ATTR_IMPL_SUFFIX + "\n");
			}
			routedSB.AppendFront(";\n");
			routedSB.Unindent();

			routedSB.AppendFront("visitedObjects.Remove(this);\n");
			routedSB.AppendFront("visitedObjects.Remove(that);\n");

			routedSB.AppendFront("return result;\n");

			routedSB.Unindent();
			routedSB.AppendFront("}\n");
		}

		private void GenElementCreateMethods(InheritanceType type, bool isNode, string elemref, string allocName)
		{
			if(isNode)
			{
				sb.AppendFront("public static " + elemref + " CreateNode(GRGEN_LGSP.LGSPGraph graph)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront(elemref + " node;\n");
				sb.AppendFront("if(poolLevel == 0)\n");
				sb.AppendFrontIndented("node = new " + allocName + "();\n");
				sb.AppendFront("else\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("if(pool == null)\n");
				sb.AppendFrontIndented("pool = new " + elemref + "[GRGEN_LGSP.LGSPGraph.poolSize];\n");
				sb.AppendFront("node = pool[--poolLevel];\n");
				sb.AppendFront("node.lgspInhead = null;\n");
				sb.AppendFront("node.lgspOuthead = null;\n");
				sb.AppendFront("node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;\n");
				InitAllMembersNonConst(type, "node", true, false);
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.AppendFront("graph.AddNode(node);\n");
				sb.AppendFront("return node;\n");
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.Append("\n");

				sb.AppendFront("public static " + elemref + " CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, "
						+ "string nodeName)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront(elemref + " node;\n");
				sb.AppendFront("if(poolLevel == 0)\n");
				sb.AppendFrontIndented("node = new " + allocName + "();\n");
				sb.AppendFront("else\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("if(pool == null)\n");
				sb.AppendFrontIndented("pool = new " + elemref + "[GRGEN_LGSP.LGSPGraph.poolSize];\n");
				sb.AppendFront("node = pool[--poolLevel];\n");
				sb.AppendFront("node.lgspInhead = null;\n");
				sb.AppendFront("node.lgspOuthead = null;\n");
				sb.AppendFront("node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;\n");
				InitAllMembersNonConst(type, "node", true, false);
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.AppendFront("graph.AddNode(node, nodeName);\n");
				sb.AppendFront("return node;\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
			else
			{
				sb.AppendFront("public static " + elemref + " CreateEdge(GRGEN_LGSP.LGSPGraph graph, "
						+ "GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront(elemref + " edge;\n");
				sb.AppendFront("if(poolLevel == 0)\n");
				sb.AppendFrontIndented("edge = new " + allocName + "(source, target);\n");
				sb.AppendFront("else\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("if(pool == null)\n");
				sb.AppendFrontIndented("pool = new " + elemref + "[GRGEN_LGSP.LGSPGraph.poolSize];\n");
				sb.AppendFront("edge = pool[--poolLevel];\n");
				sb.AppendFront("edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;\n");
				sb.AppendFront("edge.lgspSource = source;\n");
				sb.AppendFront("edge.lgspTarget = target;\n");
				InitAllMembersNonConst(type, "edge", true, false);
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.AppendFront("graph.AddEdge(edge);\n");
				sb.AppendFront("return edge;\n");
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.Append("\n");

				sb.AppendFront("public static " + elemref + " CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, "
						+ "GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront(elemref + " edge;\n");
				sb.AppendFront("if(poolLevel == 0)\n");
				sb.AppendFrontIndented("edge = new " + allocName + "(source, target);\n");
				sb.AppendFront("else\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("if(pool == null)\n");
				sb.AppendFrontIndented("pool = new " + elemref + "[GRGEN_LGSP.LGSPGraph.poolSize];\n");
				sb.AppendFront("edge = pool[--poolLevel];\n");
				sb.AppendFront("edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;\n");
				sb.AppendFront("edge.lgspSource = source;\n");
				sb.AppendFront("edge.lgspTarget = target;\n");
				InitAllMembersNonConst(type, "edge", true, false);
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.AppendFront("graph.AddEdge(edge, edgeName);\n");
				sb.AppendFront("return edge;\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		private void GenElementRecycleMethod(string elemref)
		{
			sb.AppendFront("public override void Recycle()\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("if(pool == null)\n");
			sb.AppendFrontIndented("pool = new " + elemref + "[GRGEN_LGSP.LGSPGraph.poolSize];\n");
			sb.AppendFront("if(poolLevel < pool.Length)\n");
			sb.AppendFrontIndented("pool[poolLevel++] = this;\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void InitAllMembersNonConst(InheritanceType type, string varName,
				bool withDefaultInits, bool isResetAllAttributes)
		{
			curMemberOwner = varName;

			// if we don't currently create the method ResetAllAttributes
			// we replace the initialization code by a call to ResetAllAttributes, if it gets to large
			if(!isResetAllAttributes
					&& InitializationOperationsCount(type) > MAX_OPERATIONS_FOR_ATTRIBUTE_INITIALIZATION_INLINING)
			{
				sb.AppendFront(varName + ".ResetAllAttributes();\n");
				curMemberOwner = null;
				return;
			}

			sb.AppendFront("// implicit initialization, container creation of " + FormatIdentifiable(type) + "\n");

			// default attribute inits need to be generated if code must overwrite old values
			// only in constructor not needed, cause there taken care of by c#
			// if there is explicit initialization code, it's not needed, too,
			// but that's left for the compiler to optimize away
			if(withDefaultInits)
				GenDefaultInits(type, varName);

			// create containers, i.e. maps, sets, arrays, deques
			GenContainerInits(type, varName);

			// generate the user defined initializations, first for super types
			foreach(InheritanceType superType in type.AllSuperTypes)
				GenMemberInitsNonConst(superType, type, varName, withDefaultInits, isResetAllAttributes);
			// then for current type
			GenMemberInitsNonConst(type, type, varName, withDefaultInits, isResetAllAttributes);

			curMemberOwner = null;
		}

		private void GenDefaultInits(InheritanceType type, string varName)
		{
			foreach(Entity member in type.AllMembers)
			{
				if(member.IsConst())
					continue;

				Type t = member.Type;
				// handled down below, as containers must be created independent of initialization
				if(t is MapType || t is SetType
						|| t is ArrayType || t is DequeType)
					continue;

				string attrName = FormatIdentifiable(member);
				sb.AppendFront(varName + ".@" + attrName + " = ");
				if(t is ByteType || t is ShortType || t is IntType
						|| t is EnumType || t is DoubleType)
					sb.Append("0;\n");
				else if(t is FloatType)
					sb.Append("0f;\n");
				else if(t is LongType)
					sb.Append("0L;\n");
				else if(t is BooleanType)
					sb.Append("false;\n");
				else if(t is StringType || t is ObjectType || t is VoidType
						|| t is ExternalObjectType || t is GraphType || t is InheritanceType)
					sb.Append("null;\n");
				else
					throw new ArgumentException("Unknown Entity: " + member + "(" + t + ")");
			}
		}

		private void GenContainerInits(InheritanceType type, string varName)
		{
			foreach(Entity member in type.AllMembers)
			{
				if(member.IsConst())
					continue;

				Type t = member.Type;
				if(!(t is MapType || t is SetType
						|| t is ArrayType || t is DequeType))
					continue;

				string attrName = FormatIdentifiable(member);
				sb.AppendFront(varName + ".@" + attrName + " = ");
				if(t is MapType)
				{
					MapType mapType = (MapType)t;
					sb.Append("new " + formatAttributeType(mapType) + "();\n");
				}
				else if(t is SetType)
				{
					SetType setType = (SetType)t;
					sb.Append("new " + formatAttributeType(setType) + "();\n");
				}
				else if(t is ArrayType)
				{
					ArrayType arrayType = (ArrayType)t;
					sb.Append("new " + formatAttributeType(arrayType) + "();\n");
				}
				else if(t is DequeType)
				{
					DequeType dequeType = (DequeType)t;
					sb.Append("new " + formatAttributeType(dequeType) + "();\n");
				}
			}
		}

		private static int InitializationOperationsCount(InheritanceType targetType)
		{
			int initializationOperations = 0;

			// attribute initializations from super classes not overridden in target class
			foreach(InheritanceType superType in targetType.AllSuperTypes)
			{
				foreach(MemberInit memberInit in superType.MemberInits)
				{
					if(memberInit.Member.IsConst())
						continue;
					foreach(MemberInit tmi in targetType.MemberInits)
					{
						if(memberInit.Member == tmi.Member)
							goto member_init_loopContinue;
					}
					++initializationOperations;
		member_init_loopContinue:;
				}
	member_init_loopBreak:
				foreach(MapInit mapInit in superType.MapInits)
				{
					if(mapInit.Member.IsConst())
						continue;
					foreach(MapInit tmi in targetType.MapInits)
					{
						if(mapInit.Member == tmi.Member)
							goto map_init_loopContinue;
					}
					initializationOperations += mapInit.MapItems.Count;
		map_init_loopContinue:;
				}
	map_init_loopBreak:
				foreach(SetInit setInit in superType.SetInits)
				{
					if(setInit.Member.IsConst())
						continue;
					foreach(SetInit tsi in targetType.SetInits)
					{
						if(setInit.Member == tsi.Member)
							goto set_init_loopContinue;
					}
					initializationOperations += setInit.SetItems.Count;
		set_init_loopContinue:;
				}
	set_init_loopBreak:
				foreach(ArrayInit arrayInit in superType.ArrayInits)
				{
					if(arrayInit.Member.IsConst())
						continue;
					foreach(ArrayInit tai in targetType.ArrayInits)
					{
						if(arrayInit.Member == tai.Member)
							goto array_init_loopContinue;
					}
					initializationOperations += arrayInit.ArrayItems.Count;
		array_init_loopContinue:;
				}
	array_init_loopBreak:
				foreach(DequeInit dequeInit in superType.DequeInits)
				{
					if(dequeInit.Member.IsConst())
						continue;
					foreach(DequeInit tdi in targetType.DequeInits)
					{
						if(dequeInit.Member == tdi.Member)
							goto deque_init_loopContinue;
					}
					initializationOperations += dequeInit.DequeItems.Count;
		deque_init_loopContinue:;
				}
	deque_init_loopBreak:;
			}

			// attribute initializations of target class
			foreach(MemberInit memberInit in targetType.MemberInits)
			{
				if(!memberInit.Member.IsConst())
					++initializationOperations;
			}

			foreach(MapInit mapInit in targetType.MapInits)
			{
				if(!mapInit.Member.IsConst())
					initializationOperations += mapInit.MapItems.Count;
			}

			foreach(SetInit setInit in targetType.SetInits)
			{
				if(!setInit.Member.IsConst())
					initializationOperations += setInit.SetItems.Count;
			}

			foreach(ArrayInit arrayInit in targetType.ArrayInits)
			{
				if(!arrayInit.Member.IsConst())
					initializationOperations += arrayInit.ArrayItems.Count;
			}

			foreach(DequeInit dequeInit in targetType.DequeInits)
			{
				if(!dequeInit.Member.IsConst())
					initializationOperations += dequeInit.DequeItems.Count;
			}

			return initializationOperations;
		}

		private void InitAllMembersConst(InheritanceType type, string className,
				string varName)
		{
			curMemberOwner = varName;

			IList<string> staticInitializers = new List<string>();

			sb.Append("\n");

			// generate the user defined initializations, first for super types
			foreach(InheritanceType superType in type.AllSuperTypes)
				GenMemberInitsConst(superType, type, staticInitializers);
			// then for current type
			GenMemberInitsConst(type, type, staticInitializers);

			sb.AppendFront("static " + className + "() {\n");
			sb.Indent();
			foreach(string staticInit in staticInitializers)
				sb.AppendFront(staticInit + "();\n");
			sb.Unindent();
			sb.AppendFront("}\n");

			curMemberOwner = null;
		}

		private void GenMemberInitsNonConst(InheritanceType type, InheritanceType targetType,
				string varName, bool withDefaultInits, bool isResetAllAttributes)
		{
			if(rootTypes.Contains(type.Ident.ToString())) // skip root types, they don't possess attributes
				return;
			sb.AppendFront("// explicit initializations of " + FormatIdentifiable(type)
					+ " for target " + FormatIdentifiable(targetType) + "\n");

			// emit all initializations in base classes of members that are used for init'ing other members,
			// i.e. prevent optimization of using only the closest initialization
			// TODO: generalize to all types in between type and target type

			// init members of primitive value with explicit initialization
			GenMemberInitsNonConstPrimitiveType(type, targetType, varName);

			// init members of map value with explicit initialization
			GenMemberInitsNonConstMapType(type, targetType, varName);

			// init members of set value with explicit initialization
			GenMemberInitsNonConstSetType(type, targetType, varName);

			// init members of array value with explicit initialization
			GenMemberInitsNonConstArrayType(type, targetType, varName);

			// init members of deque value with explicit initialization
			GenMemberInitsNonConstDequeType(type, targetType, varName);
		}

		private void GenMemberInitsNonConstPrimitiveType(InheritanceType type, InheritanceType targetType, string varName)
		{
			NeededEntities needs = new NeededEntities(EnumSet.Of(Needs.MEMBERS));
			foreach(MemberInit memberInit in type.MemberInits)
				memberInit.Expression.CollectNeededEntities(needs);
			foreach(MemberInit memberInit in targetType.MemberInits)
				memberInit.Expression.CollectNeededEntities(needs);

			foreach(MemberInit memberInit in type.MemberInits)
			{
				Entity member = memberInit.Member;
				if(memberInit.Member.IsConst())
					continue;
				if(!needs.members.Contains(member)
						&& !GenerateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
					continue;

				string attrName = FormatIdentifiable(memberInit.Member);
				sb.AppendFront(varName + ".@" + attrName + " = ");
				GenExpression(sb, memberInit.Expression, null);
				sb.Append(";\n");
			}
		}

		private void GenMemberInitsNonConstMapType(InheritanceType type, InheritanceType targetType, string varName)
		{
			foreach(MapInit mapInit in type.MapInits)
			{
				Entity member = mapInit.Member;
				if(mapInit.Member.IsConst())
					continue;
				if(!GenerateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
					continue;

				string attrName = FormatIdentifiable(mapInit.Member);
				foreach(ExpressionPair item in mapInit.MapItems)
				{
					sb.AppendFront(varName + ".@" + attrName + "[");
					GenExpression(sb, item.KeyExpr, null);
					sb.Append("] = ");
					GenExpression(sb, item.ValueExpr, null);
					sb.Append(";\n");
				}
			}
		}

		private void GenMemberInitsNonConstSetType(InheritanceType type, InheritanceType targetType, string varName)
		{
			foreach(SetInit setInit in type.SetInits)
			{
				Entity member = setInit.Member;
				if(setInit.Member.IsConst())
					continue;
				if(!GenerateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
					continue;

				string attrName = FormatIdentifiable(setInit.Member);
				foreach(Expression item in setInit.SetItems)
				{
					sb.AppendFront(varName + ".@" + attrName + "[");
					GenExpression(sb, item, null);
					sb.Append("] = null;\n");
				}
			}
		}

		private void GenMemberInitsNonConstArrayType(InheritanceType type, InheritanceType targetType, string varName)
		{
			foreach(ArrayInit arrayInit in type.ArrayInits)
			{
				Entity member = arrayInit.Member;
				if(arrayInit.Member.IsConst())
					continue;
				if(!GenerateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
					continue;

				string attrName = FormatIdentifiable(arrayInit.Member);
				foreach(Expression item in arrayInit.ArrayItems)
				{
					sb.AppendFront(varName + ".@" + attrName + ".Add(");
					GenExpression(sb, item, null);
					sb.Append(");\n");
				}
			}
		}

		private void GenMemberInitsNonConstDequeType(InheritanceType type, InheritanceType targetType, string varName)
		{
			foreach(DequeInit dequeInit in type.DequeInits)
			{
				Entity member = dequeInit.Member;
				if(dequeInit.Member.IsConst())
					continue;
				if(!GenerateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
					continue;

				string attrName = FormatIdentifiable(dequeInit.Member);
				foreach(Expression item in dequeInit.DequeItems)
				{
					sb.AppendFront(varName + ".@" + attrName + ".Add(");
					GenExpression(sb, item, null);
					sb.Append(");\n");
				}
			}
		}

		private void GenMemberInitsConst(InheritanceType type, InheritanceType targetType,
				IList<string> staticInitializers)
		{
			if(rootTypes.Contains(type.Ident.ToString())) // skip root types, they don't possess attributes
				return;
			sb.AppendFront("// explicit initializations of " + FormatIdentifiable(type)
					+ " for target " + FormatIdentifiable(targetType) + "\n");

			HashSet<Entity> initializedConstMembers = new HashSet<Entity>();

			// init const members of primitive value with explicit initialization
			GenMemberInitsConstPrimitiveType(type, targetType, initializedConstMembers);

			// init const members of map value with explicit initialization
			GenMemberInitsConstMapType(type, targetType, staticInitializers, initializedConstMembers);

			// init const members of set value with explicit initialization
			GenMemberInitsConstSetType(type, targetType, staticInitializers, initializedConstMembers);

			// init const members of array value with explicit initialization
			GenMemberInitsConstArrayType(type, targetType, staticInitializers, initializedConstMembers);

			// init const members of deque value with explicit initialization
			GenMemberInitsConstDequeType(type, targetType, staticInitializers, initializedConstMembers);

			sb.AppendFront("// implicit initializations of " + FormatIdentifiable(type)
					+ " for target " + FormatIdentifiable(targetType) + "\n");

			GenMemberImplicitInitsNonConst(type, targetType, initializedConstMembers);
		}

		private void GenMemberInitsConstPrimitiveType(InheritanceType type, InheritanceType targetType,
				HashSet<Entity> initializedConstMembers)
		{
			foreach(MemberInit memberInit in type.MemberInits)
			{
				Entity member = memberInit.Member;
				if(!member.IsConst())
					continue;
				if(!GenerateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
					continue;

				string attrType = FormatAttributeType(member);
				string attrName = FormatIdentifiable(member);
				sb.AppendFront("private static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = ");
				GenExpression(sb, memberInit.Expression, null);
				sb.Append(";\n");

				initializedConstMembers.Add(member);
			}
		}

		private void GenMemberInitsConstMapType(InheritanceType type, InheritanceType targetType,
				IList<string> staticInitializers, HashSet<Entity> initializedConstMembers)
		{
			foreach(MapInit mapInit in type.MapInits)
			{
				Entity member = mapInit.Member;
				if(!member.IsConst())
					continue;
				if(!GenerateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
					continue;

				string attrType = FormatAttributeType(member);
				string attrName = FormatIdentifiable(member);
				sb.AppendFront("private static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = " +
						"new " + attrType + "();\n");
				staticInitializers.Add("init_" + attrName);
				sb.AppendFront("static void init_" + attrName + "() {\n");
				sb.Indent();
				foreach(ExpressionPair item in mapInit.MapItems)
				{
					sb.AppendFront("");
					sb.Append(attrName + ModelGen.ATTR_IMPL_SUFFIX);
					sb.Append("[");
					GenExpression(sb, item.KeyExpr, null);
					sb.Append("] = ");
					GenExpression(sb, item.ValueExpr, null);
					sb.Append(";\n");
				}
				sb.Unindent();
				sb.AppendFront("}\n");

				initializedConstMembers.Add(member);
			}
		}

		private void GenMemberInitsConstSetType(InheritanceType type, InheritanceType targetType,
				IList<string> staticInitializers, HashSet<Entity> initializedConstMembers)
		{
			foreach(SetInit setInit in type.SetInits)
			{
				Entity member = setInit.Member;
				if(!member.IsConst())
					continue;
				if(!GenerateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
					continue;

				string attrType = FormatAttributeType(member);
				string attrName = FormatIdentifiable(member);
				sb.AppendFront("private static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = " +
						"new " + attrType + "();\n");
				staticInitializers.Add("init_" + attrName);
				sb.AppendFront("static void init_" + attrName + "() {\n");
				sb.Indent();
				foreach(Expression item in setInit.SetItems)
				{
					sb.AppendFront("");
					sb.Append(attrName + ModelGen.ATTR_IMPL_SUFFIX);
					sb.Append("[");
					GenExpression(sb, item, null);
					sb.Append("] = null;\n");
				}
				sb.Unindent();
				sb.AppendFront("}\n");

				initializedConstMembers.Add(member);
			}
		}

		private void GenMemberInitsConstArrayType(InheritanceType type, InheritanceType targetType,
				IList<string> staticInitializers, HashSet<Entity> initializedConstMembers)
		{
			foreach(ArrayInit arrayInit in type.ArrayInits)
			{
				Entity member = arrayInit.Member;
				if(!member.IsConst())
					continue;
				if(!GenerateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
					continue;

				string attrType = FormatAttributeType(member);
				string attrName = FormatIdentifiable(member);
				sb.AppendFront("private static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = " +
						"new " + attrType + "();\n");
				staticInitializers.Add("init_" + attrName);
				sb.AppendFront("static void init_" + attrName + "() {\n");
				sb.Indent();
				foreach(Expression item in arrayInit.ArrayItems)
				{
					sb.AppendFront("");
					sb.Append(attrName + ModelGen.ATTR_IMPL_SUFFIX);
					sb.Append(".Add(");
					GenExpression(sb, item, null);
					sb.Append(");\n");
				}
				sb.Unindent();
				sb.AppendFront("}\n");

				initializedConstMembers.Add(member);
			}
		}

		private void GenMemberInitsConstDequeType(InheritanceType type, InheritanceType targetType,
				IList<string> staticInitializers, HashSet<Entity> initializedConstMembers)
		{
			foreach(DequeInit dequeInit in type.DequeInits)
			{
				Entity member = dequeInit.Member;
				if(!member.IsConst())
					continue;
				if(!GenerateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
					continue;

				string attrType = FormatAttributeType(member);
				string attrName = FormatIdentifiable(member);
				sb.AppendFront("private static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = " +
						"new " + attrType + "();\n");
				staticInitializers.Add("init_" + attrName);
				sb.AppendFront("static void init_" + attrName + "() {\n");
				sb.Indent();
				foreach(Expression item in dequeInit.DequeItems)
				{
					sb.AppendFront("");
					sb.Append(attrName + ModelGen.ATTR_IMPL_SUFFIX);
					sb.Append(".Enqueue(");
					GenExpression(sb, item, null);
					sb.Append(");\n");
				}
				sb.Unindent();
				sb.AppendFront("}\n");

				initializedConstMembers.Add(member);
			}
		}

		private void GenMemberImplicitInitsNonConst(InheritanceType type, InheritanceType targetType,
				HashSet<Entity> initializedConstMembers)
		{
			foreach(Entity member in type.Members)
			{
				if(!member.IsConst())
					continue;
				if(initializedConstMembers.Contains(member))
					continue;
				if(!GenerateInitializationOfTypeAtCreatingTargetTypeInitialization(member, type, targetType))
					continue;

				Type memberType = member.Type;
				string attrType = FormatAttributeType(member);
				string attrName = FormatIdentifiable(member);

				if(memberType is MapType || memberType is SetType
						|| memberType is ArrayType || memberType is DequeType)
				{
					sb.AppendFront("private static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX
							+ " = " + "new " + attrType + "();\n");
				}
				else
					sb.AppendFront("private static readonly " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + ";\n");
			}
		}

		internal static bool GenerateInitializationOfTypeAtCreatingTargetTypeInitialization(
				Entity member, InheritanceType type, InheritanceType targetType)
		{
			// to decide on generating targetType initialization:
			//  - generate initialization of currently focused supertype type?
			// goal: only generate the initialization closest to the target type
			// -> don't generate initialization of type if there exists a subtype of type,
			// which is a supertype of the target type, and which contains an initialization

			ISet<InheritanceType> childrenOfFocusedType = new LinkedHashSet<InheritanceType>(type.AllSubTypes);
			childrenOfFocusedType.Remove(type); // we want children only, comes with type itself included

			ISet<InheritanceType> targetTypeAndParents = new LinkedHashSet<InheritanceType>(targetType.AllSuperTypes);
			targetTypeAndParents.Add(targetType); // we want it inclusive target, comes exclusive

			ISet<InheritanceType> intersection = new LinkedHashSet<InheritanceType>(childrenOfFocusedType);
			intersection.RetainAll(targetTypeAndParents); // the set is empty if type==targetType

			foreach(InheritanceType relevantChildrenOfFocusedType in intersection)
			{
				// if a type below focused type contains an initialization for current member
				// then we skip the initialization of the focused type
				foreach(MemberInit tmi in relevantChildrenOfFocusedType.MemberInits)
				{
					if(member == tmi.Member)
						return false;
				}
				foreach(MapInit tmi in relevantChildrenOfFocusedType.MapInits)
				{
					if(member == tmi.Member)
						return false;
				}
				foreach(SetInit tsi in relevantChildrenOfFocusedType.SetInits)
				{
					if(member == tsi.Member)
						return false;
				}
				foreach(ArrayInit tai in relevantChildrenOfFocusedType.ArrayInits)
				{
					if(member == tai.Member)
						return false;
				}
				foreach(DequeInit tdi in relevantChildrenOfFocusedType.DequeInits)
				{
					if(member == tdi.Member)
						return false;
				}
			}

			return true;
		}

		protected internal override void GenQualAccess(SourceBuilder sb, Qualification qual, object modifyGenerationState)
		{
			Entity owner = qual.Owner;
			sb.Append("((I" + GetInheritanceTypePrefix(owner) +
					FormatIdentifiable(owner.Type) + ") ");
			sb.Append(FormatEntity(owner) + ").@" + FormatIdentifiable(qual.Member));
		}

		protected internal override void GenMemberAccess(SourceBuilder sb, Entity member)
		{
			if(!string.ReferenceEquals(curMemberOwner, null))
				sb.Append(curMemberOwner + ".");
			sb.Append("@" + FormatIdentifiable(member));
		}

		/// <summary>
		/// Generate the attribute accessor implementations of the given type
		/// </summary>
		private void GenAttributesAndAttributeAccessImpl(InheritanceType type)
		{
			SourceBuilder routedSB = sb;
			string extName = type.ExternalName;
			string extModifier = "";

			// what's that?
			if(!string.ReferenceEquals(extName, null))
			{
				routedSB = StubBuffer;
				extModifier = "override ";

				foreach(Entity e in type.AllMembers)
					GenAttributeAccess(type, e, "public abstract ");
			}

			// Create the implementation of the attributes.
			// If an external name is given for this type, this is written
			// into the stub file with an "override" modifier on the accessors.
			foreach(Entity member in type.AllMembers)
				GenAttributeGetterSetterAndMember(type, routedSB, extModifier, member);

			GenGetAttributeByName(type);

			GenSetAttributeByName(type);

			GenResetAllAttributes(type);
		}

		private void GenAttributeGetterSetterAndMember(InheritanceType type, SourceBuilder routedSB, string extModifier,
				Entity member)
		{
			string attrType = FormatAttributeType(member);
			string attrName = FormatIdentifiable(member);

			if(member.IsConst())
			{
				// no member for const attributes, no setter for const attributes
				// they are class static, the member is created at the point of initialization
				routedSB.AppendFront("public " + extModifier + attrType + " @" + attrName + "\n");
				routedSB.AppendFront("{\n");
				routedSB.AppendFrontIndented("get { return " + attrName + ModelGen.ATTR_IMPL_SUFFIX + "; }\n");
				routedSB.AppendFront("}\n");
			}
			else
			{
				// member, getter, setter for non-const attributes
				routedSB.Append("\n");
				routedSB.AppendFront("private " + attrType + " " + attrName + ModelGen.ATTR_IMPL_SUFFIX + ";\n");
				routedSB.AppendFront("public " + extModifier + attrType + " @" + attrName + "\n");
				routedSB.AppendFront("{\n");
				routedSB.Indent();
				routedSB.AppendFront("get { return " + attrName + ModelGen.ATTR_IMPL_SUFFIX + "; }\n");
				routedSB.AppendFront("set { " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = value; }\n");
				routedSB.Unindent();
				routedSB.AppendFront("}\n");
			}

			// what's that?
			Entity overriddenMember = type.GetOverriddenMember(member);
			if(overriddenMember != null)
			{
				routedSB.Append("\n");
				routedSB.AppendFront("object "
						+ FormatElementInterfaceRef(overriddenMember.Owner)
						+ ".@" + attrName + "\n");
				routedSB.AppendFront("{\n");
				routedSB.Indent();
				routedSB.AppendFront("get { return " + attrName + ModelGen.ATTR_IMPL_SUFFIX + "; }\n");
				routedSB.AppendFront("set { " + attrName + ModelGen.ATTR_IMPL_SUFFIX + " = (" + attrType + ") value; }\n");
				routedSB.Unindent();
				routedSB.AppendFront("}\n");
			}
		}

		private void GenGetAttributeByName(InheritanceType type)
		{
			sb.AppendFront("public override object GetAttribute(string attrName)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			if(type.AllMembers.Count != 0)
			{
				sb.AppendFront("switch(attrName)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				foreach(Entity member in type.AllMembers)
				{
					string name = FormatIdentifiable(member);
					sb.AppendFront("case \"" + name + "\": return this.@" + name + ";\n");
				}
				sb.Unindent();
				sb.AppendFront("}\n");
			}
			sb.AppendFront("throw new NullReferenceException(\n");
			sb.AppendFrontIndented("\"The " + GetKindName(type)
					+ " type \\\"" + FormatIdentifiable(type)
					+ "\\\" does not have the attribute \\\"\" + attrName + \"\\\"!\");\n");

			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenSetAttributeByName(InheritanceType type)
		{
			sb.AppendFront("public override void SetAttribute(string attrName, object value)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			if(type.AllMembers.Count != 0)
			{
				sb.AppendFront("switch(attrName)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				foreach(Entity member in type.AllMembers)
				{
					string name = FormatIdentifiable(member);
					if(member.IsConst())
					{
						sb.AppendFront("case \"" + name + "\": ");
						sb.Append("throw new NullReferenceException(");
						sb.Append("\"The attribute " + name + " of the " + GetKindName(type)
								+ " type \\\"" + FormatIdentifiable(type)
								+ "\\\" is read only!\");\n");
					}
					else
					{
						sb.AppendFront("case \"" + name + "\": this.@" + name + " = ("
								+ FormatAttributeType(member) + ") value; return;\n");
					}
				}
				sb.Unindent();
				sb.AppendFront("}\n");
			}
			sb.AppendFront("throw new NullReferenceException(\n");
			sb.AppendFrontIndented("\"The " + GetKindName(type)
					+ " type \\\"" + FormatIdentifiable(type)
					+ "\\\" does not have the attribute \\\"\" + attrName + \"\\\"!\");\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenResetAllAttributes(InheritanceType type)
		{
			sb.AppendFront("public override void ResetAllAttributes()\n");
			sb.AppendFront("{\n");
			sb.Indent();
			InitAllMembersNonConst(type, "this", true, true);
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenParameterPassingMethodCall(InheritanceType type, FunctionMethod fm)
		{
			sb.AppendFront("case \"" + fm.Ident.ToString() + "\":\n");
			sb.AppendFrontIndented("return @" + fm.Ident.ToString() + "(actionEnv, graph");
			int i = 0;
			foreach(Entity inParam in fm.Parameters)
			{
				sb.Append(", (" + FormatType(inParam.Type) + ")arguments[" + i + "]");
				++i;
			}
			sb.Append(");\n");
		}

		private void GenParameterPassingMethodCall(InheritanceType type, ProcedureMethod pm)
		{
			sb.AppendFront("case \"" + pm.Ident.ToString() + "\":\n");
			sb.AppendFront("{\n");
			sb.Indent();
			int i = 0;
			foreach(Type outType in pm.ReturnTypes)
			{
				sb.AppendFront(FormatType(outType));
				sb.Append(" ");
				sb.Append("_out_param_" + i + ";\n");
				++i;
			}
			sb.AppendFront("@" + pm.Ident.ToString() + "(actionEnv, graph");
			i = 0;
			foreach(Entity inParam in pm.Parameters)
			{
				sb.Append(", (" + FormatType(inParam.Type) + ")arguments[" + i + "]");
				++i;
			}
			for(i = 0; i < pm.ReturnTypes.Count; ++i)
			{
				sb.Append(", out ");
				sb.Append("_out_param_" + i);
			}
			sb.Append(");\n");
			for(i = 0; i < pm.ReturnTypes.Count; ++i)
			{
				sb.AppendFront("ReturnArray_" + pm.Ident.ToString() + "_" + type.Ident.ToString()
						+ "[" + i + "] = ");
				sb.Append("_out_param_" + i + ";\n");
			}
			sb.AppendFront("return ReturnArray_" + pm.Ident.ToString() + "_" + type.Ident.ToString() + ";\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenParameterPassingReturnArray(InheritanceType type, ProcedureMethod pm)
		{
			sb.AppendFront("private static object[] ReturnArray_" + pm.Ident.ToString() + "_"
					+ type.Ident.ToString() + " = new object[" + pm.ReturnTypes.Count
					+ "]; // helper array for multi-value-returns, to allow for contravariant parameter assignment\n");
		}

		private void GenMethods(InheritanceType type)
		{
			sb.Append("\n");

			GenApplyFunctionMethodDispatcher(type);

			foreach(FunctionMethod fm in type.AllFunctionMethods)
				GenFunctionMethod(fm);

			//////////////////////////////////////////////////////////////

			GenApplyProcedureMethodDispatcher(type);

			foreach(ProcedureMethod pm in type.AllProcedureMethods)
			{
				ForceNotConstant(pm.Statements);
				GenParameterPassingReturnArray(type, pm);
			}

			foreach(ProcedureMethod pm in type.AllProcedureMethods)
				GenProcedureMethod(pm);
		}

		private void GenApplyFunctionMethodDispatcher(InheritanceType type)
		{
			sb.AppendFront("public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, "
					+ "string name, object[] arguments)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("switch(name)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			foreach(FunctionMethod fm in type.AllFunctionMethods)
			{
				ForceNotConstant(fm.Statements);
				GenParameterPassingMethodCall(type, fm);
			}
			sb.AppendFront("default: throw new NullReferenceException(\"" + FormatIdentifiable(type)
					+ " does not have the function method \" + name + \"!\");\n");
			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenFunctionMethod(FunctionMethod fm)
		{
			IList<string> staticInitializers = new List<string>();
			string pathPrefixForElements = "";
			Dictionary<Entity, string> alreadyDefinedEntityToName = new Dictionary<Entity, string>();
			GenLocalContainersEvals(sb, fm.Statements, staticInitializers,
					pathPrefixForElements, alreadyDefinedEntityToName);

			sb.AppendFront("public " + FormatType(fm.ReturnType) + " ");
			sb.Append(fm.Ident.ToString()
					+ "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_");
			foreach(Entity inParam in fm.Parameters)
			{
				sb.Append(", ");
				sb.Append(FormatType(inParam.Type));
				sb.Append(" ");
				sb.Append(FormatEntity(inParam));
			}
			sb.Append(")\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;\n");
			sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;\n");
			ModifyGenerationState modifyGenState = new ModifyGenerationState(model, null, "", false,
					be.sys.EmitProfilingInstrumentation());
			ModifyEvalGen evalGen = new ModifyEvalGen(be, null, nodeTypePrefix, edgeTypePrefix, objectTypePrefix, transientObjectTypePrefix);
			foreach(EvalStatement evalStmt in fm.Statements)
			{
				modifyGenState.functionOrProcedureName = fm.Ident.ToString();
				evalGen.GenEvalStmt(sb, modifyGenState, evalStmt);
			}
			sb.Unindent();
			sb.AppendFront("}\n");

			if(model.AreFunctionsParallel())
			{
				sb.AppendFront("public " + FormatType(fm.ReturnType) + " ");
				sb.Append(fm.Ident.ToString()
						+ "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_");
				foreach(Entity inParam in fm.Parameters)
				{
					sb.Append(", ");
					sb.Append(FormatType(inParam.Type));
					sb.Append(" ");
					sb.Append(FormatEntity(inParam));
				}
				sb.Append(", int threadId");
				sb.Append(")\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;\n");
				sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;\n");
				modifyGenState = new ModifyGenerationState(model, null, "", true, be.sys.EmitProfilingInstrumentation());
				foreach(EvalStatement evalStmt in fm.Statements)
				{
					modifyGenState.functionOrProcedureName = fm.Ident.ToString();
					evalGen.GenEvalStmt(sb, modifyGenState, evalStmt);
				}
				sb.Unindent();
				sb.Append("}\n");
			}
		}

		private void GenApplyProcedureMethodDispatcher(InheritanceType type)
		{
			sb.AppendFront("public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph,"
					+ " string name, object[] arguments)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("switch(name)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			foreach(ProcedureMethod pm in type.AllProcedureMethods)
				GenParameterPassingMethodCall(type, pm);
			sb.AppendFront("default: throw new NullReferenceException(\"" + FormatIdentifiable(type)
					+ " does not have the procedure method \" + name + \"!\");\n");
			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenProcedureMethod(ProcedureMethod pm)
		{
			IList<string> staticInitializers = new List<string>();
			string pathPrefixForElements = "";
			Dictionary<Entity, string> alreadyDefinedEntityToName = new Dictionary<Entity, string>();
			GenLocalContainersEvals(sb, pm.Statements, staticInitializers,
					pathPrefixForElements, alreadyDefinedEntityToName);

			sb.AppendFront("public void ");
			sb.Append(pm.Ident.ToString() + "(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_");
			foreach(Entity inParam in pm.Parameters)
			{
				sb.Append(", ");
				sb.Append(FormatType(inParam.Type));
				sb.Append(" ");
				sb.Append(FormatEntity(inParam));
			}
			int i = 0;
			foreach(Type outType in pm.ReturnTypes)
			{
				sb.Append(", out ");
				sb.Append(FormatType(outType));
				sb.Append(" ");
				sb.Append("_out_param_" + i);
				++i;
			}
			sb.Append(")\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;\n");
			sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;\n");
			ModifyGenerationState modifyGenState = new ModifyGenerationState(model, null, "", false,
					be.sys.EmitProfilingInstrumentation());
			ModifyExecGen execGen = new ModifyExecGen(be, nodeTypePrefix, edgeTypePrefix, objectTypePrefix, transientObjectTypePrefix);
			ModifyEvalGen evalGen = new ModifyEvalGen(be, execGen, nodeTypePrefix, edgeTypePrefix, objectTypePrefix, transientObjectTypePrefix);

			if(be.sys.MayFireDebugEvents())
			{
				sb.AppendFront("((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering(");
				sb.Append("\"" + pm.Ident.ToString() + "\"");
				foreach(Entity inParam in pm.Parameters)
				{
					sb.Append(", ");
					sb.Append(FormatEntity(inParam));
				}
				sb.Append(");\n");
			}

			foreach(EvalStatement evalStmt in pm.Statements)
			{
				modifyGenState.functionOrProcedureName = pm.Ident.ToString();
				evalGen.GenEvalStmt(sb, modifyGenState, evalStmt);
			}
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		////////////////////////////////////
		// Type implementation generation //
		////////////////////////////////////

		/// <summary>
		/// Generates the type implementation
		/// </summary>
		private void GenTypeImplementation<T1>(ICollection<T1> allTypes, InheritanceType type,
				string packageName) where T1 : de.unika.ipd.grgen.ir.model.type.InheritanceType
		{
			string typeident = FormatIdentifiable(type);
			string typename = FormatTypeClassName(type);
			string typeref = FormatTypeClassRef(type);
			string elemref = FormatInheritanceClassRef(type);
			string extName = type.ExternalName;
			string allocName = !string.ReferenceEquals(extName, null) ? "global::" + extName : elemref;
			string kindStr = GetKindName(type);

			sb.Append("\n");
			sb.AppendFront("public sealed partial class " + typename + " : GRGEN_LIBGR." + kindStr + "Type\n");
			sb.AppendFront("{\n");
			sb.Indent();

			sb.AppendFront("public static " + typeref + " typeVar = new " + typeref + "();\n");
			GenIsA(allTypes, type);
			GenIsMyType(allTypes, type);
			GenAttributeAttributes(type);

			sb.AppendFront("public " + typename + "() "
					+ ": base((int) " + FormatInheritanceTypeValue(type) + "Types.@" + typeident + ")\n");
			sb.AppendFront("{\n");
			sb.Indent();
			GenAttributeInit(type);
			AddAnnotations(sb, type, "annotations");
			sb.Unindent();
			sb.AppendFront("}\n");

			sb.AppendFront("public override string Name { get { return \"" + typeident + "\"; } }\n");
			sb.AppendFront("public override string Package { get { return " + (!GetPackagePrefix(type).Equals("") ? "\"" + GetPackagePrefix(type) + "\"" : "null") + "; } }\n");
			sb.AppendFront("public override string PackagePrefixedName { get { return \"" + GetPackagePrefixDoubleColon(type) + typeident + "\"; } }\n");
			switch(type.Ident.ToString())
			{
			case "Node":
				sb.AppendFront("public override string " + FormatInheritanceTypeValue(type) + "InterfaceName { get { return "
						+ "\"de.unika.ipd.grGen.libGr.INode\"; } }\n");
				break;
			case "AEdge":
				sb.AppendFront("public override string " + FormatInheritanceTypeValue(type) + "InterfaceName { get { return "
						+ "\"de.unika.ipd.grGen.libGr.IEdge\"; } }\n");
				break;
			case "Edge":
				sb.AppendFront("public override string " + FormatInheritanceTypeValue(type) + "InterfaceName { get { return "
						+ "\"de.unika.ipd.grGen.libGr.IDEdge\"; } }\n");
				break;
			case "UEdge":
				sb.AppendFront("public override string " + FormatInheritanceTypeValue(type) + "InterfaceName { get { return "
						+ "\"de.unika.ipd.grGen.libGr.IUEdge\"; } }\n");
				break;
			default:
				sb.AppendFront("public override string " + FormatInheritanceTypeValue(type) + "InterfaceName { get { return "
						+ "\"de.unika.ipd.grGen.Model_" + model.Ident + "."
						+ GetPackagePrefixDot(type) + "I" + getInheritanceTypePrefix(type) + FormatIdentifiable(type)
						+ "\"; } }\n");
			break;
			}
			if(type.IsAbstract())
				sb.AppendFront("public override string " + FormatInheritanceTypeValue(type) + "ClassName { get { return null; } }\n");
			else
			{
				sb.AppendFront("public override string " + FormatInheritanceTypeValue(type)
						+ "ClassName { get { return \"de.unika.ipd.grGen.Model_"
						+ model.Ident + "." + GetPackagePrefixDot(type) + FormatInheritanceClassName(type) + "\"; } }\n");
			}

			if(type is NodeType)
			{
				sb.AppendFront("public override GRGEN_LIBGR.INode CreateNode()\n");
				sb.AppendFront("{\n");
				sb.Indent();
				if(type.IsAbstract())
					sb.AppendFront("throw new Exception(\"The abstract node type " + typeident + " cannot be instantiated!\");\n");
				else
					sb.AppendFront("return new " + allocName + "();\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
			else if(type is EdgeType)
			{
				EdgeType edgeType = (EdgeType)type;
				sb.AppendFront("public override GRGEN_LIBGR.Directedness Directedness " + "{ get { return GRGEN_LIBGR.Directedness.");
				switch(edgeType.Directedness)
				{
				case EdgeType.DirectednessKind.Arbitrary:
					sb.Append("Arbitrary; } }\n");
					break;
				case EdgeType.DirectednessKind.Directed:
					sb.Append("Directed; } }\n");
					break;
				case EdgeType.DirectednessKind.Undirected:
					sb.Append("Undirected; } }\n");
					break;
				default:
					throw new System.NotSupportedException("Illegal directedness of edge type \"" + FormatIdentifiable(type) + "\"");
				}
				sb.AppendFront("public override GRGEN_LIBGR.IEdge CreateEdge(" + "GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				if(type.IsAbstract())
				{
					sb.AppendFront("throw new Exception(\"The abstract edge type "
							+ typeident + " cannot be instantiated!\");\n");
				}
				else
					sb.AppendFront("return new " + allocName + "((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);\n");
				sb.Unindent();
				sb.AppendFront("}\n\n");
				sb.Append("\n");
				sb.AppendFront("public override void SetSourceAndTarget(" + "GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				if(type.IsAbstract())
				{
					sb.AppendFront("throw new Exception(\"The abstract edge type "
							+ typeident + " does not support source and target setting!\");\n");
				}
				else
					sb.AppendFront("((GRGEN_LGSP.LGSPEdge)edge).SetSourceAndTarget" + "((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
			else if(type is InternalTransientObjectType)
			{
				sb.AppendFront("public override GRGEN_LIBGR.ITransientObject CreateTransientObject()\n");
				sb.AppendFront("{\n");
				sb.Indent();
				if(type.IsAbstract())
				{
					sb.AppendFront("throw new Exception(\"The abstract transient object class type "
							+ typeident + " cannot be instantiated!\");\n");
				}
				else
					sb.AppendFront("return new " + allocName + "();\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
			else
			{
				sb.AppendFront("public override GRGEN_LIBGR.IObject CreateObject(GRGEN_LIBGR.IGraph graph, long uniqueId)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				if(type.IsAbstract())
				{
					sb.AppendFront("throw new Exception(\"The abstract object class type "
							+ typeident + " cannot be instantiated!\");\n");
				}
				else
				{
					sb.AppendFront("if(uniqueId != -1) {\n");
					sb.Indent();
					if(model.IsUniqueClassDefined())
					{
						sb.AppendFront(allocName + " newObject = new " + allocName + "(" + "graph.GlobalVariables.RequestObjectUniqueId(uniqueId)" + ");\n");
						sb.AppendFront("((GRGEN_LIBGR.BaseGraph)graph).ObjectCreated(newObject);\n");
						sb.AppendFront("return newObject;\n");
					}
					else
					{
						sb.AppendFront("throw new Exception(\"The model of the object class type "
								+ typeident + " does not support uniqueIds!\");\n");
					}
					sb.Unindent();
					sb.AppendFront("} else {\n");
					sb.Indent();
					sb.AppendFront(allocName + " newObject = new " + allocName + "("
							+ (model.IsUniqueClassDefined() ? "graph.GlobalVariables.FetchObjectUniqueId()" : "-1") + ");\n");
					sb.AppendFront("((GRGEN_LIBGR.BaseGraph)graph).ObjectCreated(newObject);\n");
					sb.AppendFront("return newObject;\n");
					sb.Unindent();
					sb.AppendFront("}\n");
				}
				sb.Unindent();
				sb.AppendFront("}\n");
			}

			sb.AppendFront("public override bool IsAbstract { get { return " + (type.IsAbstract() ? "true" : "false") + "; } }\n");
			sb.AppendFront("public override bool IsConst { get { return " + (type.IsConst() ? "true" : "false") + "; } }\n");

			sb.AppendFront("public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }\n");
			sb.AppendFront("public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();\n");

			sb.AppendFront("public override int NumAttributes { get { return " + type.AllMembers.Count + "; } }\n");
			GenAttributeTypesEnumerator(type);
			GenGetAttributeType(type);

			sb.AppendFront("public override int NumFunctionMethods { get { return " + type.AllFunctionMethods.Count + "; } }\n");
			GenFunctionMethodsEnumerator(type);
			GenGetFunctionMethod(type);

			sb.AppendFront("public override int NumProcedureMethods { get { return " + type.AllProcedureMethods.Count + "; } }\n");
			GenProcedureMethodsEnumerator(type);
			GenGetProcedureMethod(type);

			sb.AppendFront("public override bool IsA(GRGEN_LIBGR.GrGenType other)\n");
			sb.AppendFront("{\n");
			sb.AppendFrontIndented("return (this == other) || isA[other.TypeID];\n");
			sb.AppendFront("}\n");

			if(type is NodeType || type is EdgeType)
				GenCreateWithCopyCommons(type);
			sb.Unindent();
			sb.AppendFront("}\n");

			// generate function method info classes
			ICollection<FunctionMethod> allFunctionMethods = type.AllFunctionMethods;
			foreach(FunctionMethod fm in allFunctionMethods)
				GenFunctionMethodInfo(fm, type, packageName);

			// generate procedure method info classes
			ICollection<ProcedureMethod> allProcedureMethods = type.AllProcedureMethods;
			foreach(ProcedureMethod pm in allProcedureMethods)
				GenProcedureMethodInfo(pm, type, packageName);
		}

		private void GenIsA<T1>(ICollection<T1> types, InheritanceType type) where T1 : de.unika.ipd.grgen.ir.model.type.InheritanceType
		{
			sb.AppendFront("public static bool[] isA = new bool[] { ");
			foreach(InheritanceType nt in types)
			{
				if(type.IsCastableTo(nt))
					sb.Append("true, ");
				else
					sb.Append("false, ");
			}
			sb.Append("};\n");
			sb.AppendFront("public override bool IsA(int typeID) { return isA[typeID]; }\n");
		}

		private void GenIsMyType<T1>(ICollection<T1> types, InheritanceType type) where T1 : de.unika.ipd.grgen.ir.model.type.InheritanceType
		{
			sb.AppendFront("public static bool[] isMyType = new bool[] { ");
			foreach(InheritanceType nt in types)
			{
				if(nt.IsCastableTo(type))
					sb.Append("true, ");
				else
					sb.Append("false, ");
			}
			sb.Append("};\n");
			sb.AppendFront("public override bool IsMyType(int typeID) { return isMyType[typeID]; }\n");
		}

		private void GenAttributeAttributes(InheritanceType type)
		{
			foreach(Entity member in type.Members)
			{ // only for locally defined members
				sb.AppendFront("public static GRGEN_LIBGR.AttributeType " + FormatAttributeTypeName(member) + ";\n");

				// attribute types T/S of map<T,S>/set<T>/array<T>/deque<T>
				if(member.Type is MapType)
				{
					sb.AppendFront("public static GRGEN_LIBGR.AttributeType " + FormatAttributeTypeName(member) + "_map_domain_type;\n");
					sb.AppendFront("public static GRGEN_LIBGR.AttributeType " + FormatAttributeTypeName(member) + "_map_range_type;\n");
				}
				if(member.Type is SetType)
					sb.AppendFront("public static GRGEN_LIBGR.AttributeType " + FormatAttributeTypeName(member) + "_set_member_type;\n");
				if(member.Type is ArrayType)
					sb.AppendFront("public static GRGEN_LIBGR.AttributeType " + FormatAttributeTypeName(member) + "_array_member_type;\n");
				if(member.Type is DequeType)
					sb.AppendFront("public static GRGEN_LIBGR.AttributeType " + FormatAttributeTypeName(member) + "_deque_member_type;\n");
			}
		}

		private void GenAttributeInit(InheritanceType type)
		{
			foreach(Entity e in type.Members)
			{
				string attributeTypeName = FormatAttributeTypeName(e);
				Type t = e.Type;

				if(t is MapType)
				{
					MapType mt = (MapType)t;

					// attribute types T of map<T,S>
					sb.AppendFront(attributeTypeName + "_map_domain_type = new GRGEN_LIBGR.AttributeType(");
					sb.Append("\"" + FormatIdentifiable(e) + "_map_domain_type\", this, ");
					GenAttributeInitTypeDependentStuff(mt.KeyType, e);
					sb.Append(");\n");

					// attribute types S of map<T,S>
					sb.AppendFront(attributeTypeName + "_map_range_type = new GRGEN_LIBGR.AttributeType(");
					sb.Append("\"" + FormatIdentifiable(e) + "_map_range_type\", this, ");
					GenAttributeInitTypeDependentStuff(mt.ValueType, e);
					sb.Append(");\n");
				}
				else if(t is SetType)
				{
					SetType st = (SetType)t;

					// attribute type T of set<T>
					sb.AppendFront(attributeTypeName + "_set_member_type = new GRGEN_LIBGR.AttributeType(");
					sb.Append("\"" + FormatIdentifiable(e) + "_set_member_type\", this, ");
					GenAttributeInitTypeDependentStuff(st.ValueType, e);
					sb.Append(");\n");
				}
				else if(t is ArrayType)
				{
					ArrayType at = (ArrayType)t;

					// attribute type T of set<T>
					sb.AppendFront(attributeTypeName + "_array_member_type = new GRGEN_LIBGR.AttributeType(");
					sb.Append("\"" + FormatIdentifiable(e) + "_array_member_type\", this, ");
					GenAttributeInitTypeDependentStuff(at.ValueType, e);
					sb.Append(");\n");
				}
				else if(t is DequeType)
				{
					DequeType qt = (DequeType)t;

					// attribute type T of deque<T>
					sb.AppendFront(attributeTypeName + "_deque_member_type = new GRGEN_LIBGR.AttributeType(");
					sb.Append("\"" + FormatIdentifiable(e) + "_deque_member_type\", this, ");
					GenAttributeInitTypeDependentStuff(qt.ValueType, e);
					sb.Append(");\n");
				}

				sb.AppendFront(attributeTypeName + " = new GRGEN_LIBGR.AttributeType(");
				sb.Append("\"" + FormatIdentifiable(e) + "\", this, ");
				GenAttributeInitTypeDependentStuff(t, e);
				sb.Append(");\n");

				AddAnnotations(sb, e, attributeTypeName + ".annotations");
			}
		}

		private void GenAttributeInitTypeDependentStuff(Type t, Entity e)
		{
			if(t is EnumType)
			{
				sb.Append(GetAttributeKind(t)
						+ ", GRGEN_MODEL." + GetPackagePrefixDot(t) + "Enums.@" + FormatIdentifiable(t) + ", "
						+ "null, null, "
						+ "null, null, null, typeof(" + FormatAttributeType(t) + ")");
			}
			else if(t is MapType)
			{
				sb.Append(GetAttributeKind(t) + ", null, "
						+ FormatAttributeTypeName(e) + "_map_range_type" + ", "
						+ FormatAttributeTypeName(e) + "_map_domain_type" + ", "
						+ "null, null, null, typeof(" + FormatAttributeType(t) + ")");
			}
			else if(t is SetType)
			{
				sb.Append(GetAttributeKind(t) + ", null, "
						+ FormatAttributeTypeName(e) + "_set_member_type" + ", null, "
						+ "null, null, null, typeof(" + FormatAttributeType(t) + ")");
			}
			else if(t is ArrayType)
			{
				sb.Append(GetAttributeKind(t) + ", null, "
						+ FormatAttributeTypeName(e) + "_array_member_type" + ", null, "
						+ "null, null, null, typeof(" + FormatAttributeType(t) + ")");
			}
			else if(t is DequeType)
			{
				sb.Append(GetAttributeKind(t) + ", null, "
						+ FormatAttributeTypeName(e) + "_deque_member_type" + ", null, "
						+ "null, null, null, typeof(" + FormatAttributeType(t) + ")");
			}
			else if(t is NodeType || t is EdgeType)
			{
				sb.Append(GetAttributeKind(t) + ", null, "
						+ "null, null, "
						+ "\"" + FormatIdentifiable(t) + "\","
						+ (!string.ReferenceEquals(((ContainedInPackage)t).PackageContainedIn, null)
								? "\"" + ((ContainedInPackage)t).PackageContainedIn + "\""
								: "null")
						+ ","
						+ "\"" + GetPackagePrefixDoubleColon(t) + FormatIdentifiable(t) + "\","
						+ "typeof(" + FormatElementInterfaceRef(t) + ")");
			}
			else if(t is InternalObjectType || t is InternalTransientObjectType)
			{
				sb.Append(GetAttributeKind(t) + ", null, "
						+ "null, null, "
						+ "\"" + FormatIdentifiable(t) + "\","
						+ (!string.ReferenceEquals(((ContainedInPackage)t).PackageContainedIn, null)
								? "\"" + ((ContainedInPackage)t).PackageContainedIn + "\""
								: "null")
						+ ","
						+ "\"" + GetPackagePrefixDoubleColon(t) + FormatIdentifiable(t) + "\","
						+ "typeof(" + FormatElementInterfaceRef(t) + ")");
			}
			else
			{
				sb.Append(GetAttributeKind(t) + ", null, "
						+ "null, null, "
						+ "null, null, null, typeof(" + FormatAttributeType(t) + ")");
			}
		}

		private static string GetAttributeKind(Type t)
		{
			if(t is ByteType)
				return "GRGEN_LIBGR.AttributeKind.ByteAttr";
			else if(t is ShortType)
				return "GRGEN_LIBGR.AttributeKind.ShortAttr";
			else if(t is IntType)
				return "GRGEN_LIBGR.AttributeKind.IntegerAttr";
			else if(t is LongType)
				return "GRGEN_LIBGR.AttributeKind.LongAttr";
			else if(t is FloatType)
				return "GRGEN_LIBGR.AttributeKind.FloatAttr";
			else if(t is DoubleType)
				return "GRGEN_LIBGR.AttributeKind.DoubleAttr";
			else if(t is BooleanType)
				return "GRGEN_LIBGR.AttributeKind.BooleanAttr";
			else if(t is StringType)
				return "GRGEN_LIBGR.AttributeKind.StringAttr";
			else if(t is EnumType)
				return "GRGEN_LIBGR.AttributeKind.EnumAttr";
			else if(t is ObjectType || t is VoidType || t is ExternalObjectType)
				return "GRGEN_LIBGR.AttributeKind.ObjectAttr";
			else if(t is MapType)
				return "GRGEN_LIBGR.AttributeKind.MapAttr";
			else if(t is SetType)
				return "GRGEN_LIBGR.AttributeKind.SetAttr";
			else if(t is ArrayType)
				return "GRGEN_LIBGR.AttributeKind.ArrayAttr";
			else if(t is DequeType)
				return "GRGEN_LIBGR.AttributeKind.DequeAttr";
			else if(t is NodeType)
				return "GRGEN_LIBGR.AttributeKind.NodeAttr";
			else if(t is EdgeType)
				return "GRGEN_LIBGR.AttributeKind.EdgeAttr";
			else if(t is GraphType)
				return "GRGEN_LIBGR.AttributeKind.GraphAttr";
			else if(t is InternalObjectType)
				return "GRGEN_LIBGR.AttributeKind.InternalClassObjectAttr";
			else if(t is InternalTransientObjectType)
				return "GRGEN_LIBGR.AttributeKind.InternalClassTransientObjectAttr";
			else
				throw new ArgumentException("Unknown Type: " + t);
		}

		private void GenAttributeTypesEnumerator(InheritanceType type)
		{
			ICollection<Entity> allMembers = type.AllMembers;
			sb.AppendFront("public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes");

			if(allMembers.Count == 0)
				sb.Append(" { get { yield break; } }\n");
			else
			{
				sb.Append("\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("get\n");
				sb.AppendFront("{\n");
				sb.Indent();
				foreach(Entity e in allMembers)
				{
					Type ownerType = e.Owner;
					if(ownerType == type)
						sb.AppendFront("yield return " + FormatAttributeTypeName(e) + ";\n");
					else
						sb.AppendFront("yield return " + FormatTypeClassRef(ownerType) + "." + FormatAttributeTypeName(e) + ";\n");
				}
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		private void GenGetAttributeType(InheritanceType type)
		{
			ICollection<Entity> allMembers = type.AllMembers;
			sb.AppendFront("public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)");

			if(allMembers.Count == 0)
				sb.Append(" { return null; }\n");
			else
			{
				sb.Append("\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("switch(name)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				foreach(Entity e in allMembers)
				{
					Type ownerType = e.Owner;
					if(ownerType == type)
					{
						sb.AppendFront("case \"" + FormatIdentifiable(e) + "\" : return " +
								FormatAttributeTypeName(e) + ";\n");
					}
					else
					{
						sb.AppendFront("case \"" + FormatIdentifiable(e) + "\" : return " +
								FormatTypeClassRef(ownerType) + "." + FormatAttributeTypeName(e) + ";\n");
					}
				}
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.AppendFront("return null;\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		private void GenFunctionMethodsEnumerator(InheritanceType type)
		{
			ICollection<FunctionMethod> allFunctionMethods = type.AllFunctionMethods;
			sb.AppendFront("public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods");

			if(allFunctionMethods.Count == 0)
				sb.Append(" { get { yield break; } }\n");
			else
			{
				sb.Append("\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("get\n");
				sb.AppendFront("{\n");
				sb.Indent();
				foreach(FunctionMethod fm in allFunctionMethods)
					sb.AppendFront("yield return " + FormatFunctionMethodInfoName(fm, type) + ".Instance;\n");
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		private void GenGetFunctionMethod(InheritanceType type)
		{
			ICollection<FunctionMethod> allFunctionMethods = type.AllFunctionMethods;
			sb.AppendFront("public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name)");

			if(allFunctionMethods.Count == 0)
				sb.Append(" { return null; }\n");
			else
			{
				sb.Append("\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("switch(name)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				foreach(FunctionMethod fm in allFunctionMethods)
					sb.AppendFront("case \"" + FormatIdentifiable(fm) + "\" : return " + FormatFunctionMethodInfoName(fm, type) + ".Instance;\n");
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.AppendFront("return null;\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		private void GenProcedureMethodsEnumerator(InheritanceType type)
		{
			ICollection<ProcedureMethod> allProcedureMethods = type.AllProcedureMethods;
			sb.AppendFront("public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods");

			if(allProcedureMethods.Count == 0)
				sb.Append(" { get { yield break; } }\n");
			else
			{
				sb.Append("\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("get\n");
				sb.AppendFront("{\n");
				sb.Indent();
				foreach(ProcedureMethod pm in allProcedureMethods)
					sb.AppendFront("yield return " + FormatProcedureMethodInfoName(pm, type) + ".Instance;\n");
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		private void GenGetProcedureMethod(InheritanceType type)
		{
			ICollection<ProcedureMethod> allProcedureMethods = type.AllProcedureMethods;
			sb.AppendFront("public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name)");

			if(allProcedureMethods.Count == 0)
				sb.Append(" { return null; }\n");
			else
			{
				sb.Append("\n");
				sb.AppendFront("{\n");
				sb.Indent();
				sb.AppendFront("switch(name)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				foreach(ProcedureMethod pm in allProcedureMethods)
					sb.AppendFront("case \"" + FormatIdentifiable(pm) + "\" : return " + FormatProcedureMethodInfoName(pm, type) + ".Instance;\n");
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.AppendFront("return null;\n");
				sb.Unindent();
				sb.Append("}\n");
			}
		}

		private void GetFirstCommonAncestors(InheritanceType curType,
				InheritanceType type, ISet<InheritanceType> resTypes)
		{
			if(type.IsCastableTo(curType))
				resTypes.Add(curType);
			else
			{
				foreach(InheritanceType superType in curType.DirectSuperTypes)
					GetFirstCommonAncestors(superType, type, resTypes);
			}
		}

		private void GenCreateWithCopyCommons(InheritanceType type)
		{
			string elemref = FormatInheritanceClassRef(type);
			string extName = type.ExternalName;
			string allocName = !string.ReferenceEquals(extName, null) ? "global::" + extName : elemref;
			string kindName = GetKindName(type);

			if(type is NodeType)
			{
				sb.AppendFront("public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons("
						+ "GRGEN_LIBGR.INode oldINode)\n");
				sb.AppendFront("{\n");
				sb.Indent();
			}
			else
			{
				sb.AppendFront("public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons("
						+ "GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, "
						+ "GRGEN_LIBGR.IEdge oldIEdge)\n");
				sb.AppendFront("{\n");
				sb.Indent();
			}

			if(type.IsAbstract())
			{
				sb.AppendFront("throw new Exception(\"Cannot retype to the abstract type " + FormatIdentifiable(type) + "!\");\n");
				sb.Unindent();
				sb.AppendFront("}\n");
				return;
			}

			IDictionary<BitArray, IList<InheritanceType>> commonGroups = GetCommonGroups(type);

			if(commonGroups.Count != 0)
			{
				if(type is NodeType)
				{
					sb.AppendFront("GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;\n");
					sb.AppendFront(elemref + " newNode = new " + allocName + "();\n");
				}
				else
				{
					sb.AppendFront("GRGEN_LGSP.LGSPEdge oldEdge = (GRGEN_LGSP.LGSPEdge) oldIEdge;\n");
					sb.AppendFront(elemref + " newEdge = new " + allocName
							+ "((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);\n");
				}
				sb.AppendFront("switch(old" + kindName + ".Type.TypeID)\n");
				sb.AppendFront("{\n");
				sb.Indent();
				foreach(KeyValuePair<BitArray, IList<InheritanceType>> entry in commonGroups.SetOfKeyValuePairs())
					EmitCommonGroup(type, kindName, entry);
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.AppendFront("return new" + kindName + ";\n");
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.Append("\n");
			}
			else
			{
				if(type is NodeType)
					sb.AppendFront("return new " + allocName + "();\n");
				else {
					sb.AppendFront("return new " + allocName
							+ "((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);\n");
				}
				sb.Unindent();
				sb.AppendFront("}\n");
				sb.Append("\n");
			}
		}

		private IDictionary<BitArray, IList<InheritanceType>> GetCommonGroups(InheritanceType type)
		{
			bool isNode = type is NodeType;

			IDictionary<BitArray, IList<InheritanceType>> commonGroups = new LinkedHashMap<BitArray, IList<InheritanceType>>();

			ICollection<InheritanceType> types = isNode
					? GetInheritanceTypes(model.AllNodeTypes)
					: GetInheritanceTypes(model.AllEdgeTypes);
			foreach(InheritanceType itype in types)
			{
				if(itype.IsAbstract())
					continue;

				ISet<InheritanceType> firstCommonAncestors = new LinkedHashSet<InheritanceType>();
				GetFirstCommonAncestors(itype, type, firstCommonAncestors);

				SortedSet<InheritanceType> sortedCommonTypes = new SortedSet<InheritanceType>(new ComparatorAnonymousInnerClass(this));

				sortedCommonTypes.AddAll(firstCommonAncestors);
				foreach(InheritanceType commonType in sortedCommonTypes)
				{
					if(!firstCommonAncestors.Contains(commonType))
						continue;
					foreach(InheritanceType superType in commonType.AllSuperTypes)
						firstCommonAncestors.Remove(superType);
				}

				bool mustCopyAttribs = false;
				foreach(InheritanceType commonType in firstCommonAncestors)
				{
					foreach(Entity member in commonType.AllMembers)
					{
						if(member.Type.IsVoid()) // is it an abstract member?
							continue;
						mustCopyAttribs = true;
						goto commonLoopBreak;
					}
		commonLoopContinue:;
				}
	commonLoopBreak:

				if(!mustCopyAttribs)
					continue;

				BitArray commonTypesBitset = new BitArray();
				foreach(InheritanceType commonType in firstCommonAncestors)
					commonTypesBitset.Set(commonType.TypeID, true);
				IList<InheritanceType> commonList = commonGroups[commonTypesBitset];
				if(commonList == null)
				{
					commonList = new List<InheritanceType>();
					commonGroups[commonTypesBitset] = commonList;
				}
				commonList.Add(itype);
			}
			return commonGroups;
		}

		private class ComparatorAnonymousInnerClass : IComparer<InheritanceType>
		{
			private readonly ModelGen outerInstance;

			public ComparatorAnonymousInnerClass(ModelGen outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public int Compare(InheritanceType o1, InheritanceType o2)
			{
				return o2.MaxDist - o1.MaxDist;
			}
		}

		private void EmitCommonGroup(InheritanceType type, string kindName,
				KeyValuePair<BitArray, IList<InheritanceType>> entry)
		{
			foreach(InheritanceType itype in entry.Value)
			{
				sb.AppendFront("case (int) GRGEN_MODEL." + GetPackagePrefixDot(itype) + kindName + "Types.@"
						+ FormatIdentifiable(itype) + ":\n");
			}
			sb.Indent();
			BitArray bitset = entry.Key;
			HashSet<Entity> copiedAttribs = new HashSet<Entity>();
			for(int i = bitset.NextSetBit(0); i >= 0; i = bitset.NextSetBit(i + 1))
			{
				InheritanceType commonType = InheritanceType.GetByTypeID(i);
				ICollection<Entity> members = commonType.AllMembers;
				if(members.Count != 0)
				{
					sb.AppendFront("// copy attributes for: "
							+ FormatIdentifiable(commonType) + "\n");
					bool alreadyCasted = false;
					foreach(Entity member in members)
					{
						if(member.IsConst())
						{
							sb.AppendFrontIndented("// is const: " + FormatIdentifiable(member) + "\n");
							continue;
						}
						if(member.Type.IsVoid())
						{
							sb.AppendFrontIndented("// is abstract: " + FormatIdentifiable(member) + "\n");
							continue;
						}
						if(copiedAttribs.Contains(member))
						{
							sb.AppendFrontIndented("// already copied: " + FormatIdentifiable(member) + "\n");
							continue;
						}
						if(!alreadyCasted)
						{
							alreadyCasted = true;
							sb.AppendFront("{\n");
							sb.Indent();
							sb.AppendFront(FormatVarDeclWithCast(FormatElementInterfaceRef(commonType), "old") + "old" + kindName + ";\n");
						}
						copiedAttribs.Add(member);
						string memberName = FormatIdentifiable(member);
						// what's that?
						if(type.GetOverriddenMember(member) != null)
						{
							// Workaround for Mono Bug 357287
							// "Access to hiding properties of interfaces resolves wrong member"
							// https://bugzilla.novell.com/show_bug.cgi?id=357287
							sb.AppendFront("new" + kindName + ".@" + memberName
									+ " = (" + FormatAttributeType(member) + ") old.@" + memberName
									+ ";   // Mono workaround (bug #357287)\n");
						}
						else
						{
							if(member.Type is MapType || member.Type is SetType
									|| member.Type is ArrayType || member.Type is DequeType)
							{
								sb.AppendFront("new" + kindName + ".@" + memberName
										+ " = new " + FormatAttributeType(member.Type)
										+ "(old.@" + memberName + ");\n");
							}
							else
							{
								sb.AppendFront("new" + kindName + ".@" + memberName
										+ " = old.@" + memberName + ";\n");
							}
						}
					}
					if(alreadyCasted)
					{
						sb.Unindent();
						sb.AppendFront("}\n");
					}
				}
			}
			sb.AppendFront("break;\n");
			sb.Unindent();
		}

		/// <summary>
		/// Generates the function info for the given function method
		/// </summary>
		private void GenFunctionMethodInfo(FunctionMethod fm, InheritanceType type, string packageName)
		{
			string functionMethodName = FormatIdentifiable(fm);
			string className = FormatFunctionMethodInfoName(fm, type);

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
			sb.AppendFront("\"" + functionMethodName + "\",\n");
			sb.AppendFront((!string.ReferenceEquals(packageName, null) ? "\"" + packageName + "\"" : "null") + ", ");
			sb.Append("\"" + (!string.ReferenceEquals(packageName, null)
					? packageName + "::" + functionMethodName
					: functionMethodName) + "\",\n");
			sb.AppendFront("false,\n");
			sb.AppendFront("new String[] { ");
			foreach(Entity inParam in fm.Parameters)
				sb.Append("\"" + inParam.Ident + "\", ");
			sb.Append(" },\n");
			sb.AppendFront("new GRGEN_LIBGR.GrGenType[] { ");
			foreach(Entity inParam in fm.Parameters)
			{
				if(inParam.Type is InheritanceType && !(inParam.Type is ExternalObjectType))
					sb.Append(FormatTypeClassRef(inParam.Type) + ".typeVar, ");
				else
					sb.Append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + FormatAttributeType(inParam.Type) + ")), ");
			}
			sb.Append(" },\n");
			Type outType = fm.ReturnType;
			if(outType is InheritanceType && !(outType is ExternalObjectType))
				sb.AppendFront(FormatTypeClassRef(outType) + ".typeVar\n");
			else
				sb.AppendFront("GRGEN_LIBGR.VarType.GetVarType(typeof(" + FormatAttributeType(outType) + "))\n");
			sb.Unindent();
			sb.AppendFront(")\n");
			sb.Unindent();
			sb.AppendFront("{\n");
			sb.AppendFront("}\n");

			sb.AppendFront("public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, "
					+ "object[] arguments)\n");
			sb.AppendFront("{\n");
			sb.AppendFrontIndented("throw new Exception(\"Not implemented, can't call function method without this object!\");\n");
			sb.AppendFront("}\n");

			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Append("\n");
		}

		/// <summary>
		/// Generates the procedure info for the given procedure method
		/// </summary>
		private void GenProcedureMethodInfo(ProcedureMethod pm, InheritanceType type, string packageName)
		{
			string procedureMethodName = FormatIdentifiable(pm);
			string className = FormatProcedureMethodInfoName(pm, type);

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
			sb.AppendFront("\"" + procedureMethodName + "\",\n");
			sb.AppendFront((!string.ReferenceEquals(packageName, null) ? "\"" + packageName + "\"" : "null") + ", ");
			sb.Append("\"" + (!string.ReferenceEquals(packageName, null)
					? packageName + "::" + procedureMethodName
					: procedureMethodName) + "\",\n");
			sb.AppendFront("false,\n");
			sb.AppendFront("new String[] { ");
			foreach(Entity inParam in pm.Parameters)
				sb.Append("\"" + inParam.Ident + "\", ");
			sb.Append(" },\n");
			sb.AppendFront("new GRGEN_LIBGR.GrGenType[] { ");
			foreach(Entity inParam in pm.Parameters)
			{
				if(inParam.Type is InheritanceType && !(inParam.Type is ExternalObjectType))
					sb.Append(FormatTypeClassRef(inParam.Type) + ".typeVar, ");
				else
					sb.Append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + FormatAttributeType(inParam.Type) + ")), ");
			}
			sb.Append(" },\n");
			sb.AppendFront("new GRGEN_LIBGR.GrGenType[] { ");
			foreach(Type outType in pm.ReturnTypes)
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

			sb.AppendFront("public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph,"
					+ " object[] arguments)\n");
			sb.AppendFront("{\n");
			sb.AppendFrontIndented("throw new Exception(\"Not implemented, can't call procedure method without this object!\");\n");
			sb.AppendFront("}\n");

			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Append("\n");
		}

		internal virtual void GenAttributeArrayHelpersAndComparers(InheritanceType type)
		{
			foreach(Entity entity in type.AllMembers)
			{
				if(HasArrayHelpers(entity))
					GenAttributeArrayHelpersAndComparers(type, entity);
			}
		}

		private bool HasArrayHelpers(Entity entity)
		{
			if(entity.Type.IsFilterableType()
					|| entity.Type.Classify() == Type.TypeClass.IS_EXTERNAL_CLASS_OBJECT
					|| entity.Type.Classify() == Type.TypeClass.IS_OBJECT)
			{
				if((entity.Type.Classify() == Type.TypeClass.IS_EXTERNAL_CLASS_OBJECT
						|| entity.Type.Classify() == Type.TypeClass.IS_OBJECT)
						&& !(model.IsEqualClassDefined() && model.IsLowerClassDefined()))
					return false;
				if(entity.IsConst())
					return false;
				return true;
			}
			else
				return false;
		}

		/// <summary>
		/// Generates the orderAscendingBy/orderDescendingBy/groupBy/keepOneForEach member array functions
		/// plus the Comparer classes (shared with the corresponding orderAscendingBy/orderDescendingBy filters)
		/// and further array helper functions
		/// </summary>
		internal virtual void GenAttributeArrayHelpersAndComparers(InheritanceType type, Entity entity)
		{
			string typeName = FormatElementInterfaceRef(type);
			string attributeName = FormatIdentifiable(entity);
			string attributeTypeName = FormatAttributeType(entity.Type);
			string arrayHelperClassName = "ArrayHelper_" + type.Ident.ToString() + "_" + attributeName;
			string comparerClassName = "Comparer_" + type.Ident.ToString() + "_" + attributeName;
			string reverseComparerClassName = "ReverseComparer_" + type.Ident.ToString() + "_" + attributeName;

			InheritanceType nonAbstractTypeOrSubtype = GetNonAbstractTypeOrSubtype(type);
			if(nonAbstractTypeOrSubtype == null)
				return; // can't generate comparer for abstract types that have no concrete subtype

			if(entity.Type.IsOrderableType())
			{
				GenAttributeArrayComparer(type, entity);
				GenAttributeArrayReverseComparer(type, entity);
			}

			sb.Append("\n");
			sb.AppendFront("public class " + arrayHelperClassName + "\n");
			sb.AppendFront("{\n");
			sb.Indent();

			GenInstanceBearingAttributeForSearch(sb, type, nonAbstractTypeOrSubtype);

			GenIndexOfByMethod(typeName, attributeName, attributeTypeName);
			GenIndexOfByWithStartMethod(typeName, attributeName, attributeTypeName);

			GenLastIndexOfByMethod(typeName, attributeName, attributeTypeName);
			GenLastIndexOfByWithStartMethod(typeName, attributeName, attributeTypeName);

			if(entity.Type.IsOrderableType())
			{
				GenIndexOfOrderedByMethod(typeName, attributeName, attributeTypeName, comparerClassName);

				GenArrayOrderAscendingByMethod(typeName, comparerClassName);
				GenArrayOrderDescendingByMethod(typeName, reverseComparerClassName);
			}

			GenerateArrayGroupBy(sb, "ArrayGroupBy", typeName, attributeName, attributeTypeName);
			GenerateArrayKeepOneForEach(sb, "ArrayKeepOneForEachBy", typeName, attributeName, attributeTypeName);

			GenArrayExtractMethod(typeName, attributeName, attributeTypeName);

			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Append("\n");
		}

		internal virtual void GenInstanceBearingAttributeForSearch(SourceBuilder sb, InheritanceType type, InheritanceType nonAbstractTypeOrSubtype)
		{
			if(type is EdgeType)
			{
				sb.AppendFront("private static " + FormatElementInterfaceRef(type) + " instanceBearingAttributeForSearch = "
						+ "new " + FormatInheritanceClassRef(nonAbstractTypeOrSubtype) + "(null, null);\n");
			}
			else if(type is InternalObjectType)
			{
				sb.AppendFront("private static " + FormatElementInterfaceRef(type) + " instanceBearingAttributeForSearch = "
						+ "new " + FormatInheritanceClassRef(nonAbstractTypeOrSubtype) + "(-1);\n");
			}
			else
			{
				sb.AppendFront("private static " + FormatElementInterfaceRef(type) + " instanceBearingAttributeForSearch = "
						+ "new " + FormatInheritanceClassRef(nonAbstractTypeOrSubtype) + "();\n");
			}
		}

		private static InheritanceType GetNonAbstractTypeOrSubtype(InheritanceType type)
		{
			InheritanceType nonAbstractTypeOrSubtype = null;
			if(!type.IsAbstract() && string.ReferenceEquals(type.ExternalName, null))
				nonAbstractTypeOrSubtype = type;
			else
			{
				foreach(InheritanceType subtype in type.AllSubTypes)
				{
					if(!subtype.IsAbstract() && string.ReferenceEquals(type.ExternalName, null))
					{
						nonAbstractTypeOrSubtype = subtype;
						break;
					}
				}
			}
			return nonAbstractTypeOrSubtype;
		}

		internal virtual void GenIndexOfByMethod(string typeName, string attributeName, string attributeTypeName)
		{
			sb.AppendFront("public static int ArrayIndexOfBy(IList<" + typeName + "> list, " + attributeTypeName + " entry)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("for(int i = 0; i < list.Count; ++i)\n");
			sb.Indent();
			sb.AppendFront("if(list[i].@" + attributeName + ".Equals(entry))\n");
			sb.AppendFrontIndented("return i;\n");
			sb.Unindent();
			sb.AppendFront("return -1;\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		internal virtual void GenIndexOfByWithStartMethod(string typeName, string attributeName, string attributeTypeName)
		{
			sb.AppendFront("public static int ArrayIndexOfBy(IList<" + typeName + "> list, "
					+ attributeTypeName + " entry, int startIndex)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("for(int i = startIndex; i < list.Count; ++i)\n");
			sb.Indent();
			sb.AppendFront("if(list[i].@" + attributeName + ".Equals(entry))\n");
			sb.AppendFrontIndented("return i;\n");
			sb.Unindent();
			sb.AppendFront("return -1;\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		internal virtual void GenLastIndexOfByMethod(string typeName, string attributeName, string attributeTypeName)
		{
			sb.AppendFront("public static int ArrayLastIndexOfBy(IList<" + typeName + "> list, "
					+ attributeTypeName + " entry)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("for(int i = list.Count - 1; i >= 0; --i)\n");
			sb.Indent();
			sb.AppendFront("if(list[i].@" + attributeName + ".Equals(entry))\n");
			sb.AppendFrontIndented("return i;\n");
			sb.Unindent();
			sb.AppendFront("return -1;\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		internal virtual void GenLastIndexOfByWithStartMethod(string typeName, string attributeName, string attributeTypeName)
		{
			sb.AppendFront("public static int ArrayLastIndexOfBy(IList<" + typeName + "> list, "
					+ attributeTypeName + " entry, int startIndex)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("for(int i = startIndex; i >= 0; --i)\n");
			sb.Indent();
			sb.AppendFront("if(list[i].@" + attributeName + ".Equals(entry))\n");
			sb.AppendFrontIndented("return i;\n");
			sb.Unindent();
			sb.AppendFront("return -1;\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		internal virtual void GenIndexOfOrderedByMethod(string typeName, string attributeName, string attributeTypeName,
				string comparerClassName)
		{
			sb.AppendFront("public static int ArrayIndexOfOrderedBy(List<" + typeName + "> list, "
					+ attributeTypeName + " entry)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("instanceBearingAttributeForSearch.@" + attributeName + " = entry;\n");
			sb.AppendFront("return list.BinarySearch(instanceBearingAttributeForSearch, " + comparerClassName + ".thisComparer);\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		internal virtual void GenArrayOrderAscendingByMethod(string typeName, string comparerClassName)
		{
			sb.AppendFront("public static List<" + typeName + "> ArrayOrderAscendingBy(List<" + typeName + "> list)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("List<" + typeName + "> newList = new List<" + typeName + ">(list);\n");
			sb.AppendFront("newList.Sort(" + comparerClassName + ".thisComparer);\n");
			sb.AppendFront("return newList;\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		internal virtual void GenArrayOrderDescendingByMethod(string typeName, string reverseComparerClassName)
		{
			sb.AppendFront("public static List<" + typeName + "> ArrayOrderDescendingBy(List<" + typeName + "> list)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("List<" + typeName + "> newList = new List<" + typeName + ">(list);\n");
			sb.AppendFront("newList.Sort(" + reverseComparerClassName + ".thisComparer);\n");
			sb.AppendFront("return newList;\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		internal virtual void GenArrayExtractMethod(string typeName, string attributeName, string attributeTypeName)
		{
			sb.AppendFront("public static List<" + attributeTypeName + "> Extract(List<" + typeName + "> list)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("List<" + attributeTypeName + "> resultList = new List<"
					+ attributeTypeName + ">(list.Count);\n");
			sb.AppendFront("foreach(" + typeName + " entry in list)\n");
			sb.AppendFrontIndented("resultList.Add(entry.@" + attributeName + ");\n");
			sb.AppendFront("return resultList;\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		internal virtual void GenAttributeArrayComparer(InheritanceType type, Entity entity)
		{
			string typeName = FormatElementInterfaceRef(type);
			string attributeName = FormatIdentifiable(entity);
			string comparerClassName = "Comparer_" + type.Ident.ToString() + "_" + attributeName;

			sb.Append("\n");
			sb.AppendFront("public class " + comparerClassName + " : Comparer<" + typeName + ">\n");
			sb.AppendFront("{\n");
			sb.Indent();

			sb.AppendFront("public static " + comparerClassName + " thisComparer = "
					+ "new " + comparerClassName + "();\n");

			GenCompareMethod(sb, typeName, FormatIdentifiable(entity), entity.Type, true);

			sb.Unindent();
			sb.AppendFront("}\n");
		}

		internal virtual void GenAttributeArrayReverseComparer(InheritanceType type, Entity entity)
		{
			string typeName = FormatElementInterfaceRef(type);
			string attributeName = FormatIdentifiable(entity);
			string reverseComparerClassName = "ReverseComparer_" + type.Ident.ToString() + "_" + attributeName;

			sb.Append("\n");
			sb.AppendFront("public class " + reverseComparerClassName + " : Comparer<" + typeName + ">\n");
			sb.AppendFront("{\n");
			sb.Indent();

			sb.AppendFront("public static " + reverseComparerClassName + " thisComparer = "
					+ "new " + reverseComparerClassName + "();\n");

			GenCompareMethod(sb, typeName, FormatIdentifiable(entity), entity.Type, false);

			sb.Unindent();
			sb.AppendFront("}\n");
		}

		////////////////////////////
		// Model class generation //
		////////////////////////////

		/// <summary>
		/// Generates the model class for the node or edge or class object or transient class object types.
		/// </summary>
		private void GenModelClass<T1>(ICollection<T1> types, InheritanceTypeType typeType) where T1 : de.unika.ipd.grgen.ir.model.type.InheritanceType
		{
			string kindStr = typeType.ToString();

			sb.AppendFront("//\n");
			sb.AppendFront("// " + kindStr + " model\n");
			sb.AppendFront("//\n");
			sb.Append("\n");
			sb.AppendFront("public sealed class " + model.Ident + kindStr
					+ "Model : GRGEN_LIBGR.I" + kindStr + "Model\n");
			sb.AppendFront("{\n");
			sb.Indent();

			InheritanceType rootType = GenModelConstructor(typeType, types);

			if(typeType == InheritanceTypeType.Node)
				sb.AppendFront("public bool IsNodeModel { get { return true; } }\n");
			else if(typeType == InheritanceTypeType.Edge)
				sb.AppendFront("public bool IsNodeModel { get { return false; } }\n");
			else if(typeType == InheritanceTypeType.Object)
				sb.AppendFront("public bool IsTransientModel { get { return false; } }\n");
			else
				sb.AppendFront("public bool IsTransientModel { get { return true; } }\n");

			sb.AppendFront("public GRGEN_LIBGR." + kindStr + "Type RootType { get { return "
					+ FormatTypeClassRef(rootType) + ".typeVar; } }\n");
			if(typeType == InheritanceTypeType.Node || typeType == InheritanceTypeType.Edge)
			{
				sb.AppendFront("GRGEN_LIBGR.GraphElementType GRGEN_LIBGR.IGraphElementTypeModel.RootType { get { return "
						+ FormatTypeClassRef(rootType) + ".typeVar; } }\n");
			}
			else
			{
				sb.AppendFront("GRGEN_LIBGR.BaseObjectType GRGEN_LIBGR.IBaseObjectTypeModel.RootType { get { return "
						+ FormatTypeClassRef(rootType) + ".typeVar; } }\n");
			}
			sb.AppendFront("GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.RootType { get { return "
					+ FormatTypeClassRef(rootType) + ".typeVar; } }\n");

			sb.AppendFront("public GRGEN_LIBGR." + kindStr + "Type GetType(string name)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("switch(name)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			foreach(InheritanceType type in types)
			{
				sb.AppendFront("case \"" + GetPackagePrefixDoubleColon(type) + FormatIdentifiable(type) + "\" : "
						+ "return " + FormatTypeClassRef(type) + ".typeVar;\n");
			}
			sb.Unindent();
			sb.AppendFront("}\n");
			sb.AppendFront("return null;\n");
			sb.Unindent();
			sb.AppendFront("}\n");

			if(typeType == InheritanceTypeType.Node || typeType == InheritanceTypeType.Edge)
			{
				sb.AppendFront("GRGEN_LIBGR.GraphElementType GRGEN_LIBGR.IGraphElementTypeModel.GetType(string name)\n");
				sb.AppendFront("{\n");
				sb.AppendFrontIndented("return GetType(name);\n");
				sb.AppendFront("}\n");
			}
			else
			{
				sb.AppendFront("GRGEN_LIBGR.BaseObjectType GRGEN_LIBGR.IBaseObjectTypeModel.GetType(string name)\n");
				sb.AppendFront("{\n");
				sb.AppendFrontIndented("return GetType(name);\n");
				sb.AppendFront("}\n");
			}

			sb.AppendFront("GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.GetType(string name)\n");
			sb.AppendFront("{\n");
			sb.AppendFrontIndented("return GetType(name);\n");
			sb.AppendFront("}\n");

			sb.AppendFront("private GRGEN_LIBGR." + kindStr + "Type[] types = {\n");
			sb.Indent();
			foreach(InheritanceType type in types)
				sb.AppendFront(FormatTypeClassRef(type) + ".typeVar,\n");
			sb.Unindent();
			sb.AppendFront("};\n");

			sb.AppendFront("public GRGEN_LIBGR." + kindStr + "Type[] Types { get { return types; } }\n");
			if(typeType == InheritanceTypeType.Node || typeType == InheritanceTypeType.Edge)
				sb.AppendFront("GRGEN_LIBGR.GraphElementType[] GRGEN_LIBGR.IGraphElementTypeModel.Types " + "{ get { return types; } }\n");
			else
				sb.AppendFront("GRGEN_LIBGR.BaseObjectType[] GRGEN_LIBGR.IBaseObjectTypeModel.Types " + "{ get { return types; } }\n");
			sb.AppendFront("GRGEN_LIBGR.InheritanceType[] GRGEN_LIBGR.ITypeModel.Types " + "{ get { return types; } }\n");

			sb.AppendFront("private global::System.Type[] typeTypes = {\n");
			sb.Indent();
			foreach(InheritanceType type in types)
				sb.AppendFront("typeof(" + FormatTypeClassRef(type) + "),\n");
			sb.Unindent();
			sb.AppendFront("};\n");
			sb.AppendFront("public global::System.Type[] TypeTypes { get { return typeTypes; } }\n");

			sb.AppendFront("private GRGEN_LIBGR.AttributeType[] attributeTypes = {\n");
			sb.Indent();
			foreach(InheritanceType type in types)
			{
				string ctype = FormatTypeClassRef(type);
				foreach(Entity member in type.Members)
					sb.AppendFront(ctype + "." + FormatAttributeTypeName(member) + ",\n");
			}
			sb.Unindent();
			sb.AppendFront("};\n");
			sb.AppendFront("public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes " + "{ get { return attributeTypes; } }\n");

			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private InheritanceType GenModelConstructor<T1>(InheritanceTypeType typeType, ICollection<T1> types) where T1 : de.unika.ipd.grgen.ir.model.type.InheritanceType
		{
			string kindStr = typeType.ToString();
			InheritanceType rootType = null;

			sb.AppendFront("public " + model.Ident + kindStr + "Model()\n");
			sb.AppendFront("{\n");
			sb.Indent();
			foreach(InheritanceType type in types)
			{
				string ctype = FormatTypeClassRef(type);
				sb.AppendFront(ctype + ".typeVar.subOrSameGrGenTypes = "
						+ ctype + ".typeVar.subOrSameTypes = new GRGEN_LIBGR."
						+ kindStr + "Type[] {\n");
				sb.Indent();
				sb.AppendFront(ctype + ".typeVar,\n");
				foreach(InheritanceType otherType in types)
				{
					if(type != otherType && otherType.IsCastableTo(type))
						sb.AppendFront(FormatTypeClassRef(otherType) + ".typeVar,\n");
				}
				sb.Unindent();
				sb.AppendFront("};\n");

				sb.AppendFront(ctype + ".typeVar.directSubGrGenTypes = "
						+ ctype + ".typeVar.directSubTypes = new GRGEN_LIBGR."
						+ kindStr + "Type[] {\n");
				sb.Indent();
				foreach(InheritanceType subType in type.DirectSubTypes)
				{
					// TODO: HACK, because direct sub types may also contain types from other models...
					if(!types.Contains(subType))
						continue;
					sb.AppendFront(FormatTypeClassRef(subType) + ".typeVar,\n");
				}
				sb.Unindent();
				sb.AppendFront("};\n");

				sb.AppendFront(ctype + ".typeVar.superOrSameGrGenTypes = "
						+ ctype + ".typeVar.superOrSameTypes = new GRGEN_LIBGR."
						+ kindStr + "Type[] {\n");
				sb.Indent();
				sb.AppendFront(ctype + ".typeVar,\n");
				foreach(InheritanceType otherType in types)
				{
					if(type != otherType && type.IsCastableTo(otherType))
						sb.AppendFront(FormatTypeClassRef(otherType) + ".typeVar,\n");
				}
				sb.Unindent();
				sb.AppendFront("};\n");

				sb.AppendFront(ctype + ".typeVar.directSuperGrGenTypes = "
						+ ctype + ".typeVar.directSuperTypes = new GRGEN_LIBGR."
						+ kindStr + "Type[] {\n");
				sb.Indent();
				foreach(InheritanceType superType in type.DirectSuperTypes)
					sb.AppendFront(FormatTypeClassRef(superType) + ".typeVar,\n");
				sb.Unindent();
				sb.AppendFront("};\n");

				if(type.IsRoot())
					rootType = type;
			}
			sb.Unindent();
			sb.AppendFront("}\n");

			return rootType;
		}

		/// <summary>
		/// Generates the graph model class.
		/// </summary>
		private void GenGraphModel()
		{
			string modelName = model.Ident.ToString();
			sb.AppendFront("//\n");
			sb.AppendFront("// IGraphModel (LGSPGraphModel) implementation\n");
			sb.AppendFront("//\n");

			sb.AppendFront("public sealed class " + modelName + "GraphModel : GRGEN_LGSP.LGSPGraphModel\n");
			sb.AppendFront("{\n");
			sb.Indent();

			sb.AppendFront("public " + modelName + "GraphModel()\n");
			sb.AppendFront("{\n");
			sb.AppendFrontIndented("FullyInitializeExternalObjectTypes();\n");
			sb.AppendFront("}\n");
			sb.Append("\n");

			GenGraphModelBody(modelName);

			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenGraphClass()
		{
			string modelName = model.Ident.ToString();

			sb.AppendFront("//\n");
			sb.AppendFront("// IGraph (LGSPGraph) implementation\n");
			sb.AppendFront("//\n");

			sb.AppendFront("public class " + modelName + "Graph : GRGEN_LGSP.LGSPGraph\n");
			sb.AppendFront("{\n");
			sb.Indent();

			sb.AppendFront("public " + modelName + "Graph(GRGEN_LGSP.LGSPGlobalVariables globalVariables) : base(new " + modelName + "GraphModel(), globalVariables, GetGraphName())\n");
			sb.AppendFront("{\n");
			sb.AppendFront("}\n");
			sb.Append("\n");

			foreach(NodeType nt in model.NodeTypes)
				GenCreateNodeConvenienceHelper(nt, false);
			foreach(PackageType pt in model.Packages)
			{
				foreach(NodeType nt in pt.NodeTypes)
					GenCreateNodeConvenienceHelper(nt, false);
			}

			foreach(EdgeType et in model.EdgeTypes)
				GenCreateEdgeConvenienceHelper(et, false);
			foreach(PackageType pt in model.Packages)
			{
				foreach(EdgeType et in pt.EdgeTypes)
					GenCreateEdgeConvenienceHelper(et, false);
			}

			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenNamedGraphClass()
		{
			string modelName = model.Ident.ToString();

			sb.AppendFront("//\n");
			sb.AppendFront("// INamedGraph (LGSPNamedGraph) implementation\n");
			sb.AppendFront("//\n");

			sb.AppendFront("public class " + modelName + "NamedGraph : GRGEN_LGSP.LGSPNamedGraph\n");
			sb.AppendFront("{\n");
			sb.Indent();

			sb.AppendFront("public " + modelName + "NamedGraph(GRGEN_LGSP.LGSPGlobalVariables globalVariables) "
					+ ": base(new " + modelName + "GraphModel(), globalVariables, GetGraphName(), 0)\n");
			sb.AppendFront("{\n");
			sb.AppendFront("}\n");
			sb.Append("\n");

			foreach(NodeType nodeType in model.NodeTypes)
				GenCreateNodeConvenienceHelper(nodeType, true);
			foreach(PackageType pt in model.Packages)
			{
				foreach(NodeType nt in pt.NodeTypes)
					GenCreateNodeConvenienceHelper(nt, true);
			}

			foreach(EdgeType et in model.EdgeTypes)
				GenCreateEdgeConvenienceHelper(et, true);
			foreach(PackageType pt in model.Packages)
			{
				foreach(EdgeType et in pt.EdgeTypes)
					GenCreateEdgeConvenienceHelper(et, true);
			}

			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenCreateNodeConvenienceHelper(NodeType nodeType, bool isNamed)
		{
			if(nodeType.IsAbstract())
				return;

			string name = GetPackagePrefix(nodeType) + FormatIdentifiable(nodeType);
			string elemref = FormatInheritanceClassRef(nodeType);
			sb.AppendFront("public " + elemref + " CreateNode" + name + "()\n");
			sb.AppendFront("{\n");
			sb.AppendFrontIndented("return " + elemref + ".CreateNode(this);\n");
			sb.AppendFront("}\n");
			sb.Append("\n");

			if(!isNamed)
				return;

			sb.AppendFront("public " + elemref + " CreateNode" + name + "(string nodeName)\n");
			sb.AppendFront("{\n");
			sb.AppendFrontIndented("return " + elemref + ".CreateNode(this, nodeName);\n");
			sb.AppendFront("}\n");
			sb.Append("\n");
		}

		private void GenCreateEdgeConvenienceHelper(EdgeType edgeType, bool isNamed)
		{
			if(edgeType.IsAbstract())
				return;

			string name = GetPackagePrefix(edgeType) + FormatIdentifiable(edgeType);
			string elemref = FormatInheritanceClassRef(edgeType);
			sb.AppendFront("public @" + elemref + " CreateEdge" + name);
			sb.Append("(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)\n");
			sb.AppendFront("{\n");
			sb.AppendFrontIndented("return @" + elemref + ".CreateEdge(this, source, target);\n");
			sb.AppendFront("}\n");
			sb.Append("\n");

			if(!isNamed)
				return;

			sb.AppendFront("public @" + elemref + " CreateEdge" + name);
			sb.Append("(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)\n");
			sb.AppendFront("{\n");
			sb.AppendFrontIndented("return @" + elemref + ".CreateEdge(this, source, target, edgeName);\n");
			sb.AppendFront("}\n");
			sb.Append("\n");
		}

		private void GenGraphModelBody(string modelName)
		{
			sb.AppendFront("private " + modelName + "NodeModel nodeModel = new " + modelName + "NodeModel();\n");
			sb.AppendFront("private " + modelName + "EdgeModel edgeModel = new " + modelName + "EdgeModel();\n");
			sb.AppendFront("private " + modelName + "ObjectModel objectModel = new " + modelName + "ObjectModel();\n");
			sb.AppendFront("private " + modelName + "TransientObjectModel transientObjectModel = new " + modelName + "TransientObjectModel();\n");

			GenPackages();
			GenEnumAttributeTypes();
			GenValidates();
			GenIndexDescriptions();
			GenIndicesGraphBinding();
			sb.Append("\n");

			sb.AppendFront("public override string ModelName { get { return \"" + modelName + "\"; } }\n");

			sb.AppendFront("public override GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }\n");
			sb.AppendFront("public override GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }\n");
			sb.AppendFront("public override GRGEN_LIBGR.IObjectModel ObjectModel { get { return objectModel; } }\n");
			sb.AppendFront("public override GRGEN_LIBGR.ITransientObjectModel TransientObjectModel { get { return transientObjectModel; } }\n");

			sb.AppendFront("public override IEnumerable<string> Packages "
					+ "{ get { return packages; } }\n");
			sb.AppendFront("public override IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes "
					+ "{ get { return enumAttributeTypes; } }\n");
			sb.AppendFront("public override IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo "
					+ "{ get { return validateInfos; } }\n");
			sb.AppendFront("public override IEnumerable<GRGEN_LIBGR.IndexDescription> IndexDescriptions "
					+ "{ get { return indexDescriptions; } }\n");
			sb.AppendFront("public static GRGEN_LIBGR.IndexDescription GetIndexDescription(int i) "
					+ "{ return indexDescriptions[i]; }\n");
			sb.AppendFront("public static GRGEN_LIBGR.IndexDescription GetIndexDescription(string indexName)\n ");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("for(int i=0; i<indexDescriptions.Length; ++i)\n");
			sb.Indent();
			sb.AppendFront("if(indexDescriptions[i].Name==indexName)\n");
			sb.AppendFrontIndented("return indexDescriptions[i];\n");
			sb.Unindent();
			sb.AppendFront("return null;\n");
			sb.Unindent();
			sb.AppendFront("}\n");
			sb.AppendFront("public override bool GraphElementsReferenceContainingGraph { get { return "
					+ (model.IsGraphofDefined() ? "true" : "false") + "; } }\n");
			sb.AppendFront("public override bool GraphElementUniquenessIsEnsured { get { return "
					+ (model.IsUniqueResulting() ? "true" : "false") + "; } }\n");
			sb.AppendFront("public override bool GraphElementUniquenessIsUserRequested { get { return "
					+ (model.IsUniqueDefined() ? "true" : "false") + "; } }\n");
			sb.AppendFront("public override bool ObjectUniquenessIsEnsured { get { return "
					+ (model.IsUniqueClassDefined() ? "true" : "false") + "; } }\n");
			sb.AppendFront("public override bool GraphElementsAreAccessibleByUniqueId { get { return "
					+ (model.IsUniqueIndexDefined() ? "true" : "false") + "; } }\n");
			sb.AppendFront("public override bool AreFunctionsParallelized { get { return "
					+ model.AreFunctionsParallel() + "; } }\n");
			sb.AppendFront("public override int BranchingFactorForEqualsAny { get { return "
					+ model.IsoParallel + "; } }\n");
			sb.AppendFront("public override int ThreadPoolSizeForSequencesParallelExecution { get { return "
					+ model.SequencesParallel + "; } }\n");

			GenGraphModelBodyAccessToExternalParts();

			GenArrayHelperDispatchers();

			sb.Append("\n");
			sb.AppendFront("public override void FailAssertion() { Debug.Assert(false); }\n");
			sb.AppendFront("public override string MD5Hash { get { return \"" + be.unit.TypeDigest + "\"; } }\n");
		}

		private void GenArrayHelperDispatchers()
		{
			sb.Append("\n");

			sb.AppendFront("public override global::System.Collections.IList ArrayOrderAscendingBy(global::System.Collections.IList array, string member)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			GenArrayHelperDispatcher("ArrayOrderAscendingBy", true, false, false);
			sb.Unindent();
			sb.AppendFront("}\n");

			sb.Append("\n");

			sb.AppendFront("public override global::System.Collections.IList ArrayOrderDescendingBy(global::System.Collections.IList array, string member)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			GenArrayHelperDispatcher("ArrayOrderDescendingBy", true, false, false);
			sb.Unindent();
			sb.AppendFront("}\n");

			sb.Append("\n");

			sb.AppendFront("public override global::System.Collections.IList ArrayGroupBy(global::System.Collections.IList array, string member)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			GenArrayHelperDispatcher("ArrayGroupBy", false, false, false);
			sb.Unindent();
			sb.AppendFront("}\n");

			sb.Append("\n");

			sb.AppendFront("public override global::System.Collections.IList ArrayKeepOneForEach(global::System.Collections.IList array, string member)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			GenArrayHelperDispatcher("ArrayKeepOneForEachBy", false, false, false);
			sb.Unindent();
			sb.AppendFront("}\n");

			sb.Append("\n"); ///////////////////////////////////////////////////////////////////////

			sb.AppendFront("public override int ArrayIndexOfBy(global::System.Collections.IList array, string member, object value)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			GenArrayHelperDispatcher("ArrayIndexOfBy", false, true, false);
			sb.Unindent();
			sb.AppendFront("}\n");

			sb.Append("\n");

			sb.AppendFront("public override int ArrayIndexOfBy(global::System.Collections.IList array, string member, object value, int startIndex)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			GenArrayHelperDispatcher("ArrayIndexOfBy", false, true, true);
			sb.Unindent();
			sb.AppendFront("}\n");

			sb.Append("\n");

			sb.AppendFront("public override int ArrayLastIndexOfBy(global::System.Collections.IList array, string member, object value)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			GenArrayHelperDispatcher("ArrayLastIndexOfBy", false, true, false);
			sb.Unindent();
			sb.AppendFront("}\n");

			sb.Append("\n");

			sb.AppendFront("public override int ArrayLastIndexOfBy(global::System.Collections.IList array, string member, object value, int startIndex)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			GenArrayHelperDispatcher("ArrayLastIndexOfBy", false, true, true);
			sb.Unindent();
			sb.AppendFront("}\n");

			sb.Append("\n");

			sb.AppendFront("public override int ArrayIndexOfOrderedBy(global::System.Collections.IList array, string member, object value)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			GenArrayHelperDispatcher("ArrayIndexOfOrderedBy", true, true, false);
			sb.Unindent();
			sb.AppendFront("}\n");

			sb.Append("\n");
		}

		// count arguments is sufficient to determine name and type of the arguments
		private void GenArrayHelperDispatcher(string name, bool requiresOrderable, bool isIndexOfByMethod, bool hasStartIndex)
		{
			sb.AppendFront("if(array.Count == 0)\n");
			sb.AppendFrontIndented("return " + (isIndexOfByMethod ? "-1" : "array") + ";\n");
			sb.AppendFront("if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))\n");
			sb.AppendFrontIndented("return " + (isIndexOfByMethod ? "-1" : "null") + ";\n");
			sb.AppendFront("GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];\n");
			sb.AppendFront("switch(elem.Type.PackagePrefixedName)\n");
			sb.AppendFront("{\n");
			foreach(InheritanceType type in model.AllInheritanceTypes)
			{
				if(GetNonAbstractTypeOrSubtype(type) == null)
					continue;
				GenArrayHelperByTypeDispatcher(type, name, requiresOrderable, isIndexOfByMethod, hasStartIndex);
			}
			sb.AppendFront("default: return " + (isIndexOfByMethod ? "-1" : "null") + ";\n");
			sb.AppendFront("}\n");
		}

		private void GenArrayHelperByTypeDispatcher(InheritanceType type, string name, bool requiresOrderable, bool isIndexOfByMethod, bool hasStartIndex)
		{
			sb.AppendFront("case \"" + GetPackagePrefixDoubleColon(type) + FormatIdentifiable(type) + "\":\n");
			sb.Indent();
			sb.AppendFront("switch(member)\n");
			sb.AppendFront("{\n");
			foreach(Entity entity in type.AllMembers)
			{
				if(!HasArrayHelpers(entity))
					continue;
				if(requiresOrderable && !entity.Type.IsOrderableType())
					continue;
				GenArrayHelperByMemberDispatcher(type, entity, name, isIndexOfByMethod, hasStartIndex);
			}
			sb.AppendFront("default:\n");
			sb.AppendFrontIndented("return " + (isIndexOfByMethod ? "-1" : "null") + ";\n");
			sb.AppendFront("}\n");
			sb.Unindent();
		}

		private void GenArrayHelperByMemberDispatcher(InheritanceType type, Entity entity, string functionName, bool isIndexOfByMethod, bool hasStartIndex)
		{
			string typeName = FormatType(type);
			string attributeName = FormatIdentifiable(entity);
			string attributeType = FormatAttributeType(entity);
			string arrayHelperClassName = GetPackagePrefixDot(type) + "ArrayHelper_" + type.Ident.ToString() + "_" + attributeName;
			sb.AppendFront("case \"" + attributeName + "\":\n");
			sb.AppendFrontIndented("return " + arrayHelperClassName
					+ "." + functionName + "((List<" + typeName + ">)array");
			if(isIndexOfByMethod)
				sb.Append(", (" + attributeType + ")value");
			if(hasStartIndex)
				sb.Append(", startIndex");
			sb.Append(");\n");
		}

		private void GenGraphModelBodyAccessToExternalParts()
		{
			if(model.IsEmitClassDefined())
			{
				sb.Append("\n");
				sb.AppendFront("public override object Parse(TextReader reader, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
				sb.AppendFront("{\n");
				sb.AppendFrontIndented("return AttributeTypeObjectEmitterParser.Parse(reader, attrType, graph);\n");
				sb.AppendFront("}\n");
				sb.AppendFront("public override string Serialize(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
				sb.AppendFront("{\n");
				sb.AppendFrontIndented("return AttributeTypeObjectEmitterParser.Serialize(attribute, attrType, graph);\n");
				sb.AppendFront("}\n");
				sb.AppendFront("public override string Emit(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
				sb.AppendFront("{\n");
				sb.AppendFrontIndented("return AttributeTypeObjectEmitterParser.Emit(attribute, attrType, graph);\n");
				sb.AppendFront("}\n");
				sb.AppendFront("public override void External(string line, GRGEN_LIBGR.IGraph graph)\n");
				sb.AppendFront("{\n");
				sb.AppendFrontIndented("AttributeTypeObjectEmitterParser.External(line, graph);\n");
				sb.AppendFront("}\n");
			}
			if(model.IsEmitGraphClassDefined())
			{
				sb.Append("\n");
				sb.AppendFront("public override GRGEN_LIBGR.INamedGraph AsGraph(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)\n");
				sb.AppendFront("{\n");
				sb.AppendFrontIndented("return AttributeTypeObjectEmitterParser.AsGraph(attribute, attrType, graph);\n");
				sb.AppendFront("}\n");
			}

			GenExternalObjectTypes();
			sb.AppendFront("public override GRGEN_LIBGR.ExternalObjectType[] ExternalObjectTypes { get { return externalObjectTypes; } }\n");

			sb.Append("\n");
			sb.AppendFront("private void FullyInitializeExternalObjectTypes()\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("externalObjectType_object.InitDirectSupertypes( new GRGEN_LIBGR.ExternalObjectType[] { } );\n");
			foreach(ExternalObjectType et in model.ExternalObjectTypes)
			{
				sb.AppendFront("externalObjectType_" + et.Ident + ".InitDirectSupertypes( "
						+ "new GRGEN_LIBGR.ExternalObjectType[] { ");
				bool directSupertypeAvailable = false;
				foreach(InheritanceType superType in et.DirectSuperTypes)
				{
					sb.Append("externalObjectType_" + superType.Ident + ", ");
					directSupertypeAvailable = true;
				}
				if(!directSupertypeAvailable)
					sb.Append("externalObjectType_object ");
				sb.Append("} );\n");
			}
			sb.Unindent();
			sb.AppendFront("}\n");

			if(model.IsEqualClassDefined() && model.IsLowerClassDefined())
			{
				sb.Append("\n");
				sb.AppendFront("public override bool IsEqualClassDefined { get { return true; } }\n");
				sb.AppendFront("public override bool IsLowerClassDefined { get { return true; } }\n");
				sb.AppendFront("public override bool IsEqual(object this_, object that, IDictionary<object, object> visitedObjects)\n");
				sb.AppendFront("{\n");
				sb.AppendFrontIndented("return AttributeTypeObjectCopierComparer.IsEqual(this_, that, visitedObjects);\n");
				sb.AppendFront("}\n");
				sb.AppendFront("public override bool IsLower(object this_, object that, IDictionary<object, object> visitedObjects)\n");
				sb.AppendFront("{\n");
				sb.AppendFrontIndented("return AttributeTypeObjectCopierComparer.IsLower(this_, that, visitedObjects);\n");
				sb.AppendFront("}\n");
			}
			else if(model.IsEqualClassDefined())
			{
				sb.Append("\n");
				sb.AppendFront("public override bool IsEqualClassDefined { get { return true; } }\n");
				sb.AppendFront("public override bool IsEqual(object this_, object that, IDictionary<object, object> visitedObjects)\n");
				sb.AppendFront("{\n");
				sb.AppendFrontIndented("return AttributeTypeObjectCopierComparer.IsEqual(this_, that, visitedObjects);\n");
				sb.AppendFront("}\n");
			}
		}

		private void GenExternalObjectTypes()
		{
			sb.Append("\n");
			sb.AppendFront("public static GRGEN_LIBGR.ExternalObjectType externalObjectType_object = new ExternalObjectType_object();\n");
			foreach(ExternalObjectType et in model.ExternalObjectTypes)
			{
				sb.AppendFront("public static GRGEN_LIBGR.ExternalObjectType externalObjectType_" + et.Ident
						+ " = new ExternalObjectType_" + et.Ident + "();\n");
			}

			sb.AppendFront("private GRGEN_LIBGR.ExternalObjectType[] externalObjectTypes = { ");
			sb.Append("externalObjectType_object");
			foreach(ExternalObjectType et in model.ExternalObjectTypes)
				sb.Append(", externalObjectType_" + et.Ident);
			sb.Append(" };\n");
		}

		private void GenPackages()
		{
			sb.AppendFront("private string[] packages = {\n");
			sb.Indent();
			foreach(PackageType pt in model.Packages)
				sb.AppendFront("\"" + pt.Ident + "\",\n");
			sb.Unindent();
			sb.AppendFront("};\n");
		}

		private void GenValidates()
		{
			sb.AppendFront("private GRGEN_LIBGR.ValidateInfo[] validateInfos = {\n");

			foreach(EdgeType edgeType in model.EdgeTypes)
				GenValidate(edgeType);

			foreach(PackageType pt in model.Packages)
			{
				foreach(EdgeType edgeType in pt.EdgeTypes)
					GenValidate(edgeType);
			}

			sb.AppendFront("};\n");
		}

		private void GenValidate(EdgeType edgeType)
		{
			foreach(ConnAssert ca in edgeType.ConnAsserts)
			{
				sb.AppendFront("new GRGEN_LIBGR.ValidateInfo(");
				sb.Append(FormatTypeClassRef(edgeType) + ".typeVar, ");
				sb.Append(FormatTypeClassRef(ca.SrcType) + ".typeVar, ");
				sb.Append(FormatTypeClassRef(ca.TgtType) + ".typeVar, ");
				sb.Append(FormatLong(ca.SrcLower) + ", ");
				sb.Append(FormatLong(ca.SrcUpper) + ", ");
				sb.Append(FormatLong(ca.TgtLower) + ", ");
				sb.Append(FormatLong(ca.TgtUpper) + ", ");
				sb.Append(ca.BothDirections);
				sb.Append("),\n");
			}
		}

		private void GenIndexDescriptions()
		{
			sb.AppendFront("private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {\n");

			foreach(Index index in model.Indices)
			{
				if(index is AttributeIndex)
					GenIndexDescription((AttributeIndex)index);
				else
					GenIndexDescription((IncidenceCountIndex)index);
			}

			/*for(PackageType pt : model.getPackages()) {
				for(AttributeIndex index : pt.getIndices()) {
					genIndexDescription(index);
				}
			}*/

			sb.AppendFront("};\n");
		}

		private void GenIndexDescription(AttributeIndex index)
		{
			sb.AppendFront("new GRGEN_LIBGR.AttributeIndexDescription(");
			sb.Append("\"" + index.Ident + "\", ");
			sb.Append(FormatTypeClassName(index.type) + ".typeVar, ");
			sb.Append(FormatTypeClassName(index.entity.Owner) + "." + FormatAttributeTypeName(index.entity));
			sb.Append("),\n");
		}

		private void GenIndexDescription(IncidenceCountIndex index)
		{
			sb.AppendFront("new GRGEN_LIBGR.IncidenceCountIndexDescription(");
			sb.Append("\"" + index.Ident + "\", ");
			switch(index.Direction())
			{
			case de.unika.ipd.grgen.util.Direction.OUTGOING:
				sb.Append("GRGEN_LIBGR.IncidenceDirection.OUTGOING, ");
				break;
			case de.unika.ipd.grgen.util.Direction.INCOMING:
				sb.Append("GRGEN_LIBGR.IncidenceDirection.INCOMING, ");
				break;
			case de.unika.ipd.grgen.util.Direction.INCIDENT:
				sb.Append("GRGEN_LIBGR.IncidenceDirection.INCIDENT, ");
				break;
			case de.unika.ipd.grgen.util.Direction.INVALID:
				throw new Exception("Internal compiler error");
			}
			sb.Append(FormatTypeClassRefInstance(index.StartNodeType) + ", ");
			sb.Append(FormatTypeClassRefInstance(index.IncidentEdgeType) + ", ");
			sb.Append(FormatTypeClassRefInstance(index.AdjacentNodeType));
			sb.Append("),\n");
		}

		private void GenIndicesGraphBinding()
		{
			sb.AppendFront("public override GRGEN_LIBGR.IUniquenessHandler CreateUniquenessHandler(GRGEN_LIBGR.IGraph graph) {\n");
			sb.Indent();
			if(model.IsUniqueIndexDefined())
			{
				sb.AppendFront("return new GRGEN_LGSP.LGSPUniquenessIndex((GRGEN_LGSP.LGSPGraph)graph); "
						+ "// must be called before the indices so that its event handler is registered first, doing the unique id computation the indices depend upon\n");
			}
			else if(model.IsUniqueResulting())
			{
				sb.AppendFront("return new GRGEN_LGSP.LGSPUniquenessEnsurer((GRGEN_LGSP.LGSPGraph)graph); "
						+ "// must be called before the indices so that its event handler is registered first, doing the unique id computation the indices depend upon\n");
			}
			else
				sb.AppendFront("return null;\n");
			sb.Unindent();
			sb.AppendFront("}\n");

			sb.AppendFront("public override GRGEN_LIBGR.IIndexSet CreateIndexSet(GRGEN_LIBGR.IGraph graph) {\n");
			sb.AppendFrontIndented("return new " + model.Ident + "IndexSet((GRGEN_LGSP.LGSPGraph)graph);\n");
			sb.AppendFront("}\n");

			sb.AppendFront("public override void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, "
					+ "IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {\n");
			sb.Indent();
			if(model.IsUniqueResulting())
			{
				sb.AppendFront("((GRGEN_LGSP.LGSPUniquenessEnsurer)graph.UniquenessHandler).FillAsClone("
						+ "(GRGEN_LGSP.LGSPUniquenessEnsurer)originalGraph.UniquenessHandler, oldToNewMap);\n");
			}
			sb.AppendFront("((" + model.Ident + "IndexSet)graph.Indices).FillAsClone("
					+ "(GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		private void GenEnumAttributeTypes()
		{
			sb.AppendFront("private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {\n");
			sb.Indent();
			foreach(EnumType enumt in model.EnumTypes)
				GenEnumAttributeType(enumt);
			foreach(PackageType pt in model.Packages)
			{
				foreach(EnumType enumt in pt.EnumTypes)
					GenEnumAttributeType(enumt);
			}
			sb.Unindent();
			sb.AppendFront("};\n");
		}

		private void GenEnumAttributeType(EnumType enumt)
		{
			sb.AppendFront("GRGEN_MODEL." + GetPackagePrefixDot(enumt) + "Enums.@" + FormatIdentifiable(enumt) + ",\n");
		}

		private static string GetKindName(InheritanceType type)
		{
			if(type is NodeType)
				return "Node";
			else if(type is EdgeType)
				return "Edge";
			else if(type is InternalTransientObjectType)
				return "TransientObject";
			else
				return "Object";
		}

		private static ICollection<InheritanceType> GetInheritanceTypes<T1>(ICollection<T1> inheritanceTypes) where T1 : de.unika.ipd.grgen.ir.model.type.InheritanceType
		{
			return new List<InheritanceType>(inheritanceTypes); // TODO: performance optimization caching (and maybe another collection type fits better)
		}

		///////////////////////
		// Private variables //
		///////////////////////

		private SearchPlanBackend2 be;
		private Model model;
		private SourceBuilder sb = null;
		private SourceBuilder stubsb = null;
		private string curMemberOwner = null;
		private HashSet<string> rootTypes;
	}

}
