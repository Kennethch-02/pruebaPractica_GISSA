USE [WideWorldImporters]
/* Opcion con una consulta temporal */
;WITH CTE AS (
    SELECT 
        CustomerID, 
        COUNT(InvoiceID) AS TotalInvoices,
        ROW_NUMBER() OVER (ORDER BY COUNT(InvoiceID) DESC) AS OrderColumn
    FROM 
        Sales.Invoices 
    GROUP BY 
        CustomerID
)
SELECT 
    CTE.CustomerID, 
    Sales.Customers.CustomerName, 
    CTE.TotalInvoices, 
    CTE.OrderColumn
FROM 
    CTE 
INNER JOIN 
    Sales.Customers ON CTE.CustomerID = Customers.CustomerID
ORDER BY 
    CTE.TotalInvoices DESC, 
    CTE.CustomerID;