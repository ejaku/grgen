/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ast.stmt
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using ConstraintDeclNode = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
using ConnectionNode = de.unika.ipd.grgen.ast.pattern.ConnectionNode;
using SingleNodeConnNode = de.unika.ipd.grgen.ast.pattern.SingleNodeConnNode;
using de.unika.ipd.grgen.ast.util;
using DefDeclGraphEntityStatement = de.unika.ipd.grgen.ir.stmt.DefDeclGraphEntityStatement;
using DefDeclVarStatement = de.unika.ipd.grgen.ir.stmt.DefDeclVarStatement;
using IR = de.unika.ipd.grgen.ir.IR;
using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// AST node representing a def declaration statement node (a variable that can be assigned in an attribute evaluation statements).
/// </summary>
public class DefDeclStatementNode : EvalStatementNode
{
	static DefDeclStatementNode()
	{
		SetClassName(typeof(DefDeclStatementNode), "def decl statement");
	}

	internal BaseNode defDeclUnresolved;
	internal int context;

	internal VarDeclNode defDeclVar;
	internal ConstraintDeclNode defDeclGraphElement;

	public DefDeclStatementNode(Coords coords, BaseNode target, int context)
		: base(coords)
	{
		this.defDeclUnresolved = target;
		BecomeParent(this.defDeclUnresolved);
		this.context = context;
	}

	public DefDeclStatementNode(Coords coords, VarDeclNode defDeclVar, int context)
		: base(coords)
	{
		this.defDeclVar = defDeclVar;
		this.context = context;
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(GetValidVersion(defDeclUnresolved, defDeclVar, defDeclGraphElement));
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
		childrenNames.Add("defDecl");
		return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool successfullyResolved = true;
		DeclNode decl = Decl;
		if(decl.typeUnresolved is PackageIdentNode)
			Resolver.ResolveOwner((PackageIdentNode)decl.typeUnresolved);
		else
			FixupDefinition(decl.typeUnresolved, decl.typeUnresolved.Scope);
		successfullyResolved = decl.Resolve();
		return successfullyResolved;
	}

	protected internal override bool CheckLocal()
	{
		return true;
	}

	public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	public virtual DeclNode Decl
	{
		get
		{
		if(defDeclUnresolved == null)
			return defDeclVar;

		if(defDeclUnresolved is VarDeclNode)
		{
			defDeclVar = (VarDeclNode)defDeclUnresolved;
			return defDeclVar;
		}
		else if(defDeclUnresolved is SingleNodeConnNode)
		{
			SingleNodeConnNode sncn = (SingleNodeConnNode)defDeclUnresolved;
			defDeclGraphElement = (NodeDeclNode)sncn.nodeUnresolved;
			return defDeclGraphElement;
		}
		else if(defDeclUnresolved is ConstraintDeclNode)
		{
			defDeclGraphElement = (ConstraintDeclNode)defDeclUnresolved;
			return defDeclGraphElement;
		}
		else
		{
			ConnectionNode cn = (ConnectionNode)defDeclUnresolved;
			defDeclGraphElement = ((EdgeDeclNode)cn.edgeUnresolved);
			return defDeclGraphElement;
		}
		}
	}

	protected internal override IR ConstructIR()
	{
		// potential initialization is attached to the Var or the GraphEntity
		if(defDeclVar != null)
		{
			Variable var = defDeclVar.CheckIR(typeof(Variable));
			return new DefDeclVarStatement(var);
		}
		else
		{
			GraphEntity graphEntity = defDeclGraphElement.CheckIR(typeof(GraphEntity));
			return new DefDeclGraphEntityStatement(graphEntity);
		}
	}
}

}
