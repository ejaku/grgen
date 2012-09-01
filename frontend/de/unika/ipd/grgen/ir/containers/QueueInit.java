/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.5
 * Copyright (C) 2003-2012 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.containers;

import de.unika.ipd.grgen.ir.*;
import java.util.Collection;

public class QueueInit extends Expression {
	private Collection<QueueItem> queueItems;
	private Entity member;
	private QueueType queueType;
	private boolean isConst;
	private int anonymousQueueId;
	private static int anonymousQueueCounter;

	public QueueInit(Collection<QueueItem> queueItems, Entity member, QueueType queueType, boolean isConst) {
		super("queue init", member!=null ? member.getType() : queueType);
		this.queueItems = queueItems;
		this.member = member;
		this.queueType = queueType;
		this.isConst = isConst;
		if(member==null) {
			anonymousQueueId = anonymousQueueCounter;
			++anonymousQueueCounter;
		}
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.add(this);
		for(QueueItem queueItem : queueItems) {
			queueItem.collectNeededEntities(needs);
		}
	}

	public Collection<QueueItem> getQueueItems() {
		return queueItems;
	}

	public void setMember(Entity entity) {
		assert(member==null && entity!=null);
		member = entity;
	}

	public Entity getMember() {
		return member;
	}

	public QueueType getQueueType() {
		return queueType;
	}

	public boolean isConstant() {
		return isConst;
	}

	public String getAnonymousQueueName() {
		return "anonymous_queue_" + anonymousQueueId;
	}
}
