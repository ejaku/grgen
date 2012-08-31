/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.5
 * Copyright (C) 2003-2012 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.QueueInit;
import de.unika.ipd.grgen.ir.QueueItem;
import de.unika.ipd.grgen.ir.QueueType;
import de.unika.ipd.grgen.parser.Coords;

public class QueueInitNode extends ExprNode
{
	static {
		setName(QueueInitNode.class, "queue init");
	}

	private CollectNode<QueueItemNode> queueItems = new CollectNode<QueueItemNode>();

	// if queue init node is used in model, for member init
	//     then lhs != null, queueType == null
	// if queue init node is used in actions, for anonymous const queue with specified type
	//     then lhs == null, queueType != null -- adjust type of queue items to this type
	// if queue init node is used in actions, for anonymous const queue without specified type
	//     then lhs == null, queueType == null -- determine queue type from first item, all items must be exactly of this type
	private BaseNode lhsUnresolved;
	private DeclNode lhs;
	private QueueTypeNode queueType;

	public QueueInitNode(Coords coords, IdentNode member, QueueTypeNode queueType) {
		super(coords);

		if(member!=null) {
			lhsUnresolved = becomeParent(member);
		} else {
			this.queueType = queueType;
		}
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(queueItems);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("queueItems");
		return childrenNames;
	}

	public void addQueueItem(QueueItemNode item) {
		queueItems.addChild(item);
	}

	private static final MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

	@Override
	protected boolean resolveLocal() {
		if(lhsUnresolved!=null) {
			if(!lhsResolver.resolve(lhsUnresolved)) return false;
			lhs = lhsResolver.getResult(DeclNode.class);
			return lhsResolver.finish();
		} else if(queueType!=null) {
			return queueType.resolve();
		} else {
			return true;
		}
	}

	@Override
	protected boolean checkLocal() {
		boolean success = true;

		QueueTypeNode queueType;
		if(lhs!=null) {
			TypeNode type = lhs.getDeclType();
			assert type instanceof QueueTypeNode: "Lhs should be a Queue<Value>";
			queueType = (QueueTypeNode) type;
		} else if(this.queueType!=null) {
			queueType = this.queueType;
		} else {
			TypeNode queueTypeNode = getQueueType();
			if(queueTypeNode instanceof QueueTypeNode) {
				queueType = (QueueTypeNode)queueTypeNode;
			} else {
				return false;
			}
		}

		for(QueueItemNode item : queueItems.getChildren()) {
			if(item.valueExpr.getType() != queueType.valueType) {
				if(this.queueType!=null) {
					ExprNode oldValueExpr = item.valueExpr;
					item.valueExpr = item.valueExpr.adjustType(queueType.valueType, getCoords());
					item.switchParenthood(oldValueExpr, item.valueExpr);
					if(item.valueExpr == ConstNode.getInvalid()) {
						success = false;
						item.valueExpr.reportError("Value type \"" + oldValueExpr.getType()
								+ "\" of initializer doesn't fit to value type \""
								+ queueType.valueType + "\" of queue.");
					}
				} else {
					success = false;
					item.valueExpr.reportError("Value type \"" + item.valueExpr.getType()
							+ "\" of initializer doesn't fit to value type \""
							+ queueType.valueType + "\" of queue (all items must be of exactly the same type).");
				}
			}
		}

		if(lhs==null && this.queueType==null) {
			this.queueType = queueType;
		}

		if(!isConstant() && lhs!=null) {
			reportError("Only constant items allowed in queue initialization in model");
			success = false;
		}

		return success;
	}

	protected TypeNode getQueueType() {
		TypeNode itemTypeNode = queueItems.getChildren().iterator().next().valueExpr.getType();
		if(!(itemTypeNode instanceof DeclaredTypeNode)) {
			reportError("Queue items have to be of basic or enum type");
			return BasicTypeNode.errorType;
		}
		IdentNode itemTypeIdent = ((DeclaredTypeNode)itemTypeNode).getIdentNode();
		return QueueTypeNode.getQueueType(itemTypeIdent);
	}

	/**
	 * Checks whether the set only contains constants.
	 * @return True, if all set items are constant.
	 */
	protected boolean isConstant() {
		for(QueueItemNode item : queueItems.getChildren()) {
			if(!(item.valueExpr instanceof ConstNode || isEnumValue(item.valueExpr)))
				return false;
		}
		return true;
	}

	protected boolean isEnumValue(ExprNode expr) {
		if(!(expr instanceof DeclExprNode))
			return false;
		if(!(((DeclExprNode)expr).declUnresolved instanceof EnumExprNode))
			return false;
		return true;
	}

	protected boolean contains(ConstNode node) {
		for(QueueItemNode item : queueItems.getChildren()) {
			if(item.valueExpr instanceof ConstNode) {
				ConstNode itemConst = (ConstNode) item.valueExpr;
				if(node.getValue().equals(itemConst.getValue()))
					return true;
			}
		}
		return false;
	}

	@Override
	public TypeNode getType() {
		assert(isResolved());
		if(lhs!=null) {
			TypeNode type = lhs.getDeclType();
			return (SetTypeNode) type;
		} else if(queueType!=null) {
			return queueType;
		} else {
			return getQueueType();
		}
	}

	protected CollectNode<QueueItemNode> getItems() {
		return queueItems;
	}

	@Override
	protected IR constructIR() {
		Vector<QueueItem> items = new Vector<QueueItem>();
		for(QueueItemNode item : queueItems.getChildren()) {
			items.add(item.getQueueItem());
		}
		Entity member = lhs!=null ? lhs.getEntity() : null;
		QueueType type = queueType!=null ? (QueueType)queueType.getIR() : null;
		return new QueueInit(items, member, type, isConstant());
	}

	protected QueueInit getQueueInit() {
		return checkIR(QueueInit.class);
	}

	public static String getUseStr() {
		return "queue initialization";
	}
}
