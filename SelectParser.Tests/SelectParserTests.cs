using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SelectParser.Tests
{
    [TestClass]
    public class SelectParserTests
    {

        [TestMethod]
        [DataRow("SELECT CustomerName, City FROM Customers;",
            "Customers.CustomerName", "Customers.City")]
        [DataRow("SELECT c.CustomerName, c.City, s.PurchaseID, s.PurchaseDate FROM Customers c JOIN Sales s ON s.CustomerID = c.ID;",
            "Customers.CustomerName", "Customers.City", "Sales.PurchaseID", "Sales.PurchaseDate")]
        [DataRow("SELECT pod.PurchaseOrderID, pod.ReceivedQty, pod.RejectedQty, rp.RejectedQty / pod.ReceivedQty AS RejectRatio, pod.DueDate " +
            "FROM Purchasing.PurchaseOrderDetail pod JOIN SalesRejectedProducts rp ON pod.RejectedId = rp.Id WHERE RejectedQty / ReceivedQty > 0 " +
            "AND DueDate > CONVERT(DATETIME, ‘20010630’, 101);",
            "Purchasing.PurchaseOrderDetail.PurchaseOrderID", "Purchasing.PurchaseOrderDetail.ReceivedQty", "Purchasing.PurchaseOrderDetail.RejectedQty",
            "SalesRejectedProducts.RejectedQty", "Purchasing.PurchaseOrderDetail.DueDate")]
        public void FindColumnsInQueryTest(string sqlQuery, params string[] expectedColumns)
        {
            var columnsResult = ParserService.FindColumnsInQuery(sqlQuery).ToArray();

            columnsResult.Should().BeEquivalentTo(expectedColumns);
        }
    }
}
