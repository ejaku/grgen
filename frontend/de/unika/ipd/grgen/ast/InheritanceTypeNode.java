/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.Map;
import java.util.Vector;

import de.unika.ipd.grgen.ast.containers.*;
import de.unika.ipd.grgen.ast.exprevals.EvalStatementNode;
import de.unika.ipd.grgen.ast.exprevals.FunctionBase;
import de.unika.ipd.grgen.ast.exprevals.FunctionDeclNode;
import de.unika.ipd.grgen.ast.exprevals.ProcedureBase;
import de.unika.ipd.grgen.ast.exprevals.ProcedureDeclNode;
import de.unika.ipd.grgen.ir.containers.ArrayInit;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.exprevals.FunctionMethod;
import de.unika.ipd.grgen.ir.exprevals.MemberInit;
import de.unika.ipd.grgen.ir.exprevals.ProcedureMethod;
import de.unika.ipd.grgen.ir.containers.MapInit;
import de.unika.ipd.grgen.ir.containers.DequeInit;
import de.unika.ipd.grgen.ir.containers.SetInit;
import de.unika.ipd.grgen.parser.Symbol;

/**
 * Base class for compound types, that allow inheritance.
 */
public abstract class InheritanceTypeNode extends CompoundTypeNode
{
	public static final int MOD_CONST = 1;
	public static final int MOD_ABSTRACT = 2;

	protected CollectNode<IdentNode> extendUnresolved;
	protected CollectNode<BaseNode> bodyUnresolved;

	protected CollectNode<BaseNode> body;

	/**
	 * The modifiers for this type.
	 * An ORed combination of the constants above.
	 */
	private int modifiers = 0;

	/**
	 * The name of the external implementation of this type or null.
	 * This is for the (unsupported) "Embedding GrGen into C#" prototype.
	 */
	private String externalName = null;

	/** Maps all member (attribute) names to their declarations. */
	private Map<String, DeclNode> allMembers = null;

	/** Contains all super types of this type (not including this itself) */
	private Collection<InheritanceTypeNode> allSuperTypes = null;

	/** Contains all direct sub types of this type */
	private Collection<InheritanceTypeNode> directSubTypes = new LinkedHashSet<InheritanceTypeNode>();

	/** Contains all sub types of this type (not including this itself) */
	private Collection<InheritanceTypeNode> allSubTypes = null;

	public void addDirectSubType(InheritanceTypeNode type) {
		directSubTypes.add(type);
	}

	/** Returns all sub types of this type (not including itself). */
	protected Collection<InheritanceTypeNode> getAllSubTypes() {
		assert isResolved();

		if (allSubTypes == null) {
			allSubTypes = new HashSet<InheritanceTypeNode>();

			for(InheritanceTypeNode type : directSubTypes) {
				allSubTypes.addAll(type.getAllSubTypes());
				allSubTypes.add(type);
			}
		}
		return allSubTypes;
	}

	protected boolean isA(InheritanceTypeNode type) {
		assert type != null;
		return this==type || getAllSuperTypes().contains(type);
	}

	/** Returns all super types of this type (not including itself). */
	protected Collection<InheritanceTypeNode> getAllSuperTypes() {
		if(allSuperTypes==null) {
			allSuperTypes = new HashSet<InheritanceTypeNode>();

			for(InheritanceTypeNode type : getDirectSuperTypes()) {
				allSuperTypes.addAll(type.getAllSuperTypes());
				allSuperTypes.add(type);
			}
		}
		return allSuperTypes;
	}

	public CollectNode<BaseNode> getBody() {
		return body;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		boolean res = super.checkLocal();
		getAllSuperTypes();

		for(DeclNode member : getAllMembers().values()) {
			if(member instanceof AbstractMemberDeclNode && !isAbstract()) {
				error.error(getIdentNode().getCoords(),
						getUseStr() + " \"" + getIdentNode() + "\" must be declared abstract, because member \"" +
						member + "\" is abstract.");
				res = false;
			}
		}

		for(BaseNode n : body.getChildren()) {
			if(n instanceof DeclNode && !(n instanceof ConstructorDeclNode)) {
				DeclNode directMember = (DeclNode)n;
				if(directMember.getIdentNode().getIdent().toString().equals(getIdentNode().getIdent().toString())) {
					error.error(getIdentNode().getCoords(),
							"the member \"" + directMember.getIdentNode() + "\" must be named differently than its containing " + getUseStr() + " \"" + getIdentNode() + "\"");
				}
			}
		}

		// Check constructors for ambiguity
		Vector<ConstructorDeclNode> constrs = new Vector<ConstructorDeclNode>();
		for(BaseNode n : body.getChildren()) {
			if(n instanceof ConstructorDeclNode)
				constrs.add((ConstructorDeclNode) n);
		}

		for(int i = 0; i < constrs.size(); i++) {
			ConstructorDeclNode c1 = constrs.get(i);
			Vector<ConstructorParamNode> params1 = c1.getParameters().children;
			int numParams1 = params1.size();
			for(int j = i + 1; j < constrs.size(); j++) {
				ConstructorDeclNode c2 = constrs.get(j);
				Vector<ConstructorParamNode> params2 = c2.getParameters().children;
				int numParams2 = params2.size();
				int p = 0;
				boolean ambiguous = false;
				for(; p < numParams1 && p < numParams2; p++) {
					ConstructorParamNode param1 = params1.get(p);
					ConstructorParamNode param2 = params2.get(p);
					if(param1.rhs != null && param2.rhs != null)
					{
						ambiguous = true;  // non-optional part is identical => ambiguous
						break;
					}
					else if(param1.lhs.getDeclType() != param2.lhs.getDeclType())
						break;           // found a difference => not ambiguous
				}

				// Constructors are also ambiguous, if both have identical parameter types,
				// or if their non-optional parts have identical types and one also has an optional part.
				if(p == numParams1 && p == numParams2
						|| p == numParams1 && params2.get(p).rhs != null
						|| p == numParams2 && params1.get(p).rhs != null)
					ambiguous = true;

				if(ambiguous) {
					c1.reportError("Constructor is ambiguous (see constructor at "
							+ c2.getCoords() + ")");
					res = false;
				}
			}
		}

		return res;
	}

