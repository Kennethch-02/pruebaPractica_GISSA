USE [master]
RESTORE DATABASE [WideWorldImporters] FROM 
DISK = N'A:\WideWorldImporters-Full.bak' WITH
MOVE N'WWI_Primary' TO N'A:\WideWorldImporters-Full\WideWorldImporters.mdf',  
MOVE N'WWI_UserData' TO N'A:\WideWorldImporters-Full\WideWorldImporters_UserData.ndf',  
MOVE N'WWI_Log' TO N'A:\WideWorldImporters-Full\WideWorldImporters.ldf',  
MOVE N'WWI_InMemory_Data_1'
TO N'A:\WideWorldImporters-Full\WideWorldImporters_InMemory_Data_1'
GO