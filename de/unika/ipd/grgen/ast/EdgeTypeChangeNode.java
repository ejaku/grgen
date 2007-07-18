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
 * @author Sebastian Hack, Adam Szalkowski
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.RetypedEdge;

/**
 *
 */
public class EdgeTypeChangeNode extends EdgeDeclNode implements EdgeCharacter {

	static {
		setName(EdgeTypeChangeNode.class, "edge type change decl");
	}

	private static final int OLD = CONSTRAINTS + 1;
	
	private static final Resolver edgeResolver =
		new DeclResolver(new Class[] { EdgeDeclNode.class});
	
	private static final Checker edgeChecker =
		new TypeChecker(EdgeTypeNode.class);
		
  public EdgeTypeChangeNode(IdentNode id, BaseNode newType, BaseNode oldid) {
  		
  	super(id, newType, TypeExprNode.getEmpty() );
	addChild(oldid);
  	addResolver(OLD, edgeResolver);
  }

  /**
   * @return the original edge for this retyped edge
   */
  public EdgeCharacter getOldEdge()
  {
    return (EdgeCharacter) getChild(OLD);
  }
  
  public IdentNode getOldEdgeIdent()
  {
	  if (getChild(OLD) instanceof IdentNode) return (IdentNode) getChild(OLD);
	  if (getChild(OLD) instanceof EdgeDeclNode)
		  return ((EdgeDeclNode) getChild(OLD)).getIdentNode();
	  
	  return IdentNode.getInvalid();
  }
  
  /**
   * @see de.unika.ipd.grgen.ast.BaseEdge#check()
   */
  protected boolean check() {
    return super.check()
	&&  checkChild(OLD, edgeChecker);
  }
  
  public Edge getEdge() {
  	return (Edge) checkIR(Edge.class);
  }

  /**
   * @see de.unika.ipd.grgen.ast.BaseEdge#constructIR()
   */
  protected IR constructIR() {
    // This cast must be ok after checking.
  	EdgeCharacter oldEdgeDecl = (EdgeCharacter) getChild(OLD);

	// This cast must be ok after checking.
	EdgeTypeNode tn = (EdgeTypeNode) getDeclType();
	EdgeType nt = tn.getEdgeType();
	IdentNode ident = getIdentNode();
		
	RetypedEdge res = new RetypedEdge(ident.getIdent(), nt, ident.getAttributes());

  	Edge edge = oldEdgeDecl.getEdge();
  	edge.setRetypedEdge(res);
	res.setOldEdge(edge);
	
	if(inheritsType()) {
		res.setTypeof( (Edge) getChild(TYPE).checkIR(Edge.class) );
	}
	  
	return res;
  }

}

