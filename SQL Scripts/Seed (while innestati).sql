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

DECLARE @Counter INT;
DECLARE @randomString VARCHAR(15);
DECLARE @randomName VARCHAR(20);
DECLARE @CounterBrandProd INT;
DECLARE @CountProd INT;
DECLARE @CountReq INT;
DECLARE @PrevIdentity INT;
DECLARE @BrandIdentity INT;
DECLARE @ReqIdentity INT;
DECLARE @RandUser INT;
DECLARE @RandP INT;

DECLARE @AccountNumber INT = 50;
DECLARE @CategoryNumber INT = 20;
DECLARE @MaxProd INT = 50;
DECLARE @MinProd INT = 10;
DECLARE @MaxReqInfo INT = 10;
DECLARE @MaxReqInfoReply INT = 3;
DECLARE @MinReqInfoReply INT = 1;

SET @CountProd=1;
SET @CounterBrandProd=1;
SET @Counter=1;
SET @CountReq=1;

INSERT INTO Nation VALUES ('Italy');
INSERT INTO Nation VALUES ('Spain');
INSERT INTO Nation VALUES ('Portugal');
INSERT INTO Nation VALUES ('Germany');
INSERT INTO Nation VALUES ('France');
INSERT INTO Nation VALUES ('United Kingdom');
INSERT INTO Nation VALUES ('United States');
INSERT INTO Nation VALUES ('Brasil');
INSERT INTO Nation VALUES ('Canada');




WHILE( @Counter <= @CategoryNumber)
BEGIN
	SELECT @randomName = CONVERT(varchar(255), NEWID());
    INSERT INTO Category VALUES ('CatName_'+@randomName);
	SET @Counter  = @Counter  + 1;
END

SET @Counter=1;
WHILE ( @Counter <= @AccountNumber)
BEGIN
	SELECT @randomString = CONVERT(varchar(255), NEWID());
	SELECT @randomName = CONVERT(varchar(255), NEWID());
    INSERT INTO Account VALUES (@randomString+'@email.it', HASHBYTES('SHA2_512', 'password'),2);
	INSERT INTO tUser VALUES ((SELECT SCOPE_IDENTITY()), 'Name_'+@randomName, 'Lastname_'+@randomName);
    SET @Counter  = @Counter  + 1;
END


SET @Counter=1;
WHILE ( @Counter <= @AccountNumber)
BEGIN
	SELECT @randomString = CONVERT(varchar(255), NEWID());
	SELECT @randomName = CONVERT(varchar(255), NEWID());
    INSERT INTO Account VALUES (@randomString+'@email.it', HASHBYTES('SHA2_512', 'password'),1);
	INSERT INTO Brand VALUES ((SELECT SCOPE_IDENTITY()), 'Brandname_'+@randomName, 'Lorm ipsum ' + @randomName + ' sosi listindisti');
	SET @BrandIdentity=(SELECT SCOPE_IDENTITY());

	SET @CounterBrandProd=1;
	SET @RandP = (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (@MaxProd-@MinProd+1)+@MinProd) )
	WHILE ( @CounterBrandProd <= @RandP)
	BEGIN

		SELECT @randomName = CONVERT(varchar(255), NEWID());
		INSERT INTO Product VALUES (@BrandIdentity, 'ProdName_'+@randomName, 'Lorm ipsum ' + @randomName + ' sosi listindisti', ROUND(RAND(CHECKSUM(NEWID())) * (100), 2) ,'Lorm ipsum Long ' + @randomName + ' sosi listindisti che asfjldsj l adslglasdg lasdlgasdl');
		SET @PrevIdentity=(SELECT SCOPE_IDENTITY());

		WHILE ( @CountProd <= (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (5+1)) ))
		BEGIN
			--BEGIN TRY
			--	INSERT INTO Product_Category VALUES (@PrevIdentity, (SELECT TOP 1 Id FROM Category ORDER BY NEWID()));
			--END TRY
			--BEGIN CATCH
			--	SELECT   
			--		ERROR_MESSAGE() AS ErrorMessage; 
			--END CATCH
			
			INSERT INTO Product_Category VALUES (@PrevIdentity, ( 
				SELECT TOP 1 Id FROM Category WHERE Id NOT IN (SELECT Id FROM Category c INNER JOIN Product_Category pc ON c.Id = pc.CategoryId AND pc.ProductId = @PrevIdentity) ORDER BY NEWID() )
			);
			
			SET @CountProd  = @CountProd  + 1;
		END
		SET @CountProd  = 1;

		
		
		SET @CountProd  = 1;
		WHILE ( @CountProd <= (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (@MaxReqInfo+1)) ))
		BEGIN
			SET @RandUser = (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (3+1)) )
			IF @RandUser > 0
				BEGIN
					SET @RandUser = (SELECT TOP 1 Id FROM tUser ORDER BY NEWID());
					INSERT INTO InfoRequest VALUES (@RandUser, @PrevIdentity, (SELECT Name FROM tUser WHERE Id = @RandUser), (SELECT LastName FROM tUser WHERE Id = @RandUser), (SELECT Account.Email FROM Account INNER JOIN tUser ON tUser.AccountId = Account.Id WHERE tUser.AccountId = @RandUser), null, (SELECT TOP 1 Id FROM Nation ORDER BY NEWID()), null, null, 'testo richiesta', DATEADD(day, (ABS(CHECKSUM(NEWID())) % 65530), 0));
				END
			ELSE
				BEGIN
					SELECT @randomName = CONVERT(varchar(255), NEWID());
					SELECT @randomString = CONVERT(varchar(255), NEWID());
					INSERT INTO InfoRequest VALUES (NULL, @PrevIdentity, 'NameRnd_'+@randomName, 'LastnameRnd_'+@randomName, (@randomString+'@email.it'), null, (SELECT TOP 1 Id FROM Nation ORDER BY NEWID()), null, null, 'testo richiesta', DATEADD(day, (ABS(CHECKSUM(NEWID())) % 65530), 0));
				END


			SET @ReqIdentity=(SELECT SCOPE_IDENTITY());

			SET @CountReq  = 1;
			WHILE ( @CountReq <= (SELECT FLOOR(RAND(CHECKSUM(NEWID())) * (@MaxReqInfoReply-@MinReqInfoReply+1)+@MinReqInfoReply) ))
			BEGIN
				INSERT INTO InfoRequestReply VALUES (@ReqIdentity, (SELECT TOP 1 r.Id FROM (SELECT DISTINCT a.Id FROM Account a, InfoRequest i, Product p, Brand b
													WHERE i.Id = @ReqIdentity AND i.ProductId = p.Id AND p.BrandId = b.Id AND b.AccountId = a.Id
													UNION
													SELECT a.Id
													FROM Account a, InfoRequest i, Product p, tUser t
													WHERE i.Id = @ReqIdentity AND i.UserId = t.Id AND t.AccountId = a.Id) r
												ORDER BY NEWID()), 'Reply text', DATEADD(day, (ABS(CHECKSUM(NEWID())) % 65530), 0));

			SET @CountReq  = @CountReq  + 1;
			END

			SET @CountProd  = @CountProd  + 1;
		END
		


		SET @CounterBrandProd  = @CounterBrandProd  + 1;
	END
    SET @Counter  = @Counter  + 1;
END
--ROLLBACK TRANSACTION
COMMIT TRANSACTION
SET NOCOUNT OFF;
