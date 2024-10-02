CREATE TABLE [cfg].[Subjects] (
    [ID]        UNIQUEIDENTIFIER NOT NULL,
    [Name]      NVARCHAR (100)   NOT NULL,
    [Code]      NVARCHAR (50)    NOT NULL,
    [UpdatedAt] DATETIME2 (7)    NOT NULL,
    [UpdatedBy] UNIQUEIDENTIFIER NOT NULL,
    [CreatedAt] DATETIME2 (7)    NOT NULL,
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Subjects] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Subjects_Code]
    ON [cfg].[Subjects]([Code] ASC);

