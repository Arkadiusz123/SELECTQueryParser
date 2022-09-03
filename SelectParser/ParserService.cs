using Antlr4.Runtime;
using SelectParser.Antlr;
using System.Collections.Generic;

namespace SelectParser
{
    public static class ParserService
    {

        public static List<string> FindColumnsInQuery(string sqlQuery)
        {
            var inputStream = new AntlrInputStream(sqlQuery);
            var lexer = new SqlParserLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new SqlParserParser(commonTokenStream);

            var visitor = new SqlParserVisitor();
            visitor.Visit(parser.stat());

            return visitor.fullColumnsList;
        }
    }
}
