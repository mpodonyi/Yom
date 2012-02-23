
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 02/23/2012 10:00:33
-- Generated from EDMX file: C:\Users\mpodonyi\Desktop\Yom\src\Yom.Lib\Data\EF\Yom.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [db266d57c4634e482082f89fd500994ef7];
GO
IF SCHEMA_ID(N'Yom') IS NULL EXECUTE(N'CREATE SCHEMA [Yom]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[Yom].[FK_UserReferenceUser]', 'F') IS NOT NULL
    ALTER TABLE [Yom].[ReferenceUsers] DROP CONSTRAINT [FK_UserReferenceUser];
GO
IF OBJECT_ID(N'[Yom].[FK_UserItem]', 'F') IS NOT NULL
    ALTER TABLE [Yom].[Items] DROP CONSTRAINT [FK_UserItem];
GO
IF OBJECT_ID(N'[Yom].[FK_ItemOweItem]', 'F') IS NOT NULL
    ALTER TABLE [Yom].[OweItems] DROP CONSTRAINT [FK_ItemOweItem];
GO
IF OBJECT_ID(N'[Yom].[FK_ReferenceUserOweItem]', 'F') IS NOT NULL
    ALTER TABLE [Yom].[OweItems] DROP CONSTRAINT [FK_ReferenceUserOweItem];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[Yom].[Users]', 'U') IS NOT NULL
    DROP TABLE [Yom].[Users];
GO
IF OBJECT_ID(N'[Yom].[ReferenceUsers]', 'U') IS NOT NULL
    DROP TABLE [Yom].[ReferenceUsers];
GO
IF OBJECT_ID(N'[Yom].[Items]', 'U') IS NOT NULL
    DROP TABLE [Yom].[Items];
GO
IF OBJECT_ID(N'[Yom].[OweItems]', 'U') IS NOT NULL
    DROP TABLE [Yom].[OweItems];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [Yom].[Users] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [ProviderUserKey] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'ReferenceUsers'
CREATE TABLE [Yom].[ReferenceUsers] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [UserId] bigint  NOT NULL,
    [Mail] nvarchar(256)  NOT NULL,
    [Firstname] nvarchar(50)  NULL,
    [Lastname] nvarchar(50)  NULL
);
GO

-- Creating table 'Items'
CREATE TABLE [Yom].[Items] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Type] smallint  NOT NULL,
    [UserId] bigint  NOT NULL
);
GO

-- Creating table 'OweItems'
CREATE TABLE [Yom].[OweItems] (
    [Id] bigint  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Amount] decimal(18,2)  NOT NULL,
    [ReferenceUser] bigint  NOT NULL
);
GO

-- Creating table 'PayItems'
CREATE TABLE [Yom].[PayItems] (
    [Id] bigint  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Amount] decimal(18,2)  NOT NULL,
    [ReferenceUser] bigint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [Yom].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReferenceUsers'
ALTER TABLE [Yom].[ReferenceUsers]
ADD CONSTRAINT [PK_ReferenceUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Items'
ALTER TABLE [Yom].[Items]
ADD CONSTRAINT [PK_Items]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OweItems'
ALTER TABLE [Yom].[OweItems]
ADD CONSTRAINT [PK_OweItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PayItems'
ALTER TABLE [Yom].[PayItems]
ADD CONSTRAINT [PK_PayItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'ReferenceUsers'
ALTER TABLE [Yom].[ReferenceUsers]
ADD CONSTRAINT [FK_UserReferenceUser]
    FOREIGN KEY ([UserId])
    REFERENCES [Yom].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserReferenceUser'
CREATE INDEX [IX_FK_UserReferenceUser]
ON [Yom].[ReferenceUsers]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'Items'
ALTER TABLE [Yom].[Items]
ADD CONSTRAINT [FK_UserItem]
    FOREIGN KEY ([UserId])
    REFERENCES [Yom].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserItem'
CREATE INDEX [IX_FK_UserItem]
ON [Yom].[Items]
    ([UserId]);
GO

-- Creating foreign key on [Id] in table 'OweItems'
ALTER TABLE [Yom].[OweItems]
ADD CONSTRAINT [FK_ItemOweItem]
    FOREIGN KEY ([Id])
    REFERENCES [Yom].[Items]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ReferenceUser] in table 'OweItems'
ALTER TABLE [Yom].[OweItems]
ADD CONSTRAINT [FK_ReferenceUserOweItem]
    FOREIGN KEY ([ReferenceUser])
    REFERENCES [Yom].[ReferenceUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReferenceUserOweItem'
CREATE INDEX [IX_FK_ReferenceUserOweItem]
ON [Yom].[OweItems]
    ([ReferenceUser]);
GO

-- Creating foreign key on [Id] in table 'PayItems'
ALTER TABLE [Yom].[PayItems]
ADD CONSTRAINT [FK_ItemPayItem]
    FOREIGN KEY ([Id])
    REFERENCES [Yom].[Items]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ReferenceUser] in table 'PayItems'
ALTER TABLE [Yom].[PayItems]
ADD CONSTRAINT [FK_ReferenceUserPayItem]
    FOREIGN KEY ([ReferenceUser])
    REFERENCES [Yom].[ReferenceUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReferenceUserPayItem'
CREATE INDEX [IX_FK_ReferenceUserPayItem]
ON [Yom].[PayItems]
    ([ReferenceUser]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------