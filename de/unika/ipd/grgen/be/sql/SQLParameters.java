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
 * Created on Mar 11, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;


/**
 * Parameters for the database.
 * Some functions return the names of the tables and columns used in the database
 * to model the graph.
 *
 * Other functions return types and other parameters to use while constructing SQL
 * queries.
 */
public interface SQLParameters {

	/** The name of the table with one column and row. */
	String getTableNeutral();
	
	/** The name of the nodes table. */
	String getTableNodes();
	
	/** The name of the edge table. */
	String getTableEdges();
	
	/** The name of the node attributes table. */
	String getTableNodeAttrs();
	
	/** The name of the edge attributes table. */
	String getTableEdgeAttrs();

	/** Get the name of the node type relation. */
	String getTableTypeRel(boolean forNode);
	
	String getColTypeRelId(boolean forNode);
	
	String getColTypeRelIsAId(boolean forNode);
	
	/** The name of the node ID column. */
	String getColNodesId();
	
	/** The name of the node type ID column. */
	String getColNodesTypeId();
	
	/** The name of the edge ID column. */
	String getColEdgesId();
	
	/** The name of the edge type ID column. */
	String getColEdgesTypeId();
	
	/** The name of the source node column. */
	String getColEdgesSrcId();
	
	/** The name of the target node column. */
	String getColEdgesTgtId();
	
	String getColNodeAttrNodeId();
	
	String getColEdgeAttrEdgeId();
	
	String getIdType();
	
	

}
