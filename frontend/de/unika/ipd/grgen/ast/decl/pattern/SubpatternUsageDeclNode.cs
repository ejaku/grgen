/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.decl.pattern
{

using System.Collections.Generic;
using System.Diagnostics;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using de.unika.ipd.grgen.ast;
using DeclaredCharacter = de.unika.ipd.grgen.ast.DeclaredCharacter;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using PackageIdentNode = de.unika.ipd.grgen.ast.PackageIdentNode;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using SubpatternDeclNode = de.unika.ipd.grgen.ast.decl.executable.SubpatternDeclNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using IdentExprNode = de.unika.ipd.grgen.ast.expr.IdentExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using Rule = de.unika.ipd.grgen.ir.executable.Rule;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using SubpatternUsage = de.unika.ipd.grgen.ir.pattern.SubpatternUsage;

public class SubpatternUsageDeclNode : DeclNode
{
	static SubpatternUsageDeclNode()
	{
		SetClassName(typeof(SubpatternUsageDeclNode), "subpattern node");
	}

	private CollectNode<ExprNode> connections;

	public SubpatternDeclNode type = null;
	public int context;

	public SubpatternUsageDeclNode(IdentNode n, BaseNode t, int context, CollectNode<ExprNode> c)
		: base(n, t)
	{
		this.context = context;
		this.connections = c;
		BecomeParent(this.connections);
	}

	public override TypeNode DeclType
	{
		get
		{
			Debug.Assert(IsResolved());

			return type.DeclType;
		}
	}

	public virtual SubpatternDeclNode SubpatternDecl
	{
		get
		{
			Debug.Assert(IsResolved());

			return type;
		}
	}

	public virtual int Context
	{
		get
		{
			return context;
		}
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(ident);
			children.Add(GetValidVersion(typeUnresolved, type));
			children.Add(connections);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("ident");
			childrenNames.Add("type");
			childrenNames.Add("connections");
			return childrenNames;
		}
	}

	private static readonly DeclarationResolver<SubpatternDeclNode> actionResolver =
			new DeclarationResolver<SubpatternDeclNode>(typeof(SubpatternDeclNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		if(!(typeUnresolved is PackageIdentNode))
			FixupDefinition((IdentNode)typeUnresolved, typeUnresolved.Scope);
		type = actionResolver.Resolve(typeUnresolved, this);
		return type != null;
	}

	protected internal override bool CheckLocal()
	{
		return CheckSubpatternSignatureAdhered();
	}

	/// <summary>
	/// Check whether the subpattern usage adheres to the signature of the subpattern declaration </summary>
	private bool CheckSubpatternSignatureAdhered()
	{
		// check if the number of parameters are correct
		int expected = type.pattern.ParamDecls.Count;
		int actual = connections.ChildrenExact.Count;
		if(expected != actual)
		{
			string patternName = type.ident.ToString();
			ident.ReportError("The (sub)pattern " + patternName + type.DeclarationCoords + " expects " + expected
					+ " arguments, given by the subpattern usage" + EmptyWhenAnonymousPostfix(" ") + " are " + actual + " arguments.");
			return false;
		}

		// check if the types of the parameters are correct
		bool res = true;
		IList<DeclNode> formalParameters = type.pattern.ParamDecls;
		for(int i = 0; i < connections.Size(); ++i)
		{
			ExprNode actualParameter = connections.Get(i);
			DeclNode formalParameter = formalParameters[i];
			if(actualParameter is IdentExprNode && ((IdentExprNode)actualParameter).yieldedTo)
				res &= CheckYieldedToParameter(i, actualParameter, formalParameter);
			else
				res &= CheckParameter(i, actualParameter, formalParameter);
			res &= CheckDefArgument(i, actualParameter, formalParameter);
		}
		return res;
	}

	private bool CheckYieldedToParameter(int i, ExprNode actualParameter, DeclNode formalParameter)
	{
		bool res = true;

		TypeNode actualParameterType = actualParameter.Type;
		TypeNode formalParameterType = formalParameter.DeclType;

		if(formalParameter is ConstraintDeclNode)
		{
			ConstraintDeclNode parameterElement = (ConstraintDeclNode)formalParameter;
			if(!parameterElement.defEntityToBeYieldedTo)
			{
				res = false;
				ident.ReportError("The " + (i + 1) + ". subpattern usage argument is yielded to, "
						+ "but the parameter at this position is not declared as def"
						+ WhenUsingSpecification(parameterElement) + ".");
			}
		}
		else
		{ //if(formalParameter instanceof VarDeclNode)
			VarDeclNode parameterVar = (VarDeclNode)formalParameter;
			if(!parameterVar.defEntityToBeYieldedTo)
			{
				res = false;
				ident.ReportError("The " + (i + 1) + ". subpattern usage argument is yielded to, "
						+ "but the parameter at this position is not declared as def"
						+ WhenUsingSpecification(parameterVar) + ".");
			}
		}

		DeclaredCharacter argument = ((IdentExprNode)actualParameter).decl;
		if(argument is VarDeclNode)
		{
			VarDeclNode argumentVar = (VarDeclNode)argument;
			if(!argumentVar.defEntityToBeYieldedTo)
			{
				res = false;
				ident.ReportError("Cannot yield to non-def arguments - the " + (i + 1)
						+ ". subpattern usage argument is yielded to but not declared as def"
						+ WhenUsingSpecification(argumentVar) + ".");
			}
		}
		else
		{ //if(argument instanceof ConstraintDeclNode)
			ConstraintDeclNode argumentElement = (ConstraintDeclNode)argument;
			if(!argumentElement.defEntityToBeYieldedTo)
			{
				res = false;
				ident.ReportError("Cannot yield to non-def arguments - the " + (i + 1)
						+ ". subpattern usage argument is yielded to but not declared as def"
						+ WhenUsingSpecification(argumentElement) + ".");
			}
		}

		if(!formalParameterType.IsCompatibleTo(actualParameterType))
		{
			res = false;
			string exprTypeName = actualParameterType.TypeName;
			string paramTypeName = formalParameterType.TypeName;
			ident.ReportError("The " + (i + 1) + ". subpattern usage argument of type " + exprTypeName
					+ " cannot be yielded to from the subpattern def parameter of incompatible type " + paramTypeName
					+ WhenUsingSpecification(null)
					+ actualParameterType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
					+ formalParameterType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
					+ ".");
		}

		return res;
	}

	private bool CheckParameter(int i, ExprNode actualParameter, DeclNode formalParameter)
	{
		bool res = true;

		TypeNode actualParameterType = actualParameter.Type;
		TypeNode formalParameterType = formalParameter.DeclType;

		if(formalParameter is ConstraintDeclNode)
		{
			ConstraintDeclNode parameterElement = (ConstraintDeclNode)formalParameter;
			if(parameterElement.defEntityToBeYieldedTo)
			{
				res = false;
				ident.ReportError("The " + (i + 1) + ". subpattern usage argument is not yielded to, "
						+ "but the parameter at this position is declared as def"
						+ WhenUsingSpecification(parameterElement) + ".");
			}
		}
		else
		{ //if(formalParameter instanceof VarDeclNode)
			VarDeclNode parameterVar = (VarDeclNode)formalParameter;
			if(parameterVar.defEntityToBeYieldedTo)
			{
				res = false;
				ident.ReportError("The " + (i + 1) + ". subpattern usage argument is not yielded to, "
						+ "but the parameter at this position is declared as def"
						+ WhenUsingSpecification(parameterVar) + ".");
			}
		}

		if(!actualParameterType.IsCompatibleTo(formalParameterType))
		{
			res = false;
			string exprTypeName = actualParameterType.TypeName;
			string paramTypeName = formalParameterType.TypeName;
			ident.ReportError("Cannot convert " + (i + 1) + ". subpattern usage argument from " + exprTypeName
					+ " to the expected " + paramTypeName
					+ WhenUsingSpecification(null)
					+ actualParameterType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
					+ formalParameterType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
					+ ".");
		}

		return res;
	}

	private bool CheckDefArgument(int i, ExprNode actualParameter, DeclNode formalParameter)
	{
		if(!(actualParameter is IdentExprNode))
			return true;

		DeclaredCharacter argument = ((IdentExprNode)actualParameter).decl;
		if(argument is VarDeclNode)
		{
			VarDeclNode argumentVar = (VarDeclNode)argument;
			if(argumentVar.defEntityToBeYieldedTo)
			{
				if(formalParameter is VarDeclNode)
				{
					VarDeclNode parameterVar = (VarDeclNode)formalParameter;
					if(!parameterVar.defEntityToBeYieldedTo)
					{
						ident.ReportError("Cannot use def elements as non-def arguments to subpatterns"
								+ " - the " + (i + 1) + ". subpattern usage argument is declared as def,"
								+ " but the parameter at this position is not declared as def"
								+ WhenUsingSpecification(parameterVar) + ".");
						return false;
					}
				}
			}
		}
		else
		{ //if(argument instanceof ConstraintDeclNode)
			ConstraintDeclNode argumentElement = (ConstraintDeclNode)argument;
			if(argumentElement.defEntityToBeYieldedTo)
			{
				if(formalParameter is ConstraintDeclNode)
				{
					ConstraintDeclNode parameterElement = (ConstraintDeclNode)formalParameter;
					if(!parameterElement.defEntityToBeYieldedTo)
					{
						ident.ReportError("Cannot use def elements as non-def arguments to subpatterns"
								+ " - the " + (i + 1) + ". subpattern usage argument is declared as def,"
								+ " but the parameter at this position is not declared as def"
								+ WhenUsingSpecification(parameterElement) + ".");
						return false;
					}
				}
			}
		}

		return true;
	}

	private string WhenUsingSpecification(DeclNode focusedElement)
	{
		return " (" + (focusedElement != null ? focusedElement.Ident + " " : "")
				+ "when using " + type.ToStringWithDeclarationCoords() + EmptyWhenAnonymous(" by " + ident)
				+ ")";
	}

	protected internal override IR ConstructIR()
	{
		IList<Expression> subpatternConnections = new List<Expression>();
		IList<Expression> subpatternYields = new List<Expression>();
		foreach(ExprNode connection in connections.ChildrenExact)
		{
			ExprNode connectionEvaluated = connection.Evaluate();
			if(connectionEvaluated is IdentExprNode && ((IdentExprNode)connectionEvaluated).yieldedTo)
				subpatternYields.Add(connectionEvaluated.CheckIR(typeof(Expression)));
			else
				subpatternConnections.Add(connectionEvaluated.CheckIR(typeof(Expression)));
		}
		return new SubpatternUsage("subpattern", Ident.IRIdent, type.CheckIR(typeof(Rule)),
				subpatternConnections, subpatternYields);
	}
}

}
