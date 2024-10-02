CREATE TABLE [cfg].[Students] (
    [ID]        UNIQUEIDENTIFIER NOT NULL,
    [Number]    NVARCHAR (50)    NOT NULL,
    [FirstName] NVARCHAR (100)   NOT NULL,
    [MidName]   NVARCHAR (100)   NOT NULL,
    [LastName]  NVARCHAR (100)   NOT NULL,
    [BirthDate] DATETIME2 (7)    NOT NULL,
    [UpdatedAt] DATETIME2 (7)    NOT NULL,
    [UpdatedBy] UNIQUEIDENTIFIER NOT NULL,
    [CreatedAt] DATETIME2 (7)    NOT NULL,
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Students_Number]
    ON [cfg].[Students]([Number] ASC);

