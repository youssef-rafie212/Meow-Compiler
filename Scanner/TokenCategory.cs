namespace MeowLangCompiler.Scanner
{
    // Available token types. 
    public enum TokenCategory
    {
        DataType,
        ControlFlowKeywordIf,
        ControlFlowKeywordElse,
        BooleanValue,
        ReturnKeyword,
        Identifier,
        ArithmaticOperator,
        ComparisonOperator,
        AssignmentOperator,
        StringLiteral,
        FloatLiteral,
        IntLiteral,
        PunctuationSemiColon,
        PunctuationOpenParentheses,
        PunctuationCloseParentheses,
        PunctuationOpenCurly,
        PunctuationCloseCurly,
    }
}
