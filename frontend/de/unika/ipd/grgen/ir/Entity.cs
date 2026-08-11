/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ir
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using PatternGraphBase = de.unika.ipd.grgen.ir.pattern.PatternGraphBase;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	/// <summary>
	/// An instantiation of a type.
	/// </summary>
	public class Entity : Identifiable
	{
		protected internal static readonly string[] childrenNames = new string[] { "type" };

		/// <summary>
		/// Type of the entity. </summary>
		protected internal readonly Type type;

		/// <summary>
		/// The entity's owner. </summary>
		protected internal Type owner = null;

		/// <summary>
		/// Is the entity constant - (only) relevant in backend for node/edge attributes. </summary>
		protected internal bool isConst = false;

		/// <summary>
		/// Is the entity a defined entity only, to be filled with yields from nested patterns? </summary>
		protected internal bool isDefToBeYieldedTo = false;

		/// <summary>
		/// Only in case of isDefToBeYieldedTo: gives the pattern graph in which the entity is to be deleted (can't use LHS\RHS for deciding this) </summary>
		protected internal PatternGraphBase patternGraphDefYieldedIsToBeDeleted = null; // todo: DELETE=LHS\RHS does not work any more due to nesting and def entities, switch to delete annotations in AST, IR

		/// <summary>
		/// Context of the declaration </summary>
		public int context;

		/// <summary>
		/// Make a new entity of a given type </summary>
		/// <param name="name"> The name of the entity. </param>
		/// <param name="ident"> The declaring identifier. </param>
		/// <param name="type"> The type used in the declaration. </param>
		/// <param name="isConst"> Is the entity constant. </param>
		/// <param name="isDefToBeYieldedTo"> Is the entity a defined entity only, to be filled with yields from nested patterns. </param>
		/// <param name="context"> The context of the declaration </param>
		public Entity(string name, Ident ident, Type type, bool isConst, bool isDefToBeYieldedTo, int context)
			: base(name, ident)
		{
			ChildrenNames = childrenNames;
			this.type = type;
			this.isConst = isConst;
			this.isDefToBeYieldedTo = isDefToBeYieldedTo;
			this.context = context;
		}

		/// <returns> The entity's type. </returns>
		public virtual Type Type
		{
			get
			{
				return type;
			}
		}

		/// <returns> The entity's owner. </returns>
		public virtual Type Owner
		{
			get
			{
				return owner;
			}
			set // Set the owner of the entity.  This function is just called from other IR classes.
			{
			owner = value;
			}
		}


		/// <returns> true, if the entity has an owner, else false </returns>
		public virtual bool HasOwner()
		{
			return owner != null;
		}

		public override void AddFields(IDictionary<string, object> fields)
		{
			base.AddFields(fields);
			fields["type"] = Collections.Singleton(type);
			fields["owner"] = Collections.Singleton(owner);
		}

		/// <returns> true, if this is a retyped entity, i.e. the result of a retype, else false </returns>
		public virtual bool IsRetyped()
		{
			return false;
		}

		/// <returns> true, if this is a constant entity, else false </returns>
		public virtual bool IsConst()
		{
			return isConst;
		}

		/// <returns> true, if this is an entity declared in the right pattern, else false </returns>
		public virtual bool IsRHSEntity()
		{
			return (context & BaseNode.CONTEXT_LHS_OR_RHS) == BaseNode.CONTEXT_RHS;
		}

		/// <returns> true, if this is a defined only entity to be filled from nested patterns, else false </returns>
		public virtual bool IsDefToBeYieldedTo()
		{
			return isDefToBeYieldedTo;
		}

		public virtual int Context
		{
			get
			{
				return context;
			}
		}

		public virtual PatternGraphBase PatternGraphDefYieldedIsToBeDeleted
		{
			set
			{
				Debug.Assert(isDefToBeYieldedTo);
				patternGraphDefYieldedIsToBeDeleted = value;
			}
			get
			{
				return patternGraphDefYieldedIsToBeDeleted;
			}
		}


		public virtual string Kind
		{
			get
			{
				return "entity";
			}
		}
	}

}
