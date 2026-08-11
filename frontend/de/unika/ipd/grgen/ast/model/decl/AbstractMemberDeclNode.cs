/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.model.decl
{
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;

	/// <summary>
	/// A compound type member declaration that is abstract, i.e. has no type defined yet, but just a member name.
	/// </summary>
	public class AbstractMemberDeclNode : MemberDeclNode
	{
		static AbstractMemberDeclNode()
		{
			SetClassName(typeof(AbstractMemberDeclNode), "abstract member declaration");
		}

		/// <param name="n"> Identifier which declared the member. </param>
		/// <param name="t"> Type with which the member was declared. </param>
		public AbstractMemberDeclNode(IdentNode n, bool isConst)
			: base(n, BasicTypeNode.voidType, isConst)
		{
		}
	}

}
