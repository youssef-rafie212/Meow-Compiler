using System.Text.RegularExpressions;

namespace MeowLangCompiler.Scanner
{
    // A static class that contains all the regex needed to match tokens with their categories. 
    public static class RegularExpressions
    {
        public static readonly Regex DataType = new(@"\b(int|float|string|boolean)\b");
        public static readonly Regex ControlFlowKeywordIf = new(@"\b(if)\b");
        public static readonly Regex ControlFlowKeywordElse = new(@"\b(else)\b");
        public static readonly Regex BooleanValue = new(@"\b(true|false)\b");
        public static readonly Regex Identifier = new(@"\b[a-zA-Z_]\w*\b");
        public static readonly Regex ReturnKeyword = new(@"\b(return)\b");
        public static readonly Regex ArithmaticOperator = new(@"(\+|-|\*|/)");
        public static readonly Regex ComparisonOperator = new(@"(==|!=|<=|>=|<|>)");
        public static readonly Regex AssignmentOperator = new(@"=");
        public static readonly Regex StringLiteral = new(@"""[^""]*""");
        public static readonly Regex FloatLiteral = new(@"\b\d+\.\d+\b");
        public static readonly Regex IntLiteral = new(@"\b\d+\b");
        public static readonly Regex PunctuationSemiColon = new(@";");
        public static readonly Regex PunctuationOpenParentheses = new(@"\(");
        public static readonly Regex PunctuationCloseParentheses = new(@"\)");
        public static readonly Regex PunctuationOpenCurly = new(@"{");
        public static readonly Regex PunctuationCloseCurly = new(@"}");
    }
}
