/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * An edge in a graph.
 */
public class Edge extends Entity {

  /**
   * Make a new edge.
   * @param ident The identifier for the edge.
   * @param type The type of the edge.
   * @param Is the edge nedgated.
   */
  public Edge(Ident ident, EdgeType type) {
    super("edge", ident, type);
  }
  
  /**
   * Get the edge type.
   * @return The type of the edge.
   */
  public EdgeType getEdgeType() {
  	assert getType() instanceof EdgeType : "type of edge must be edge type";
  	return (EdgeType) getType();  
  }
  
  /**
   * Check, if the edge is anonymous.
   * @return true, if the edge is anonymous, false if not. 
   */
  public boolean isAnonymous() {
  	return false;
  }
  
}
