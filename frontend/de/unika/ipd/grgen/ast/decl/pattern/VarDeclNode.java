/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 */

package de.unika.ipd.grgen.ast.decl.pattern;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.PackageIdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.InvalidDeclNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.Variable;

/**
 * Declaration of a variable.
 */
public class VarDeclNode extends DeclNode
{
	private TypeNode type;

	public PatternGraphLhsNode directlyNestingLHSGraph;
	public boolean defEntityToBeYieldedTo;

	public ExprNode initialization = null;

	public int context;
	
	private String modifier;

	public boolean lambdaExpressionVariable = false;


	public VarDeclNode(IdentNode id, IdentNode type,
			PatternGraphLhsNode directlyNestingLHSGraph, int context,
			boolean defEntityToBeYieldedTo, boolean lambdaExpressionVariable,
			String modifier)
	{
		super(id, type);
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
		this.defEntityToBeYieldedTo = defEntityToBeYieldedTo;
		this.context = context;
		this.lambdaExpressionVariable = lambdaExpressionVariable;
		this.modifier = modifier;
	}

	public VarDeclNode(IdentNode id, IdentNode type,
			PatternGraphLhsNode directlyNestingLHSGraph, int context,
			String modifier)
	{
		this(id, type, directlyNestingLHSGraph, context, false, false, modifier);
	}

	public VarDeclNode(IdentNode id, TypeNode type,
			PatternGraphLhsNode directlyNestingLHSGraph, int context,
			boolean defEntityToBeYieldedTo, boolean lambdaExpressionVariable,
			String modifier)
	{
		super(id, type);
		this.type = type;
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
		this.defEntityToBeYieldedTo = defEntityToBeYieldedTo;
		this.context = context;
		this.lambdaExpressionVariable = lambdaExpressionVariable;
		this.modifier = modifier;
	}

	public VarDeclNode(IdentNode id, TypeNode type,
			PatternGraphLhsNode directlyNestingLHSGraph, int context, String modifier)
	{
		this(id, type, directlyNestingLHSGraph, context, false, false, modifier);
	}

	public VarDeclNode cloneForAuto(PatternGraphLhsNode parent)
	{
		VarDeclNode varDecl = new VarDeclNode(this.ident, this.type, 
				parent, this.context, this.defEntityToBeYieldedTo, this.lambdaExpressionVariable, this.modifier);
		varDecl.resolve();
		varDecl.check();
		return varDecl;
	}

	/** Get an invalid var declaration. */
	public static final VarDeclNode getInvalidVar(PatternGraphLhsNode directlyNestingLHSGraph, int context)
	{
		return new VarDeclNode(IdentNode.getInvalid(), IdentNode.getInvalid(), directlyNestingLHSGraph, context, "");
	}

	/** sets an expression to be used to initialize the variable */
	public void setInitialization(ExprNode initialization)
	{
		this.initialization = initialization;
	}

	/** returns children of this node */
	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		if(initialization != null)
			children.add(initialization);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		if(initialization != null)
			childrenNames.add("initialization expression");
		return childrenNames;
	}

	private static final DeclarationResolver<DeclNode> declOfTypeResolver =
			new DeclarationResolver<DeclNode>(DeclNode.class);

	@Override
	protected boolean resolveLocal()
	{
		if(type != null) { // Type was already known at construction?
			return true;
		}
		if(!(typeUnresolved instanceof PackageIdentNode)) {
			fixupDefinition(typeUnresolved, typeUnresolved.getScope());
		}
		DeclNode typeDecl = declOfTypeResolver.resolve(typeUnresolved, this);
		if(typeDecl instanceof InvalidDeclNode) {
			typeUnresolved.reportError("Unknown type: \"" + typeUnresolved + "\"");
			return false;
		}
		if(!typeDecl.resolve()) {
			return false;
		}
		type = typeDecl.getDeclType();
		return type != null;
	}

	@Override
	protected boolean checkLocal()
	{
		if(modifier != null) {
			if(type.isValueType() && !modifier.equals("var")) {
				reportError("var keyword needed before a variable of value type (basic type, enum type, external type)");
				return false;
			}
			else if(type.isReferenceType() && !modifier.equals("ref")) {
				reportError("ref keyword needed before a variable of reference type (container type, match type, object class type, transient object class type).");
				return false;
			}
		}
		
		if(initialization == null)
			return true;

		TypeNode targetType = getDeclType();
		TypeNode exprType = initialization.getType();

		if(exprType.isEqual(targetType))
			return true;

		initialization = becomeParent(initialization.adjustType(targetType, getCoords()));
		return initialization != ConstNode.getInvalid();
	}

	/** @return The type node of the declaration */
	@Override
	public TypeNode getDeclType()
	{
		assert isResolved();
		//assert type != null;
		return type;
	}

	public static String getKindStr()
	{
		return "variable";
	}

	/**
	 * Get the IR object correctly casted.
	 * @return The Variable IR object.
	 */
	public Variable getVariable()
	{
		return checkIR(Variable.class);
	}

	@Override
	protected IR constructIR()
	{
		if(isIRAlreadySet()) {
			return (Variable)getIR();
		}

		Variable var = new Variable("Var", getIdentNode().getIdent(), type.getType(), defEntityToBeYieldedTo,
				directlyNestingLHSGraph != null ? directlyNestingLHSGraph.getPatternGraph() : null,
				context, lambdaExpressionVariable);

		setIR(var);

		if(initialization != null) {
			initialization = initialization.evaluate();
			var.setInitialization(initialization.checkIR(Expression.class));
		}

		return var;
	}
}
