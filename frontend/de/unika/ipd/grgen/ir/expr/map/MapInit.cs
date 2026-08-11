/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr.map
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using de.unika.ipd.grgen.ir;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ExpressionPair = de.unika.ipd.grgen.ir.expr.ExpressionPair;
	using MapType = de.unika.ipd.grgen.ir.type.container.MapType;

	public class MapInit : Expression
	{
		private ICollection<ExpressionPair> mapItems;
		private Entity member;
		private MapType mapType;
		private bool isConst;

		public MapInit(ICollection<ExpressionPair> mapItems, Entity member, MapType mapType, bool isConst)
			: base("map init", member != null ? member.Type : mapType)
		{
			this.mapItems = mapItems;
			this.member = member;
			this.mapType = mapType;
			this.isConst = isConst;
		}

		public override void CollectNeededEntities(NeededEntities needs)
		{
			needs.Add(this);
			foreach(ExpressionPair mapItem in mapItems)
				mapItem.CollectNeededEntities(needs);
		}

		public virtual ICollection<ExpressionPair> MapItems
		{
			get
			{
				return mapItems;
			}
		}

		public virtual Entity Member
		{
			set
			{
				Debug.Assert((member == null && value != null));
				member = value;
			}
			get
			{
				return member;
			}
		}


		public virtual MapType MapType
		{
			get
			{
				return mapType;
			}
		}

		public virtual void ForceNotConstant()
		{
			isConst = false;
		}

		public virtual bool IsConstant()
		{
			return isConst;
		}

		public virtual string AnonymousMapName
		{
			get
			{
				return "anonymous_map_" + Id;
			}
		}
	}

}
