/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.util
{
	using System;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using Util = de.unika.ipd.grgen.util.Util;

	/// <summary>
	/// A resolver, that resolves an identifier into it's AST type node.
	/// </summary>
	public class DeclarationTypeResolver<T> : Resolver<T> where T : de.unika.ipd.grgen.ast.BaseNode
	{
		private Type cls = typeof(T);

		/// <summary>
		/// Make a new type declaration resolver.
		/// </summary>
		/// <param name="cls"> A class, the resolved node must be an instance of. </param>
		public DeclarationTypeResolver(Type cls)
		{
			this.cls = cls;
		}

		/// <summary>
		/// Resolves n to node of type R, via declaration type if n is an identifier, via simple cast otherwise
		/// returns null if n's declaration or n can't be cast to R.
		/// </summary>
		public override T Resolve(BaseNode bn, BaseNode parent)
		{
			if(bn is IdentNode)
			{
				T resolved = Resolve((IdentNode)bn);
				parent.BecomeParent(resolved);
				return resolved;
			}
			if(cls.IsInstanceOfType(bn))
				return cls.Cast(bn);
			bn.ReportError(bn + " is a " + bn.Kind +
					" but a " + Util.GetStr(cls, typeof(BaseNode), "getKindStr") + " is expected.");
			return default(T);
		}

		/// <summary>
		/// Resolves n to node of type R, via declaration type
		/// returns null if n's declaration can't be cast to R.
		/// </summary>
		public virtual T Resolve(IdentNode n)
		{
			// ensure that the used types are resolved
			DeclarationResolver<DeclNode> declResolver = new DeclarationResolver<DeclNode>(typeof(DeclNode));
			DeclNode decl = declResolver.Resolve(n);
			if(decl != null)
			{
				decl.Resolve();

				TypeNode resolved = decl.DeclType;
				if(cls.IsInstanceOfType(resolved))
					return cls.Cast(resolved);
				n.ReportError(n + " is a " + resolved.Kind +
						" but a " + Util.GetStr(cls, typeof(BaseNode), "getKindStr") + " is expected.");
			}

			return default(T);
		}
	}

}
