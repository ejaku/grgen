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
package de.unika.ipd.grgen.ir;

/**
 * An anonymous edge.
 */
import de.unika.ipd.grgen.util.EmptyAttributes;

public class AnonymousEdge extends Edge {

  /**
   * @param ident The identifier (here will be generated one, since
   * the edge is anonymous).
   * @param type The edge type.
   */
  public AnonymousEdge(Ident ident, EdgeType type) {
    super(ident, type, EmptyAttributes.get());
  }

  /**
   * @see de.unika.ipd.grgen.ir.Edge#isAnonymous()
   */
  public boolean isAnonymous() {
    return true;
  }

}
