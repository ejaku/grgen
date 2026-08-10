/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
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
import java.util.List;
import java.util.ArrayList;

/**
 * AST node for a graph rewrite sequence definition.
 */
public class SequenceDeclNode extends DeclNode
{
	static {
		setClassName(SequenceDeclNode.class, "sequence declaration");
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
		List<BaseNode> children = new ArrayList<BaseNode>();
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
		List<String> childrenNames = new ArrayList<String>();
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
	public Sequence getIRSequence()
	{
		return checkIR(Sequence.class);
	}

	public List<DeclNode> getParamDecls()
	{
		return new ArrayList<DeclNode>(inParams.getChildrenExact());
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR()
	{
		Sequence sequence = new Sequence(getIdent().getIRIdent(), exec.checkIR(Exec.class));
		for(ExecVarDeclNode inParam : inParams.getChildrenExact()) {
			sequence.addInParam(inParam.checkIR(ExecVariable.class));
		}
		for(ExecVarDeclNode outParam : outParams.getChildrenExact()) {
			sequence.addOutParam(outParam.checkIR(ExecVariable.class));
		}
		return sequence;
	}

	@Override
	public TypeNode getDeclType()
	{
		assert isResolved();

		return type;
	}

	public static String getKindStr()
	{
		return "sequence";
	}
}
