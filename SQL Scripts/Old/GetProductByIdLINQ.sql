SELECT [p].[Id], [p].[Name], [b].[BrandName], (
    SELECT COUNT(*)
    FROM [InfoRequest] AS [i]
    WHERE ([p].[Id] = [i].[ProductId]) AND [i].[UserId] IS NULL), (
    SELECT COUNT(*)
    FROM [InfoRequest] AS [i0]
    WHERE ([p].[Id] = [i0].[ProductId]) AND [i0].[UserId] IS NOT NULL), [b].[Id], [t].[Id], [t].[Name], [t].[ProductId], [t].[CategoryId], [t1].[Id], [t1].[c], [t1].[c0], [t1].[c1], [t1].[c2]
FROM [Product] AS [p]
INNER JOIN [Brand] AS [b] ON [p].[BrandId] = [b].[Id]
LEFT JOIN (
    SELECT [c].[Id], [c].[Name], [p0].[ProductId], [p0].[CategoryId]
    FROM [Product_Category] AS [p0]
    INNER JOIN [Category] AS [c] ON [p0].[CategoryId] = [c].[Id]
) AS [t] ON [p].[Id] = [t].[ProductId]
LEFT JOIN (
    SELECT [i4].[Id], CASE
        WHEN [i4].[UserId] IS NULL THEN [i4].[Name]
        ELSE [t0].[Name]
    END AS [c], CASE
        WHEN [i4].[UserId] IS NULL THEN [i4].[LastName]
        ELSE [t0].[LastName]
    END AS [c0], (
        SELECT COUNT(*)
        FROM [InfoRequestReply] AS [i1]
        WHERE [i4].[Id] = [i1].[InfoRequestId]) AS [c1], (
        SELECT MAX([i2].[InsertDate])
        FROM [InfoRequestReply] AS [i2]
        WHERE [i4].[Id] = [i2].[InfoRequestId]) AS [c2], (
        SELECT MAX([i3].[InsertDate])
        FROM [InfoRequestReply] AS [i3]
        WHERE [i4].[Id] = [i3].[InfoRequestId]) AS [c3], [i4].[ProductId]
    FROM [InfoRequest] AS [i4]
    LEFT JOIN [tUser] AS [t0] ON [i4].[UserId] = [t0].[Id]
) AS [t1] ON [p].[Id] = [t1].[ProductId]
WHERE [p].[Id] = 39
ORDER BY [p].[Id], [b].[Id], [t].[ProductId], [t].[CategoryId], [t].[Id], [t1].[c3] DESC, [t1].[Id]