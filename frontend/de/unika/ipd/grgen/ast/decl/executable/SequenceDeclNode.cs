/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.decl.executable
{
using System.Collections.Generic;
using System.Diagnostics;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using de.unika.ipd.grgen.ast;
using ExecNode = de.unika.ipd.grgen.ast.ExecNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using ExecVarDeclNode = de.unika.ipd.grgen.ast.decl.ExecVarDeclNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using SequenceTypeNode = de.unika.ipd.grgen.ast.type.executable.SequenceTypeNode;
using de.unika.ipd.grgen.ast.util;
using Exec = de.unika.ipd.grgen.ir.Exec;
using ExecVariable = de.unika.ipd.grgen.ir.ExecVariable;
using IR = de.unika.ipd.grgen.ir.IR;
using Sequence = de.unika.ipd.grgen.ir.executable.Sequence;


/// <summary>
/// AST node for a graph rewrite sequence definition.
/// </summary>
public class SequenceDeclNode : DeclNode
{
	static SequenceDeclNode()
	{
		SetClassName(typeof(SequenceDeclNode), "sequence declaration");
	}

	protected internal SequenceTypeNode type;

	protected internal ExecNode exec;
	public CollectNode<ExecVarDeclNode> inParams;
	public CollectNode<ExecVarDeclNode> outParams;

	/// <summary>
	/// Type for this declaration. </summary>
	private static readonly TypeNode sequenceType = new SequenceTypeNode();

	/// <summary>
	/// Make a sequence definition. </summary>
	public SequenceDeclNode(IdentNode id, ExecNode exec,
			CollectNode<ExecVarDeclNode> inParams, CollectNode<ExecVarDeclNode> outParams)
		: base(id, sequenceType)
	{
		this.exec = exec;
		BecomeParent(this.exec);
		this.inParams = inParams;
		BecomeParent(this.inParams);
		this.outParams = outParams;
		BecomeParent(this.outParams);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(ident);
		children.Add(exec);
		children.Add(inParams);
		children.Add(outParams);
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
		childrenNames.Add("ident");
		childrenNames.Add("exec");
		childrenNames.Add("inParams");
		childrenNames.Add("outParams");
		return childrenNames;
		}
	}

	protected internal static readonly DeclarationTypeResolver<SequenceTypeNode> typeResolver =
			new DeclarationTypeResolver<SequenceTypeNode>(typeof(SequenceTypeNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		type = typeResolver.Resolve(typeUnresolved, this);

		return type != null;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		return true;
	}

	/// <summary>
	/// Returns the IR object for this sequence node. </summary>
	public virtual Sequence IRSequence
	{
		get
		{
		return CheckIR(typeof(Sequence));
		}
	}

	public virtual IList<DeclNode> ParamDecls
	{
		get
		{
		return new List<DeclNode>(inParams.ChildrenExact);
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
	protected internal override IR ConstructIR()
	{
		Sequence sequence = new Sequence(Ident.IRIdent, exec.CheckIR(typeof(Exec)));
		foreach(ExecVarDeclNode inParam in inParams.ChildrenExact)
			sequence.AddInParam(inParam.CheckIR(typeof(ExecVariable)));
		foreach(ExecVarDeclNode outParam in outParams.ChildrenExact)
			sequence.AddOutParam(outParam.CheckIR(typeof(ExecVariable)));
		return sequence;
	}

	public override TypeNode DeclType
	{
		get
		{
		Debug.Assert(IsResolved());

		return type;
		}
	}

	public static string KindStr
	{
		get
		{
		return "sequence";
		}
	}
}

}
