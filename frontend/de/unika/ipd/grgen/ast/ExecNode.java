/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 */

package de.unika.ipd.grgen.ast;

import java.awt.Color;
import java.util.Collection;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.ExecVarDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.DeclExprNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.IdentExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.basic.BooleanTypeNode;
import de.unika.ipd.grgen.ast.type.basic.ByteTypeNode;
import de.unika.ipd.grgen.ast.type.basic.DoubleTypeNode;
import de.unika.ipd.grgen.ast.type.basic.FloatTypeNode;
import de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
import de.unika.ipd.grgen.ast.type.basic.LongTypeNode;
import de.unika.ipd.grgen.ast.type.basic.NullTypeNode;
import de.unika.ipd.grgen.ast.type.basic.ShortTypeNode;
import de.unika.ipd.grgen.ast.type.basic.StringTypeNode;
import de.unika.ipd.grgen.ast.util.CollectQuadrupleResolver;
import de.unika.ipd.grgen.ast.util.DeclarationQuadrupleResolver;
import de.unika.ipd.grgen.ast.util.Quadruple;
import de.unika.ipd.grgen.ir.Exec;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.GraphEntityExpression;
import de.unika.ipd.grgen.ir.expr.VariableExpression;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.Symbol;

public class ExecNode extends BaseNode
{
	static {
		setName(ExecNode.class, "exec");
	}

	private static final CollectQuadrupleResolver<ExecVarDeclNode, NodeDeclNode, EdgeDeclNode, VarDeclNode> graphElementUsageOutsideOfCallResolver =
		new CollectQuadrupleResolver<ExecVarDeclNode, NodeDeclNode, EdgeDeclNode, VarDeclNode>(
		new DeclarationQuadrupleResolver<ExecVarDeclNode, NodeDeclNode, EdgeDeclNode, VarDeclNode>(ExecVarDeclNode.class, NodeDeclNode.class, EdgeDeclNode.class, VarDeclNode.class));

	private StringBuilder sb = new StringBuilder(); // if sb.length()==0 this is an external exec implemented externally

	protected CollectNode<MultiCallActionNode> multiCallActions = new CollectNode<MultiCallActionNode>();
	public CollectNode<CallActionNode> callActions = new CollectNode<CallActionNode>();
	private CollectNode<ExecVarDeclNode> varDecls = new CollectNode<ExecVarDeclNode>();
	private CollectNode<IdentNode> usageUnresolved = new CollectNode<IdentNode>();
	private CollectNode<IdentNode> writeUsageUnresolved = new CollectNode<IdentNode>();
	private CollectNode<DeclNode> usage = new CollectNode<DeclNode>();
	private CollectNode<DeclNode> writeUsage = new CollectNode<DeclNode>();

	private boolean disableXgrsStringBuilding = false;

	public ExecNode(Coords coords)
	{
		super(coords);
		becomeParent(multiCallActions);
		becomeParent(callActions);
	}

	public void append(Object n)
	{
		assert !isResolved();
		
		if(disableXgrsStringBuilding)
			return;
		
		if(n instanceof ConstNode) {
			ConstNode constant = (ConstNode)n;
			TypeNode type = constant.getType();
			Object value = constant.getValue();

			if(type instanceof StringTypeNode) {
				if(value == null)
					sb.append("null");
				else
					sb.append("\"" + value + "\"");
			} else if(type instanceof IntTypeNode || type instanceof DoubleTypeNode
					|| type instanceof ByteTypeNode || type instanceof ShortTypeNode)
				sb.append(value);
			else if(type instanceof FloatTypeNode)
				sb.append(value + "f");
			else if(type instanceof LongTypeNode)
				sb.append(value + "L");
			else if(type instanceof BooleanTypeNode)
				sb.append(((Boolean)value).booleanValue() ? "true" : "false");
			else if(type instanceof NullTypeNode)
				sb.append("null");
			else
				throw new UnsupportedOperationException("unsupported type");
		} else if(n instanceof IdentExprNode) {
			IdentExprNode identExpr = (IdentExprNode)n;
			sb.append(identExpr.getIdent());
		} else if(n instanceof DeclExprNode) {
			DeclExprNode declExpr = (DeclExprNode)n;
			sb.append(declExpr.declUnresolved);
		} else
			sb.append(n);
	}

