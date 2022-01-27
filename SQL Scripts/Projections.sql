USE sqltest

--Numero richieste informazioni raccolte per prodotto
SELECT p.Name,COUNT(i.Id) AS Number_Requests
FROM InfoRequest i
LEFT JOIN Product p ON p.Id = i.ProductId
GROUP BY p.Id, p.Name
ORDER BY p.Name;

--Numero delle richieste informazioni raccolte per Brand
--TO REMEMBER: check Group by name try to switch into Id (Supposing name are unique)
SELECT b.BrandName, COUNT(i.Id) AS Number_Requests
FROM InfoRequest i, Brand b 
INNER JOIN Product p ON p.BrandId = b.Id
WHERE p.Id = i.ProductId
GROUP BY b.BrandName;

--Numero delle richieste informazioni raccolte per Prodotto riportando anche il nome del Brand
SELECT b.BrandName, p.Name, COUNT(i.Id) AS Number_Requests
FROM InfoRequest i, Brand b
INNER JOIN Product p ON p.BrandId = b.Id
WHERE p.Id = i.ProductId
GROUP BY p.Name, b.BrandName;

--Numero dei prodotti per Categoria di ciascun Brand
SELECT b.BrandName, c.Name, COUNT(p.Id) AS Number_Products
FROM Brand b
INNER JOIN Product p ON p.BrandId = b.Id
INNER JOIN Product_Category pc ON pc.ProductId = p.Id
INNER JOIN Category c ON c.Id = pc.CategoryId
GROUP BY c.Name, b.BrandName;

--Elenco dei prodotti con più di una categoria associata
--Same group by name here
SELECT p.Name, COUNT(pc.CategoryId) AS Number_Categories
FROM Product p
INNER JOIN Product_Category pc ON p.Id = pc.ProductId
GROUP BY p.Name
HAVING COUNT(pc.CategoryId)>1;

--Elenco dei prodotti con nessuna categoria associata
SELECT p.Name
FROM Product p
EXCEPT
SELECT p.Name
FROM Product p
INNER JOIN Product_Category pc ON p.Id = pc.ProductId
INNER JOIN Category c ON pc.CategoryId = c.Id;

--Numero dei prodotti per Brand, ordinata per numero dei prodotti decrescente
--Same group by name here
SELECT b.BrandName, COUNT(p.Id) AS Number_Products
FROM Product p
INNER JOIN Brand b ON b.Id = p.BrandId
GROUP BY b.BrandName
ORDER BY Number_Products DESC;

--Prezzo medio dei prodotti per Brand, ordinata del prezzo medio più alto al più basso
SELECT b.BrandName, AVG(p.Price) AS Average_Prices
FROM Product p
INNER JOIN Brand b ON b.Id = p.BrandId
GROUP BY b.BrandName
ORDER BY Average_Prices DESC;

--Il prodotto più caro di ciascun Brand con il rispettivo nome prodotto
SELECT r.Id, p.Name, r.prc AS Max_Price
FROM Product p
INNER JOIN 
(SELECT b.Id, MAX(p.Price) AS prc
FROM Product p
INNER JOIN Brand b ON b.Id = p.BrandId
GROUP BY b.Id) r ON p.BrandId = r.Id AND p.Price = r.prc;

--Il prodotto con il prezzo più alto e il prodotto con il prezzo più basso per ciascun Brand con i rispettivi nomi prodotto
----- Not working
--SELECT DISTINCT r.BrandName, p1.Name, p1.Price, p2.Name, p2.Price
--FROM Product p2
--INNER JOIN (SELECT DISTINCT b.BrandName, MIN(p.Price) AS Prezzo_minimo, MAX(p.Price) AS Prezzo_massimo
--FROM Product p1, Product p2, Brand b
--INNER JOIN Product p ON p.BrandId = b.Id
--GROUP BY b.BrandName) AS r ON p2.Price = r.Prezzo_massimo
--INNER JOIN Product p1 ON r.Prezzo_minimo = p1.Price
--ORDER BY p2.price DESC;
------

-- Can be improved
SELECT Max_.BrandName, Max_.Name, Max_.Price, Min_.Name, Min_.Price
FROM
(SELECT b.BrandName, b.Id, p.Name, p.Price
FROM Brand b
INNER JOIN Product p ON p.BrandId = b.Id
WHERE p.Price = (SELECT MIN(Price) FROM Product p WHERE p.BrandId = b.Id)) AS Min_
JOIN
(SELECT b.BrandName, b.Id, p.Name, p.Price
FROM Brand b
INNER JOIN Product p ON p.BrandId = b.Id
WHERE p.Price = (SELECT MAX(Price) FROM Product p WHERE p.BrandId = b.Id)) AS Max_
ON Max_.Id = Min_.Id
ORDER BY Max_.Price;

