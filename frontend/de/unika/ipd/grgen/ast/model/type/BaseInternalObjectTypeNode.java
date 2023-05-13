/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast.model.type;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.ConstructorDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayInitNode;
import de.unika.ipd.grgen.ast.expr.deque.DequeInitNode;
import de.unika.ipd.grgen.ast.expr.map.MapInitNode;
import de.unika.ipd.grgen.ast.expr.set.SetInitNode;
import de.unika.ipd.grgen.ast.model.MemberInitNode;
import de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;

/**
 * A class representing the base type for internal (non-node/edge) object types (i.e. classes)
 */
public abstract class BaseInternalObjectTypeNode extends InheritanceTypeNode
{
	static {
		setName(BaseInternalObjectTypeNode.class, "base internal object type");
	}

	/**
	 * Create a new base internal object type (i.e. class)
	 * @param ext The collect node containing the base object types which are extended by this type.
	 * @param body the collect node with body declarations
	 * @param modifiers Type modifiers for this type.
	 */
	public BaseInternalObjectTypeNode(CollectNode<IdentNode> ext, CollectNode<BaseNode> body, int modifiers)
	{
		this.extendUnresolved = ext;
		becomeParent(this.extendUnresolved);
		this.bodyUnresolved = body;
		becomeParent(this.bodyUnresolved);
		setModifiers(modifiers);
		setExternalName(null);
	}

	private static final CollectResolver<BaseNode> bodyResolver = new CollectResolver<BaseNode>(
			new DeclarationResolver<BaseNode>(MemberDeclNode.class, MemberInitNode.class, ConstructorDeclNode.class,
					MapInitNode.class, SetInitNode.class, ArrayInitNode.class, DequeInitNode.class,
					FunctionDeclNode.class, ProcedureDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		body = bodyResolver.resolve(bodyUnresolved, this);

		return body != null;
	}
}
