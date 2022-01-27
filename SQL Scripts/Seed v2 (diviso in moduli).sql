USE sqltest
SET NOCOUNT ON;
BEGIN TRANSACTION
--DROP CONSTRAINT

ALTER TABLE Brand DROP CONSTRAINT Brand_AccountId;
ALTER TABLE tUser DROP CONSTRAINT User_AccountId;
ALTER TABLE Product DROP CONSTRAINT Product_BrandId;
ALTER TABLE Product_Category DROP CONSTRAINT ProductCategory_ProductId, ProductCategory_CategoryId;
ALTER TABLE InfoRequest DROP CONSTRAINT InfoRequest_UserId, InfoRequest_ProductId, InfoRequest_NationId, chk_phone;
ALTER TABLE InfoRequestReply DROP CONSTRAINT InfoRequestReply_InfoRequestId, InfoRequestReply_AccountId;

--DELETE

TRUNCATE TABLE Account;
DBCC CHECKIDENT ('Account', RESEED, 1);
TRUNCATE TABLE Brand;
DBCC CHECKIDENT ('Brand', RESEED, 1);
TRUNCATE TABLE tUser;
DBCC CHECKIDENT ('tUser', RESEED, 1);
TRUNCATE TABLE Product;
DBCC CHECKIDENT ('Product', RESEED, 1);
TRUNCATE TABLE Category;
DBCC CHECKIDENT ('Category', RESEED, 1);
TRUNCATE TABLE Product_Category;
TRUNCATE TABLE InfoRequest;
DBCC CHECKIDENT ('InfoRequest', RESEED, 1);
TRUNCATE TABLE InfoRequestReply;
DBCC CHECKIDENT ('InfoRequestReply', RESEED, 1);
TRUNCATE TABLE Nation;
DBCC CHECKIDENT ('Nation', RESEED, 1);

--ADD CONSTRAINT

ALTER TABLE Brand ADD CONSTRAINT Brand_AccountId FOREIGN KEY (AccountId) REFERENCES Account(Id);
ALTER TABLE tUser ADD CONSTRAINT User_AccountId FOREIGN KEY (AccountId) REFERENCES Account(Id);
ALTER TABLE Product ADD CONSTRAINT Product_BrandId FOREIGN KEY (BrandId) REFERENCES Brand(Id);
ALTER TABLE Product_Category ADD CONSTRAINT ProductCategory_ProductId FOREIGN KEY (ProductId) REFERENCES Product(Id),
								CONSTRAINT ProductCategory_CategoryId FOREIGN KEY (CategoryId) REFERENCES Category(Id);
ALTER TABLE InfoRequest ADD CONSTRAINT InfoRequest_UserId FOREIGN KEY (UserId) REFERENCES tUser(Id),
							CONSTRAINT InfoRequest_ProductId FOREIGN KEY (ProductId) REFERENCES Product(Id),
							CONSTRAINT InfoRequest_NationId FOREIGN KEY (NationId) REFERENCES Nation(Id),
							CONSTRAINT chk_phone CHECK (Phone not like '%[^0-9]%');
ALTER TABLE InfoRequestReply ADD CONSTRAINT InfoRequestReply_InfoRequestId FOREIGN KEY (InfoRequestId) REFERENCES InfoRequest(Id),
								CONSTRAINT InfoRequestReply_AccountId FOREIGN KEY (AccountId) REFERENCES Account(Id);

--SEEDING

DECLARE @CatNumber INT = 20;
DECLARE @UsersNumber INT = 50;
DECLARE @BrandsNumber INT = 50;
DECLARE @MaxProd INT = 50;
DECLARE @MinProd INT = 10;
DECLARE @MaxReqInfo INT = 10;
DECLARE @MaxReqInfoReply INT = 3;
DECLARE @MinReqInfoReply INT = 1;
DECLARE @RandProductsNumber INT = (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (50-10+1)+10) );
DECLARE @RandProductCategoryNumber INT = (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (5+1)) );
DECLARE @RandInfoRequest INT = (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (10+1)) );

DECLARE @Counter INT;
DECLARE @randomString VARCHAR(15);
DECLARE @randomName VARCHAR(20);
DECLARE @RandUser INT;
DECLARE @CounterT INT;
DECLARE @c INT;



--Insert Nations
INSERT INTO Nation VALUES ('Italy');
INSERT INTO Nation VALUES ('Spain');
INSERT INTO Nation VALUES ('Portugal');
INSERT INTO Nation VALUES ('Germany');
INSERT INTO Nation VALUES ('France');
INSERT INTO Nation VALUES ('United Kingdom');
INSERT INTO Nation VALUES ('United States');
INSERT INTO Nation VALUES ('Brasil');
INSERT INTO Nation VALUES ('Canada');



