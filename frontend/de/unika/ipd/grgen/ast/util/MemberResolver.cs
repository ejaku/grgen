/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll
/// </summary>

namespace de.unika.ipd.grgen.ast.util
{
	using System;
	using System.Collections.Generic;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using InvalidDeclNode = de.unika.ipd.grgen.ast.decl.InvalidDeclNode;
	using MatcherDeclNode = de.unika.ipd.grgen.ast.decl.executable.MatcherDeclNode;
	using FilterFunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FilterFunctionDeclNode;
	using FunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
	using ProcedureDeclNode = de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
	using SequenceDeclNode = de.unika.ipd.grgen.ast.decl.executable.SequenceDeclNode;
	using EnumTypeNode = de.unika.ipd.grgen.ast.model.type.EnumTypeNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using Base = de.unika.ipd.grgen.util.Base;
	using Util = de.unika.ipd.grgen.util.Util;

	/// <summary>
	/// A resolver, that resolves a declaration node from an identifier.
	/// </summary>
	public class MemberResolver<T> : Base
	{
		// for error message
		private BaseNode orginalNode;

		private BaseNode unresolvedNode;
		private T resolvedNode;
		private IList<Type> triedClasses = new List<Type>();
		private int validClasses;

		/// <summary>
		/// Tries to resolve the given BaseNode.
		/// @returns True, if the BaseNode was resolved.
		///          False, when an error occurred (the error is reported).
		/// </summary>
		public virtual bool Resolve(BaseNode bn)
		{
			triedClasses.Clear();
			validClasses = 0;

			orginalNode = bn;
			if(!(orginalNode is IdentNode))
			{
				unresolvedNode = orginalNode;
				return true;
			}

			IdentNode identNode = (IdentNode)orginalNode;
			unresolvedNode = identNode.Decl;

			if(unresolvedNode is InvalidDeclNode)
			{
				DeclNode scopeDecl = identNode.Scope.Ident.Decl;
				if(scopeDecl is MatcherDeclNode || scopeDecl is SequenceDeclNode
						|| scopeDecl is ProcedureDeclNode || scopeDecl is FunctionDeclNode
						|| scopeDecl is FilterFunctionDeclNode || scopeDecl is InvalidDeclNode)
				{
					identNode.ReportError("Undefined identifier " + identNode + ".");
					return false;
				}
				else
				{
					if(scopeDecl.DeclType is EnumTypeNode)
					{
						identNode.ReportError("Resolving failure, see error messages before; unexpected enum member "
								+ identNode.ToString() + " of " + scopeDecl.DeclType.ToStringWithDeclarationCoords() + ".");
						return false;
					}
					InheritanceTypeNode typeNode = (InheritanceTypeNode)scopeDecl.DeclType;
					IDictionary<string, DeclNode> allMembers = typeNode.AllMembers;
					unresolvedNode = allMembers[identNode.ToString()];
					if(unresolvedNode == null)
					{
						identNode.ReportError("Undefined member " + identNode
								+ " of " + typeNode.ToStringWithDeclarationCoords() + ".");
						return false;
					}
				}
			}
			return true;
		}

		public virtual T Result
		{
			get
			{
				return resolvedNode;
			}
		}

		/// <summary>
		/// Returns the last resolved BaseNode, if it has the given type.
		/// Otherwise it returns null.
		/// </summary>
		public virtual S GetResult<S>(Type cls) where S : class, T
		{
			triedClasses.Add(cls);
			if(cls.IsInstanceOfType(unresolvedNode))
			{
				validClasses++;
				resolvedNode = unresolvedNode as S;
				return unresolvedNode as S;
			}

			return default(S);
		}

		/// <summary>
		/// Reports an error with all failed classes for the last resolved BaseNode.
		/// </summary>
		public virtual void Failed()
		{
			Type[] classes = new Type[triedClasses.Count];
			triedClasses.CopyTo(classes, 0);
			orginalNode.ReportError(orginalNode + " is a " + orginalNode.Kind + " but a "
					+ Util.GetStrListWithOr(classes, typeof(BaseNode), "KindStr")
					+ " is expected.");
		}

		/// <summary>
		/// Returns true, if exactly one valid result was returned for the last resolved BaseNode.
		/// Otherwise it reports an error with all expected classes.
		/// </summary>
		public virtual bool Finish()
		{
			if(validClasses == 1)
				return true;
			Failed();
			return false;
		}
	}

}
