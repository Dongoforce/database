CREATE TRIGGER Createtable
ON DATABASE
FOR CREATE_TABLE
AS
BEGIN
	declare @x xml = EVENTDATA();
	declare @tableName sysname; 
	set @tableName = @x.value(N'(/EVENT_INSTANCE/ObjectName)[1]', N'sysname');
	SELECT sm.object_id AS TableID, OBJECT_NAME(sm.object_id) AS TableName 
	FROM sys.dm_db_index_physical_stats(DB_ID(),OBJECT_ID(@tableName),null,null,null) as sm 
	SELECT create_date FROM sys.tables where name = @tableName
END
