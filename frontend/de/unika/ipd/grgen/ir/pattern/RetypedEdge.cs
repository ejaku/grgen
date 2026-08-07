/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.pattern
{
using Entity = de.unika.ipd.grgen.ir.Entity;
using Ident = de.unika.ipd.grgen.ir.Ident;
using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
using Annotations = de.unika.ipd.grgen.util.Annotations;
using Retyped = de.unika.ipd.grgen.util.Retyped;

public class RetypedEdge : Edge, Retyped
{
	/// <summary>
	///  The original edge </summary>
	public Edge oldEdge = null;

	public RetypedEdge(Ident ident, EdgeType type, Annotations annots,
			bool maybeDeleted, bool maybeRetyped, bool isDefToBeYieldedTo, int context)
		: base(ident, type, annots, null, maybeDeleted, maybeRetyped, isDefToBeYieldedTo, context)
	{
	}

	public virtual Entity OldEntity
	{
		get
		{
		return oldEdge;
		}
		set
		{
		this.oldEdge = (Edge)value;
		}
	}


	/// <summary>
	/// returns the original edge in the graph. </summary>
	public virtual Edge OldEdge
	{
		get
		{
		return oldEdge;
		}
		set
		{
		this.oldEdge = value;
		}
	}


	public override bool IsRetyped()
	{
		return true;
	}
}

}
