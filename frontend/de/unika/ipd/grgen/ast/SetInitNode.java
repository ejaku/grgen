/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: MapInitNode.java 22956 2008-10-16 20:35:04Z buchwald $
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.SetInit;
import de.unika.ipd.grgen.ir.SetItem;
import de.unika.ipd.grgen.parser.Coords;

public class SetInitNode extends BaseNode
{
	static {
		setName(SetInitNode.class, "set init");
	}

	BaseNode lhsUnresolved;
	DeclNode lhs;
	CollectNode<SetItemNode> setItems = new CollectNode<SetItemNode>();

	public SetInitNode(Coords coords, IdentNode member) {
		super(coords);
		lhsUnresolved = becomeParent(member);
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(lhsUnresolved, lhs));
		children.add(setItems);
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("lhs");
		childrenNames.add("setItems");
		return childrenNames;
	}

	public void addSetItem(SetItemNode item) {
		setItems.addChild(item);
	}

	private static final MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

	protected boolean resolveLocal() {
		if(!lhsResolver.resolve(lhsUnresolved)) return false;
		lhs = lhsResolver.getResult(DeclNode.class);

		return lhsResolver.finish();
	}

	protected boolean checkLocal() {
		boolean success = true;
		TypeNode type = lhs.getDeclType();
		assert type instanceof SetTypeNode: "Lhs should be a Set[Value]";
		SetTypeNode setType = (SetTypeNode) type;

		for(SetItemNode item : setItems.getChildren()) {
			if (item.valueExpr.getType() != setType.valueType) {
				item.valueExpr.reportError("Value type \"" + item.valueExpr.getType()
						+ "\" of initializer doesn't fit to value type \""
						+ setType.valueType + "\" of set.");
				success = false;
			}
		}

		return success;
	}

	protected IR constructIR() {
		Vector<SetItem> items = new Vector<SetItem>();
		for(SetItemNode item : setItems.getChildren()) {
			items.add(item.getSetItem());
		}
		return new SetInit(lhs.getEntity(), items);
	}

	public SetInit getSetInit() {
		return checkIR(SetInit.class);
	}
}
