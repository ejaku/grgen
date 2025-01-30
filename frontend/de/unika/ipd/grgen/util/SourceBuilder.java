/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * The SourceBuilder acts like a StringBuilder with support for indentation added.
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.util;

public class SourceBuilder
{
	public SourceBuilder()
	{
		stringBuilder = new StringBuilder(16);
		indentationLevel = 0;
	}

	public SourceBuilder indent()
	{
		++indentationLevel;
		return this;
	}

	public SourceBuilder unindent()
	{
		--indentationLevel;
		return this;
	}

	public SourceBuilder append(String str)
	{
		stringBuilder.append(str);
		return this;
	}

	public SourceBuilder appendFront(String str)
	{
		for(int i = 0; i < indentationLevel; ++i) {
			stringBuilder.append("\t");
		}
		stringBuilder.append(str);
		return this;
	}

	public SourceBuilder appendFrontIndented(String str)
	{
		for(int i = 0; i < indentationLevel + 1; ++i) {
			stringBuilder.append("\t");
		}
		stringBuilder.append(str);
		return this;
	}

	public SourceBuilder append(boolean b)
	{
		stringBuilder.append(b);
		return this;
	}

	public String getIndent()
	{
		StringBuilder sb = new StringBuilder();
		for(int i = 0; i < indentationLevel; ++i) {
			sb.append("\t");
		}
		return sb.toString();
	}

	public StringBuilder getStringBuilder()
	{
		return stringBuilder;
	}

	public int length()
	{
		return stringBuilder.length();
	}

	public void delete(int start, int end)
	{
		stringBuilder.delete(start, end);
	}

	@Override
	public String toString()
	{
		return stringBuilder.toString();
	}

	public int getIndentationLevel()
	{
		return indentationLevel;
	}

	public void setIndentationLevel(int indentationLevel)
	{
		this.indentationLevel = indentationLevel;
	}

	private StringBuilder stringBuilder;
	private int indentationLevel;
}
