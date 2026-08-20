/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// XMLDumper.java
/// 
/// @author Created by Omnicore CodeGuide
/// </summary>

namespace de.unika.ipd.grgen.util
{

	using System.Collections.Generic;
	using System.Diagnostics;

	public class XMLDumper
	{
		private readonly PrintStream ps;

		private int indentationLevel = 0;

		private readonly string indentString;

		private readonly ICollection<XMLDumpable> visited = new HashSet<XMLDumpable>();

		public XMLDumper(PrintStream ps)
			: this(ps, "  ")
		{
		}

		public XMLDumper(PrintStream ps, string indentString)
		{
			this.ps = ps;
			this.indentString = indentString;
		}

		public virtual void Dump(XMLDumpable dumpable)
		{
			if(visited.Contains(dumpable))
			{
				DumpRef(dumpable);
				return;
			}

			visited.Add(dumpable);

			IDictionary<string, object> fields = new Dictionary<string, object>();
			dumpable.AddFields(fields);
			string tagName = dumpable.TagName;

			Indent();
			ps.Print('<');
			ps.Print(tagName);
			ps.Print(" id=\"");
			ps.Print(dumpable.XMLId);
			ps.Print('\"');

			IList<string> keysToRemove = new List<string>();

			foreach(string obj in fields.Keys)
			{
				object val = fields[obj];
	// JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in C#:
	// ORIGINAL LINE: if(!(val instanceof java.util.Iterator<?>))
				if(!(val is IEnumerator<object>))
				{
					ps.Print(' ');
					ps.Print(obj);
					ps.Print("=\"");
					ps.Print(val);
					ps.Print('\"');
					keysToRemove.Add(obj);
				}
			}

			foreach(string keyToRemove in keysToRemove)
				fields.Remove(keyToRemove);

			if(fields.Count > 0)
			{
				ps.Println('>');
				indentationLevel++;
				foreach(object obj in fields.Keys)
				{
					// the cast was checked some lines above
	// JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in C#:
	// ORIGINAL LINE: java.util.Iterator<?> childs = (java.util.Iterator<?>)fields.get(obj);
					IEnumerator<object> childs = (IEnumerator<object>)fields[obj];
					string tag = obj.ToString();

					bool hasNext = childs.MoveNext();
					if(hasNext)
					{
						Indent();
						ps.Print('<');
						ps.Print(tag);
						ps.Println('>');
						indentationLevel++;

						do
						{
							object d = childs.Current;

							Debug.Assert(d is XMLDumpable);
							Dump((XMLDumpable)d);
						}
						while(childs.MoveNext());

						indentationLevel--;
						Indent();
						ps.Print("</");
						ps.Print(tag);
						ps.Println('>');
					}
				}
				indentationLevel--;
				Indent();
				ps.Print("</");
				ps.Print(tagName);
				ps.Println('>');
			}
			else
				ps.Println("/>");
		}

		private void DumpRef(XMLDumpable dumpable)
		{
			Indent();
			ps.Print('<');
			ps.Print(dumpable.RefTagName);
			ps.Print(" id=\"");
			ps.Print(dumpable.XMLId);
			ps.Println("\"/>");
		}

		private void Indent()
		{
			for(int i = 0; i < indentationLevel; i++)
				ps.Print(indentString);
		}
	}

}
