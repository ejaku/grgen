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

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.util.Util;

/**
 * An identifier resolver.
 * It's a framework for a resolver, that finds the AST node, that is
 * declared by an identifier and replaces the ident node by the resolved
 * node.
 */
public abstract class IdentResolver extends Resolver
{
	/**
	 * The class of the resolved node must be in the set, otherwise,
	 * an error is reported.
	 */
	private Class<?>[] classes;
	
	/** A string with names of the classes, which are expected. */
	private String expectList;
	
	
	/**
	 * Make a new ident resolver.
	 * @param classes An array of classes of which the resolved ident
	 * must be an instance of.
	 */
	public IdentResolver(Class<?>[] classes)
	{
		this.classes = classes;
		
		try {
			expectList = Util.getStrListWithOr(
				classes, BaseNode.class, "getUseStr");
		}
		catch(Exception e) {}
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.util.Resolver#resolve(de.unika.ipd.grgen.ast.BaseNode, int)
	 * The function resolves and IdentNode to another node (probably
	 * the declaration). If <code>n</code> at position <code>pos</code>
	 * is not an IdentNode, the method returns true and the node is
	 * considered as resolved.
	 */
	public boolean resolve(BaseNode n, int pos)
	{
		boolean res = true;
		
		assert pos < n.children() : "position exceeds children count";
		
		debug.report(NOTE, "resolving position: " + pos);
		
		BaseNode c = n.getChild(pos);
		debug.report(NOTE, "child is a: " + c.getName() + " (" + c + ")");
		
		// Check, if the desired node is an identifier.
		// If, not, everything is fine, and the method returns true.
		if(c instanceof IdentNode)
		{
			BaseNode get = resolveIdent((IdentNode) c);
			
			debug.report(NOTE, "resolved to a: " + get.getName());
			
			// Check, if the class of the resolved node is in the desired classes.
			// If that's true, replace the desired node with the resolved one.
			for(int i = 0; i < classes.length; i++)
				if(classes[i].isInstance(get))
				{
					n.replaceChild(pos, get);
					debug.report(NOTE, "child is now a: " + n.getChild(pos));
					return true;
				}
			
			reportError(c, "\"" + c + "\" is a " + get.getUseString() +
						" but a " + expectList + " is expected");
			
			n.replaceChild(pos, getDefaultResolution());
			res = false;
		}
		
		// else
		//  reportError(n, "Expected an identifier, not a \"" + c.getName() + "\"");
		return res;
	}
	
	/**
	 * Get a default resolution if the resolving fails.
	 */
	protected BaseNode getDefaultResolution()
	{
		return BaseNode.getErrorNode();
	}
	
	/**
	 * Get the resolved AST node for an Identifier.
	 * This can be the declaration, which the identifier occurs in, for
	 * example. See {@link DeclResolver} as an example.
	 * @param n The identifier.
	 * @return The resolved AST node.
	 */
	protected abstract BaseNode resolveIdent(IdentNode n);
	
}

