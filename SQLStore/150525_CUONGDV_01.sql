--  
-- Script to Update dbo.dmuc_nhanvien in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.dmuc_nhanvien Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[dmuc_nhanvien]
      ADD [quyen_xemphieudieutricuabacsinoitrukhac] [tinyint] NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.dmuc_nhanvien Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.dmuc_nhanvien Table'
END
GO


--  
-- Script to Update dbo.kcb_donthuoc_chitiet in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.kcb_donthuoc_chitiet Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[kcb_donthuoc_chitiet]
      ADD [trangthai_tonghop] [tinyint] NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.kcb_donthuoc_chitiet Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.kcb_donthuoc_chitiet Table'
END
GO


--  
-- Script to Create dbo.noitru_dmuc_dinhduong in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.noitru_dmuc_dinhduong Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

CREATE TABLE [dbo].[noitru_dmuc_dinhduong] (
   [IdDinhduong] [int] NOT NULL,
   [MaDinhduong] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
   [IdCha] [int] NULL,
   [TenDinhduong] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
   [SttHienthi] [int] NULL,
   [MaNhomDinhduong] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
   [Trangthai] [tinyint] NOT NULL
)
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.noitru_dmuc_dinhduong Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.noitru_dmuc_dinhduong Table'
END
GO


--  
-- Script to Update dbo.noitru_phieudieutri in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.noitru_phieudieutri Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[noitru_phieudieutri]
      ADD [id_buong] [bigint] NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[noitru_phieudieutri]
      ADD [id_giuong] [bigint] NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.noitru_phieudieutri Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.noitru_phieudieutri Table'
END
GO


--  
-- Script to Create dbo.noitru_phieudinhduong in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.noitru_phieudinhduong Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

CREATE TABLE [dbo].[noitru_phieudinhduong] (
   [Id] [bigint] IDENTITY (1, 1) NOT NULL,
   [Ngay_Lap] [datetime] NULL,
   [Id_Phieudieutri] [bigint] NULL,
   [Ma_Holy] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [Ma_Dinhduong] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [Ngay_tao] [datetime] NULL,
   [Nguoi_tao] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[noitru_phieudinhduong] ADD CONSTRAINT [PK_noitru_phieudinhduong] PRIMARY KEY CLUSTERED ([Id])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.noitru_phieudinhduong Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.noitru_phieudinhduong Table'
END
GO


--  
-- Script to Update dbo.Sys_Reports in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Sys_Reports Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[Sys_Reports]
      ADD [printNumber] [smallint] NULL CONSTRAINT [DF_Sys_Reports_printNumber] DEFAULT ((1))
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Sys_Reports Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Sys_Reports Table'
END
GO


--  
-- Script to Create dbo.Dmuc_Laydanhsach_CackhoaKCBtheo_Bacsi in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.Dmuc_Laydanhsach_CackhoaKCBtheo_Bacsi Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE [dbo].[Dmuc_Laydanhsach_CackhoaKCBtheo_Bacsi]
	@UserName NVARCHAR(30),
	@IsAdmin TINYINT,
	@Noitru TINYINT
AS
SELECT p3.* FROM 
dmuc_khoaphong as p3
INNER JOIN
(
	SELECT DISTINCT c.id_khoa
	FROM  qhe_bacsi_khoaphong AS c
	WHERE  (@Noitru = -1 OR c.noitru = @Noitru)
	       AND (
	               @IsAdmin = 1
	               OR EXISTS(
	                      SELECT 1
	                      FROM   dmuc_nhanvien dn
	                      WHERE  dn.id_nhanvien = c.id_bacsi
	                             AND dn.[user_name] = @UserName
	                  )
	       )
) AS p2
ON p3.id_khoaphong=p2.id_khoa
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Dmuc_Laydanhsach_CackhoaKCBtheo_Bacsi Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.Dmuc_Laydanhsach_CackhoaKCBtheo_Bacsi Procedure'
END
GO


--  
-- Script to Update dbo.Donthuoc_Laythongtin_Thuoctrongkho_Kedon in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
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
	DECLARE @id_loaidoituong_kcb TINYINT
	DECLARE @chophep_chongia TINYINT
	DECLARE @tutuc TINYINT
	DECLARE @latutruc TINYINT
	DECLARE @THUOC_KIEUXUATTHUOC NVARCHAR(10)
	
	DECLARE @BHYT_TRAITUYENNGOAITRU_GIADICHVU TINYINT
	SET @BHYT_TRAITUYENNGOAITRU_GIADICHVU = ISNULL(
	        (
	            SELECT TOP 1 dk.sValue
	            FROM   Sys_SystemParameters dk
	            WHERE  dk.sName = ''BHYT_TRAITUYENNGOAITRU_GIADICHVU''
	        ),
	        0
	    )
	
	SET @latutruc = ISNULL(
	        (
	            SELECT TOP 1 la_tuthuoc
	            FROM   t_dmuc_kho tdk
	            WHERE  id_kho = @IdKho
	        ),
	        0
	    )
	
	SET @chophep_chongia = ISNULL(
	        (
	            SELECT TOP 1 chophep_chongia
	            FROM   t_dmuc_kho tdk
	            WHERE  id_kho = @IdKho
	        ),
	        0
	    )
	
	SET @THUOC_KIEUXUATTHUOC = ISNULL(
	        (
	            SELECT TOP 1 dk.sValue
	            FROM   Sys_SystemParameters dk
	            WHERE  dk.sName = ''THUOC_KIEUXUATTHUOC''
	        ),
	        0
	    )
	
	IF @THUOC_KIEUXUATTHUOC = ''STT''
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
	
	
	IF @chophep_chongia = 1
	BEGIN
	    --Lay thuoc trong ban thuoc kho, cho phep ke don thuoc nhieu gia
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
	           (
	               CASE 
	                    WHEN @latutruc = 0
	               OR @Noitru = 0
	               OR tbl.co_chiathuoc = 0 THEN tbl.GIA_BAN
	                  ELSE tbl.dongia_chia
	                  END
	           ) AS GIA_BAN,
	           (
	               CASE 
	                    WHEN @id_loaidoituong_kcb = 1--Doi uong dich vu
	               OR (tbl.tu_tuc = 1 AND @id_loaidoituong_kcb = 0) --Doi tuong BHYT va dung thuoc tu tuc-->Tinh gia Dich vu+Phuthu
	                  THEN 0 ELSE tbl.phu_thu END--Doi tuong BHYT dung thuoc khong tu tuc-->Tinh gia BHYT+Phuthu
	           ) AS phu_thu,
	           CONVERT(TINYINT, tbl.tu_tuc) AS tu_tuc,
	           (
	               CASE 
	                    WHEN @latutruc = 0
	               OR @Noitru = 0
	               OR tbl.co_chiathuoc = 0 THEN tbl.ten_donvitinh
	                  WHEN tbl.co_chiathuoc = 1
	               AND ten_donvichia = '''' THEN tbl.ten_donvitinh ELSE 
	                   ten_donvichia 
	                   END
	           ) AS ten_donvitinh,
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
	                                   WHEN @id_loaidoituong_kcb = 1
	                              OR (
	                                     @id_loaidoituong_kcb = 0
	                                     AND @Dungtuyen = 0
	                                     AND @BHYT_TRAITUYENNGOAITRU_GIADICHVU = 
	                                         1
	                                     AND @Noitru = 0
	                                 )
	                                 THEN 0
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
	                               WHEN @id_loaidoituong_kcb = 1 THEN 0--Doi tuong dich vu tu tuc luon =0
	                               ELSE ISNULL(ld.tu_tuc, 0)
	                          END
	                      ) tu_tuc,
	                      (
	                          SELECT TOP 1 TEN
	                          FROM   dmuc_chung lmu
	                          WHERE  lmu.MA = ld.ma_donvitinh
	                                 AND LOAI = ''DONVITINH''
	                      ) AS ten_donvitinh,
	                      ISNULL(
	                          (
	                              SELECT TOP 1 TEN
	                              FROM   dmuc_chung lmu
	                              WHERE  lmu.MA = ld.ma_dvichia
	                                     AND LOAI = ''DONVITINH''
	                          ),
	                          ''''
	                      ) AS ten_donvichia,
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
	                                                               = 0 THEN (
	                                                                   CASE 
	                                                                        WHEN 
	                                                                             @Dungtuyen 
	                                                                             = 
	                                                                             1
	                                                                   OR @BHYT_TRAITUYENNGOAITRU_GIADICHVU 
	                                                                      = 0 
	                                                                      THEN 
	                                                                      dtk.gia_bhyt
	                                                                      ELSE 
	                                                                      dtk.GIA_BAN 
	                                                                      END
	                                                               )
	                                                          ELSE dtk.GIA_BAN
	                                                     END
	                                                 ) AS Gia_ban,
	                                                 dtk.id_thuockho
	                                          FROM   t_thuockho dtk
	                                          WHERE  dtk.ID_KHO = @IdKho
	                                                 AND dtk.SO_LUONG > 0
	                                                 AND dtk.GIA_BAN > 0
	                                                 AND dbo.trunDateTime(dtk.ngay_hethan) 
	                                                     >= dbo .trunDateTime (GETDATE())
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
	    --Khong can quan tam den gia va Id_thuockho
	BEGIN
	    --Lay thuoc trong ban thuoc kho, cho phep ke don thuoc nhieu gia
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
	           
	           (
	               CASE 
	                    WHEN @latutruc = 0
	               OR @Noitru = 0
	               OR tbl.co_chiathuoc = 0 THEN tbl.GIA_BAN
	                  ELSE tbl.dongia_chia
	                  END
	           ) AS GIA_BAN,
	           (
	               CASE 
	                    WHEN @id_loaidoituong_kcb = 1--Doi uong dich vu
	               OR (tbl.tu_tuc = 1 AND @id_loaidoituong_kcb = 0) --Doi tuong BHYT va dung thuoc tu tuc-->Tinh gia Dich vu+Phuthu
	                  THEN 0 ELSE tbl.phu_thu END--Doi tuong BHYT dung thuoc khong tu tuc-->Tinh gia BHYT+Phuthu
	           ) AS phu_thu,
	           CONVERT(TINYINT, tbl.tu_tuc) AS tu_tuc,
	           (
	               CASE 
	                    WHEN @latutruc = 0
	               OR @Noitru = 0
	               OR tbl.co_chiathuoc = 0 THEN tbl.ten_donvitinh
	                  WHEN tbl.co_chiathuoc = 1
	               AND ten_donvichia = '''' THEN tbl.ten_donvitinh ELSE 
	                   ten_donvichia 
	                   END
	           ) AS ten_donvitinh,
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
	                      
	                      T.gia_ban AS GIA_BAN,
	                      ISNULL(
	                          (
	                              CASE 
	                                   WHEN @id_loaidoituong_kcb = 1
	                              OR (
	                                     @id_loaidoituong_kcb = 0
	                                     AND @Dungtuyen = 0
	                                     AND @BHYT_TRAITUYENNGOAITRU_GIADICHVU = 
	                                         1
	                                     AND @Noitru = 0
	                                 )
	                                 THEN 0
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
	                               WHEN @id_loaidoituong_kcb = 1 THEN 0--Doi tuong dich vu tu tuc luon =0
	                               ELSE ISNULL(ld.tu_tuc, 0)
	                          END
	                      ) tu_tuc,
	                      (
	                          SELECT TOP 1 TEN
	                          FROM   dmuc_chung lmu
	                          WHERE  lmu.MA = ld.ma_donvitinh
	                                 AND LOAI = ''DONVITINH''
	                      ) AS ten_donvitinh,
	                      ISNULL(
	                          (
	                              SELECT TOP 1 TEN
	                              FROM   dmuc_chung lmu
	                              WHERE  lmu.MA = ld.ma_dvichia
	                                     AND LOAI = ''DONVITINH''
	                          ),
	                          ''''
	                      ) AS ten_donvichia,
	                      T.phuthu_dungtuyen,
	                      T.phuthu_traituyen
	               FROM   dmuc_thuoc ld
	                      INNER JOIN (
	                               SELECT dtk.id_thuoc,
	                                      0 AS phuthu_dungtuyen,
	                                      0 AS phuthu_traituyen,
	                                      0 AS Gia_ban,
	                                      -1 AS id_thuockho
	                               FROM   t_thuockho dtk
	                               WHERE  dtk.ID_KHO = @IdKho
	                                      AND dtk.SO_LUONG > 0
	                                      AND dtk.GIA_BAN > 0
	                                      AND dbo.trunDateTime(dtk.ngay_hethan) 
	                                          >= dbo .trunDateTime (GETDATE())
	                               GROUP BY
	                                      dtk.id_thuoc
	                           ) AS T
	                           ON  ld.id_thuoc = T.ID_THUOC
	               WHERE  (ld.kieu_thuocvattu = @KieuThuocVT OR @KieuThuocVT = ''-1'')
	                      AND ld.trang_thai = 1
	           ) AS tbl
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
-- Script to Create dbo.Donthuoc_Laythongtin_Thuoctrongkho_Kedon_Old in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.Donthuoc_Laythongtin_Thuoctrongkho_Kedon_Old Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE [dbo].[Donthuoc_Laythongtin_Thuoctrongkho_Kedon_Old]
	@IdKho INT,
	@KieuThuocVT NVARCHAR(50),
	@madoituongkcb NVARCHAR(10),
	@Dungtuyen INT,
	@Noitru INT,
	@MA_KHOA_THIEN NVARCHAR(10)
