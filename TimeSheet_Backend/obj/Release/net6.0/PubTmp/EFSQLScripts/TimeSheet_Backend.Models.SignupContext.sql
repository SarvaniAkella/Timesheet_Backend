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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230803045549_initial')
BEGIN
    CREATE TABLE [Activities] (
        [ActivityId] int NOT NULL IDENTITY,
        [ActivityName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Activities] PRIMARY KEY ([ActivityId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230803045549_initial')
BEGIN
    CREATE TABLE [Projects] (
        [ProjectId] int NOT NULL IDENTITY,
        [ProjectName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Projects] PRIMARY KEY ([ProjectId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230803045549_initial')
BEGIN
    CREATE TABLE [roles] (
        [roleId] int NOT NULL IDENTITY,
        [roleName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_roles] PRIMARY KEY ([roleId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230803045549_initial')
BEGIN
    CREATE TABLE [Users] (
        [UserId] int NOT NULL IDENTITY,
        [Username] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [PasswordHash] varbinary(max) NOT NULL,
        [PasswordSalt] varbinary(max) NOT NULL,
        [IsVerified] bit NOT NULL,
        [VerificationToken] nvarchar(max) NULL,
        [PasswordToken] nvarchar(max) NULL,
        [Mobileno] nvarchar(max) NOT NULL,
        [roleId] int NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230803045549_initial')
BEGIN
    CREATE TABLE [TimeSheets] (
        [TimeSheetId] int NOT NULL IDENTITY,
        [task] nvarchar(max) NOT NULL,
        [hours] int NOT NULL,
        [CreatedDate] date NOT NULL,
        [ProjectId] int NOT NULL,
        [UserId] int NOT NULL,
        [ActivityId] int NOT NULL,
        CONSTRAINT [PK_TimeSheets] PRIMARY KEY ([TimeSheetId]),
        CONSTRAINT [FK_TimeSheets_Activities_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [Activities] ([ActivityId]) ON DELETE CASCADE,
        CONSTRAINT [FK_TimeSheets_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([ProjectId]) ON DELETE CASCADE,
        CONSTRAINT [FK_TimeSheets_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230803045549_initial')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ActivityId', N'ActivityName') AND [object_id] = OBJECT_ID(N'[Activities]'))
        SET IDENTITY_INSERT [Activities] ON;
    EXEC(N'INSERT INTO [Activities] ([ActivityId], [ActivityName])
    VALUES (1, N''Unit Testing''),
    (2, N''Acceptance Testing''),
    (3, N''Warranty/MC''),
    (4, N''System Testing''),
    (5, N''Coding/Implementation''),
    (6, N''Design''),
    (7, N''Support''),
    (8, N''Integration Testing''),
    (9, N''Requirements Development''),
    (10, N''Planning''),
    (11, N''PTO'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ActivityId', N'ActivityName') AND [object_id] = OBJECT_ID(N'[Activities]'))
        SET IDENTITY_INSERT [Activities] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230803045549_initial')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProjectId', N'ProjectName') AND [object_id] = OBJECT_ID(N'[Projects]'))
        SET IDENTITY_INSERT [Projects] ON;
    EXEC(N'INSERT INTO [Projects] ([ProjectId], [ProjectName])
    VALUES (1, N''Persona Nutrition''),
    (2, N''Puritains''),
    (3, N''Nestle Health Sciences''),
    (4, N''Market Central''),
    (5, N''Family Central''),
    (6, N''Internal POC''),
    (7, N''External POC''),
    (8, N''Marketing & Sales'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProjectId', N'ProjectName') AND [object_id] = OBJECT_ID(N'[Projects]'))
        SET IDENTITY_INSERT [Projects] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230803045549_initial')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'roleId', N'roleName') AND [object_id] = OBJECT_ID(N'[roles]'))
        SET IDENTITY_INSERT [roles] ON;
    EXEC(N'INSERT INTO [roles] ([roleId], [roleName])
    VALUES (1, N''User''),
    (2, N''Hr''),
    (3, N''Admin'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'roleId', N'roleName') AND [object_id] = OBJECT_ID(N'[roles]'))
        SET IDENTITY_INSERT [roles] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230803045549_initial')
BEGIN
    CREATE INDEX [IX_TimeSheets_ActivityId] ON [TimeSheets] ([ActivityId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230803045549_initial')
BEGIN
    CREATE INDEX [IX_TimeSheets_ProjectId] ON [TimeSheets] ([ProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230803045549_initial')
BEGIN
    CREATE INDEX [IX_TimeSheets_UserId] ON [TimeSheets] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230803045549_initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230803045549_initial', N'7.0.9');
END;
GO

COMMIT;
GO

