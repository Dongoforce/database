CREATE TABLE [dbo].[Monster] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (50) NOT NULL,
    [ThreatLevel]      NVARCHAR (20) NOT NULL,
    [Class]            NVARCHAR (20) NOT NULL,
    [SusceptibilityId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([SusceptibilityId]) REFERENCES [dbo].[Susceptibility] ([Id])
);

