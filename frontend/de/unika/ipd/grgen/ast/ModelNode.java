/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * ModelNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashSet;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.Model;


public class ModelNode extends DeclNode {
	static {
		setName(ModelNode.class, "model declaration");
	}

	protected static final TypeNode modelType = new ModelTypeNode();

	private CollectNode<ModelNode> usedModels;

	CollectNode<TypeDeclNode> decls;
	CollectNode<IdentNode> declsUnresolved;
	ModelTypeNode type;

	public ModelNode(IdentNode id, CollectNode<IdentNode> decls, CollectNode<ModelNode> usedModels) {
		super(id, modelType);

		this.declsUnresolved = decls;
		becomeParent(this.declsUnresolved);
		this.usedModels = usedModels;
		becomeParent(this.usedModels);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(getValidVersion(declsUnresolved, decls));
		children.add(usedModels);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("decls");
		childrenNames.add("usedModels");
		return childrenNames;
	}

	CollectResolver<TypeDeclNode> declsResolver = new CollectResolver<TypeDeclNode>(
		new DeclarationResolver<TypeDeclNode>(TypeDeclNode.class));

	DeclarationTypeResolver<ModelTypeNode> typeResolver = new DeclarationTypeResolver<ModelTypeNode>(ModelTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		decls = declsResolver.resolve(declsUnresolved, this);
		type = typeResolver.resolve(typeUnresolved, this);

		return decls != null && type != null;
	}

	/**
	 * The main node has an ident node and a collect node with
	 * - group declarations
	 * - edge class decls
	 * - node class decls
	 * as child.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		return checkInhCycleFree();
	}

	/**
	 * Get the IR model node for this AST node.
	 * @return The model for this AST node.
	 */
	public Model getModel() {
		return checkIR(Model.class);
	}

	/**
	 * Construct the IR object for this AST node.
	 * For a main node, this is a unit.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		Ident id = ident.checkIR(Ident.class);
		Model res = new Model(id);
		for(ModelNode model : usedModels.getChildren())
			res.addUsedModel(model.getModel());
		for(TypeDeclNode typeDecl : decls.getChildren()) {
			res.addType(typeDecl.getDeclType().getType());
		}
		return res;
	}

	private boolean checkInhCycleFree_rec(InheritanceTypeNode inhType,
										  Collection<BaseNode> inProgress,
										  Collection<BaseNode> done) {
		inProgress.add(inhType);
		for (BaseNode t : inhType.getDirectSuperTypes()) {
			if ( ! (t instanceof InheritanceTypeNode)) {
				continue;
			}

			assert (
					   ((inhType instanceof NodeTypeNode) && (t instanceof NodeTypeNode)) ||
				((inhType instanceof EdgeTypeNode) && (t instanceof EdgeTypeNode))
			): "nodes should extend nodes and edges should extend edges";

			InheritanceTypeNode superType = (InheritanceTypeNode) t;

			if ( inProgress.contains(superType) ) {
				inhType.getIdentNode().reportError(
					"\"" + inhType.getIdentNode() + "\" extends \"" + superType.getIdentNode() +
						"\", which introduces a cycle to the type hierarchy");
				return false;
			}
			if ( ! done.contains(superType) ) {
				if ( ! checkInhCycleFree_rec(superType, inProgress, done) ) {
					return false;
				}
			}
		}
		inProgress.remove(inhType);
		done.add(inhType);
		return true;
	}

	/**
	 * ensure there are no cycles in the inheritance hierarchy
	 * @return	<code>true</code> if there are no cycles,
	 * 			<code>false</code> otherwise
	 */
	private boolean checkInhCycleFree() {
		Collection<TypeDeclNode> coll = decls.getChildren();
		for (TypeDeclNode t : coll) {
			TypeNode type = t.getDeclType();

			if ( !(type instanceof InheritanceTypeNode) ) {
				continue ;
			}

			Collection<BaseNode> inProgress = new HashSet<BaseNode>();
			Collection<BaseNode> done = new HashSet<BaseNode>();

			boolean isCycleFree = checkInhCycleFree_rec( (InheritanceTypeNode)type, inProgress, done);

			if ( ! isCycleFree ) {
				return false;
			}
		}
		return true;
	}

	@Override
	public ModelTypeNode getDeclType() {
		assert isResolved();

		return type;
	}

	/*
	 Collection<BaseNode> alreadyExtended = new HashSet<BaseNode>();
	 TypeNode type = (TypeNode) ((TypeDeclNode)t).getDeclType();
	 alreadyExtended.add(type);

	 for ( BaseNode tt : alreadyExtended ) {

	 if ( !(tt instanceof InheritanceTypeNode) ) continue ;

	 InheritanceTypeNode inhType = (InheritanceTypeNode) tt;
	 Collection<BaseNode> superTypes = inhType.getDirectSuperTypes();

	 for ( BaseNode s : superTypes ) {
	 if ( alreadyExtended.contains(s) ) {
	 s.reportError("extending \"" + s + "\" causes cyclic inheritance");
	 return false;
	 }
	 }
	 alreadyExtended.addAll(superTypes);
	 }
	 */
}

