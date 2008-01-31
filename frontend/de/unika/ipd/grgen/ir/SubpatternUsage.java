package de.unika.ipd.grgen.ir;

import java.util.List;

public class SubpatternUsage extends Identifiable
{
	MatchingAction subpatternAction;
	List<GraphEntity> subpatternConnections;
	
	public SubpatternUsage(String name, Ident ident, MatchingAction subpatternAction, List<GraphEntity> connections) {
		super(name, ident);
		this.subpatternAction = subpatternAction;
		this.subpatternConnections = connections;
	}
	
	public MatchingAction getSubpatternAction() {
		return subpatternAction;
	}
	
	public List<GraphEntity> getSubpatternConnections() {
		return subpatternConnections;
	}
}
