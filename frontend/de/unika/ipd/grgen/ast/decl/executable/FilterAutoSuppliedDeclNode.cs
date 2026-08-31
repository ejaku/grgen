/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.decl.executable
{

	using System.Collections.Generic;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using IteratedDeclNode = de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using FilterAutoSupplied = de.unika.ipd.grgen.ir.executable.FilterAutoSupplied;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;

	/// <summary>
	/// AST node class representing auto supplied filters (automatically declared)
	/// </summary>
	public class FilterAutoSuppliedDeclNode : FilterAutoDeclNode
	{
		static FilterAutoSuppliedDeclNode()
		{
			SetClassName(typeof(FilterAutoSuppliedDeclNode), "auto supplied filter");
		}

		protected internal IdentNode actionUnresolved;
		protected internal ActionDeclNode action;
		protected internal IteratedDeclNode iterated;

		public FilterAutoSuppliedDeclNode(IdentNode ident, IdentNode action)
			: base(ident)
		{

			this.ident = ident;
			this.actionUnresolved = action;
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(actionUnresolved, action, iterated));
				return children;
			}
		}

		/// <summary>
		/// returns names of the children, same order as in getChildren </summary>
		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("actionOrIterated");
				return childrenNames;
			}
		}

		private static readonly DeclarationPairResolver<ActionDeclNode, IteratedDeclNode> actionOrIteratedResolver =
				new DeclarationPairResolver<ActionDeclNode, IteratedDeclNode>(typeof(ActionDeclNode), typeof(IteratedDeclNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			Pair<ActionDeclNode, IteratedDeclNode> actionOrIterated = actionOrIteratedResolver.Resolve(actionUnresolved, this);
			if(actionOrIterated == null)
				return false;
			action = actionOrIterated.fst;
			iterated = actionOrIterated.snd;
			return action != null || iterated != null;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool CheckLocal()
		{
			return true;
		}

		protected internal override IR ConstructIR()
		{
			// return if the IR object was already constructed
			// that may happen in recursive calls
			if(IsIRAlreadySet())
				return IR;

			FilterAutoSupplied filterAutoSup = new FilterAutoSupplied(ident.ToString());

			// mark this node as already visited
			IR = filterAutoSup;

			Rule actionOrIterated = action != null ? action.IRMatcher : iterated.IRMatcher;
			filterAutoSup.Action = actionOrIterated;
			actionOrIterated.AddFilter(filterAutoSup);

			return filterAutoSup;
		}

		public static new string KindStr
		{
			get
			{
				return "auto supplied filter";
			}
		}
	}

}
