/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Iterator;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;

/**
 * Base class for compound types, that allow inheritance.
 */
public abstract class InheritanceTypeNode extends CompoundTypeNode {

	/** Index of the inheritance types collect node. */
	private int inhIndex;

	private final Checker inhChecker;
	
	private static final Checker myInhChecker = 
		new CollectChecker(new SimpleChecker(InheritanceTypeNode.class));

  /**
   * @param bodyIndex Index of the body collect node.
   * @param inhIndex Index of the inheritance types collect node.
   */
  protected InheritanceTypeNode(int bodyIndex,
  															Checker bodyChecker,
  															Resolver bodyResolver, 
  															int inhIndex,
  															Checker inhChecker, 
  															Resolver inhResolver) {
  		
    super(bodyIndex, bodyChecker, bodyResolver);
    this.inhIndex = inhIndex;
		this.inhChecker = inhChecker;
		
    addResolver(inhIndex, inhResolver);
  }
  
  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#check()
   */
  protected boolean check() {
    return super.check()
    	&& checkChild(inhIndex, myInhChecker) 
    	&& checkChild(inhIndex, inhChecker);
  }

  /**
   * @see de.unika.ipd.grgen.ast.ScopeOwner#fixupDefinition(de.unika.ipd.grgen.ast.IdentNode)
   */
  public boolean fixupDefinition(IdentNode id) {
  	boolean found = super.fixupDefinition(id, false);
  	
  	if(!found) {
  		
  		for(Iterator it = getChild(inhIndex).getChildren(); it.hasNext(); ) {
  			InheritanceTypeNode t = (InheritanceTypeNode) it.next();
  			boolean result = t.fixupDefinition(id);
  			
  			if(found && result)
  				reportError("Identifier " + id + " cannot be resolved unambigously");

				found = found || result;
  		}
  	}
  	
  	return found;
  }

  /**
   * @see de.unika.ipd.grgen.ast.TypeNode#doGetCastableToTypes(java.util.Collection)
   */
  protected void doGetCastableToTypes(Collection coll) {
  	for(Iterator it = getChild(inhIndex).getChildren(); it.hasNext();)
			coll.add(it.next());
  }

}
