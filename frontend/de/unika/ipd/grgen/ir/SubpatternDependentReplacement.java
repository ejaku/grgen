/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2008  IPD Goos, Universit"at Karlsruhe, Germany

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
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.List;

public class SubpatternDependentReplacement extends Identifiable {
	SubpatternUsage subpatternUsage;
	List<GraphEntity> replConnections;

	public SubpatternDependentReplacement(String name, Ident ident, SubpatternUsage subpatternUsage, List<GraphEntity> replConnections) {
		super(name, ident);
		this.subpatternUsage = subpatternUsage;
		this.replConnections = replConnections;
	}

	public SubpatternUsage getSubpatternUsage() {
		return subpatternUsage;
	}

	public List<GraphEntity> getReplConnections() {
		return replConnections;
	}
}
