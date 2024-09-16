using System.Text.RegularExpressions;

namespace MeowLangCompiler.Scanner
{
    // A class that represents the actual lexer and it's related operations.
    public class Lexer
    {
        private string _input;
        private int _position = 0;
        private int _line = 1;
        private List<Token> _tokens = new();

        public Lexer(string input)
        {
            _input = input;
        }

        // Scan the input and return a sequence of tokens.
        public List<Token> Scan()
        {
            //Remove all the comments before processing the input.
            RemoveComments();

            while (_position < _input.Length)
            {
                IgnoreSpaces();

                // Check if the string is already fully scanned after removing whitespaces.
                if (_position == _input.Length) break;

                Token? token = GetToken();

                // If no token found throw lexical error.
                if (token == null)
                {
                    throw new Exception($"Invalid token at line: {_line}");
                }

                // If a token was found then add it to the list.
                _tokens.Add(token!);
            }

            return _tokens;
        }

        // Ignores spaces at the start of the input string.
        private void IgnoreSpaces()
        {
            while (_position < _input.Length && char.IsWhiteSpace(_input[_position]))
            {
                // Check if the character is a new line then increment the _line variable.
                if (_input[_position] == '\n') _line++;

                _position++;
            }
        }

        // Removes all comments from the source code.
        // It handles both single line comments and multiple line comments.
        private void RemoveComments()
        {
            Regex commentRegex = new(@"(//[^\n]*\n)|(/\*[\s\S]*\*/)");

            /* Replace all the matched strings (comments) with one or more new line breaks
             * depending on the type of the comment to maintain the number of lines
             * of the original source code and then the IgnoreSpaces method will ignore these new break lines. */
            _input = commentRegex.Replace(_input, match =>
            {
                if (match.Value.StartsWith("//"))
                {
                    return "\n";
                }
                else if (match.Value.StartsWith("/*"))
                {
                    // Count the number of lines of the multiple lines comment
                    int numberOflines = match.Value.Count(c => c == '\n');

                    return new string('\n', numberOflines);
                }

                // Will not reach this line;
                return match.Value;
            });
        }

        // Returns the first token that was found in the remaining input string.
        // If no token was found it returns null.
        private Token? GetToken()
        {
            string restOfTheInput = _input.Substring(_position);

            if (TryMatchWith(RegularExpressions.DataType, restOfTheInput, out string? dataTypeResult))
            {
                return new Token() { Category = TokenCategory.DataType, Value = dataTypeResult, Line = _line };
            }

            if (TryMatchWith(RegularExpressions.ControlFlowKeywordIf, restOfTheInput, out string? ControlFlowIfResult))
            {
                return new Token() { Category = TokenCategory.ControlFlowKeywordIf, Value = ControlFlowIfResult, Line = _line };
            }

            if (TryMatchWith(RegularExpressions.ControlFlowKeywordElse, restOfTheInput, out string? ControlFlowElseResult))
            {
                return new Token() { Category = TokenCategory.ControlFlowKeywordElse, Value = ControlFlowElseResult, Line = _line };
            }

            if (TryMatchWith(RegularExpressions.BooleanValue, restOfTheInput, out string? BooleanValueResult))
            {
                return new Token() { Category = TokenCategory.BooleanValue, Value = BooleanValueResult, Line = _line };
            }

            if (TryMatchWith(RegularExpressions.ReturnKeyword, restOfTheInput, out string? ReturnKeywordResult))
            {
                return new Token() { Category = TokenCategory.ReturnKeyword, Value = ReturnKeywordResult, Line = _line };
            }

            if (TryMatchWith(RegularExpressions.Identifier, restOfTheInput, out string? IdentifierResult))
            {
                return new Token() { Category = TokenCategory.Identifier, Value = IdentifierResult, Line = _line };
            }

            if (TryMatchWith(RegularExpressions.ComparisonOperator, restOfTheInput, out string? ComparisonOperatorResult))
            {
                return new Token() { Category = TokenCategory.ComparisonOperator, Value = ComparisonOperatorResult, Line = _line };
            }

            if (TryMatchWith(RegularExpressions.ArithmaticOperator, restOfTheInput, out string? ArithmaticOperatorResult))
            {
                return new Token() { Category = TokenCategory.ArithmaticOperator, Value = ArithmaticOperatorResult, Line = _line };
            }

            if (TryMatchWith(RegularExpressions.AssignmentOperator, restOfTheInput, out string? AssignmentOperatorResult))
            {
                return new Token() { Category = TokenCategory.AssignmentOperator, Value = AssignmentOperatorResult, Line = _line };
            }

            if (TryMatchWith(RegularExpressions.StringLiteral, restOfTheInput, out string? StringLiteralResult))
            {
                return new Token() { Category = TokenCategory.StringLiteral, Value = StringLiteralResult, Line = _line };
            }

            if (TryMatchWith(RegularExpressions.FloatLiteral, restOfTheInput, out string? FloatLiteralResult))
            {
                return new Token() { Category = TokenCategory.FloatLiteral, Value = FloatLiteralResult, Line = _line };
            }

            if (TryMatchWith(RegularExpressions.IntLiteral, restOfTheInput, out string? IntLiteralResult))
            {
                return new Token() { Category = TokenCategory.IntLiteral, Value = IntLiteralResult, Line = _line };
            }

            if (TryMatchWith(RegularExpressions.PunctuationSemiColon, restOfTheInput, out string? PunctuationSemiColonResult))
            {
                return new Token() { Category = TokenCategory.PunctuationSemiColon, Value = PunctuationSemiColonResult, Line = _line };
            }

            if (TryMatchWith(RegularExpressions.PunctuationOpenParentheses, restOfTheInput, out string? PunctuationOpenParenthesesResult))
            {
                return new Token() { Category = TokenCategory.PunctuationOpenParentheses, Value = PunctuationOpenParenthesesResult, Line = _line };
            }

            if (TryMatchWith(RegularExpressions.PunctuationCloseParentheses, restOfTheInput, out string? PunctuationCloseParenthesesResult))
            {
                return new Token() { Category = TokenCategory.PunctuationCloseParentheses, Value = PunctuationCloseParenthesesResult, Line = _line };
            }

            if (TryMatchWith(RegularExpressions.PunctuationOpenCurly, restOfTheInput, out string? PunctuationOpenCurlyResult))
            {
                return new Token() { Category = TokenCategory.PunctuationOpenCurly, Value = PunctuationOpenCurlyResult, Line = _line };
            }

            if (TryMatchWith(RegularExpressions.PunctuationCloseCurly, restOfTheInput, out string? PunctuationCloseCurlyResult))
            {
                return new Token() { Category = TokenCategory.PunctuationCloseCurly, Value = PunctuationCloseCurlyResult, Line = _line };
            }

            return null;
        }

        // Tries matching the regex with the giving string, if it does match then it return true and the matched string in the result parameter.
        // If it doesn't match it returns false and the result parameter will be null.
        private bool TryMatchWith(Regex regex, string str, out string? result)
        {
            Match regexResult = regex.Match(str);

            // Check if there was a match and that this match starts from the beginning of the string.
            // If the match doesn't start from the beginning of the string it means that there are invalid characters before that match.
            if (regexResult.Success && regexResult.Index == 0)
            {
                // Set the new position after matching a token       
                _position = _position + regexResult.Length;

                result = regexResult.Value;
                return true;
            }

            result = null;
            return false;
        }
    }
}