AS
--TAM REM LAI DUNG THU TUC MOI CHI BOC THUOC TRONG BANG T_THUOC_KHO
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
   PRINT 'dbo.Donthuoc_Laythongtin_Thuoctrongkho_Kedon_Old Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.Donthuoc_Laythongtin_Thuoctrongkho_Kedon_Old Procedure'
END
GO


--  
-- Script to Update dbo.Kcb_KiemtrathanhtoanhetDoituongDV_Truockhinhapvien in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Kcb_KiemtrathanhtoanhetDoituongDV_Truockhinhapvien Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('-- Stored Procedure

ALTER PROCEDURE dbo.Kcb_KiemtrathanhtoanhetDoituongDV_Truockhinhapvien
@id_benhnhan bigint,
@ma_luotkham NVARCHAR(10)

AS
--SELECT kdk.bnhan_chitra
--FROM kcb_dangky_kcb kdk
--WHERE ISNULL(kdk.noitru,0)=0
--AND kdk.id_benhnhan=@id_benhnhan
--AND kdk.ma_luotkham=@ma_luotkham
--AND isnull(kdk.trangthai_thanhtoan,0)=0
--UNION
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
   PRINT 'dbo.Kcb_KiemtrathanhtoanhetDoituongDV_Truockhinhapvien Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Kcb_KiemtrathanhtoanhetDoituongDV_Truockhinhapvien Procedure'
END
GO


--  
-- Script to Create dbo.Noitru_Inphieutamung in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.Noitru_Inphieutamung Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE dbo.Noitru_Inphieutamung
	@IdTamung bigint
AS
SELECT p.ma_luotkham, ten_benhnhan,p.sngay_sinh,p.so_benh_an,gioi_tinh,ten_doituong_kcb,p.dia_chi,mathe_bhyt,
convert(nvarchar,c.id) AS so_phieu,c.so_tien,c.mota_them 
  FROM v_kcb_luotkham p 
,
(select * from noitru_tamung  WHERE id=@IdTamung) AS c
WHERE p.ma_luotkham=c.ma_luotkham
AND p.id_benhnhan=c.id_benhnhan
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Noitru_Inphieutamung Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.Noitru_Inphieutamung Procedure'
END
GO


--  
-- Script to Create dbo.Noitru_KiemtraXoaphieudieutri in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.Noitru_KiemtraXoaphieudieutri Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE [dbo].[Noitru_KiemtraXoaphieudieutri]
	@IdPhieudieutri INT,
	@reval INT OUT
AS
	DECLARE @tonghop INT
	DECLARE @thanhtoan INT
	SET @reval = -1
	
	SELECT @tonghop = ISNULL(MAX(trangthai_tonghop), 0),
	       @thanhtoan = ISNULL(MAX(trangthai_thanhtoan), 0)
	FROM   kcb_donthuoc_chitiet
	WHERE  id_donthuoc IN (SELECT tp.id_donthuoc
	                       FROM   kcb_donthuoc tp
	                       WHERE  tp.id_phieudieutri = @IdPhieudieutri)
	
	IF @tonghop > 0
	    SET @reval = 1
	ELSE
	BEGIN
	    IF @thanhtoan > 0
	        SET @reval = 2
	END
	
	IF @reval > 0
	BEGIN
	    SELECT @tonghop = ISNULL(MAX(trang_thai), 0),
	           @thanhtoan = ISNULL(MAX(trangthai_thanhtoan), 0)
	    FROM   kcb_chidinhcls_chitiet
	    WHERE  id_chidinh IN (SELECT tai.id_chidinh
	                          FROM   kcb_chidinhcls tai
	                          WHERE  tai.id_dieutri = @IdPhieudieutri)
	    
	    IF @tonghop > 0
	        SET @reval = 3
	    ELSE
	    BEGIN
	        IF @thanhtoan > 0
	            SET @reval = 4
	    END
	END
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Noitru_KiemtraXoaphieudieutri Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.Noitru_KiemtraXoaphieudieutri Procedure'
END
GO


--  
-- Script to Update dbo.Noitru_Laydulieucls_Thuoc_Vtth_Saochep in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Noitru_Laydulieucls_Thuoc_Vtth_Saochep Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('ALTER PROC [dbo].[Noitru_Laydulieucls_Thuoc_Vtth_Saochep]
@IdPhieudieutri INT,
@ma_luotkham NVARCHAR(50),
@id_benhnhan INT
AS
BEGIN
SELECT P.*,
	       CASE p.trangthai_thanhtoan
	            WHEN 1 THEN 0
	            WHEN 0 THEN CASE p.trangthai_huy
	                             WHEN 1 THEN 0
	                             ELSE 1
	                        END
	            ELSE 1
	       END AS CHON,
	       dbo.fLaytendichvuthanhtoan(P.id_loaithanhtoan) AS ten_loaithanhtoan,
	       (
	           (ISNULL(p.don_gia, 0) + ISNULL(p.phu_thu, 0)) * ISNULL(p.so_luong, 0)
	       ) AS TT,
	       (
	           (ISNULL(p.bnhan_chitra, 0) + ISNULL(p.phu_thu, 0)) * ISNULL(p.so_luong, 0)
	       ) AS TT_BN,
	       
	          (CASE WHEN isnull(tu_tuc,0)=0 then 
	          	(ISNULL(p.bnhan_chitra, 0) * ISNULL(p.so_luong, 0))
	        ELSE 0 END ) AS TT_BN_KHONG_TUTUC,
	       (ISNULL(p.bnhan_chitra, 0) * ISNULL(p.so_luong, 0)) AS 
	       TT_BN_KHONG_PHUTHU,
	       (ISNULL(p.bhyt_chitra, 0) * ISNULL(p.so_luong, 0)) AS TT_BHYT,
	       (ISNULL(p.don_gia, 0) * ISNULL(p.so_luong, 0)) AS TT_KHONG_PHUTHU,
	       (ISNULL(p.phu_thu, 0) * ISNULL(p.so_luong, 0)) AS TT_PHUTHU
	FROM   (
	           SELECT 2 AS id_loaithanhtoan,
	            0 AS id_kho,
	                  tai.id_chidinh AS id_phieu,
	                  tad.don_gia,
	                  tad.bnhan_chitra,
	                  tad.bhyt_chitra,
	                  tad.tile_chietkhau,
	                  tad.tien_chietkhau,
	                  tad.kieu_chietkhau,
	                  tad.phu_thu,
	                  tad.id_dichvu AS id_dichvu,
	                  tad.id_chitietdichvu AS id_chitietdichvu,
	                  tai.ngay_tao AS CreatedDate,
	                  tad.trangthai_thanhtoan AS trangthai_thanhtoan,
	                  tad.id_chitietchidinh AS id_phieu_chitiet,
	                  tai.ma_luotkham,
	                  tai.id_benhnhan,
	                  (
	                      SELECT TOP 1 lsd.ten_chitietdichvu
	                      FROM   dmuc_dichvucls_chitiet lsd
	                      WHERE  lsd.id_chitietdichvu = tad.id_chitietdichvu
	                  ) AS ten_chitietdichvu,
	                  (
	                      SELECT TOP 1 lsd.ten_bhyt
	                      FROM   dmuc_dichvucls_chitiet lsd
	                      WHERE  lsd.id_chitietdichvu = tad.id_chitietdichvu
	                  ) AS ten_bhyt,
	                  tad.so_luong,
	                  ISNULL(tad.tu_tuc, 0) AS tu_tuc,
	                  ISNULL(tad.trangthai_huy, 0) AS trangthai_huy,
	                  ISNULL(tai.id_phong_chidinh, 0) AS id_phongkham,
	                  ISNULL(tai.id_bacsi_chidinh, 0) AS id_bacsi,
	                  tad.trangthai_bhyt,
	                  2 AS stt_in,
	                  dbo.fLayDonvitinh(2, tad.id_chitietdichvu) AS 
	                  ten_donvitinh,
	                  (
	                      CASE 
	                           WHEN CONVERT(INT, ISNULL(tad.trang_thai, 0))
	                                >= 1 THEN 1
	                           ELSE 0
	                      END
	                  ) AS trang_thai,isnull(tai.id_kham,-1) AS id_kham
	           FROM   kcb_chidinhcls_chitiet tad
	                  JOIN kcb_chidinhcls tai
	                       ON  tai.id_chidinh = tad.id_chidinh
	           WHERE  tai.ma_luotkham = @ma_luotkham
	                  AND tai.id_benhnhan = @id_benhnhan
	                      --  AND (dbo.trunc(tad.Input_Date) BETWEEN dbo.trunc(@FromDate) AND dbo.trunc(@toDate) )
	                  AND ISNULL(tai.noitru, 0) = 1
	                  AND tai.id_dieutri =@IdPhieudieutri
	           
	           UNION	
	           SELECT 3 AS id_loaithanhtoan,
	           tpd.id_kho AS id_kho,
	                  tp.id_donthuoc AS id_phieu,
	                  tpd.don_gia AS don_gia,
	                  tpd.bnhan_chitra,
	                  tpd.bhyt_chitra,
	                   tpd.tile_chietkhau,
	                  tpd.tien_chietkhau,
	                  tpd.kieu_chietkhau,
	                  tpd.phu_thu,
	                  tpd.id_thuoc AS id_dichvu,
	                  tpd.id_thuoc AS id_chitietdichvu,
	                  tp.ngay_tao AS CreatedDate,
	                  tpd.trangthai_thanhtoan AS trangthai_thanhtoan,
	                  tpd.id_chitietdonthuoc AS id_phieu_chitiet,
	                  tp.ma_luotkham,
	                  tp.id_benhnhan,
	                  (
	                      SELECT TOP 1 ld.ten_thuoc
	                      FROM   dmuc_thuoc ld
	                      WHERE  ld.id_thuoc = tpd.id_thuoc
	                  ) AS ten_chitietdichvu,
	                  (
	                      SELECT TOP 1 ld.ten_bhyt
	                      FROM   dmuc_thuoc ld
	                      WHERE  ld.id_thuoc = tpd.id_thuoc
	                  ) AS ten_bhyt,
	                  tpd.so_luong,
	                  tpd.tu_tuc AS tu_tuc,
	                  ISNULL(tpd.trangthai_huy, 0) AS trangthai_huy,
	                  id_phongkham AS id_phongkham,
	                  ISNULL(tp.id_bacsi_chidinh, 0) AS id_bacsi,
	                  tpd.trangthai_bhyt,
	                  tpd.stt_in,
	                  dbo.fLayDonvitinh(3, tpd.id_thuoc) AS ten_donvitinh,
	                  CONVERT(INT, ISNULL(tpd.trang_thai, 0)) AS trang_thai
	                  ,isnull(tp.id_kham,-1) AS id_kham
	           FROM   kcb_donthuoc_chitiet tpd
	                  JOIN kcb_donthuoc tp
	                       ON  tpd.id_donthuoc = tp.id_donthuoc
	           WHERE  NOT EXISTS(
	                      SELECT 1
	                      FROM   kcb_donthuoc
	                      WHERE  id_donthuocthaythe = tp.id_donthuoc
	                  )
	                  AND tp.kieu_donthuoc = 0
	                  AND tp.id_phieudieutri =@IdPhieudieutri
	                  AND ISNULL(tp.noitru, 0) = 1
	                  AND tp.ma_luotkham = @ma_luotkham
	                  AND tp.id_benhnhan = @id_benhnhan
	                  AND ISNULL(tp.donthuoc_taiquay, 0) = 0
	                  AND tp.kieu_thuocvattu=''THUOC''
	                  --AND tpd.id_kho NOT IN (SELECT dk.ID_KHO
	                  --                       FROM   t_dmuc_kho dk
	                  --                       WHERE  dk.la_quaythuoc = 1)
	           UNION--Vat tu tieu hao
	           SELECT 5 AS id_loaithanhtoan,
	           tpd.id_kho AS id_kho,
	                  tp.id_donthuoc AS id_phieu,
	                  tpd.don_gia AS don_gia,
	                  tpd.bnhan_chitra,
	                  tpd.bhyt_chitra,
	                   tpd.tile_chietkhau,
	                  tpd.tien_chietkhau,
	                  tpd.kieu_chietkhau,
	                  tpd.phu_thu,
	                  tpd.id_thuoc AS id_dichvu,
	                  tpd.id_thuoc AS id_chitietdichvu,
	                  tp.ngay_tao AS CreatedDate,
	                  tpd.trangthai_thanhtoan AS trangthai_thanhtoan,
	                  tpd.id_chitietdonthuoc AS id_phieu_chitiet,
	                  tp.ma_luotkham,
	                  tp.id_benhnhan,
	                  (
	                      SELECT TOP 1 ld.ten_thuoc
	                      FROM   dmuc_thuoc ld
	                      WHERE  ld.id_thuoc = tpd.id_thuoc
	                  ) AS ten_chitietdichvu,
	                  (
	                      SELECT TOP 1 ld.ten_bhyt
	                      FROM   dmuc_thuoc ld
	                      WHERE  ld.id_thuoc = tpd.id_thuoc
	                  ) AS ten_bhyt,
	                  tpd.so_luong,
	                  tpd.tu_tuc AS tu_tuc,
	                  ISNULL(tpd.trangthai_huy, 0) AS trangthai_huy,
	                  (
	                      SELECT TOP 1 ls.id_khoa
	                      FROM   dmuc_nhanvien ls
	                      WHERE  ls.id_nhanvien = tp.id_bacsi_chidinh
	                  ) AS id_phongkham,
	                  ISNULL(tp.id_bacsi_chidinh, 0) AS id_bacsi,
	                  tpd.trangthai_bhyt,
	                  tpd.stt_in,
	                  dbo.fLayDonvitinh(3, tpd.id_thuoc) AS ten_donvitinh,
	                  CONVERT(INT, ISNULL(tpd.trang_thai, 0)) AS trang_thai
	                  ,isnull(tp.id_kham,-1) AS id_kham
	           FROM   kcb_donthuoc_chitiet tpd
	                  JOIN kcb_donthuoc tp
	                       ON  tpd.id_donthuoc = tp.id_donthuoc
	           WHERE  NOT EXISTS(
	                      SELECT 1
	                      FROM   kcb_donthuoc
	                      WHERE  id_donthuocthaythe = tp.id_donthuoc
	                  )
	                  AND tp.kieu_donthuoc = 1
	                  AND ISNULL(tp.donthuoc_taiquay, 0) = 0
	                  AND tp.id_phieudieutri =@IdPhieudieutri
	                  AND ISNULL(tp.noitru, 0) = 1
	                  AND tp.kieu_thuocvattu=''VT''
	                  AND tp.ma_luotkham = @ma_luotkham
	                  AND tp.id_benhnhan = @id_benhnhan
	       ) AS p
	ORDER BY
	       p.id_loaithanhtoan,
	       p.ten_chitietdichvu
END
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Noitru_Laydulieucls_Thuoc_Vtth_Saochep Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Noitru_Laydulieucls_Thuoc_Vtth_Saochep Procedure'
END
GO


--  
-- Script to Update dbo.Noitru_Laythongtincls_Thuoc_Theophieudieutri in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Noitru_Laythongtincls_Thuoc_Theophieudieutri Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('ALTER PROC [dbo].[Noitru_Laythongtincls_Thuoc_Theophieudieutri]
@IdBenhnhan INT,
@Maluotkham NVARCHAR(20),
@Idphieudieutri INT
AS

--Chi dinh CLS
SELECT
* FROM v_kcb_chidinhcls tai
WHERE  tai.id_dieutri = @Idphieudieutri
       AND isnull(tai.chidinh_goidichvu,0)=0
       AND tai.ma_luotkham = @Maluotkham
       AND tai.id_benhnhan = @IdBenhnhan
       AND ISNULL(tai.noitru, 0) = 1
       AND ISNULL(id_goi,0)<=0
        AND ISNULL(trong_goi, 0) <= 0
ORDER BY
       tai.stt_hthi_dichvu,tai.stt_hthi_chitiet,tai.ten_chitietdichvu

--Don thuoc
SELECT
*
FROM v_kcb_donthuoc as pres
WHERE  
        Pres.id_benhnhan = @IdBenhnhan
       AND Pres.ma_luotkham = @Maluotkham
       AND Pres.id_phieudieutri = @Idphieudieutri
        AND ISNULL(pres.noitru, 0) = 1
        AND ISNULL(id_goi,0)<=0
         AND ISNULL(pres.trong_goi, 0) <= 0
	       AND pres.kieu_thuocvattu = ''THUOC''

--VTTH       
SELECT
*
FROM v_kcb_donthuoc as pres
WHERE  
        Pres.id_benhnhan = @IdBenhnhan
       AND Pres.ma_luotkham = @Maluotkham
       AND Pres.id_phieudieutri = @Idphieudieutri
        AND ISNULL(pres.noitru, 0) = 1
	       AND pres.kieu_thuocvattu = ''VT''
	       AND ISNULL(pres.id_goi, 0) <= 0
	       AND ISNULL(pres.trong_goi, 0) <= 0
       
--Goi dich vu noi tru
SELECT
* FROM v_kcb_chidinhcls tai
WHERE  tai.id_dieutri = @Idphieudieutri
       AND isnull(tai.chidinh_goidichvu,0)=1
       AND tai.ma_luotkham = @Maluotkham
       AND tai.id_benhnhan = @IdBenhnhan
ORDER BY
       tai.stt_hthi_dichvu,tai.stt_hthi_chitiet,tai.ten_chitietdichvu       
       
--Chandoan KCB
SELECT
*,CONVERT(NVARCHAR(10),ngay_chandoan,103) AS sngay_chandoan
 FROM kcb_chandoan_ketluan tai
WHERE  tai.id_phieudieutri = @Idphieudieutri
       AND tai.ma_luotkham = @Maluotkham
       AND tai.id_benhnhan = @IdBenhnhan
       --AND ISNULL(noitru,0)<=0
ORDER BY
       ngay_chandoan DESC 
       
SELECT *,(SELECT dc.TEN FROM dmuc_chung dc WHERE dc.LOAI=''CHEDODINHDUONG''
AND dc.MA=p.ma_dinhduong) AS ten_dinhduong,
(SELECT dc.STT_HTHI FROM dmuc_chung dc WHERE dc.LOAI=''CHEDODINHDUONG''
AND dc.MA=p.ma_dinhduong) AS STT_HTHI,
(SELECT dc.TEN FROM dmuc_chung dc WHERE dc.LOAI=''HOLY''
AND dc.MA=p.ma_holy) AS ten_holy
FROM noitru_phieudinhduong AS p
WHERE   id_phieudieutri = @Idphieudieutri
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Noitru_Laythongtincls_Thuoc_Theophieudieutri Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Noitru_Laythongtincls_Thuoc_Theophieudieutri Procedure'
END
GO


--  
-- Script to Update dbo.Noitru_Laythongtinphieudieutri_In in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Noitru_Laythongtinphieudieutri_In Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('ALTER PROCEDURE [dbo].[Noitru_Laythongtinphieudieutri_In]
	@lstIdPhieudieutri NVARCHAR(100)
AS
	IF OBJECT_ID(''tempdb..#phieudieutri'') IS NOT NULL
	    DROP TABLE #phieudieutri
	 UPDATE noitru_phieudieutri
	    SET
	    	tthai_in = 1
	    WHERE id_phieudieutri IN (SELECT *
	                       FROM   dbo.fromStringintoIntTable(@lstIdPhieudieutri)) 
	SELECT
	       (
	           CONVERT(VARCHAR(10), tt.ngay_dieutri, 103) + '' '' + tt.gio_dieutri
	       )                    AS NGAY_LAPPHIEU,
	       tt.thongtin_dieutri            AS DIENBIEN,
	       tt.thongtin_theodoi            AS xu_tri,
	       '''' AS                   YLENH,
	       tt.id_phieudieutri,
	       tt.ma_luotkham,
	       tt.id_benhnhan,
	       tt.id_bacsi,
	       tt.tthai_bosung,
	       tt.ngay_tao,
	       tt.nguoi_tao,
	       tt.id_khoanoitru,
	       (SELECT id_buong FROM noitru_phanbuonggiuong np WHERE np.id=tt.id_buong_giuong) AS id_buong,
	       (SELECT id_giuong FROM noitru_phanbuonggiuong np WHERE np.id=tt.id_buong_giuong) AS id_giuong,
	       
	       (
	           SELECT TOP 1 ls.ten_nhanvien
	           FROM   dmuc_nhanvien AS ls
	           WHERE  ls.id_nhanvien = tt.id_bacsi
	       )                    AS ten_bacsidieutri
	       INTO #phieudieutri
	FROM   noitru_phieudieutri tt
	       JOIN kcb_luotkham   AS tpe WITH(NOLOCK) 
	            ON  tpe.ma_luotkham = tt.ma_luotkham
	WHERE  tt.id_phieudieutri IN (SELECT *
	                       FROM   dbo.fromStringintoIntTable(@lstIdPhieudieutri))
	
	
	SELECT P.*,
	ISNULL((SELECT  top 1 dk.ten_khoaphong
         FROM dmuc_khoaphong dk WHERE dk.id_khoaphong=p.id_khoanoitru),'''') AS ten_khoanoitru,
          ISNULL((SELECT  top 1 dk.ten_buong
         FROM noitru_dmuc_buong dk WHERE dk.id_buong=p.id_buong),'''') AS ten_buong,
          ISNULL((SELECT top 1 dk.ten_giuong
         FROM noitru_dmuc_giuongbenh dk WHERE dk.id_giuong=p.id_giuong),'''') AS ten_giuong,
	       tpe.nam_sinh,tpe.Tuoi,
	       tpe.gioi_tinh,
	       tpe.ten_benhnhan,
	       tpe.so_benh_an,tpe.chan_doan  
	FROM   #phieudieutri        AS P
	       , v_kcb_luotkham  AS tpe WITH(NOLOCK) 
	WHERE  p.id_benhnhan =tpe.id_benhnhan
	AND  p.ma_luotkham =tpe.ma_luotkham
	           
	
	SELECT T.*
	FROM   (
	           SELECT tai.id_dieutri  AS id_phieudieutri,
	                  tai.id_dichvu AS ID,
	                   tai.id_chitietdichvu   AS ID_chitiet,
	                  tai.ten_chitietdichvu            AS TEN,
	                  tai.so_luong  AS SOLUONG,
	                  tai.ten_donvitinh  AS DONVI,
	                  2             AS LOAI,
	                  2 AS id_loaithanhtoan,
	                  '''' AS sDesc
	           FROM   v_kcb_chidinhcls tai
	           WHERE  isnull(tai.noitru,0) = 1
	                  AND tai.id_dieutri IN (SELECT *
	                                        FROM   dbo.fromStringintoIntTable(@lstIdPhieudieutri))
	           UNION
	           SELECT tp.id_phieudieutri   ,
	                  tp.id_donthuoc   AS ID,
	                  tp.id_thuoc   AS ID_chitiet,
	                  tp.ten_thuoc        AS TEN,
	                  tp.so_luong  AS SOLUONG,
	                  tp.ten_donvitinh   AS DONVI,
	                  1             AS LOAI,
	                  3 AS id_loaithanhtoan,
	                  tp.mota_them  as sDesc
	           FROM   v_kcb_donthuoc tp
	           WHERE  isnull(tp.noitru,0) = 1
	                  AND tp.id_phieudieutri IN (SELECT *
	                                      FROM   dbo.fromStringintoIntTable(@lstIdPhieudieutri))
	       ) AS T
	ORDER BY
	       T.LOAI ASC
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Noitru_Laythongtinphieudieutri_In Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Noitru_Laythongtinphieudieutri_In Procedure'
END
GO


--  
-- Script to Update dbo.Noitru_Timkiemphieudieutri_Theoluotkham in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Noitru_Timkiemphieudieutri_Theoluotkham Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('ALTER PROCEDURE [dbo].[Noitru_Timkiemphieudieutri_Theoluotkham]
	@UserName NVARCHAR(30),
	@IsAdmin TINYINT,
	@Ngaydieutri NVARCHAR(10),
	@maluotkham NVARCHAR(10),
	@idBenhnhan INT,
	@idKhoanoitru NVARCHAR(100),
	@Songayhienthi INT
AS
DECLARE @dtmNgaydieutri date
SET @dtmNgaydieutri=CONVERT(date,@Ngaydieutri,103)
	SELECT P.*,0 AS CHON,
	       (
	           SELECT TOP 1 dn.ten_nhanvien
	           FROM   dmuc_nhanvien dn
	           WHERE  dn.id_nhanvien = P.id_bacsi
	       ) AS ten_bacsidieutri
	FROM   (
	           SELECT *,CONVERT(NVARCHAR(10),ngay_dieutri,103) AS sngay_dieutri,
	                  (ROW_NUMBER() OVER(ORDER BY np.ngay_dieutri DESC)) AS 
	                  _rownum
	           FROM   noitru_phieudieutri np
	           WHERE 
	           (
	           	@Ngaydieutri=''01/01/1900''--Lay tat ca cac phieu dieu tri 
	           OR (@Songayhienthi>0 AND ngay_dieutri<=@dtmNgaydieutri )--Lay tat ca cac phieu dieu tri truoc do den ngay hien tai
	           OR ngay_dieutri=@dtmNgaydieutri--Chi lay phieu theo ngay hien tai
	           )
	           AND (@IsAdmin=1 OR nguoi_tao=@UserName)
	                  AND  np.ma_luotkham = @maluotkham
	                  AND np.id_benhnhan = @idBenhnhan
	                  AND (@idKhoanoitru=''-1'' or np.id_khoanoitru IN(SELECT * FROM dbo.fromStringintoIntTable(@idKhoanoitru)))
	                  
	       ) AS P
	WHERE  (@Songayhienthi <=0 OR _rownum <= @Songayhienthi)
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Noitru_Timkiemphieudieutri_Theoluotkham Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Noitru_Timkiemphieudieutri_Theoluotkham Procedure'
END
GO


--  
-- Script to Create dbo.Noitru_XoaDinhduong in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.Noitru_XoaDinhduong Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the T_Prescription_Detail table
----------------------------------------------------------------------------------------------------
*/


create PROCEDURE [dbo].[Noitru_XoaDinhduong]
(@lstIdDinhduong NVARCHAR(1000))
AS
	
	DELETE 
	FROM   [dbo].noitru_phieudinhduong
	WHERE  
	 ma_dinhduong IN (SELECT * FROM dbo.fromStringintoIntTable(@lstIdDinhduong) fsit)
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Noitru_XoaDinhduong Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.Noitru_XoaDinhduong Procedure'
END
GO


--  
-- Script to Update dbo.Noitru_Xoaphieudieutri in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Noitru_Xoaphieudieutri Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('ALTER PROCEDURE [dbo].[Noitru_Xoaphieudieutri]
	@IdPhieudieutri INT
AS
	DECLARE @sophieudieutri INT
	DECLARE @maluotkham NVARCHAR(10)
	DECLARE @idbenhnhan BIGINT
	BEGIN TRANSACTION
	DELETE 
	FROM   kcb_chidinhcls_chitiet
	WHERE  id_chidinh IN (SELECT tai.id_chidinh
	                      FROM   kcb_chidinhcls tai
	                      WHERE  tai.id_dieutri = @IdPhieudieutri)
	
	
	DELETE 
	FROM   kcb_chidinhcls
	WHERE  id_dieutri = @IdPhieudieutri
	
	
	DELETE 
	FROM   kcb_donthuoc_chitiet
	WHERE  id_donthuoc IN (SELECT tp.id_donthuoc
	                       FROM   kcb_donthuoc tp
	                       WHERE  tp.id_phieudieutri = @IdPhieudieutri)
	
	DELETE 
	FROM   kcb_donthuoc
	WHERE  id_phieudieutri = @IdPhieudieutri
	
	DELETE 
	FROM   kcb_chandoan_ketluan
	WHERE  id_phieudieutri = @IdPhieudieutri
	
	SET @idbenhnhan = (
	        SELECT id_benhnhan
	        FROM   noitru_phieudieutri WITH (ROWLOCK)
	        WHERE  id_phieudieutri = @IdPhieudieutri
	    )
	
	SET @maluotkham = (
	        SELECT ma_luotkham
	        FROM   noitru_phieudieutri WITH (ROWLOCK)
	        WHERE  id_phieudieutri = @IdPhieudieutri
	    )
	DELETE 
	FROM   noitru_phieudinhduong WITH (ROWLOCK)
	WHERE  id_phieudieutri = @IdPhieudieutri
	
	
	DELETE 
	FROM   noitru_phieudieutri WITH (ROWLOCK)
	WHERE  id_phieudieutri = @IdPhieudieutri
	
	
	SET @sophieudieutri = ISNULL(
	        (
	            SELECT COUNT(*)
	            FROM   noitru_phieudieutri
	            WHERE  noitru_phieudieutri.id_benhnhan = @idbenhnhan
	                   AND ma_luotkham = @maluotkham 
	        ),
	        0
	    )
	
	IF @sophieudieutri <= 0
	    UPDATE kcb_luotkham
	    SET    trangthai_noitru = 1
	
	COMMIT TRANSACTION
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Noitru_Xoaphieudieutri Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Noitru_Xoaphieudieutri Procedure'
END
GO


--  
-- Script to Create dbo.Sys_GetReport in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.Sys_GetReport Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('Create PROCEDURE [dbo].[Sys_GetReport]
@reportfilename nvarchar(30)
WITH RECOMPILE
AS
SELECT * FROM Sys_Reports
WHERE replace(UPPER(file_chuan),''.RPT'','''')=REPLACE(UPPER(@reportfilename),''.RPT'','''')
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Sys_GetReport Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.Sys_GetReport Procedure'
END
GO


--  
-- Script to Update dbo.Thuoc_Laythongtin_Dutruthuoc in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Thuoc_Laythongtin_Dutruthuoc Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('-- Stored Procedure

ALTER PROCEDURE [dbo].[Thuoc_Laythongtin_Dutruthuoc]
	@IDKHO SMALLINT,
	@KIEU_THUOC_VT nvarchar(10)
AS
BEGIN
	DECLARE @Latuthuoc TINYINT
	SET @Latuthuoc=ISNULL((SELECT la_tuthuoc FROM t_dmuc_kho tdk WHERE tdk.id_kho=@IDKHO),0)
	
	SELECT T.*,(CASE WHEN @Latuthuoc=1 AND LEN(ten_donvichia)>0 THEN ten_donvichia ELSE ten_donvitinh END) AS ten_donvitinh1--dung ten nay hien thi tren luoi
	,case  when (T.SO_LUONG-T.SLUONG_TRONGKHO) >0 then  (T.SO_LUONG-T.SLUONG_TRONGKHO) else 0 end   as SLUONG_CANCHUYEN 
	FROM (
		SELECT drugs.*,
			isnull((select soluong_dutru from t_dutru_thuoc where ID_KHO=@IDKHO and ID_THUOC=drugs.id_thuoc AND KIEU_THUOC_VT=@KIEU_THUOC_VT),0) as SO_LUONG,
			isnull((select SUM(SO_LUONG) from t_thuockho where ID_KHO=@IDKHO and ID_THUOC=drugs.id_thuoc  ),0) as SLUONG_TRONGKHO,
			@IDKHO AS ID_KHO,
			(case isnull((select soluong_dutru from t_dutru_thuoc where ID_KHO=@IDKHO and ID_THUOC=drugs.id_thuoc AND KIEU_THUOC_VT=@KIEU_THUOC_VT),0) when 0 then 0 else 1 end) as COQUANHE,
			(CASE tinh_chat WHEN 0 THEN N''Khong d?c h?i'' ELSE N''D?c h?i c?n c?nh bao'' END) as sDrug_Nature,
			(SELECT TOP 1 ten_loaithuoc from dmuc_loaithuoc
			Where id_loaithuoc=drugs.id_loaithuoc) as ten_loaithuoc,
			(SELECT TOP 1 ten from dmuc_chung
			Where MA=drugs.ma_donvitinh) as ten_donvitinh,
			ISNULL((SELECT TOP 1 ten from dmuc_chung
			Where MA=drugs.ma_dvichia),'''') as ten_donvichia
		FROM dmuc_thuoc drugs --LEFT JOIN t_dutru_thuoc AS dutru ON drugs.id_thuoc = dutru.ID_THUOC
		WHERE drugs.trang_thai=1
		AND drugs.kieu_thuocvattu=@KIEU_THUOC_VT
		--AND dutru.ID_KHO = @IDKHO
	)AS T
	ORDER BY COQUANHE desc,T.ten_thuoc,T.ten_donvitinh
END
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Thuoc_Laythongtin_Dutruthuoc Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Thuoc_Laythongtin_Dutruthuoc Procedure'
END
GO


--  
-- Script to Update dbo.Thuoc_Laythuoc_Trongkho_Kedon in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
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

ALTER PROCEDURE [dbo].[Thuoc_Laythuoc_Trongkho_Kedon]
	@id_kho INT,
	@id_thuoc INT,
	@id_thuockho INT,
	@id_loaidoituong_kcb TINYINT,
	@Dungtuyen TINYINT,
	@Noitru TINYINT
AS
	DECLARE @tutuc TINYINT
	DECLARE @latutruc TINYINT
	DECLARE @BHYT_TRAITUYENNGOAITRU_GIADICHVU TINYINT
	SET @BHYT_TRAITUYENNGOAITRU_GIADICHVU = ISNULL(
	        (
	            SELECT TOP 1 dk.sValue
	            FROM   Sys_SystemParameters dk
	            WHERE  dk.sName = ''BHYT_TRAITUYENNGOAITRU_GIADICHVU''
	        ),
	        0
	    )
	
	SET @latutruc = ISNULL(
	        (
	            SELECT TOP 1 la_tuthuoc
	            FROM   t_dmuc_kho tdk
	            WHERE  id_kho = @id_kho
	        ),
	        0
	    )
	
	
	
	DECLARE @THUOC_KIEUXUATTHUOC NVARCHAR(10)
	SET @THUOC_KIEUXUATTHUOC = ISNULL(
	        (
	            SELECT TOP 1 dk.sValue
	            FROM   Sys_SystemParameters dk
	            WHERE  dk.sName = ''THUOC_KIEUXUATTHUOC''
	        ),
	        ''EXP''
	    )
	
	IF @THUOC_KIEUXUATTHUOC = ''STT''
	    SELECT P1.ID_THUOC,P1.so_luong,P1.id_kho,P1.gia_nhap,P1.gia_bhyt,P1.vat,P1.so_lo,P1.ma_nhacungcap,
	                     P1.stt_ban,
	                      P1.ngay_hethan,
	                      P1.ngay_nhap,
	                      P1.sngay_nhap,
	                      P1.phuthu_dungtuyen,
	                      P1.phuthu_traituyen,
	                      P1.id_thuockho,
	                      P1.sngay_hethan,
	                      P1.tu_tuc,
	                      P1.gioihan_kedon,
	                      P1.donvi_but,
	                      P1.co_chiathuoc,
	                      P1.sluong_chia,
	                      P1.dongia_chia,
	                      P1.ma_dvichia,
	           (
	               CASE 
	                    WHEN @latutruc = 0
	               OR @Noitru = 0
	               OR P1.co_chiathuoc = 0 THEN P1.GIA_BAN
	                  ELSE P1.dongia_chia
	                  END
	           ) AS GIA_BAN,
	           (
	               CASE 
	                    WHEN @id_loaidoituong_kcb = 1--Doi uong dich vu
	               OR (P1.tu_tuc = 1 AND @id_loaidoituong_kcb = 0) --Doi tuong BHYT va dung thuoc tu tuc-->Tinh gia Dich vu+Phuthu
	                  THEN 0 ELSE P1.phu_thu END--Doi tuong BHYT dung thuoc khong tu tuc-->Tinh gia BHYT+Phuthu
	           ) AS phu_thu
	    FROM   (
	               SELECT tt.ID_THUOC,tt.so_luong,tt.id_kho,tt.gia_nhap,tt.gia_bhyt,tt.vat,tt.so_lo,tt.ma_nhacungcap,
	                      ISNULL(tt.stt_ban, 10000) AS stt_ban,
	                      tt.ngay_hethan,
	                      tt.ngay_nhap,
	                      tt.phuthu_dungtuyen,
	                      tt.phuthu_traituyen,
	                      tt.id_thuockho,
	                      ISNULL(
	                          (
	                              CASE 
	                                   WHEN @id_loaidoituong_kcb = 1
	                              OR (
	                                     @id_loaidoituong_kcb = 0
	                                     AND @Dungtuyen = 0
	                                     AND @BHYT_TRAITUYENNGOAITRU_GIADICHVU =
	                                         1
	                                     AND @Noitru = 0
	                                 )
	                                 THEN 0
	                                 ELSE (
	                                     CASE 
	                                          WHEN @Dungtuyen = 1 THEN tt.phuthu_dungtuyen
	                                          ELSE tt.phuthu_traituyen
	                                     END
	                                 )
	                                 END
	                          ),
	                          0
	                      ) AS phu_thu,
	                      (
	                          CASE 
	                               WHEN @id_loaidoituong_kcb 
	                                    = 0 THEN (
	                                        CASE 
	                                             WHEN @Dungtuyen 
	                                                  =
	                                                  1
	                                        OR @BHYT_TRAITUYENNGOAITRU_GIADICHVU 
	                                           = 0 
	                                           THEN 
	                                           tt.gia_bhyt
	                                           ELSE 
	                                           tt.GIA_BAN 
	                                           END
	                                    )
	                               ELSE tt.GIA_BAN
	                          END
	                      ) AS Gia_ban,
	                      CONVERT(NVARCHAR(10), tt.ngay_nhap, 103) AS sngay_nhap,
	                      CONVERT(NVARCHAR(10), tt.ngay_hethan, 103) AS 
	                      sngay_hethan,
	                      (
	                          CASE 
	                               WHEN @id_loaidoituong_kcb = 1 THEN 0--Doi tuong dich vu tu tuc luon =0
	                               ELSE ISNULL(ld.tu_tuc, 0)
	                          END
	                      ) tu_tuc,
	                      ISNULL(ld.gioihan_kedon, -1) AS gioihan_kedon,
	                      ISNULL(ld.donvi_but, -1) AS donvi_but,
	                      ISNULL(ld.co_chiathuoc, 0) AS co_chiathuoc,
	                      ISNULL(ld.sluong_chia, 1) AS sluong_chia,
	                      ISNULL(ld.dongia_chia, 0) AS dongia_chia,
	                      ISNULL(ld.ma_dvichia, ld.ma_donvitinh) AS ma_dvichia
	               FROM   t_thuockho tt
	                      INNER JOIN (
	                               SELECT *
	                               FROM   dmuc_thuoc
	                               WHERE  id_thuoc = @id_thuoc
	                           ) AS ld
	                           ON  tt.id_thuoc = ld.id_thuoc
	               WHERE  (@id_thuockho = -1 OR id_thuockho = @id_thuockho)
	                      AND tt.id_kho = @id_kho
	                      AND tt.id_thuoc = @id_thuoc
	                      AND so_luong > 0
	                      AND dbo.trunDateTime(tt.ngay_hethan) >= dbo .trunDateTime (GETDATE())
	           ) AS P1
	    ORDER BY
	           P1.stt_ban,
	           P1.ngay_hethan
	ELSE 
	IF @THUOC_KIEUXUATTHUOC = ''FIFO''
	    SELECT  P1.ID_THUOC,P1.so_luong,P1.id_kho,P1.gia_nhap,P1.gia_bhyt,P1.vat,P1.so_lo,P1.ma_nhacungcap,
	                     P1.stt_ban,
	                      P1.ngay_hethan,
	                      P1.ngay_nhap,
	                      P1.sngay_nhap,
	                      P1.phuthu_dungtuyen,
	                      P1.phuthu_traituyen,
	                      P1.id_thuockho,
	                      P1.sngay_hethan,
	                      P1.tu_tuc,
	                      P1.gioihan_kedon,
	                      P1.donvi_but,
	                      P1.co_chiathuoc,
	                      P1.sluong_chia,
	                      P1.dongia_chia,
	                      P1.ma_dvichia,
	           (
	               CASE 
	                    WHEN @latutruc = 0
	               OR @Noitru = 0
	               OR P1.co_chiathuoc = 0 THEN P1.GIA_BAN
	                  ELSE P1.dongia_chia
	                  END
	           ) AS GIA_BAN,
	           (
	               CASE 
	                    WHEN @id_loaidoituong_kcb = 1--Doi uong dich vu
	               OR (P1.tu_tuc = 1 AND @id_loaidoituong_kcb = 0) --Doi tuong BHYT va dung thuoc tu tuc-->Tinh gia Dich vu+Phuthu
	                  THEN 0 ELSE P1.phu_thu END--Doi tuong BHYT dung thuoc khong tu tuc-->Tinh gia BHYT+Phuthu
	           ) AS phu_thu
	    FROM   (
	               SELECT tt.ID_THUOC,tt.so_luong,tt.id_kho,tt.gia_nhap,tt.gia_bhyt,tt.vat,tt.so_lo,tt.ma_nhacungcap,
	                      ISNULL(tt.stt_ban, 10000) AS stt_ban,
	                      tt.ngay_hethan,
	                      tt.ngay_nhap,
	                      tt.phuthu_dungtuyen,
	                      tt.phuthu_traituyen,
	                      tt.id_thuockho,
	                      ISNULL(
	                          (
	                              CASE 
	                                   WHEN @id_loaidoituong_kcb = 1
	                              OR (
	                                     @id_loaidoituong_kcb = 0
	                                     AND @Dungtuyen = 0
	                                     AND @BHYT_TRAITUYENNGOAITRU_GIADICHVU =
	                                         1
	                                     AND @Noitru = 0
	                                 )
	                                 THEN 0
	                                 ELSE (
	                                     CASE 
	                                          WHEN @Dungtuyen = 1 THEN tt.phuthu_dungtuyen
	                                          ELSE tt.phuthu_traituyen
	                                     END
	                                 )
	                                 END
	                          ),
	                          0
	                      ) AS phu_thu,
	                      (
	                          CASE 
	                               WHEN @id_loaidoituong_kcb 
	                                    = 0 THEN (
	                                        CASE 
	                                             WHEN @Dungtuyen 
	                                                  =
	                                                  1
	                                        OR @BHYT_TRAITUYENNGOAITRU_GIADICHVU 
	                                           = 0 
	                                           THEN 
	                                           tt.gia_bhyt
	                                           ELSE 
	                                           tt.GIA_BAN 
	                                           END
	                                    )
	                               ELSE tt.GIA_BAN
	                          END
	                      ) AS Gia_ban,
	                      CONVERT(NVARCHAR(10), tt.ngay_nhap, 103) AS sngay_nhap,
	                      CONVERT(NVARCHAR(10), tt.ngay_hethan, 103) AS 
	                      sngay_hethan,
	                      (
	                          CASE 
	                               WHEN @id_loaidoituong_kcb = 1 THEN 0--Doi tuong dich vu tu tuc luon =0
	                               ELSE ISNULL(ld.tu_tuc, 0)
	                          END
	                      ) tu_tuc,
	                      ISNULL(ld.gioihan_kedon, -1) AS gioihan_kedon,
	                      ISNULL(ld.donvi_but, -1) AS donvi_but,
	                      ISNULL(ld.co_chiathuoc, 0) AS co_chiathuoc,
	                      ISNULL(ld.sluong_chia, 1) AS sluong_chia,
	                      ISNULL(ld.dongia_chia, 0) AS dongia_chia,
	                      ISNULL(ld.ma_dvichia, ld.ma_donvitinh) AS ma_dvichia
	               FROM   t_thuockho tt
	                      INNER JOIN (
	                               SELECT *
	                               FROM   dmuc_thuoc
	                               WHERE  id_thuoc = @id_thuoc
	                           ) AS ld
	                           ON  tt.id_thuoc = ld.id_thuoc
	               WHERE  (@id_thuockho = -1 OR id_thuockho = @id_thuockho)
	                      AND tt.id_kho = @id_kho
	                      AND tt.id_thuoc = @id_thuoc
	                      AND so_luong > 0
	                      AND dbo.trunDateTime(tt.ngay_hethan) >= dbo .trunDateTime (GETDATE())
	           ) AS P1
	    ORDER BY
	           P1.ngay_nhap,
	           P1.ngay_hethan
	ELSE 
	IF @THUOC_KIEUXUATTHUOC = ''LIFO''
	    SELECT  P1.ID_THUOC,P1.so_luong,P1.id_kho,P1.gia_nhap,P1.gia_bhyt,P1.vat,P1.so_lo,P1.ma_nhacungcap,
	                     P1.stt_ban,
	                      P1.ngay_hethan,
	                      P1.ngay_nhap,
	                      P1.sngay_nhap,
	                      P1.phuthu_dungtuyen,
	                      P1.phuthu_traituyen,
	                      P1.id_thuockho,
	                      P1.sngay_hethan,
	                      P1.tu_tuc,
	                      P1.gioihan_kedon,
	                      P1.donvi_but,
	                      P1.co_chiathuoc,
	                      P1.sluong_chia,
	                      P1.dongia_chia,
	                      P1.ma_dvichia,
	           (
	               CASE 
	                    WHEN @latutruc = 0
	               OR @Noitru = 0
	               OR P1.co_chiathuoc = 0 THEN P1.GIA_BAN
	                  ELSE P1.dongia_chia
	                  END
	           ) AS GIA_BAN,
	           (
	               CASE 
	                    WHEN @id_loaidoituong_kcb = 1--Doi uong dich vu
	               OR (P1.tu_tuc = 1 AND @id_loaidoituong_kcb = 0) --Doi tuong BHYT va dung thuoc tu tuc-->Tinh gia Dich vu+Phuthu
	                  THEN 0 ELSE P1.phu_thu END--Doi tuong BHYT dung thuoc khong tu tuc-->Tinh gia BHYT+Phuthu
	           ) AS phu_thu
	    FROM   (
	               SELECT tt.ID_THUOC,tt.so_luong,tt.id_kho,tt.gia_nhap,tt.gia_bhyt,tt.vat,tt.so_lo,tt.ma_nhacungcap,
	                      ISNULL(tt.stt_ban, 10000) AS stt_ban,
	                      tt.ngay_hethan,
	                      tt.ngay_nhap,
	                      tt.phuthu_dungtuyen,
	                      tt.phuthu_traituyen,
	                      tt.id_thuockho,
	                      ISNULL(
	                          (
	                              CASE 
	                                   WHEN @id_loaidoituong_kcb = 1
	                              OR (
	                                     @id_loaidoituong_kcb = 0
	                                     AND @Dungtuyen = 0
	                                     AND @BHYT_TRAITUYENNGOAITRU_GIADICHVU =
	                                         1
	                                     AND @Noitru = 0
	                                 )
	                                 THEN 0
	                                 ELSE (
	                                     CASE 
	                                          WHEN @Dungtuyen = 1 THEN tt.phuthu_dungtuyen
	                                          ELSE tt.phuthu_traituyen
	                                     END
	                                 )
	                                 END
	                          ),
	                          0
	                      ) AS phu_thu,
	                      (
	                          CASE 
	                               WHEN @id_loaidoituong_kcb 
	                                    = 0 THEN (
	                                        CASE 
	                                             WHEN @Dungtuyen 
	                                                  =
	                                                  1
	                                        OR @BHYT_TRAITUYENNGOAITRU_GIADICHVU 
	                                           = 0 
	                                           THEN 
	                                           tt.gia_bhyt
	                                           ELSE 
	                                           tt.GIA_BAN 
	                                           END
	                                    )
	                               ELSE tt.GIA_BAN
	                          END
	                      ) AS Gia_ban,
	                      CONVERT(NVARCHAR(10), tt.ngay_nhap, 103) AS sngay_nhap,
	                      CONVERT(NVARCHAR(10), tt.ngay_hethan, 103) AS 
	                      sngay_hethan,
	                      (
	                          CASE 
	                               WHEN @id_loaidoituong_kcb = 1 THEN 0--Doi tuong dich vu tu tuc luon =0
	                               ELSE ISNULL(ld.tu_tuc, 0)
	                          END
	                      ) tu_tuc,
	                      ISNULL(ld.gioihan_kedon, -1) AS gioihan_kedon,
	                      ISNULL(ld.donvi_but, -1) AS donvi_but,
	                      ISNULL(ld.co_chiathuoc, 0) AS co_chiathuoc,
	                      ISNULL(ld.sluong_chia, 1) AS sluong_chia,
	                      ISNULL(ld.dongia_chia, 0) AS dongia_chia,
	                      ISNULL(ld.ma_dvichia, ld.ma_donvitinh) AS ma_dvichia
	               FROM   t_thuockho tt
	                      INNER JOIN (
	                               SELECT *
	                               FROM   dmuc_thuoc
	                               WHERE  id_thuoc = @id_thuoc
	                           ) AS ld
	                           ON  tt.id_thuoc = ld.id_thuoc
	               WHERE  (@id_thuockho = -1 OR id_thuockho = @id_thuockho)
	                      AND tt.id_kho = @id_kho
	                      AND tt.id_thuoc = @id_thuoc
	                      AND so_luong > 0
	                      AND dbo.trunDateTime(tt.ngay_hethan) >= dbo .trunDateTime (GETDATE())
	           ) AS P1
	    ORDER BY
	           P1.ngay_nhap desc,
	           P1.ngay_hethan
	ELSE 
	IF @THUOC_KIEUXUATTHUOC = ''EXP''
	     SELECT P1.ID_THUOC,P1.so_luong,P1.id_kho,P1.gia_nhap,P1.gia_bhyt,P1.vat,P1.so_lo,P1.ma_nhacungcap,
	                     P1.stt_ban,
	                      P1.ngay_hethan,
	                      P1.ngay_nhap,
	                      P1.sngay_nhap,
	                      P1.phuthu_dungtuyen,
	                      P1.phuthu_traituyen,
	                      P1.id_thuockho,
	                      P1.sngay_hethan,
	                      P1.tu_tuc,
	                      P1.gioihan_kedon,
	                      P1.donvi_but,
	                      P1.co_chiathuoc,
	                      P1.sluong_chia,
	                      P1.dongia_chia,
	                      P1.ma_dvichia,
	           (
	               CASE 
	                    WHEN @latutruc = 0
	               OR @Noitru = 0
	               OR P1.co_chiathuoc = 0 THEN P1.GIA_BAN
	                  ELSE P1.dongia_chia
	                  END
	           ) AS GIA_BAN,
	           (
	               CASE 
	                    WHEN @id_loaidoituong_kcb = 1--Doi uong dich vu
	               OR (P1.tu_tuc = 1 AND @id_loaidoituong_kcb = 0) --Doi tuong BHYT va dung thuoc tu tuc-->Tinh gia Dich vu+Phuthu
	                  THEN 0 ELSE P1.phu_thu END--Doi tuong BHYT dung thuoc khong tu tuc-->Tinh gia BHYT+Phuthu
	           ) AS phu_thu
	    FROM   (
	               SELECT tt.ID_THUOC,tt.so_luong,tt.id_kho,tt.gia_nhap,tt.gia_bhyt,tt.vat,tt.so_lo,tt.ma_nhacungcap,
	                      ISNULL(tt.stt_ban, 10000) AS stt_ban,
	                      tt.ngay_hethan,
	                      tt.ngay_nhap,
	                      tt.phuthu_dungtuyen,
	                      tt.phuthu_traituyen,
	                      tt.id_thuockho,
	                      ISNULL(
	                          (
	                              CASE 
	                                   WHEN @id_loaidoituong_kcb = 1
	                              OR (
	                                     @id_loaidoituong_kcb = 0
	                                     AND @Dungtuyen = 0
	                                     AND @BHYT_TRAITUYENNGOAITRU_GIADICHVU =
	                                         1
	                                     AND @Noitru = 0
	                                 )
	                                 THEN 0
	                                 ELSE (
	                                     CASE 
	                                          WHEN @Dungtuyen = 1 THEN tt.phuthu_dungtuyen
	                                          ELSE tt.phuthu_traituyen
	                                     END
	                                 )
	                                 END
	                          ),
	                          0
	                      ) AS phu_thu,
	                      (
	                          CASE 
	                               WHEN @id_loaidoituong_kcb 
	                                    = 0 THEN (
	                                        CASE 
	                                             WHEN @Dungtuyen 
	                                                  =
	                                                  1
	                                        OR @BHYT_TRAITUYENNGOAITRU_GIADICHVU 
	                                           = 0 
	                                           THEN 
	                                           tt.gia_bhyt
	                                           ELSE 
	                                           tt.GIA_BAN 
	                                           END
	                                    )
	                               ELSE tt.GIA_BAN
	                          END
	                      ) AS Gia_ban,
	                      CONVERT(NVARCHAR(10), tt.ngay_hethan, 103) AS 
	                      sngay_hethan,
	                      CONVERT(NVARCHAR(10), tt.ngay_nhap, 103) AS sngay_nhap,
	                      (
	                          CASE 
	                               WHEN @id_loaidoituong_kcb = 1 THEN 0--Doi tuong dich vu tu tuc luon =0
	                               ELSE ISNULL(ld.tu_tuc, 0)
	                          END
	                      ) tu_tuc,
	                      ISNULL(ld.gioihan_kedon, -1) AS gioihan_kedon,
	                      ISNULL(ld.donvi_but, -1) AS donvi_but,
	                      ISNULL(ld.co_chiathuoc, 0) AS co_chiathuoc,
	                      ISNULL(ld.sluong_chia, 1) AS sluong_chia,
	                      ISNULL(ld.dongia_chia, 0) AS dongia_chia,
	                      ISNULL(ld.ma_dvichia, ld.ma_donvitinh) AS ma_dvichia
	               FROM   t_thuockho tt
	                      INNER JOIN (
	                               SELECT *
	                               FROM   dmuc_thuoc
	                               WHERE  id_thuoc = @id_thuoc
	                           ) AS ld
	                           ON  tt.id_thuoc = ld.id_thuoc
	               WHERE  (@id_thuockho = -1 OR id_thuockho = @id_thuockho)
	                      AND tt.id_kho = @id_kho
	                      AND tt.id_thuoc = @id_thuoc
	                      AND so_luong > 0
	                      AND dbo.trunDateTime(tt.ngay_hethan) >= dbo .trunDateTime (GETDATE())
	           ) AS P1
	    ORDER BY
	           P1.ngay_hethan,P1.stt_ban
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
-- Script to Update dbo.Thuoc_Laythuoctrongkhoxuat in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
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
--CASE WHEN @LoaiPhieu=1 THEN N''PNK''--phieu nhap kho thuoc
	--					     WHEN @LoaiPhieu=2 THEN N''PXK''--phieu xuat kho thuoc
	--					     WHEN @LoaiPhieu=3 THEN N''PXKBN''--phieu xuat kho benh nhan
	--					     WHEN @LoaiPhieu=4 THEN N''PNTL''--phieu tra thuoc tu kho Le ve kho Chan-->ap dung cho the thuoc kho chan
	--					     WHEN @LoaiPhieu=5 THEN N''PNTKL''--Phieu tra thuoc tu Tu truc khoa noi tru ve kho Le
	--					     WHEN @LoaiPhieu=6 THEN N''PXKHOA''--phieu xuat tu truc khoa noi tru
	--						 WHEN @LoaiPhieu=7 THEN N''PHUY''--Phieu huy
	--						 WHEN @LoaiPhieu=8 THEN N''PTNCC''--Phieu tra nha cung cap
	--						 WHEN @LoaiPhieu=9 THEN N''PXKTL''--Phieu tra thuoc kho Le ve kho Chan-->Ap dung the thuoc kho le
	
	--					     end
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
	                                         WHERE  LOAI_PHIEU IN(2,6)
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
-- Script to Update dbo.Thuoc_Nhapkho_Output in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Thuoc_Nhapkho_Output Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('/************************************************************
 * Code formatted by SoftTree SQL Assistant C v4.7.11
 * Time: 13/09/2013 1:17:47 PM
 ************************************************************/


ALTER PROCEDURE [dbo].[Thuoc_Nhapkho_Output]
	@Ngay_hethan DATETIME,
	@don_gia DECIMAL(18, 2),
	@Gia_ban DECIMAL(18, 2),
	@soluong INT,
	@vat DECIMAL(18, 2),
	@id_thuoc INT,
	@id_kho INT,
	@ma_nhacungcap NVARCHAR(20),
	@so_lo NVARCHAR(30),
	@idthuockho BIGINT,
	@idthuockho_out BIGINT OUTPUT,
	@ngay_nhap DATETIME,
	@gia_bhyt DECIMAL(18, 2),
	@phuthu_dungtuyen DECIMAL(18, 2),
	@phuthu_traituyen DECIMAL(18, 2)
AS
	SET @idthuockho_out = -1
	SET @Ngay_hethan = dbo.trunc(@Ngay_hethan)
	 SELECT @idthuockho_out=ISNULL(id_thuockho,-1)
	       FROM   t_thuockho P
	       WHERE  P.ID_KHO = @id_kho
	              AND P.ID_THUOC = @id_thuoc
	              AND P.ngay_hethan = @Ngay_hethan
	              AND P.gia_nhap = @don_gia
	              AND P.GIA_BAN = @Gia_ban
	              AND P.ma_nhacungcap = @ma_nhacungcap
	              AND P.so_lo = @so_lo
	              AND p.VAT = @vat
	              --AND dbo.trunc(p.ngay_nhap) = dbo.trunc(@ngay_nhap)
	              AND gia_bhyt = @gia_bhyt
	              AND phuthu_dungtuyen = @phuthu_dungtuyen
	              AND phuthu_traituyen = @phuthu_traituyen
	              
	IF @idthuockho_out=-1
	BEGIN
	    INSERT INTO t_thuockho
	      (
	        ID_KHO,
	        ID_THUOC,
	        ngay_hethan,
	        gia_nhap,
	        GIA_BAN,
	        SO_LUONG,
	        VAT,
	        ma_nhacungcap,
	        so_lo,
	        stt_ban,
	        ngay_nhap,
	        gia_bhyt,
	        phuthu_dungtuyen,
	        phuthu_traituyen
	      )
	    VALUES
	      (
	        @id_kho,
	        @id_thuoc,
	        @Ngay_hethan,
	        @don_gia,
	        @Gia_ban,
	        @soluong,
	        @vat,
	        @ma_nhacungcap,
	        @so_lo,
	        100,
	        dbo.trunc(@ngay_nhap),
	        @gia_bhyt,
	        @phuthu_dungtuyen,
	        @phuthu_traituyen
	      )
	    SET @idthuockho_out = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
	    
	    UPDATE t_thuockho
	    SET    SO_LUONG           = ISNULL(so_luong, 0) + ISNULL(@soluong, 0)
	    WHERE  ID_KHO             = @id_kho
	           AND ID_THUOC       = @id_thuoc
	           AND ngay_hethan    = @Ngay_hethan
	           AND gia_nhap       = @don_gia
	           AND vat            = @vat
	           AND GIA_BAN        = @Gia_ban
	           AND ma_nhacungcap  = @ma_nhacungcap
	           AND so_lo          = @so_lo
	           AND VAT            = @vat
	           --AND dbo.trunc(ngay_nhap) = dbo.trunc(@ngay_nhap)
	           AND gia_bhyt = @gia_bhyt
	           AND phuthu_dungtuyen = @phuthu_dungtuyen
	           AND phuthu_traituyen = @phuthu_traituyen
	END
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Thuoc_Nhapkho_Output Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Thuoc_Nhapkho_Output Procedure'
END
GO


--  
-- Script to Create dbo.Thuoc_Nhapkho_tambo in KKYBBNL-PC\SQL2K8.qlbv_tv1 
-- Generated Tuesday, May 26, 2015, at 09:13 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.qlbv_tv1 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.Thuoc_Nhapkho_tambo Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE [dbo].[thuoc_nhapkho]
	@Ngay_hethan DATETIME,
	@don_gia DECIMAL(18, 2),
	@Gia_ban DECIMAL(18, 2),
	@soluong INT,
	@vat DECIMAL(18, 2),
	@id_thuoc INT,
	@id_kho INT,
	@ma_nhacungcap NVARCHAR(20),
	@so_lo NVARCHAR(30),
	@ngay_nhap DATETIME,
	@gia_bhyt DECIMAL(18, 2)
AS
	SET @Ngay_hethan = dbo.trunc(@Ngay_hethan)
	IF NOT EXISTS(
	       SELECT 1
	       FROM   t_thuockho P
	       WHERE  P.ID_KHO = @id_kho
	              AND P.id_thuoc = @id_thuoc
	              AND P.ngay_hethan = @Ngay_hethan
	              AND P.gia_nhap = @don_gia
	              AND P.GIA_BAN = @Gia_ban
	              AND P.ma_nhacungcap = @ma_nhacungcap
	              AND P.so_lo = @so_lo
	              AND p.VAT = @vat
	              AND dbo.trunc(p.ngay_nhap) = dbo.trunc(@ngay_nhap)
	              AND p.gia_bhyt = @gia_bhyt
	   )
	BEGIN
	    INSERT INTO t_thuockho
	      (
	        ID_KHO,
	        ID_THUOC,
	        ngay_hethan,
	        gia_nhap,
	        GIA_BAN,
	        SO_LUONG,
	        VAT,
	        ma_nhacungcap,
	        so_lo,
	        stt_ban,
	        ngay_nhap,
	        gia_bhyt
	      )
	    VALUES
	      (
	        @id_kho,
	        @id_thuoc,
	        @Ngay_hethan,
	        @don_gia,
	        @Gia_ban,
	        @soluong,
	        @vat,
	        @ma_nhacungcap,
	        @so_lo,
	        100,
	        dbo.trunc(@ngay_nhap),
	        @gia_bhyt
	      )
	END
	ELSE
	BEGIN
	    UPDATE t_thuockho
	    SET    SO_LUONG           = ISNULL(so_luong, 0) + ISNULL(@soluong, 0)
	    WHERE  ID_KHO             = @id_kho
	           AND ID_THUOC       = @id_thuoc
	           AND ngay_hethan    = @Ngay_hethan
	           AND gia_nhap       = @don_gia
	           AND vat            = @vat
	           AND GIA_BAN        = @Gia_ban
	           AND ma_nhacungcap  = @ma_nhacungcap
	           AND so_lo          = @so_lo
	           AND VAT            = @vat
	           AND dbo.trunc(ngay_nhap)      = dbo.trunc(@ngay_nhap)
	           AND gia_bhyt       = @gia_bhyt
	END
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Thuoc_Nhapkho_tambo Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.Thuoc_Nhapkho_tambo Procedure'
END
GO