	/**
	 * Get the IR object as type.
	 * The cast must always succeed.
	 * @return The IR object as type.
	 */
	@Override
	public InheritanceType getType() {
		return checkIR(InheritanceType.class);
	}

	protected abstract CollectNode<? extends InheritanceTypeNode> getExtends();

	@Override
    public boolean fixupDefinition(IdentNode id) {
		assert isResolved();

		if(fixupDefinition(id, getScope(), false))
			return true;

		Symbol.Definition def = null;
		for(InheritanceTypeNode inh : getExtends().getChildren()) {
			if(inh.fixupDefinition(id)) {
				Symbol.Definition newDef = id.getSymDef();
				if(def == null) def = newDef;
				else if(def != newDef) {
					error.error(getIdentNode().getCoords(), "Identifier " + id
							+ " is ambiguous (other definition at " + def.getCoords() + ")."
							+ " There must be one unique declaration of a member, in a common parent; or different names must be used for different members. "
							+ " A method that comes in from more than one parent must be implemented locally, overriding the parental versions.");
				}
			}
		}

		return def != null;
    }

	protected void setModifiers(int modifiers) {
		this.modifiers = modifiers;
	}

	protected final boolean isAbstract() {
		return (modifiers & MOD_ABSTRACT) != 0;
	}

	public final boolean isConst() {
		return (modifiers & MOD_CONST) != 0;
	}

	protected final int getIRModifiers() {
		return (isAbstract() ? InheritanceType.ABSTRACT : 0)
			| (isConst() ? InheritanceType.CONST : 0);
	}

	protected void setExternalName(String extName) {
		externalName = extName;
	}

	protected final String getExternalName() {
		return externalName;
	}

	protected abstract Collection<? extends InheritanceTypeNode> getDirectSuperTypes();

	protected abstract void getMembers(Map<String, DeclNode> members);

	/** Returns all members (including inherited ones) of this type. */
	public Map<String, DeclNode> getAllMembers()
	{
		if(allMembers==null) {
			allMembers = new LinkedHashMap<String, DeclNode>();

			for(InheritanceTypeNode superType : getDirectSuperTypes()) {
				allMembers.putAll(superType.getAllMembers());
			}

			getMembers(allMembers);
		}

		return allMembers;
	}

	protected boolean checkStatementsInMethods() {
		boolean res = true;
		for(BaseNode n : body.getChildren()) {
			if(n instanceof FunctionDeclNode) {
				FunctionDeclNode function = (FunctionDeclNode)n;
				res &= EvalStatementNode.checkStatements(true, function, null, function.evals, true);
			} else if(n instanceof ProcedureDeclNode) {
				ProcedureDeclNode procedure = (ProcedureDeclNode)n;
				res &= EvalStatementNode.checkStatements(false, procedure, null, procedure.evals, true);
			} 
		}
		return res;
	}
	
	/** Check whether the override adheres to the signature of the base declaration */
	protected boolean checkSignatureAdhered(FunctionBase base, FunctionBase override) {
		String functionName = base.ident.toString();

		Vector<TypeNode> baseParams = base.getParameterTypes();
		Vector<TypeNode> overrideParams = override.getParameterTypes();

		// check if the number of parameters is correct
		int numBaseParams = baseParams.size();
		int numOverrideParams = overrideParams.size();
		if(numBaseParams != numOverrideParams) {
			override.reportError("The function method \"" + functionName + "\" is declared with "
					+ numBaseParams + " parameters in the base class, but overriden here with " + numOverrideParams);
			return false;
		}

		// check if the types of the parameters are correct
		boolean res = true;
		for(int i = 0; i < numBaseParams; ++i) {
			TypeNode baseParam = baseParams.get(i);
			TypeNode overrideParam = overrideParams.get(i);
			
			if(!baseParam.isEqual(overrideParam)) {
				res = false;
				override.reportError("The function method \"" + functionName + "\" differs in its " + (i+1) + ". parameter from the base class");
			}
		}
		
		// check if the return type is correct
		if(!base.getReturnType().isEqual(override.getReturnType())) {
			override.reportError("The function method \"" + functionName + "\" differs in its return type from the base class");
		}

		return res;
	}

