/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Text;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Pretty printing helper class for source code generation
    /// </summary>
    public class SourceBuilder
    {
        StringBuilder builder;
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

        public SourceBuilder AppendFrontFormat(String str, params object[] args)
        {
            builder.Append(indentation);
            builder.AppendFormat(str, args);
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
