grammar Meow;

// Tokens
DATA_TYPE     : 'int' | 'float' | 'string' | 'boolean';
CONTROLFLOW_KEYWORD_IF : 'if'; 
CONTROLFLOW_KEYWORD_ELSE : 'else';
BOOLEAN_VALUE : 'true' | 'false';
RETURN_KEYWORD : 'return';
IDENTIFIER  : [a-zA-Z_][a-zA-Z_0-9]*;
ARITHMATIC_OPERATOR    : '+' | '-' | '*' | '/';
COMPARISON_OPERATOR : '==' | '!=' | '<=' | '>=' | '<' | '>';
ASIGNMENT_OPERATOR  : '=';
STRING_LITERAL : '"' (~["])* '"';
FLOAT_LITERAL  : [0-9]+ '.' [0-9]+;
INT_LITERAL    : [0-9]+;
PUNCTUATION_SEMICOLON    : ';';
PUNCTUATION_OPEN_PARENTHESES    : '(';
PUNCTUATION_CLOSE_PARENTHESES    : ')';
PUNCTUATION_OPEN_CURLY    : '{';
PUNCTUATION_CLOSE_CURLY    : '}';

// Grammar
program     : statement*;

statement   : variableDeclaration
            | ifStatement
            | returnStatement
            ;

variableDeclaration
    : DATA_TYPE IDENTIFIER ASIGNMENT_OPERATOR expression ';'
    ;

ifStatement
    : 'if' '(' ifExpression ')' block ('else' block)?
    ;

block
    : '{' statement* '}'
    ;

returnStatement
    :  RETURN_KEYWORD expression? ';'
    ;

ifExpression
    : expression COMPARISON_OPERATOR expression
    ;

expression
    : literal
    | IDENTIFIER
    | expression ARITHMATIC_OPERATOR expression
    | expression COMPARISON_OPERATOR expression
    ; 

literal
    : INT_LITERAL
    | FLOAT_LITERAL
    | STRING_LITERAL
    | BOOLEAN_VALUE
    ;
