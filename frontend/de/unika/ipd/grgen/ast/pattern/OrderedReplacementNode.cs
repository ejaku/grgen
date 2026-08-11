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
	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public abstract class OrderedReplacementNode : BaseNode
	{
		// no functionality, allows ordering of subpattern replacement nodes and emit here nodes
		// in one container of the ordered replacement node type

		protected internal OrderedReplacementNode(Coords coords)
			: base(coords)
		{
		}

		protected internal OrderedReplacementNode()
			: base()
		{
		}

		public virtual bool NoExecStatement(bool inEvalHereContext)
		{
			bool res = true;
			foreach(BaseNode child in Children)
			{
				if(!(child is OrderedReplacementNode))
					continue;
				OrderedReplacementNode orderedReplacement = (OrderedReplacementNode)child;
				res &= orderedReplacement.NoExecStatement(inEvalHereContext);
			}
			return res;
		}
	}

}
