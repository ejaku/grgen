/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Iterator;

import de.unika.ipd.grgen.ast.util.*;
import de.unika.ipd.grgen.ir.Action;
import de.unika.ipd.grgen.ir.Group;
import de.unika.ipd.grgen.ir.IR;

/**
 * A type that represents group
 */
public class GroupDeclNode extends DeclNode {

	protected static final TypeNode groupType = new TypeNode() { };

	static {
		setName(GroupDeclNode.class, "group declaration");
		setName(groupType.getClass(), "group type");
	}

	private static final int ACTIONS = LAST + 1;

	private static final String[] childrenNames = {
		declChildrenNames[0], declChildrenNames[1], "actions"
	};

	private static Checker checker = 
	  new CollectChecker(new SimpleChecker(ActionDeclNode.class)); 

	private static Resolver resolver = 
		new CollectResolver(new DeclResolver(ActionDeclNode.class));

	public GroupDeclNode(IdentNode id, CollectNode c) {
		super(id, groupType);
		setChildrenNames(childrenNames);
		addChild(c);
		addResolver(ACTIONS, resolver);
	}
	
	/**
	 * The only child of a group type node is a collect node with all
	 * declarations, which can only be declarations with action types.
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		return checkChild(ACTIONS, checker);
	}
	
	public Group getGroup() {
		return (Group) checkIR(Group.class);
	}
	
	protected IR constructIR() {
		Group group = new Group(getIdentNode().getIdent());
		Iterator children = getChild(ACTIONS).getChildren();
		while(children.hasNext()) {
			BaseNode n = (BaseNode) children.next();
			group.addMember((Action) n.checkIR(Action.class));
		}
		
		return group;
	}

}
