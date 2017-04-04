/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.containers;

import java.util.Collection;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.exprevals.*;

public class SetInit extends Expression {
	private Collection<SetItem> setItems;
	private Entity member;
	private SetType setType;
	private boolean isConst;
	private int anonymousSetId;
	private static int anonymousSetCounter;

	public SetInit(Collection<SetItem> setItems, Entity member, SetType setType, boolean isConst) {
		super("set init", member!=null ? member.getType() : setType);
		this.setItems = setItems;
		this.member = member;
		this.setType = setType;
		this.isConst = isConst;
		if(member==null) {
			anonymousSetId = anonymousSetCounter;
			++anonymousSetCounter;
		}
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.add(this);
		for(SetItem setItem : setItems) {
			setItem.collectNeededEntities(needs);
		}
	}

	public Collection<SetItem> getSetItems() {
		return setItems;
	}

	public void setMember(Entity entity) {
		assert(member==null && entity!=null);
		member = entity;
	}

	public Entity getMember() {
		return member;
	}

	public SetType getSetType() {
		return setType;
	}

	public void forceNotConstant() {
		isConst = false;
	}
	
	public boolean isConstant() {
		return isConst;
	}

	public String getAnonymousSetName() {
		return "anonymous_set_" + anonymousSetId;
	}
}
