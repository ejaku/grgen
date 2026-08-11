/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ir.type
{

using System.Collections.Generic;
using System.Text;

using Entity = de.unika.ipd.grgen.ir.Entity;
using Ident = de.unika.ipd.grgen.ir.Ident;
using Identifiable = de.unika.ipd.grgen.ir.Identifiable;
using FunctionMethod = de.unika.ipd.grgen.ir.executable.FunctionMethod;
using ProcedureMethod = de.unika.ipd.grgen.ir.executable.ProcedureMethod;

/// <summary>
/// Abstract base class for compound types containing members.
/// </summary>
public abstract class CompoundType : Type
{
	/// <summary>
	/// Collection containing all members defined in that type. </summary>
	private List<Entity> members = new List<Entity>();

	private List<FunctionMethod> functionMethods = new List<FunctionMethod>();
	private List<ProcedureMethod> procedureMethods = new List<ProcedureMethod>();

	/// <summary>
	/// Make a new compound type. </summary>
	/// <param name="name"> The name of the type. </param>
	/// <param name="ident"> The identifier used to declare this type. </param>
	public CompoundType(string name, Ident ident)
		: base(name, ident)
	{
	}

	/// <summary>
	/// Get all members of this compound type. </summary>
	public virtual ICollection<Entity> Members
	{
		get
		{
		return members.AsReadOnly();
		}
	}

	/// <summary>
	/// Add a member entity to the compound type. </summary>
	public virtual void AddMember(Entity member)
	{
		members.Add(member);
		member.Owner = this;
	}

	public virtual ICollection<FunctionMethod> FunctionMethods
	{
		get
		{
		return functionMethods.AsReadOnly();
		}
	}

	public virtual void AddFunctionMethod(FunctionMethod method)
	{
		functionMethods.Add(method);
		method.Owner = this;
	}

	public virtual void AddProcedureMethod(ProcedureMethod method)
	{
		procedureMethods.Add(method);
		method.Owner = this;
	}

	public virtual ICollection<ProcedureMethod> ProcedureMethods
	{
		get
		{
		return procedureMethods.AsReadOnly();
		}
	}

	protected internal override void CanonicalizeLocal()
	{
		members.Sort(Identifiable.COMPARATOR);
	}

	public override void AddFields(IDictionary<string, object> fields)
	{
		base.AddFields(fields);
		fields["members"] = members.GetEnumerator();
	}

	public override void AddToDigest(StringBuilder sb)
	{
		sb.Append(this);
		sb.Append('[');

		int i = 0;
		foreach(Entity ent in members)
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
