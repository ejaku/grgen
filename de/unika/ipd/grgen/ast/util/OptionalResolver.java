/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;

/**
 * A resolver that always succeeds.
 * This resolver even succeeds, if the resolver passed as an argument 
 * to the constructor doesn't succeed. But the passed resolver is 
 * evaluated in any case, though.
 * This has the meaning of an optional resolver. If the passed resolver
 * resolves anything, then ok, else also.
 */
public class OptionalResolver extends OneOfResolver {

	private static final Resolver alwaysSucceed = new Resolver() {
		public boolean resolve(BaseNode node, int child) {
			return true;
		}
	};

	public OptionalResolver(Resolver resolver) {
		super(new Resolver[] { resolver, alwaysSucceed });
	}
	
}