-- Insert Categories
SET @Counter=1;
WHILE( @Counter <= @CatNumber)
BEGIN
	SELECT @randomName = CONVERT(varchar(255), NEWID());
    INSERT INTO Category VALUES ('CatName_'+@randomName);
	SET @Counter  = @Counter  + 1;
END

--Insert Users
SET @Counter=1;
WHILE ( @Counter <= @UsersNumber)
BEGIN
	SELECT @randomString = CONVERT(varchar(255), NEWID());
	SELECT @randomName = CONVERT(varchar(255), NEWID());
    INSERT INTO Account VALUES (@randomString+'@email.it', HASHBYTES('SHA2_512', 'password'),2);
	INSERT INTO tUser VALUES ((SELECT SCOPE_IDENTITY()), 'Name_'+@randomName, 'Lastname_'+@randomName);
    SET @Counter  = @Counter  + 1;
END

--Insert Brands
SET @Counter=1;
WHILE ( @Counter <= @BrandsNumber)
BEGIN
	SELECT @randomString = CONVERT(varchar(255), NEWID());
	SELECT @randomName = CONVERT(varchar(255), NEWID());
    INSERT INTO Account VALUES (@randomString+'@email.it', HASHBYTES('SHA2_512', 'password'),1);
	INSERT INTO Brand VALUES ((SELECT SCOPE_IDENTITY()), 'Brandname_'+@randomName, 'Lorm ipsum ' + @randomName + ' sosi listindisti');
	SET @Counter  = @Counter  + 1;
END

--Insert Products

--SET @Counter=1;
--WHILE ( @Counter <= @RandProductsNumber)
--BEGIN
	--SELECT @randomName = CONVERT(varchar(255), NEWID());
	--INSERT INTO Product VALUES (, 'ProdName_'+@randomName, 'Lorm ipsum ' + @randomName + ' sosi listindisti', ROUND(RAND(CHECKSUM(NEWID())) * (100), 2) ,'Lorm ipsum Long ' + @randomName + ' sosi listindisti che asfjldsj l adslglasdg lasdlgasdl')
	--INSERT INTO Product SELECT Id, 'ProdName_'+CONVERT(varchar(255), NEWID()), 'Lorm ipsum ' + CONVERT(varchar(255), NEWID()) + ' sosi listindisti', ROUND(RAND(CHECKSUM(NEWID())) * (100), 2) ,'Lorm ipsum Long ' + CONVERT(varchar(255), NEWID()) + ' sosi listindisti che asfjldsj l adslglasdg lasdlgasdl' FROM Brand;
	--SET @Counter  = @Counter  + 1;
--END

--SET @CounterT = 1;
--SET @c = (Select Count(*) From Brand);
--WHILE ( @CounterT <= @c )
--BEGIN
--	SET @Counter=1;
--	SET @RandProductsNumber = (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (50-10+1)+10) );
--	WHILE ( @Counter <= @RandProductsNumber)
--	BEGIN
--		SELECT @randomName = CONVERT(varchar(255), NEWID());
--		INSERT INTO Product VALUES ((SELECT Id FROM Brand WHERE Id = @CounterT), 'ProdName_'+CONVERT(varchar(255), NEWID()), 'Lorm ipsum ' + CONVERT(varchar(255), NEWID()) + ' sosi listindisti', ROUND(RAND(CHECKSUM(NEWID())) * (100), 2) ,'Lorm ipsum Long ' + CONVERT(varchar(255), NEWID()) + ' sosi listindisti che asfjldsj l adslglasdg lasdlgasdl')
--		--INSERT INTO Product SELECT Id, 'ProdName_'+CONVERT(varchar(255), NEWID()), 'Lorm ipsum ' + CONVERT(varchar(255), NEWID()) + ' sosi listindisti', ROUND(RAND(CHECKSUM(NEWID())) * (100), 2) ,'Lorm ipsum Long ' + CONVERT(varchar(255), NEWID()) + ' sosi listindisti che asfjldsj l adslglasdg lasdlgasdl' FROM Brand;
--		SET @Counter  = @Counter  + 1;
--	END
--	SET @CounterT  = @CounterT  + 1;
--END

DECLARE @brandId INT;
DECLARE brand_cursor CURSOR FAST_FORWARD
FOR SELECT Id FROM Brand;

