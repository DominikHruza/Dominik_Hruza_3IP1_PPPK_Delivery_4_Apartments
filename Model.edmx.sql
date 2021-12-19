
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/17/2021 12:24:24
-- Generated from EDMX file: C:\Users\domin\Desktop\.NET_projects\Appartments\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Apartments];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Appartments'
CREATE TABLE [dbo].[Appartments] (
    [IDAppartment] int IDENTITY(1,1) NOT NULL,
    [Address] nvarchar(50)  NOT NULL,
    [City] nvarchar(20)  NOT NULL,
    [AgentID] int  NOT NULL
);
GO

-- Creating table 'Agents'
CREATE TABLE [dbo].[Agents] (
    [IDAgent] int IDENTITY(1,1) NOT NULL,
    [Firstname] nvarchar(50)  NOT NULL,
    [Lastname] nvarchar(50)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AppartmentFiles'
CREATE TABLE [dbo].[AppartmentFiles] (
    [IDAppartmentFile] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [ContentType] nvarchar(20)  NOT NULL,
    [Content] varbinary(max)  NOT NULL,
    [AppartmentID] int  NOT NULL
);
GO

-- Creating table 'AgentFiles'
CREATE TABLE [dbo].[AgentFiles] (
    [IDAgentFile] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [ContentType] nvarchar(20)  NOT NULL,
    [Content] varbinary(max)  NOT NULL,
    [AgentID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IDAppartment] in table 'Appartments'
ALTER TABLE [dbo].[Appartments]
ADD CONSTRAINT [PK_Appartments]
    PRIMARY KEY CLUSTERED ([IDAppartment] ASC);
GO

-- Creating primary key on [IDAgent] in table 'Agents'
ALTER TABLE [dbo].[Agents]
ADD CONSTRAINT [PK_Agents]
    PRIMARY KEY CLUSTERED ([IDAgent] ASC);
GO

-- Creating primary key on [IDAppartmentFile] in table 'AppartmentFiles'
ALTER TABLE [dbo].[AppartmentFiles]
ADD CONSTRAINT [PK_AppartmentFiles]
    PRIMARY KEY CLUSTERED ([IDAppartmentFile] ASC);
GO

-- Creating primary key on [IDAgentFile] in table 'AgentFiles'
ALTER TABLE [dbo].[AgentFiles]
ADD CONSTRAINT [PK_AgentFiles]
    PRIMARY KEY CLUSTERED ([IDAgentFile] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AgentID] in table 'Appartments'
ALTER TABLE [dbo].[Appartments]
ADD CONSTRAINT [FK_AppartmentAgent]
    FOREIGN KEY ([AgentID])
    REFERENCES [dbo].[Agents]
        ([IDAgent])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AppartmentAgent'
CREATE INDEX [IX_FK_AppartmentAgent]
ON [dbo].[Appartments]
    ([AgentID]);
GO

-- Creating foreign key on [AppartmentID] in table 'AppartmentFiles'
ALTER TABLE [dbo].[AppartmentFiles]
ADD CONSTRAINT [FK_AppartmentFileAppartment]
    FOREIGN KEY ([AppartmentID])
    REFERENCES [dbo].[Appartments]
        ([IDAppartment])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AppartmentFileAppartment'
CREATE INDEX [IX_FK_AppartmentFileAppartment]
ON [dbo].[AppartmentFiles]
    ([AppartmentID]);
GO

-- Creating foreign key on [AgentID] in table 'AgentFiles'
ALTER TABLE [dbo].[AgentFiles]
ADD CONSTRAINT [FK_AgentFileAgent]
    FOREIGN KEY ([AgentID])
    REFERENCES [dbo].[Agents]
        ([IDAgent])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AgentFileAgent'
CREATE INDEX [IX_FK_AgentFileAgent]
ON [dbo].[AgentFiles]
    ([AgentID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------