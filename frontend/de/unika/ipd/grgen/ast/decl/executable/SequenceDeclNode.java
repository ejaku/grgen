/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.decl.executable;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.ExecNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.ExecVarDeclNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.executable.SequenceTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.Exec;
import de.unika.ipd.grgen.ir.ExecVariable;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.Sequence;

import java.util.Collection;
import java.util.Vector;

/**
 * AST node for a graph rewrite sequence definition.
 */
public class SequenceDeclNode extends DeclNode
{
	static {
		setName(SequenceDeclNode.class, "sequence declaration");
	}

	protected SequenceTypeNode type;

	protected ExecNode exec;
	public CollectNode<ExecVarDeclNode> inParams;
	public CollectNode<ExecVarDeclNode> outParams;

	/** Type for this declaration. */
	private static final TypeNode sequenceType = new SequenceTypeNode();

	/** Make a sequence definition. */
	public SequenceDeclNode(IdentNode id, ExecNode exec,
			CollectNode<ExecVarDeclNode> inParams, CollectNode<ExecVarDeclNode> outParams)
	{
		super(id, sequenceType);
		this.exec = exec;
		becomeParent(this.exec);
		this.inParams = inParams;
		becomeParent(this.inParams);
		this.outParams = outParams;
		becomeParent(this.outParams);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(exec);
		children.add(inParams);
		children.add(outParams);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
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
	@Override
	protected boolean resolveLocal()
	{
		type = typeResolver.resolve(typeUnresolved, this);

		return type != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	/** Returns the IR object for this sequence node. */
	public Sequence getSequence()
	{
		return checkIR(Sequence.class);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR()
	{
		Sequence sequence = new Sequence(getIdentNode().getIdent(), exec.checkIR(Exec.class));
		for(ExecVarDeclNode inParam : inParams.getChildren()) {
			sequence.addInParam(inParam.checkIR(ExecVariable.class));
		}
		for(ExecVarDeclNode outParam : outParams.getChildren()) {
			sequence.addOutParam(outParam.checkIR(ExecVariable.class));
		}
		return sequence;
	}

	@Override
	public SequenceTypeNode getDeclType()
	{
		assert isResolved();

		return type;
	}

	public static String getKindStr()
	{
		return "sequence declaration";
	}

	public static String getUseStr()
	{
		return "sequence";
	}
}
