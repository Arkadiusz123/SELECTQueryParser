using System;

namespace SelectParser
{
    class Program
    {
        static void Main(string[] args)
        {
             //string query = "SELECT CustomerName, City FROM Customers;";
             //string query = "SELECT c.CustomerName, c.City, s.PurchaseID, s.PurchaseDate FROM Customers c JOIN Sales s ON s.CustomerID = c.ID;";
             string query = "SELECT pod.PurchaseOrderID, pod.ReceivedQty, pod.RejectedQty, rp.RejectedQty / pod.ReceivedQty AS RejectRatio, pod.DueDate " +
                "FROM Purchasing.PurchaseOrderDetail pod JOIN SalesRejectedProducts rp ON pod.RejectedId = rp.Id WHERE RejectedQty / ReceivedQty > 0 " +
                "AND DueDate > CONVERT(DATETIME, ‘20010630’, 101);";

            var listOfColumns = ParserService.FindColumnsInQuery(query);

            foreach (var item in listOfColumns)
            {
                Console.WriteLine(item);
            }
        }
    }
}
