# MS SQL Connection setup
Create `local.settings.json` in FunctionApp project and add the json below.<br>
Make sure to change your connection string.
```json
{
  "IsEncrypted": false,
  "Values": {
    "ConnectionString": "<CONNECTION STRING>"
  }
}
```

Also, in your database execute this sql command to create table and data.

```sql
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssetCollection](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Desc] [nvarchar](max) NULL,
 CONSTRAINT [PK_AssetCollection] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AssetCollection] ON 
GO
INSERT [dbo].[AssetCollection] ([Id], [Name], [Desc]) VALUES (1, N'Test 1', N'Test 1 02/03/2022 12:25:39 pm')
GO
INSERT [dbo].[AssetCollection] ([Id], [Name], [Desc]) VALUES (2, N'Test 2', N'Test 2 02/03/2022 12:25:39 pm')
GO
INSERT [dbo].[AssetCollection] ([Id], [Name], [Desc]) VALUES (3, N'Test 3', N'Test 3 02/03/2022 12:25:39 pm')
GO
INSERT [dbo].[AssetCollection] ([Id], [Name], [Desc]) VALUES (4, N'Test 4', N'Test 4 02/03/2022 12:25:39 pm')
GO
INSERT [dbo].[AssetCollection] ([Id], [Name], [Desc]) VALUES (5, N'Test 5', N'Test 5 02/03/2022 12:25:39 pm')
GO
SET IDENTITY_INSERT [dbo].[AssetCollection] OFF
GO

```
