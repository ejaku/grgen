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
package de.unika.ipd.grgen.ast;


/**
 * A compound type member declaration that is abstract, i.e. has no type defined yet, but just a member name.
 */
public class AbstractMemberDeclNode extends MemberDeclNode {
	static {
		setName(AbstractMemberDeclNode.class, "abstract member declaration");
	}

	/**
	 * @param n Identifier which declared the member.
	 * @param t Type with which the member was declared.
	 */
	public AbstractMemberDeclNode(IdentNode n, boolean isConst) {
		super(n, BasicTypeNode.voidType, isConst);
	}
}
