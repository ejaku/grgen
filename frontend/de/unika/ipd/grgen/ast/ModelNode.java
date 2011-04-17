/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.Model;
import de.unika.ipd.grgen.ir.ExternalFunction;


public class ModelNode extends DeclNode {
	static {
		setName(ModelNode.class, "model declaration");
	}

	private static final TypeNode modelType = new ModelTypeNode();

	private CollectNode<ModelNode> usedModels;

	protected CollectNode<TypeDeclNode> decls;
	private CollectNode<IdentNode> declsUnresolved;
	protected CollectNode<ExternalFunctionDeclNode> externalFuncDecls;
	private CollectNode<IdentNode> externalFuncDeclsUnresolved;
	private ModelTypeNode type;

	public ModelNode(IdentNode id, CollectNode<IdentNode> decls, CollectNode<IdentNode> externalFuncs, CollectNode<ModelNode> usedModels) {
		super(id, modelType);

		this.declsUnresolved = decls;
		becomeParent(this.declsUnresolved);
		this.externalFuncDeclsUnresolved = externalFuncs;
		becomeParent(this.externalFuncDeclsUnresolved);
		this.usedModels = usedModels;
		becomeParent(this.usedModels);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(getValidVersion(declsUnresolved, decls));
		children.add(getValidVersion(externalFuncDeclsUnresolved, externalFuncDecls));
		children.add(usedModels);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("decls");
		childrenNames.add("externalFuncDecls");
		childrenNames.add("usedModels");
		return childrenNames;
	}

	private static CollectResolver<TypeDeclNode> declsResolver = new CollectResolver<TypeDeclNode>(
		new DeclarationResolver<TypeDeclNode>(TypeDeclNode.class));
	private static CollectResolver<ExternalFunctionDeclNode> externalFunctionsResolver = new CollectResolver<ExternalFunctionDeclNode>(
			new DeclarationResolver<ExternalFunctionDeclNode>(ExternalFunctionDeclNode.class));

	private static DeclarationTypeResolver<ModelTypeNode> typeResolver = new DeclarationTypeResolver<ModelTypeNode>(ModelTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		decls = declsResolver.resolve(declsUnresolved, this);
		externalFuncDecls = externalFunctionsResolver.resolve(externalFuncDeclsUnresolved, this);
		type = typeResolver.resolve(typeUnresolved, this);

		return decls != null && externalFuncDecls!=null && type != null;
	}

	/**
	 * The main node has an ident node and a collect node with
	 * - group declarations
	 * - edge class decls
	 * - node class decls
	 * as child.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal() {
		return checkInhCycleFree();
	}

	/**
	 * Get the IR model node for this AST node.
	 * @return The model for this AST node.
	 */
	protected Model getModel() {
		return checkIR(Model.class);
	}

	/**
	 * Construct the IR object for this AST node.
	 * For a main node, this is a unit.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected Model constructIR() {
		Ident id = ident.checkIR(Ident.class);
		Model res = new Model(id);
		for(ModelNode model : usedModels.getChildren())
			res.addUsedModel(model.getModel());
		for(TypeDeclNode typeDecl : decls.getChildren()) {
			res.addType(typeDecl.getDeclType().getType());
		}
		for(ExternalFunctionDeclNode externalFunctionDecl : externalFuncDecls.getChildren()) {
			res.addExternalFunction(externalFunctionDecl.checkIR(ExternalFunction.class));
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
			   ((inhType instanceof EdgeTypeNode) && (t instanceof EdgeTypeNode)) ||
			   ((inhType instanceof ExternalTypeNode) && (t instanceof ExternalTypeNode))
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

