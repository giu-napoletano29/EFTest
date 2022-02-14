UPDATE Product
SET IsDeleted = 0;

UPDATE Account
SET IsDeleted = 0;

UPDATE Brand
SET IsDeleted = 0;

UPDATE InfoRequest
SET IsDeleted = 0;

UPDATE InfoRequestReply
SET IsDeleted = 0;


--ALTER TABLE Product
--ADD CONSTRAINT df_Product
--DEFAULT 0 FOR IsDeleted;

--ALTER TABLE Account
--ADD CONSTRAINT df_Account
--DEFAULT 0 FOR IsDeleted;

--ALTER TABLE Brand
--ADD CONSTRAINT df_Brand
--DEFAULT 0 FOR IsDeleted;

--ALTER TABLE InfoRequest
--ADD CONSTRAINT df_InfoRequest
--DEFAULT 0 FOR IsDeleted;

--ALTER TABLE InfoRequestReply
--ADD CONSTRAINT df_InfoRequestReply
--DEFAULT 0 FOR IsDeleted;



