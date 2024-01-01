﻿CREATE DATABASE QUANLYSPA
GO

USE QUANLYSPA
GO

CREATE TABLE EMPLOYEE
(
	EMP_ID INT IDENTITY(1,1) PRIMARY KEY,
	EMP_MA AS CAST('NV' + RIGHT('000' + CAST(EMP_ID AS VARCHAR(3)), 3) AS CHAR(6)) PERSISTED,
	EMP_DISPLAYNAME NVARCHAR(MAX) NOT NULL,
	EMP_ADDRESS NVARCHAR(MAX) NOT NULL,
	EMP_PHONE NVARCHAR(20) NOT NULL,
	EMP_CCCD NVARCHAR(12) NOT NULL,
	EMP_SALARY MONEY NOT NULL,
	EMP_ROLE NVARCHAR(50) NOT NULL
)
GO

CREATE TABLE CUSTOMER
(
	CUS_ID INT IDENTITY(1,1) PRIMARY KEY,
	CUS_MA AS CAST('KH' + RIGHT('000' + CAST(CUS_ID AS VARCHAR(3)), 3) AS CHAR(6)) PERSISTED,
	CUS_NAME NVARCHAR(MAX) NOT NULL,
	CUS_PHONE NVARCHAR(20) NOT NULL,
	CUS_EMAIL NVARCHAR(200) NOT NULL,
	CUS_GENDER NVARCHAR(5) NOT NULL
)
GO

CREATE TABLE PRODUCT
(
	PRO_ID INT IDENTITY(1,1) PRIMARY KEY,
	PRO_MA AS CAST('MP' + RIGHT('000' + CAST(PRO_ID AS VARCHAR(3)), 3) AS CHAR(6)) PERSISTED,
	PRO_NAME NVARCHAR(MAX) NOT NULL,
	PRO_IMG NVARCHAR(MAX) NOT NULL,
	PRO_URL NVARCHAR(MAX) NOT NULL,
	INSTOCK INT NOT NULL,
	PRICE_OUT MONEY DEFAULT 0 NOT NULL,
)
GO

CREATE TABLE SERVICESS
(
	SER_ID INT IDENTITY(1,1) PRIMARY KEY,
	SER_MA AS CAST('DV' + RIGHT('000' + CAST(SER_ID AS VARCHAR(3)), 3) AS CHAR(6)) PERSISTED,
	SER_NAME NVARCHAR(MAX) NOT NULL,
	PRICE MONEY NOT NULL
)
GO

CREATE TABLE BOOKING 
(
	BK_ID INT IDENTITY(1,1) PRIMARY KEY,
	BK_MA AS CAST('BK' + RIGHT('000' + CAST(BK_ID AS VARCHAR(3)), 3) AS CHAR(6)) PERSISTED,
	C_ID INT NOT NULL,
	E_ID INT NOT NULL,
	S_ID INT NOT NULL,
	START_TIME DATETIME2 NOT NULL,
	END_TIME DATETIME2 NOT NULL,
	IS_EDITED BIT,
	FOREIGN KEY (E_ID) REFERENCES EMPLOYEE(EMP_ID),
	FOREIGN KEY (S_ID) REFERENCES SERVICESS(SER_ID),
	FOREIGN KEY (C_ID) REFERENCES CUSTOMER(CUS_ID)
)
GO

CREATE TABLE PAYMENT
(
	PMT_ID INT IDENTITY(1,1) PRIMARY KEY,
	PMT_MA AS CAST('HD' + RIGHT('000' + CAST(PMT_ID AS VARCHAR(3)), 3) AS CHAR(6)) PERSISTED,
	C_ID INT NOT NULL,
	PRICE MONEY DEFAULT 0 NOT NULL,
	DAYTIME DATETIME DEFAULT GETDATE() NOT NULL,
	FOREIGN KEY (C_ID) REFERENCES CUSTOMER(CUS_ID) 
)

GO

CREATE TABLE PAYMENT_DETAIL_SERVICE
(
	PMT_ID INT NOT NULL,
	S_ID INT NOT NULL,
	QUANTITY INT NOT NULL,
	PRICE MONEY NOT NULL,
	PRIMARY KEY (PMT_ID, S_ID),
	FOREIGN KEY (PMT_ID) REFERENCES PAYMENT (PMT_ID),
	FOREIGN KEY (S_ID) REFERENCES SERVICESS (SER_ID)
)
GO

