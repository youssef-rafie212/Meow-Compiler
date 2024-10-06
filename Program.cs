using Antlr4.Runtime;
using MeowLangCompiler.Parser;
using MeowLangCompiler.Scanner;

namespace MeowLangCompiler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Input
            string source = File.ReadAllText("C:\\Users\\joe69\\source\\repos\\MeowLangCompiler\\MeowLangCompiler\\main.meow");

            try
            {
                // Stage 1 : Lexer 
                Scanner.Lexer lexer = new(source);
                List<Token> tokens = lexer.Scan();
                
                // 1st stage Output
                foreach(Token t in tokens){
                    Console.WriteLine(t);
                }

                // Stage 2 : Parser
                CustomTokenSource tokenSource = new(tokens);
                CommonTokenStream tokenStream = new(tokenSource);
                MeowParser parser = new(tokenStream);
                MeowParser.ProgramContext programContext = parser.program();

                // 2nd stage Output
                Console.WriteLine(programContext.ToStringTree());
            }
            catch {
                // just to handle missing ";"  null exception
            }
        }
    }
}
