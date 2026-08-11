/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.type.container
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
	using OperatorDeclNode = de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
	using OperatorEvaluator = de.unika.ipd.grgen.ast.decl.executable.OperatorEvaluator;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using SetType = de.unika.ipd.grgen.ir.type.container.SetType;

	public class SetTypeNode : ContainerTypeNode
	{
		static SetTypeNode()
		{
			SetClassName(typeof(SetTypeNode), "set type");
		}

		public override string TypeName
		{
			get
			{
				return "set<" + valueTypeUnresolved.ToString() + ">";
			}
		}

		public IdentNode valueTypeUnresolved;
		public TypeNode valueType;

		// the set type node instances are created in ParserEnvironment as needed
		public SetTypeNode(IdentNode valueTypeIdent)
		{
			valueTypeUnresolved = BecomeParent(valueTypeIdent);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				// no children
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				// no children
				return childrenNames;
			}
		}

		private static readonly DeclarationTypeResolver<TypeNode> typeResolver =
				new DeclarationTypeResolver<TypeNode>(typeof(TypeNode));

		protected internal override bool ResolveLocal()
		{
			if(valueTypeUnresolved is PackageIdentNode)
				Resolver<BaseNode>.ResolveOwner((PackageIdentNode)valueTypeUnresolved);
			else
				FixupDefinition(valueTypeUnresolved, valueTypeUnresolved.Scope);
			valueType = typeResolver.Resolve(valueTypeUnresolved, this);

			if(valueType == null)
				return false;

			OperatorDeclNode.MakeBinOp(Operator.IN, BasicTypeNode.booleanType,
					valueType, this, OperatorEvaluator.setEvaluator);

			OperatorDeclNode.MakeBinOp(Operator.EQ, BasicTypeNode.booleanType,
					this, this, OperatorEvaluator.setEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.NE, BasicTypeNode.booleanType,
					this, this, OperatorEvaluator.setEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.SE, BasicTypeNode.booleanType,
					this, this, OperatorEvaluator.setEvaluator);

			OperatorDeclNode.MakeBinOp(Operator.GT, BasicTypeNode.booleanType,
					this, this, OperatorEvaluator.setEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.GE, BasicTypeNode.booleanType,
					this, this, OperatorEvaluator.setEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.LT, BasicTypeNode.booleanType,
					this, this, OperatorEvaluator.setEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.LE, BasicTypeNode.booleanType,
					this, this, OperatorEvaluator.setEvaluator);

			OperatorDeclNode.MakeBinOp(Operator.BIT_OR, this,
					this, this, OperatorEvaluator.setEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.BIT_AND, this,
					this, this, OperatorEvaluator.setEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.EXCEPT, this,
					this, this, OperatorEvaluator.setEvaluator);

			TypeNode.AddCompatibility(this, BasicTypeNode.stringType);

			return true;
		}

		public override TypeNode ElementType
		{
			get
			{
				return valueType;
			}
		}

		protected internal override IR ConstructIR()
		{
			Type vt = valueType.IRType;
			return new SetType(vt);
		}
	}

}
