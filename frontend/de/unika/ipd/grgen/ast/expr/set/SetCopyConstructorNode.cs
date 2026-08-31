/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.set
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using de.unika.ipd.grgen.ast;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using SetCopyConstructor = de.unika.ipd.grgen.ir.expr.set.SetCopyConstructor;
	using SetType = de.unika.ipd.grgen.ir.type.container.SetType;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class SetCopyConstructorNode : ExprNode
	{
		static SetCopyConstructorNode()
		{
			SetClassName(typeof(SetCopyConstructorNode), "set copy constructor");
		}

		private SetTypeNode setType;
		private ExprNode setToCopy;
		private BaseNode lhsUnresolved;

		public SetCopyConstructorNode(Coords coords, IdentNode member, SetTypeNode setType, ExprNode setToCopy)
			: base(coords)
		{

			if(member != null)
				lhsUnresolved = BecomeParent(member);
			else
				this.setType = setType;
			this.setToCopy = setToCopy;
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(setToCopy);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("setToCopy");
				return childrenNames;
			}
		}

		protected internal override bool ResolveLocal()
		{
			if(setType != null)
				return setType.Resolve();
			else
				return true;
		}

		protected internal override bool CheckLocal()
		{
			bool success = true;

			if(lhsUnresolved != null)
			{
				ReportError("A set copy constructor is not allowed in a set initialization in the model.");
				success = false;
			}
			else
			{
				if(setToCopy.Type is SetTypeNode)
				{
					SetTypeNode sourceSetType = (SetTypeNode)setToCopy.Type;
					success &= CheckCopyConstructorTypes(setType.valueType, sourceSetType.valueType, "set", false);
				}
				else
				{
					ReportError("A set copy constructor expects a value of set type to copy"
							+ " (but is given " + setToCopy.Type.TypeName + ").");
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
				return setType;
			}
		}

		protected internal override IR ConstructIR()
		{
			setToCopy = setToCopy.Evaluate();
			return new SetCopyConstructor(setToCopy.CheckIR<Expression>(typeof(Expression)), setType.CheckIR<SetType>(typeof(SetType)));
		}

		public static new string KindStr
		{
			get
			{
				return "set copy constructor";
			}
		}
	}

}
