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

import java.util.Collection;
import java.util.Iterator;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.InheritanceType;
import java.util.HashSet;

/**
 * Base class for compound types, that allow inheritance.
 */
public abstract class InheritanceTypeNode extends CompoundTypeNode {
	
	public static final int MOD_CONST = 1;
	
	public static final int MOD_ABSTRACT = 2;
	
	/**
	 * The modifiers for this type.
	 * An ORed combination of the constants above.
	 */
	private int modifiers = 0;
	
	/** Index of the inheritance types collect node. */
	private final int inhIndex;
	
	/** The body index. */
	private final int bodyIndex;
	
	/** The inheritance checker. */
	private final Checker inhChecker;
	
	private static final Checker myInhChecker =
		new CollectChecker(new SimpleChecker(InheritanceTypeNode.class));
	
	/**
	 * @param bodyIndex Index of the body collect node.
	 * @param inhIndex Index of the inheritance types collect node.
	 */
	protected InheritanceTypeNode(int bodyIndex,
								  Checker bodyChecker,
								  Resolver bodyResolver,
								  int inhIndex,
								  Checker inhChecker,
								  Resolver inhResolver) {
		
		super(bodyIndex, bodyChecker, bodyResolver);
		this.inhIndex = inhIndex;
		this.inhChecker = inhChecker;
		this.bodyIndex = bodyIndex;
		
		addResolver(inhIndex, inhResolver);
	}

	public boolean isA(InheritanceTypeNode type)
	{
		if (
			(this instanceof NodeTypeNode) && !(type instanceof NodeTypeNode)
		) return false;
		
		if (
			(this instanceof EdgeTypeNode) && !(type instanceof EdgeTypeNode)
		) return false;
		
		Collection<BaseNode> superTypes = new HashSet<BaseNode>();
		superTypes.add(this);
		
		boolean changed;
		do
		{
			changed = false;
			if ( superTypes.contains(type) ) return true;
			for (BaseNode x : superTypes) {
				InheritanceTypeNode t = (InheritanceTypeNode) x;
				Collection<BaseNode> dsts = t.getDirectSuperTypes();
				changed = superTypes.addAll(dsts) || changed;
			}
		}
		while (changed);
		
		return false;
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		return super.check()
			&& checkChild(inhIndex, myInhChecker)
			&& checkChild(inhIndex, inhChecker);
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.ScopeOwner#fixupDefinition(de.unika.ipd.grgen.ast.IdentNode)
	 */
	public boolean fixupDefinition(IdentNode id) {
		boolean found = super.fixupDefinition(id, false);
		
		if(!found) {
			
			for(BaseNode n : getChild(inhIndex).getChildren()) {
				InheritanceTypeNode t = (InheritanceTypeNode)n;
				boolean result = t.fixupDefinition(id);
				
				if(found && result)
					reportError("Identifier " + id + " is ambiguous");
				
				found = found || result;
			}
		}
		
		return found;
	}
	
	protected void doGetCompatibleToTypes(Collection<TypeNode> coll) {
		for(BaseNode n : getChild(inhIndex).getChildren()) {
			InheritanceTypeNode inh = (InheritanceTypeNode)n;
			coll.add(inh);
			inh.getCompatibleToTypes(coll);
		}
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.TypeNode#doGetCastableToTypes(java.util.Collection)
	 */
/*	protected void doGetCastableToTypes(Collection<TypeNode> coll) {
		// TODO This is wrong!!!
		for(BaseNode n : getChild(inhIndex).getChildren())
			coll.add((TypeNode)n);
	} */

	
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
	
	public abstract Collection<BaseNode> getDirectSuperTypes();
}
