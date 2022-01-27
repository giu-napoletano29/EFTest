SELECT p.Id, p.Name, b.BrandName, c.Name, i.Id AS InforeqId, MAX(ir.InsertDate) AS Last_date
FROM Product p
INNER JOIN Brand b ON p.BrandId = b.Id
INNER JOIN Product_Category pc ON p.Id = pc.ProductId
INNER JOIN Category c ON pc.CategoryId = c.Id
LEFT JOIN InfoRequest i ON i.ProductId = p.Id
INNER JOIN InfoRequestReply ir ON i.Id = ir.InfoRequestId
WHERE p.Id = 39
GROUP BY p.Id, p.Name, b.BrandName, c.Name, i.Id
ORDER BY MAX(ir.InsertDate) DESC
