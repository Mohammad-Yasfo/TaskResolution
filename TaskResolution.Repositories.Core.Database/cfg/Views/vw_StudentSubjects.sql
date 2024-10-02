CREATE VIEW [cfg].[vw_StudentSubjects] AS
WITH StudentSubjectDetails AS (
    SELECT 
        s.[ID] AS StudentId,
        s.[Number],
        s.[FirstName],
        s.[MidName],
        s.[LastName],
        sub.[Name] AS SubjectName,
        ROW_NUMBER() OVER (PARTITION BY s.[ID] ORDER BY sub.[Name]) AS rn
    FROM 
        [cfg].[Students] s
    JOIN 
        [cfg].[StudentSubjects] ss ON s.[ID] = ss.[StudentId]
    JOIN 
        [cfg].[Subjects] sub ON ss.[SubjectId] = sub.[ID]
)
SELECT 
    Number AS [Employee Number],
    CONCAT([FirstName], ' ', [MidName], ' ', [LastName]) AS [Full Name],
    MAX(CASE WHEN rn = 1 THEN SubjectName END) AS [Subject 1 Name],
    MAX(CASE WHEN rn = 2 THEN SubjectName END) AS [Subject 2 Name],
    MAX(CASE WHEN rn = 3 THEN SubjectName END) AS [Subject 3 Name],
    MAX(CASE WHEN rn = 4 THEN SubjectName END) AS [Subject 4 Name],
    MAX(CASE WHEN rn = 5 THEN SubjectName END) AS [Subject 5 Name],
    MAX(CASE WHEN rn = 6 THEN SubjectName END) AS [Subject 6 Name],
    MAX(CASE WHEN rn = 7 THEN SubjectName END) AS [Subject 7 Name]
FROM 
    StudentSubjectDetails
GROUP BY 
    [Number], [FirstName], [MidName], [LastName];