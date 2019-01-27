

sp_configure 'show advanced options', 1;  
RECONFIGURE;
GO 
sp_configure 'Ad Hoc Distributed Queries', 1;  
RECONFIGURE;  
GO 
SET IDENTITY_INSERT [Measuring].[dbo].[Types] ON
  INSERT INTO [Measuring].[dbo].[Types](TypeId, TypeName, ValidityPierod, Price, TypeDesc, ProducerId, LaboratoryId, DeviceName)
SELECT 
      [TypeId]
	  ,[TypeName]
      ,[ValidityPierod]
      ,[Price]
      ,[TypeDesc]
      ,[ProducerId]
     ,[LaboratoryId]
      ,[DeviceName]
      
FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0', 
'Excel 8.0;Database=C:\MSSQLTips\1540_OPENROWSET_Examples.xls;', 
'SELECT TypeId, TypeName, ValidityPierod, Price, TypeDesc, ProducerId, LaboratoryId, DeviceName  
FROM [UPDATE_Example$]') 
GO
SET IDENTITY_INSERT [Measuring].[dbo].[Types] OFF


