using SelectParser.Antlr;
using System.Collections.Generic;
using System.Linq;

namespace SelectParser
{
    public class SqlParserVisitor:SqlParserBaseVisitor<object>
    {
        public List<string> columnsWithoutRefs = new List<string>();       
        public List<(string, string)> columnsWithRefs = new List<(string, string)>();

        public List<string> fullColumnsList = new List<string>();

        public override object VisitColumn_name(SqlParserParser.Column_nameContext context)
        {
            columnsWithoutRefs.Add(context.GetText().TrimStart());
            return context;
        }

        public override object VisitColumn_with_table_ref(SqlParserParser.Column_with_table_refContext context)
        {
            var refAndColumName = context.GetText().TrimStart().Split(".");
            columnsWithRefs.Add((refAndColumName[0], refAndColumName[1]));
            return context;
        }
   

        public override object VisitTable_element(SqlParserParser.Table_elementContext context)
        {
            
            if (columnsWithoutRefs.Count > 0)
            {
                foreach (var item in columnsWithoutRefs.Distinct())
                {
                    fullColumnsList.Add($"{context.GetText().TrimStart()}.{item}");
                }
            }
           
           if (columnsWithRefs.Count > 0)
            {
                var splittedTableName = context.GetText().TrimStart().Split(" ");
                var tableName = splittedTableName[0].TrimStart();
                var tableAlias = splittedTableName[1];

                var columns = columnsWithRefs.Where(x => x.Item1 == tableAlias).Distinct();

                foreach (var item in columns)
                {
                    fullColumnsList.Add($"{tableName}.{item.Item2}");
                }

            }
           
            return context;
        }
    }
}
