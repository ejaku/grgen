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
 * Created on Apr 16, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;


/**
 * A SQL basic data type.
 */
public interface DataType extends MetaBase {

	/** Type constants for the classify function. */
	int ID = 0;
	int INT = 1;
	int STRING = 2;
	int BOOLEAN = 3;
	int OTHER = 4;
	
	/**
	 * Get the SQL representation of the datatype.
	 * @return The SQL representation of the datatype.
	 */
	String getText();
	
	/**
	 * Return an integer constant (defined above) to classify this
	 * datatype.
	 * @return An integer type to classify this datatype.
	 */
	int classify();
	
	/**
	 * Give an expression that is a default initializer
	 * for this type.
	 * @return An expression that represents the default initializer
	 * for an item of this type.
	 */
	Term initValue();
	
}
