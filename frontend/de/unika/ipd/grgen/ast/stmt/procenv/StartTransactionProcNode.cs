/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.procenv
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using BuiltinProcedureInvocationBaseNode = de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using StartTransactionProc = de.unika.ipd.grgen.ir.stmt.procenv.StartTransactionProc;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class StartTransactionProcNode : BuiltinProcedureInvocationBaseNode
{
	static StartTransactionProcNode()
	{
		SetClassName(typeof(StartTransactionProcNode), "start transaction procedure");
	}

	internal IList<TypeNode> returnTypes;

	public StartTransactionProcNode(Coords coords)
		: base(coords)
	{
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		return childrenNames;
		}
	}

	protected internal override bool CheckLocal()
	{
		return true;
	}

	public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	protected internal override IR ConstructIR()
	{
		StartTransactionProc startTransaction = new StartTransactionProc(BasicTypeNode.intType.GetIRType());
		return startTransaction;
	}

	public override IList<TypeNode> Type
	{
		get
		{
		if(returnTypes == null)
		{
			returnTypes = new List<TypeNode>();
			returnTypes.Add(BasicTypeNode.intType);
		}
		return returnTypes;
		}
	}
}

}
