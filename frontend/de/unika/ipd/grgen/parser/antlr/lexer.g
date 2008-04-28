header {
/*
 * GrGen: graph rewrite generator, compiling declarative graph rewrite rules into executable code
 * Copyright (C) 2005 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */
 
/**
 * @author Sebastian Hack, Daniel Grund, Rubino Geiss, Adam Szalkowski
 * @version $Id$
 */
	package de.unika.ipd.grgen.parser.antlr;

	import java.io.File;
}

class GRLexer extends Lexer;

options {
	charVocabulary = '\u0000'..'\u00FF';
	testLiterals=false;    // don't automatically test for literals
	k=4;
	codeGenBitsetTestThreshold=20;
	exportVocab = GRBase;
}

tokens {
  ABSTRACT = "abstract";
  ACTIONS = "actions";
  ALTERNATIVE = "alternative";
  ARBITRARY = "arbitrary";
  CLASS = "class";
  COND = "if";
  CONNECT = "connect";
  CONST = "const";
  DELETE = "delete";
  DIRECTED = "directed";
  DPO = "dpo";
  EDGE = "edge";
  EMIT = "emit";
  EMITF = "emitf";
  ENUM = "enum";
  EVAL = "eval";
  EXACT = "exact";
  EXEC = "exec";
  EXTENDS = "extends";
  FALSE = "false";
  HOM = "hom";
  INDEPENDENT = "independent";
  INDUCED = "induced";
  MODEL = "model";
  MODIFY = "modify";
  NEGATIVE = "negative";
  NODE = "node";
  NULL = "null";
  PATTERN = "pattern";
  REPLACE = "replace";
  RETURN = "return";
  RULE = "rule";
  TERM = "term";
  TEST = "test";
  TRUE = "true";
  TYPEOF = "typeof";
  UNDIRECTED = "undirected";
  USING = "using";
  VAR = "var";
  NUM_INTEGER;
  NUM_FLOAT;
  NUM_DOUBLE;
}

{
  GRParserEnvironment env;

  void setEnv(GRParserEnvironment env) {
    this.env = env;
  }

  ANTLRHashString getHashString() {
  	return hashString;
  }

  Hashtable getLiterals() {
  	return literals;
  }
}


QUESTION		:	'?'		;
QUESTIONMINUS	:	"?-"	;
MINUSQUESTION	:	"-?"	;
QMMQ			:	"?--?"	;
LPAREN			:	'('		;
RPAREN			:	')'		;
LBRACK			:	'['		;
RBRACK			:	']'		;
LBRACE			:	'{'		;
RBRACE			:	'}'		;
COLON			:	':'		;
DOUBLECOLON     :   "::"    ;
COMMA			:	','		;
DOT 			:	'.'		;
ASSIGN			:	'='		;
EQUAL			:	"=="	;
NOT         	:	'!'		;
TILDE			:	'~'		;
NOT_EQUAL		:	"!="	;
SL				:	"<<"	;
SR				:	">>"	;
BSR				:	">>>"	;
DIV				:	'/'		;
PLUS			:	'+'		;
MINUS			:	'-'		;
STAR			:	'*'		;
MOD				:	'%'		;
GE				:	">="	;
GT				:	">"		;
LE				:	"<="	;
LT				:	'<'		;
RARROW			:	"->"	;
LARROW			:	"<-"	;
LRARROW			:	"<-->"	;
DOUBLE_LARROW	:	"<--"	;
DOUBLE_RARROW	:	"-->"	;
BXOR			:	'^'		;
BOR				:	'|'		;
LOR				:	"||"	;
BAND			:	'&'		;
LAND			:	"&&"	;
SEMI			:	';'		;
BACKSLASH		:	'\\'	;
PLUSPLUS		:	"++"	;
MINUSMINUS		:	"--"	;
DOLLAR          :   '$'     ;

/*
MINUSMINUS
  : ("-->") => "-->" { $setType(DOUBLE_RARROW); }
  | "--"
  ;
*/

// Whitespace -- ignored
WS	:	(	' '
		|	'\t'
		|	'\f'
			// handle newlines
		|	(	options {generateAmbigWarnings=false;}
			:	"\r\n"  // Evil DOS
			|	'\r'    // Macintosh
			|	'\n'    // Unix (the right way)
			)
			{ newline(); }
		)+
		{ $setType(Token.SKIP); }
	;

SL_COMMENT
  :	"//" (~('\n'|'\r'))* ('\n'|'\r'('\n')?)
        {
			$setType(Token.SKIP);
			newline();
		}
	;

// multiple-line comments
ML_COMMENT
  :	"/*"
		(	/*	'\r' '\n' can be matched in one alternative or by matching
				'\r' in one iteration and '\n' in another.  I am trying to
				handle any flavor of newline that comes in, but the language
				that allows both "\r\n" and "\r" and "\n" to all be valid
				newline is ambiguous.  Consequently, the resulting grammar
				must be ambiguous.  I'm shutting this warning off.
			 */
			options {
				generateAmbigWarnings=false;
			}
		:
			{ LA(2)!='/' }? '*'
		|	'\r' '\n'		{newline();}
		|	'\r'			{newline();}
		|	'\n'			{newline();}
		|	~('*'|'\n'|'\r')
		)*
		"*/"
		{ $setType(Token.SKIP); }
  ;

NUMBER
   : ('0'..'9')+
   ( '.' ('0'..'9')*
     (   ('f'|'F')    { $setType(NUM_FLOAT); }
       | ('d'|'D')?   { $setType(NUM_DOUBLE); }
     )
   | { $setType(NUM_INTEGER); }
   )
   ;

NUM_HEX
	: '0' 'x' ('0'..'9' | 'a' .. 'f' | 'A' .. 'F')+
	;

protected
ESC
	:	'\\'
		(	'n'
		|	'r'
		|	't'
		|	'b'
		|	'f'
		|	'"'
		|	'\''
		|	'\\')
		;

STRING_LITERAL
	:	'"' (ESC|~('"'|'\\'))* '"'
	;

INCLUDE
  : "#include" WS s:STRING_LITERAL {
  	$setType(Token.SKIP);
	String file = s.getText();
	file = file.substring(1,file.length()-1);
	env.pushFile(new File(file));
  }
  ;

IDENT
	options {testLiterals=true;}
	: ('a'..'z'|'A'..'Z') ('a'..'z'|'A'..'Z'|'_'|'0'..'9')*;

