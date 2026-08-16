/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{
	using de.unika.ipd.grgen.ast;
	using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
	using IndexDeclNode = de.unika.ipd.grgen.ast.model.decl.IndexDeclNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node yielding the graph elements (nodes or edges) from an index (base class without constraints, the constrained ones inherit from this one).
	/// </summary>
	public abstract class FromIndexAccessExprNode : BuiltinFunctionInvocationBaseNode
	{
		static FromIndexAccessExprNode()
		{
			SetClassName(typeof(FromIndexAccessExprNode), "from index access expr");
		}

		protected internal BaseNode indexUnresolved;
		protected internal IndexDeclNode index;

		protected internal FromIndexAccessExprNode(Coords coords, BaseNode index)
			: base(coords)
		{
			this.indexUnresolved = index;
			BecomeParent(this.indexUnresolved);
		}

		private static DeclarationResolver<IndexDeclNode> indexResolver =
				new DeclarationResolver<IndexDeclNode>(typeof(IndexDeclNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			//boolean fixupWorked = fixupDefinition(indexUnresolved, indexUnresolved.getScope()); -- could be needed when used in a method in the model before the index declaration
			index = indexResolver.Resolve(indexUnresolved, this);
			if(index == null)
			{
				int indexArgumentNumber = 1 + IndexShift();
				ReportError("The function " + ShortSignature() + " expects as " + indexArgumentNumber + ". argument (index) a declared index (given is " + indexUnresolved.ToStringWithDeclarationCoords() + ").");
			}
			return index != null;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			//note the early exit in checkLocal of FromIndexAccessMultipleFromToExprNode if the same index check fails (silently), when the parts FromIndexAccessFromToPartExprNode inheriting from this class are inspected 
			bool res = true;
			TypeNode expectedEntityType = Root.Decl.DeclType;
			TypeNode entityType = index.Type;
			if(!entityType.IsCompatibleTo(expectedEntityType))
			{
				int indexArgumentNumber = 1 + IndexShift();
				ReportError("The function " + ShortSignature() + " expects as " + indexArgumentNumber + ". argument (index) a value of type index on " + expectedEntityType.ToStringWithDeclarationCoords()
						+ " (but is given a value of type index on " + entityType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			return res;
		}

		protected internal virtual int IndexShift() // the isIn(Nodes|Edges)FromIndex methods start with the candidate to be checked, shifting the regular parameter numbers by one
		{
			return 0;
		}

		protected internal abstract IdentNode Root {get;}

		protected internal abstract string ShortSignature();

		protected internal override abstract IR ConstructIR();
	}

}
