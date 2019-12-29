IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Admins] (
    [Id] int NOT NULL IDENTITY,
    [email] nvarchar(max) NULL,
    [passwordhash] varbinary(max) NULL,
    [passwordsalt] varbinary(max) NULL,
    CONSTRAINT [PK_Admins] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Customers] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [gender] nvarchar(max) NULL,
    [address] nvarchar(max) NULL,
    [phoneNumber] nvarchar(max) NULL,
    [email] nvarchar(max) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Expenses] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [date] datetime2 NOT NULL,
    [amount] decimal(18, 2) NOT NULL,
    CONSTRAINT [PK_Expenses] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ProductBrands] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_ProductBrands] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ProductCategories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_ProductCategories] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ProductColors] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_ProductColors] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ProductCountries] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_ProductCountries] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [gender] nvarchar(max) NULL,
    [address] nvarchar(max) NULL,
    [phoneNumber] nvarchar(max) NULL,
    [email] nvarchar(max) NULL,
    [passwordHash] varbinary(max) NULL,
    [passwordSalt] varbinary(max) NULL,
    [userRole] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [SalesProducts] (
    [Id] int NOT NULL IDENTITY,
    [salesdate] datetime2 NOT NULL,
    [totalQuantity] int NOT NULL,
    [totalPrice] decimal(18, 2) NOT NULL,
    [staffId] int NULL,
    [customerId] int NULL,
    CONSTRAINT [PK_SalesProducts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SalesProducts_Customers_customerId] FOREIGN KEY ([customerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SalesProducts_Users_staffId] FOREIGN KEY ([staffId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [costPrice] decimal(18, 2) NOT NULL,
    [sellingPrice] decimal(18, 2) NOT NULL,
    [productImageUrl] nvarchar(max) NULL,
    [prodCode] nvarchar(max) NULL,
    [quantityAvailable] int NOT NULL,
    [status] bit NOT NULL,
    [lastStockDate] datetime2 NOT NULL,
    [productCategoryId] int NULL,
    [productBrandId] int NULL,
    [productColorId] int NULL,
    [productCountryId] int NULL,
    [SalesProductId] int NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_SalesProducts_SalesProductId] FOREIGN KEY ([SalesProductId]) REFERENCES [SalesProducts] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Products_ProductBrands_productBrandId] FOREIGN KEY ([productBrandId]) REFERENCES [ProductBrands] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Products_ProductCategories_productCategoryId] FOREIGN KEY ([productCategoryId]) REFERENCES [ProductCategories] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Products_ProductColors_productColorId] FOREIGN KEY ([productColorId]) REFERENCES [ProductColors] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Products_ProductCountries_productCountryId] FOREIGN KEY ([productCountryId]) REFERENCES [ProductCountries] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Products_SalesProductId] ON [Products] ([SalesProductId]);

GO

CREATE INDEX [IX_Products_productBrandId] ON [Products] ([productBrandId]);

GO

CREATE INDEX [IX_Products_productCategoryId] ON [Products] ([productCategoryId]);

GO

CREATE INDEX [IX_Products_productColorId] ON [Products] ([productColorId]);

GO

CREATE INDEX [IX_Products_productCountryId] ON [Products] ([productCountryId]);

GO

CREATE INDEX [IX_SalesProducts_customerId] ON [SalesProducts] ([customerId]);

GO

CREATE INDEX [IX_SalesProducts_staffId] ON [SalesProducts] ([staffId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190608143943_InitialCreate', N'2.1.11-servicing-32099');

GO

ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_SalesProducts_SalesProductId];

GO

ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_ProductBrands_productBrandId];

GO

ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_ProductCategories_productCategoryId];

GO

ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_ProductColors_productColorId];

GO

ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_ProductCountries_productCountryId];

GO

DROP TABLE [Admins];

GO

DROP TABLE [SalesProducts];

GO

DROP TABLE [Users];

GO

DROP INDEX [IX_Products_SalesProductId] ON [Products];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'SalesProductId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Products] DROP COLUMN [SalesProductId];

GO

DROP INDEX [IX_Products_productCountryId] ON [Products];
DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'productCountryId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Products] ALTER COLUMN [productCountryId] int NOT NULL;
CREATE INDEX [IX_Products_productCountryId] ON [Products] ([productCountryId]);

GO

