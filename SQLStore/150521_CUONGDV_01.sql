--  
-- Script to Update dbo.dmuc_thuoc in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.dmuc_thuoc Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[dmuc_thuoc]
      ADD [co_chiathuoc] [tinyint] NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[dmuc_thuoc]
      ADD [dongia_chia] [decimal] (18, 2) NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[dmuc_thuoc]
      ADD [ma_dvichia] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[dmuc_thuoc]
      ADD [sluong_chia] [int] NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.dmuc_thuoc Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.dmuc_thuoc Table'
END
GO


--  
-- Script to Update dbo.t_biendong_thuoc in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.t_biendong_thuoc Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[t_biendong_thuoc]
      ADD [sluong_chia] [int] NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.t_biendong_thuoc Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.t_biendong_thuoc Table'
END
GO


--  
-- Script to Update dbo.t_phieu_nhapxuatthuoc_chitiet in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.t_phieu_nhapxuatthuoc_chitiet Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[t_phieu_nhapxuatthuoc_chitiet]
      ADD [sluong_chia] [int] NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.t_phieu_nhapxuatthuoc_chitiet Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.t_phieu_nhapxuatthuoc_chitiet Table'
END
GO


--  
-- Script to Update dbo.t_thuockho in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.t_thuockho Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[t_thuockho]
      ADD [chophep_ketutruc] [tinyint] NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.t_thuockho Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.t_thuockho Table'
END
GO


--  
-- Script to Update dbo.Baocao_Chidinhcls_Chitiet in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Baocao_Chidinhcls_Chitiet Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Baocao_Chidinhcls_Chitiet]
	@FromDate DATETIME,
	@ToDate DATETIME,
	@MaDoiTuong NVARCHAR(50),
	@Loaidichvu NVARCHAR(100),
	@CreateBy NVARCHAR(50),
	@MA_KHOA_THIEN NVARCHAR(50),
	@has_exam int
WITH RECOMPILE
AS
DECLARE @sfromdate NVARCHAR(10)
SET @sfromdate=CONVERT(NVARCHAR(10),@FromDate,103)
	SET @FromDate = dbo.trunc(@FromDate)
	SET @ToDate = dbo.trunc(@ToDate)
	BEGIN
		IF OBJECT_ID(''tempdb..#tien_khoa'') IS NOT NULL
		    DROP TABLE #tien_khoa
		
		
		
		SELECT THANH_TIEN,
		       PHU_THU,
		       so_luong,
		       ten_chitietdichvu,
		       username,
		       ten_nguoichidinh
		       INTO #tien_khoa
		       -- Ma_DV
		FROM   (
		           SELECT (ISNULL(tpd.don_gia, 0) * ISNULL(tpd.so_luong, 0)) AS 
		                  THANH_TIEN,
		                  tpd.so_luong,
		                  id_loaithanhtoan,
		                  tpd.ten_chitietdichvu,
		                  (ISNULL(tpd.phu_thu, 0) * ISNULL(tpd.so_luong, 0)) AS 
		                  PHU_THU,
		                  TP.ma_khoa_thuchien,
		                  isnull((
		                      SELECT TOP 1 [USER_NAME]
		                      FROM   dmuc_nhanvien dn
		                      WHERE  dn.id_nhanvien = tpd.id_bacsi_chidinh
		                  ),''ADMIN'') AS username,
		                 ISNULL( (
		                      SELECT TOP 1 dn.ten_nhanvien
		                      FROM   dmuc_nhanvien dn
		                      WHERE  dn.id_nhanvien = tpd.id_bacsi_chidinh
		                  ),N''Qu?n tr? h? th?ng'') AS ten_nguoichidinh
		           FROM   kcb_thanhtoan_chitiet tpd
		                  JOIN kcb_thanhtoan tp
		                       ON  tpd.id_thanhtoan = tp.id_thanhtoan
		           WHERE  tp.Kieu_ThanhToan = 0
		                  AND ISNULL(tpd.trangthai_huy, 0) = 0
		                  AND tpd.id_loaithanhtoan IN (2)--Ch? b?c ti?n kham va d?ch v? CLS
		                  AND (@sfromdate=''01/01/1900'' OR dbo.trunc(tp.ngay_thanhtoan) 
		                      BETWEEN @FromDate AND @ToDate)
		                  AND (
		                          @MA_KHOA_THIEN = ''-1''
		                          OR TP.ma_khoa_thuchien = @MA_KHOA_THIEN
		                      )
		                  AND (tpd.ma_doituong_kcb = @MaDoiTuong OR @MaDoiTuong = ''-1'')
		                  AND(@has_exam=-1 OR (@has_exam=0 AND isnull(id_kham,-1)=-1)
		                  OR (@has_exam=1 AND isnull(id_kham,-1)>0))
		                  AND (@Loaidichvu=''-1'' OR tpd.id_dichvu IN (SELECT *
		                                        FROM   dbo.fromStringintoIntTable(@Loaidichvu) 
		                                               fsit))
		       ) AS P
		
		
		--SELECT  * FROM #tien_khoa
		SELECT (SUM(THANH_TIEN) + SUM(PHU_THU)) AS THANH_TIEN,
		       SUM(so_luong) AS so_luong,
		       ten_chitietdichvu AS ten_dichvuchitiet,
		       ten_nguoichidinh
		FROM   #tien_khoa AS P
		WHERE  (P.username = @CreateBy OR @CreateBy = ''-1'')
		GROUP BY
		       ten_chitietdichvu,
		       ten_nguoichidinh
		ORDER BY
		       ten_nguoichidinh
	END
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Baocao_Chidinhcls_Chitiet Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Baocao_Chidinhcls_Chitiet Procedure'
END
GO


--  
-- Script to Update dbo.Baocao_Chidinhcls_Tonghop in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Baocao_Chidinhcls_Tonghop Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Baocao_Chidinhcls_Tonghop]
	@FromDate DATETIME,
	@ToDate DATETIME,
	@MaDoiTuong NVARCHAR(50),
	@CreateBy NVARCHAR(50),
	@Loaidichvu NVARCHAR(100),
	@MA_KHOA_THIEN NVARCHAR(50),
	@has_exam int
AS
DECLARE @sfromdate NVARCHAR(10)
SET @sfromdate=CONVERT(NVARCHAR(10),@FromDate,103)
	SET @FromDate = dbo.trunc(@FromDate)
	SET @ToDate = dbo.trunc(@ToDate)
	BEGIN
		SELECT (SUM(THANH_TIEN) + SUM(PHU_THU)) AS thanh_tien,
		       SUM(so_luong) AS so_luong,
		       ten_nguoichidinh
		FROM   (
		           SELECT (ISNULL(tpd.don_gia, 0) * ISNULL(tpd.so_luong, 0)) AS 
		                  THANH_TIEN,
		                  tpd.so_luong,
		                  id_loaithanhtoan,
		                  tpd.ten_chitietdichvu,
		                  (ISNULL(tpd.phu_thu, 0) * ISNULL(tpd.so_luong, 0)) AS 
		                  PHU_THU,
		                  TP.ma_khoa_thuchien,
		                  tpd.ma_doituong_kcb,
		                   isnull((
		                      SELECT TOP 1 [USER_NAME]
		                      FROM   dmuc_nhanvien dn
		                      WHERE  dn.id_nhanvien = tpd.id_bacsi_chidinh
		                  ),''ADMIN'') AS username,
		                 ISNULL( (
		                      SELECT TOP 1 dn.ten_nhanvien
		                      FROM   dmuc_nhanvien dn
		                      WHERE  dn.id_nhanvien = tpd.id_bacsi_chidinh
		                  ),N''Qu?n tr? h? th?ng'') AS ten_nguoichidinh
		           FROM   kcb_thanhtoan_chitiet tpd
		                  JOIN kcb_thanhtoan tp
		                       ON  tpd.id_thanhtoan = tp.id_thanhtoan
		           WHERE  tp.Kieu_ThanhToan = 0
		                  AND ISNULL(tpd.trangthai_huy, 0) = 0
		                  AND tpd.id_loaithanhtoan IN (2)
		                  AND (@sfromdate=''01/01/1900'' OR dbo.trunc(tp.ngay_thanhtoan) 
		                      BETWEEN @FromDate AND @ToDate)
		                  AND (
		                          @MA_KHOA_THIEN = ''-1''
		                          OR tp.ma_khoa_thuchien = @MA_KHOA_THIEN
		                      )
		                  AND (tpd.ma_doituong_kcb = @MaDoiTuong OR @MaDoiTuong = ''-1'')
		                   AND(@has_exam=-1 OR (@has_exam=0 AND isnull(id_kham,-1)=-1)
		                  OR (@has_exam=1 AND isnull(id_kham,-1)>0))
		                   AND (@Loaidichvu=''-1'' OR tpd.id_dichvu IN (SELECT *
		                                        FROM   dbo.fromStringintoIntTable(@Loaidichvu) 
		                                               fsit))
		       ) AS P
		WHERE  (P.username = @CreateBy OR @CreateBy = ''-1'')
		GROUP BY
		       ten_nguoichidinh
		ORDER BY
		       ten_nguoichidinh
	END
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Baocao_Chidinhcls_Tonghop Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Baocao_Chidinhcls_Tonghop Procedure'
END
GO


