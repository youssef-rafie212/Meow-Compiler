namespace MeowLangCompiler.Scanner
{
    // A class representing the token that will be a result of scanning the input using the lexer (scanner).
    public class Token
    {
        public TokenCategory Category { get; set; }
        public string? Value { get; set; }
        public int Line { get; set; }
        public override string ToString()
        {
            return $"{Value} => {Category}, Line {Line}.";
        }
    }
}