DROP INDEX [IX_Products_productColorId] ON [Products];
DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'productColorId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Products] ALTER COLUMN [productColorId] int NOT NULL;
CREATE INDEX [IX_Products_productColorId] ON [Products] ([productColorId]);

GO

DROP INDEX [IX_Products_productCategoryId] ON [Products];
DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'productCategoryId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Products] ALTER COLUMN [productCategoryId] int NOT NULL;
CREATE INDEX [IX_Products_productCategoryId] ON [Products] ([productCategoryId]);

GO

DROP INDEX [IX_Products_productBrandId] ON [Products];
DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'productBrandId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Products] ALTER COLUMN [productBrandId] int NOT NULL;
CREATE INDEX [IX_Products_productBrandId] ON [Products] ([productBrandId]);

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [fullName] nvarchar(max) NULL,
    [gender] nvarchar(max) NULL,
    [gaurantorName] nvarchar(max) NULL,
    [gaurantorPhoneNumber] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ShoppingCarts] (
    [ShoppingCartId] nvarchar(450) NOT NULL,
    [salesdate] datetime2 NOT NULL,
    [totalQuantity] int NOT NULL,
    [totalPrice] decimal(18, 2) NOT NULL,
    [staffId] nvarchar(450) NULL,
    [customerId] int NULL,
    CONSTRAINT [PK_ShoppingCarts] PRIMARY KEY ([ShoppingCartId]),
    CONSTRAINT [FK_ShoppingCarts_Customers_customerId] FOREIGN KEY ([customerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ShoppingCarts_AspNetUsers_staffId] FOREIGN KEY ([staffId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ShoppingCartitems] (
    [shoppingCartItemId] int NOT NULL IDENTITY,
    [quantity] int NOT NULL,
    [productId] int NULL,
    [CartId] nvarchar(max) NULL,
    [ShoppingCartId] nvarchar(450) NULL,
    CONSTRAINT [PK_ShoppingCartitems] PRIMARY KEY ([shoppingCartItemId]),
    CONSTRAINT [FK_ShoppingCartitems_ShoppingCarts_ShoppingCartId] FOREIGN KEY ([ShoppingCartId]) REFERENCES [ShoppingCarts] ([ShoppingCartId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ShoppingCartitems_Products_productId] FOREIGN KEY ([productId]) REFERENCES [Products] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

CREATE INDEX [IX_ShoppingCartitems_ShoppingCartId] ON [ShoppingCartitems] ([ShoppingCartId]);

GO

CREATE INDEX [IX_ShoppingCartitems_productId] ON [ShoppingCartitems] ([productId]);

GO

CREATE INDEX [IX_ShoppingCarts_customerId] ON [ShoppingCarts] ([customerId]);

GO

CREATE INDEX [IX_ShoppingCarts_staffId] ON [ShoppingCarts] ([staffId]);

GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_ProductBrands_productBrandId] FOREIGN KEY ([productBrandId]) REFERENCES [ProductBrands] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_ProductCategories_productCategoryId] FOREIGN KEY ([productCategoryId]) REFERENCES [ProductCategories] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_ProductColors_productColorId] FOREIGN KEY ([productColorId]) REFERENCES [ProductColors] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_ProductCountries_productCountryId] FOREIGN KEY ([productCountryId]) REFERENCES [ProductCountries] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190707130255_AddedIdentity_UpdatedDataModel', N'2.1.11-servicing-32099');

GO

ALTER TABLE [AspNetUsers] ADD [address] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190707194650_AddedAddressColumn_UserTB', N'2.1.11-servicing-32099');

GO

ALTER TABLE [ShoppingCartitems] DROP CONSTRAINT [FK_ShoppingCartitems_ShoppingCarts_ShoppingCartId];

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShoppingCartitems]') AND [c].[name] = N'CartId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [ShoppingCartitems] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [ShoppingCartitems] DROP COLUMN [CartId];

GO

EXEC sp_rename N'[ShoppingCartitems].[ShoppingCartId]', N'cartShoppingCartId', N'COLUMN';

GO

EXEC sp_rename N'[ShoppingCartitems].[IX_ShoppingCartitems_ShoppingCartId]', N'IX_ShoppingCartitems_cartShoppingCartId', N'INDEX';

GO

