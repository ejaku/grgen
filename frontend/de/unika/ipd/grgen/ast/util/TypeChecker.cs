/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author adam
/// </summary>

namespace de.unika.ipd.grgen.ast.util
{
	using System;
	using System.Text;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using ErrorReporter = de.unika.ipd.grgen.util.report.ErrorReporter;

	/// <summary>
	/// A checker that checks whether the declared type of the AST declaration node is one of the specified types
	/// </summary>
	public class TypeChecker : Checker
	{
		/// <summary>
		/// The types the declaration type is to be checked against </summary>
		private Type[] validTypes;

		/// <summary>
		/// Create checker with one type to check the declared type of the AST declaration node against </summary>
		public TypeChecker(Type[] types)
		{
			this.validTypes = types;
		}

		/// <summary>
		/// Create checker with the types to check the declared type of the AST declaration node against </summary>
		public TypeChecker(Type type)
			: this(new Type[] { type })
		{
		}

		/// <summary>
		/// Check if node is an instance of DeclNode
		/// if so check whether the declaration has the right type </summary>
		/// <seealso cref="de.unika.ipd.grgen.ast.check.Checker.check(de.unika.ipd.grgen.ast.BaseNode, de.unika.ipd.grgen.util.report.ErrorReporter)"/>
		public virtual bool Check(BaseNode bn, ErrorReporter reporter)
		{
			bool res = (bn is DeclNode);

			if(!res)
				bn.ReportError("Not a " + BaseNode.GetClassName(typeof(DeclNode)));
			else
			{
				TypeNode type = ((DeclNode)bn).DeclType;

				res = false;
				foreach(Type c in this.validTypes)
				{
					if(c.IsInstanceOfType(type))
					{
						res = true;
						break;
					}
				}

				if(!res)
					((DeclNode)bn).Ident.ReportError(GetErrorMsg(validTypes, bn));

			}

			return res;
		}

		protected internal static string GetExpection(Type cls)
		{
			string res = "";

			try
			{
				res = (string)cls.GetMethod("getKindStr").Invoke(null);
			}
			catch(Exception)
			{
				res = "<invalid>";
			}

			return res;
		}

		protected internal static string GetExpectionList(Type[] classes)
		{
			StringBuilder list = new StringBuilder();
			for(int i = 0; i < classes.Length; i++)
			{
				list.Append(GetExpection(classes[i]));
				if(i < classes.Length - 2)
					list.Append(", ");
				else if(i == classes.Length - 2)
					list.Append(" or ");
			}
			return list.ToString();
		}

		protected internal static string GetErrorMsg(Type[] classes, BaseNode bn)
		{
			return "expected a " + GetExpectionList(classes);
		}
	}

}
