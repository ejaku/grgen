/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.util
{
using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using PackageIdentNode = de.unika.ipd.grgen.ast.PackageIdentNode;
using ScopeOwner = de.unika.ipd.grgen.ast.ScopeOwner;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using ActionDeclNode = de.unika.ipd.grgen.ast.decl.executable.ActionDeclNode;
using TopLevelMatcherDeclNode = de.unika.ipd.grgen.ast.decl.executable.TopLevelMatcherDeclNode;
using IteratedDeclNode = de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
using MemberDeclNode = de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
using CompoundTypeNode = de.unika.ipd.grgen.ast.type.CompoundTypeNode;
using DefinedMatchTypeNode = de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
using MatchTypeIteratedNode = de.unika.ipd.grgen.ast.type.MatchTypeIteratedNode;
using MatchTypeActionNode = de.unika.ipd.grgen.ast.type.MatchTypeActionNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using Base = de.unika.ipd.grgen.util.Base;

/// <summary>
/// something, that resolves a node to another node.
/// </summary>
/// @param <T> the type of the resolution result. </param>
public abstract class Resolver<T> : Base
{
	/// <summary>
	/// Resolves a node to another node.
	/// (but doesn't replace the node in the AST)
	/// </summary>
	/// <param name="bn"> The original node to resolve. </param>
	/// <param name="parent"> The new parent of the resolved node. </param>
	/// <returns> The node the original node was resolved to (which might be the
	///         original node itself), or null if the resolving failed. </returns>
	public abstract T Resolve(BaseNode bn, BaseNode parent);

	public static bool ResolveOwner(PackageIdentNode pn)
	{
		if(pn.OwnerSymbol.ToString().Equals("global"))
			return true;

		DeclNode owner = pn.OwnerDecl;
		if(owner == null)
		{
			pn.ReportError("Failure in resolving package of " + pn + ".");
			return false;
		}
		bool success = owner.Resolve();
		if(!success)
		{
			pn.ReportError("Failure in resolving package of " + pn + ".");
			return false;
		}
		TypeNode tn = owner.DeclType;
		if(tn == null)
		{
			pn.ReportError("Failure in resolving package of " + pn + ".");
			return false;
		}
		if(!(tn is CompoundTypeNode))
		{
			pn.ReportError("Failure in resolving package of " + pn + ".");
			return false;
		}
		CompoundTypeNode ownerType = (CompoundTypeNode)tn;
		success = ownerType.FixupDefinition(pn);
		if(!success)
		{
			pn.ReportError("Failure in resolving the member in the package, regarding " + pn + ".");
			return false;
		}
		return true;
	}

	public static DeclNode ResolveMember(TypeNode type, IdentNode member)
	{
		DeclNode result = null;

		string memberName = member.ToString();
		if(type is MatchTypeIteratedNode)
		{
			MatchTypeIteratedNode matchTypeIterated = (MatchTypeIteratedNode)type;
			if(!matchTypeIterated.Resolve())
				return null;
			TopLevelMatcherDeclNode topLevelMatcher = matchTypeIterated.TopLevelMatcher;
			IteratedDeclNode iterated = matchTypeIterated.Iterated;
			result = matchTypeIterated.TryGetMember(member.ToString());
			if(result == null)
			{
				string actionName = topLevelMatcher.Ident.ToString();
				string iteratedName = iterated.Ident.ToString();
				member.ReportError("Unknown member " + memberName
						+ " in match<" + actionName + "." + iteratedName + ">.");
			}
		}
		else if(type is MatchTypeActionNode)
		{
			MatchTypeActionNode matchType = (MatchTypeActionNode)type;
			if(!matchType.Resolve())
				return null;
			ActionDeclNode action = matchType.Action;
			result = matchType.TryGetMember(member.ToString());
			if(result == null)
			{
				string actionName = action.Ident.ToString();
				member.ReportError("Unknown member " + memberName
						+ " in match< " + actionName + ">.");
			}
		}
		else if(type is DefinedMatchTypeNode)
		{
			DefinedMatchTypeNode definedMatchType = (DefinedMatchTypeNode)type;
			if(!definedMatchType.Resolve())
				return null;
			result = definedMatchType.TryGetMember(member.ToString());
			if(result == null)
			{
				string matchClassName = definedMatchType.TypeName;
				member.ReportError("Unknown member " + memberName
						+ " in match<class " + matchClassName + ">.");
			}
		}
		else if(type is InheritanceTypeNode)
		{
			ScopeOwner o = (ScopeOwner)type;
			o.FixupDefinition(member);

			InheritanceTypeNode inheritanceType = (InheritanceTypeNode)type;
			result = (MemberDeclNode)inheritanceType.TryGetMember(member.IRIdent.ToString());
			if(result == null)
			{
				string kind = inheritanceType.Kind;
				string className = inheritanceType.TypeName;
				member.ReportError("Unknown member " + memberName
						+ " in " + kind + " " + className + ".");
			}
		}
		else
			member.ReportError("The type " + type + " does not support members (when accessing " + memberName + " of the " + type.Kind + ").");

		return result;
	}
}

}
