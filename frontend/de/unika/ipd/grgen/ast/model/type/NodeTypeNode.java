/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */
package de.unika.ipd.grgen.ast.model.type;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.ConstructorDeclNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.OperatorEvaluator;
import de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode.Operator;
import de.unika.ipd.grgen.ast.expr.ContainerInitNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayInitNode;
import de.unika.ipd.grgen.ast.expr.deque.DequeInitNode;
import de.unika.ipd.grgen.ast.expr.map.MapInitNode;
import de.unika.ipd.grgen.ast.expr.set.SetInitNode;
import de.unika.ipd.grgen.ast.model.MemberInitNode;
import de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.model.type.NodeType;

/**
 * A class representing a node type
 */
public class NodeTypeNode extends InheritanceTypeNode
{
	static {
		setName(NodeTypeNode.class, "node type");
	}

	public static NodeTypeNode nodeType;

	private CollectNode<NodeTypeNode> extend;

	/**
	 * Create a new node type
	 * @param ext The collect node containing the node types which are extended by this type.
	 * @param body the collect node with body declarations
	 * @param modifiers Type modifiers for this type.
	 * @param externalName The name of the external implementation of this type or null.
	 */
	public NodeTypeNode(CollectNode<IdentNode> ext, CollectNode<BaseNode> body, int modifiers, String externalName)
	{
		this.extendUnresolved = ext;
		becomeParent(this.extendUnresolved);
		this.bodyUnresolved = body;
		becomeParent(this.bodyUnresolved);
		setModifiers(modifiers);
		setExternalName(externalName);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(extendUnresolved, extend));
		children.add(getValidVersion(bodyUnresolved, body));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("extends");
		childrenNames.add("body");
		return childrenNames;
	}

	private static final CollectResolver<NodeTypeNode> extendResolver =
			new CollectResolver<NodeTypeNode>(new DeclarationTypeResolver<NodeTypeNode>(NodeTypeNode.class));

	private static final CollectResolver<BaseNode> bodyResolver = new CollectResolver<BaseNode>(
			new DeclarationResolver<BaseNode>(MemberDeclNode.class, MemberInitNode.class, ConstructorDeclNode.class,
					MapInitNode.class, SetInitNode.class, ArrayInitNode.class, DequeInitNode.class,
					FunctionDeclNode.class, ProcedureDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		OperatorDeclNode.makeOp(Operator.COND, this, new TypeNode[] { BasicTypeNode.booleanType, this, this }, OperatorEvaluator.condEvaluator);

		OperatorDeclNode.makeBinOp(Operator.EQ, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
		OperatorDeclNode.makeBinOp(Operator.NE, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
		OperatorDeclNode.makeBinOp(Operator.SE, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);

		body = bodyResolver.resolve(bodyUnresolved, this);
		extend = extendResolver.resolve(extendUnresolved, this);

		// Initialize direct sub types
		if(extend != null) {
			for(InheritanceTypeNode type : extend.getChildren()) {
				type.addDirectSubType(this);
			}
		}

		return body != null && extend != null;
	}

	@Override
	protected boolean checkLocal()
	{
		boolean res = super.checkLocal();

		for(BaseNode child : body.getChildren()) {
			if(child instanceof ConstructorDeclNode
					|| child instanceof MemberInitNode
					|| child instanceof ContainerInitNode
					|| child instanceof FunctionDeclNode
					|| child instanceof ProcedureDeclNode)
				continue;
			
			DeclNode decl = (DeclNode)child;
			if(decl.getDeclType() instanceof InternalTransientObjectTypeNode) {
				decl.reportError("Only transient object classes may contain attributes of transient object class types"
						+ " (but the attribute " + decl.getIdentNode()
						+ " is of transient object class type " + decl.getDeclType().toStringWithDeclarationCoords()
						+ " in node class " + getIdentNode() + ").");
				res &= false;
			}
		}
		
		return res;
	}

	/**
	 * Get the IR node type for this AST node.
	 * @return The correctly casted IR node type.
	 */
	public NodeType getNodeType()
	{
		return checkIR(NodeType.class);
	}

	/**
	 * Construct IR object for this AST node.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR()
	{
		if(isIRAlreadySet()) { // break endless recursion in case of a member of node or container of node type
			return getIR();
		}

		NodeType nt = new NodeType(getDecl().getIdentNode().getIdent(), getIRModifiers(), getExternalName());

		setIR(nt);

		constructIR(nt);

		return nt;
	}

	@Override
	protected CollectNode<? extends InheritanceTypeNode> getExtends()
	{
		return extend;
	}

	@Override
	public void doGetCompatibleToTypes(Collection<TypeNode> coll)
	{
		assert isResolved();

		for(NodeTypeNode inh : extend.getChildren()) {
			coll.add(inh);
			coll.addAll(inh.getCompatibleToTypes());
		}
		
		coll.add(BasicTypeNode.typeType); // ~~ addCompatibility(this, BasicTypeNode.typeType);
	}

	public static String getKindStr()
	{
		return "node class";
	}

	@Override
	public Collection<NodeTypeNode> getDirectSuperTypes()
	{
		assert isResolved();

		return extend.getChildren();
	}
}
