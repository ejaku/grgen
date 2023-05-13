/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Buchwald
 */
package de.unika.ipd.grgen.ast.model.type;

import java.util.Collection;
import java.util.Vector;
import java.util.Iterator;

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
import de.unika.ipd.grgen.ast.model.ConnAssertNode;
import de.unika.ipd.grgen.ast.model.MemberInitNode;
import de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.model.ConnAssert;
import de.unika.ipd.grgen.ir.model.type.EdgeType;

public abstract class EdgeTypeNode extends InheritanceTypeNode
{
	static {
		setName(EdgeTypeNode.class, "edge type");
	}

	public static ArbitraryEdgeTypeNode arbitraryEdgeType;
	public static DirectedEdgeTypeNode directedEdgeType;
	public static UndirectedEdgeTypeNode undirectedEdgeType;

	private static final CollectResolver<BaseNode> bodyResolver = new CollectResolver<BaseNode>(
			new DeclarationResolver<BaseNode>(MemberDeclNode.class, MemberInitNode.class, ConstructorDeclNode.class,
					MapInitNode.class, SetInitNode.class, ArrayInitNode.class, DequeInitNode.class,
					FunctionDeclNode.class, ProcedureDeclNode.class));

	private static final CollectResolver<EdgeTypeNode> extendResolver =
			new CollectResolver<EdgeTypeNode>(new DeclarationTypeResolver<EdgeTypeNode>(EdgeTypeNode.class));

	private CollectNode<EdgeTypeNode> extend;
	private CollectNode<ConnAssertNode> cas;

	/**
	 * Make a new edge type node.
	 * @param ext The collect node with all edge classes that this one extends.
	 * @param cas The collect node with all connection assertion of this type.
	 * @param body The body of the type declaration. It consists of basic
	 * declarations.
	 * @param modifiers The modifiers for this type.
	 * @param externalName The name of the external implementation of this type or null.
	 */
	public EdgeTypeNode(CollectNode<IdentNode> ext, CollectNode<ConnAssertNode> cas, CollectNode<BaseNode> body,
			int modifiers, String externalName)
	{
		this.extendUnresolved = ext;
		becomeParent(this.extendUnresolved);
		this.bodyUnresolved = body;
		becomeParent(this.bodyUnresolved);
		this.cas = cas;
		becomeParent(this.cas);
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
		children.add(cas);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("extends");
		childrenNames.add("body");
		childrenNames.add("cas");
		return childrenNames;
	}

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

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		boolean res = super.checkLocal();

		// check all super types to ensure their copy extends are resolved
		for(EdgeTypeNode parent : extend.getChildren()) {
			if(!parent.visitedDuringCheck()) { // only if not already visited
				parent.check();
			}
		}

		// "resolve" connection assertion inheritance,
		// after resolve to ensure everything is available, before IR building
		Vector<ConnAssertNode> connAssertsToCopy = getConnectionAssertionsToCopy();
		for(ConnAssertNode caToCopy : connAssertsToCopy) {
			cas.addChild(caToCopy);
		}

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
						+ " in edge class " + getIdentNode() + ").");
				res &= false; 
			}
		}

		// todo: check for duplicate connection assertions and issue warning about being senseless

		return res;
	}

	private Vector<ConnAssertNode> getConnectionAssertionsToCopy()
	{
		// return connection assertions to copy to prevent iterator from becoming stale, copied after iteration 
		Vector<ConnAssertNode> connAssertsToCopy = new Vector<ConnAssertNode>();

		boolean alreadyCopiedExtends = false;
		Iterator<ConnAssertNode> it = cas.getChildren().iterator();
		while(it.hasNext()) {
			ConnAssertNode ca = it.next();
			if(ca.copyExtends) {
				if(alreadyCopiedExtends) {
					reportWarning("more than one copy extends only causes double work without benefit");
				}

				for(EdgeTypeNode parent : extend.getChildren()) {
					for(ConnAssertNode caToCopy : parent.cas.getChildren()) {
						if(caToCopy.copyExtends) {
							reportError("Internal error: copy extends in parent while copying connection assertions from parent.");
							assert false;
						}
						connAssertsToCopy.add(caToCopy);
					}
				}

				it.remove();
				alreadyCopiedExtends = true;
			}
		}
		
		return connAssertsToCopy;
	}

	/**
	 * Get the edge type IR object.
	 * @return The edge type IR object for this AST node.
	 */
	public final EdgeType getEdgeType()
	{
		return checkIR(EdgeType.class);
	}

	public static String getKindStr()
	{
		return "edge class";
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

		for(EdgeTypeNode inh : extend.getChildren()) {
			coll.add(inh);
			coll.addAll(inh.getCompatibleToTypes());
		}
		
		coll.add(BasicTypeNode.typeType); // ~~ addCompatibility(this, BasicTypeNode.typeType);
	}

	@Override
	public Collection<EdgeTypeNode> getDirectSuperTypes()
	{
		assert isResolved();

		return extend.getChildren();
	}

	protected abstract void setDirectednessIR(EdgeType inhType);

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR()
	{
		if(isIRAlreadySet()) { // break endless recursion in case of a member of edge or container of edge typ
			return getIR();
		}

		EdgeType et = new EdgeType(getDecl().getIdentNode().getIdent(), getIRModifiers(), getExternalName());

		setIR(et);

		constructIR(et); // from InheritanceTypeNode

		setDirectednessIR(et); // from Undirected/Arbitrary/Directed-EdgeTypeNode

		for(ConnAssertNode can : cas.getChildren()) {
			et.addConnAssert(can.checkIR(ConnAssert.class));
		}

		return et;
	}
}
