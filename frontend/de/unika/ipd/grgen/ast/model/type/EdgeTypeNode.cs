/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Buchwald
/// </summary>
namespace de.unika.ipd.grgen.ast.model.type
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using de.unika.ipd.grgen.ast;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using ConstructorDeclNode = de.unika.ipd.grgen.ast.decl.ConstructorDeclNode;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using FunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
	using OperatorDeclNode = de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
	using OperatorEvaluator = de.unika.ipd.grgen.ast.decl.executable.OperatorEvaluator;
	using ProcedureDeclNode = de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
	using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
	using ContainerInitNode = de.unika.ipd.grgen.ast.expr.ContainerInitNode;
	using ArrayInitNode = de.unika.ipd.grgen.ast.expr.array.ArrayInitNode;
	using DequeInitNode = de.unika.ipd.grgen.ast.expr.deque.DequeInitNode;
	using MapInitNode = de.unika.ipd.grgen.ast.expr.map.MapInitNode;
	using SetInitNode = de.unika.ipd.grgen.ast.expr.set.SetInitNode;
	using ConnAssertNode = de.unika.ipd.grgen.ast.model.ConnAssertNode;
	using MemberInitNode = de.unika.ipd.grgen.ast.model.MemberInitNode;
	using MemberDeclNode = de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using ConnAssert = de.unika.ipd.grgen.ir.model.ConnAssert;
	using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;

	public abstract class EdgeTypeNode : InheritanceTypeNode
	{
		static EdgeTypeNode()
		{
			SetClassName(typeof(EdgeTypeNode), "edge type");
		}

		public static ArbitraryEdgeTypeNode arbitraryEdgeType;
		public static DirectedEdgeTypeNode directedEdgeType;
		public static UndirectedEdgeTypeNode undirectedEdgeType;

		private static readonly CollectResolver<BaseNode> bodyResolver = new CollectResolver<BaseNode>(
				new DeclarationResolver<BaseNode>(typeof(MemberDeclNode), typeof(MemberInitNode), typeof(ConstructorDeclNode),
						typeof(MapInitNode), typeof(SetInitNode), typeof(ArrayInitNode), typeof(DequeInitNode),
						typeof(FunctionDeclNode), typeof(ProcedureDeclNode)));

		private static readonly CollectResolver<EdgeTypeNode> extendResolver =
				new CollectResolver<EdgeTypeNode>(new DeclarationTypeResolver<EdgeTypeNode>(typeof(EdgeTypeNode)));

		private CollectNode<EdgeTypeNode> extend;
		private CollectNode<ConnAssertNode> cas;

		/// <summary>
		/// Make a new edge type node. </summary>
		/// <param name="ext"> The collect node with all edge classes that this one extends. </param>
		/// <param name="cas"> The collect node with all connection assertion of this type. </param>
		/// <param name="body"> The body of the type declaration. It consists of basic
		/// declarations. </param>
		/// <param name="modifiers"> The modifiers for this type. </param>
		/// <param name="externalName"> The name of the external implementation of this type or null. </param>
		public EdgeTypeNode(CollectNode<IdentNode> ext, CollectNode<ConnAssertNode> cas, CollectNode<BaseNode> body,
				int modifiers, string externalName)
		{
			this.extendUnresolved = ext;
			BecomeParent(this.extendUnresolved);
			this.bodyUnresolved = body;
			BecomeParent(this.bodyUnresolved);
			this.cas = cas;
			BecomeParent(this.cas);
			Modifiers = modifiers;
			ExternalName = externalName;
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersionCollectNode(extendUnresolved, extend));
				children.Add(GetValidVersionCollectNode(bodyUnresolved, body));
				children.Add(cas);
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
				childrenNames.Add("extends");
				childrenNames.Add("body");
				childrenNames.Add("cas");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			OperatorDeclNode.MakeOp(Operator.COND, this, new TypeNode[] { BasicTypeNode.booleanType, this, this }, OperatorEvaluator.condEvaluator);

			OperatorDeclNode.MakeBinOp(Operator.EQ, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.NE, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.SE, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);

			body = bodyResolver.Resolve(bodyUnresolved, this);
			extend = extendResolver.Resolve(extendUnresolved, this);

			// Initialize direct sub types
			if(extend != null)
			{
				foreach(InheritanceTypeNode type in extend.ChildrenExact)
					type.AddDirectSubType(this);
			}

			return body != null && extend != null;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			bool res = base.CheckLocal();

			// check all super types to ensure their copy extends are resolved
			foreach(EdgeTypeNode parent in extend.ChildrenExact)
			{
				if(!parent.VisitedDuringCheck())
					parent.Check();
			}

			// "resolve" connection assertion inheritance,
			// after resolve to ensure everything is available, before IR building
			IList<ConnAssertNode> connAssertsToCopy = ConnectionAssertionsToCopy;
			foreach(ConnAssertNode caToCopy in connAssertsToCopy)
				cas.AddChild(caToCopy);

			foreach(BaseNode child in body.ChildrenExact)
			{
				if(child is ConstructorDeclNode
						|| child is MemberInitNode
						|| child is ContainerInitNode
						|| child is FunctionDeclNode
						|| child is ProcedureDeclNode)
					continue;

				DeclNode decl = (DeclNode)child;
				if(decl.DeclType is InternalTransientObjectTypeNode)
				{
					decl.ReportError("Only transient object classes may contain attributes of transient object class types"
							+ " (but the attribute " + decl.Ident
							+ " is of transient object class type " + decl.DeclType.ToStringWithDeclarationCoords()
							+ " in edge class " + Ident + ").");
					res &= false;
				}
			}

			// todo: check for duplicate connection assertions and issue warning about being senseless

			return res;
		}

		private IList<ConnAssertNode> ConnectionAssertionsToCopy
		{
			get
			{
				// return connection assertions to copy to prevent iterator from becoming stale, copied after iteration 
				IList<ConnAssertNode> connAssertsToCopy = new List<ConnAssertNode>();
				IList<ConnAssertNode> connAssertsToDelete = new List<ConnAssertNode>();
				bool alreadyCopiedExtends = false;
				foreach(ConnAssertNode ca in cas.ChildrenExact)
				{
					if(ca.copyExtends)
					{
						if(alreadyCopiedExtends)
							ReportWarning("more than one copy extends only causes double work without benefit");

						foreach(EdgeTypeNode parent in extend.ChildrenExact)
						{
							foreach(ConnAssertNode caToCopy in parent.cas.ChildrenExact)
							{
								if(caToCopy.copyExtends)
								{
									ReportError("Internal error: copy extends in parent while copying connection assertions from parent.");
									Debug.Assert(false);
								}
								connAssertsToCopy.Add(caToCopy);
							}
						}

						connAssertsToDelete.Add(ca);
						alreadyCopiedExtends = true;
					}
				}

				cas.ChildrenExact.RemoveAll(connAssertsToDelete);

				return connAssertsToCopy;
			}
		}

		/// <summary>
		/// Get the edge type IR object. </summary>
		/// <returns> The edge type IR object for this AST node. </returns>
		public EdgeType IREdgeType
		{
			get
			{
				return CheckIR<EdgeType>(typeof(EdgeType));
			}
		}

		public static string KindStr
		{
			get
			{
				return "edge class";
			}
		}

		public override void DoGetCompatibleToTypes(ICollection<TypeNode> coll)
		{
			Debug.Assert(IsResolved());

			foreach(EdgeTypeNode inh in extend.ChildrenExact)
			{
				coll.Add(inh);
				coll.AddAll(inh.CompatibleToTypes);
			}

			coll.Add(BasicTypeNode.typeType); // ~~ addCompatibility(this, BasicTypeNode.typeType);
		}

		public override ICollection<InheritanceTypeNode> DirectSuperTypes
		{
			get
			{
				Debug.Assert(IsResolved());

				return new List<InheritanceTypeNode>(extend.ChildrenExact);
			}
		}

		protected internal abstract EdgeType DirectednessIR {set;}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
		protected internal override IR ConstructIR()
		{
			if(IsIRAlreadySet()) // break endless recursion in case of a member of edge or container of edge typ
				return IR;

			EdgeType et = new EdgeType(Decl.Ident.IRIdent, IRModifiers, ExternalName);

			IR = et;

			ConstructIR(et); // from InheritanceTypeNode

			DirectednessIR = et; // from Undirected/Arbitrary/Directed-EdgeTypeNode

			foreach(ConnAssertNode can in cas.ChildrenExact)
				et.AddConnAssert(can.CheckIR<ConnAssert>(typeof(ConnAssert)));

			return et;
		}
	}

}
