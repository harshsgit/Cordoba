
ALTER TABLE [dbo].[product] DROP CONSTRAINT [DF__product__date_mo__17F790F9]
GO



ALTER TABLE Category DROP CONSTRAINT DF__category__date_m__20C1E124
GO


ALTER TABLE Category
ALTER COLUMN date_modified datetime2 null;