--Data una richiesta informazioni, elencare la cronologia delle risposte riportando i seguenti dati
SELECT DISTINCT CASE WHEN a.AccountType = 1 THEN b.BrandName ELSE t.Name + ' ' + t.LastName END AS User_, ir.ReplyText, ir.InsertDate
FROM Brand b, tUser t
INNER JOIN InfoRequestReply ir ON ir.InfoRequestId = (SELECT TOP 1 Id FROM InfoRequest ORDER BY NEWID())
INNER JOIN Account a ON ir.AccountId = a.Id
WHERE a.Id = CASE WHEN a.AccountType = 1 THEN b.AccountId ELSE t.AccountId END;



--Dato un brand, recuperare l’elenco delle richieste informazioni per prodotto ordinate per ultima risposta più recente
--Id InfoRequest | IdProdotto | Nome Prodotto | Nome Richiedente | Cognome Richiedente | Testo richiesta | Data ultima risposta 
SELECT i.Id, p.Id, p.Name, i.Name, i.Lastname, i.RequestText, MAX(ir.InsertDate) AS Last_Reply
FROM tUser t
INNER JOIN Product p ON p.BrandId = (SELECT TOP 1 Id FROM Brand ORDER BY NEWID())
INNER JOIN InfoRequest i ON p.Id = i.ProductId
INNER JOIN InfoRequestReply ir ON i.Id = ir.InfoRequestId
GROUP BY p.Id, i.Id, p.Name, i.Name, i.Lastname, i.RequestText
ORDER BY Last_Reply DESC;

--I primi tre più costosi del brand ordinate per brand e prezzo

--SELECT r.Id, r.BrandName, p.Name, p.Price AS Max_Price
--FROM Product p
--INNER JOIN 
--(SELECT b.Id, b.Brandname, MAX(p.Price) AS prc
--FROM Product p
--INNER JOIN Brand b ON b.Id = p.BrandId
--GROUP BY b.Id, b.BrandName) r ON p.BrandId = r.Id
--ORDER BY r.BrandName, Price DESC;

WITH cte AS
  ( SELECT b.Id, b.BrandName, p.Price, ROW_NUMBER() OVER (PARTITION BY b.BrandName ORDER BY p.Price DESC) AS rn
    FROM Product p
	INNER JOIN Brand b ON b.Id = p.BrandId
  )
SELECT BrandName, Price
FROM cte
WHERE rn <= 3
ORDER BY BrandName, Price DESC;

SELECT b.BrandName, p.Price
FROM Brand b
CROSS APPLY
(
	SELECT TOP 3 Name, Price
	FROM Product
	WHERE BrandId = b.Id
	ORDER BY Price DESC
) p
ORDER BY b.BrandName




-- prendere tutti i prodotti senza categoria e associarne almeno una random
DECLARE @prodId INT;
DECLARE product_cursor CURSOR FAST_FORWARD
FOR SELECT Id 
	FROM Product
	WHERE Id NOT IN (SELECT DISTINCT ProductId FROM Product_Category);

OPEN product_cursor;
FETCH NEXT FROM product_cursor INTO @prodId;
WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO Product_Category VALUES (@prodId, (SELECT TOP 1 Id FROM Category ORDER BY NEWID()))
	FETCH NEXT FROM product_cursor INTO @prodId;
END
CLOSE product_cursor;
DEALLOCATE product_cursor;


--individuare gli utenti guest e registrarli al sito come account utenti

DECLARE @name VARCHAR(80);
DECLARE @lastname VARCHAR(80);
DECLARE @email VARCHAR(319);
DECLARE @id INT;
DECLARE @userId INT;
DECLARE inforeq_cursor CURSOR FAST_FORWARD
FOR SELECT Id, Name, Lastname, Email FROM InfoRequest WHERE UserId IS NULL

OPEN inforeq_cursor;
FETCH NEXT FROM inforeq_cursor INTO @id, @name, @lastname, @email;

