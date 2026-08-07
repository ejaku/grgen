/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.expr
{

using System.Collections.Generic;
using System.Diagnostics;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using ExecVarDeclNode = de.unika.ipd.grgen.ast.decl.ExecVarDeclNode;
using ConstraintDeclNode = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
using EnumItemDeclNode = de.unika.ipd.grgen.ast.model.decl.EnumItemDeclNode;
using MemberDeclNode = de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using de.unika.ipd.grgen.ast.util;
using Entity = de.unika.ipd.grgen.ir.Entity;
using ExecVariable = de.unika.ipd.grgen.ir.ExecVariable;
using ExecVariableExpression = de.unika.ipd.grgen.ir.ExecVariableExpression;
using IR = de.unika.ipd.grgen.ir.IR;
using GraphEntityExpression = de.unika.ipd.grgen.ir.expr.GraphEntityExpression;
using MemberExpression = de.unika.ipd.grgen.ir.expr.MemberExpression;
using VariableExpression = de.unika.ipd.grgen.ir.expr.VariableExpression;
using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

/// <summary>
/// An expression that results from a declared identifier.
/// </summary>
public class DeclExprNode : ExprNode
{
	static DeclExprNode()
	{
		SetClassName(typeof(DeclExprNode), "decl expression");
	}

	public BaseNode declUnresolved; // either EnumExprNode if constructed locally, or IdentNode if constructed from IdentExprNode
	public DeclaredCharacter decl;

	/// <summary>
	/// Make a new declaration expression. </summary>
	/// <param name="coords"> The source code coordinates. </param>
	/// <param name="declCharacter"> Some base node, that is a decl character. </param>
	public DeclExprNode(BaseNode declCharacter)
		: base(declCharacter.Coords)
	{
		this.declUnresolved = declCharacter;
		this.decl = (DeclaredCharacter)declCharacter;
		BecomeParent(this.declUnresolved);
	}

	/// <summary>
	/// Make a new declaration expression from an enum expression. </summary>
	/// <param name="coords"> The source code coordinates. </param>
	/// <param name="declCharacter"> Some base node, that is a decl character. </param>
	public DeclExprNode(EnumExprNode declCharacter)
		: base(declCharacter.Coords)
	{
		this.declUnresolved = declCharacter;
		this.decl = declCharacter;
		BecomeParent(this.declUnresolved);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add((BaseNode)decl);
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
		childrenNames.Add("decl");
		return childrenNames;
		}
	}

	private static MemberResolver<DeclaredCharacter> memberResolver = new MemberResolver<DeclaredCharacter>();

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		if(!(declUnresolved is PackageIdentNode))
			TryFixupDefinition(declUnresolved, declUnresolved.Scope);

		if(!memberResolver.Resolve(declUnresolved))
			return false;

		memberResolver.GetResult(typeof(MemberDeclNode));
		memberResolver.GetResult(typeof(EnumExprNode));
		memberResolver.GetResult(typeof(VarDeclNode));
		memberResolver.GetResult(typeof(ExecVarDeclNode));
		memberResolver.GetResult(typeof(ConstraintDeclNode));
		decl = memberResolver.Result;

		return memberResolver.Finish();
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.expr.ExprNode.getType() "/>
	public override TypeNode Type
	{
		get
		{
		return decl.Decl.DeclType;
		}
	}

	/// <summary>
	/// Gets the ConstraintDeclNode this DeclExprNode resolved to, or null if it is something else.
	/// </summary>
	public virtual ConstraintDeclNode ConstraintDecl
	{
		get
		{
		Debug.Assert(IsResolved());
		if(decl is ConstraintDeclNode)
			return (ConstraintDeclNode)decl;
		return null;
		}
	}

	/// <summary>
	/// returns the node this DeclExprNode was resolved to. </summary>
	public virtual BaseNode ResolvedNode
	{
		get
		{
		Debug.Assert(IsResolved());
		return (BaseNode)decl;
		}
	}

	public virtual bool IsEnumValue()
	{
		return declUnresolved is EnumExprNode;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.expr.ExprNode.evaluate() "/>
	public override ExprNode Evaluate()
	{
		ExprNode res = this;
		DeclNode declNode = decl.Decl;

		if(declNode is EnumItemDeclNode)
			res = ((EnumItemDeclNode)declNode).Value;

		return res;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		return true;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
	protected internal override IR ConstructIR()
	{
		BaseNode declNode = (BaseNode)decl;
		if(declNode is MemberDeclNode)
			return new MemberExpression(declNode.CheckIR(typeof(Entity)));
		else if(declNode is VarDeclNode)
			return new VariableExpression(declNode.CheckIR(typeof(Variable)));
		else if(declNode is ExecVarDeclNode)
			return new ExecVariableExpression(declNode.CheckIR(typeof(ExecVariable)));
		else if(declNode is ConstraintDeclNode)
			return new GraphEntityExpression((GraphEntity)declNode.IR);
		else
			return declNode.IR;
	}

	public override bool NoDefElement(string containingConstruct)
	{
		if(decl is NodeDeclNode)
		{
			NodeDeclNode node = (NodeDeclNode)decl;
			if(node.defEntityToBeYieldedTo)
			{
				declUnresolved.ReportError("A def node (" + node + ")"
						+ " cannot be accessed from a(n) " + containingConstruct + ".");
				return false;
			}
		}
		if(decl is EdgeDeclNode)
		{
			EdgeDeclNode edge = (EdgeDeclNode)decl;
			if(edge.defEntityToBeYieldedTo)
			{
				declUnresolved.ReportError("A def edge (" + edge + ")"
						+ " cannot be accessed from a(n) " + containingConstruct + ".");
				return false;
			}
		}
		if(decl is VarDeclNode)
		{
			VarDeclNode entity = (VarDeclNode)decl;
			if(entity.defEntityToBeYieldedTo && !entity.lambdaExpressionVariable)
			{
				declUnresolved.ReportError("A def variable (" + entity + ")"
						+ " cannot be accessed from a(n) " + containingConstruct + ".");
				return false;
			}
		}
		return true;
	}
}

}
