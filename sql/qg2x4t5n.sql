IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210913103009_initial')
BEGIN
    CREATE TABLE [Categories] (
        [Id] uniqueidentifier NOT NULL,
        [Description] varchar(300) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210913103009_initial')
BEGIN
    CREATE TABLE [Products] (
        [Id] uniqueidentifier NOT NULL,
        [Desc] varchar(300) NOT NULL,
        [Code] varchar(20) NOT NULL,
        [Reference] varchar(20) NOT NULL,
        [StockBalance] int NOT NULL,
        [Price] decimal(18,4) NOT NULL DEFAULT 0.0,
        [IsActive] bit NOT NULL,
        [Height] decimal(18,4) NULL DEFAULT 0.0,
        [Width] decimal(18,4) NULL DEFAULT 0.0,
        [Depth] decimal(18,4) NULL DEFAULT 0.0,
        [CategoryId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210913103009_initial')
BEGIN
    CREATE INDEX [IX_Categories_Description] ON [Categories] ([Description]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210913103009_initial')
BEGIN
    CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210913103009_initial')
BEGIN
    CREATE INDEX [IX_Products_Code] ON [Products] ([Code]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210913103009_initial')
BEGIN
    CREATE INDEX [IX_Products_Code_Reference] ON [Products] ([Code], [Reference]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210913103009_initial')
BEGIN
    CREATE INDEX [IX_Products_Desc] ON [Products] ([Desc]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210913103009_initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210913103009_initial', N'5.0.9');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210913103813_001')
BEGIN
    EXEC sp_rename N'[Products].[Desc]', N'Description', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210913103813_001')
BEGIN
    EXEC sp_rename N'[Products].[IX_Products_Desc]', N'IX_Products_Description', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210913103813_001')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210913103813_001', N'5.0.9');
END;
GO

COMMIT;
GO

