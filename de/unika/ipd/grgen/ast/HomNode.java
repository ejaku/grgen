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
 * @file CollectNode.java
 * @author shack
 * @date Jul 21, 2003
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.awt.Color;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import de.unika.ipd.grgen.parser.Coords;


/**
 * A node that represents a set of potentially homomorph nodes
 */
public class HomNode extends BaseNode {

	static {
		setName(HomNode.class, "homomorph");
	}
	
	class DeclChecker implements Checker {
		private Class cls;
		
		public DeclChecker(Class cls) {
			this.cls = cls;
		}
		
		 public boolean check(BaseNode node, ErrorReporter reporter) {
			 boolean decl = true;
			 IdentNode id = (IdentNode) node;
			 BaseNode type = id.getDecl().getDeclType();
			 if (!cls.isInstance(type)) {
				 node.reportError("needs to be of class " + shortClassName(cls));
				 decl = false;
			 }
			 return decl;
		 }
	};
	
  public HomNode(Coords coords) {
    super(coords);
  }

	/**
	 * Check whether all children are of same type (node or edge)
	 * and addtionally one entity may not be used in two different hom
	 * statements
	 */
  protected boolean check() {
    if(getChildren().isEmpty()) {
  		this.reportError("hom statement empty");
  		return false;
  	}
  	
  	if(!checkAllChildren(IdentNode.class)) {
		return false;
  	}
  	
  	IdentNode child = (IdentNode)getChild(0);
  	DeclChecker checker;
  	
  	if(child.getDecl().getDeclType() instanceof NodeTypeNode) {
  		checker = new DeclChecker(NodeTypeNode.class);
  	} else {
  		checker = new DeclChecker(EdgeTypeNode.class);
  	}
  	
  	return checkAllChildren(checker);
  }
  
  public Color getNodeColor() {
  	return Color.PINK;
  }
}
