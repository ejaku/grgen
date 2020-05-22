/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */
package de.unika.ipd.grgen.ast.type.model;

import java.util.Collection;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.Map;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.ConstructorParamNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.MemberAccessor;
import de.unika.ipd.grgen.ast.MemberInitNode;
import de.unika.ipd.grgen.ast.decl.ConstructorDeclNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.FunctionDeclBaseNode;
import de.unika.ipd.grgen.ast.decl.FunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.ProcedureDeclBaseNode;
import de.unika.ipd.grgen.ast.decl.ProcedureDeclNode;
import de.unika.ipd.grgen.ast.decl.model.AbstractMemberDeclNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayInitNode;
import de.unika.ipd.grgen.ast.expr.deque.DequeInitNode;
import de.unika.ipd.grgen.ast.expr.map.MapInitNode;
import de.unika.ipd.grgen.ast.expr.set.SetInitNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.CompoundTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.FunctionMethod;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MemberInit;
import de.unika.ipd.grgen.ir.ProcedureMethod;
import de.unika.ipd.grgen.ir.expr.array.ArrayInit;
import de.unika.ipd.grgen.ir.expr.deque.DequeInit;
import de.unika.ipd.grgen.ir.expr.map.MapInit;
import de.unika.ipd.grgen.ir.expr.set.SetInit;
import de.unika.ipd.grgen.ir.type.InheritanceType;
import de.unika.ipd.grgen.parser.Symbol;

/**
 * Base class for compound types, that allow inheritance.
 */
