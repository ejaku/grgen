/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.map
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using de.unika.ipd.grgen.ast;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using MapTypeNode = de.unika.ipd.grgen.ast.type.container.MapTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using MapCopyConstructor = de.unika.ipd.grgen.ir.expr.map.MapCopyConstructor;
	using MapType = de.unika.ipd.grgen.ir.type.container.MapType;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class MapCopyConstructorNode : ExprNode
	{
		static MapCopyConstructorNode()
		{
			SetClassName(typeof(MapCopyConstructorNode), "map copy constructor");
		}

		private MapTypeNode mapType;
		private ExprNode mapToCopy;
		private BaseNode lhsUnresolved;

		public MapCopyConstructorNode(Coords coords, IdentNode member, MapTypeNode mapType, ExprNode mapToCopy)
			 : base(coords)
		{

			if(member != null)
				lhsUnresolved = BecomeParent(member);
			else
				this.mapType = mapType;
			this.mapToCopy = mapToCopy;
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(mapToCopy);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("mapToCopy");
				return childrenNames;
			}
		}

		protected internal override bool ResolveLocal()
		{
			if(mapType != null)
				return mapType.Resolve();
			else
				return true;
		}

		protected internal override bool CheckLocal()
		{
			bool success = true;

			if(lhsUnresolved != null)
			{
				ReportError("A map copy constructor is not allowed in a map initialization in the model.");
				success = false;
			}
			else
			{
				if(mapToCopy.Type is MapTypeNode)
				{
					MapTypeNode sourceMapType = (MapTypeNode)mapToCopy.Type;
					success &= CheckCopyConstructorTypes(mapType.keyType, sourceMapType.keyType, "map", true);
					success &= CheckCopyConstructorTypes(mapType.valueType, sourceMapType.valueType, "map", false);
				}
				else
				{
					ReportError("A map copy constructor expects a value of map type to copy"
							+ " (but is given " + mapToCopy.Type.TypeName + ").");
					success = false;
				}
			}

			return success;
		}

		public override TypeNode Type
		{
			get
			{
				Debug.Assert((IsResolved()));
				return mapType;
			}
		}

		protected internal override IR ConstructIR()
		{
			mapToCopy = mapToCopy.Evaluate();
			return new MapCopyConstructor(mapToCopy.CheckIR<Expression>(typeof(Expression)), mapType.CheckIR<MapType>(typeof(MapType)));
		}

		public static string KindStr
		{
			get
			{
				return "map copy constructor";
			}
		}
	}

}
