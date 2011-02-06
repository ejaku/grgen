/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;


import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.Exec;
import de.unika.ipd.grgen.ir.ExecVariable;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Sequence;

import java.util.Collection;
import java.util.Vector;


/**
 * AST node for a graph rewrite sequence definition.
 */
public class SequenceDeclNode extends DeclNode {
	static {
		setName(SequenceDeclNode.class, "sequence declaration");
	}

	protected SequenceTypeNode type;
	
	protected ExecNode exec;
	protected CollectNode<ExecVarDeclNode> inParams;
	protected CollectNode<ExecVarDeclNode> outParams;

	/** Type for this declaration. */
	private static final TypeNode sequenceType = new SequenceTypeNode();

	/** Make a sequence definition. */
	public SequenceDeclNode(IdentNode id, ExecNode exec,
			CollectNode<ExecVarDeclNode> inParams, CollectNode<ExecVarDeclNode> outParams) {
		super(id, sequenceType);
		this.exec = exec;
		becomeParent(this.exec);
		this.inParams = inParams;
		becomeParent(this.inParams);
		this.outParams = outParams;
		becomeParent(this.outParams);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(exec);
		children.add(inParams);
		children.add(outParams);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("exec");
		childrenNames.add("inParams");
		childrenNames.add("outParams");
		return childrenNames;
	}

	protected static final DeclarationTypeResolver<SequenceTypeNode> typeResolver =
		new DeclarationTypeResolver<SequenceTypeNode>(SequenceTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		type = typeResolver.resolve(typeUnresolved, this);

		return type != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		return true;
	}

    /** Returns the IR object for this sequence node. */
    protected Sequence getSequence() {
        return checkIR(Sequence.class);
    }
    
	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	protected IR constructIR() {
		Sequence sequence = new Sequence(getIdentNode().getIdent(), exec.checkIR(Exec.class));
		for(ExecVarDeclNode inParam : inParams.children) {
			sequence.addInParam(inParam.checkIR(ExecVariable.class));
		}
		for(ExecVarDeclNode outParam : outParams.children) {
			sequence.addOutParam(outParam.checkIR(ExecVariable.class));
		}
		return sequence;
	}

	@Override
	public SequenceTypeNode getDeclType() {
		assert isResolved();

		return type;
	}
}

