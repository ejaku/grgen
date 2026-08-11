/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ir
{

using System;
using System.Collections.Generic;

using Annotated = de.unika.ipd.grgen.util.Annotated;
using Annotations = de.unika.ipd.grgen.util.Annotations;

/// <summary>
/// Identifiable with an identifier.
/// This is a super class for all classes which are associated with an identifier.
/// </summary>
public abstract class Identifiable : IR, Annotated, IComparable<Identifiable>
{
	/// <summary>
	/// helper class for comparing objects of type Identifiable, used in compareTo </summary>
	protected internal static readonly IComparer<Identifiable> COMPARATOR = new ComparatorAnonymousInnerClass();

	private class ComparatorAnonymousInnerClass : IComparer<Identifiable>
	{
		private readonly Identifiable outerInstance;

		public int Compare(Identifiable lt, Identifiable rt)
		{
			return lt.Ident.CompareTo(rt.Ident);
		}
	}

	/// <summary>
	/// The identifier </summary>
	private Ident ident;

	/// <param name="name"> The name of the IR class </param>
	///  <param name="ident"> The identifier associated with this IR object  </param>
	public Identifiable(string name, Ident ident)
		: base(name)
	{
		this.ident = ident;
	}

	/// <returns> The identifier that identifies this IR structure. </returns>
	public virtual Ident Ident
	{
		get
		{
			return ident;
		}
		set
		{
			this.ident = value;
		}
	}


	/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeLabel() "/>
	public override string NodeLabel
	{
		get
		{
			return ToString();
		}
	}

	public override string NodeInfo
	{
		get
		{
			return ident.NodeInfo;
		}
	}

	public override string ToString()
	{
		return Name + " " + ident;
	}

	public override void AddFields(IDictionary<string, object> fields)
	{
		fields["ident"] = ident.ToString();
	}

	public override int GetHashCode()
	{
		return Ident.hashCode();
	}

	public virtual int CompareTo(Identifiable id)
	{
		return COMPARATOR.Compare(this, id);
	}

	/// <returns> The annotations. </returns>
	public virtual Annotations Annotations
	{
		get
		{
			return Ident.GetCustomAttributes(true);
		}
	}
}

}
