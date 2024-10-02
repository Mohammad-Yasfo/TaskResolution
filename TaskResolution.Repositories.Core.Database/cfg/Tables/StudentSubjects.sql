CREATE TABLE [cfg].[StudentSubjects] (
    [StudentId]       UNIQUEIDENTIFIER NOT NULL,
    [SubjectId]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedDateTime] DATETIME2 (7)    NOT NULL,
    [CreatedBy]       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_StudentSubjects] PRIMARY KEY CLUSTERED ([StudentId] ASC, [SubjectId] ASC),
    CONSTRAINT [FK_StudentSubjects_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [cfg].[Students] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_StudentSubjects_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [cfg].[Subjects] ([ID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_StudentSubjects_SubjectId]
    ON [cfg].[StudentSubjects]([SubjectId] ASC);

