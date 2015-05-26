// Grabbed quite a few definitions from:
// - https://github.com/antlr/grammars-v4/blob/master/java/Java.g4
// - https://github.com/ChristianWulf/CSharpGrammar/tree/master/grammars
grammar BridgeCodeV001;

compileUnit         :   moduleDeclaration EOF;
moduleDeclaration   :   annotation* 'module' qualifiedName moduleBody;
moduleBody          :   '{' typeDeclaration* '}';   
typeDeclaration     :   annotation* classModifier* classDeclaration;

// Modifiers
fieldModifier       :   'public';
classModifier       :   'public' | 'const';

// class
classDeclaration        :   'class' Identifier classBody;
classBody               :   '{' classBodyDeclaration* '}';
classBodyDeclaration    :   ';'
                        |   fieldModifier* fieldDeclaration
                        ;

// field
fieldDeclaration        :   type variableDeclarators ';';

variableDeclarators     :   variableDeclarator (',' variableDeclarator)*;
variableDeclarator      :   variableDeclaratorId ('=' variableInitializer)?;
variableDeclaratorId    :   Identifier; // TODO: C# style is type[] variable, not type variable[]
variableInitializer     :   arrayInitializer    |   expression;

arrayInitializer        :   '{' (variableInitializer (',' variableInitializer)* (',')? )? '}';

// Types
type            :   classType ('[' ']')* | primitiveType ('[' ']')*;
classType       :   qualifiedName;
primitiveType   :   INT | STRING;

// Annotations
annotation      :   '@' annotationName '(' ')'; //( '(' ( elementValuePairs | elementValue )? ')' )?
annotationName  : qualifiedName;

// --------------------------------------------------------------------------------------------------------
// Expression

expressionList  :   expression (',' expression)*;

expression  :   primary
            |   expression '.' Identifier
            |   expression '[' expression ']'
            |   expression '(' expressionList? ')'
            |   '(' type ')' expression
            |   expression ('++' | '--')
            |   ('+'|'-'|'++'|'--') expression
            |   ('~'|'!') expression
            |   expression ('*'|'/'|'%') expression
            |   expression ('+'|'-') expression                
            |   expression ('<' '<' | '>' '>' '>' | '>' '>') expression
            |   expression ('<=' | '>=' | '>' | '<') expression
            |   expression ('==' | '!=') expression
            |   expression '&' expression
            |   expression '^' expression
            |   expression '|' expression
            |   expression '&&' expression
            |   expression '||' expression
            |   expression '?' expression ':' expression
            |/*<assoc=right>*/ expression   
            (   '='
            |   '+='
            |   '-='
            |   '*='
            |   '/='
            |   '&='
            |   '|='
            |   '^='
            |   '>>='
            |   '>>>='
            |   '<<='
            |   '%=') expression
            ;

primary     :   '(' expression ')'
            |   literal
            |   Identifier
            ;

// --------------------------------------------------------------------------------------------------------
// Keywords
MODULE      : 'module';
CLASS       : 'class';
CONST       : 'const';
PUBLIC      : 'public';

// Types
INT         : 'int';
STRING      : 'string';
              

// Names
qualifiedName   :   Identifier ('.' Identifier)*;

// --------------------------------------------------------------------------------------------------------
// Literals

literal :   IntegerLiteral
        |   FloatingPointLiteral
        |   CharacterLiteral
        |   StringLiteral
        |   BooleanLiteral
        |   NullLiteral
        ;

NullLiteral     :   'null';
BooleanLiteral  :   'true' | 'false';

// Integer Literals
IntegerLiteral  :   DecimalIntegerLiteral 
                |   HexIntegerLiteral 
                |   OctalIntegerLiteral
                |   BinaryIntegerLiteral
                ;

fragment DecimalIntegerLiteral  :   DecimalNumeral IntegerTypeSuffix?;
fragment HexIntegerLiteral      :   HexNumeral IntegerTypeSuffix?;
fragment OctalIntegerLiteral    :   OctalNumeral IntegerTypeSuffix?;
fragment BinaryIntegerLiteral   :   BinaryNumeral IntegerTypeSuffix?;