public abstract class InheritanceTypeNode extends CompoundTypeNode implements MemberAccessor
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

	public void addDirectSubType(InheritanceTypeNode type)
	{
		directSubTypes.add(type);
	}

	/** Returns all sub types of this type (not including itself). */
	protected Collection<InheritanceTypeNode> getAllSubTypes()
	{
		assert isResolved();

		if(allSubTypes == null) {
			allSubTypes = new HashSet<InheritanceTypeNode>();

			for(InheritanceTypeNode type : directSubTypes) {
				allSubTypes.addAll(type.getAllSubTypes());
				allSubTypes.add(type);
			}
		}
		return allSubTypes;
	}

	public boolean isA(InheritanceTypeNode type)
	{
		assert type != null;
		return this == type || getAllSuperTypes().contains(type);
	}

	/** Returns all super types of this type (not including itself). */
	protected Collection<InheritanceTypeNode> getAllSuperTypes()
	{
		if(allSuperTypes == null) {
			allSuperTypes = new HashSet<InheritanceTypeNode>();

			for(InheritanceTypeNode type : getDirectSuperTypes()) {
				allSuperTypes.addAll(type.getAllSuperTypes());
				allSuperTypes.add(type);
			}
		}
		return allSuperTypes;
	}

	public static boolean hasCommonSubtype(InheritanceTypeNode type1, InheritanceTypeNode type2)
	{
		if(type1.isA(type2))
			return true;
		if(type2.isA(type1))
			return true;

		Collection<InheritanceTypeNode> subTypes1 = type1.getAllSubTypes();
		Collection<InheritanceTypeNode> subTypes2 = type2.getAllSubTypes();
		for(TypeNode typeNode2 : subTypes2) {
			if(subTypes1.contains(typeNode2)) {
				return true;
			}
		}
		
		return false;
	}

	public CollectNode<BaseNode> getBody()
	{
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
				error.error(getIdentNode().getCoords(), getUseStr() + " \"" + getTypeName()
						+ "\" must be declared abstract, because member \"" + member + "\" is abstract.");
				res = false;
			}
		}

		for(BaseNode child : body.getChildren()) {
			if(child instanceof DeclNode && !(child instanceof ConstructorDeclNode)) {
				DeclNode directMember = (DeclNode)child;
				if(directMember.getIdentNode().toString().equals(getIdentNode().toString())) {
					error.error(getIdentNode().getCoords(), "the member \"" + directMember.getIdentNode()
									+ "\" must be named differently than its containing " + getUseStr() + " \""
									+ getTypeName() + "\"");
				}
			}
		}

		// Check constructors for ambiguity
		Vector<ConstructorDeclNode> constrs = new Vector<ConstructorDeclNode>();
		for(BaseNode child : body.getChildren()) {
			if(child instanceof ConstructorDeclNode)
				constrs.add((ConstructorDeclNode)child);
		}

		for(int i = 0; i < constrs.size(); i++) {
			ConstructorDeclNode c1 = constrs.get(i);
			Vector<ConstructorParamNode> params1 = c1.getParameters().getChildrenAsVector();
			int numParams1 = params1.size();
			for(int j = i + 1; j < constrs.size(); j++) {
				ConstructorDeclNode c2 = constrs.get(j);
				Vector<ConstructorParamNode> params2 = c2.getParameters().getChildrenAsVector();
				int numParams2 = params2.size();
				int p = 0;
				boolean ambiguous = false;
				for(; p < numParams1 && p < numParams2; p++) {
					ConstructorParamNode param1 = params1.get(p);
					ConstructorParamNode param2 = params2.get(p);
					if(param1.rhs != null && param2.rhs != null) {
						ambiguous = true; // non-optional part is identical => ambiguous
						break;
					} else if(param1.lhs.getDeclType() != param2.lhs.getDeclType())
						break; // found a difference => not ambiguous
				}

				// Constructors are also ambiguous, if both have identical parameter types,
				// or if their non-optional parts have identical types and one also has an optional part.
				if(p == numParams1 && p == numParams2
						|| p == numParams1 && params2.get(p).rhs != null
						|| p == numParams2 && params1.get(p).rhs != null)
					ambiguous = true;

				if(ambiguous) {
					c1.reportError("Constructor is ambiguous (see constructor at " + c2.getCoords() + ")");
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
	public InheritanceType getType()
	{
		return checkIR(InheritanceType.class);
	}

	protected abstract CollectNode<? extends InheritanceTypeNode> getExtends();

	@Override
	public boolean fixupDefinition(IdentNode id)
	{
		assert isResolved();

		if(fixupDefinition(id, getScope(), false))
			return true;

		Symbol.Definition def = null;
		for(InheritanceTypeNode inh : getExtends().getChildren()) {
			if(inh.fixupDefinition(id)) {
				Symbol.Definition newDef = id.getSymDef();
				if(def == null)
					def = newDef;
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

	protected void setModifiers(int modifiers)
	{
		this.modifiers = modifiers;
	}

	public final boolean isAbstract()
	{
		return (modifiers & MOD_ABSTRACT) != 0;
	}

	public final boolean isConst()
	{
		return (modifiers & MOD_CONST) != 0;
	}

	protected final int getIRModifiers()
	{
		return (isAbstract() ? InheritanceType.ABSTRACT : 0) | (isConst() ? InheritanceType.CONST : 0);
	}

	protected void setExternalName(String extName)
	{
		externalName = extName;
	}

	protected final String getExternalName()
	{
		return externalName;
	}

	public abstract Collection<? extends InheritanceTypeNode> getDirectSuperTypes();

	public DeclNode tryGetMember(String name)
	{
		return getAllMembers().get(name);
	}

	/** Returns all members (including inherited ones) of this type. Checks the members as side effect. */
	protected void getMembers(Map<String, DeclNode> members)
	{
		assert isResolved();

		for(BaseNode child : body.getChildren()) {
			if(child instanceof ConstructorDeclNode)
				continue;

			if(child instanceof FunctionDeclNode) {
				FunctionDeclNode function = (FunctionDeclNode)child;
				checkFunctionOverride(function);
			} else if(child instanceof ProcedureDeclNode) {
				ProcedureDeclNode procedure = (ProcedureDeclNode)child;
				checkProcedureOverride(procedure);
			} else if(child instanceof DeclNode) {
				DeclNode decl = (DeclNode)child;
				DeclNode old = members.put(decl.getIdentNode().toString(), decl);
				if(old != null && !(old instanceof AbstractMemberDeclNode)) {
					// TODO this should be part of a check (that return false)
					error.error(decl.getCoords(), "member " + decl.toString() + " of " + getUseString() + " "
							+ getIdentNode() + " already defined in " + old.getParents() + "." // TODO improve error message
					);
				}
			}
		}
	}

	private void checkFunctionOverride(FunctionDeclNode function)
	{
		if(!function.isChecked())
			return;
		for(InheritanceTypeNode base : getAllSuperTypes()) {
			for(BaseNode baseChild : base.getBody().getChildren()) {
				if(baseChild instanceof FunctionDeclNode) {
					FunctionDeclNode functionBase = (FunctionDeclNode)baseChild;
					if(!functionBase.isChecked())
						continue;
					if(function.ident.toString().equals(functionBase.ident.toString()))
						checkSignatureAdhered(functionBase, function);
				}
			}
		}
	}

	private void checkProcedureOverride(ProcedureDeclNode procedure)
	{
		if(!procedure.isChecked())
			return;
		for(InheritanceTypeNode base : getAllSuperTypes()) {
			for(BaseNode baseChild : base.getBody().getChildren()) {
				if(baseChild instanceof ProcedureDeclNode) {
					ProcedureDeclNode procedureBase = (ProcedureDeclNode)baseChild;
					if(!procedureBase.isChecked())
						continue;
					if(procedure.ident.toString().equals(procedureBase.ident.toString()))
						checkSignatureAdhered(procedureBase, procedure);
				}
			}
		}
	}

	/** Returns all members (including inherited ones) of this type. Checks the members as side effect. */
	public Map<String, DeclNode> getAllMembers()
	{
		if(allMembers == null) {
			allMembers = new LinkedHashMap<String, DeclNode>();

			for(InheritanceTypeNode superType : getDirectSuperTypes()) {
				allMembers.putAll(superType.getAllMembers());
			}

			getMembers(allMembers);
		}

		return allMembers;
	}

	public boolean checkStatementsInMethods()
	{
		boolean res = true;
		for(BaseNode child : body.getChildren()) {
			if(child instanceof FunctionDeclNode) {
				FunctionDeclNode function = (FunctionDeclNode)child;
				res &= EvalStatementNode.checkStatements(true, function, null, function.evals, true);
			} else if(child instanceof ProcedureDeclNode) {
				ProcedureDeclNode procedure = (ProcedureDeclNode)child;
				res &= EvalStatementNode.checkStatements(false, procedure, null, procedure.evals, true);
			}
		}
		return res;
	}

	/** Check whether the override adheres to the signature of the base declaration */
	protected boolean checkSignatureAdhered(FunctionDeclBaseNode base, FunctionDeclBaseNode override)
	{
		String functionName = base.ident.toString();

		Vector<TypeNode> baseParams = base.getParameterTypes();
		Vector<TypeNode> overrideParams = override.getParameterTypes();

		// check if the number of parameters is correct
		int numBaseParams = baseParams.size();
		int numOverrideParams = overrideParams.size();
		if(numBaseParams != numOverrideParams) {
			override.reportError("The function method \"" + functionName + "\" is declared with " + numBaseParams
					+ " parameters in the base class, but overriden here with " + numOverrideParams);
			return false;
		}

		// check if the types of the parameters are correct
		boolean res = true;
		for(int i = 0; i < numBaseParams; ++i) {
			TypeNode baseParam = baseParams.get(i);
			TypeNode overrideParam = overrideParams.get(i);

			if(!baseParam.isEqual(overrideParam)) {
				res = false;
				override.reportError("The function method \"" + functionName + "\" differs in its " + (i + 1)
						+ ". parameter from the base class");
			}
		}

		// check if the return type is correct
		if(!base.getReturnType().isEqual(override.getReturnType())) {
			override.reportError("The function method \"" + functionName + "\" differs in its return type from the base class");
		}

		return res;
	}

	/** Check whether the override adheres to the signature of the base declaration */
	protected boolean checkSignatureAdhered(ProcedureDeclBaseNode base, ProcedureDeclBaseNode override)
	{
		String procedureName = base.ident.toString();

		Vector<TypeNode> baseParams = base.getParameterTypes();
		Vector<TypeNode> overrideParams = override.getParameterTypes();

		// check if the number of parameters is correct
		int numBaseParams = baseParams.size();
		int numOverrideParams = overrideParams.size();
		if(numBaseParams != numOverrideParams) {
			override.reportError("The procedure method \"" + procedureName + "\" is declared with " + numBaseParams
					+ " parameters in the base class, but overriden here with " + numOverrideParams);
			return false;
		}

		// check if the types of the parameters are correct
		boolean res = true;
		for(int i = 0; i < numBaseParams; ++i) {
			TypeNode baseParam = baseParams.get(i);
			TypeNode overrideParam = overrideParams.get(i);

			if(!baseParam.isEqual(overrideParam)) {
				res = false;
				override.reportError("The procedure method \"" + procedureName + "\" differs in its " + (i + 1)
						+ ". parameter from the base class");
			}
		}

		Vector<TypeNode> baseReturnParams = base.getReturnTypes();
		Vector<TypeNode> overrideReturnParams = override.getReturnTypes();

		// check if the number of parameters is correct
		int numBaseReturnParams = baseReturnParams.size();
		int numOverrideReturnParams = overrideReturnParams.size();
		if(numBaseReturnParams != numOverrideReturnParams) {
			override.reportError("The procedure method \"" + procedureName + "\" is declared with "
					+ numBaseReturnParams + " return parameters in the base class, but overriden here with "
					+ numOverrideReturnParams);
			return false;
		}

		// check if the types of the parameters are correct
		for(int i = 0; i < numBaseReturnParams; ++i) {
			TypeNode baseReturnParam = baseReturnParams.get(i);
			TypeNode overrideReturnParam = overrideReturnParams.get(i);

			if(!baseReturnParam.isEqual(overrideReturnParam)) {
				res = false;
				override.reportError("The procedure method \"" + procedureName + "\" differs in its " + (i + 1)
						+ ". return parameter from the base class");
			}
		}

		return res;
	}

	protected void constructIR(InheritanceType inhType)
	{
		for(BaseNode child : body.getChildren()) {
			constructAndAddIRChild(inhType, child);
		}
		for(InheritanceTypeNode inh : getExtends().getChildren()) {
			inhType.addDirectSuperType((InheritanceType)inh.getType());
		}
	}

	private void constructAndAddIRChild(InheritanceType inhType, BaseNode child)
	{
		if(child instanceof ConstructorDeclNode) {
			ConstructorDeclNode cd = (ConstructorDeclNode)child;
			inhType.addConstructor(cd.getConstructor());
		} else if(child instanceof DeclNode) {
			DeclNode decl = (DeclNode)child;
			if(child instanceof FunctionDeclNode) {
				inhType.addFunctionMethod(child.checkIR(FunctionMethod.class));
			} else if(child instanceof ProcedureDeclNode) {
				inhType.addProcedureMethod(child.checkIR(ProcedureMethod.class));
			} else {
				inhType.addMember(decl.getEntity());
			}
		} else if(child instanceof MemberInitNode) {
			MemberInitNode mi = (MemberInitNode)child;
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
		} else if(child instanceof MapInitNode) {
			MapInitNode mi = (MapInitNode)child;
			inhType.addMapInit(mi.getMapInit());
		} else if(child instanceof SetInitNode) {
			SetInitNode si = (SetInitNode)child;
			inhType.addSetInit(si.getSetInit());
		} else if(child instanceof ArrayInitNode) {
			ArrayInitNode ai = (ArrayInitNode)child;
			inhType.addArrayInit(ai.getArrayInit());
		} else if(child instanceof DequeInitNode) {
			DequeInitNode di = (DequeInitNode)child;
			inhType.addDequeInit(di.getDequeInit());
		}
	}

	@Override
	public String toString()
	{
		return getIdentNode().toString() + "(" + super.toString() + ")";
	}
}
