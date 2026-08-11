/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.pattern
{

	using System.Collections.Generic;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using de.unika.ipd.grgen.ast;
	using IR = de.unika.ipd.grgen.ir.IR;
	using OrderedReplacement = de.unika.ipd.grgen.ir.pattern.OrderedReplacement;
	using OrderedReplacements = de.unika.ipd.grgen.ir.pattern.OrderedReplacements;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class OrderedReplacementsNode : BaseNode
	{
		public string name;
		public CollectNode<OrderedReplacementNode> orderedReplacements;

		public OrderedReplacementsNode(Coords coords, string name)
			: base(coords)
		{
			this.name = name;
			orderedReplacements = new CollectNode<OrderedReplacementNode>();
		}

		public virtual void AddChild(OrderedReplacementNode c)
		{
			orderedReplacements.AddChild(c);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				return orderedReplacements.Children;
			}
		}

		public virtual ICollection<OrderedReplacementNode> ChildrenExact
		{
			get
			{
				return orderedReplacements.ChildrenExact;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> res = new List<string>();
				for(int i = 0; i < Children.Count; ++i)
					res.Add("eval" + i);
				return res;
			}
		}

		protected internal override bool ResolveLocal()
		{
			return true;
		}

		protected internal override bool CheckLocal()
		{
			return true;
		}

		public virtual bool NoExecStatement()
		{
			bool res = true;
			foreach(OrderedReplacementNode orderedReplacement in orderedReplacements.ChildrenExact)
				res &= orderedReplacement.NoExecStatement(true);
			return res;
		}

		protected internal override IR ConstructIR()
		{
			OrderedReplacements ors = new OrderedReplacements(name);

			foreach(OrderedReplacementNode orderedReplacement in orderedReplacements.ChildrenExact)
				ors.orderedReplacements.Add((OrderedReplacement)orderedReplacement.IR);

			return ors;
		}
	}

}
