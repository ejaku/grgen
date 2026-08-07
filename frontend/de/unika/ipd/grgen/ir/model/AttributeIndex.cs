/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.model
{
using Entity = de.unika.ipd.grgen.ir.Entity;
using Ident = de.unika.ipd.grgen.ir.Ident;
using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;

/// <summary>
/// An attribute index.
/// </summary>
public class AttributeIndex : Index
{
	public InheritanceType type;
	public Entity entity;

	/// <param name="name"> The name of the attribute index. </param>
	/// <param name="ident"> The identifier that identifies this object. </param>
	public AttributeIndex(string name, Ident ident, InheritanceType type, Entity entity)
		: base(name, ident)
	{
		this.type = type;
		this.entity = entity;
	}
}

}
