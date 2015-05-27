// Sub-grammar for BridgeCode
grammar literals;

// --------------------------------------------------------------------------------------------------------
// Literals

literal			:	integerLiteral
				|	floatingPointLiteral
				;

// Integer Literals
integerLiteral  :   IntegerLiteral;
IntegerLiteral	:	DecimalIntegerLiteral 
                |   HexIntegerLiteral 
                |   OctalIntegerLiteral
                |   BinaryIntegerLiteral
                ;

fragment DecimalIntegerLiteral  :   DecimalNumeral IntegerTypeSuffix?;
fragment HexIntegerLiteral      :   HexNumeral IntegerTypeSuffix?;
fragment OctalIntegerLiteral    :   OctalNumeral IntegerTypeSuffix?;
fragment BinaryIntegerLiteral   :   BinaryNumeral IntegerTypeSuffix?;

fragment IntegerTypeSuffix		:   [lL]; // no unsigned

fragment DecimalNumeral			:   '0' | NonZeroDigit (Digits? | Underscores Digits);
fragment HexNumeral				:   '0' [xX] HexDigits;
fragment OctalNumeral			:   '0' Underscores? OctalDigits;
fragment BinaryNumeral			:   '0' [bB] BinaryDigits;

fragment Underscores				:   '_'+;

fragment Digits						:   Digit (DigitOrUnderscore* Digit)?;
fragment Digit						:   '0' | NonZeroDigit;
fragment NonZeroDigit				:   [1-9];
fragment DigitOrUnderscore			:   Digit |   '_';

fragment HexDigits					:   HexDigit (HexDigitOrUnderscore* HexDigit)?;
fragment HexDigit					:   [0-9a-fA-F];
fragment HexDigitOrUnderscore		:   HexDigit | '_';

fragment OctalDigits				:   OctalDigit (OctalDigitOrUnderscore* OctalDigit)?;
fragment OctalDigit					:   [0-7];
fragment OctalDigitOrUnderscore		:   OctalDigit | '_';

fragment BinaryDigits               :   BinaryDigit (BinaryDigitOrUnderscore* BinaryDigit)?;
fragment BinaryDigit                :   [01];
fragment BinaryDigitOrUnderscore    :   BinaryDigit | '_';

// Floating-Point Literals (we don't support hexadecimal floats for now...)
floatingPointLiteral	: FloatingPointLiteral;
FloatingPointLiteral	: DecimalFloatingPointLiteral;

fragment DecimalFloatingPointLiteral        :   Digits DOT Digits? ExponentPart? FloatTypeSuffix?
                                            |   DOT Digits ExponentPart? FloatTypeSuffix?
                                            |   Digits ExponentPart FloatTypeSuffix?
                                            |   Digits FloatTypeSuffix;

fragment ExponentPart						:   ExponentIndicator SignedInteger;
fragment ExponentIndicator					:   [eE];
fragment SignedInteger						:   Sign? Digits;
fragment Sign								:   [+-];
fragment FloatTypeSuffix					:   [fFdD];

// --------------------------------------------------------------------------------------------------------
// LEXER tokens

DOT             : '.';