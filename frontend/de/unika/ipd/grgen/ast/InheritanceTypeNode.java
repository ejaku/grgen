/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.Vector;

import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.MemberInit;
import de.unika.ipd.grgen.parser.Symbol;

/**
 * Base class for compound types, that allow inheritance.
 */
public abstract class InheritanceTypeNode extends CompoundTypeNode
{
	public static final int MOD_CONST = 1;
	public static final int MOD_ABSTRACT = 2;

	protected CollectNode<IdentNode> extendUnresolved;
	protected CollectNode<BaseNode> bodyUnresolved;

	protected CollectNode<BaseNode> body;
	
	/**
	 * The modifiers for this type.
	 * An ORed combination of the constants above.
	 */
	private int modifiers = 0;

	/**
	 * The name of the external implementation of this type or null.
	 */
	private String externalName = null;

	/** Maps all member (attribute) names to their declarations. */
	private Map<String, DeclNode> allMembers = null;

	/** Contains all super types of this type (not including this itself) */
	private Collection<InheritanceTypeNode> allSuperTypes = null;

	public boolean isA(InheritanceTypeNode type) {
		assert type != null;
		return this==type || getAllSuperTypes().contains(type);
	}

	/** Returns all super types of this type (not including itself). */
	public Collection<InheritanceTypeNode> getAllSuperTypes() {
		if(allSuperTypes==null) {
			allSuperTypes = new HashSet<InheritanceTypeNode>();

			for(InheritanceTypeNode type : getDirectSuperTypes()) {
				allSuperTypes.addAll(type.getAllSuperTypes());
				allSuperTypes.add(type);
			}
		}
		return allSuperTypes;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal()
	{
		boolean res = true;
		getAllSuperTypes();

		for(DeclNode member : getAllMembers().values()) {
			if(member instanceof AbstractMemberDeclNode && !isAbstract()) {
				error.error(getIdentNode().getCoords(),
						getUseStr() + " \"" + getIdentNode() + "\" must be declared abstract, because member \"" +
						member + "\" is abstract.");
				res = false;
			}
		}

		// Check constructors for ambiguity
		Vector<ConstructorDeclNode> constrs = new Vector<ConstructorDeclNode>();
		for(BaseNode n : body.getChildren()) {
			if(n instanceof ConstructorDeclNode)
				constrs.add((ConstructorDeclNode) n);
		}
		
		for(int i = 0; i < constrs.size(); i++) {
			ConstructorDeclNode c1 = constrs.get(i);
			Vector<ConstructorParamNode> params1 = c1.getParameters().children;
			int numParams1 = params1.size();
			for(int j = i + 1; j < constrs.size(); j++) {
				ConstructorDeclNode c2 = constrs.get(j);
				Vector<ConstructorParamNode> params2 = c2.getParameters().children;
				int numParams2 = params2.size();
				int p = 0;
				boolean ambiguous = false;
				for(; p < numParams1 && p < numParams2; p++) {
					ConstructorParamNode param1 = params1.get(p);
					ConstructorParamNode param2 = params2.get(p);
					if(param1.rhs != null && param2.rhs != null)
					{
						ambiguous = true;  // non-optional part is identical => ambiguous
						break;
					}
					else if(param1.lhs.getDeclType() != param2.lhs.getDeclType())
						break;           // found a difference => not ambiguous
				}
				
				// Constructors are also ambiguous, if both have identical parameter types,
				// or if their non-optional parts have identical types and one also has an optional part.
				if(p == numParams1 && p == numParams2
						|| p == numParams1 && params2.get(p).rhs != null
						|| p == numParams2 && params1.get(p).rhs != null)
					ambiguous = true;
				
				if(ambiguous) {
					c1.reportError("Constructor is ambiguous (see constructor at "
							+ c2.getCoords() + ")");
					res = false;
				}
			}
		}

		return res;
	}

	/**
	 * Get the IR object as type.
	 * The cast must always succeed.
	 * @return The IR object as type.
	 */
	public InheritanceType getType() {
		return checkIR(InheritanceType.class);
	}

	public abstract CollectNode<? extends InheritanceTypeNode> getExtends();

    public boolean fixupDefinition(IdentNode id) {
		assert isResolved();

		if(super.fixupDefinition(id, false)) return true;

		Symbol.Definition def = null;
		for(InheritanceTypeNode inh : getExtends().getChildren()) {
			if(inh.fixupDefinition(id)) {
				Symbol.Definition newDef = id.getSymDef();
				if(def == null) def = newDef;
				else if(def != newDef) {
					error.error(getIdentNode().getCoords(), "Identifier " + id
							+ " is ambiguous (other definition at " + def.getCoords() + ")");
				}
			}
		}

		return def != null;
    }

	public void setModifiers(int modifiers) {
		this.modifiers = modifiers;
	}

	public final boolean isAbstract() {
		return (modifiers & MOD_ABSTRACT) != 0;
	}

	public final boolean isConst() {
		return (modifiers & MOD_CONST) != 0;
	}

	protected final int getIRModifiers() {
		return (isAbstract() ? InheritanceType.ABSTRACT : 0)
			| (isConst() ? InheritanceType.CONST : 0);
	}

	public void setExternalName(String extName) {
		externalName = extName;
	}

	public final String getExternalName() {
		return externalName;
	}

	public abstract Collection<? extends InheritanceTypeNode> getDirectSuperTypes();

	protected abstract void getMembers(Map<String, DeclNode> members);

	/** Returns all members (including inherited ones) of this type. */
	public Map<String, DeclNode> getAllMembers()
	{
		if(allMembers==null) {
			allMembers = new LinkedHashMap<String, DeclNode>();

			for(InheritanceTypeNode superType : getDirectSuperTypes()) {
				allMembers.putAll(superType.getAllMembers());
			}

			getMembers(allMembers);
		}

		return allMembers;
	}
	
	protected void constructIR(InheritanceType inhType) {
		for(BaseNode n : body.getChildren()) {
			if(n instanceof ConstructorDeclNode) {
				ConstructorDeclNode cd = (ConstructorDeclNode) n;
				inhType.addConstructor(cd.getConstructor());
			}
			else if(n instanceof DeclNode) {
				DeclNode decl = (DeclNode)n;
				inhType.addMember(decl.getEntity());
			}
			else if(n instanceof MemberInitNode) {
				MemberInitNode mi = (MemberInitNode)n;
				inhType.addMemberInit(mi.checkIR(MemberInit.class));
			}
			else if(n instanceof MapInitNode) {
				MapInitNode mi = (MapInitNode) n;
				inhType.addMapInit(mi.getMapInit());
			}
		}
		for(InheritanceTypeNode inh : getExtends().getChildren()) {
			inhType.addDirectSuperType((InheritanceType)inh.getType());
		}

		// to check overwriting of attributes
		inhType.getAllMembers();
    }
}
