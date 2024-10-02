
CREATE VIEW [cfg].[vw_SubjectStudents] AS
SELECT 
    sub.[Code] AS [Subject Code],
    sub.[Name] AS [Subject Name],
    CONCAT(s.[FirstName], ' ', s.[MidName], ' ', s.[LastName]) AS [Full Name],
    s.[Number] AS [Employee Number]
FROM 
    [cfg].[Subjects] sub
JOIN 
    [cfg].[StudentSubjects] ss ON sub.[ID] = ss.[SubjectId]
JOIN 
    [cfg].[Students] s ON ss.[StudentId] = s.[ID];