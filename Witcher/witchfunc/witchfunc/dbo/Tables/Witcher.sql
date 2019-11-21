CREATE TABLE [dbo].[Witcher] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (50) NOT NULL,
    [SkillLevel]    NVARCHAR (20) NOT NULL,
    [NumberOfKills] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

