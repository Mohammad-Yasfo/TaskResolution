CREATE VIEW [cfg].[vw_StudentSummary] AS
SELECT 
    s.[Number] AS [Employee Number],
    CONCAT(s.[FirstName], ' ', s.[MidName], ' ', s.[LastName]) AS [Full Name],
    FORMAT(s.[BirthDate], 'dd-MMM-yyyy') AS [Birth Date],
    DATEDIFF(YEAR, s.[BirthDate], GETDATE()) AS [Age],
    COUNT(ss.[SubjectId]) AS [No Of Subjects]
FROM 
    [cfg].[Students] s
LEFT JOIN 
    [cfg].[StudentSubjects] ss ON s.[ID] = ss.[StudentId]
GROUP BY 
    s.[Number], s.[FirstName], s.[MidName], s.[LastName], s.[BirthDate];