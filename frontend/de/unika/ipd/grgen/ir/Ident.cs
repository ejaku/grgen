/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ir
{

	using System;
	using System.Collections.Generic;

	using Coords = de.unika.ipd.grgen.parser.Coords;
	using Scope = de.unika.ipd.grgen.parser.Scope;
	using Symbol = de.unika.ipd.grgen.parser.Symbol;
	using SymbolTable = de.unika.ipd.grgen.parser.SymbolTable;
	using Annotated = de.unika.ipd.grgen.util.Annotated;
	using Annotations = de.unika.ipd.grgen.util.Annotations;
	using EmptyAnnotations = de.unika.ipd.grgen.util.EmptyAnnotations;

	/// <summary>
	/// A class representing an identifier.
	/// </summary>
	public class Ident : IR, IComparable<Ident>, Annotated
	{
		/// <summary>
		/// Symbol table recording all identifiers. </summary>
		private static Dictionary<string, Ident> identifiers = new Dictionary<string, Ident>();

		/// <summary>
		/// Text of the identifier </summary>
		private readonly string text;

		private readonly SymbolTable symTab;

		private readonly Scope scope;

		/// <summary>
		/// The scope/namespace the identifier was defined in. </summary>
		//private final String scope;

		/// <summary>
		/// location of the definition of the identifier </summary>
		private readonly Coords def;

		/// <summary>
		/// The annotations for the identifier. </summary>
		private readonly Annotations annots;

		/// <summary>
		/// A precomputed hash code. </summary>
		private readonly int precomputedHashCode;

		/// <summary>
		/// New Identifier. </summary>
		/// <param name="text"> The text of the identifier. </param>
		/// <param name="scope"> The scope/namespace of the identifier. </param>
		/// <param name="def"> The location of the definition of the identifier. </param>
		/// <param name="annots"> The annotations of this identifier
		/// (Each identifier can carry several annotations which serve as meta information usable by backend components). </param>
		private Ident(string text, SymbolTable symTab, Scope scope, Coords def, Annotations annots)
			: base("ident")
		{
			this.text = text;
			this.scope = scope;
			this.symTab = symTab;
			this.def = def;
			this.annots = annots;
			this.precomputedHashCode = (symTab.Name + ":" + text).GetHashCode();
		}

		/// <summary>
		/// New Identifier. </summary>
		/// <param name="text"> The text of the identifier. </param>
		/// <param name="def"> The location of the definition of the identifier. </param>
		private Ident(string text, Coords def, Annotations annots)
			: this(text, SymbolTable.Invalid, Scope.Invalid, def, annots)
		{
		}

		// for internal code generation (in contrast to parsing/buildup from AST)
		public Ident(string text, Coords def)
			: this(text, def, EmptyAnnotations.Get())
		{
		}

		/// <summary>
		/// The string of an identifier is its text. </summary>
		///  <seealso cref="java.lang.Object.toString() "/>
		public override string ToString()
		{
			return text;
		}

		/// <returns> The location where the identifier was defined. </returns>
		public virtual Coords Coords
		{
			get
			{
				return def;
			}
		}

		/// <seealso cref="java.lang.Object.equals(java.lang.Object)"
		/// Two identifiers are equal, if they have the same names and the same location of definition./>
		public override bool Equals(object obj)
		{
			bool res = false;
			if(obj is Ident)
			{
				Ident id = (Ident)obj;
				res = text.Equals(id.text) && scope.Equals(id.scope);
			}
			return res;
		}

		/// <summary>
		/// Identifier factory.
		/// Use this to get a new Identifier using a string and a location </summary>
		/// <param name="text"> The text of the identifier. </param>
		/// <param name="scope"> The scope/namespace the identifier was defined in. </param>
		/// <param name="loc"> The location of the identifier. </param>
		/// <param name="annots"> The annotations of this identifier. </param>
		/// <returns> The IR identifier object for the desired identifier. </returns>
		public static Ident Get(string text, Symbol.Definition def, Annotations annots)
		{
			Coords loc = def.Coords;
			string key = text + "#" + loc.ToString();
			Ident res;

			if(identifiers.ContainsKey(key))
				res = identifiers[key];
			else
			{
				res = new Ident(text, def.Symbol.GetSymbolTable(), def.Scope, loc, annots);
				identifiers[key] = res;
			}
			return res;
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeInfo() "/>
		public override string NodeInfo
		{
			get
			{
				return base.NodeInfo + "\nCoords: " + def + "\nScope: " + scope.Path;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeLabel() "/>
		public override string NodeLabel
		{
			get
			{
				return Name + " " + text;
			}
		}

		/// <summary>
		/// Compare an identifier to another. </summary>
		/// <param name="obj"> The other identifier. </param>
		/// <returns> -1, 0, 1, respectively. </returns>
		public virtual int CompareTo(Ident id)
		{
			return string.CompareOrdinal(ToString(), id.ToString());
		}

		public override int GetHashCode()
		{
			return precomputedHashCode;
		}

		public virtual Scope Scope
		{
			get
			{
				return scope;
			}
		}

		public virtual SymbolTable SymbolTable
		{
			get
			{
				return symTab;
			}
		}

		/// <returns> The annotations. </returns>
		public virtual Annotations Annotations
		{
			get
			{
				return annots;
			}
		}
	}

}
