/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.PackageIdentNode;
import de.unika.ipd.grgen.ast.ScopeOwner;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.executable.ActionDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.TopLevelMatcherDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
import de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.type.CompoundTypeNode;
import de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
import de.unika.ipd.grgen.ast.type.MatchTypeIteratedNode;
import de.unika.ipd.grgen.ast.type.MatchTypeActionNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.util.Base;

/**
 * something, that resolves a node to another node.
 *
 * @param <T> the type of the resolution result.
 */
public abstract class Resolver<T> extends Base
{
	/**
	 * Resolves a node to another node.
	 * (but doesn't replace the node in the AST)
	 *
	 * @param bn The original node to resolve.
	 * @param parent The new parent of the resolved node.
	 * @return The node the original node was resolved to (which might be the
	 *         original node itself), or null if the resolving failed.
	 */
	public abstract T resolve(BaseNode bn, BaseNode parent);

	public static boolean resolveOwner(PackageIdentNode pn)
	{
		if(pn.getOwnerSymbol().toString().equals("global")) {
			return true;
		}

		DeclNode owner = pn.getOwnerDecl();
		if(owner == null) {
			pn.reportError("Failure in resolving package of " + pn + ".");
			return false;
		}
		boolean success = owner.resolve();
		if(!success) {
			pn.reportError("Failure in resolving package of " + pn + ".");
			return false;
		}
		TypeNode tn = owner.getDeclType();
		if(tn == null) {
			pn.reportError("Failure in resolving package of " + pn + ".");
			return false;
		}
		if(!(tn instanceof CompoundTypeNode)) {
			pn.reportError("Failure in resolving package of " + pn + ".");
			return false;
		}
		CompoundTypeNode ownerType = (CompoundTypeNode)tn;
		success = ownerType.fixupDefinition(pn);
		if(!success) {
			pn.reportError("Failure in resolving the member in the package, regarding " + pn + ".");
			return false;
		}
		return true;
	}

	public static DeclNode resolveMember(TypeNode type, IdentNode member)
	{
		DeclNode result = null;

		String memberName = member.toString();
		if(type instanceof MatchTypeIteratedNode) {
			MatchTypeIteratedNode matchTypeIterated = (MatchTypeIteratedNode)type;
			if(!matchTypeIterated.resolve()) {
				return null;
			}
			TopLevelMatcherDeclNode topLevelMatcher = matchTypeIterated.getTopLevelMatcher();
			IteratedDeclNode iterated = matchTypeIterated.getIterated();
			result = matchTypeIterated.tryGetMember(member.toString());
			if(result == null) {
				String actionName = topLevelMatcher.getIdentNode().toString();
				String iteratedName = iterated.getIdentNode().toString();
				member.reportError("Unknown member " + memberName + ","
						+ " cannot find in match<" + actionName + "." + iteratedName + ">.");
			}
		} else if(type instanceof MatchTypeActionNode) {
			MatchTypeActionNode matchType = (MatchTypeActionNode)type;
			if(!matchType.resolve()) {
				return null;
			}
			ActionDeclNode action = matchType.getAction();
			result = matchType.tryGetMember(member.toString());
			if(result == null) {
				String actionName = action.getIdentNode().toString();
				member.reportError("Unknown member " + memberName + ","
						+ " cannot find in match< " + actionName + ">.");
			}
		} else if(type instanceof DefinedMatchTypeNode) {
			DefinedMatchTypeNode definedMatchType = (DefinedMatchTypeNode)type;
			if(!definedMatchType.resolve()) {
				return null;
			}
			result = definedMatchType.tryGetMember(member.toString());
			if(result == null) {
				String matchClassName = definedMatchType.getTypeName();
				member.reportError("Unknown member " + memberName + ","
						+ " cannot find in match<class " + matchClassName + ">.");
			}
		} else if(type instanceof InheritanceTypeNode) {
			ScopeOwner o = (ScopeOwner)type;
			o.fixupDefinition(member);

			InheritanceTypeNode inheritanceType = (InheritanceTypeNode)type;
			result = (MemberDeclNode)inheritanceType.tryGetMember(member.getIdent().toString());
			if(result == null) {
				String kind = inheritanceType.getKind();
				String className = inheritanceType.getTypeName();
				member.reportError("Unknown member " + memberName + ","
						+ " cannot find in " + kind + " " + className + ".");
			}
		} else {
			member.reportError("The type " + type + " does not support members (when accessing " + memberName + " of the " + type.getKind() + ").");
		}

		return result;
	}
}
