/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll

using System;
using System.Text;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Pretty printing helper class for source code generation
    /// </summary>
    public class SourceBuilder
    {
        readonly StringBuilder builder;
        String indentation = "";

        /// <summary>
        /// If true, the source code should be generated with comments.
        /// </summary>
        public bool CommentSourceCode;

		public SourceBuilder()
		{
			builder = new StringBuilder();
		}

		public SourceBuilder(String initialStr)
		{
			builder = new StringBuilder(initialStr);
		}

        public SourceBuilder(bool commentSourceCode)
        {
            CommentSourceCode = commentSourceCode;
			builder = new StringBuilder();
        }

        public void Reset()
        {
            builder.Length = 0;
            indentation = "";
        }

        public SourceBuilder Append(String str)
        {
            builder.Append(str);
            return this;
        }

        public SourceBuilder AppendFormat(String str, params object[] args)
        {
            builder.AppendFormat(str, args);
            return this;
        }

        public SourceBuilder AppendFront(String str)
        {
            builder.Append(indentation);
            builder.Append(str);
            return this;
        }

        public SourceBuilder AppendFrontIndented(String str)
        {
            Indent();
            builder.Append(indentation);
            builder.Append(str);
            Unindent();
            return this;
        }

        public SourceBuilder AppendFrontFormat(String str, params object[] args)
        {
            builder.Append(indentation);
            builder.AppendFormat(str, args);
            return this;
        }

        public SourceBuilder AppendFrontIndentedFormat(String str, params object[] args)
        {
            Indent();
            builder.Append(indentation);
            builder.AppendFormat(str, args);
            Unindent();
            return this;
        }

        public void Indent()
        {
            indentation += "    ";
        }

        public void Unindent()
        {
            indentation = indentation.Substring(4);
        }

        public override String ToString()
        {
            return builder.ToString();
        }
    }
}
