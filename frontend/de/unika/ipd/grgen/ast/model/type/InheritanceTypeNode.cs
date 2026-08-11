/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>
namespace de.unika.ipd.grgen.ast.model.type
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using de.unika.ipd.grgen.ast;
	using ConstructorParamNode = de.unika.ipd.grgen.ast.ConstructorParamNode;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using MemberAccessor = de.unika.ipd.grgen.ast.MemberAccessor;
	using ConstructorDeclNode = de.unika.ipd.grgen.ast.decl.ConstructorDeclNode;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using FunctionOrOperatorDeclBaseNode = de.unika.ipd.grgen.ast.decl.executable.FunctionOrOperatorDeclBaseNode;
	using FunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
	using ProcedureDeclBaseNode = de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclBaseNode;
	using ProcedureDeclNode = de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
	using ArrayInitNode = de.unika.ipd.grgen.ast.expr.array.ArrayInitNode;
	using DequeInitNode = de.unika.ipd.grgen.ast.expr.deque.DequeInitNode;
	using MapInitNode = de.unika.ipd.grgen.ast.expr.map.MapInitNode;
	using SetInitNode = de.unika.ipd.grgen.ast.expr.set.SetInitNode;
	using MemberInitNode = de.unika.ipd.grgen.ast.model.MemberInitNode;
	using AbstractMemberDeclNode = de.unika.ipd.grgen.ast.model.decl.AbstractMemberDeclNode;
	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using CompoundTypeNode = de.unika.ipd.grgen.ast.type.CompoundTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using FunctionMethod = de.unika.ipd.grgen.ir.executable.FunctionMethod;
	using ProcedureMethod = de.unika.ipd.grgen.ir.executable.ProcedureMethod;
	using ArrayInit = de.unika.ipd.grgen.ir.expr.array.ArrayInit;
	using DequeInit = de.unika.ipd.grgen.ir.expr.deque.DequeInit;
	using MapInit = de.unika.ipd.grgen.ir.expr.map.MapInit;
	using SetInit = de.unika.ipd.grgen.ir.expr.set.SetInit;
	using MemberInit = de.unika.ipd.grgen.ir.model.MemberInit;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using Symbol = de.unika.ipd.grgen.parser.Symbol;

	/// <summary>
	/// Base class for compound types, that allow inheritance.
	/// </summary>
	public abstract class InheritanceTypeNode : CompoundTypeNode, MemberAccessor
	{
		public const int MOD_CONST = 1;
		public const int MOD_ABSTRACT = 2;

		protected internal CollectNode<IdentNode> extendUnresolved;
		protected internal CollectNode<BaseNode> bodyUnresolved;

		protected internal CollectNode<BaseNode> body;

		/// <summary>
		/// The modifiers for this type.
		/// An ORed combination of the constants above.
		/// </summary>
		private int modifiers = 0;

		/// <summary>
		/// The name of the external implementation of this type or null.
		/// This is for the (unsupported) "Embedding GrGen into C#" prototype.
		/// </summary>
		private string externalName = null;

		/// <summary>
		/// Maps all member (attribute) names to their declarations. </summary>
		private IDictionary<string, DeclNode> allMembers = null;

		/// <summary>
		/// Contains all super types of this type (not including this itself) </summary>
		private ISet<InheritanceTypeNode> allSuperTypes = null;

		/// <summary>
		/// Contains all direct sub types of this type </summary>
		private ISet<InheritanceTypeNode> directSubTypes = new LinkedHashSet<InheritanceTypeNode>();

		/// <summary>
		/// Contains all sub types of this type (not including this itself) </summary>
		private ISet<InheritanceTypeNode> allSubTypes = null;

		public virtual void AddDirectSubType(InheritanceTypeNode type)
		{
			directSubTypes.Add(type);
		}

		/// <summary>
		/// Returns all sub types of this type (not including itself). </summary>
		protected internal virtual ICollection<InheritanceTypeNode> AllSubTypes
		{
			get
			{
				Debug.Assert(IsResolved());

				if(allSubTypes == null)
				{
					allSubTypes = new HashSet<InheritanceTypeNode>();

					foreach(InheritanceTypeNode type in directSubTypes)
					{
						allSubTypes.AddAll(type.AllSubTypes);
						allSubTypes.Add(type);
					}
				}
				return allSubTypes;
			}
		}

		public virtual bool IsA(InheritanceTypeNode type)
		{
			Debug.Assert(type != null);
			return this == type || AllSuperTypes.Contains(type);
		}

		/// <summary>
		/// Returns all super types of this type (not including itself). </summary>
		protected internal virtual ICollection<InheritanceTypeNode> AllSuperTypes
		{
			get
			{
				if(allSuperTypes == null)
				{
					allSuperTypes = new HashSet<InheritanceTypeNode>();

					foreach(InheritanceTypeNode type in DirectSuperTypes)
					{
						allSuperTypes.AddAll(type.AllSuperTypes);
						allSuperTypes.Add(type);
					}
				}
				return allSuperTypes;
			}
		}

		public static bool HasCommonSubtype(InheritanceTypeNode type1, InheritanceTypeNode type2)
		{
			if(type1.IsA(type2))
				return true;
			if(type2.IsA(type1))
				return true;

			ICollection<InheritanceTypeNode> subTypes1 = type1.AllSubTypes;
			ICollection<InheritanceTypeNode> subTypes2 = type2.AllSubTypes;
			foreach(InheritanceTypeNode typeNode2 in subTypes2)
			{
				if(subTypes1.Contains(typeNode2))
					return true;
			}

			return false;
		}

		public virtual CollectNode<BaseNode> Body
		{
			get
			{
				return body;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			bool res = base.CheckLocal();
			AllSuperTypes;

			foreach(DeclNode member in AllMembers.Values)
			{
				if(member is AbstractMemberDeclNode && !IsAbstract())
				{
					Ident.ReportError("The " + Kind + " " + TypeName
							+ " must be declared abstract, because member " + member + " is abstract.");
					res = false;
				}
			}

			foreach(BaseNode child in body.ChildrenExact)
			{
				if(child is DeclNode && !(child is ConstructorDeclNode))
				{
					DeclNode directMember = (DeclNode)child;
					if(directMember.Ident.ToString().Equals(Ident.ToString()))
					{
						Ident.ReportError("The member " + directMember.Ident
										+ " must be named differently than its containing " + Kind
										+ " " + TypeName + ".");
					}
				}
			}

			// Check constructors for ambiguity
			IList<ConstructorDeclNode> constrs = new List<ConstructorDeclNode>();
			foreach(BaseNode child in body.ChildrenExact)
			{
				if(child is ConstructorDeclNode)
					constrs.Add((ConstructorDeclNode)child);
			}

			for(int i = 0; i < constrs.Count; i++)
			{
				ConstructorDeclNode c1 = constrs[i];
				IList<ConstructorParamNode> params1 = c1.Parameters.ChildrenAsList;
				int numParams1 = params1.Count;
				for(int j = i + 1; j < constrs.Count; j++)
				{
					ConstructorDeclNode c2 = constrs[j];
					IList<ConstructorParamNode> params2 = c2.Parameters.ChildrenAsList;
					int numParams2 = params2.Count;
					int p = 0;
					bool ambiguous = false;
					for(; p < numParams1 && p < numParams2; p++)
					{
						ConstructorParamNode param1 = params1[p];
						ConstructorParamNode param2 = params2[p];
						if(param1.rhs != null && param2.rhs != null)
						{
							ambiguous = true; // non-optional part is identical => ambiguous
							break;
						}
						else if(param1.lhs.DeclType != param2.lhs.DeclType)
							break; // found a difference => not ambiguous
					}

					// Constructors are also ambiguous, if both have identical parameter types,
					// or if their non-optional parts have identical types and one also has an optional part.
					if(p == numParams1 && p == numParams2
							|| p == numParams1 && params2[p].rhs != null
							|| p == numParams2 && params1[p].rhs != null)
						ambiguous = true;

					if(ambiguous)
					{
						c1.ReportError("Constructor is ambiguous (other constructor at " + c2.Coords + ").");
						res = false;
					}
				}
			}

			res &= CheckMembers();

			return res;
		}

		/// <summary>
		/// Get the IR object as type.
		/// The cast must always succeed. </summary>
		/// <returns> The IR object as type. </returns>
		public override Type IRType
		{
			get
			{
				return InheritanceIRType;
			}
		}

		public virtual InheritanceType InheritanceIRType
		{
			get
			{
				return CheckIR(typeof(InheritanceType));
			}
		}

		public override bool FixupDefinition(IdentNode id)
		{
			Debug.Assert(IsResolved());

			if(FixupDefinition(id, Scope, false))
				return true;

			Symbol.Definition def = null;
			foreach(InheritanceTypeNode inh in DirectSuperTypes)
			{
				if(inh.FixupDefinition(id))
				{
					Symbol.Definition newDef = id.SymDef;
					if(def == null)
						def = newDef;
					else if(def != newDef)
						Ident.ReportError("Identifier " + id + " is ambiguous"
								+ " [one declaration at " + newDef.Coords + ", another declaration at " + def.Coords + "]."
								+ " There must be one unique declaration of a member, in a common parent; or different names must be used for different members."
								+ " A method that comes in from more than one parent must be implemented locally, overriding the parental versions.");
				}
			}

			return def != null;
		}

		protected internal virtual int Modifiers
		{
			set
			{
				this.modifiers = value;
			}
		}

		public bool IsAbstract()
		{
			return (modifiers & MOD_ABSTRACT) != 0;
		}

		public bool IsConst()
		{
			return (modifiers & MOD_CONST) != 0;
		}

		protected internal int IRModifiers
		{
			get
			{
				return (IsAbstract() ? InheritanceType.ABSTRACT : 0) | (IsConst() ? InheritanceType.CONST : 0);
			}
		}

		protected internal virtual string ExternalName
		{
			set
			{
				externalName = value;
			}
			get
			{
				return externalName;
			}
		}


		public abstract ICollection<InheritanceTypeNode> DirectSuperTypes {get;}

		public virtual DeclNode TryGetMember(string name)
		{
			return AllMembers[name];
		}

		/// <summary>
		/// Returns all members of this type. </summary>
		protected internal virtual void GetMembers(IDictionary<string, DeclNode> members)
		{
			Debug.Assert(IsResolved());
			foreach(BaseNode child in body.ChildrenExact)
			{
				if(child is DeclNode)
				{
					DeclNode decl = (DeclNode)child;
					members[decl.Ident.ToString()] = decl;
				}
			}
		}

		/// <summary>
		/// Checks the members of this typet. </summary>
		protected internal virtual bool CheckMembers()
		{
			bool res = true;

			Debug.Assert(IsResolved());

			LinkedHashMap<string, DeclNode> allInheritedMembers = new LinkedHashMap<string, DeclNode>();

			foreach(InheritanceTypeNode superType in DirectSuperTypes)
				allInheritedMembers.PutAll(superType.AllMembers);

			foreach(BaseNode child in body.ChildrenExact)
			{
				if(child is ConstructorDeclNode)
					continue;

				if(child is FunctionDeclNode)
				{
					FunctionDeclNode function = (FunctionDeclNode)child;
					res &= CheckFunctionOverride(function);
				}
				else if(child is ProcedureDeclNode)
				{
					ProcedureDeclNode procedure = (ProcedureDeclNode)child;
					res &= CheckProcedureOverride(procedure);
				}
				else if(child is DeclNode)
				{
					DeclNode decl = (DeclNode)child;
					DeclNode old = allInheritedMembers.Get(decl.Ident.ToString());
					if(old != null && !(old is AbstractMemberDeclNode))
					{
						decl.ReportError("The member " + decl.Ident
								+ " of " + Kind + " " + Ident
								+ " is already declared at " + GetContainingType(old).ToStringWithDeclarationCoords() + ".");
						res = false;
					}
				}
			}

			return res;
		}

		// maybe TODO: maybe remember type of member instead of ascending syntax tree afterwards
		private TypeNode GetContainingType(DeclNode decl)
		{
			BaseNode parent = decl.Parents.GetEnumerator().Next();
			BaseNode grandParent = parent.Parents.GetEnumerator().Next();
			return (TypeNode)grandParent;
		}

		private bool CheckFunctionOverride(FunctionDeclNode function)
		{
			bool res = true;

			if(!function.IsChecked())
				return res;
			foreach(InheritanceTypeNode @base in AllSuperTypes)
			{
				foreach(BaseNode baseChild in @base.Body.ChildrenExact)
				{
					if(baseChild is FunctionDeclNode)
					{
						FunctionDeclNode functionBase = (FunctionDeclNode)baseChild;
						if(!functionBase.IsChecked())
							continue;
						if(function.ident.ToString().Equals(functionBase.ident.ToString()))
							res &= checkSignatureAdhered(functionBase, function);
					}
				}
			}

			return res;
		}

		private bool CheckProcedureOverride(ProcedureDeclNode procedure)
		{
			bool res = true;

			if(!procedure.IsChecked())
				return res;
			foreach(InheritanceTypeNode @base in AllSuperTypes)
			{
				foreach(BaseNode baseChild in @base.Body.ChildrenExact)
				{
					if(baseChild is ProcedureDeclNode)
					{
						ProcedureDeclNode procedureBase = (ProcedureDeclNode)baseChild;
						if(!procedureBase.IsChecked())
							continue;
						if(procedure.ident.ToString().Equals(procedureBase.ident.ToString()))
							res &= CheckSignatureAdhered(procedureBase, procedure);
					}
				}
			}

			return res;
		}

		/// <summary>
		/// Returns all members (including inherited ones) of this type. </summary>
		public virtual IDictionary<string, DeclNode> AllMembers
		{
			get
			{
				if(allMembers == null)
				{
					allMembers = new LinkedHashMap<string, DeclNode>();

					foreach(InheritanceTypeNode superType in DirectSuperTypes)
						allMembers.PutAll(superType.AllMembers);

					GetMembers(allMembers);
				}

				return allMembers;
			}
		}

		public virtual bool CheckStatementsInMethods()
		{
			bool res = true;
			foreach(BaseNode child in body.ChildrenExact)
			{
				if(child is FunctionDeclNode)
				{
					FunctionDeclNode function = (FunctionDeclNode)child;
					res &= EvalStatementNode.CheckStatements(true, function, null, function.evalStatements, true);
				}
				else if(child is ProcedureDeclNode)
				{
					ProcedureDeclNode procedure = (ProcedureDeclNode)child;
					res &= EvalStatementNode.CheckStatements(false, procedure, null, procedure.evalStatements, true);
				}
			}
			return res;
		}

		/// <summary>
		/// Check whether the override adheres to the signature of the base declaration </summary>
		protected internal static bool CheckSignatureAdhered(FunctionOrOperatorDeclBaseNode @base, FunctionOrOperatorDeclBaseNode @override)
		{
			string functionName = @base.ident.ToString();

			IList<TypeNode> baseParamTypes = @base.ParameterTypes;
			IList<TypeNode> overrideParamTypes = @override.ParameterTypes;

			// check if the number of parameters is correct
			int numBaseParams = baseParamTypes.Count;
			int numOverrideParams = overrideParamTypes.Count;
			if(numBaseParams != numOverrideParams)
			{
				@override.ReportError("The function method " + functionName + " is declared with " + numBaseParams
						+ " parameters in a parent class" + @base.AtCoords + ", but is overriden here with " + numOverrideParams + " parameters.");
				return false;
			}

			// check if the types of the parameters are correct
			bool res = true;
			for(int i = 0; i < numBaseParams; ++i)
			{
				TypeNode baseParamType = baseParamTypes[i];
				TypeNode overrideParamType = overrideParamTypes[i];

				if(!baseParamType.IsEqual(overrideParamType))
				{
					res = false;
					@override.ReportError("The function method " + functionName + " is declared with a " + (i + 1) + ". parameter of type " + baseParamType.TypeName
							+ " in a parent class" + @base.AtCoords + ", but is overriden here with a " + (i + 1) + ". parameter of type " + overrideParamType.TypeName + ".");
				}
			}

			// check if the return type is correct
			if(!@base.ResultType.IsEqual(@override.ResultType))
				@override.ReportError("The function method " + functionName + " is declared with a return parameter of type " + @base.ResultType.TypeName
						+ " in a parent class" + @base.AtCoords + ", but is overriden here with a return parameter of type " + @override.ResultType.TypeName + ".");

			return res;
		}

		/// <summary>
		/// Check whether the override adheres to the signature of the base declaration </summary>
		protected internal static bool CheckSignatureAdhered(ProcedureDeclBaseNode @base, ProcedureDeclBaseNode @override)
		{
			string procedureName = @base.ident.ToString();

			IList<TypeNode> baseParamTypes = @base.ParameterTypes;
			IList<TypeNode> overrideParamTypes = @override.ParameterTypes;

			// check if the number of parameters is correct
			int numBaseParams = baseParamTypes.Count;
			int numOverrideParams = overrideParamTypes.Count;
			if(numBaseParams != numOverrideParams)
			{
				@override.ReportError("The procedure method " + procedureName + " is declared with " + numBaseParams
						+ " parameters in a parent class" + @base.AtCoords + ", but is overriden here with " + numOverrideParams + " parameters.");
				return false;
			}

			// check if the types of the parameters are correct
			bool res = true;
			for(int i = 0; i < numBaseParams; ++i)
			{
				TypeNode baseParamType = baseParamTypes[i];
				TypeNode overrideParamType = overrideParamTypes[i];

				if(!baseParamType.IsEqual(overrideParamType))
				{
					res = false;
					@override.ReportError("The procedure method " + procedureName + " is declared with a " + (i + 1) + ". parameter of type " + baseParamType.TypeName
							+ " in a parent class" + @base.AtCoords + ", but is overriden here with a " + (i + 1) + ". parameter of type " + overrideParamType.TypeName + ".");
				}
			}

			IList<TypeNode> baseReturnParams = @base.ResultTypes;
			IList<TypeNode> overrideReturnParams = @override.ResultTypes;

			// check if the number of parameters is correct
			int numBaseReturnParams = baseReturnParams.Count;
			int numOverrideReturnParams = overrideReturnParams.Count;
			if(numBaseReturnParams != numOverrideReturnParams)
			{
				@override.ReportError("The procedure method " + procedureName + " is declared with " + numBaseReturnParams
						+ " return parameters in a parent class" + @base.AtCoords + ", but is overriden here with " + numOverrideReturnParams + " return parameters.");
				return false;
			}

			// check if the types of the parameters are correct
			for(int i = 0; i < numBaseReturnParams; ++i)
			{
				TypeNode baseReturnParamType = baseReturnParams[i];
				TypeNode overrideReturnParamType = overrideReturnParams[i];

				if(!baseReturnParamType.IsEqual(overrideReturnParamType))
				{
					res = false;
					@override.ReportError("The procedure method " + procedureName + " is declared with a " + (i + 1) + ". return parameter of type " + baseReturnParamType.TypeName
							+ " in a parent class" + @base.AtCoords + ", but is overriden here with a " + (i + 1) + ". return parameter of type " + overrideReturnParamType.TypeName + ".");
				}
			}

			return res;
		}

		protected internal virtual void ConstructIR(InheritanceType inhType)
		{
			foreach(BaseNode child in body.ChildrenExact)
				ConstructAndAddIRChild(inhType, child);
			foreach(InheritanceTypeNode inh in DirectSuperTypes)
				inhType.AddDirectSuperType(inh.InheritanceIRType);
		}

		private static void ConstructAndAddIRChild(InheritanceType inhType, BaseNode child)
		{
			if(child is ConstructorDeclNode)
			{
				ConstructorDeclNode cd = (ConstructorDeclNode)child;
				inhType.AddConstructor(cd.IRConstructor);
			}
			else if(child is DeclNode)
			{
				DeclNode decl = (DeclNode)child;
				if(child is FunctionDeclNode)
					inhType.AddFunctionMethod(child.CheckIR(typeof(FunctionMethod)));
				else if(child is ProcedureDeclNode)
					inhType.AddProcedureMethod(child.CheckIR(typeof(ProcedureMethod)));
				else
					inhType.AddMember(decl.IREntity);
			}
			else if(child is MemberInitNode)
			{
				MemberInitNode mi = (MemberInitNode)child;
				IR init = mi.IR;
				if(init is MapInit)
					inhType.AddMapInit(mi.CheckIR(typeof(MapInit)));
				else if(init is SetInit)
					inhType.AddSetInit(mi.CheckIR(typeof(SetInit)));
				else if(init is ArrayInit)
					inhType.AddArrayInit(mi.CheckIR(typeof(ArrayInit)));
				else if(init is DequeInit)
					inhType.AddDequeInit(mi.CheckIR(typeof(DequeInit)));
				else
					inhType.AddMemberInit(mi.CheckIR(typeof(MemberInit)));
			}
			else if(child is MapInitNode)
			{
				MapInitNode mi = (MapInitNode)child;
				inhType.AddMapInit(mi.IRMapInit);
			}
			else if(child is SetInitNode)
			{
				SetInitNode si = (SetInitNode)child;
				inhType.AddSetInit(si.IRSetInit);
			}
			else if(child is ArrayInitNode)
			{
				ArrayInitNode ai = (ArrayInitNode)child;
				inhType.AddArrayInit(ai.IRArrayInit);
			}
			else if(child is DequeInitNode)
			{
				DequeInitNode di = (DequeInitNode)child;
				inhType.AddDequeInit(di.IRDequeInit);
			}
		}

		public override string ToString()
		{
			return Ident.ToString() + " (" + base.ToString() + ")";
		}
	}

}
