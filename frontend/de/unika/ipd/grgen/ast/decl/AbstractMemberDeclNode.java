/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.decl;

import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.type.BasicTypeNode;

/**
 * A compound type member declaration that is abstract, i.e. has no type defined yet, but just a member name.
 */
public class AbstractMemberDeclNode extends MemberDeclNode
{
	static {
		setName(AbstractMemberDeclNode.class, "abstract member declaration");
	}

	/**
	 * @param n Identifier which declared the member.
	 * @param t Type with which the member was declared.
	 */
	public AbstractMemberDeclNode(IdentNode n, boolean isConst)
	{
		super(n, BasicTypeNode.voidType, isConst);
	}
}
