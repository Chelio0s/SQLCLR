﻿-- Процедура, вызывает CLR процедуру на создание оборудования и 
-- распределение его по группам, затем выводит эту красоту на экран

CREATE PROCEDURE [SelectEquipmentInGroups]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   EXECUTE temp.[dbo].[ParseEquipment]
   SELECT * FROM [belwestDB].[dbo].[temp_equip]
END
GO

