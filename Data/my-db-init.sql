-- Create a new table called 'purchasing_power' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.purchasing_power', 'U') IS NOT NULL
DROP TABLE dbo.purchasing_power
GO
-- Create the table in the specified schema
CREATE TABLE dbo.purchasing_power
(
    id INT NOT NULL PRIMARY KEY IDENTITY,
    year INT NOT NULL,
    city NVARCHAR(50) NOT NULL,
    category CHAR(1) NOT NULL,
    value DECIMAL(7, 2) NOT NULL
);
GO

BULK INSERT dbo.purchasing_power
FROM '\tmp\UBS_PricesAndEarnings_OpenData.prepared.csv'
WITH ( FORMAT='CSV',
       -- FIRSTROW=2,
       FIELDTERMINATOR=',',
       ROWTERMINATOR='0x0a');
GO

SELECT * FROM dbo.purchasing_power;
GO

-- Get a list of tables and views in the current database
-- SELECT table_catalog [database], table_schema [schema], table_name name, table_type type
-- FROM INFORMATION_SCHEMA.TABLES
-- GO