	private String getXGRSString()
	{
		return sb.toString();
	}

	public void enableXgrsStringBuilding()
	{
		disableXgrsStringBuilding = false;
	}
	
	public void disableXgrsStringBuilding()
	{
		disableXgrsStringBuilding = true;
	}
	
	public void addMultiCallAction(MultiCallActionNode m)
	{
		assert !isResolved();
		becomeParent(m);
		multiCallActions.addChild(m);
	}

	public void addCallAction(CallActionNode n)
	{
		assert !isResolved();
		becomeParent(n);
		callActions.addChild(n);
	}

	/**
	 * Registers an explicit sequence-local variable declaration
	 */
	public void addVarDecl(ExecVarDeclNode varDecl)
	{
		assert !isResolved();
		becomeParent(varDecl);
		varDecls.addChild(varDecl);
	}

	/**
	 * Registers an identifier usage which might denote
	 * a) the use of a declared pattern graph element (node/edge)
	 * b) the use of a graph-global or sequence-local variable
	 * c) the implicit declaration of a graph-global variable at the first occurance
	 * which appears outside of a call (i.e. is not a rule call (input) parameter)
	 */
	public void addUsage(IdentNode id)
	{
		assert !isResolved();
		becomeParent(id);
		usageUnresolved.addChild(id);
	}

	public void addWriteUsage(IdentNode id)
	{
		assert !isResolved();
		becomeParent(id);
		writeUsageUnresolved.addChild(id);
	}

