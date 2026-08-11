/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.pattern
{

using System.Collections.Generic;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using de.unika.ipd.grgen.ast;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using ConstraintDeclNode = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
using RhsDeclNode = de.unika.ipd.grgen.ast.decl.pattern.RhsDeclNode;
using SubpatternUsageDeclNode = de.unika.ipd.grgen.ast.decl.pattern.SubpatternUsageDeclNode;
using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using IdentExprNode = de.unika.ipd.grgen.ast.expr.IdentExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using SubpatternDependentReplacement = de.unika.ipd.grgen.ir.pattern.SubpatternDependentReplacement;
using SubpatternUsage = de.unika.ipd.grgen.ir.pattern.SubpatternUsage;

public class SubpatternReplNode : OrderedReplacementNode
{
	static SubpatternReplNode()
	{
		SetClassName(typeof(SubpatternReplNode), "subpattern repl node");
	}

	private IdentNode subpatternUnresolved;
	private SubpatternUsageDeclNode subpattern;
	private CollectNode<ExprNode> replConnections;

	public SubpatternReplNode(IdentNode n, CollectNode<ExprNode> c)
	{
		this.subpatternUnresolved = n;
		BecomeParent(this.subpatternUnresolved);
		this.replConnections = c;
		BecomeParent(this.replConnections);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(GetValidVersion(subpatternUnresolved, subpattern));
			children.Add(replConnections);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("subpattern");
			childrenNames.Add("replConnections");
			return childrenNames;
		}
	}

	private static readonly DeclarationResolver<SubpatternUsageDeclNode> subpatternResolver =
			new DeclarationResolver<SubpatternUsageDeclNode>(typeof(SubpatternUsageDeclNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		subpattern = subpatternResolver.Resolve(subpatternUnresolved, this);
		return subpattern != null;
	}

	protected internal override bool CheckLocal()
	{
		RhsDeclNode right = subpattern.type.right;
		string patternName = subpattern.type.pattern.nameOfGraph;

		if((subpattern.context & CONTEXT_LHS_OR_RHS) != CONTEXT_LHS)
		{
			subpatternUnresolved.ReportError("A subpattern rewrite application"
					+ " can only be given for a subpattern entity declaration from the pattern part,"
					+ " but is given for a subpattern entity declaration from the rewrite part"
					+ " (this occurs for " + subpatternUnresolved + "())"
					+ " (only a subpattern entity matched can be rewritten, not one just created).");
			return false;
		}

		// check whether the used pattern contains one rhs
		if(right == null)
		{
			subpattern.type.pattern.ReportError("No rewrite part specified in subpattern " + patternName
					+ " (which is referenced by the subpattern rewrite application " + subpatternUnresolved + ").");
			return false;
		}

		return CheckSubpatternSignatureAdhered();
	}

	/// <summary>
	/// Check whether the subpattern replacement usage adheres to the signature of the subpattern replacement declaration </summary>
	private bool CheckSubpatternSignatureAdhered()
	{
		// check if the number of parameters is correct
		PatternGraphLhsNode pattern = subpattern.type.pattern;
		RhsDeclNode right = subpattern.type.right;
		IList<DeclNode> formalReplacementParameters = right.patternGraph.ParamDecls;
		int expected = formalReplacementParameters.Count;
		int actual = replConnections.Size();
		if(expected != actual)
		{
			subpattern.ident.ReportError("The rewrite part specified in " + pattern.ToStringWithDeclarationCoords() + " expects "
					+ expected + " parameters, but given by the subpattern rewrite application " + subpatternUnresolved + " are "
					+ actual + " arguments.");
			return false;
		}

		// check if the types of the parameters are correct
		bool res = true;
		for(int i = 0; i < formalReplacementParameters.Count; ++i)
		{
			ExprNode actualParameter = replConnections.Get(i);
			DeclNode formalParameter = formalReplacementParameters[i];
			if(actualParameter is IdentExprNode && ((IdentExprNode)actualParameter).yieldedTo)
				res &= CheckYieldedToParameter(i, actualParameter, formalParameter);
			else
				res &= CheckParameter(i, actualParameter, formalParameter);
		}
		return res;
	}

	private bool CheckYieldedToParameter(int i, ExprNode actualParameter, DeclNode formalParameter)
	{
		bool res = true;

		PatternGraphLhsNode pattern = subpattern.type.pattern;

		TypeNode actualParameterType = actualParameter.Type;
		TypeNode formalParameterType = formalParameter.DeclType;

		if(formalParameter is ConstraintDeclNode)
		{
			ConstraintDeclNode parameterElement = (ConstraintDeclNode)formalParameter;
			if(!parameterElement.defEntityToBeYieldedTo)
			{
				res = false;
				subpatternUnresolved.ReportError("The " + (i + 1) + ". argument to the subpattern rewrite application " + subpatternUnresolved + " is yielded to,"
						+ " but the rewrite parameter at this position is not declared as def "
						+ "(" + parameterElement.Ident + " in " + pattern.ToStringWithDeclarationCoords() + ")" + ".");
			}
		}
		else
		{ //if(formalParameter instanceof VarDeclNode)
			VarDeclNode parameterVar = (VarDeclNode)formalParameter;
			if(!parameterVar.defEntityToBeYieldedTo)
			{
				res = false;
				subpatternUnresolved.ReportError("The " + (i + 1) + ". argument to the subpattern rewrite application " + subpatternUnresolved + " is yielded to,"
						+ " but the rewrite parameter at this position is not declared as def "
						+ "(" + parameterVar.Ident + " in " + pattern.ToStringWithDeclarationCoords() + ")" + ".");
			}
		}

		BaseNode argument = ((IdentExprNode)actualParameter).ResolvedNode;
		if(argument is VarDeclNode)
		{
			VarDeclNode argumentVar = (VarDeclNode)argument;
			if((argumentVar.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS)
			{
				res = false;
				subpatternUnresolved.ReportError("Cannot yield from a subpattern rewrite application (" + (i + 1) + " argument of " + subpatternUnresolved + ") in the rewrite part"
						+ " to a def variable in the pattern part"
						+ " (" + argumentVar.Ident + " was declared in the pattern part).");
			}
		}
		else
		{ //if(argument instanceof ConstraintDeclNode)
			ConstraintDeclNode argumentElement = (ConstraintDeclNode)argument;
			if((argumentElement.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS)
			{
				res = false;
				subpatternUnresolved.ReportError("Cannot yield from a subpattern rewrite application (" + (i + 1) + " argument of " + subpatternUnresolved + ") in the rewrite part"
						+ " to a def graph element in the pattern part"
						+ " (" + argumentElement.Ident + " was declared in the pattern part).");
			}
		}

		if(!formalParameterType.IsCompatibleTo(actualParameterType))
		{
			res = false;
			subpatternUnresolved.ReportError("The " + (i + 1) + ". argument of type " + actualParameterType.TypeName
					+ " of the subpattern rewrite application " + subpatternUnresolved
					+ " cannot be yielded to from the rewrite def parameter of incompatible type " + formalParameterType.TypeName
					+ " (" + formalParameter.Ident + " of subpattern " + pattern.ToStringWithDeclarationCoords() + ")"
					+ actualParameterType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
					+ formalParameterType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
					+ ".");
		}

		return res;
	}

	private bool CheckParameter(int i, ExprNode actualParameter, DeclNode formalParameter)
	{
		bool res = true;

		PatternGraphLhsNode pattern = subpattern.type.pattern;

		TypeNode actualParameterType = actualParameter.Type;
		TypeNode formalParameterType = formalParameter.DeclType;

		if(formalParameter is ConstraintDeclNode)
		{
			ConstraintDeclNode parameterElement = (ConstraintDeclNode)formalParameter;
			if(parameterElement.defEntityToBeYieldedTo)
			{
				res = false;
				subpatternUnresolved.ReportError("The " + (i + 1) + ". argument of the subpattern rewrite application " + subpatternUnresolved + " is not yielded to,"
						+ " but the rewrite parameter at this position is declared as def (" + parameterElement.Ident + " in " + pattern.ToStringWithDeclarationCoords() + ")" + ".");
			}
		}
		else
		{ //if(formalParameter instanceof VarDeclNode)
			VarDeclNode parameterVar = (VarDeclNode)formalParameter;
			if(parameterVar.defEntityToBeYieldedTo)
			{
				res = false;
				subpatternUnresolved.ReportError("The " + (i + 1) + ". argument of the subpattern rewrite application " + subpatternUnresolved + " is not yielded to,"
						+ " but the rewrite parameter at this position is declared as def (" + parameterVar.Ident + " in " + pattern.ToStringWithDeclarationCoords() + ")" + ".");
			}
		}

		if(!actualParameterType.IsCompatibleTo(formalParameterType))
		{
			res = false;
			subpatternUnresolved.ReportError("Cannot convert " + (i + 1) + ". argument of the subpattern rewrite application " + subpatternUnresolved + " from "
					+ actualParameterType.TypeName + " to " + formalParameterType.TypeName
					+ " (expected by the rewrite parameter " + formalParameter.Ident + " of subpattern " + pattern.ToStringWithDeclarationCoords() + ")"
					+ actualParameterType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
					+ formalParameterType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
					+ ".");
		}

		return res;
	}

	public virtual IdentNode SubpatternIdent
	{
		get
		{
			return subpatternUnresolved;
		}
	}

	protected internal override IR ConstructIR()
	{
		IList<Expression> replConnections = new List<Expression>();
		foreach(ExprNode e in this.replConnections.ChildrenExact)
		{
			e = e.Evaluate();
			replConnections.Add(e.CheckIR(typeof(Expression)));
		}
		return new SubpatternDependentReplacement("dependent replacement", subpatternUnresolved.IRIdent,
				subpattern.CheckIR(typeof(SubpatternUsage)), replConnections);
	}
}

}
