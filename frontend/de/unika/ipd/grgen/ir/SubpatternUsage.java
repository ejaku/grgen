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

public class SubpatternUsage extends Identifiable {
	MatchingAction subpatternAction;
	List<GraphEntity> subpatternConnections;

	public SubpatternUsage(String name, Ident ident, MatchingAction subpatternAction, List<GraphEntity> connections) {
		super(name, ident);
		this.subpatternAction = subpatternAction;
		this.subpatternConnections = connections;
	}

	public MatchingAction getSubpatternAction() {
		return subpatternAction;
	}

	public List<GraphEntity> getSubpatternConnections() {
		return subpatternConnections;
	}
}