	/** returns children of this node */
	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> res = new Vector<BaseNode>();
		res.add(multiCallActions);
		res.add(callActions);
		res.add(varDecls);
		res.add(getValidVersion(usageUnresolved, usage));
		res.add(getValidVersion(writeUsageUnresolved, writeUsage));
		return res;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("multi call actions");
		childrenNames.add("call actions");
		childrenNames.add("var decls");
		childrenNames.add("graph element usage outside of a call");
		childrenNames.add("writing graph element usage (outside of a call)");
		return childrenNames;
	}

	/*
	 * This introduces an ExecVar definition if an identifier is not defined
	 * to support the usage-is-definition policy of the graph global variables in the sequences.
	 * Note: an (x)=r() & (x:A)=r() error will not be found due to the grgen symbol table and the fixupDefinition
	 * not taking care of the position of the definition compared to the uses
	 * (which makes sense for every other construct of the grgen language);
	 * this error will be caught later on when the xgrs is processed by the libgr sequence parser and symbol table.
	 */
	public void addImplicitDefinitions()
	{
		for(IdentNode id : usageUnresolved.getChildren()) {
			debug.report(NOTE, "Implicit definition for " + id + " in scope " + getScope());

			// Get the definition of the ident's symbol local to the owned scope.
			Symbol.Definition def = getScope().getCurrDef(id.getSymbol());
			debug.report(NOTE, "definition is: " + def);

			// If this definition is valid, i.e. it exists, it will be used
			// else, an ExecVarDeclNode of this name is added to the scope
			if(def.isValid()) {
				id.setSymDef(def);
			} else {
				Symbol.Definition vdef = getScope().define(id.getSymbol(), id.getCoords());
				id.setSymDef(vdef);
				vdef.setNode(id);
				getScope().leaveScope();
				ExecVarDeclNode evd = new ExecVarDeclNode(id, BasicTypeNode.untypedType);
				id.setDecl(evd);
				addVarDecl(evd);
			}
		}

		for(IdentNode id : writeUsageUnresolved.getChildren()) {
			debug.report(NOTE, "Implicit definition for " + id + " in scope " + getScope());

			// Get the definition of the ident's symbol local to the owned scope.
			Symbol.Definition def = getScope().getCurrDef(id.getSymbol());
			debug.report(NOTE, "definition is: " + def);

			// If this definition is valid, i.e. it exists, it will be used
			// else, an ExecVarDeclNode of this name is added to the scope
			if(def.isValid()) {
				id.setSymDef(def);
			} else {
				Symbol.Definition vdef = getScope().define(id.getSymbol(), id.getCoords());
				id.setSymDef(vdef);
				vdef.setNode(id);
				getScope().leaveScope();
				ExecVarDeclNode evd = new ExecVarDeclNode(id, BasicTypeNode.untypedType);
				id.setDecl(evd);
				addVarDecl(evd);
			}
		}
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		addImplicitDefinitions();
		Quadruple<CollectNode<ExecVarDeclNode>, CollectNode<NodeDeclNode>, CollectNode<EdgeDeclNode>, CollectNode<VarDeclNode>> resolve =
			graphElementUsageOutsideOfCallResolver.resolve(usageUnresolved);

		if(resolve != null) {
			if(resolve.first != null) {
				for(ExecVarDeclNode c : resolve.first.getChildren()) {
					usage.addChild(c);
				}
			}

			if(resolve.second != null) {
				for(NodeDeclNode c : resolve.second.getChildren()) {
					usage.addChild(c);
				}
			}

			if(resolve.third != null) {
				for(EdgeDeclNode c : resolve.third.getChildren()) {
					usage.addChild(c);
				}
			}

			if(resolve.fourth != null) {
				for(VarDeclNode c : resolve.fourth.getChildren()) {
					usage.addChild(c);
				}
			}

			becomeParent(usage);
		}

		Quadruple<CollectNode<ExecVarDeclNode>, CollectNode<NodeDeclNode>, CollectNode<EdgeDeclNode>, CollectNode<VarDeclNode>> writeResolve =
				graphElementUsageOutsideOfCallResolver.resolve(writeUsageUnresolved);

		if(writeResolve != null) {
			if(writeResolve.first != null) {
				for(ExecVarDeclNode c : writeResolve.first.getChildren()) {
					writeUsage.addChild(c);
				}
			}

			if(writeResolve.second != null) {
				for(NodeDeclNode c : writeResolve.second.getChildren()) {
					if(!c.defEntityToBeYieldedTo) {
						reportError("Only a def (to be yielded to) node is allowed to be written from an exec statement"
								+ " (violated by " + c.getIdentNode().toString() + ").");
					}
					writeUsage.addChild(c);
				}
			}

			if(writeResolve.third != null) {
				for(EdgeDeclNode c : writeResolve.third.getChildren()) {
					if(!c.defEntityToBeYieldedTo) {
						reportError("Only a def (to be yielded to) edge is allowed to be written from an exec statement"
								+ " (violated by " + c.getIdentNode().toString() + ").");
					}
					writeUsage.addChild(c);
				}
			}

			if(writeResolve.fourth != null) {
				for(VarDeclNode c : writeResolve.fourth.getChildren()) {
					if(!c.defEntityToBeYieldedTo) {
						reportError("Only a def (to be yielded to) variable is allowed to be written from an exec statement"
								+ " (violated by " + c.getIdentNode().toString() + ").");
					}
					writeUsage.addChild(c);
				}
			}

			becomeParent(writeUsage);
		}

		return resolve != null && writeResolve != null;
	}

	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	public Color getNodeColor()
	{
		return Color.PINK;
	}

	@Override
	protected IR constructIR()
	{
		Set<Expression> parameters = new LinkedHashSet<Expression>();
		for(DeclNode dn : usage.getChildren()) {
			if(dn instanceof ConstraintDeclNode)
				parameters.add(new GraphEntityExpression(dn.checkIR(GraphEntity.class)));
			else if(dn instanceof VarDeclNode)
				parameters.add(new VariableExpression(dn.checkIR(Variable.class)));
		}
		for(DeclNode dn : writeUsage.getChildren()) {
			if(dn instanceof ConstraintDeclNode)
				parameters.add(new GraphEntityExpression(dn.checkIR(GraphEntity.class)));
			else if(dn instanceof VarDeclNode)
				parameters.add(new VariableExpression(dn.checkIR(Variable.class)));
		}
		for(CallActionNode callActionNode : callActions.getChildren()) {
			callActionNode.checkPost();
			for(ExprNode param : callActionNode.getParams().getChildren()) {
				param = param.evaluate();
				parameters.add(param.checkIR(Expression.class));
			}
		}
		for(MultiCallActionNode multiCallActionNode : multiCallActions.getChildren()) {
			multiCallActionNode.checkPost();
		}
		Exec res = new Exec(getXGRSString(), parameters, getCoords().getLine());
		return res;
	}
}