CREATE TABLE PAYMENT_DETAIL_PRODUCT
(
	PMT_ID INT NOT NULL,
	P_ID INT NOT NULL,
	QUANTITY INT NOT NULL,
	PRICE MONEY NOT NULL,
	PRIMARY KEY (PMT_ID, P_ID),
	FOREIGN KEY (PMT_ID) REFERENCES PAYMENT(PMT_ID),
	FOREIGN KEY (P_ID) REFERENCES PRODUCT(PRO_ID)
)
GO

CREATE TABLE RECEIPT
(
	REC_ID INT IDENTITY(1,1) PRIMARY KEY,
	REC_MA AS CAST('NH' + RIGHT('000' + CAST(REC_ID AS VARCHAR(3)), 3) AS CHAR(6)) PERSISTED,
	REC_DATE DATETIME NOT NULL,
	REC_PRICE MONEY NOT NULL,
)
GO

CREATE TABLE RECEIPT_DETAIL
(
	REC_ID INT NOT NULL,
	P_ID INT NOT NULL,
	QUANTITY INT NOT NULL,
	PRICE MONEY NOT NULL,
	PRIMARY KEY (REC_ID, P_ID),
	FOREIGN KEY (REC_ID) REFERENCES RECEIPT(REC_ID),
	FOREIGN KEY (P_ID) REFERENCES PRODUCT(PRO_ID),
)
GO

CREATE TABLE ACCOUNT
(
	A_USERNAME NVARCHAR(255) PRIMARY KEY,
	A_PASSWORD NVARCHAR(255) NOT NULL,
	A_EMAIL NVARCHAR(255) NOT NULL,
	A_DISPLAYNAME NVARCHAR(255) NOT NULL,
	A_GENDER NVARCHAR(5) NOT NULL,
	A_BDAY DATE NOT NULL,
	A_ADDRESS NVARCHAR(MAX) NOT NULL,
	A_PHONE NVARCHAR(20) NOT NULL,
)
GO

INSERT INTO ACCOUNT(A_USERNAME, A_PASSWORD, A_EMAIL, A_DISPLAYNAME, A_GENDER, A_BDAY, A_ADDRESS, A_PHONE) VALUES (N'admin', N'YWRtaW4=',N'phanchauhoang2004@gmail.com',N'Phan Châu Hoàng',N'Nam','2004-6-8',N'16 Nguyên Phi Ỷ Lan','0919277708')
GO

--CREATE TOP SALES
CREATE OR ALTER PROC TOP_SALES @TOP INT
AS
BEGIN
	SELECT TOP (@TOP) WITH TIES P.PRO_NAME AS N'Mỹ phẩm', A.ToTal AS N'SL'
	FROM PRODUCT P
	JOIN 
	(
		SELECT PT.P_ID, SUM(PT.QUANTITY) AS ToTal 
		FROM PAYMENT P
		JOIN PAYMENT_DETAIL_PRODUCT PT ON P.PMT_ID = PT.PMT_ID
		WHERE MONTH(P.DAYTIME) = MONTH(GETDATE())
		GROUP BY PT.P_ID
	) AS A
	ON P.PRO_ID = A.P_ID
	ORDER BY A.ToTal DESC
END
GO

--CREATE TOTALPRODUCT
CREATE OR ALTER PROC TotalProduct @m INT
AS
BEGIN
	SELECT SUM(PDP.QUANTITY)
	FROM PAYMENT P
	INNER JOIN PAYMENT_DETAIL_PRODUCT PDP ON P.PMT_ID = PDP.PMT_ID
	WHERE MONTH(P.DAYTIME) = @m 
END
GO	

--CREATE TOP SERVICE
CREATE OR ALTER PROC TOP_SERVICE @TOP INT
AS
BEGIN
	SELECT TOP (@TOP) WITH TIES  
		ROW_NUMBER() OVER (ORDER BY A.Total DESC) AS 'TOP', 
		S.SER_NAME AS N'Dịch Vụ'
	FROM SERVICESS S
	JOIN 
	(
		SELECT PT.S_ID, SUM(PT.QUANTITY) AS ToTal 
		FROM PAYMENT P
		JOIN PAYMENT_DETAIL_SERVICE PT ON P.PMT_ID = PT.PMT_ID
		WHERE MONTH(P.DAYTIME) = MONTH(GETDATE())
		GROUP BY PT.S_ID
	) AS A
	ON S.SER_ID = A.S_ID
	ORDER BY A.ToTal DESC
END