OPEN brand_cursor;
FETCH NEXT FROM brand_cursor INTO @brandId;
WHILE @@FETCH_STATUS = 0
BEGIN
	SET @Counter=1;
	SET @RandProductsNumber = (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (@MaxProd-@MinProd+1)+@MinProd) );
	WHILE ( @Counter <= @RandProductsNumber)
	BEGIN
		SELECT @randomName = CONVERT(varchar(255), NEWID());
		INSERT INTO Product VALUES (@brandId, 'ProdName_'+CONVERT(varchar(255), NEWID()), 'Lorm ipsum ' + CONVERT(varchar(255), NEWID()) + ' sosi listindisti', ROUND(RAND(CHECKSUM(NEWID())) * (100), 2) ,'Lorm ipsum Long ' + CONVERT(varchar(255), NEWID()) + ' sosi listindisti che asfjldsj l adslglasdg lasdlgasdl');
		SET @Counter  = @Counter  + 1;
	END
	FETCH NEXT FROM brand_cursor INTO @brandId;
END
CLOSE brand_cursor;
DEALLOCATE brand_cursor;




--Insert Product_Category


--SET @CounterT=1;
--SET @c = (Select Count(*) From Product);
--WHILE ( @CounterT <= @c )
--BEGIN
--	SET @Counter=1;
--	SET @RandProductCategoryNumber = (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (5+1)) );
--	WHILE ( @Counter <= @RandProductCategoryNumber )
--	BEGIN
--		--INSERT INTO Product_Category VALUES (@PrevIdentity, (SELECT TOP 1 Id FROM Category ORDER BY NEWID()));
--		--INSERT INTO Product_Category VALUES ((SELECT TOP (@CounterT) Id FROM Product), (SELECT TOP 1 Id FROM Category ORDER BY NEWID()))	
--		INSERT INTO Product_Category VALUES ((SELECT Id FROM Product WHERE Id = @CounterT), (SELECT TOP 1 Id FROM (SELECT Id FROM Category
--																							EXCEPT
--																							SELECT c.Id FROM Category c, Product_Category pc WHERE pc.CategoryId = c.ID AND pc.ProductId = @CounterT) r ORDER BY NEWID()))
--		SET @Counter  = @Counter  + 1;
--	END
--	SET @CounterT  = @CounterT  + 1;
--END

DECLARE @ProductId INT;
DECLARE Productt_cursor CURSOR FAST_FORWARD
FOR SELECT Id FROM Product;

OPEN Productt_cursor;
FETCH NEXT FROM Productt_cursor INTO @ProductId;
WHILE @@FETCH_STATUS = 0
BEGIN
	SET @Counter=1;
	SET @RandProductCategoryNumber = (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (5+1)) );
	WHILE ( @Counter <= @RandProductCategoryNumber )
	BEGIN
		INSERT INTO Product_Category VALUES (@ProductId, (SELECT TOP 1 Id FROM (SELECT Id FROM Category
																							EXCEPT
																							SELECT c.Id FROM Category c, Product_Category pc WHERE pc.CategoryId = c.ID AND pc.ProductId = @ProductId) r ORDER BY NEWID()))
		SET @Counter  = @Counter  + 1;
	END
	FETCH NEXT FROM Productt_cursor INTO @ProductId;
END

CLOSE Productt_cursor;

--Insert InfoRequest

--SET @CounterT = 1;
--WHILE ( @CounterT <= @c )
--BEGIN
--	SET @Counter = 1;
--	SET @RandInfoRequest = (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (10+1)) );
--	WHILE ( @Counter <= @RandInfoRequest )
--	BEGIN
--		SET @RandUser = (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (3+1)) )
--		IF @RandUser > 0
--			BEGIN
--				SET @RandUser = (SELECT TOP 1 Id FROM tUser ORDER BY NEWID());
--				INSERT INTO InfoRequest VALUES (@RandUser, (SELECT Id FROM Product WHERE Id = @CounterT), (SELECT Name FROM tUser WHERE Id = @RandUser), (SELECT LastName FROM tUser WHERE Id = @RandUser), (SELECT Account.Email FROM Account INNER JOIN tUser ON tUser.AccountId = Account.Id WHERE tUser.AccountId = @RandUser), null, (SELECT TOP 1 Id FROM Nation ORDER BY NEWID()), null, null, 'testo richiesta', DATEADD(day, (ABS(CHECKSUM(NEWID())) % 65530), 0));
--			END
--		ELSE
--			BEGIN
--				SELECT @randomName = CONVERT(varchar(255), NEWID());
--				INSERT INTO InfoRequest VALUES (NULL, (SELECT Id FROM Product WHERE Id = @CounterT), 'NameRnd_'+@randomName, 'LastnameRnd_'+@randomName, null, null, (SELECT TOP 1 Id FROM Nation ORDER BY NEWID()), null, null, 'testo richiesta', DATEADD(day, (ABS(CHECKSUM(NEWID())) % 65530), 0));
--			END
	
--		SET @Counter  = @Counter  + 1;
--	END
--	SET @CounterT  = @CounterT  + 1;
--END

