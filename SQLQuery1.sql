sp_configure 'show advanced options', 1;  
RECONFIGURE;
GO 
sp_configure 'Ad Hoc Distributed Queries', 1;  
RECONFIGURE;  
GO 
SET IDENTITY_INSERT [Measuring].[dbo].[Devices] ON
  INSERT INTO [Measuring].[dbo].[Devices](DeviceId, InventoryNo, SerialNo, VerificationDate, TimeToVerification, VerificationResult, ProductionDate, DeviceDesc, TypeId, RegistrationNo )
SELECT 
         [DeviceId]
		 ,[InventoryNo]
      ,[SerialNo]
      ,[VerificationDate]
      ,[TimeToVerification]
      ,[VerificationResult]
     ,[ProductionDate]
      ,[DeviceDesc]
      ,[TypeId]
         ,[RegistrationNo]
FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0', 
'Excel 8.0;Database=C:\MSSQLTips\1540_OPENROWSET_Examples.xls;', 
'SELECT DeviceId,InventoryNo, SerialNo, VerificationDate, TimeToVerification, VerificationResult, ProductionDate, DeviceDesc, TypeId, RegistrationNo  
FROM [INSERT_Example$]') 
GO
SET IDENTITY_INSERT [Measuring].[dbo].[Devices] OFF



SELECT TOP 1000 [DeviceId]
      ,[InventoryNo]
      ,[SerialNo]
      ,[VerificationDate]
      ,[TimeToVerification]
      ,[VerificationResult]
      ,[ProductionDate]
      ,[DeviceDesc]
      ,[TypeId]
      ,[RegistrationNo]
  FROM [Measuring].[dbo].[Devices]