ALTER TABLE [ShoppingCartitems] ADD CONSTRAINT [FK_ShoppingCartitems_ShoppingCarts_cartShoppingCartId] FOREIGN KEY ([cartShoppingCartId]) REFERENCES [ShoppingCarts] ([ShoppingCartId]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190717052241_Edited_Shopping_Item_Model', N'2.1.11-servicing-32099');

GO

ALTER TABLE [ShoppingCartitems] DROP CONSTRAINT [FK_ShoppingCartitems_ShoppingCarts_cartShoppingCartId];

GO

EXEC sp_rename N'[ShoppingCartitems].[cartShoppingCartId]', N'ShoppingCartId', N'COLUMN';

GO

EXEC sp_rename N'[ShoppingCartitems].[IX_ShoppingCartitems_cartShoppingCartId]', N'IX_ShoppingCartitems_ShoppingCartId', N'INDEX';

GO

ALTER TABLE [ShoppingCartitems] ADD CONSTRAINT [FK_ShoppingCartitems_ShoppingCarts_ShoppingCartId] FOREIGN KEY ([ShoppingCartId]) REFERENCES [ShoppingCarts] ([ShoppingCartId]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190717060751_Edited_ShoppingCart_Model', N'2.1.11-servicing-32099');

GO

ALTER TABLE [ShoppingCartitems] DROP CONSTRAINT [FK_ShoppingCartitems_Products_productId];

GO

ALTER TABLE [ShoppingCarts] DROP CONSTRAINT [FK_ShoppingCarts_Customers_customerId];

GO

ALTER TABLE [ShoppingCarts] DROP CONSTRAINT [FK_ShoppingCarts_AspNetUsers_staffId];

GO

EXEC sp_rename N'[ShoppingCarts].[staffId]', N'StaffId', N'COLUMN';

GO

EXEC sp_rename N'[ShoppingCarts].[customerId]', N'CustomerId', N'COLUMN';

GO

EXEC sp_rename N'[ShoppingCarts].[IX_ShoppingCarts_staffId]', N'IX_ShoppingCarts_StaffId', N'INDEX';

GO

EXEC sp_rename N'[ShoppingCarts].[IX_ShoppingCarts_customerId]', N'IX_ShoppingCarts_CustomerId', N'INDEX';

GO

EXEC sp_rename N'[ShoppingCartitems].[productId]', N'ProductId', N'COLUMN';

GO

EXEC sp_rename N'[ShoppingCartitems].[IX_ShoppingCartitems_productId]', N'IX_ShoppingCartitems_ProductId', N'INDEX';

GO

DROP INDEX [IX_ShoppingCarts_CustomerId] ON [ShoppingCarts];
DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShoppingCarts]') AND [c].[name] = N'CustomerId');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [ShoppingCarts] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [ShoppingCarts] ALTER COLUMN [CustomerId] int NOT NULL;
CREATE INDEX [IX_ShoppingCarts_CustomerId] ON [ShoppingCarts] ([CustomerId]);

GO

DROP INDEX [IX_ShoppingCartitems_ProductId] ON [ShoppingCartitems];
DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShoppingCartitems]') AND [c].[name] = N'ProductId');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [ShoppingCartitems] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [ShoppingCartitems] ALTER COLUMN [ProductId] int NOT NULL;
CREATE INDEX [IX_ShoppingCartitems_ProductId] ON [ShoppingCartitems] ([ProductId]);

GO

ALTER TABLE [ShoppingCartitems] ADD [profit] decimal(18, 2) NOT NULL DEFAULT 0.0;

GO

ALTER TABLE [ShoppingCartitems] ADD CONSTRAINT [FK_ShoppingCartitems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [ShoppingCarts] ADD CONSTRAINT [FK_ShoppingCarts_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [ShoppingCarts] ADD CONSTRAINT [FK_ShoppingCarts_AspNetUsers_StaffId] FOREIGN KEY ([StaffId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190721185806_ProfitColumnAdded_ToShoppingCartItemModel', N'2.1.11-servicing-32099');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190721190243_Fixed_All_the_DecimatypeWarning', N'2.1.11-servicing-32099');

GO

ALTER TABLE [ShoppingCarts] ADD [totalProfit] decimal(18, 2) NOT NULL DEFAULT 0.0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190721191504_AddedTotalProfit_ShopModels', N'2.1.11-servicing-32099');

GO

