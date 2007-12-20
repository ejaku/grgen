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

import de.unika.ipd.grgen.ast.util.*;

import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.MemberInit;
import java.util.Collection;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.Map;

/**
 * Base class for compound types, that allow inheritance.
 */
public abstract class InheritanceTypeNode extends CompoundTypeNode 
{
	public static final int MOD_CONST = 1;
	public static final int MOD_ABSTRACT = 2;

	protected static final int EXTENDS = 0;
	protected static final int BODY = 1;

	private static final String[] childrenNames = {
		"extends", "body"
	};

	private static final Checker bodyChecker =
		new CollectChecker(new SimpleChecker(new Class[] {MemberDeclNode.class, MemberInitNode.class}));

	private static final Resolver bodyResolver =
		new CollectResolver(new DeclResolver(new Class[] {MemberDeclNode.class, MemberInitNode.class}));

	/**
	 * The modifiers for this type.
	 * An ORed combination of the constants above.
	 */
	private int modifiers = 0;

	/**
	 * The name of the external implementation of this type or null.
	 */
	private String externalName = null;

	/** The inheritance checker. */
	private final Checker inhChecker;

	private static final Checker myInhChecker =
		new CollectChecker(new SimpleChecker(InheritanceTypeNode.class));

	/** Maps all member (attribute) names to their declarations. */
	private Map<String, DeclNode> allMembers = null;

	/** Contains all super types of this type (not including this itself) */
	private Collection<InheritanceTypeNode> allSuperTypes = null;

	/**
	 * @param bodyIndex Index of the body collect node.
	 * @param inhIndex Index of the inheritance types collect node.
	 */
	protected InheritanceTypeNode(CollectNode ext,
								  CollectNode body,
								  Checker inhChecker,
								  Resolver inhResolver) 
	{
		super(BODY, bodyChecker, bodyResolver);
		this.inhChecker = inhChecker;

		addChild(ext);
		addChild(body);

		setChildrenNames(childrenNames);
		setResolver(EXTENDS, inhResolver);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#doResolve() */
	protected boolean doResolve() {
		if(isResolved()) {
			return getResolve();
		}
		
		boolean successfullyResolved = resolve();
		successfullyResolved = getChild(EXTENDS).doResolve() && successfullyResolved;
		successfullyResolved = getChild(BODY).doResolve() && successfullyResolved;
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#doCheck() */
	protected boolean doCheck() {
		if(!getResolve()) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}
		
		boolean successfullyChecked = getCheck();
		if(successfullyChecked) {
			successfullyChecked = getTypeCheck();
		}
		successfullyChecked = getChild(EXTENDS).doCheck() && successfullyChecked;
		successfullyChecked = getChild(BODY).doCheck() && successfullyChecked;
		return successfullyChecked;
	}
	
	public boolean isA(InheritanceTypeNode type) {
		assert type != null;
		return this==type || getAllSuperTypes().contains(type);
	}

	/**
	 * Returns all super types of this type (not including itself).
	 */
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

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() 
	{
		getAllMembers();
		getAllSuperTypes();
		return super.check()
			&& checkChild(EXTENDS, myInhChecker)
			&& checkChild(EXTENDS, inhChecker);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.ScopeOwner#fixupDefinition(de.unika.ipd.grgen.ast.IdentNode)
	 */
	public boolean fixupDefinition(IdentNode id) 
	{
		boolean found = super.fixupDefinition(id, false);

		if(!found) {
			for(BaseNode n : getChild(EXTENDS).getChildren()) {
				InheritanceTypeNode t = (InheritanceTypeNode)n;
				boolean result = t.fixupDefinition(id);

				if(found && result) {
					reportError("Identifier " + id + " is ambiguous");
				}
				
				found = found || result;
			}
		}

		return found;
	}

	protected void doGetCompatibleToTypes(Collection<TypeNode> coll) 
	{
		for(BaseNode n : getChild(EXTENDS).getChildren()) {
			InheritanceTypeNode inh = (InheritanceTypeNode)n;
			coll.add(inh);
			inh.getCompatibleToTypes(coll);
		}
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

	public Collection<InheritanceTypeNode> getDirectSuperTypes() {
		return (Collection<InheritanceTypeNode>)(Collection)getChild(EXTENDS).getChildren();
	}

	private void getMembers(Map<String, DeclNode> members) {
		for(BaseNode n : getChild(BODY).getChildren()) {
			if(n instanceof DeclNode) {
				DeclNode decl = (DeclNode)n;

				DeclNode old=members.put(decl.getIdentNode().toString(), decl);
				if(old!=null) {
					error.error(decl.getCoords(), decl.toString() +" of " + getUseString() + " " + getIdentNode() + " already defined. " +
									"It is also declared in " + old.getParents() + "." // TODO improve error message
							   );
				}
			}
		}
	}

	/**
	 * Returns all members (including inherited ones) of this type.
	 */
	public Map<String, DeclNode> getAllMembers() 
	{
		if(allMembers==null) {
			allMembers = new LinkedHashMap<String, DeclNode>();

			for(InheritanceTypeNode superType : getDirectSuperTypes()) {
				allMembers.putAll(superType.getAllMembers());
			}

			getMembers(allMembers);
		}
		//System.out.println("+++++++ getAllSuperTypes: " + getAllSuperTypes());

		return allMembers;
	}

	protected void constructIR(InheritanceType inhType) 
	{
		for(BaseNode n : getChild(BODY).getChildren()) {
			if(n instanceof DeclNode) {
				DeclNode decl = (DeclNode)n;
				inhType.addMember(decl.getEntity());
			}
			else if(n instanceof MemberInitNode) {
				MemberInitNode mi = (MemberInitNode)n;
				inhType.addMemberInit((MemberInit)mi.getIR());
			}
		}
		for(BaseNode n : getChild(EXTENDS).getChildren()) {
			InheritanceTypeNode x = (InheritanceTypeNode)n;
			inhType.addDirectSuperType((InheritanceType)x.getType());
		}

		// to check overwriting of attributes
		inhType.getAllMembers();
	}
}
