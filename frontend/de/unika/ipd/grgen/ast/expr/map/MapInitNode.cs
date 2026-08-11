/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.map
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
	using ContainerInitNode = de.unika.ipd.grgen.ast.expr.ContainerInitNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using ExprPairNode = de.unika.ipd.grgen.ast.expr.ExprPairNode;
	using DeclaredTypeNode = de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using ContainerTypeNode = de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
	using MapTypeNode = de.unika.ipd.grgen.ast.type.container.MapTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using ExpressionPair = de.unika.ipd.grgen.ir.expr.ExpressionPair;
	using MapInit = de.unika.ipd.grgen.ir.expr.map.MapInit;
	using MapType = de.unika.ipd.grgen.ir.type.container.MapType;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class MapInitNode : ContainerInitNode
	{
		static MapInitNode()
		{
			SetClassName(typeof(MapInitNode), "map init");
		}

		private CollectNode<ExprPairNode> mapItems = new CollectNode<ExprPairNode>();

		// if map init node is used in model, for member init
		//     then lhs != null, mapType == null
		// if map init node is used in actions, for anonymous const map with specified types
		//     then lhs == null, mapType != null -- adjust type of map items to this type
		private BaseNode lhsUnresolved;
		private DeclNode lhs;
		private MapTypeNode mapType;

		public MapInitNode(Coords coords, IdentNode member, MapTypeNode mapType)
			: base(coords)
		{

			if(member != null)
				lhsUnresolved = BecomeParent(member);
			else
				this.mapType = mapType;
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(mapItems);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("mapItems");
				return childrenNames;
			}
		}

		public virtual void AddPairItem(ExprPairNode item)
		{
			mapItems.AddChild(item);
		}

		private static readonly MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

		protected internal override bool ResolveLocal()
		{
			if(lhsUnresolved != null)
			{
				if(!lhsResolver.Resolve(lhsUnresolved))
					return false;
				lhs = lhsResolver.GetResult<DeclNode>(typeof(DeclNode));
				return lhsResolver.Finish();
			}
			else
			{
				if(mapType == null)
					mapType = CreateMapType();
				return mapType.Resolve();
			}
		}

		protected internal override bool CheckLocal()
		{
			bool success = true;

			MapTypeNode mapType = (MapTypeNode)ContainerType;
			foreach(ExprPairNode item in mapItems.ChildrenExact)
			{
				if(item.keyExpr.GetType() != mapType.keyType)
				{
					if(!IsInitInModel())
					{
						ExprNode oldKeyExpr = item.keyExpr;
						item.keyExpr = item.keyExpr.AdjustType(mapType.keyType, Coords);
						item.SwitchParenthood(oldKeyExpr, item.keyExpr);
						if(item.keyExpr == ConstNode.Invalid)
						{
							success = false;
							oldKeyExpr.ReportError("The key type " + oldKeyExpr.Type.ToStringWithDeclarationCoords()
									+ " of the initializer does not fit to the key type " + mapType.keyType.ToStringWithDeclarationCoords()
									+ " of the map (" + mapType.TypeName + ").");
						}
					}
					else
					{
						success = false;
						item.keyExpr.ReportError("The key type " + item.keyExpr.GetType().ToStringWithDeclarationCoords()
								+ " of the initializer does not fit to the key type " + mapType.keyType.ToStringWithDeclarationCoords()
								+ " of the map (" + mapType.TypeName
								+ " -- all items must be of exactly the same type).");
					}
				}
				if(item.valueExpr.GetType() != mapType.valueType)
				{
					if(this.mapType != null)
					{
						ExprNode oldValueExpr = item.valueExpr;
						item.valueExpr = item.valueExpr.AdjustType(mapType.valueType, Coords);
						item.SwitchParenthood(oldValueExpr, item.valueExpr);
						if(item.valueExpr == ConstNode.Invalid)
						{
							success = false;
							oldValueExpr.ReportError("The value type " + oldValueExpr.Type.ToStringWithDeclarationCoords()
									+ " of the initializer does not fit to the value type " + mapType.valueType.ToStringWithDeclarationCoords()
									+ " of the map (" + mapType.TypeName + ").");
						}
					}
					else
					{
						success = false;
						item.valueExpr.ReportError("The value type " + item.valueExpr.GetType().ToStringWithDeclarationCoords()
								+ " of the initializer does not fit to the value type " + mapType.valueType.ToStringWithDeclarationCoords()
								+ " of the map (" + mapType.TypeName
								+ " -- all items must be of exactly the same type).");
					}
				}
			}

			if(!IsConstant() && lhs != null)
			{
				ReportError("Only constant items are allowed in a map initialization in the model.");
				success = false;
			}

			return success;
		}

		private MapTypeNode CreateMapType()
		{
			TypeNode keyTypeNode = mapItems.ChildrenExact.GetEnumerator().Next().keyExpr.GetType();
			TypeNode valueTypeNode = mapItems.ChildrenExact.GetEnumerator().Next().valueExpr.GetType();
			IdentNode keyTypeIdent = ((DeclaredTypeNode)keyTypeNode).Ident;
			IdentNode valueTypeIdent = ((DeclaredTypeNode)valueTypeNode).Ident;
			return new MapTypeNode(keyTypeIdent, valueTypeIdent);
		}

		/// <summary>
		/// Checks whether the map only contains constants. </summary>
		/// <returns> True, if all map items are constant. </returns>
		public bool IsConstant()
		{
			foreach(ExprPairNode item in mapItems.ChildrenExact)
			{
				if(!(item.keyExpr is ConstNode || IsEnumValue(item.keyExpr)))
					return false;
				if(!(item.valueExpr is ConstNode || IsEnumValue(item.valueExpr)))
					return false;
			}
			return true;
		}

		public bool AreKeysConstant()
		{
			foreach(ExprPairNode item in mapItems.ChildrenExact)
			{
				if(!(item.keyExpr is ConstNode || IsEnumValue(item.keyExpr)))
					return false;
			}
			return true;
		}

		public virtual bool Contains(ConstNode node)
		{
			foreach(ExprPairNode item in mapItems.ChildrenExact)
			{
				if(item.keyExpr is ConstNode)
				{
					ConstNode itemConst = (ConstNode)item.keyExpr;
					if(node.Value.Equals(itemConst.Value))
						return true;
				}
			}
			return false;
		}

		public virtual ExprNode GetAtIndex(ConstNode node)
		{
			foreach(ExprPairNode item in mapItems.ChildrenExact)
			{
				if(item.keyExpr is ConstNode)
				{
					ConstNode itemConst = (ConstNode)item.keyExpr;
					if(node.Value.Equals(itemConst.Value))
						return item.valueExpr;
				}
			}
			return null;
		}

		public override ContainerTypeNode ContainerType
		{
			get
			{
				Debug.Assert((IsResolved()));
				if(lhs != null)
				{
					TypeNode type = lhs.DeclType;
					return (MapTypeNode)type;
				}
				else
					return mapType;
			}
		}

		public override bool IsInitInModel()
		{
			return mapType == null;
		}

		public virtual CollectNode<ExprPairNode> Items
		{
			get
			{
				return mapItems;
			}
		}

		protected internal override IR ConstructIR()
		{
			IList<ExpressionPair> items = new List<ExpressionPair>();
			foreach(ExprPairNode item in mapItems.ChildrenExact)
				items.Add(item.IRExpressionPair);
			Entity member = lhs != null ? lhs.IREntity : null;
			MapType type = mapType != null ? mapType.CheckIR<MapType>(typeof(MapType)) : null;
			return new MapInit(items, member, type, IsConstant());
		}

		public virtual MapInit IRMapInit
		{
			get
			{
				return CheckIR<MapInit>(typeof(MapInit));
			}
		}

		public static string KindStr
		{
			get
			{
				return "map initialization";
			}
		}
	}

}
