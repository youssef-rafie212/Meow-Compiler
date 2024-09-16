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
            string source =
            @" // Defining some variables  
               int x = 2;
               int y = 20;
               string txt = ""World, hello!"";
                
               /*
                If condition 
                to check if x is greater than y
               */
               if (x > y)
               {
                    return true;
               } 
               else 
               {
                    return false;
               }
            ";

            try
            {
                // Stage 1 : Lexer 
                Scanner.Lexer lexer = new(source);
                List<Token> tokens = lexer.Scan();

                // Stage 2 : Parser
                CustomTokenSource tokenSource = new(tokens);
                CommonTokenStream tokenStream = new(tokenSource);
                MeowParser parser = new(tokenStream);
                MeowParser.ProgramContext programContext = parser.program();

                // Output
                Console.WriteLine(programContext.ToStringTree());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
