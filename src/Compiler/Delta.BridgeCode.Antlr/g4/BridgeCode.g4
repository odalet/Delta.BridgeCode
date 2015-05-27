//grammar BridgeCode;

///*
// * Parser Rules 
// */

//compileUnit  
//	:	EOF
//	;

///*
// * Lexer Rules
// */

//WS
//	:	' ' -> channel(HIDDEN)
//	;

grammar BridgeCode;

import literals;

// Compilation unit and namespace
compileUnit				:   namespaceDeclaration EOF;
namespaceDeclaration	:	NAMESPACE qualifiedName namespaceBody;
namespaceBody			:	LBRACE typeDeclaration* RBRACE;

// Types
typeDeclaration			:	moduleDeclaration;

// Module
moduleDeclaration		:	MODULE identifier moduleBody;
moduleBody				:	LBRACE moduleBodyDeclaration* RBRACE;
moduleBodyDeclaration	:	SEMI
						|	fieldDeclaration
						;

// Field
fieldDeclaration		:	type variableDeclarator SEMI;

// Variable
variableDeclarator      :   variableDeclaratorId;
variableDeclaratorId	:	IDENTIFIER;

// Types
type					:	primitiveType;
primitiveType			:	BOOL | BYTE | SHORT | INT | LONG | FLOAT | DOUBLE | CHAR | STRING;

// Names
qualifiedName   :   identifier ('.' identifier)*;

identifier	: IDENTIFIER | literal;



// --------------------------------------------------------------------------------------------------------
// LEXER tokens

// Separators
LPAREN          : '(';
RPAREN          : ')';
LBRACE          : '{';
RBRACE          : '}';
LBRACK          : '[';
RBRACK          : ']';
SEMI            : ';';
COMMA           : ',';
DOT             : '.';

// Operators
ASSIGN          : '=';
GT              : '>';
LT              : '<';
BANG            : '!';
TILDE           : '~';
QUESTION        : '?';
COLON           : ':';
EQUAL           : '==';
LE              : '<=';
GE              : '>=';
NOTEQUAL        : '!=';
AND             : '&&';
OR              : '||';
INC             : '++';
DEC             : '--';
ADD             : '+';
SUB             : '-';
MUL             : '*';
DIV             : '/';
BITAND          : '&';
BITOR           : '|';
CARET           : '^';
MOD             : '%';

ADD_ASSIGN      : '+=';
SUB_ASSIGN      : '-=';
MUL_ASSIGN      : '*=';
DIV_ASSIGN      : '/=';
AND_ASSIGN      : '&=';
OR_ASSIGN       : '|=';
XOR_ASSIGN      : '^=';
MOD_ASSIGN      : '%=';
LSHIFT_ASSIGN   : '<<=';
RSHIFT_ASSIGN   : '>>=';
URSHIFT_ASSIGN  : '>>>=';

// Keywords
NAMESPACE		: 'namespace';
MODULE			: 'module';

// Types
BOOL			: 'bool';
BYTE			: 'byte';
SHORT			: 'short';
INT				: 'int';
LONG			: 'long';
FLOAT			: 'float';
DOUBLE			: 'double';
CHAR			: 'char';
STRING			: 'string';

// --------------------------------------------------------------------------------------------------------
// Identifiers (must appear after all keywords in the grammar)
// NB: for now we don't support fancy characters (see commented code)
IDENTIFIER              :   Letter LetterOrDigit*;

fragment Letter         : [a-zA-Z_]
                        //| ~[\u0000-\u00FF\uD800-\uDBFF] { LexerHelper.IsIdentifierStart(_input.La(-1)) }?
                        //| [\uD800-\uDBFF] [\uDC00-\uDFFF] { LexerHelper.IsIdentifierStart(LexerHelper.GetCodePoint(_input.La(-2), _input.La(-1))) }?
                        ;

fragment LetterOrDigit  : [a-zA-Z0-9_]
                        //| ~[\u0000-\u00FF\uD800-\uDBFF] { LexerHelper.IsIdentifierPart(_input.La(-1)) }?
                        //| [\uD800-\uDBFF] [\uDC00-\uDFFF] { LexerHelper.IsIdentifierPart(LexerHelper.GetCodePoint(_input.La(-2), _input.La(-1))) }?
                        ;

AT : '@';

// Whitespace and comments
WS              :   [ \t\r\n\u000C]+ -> skip;
COMMENT         :   '/*' .*? '*/' -> skip;
LINE_COMMENT    :   '//' ~[\r\n]* -> skip;