WHILE @@FETCH_STATUS = 0
BEGIN
	IF NOT EXISTS(
		SELECT * FROM Account WHERE Email = @email
	)
	BEGIN
		INSERT INTO Account VALUES (@email, HASHBYTES('SHA2_512', 'password'),2);
		INSERT INTO tUser VALUES ((SELECT SCOPE_IDENTITY()), @name, @lastname);
		SET @userId = (SELECT SCOPE_IDENTITY())

		UPDATE InfoRequest SET UserId = @userId WHERE Id = @id OR Email = @email;
	END ---Guest con email già usata
	ELSE
		BEGIN
			SET @userId = (SELECT t.Id FROM tUser t INNER JOIN Account a ON t.AccountId = a.Id WHERE Email = @email)
			UPDATE InfoRequest SET UserId = @userId WHERE Email = @email
		END

	FETCH NEXT FROM inforeq_cursor INTO @id, @name, @lastname, @email;
END
CLOSE inforeq_cursor;
DEALLOCATE inforeq_cursor;




--Creazione di una Stored Procedure di paginazione Prodotti
USE sqltest;  
GO  
DROP PROCEDURE paginationProd
GO
CREATE PROCEDURE paginationProd   
    @PageSize int,   
    @PageNum int,
	@Category int,
	@orderby int
AS   
    SET NOCOUNT ON;
	WITH listProd AS (
		SELECT Id, Name, ROW_NUMBER() OVER (ORDER BY part) AS rownumber
		FROM (
			SELECT Id, Name, 1 AS part FROM
				(SELECT TOP 20 p.Id, p.Name
				FROM  Product p
				INNER JOIN Product_Category pc ON p.Id = pc.ProductId AND pc.CategoryId = @Category 
				ORDER BY CASE WHEN @orderby = 1 THEN p.Name END ,
						CASE WHEN @orderby = 2 THEN p.Price END DESC,
						CASE WHEN @orderby = 3 THEN p.Price END ASC,
						p.Id DESC
				) r
			GROUP BY Id, Name
			UNION ALL
			SELECT Id, Name, 2 AS part FROM
				(SELECT TOP 100 p.Id, p.Name
				FROM InfoRequest i
				INNER JOIN Product p ON i.ProductId = p.Id
				INNER JOIN Product_Category pc ON p.Id = pc.ProductId AND pc.CategoryId = @Category 
				GROUP BY p.Id, P.Name, p.Price
				ORDER BY COUNT(i.Id) DESC,
						CASE WHEN @orderby = 1 THEN p.Name END ,
						CASE WHEN @orderby = 2 THEN p.Price END DESC,
						CASE WHEN @orderby = 3 THEN p.Price END ASC
				) r
			GROUP BY Id, Name
			UNION ALL
			SELECT Id, Name, 3 AS part FROM
				(SELECT TOP 10 p.Id, p.Name
				FROM Product p
				INNER JOIN Product_Category pc ON p.Id = pc.ProductId AND pc.CategoryId = @Category 
				WHERE p.Price >= 200 AND p.Price <= 500
				ORDER BY CASE WHEN @orderby = 1 THEN p.Name END ,
						CASE WHEN @orderby = 2 THEN p.Price END DESC,
						CASE WHEN @orderby = 3 THEN p.Price END ASC,
						p.Id
				) r
			GROUP BY Id, Name
			UNION ALL
			SELECT Id, Name, 4 AS part FROM
				(SELECT TOP 100 p.Id, p.Name
				FROM Product p
				INNER JOIN Product_Category pc ON p.Id = pc.ProductId AND pc.CategoryId = @Category 
				WHERE p.Id NOT IN (SELECT i.ProductId FROM InfoRequest i)
				ORDER BY CASE WHEN @orderby = 1 THEN p.Name END ,
						CASE WHEN @orderby = 2 THEN p.Price END DESC,
						CASE WHEN @orderby = 3 THEN p.Price END ASC,
						p.Id
				) r
			GROUP BY Id, Name
		) res
	)
	SELECT Id, Name
	FROM listProd
	WHERE rownumber > ((@PageSize * @PageNum) - @PageSize) AND rownumber <= ((@PageSize * @PageNum))

GO

EXECUTE paginationProd @PageSize = 20, @PageNum = 1, @Category = 5, @orderby = 1;
GO  

--SELECT TOP 20 p.Id, p.Name
--FROM  Product p
--ORDER BY p.Id

--SELECT TOP 100 p.Id, p.Name
--FROM InfoRequest i
--INNER JOIN Product p ON i.ProductId = p.Id
--GROUP BY p.Id, P.Name
--ORDER BY COUNT(i.Id) DESC, p.Id

--SELECT TOP 10 p.Id, p.Name
--FROM Product p
--WHERE p.Price > 200 AND p.Price < 500
--ORDER BY p.Name

--SELECT TOP 100 p.Id, p.Name
--FROM Product p
--WHERE p.Id NOT IN (SELECT i.ProductId FROM InfoRequest i)
--ORDER BY p.Id





