/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.model.type
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
	using OperatorDeclNode = de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
	using OperatorEvaluator = de.unika.ipd.grgen.ast.decl.executable.OperatorEvaluator;
	using EnumItemDeclNode = de.unika.ipd.grgen.ast.model.decl.EnumItemDeclNode;
	using CompoundTypeNode = de.unika.ipd.grgen.ast.type.CompoundTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Ident = de.unika.ipd.grgen.ir.Ident;
	using EnumItem = de.unika.ipd.grgen.ir.model.EnumItem;
	using EnumType = de.unika.ipd.grgen.ir.model.type.EnumType;

	/// <summary>
	/// An enumeration type AST node.
	/// </summary>
	public class EnumTypeNode : CompoundTypeNode
	{
		static EnumTypeNode()
		{
			SetClassName(typeof(EnumTypeNode), "enum type");
		}

		private CollectNode<EnumItemDeclNode> elements;

		/*
		 private static final OperatorSignature.Evaluator enumEvaluator =
		 new OperatorSignature.Evaluator() {
		 public ConstNode evaluate(Coords coords, OperatorSignature op,
		 ConstNode[] args) {
	
		 switch(op.getOpId()) {
		 case OperatorSignature.EQ:
		 return new BoolConstNode(coords, args[0].Value.equals(args[1].getValue()));
		 case OperatorSignature.NE:
		 return new BoolConstNode(coords, !args[0].Value.equals(args[1].getValue()));
		 }
		 return ConstNode.getInvalid();
		 }
		 };
		 */

		public EnumTypeNode(CollectNode<EnumItemDeclNode> body)
		{
			this.elements = body;
			BecomeParent(this.elements);

			//enumerations can be used with the conditional operator
			OperatorDeclNode.MakeOp(Operator.COND, this,
					new TypeNode[] { BasicTypeNode.booleanType, this, this }, OperatorEvaluator.condEvaluator);

			//the compatibility of the this enum type
			AddCompatibility(this, BasicTypeNode.byteType);
			AddCompatibility(this, BasicTypeNode.shortType);
			AddCompatibility(this, BasicTypeNode.intType);
			AddCompatibility(this, BasicTypeNode.longType);
			AddCompatibility(this, BasicTypeNode.floatType);
			AddCompatibility(this, BasicTypeNode.doubleType);
			AddCompatibility(this, BasicTypeNode.stringType);
		}

		/*
		 protected void doGetCastableToTypes(Collection<TypeNode> coll) {
		 Object obj = BasicTypeNode.castableMap.get(this);
		 if(obj != null)
		 coll.addAll((Collection) obj);
		 }*/

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(elements);
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
				childrenNames.Add("elements");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
		protected internal override IR ConstructIR()
		{
			Ident name = Ident.CheckIR<Ident>(typeof(Ident));
			EnumType ty = new EnumType(name);

			foreach(EnumItemDeclNode item in elements.ChildrenExact)
			{
				EnumItem it = item.Item;
				it.Value.LateInit(ty, it);
				ty.AddItem(it);
			}

			return ty;
		}

		public override string ToString()
		{
			return "enum " + Ident.ToString();
		}

		public static string KindStr
		{
			get
			{
				return "enum";
			}
		}
	}

}
