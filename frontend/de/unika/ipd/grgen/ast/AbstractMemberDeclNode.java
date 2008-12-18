/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
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
