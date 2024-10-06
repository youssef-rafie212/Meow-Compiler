using Antlr4.Runtime;
using MeowLangCompiler.Scanner;

namespace MeowLangCompiler.Parser
{
    public class CustomTokenSource : ITokenSource
    {
        // List of tokens from the manual lexer
        private readonly List<Token> _tokens;
        private int _currentTokenIndex;

        public CustomTokenSource(List<Token> tokens)
        {
            _tokens = tokens;
            _currentTokenIndex = 0;
        }

        public IToken NextToken()
        {
            if (_currentTokenIndex >= _tokens.Count)
            {
                // Return EOF if the input is fully scanned.
                return new CommonToken(TokenConstants.EOF);
            }

            // Get the current token
            Token manualToken = _tokens[_currentTokenIndex];
            _currentTokenIndex++;

            // Map manual token categories to ANTLR token types
            int antlrTokenType = GetAntlrTokenType(manualToken.Category, manualToken.Value!);

            // Create and return an ANTLR token
            var antlrToken = new CommonToken(antlrTokenType, manualToken.Value)
            {
                Line = manualToken.Line,
                StartIndex = 0,
                StopIndex = manualToken.Value!.Length - 1
            };

            return antlrToken;
        }

        // Map manual lexer's token categories to ANTLR token types
        private int GetAntlrTokenType(TokenCategory category, string value)
        {
            switch (category)
            {
                case TokenCategory.DataType: return 1;
                case TokenCategory.ControlFlowKeywordIf: return 2;
                case TokenCategory.ControlFlowKeywordElse: return 3;
                case TokenCategory.BooleanValue: return 4;
                case TokenCategory.ReturnKeyword: return 5;
                case TokenCategory.Identifier: return 6;
                case TokenCategory.ArithmaticOperator: return 7;
                case TokenCategory.ComparisonOperator: return 8;
                case TokenCategory.AssignmentOperator: return 9;
                case TokenCategory.StringLiteral: return 10;
                case TokenCategory.FloatLiteral: return 11;
                case TokenCategory.IntLiteral: return 12;
                case TokenCategory.PunctuationSemiColon: return 13;
                case TokenCategory.PunctuationOpenParentheses: return 14;
                case TokenCategory.PunctuationCloseParentheses: return 15;
                case TokenCategory.PunctuationOpenCurly: return 16;
                case TokenCategory.PunctuationCloseCurly: return 17;

                // Will not reach this case as the lexer will throw an error if it encounters an invalid token.
                default: return TokenConstants.InvalidType;
            }
        }

        // Required properties for ITokenSource
        public int Line => _currentTokenIndex < _tokens.Count ? _tokens[_currentTokenIndex].Line : 0;
        public ICharStream InputStream => null;
        public string SourceName => "Lexer";
        public int CharPositionInLine => 0;
        public ITokenFactory TokenFactory { get; set; } = CommonTokenFactory.Default;
        public int Column => 0; // TODO: Get the column value.
    }
}
