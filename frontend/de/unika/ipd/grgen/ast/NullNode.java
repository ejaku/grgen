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
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.awt.Color;
import java.util.Collection;
import java.util.Collections;

/**
 * A dummy node, that is used in the case of an error.
 */
public class NullNode extends BaseNode {
	
	static {
		setName(NullNode.class, "error node");
	}
	
	protected NullNode() {
		super();
	}
	
	/*
	 public void addChild(BaseNode n) {
	 }
	 
	 public void addChildren(BaseNode n) {
	 }
	 
	 protected boolean check() {
	 return false;
	 }
	 
	 public boolean checkChild(int child, Class cls) {
	 return false;
	 }
	 
	 public int children() {
	 return 0;
	 }
	 
	 public BaseNode getChild(int i) {
	 return BaseNode.NULL;
	 }
	 
	 public Iterator getChildren() {
	 return dummy;
	 }
	 */
	public Color getNodeColor() {
		return Color.RED;
	}
	
	public String getNodeLabel() {
		return "Error";
	}
	
	public Collection<? extends BaseNode> getWalkableChildren() {
		return Collections.emptySet();
	}
	
	public boolean isError() {
		return true;
	}
	
}
