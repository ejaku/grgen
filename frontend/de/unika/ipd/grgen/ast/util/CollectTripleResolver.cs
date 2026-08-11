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
	/// A resolver, that resolves a source AST CollectNode into three target AST
	/// CollectNode of type R, S and T by using a given resolver.
	/// </summary>
	public class CollectTripleResolver<R, S, T> where R : de.unika.ipd.grgen.ast.BaseNode where S : de.unika.ipd.grgen.ast.BaseNode where T : de.unika.ipd.grgen.ast.BaseNode
	{
		private Resolver<Triple<R, S, T>> resolver;

		public CollectTripleResolver(Resolver<Triple<R, S, T>> resolver)
		{
			this.resolver = resolver;
		}

		/// <summary>
		/// resolves the collect node to collect nodes of type R, S and T via
		/// the given resolver
		/// </summary>
		public virtual Triple<CollectNode<R>, CollectNode<S>, CollectNode<T>> Resolve(CollectBaseNode collect)
		{
			CollectNode<R> first = null;
			CollectNode<S> second = null;
			CollectNode<T> third = null;

			foreach(BaseNode child in collect.Children)
			{
				Triple<R, S, T> triple = resolver.Resolve(child, collect);
				if(triple == null)
					return null;
				if(triple.first != null)
				{
					if(first == null)
					{
						first = new CollectNode<R>();
						first.Coords = collect.Coords;
					}
					first.AddChild(triple.first);
				}
				if(triple.second != null)
				{
					if(second == null)
					{
						second = new CollectNode<S>();
						second.Coords = collect.Coords;
					}
					second.AddChild(triple.second);
				}
				if(triple.third != null)
				{
					if(third == null)
					{
						third = new CollectNode<T>();
						third.Coords = collect.Coords;
					}
					third.AddChild(triple.third);
				}
			}

			Triple<CollectNode<R>, CollectNode<S>, CollectNode<T>> res = new Triple<CollectNode<R>, CollectNode<S>, CollectNode<T>>();
			res.first = first;
			res.second = second;
			res.third = third;

			return res;
		}
	}

}
