/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast
{

	using System;
	using System.Collections.Generic;

	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using FunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
	using DefinedMatchTypeNode = de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
	using MatchTypeActionNode = de.unika.ipd.grgen.ast.type.MatchTypeActionNode;
	using MatchTypeNode = de.unika.ipd.grgen.ast.type.MatchTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using Ident = de.unika.ipd.grgen.ir.Ident;
	using NestingStatement = de.unika.ipd.grgen.ir.NestingStatement;
	using Function = de.unika.ipd.grgen.ir.executable.Function;
	using Constant = de.unika.ipd.grgen.ir.expr.Constant;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using MatchAccess = de.unika.ipd.grgen.ir.expr.MatchAccess;
	using MatchInit = de.unika.ipd.grgen.ir.expr.MatchInit;
	using Operator = de.unika.ipd.grgen.ir.expr.Operator;
	using OperatorCode = de.unika.ipd.grgen.ir.expr.OperatorCode;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using VariableExpression = de.unika.ipd.grgen.ir.expr.VariableExpression;
	using ArrayInit = de.unika.ipd.grgen.ir.expr.array.ArrayInit;
	using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using Assignment = de.unika.ipd.grgen.ir.stmt.Assignment;
	using ConditionStatement = de.unika.ipd.grgen.ir.stmt.ConditionStatement;
	using ContainerAccumulationYield = de.unika.ipd.grgen.ir.stmt.ContainerAccumulationYield;
	using DefDeclVarStatement = de.unika.ipd.grgen.ir.stmt.DefDeclVarStatement;
	using ReturnStatement = de.unika.ipd.grgen.ir.stmt.ReturnStatement;
	using ArrayVarAddItem = de.unika.ipd.grgen.ir.stmt.array.ArrayVarAddItem;
	using DefinedMatchType = de.unika.ipd.grgen.ir.type.DefinedMatchType;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// AST node that represents a function auto node
	/// </summary>
	public class FunctionAutoJoinNode : FunctionAutoNode
	{
		static FunctionAutoJoinNode()
		{
			SetClassName(typeof(FunctionAutoJoinNode), "function auto");
		}

		private string joinFunction;

		private CollectNode<VarDeclNode> arguments = new CollectNode<VarDeclNode>();
		private CollectNode<IdentNode> argumentsUnresolved = new CollectNode<IdentNode>();

		public FunctionAutoJoinNode(Coords coords, string function, string joinFunction, CollectNode<IdentNode> arguments)
			: base(coords, function)
		{
			this.joinFunction = joinFunction;
			this.argumentsUnresolved = arguments;
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersionCollectNode(argumentsUnresolved, arguments));
				return children;
			}
		}

		/// <summary>
		/// returns names of the children, same order as in getChildren </summary>
		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("arguments");
				return childrenNames;
			}
		}

		private static readonly CollectResolver<VarDeclNode> argumentsResolver =
				new CollectResolver<VarDeclNode>(new DeclarationResolver<VarDeclNode>(typeof(VarDeclNode)));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool successfullyResolved = true;

			arguments = argumentsResolver.Resolve(argumentsUnresolved, this);

			return successfullyResolved;
		}

		public override bool ResolveLocalBypass()
		{
			return ResolveLocal();
		}

		protected internal override bool CheckLocal()
		{
			if(!function.Equals("join"))
			{
				ReportError("Unknown function in auto(), expected join (e.g. join<natural>).");
				return false;
			}

			if(!joinFunction.Equals("natural") && !joinFunction.Equals("cartesian"))
			{
				ReportError("Unknown join function in auto(), only natural and cartesian are supported (giving e.g. join<natural>).");
				return false;
			}

			bool result = true;

			if(arguments.ChildrenExact.Count != 2)
			{
				ReportError(FunctionName() + " must have 2 arguments.");
				result = false;
			}

			int i = 1;
			foreach(VarDeclNode argument in arguments.ChildrenExact)
			{
				if(!(argument.DeclType is ArrayTypeNode))
				{
					ReportError("The " + i + ". argument to " + FunctionName() + "() must be an array.");
					result = false;
					continue;
				}
				ArrayTypeNode argumentType = (ArrayTypeNode)argument.DeclType;
				if(!(argumentType.ElementType is MatchTypeActionNode)
						&& !(argumentType.ElementType is DefinedMatchTypeNode))
				{
					ReportError("The " + i + ". argument to " + FunctionName() + "() must be an array<match<T>> or array<match<class T>>.");
					result = false;
					continue;
				}
				++i;
			}

			VarDeclNode leftArgument = arguments.ChildrenAsList[0];
			ArrayTypeNode leftArrayType = (ArrayTypeNode)leftArgument.DeclType;
			MatchTypeNode leftMatchType = (MatchTypeNode)leftArrayType.valueType;

			VarDeclNode rightArgument = arguments.ChildrenAsList[1];
			ArrayTypeNode rightArrayType = (ArrayTypeNode)rightArgument.DeclType;
			MatchTypeNode rightMatchType = (MatchTypeNode)rightArrayType.valueType;

			ISet<string> sharedNames = GetNamesOfCommonEntities(leftMatchType, rightMatchType);
			foreach(string sharedName in sharedNames)
			{
				TypeNode leftMemberType = leftMatchType.TryGetMember(sharedName).DeclType;
				TypeNode rightMemberType = rightMatchType.TryGetMember(sharedName).DeclType;
				if(!leftMemberType.IsEqual(rightMemberType))
				{
					ReportError("The member " + sharedName
							+ " must be of the same type in " + leftMatchType.ToStringWithDeclarationCoords()
							+ " and in " + rightMatchType.ToStringWithDeclarationCoords()
							+ " (but is of type " + leftMemberType.TypeName + " and " + rightMemberType.TypeName + ").");
				}
			}

			return result;
		}

		public override bool CheckLocalBypass()
		{
			return CheckLocal();
		}

		public virtual ISet<string> GetNamesOfCommonEntities(MatchTypeNode this_, MatchTypeNode that)
		{
			ISet<string> namesFromThis = this_.NamesOfEntities;
			ISet<string> namesFromThat = that.NamesOfEntities;
			namesFromThis.RetainAll(namesFromThat);
			return namesFromThis;
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
			if(!(resultType.ElementType is DefinedMatchTypeNode))
			{
				ReportError("The result type of the function " + functionDecl.Ident
						+ " employing the auto-generated function " + FunctionName()
						+ " must be an array<match<class T>> (but is of type " + functionDecl.ResultType.TypeName + ").");
				return false;
			}

			bool result = true;

			DefinedMatchTypeNode resultMatchType = (DefinedMatchTypeNode)resultType.ElementType;
			foreach(DeclNode resultMember in resultMatchType.Entities)
			{
				string resultMemberName = resultMember.Ident.ToString();
				TypeNode resultMemberType = resultMember.DeclType;
				foreach(VarDeclNode argument in arguments.ChildrenExact)
				{
					ArrayTypeNode argumentType = (ArrayTypeNode)argument.DeclType;
					MatchTypeNode argumentMatchType = (MatchTypeNode)argumentType.ElementType;
					DeclNode argumentMember = argumentMatchType.TryGetMember(resultMemberName);
					if(argumentMember != null)
					{
						TypeNode argumentMemberType = argumentMember.DeclType;
						if(!argumentMemberType.IsEqual(resultMemberType))
						{
							ReportError("The member " + resultMemberName
									+ " must be of the same type in " + resultMatchType.ToStringWithDeclarationCoords()
									+ " and in " + argumentMatchType.ToStringWithDeclarationCoords()
									+ " (but is of type " + resultMemberType.TypeName + " and " + argumentMemberType.TypeName + ").");
							result = false;
						}
					}
				}
			}

			// members of resulting type without counterparts in argument types are ignored / kept untouched
			// (i.e. they contain the C# initialization values)

			return result;
		}

		public override void GetStatements(FunctionDeclNode functionDecl, Function function)
		{
			/* generates code for joining that looks like this example from the queries test:
			function naturalJoin(ref matchesSameCompany:array<match<sameCompany>>, ref matchesSharedInterest:array<match<sharedInterest>>) : array<match<class SameCompanySharedInterest>>
			{
				def ref res:array<match<class SameCompanySharedInterest>> = array<match<class SameCompanySharedInterest>>[];
				for(matchSameCompany:match<sameCompany> in matchesSameCompany)
				{
					for(matchSharedInterest:match<sharedInterest> in matchesSharedInterest)
					{
						if(matchSameCompany.subject == matchSharedInterest.subject && matchSameCompany.person == matchSharedInterest.person) {
							def ref m:match<class SameCompanySharedInterest> = match<class SameCompanySharedInterest>();
							m.subject = matchSameCompany.subject;
							m.person = matchSameCompany.person;
							m.company = matchSameCompany.company;
							m.interest = matchSharedInterest.interest;
							res.add(m);
						}
					}
				}
				return(res);
			}
			*/

			NestingStatement insertionPoint = function;

			ArrayTypeNode resultArrayType = (ArrayTypeNode)functionDecl.resultType;
			DefinedMatchTypeNode resultMatchType = (DefinedMatchTypeNode)resultArrayType.ElementType;

			Ident resultVarIdent = new Ident("res", Coords);
			ArrayType resultVarType = (ArrayType)function.ReturnType;
			PatternGraphLhs fakePatternGraph = PatternGraphLhsNode.Invalid.CheckIR<PatternGraphLhs>(typeof(PatternGraphLhs));
			Variable resultVar = new Variable("res", resultVarIdent, resultVarType,
					true, fakePatternGraph, BaseNode.CONTEXT_FUNCTION, false);
			Expression emptyArray = new ArrayInit(new List<Expression>(), null, resultVarType, true);
			resultVar.Initialization = emptyArray;
			DefDeclVarStatement resultVarDecl = new DefDeclVarStatement(resultVar);
			insertionPoint.AddStatement(resultVarDecl);

			VarDeclNode leftArgument = arguments.ChildrenAsList[0];
			string leftIterationVarName = "$match_" + leftArgument.Ident.ToString();
			Ident leftIterationVarIdent = new Ident(leftIterationVarName, Coords);
			ArrayTypeNode leftArrayType = (ArrayTypeNode)leftArgument.DeclType;
			MatchTypeNode leftMatchType = (MatchTypeNode)leftArrayType.valueType;
			Type leftIterationVarType = leftMatchType.CheckIR<Type>(typeof(Type));
			Variable leftIterationVar = new Variable(leftIterationVarName, leftIterationVarIdent, leftIterationVarType,
					true, fakePatternGraph, BaseNode.CONTEXT_FUNCTION, false);
			ContainerAccumulationYield leftMatchesIteration = new ContainerAccumulationYield(leftIterationVar, null,
					leftArgument.CheckIR<Variable>(typeof(Variable)));
			insertionPoint.AddStatement(leftMatchesIteration);
			insertionPoint = leftMatchesIteration;

			VarDeclNode rightArgument = arguments.ChildrenAsList[1];
			string rightIterationVarName = "$match_" + rightArgument.Ident.ToString();
			Ident rightIterationVarIdent = new Ident(rightIterationVarName, Coords);
			ArrayTypeNode rightArrayType = (ArrayTypeNode)rightArgument.DeclType;
			MatchTypeNode rightMatchType = (MatchTypeNode)rightArrayType.valueType;
			Type rightIterationVarType = rightMatchType.CheckIR<Type>(typeof(Type));
			Variable rightIterationVar = new Variable(rightIterationVarName, rightIterationVarIdent, rightIterationVarType,
					true, fakePatternGraph, BaseNode.CONTEXT_FUNCTION, false);
			ContainerAccumulationYield rightMatchesIteration = new ContainerAccumulationYield(rightIterationVar, null,
					rightArgument.CheckIR<Variable>(typeof(Variable)));
			insertionPoint.AddStatement(rightMatchesIteration);
			insertionPoint = rightMatchesIteration;

			ISet<string> sharedNames = GetNamesOfCommonEntities(leftMatchType, rightMatchType);
			if(joinFunction.Equals("natural"))
			{
				Expression condition = new Constant(BasicTypeNode.booleanType.IRType, true);
				foreach(string sharedName in sharedNames)
				{
					DeclNode leftMemberDecl = leftMatchType.TryGetMember(sharedName);
					Entity leftMember = leftMemberDecl.CheckIR<Entity>(typeof(Entity));

					DeclNode rightMemberDecl = rightMatchType.TryGetMember(sharedName);
					Entity rightMember = rightMemberDecl.CheckIR<Entity>(typeof(Entity));

					Operator opEqual = new Operator(BasicTypeNode.booleanType.IRType, OperatorCode.EQ);
					opEqual.AddOperand(new MatchAccess(new VariableExpression(leftIterationVar), leftMember));
					opEqual.AddOperand(new MatchAccess(new VariableExpression(rightIterationVar), rightMember));

					Operator opAnd = new Operator(BasicTypeNode.booleanType.IRType, OperatorCode.LOG_AND);
					opAnd.AddOperand(condition);
					opAnd.AddOperand(opEqual);

					condition = opAnd;
				}

				ConditionStatement condStmt = new ConditionStatement(condition);
				insertionPoint.AddStatement(condStmt);
				insertionPoint = condStmt;
			}

			Ident matchVarIdent = new Ident("$m", Coords);
			DefinedMatchType matchVarType = (DefinedMatchType)resultVarType.ValueType;
			Variable matchVar = new Variable("$m", matchVarIdent, matchVarType,
					true, fakePatternGraph, BaseNode.CONTEXT_FUNCTION, false);
			Expression matchInit = new MatchInit(matchVarType);
			matchVar.Initialization = matchInit;
			DefDeclVarStatement matchVarDecl = new DefDeclVarStatement(matchVar);
			insertionPoint.AddStatement(matchVarDecl);

			foreach(DeclNode leftMember in leftMatchType.Entities)
			{
				string memberName = leftMember.Ident.ToString();
				if(memberName.StartsWith("$", StringComparison.Ordinal))
					continue;
				if(resultMatchType.TryGetMember(memberName) == null)
					continue;

				Entity matchMember = matchVarType.PatternGraph.TryGetMember(memberName);
				Qualification lhsQual = new Qualification(matchVar, matchMember);
				Qualification rhsQual = new Qualification(leftIterationVar, leftMember.CheckIR<Entity>(typeof(Entity)));
				Assignment assignment = new Assignment(lhsQual, rhsQual);
				insertionPoint.AddStatement(assignment);
			}

			foreach(DeclNode rightMember in rightMatchType.Entities)
			{
				string memberName = rightMember.Ident.ToString();
				if(memberName.StartsWith("$", StringComparison.Ordinal))
					continue;
				if(resultMatchType.TryGetMember(memberName) == null)
					continue;
				if(sharedNames.Contains(memberName))
					continue;

				Entity matchMember = matchVarType.PatternGraph.TryGetMember(memberName);
				Qualification lhsQual = new Qualification(matchVar, matchMember);
				Qualification rhsQual = new Qualification(rightIterationVar, rightMember.CheckIR<Entity>(typeof(Entity)));
				Assignment assignment = new Assignment(lhsQual, rhsQual);
				insertionPoint.AddStatement(assignment);
			}

			Expression matchVarExpr = new VariableExpression(matchVar);
			ArrayVarAddItem arrayAddItem = new ArrayVarAddItem(resultVar, matchVarExpr, null);
			insertionPoint.AddStatement(arrayAddItem);

			Expression returnValueExpr = new VariableExpression(resultVar);
			ReturnStatement returnStmt = new ReturnStatement(returnValueExpr);
			function.AddStatement(returnStmt);
		}

		private string FunctionName()
		{
			return "join<" + joinFunction + ">";
		}
	}

}
