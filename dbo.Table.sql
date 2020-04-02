CREATE TABLE [dbo].[TB_T_Supplier]
(
	[Email] NVARCHAR(256) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Address] NVARCHAR(50) NOT NULL, 
    [City] NVARCHAR(50) NOT NULL, 
    [State] NVARCHAR(50) NOT NULL, 
    [Country] NVARCHAR(50) NOT NULL, 
    [Zipcode] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_TB_T_Supplier_ToTable] FOREIGN KEY (Email) REFERENCES [AspNetUsers](Email)
)
