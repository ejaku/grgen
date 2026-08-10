/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr;

public enum OperatorCode
{
	COND,
	LOG_OR,
	LOG_AND,
	BIT_OR,
	BIT_XOR,
	BIT_AND,
	EQ,
	NE,
	LT,
	LE,
	GT,
	GE,
	SHL,
	SHR,
	BIT_SHR,
	ADD,
	SUB,
	MUL,
	DIV,
	MOD,
	LOG_NOT,
	BIT_NOT,
	NEG,
	IN,
	EXCEPT,
	SE
}
