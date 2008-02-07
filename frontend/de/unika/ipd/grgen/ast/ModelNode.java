/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
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

	CollectNode<TypeDeclNode> decls;
	CollectNode<IdentNode> declsUnresolved;
	ModelTypeNode type;

	public ModelNode(IdentNode id, CollectNode<IdentNode> decls) {
		super(id, modelType);
		this.declsUnresolved = decls;
		becomeParent(this.declsUnresolved);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(getValidVersion(declsUnresolved, decls));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("decls");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		DeclarationResolver<TypeDeclNode> declResolver =
			new DeclarationResolver<TypeDeclNode>(TypeDeclNode.class);
		CollectResolver<TypeDeclNode> declsResolver =
			new CollectResolver<TypeDeclNode>(declResolver);
		decls = declsResolver.resolve(declsUnresolved);
		successfullyResolved = decls!=null && successfullyResolved;
		DeclarationTypeResolver<ModelTypeNode> typeResolver = 
			new DeclarationTypeResolver<ModelTypeNode>(ModelTypeNode.class);
		type = typeResolver.resolve(typeUnresolved, this);
		successfullyResolved = type!=null && successfullyResolved;
		return successfullyResolved;
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
		return (Model) checkIR(Model.class);
	}

	/**
	 * Construct the IR object for this AST node.
	 * For a main node, this is a unit.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		Ident id = (Ident) ident.checkIR(Ident.class);
		Model res = new Model(id);
		for(TypeDeclNode typeDecl : decls.getChildren()) {
			res.addType(((TypeNode) typeDecl.getDeclType()).getType());
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
			TypeNode type = (TypeNode) t.getDeclType();

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

