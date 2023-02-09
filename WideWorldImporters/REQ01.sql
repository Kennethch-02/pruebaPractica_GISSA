USE [WideWorldImporters]
/* Opcion mas facil de entender gracias a la funcion COALESCE */
SELECT Sales.Invoices.InvoiceID, Sales.Invoices.CustomerID, Sales.Customers.CustomerName, Application.DeliveryMethods.DeliveryMethodName,
       COALESCE(Customers.CreditLimit, -1) AS CreditLimit, People.FullName AS SellerName
FROM Sales.Invoices
INNER JOIN Sales.Customers ON Sales.Invoices.CustomerID = Sales.Customers.CustomerID
INNER JOIN Application.DeliveryMethods ON Sales.Invoices.DeliveryMethodID = Application.DeliveryMethods.DeliveryMethodID
INNER JOIN Application.People ON Sales.Invoices.SalespersonPersonID = Application.People.PersonID
WHERE Sales.Invoices.InvoiceDate BETWEEN '2013-01-01' AND '2015-12-31'