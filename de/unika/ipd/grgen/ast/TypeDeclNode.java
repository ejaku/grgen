/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.IR;

/**
 * Declaration of a type.
 */
public class TypeDeclNode extends DeclNode {
    
    static {
        setName(TypeDeclNode.class, "type declaration");
    }
    
    public TypeDeclNode(IdentNode i, BaseNode t) {
        super(i, t);
        
        // Set the declaration of the declared type node to this
        // node.
        if(t instanceof DeclaredTypeNode) {
            ((DeclaredTypeNode) t).setDecl(this);
        }
    }
    
    /**
     * The check succeeds, if the decl node check succeeds and the type
     * of this declaration is instance of {@link DeclaredTypeNode}.
     * @see de.unika.ipd.grgen.ast.BaseNode#check()
     */
    protected boolean check() {
        return super.check() && checkChild(TYPE, DeclaredTypeNode.class);
    }
    
    /**
     * A type declaration returns the declared type
     * as result.
     * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
     */
    protected IR constructIR() {
        TypeNode declType = (TypeNode) getDeclType();
        return declType.getIR();
    }
    
}
