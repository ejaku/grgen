/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.ArrayIterator;
import java.util.Iterator;

public class Qualification extends Expression {
	
	/** The owner of the expression. */
	private final Entity owner;

	/** The member of the qualification. */
	private final Entity member;
	
	public Qualification(Entity owner, Entity member) {
		super("qual", member.getType());
		this.owner = owner;
		this.member = member;
	}
	
	public Entity getOwner() {
		return owner;
	}
	
	public Entity getMember() {
		return member;
	}
	
	public String getNodeLabel() {
		return "<" + owner + ">.<" + member + ">";
	}
	
}

