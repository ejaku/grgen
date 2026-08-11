/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ast.model.type
{
	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using de.unika.ipd.grgen.ast;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using ConstructorDeclNode = de.unika.ipd.grgen.ast.decl.ConstructorDeclNode;
	using FunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
	using ProcedureDeclNode = de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
	using ArrayInitNode = de.unika.ipd.grgen.ast.expr.array.ArrayInitNode;
	using DequeInitNode = de.unika.ipd.grgen.ast.expr.deque.DequeInitNode;
	using MapInitNode = de.unika.ipd.grgen.ast.expr.map.MapInitNode;
	using SetInitNode = de.unika.ipd.grgen.ast.expr.set.SetInitNode;
	using MemberInitNode = de.unika.ipd.grgen.ast.model.MemberInitNode;
	using MemberDeclNode = de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;

	/// <summary>
	/// A class representing the base type for internal (non-node/edge) object types (i.e. classes)
	/// </summary>
	public abstract class BaseInternalObjectTypeNode : InheritanceTypeNode
	{
		static BaseInternalObjectTypeNode()
		{
			SetClassName(typeof(BaseInternalObjectTypeNode), "base internal object type");
		}

		/// <summary>
		/// Create a new base internal object type (i.e. class) </summary>
		/// <param name="ext"> The collect node containing the base object types which are extended by this type. </param>
		/// <param name="body"> the collect node with body declarations </param>
		/// <param name="modifiers"> Type modifiers for this type. </param>
		public BaseInternalObjectTypeNode(CollectNode<IdentNode> ext, CollectNode<BaseNode> body, int modifiers)
		{
			this.extendUnresolved = ext;
			BecomeParent(this.extendUnresolved);
			this.bodyUnresolved = body;
			BecomeParent(this.bodyUnresolved);
			Modifiers = modifiers;
			ExternalName = null;
		}

		private static readonly CollectResolver<BaseNode> bodyResolver = new CollectResolver<BaseNode>(
				new DeclarationResolver<BaseNode>(typeof(MemberDeclNode), typeof(MemberInitNode), typeof(ConstructorDeclNode),
						typeof(MapInitNode), typeof(SetInitNode), typeof(ArrayInitNode), typeof(DequeInitNode),
						typeof(FunctionDeclNode), typeof(ProcedureDeclNode)));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			body = bodyResolver.Resolve(bodyUnresolved, this);

			return body != null;
		}
	}

}