OPEN Productt_cursor;
FETCH NEXT FROM Productt_cursor INTO @ProductId;
WHILE @@FETCH_STATUS = 0
BEGIN
	SET @Counter = 1;
	SET @RandInfoRequest = (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (@MaxReqInfo+1)) );
	WHILE ( @Counter <= @RandInfoRequest )
	BEGIN
		SET @RandUser = (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (3+1)) )
		IF @RandUser > 0
			BEGIN
				SET @RandUser = (SELECT TOP 1 Id FROM tUser ORDER BY NEWID());
				INSERT INTO InfoRequest VALUES (@RandUser, @ProductId, (SELECT Name FROM tUser WHERE Id = @RandUser), (SELECT LastName FROM tUser WHERE Id = @RandUser), (SELECT Account.Email FROM Account INNER JOIN tUser ON tUser.AccountId = Account.Id WHERE tUser.AccountId = @RandUser), null, (SELECT TOP 1 Id FROM Nation ORDER BY NEWID()), null, null, 'testo richiesta', DATEADD(day, (ABS(CHECKSUM(NEWID())) % 65530), 0));
			END
		ELSE
			BEGIN
				SELECT @randomName = CONVERT(varchar(255), NEWID());
				INSERT INTO InfoRequest VALUES (NULL, @ProductId, 'NameRnd_'+@randomName, 'LastnameRnd_'+@randomName, null, null, (SELECT TOP 1 Id FROM Nation ORDER BY NEWID()), null, null, 'testo richiesta', DATEADD(day, (ABS(CHECKSUM(NEWID())) % 65530), 0));
			END
	
		SET @Counter  = @Counter  + 1;
	END
	FETCH NEXT FROM Productt_cursor INTO @ProductId;
END
CLOSE Productt_cursor;
DEALLOCATE Productt_cursor;


--Insert InfoRequestReply


--SET @c = (Select Count(Id) From InfoRequest);
--SET @CounterT = 1;
--WHILE ( @CounterT <= @c )
--BEGIN
--	SET @Counter = 1;
--	WHILE ( @Counter <= (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (3-1+1)+1) ))
--	BEGIN
--		INSERT INTO InfoRequestReply VALUES ((SELECT Id FROM InfoRequest WHERE Id = @CounterT), (SELECT TOP 1 r.Id FROM (SELECT DISTINCT a.Id FROM Account a, InfoRequest i, Product p, Brand b
--														WHERE i.Id = @CounterT AND i.ProductId = p.Id AND p.BrandId = b.Id AND b.AccountId = a.Id
--														UNION
--														SELECT a.Id
--														FROM Account a, InfoRequest i, Product p, tUser t
--														WHERE i.Id = (SELECT Id FROM InfoRequest WHERE Id = @CounterT) AND i.UserId = t.Id AND t.AccountId = a.Id) r
--													ORDER BY NEWID()), 'Reply text', DATEADD(day, (ABS(CHECKSUM(NEWID())) % 65530), 0));

--		SET @Counter  = @Counter  + 1;
--	END
--	SET @CounterT  = @CounterT  + 1;
--END

DECLARE @InfoRequestId INT;
DECLARE InfoRequest_cursor CURSOR FAST_FORWARD
FOR SELECT Id FROM InfoRequest;

OPEN InfoRequest_cursor;
FETCH NEXT FROM InfoRequest_cursor INTO @InfoRequestId;
WHILE @@FETCH_STATUS = 0
BEGIN
	SET @Counter = 1;
	WHILE ( @Counter <= (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (@MaxReqInfoReply-@MinReqInfoReply+1)+@MinReqInfoReply) ))
	BEGIN
		INSERT INTO InfoRequestReply VALUES (@InfoRequestId, (SELECT TOP 1 r.Id FROM (SELECT DISTINCT a.Id FROM Account a, InfoRequest i, Product p, Brand b
														WHERE i.Id = @InfoRequestId AND i.ProductId = p.Id AND p.BrandId = b.Id AND b.AccountId = a.Id
														UNION
														SELECT a.Id
														FROM Account a, InfoRequest i, Product p, tUser t
														WHERE i.Id = @InfoRequestId AND i.UserId = t.Id AND t.AccountId = a.Id) r
													ORDER BY NEWID()), 'Reply text', DATEADD(day, (ABS(CHECKSUM(NEWID())) % 65530), 0));

		SET @Counter  = @Counter  + 1;
	END
	FETCH NEXT FROM InfoRequest_cursor INTO @InfoRequestId;
END
CLOSE InfoRequest_cursor;
DEALLOCATE InfoRequest_cursor;

COMMIT TRANSACTION
SET NOCOUNT OFF;