	/** Check whether the override adheres to the signature of the base declaration */
	protected boolean checkSignatureAdhered(ProcedureBase base, ProcedureBase override) {
		String procedureName = base.ident.toString();

		Vector<TypeNode> baseParams = base.getParameterTypes();
		Vector<TypeNode> overrideParams = override.getParameterTypes();

		// check if the number of parameters is correct
		int numBaseParams = baseParams.size();
		int numOverrideParams = overrideParams.size();
		if(numBaseParams != numOverrideParams) {
			override.reportError("The procedure method \"" + procedureName + "\" is declared with "
					+ numBaseParams + " parameters in the base class, but overriden here with " + numOverrideParams);
			return false;
		}

		// check if the types of the parameters are correct
		boolean res = true;
		for(int i = 0; i < numBaseParams; ++i) {
			TypeNode baseParam = baseParams.get(i);
			TypeNode overrideParam = overrideParams.get(i);
			
			if(!baseParam.isEqual(overrideParam)) {
				res = false;
				override.reportError("The procedure method \"" + procedureName + "\" differs in its " + (i+1) + ". parameter from the base class");
			}
		}

		Vector<TypeNode> baseReturnParams = base.getReturnTypes();
		Vector<TypeNode> overrideReturnParams = override.getReturnTypes();

		// check if the number of parameters is correct
		int numBaseReturnParams = baseReturnParams.size();
		int numOverrideReturnParams = overrideReturnParams.size();
		if(numBaseReturnParams != numOverrideReturnParams) {
			override.reportError("The procedure method \"" + procedureName + "\" is declared with "
					+ numBaseReturnParams + " return parameters in the base class, but overriden here with " + numOverrideReturnParams);
			return false;
		}

		// check if the types of the parameters are correct
		for(int i = 0; i < numBaseReturnParams; ++i) {
			TypeNode baseReturnParam = baseReturnParams.get(i);
			TypeNode overrideReturnParam = overrideReturnParams.get(i);
			
			if(!baseReturnParam.isEqual(overrideReturnParam)) {
				res = false;
				override.reportError("The procedure method \"" + procedureName + "\" differs in its " + (i+1) + ". return parameter from the base class");
			}
		}

		return res;
	}

	protected void constructIR(InheritanceType inhType) {
		for(BaseNode n : body.getChildren()) {
			if(n instanceof ConstructorDeclNode) {
				ConstructorDeclNode cd = (ConstructorDeclNode) n;
				inhType.addConstructor(cd.getConstructor());
			}
			else if(n instanceof DeclNode) {
				DeclNode decl = (DeclNode)n;
				if(n instanceof FunctionDeclNode) {
					inhType.addFunctionMethod(n.checkIR(FunctionMethod.class));
				} else if(n instanceof ProcedureDeclNode) {
					inhType.addProcedureMethod(n.checkIR(ProcedureMethod.class));
				} else {
					inhType.addMember(decl.getEntity());
				}
			}
			else if(n instanceof MemberInitNode) {
				MemberInitNode mi = (MemberInitNode)n;
				IR init = mi.getIR();
				if(init instanceof MapInit) {
					inhType.addMapInit(mi.checkIR(MapInit.class));
				} else if(init instanceof SetInit) {
					inhType.addSetInit(mi.checkIR(SetInit.class));
				} else if(init instanceof ArrayInit) {
					inhType.addArrayInit(mi.checkIR(ArrayInit.class));
				} else if(init instanceof DequeInit) {
					inhType.addDequeInit(mi.checkIR(DequeInit.class));
				} else {
					inhType.addMemberInit(mi.checkIR(MemberInit.class));
				}
			}
			else if(n instanceof MapInitNode) {
				MapInitNode mi = (MapInitNode) n;
				inhType.addMapInit(mi.getMapInit());
			}
			else if(n instanceof SetInitNode) {
				SetInitNode si = (SetInitNode) n;
				inhType.addSetInit(si.getSetInit());
			}
			else if(n instanceof ArrayInitNode) {
				ArrayInitNode ai = (ArrayInitNode) n;
				inhType.addArrayInit(ai.getArrayInit());
			}
			else if(n instanceof DequeInitNode) {
				DequeInitNode di = (DequeInitNode) n;
				inhType.addDequeInit(di.getDequeInit());
			}
		}
		for(InheritanceTypeNode inh : getExtends().getChildren()) {
			inhType.addDirectSuperType((InheritanceType)inh.getType());
		}
    }

	@Override
	public String toString() {
		return getIdentNode().toString() + "(" + super.toString() + ")";
	}
}
