/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.util
{
	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using CollectBaseNode = de.unika.ipd.grgen.ast.CollectBaseNode;
	using de.unika.ipd.grgen.ast;

	/// <summary>
	/// A resolver, that resolves a source AST CollectNode into four target AST
	/// CollectNode of type R, S, T  and U by using a given resolver.
	/// </summary>
	public class CollectQuadrupleResolver<R, S, T, U> where R : de.unika.ipd.grgen.ast.BaseNode where S : de.unika.ipd.grgen.ast.BaseNode where T : de.unika.ipd.grgen.ast.BaseNode where U : de.unika.ipd.grgen.ast.BaseNode
	{
		private Resolver<Quadruple<R, S, T, U>> resolver;

		public CollectQuadrupleResolver(Resolver<Quadruple<R, S, T, U>> resolver)
		{
			this.resolver = resolver;
		}

		/// <summary>
		/// resolves the collect node to collect nodes of type R, S, T and U via
		/// the given resolver
		/// </summary>
		public virtual Quadruple<CollectNode<R>, CollectNode<S>, CollectNode<T>, CollectNode<U>> Resolve(CollectBaseNode collect)
		{
			CollectNode<R> first = null;
			CollectNode<S> second = null;
			CollectNode<T> third = null;
			CollectNode<U> fourth = null;

			foreach(BaseNode child in collect.Children)
			{
				Quadruple<R, S, T, U> quadruple = resolver.Resolve(child, collect);
				if(quadruple == null)
					return null;
				if(quadruple.first != null)
				{
					if(first == null)
					{
						first = new CollectNode<R>();
						first.Coords = collect.Coords;
					}
					first.AddChild(quadruple.first);
				}
				if(quadruple.second != null)
				{
					if(second == null)
					{
						second = new CollectNode<S>();
						second.Coords = collect.Coords;
					}
					second.AddChild(quadruple.second);
				}
				if(quadruple.third != null)
				{
					if(third == null)
					{
						third = new CollectNode<T>();
						third.Coords = collect.Coords;
					}
					third.AddChild(quadruple.third);
				}
				if(quadruple.fourth != null)
				{
					if(fourth == null)
					{
						fourth = new CollectNode<U>();
						fourth.Coords = collect.Coords;
					}
					fourth.AddChild(quadruple.fourth);
				}
			}

			Quadruple<CollectNode<R>, CollectNode<S>, CollectNode<T>, CollectNode<U>> res = new Quadruple<CollectNode<R>, CollectNode<S>, CollectNode<T>, CollectNode<U>>();
			res.first = first;
			res.second = second;
			res.third = third;
			res.fourth = fourth;

			return res;
		}
	}

}
