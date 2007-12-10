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
 * @version $Id: DeclResolver.java 16773 2007-11-18 16:30:56Z buchwald $
 */
package de.unika.ipd.grgen.ast.util;


import de.unika.ipd.grgen.ast.*;

import java.util.Collection;
import java.util.HashSet;

/**
 * A resolver, that resolves a declaration node from an identifier.
 */
public class MemberInitResolver extends IdentResolver {

	/**
	 * Make a new declaration resolver.
	 * @param classes A list of classes, the resolved node must be
	 * instance of.
	 */
	public MemberInitResolver(Class<?>[] classes) {
		super(classes);
	}

	/**
	 * Just a convenience constructor for {@link #DeclResolver(Class[])}
	 */
	public MemberInitResolver(Class<?> cls) {
		super(new Class[] { cls });
	}

	/**
	 * @see de.unika.ipd.grgen.ast.check.Resolver#resolve()
	 */
	protected BaseNode resolveIdent(IdentNode n) {
		System.out.println("resolveIdent:" + n);
		System.out.println("resolveIdent getDecl():" + n.getDecl());
		for(BaseNode p : n.getParents())
			if(p instanceof MemberInitNode)
				for(BaseNode p2 : p.getParents()) {
					assert p2 instanceof CollectNode;
					for(BaseNode p3 : p2.getParents()) {
						InheritanceTypeNode typeNode = (InheritanceTypeNode)p3;
						Collection<TypeNode> allSuperTypes = new HashSet<TypeNode>();
						typeNode.getCastableToTypes(allSuperTypes);
						System.out.println("Parents:" + p3);
						System.out.println("getCastableToTypes:" + allSuperTypes);
						for(TypeNode superType : allSuperTypes)
							;//superType.get;
					}
				}
		return n.getDecl();
	}


	protected BaseNode getDefaultResolution() {
		return DeclNode.getInvalid();
	}

}
