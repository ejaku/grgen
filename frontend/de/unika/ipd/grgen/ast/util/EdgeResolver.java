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
import de.unika.ipd.grgen.ast.EdgeDeclNode;
import de.unika.ipd.grgen.ast.EdgeTypeNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.TypeDeclNode;
import de.unika.ipd.grgen.ast.TypeNode;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.Scope;

/**
 * A resolver, that resolves the identifier in an edge node.
 */
public class EdgeResolver extends IdentResolver {

  private static final Class<?>[] edgeClass = {
		EdgeDeclNode.class
	};
	
	private Scope scope;

	private Coords coords;
	
	public EdgeResolver(Scope scope, Coords coords) {
		super(edgeClass);
		this.scope = scope;
		this.coords = coords;
	}

  /**
   * @see de.unika.ipd.grgen.ast.util.IdentResolver#resolveIdent(de.unika.ipd.grgen.ast.IdentNode)
   */
  protected BaseNode resolveIdent(IdentNode n) {
  	BaseNode d = n.getDecl();
  	BaseNode res = BaseNode.getErrorNode();
  	
  	if(d instanceof TypeDeclNode) {
  		TypeNode ty = (TypeNode) ((TypeDeclNode) d).getDeclType();
  		if(ty instanceof EdgeTypeNode) {
				System.out.println("here!!");
  			// TODO Symbol.Definition def = scope.defineAnonymous("edge", coords);
				//res = new EdgeDeclNode(new IdentNode(def), ty);
  		} else
  			reportError(n, "identifier \"" + n + "\" is expected to declare "
  			  + "an edge type not a \"" + ty.getName() + "\"");
  	} else if(d instanceof EdgeDeclNode) {
			res = d;
  	}
  		
  	else
  		reportError(n, "edge or edge type expected");
  	
    return res;
  }


}
