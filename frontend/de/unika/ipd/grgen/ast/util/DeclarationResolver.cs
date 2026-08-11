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
	using PackageIdentNode = de.unika.ipd.grgen.ast.PackageIdentNode;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using Util = de.unika.ipd.grgen.util.Util;

	/// <summary>
	/// A resolver, that resolves a source AST node into a target AST node of type R,
	/// by drawing the declaration node out of the source node if it is an identifier node,
	/// or by simply casting source to R otherwise
	/// </summary>
	public class DeclarationResolver<R> : Resolver<R> where R : de.unika.ipd.grgen.ast.BaseNode
	{
		private Type cls = typeof(R);
		private Type[] classes;

		public DeclarationResolver(Type cls)
		{
			this.cls = cls;
		}

	// JAVA TO C# CONVERTER TASK: Most Java annotations will not have direct .NET equivalent attributes:
	// ORIGINAL LINE: @SafeVarargs public DeclarationResolver(Class... classes)
		public DeclarationResolver(params Type[] classes)
		{
			this.classes = classes;
		}

		/// <summary>
		/// resolves n to node of type R, via declaration if n is an identifier, via simple cast otherwise
		///  returns null if n's declaration or n can't be cast to R 
		/// </summary>
		public override R Resolve(BaseNode bn, BaseNode parent)
		{
			if(bn is IdentNode)
			{
				R resolved = Resolve((IdentNode)bn);
				parent.BecomeParent(resolved);
				return resolved;
			}

			R res = TryCast(bn);
			if(res != null)
				return res;

			bn.ReportError(bn + " is a " + bn.Kind +
					" but a " + AllowedNames + " is expected.");
			return default(R);
		}

		/// <summary>
		/// resolves n to node of type R, via declaration
		///  returns null if n's declaration can't be cast to R 
		/// </summary>
		public virtual R Resolve(IdentNode n)
		{
			if(n is PackageIdentNode)
			{
				if(!ResolveOwner((PackageIdentNode)n))
					return default(R);
			}

			DeclNode resolved = n.Decl;

			R res = TryCast(resolved);
			if(res != null)
				return res;

			n.ReportError(n + " is a " + resolved.Kind +
					" but a " + AllowedNames + " is expected.");
			return default(R);
		}

		private R TryCast(BaseNode bn)
		{
			if(cls == null)
			{
				foreach(Type curCls in classes)
				{
					if(curCls.IsInstanceOfType(bn))
						return curCls.Cast(bn);
				}
			}
			else if(cls.IsInstanceOfType(bn))
				return cls.Cast(bn);
			return default(R);
		}

		private string AllowedNames
		{
			get
			{
				if(cls != null)
					return Util.GetStr(cls, typeof(BaseNode), "getKindStr");
				else
					return Util.GetStrListWithOr(classes, typeof(BaseNode), "getKindStr");
			}
		}
	}

}
