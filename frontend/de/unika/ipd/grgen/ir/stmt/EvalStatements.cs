/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.stmt
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ir;

public class EvalStatements : IR
{
	public IList<EvalStatement> evalStatements = new List<EvalStatement>();

	public EvalStatements(string name)
		: base(name)
	{
	}

	/// <summary>
	/// Method collectNeededEntities extracts the nodes, edges, and variables occurring in this Expression.
	/// We don't collect global variables (::-prefixed), as no entities and no processing are needed for them at all, they are only accessed. </summary>
	/// <param name="needs"> A NeededEntities instance aggregating the needed elements. </param>
	public virtual void CollectNeededEntities(NeededEntities needs)
	{
		foreach(EvalStatement evalStatement in evalStatements)
			evalStatement.CollectNeededEntities(needs);
	}
}

}
