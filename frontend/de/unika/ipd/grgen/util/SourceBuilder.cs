/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// The SourceBuilder acts like a StringBuilder with support for indentation added.
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.util
{
	public class SourceBuilder
	{
		public SourceBuilder()
		{
			stringBuilder = new System.Text.StringBuilder(16);
			indentationLevel = 0;
		}

		public virtual SourceBuilder Indent()
		{
			++indentationLevel;
			return this;
		}

		public virtual SourceBuilder Unindent()
		{
			--indentationLevel;
			return this;
		}

		public virtual SourceBuilder Append(string str)
		{
			stringBuilder.Append(str);
			return this;
		}

		public virtual SourceBuilder AppendFront(string str)
		{
			for(int i = 0; i < indentationLevel; ++i)
				stringBuilder.Append("\t");
			stringBuilder.Append(str);
			return this;
		}

		public virtual SourceBuilder AppendFrontIndented(string str)
		{
			for(int i = 0; i < indentationLevel + 1; ++i)
				stringBuilder.Append("\t");
			stringBuilder.Append(str);
			return this;
		}

		public virtual SourceBuilder Append(bool b)
		{
			stringBuilder.Append(b);
			return this;
		}

		public virtual string Indentation
		{
			get
			{
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				for(int i = 0; i < indentationLevel; ++i)
					sb.Append("\t");
				return sb.ToString();
			}
		}

		public virtual System.Text.StringBuilder StringBuilder
		{
			get
			{
				return stringBuilder;
			}
		}

		public virtual int Length()
		{
			return stringBuilder.Length;
		}

		public virtual void Delete(int start, int end)
		{
			stringBuilder.Remove(start, end - start);
		}

		public override string ToString()
		{
			return stringBuilder.ToString();
		}

		public virtual int IndentationLevel
		{
			get
			{
				return indentationLevel;
			}
			set
			{
				this.indentationLevel = value;
			}
		}


		private System.Text.StringBuilder stringBuilder;
		private int indentationLevel;
	}

}
