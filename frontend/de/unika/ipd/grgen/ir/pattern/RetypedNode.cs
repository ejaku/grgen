/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.pattern
{

using System;
using System.Collections.Generic;

using Entity = de.unika.ipd.grgen.ir.Entity;
using Ident = de.unika.ipd.grgen.ir.Ident;
using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;
using Annotations = de.unika.ipd.grgen.util.Annotations;
using Retyped = de.unika.ipd.grgen.util.Retyped;

public class RetypedNode : Node, Retyped
{
	/// <summary>
	///  The original entity if this is a retyped entity </summary>
	protected internal Node oldNode = null;

	/// <summary>
	/// A list of nodes to be additionally merged into the retyped node </summary>
	private readonly List<Node> mergees = new List<Node>();

	public RetypedNode(Ident ident, NodeType type, Annotations annots,
			bool maybeDeleted, bool maybeRetyped, bool isDefToBeYieldedTo, int context)
		: base(ident, type, annots, null, maybeDeleted, maybeRetyped, isDefToBeYieldedTo, context)
	{
	}

	public virtual Entity OldEntity
	{
		get
		{
		return oldNode;
		}
		set
		{
		this.oldNode = (Node)value;
		}
	}


	/// <summary>
	/// returns the original node in the graph. </summary>
	public virtual Node OldNode
	{
		get
		{
		return oldNode;
		}
		set
		{
		this.oldNode = value;
		}
	}


	public override bool IsRetyped()
	{
		return true;
	}

	public virtual void AddMergee(Node mergee)
	{
		mergees.Add(mergee);
	}

	public virtual IList<Node> Mergees
	{
		get
		{
		return mergees.AsReadOnly();
		}
	}

	public virtual int CombinedDependencyLevel
	{
		get
		{
		int depLevel = oldNode.DependencyLevel;
		foreach(Node mergee in mergees)
			depLevel = Math.Max(depLevel, mergee.DependencyLevel);
		return depLevel;
		}
	}
}

}
