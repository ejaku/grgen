/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Rubino Geiss
/// </summary>

namespace de.unika.ipd.grgen.ir.expr
{
	using de.unika.ipd.grgen.ir;
	using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using DefinedMatchType = de.unika.ipd.grgen.ir.type.DefinedMatchType;
	using MatchType = de.unika.ipd.grgen.ir.type.MatchType;

	public class Qualification : Expression
	{
		/// <summary>
		/// The owner of the qualification. </summary>
		private readonly Entity owner;

		/// <summary>
		/// The owner of the casted qualification. </summary>
		private readonly Expression ownerExpr;

		/// <summary>
		/// The member of the qualification. </summary>
		private readonly Entity member;

		public Qualification(Entity owner, Entity member)
			: base("qual", member.Type)
		{
			this.owner = owner;
			this.ownerExpr = null;
			this.member = member;
		}

		public Qualification(Expression ownerExpr, Entity member)
			: base("qual", member.Type)
		{
			this.owner = null;
			this.ownerExpr = ownerExpr;
			this.member = member;
		}

		public virtual Entity Owner
		{
			get
			{
				return owner;
			}
		}

		public virtual Expression OwnerExpr
		{
			get
			{
				return ownerExpr;
			}
		}

		public virtual Entity Member
		{
			get
			{
				return member;
			}
		}

		public override string NodeLabel
		{
			get
			{
				return "<" + owner + ">.<" + member + ">";
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
		public override void CollectNeededEntities(NeededEntities needs)
		{
			if(owner != null)
			{
				if(!IsGlobalVariable(owner)
					&& !(owner.Type is MatchType)
					&& !(owner.Type is DefinedMatchType))
				{
					if(owner is GraphEntity)
						needs.AddAttr((GraphEntity)owner, member);
					else
						needs.Add((Variable)owner);
				}
			}
			else
				ownerExpr.CollectNeededEntities(needs);
		}
	}

}
