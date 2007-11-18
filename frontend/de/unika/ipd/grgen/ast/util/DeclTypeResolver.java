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
import de.unika.ipd.grgen.ast.DeclNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.BasicTypeNode;
import de.unika.ipd.grgen.ast.TypeNode;

/**
 * Resolve the type of a type declaration.
 */
public class DeclTypeResolver extends IdentResolver
{
	
	/**
	 * Make a new type decl resolver.
	 * @param classes An array of classes, the resolved node must be
	 * instance of. E.g., If you have a declaration declaring an
	 * <code>BasicTypeNode</code> instance with "int", you can give
	 * <code>new TypeNode[] { BasicTypeNode }</code> as an argument.
	 */
	public DeclTypeResolver(Class<?>[] classes)
	{
		super(classes);
	}
	
	/**
	 * A convenience function for {@link #DeclTypeResolver(Class[])}.
	 * @param cls The class
	 * @see #DeclTypeResolver(Class[]).
	 */
	public DeclTypeResolver(Class<?> cls)
	{
		this(new Class[] { cls });
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.check.Resolver#doResolve(de.unika.ipd.grgen.ast.IdentNode)
	 */
	protected BaseNode resolveIdent(IdentNode n)
	{
		BaseNode decl = n.getDecl();
		BaseNode res = BasicTypeNode.errorType;
		
		if(decl instanceof DeclNode) {
			BaseNode t = ((DeclNode) decl).getDeclType();
			if ( t instanceof TypeNode ) res = t;
		}
		
		if(res == BasicTypeNode.errorType) res = n;
		
		return res;
	}
	
	protected BaseNode getDefaultResolution()
	{
		return BasicTypeNode.errorType;
	}
}

