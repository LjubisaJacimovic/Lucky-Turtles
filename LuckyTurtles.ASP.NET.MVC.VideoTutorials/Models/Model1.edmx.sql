
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/08/2015 22:15:09
-- Generated from EDMX file: C:\Users\Toni\Documents\Visual Studio 2013\Projects\Team Project\SEDC.LuckyTurtles.OLS\LuckyTurtles.ASP.NET.MVC.VideoTutorials\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LuckyTurtlesVideoTuts];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_History_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[History] DROP CONSTRAINT [FK_History_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_History_Videos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[History] DROP CONSTRAINT [FK_History_Videos];
GO
IF OBJECT_ID(N'[dbo].[FK_Videos_RejectMessage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Videos] DROP CONSTRAINT [FK_Videos_RejectMessage];
GO
IF OBJECT_ID(N'[dbo].[FK_Videos_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Videos] DROP CONSTRAINT [FK_Videos_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_VideoTags_Tags]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VideoTags] DROP CONSTRAINT [FK_VideoTags_Tags];
GO
IF OBJECT_ID(N'[dbo].[FK_VideoTags_Videos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VideoTags] DROP CONSTRAINT [FK_VideoTags_Videos];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[History]', 'U') IS NOT NULL
    DROP TABLE [dbo].[History];
GO
IF OBJECT_ID(N'[dbo].[RejectMessage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RejectMessage];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Tags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tags];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Videos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Videos];
GO
IF OBJECT_ID(N'[dbo].[VideoTags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VideoTags];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'History'
CREATE TABLE [dbo].[History] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [userId] int  NOT NULL,
    [videoId] int  NOT NULL
);
GO

-- Creating table 'RejectMessage'
CREATE TABLE [dbo].[RejectMessage] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Message] nvarchar(max)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Tags'
CREATE TABLE [dbo].[Tags] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TagName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Username] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [isAdmin] bit  NOT NULL,
    [Approved] bit  NOT NULL
);
GO

-- Creating table 'Videos'
CREATE TABLE [dbo].[Videos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [SourceLink] nvarchar(50)  NOT NULL,
    [DatePublished] datetime  NOT NULL,
    [Duration] int  NULL,
    [UserId] int  NOT NULL,
    [isApproved] bit  NOT NULL,
    [Rejected] int  NULL
);
GO

-- Creating table 'VideoTags'
CREATE TABLE [dbo].[VideoTags] (
    [Tags_Id] int  NOT NULL,
    [Videos_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'History'
ALTER TABLE [dbo].[History]
ADD CONSTRAINT [PK_History]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RejectMessage'
ALTER TABLE [dbo].[RejectMessage]
ADD CONSTRAINT [PK_RejectMessage]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'Tags'
ALTER TABLE [dbo].[Tags]
ADD CONSTRAINT [PK_Tags]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Videos'
ALTER TABLE [dbo].[Videos]
ADD CONSTRAINT [PK_Videos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Tags_Id], [Videos_Id] in table 'VideoTags'
ALTER TABLE [dbo].[VideoTags]
ADD CONSTRAINT [PK_VideoTags]
    PRIMARY KEY CLUSTERED ([Tags_Id], [Videos_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [userId] in table 'History'
ALTER TABLE [dbo].[History]
ADD CONSTRAINT [FK_History_Users]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_History_Users'
CREATE INDEX [IX_FK_History_Users]
ON [dbo].[History]
    ([userId]);
GO

-- Creating foreign key on [videoId] in table 'History'
ALTER TABLE [dbo].[History]
ADD CONSTRAINT [FK_History_Videos]
    FOREIGN KEY ([videoId])
    REFERENCES [dbo].[Videos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_History_Videos'
CREATE INDEX [IX_FK_History_Videos]
ON [dbo].[History]
    ([videoId]);
GO

-- Creating foreign key on [Rejected] in table 'Videos'
ALTER TABLE [dbo].[Videos]
ADD CONSTRAINT [FK_Videos_RejectMessage]
    FOREIGN KEY ([Rejected])
    REFERENCES [dbo].[RejectMessage]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Videos_RejectMessage'
CREATE INDEX [IX_FK_Videos_RejectMessage]
ON [dbo].[Videos]
    ([Rejected]);
GO

-- Creating foreign key on [UserId] in table 'Videos'
ALTER TABLE [dbo].[Videos]
ADD CONSTRAINT [FK_Videos_Users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Videos_Users'
CREATE INDEX [IX_FK_Videos_Users]
ON [dbo].[Videos]
    ([UserId]);
GO

-- Creating foreign key on [Tags_Id] in table 'VideoTags'
ALTER TABLE [dbo].[VideoTags]
ADD CONSTRAINT [FK_VideoTags_Tags]
    FOREIGN KEY ([Tags_Id])
    REFERENCES [dbo].[Tags]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Videos_Id] in table 'VideoTags'
ALTER TABLE [dbo].[VideoTags]
ADD CONSTRAINT [FK_VideoTags_Videos]
    FOREIGN KEY ([Videos_Id])
    REFERENCES [dbo].[Videos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_VideoTags_Videos'
CREATE INDEX [IX_FK_VideoTags_Videos]
ON [dbo].[VideoTags]
    ([Videos_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------