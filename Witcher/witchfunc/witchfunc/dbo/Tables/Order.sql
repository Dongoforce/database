CREATE TABLE [dbo].[Order] (
    [Id]           INT IDENTITY (1, 1) NOT NULL,
    [WitcherId]    INT NOT NULL,
    [MonsterId]    INT NOT NULL,
    [CountOfMoney] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CHECK ([CountOfMoney]>=(1) AND [CountOfMoney]<=(10000)),
    FOREIGN KEY ([MonsterId]) REFERENCES [dbo].[Monster] ([Id]),
    FOREIGN KEY ([WitcherId]) REFERENCES [dbo].[Witcher] ([Id])
);

