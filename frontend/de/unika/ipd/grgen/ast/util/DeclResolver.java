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
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.DeclNode;

/**
 * A resolver, that resolves a declaration node from an identifier.
 */
public class DeclResolver extends IdentResolver
{
	
	/**
	 * Make a new declaration resolver.
	 * @param classes A list of classes, the resolved node must be
	 * instance of.
	 */
	public DeclResolver(Class<?>[] classes)
	{
		super(classes);
	}
	
	/**
	 * Just a convenience constructor for {@link #DeclResolver(Class[])}
	 */
	public DeclResolver(Class<?> cls)
	{
		super(new Class[] { cls });
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.check.Resolver#resolve()
	 */
	protected BaseNode resolveIdent(IdentNode n)
	{
		return n.getDecl();
	}
	
	
	protected BaseNode getDefaultResolution()
	{
		return DeclNode.getInvalid();
	}
	
}
