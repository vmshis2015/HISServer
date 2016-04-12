--  
-- Script to Update dbo.fLaytendichvuthanhtoan in KKYBBNL-PC\SQL2K8.BN160408 
-- Generated Friday, April 8, 2016, at 11:01 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.BN160408 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.fLaytendichvuthanhtoan Function'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('ALTER FUNCTION [dbo].[fLaytendichvuthanhtoan](@PaymentTypeID [tinyint])
RETURNS [nvarchar](100)   
AS 
BEGIN
	DECLARE @PayementType_Name NVARCHAR(100)
	SET @PayementType_Name=(
						CASE WHEN @PaymentTypeID=1 THEN N''Thong tin kham ch?a b?nh''
						     WHEN @PaymentTypeID=2 THEN N''Thong tin d?ch v? CLS''
						     WHEN @PaymentTypeID=3 THEN N''Thong tin thu?c''
						    WHEN @PaymentTypeID=4 THEN N''Thong tin giu?ng b?nh''
							WHEN @PaymentTypeID=5 THEN N''V?t tu tieu hao''
							WHEN @PaymentTypeID=6 THEN N''Thong tin ti?n t?m ?ng''
							WHEN @PaymentTypeID=7 THEN N''Thong tin phi?u an''
						    WHEN @PaymentTypeID=8 THEN N''Thong tin goi d?ch v?''
						    WHEN @PaymentTypeID=9 THEN N''Thong tin chi phi them''
						    WHEN @PaymentTypeID=10 THEN N''Ti?n mua s? kham''
						     WHEN @PaymentTypeID=0 THEN N''Phi d?ch v? yeu c?u''
						     WHEN @PaymentTypeID=11 THEN N''Cong tiem ch?ng''
						     WHEN @PaymentTypeID=12 THEN N''Thong tin d?ch v? ki?m nghi?m''
						     end
	)
	RETURN @PayementType_Name
END
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.fLaytendichvuthanhtoan Function Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.fLaytendichvuthanhtoan Function'
END
GO


--  
-- Script to Create dbo.fLaytentrangthaicls in KKYBBNL-PC\SQL2K8.BN160408 
-- Generated Friday, April 8, 2016, at 11:01 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.BN160408 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.fLaytentrangthaicls Function'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('Create FUNCTION [dbo].[fLaytentrangthaicls](@trang_thai [tinyint])
RETURNS [nvarchar](100)   
AS 
BEGIN
	DECLARE @ten_trangthai NVARCHAR(100)
	--0=M?i ch? d?nh;1=Da chuy?n CLS;2=Dang th?c hi?n;3= Da co k?t qu? CLS;4=Da xac nh?n k?t qu?
	SET @ten_trangthai=(
						CASE WHEN @trang_thai=0 THEN N''Chua ban giao''
						     WHEN @trang_thai=1 THEN N''Da ban giao''
						     WHEN @trang_thai=2 THEN N''Dang nh?p k?t qu?''
						     WHEN @trang_thai=3 THEN N''Da co k?t qu?''
						    WHEN @trang_thai=4 THEN N''Da xac nh?n k?t qu?''
							WHEN @trang_thai=5 THEN N''Da in k?t qu?''
						     end
	)
	RETURN @ten_trangthai
END
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.fLaytentrangthaicls Function Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.fLaytentrangthaicls Function'
END
GO
