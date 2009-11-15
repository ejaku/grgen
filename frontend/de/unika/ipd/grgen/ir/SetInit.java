/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: MapInit.java 22945 2008-10-16 16:02:13Z moritz $
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;

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

	public boolean isConstant() {
		return isConst;
	}

	public String getAnonymnousSetName() {
		return "anonymous_set_" + anonymousSetId;
	}
}
