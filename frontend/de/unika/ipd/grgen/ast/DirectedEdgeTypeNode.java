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
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.CollectPairResolver;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.ConnAssert;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.MemberInit;
import java.util.Collection;
import java.util.Map;
import java.util.Vector;

public class DirectedEdgeTypeNode extends InheritanceTypeNode {
	static {
		setName(DirectedEdgeTypeNode.class, "directed edge type");
	}

	CollectNode<BaseNode> body;
	CollectNode<ConnAssertNode> cas; // connection assertions
	CollectNode<DirectedEdgeTypeNode> extend;

	/**
	 * Make a new directed edge type node.
	 * @param ext The collect node with all edge classes that this one extends.
	 * @param cas The collect node with all connection assertion of this type.
	 * @param body The body of the type declaration. It consists of basic
	 * declarations.
	 * @param modifiers The modifiers for this type.
	 * @param externalName The name of the external implementation of this type or null.
	 */
	public DirectedEdgeTypeNode(CollectNode<IdentNode> ext, CollectNode<ConnAssertNode> cas, CollectNode<BaseNode> body,
						int modifiers, String externalName) {
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
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(extendUnresolved, extend));
		children.add(getValidVersion(bodyUnresolved, body));
		children.add(cas);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("extends");
		childrenNames.add("body");
		childrenNames.add("cas");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		DeclarationPairResolver<MemberDeclNode, MemberInitNode> bodyPairResolver =
			new DeclarationPairResolver<MemberDeclNode, MemberInitNode>(MemberDeclNode.class, MemberInitNode.class);
		CollectPairResolver<BaseNode> bodyResolver =
			new CollectPairResolver<BaseNode>(bodyPairResolver);
		DeclarationTypeResolver<DirectedEdgeTypeNode> typeResolver =
			new DeclarationTypeResolver<DirectedEdgeTypeNode>(DirectedEdgeTypeNode.class);
		CollectResolver<DirectedEdgeTypeNode> extendResolver =
			new CollectResolver<DirectedEdgeTypeNode>(typeResolver);
		body = bodyResolver.resolve(bodyUnresolved);
		
		// ensure that all supertypes are resolved
		DeclarationResolver<TypeDeclNode> declResolver =
			new DeclarationResolver<TypeDeclNode>(TypeDeclNode.class);
		CollectResolver<TypeDeclNode> declsResolver =
			new CollectResolver<TypeDeclNode>(declResolver);
		CollectNode<TypeDeclNode> decls = declsResolver.resolve(extendUnresolved);
		if (decls != null) {
			decls.resolve();
		}
		
		extend = extendResolver.resolve(extendUnresolved);
		return body != null && extend != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal()  {
		return super.checkLocal();
	}

	/**
	 * Get the edge type IR object.
	 * @return The edge type IR object for this AST node.
	 */
	public EdgeType getEdgeType() {
		return (EdgeType) checkIR(EdgeType.class);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR()  {
		EdgeType et = new EdgeType(getDecl().getIdentNode().getIdent(),
								   getIRModifiers(), getExternalName());

		constructIR(et);

		for(BaseNode n : cas.getChildren()) {
			ConnAssertNode can = (ConnAssertNode)n;
			et.addConnAssert((ConnAssert)can.checkIR(ConnAssert.class));
		}

		return et;
	}

	/** @see de.unika.ipd.grgen.ast.ScopeOwner#fixupDefinition(de.unika.ipd.grgen.ast.IdentNode) */
    public boolean fixupDefinition(IdentNode id) {
		assert isResolved();

		boolean found = super.fixupDefinition(id, false);

		if(!found) {
			for(InheritanceTypeNode inh : extend.getChildren()) {
				boolean result = inh.fixupDefinition(id);

				if(found && result) {
					error.error(getIdentNode().getCoords(), "Identifier " + id + " is ambiguous");
				}

				found = found || result;
			}
		}

		return found;
    }

	protected void doGetCompatibleToTypes(Collection<TypeNode> coll) {
		assert isResolved();

		for(InheritanceTypeNode inh : extend.getChildren()) {
			coll.add(inh);
			inh.getCompatibleToTypes(coll);
		}
    }

	protected void constructIR(InheritanceType inhType) {
		for(BaseNode n : body.getChildren()) {
			if(n instanceof DeclNode) {
				DeclNode decl = (DeclNode)n;
				inhType.addMember(decl.getEntity());
			}
			else if(n instanceof MemberInitNode) {
				MemberInitNode mi = (MemberInitNode)n;
				inhType.addMemberInit((MemberInit)mi.getIR());
			}
		}
		for(InheritanceTypeNode inh : extend.getChildren()) {
			inhType.addDirectSuperType((InheritanceType)inh.getType());
		}

		// to check overwriting of attributes
		inhType.getAllMembers();
    }

	public static String getKindStr() {
		return "directed edge type";
	}

	public static String getUseStr() {
		return "directed edge type";
	}

	@Override
		public Collection<DirectedEdgeTypeNode> getDirectSuperTypes() {
		assert isResolved();

	    return extend.getChildren();
    }

	@Override
		protected void getMembers(Map<String, DeclNode> members) {
		assert isResolved();

		for(BaseNode n : body.getChildren()) {
			if(n instanceof DeclNode) {
				DeclNode decl = (DeclNode)n;

				DeclNode old=members.put(decl.getIdentNode().toString(), decl);
				if(old!=null && !(old instanceof AbstractMemberDeclNode)) {
					error.error(decl.getCoords(), "member " + decl.toString() +" of " +
									getUseString() + " " + getIdentNode() +
									" already defined in " + old.getParents() + "." // TODO improve error message
							   );
				}
			}
		}
	}
}
