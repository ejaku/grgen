/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.map
{
using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
using QualIdentNode = de.unika.ipd.grgen.ast.expr.QualIdentNode;
using ContainerProcedureMethodInvocationBaseNode = de.unika.ipd.grgen.ast.stmt.ContainerProcedureMethodInvocationBaseNode;
using MapTypeNode = de.unika.ipd.grgen.ast.type.container.MapTypeNode;
using Coords = de.unika.ipd.grgen.parser.Coords;

public abstract class MapProcedureMethodInvocationBaseNode : ContainerProcedureMethodInvocationBaseNode
{
	static MapProcedureMethodInvocationBaseNode()
	{
		SetClassName(typeof(MapProcedureMethodInvocationBaseNode), "map procedure method invocation base");
	}

	protected internal MapProcedureMethodInvocationBaseNode(Coords coords, QualIdentNode target)
		: base(coords, target)
	{
	}

	protected internal MapProcedureMethodInvocationBaseNode(Coords coords, VarDeclNode targetVar)
		: base(coords, targetVar)
	{
	}

	protected internal virtual MapTypeNode TargetTypeExact
	{
		get
		{
		return (MapTypeNode)TargetType;
		}
	}
}

}
