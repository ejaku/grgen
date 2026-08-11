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

using de.unika.ipd.grgen.ast;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Coords = de.unika.ipd.grgen.parser.Coords;

public abstract class ContainerSingleElementInitNode : ContainerInitNode
{
	static ContainerSingleElementInitNode()
	{
		SetClassName(typeof(ContainerSingleElementInitNode), "container single element init");
	}

	protected internal CollectNode<ExprNode> containerItems = new CollectNode<ExprNode>();


	public ContainerSingleElementInitNode(Coords coords)
		: base(coords)
	{
	}

	public virtual void AddItem(ExprNode item)
	{
		containerItems.AddChild(item);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(containerItems);
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("containerItems");
		return childrenNames;
		}
	}

	protected internal virtual bool CheckContainerItems()
	{
		bool success = true;

		TypeNode containerElementType = ContainerType.ElementType;
		foreach(ExprNode item in containerItems.ChildrenExact)
		{
			if(item.Type != containerElementType)
			{
				if(!IsInitInModel())
				{
					ExprNode oldValueExpr = item;
					ExprNode newValueExpr = item.AdjustType(containerElementType, Coords);
					containerItems.Replace(oldValueExpr, newValueExpr);
					if(newValueExpr == ConstNode.Invalid)
					{
						success = false;
						oldValueExpr.ReportError("The value type " + oldValueExpr.Type.ToStringWithDeclarationCoords()
								+ " of the initializer does not fit to the value type " + containerElementType.ToStringWithDeclarationCoords()
								+ " of the container (" + ContainerType.TypeName + ").");
					}
				}
				else
				{
					success = false;
					item.ReportError("The value type " + item.Type.ToStringWithDeclarationCoords()
							+ " of the initializer does not fit to the value type " + containerElementType.ToStringWithDeclarationCoords()
							+ " of the container (" + ContainerType.TypeName
							+ " -- all items must be of exactly the same type).");
				}
			}
		}

		return success;
	}

	/// <summary>
	/// Checks whether the container only contains constants. </summary>
	/// <returns> True, if all container items are constant. </returns>
	public virtual bool IsConstant()
	{
		foreach(ExprNode item in containerItems.ChildrenExact)
		{
			if(!(item is ConstNode || IsEnumValue(item)))
				return false;
		}
		return true;
	}

	public virtual bool Contains(ConstNode node)
	{
		foreach(ExprNode item in containerItems.ChildrenExact)
		{
			if(item is ConstNode)
			{
				ConstNode itemConst = (ConstNode)item;
				if(node.Value.Equals(itemConst.Value))
					return true;
			}
		}
		return false;
	}

	protected internal virtual CollectNode<ExprNode> Items
	{
		get
		{
		return containerItems;
		}
	}

	protected internal virtual IList<Expression> ConstructItems()
	{
		IList<Expression> items = new List<Expression>();
		foreach(ExprNode item in containerItems.ChildrenExact)
		{
			item = item.Evaluate();
			items.Add(item.CheckIR(typeof(Expression)));
		}
		return items;
	}
}

}
