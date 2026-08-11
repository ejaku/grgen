/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt
{
using de.unika.ipd.grgen.ir;
using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;

/// <summary>
/// Represents a declaration of a local variable of graph element type in the IR.
/// </summary>
public class DefDeclGraphEntityStatement : EvalStatement
{
	private GraphEntity target;

	public DefDeclGraphEntityStatement(GraphEntity target)
		: base("def decl graph entity")
	{
		this.target = target;
	}

	public virtual GraphEntity Target
	{
		get
		{
		return target;
		}
	}

	public override string ToString()
	{
		return target.Ident.ToString();
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		//needs.add(target); needed?
	}
}

}