fragment IntegerTypeSuffix  :   [lL]; // no unsigned

fragment DecimalNumeral :   '0' | NonZeroDigit (Digits? | Underscores Digits);
fragment HexNumeral     :   '0' [xX] HexDigits;
fragment OctalNumeral   :   '0' Underscores? OctalDigits;
fragment BinaryNumeral  :   '0' [bB] BinaryDigits;

fragment Digits             :   Digit (DigitOrUnderscore* Digit)?;
fragment Digit              :   '0' | NonZeroDigit;
fragment NonZeroDigit       :   [1-9];
fragment DigitOrUnderscore  :   Digit |   '_';
fragment Underscores        :   '_'+;

fragment HexDigits              :   HexDigit (HexDigitOrUnderscore* HexDigit)?;
fragment HexDigit               :   [0-9a-fA-F];
fragment HexDigitOrUnderscore   :   HexDigit | '_';

fragment OctalDigits            :   OctalDigit (OctalDigitOrUnderscore* OctalDigit)?;
fragment OctalDigit             :   [0-7];
fragment OctalDigitOrUnderscore :   OctalDigit | '_';

fragment BinaryDigits               :   BinaryDigit (BinaryDigitOrUnderscore* BinaryDigit)?;
fragment BinaryDigit                :   [01];
fragment BinaryDigitOrUnderscore    :   BinaryDigit | '_';

// Floating-Point Literals (we don't support hexadecimal floats for now...
FloatingPointLiteral    :   DecimalFloatingPointLiteral;

fragment DecimalFloatingPointLiteral        :   Digits '.' Digits? ExponentPart? FloatTypeSuffix?
                                            |   '.' Digits ExponentPart? FloatTypeSuffix?
                                            |   Digits ExponentPart FloatTypeSuffix?
                                            |   Digits FloatTypeSuffix;

fragment ExponentPart       :   ExponentIndicator SignedInteger;
fragment ExponentIndicator  :   [eE];
fragment SignedInteger      :   Sign? Digits;
fragment Sign               :   [+-];
fragment FloatTypeSuffix    :   [fFdD];

// String and character literals
CharacterLiteral    :   '\'' SingleCharacter '\'' |   '\'' EscapeSequence '\'';
StringLiteral       :   '"' StringCharacters? '"'; // TODO: C#'s @"" 

fragment StringCharacters   :   StringCharacter+;
fragment StringCharacter    :   ~["\\] | EscapeSequence;
fragment SingleCharacter    :   ~['\\];
fragment EscapeSequence :   '\\' [btnfr"'\\] | OctalEscape | UnicodeEscape;
fragment OctalEscape    :   '\\' OctalDigit | '\\' OctalDigit OctalDigit | '\\' ZeroToThree OctalDigit OctalDigit;
fragment ZeroToThree    :   [0-3];
fragment UnicodeEscape  :   '\\' 'u' HexDigit HexDigit HexDigit HexDigit;

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

// --------------------------------------------------------------------------------------------------------
// Identifiers (must appear after all keywords in the grammar)
Identifier              :   Letter LetterOrDigit*;

fragment Letter         : [a-zA-Z_]
                        | ~[\u0000-\u00FF\uD800-\uDBFF] { LexerHelper.IsIdentifierStart(_input.La(-1)) }?
                        | [\uD800-\uDBFF] [\uDC00-\uDFFF] { LexerHelper.IsIdentifierStart(LexerHelper.GetCodePoint(_input.La(-2), _input.La(-1))) }?
                        ;

fragment LetterOrDigit  : [a-zA-Z0-9_]
                        | ~[\u0000-\u00FF\uD800-\uDBFF] { LexerHelper.IsIdentifierPart(_input.La(-1)) }?
                        | [\uD800-\uDBFF] [\uDC00-\uDFFF] { LexerHelper.IsIdentifierPart(LexerHelper.GetCodePoint(_input.La(-2), _input.La(-1))) }?
                        ;

AT : '@';

// Whitespace and comments
WS              :   [ \t\r\n\u000C]+ -> skip;
COMMENT         :   '/*' .*? '*/' -> skip;
LINE_COMMENT    :   '//' ~[\r\n]* -> skip;
