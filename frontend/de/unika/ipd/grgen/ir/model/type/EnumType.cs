/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ir.model.type
{

using System.Collections.Generic;
using System.Text;

using de.unika.ipd.grgen.ir;
using EnumItem = de.unika.ipd.grgen.ir.model.EnumItem;
using PrimitiveType = de.unika.ipd.grgen.ir.type.basic.PrimitiveType;

/// <summary>
/// An enumeration type.
/// </summary>
public class EnumType : PrimitiveType, ContainedInPackage
{
	private string packageContainedIn;

	private readonly List<EnumItem> items = new List<EnumItem>();

	/// <summary>
	/// Make a new enum type. </summary>
	///  <param name="ident"> The identifier of this enumeration.  </param>
	public EnumType(Ident ident)
		: base("enum type", ident)
	{
	}

	/// <summary>
	/// Add teh given item to a this enum type and autoenumerate it. </summary>
	public virtual void AddItem(EnumItem item)
	{
		items.Add(item);
	}

	/// <returns> A list with the identifiers in the enum type. </returns>
	public virtual IList<EnumItem> Items
	{
		get
		{
		return items.AsReadOnly();
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ir.type.Type.classify() "/>
	public override TypeClass Classify()
	{
		return TypeClass.IS_INTEGER;
	}

	public virtual string PackageContainedIn
	{
		get
		{
		return packageContainedIn;
		}
		set
		{
		this.packageContainedIn = value;
		}
	}


	protected internal override void CanonicalizeLocal()
	{
		items.Sort(Identifiable.COMPARATOR);
	}

	public override void AddToDigest(StringBuilder sb)
	{
		sb.Append(this);
		sb.Append('[');

		int i = 0;
		foreach(EnumItem ent in items)
		{
			if(i > 0)
				sb.Append(',');
			sb.Append(ent);
			++i;
		}

		sb.Append(']');
	}
}

}
