/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.exprevals.Procedure;
import de.unika.ipd.grgen.ir.exprevals.ProcedureMethod;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.exprevals.EvalStatement;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;


/**
 * AST node class representing procedure declarations
 */
public class ProcedureDeclNode extends ProcedureBase {
	static {
		setName(ProcedureDeclNode.class, "procedure declaration");
	}

	protected CollectNode<BaseNode> paramsUnresolved;
	protected CollectNode<DeclNode> params;

	public CollectNode<EvalStatementNode> evals;
	static final ProcedureTypeNode procedureType = new ProcedureTypeNode();
	
	boolean isMethod;

	public ProcedureDeclNode(IdentNode id, CollectNode<EvalStatementNode> evals, CollectNode<BaseNode> params, CollectNode<BaseNode> rets, boolean isMethod) {
		super(id, procedureType);
		this.evals = evals;
		becomeParent(this.evals);
		this.paramsUnresolved = params;
		becomeParent(this.paramsUnresolved);
		this.retsUnresolved = rets;
		becomeParent(this.retsUnresolved);
		this.isMethod = isMethod;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(evals);
		children.add(paramsUnresolved);
		children.add(getValidVersion(retsUnresolved, returnTypes));
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

	/** Returns the IR object for this procedure node. */
    public Procedure getProcedure() {
        return checkIR(Procedure.class);
    }
    
	@Override
	public TypeNode getDeclType() {
		assert isResolved();
	
		return procedureType;
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

		Procedure procedure = isMethod ? 
				new ProcedureMethod(getIdentNode().toString(), getIdentNode().getIdent()) : 
				new Procedure(getIdentNode().toString(), getIdentNode().getIdent());

		// mark this node as already visited
		setIR(procedure);

		// add return types to the IR
		for(TypeNode retType : returnTypes.getChildren()) {
			procedure.addReturnType(retType.checkIR(Type.class));
		}

		// add Params to the IR
		for(DeclNode decl : params.getChildren()) {
			procedure.addParameter(decl.checkIR(Entity.class));
		}

		// add Computation Statements to the IR
		for(EvalStatementNode eval : evals.getChildren()) {
			procedure.addComputationStatement(eval.checkIR(EvalStatement.class));
		}

		return procedure;
	}
	
	public static String getKindStr() {
		return "procedure declaration";
	}

	public static String getUseStr() {
		return "procedure";
	}
}


