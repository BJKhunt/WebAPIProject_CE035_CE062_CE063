Declare @JSON varchar(max)
SELECT @JSON=BulkColumn
FROM OPENROWSET (BULK 'C:\Users\Brijesh\Desktop\TrashBin\againgiveit\fundastock.json', SINGLE_CLOB) import
SELECT * INTO Stock
FROM OPENJSON (@JSON)
WITH 
(
    [Logo] varchar(max), 
    [Listdate] varchar(max), 
    [Country] varchar(max), 
    [Industry] varchar(max), 
    [Sector] varchar(max), 
    [Marketcap] varchar(max), 
    [Employees] varchar(max), 
    [Phone] varchar(max),
    [Ceo] varchar(max),
    [Url] varchar(max),
    [Description] varchar(max),
    [Exchange] varchar(max),
    [Name] varchar(max),
    [Symbol] varchar(max),
    [Hq_address] varchar(max)
)