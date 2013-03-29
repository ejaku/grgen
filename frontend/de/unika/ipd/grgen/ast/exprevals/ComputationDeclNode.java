/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.exprevals.Computation;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.exprevals.EvalStatement;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;


/**
 * AST node class representing computation declarations
 */
public class ComputationDeclNode extends ComputationCharacter {
	static {
		setName(ComputationDeclNode.class, "computation declaration");
	}

	protected CollectNode<BaseNode> paramsUnresolved;
	protected CollectNode<DeclNode> params;
	
	protected CollectNode<EvalStatementNode> evals;
	static final ComputationTypeNode computationType =
		new ComputationTypeNode();

	public ComputationDeclNode(IdentNode id, CollectNode<EvalStatementNode> evals, CollectNode<BaseNode> params, BaseNode ret) {
		super(id, computationType);
		this.evals = evals;
		becomeParent(this.evals);
		this.paramsUnresolved = params;
		becomeParent(this.paramsUnresolved);
		this.retUnresolved = ret;
		becomeParent(this.retUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(evals);
		children.add(paramsUnresolved);
		children.add(getValidVersion(retUnresolved, ret));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("evals");
		childrenNames.add("params");
		childrenNames.add("ret");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean checkLocal() {
		params = new CollectNode<DeclNode>();
		for (BaseNode param : paramsUnresolved.getChildren()) {
	        if (param instanceof ConnectionNode) {
	        	ConnectionNode conn = (ConnectionNode) param;
	        	params.addChild(conn.getEdge().getDecl());
	        }
	        else if (param instanceof SingleNodeConnNode) {
	        	NodeDeclNode node = ((SingleNodeConnNode) param).getNode();
	        	params.addChild(node);
	        }
			else if(param instanceof VarDeclNode) {
				params.addChild((VarDeclNode) param);
			}
			else
				throw new UnsupportedOperationException("Unsupported parameter (" + param + ")");
        }

		return true;
	}

	/** Returns the IR object for this computation node. */
    public Computation getComputation() {
        return checkIR(Computation.class);
    }
    
	@Override
	public TypeNode getDeclType() {
		assert isResolved();
	
		return computationType;
	}
	
	public Vector<TypeNode> getParameterTypes() {
		assert isChecked();
		
		Vector<TypeNode> types = new Vector<TypeNode>();
		for(DeclNode decl : params.getChildren()) {
			types.add(decl.getDeclType());
		}

		return types;
	}

	@Override
	protected IR constructIR() {
		// return if the IR object was already constructed
		// that may happen in recursive calls
		if (isIRAlreadySet()) {
			return getIR();
		}

		Computation computation = new Computation(getIdentNode().toString(),
				getIdentNode().getIdent(), ret.checkIR(Type.class));

		// mark this node as already visited
		setIR(computation);

		// add Params to the IR
		for(DeclNode decl : params.getChildren()) {
			computation.addParameter(decl.checkIR(Entity.class));
		}

		// add Computation Statements to the IR
		for(EvalStatementNode eval : evals.getChildren()) {
			computation.addComputationStatement(eval.checkIR(EvalStatement.class));
		}

		return computation;
	}
}


