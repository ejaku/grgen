package de.unika.ipd.grgen.ir;

public class SubpatternUsage extends Identifiable
{
	MatchingAction subpatternAction;
	
	public SubpatternUsage(String name, Ident ident, MatchingAction subpatternAction) {
		super(name, ident);
		this.subpatternAction = subpatternAction;
	}
}
