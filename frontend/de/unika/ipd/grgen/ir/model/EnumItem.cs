/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.model
{

	using System.Collections.Generic;

	using IR = de.unika.ipd.grgen.ir.IR;
	using Ident = de.unika.ipd.grgen.ir.Ident;
	using Identifiable = de.unika.ipd.grgen.ir.Identifiable;
	using EnumExpression = de.unika.ipd.grgen.ir.expr.EnumExpression;

	/// <summary>
	/// An enumeration value
	/// </summary>
	public class EnumItem : Identifiable
	{
		private readonly Ident id;

		private readonly EnumExpression value;

		/// <summary>
		/// Make a new enumeration value. </summary>
		/// <param name="id"> The enumeration item identifier. </param>
		/// <param name="value"> The associated value. </param>
		public EnumItem(Ident id, EnumExpression value)
			: base("enum item", id)
		{
			this.id = id;
			this.value = value;
		}

		/// <returns> The identifier of the enum item. </returns>
		public override Ident Ident
		{
			get
			{
				return id;
			}
		}

		/// <summary>
		/// The string of an enum item is its identifier's text. </summary>
		/// <seealso cref="java.lang.Object.toString() "/>
		public override string ToString()
		{
			return id.ToString();
		}

		/// <returns> The value of the enum item. </returns>
		public virtual EnumExpression Value
		{
			get
			{
				return value;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.Walkable.getWalkableChildren() "/>
		public virtual ICollection<IR> WalkableChildren
		{
			get
			{
				ISet<IR> res = new HashSet<IR>();
				res.Add(id);
				res.Add(value);
				return res;
			}
		}
	}

}
