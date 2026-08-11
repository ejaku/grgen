/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Rubino Geiss
/// </summary>

namespace de.unika.ipd.grgen.ast.decl.pattern
{

using System.Collections.Generic;
using System.Diagnostics;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using PackageIdentNode = de.unika.ipd.grgen.ast.PackageIdentNode;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using InvalidDeclNode = de.unika.ipd.grgen.ast.decl.InvalidDeclNode;
using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

/// <summary>
/// Declaration of a variable.
/// </summary>
public class VarDeclNode : DeclNode
{
	private TypeNode type;

	public PatternGraphLhsNode directlyNestingLHSGraph;
	public bool defEntityToBeYieldedTo;

	public ExprNode initialization = null;

	public int context;

	private string modifier;

	public bool lambdaExpressionVariable = false;


	public VarDeclNode(IdentNode id, IdentNode type,
			PatternGraphLhsNode directlyNestingLHSGraph, int context,
			bool defEntityToBeYieldedTo, bool lambdaExpressionVariable,
			string modifier)
		: base(id, type)
	{
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
		this.defEntityToBeYieldedTo = defEntityToBeYieldedTo;
		this.context = context;
		this.lambdaExpressionVariable = lambdaExpressionVariable;
		this.modifier = modifier;
	}

	public VarDeclNode(IdentNode id, IdentNode type,
			PatternGraphLhsNode directlyNestingLHSGraph, int context,
			string modifier)
		: this(id, type, directlyNestingLHSGraph, context, false, false, modifier)
	{
	}

	public VarDeclNode(IdentNode id, TypeNode type,
			PatternGraphLhsNode directlyNestingLHSGraph, int context,
			bool defEntityToBeYieldedTo, bool lambdaExpressionVariable,
			string modifier)
		: base(id, type)
	{
		this.type = type;
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
		this.defEntityToBeYieldedTo = defEntityToBeYieldedTo;
		this.context = context;
		this.lambdaExpressionVariable = lambdaExpressionVariable;
		this.modifier = modifier;
	}

	public VarDeclNode(IdentNode id, TypeNode type,
			PatternGraphLhsNode directlyNestingLHSGraph, int context, string modifier)
		: this(id, type, directlyNestingLHSGraph, context, false, false, modifier)
	{
	}

	public virtual VarDeclNode CloneForAuto(PatternGraphLhsNode parent)
	{
		VarDeclNode varDecl = new VarDeclNode(this.ident, this.type,
				parent, this.context, this.defEntityToBeYieldedTo, this.lambdaExpressionVariable, this.modifier);
		varDecl.Resolve();
		varDecl.Check();
		return varDecl;
	}

	/// <summary>
	/// Get an invalid var declaration. </summary>
	public static VarDeclNode GetInvalidVar(PatternGraphLhsNode directlyNestingLHSGraph, int context)
	{
		return new VarDeclNode(IdentNode.Invalid, IdentNode.Invalid, directlyNestingLHSGraph, context, "");
	}

	/// <summary>
	/// sets an expression to be used to initialize the variable </summary>
	public virtual ExprNode Initialization
	{
		set
		{
			this.initialization = value;
		}
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(ident);
			children.Add(GetValidVersion(typeUnresolved, type));
			if(initialization != null)
				children.Add(initialization);
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
			childrenNames.Add("ident");
			childrenNames.Add("type");
			if(initialization != null)
				childrenNames.Add("initialization expression");
			return childrenNames;
		}
	}

	private static readonly DeclarationResolver<DeclNode> declOfTypeResolver =
			new DeclarationResolver<DeclNode>(typeof(DeclNode));

	protected internal override bool ResolveLocal()
	{
		if(type != null) // Type was already known at construction?
			return true;
		if(!(typeUnresolved is PackageIdentNode))
			FixupDefinition(typeUnresolved, typeUnresolved.Scope);
		DeclNode typeDecl = declOfTypeResolver.Resolve(typeUnresolved, this);
		if(typeDecl is InvalidDeclNode)
		{
			typeUnresolved.ReportError("The variable " + Ident + " has an unknown type " + typeUnresolved + ".");
			return false;
		}
		if(!typeDecl.Resolve())
			return false;
		type = typeDecl.DeclType;
		return type != null;
	}

	protected internal override bool CheckLocal()
	{
		if(!string.ReferenceEquals(modifier, null))
		{
			if(type.IsValueType() && !modifier.Equals("var"))
			{
				ReportError("A var keyword is needed before a variable of value type "
						+ "(basic type, enum type, external type) (missing at " + Ident + " of type " + type.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			else if(type.IsReferenceType() && !modifier.Equals("ref"))
			{
				ReportError("A ref keyword is needed before a variable of reference type "
						+ "(container type, match type, object class type, transient object class type) (missing at " + Ident + " of type " + type.ToStringWithDeclarationCoords() + ").");
				return false;
			}
		}

		if(initialization == null)
			return true;

		TypeNode targetType = DeclType;
		TypeNode exprType = initialization.Type;

		if(exprType.IsEqual(targetType))
			return true;

		initialization = BecomeParent(initialization.AdjustType(targetType, Coords));
		return initialization != ConstNode.Invalid;
	}

	/// <returns> The type node of the declaration </returns>
	public override TypeNode DeclType
	{
		get
		{
			Debug.Assert(IsResolved());
			//assert type != null;
			return type;
		}
	}

	public static string KindStr
	{
		get
		{
			return "variable";
		}
	}

	/// <summary>
	/// Get the IR object correctly casted. </summary>
	/// <returns> The Variable IR object. </returns>
	public virtual Variable IRVariable
	{
		get
		{
			return CheckIR(typeof(Variable));
		}
	}

	protected internal override IR ConstructIR()
	{
		if(IsIRAlreadySet())
			return (Variable)IR;

		Variable var = new Variable("Var", Ident.IRIdent, type.IRType, defEntityToBeYieldedTo,
				directlyNestingLHSGraph != null ? directlyNestingLHSGraph.IRPatternGraphLhs : null,
				context, lambdaExpressionVariable);

		IR = var;

		if(initialization != null)
		{
			initialization = initialization.Evaluate();
			var.Initialization = initialization.CheckIR(typeof(Expression));
		}

		return var;
	}
}

}
