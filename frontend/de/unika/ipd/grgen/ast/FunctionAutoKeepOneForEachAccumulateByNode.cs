/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast
{

	using System.Collections.Generic;

	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using FunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using DefinedMatchTypeNode = de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
	using MatchTypeActionNode = de.unika.ipd.grgen.ast.type.MatchTypeActionNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using FunctionAutoKeepOneForEachAccumulateBy = de.unika.ipd.grgen.ir.stmt.FunctionAutoKeepOneForEachAccumulateBy;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using Function = de.unika.ipd.grgen.ir.executable.Function;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class FunctionAutoKeepOneForEachAccumulateByNode : FunctionAutoNode
	{
		static FunctionAutoKeepOneForEachAccumulateByNode()
		{
			SetClassName(typeof(FunctionAutoKeepOneForEachAccumulateByNode), "auto keep one for each accumulate by");
		}

		private IdentNode target;
		private VarDeclNode targetVar;

		private IdentNode attribute;
		private DeclNode member;

		private IdentNode accumulationAttribute;
		private DeclNode accumulationMember;

		private string accumulationMethod;

		public FunctionAutoKeepOneForEachAccumulateByNode(Coords coords, string function,
				IdentNode attribute, IdentNode accumulationAttribute, string accumulationMethod,
				IdentNode target)
			: base(coords, function)
		{
			this.attribute = attribute;
			this.accumulationAttribute = accumulationAttribute;
			this.accumulationMethod = accumulationMethod;
			this.target = target;
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				//children.add(targetExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				//childrenNames.add("targetExpr");
				return childrenNames;
			}
		}

		private static readonly DeclarationResolver<VarDeclNode> targetResolver =
				new DeclarationResolver<VarDeclNode>(typeof(VarDeclNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			targetVar = targetResolver.Resolve(target, this);
			return targetVar != null;
		}

		public override bool ResolveLocalBypass()
		{
			return ResolveLocal();
		}

		protected internal override bool CheckLocal()
		{
			if(!function.Equals("keepOneForEachAccumulateBy"))
			{
				ReportError("Unknown function in auto(), expected keepOneForEachAccumulateBy (e.g. keepOneForEach<foo>Accumulate<bar>By<sum>).");
				return false;
			}

			ArrayTypeNode arrayType = TargetType;
			if(!(arrayType.valueType is MatchTypeActionNode)
					&& !(arrayType.valueType is DefinedMatchTypeNode))
			{
				ReportError("The auto-generated function keepOneForEachAccumulateBy can only be employed on an array of match or match class types"
						+ " (but is employed on an array of " + arrayType.valueType.TypeName + ").");
				return false;
			}

			TypeNode valueType = arrayType.valueType;
			member = Resolver<BaseNode>.ResolveMember(valueType, attribute);
			if(member == null)
				return false;

			TypeNode memberType = TypeOfElementToBeExtracted;
			if(!memberType.IsFilterableType())
			{
				target.ReportError("The keepOneForEach argument of the auto-generated function keepOneForEachAccumulateBy is only available for attributes of type "
						+ TypeNode.FilterableTypesAsString + " (but is employed on an attribute of type " + memberType.TypeName + ").");
				return false;
			}

			accumulationMember = Resolver<BaseNode>.ResolveMember(valueType, accumulationAttribute);
			if(accumulationMember == null)
				return false;

			TypeNode accumulationMemberType = TypeOfAccumulationElementToBeExtracted;
			if(!accumulationMemberType.IsAccumulatableType())
			{
				target.ReportError("The accumulate argument of the auto-generated function keepOneForEachAccumulateBy is only available for attributes of type "
						+ TypeNode.AccumulatableTypesAsString + " (but is employed on an attribute of type " + accumulationMemberType.TypeName + ").");
				return false;
			}

			return true;
		}

		public override bool CheckLocalBypass()
		{
			return CheckLocal();
		}

		public override bool CheckLocal(FunctionDeclNode functionDecl)
		{
			if(!(functionDecl.ResultType is ArrayTypeNode))
			{
				ReportError("The result type of the function " + functionDecl.Ident
						+ " employing the auto-generated function " + FunctionName()
						+ " must be an array (but is of type " + functionDecl.ResultType.TypeName + ").");
				return false;
			}
			ArrayTypeNode resultType = (ArrayTypeNode)functionDecl.ResultType;
			if(!(resultType.ElementType is DefinedMatchTypeNode)
					&& !(resultType.ElementType is MatchTypeActionNode))
			{
				ReportError("The result type of the function " + functionDecl.Ident
						+ " employing the auto-generated function " + FunctionName()
						+ " must be an array<match<class T>> or array<match<T>>"
						+ " (but is of type " + functionDecl.ResultType.TypeName + ").");
				return false;
			}

			return true;
		}

		public virtual TypeNode Type
		{
			get
			{
				return TargetType;
			}
		}

		protected internal virtual ArrayTypeNode TargetType
		{
			get
			{
				TypeNode targetType = targetVar.DeclType;
				return (ArrayTypeNode)targetType;
			}
		}

		private TypeNode TypeOfElementToBeExtracted
		{
			get
			{
				if(member != null)
					return member.DeclType;
				return null;
			}
		}

		private TypeNode TypeOfAccumulationElementToBeExtracted
		{
			get
			{
				if(accumulationMember != null)
					return accumulationMember.DeclType;
				return null;
			}
		}

		public override void GetStatements(FunctionDeclNode functionDecl, Function function)
		{
			Entity accessedMember = member.CheckIR<Entity>(typeof(Entity));

			Variable accessedAccumulationMember = accumulationMember.CheckIR<Variable>(typeof(Variable));

			FunctionAutoKeepOneForEachAccumulateBy stmt = new FunctionAutoKeepOneForEachAccumulateBy(
					targetVar.CheckIR<Variable>(typeof(Variable)),
					accessedMember, accessedAccumulationMember, accumulationMethod);
			function.AddStatement(stmt);
		}

		private string FunctionName()
		{
			return "keepOneForEach<" + attribute.IRIdent
					+ ">Accumulate<" + accumulationAttribute.IRIdent
					+ ">By<" + accumulationMethod + ">";
		}
	}

}
