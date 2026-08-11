/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll, Edgar Jakumeit
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
	using MapType = de.unika.ipd.grgen.ir.type.container.MapType;

	public class MapTypeNode : ContainerTypeNode
	{
		static MapTypeNode()
		{
			SetClassName(typeof(MapTypeNode), "map type");
		}

		public override string TypeName
		{
			get
			{
				return "map<" + keyTypeUnresolved.ToString() + "," + valueTypeUnresolved.ToString() + ">";
			}
		}

		public IdentNode keyTypeUnresolved;
		public TypeNode keyType;
		public IdentNode valueTypeUnresolved;
		public TypeNode valueType;

		private SetTypeNode exceptCompatibleSetTyp;

		// the map type node instances are created in ParserEnvironment as needed
		public MapTypeNode(IdentNode keyTypeIdent, IdentNode valueTypeIdent)
		{
			keyTypeUnresolved = BecomeParent(keyTypeIdent);
			valueTypeUnresolved = BecomeParent(valueTypeIdent);
			exceptCompatibleSetTyp = new SetTypeNode(keyTypeIdent);
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

		private static DeclarationTypeResolver<TypeNode> typeResolver =
				new DeclarationTypeResolver<TypeNode>(typeof(TypeNode));

		protected internal override bool ResolveLocal()
		{
			if(keyTypeUnresolved is PackageIdentNode)
				Resolver<BaseNode>.ResolveOwner((PackageIdentNode)keyTypeUnresolved);
			else
				FixupDefinition(keyTypeUnresolved, keyTypeUnresolved.Scope);
			if(valueTypeUnresolved is PackageIdentNode)
				Resolver<BaseNode>.ResolveOwner((PackageIdentNode)valueTypeUnresolved);
			else
				FixupDefinition(valueTypeUnresolved, valueTypeUnresolved.Scope);

			keyType = typeResolver.Resolve(keyTypeUnresolved, this);
			valueType = typeResolver.Resolve(valueTypeUnresolved, this);

			exceptCompatibleSetTyp.Resolve();

			if(keyType == null || valueType == null)
				return false;

			OperatorDeclNode.MakeBinOp(Operator.IN, BasicTypeNode.booleanType,
					keyType, this, OperatorEvaluator.mapEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.INDEX, valueType,
					this, keyType, OperatorEvaluator.mapEvaluator);

			OperatorDeclNode.MakeBinOp(Operator.EQ, BasicTypeNode.booleanType,
					this, this, OperatorEvaluator.mapEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.NE, BasicTypeNode.booleanType,
					this, this, OperatorEvaluator.mapEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.SE, BasicTypeNode.booleanType,
					this, this, OperatorEvaluator.mapEvaluator);

			OperatorDeclNode.MakeBinOp(Operator.GT, BasicTypeNode.booleanType,
					this, this, OperatorEvaluator.mapEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.GE, BasicTypeNode.booleanType,
					this, this, OperatorEvaluator.mapEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.LT, BasicTypeNode.booleanType,
					this, this, OperatorEvaluator.mapEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.LE, BasicTypeNode.booleanType,
					this, this, OperatorEvaluator.mapEvaluator);

			OperatorDeclNode.MakeBinOp(Operator.BIT_OR, this,
					this, this, OperatorEvaluator.mapEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.BIT_AND, this,
					this, this, OperatorEvaluator.mapEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.EXCEPT, this,
					this, this, OperatorEvaluator.mapEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.EXCEPT, this,
					this, exceptCompatibleSetTyp, OperatorEvaluator.mapEvaluator);

			TypeNode.AddCompatibility(this, BasicTypeNode.stringType);

			return true;
		}

		public override TypeNode ElementType
		{
			get
			{
				return keyType;
			}
		}

		protected internal override IR ConstructIR()
		{
			Type kt = keyType.IRType;
			Type vt = valueType.IRType;
			return new MapType(kt, vt);
		}
	}

}