--  
-- Script to Update dbo.Donthuoc_Laythongtin_Thuoctrongkho_Kedon in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Donthuoc_Laythongtin_Thuoctrongkho_Kedon Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('ALTER PROCEDURE [dbo].[Donthuoc_Laythongtin_Thuoctrongkho_Kedon]
	@IdKho INT,
	@KieuThuocVT NVARCHAR(50),
	@madoituongkcb NVARCHAR(10),
	@Dungtuyen INT,
	@Noitru INT,
	@MA_KHOA_THIEN NVARCHAR(10)
AS
	DECLARE @LOAIKHO INT
	
	DECLARE @id_loaidoituong_kcb TINYINT
	DECLARE @tutuc TINYINT
	DECLARE @GIATHUOCKHO INT
	DECLARE @APDUNG_GIATHUOC_DOITUONG INT
	DECLARE @Chongia_trongkho INT
	DECLARE @THUOC_GIATHEO_KHOAKCB INT
	DECLARE @giathuoc_quanhe INT
	
	DECLARE @BHYT_TRAITUYENNGOAITRU_GIADICHVU TINYINT
	SET @BHYT_TRAITUYENNGOAITRU_GIADICHVU = ISNULL(
	        (
	            SELECT TOP 1 dk.sValue
	            FROM   Sys_SystemParameters dk
	            WHERE  dk.sName = ''BHYT_TRAITUYENNGOAITRU_GIADICHVU''
	        ),
	        0
	    )
	
	
	SET @GIATHUOCKHO = 1
	SET @giathuoc_quanhe = ISNULL(
	        (
	            SELECT dd.giathuoc_quanhe
	            FROM   dmuc_doituongkcb dd
	            WHERE  ma_doituong_kcb = @madoituongkcb
	        ),
	        0
	    )
	
	SET @THUOC_GIATHEO_KHOAKCB = ISNULL(
	        (
	            SELECT TOP 1 dk.sValue
	            FROM   Sys_SystemParameters dk
	            WHERE  dk.sName = ''THUOC_GIATHEO_KHOAKCB''
	        ),
	        0
	    )
	
	SET @APDUNG_GIATHUOC_DOITUONG = ISNULL(
	        (
	            SELECT TOP 1 CONVERT(INT, svalue)
	            FROM   Sys_SystemParameters
	            WHERE  UPPER(LTRIM(RTRIM(sName))) = ''APDUNG_GIATHUOC_DOITUONG''
	        ),
	        0
	    )
	
	
	
	SET @Chongia_trongkho = ISNULL(
	        (
	            SELECT TOP 1 dk.chophep_chongia
	            FROM   t_dmuc_kho dk
	            WHERE  dk.ID_KHO = @IdKho
	        ),
	        0
	    )
	
	SET @LOAIKHO = (
	        SELECT TOP 1 dk.la_quaythuoc
	        FROM   t_dmuc_kho dk
	        WHERE  dk.ID_KHO = @IdKho
	    )
	
	SET @tutuc = (
	        SELECT TOP 1 dk.tu_tuc
	        FROM   t_dmuc_kho dk
	        WHERE  dk.ID_KHO = @IdKho
	    )
	
	SET @id_loaidoituong_kcb = (
	        SELECT TOP 1 dk.id_loaidoituong_kcb
	        FROM   dmuc_doituongkcb dk
	        WHERE  dk.ma_doituong_kcb = @madoituongkcb
	    )
	
	DECLARE @KTRA_TON INT
	SET @KTRA_TON = (
	        SELECT TOP 1 dk.ktra_ton
	        FROM   t_dmuc_kho dk
	        WHERE  dk.ID_KHO = @IdKho
	    )
	
	
	IF (@APDUNG_GIATHUOC_DOITUONG = 1 OR @giathuoc_quanhe = 1)--Lay gia thuoc trong bang quan he
	BEGIN
	    SELECT T.*,
	           -1 AS id_thuockho,
	           GETDATE() AS ngay_nhap,
	           (
	               CASE 
	                    WHEN @Dungtuyen = 1 THEN phuthu_dungtuyen
	                    ELSE phuthu_traituyen
	               END
	           ) AS phu_thu,
	           phuthu_dungtuyen,
	           phuthu_traituyen,
	           (CASE WHEN @id_loaidoituong_kcb = 1 THEN 0 ELSE TUTUC END) AS 
	           tu_tuc,
	           (
	               SELECT TOP 1 TEN
	               FROM   dmuc_chung lmu
	               WHERE  lmu.MA = T.ma_donvitinh
	                      AND LOAI = ''DONVITINH''
	           ) AS ten_donvitinh
	    FROM   (
	               SELECT P.id_thuoc,
	                      p.tinh_chat,
	                      p.mota_them,
	                      ISNULL(p.gioihan_kedon, -1) AS gioihan_kedon,
	                      ISNULL(p.donvi_but, -1) AS donvi_but,
	                      P.ten_thuoc,
	                      P.ma_donvitinh,
	                      P.ma_thuoc,
	                      P.hoat_chat,
	                      P.ham_luong,
	                      P.ma_loaithuoc,
	                      p.gia_bhyt AS gia_bhyt_dmuc,
	                      p.phuthu_dungtuyen AS ptdt_dmuc,
	                      p.phuthu_traituyen AS pttt_dmuc,
	                      (
	                          CASE 
	                               WHEN CO_QHE <= 0
	                          OR TON_TAI_DVU <= 0 THEN (
	                                 CASE 
	                                      WHEN @id_loaidoituong_kcb = 1 THEN p.don_gia
	                                      ELSE p.gia_bhyt
	                                 END
	                             )
	                             ELSE (
	                                 CASE 
	                                      WHEN TON_TAI > 0
	                                 AND (
	                                         @Dungtuyen = 1
	                                         OR @BHYT_TRAITUYENNGOAITRU_GIADICHVU 
	                                            = 0
	                                         OR @Noitru = 1
	                                     ) 
	                                     THEN gia_ban ELSE gia_ban_DV END
	                             )
	                             END
	                      ) AS GIA_BAN,
	                      (
	                          CASE 
	                               WHEN CO_QHE <= 0
	                          OR TON_TAI_DVU <= 0 THEN p.phuthu_dungtuyen
	                             ELSE (
	                                 CASE 
	                                      WHEN TON_TAI > 0
	                                 AND (
	                                         @Dungtuyen = 1
	                                         OR @BHYT_TRAITUYENNGOAITRU_GIADICHVU 
	                                            = 0
	                                         OR @Noitru = 1
	                                     ) 
	                                     THEN phuthu_dungtuyen_dt
	                                     ELSE phuthu_dungtuyen_DV
	                                     END
	                             )
	                             END
	                      ) AS phuthu_dungtuyen,
	                      (
	                          CASE 
	                               WHEN CO_QHE <= 0
	                          OR TON_TAI_DVU <= 0 THEN p.phuthu_traituyen 
	                             ELSE (
	                                 CASE 
	                                      WHEN TON_TAI > 0
	                                 AND (
	                                         @Dungtuyen = 1
	                                         OR @BHYT_TRAITUYENNGOAITRU_GIADICHVU 
	                                            = 0
	                                         OR @Noitru = 1
	                                     ) 
	                                     THEN phuthu_traituyen_dt
	                                     ELSE phuthu_traituyen_DV
	                                     END
	                             )
	                             END
	                      ) AS phuthu_traituyen,
	                      (
	                          CASE 
	                               WHEN TON_TAI < 0
	                          OR tutuc_chitiet = 1
	                          OR (
	                                 @Noitru = 0
	                                 AND (@Dungtuyen = 0 OR @BHYT_TRAITUYENNGOAITRU_GIADICHVU = 1)
	                             )
	                             THEN 1
	                             ELSE 0
	                             END
	                      ) AS TUTUC
	                      
	                      --(
	                      --    CASE
	                      --         WHEN TON_TAI > 0
	                      --    AND (@Dungtuyen = 1 OR @BHYT_TRAITUYENNGOAITRU_GIADICHVU = 0)
	                      --        THEN
	                      --        gia_ban ELSE ( CASE WHEN  gia_ban_DV) END
	                      --) AS GIA_BAN,
	                      --(
	                      --    CASE
	                      --         WHEN TON_TAI > 0
	                      --    AND (@Dungtuyen = 1 OR @BHYT_TRAITUYENNGOAITRU_GIADICHVU = 0)
	                      --        THEN
	                      --        phuthu_dungtuyen_dt
	                      --        ELSE phuthu_dungtuyen_DV
	                      --        END
	                      --) AS phuthu_dungtuyen,
	                      --(
	                      --    CASE
	                      --         WHEN TON_TAI > 0
	                      --    AND (@Dungtuyen = 1 OR @BHYT_TRAITUYENNGOAITRU_GIADICHVU = 0)
	                      --        THEN
	                      --        phuthu_traituyen_dt
	                      --        ELSE phuthu_traituyen_DV
	                      --        END
	                      --) AS phuthu_traituyen,
	                      --(
	                      --    CASE
	                      --         WHEN TON_TAI < 0
	                      --    OR (@Dungtuyen = 0 OR @BHYT_TRAITUYENNGOAITRU_GIADICHVU = 1)
	                      --    OR tutuc_chitiet = 1
	                      --       THEN 1
	                      --       ELSE 0
	                      --       END
	                      --) AS TUTUC
	                      --(
	                      --    CASE
	                      --         WHEN TON_TAI > 0
	                      --    AND tutuc_chitiet <= 0  AND (@Dungtuyen=0 OR @Noitru=1 ) THEN 0 ELSE 1 END
	                      --) AS TUTUC
	               FROM   (
	                          SELECT ld.*,
	                                 ISNULL(ld.tu_tuc, 0) AS tutuc_chitiet,
	                                 ISNULL(
	                                     (
	                                         SELECT COUNT(*)
	                                         FROM   qhe_doituong_thuoc P
	                                         WHERE  P.id_thuoc = ld.id_thuoc
	                                                AND P.ma_doituong_kcb = @madoituongkcb
	                                                AND (
	                                                        @THUOC_GIATHEO_KHOAKCB 
	                                                        = 0
	                                                        OR ma_khoa_thuchien 
	                                                           = @MA_KHOA_THIEN
	                                                    )
	                                     ),
	                                     0
	                                 ) AS TON_TAI,
	                                 ISNULL(
	                                     (
	                                         SELECT COUNT(*)
	                                         FROM   qhe_doituong_thuoc P
	                                         WHERE  P.id_thuoc = ld.id_thuoc
	                                                AND P.ma_doituong_kcb = ''DV''
	                                                AND (
	                                                        @THUOC_GIATHEO_KHOAKCB 
	                                                        = 0
	                                                        OR ma_khoa_thuchien 
	                                                           = @MA_KHOA_THIEN
	                                                    )
	                                     ),
	                                     0
	                                 ) AS TON_TAI_DVU,
	                                 ISNULL(
	                                     (
	                                         SELECT COUNT(*)
	                                         FROM   qhe_doituong_thuoc P
	                                         WHERE  P.id_thuoc = ld.id_thuoc
	                                                --AND P.ma_doituong_kcb = @madoituongkcb
	                                     ),
	                                     0
	                                 ) AS CO_QHE,
	                                 ISNULL(
	                                     (
	                                         SELECT TOP 1 p.don_gia
	                                         FROM   qhe_doituong_thuoc P
	                                         WHERE  P.id_thuoc = ld.id_thuoc
	                                                AND P.ma_doituong_kcb = @madoituongkcb
	                                                AND (
	                                                        @THUOC_GIATHEO_KHOAKCB 
	                                                        = 0
	                                                        OR ma_khoa_thuchien 
	                                                           = @MA_KHOA_THIEN
	                                                    )
	                                     ),
	                                     0
	                                 ) AS gia_ban,
	                                 (
	                                     SELECT TOP 1 ma_loaithuoc
	                                     FROM   dmuc_loaithuoc
	                                     WHERE  id_loaithuoc = ld.id_loaithuoc
	                                 ) AS ma_loaithuoc,
	                                 ISNULL(
	                                     (
	                                         SELECT TOP 1 p.phuthu_dungtuyen
	                                         FROM   qhe_doituong_thuoc P
	                                         WHERE  P.id_thuoc = ld.id_thuoc
	                                                AND P.ma_doituong_kcb = @madoituongkcb
	                                                AND (
	                                                        @THUOC_GIATHEO_KHOAKCB 
	                                                        = 0
	                                                        OR ma_khoa_thuchien 
	                                                           = @MA_KHOA_THIEN
	                                                    )
	                                     ),
	                                     0
	                                 ) AS phuthu_dungtuyen_dt,
	                                 ISNULL(
	                                     (
	                                         SELECT TOP 1 p.phuthu_traituyen
	                                         FROM   qhe_doituong_thuoc P
	                                         WHERE  P.id_thuoc = ld.id_thuoc
	                                                AND P.ma_doituong_kcb = @madoituongkcb
	                                                AND (
	                                                        @THUOC_GIATHEO_KHOAKCB 
	                                                        = 0
	                                                        OR ma_khoa_thuchien 
	                                                           = @MA_KHOA_THIEN
	                                                    )
	                                     ),
	                                     0
	                                 ) AS phuthu_traituyen_dt,
	                                 ISNULL(
	                                     (
	                                         SELECT TOP 1 p.don_gia
	                                         FROM   qhe_doituong_thuoc P
	                                         WHERE  P.id_thuoc = ld.id_thuoc
	                                                AND P.ma_doituong_kcb = ''DV''
	                                                AND (
	                                                        @THUOC_GIATHEO_KHOAKCB 
	                                                        = 0
	                                                        OR ma_khoa_thuchien 
	                                                           = @MA_KHOA_THIEN
	                                                    )
	                                     ),
	                                     0
	                                 ) AS gia_ban_DV,
	                                 ISNULL(
	                                     (
	                                         SELECT TOP 1 p.phuthu_dungtuyen
	                                         FROM   qhe_doituong_thuoc P
	                                         WHERE  P.id_thuoc = ld.id_thuoc
	                                                AND P.ma_doituong_kcb = ''DV''
	                                                AND (
	                                                        @THUOC_GIATHEO_KHOAKCB 
	                                                        = 0
	                                                        OR ma_khoa_thuchien 
	                                                           = @MA_KHOA_THIEN
	                                                    )
	                                     ),
	                                     0
	                                 ) AS phuthu_dungtuyen_DV,
	                                 ISNULL(
	                                     (
	                                         SELECT TOP 1 p.phuthu_traituyen
	                                         FROM   qhe_doituong_thuoc P
	                                         WHERE  P.id_thuoc = ld.id_thuoc
	                                                AND P.ma_doituong_kcb = ''DV''
	                                                AND (
	                                                        @THUOC_GIATHEO_KHOAKCB 
	                                                        = 0
	                                                        OR ma_khoa_thuchien 
	                                                           = @MA_KHOA_THIEN
	                                                    )
	                                     ),
	                                     0
	                                 ) AS phuthu_traituyen_DV
	                          FROM   dmuc_thuoc ld
	                          WHERE  ld.trang_thai = 1
	                                 AND (ld.kieu_thuocvattu = @KieuThuocVT OR @KieuThuocVT = ''-1'')
	                                 AND LD.id_thuoc IN (SELECT DTK.ID_THUOC
	                                                     FROM   t_thuockho dtk
	                                                     WHERE  DTK.ID_KHO = @IdKho
	                                                            AND ((@KTRA_TON = 1 AND dtk.SO_LUONG > 0) OR (@KTRA_TON = 0)))
	                      ) AS P
	           ) AS T
	    WHERE  (@GIATHUOCKHO = 1 OR GIA_BAN > 0)
	END
	ELSE--Lay gia thuoc trong bang thuoc kho
	IF (@Chongia_trongkho = 1)--N?u kho cho ch?n thu?c ho?c b?
	                          ----ep tren c?u hinh h? th?ng thi ke nhi?u gia
	BEGIN
	    SELECT tbl.id_thuoc,
	                      tbl.ten_thuoc,
	                      tbl.tinh_chat,
	                      tbl.mota_them,
	                      tbl.gioihan_kedon,
	                      tbl.donvi_but,
	                      tbl.co_chiathuoc,
	                      tbl.sluong_chia,
	                      tbl.dongia_chia,
	                      tbl.ma_dvichia,
	                      tbl.ma_donvitinh,
	                      tbl.ma_thuoc,
	                      tbl.hoat_chat,
	                      tbl.ham_luong,
	                      tbl.ma_loaithuoc,
	                      tbl.id_thuockho,
	                      tbl.ngay_nhap,
	                      (case when tbl.co_chiathuoc =0 then tbl.GIA_BAN ELSE tbl.dongia_chia end) AS GIA_BAN,
	                      (case when tbl.tu_tuc=0 THEN 0 ELSE  tbl.phu_thu end) AS phu_thu,
	                      tbl.tu_tuc,
	                      (case when tbl.co_chiathuoc =0 then tbl.ten_donvitinh WHEN tbl.co_chiathuoc =1 AND ten_donvichia='''' THEN  tbl.ten_donvitinh ELSE ten_donvichia END) AS ten_donvitinh,
	                      tbl.phuthu_dungtuyen,
	                      tbl.phuthu_traituyen
	    FROM   (
	               SELECT ld.id_thuoc,
	                      ld.ten_thuoc,
	                      ld.tinh_chat,
	                      ld.mota_them,
	                      ISNULL(ld.gioihan_kedon, -1) AS gioihan_kedon,
	                      ISNULL(ld.donvi_but, -1) AS donvi_but,
	                      ISNULL(ld.co_chiathuoc, 0) AS co_chiathuoc,
	                      ISNULL(ld.sluong_chia, 1) AS sluong_chia,
	                      ISNULL(ld.dongia_chia, 0) AS dongia_chia,
	                      ISNULL(ld.ma_dvichia, ld.ma_donvitinh) AS ma_dvichia,
	                      ld.ma_donvitinh,
	                      ld.ma_thuoc,
	                      ld.hoat_chat,
	                      ld.ham_luong,
	                      (
	                          SELECT TOP 1 ma_loaithuoc
	                          FROM   dmuc_loaithuoc
	                          WHERE  id_loaithuoc = ld.id_loaithuoc
	                      ) AS ma_loaithuoc,
	                      id_thuockho,
	                      ngay_nhap,
	                      T.gia_ban AS GIA_BAN,
	                      ISNULL(
	                          (
	                              CASE 
	                                   WHEN @id_loaidoituong_kcb = 1 THEN 0
	                                   ELSE (
	                                            CASE 
	                                                 WHEN @Dungtuyen = 1 THEN T.phuthu_dungtuyen
	                                                 ELSE T.phuthu_traituyen
	                                            END
	                                        )
	                              END
	                          ),
	                          0
	                      ) AS phu_thu,
	                      (
	                          CASE 
	                               WHEN @id_loaidoituong_kcb = 1 THEN 0--Doi tuong dich vu
	                               ELSE ISNULL(ld.tu_tuc, 0)
	                          END
	                      ) tu_tuc,
	                      (
	                          SELECT TOP 1 TEN
	                          FROM   dmuc_chung lmu
	                          WHERE  lmu.MA = ld.ma_donvitinh
	                                 AND LOAI = ''DONVITINH''
	                      ) AS ten_donvitinh,
	                     ISNULL( (
	                          SELECT TOP 1 TEN
	                          FROM   dmuc_chung lmu
	                          WHERE  lmu.MA = ld.ma_dvichia
	                                 AND LOAI = ''DONVITINH''
	                      ),'''') AS ten_donvichia,
	                      T.phuthu_dungtuyen,
	                      T.phuthu_traituyen
	               FROM   dmuc_thuoc ld
	                      INNER JOIN (
	                               SELECT T9.*
	                               FROM   (
	                                          SELECT dtk.ID_THUOC,
	                                                 dtk.phuthu_dungtuyen,
	                                                 dtk.phuthu_traituyen,
	                                                 dtk.ngay_nhap,
	                                                 (
	                                                     CASE 
	                                                          WHEN @id_loaidoituong_kcb
	                                                               = 0 THEN dtk.gia_bhyt
	                                                          ELSE dtk.GIA_BAN
	                                                     END
	                                                 ) AS Gia_ban,
	                                                 dtk.id_thuockho
	                                          FROM   t_thuockho dtk
	                                          WHERE  dtk.ID_KHO = @IdKho
	                                                 AND ((@KTRA_TON = 1 AND dtk.SO_LUONG > 0) OR (@KTRA_TON = 0))
	                                                 AND dtk.GIA_BAN > 0
	                                      ) AS T9
	                               GROUP BY
	                                      T9.ID_THUOC,
	                                      T9.GIA_BAN,
	                                      T9.phuthu_dungtuyen,
	                                      T9.phuthu_traituyen,
	                                      T9.ngay_nhap,
	                                      T9.id_thuockho
	                           ) AS T
	                           ON  ld.id_thuoc = T.ID_THUOC
	               WHERE  (ld.kieu_thuocvattu = @KieuThuocVT OR @KieuThuocVT = ''-1'')
	                      AND ld.trang_thai = 1
	           ) AS tbl
	END
	ELSE
	    --Moi thuoc xuat hien 1 dong, tu dong tru theo thu tu ban
	BEGIN
	    SELECT ld.id_thuoc,
	           ld.ten_thuoc,
	           ld.tinh_chat,
	           ld.mota_them,
	           ISNULL(ld.gioihan_kedon, -1) AS gioihan_kedon,
	           ISNULL(ld.donvi_but, -1) AS donvi_but,
	           ld.ma_donvitinh,
	           ld.ma_thuoc,
	           ld.hoat_chat,
	           ld.ham_luong,
	           (
	               SELECT TOP 1 ma_loaithuoc
	               FROM   dmuc_loaithuoc
	               WHERE  id_loaithuoc = ld.id_loaithuoc
	           ) AS ma_loaithuoc,
	           0 AS GIA_BAN,	--du?c xac d?nh sau khi ch?n thu?c va ch?p nh?n
	           GETDATE() AS ngay_nhap,	--Du?c xac d?nh l?i t?i ph?n ke don theo th? t? ban
	           ISNULL(
	               (
	                   CASE 
	                        WHEN @id_loaidoituong_kcb = 1 THEN 0
	                        ELSE (
	                                 CASE 
	                                      WHEN @Dungtuyen = 1 THEN 
	                                           phuthu_dungtuyen
	                                      ELSE phuthu_traituyen
	                                 END
	                             )
	                   END
	               ),
	               0
	           ) AS phu_thu,
	           -1 AS id_thuockho,
	           (
	               CASE 
	                    WHEN @id_loaidoituong_kcb = 1 THEN 0
	                    ELSE ISNULL(ld.tu_tuc, 0)
	               END
	           ) tu_tuc,
	           (
	               SELECT TOP 1 TEN
	               FROM   dmuc_chung lmu
	               WHERE  lmu.MA = ld.ma_donvitinh
	                      AND LOAI = ''DONVITINH''
	           ) AS ten_donvitinh
	    FROM   dmuc_thuoc ld
	           INNER JOIN (
	                    SELECT dtk.ID_THUOC
	                           --dtk.GIA_BAN
	                    FROM   t_thuockho dtk
	                    WHERE  dtk.ID_KHO = @IdKho
	                           AND ((@KTRA_TON = 1 AND dtk.SO_LUONG > 0) OR (@KTRA_TON = 0))
	                           AND dtk.GIA_BAN > 0--Ch? moc d? li?u co gia >0 d? ban
	                    GROUP BY
	                           dtk.ID_THUOC--,
	                                       --dtk.GIA_BAN
	                ) AS T
	                ON  ld.id_thuoc = T.ID_THUOC
	    WHERE  (ld.kieu_thuocvattu = @KieuThuocVT OR @KieuThuocVT = ''-1'')
	           AND ld.trang_thai = 1
	END
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Donthuoc_Laythongtin_Thuoctrongkho_Kedon Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Donthuoc_Laythongtin_Thuoctrongkho_Kedon Procedure'
END
GO


--  
-- Script to Create dbo.Kcb_KiemtrathanhtoanhetDoituongDV_Truockhinhapvien in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.Kcb_KiemtrathanhtoanhetDoituongDV_Truockhinhapvien Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('-- Stored Procedure

CREATE PROCEDURE dbo.Kcb_KiemtrathanhtoanhetDoituongDV_Truockhinhapvien
@id_benhnhan bigint,
@ma_luotkham NVARCHAR(10)

AS
SELECT kdk.bnhan_chitra
FROM kcb_dangky_kcb kdk
WHERE ISNULL(kdk.noitru,0)=0
AND kdk.id_benhnhan=@id_benhnhan
AND kdk.ma_luotkham=@ma_luotkham
AND isnull(kdk.trangthai_thanhtoan,0)=0
UNION
SELECT kdk.bnhan_chitra
FROM kcb_chidinhcls_chitiet kdk
INNER JOIN kcb_chidinhcls kd
ON kd.id_chidinh=kdk.id_chidinh
WHERE ISNULL(kd.noitru,0)=0
AND kdk.id_benhnhan=@id_benhnhan
AND kdk.ma_luotkham=@ma_luotkham
AND isnull(kdk.trangthai_thanhtoan,0)=0
UNION
SELECT kdk.bnhan_chitra
FROM kcb_donthuoc_chitiet kdk
INNER JOIN kcb_donthuoc kd
ON kd.id_donthuoc=kdk.id_donthuoc
WHERE ISNULL(kd.noitru,0)=0
AND kdk.id_benhnhan=@id_benhnhan
AND kdk.ma_luotkham=@ma_luotkham
AND isnull(kdk.trangthai_thanhtoan,0)=0
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Kcb_KiemtrathanhtoanhetDoituongDV_Truockhinhapvien Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.Kcb_KiemtrathanhtoanhetDoituongDV_Truockhinhapvien Procedure'
END
GO


--  
-- Script to Update dbo.Kcb_Lichsukcb_Timkiembenhnhan in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Kcb_Lichsukcb_Timkiembenhnhan Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('ALTER PROCEDURE [dbo].[Kcb_Lichsukcb_Timkiembenhnhan]
    @tungay NVARCHAR(10),
    @denngay NVARCHAR(10),
	@maluotkham NVARCHAR(30),
	@idbenhnhan INT,
	@tenBenhnhan NVARCHAR(100),
	@matheBHYT NVARCHAR(30),
	@idBacsikham INT
AS
	SELECT *,(year(GETDATE())-p.nam_sinh) AS tuoi FROM
	kcb_danhsach_benhnhan as p
	WHERE 
	(@tungay=''01/01/1900'' 
	OR dbo.trunc( ngay_tiepdon) BETWEEN dbo.fromString2Date(@tungay) 
	AND dbo.fromString2Date(@denngay))
	AND(@maluotkham='''' OR exists(select 1 from kcb_luotkham WHERE id_benhnhan=p.id_benhnhan AND  ma_luotkham LIKE ''%''+@maluotkham+''%''))
	AND(ten_benhnhan='''' OR ten_benhnhan LIKE ''%''+@tenBenhnhan+''%'')
	AND(@matheBHYT='''' OR  exists(select 1 from kcb_luotkham WHERE id_benhnhan=p.id_benhnhan AND mathe_bhyt LIKE ''%''+@matheBHYT+''%''))
	AND(@idbenhnhan=-1 OR id_benhnhan=@idbenhnhan)
	AND(@idBacsikham=-1 OR EXISTS(SELECT 1 FROM kcb_dangky_kcb kdk WHERE kdk.id_bacsikham=@idBacsikham AND kdk.id_benhnhan=p.id_benhnhan))
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Kcb_Lichsukcb_Timkiembenhnhan Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Kcb_Lichsukcb_Timkiembenhnhan Procedure'
END
GO


--  
-- Script to Update dbo.Thuoc_Baocao_Soluongtonthuoctheokho in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Thuoc_Baocao_Soluongtonthuoctheokho Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('ALTER PROCEDURE [dbo].[Thuoc_Baocao_Soluongtonthuoctheokho]
	@ID_KHO_LIST VARCHAR(100),
	@ID_THUOC INT,
	@ID_LOAITHUOC INT,
	@HET_HAN SMALLINT,
	@kieu_thuocvattu NVARCHAR(10)
AS
BEGIN
	SELECT ld.*,
	       P.*,
	       dbo.strGhepTenthuocBietduoc(ld.ten_thuoc, ld.hoat_chat) AS 
	       tenthuoc_bietduoc,
	       (
	           CASE 
	                WHEN la_tuthuoc = 0  THEN (
	                         SELECT TOP 1 lmu.TEN
	                         FROM   dmuc_chung AS lmu
	                         WHERE  LOAI = ''DONVITINH''
	                                AND lmu.MA = ld.ma_donvitinh
	                     )
	                WHEN la_tuthuoc = 1 AND ISNULL(ld.co_chiathuoc,0)=1 THEN
	                	(
	                         SELECT TOP 1 lmu.TEN
	                         FROM   dmuc_chung AS lmu
	                         WHERE  LOAI = ''DONVITINH''
	                                AND lmu.MA = ld.ma_dvichia
	                     )
	                ELSE (
	                         SELECT TOP 1 lmu.TEN
	                         FROM   dmuc_chung AS lmu
	                         WHERE  LOAI = ''DONVITINH''
	                                AND lmu.MA = ld.ma_donvitinh
	                     )
	           END
	       ) AS ten_donvitinh,
	       (
	           SELECT TOP 1 ten_loaithuoc
	           FROM   dbo.dmuc_loaithuoc
	           WHERE  id_loaithuoc = ld.id_loaithuoc
	       ) AS ten_loaithuoc
	FROM   dmuc_thuoc AS ld
	       INNER JOIN (
	                SELECT dt.ID_KHO,
	                       dt.ID_THUOC,
	                       dt.gia_nhap,
	                       dt.GIA_BAN,
	                       dt.SO_LUONG,
	                       dt.VAT,
	                       (
	                           SELECT TOP 1 m.TEN
	                           FROM   DMUC_CHUNG m
	                           WHERE  m.MA = dt.ma_nhacungcap
	                                  AND LOAI = ''NHACUNGCAP''
	                       ) AS ten_nhacungcap,
	                       ISNULL(
	                           (
	                               SELECT TOP 1 dk.la_tuthuoc
	                               FROM   t_dmuc_kho AS dk
	                               WHERE  ID_KHO = dt.ID_KHO
	                           ),
	                           0
	                       ) AS la_tuthuoc,
	                       (
	                           SELECT TOP 1 dk.TEN_KHO
	                           FROM   t_dmuc_kho AS dk
	                           WHERE  ID_KHO = dt.ID_KHO
	                       ) AS TEN_KHO,
	                       CONVERT(DATE, dt.ngay_hethan, 103) AS ngay_hethan,
	                       (
	                           SELECT CONVERT(VARCHAR(10), dt.ngay_hethan, 103)
	                       ) AS sNGAY_HET_HAN,
	                       (CASE WHEN dt.ngay_hethan < GETDATE() THEN 1 ELSE 0 END) AS 
	                       Is_Het_Han
	                FROM   t_thuockho AS dt
	                WHERE  (
	                           dt.ID_KHO IN (SELECT *
	                                         FROM   dbo.fromStringintoIntTable(@ID_KHO_LIST) AS 
	                                                csit)
	                           OR @ID_KHO_LIST = ''-1''
	                       )
	                       AND (dt.ID_THUOC = @ID_THUOC OR @ID_THUOC = -1)
	            ) AS P
	            ON  ld.id_thuoc = P.ID_THUOC
	WHERE  (@ID_LOAITHUOC = -1 OR ld.id_loaithuoc = @ID_LOAITHUOC)
	       AND ld.kieu_thuocvattu = @kieu_thuocvattu
	       AND (P.Is_Het_Han = @HET_HAN OR @HET_HAN = -1)
END
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Thuoc_Baocao_Soluongtonthuoctheokho Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Thuoc_Baocao_Soluongtonthuoctheokho Procedure'
END
GO


--  
-- Script to Update dbo.Thuoc_Kiemtrasoluong_Tonkho in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Thuoc_Kiemtrasoluong_Tonkho Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('ALTER PROCEDURE [dbo].[Thuoc_Kiemtrasoluong_Tonkho]
    @id_donthuoc bigint,
	@ID_Thuoc INT,
	@ID_Kho BIGINT,
	@id_thuockho bigINT,
	@KiemtraChoXacNhan INT,
	@Noitru TINYINT,
	@SoLuongTon INT OUTPUT
	WITH RECOMPILE
AS
	DECLARE @SLThatTrongKho  INT
	DECLARE @SoLuongChuaLay  INT
	DECLARE @SoLuongChuaLay_Phieunhaptra  INT
	SET @SoLuongChuaLay = 0
	
	SET @SoLuongChuaLay_Phieunhaptra = 0
	
	SET @SLThatTrongKho = (
		  SELECT SUM(P1.SO_LUONG * P1.sluong_dvi)
		  FROM (
	        SELECT P.SO_LUONG, 
	        ISNULL((SELECT TOP 1 sluong_chia FROM dmuc_thuoc tct WHERE tct.id_thuoc=p.id_thuoc),1) AS sluong_dvi 
	        FROM   t_thuockho P
	        WHERE  P.ID_KHO = @ID_Kho
	               AND P.ID_THUOC = @ID_Thuoc
	               AND(@id_thuockho=-1 OR id_thuockho=@id_thuockho)
	               AND dbo.trunc(P.ngay_hethan)>=dbo.trunc(GETDATE())
	    ) AS P1
	    )
	/************************************************************
		n?u @KiemtraChoXacNhan=1 thi ki?m tra s? lu?ng t?n cac don thu?c dang ch? xac nh?n
		    @KiemtraChoXacNhan=0 thi l?y s? lu?ng tong co trong kho thu?c
 ************************************************************/
	
	IF @KiemtraChoXacNhan = 1
	BEGIN
	    SET @SoLuongChuaLay = (
	            SELECT SUM(tpd.so_luong)
	            FROM   kcb_donthuoc_chitiet tpd
	                   JOIN kcb_donthuoc tp
	                        ON  tpd.id_donthuoc = tp.id_donthuoc
	            WHERE  tpd.id_thuoc = @ID_Thuoc
	                   --Neu la noi tru thi bo dieu kien ngay ke don.
	                   AND (@Noitru=1 OR dbo.trunc(tp.ngay_kedon) = dbo.trunc(GETDATE()))
	                   AND tpd.id_kho = @ID_Kho
	                   AND ISNULL(tpd.trang_thai, 0) = 0
	                   AND(@id_thuockho=-1 OR id_thuockho=@id_thuockho)
	                   AND(tp.id_donthuoc<>@id_donthuoc)
	        )
	     SET @SoLuongChuaLay_Phieunhaptra = (
	            SELECT SUM(tpd.SO_LUONG)
	            FROM   t_phieutrathuoc_khole_vekhochan_chitiet tpd
	                   JOIN t_phieutrathuoc_khole_vekhochan tp
	                        ON  tpd.id_phieu = tp.id_phieu
	            WHERE  tpd.ID_THUOC = @ID_Thuoc
	                   AND tp.id_khotra = @ID_Kho
	                   AND ISNULL(tp.TRANG_THAI, 0) = 0
	        )
	END
SET @SoLuongTon = ISNULL(@SLThatTrongKho, 0) -ISNULL(@SoLuongChuaLay, 0)- ISNULL(@SoLuongChuaLay_Phieunhaptra, 0)
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Thuoc_Kiemtrasoluong_Tonkho Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Thuoc_Kiemtrasoluong_Tonkho Procedure'
END
GO


--  
-- Script to Update dbo.Thuoc_Laychitietphieunhaptrakholevekhochan in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Thuoc_Laychitietphieunhaptrakholevekhochan Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('/************************************************************
 * Code formatted by SoftTree SQL Assistant C v6.4.230
 * Time: 14/11/2013 9:54:24 AM
 ************************************************************/

ALTER PROCEDURE [dbo].[Thuoc_Laychitietphieunhaptrakholevekhochan]
	@ID_PHIEU_NHAPTRA INT
	WITH RECOMPILE
AS
	SELECT LD.ten_thuoc AS TEN_THUOC,
	LD.ten_thuoc+''(''+isnull(ld.hoat_chat,'''')+'')'' as ten_thuoc_hoatchat,
	       ld.ham_luong,
	       ld.hoat_chat,
	       ld.hang_sanxuat,ngay_nhap,p.gia_bhyt,
	       ld.so_dangky,
	       ld.dang_baoche,
	       ld.nuoc_sanxuat,
	       (
	           SELECT TOP 1 LMU.TEN
	           FROM   dmuc_chung AS lmu
	           WHERE  LMU.MA = LD.ma_donvitinh
	       ) AS ten_donvitinh,
	      CONVERT(VARCHAR(10), P.ngay_hethan,103) AS NGAY_HET_HAN,
	      p.ngay_hethan,
	      (CASE WHEN CONVERT(date, P.ngay_hethan,103) >= CONVERT(date,GETDATE(),103) THEN 1
			WHEN CONVERT(date,P.ngay_hethan,103) < CONVERT(date,GETDATE(),103) THEN 0
			END
			) AS IsHetHan,
	      P.gia_nhap,
	      p.GIA_BAN,p.id_phieu,
	      p.id_phieu_chitiet,p.ID_THUOC,p.SO_LUONG,p.THANH_TIEN,p.VAT,p.ma_nhacungcap,p.id_thuockho,p.so_lo,p.id_chuyen,p.phuthu_dungtuyen,p.phuthu_traituyen
	      
	FROM   t_phieutrathuoc_khole_vekhochan_chitiet AS p
	       JOIN dmuc_thuoc AS ld
	            ON  LD.id_thuoc = p.ID_THUOC
	WHERE  p.id_phieu = @ID_PHIEU_NHAPTRA
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Thuoc_Laychitietphieunhaptrakholevekhochan Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Thuoc_Laychitietphieunhaptrakholevekhochan Procedure'
END
GO


--  
-- Script to Update dbo.Thuoc_Laythongtinthuoctontrongkho_Theokho in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Thuoc_Laythongtinthuoctontrongkho_Theokho Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('-- Stored Procedure

ALTER PROCEDURE [dbo].[Thuoc_Laythongtinthuoctontrongkho_Theokho]
@id_kho INT

AS
SELECT
P.*,@id_kho AS ID_KHO,
dbo.strGhepTenthuocBietduoc( p2.ten_thuoc,p2.hoat_chat) AS ten_thuoc_hamluong,p2.trang_thai,
p2.*,
isnull((SELECT SUM(tpd.so_luong)
	            FROM   kcb_donthuoc_chitiet tpd
	                   JOIN kcb_donthuoc tp
	                        ON  tpd.id_donthuoc = tp.id_donthuoc
	            WHERE  tpd.id_thuoc = p.ID_THUOC
	                   AND dbo.trunc(tp.ngay_kedon) = dbo.trunc(GETDATE())
	                   AND tpd.id_kho =@id_kho
	                   AND ISNULL(tp.trang_thai, 0) = 0
	                   AND ISNULL(tpd.trang_thai,0)=0),0) AS CHOXACNHAN	
FROM ( select id_thuoc,SUM(so_luong)  as so_luong,isnull(MAX(chophep_ketutruc),0) AS chophep_ketutruc 
from   t_thuockho 
where ID_KHO=@id_kho
group by id_thuoc) as P
inner join dmuc_thuoc p2 on p.ID_THUOC=p2.id_thuoc 
WHERE 
 p.SO_LUONG>0
ORDER BY ten_thuoc


SELECT *,CONVERT(nvarchar(10),ngay_hethan,103) as sNgayhethan,
isnull((select TEN from DMUC_CHUNG where MA=p.ma_nhacungcap and LOAI=''NHACUNGCAP''),'''') as ten_nhacungcap
FROM t_thuockho P 
WHERE P.ID_KHO=@id_kho
and SO_LUONG>0
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Thuoc_Laythongtinthuoctontrongkho_Theokho Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Thuoc_Laythongtinthuoctontrongkho_Theokho Procedure'
END
GO


--  
-- Script to Create dbo.Thuoc_Laythuoc_Kedontrongngay_Chuaxacnhan in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.Thuoc_Laythuoc_Kedontrongngay_Chuaxacnhan Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('-- Stored Procedure

create PROCEDURE [dbo].[t_laythuoc_trongkho_kedon]
	@id_kho INT,
	@id_thuoc INT,
	@so_luong INT
AS

	              
--select * 
--from D_THUOC_KHO
--where ID_KHO=@id_kho
--and id_thuoc=@id_thuoc
--and CONVERT(date, NGAY_HET_HAN,103)>=CONVERT(date,getdate(),103)
--order by stt_ban asc,NGAY_HET_HAN asc

SELECT tpd.*
	            FROM   kcb_donthuoc_chitiet tpd
	                   JOIN kcb_donthuoc tp
	                        ON  tpd.id_donthuoc = tp.id_donthuoc
	            WHERE  tpd.id_thuoc = @id_thuoc
	                   AND dbo.trunc(tp.ngay_kedon) = dbo.trunc(GETDATE())
	                   AND tpd.id_kho = @id_kho
	                   AND ISNULL(tp.trang_thai, 0) = 0
	                   AND ISNULL(tpd.trang_thai,0)=0
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Thuoc_Laythuoc_Kedontrongngay_Chuaxacnhan Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.Thuoc_Laythuoc_Kedontrongngay_Chuaxacnhan Procedure'
END
GO


--  
-- Script to Update dbo.Thuoc_Laythuoc_Trongkho_Kedon in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Thuoc_Laythuoc_Trongkho_Kedon Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('-- Stored Procedure

ALTER PROCEDURE dbo.Thuoc_Laythuoc_Trongkho_Kedon
	@id_kho INT,
	@id_thuoc INT,
	@id_thuockho INT
AS
	DECLARE @THUOC_KIEUXUATTHUOC NVARCHAR(10)
	SET @THUOC_KIEUXUATTHUOC = ISNULL(
	        (
	            SELECT TOP 1 dk.sValue
	            FROM   Sys_SystemParameters dk
	            WHERE  dk.sName = ''THUOC_KIEUXUATTHUOC''
	        ),
	        0
	    )
	
	IF @THUOC_KIEUXUATTHUOC = ''STT''
	    SELECT *,
	           CONVERT(DATETIME, tt.ngay_hethan, 103) AS sngay_hethan
	    FROM   t_thuockho tt
	    WHERE  (@id_thuockho = -1 OR id_thuockho = @id_thuockho)
	           AND tt.id_kho = @id_kho
	           AND tt.id_thuoc = @id_thuoc
	           AND so_luong > 0
	           AND dbo.trunDateTime(tt.ngay_hethan) >= dbo .trunDateTime (GETDATE())
	    ORDER BY
	           stt_ban,
	           tt.ngay_hethan
	ELSE 
	IF @THUOC_KIEUXUATTHUOC = ''FIFO''
	    SELECT *,
	           CONVERT(DATETIME, tt.ngay_hethan, 103) AS sngay_hethan
	    FROM   t_thuockho tt
	    WHERE  (@id_thuockho = -1 OR id_thuockho = @id_thuockho)
	           AND tt.id_kho = @id_kho
	           AND tt.id_thuoc = @id_thuoc
	           AND so_luong > 0
	           AND dbo.trunDateTime(tt.ngay_hethan) >= dbo .trunDateTime (GETDATE())
	    ORDER BY
	           ngay_nhap,
	           tt.ngay_hethan
	ELSE 
	IF @THUOC_KIEUXUATTHUOC = ''LIFO''
	    SELECT *,
	           CONVERT(DATETIME, tt.ngay_hethan, 103) AS sngay_hethan
	    FROM   t_thuockho tt
	    WHERE  (@id_thuockho = -1 OR id_thuockho = @id_thuockho)
	           AND tt.id_kho = @id_kho
	           AND tt.id_thuoc = @id_thuoc
	           AND so_luong > 0
	           AND dbo.trunDateTime(tt.ngay_hethan) >= dbo .trunDateTime (GETDATE())
	    ORDER BY
	           ngay_nhap DESC,
	           tt.ngay_hethan
	ELSE 
	IF @THUOC_KIEUXUATTHUOC = ''EXP''
	    SELECT *,
	           CONVERT(DATETIME, tt.ngay_hethan, 103) AS sngay_hethan
	    FROM   t_thuockho tt
	    WHERE  (@id_thuockho = -1 OR id_thuockho = @id_thuockho)
	           AND tt.id_kho = @id_kho
	           AND tt.id_thuoc = @id_thuoc
	           AND so_luong > 0
	           AND dbo.trunDateTime(tt.ngay_hethan) >= dbo .trunDateTime (GETDATE())
	    ORDER BY
	           ngay_nhap,
	           tt.ngay_hethan
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Thuoc_Laythuoc_Trongkho_Kedon Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Thuoc_Laythuoc_Trongkho_Kedon Procedure'
END
GO


--  
-- Script to Update dbo.Thuoc_Laythuoctrongkhoxuat in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Thuoc_Laythuoctrongkhoxuat Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('-- Stored Procedure

ALTER PROCEDURE [dbo].[Thuoc_Laythuoctrongkhoxuat]
	@ID_KHO INT,
	@IsHetHan INT,
	@KIEU_THUOC_VT NVARCHAR(10)
AS
	SELECT P.*,
	       (P.soluong_chuatru -P.sluongAo) AS SO_LUONG,
	       (P.soluong_chuatru -P.sluongAo) AS SO_LUONG_THEODONVI_CHIA
	       --(P.soluong_chuatru -P.sluongAo) AS SO_LUONG_THEODONVI_CHIA
	FROM   (
	           SELECT dtk.ID_KHO,
	                  dtk.ID_THUOC,
	                  CONVERT(VARCHAR(10), dtk.ngay_hethan, 103) AS NGAY_HET_HAN,
	                  dtk.gia_nhap,
	                  dtk.SO_LUONG AS soluong_chuatru,
	                  (dtk.SO_LUONG * ISNULL(ld.sluong_chia, 1)) AS soluong_chuatru_theodonvi_chia,
	                  dtk.gia_bhyt,
	                  dtk.ngay_nhap,
	                  dtk.phuthu_dungtuyen,
	                  dtk.phuthu_traituyen,
	                  dtk.VAT,
	                  dtk.GIA_BAN,
	                  GIA_BAN AS DON_GIA,
	                  ISNULL(
	                      (
	                          SELECT SUM(SO_LUONG)
	                          FROM   t_phieu_nhapxuatthuoc_chitiet p
	                          WHERE  id_chuyen = dtk.id_thuockho
	                                 AND EXISTS (
	                                         SELECT 1
	                                         FROM   t_phieu_nhapxuatthuoc
	                                         WHERE  LOAI_PHIEU = 2
	                                                AND id_phieu = p.id_phieu
	                                                AND TRANG_THAI = 0
	                                     )
	                      ),
	                      0
	                  ) AS sluongAo,
	                  dtk.ma_nhacungcap,
	                  dtk.id_thuockho,
	                  dtk.so_lo,
	                  ISNULL(
	                      (
	                          SELECT TEN
	                          FROM   DMUC_CHUNG
	                          WHERE  loai = ''NHACUNGCAP''
	                                 AND MA = dtk.ma_nhacungcap
	                      ),
	                      ''''
	                  ) AS Ten_nhacungcap,
	                  dtk.ngay_hethan AS NGAY_HETHAN,
	                  0 AS so_luong_chuyen,
	                  dbo.strGhepTenthuocBietduoc(ld.ten_thuoc, ld.hoat_chat) AS 
	                  ten_thuoc,
	                  ld.ham_luong,
	                  ld.hoat_chat,
	                  ld.hang_sanxuat,
	                  ld.nuoc_sanxuat,
	                  ld.ma_thuoc AS ma_thuoc,
	                   ISNULL(ld.gioihan_kedon, -1) AS gioihan_kedon,
	                      ISNULL(ld.donvi_but, -1) AS donvi_but,
	                      ISNULL(ld.co_chiathuoc, 0) AS co_chiathuoc,
	                      ISNULL(ld.sluong_chia, 1) AS sluong_chia,
	                      ISNULL(ld.dongia_chia, 0) AS dongia_chia,
	                      ISNULL(ld.ma_dvichia, ld.ma_donvitinh) AS ma_dvichia,
	                      
	                  dtk.SO_LUONG AS SO_LUONG_THAT,
	                  dtk.SO_LUONG  AS SO_LUONG_THAT_THEODONVI_CHIA,
	                  --(dtk.SO_LUONG * ISNULL(ld.sluong_chia, 1)) AS SO_LUONG_THAT_THEODONVI_CHIA,
	                  -1 AS id_phieu,
	                  0 AS chiet_khau,
	                  0 AS thanh_tien,
	                  (
	                      CASE 
	                           WHEN CONVERT(date, dtk.ngay_hethan, 103) >= 
	                                CONVERT(date, GETDATE(), 103) THEN 1
	                           WHEN CONVERT(date, dtk.ngay_hethan, 103) < 
	                                CONVERT(date, GETDATE(), 103) THEN 0
	                      END
	                  ) AS IsHetHan,
	                  (
	                      SELECT TOP 1 ten
	                      FROM   dmuc_chung AS lmu
	                      WHERE  lmu.MA = ld.ma_donvitinh
	                             AND LOAI = ''DONVITINH''
	                  ) AS ten_donvitinh,
	                  (
	                      SELECT TOP 1 ten
	                      FROM   dmuc_chung AS lmu
	                      WHERE  lmu.MA = ld.ma_dvichia
	                             AND LOAI = ''DONVITINH''
	                  ) AS ten_donvichia
	           FROM   t_thuockho AS dtk
	                  JOIN dmuc_thuoc AS ld
	                       ON  ld.id_thuoc = dtk.ID_THUOC
	           WHERE  dtk.ID_KHO = @id_kho
	                  AND ld.kieu_thuocvattu = @KIEU_THUOC_VT
	       ) AS P
	WHERE  (IsHetHan = @IsHetHan OR @IsHetHan <= 0)
	       AND (P.soluong_chuatru -P.sluongAo) > 0
	ORDER BY
	       ten_thuoc
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Thuoc_Laythuoctrongkhoxuat Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Thuoc_Laythuoctrongkhoxuat Procedure'
END
GO


--  
-- Script to Create dbo.Thuoc_Thethuoc_Tutruc in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.Thuoc_Thethuoc_Tutruc Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('--CASE WHEN @LoaiPhieu=1 THEN N''PNK''--lo?i phi?u nh?p kho thu?c
	--					     WHEN @LoaiPhieu=2 THEN N''PXK''--lo?i phi?u xu?t kho thu?c
	--					     WHEN @LoaiPhieu=3 THEN N''PXKBN''--lo?i phi?u xu?t kho b?nh nhan
	--					     WHEN @LoaiPhieu=4 THEN N''PNTL''--lo?i phi?u tr? l?i t? kho l? v? kho ch?n-->Ap d?ng cho kho ch?n
	--					     WHEN @LoaiPhieu=5 THEN N''PNTKL''--lo?i phi?u tr? l?i t? khoa phong v? kho l?
	--					     WHEN @LoaiPhieu=6 THEN N''PXKHOA''--lo?i phi?u xu?t khoa
	--						 WHEN @LoaiPhieu=7 THEN N''PHUY''--lo?i phi?u h?y
	--						 WHEN @LoaiPhieu=8 THEN N''PTNCC''--Phi?u tr? nha cung c?p
	--						 WHEN @LoaiPhieu=9 THEN N''PXKTL''--Phi?u xu?t kho l? tr? kho ch?n-->Ap d?ng cho kho l?
	
	--					     end


CREATE PROCEDURE [dbo].[Thuoc_Thethuoc_Tutruc]
	@FromDate NVARCHAR(20),
	@ToDate NVARCHAR(20),
	@ID_KHO INT,
	@ID_THUOC INT,
	@NhomThuoc NVARCHAR(1000),
	@Cobiendong INT
AS
    DECLARE @TuNgay DATETIME
    DECLARE @DenNgay DATETIME
     DECLARE @KieuThuocVattu nvarchar(10)
	set @KieuThuocVattu=''THUOC''
	Select @KieuThuocVattu=kho_thuoc_vt
	from t_dmuc_kho where ID_KHO=@ID_KHO
    Set @TuNgay = Convert(datetime,@FromDate + '' 00:00:00'',103)    
    Set @DenNgay = convert(datetime, @ToDate +'' 23:59:59'',103) 
	IF OBJECT_ID(''tempdb..#NhapXuat_thethuoc'') IS NOT NULL
	    DROP TABLE #NhapXuat_thethuoc

	SELECT 
	       SUM(p.TRA_KHOLE)  AS TRA_KHOLE,
	       SUM(p.XUAT_BN)  AS XUAT_BN,
	       SUM(p.NHAP_KLE)  AS NHAP_KLE,
	       P.ID_THUOC,
	       P.ngay_biendong,
	       P.YYYYMMDD
	       INTO   #NhapXuat_thethuoc
	FROM   (
	           SELECT  
	                   (CASE WHEN P.ma_loaiphieu IN (5) and ISNULL(P.ID_KHOA_LINH,-1)>0 THEN P.SO_LUONG ELSE 0 END) TRA_KHOLE,	                  	                                          
	                   (case when P.ma_loaiphieu IN (3) then P.SO_LUONG else 0 end  )as XUAT_BN,
	                   (case when P.ma_loaiphieu IN (2)  then P.SO_LUONG else 0 end  )as NHAP_KLE,
                        Convert(nvarchar(10),P.ngay_biendong,103) AS ngay_biendong,
	                  convert(int,CONVERT(nvarchar(10), P.ngay_biendong,112)) as YYYYMMDD,
	                  P.ID_THUOC
	           FROM   (
	                      SELECT P.ID_THUOC,
	                             P.SO_LUONG,
	                             p.ID_KHOA_LINH,
	                             P.ma_loaiphieu
	                             ,P.ngay_biendong
	                      FROM   t_biendong_thuoc AS P
	                      WHERE   (P.ngay_biendong >=@TuNgay AND P.ngay_biendong < @DenNgay)
	                            AND  P.ID_KHO = @ID_KHO 
	                            AND P.ID_THUOC=@ID_THUOC
	                            AND P.kieu_thuocvattu=@KieuThuocVattu
	                  )  AS P
	       )   
	       AS P
	GROUP BY
	       P.ID_THUOC,
	       P.ngay_biendong,
	       P.YYYYMMDD

	--SELECT * FROM #NhapXuat
	IF OBJECT_ID(''tempdb..#temSLTonDau_thethuoc'') IS NOT NULL
	    DROP TABLE #temSLTonDau_thethuoc

	DECLARE @StartDate DATETIME    
	DECLARE @EndDate DATETIME    
	SET @StartDate = (CONVERT(DATETIME, @TuNgay, 103))     
	SET @EndDate = CONVERT(DATETIME, @DenNgay, 103)     
	EXEC thuoc_tinhsoluong_tondau_thethuoc @ID_KHO,@ID_THUOC,@StartDate,0 -- lay du lieu vut vao bang that BangTamSL_Ton_Nhap_Xuat    
	SELECT * INTO #temSLTonDau_thethuoc
	FROM   t_nhapxuatton_thethuoc_temp  

    IF OBJECT_ID(''tempdb..#temp_thethuoc'') IS NOT NULL
	--IF OBJECT_ID(''dbo.temp'') IS NOT NULL
	    DROP TABLE #temp_thethuoc
	--SELECT * FROM #NhapXuat
	SELECT ld.ten_thuoc               ,
	       ld.id_thuoc,
	       ld.id_loaithuoc,
	       ld.ma_donvitinh,
	         (select top 1 ten from dmuc_chung where LOAI=''DONVITINH'' AND ma=ld.ma_donvitinh ) as ten_donvitinh,
	       ISNULL(TonDau.SoLuong, 0)   AS Tontruoc,
	       ISNULL(n.TRA_KHOLE, 0)     AS TRA_KHOLE,
	       ISNULL(n.XUAT_BN, 0)     AS XUAT_BN,
	       ISNULL(n.NHAP_KLE, 0)        AS NHAP_KLE,
	       0 AS TONKC ,
	        n.ngay_biendong,
	       n.YYYYMMDD
	       INTO #temp_thethuoc
	FROM   dmuc_thuoc                     AS ld
	       LEFT OUTER JOIN #NhapXuat_thethuoc as n
	            ON  ld.id_thuoc = n.ID_THUOC
	       LEFT OUTER JOIN #temSLTonDau_thethuoc  as TonDau
	            ON  TonDau.id_thuoc = ld.id_thuoc
	WHERE  
	      @Cobiendong=0 OR
	       (
	           ISNULL(n.TRA_KHOLE, 0) > 0
	       OR  ISNULL(n.NHAP_KLE, 0) > 0
	        OR  ISNULL(n.Xuat_BN, 0) > 0
	       )    
	 AND (ld.id_thuoc=@ID_Thuoc OR @ID_Thuoc=-1)
	 AND ld.kieu_thuocvattu=@KieuThuocVattu

	SELECT *,
	       (
	           SELECT TOP 1 ldt.ten_loaithuoc
	           FROM   dmuc_loaithuoc AS ldt
	           WHERE  ldt.id_loaithuoc = P.id_loaithuoc
	       )     AS ten_loaithuoc,
	       (SELECT TOP 1 lmu.ten
	          FROM dmuc_chung AS lmu WHERE LOAI=''DONVITINH'' AND lmu.MA=P.ma_donvitinh)AS ten_donvitinh
	FROM   #temp_thethuoc     P
	WHERE (@NhomThuoc=''-1'' or P.id_loaithuoc IN (SELECT * FROM dbo.fromStringintoIntTable(@NhomThuoc)))
	ORDER BY
	       P.ten_thuoc
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Thuoc_Thethuoc_Tutruc Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.Thuoc_Thethuoc_Tutruc Procedure'
END
GO


--  
-- Script to Create dbo.Thuoc_Thethuoc_Tutruc_Chitiet in KKYBBNL-PC\SQL2K8.qlbv_old 
-- Generated Thursday, May 21, 2015, at 04:40 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_old before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.Thuoc_Thethuoc_Tutruc_Chitiet Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('--CASE WHEN @LoaiPhieu=1 THEN N''PNK''--lo?i phi?u nh?p kho thu?c
	--					     WHEN @LoaiPhieu=2 THEN N''PXK''--lo?i phi?u xu?t kho thu?c
	--					     WHEN @LoaiPhieu=3 THEN N''PXKBN''--lo?i phi?u xu?t kho b?nh nhan
	--					     WHEN @LoaiPhieu=4 THEN N''PNTL''--lo?i phi?u tr? l?i t? kho l? v? kho ch?n-->Ap d?ng cho kho ch?n
	--					     WHEN @LoaiPhieu=5 THEN N''PNTKL''--lo?i phi?u tr? l?i t? khoa phong v? kho l?
	--					     WHEN @LoaiPhieu=6 THEN N''PXKHOA''--lo?i phi?u xu?t khoa
	--						 WHEN @LoaiPhieu=7 THEN N''PHUY''--lo?i phi?u h?y
	--						 WHEN @LoaiPhieu=8 THEN N''PTNCC''--Phi?u tr? nha cung c?p
	--						 WHEN @LoaiPhieu=9 THEN N''PXKTL''--Phi?u xu?t kho l? tr? kho ch?n-->Ap d?ng cho kho l?
	
	--					     end

CREATE PROCEDURE [dbo].[Thuoc_Thethuoc_Tutruc_Chitiet]
	@FromDate NVARCHAR(10),
	@ToDate NVARCHAR(10),
	@ID_KHO INT,
	@NhomThuoc NVARCHAR(1000),
	@ID_Thuoc INT,
	@Cobiendong INT

AS
    DECLARE @TuNgay DATETIME
     DECLARE @DenNgay DATETIME
     DECLARE @LoaiKho nvarchar(10)
	DECLARE @KieuThuocVattu nvarchar(10)
	set @KieuThuocVattu=''THUOC''
	Select @KieuThuocVattu=kho_thuoc_vt
	from t_dmuc_kho where ID_KHO=@ID_KHO
	
    Set @TuNgay = Convert(datetime,@FromDate + '' 00:00:00'',103)    
    Set @DenNgay = convert(datetime, @ToDate +'' 23:59:59'',103) 
	IF OBJECT_ID(''tempdb..#NhapXuat_thethuoc_chitiet'') IS NOT NULL
	    DROP TABLE #NhapXuat_thethuoc_chitiet
	           SELECT (CASE WHEN P.ma_loaiphieu IN (1,4,5) THEN P.SO_LUONG ELSE 0 END) 
	                  SLNHAP,
	                  (CASE 
	                   WHEN P.ma_loaiphieu =2 THEN N''Nh?n t? kho l?''
	                   WHEN P.ma_loaiphieu =3 THEN N''Xu?t b?nh nhan''
	                   WHEN P.ma_loaiphieu =5 THEN N''Tr? kho l?''
	                   ELSE N''Khong xac d?nh'' END) 
	                  Noi_dung,
	                  (CASE WHEN P.ma_loaiphieu IN (3,5) THEN P.SO_LUONG ELSE 0 END) 
	                  SLXUAT,
	                  (CASE WHEN P.ma_loaiphieu IN (2) THEN CONVERT(NVARCHAR, P.id_phieu) ELSE '''' END) 
	                  SO_CTU_NHAP,
	                  (CASE WHEN P.ma_loaiphieu IN (3,5) THEN CONVERT(NVARCHAR,P.id_phieu) ELSE '''' END) 
	                  SO_CTU_XUAT,
	                  ma_loaiphieu,
	                  P.ngay_biendong,P.id_biendong,P.processed,
	                  P.ID_THUOC,P.so_lo,P.so_chungtu_kemtheo,p.ngay_hethan,p.don_gia
	                   INTO   #NhapXuat_thethuoc_chitiet
	           FROM   (
	                      SELECT P.ID_THUOC,P.so_lo,p.so_chungtu_kemtheo,P.id_phieu,P.id_biendong,0 AS processed,
	                             P.SO_LUONG,id_chuyen,
	                             P.ma_loaiphieu,
	                             p.ngay_biendong,p.ngay_hethan,p.don_gia
	                      FROM   t_biendong_thuoc AS P
	                      WHERE   (P.ngay_biendong >=@TuNgay AND P.ngay_biendong < @DenNgay)
	                              AND P.ID_KHO=@ID_KHO
	                              AND P.ID_THUOC=@ID_THUOC
	                              AND P.kieu_thuocvattu=@KieuThuocVattu
	                  )  AS P
	--SELECT * FROM #NhapXuat
	IF OBJECT_ID(''tempdb..#temSLTonDau_thethuoc_chitiet'') IS NOT NULL
	    DROP TABLE #temSLTonDau_thethuoc_chitiet
	
	DECLARE @StartDate DATETIME    
	DECLARE @EndDate DATETIME    
	SET @StartDate = (CONVERT(DATETIME, @TuNgay, 103))     
	SET @EndDate = CONVERT(DATETIME, @DenNgay, 103)     
	EXEC thuoc_tinhsoluong_tondau_thethuoc @ID_KHO,@ID_Thuoc,@StartDate,0 -- lay du lieu vut vao bang that BangTamSL_Ton_Nhap_Xuat    
	SELECT * INTO #temSLTonDau_thethuoc_chitiet
	FROM   t_nhapxuatton_thethuoc_temp  
    IF OBJECT_ID(''tempdb..#temp_thethuoc_chitiet'') IS NOT NULL
	--IF OBJECT_ID(''dbo.temp'') IS NOT NULL
	    DROP TABLE #temp_thethuoc_chitiet
	
	SELECT dbo.strGhepTenthuocBietduoc( ld.ten_thuoc,ld.hoat_chat)                AS tenbietduoc,
	       ld.id_thuoc                  AS ID_THUOC,
	       ld.id_loaithuoc              AS IDNHOM,
	       ld.ma_donvitinh,n.so_lo,n.so_chungtu_kemtheo,
	       ISNULL(TonDau.SoLuong, 0)   AS Tondau,
	       ISNULL(n.SLNHAP, 0)         AS SoLuongNhap,
	       ISNULL(n.SLXuat, 0)         AS SoLuongXuat,
	      0  AS Toncuoi ,SO_CTU_NHAP,SO_CTU_XUAT,n.id_biendong,n.processed,
	        n.ngay_biendong,ma_loaiphieu,n.ngay_hethan,n.don_gia,ld.hang_sanxuat,ld.nuoc_sanxuat,'''' AS NUOC_SXUAT,'''' AS Ghi_chu,n.noi_dung,ld.ma_thuoc AS ma_so,
	        ld.ham_luong,ld.dang_baoche,ld.hoat_chat
	       INTO #temp_thethuoc_chitiet
	FROM   dmuc_thuoc                     AS ld
	       LEFT OUTER JOIN #NhapXuat_thethuoc_chitiet as n
	            ON  ld.id_thuoc = n.ID_THUOC
	       LEFT OUTER JOIN #temSLTonDau_thethuoc_chitiet  as TonDau
	            ON  TonDau.id_thuoc = ld.id_thuoc
	WHERE  ld.trang_thai = 1 
		   AND ld.kieu_thuocvattu=@KieuThuocVattu
	       AND (@Cobiendong=0 OR
	       (
	       ISNULL(n.SLNHAP, 0) > 0
	       OR  ISNULL(n.SLXuat, 0) > 0
	       ))
	       AND (ld.id_thuoc=@ID_Thuoc OR @ID_Thuoc=-1)
	SELECT *,
	       (
	           SELECT TOP 1 ldt.ten_loaithuoc
	           FROM   dmuc_loaithuoc AS ldt
	           WHERE  ldt.id_loaithuoc = P.IDNHOM
	       )     AS tennhom,
	       (SELECT TOP 1 ten_kho from dbo.t_dmuc_kho WHERE id_kho=@ID_KHO) AS ten_kho,
	       P.IDNHOM as ID_NHOM,
	       (SELECT TOP 1 lmu.TEN
	          FROM dmuc_chung AS lmu WHERE LOAI=''DONVITINH'' AND lmu.MA=P.ma_donvitinh)AS ten_donvitinh
	          
	FROM   #temp_thethuoc_chitiet     P
	WHERE (@NhomThuoc=''-1'' or P.IDNHOM IN (SELECT * FROM dbo.fromStringintoIntTable(@NhomThuoc)))
	--AND (P.KIEU_THUOC_VT=@KieuThuocVT OR @KieuThuocVT=''ALL'')
	ORDER BY
	       P.ngay_biendong
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Thuoc_Thethuoc_Tutruc_Chitiet Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.Thuoc_Thethuoc_Tutruc_Chitiet Procedure'
END
GO
