/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr
{

using System.Collections.Generic;
using System.Diagnostics;

using de.unika.ipd.grgen.ast;
using BaseInternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.BaseInternalObjectTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using AttributeInitialization = de.unika.ipd.grgen.ir.expr.AttributeInitialization;
using InternalObjectInit = de.unika.ipd.grgen.ir.expr.InternalObjectInit;
using BaseInternalObjectType = de.unika.ipd.grgen.ir.model.type.BaseInternalObjectType;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class ObjectInitNode : ExprNode
{
	static ObjectInitNode()
	{
		SetClassName(typeof(ObjectInitNode), "internal (transient) object init");
	}

	private IdentNode objectTypeUnresolved;
	private BaseInternalObjectTypeNode objectType;

	internal CollectNode<AttributeInitializationNode> attributeInits =
			new CollectNode<AttributeInitializationNode>();

	public ObjectInitNode(Coords coords, IdentNode objectType)
		: base(coords)
	{
		this.objectTypeUnresolved = objectType;
	}

	public virtual void AddAttributeInitialization(AttributeInitializationNode attributeInit)
	{
		this.attributeInits.AddChild(attributeInit);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(attributeInits);
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
		childrenNames.Add("attributeInits");
		return childrenNames;
		}
	}

	private static readonly DeclarationTypeResolver<BaseInternalObjectTypeNode> objectTypeResolver =
			new DeclarationTypeResolver<BaseInternalObjectTypeNode>(typeof(BaseInternalObjectTypeNode));

	protected internal override bool ResolveLocal()
	{
		objectType = objectTypeResolver.Resolve(objectTypeUnresolved, this);
		return objectType != null && objectType.Resolve();
	}

	protected internal override bool CheckLocal()
	{
		return true;
	}

	public override TypeNode Type
	{
		get
		{
		return ObjectType;
		}
	}

	public virtual BaseInternalObjectTypeNode ObjectType
	{
		get
		{
		Debug.Assert((IsResolved()));
		return objectType;
		}
	}

	protected internal override IR ConstructIR()
	{
		BaseInternalObjectType type = objectType.CheckIR(typeof(BaseInternalObjectType));

		InternalObjectInit init = new InternalObjectInit(type);

		foreach(AttributeInitializationNode ain in attributeInits.ChildrenExact)
		{
			ain.objectInitIR = init;
			init.AddAttributeInitialization(ain.CheckIR(typeof(AttributeInitialization)));
		}

		return init;
	}

	public virtual InternalObjectInit IRObjectInit
	{
		get
		{
		return CheckIR(typeof(InternalObjectInit));
		}
	}

	public static string KindStr
	{
		get
		{
		return "internal (transient) object initialization";
		}
	}
}

}
