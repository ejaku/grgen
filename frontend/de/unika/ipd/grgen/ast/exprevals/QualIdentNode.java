/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.Qualification;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node that represents a qualified identifier
 * i.e. expressions like this one: a.b.c.d
 */
public class QualIdentNode extends BaseNode implements DeclaredCharacter {
	static {
		setName(QualIdentNode.class, "Qualification");
	}

	protected IdentNode ownerUnresolved;
	private DeclNode owner;

	protected IdentNode memberUnresolved;
	private MemberDeclNode member;
	private NodeDeclNode node;
	private EdgeDeclNode edge;
	private VarDeclNode var;

	/**
	 * Make a new identifier qualify node.
	 * @param coords The coordinates.
	 */
	public QualIdentNode(Coords coords, IdentNode owner, IdentNode member) {
		super(coords);
		this.ownerUnresolved = owner;
		ownerUnresolved.getCoords();
		becomeParent(this.ownerUnresolved);
		this.memberUnresolved = member;
		becomeParent(this.memberUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(ownerUnresolved, owner));
		children.add(getValidVersion(memberUnresolved, member, node, edge, var));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("owner");
		childrenNames.add("member");
		return childrenNames;
	}

	private static final DeclarationResolver<DeclNode> ownerResolver = new DeclarationResolver<DeclNode>(DeclNode.class);
	private static final DeclarationResolver<MemberDeclNode> memberResolver = new DeclarationResolver<MemberDeclNode>(MemberDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		/* 1) resolve left hand side identifier, yielding a declaration of a type owning a scope
		 * 2) the scope owned by the lhs allows the ident node of the right hand side to fix/find its definition therein
		 * 3) resolve now complete/correct right hand side identifier into its declaration */
		boolean res = fixupDefinition(ownerUnresolved, ownerUnresolved.getScope());
		if(!res) return false;

		boolean successfullyResolved = true;
		owner = ownerResolver.resolve(ownerUnresolved, this);
		successfullyResolved = owner!=null && successfullyResolved;
		boolean ownerResolveResult = owner!=null && owner.resolve();

		if (!ownerResolveResult) {
			// member can not be resolved due to inaccessible owner
			return false;
		}

		if (ownerResolveResult && owner != null) {
			if (owner instanceof NodeCharacter || owner instanceof EdgeCharacter) {
				TypeNode ownerType = owner.getDeclType();
				if(ownerType instanceof ScopeOwner) {
					ScopeOwner o = (ScopeOwner) ownerType;
					o.fixupDefinition(memberUnresolved);
					member = memberResolver.resolve(memberUnresolved, this);
					successfullyResolved = member!=null && successfullyResolved;
				} else {
					reportError("Left hand side of '.' does not own a scope");
					successfullyResolved = false;
				}
			} else if(owner instanceof VarDeclNode && owner.typeUnresolved instanceof MatchTypeNode) {
				MatchTypeNode matchType = (MatchTypeNode)owner.getDeclType();
				if(!matchType.resolve()) {
					reportError("Unkown test/rule referenced by match type in filter function");
					return false;
				}
				TestDeclNode test = matchType.getTest();
				if(!test.resolve()) {
					reportError("Error in test/rule referenced by match type in filter function");
					return false;
				}
				node = test.tryGetNode(memberUnresolved);
				edge = test.tryGetEdge(memberUnresolved);
				var = test.tryGetVar(memberUnresolved);
				if(node==null && edge==null && var==null) {
					reportError("Unknown member, can't find in test/rule referenced by match type in filter function");
					successfullyResolved = false;
				}
			} else {
				reportError("Left hand side of '.' is neither a node nor an edge (nor a match type)");
				successfullyResolved = false;
			}
		} else {
			reportError("Left hand side of '.' is neither a node nor an edge (nor a match type)");
			successfullyResolved = false;
		}

		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.DeclaredCharacter#getDecl() */
	public MemberDeclNode getDecl() {
		assert isResolved();

		return member;
	}

	protected DeclNode getOwner() {
		assert isResolved();

		return owner;
	}
	
	public boolean isMatchAssignment() {
		assert isResolved();

		return node!=null || edge!=null || var!=null;
	}
	
	public NodeDeclNode getNodeFromMatch() {
		assert isResolved();

		return node;
	}

	public EdgeDeclNode getEdgeFromMatch() {
		assert isResolved();

		return edge;
	}
	
	public VarDeclNode getVarFromMatch() {
		assert isResolved();

		return var;
	}

	@Override
	protected IR constructIR() {
		Entity ownerIR = owner.checkIR(Entity.class);
		Entity memberIR;
		if(member!=null)
			memberIR = member.checkIR(Entity.class);
		else if(node!=null)
			memberIR = node.checkIR(Entity.class);
		else if(edge!=null)
			memberIR = edge.checkIR(Entity.class);
		else
			memberIR = var.checkIR(Entity.class);

		return new Qualification(ownerIR, memberIR);
	}

	public static String getKindStr() {
		return "member";
	}

	public static String getUseStr() {
		return "qualified identifier";
	}
}
