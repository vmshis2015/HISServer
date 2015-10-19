--  
-- Script to Update dbo.dmuc_thuoc in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Monday, October 19, 2015, at 10:42 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
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
      ADD [gia_dv] [decimal] (18, 2) NULL
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
-- Script to Update dbo.kcb_donthuoc_chitiet in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Monday, October 19, 2015, at 10:42 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
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
      ADD [so_dky] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[kcb_donthuoc_chitiet]
      ADD [so_qdinhthau] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
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
-- Script to Update dbo.t_biendong_thuoc in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Monday, October 19, 2015, at 10:42 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
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
      ADD [so_dky] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[t_biendong_thuoc]
      ADD [so_qdinhthau] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
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
-- Script to Update dbo.t_dmuc_kho in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Monday, October 19, 2015, at 10:42 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.t_dmuc_kho Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[t_dmuc_kho]
      ADD [loai_kho] [tinyint] NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.t_dmuc_kho Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.t_dmuc_kho Table'
END
GO


--  
-- Script to Update dbo.t_phieu_nhapxuatthuoc_chitiet in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Monday, October 19, 2015, at 10:42 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
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
      ADD [so_dky] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[t_phieu_nhapxuatthuoc_chitiet]
      ADD [so_qdinhthau] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
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
-- Script to Update dbo.t_phieu_xuatthuoc_benhnhan_chitiet in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Monday, October 19, 2015, at 10:42 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.t_phieu_xuatthuoc_benhnhan_chitiet Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[t_phieu_xuatthuoc_benhnhan_chitiet]
      ADD [so_dky] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[t_phieu_xuatthuoc_benhnhan_chitiet]
      ADD [so_qdinhthau] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.t_phieu_xuatthuoc_benhnhan_chitiet Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.t_phieu_xuatthuoc_benhnhan_chitiet Table'
END
GO


--  
-- Script to Update dbo.t_phieutrathuoc_khole_vekhochan_chitiet in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Monday, October 19, 2015, at 10:42 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.t_phieutrathuoc_khole_vekhochan_chitiet Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[t_phieutrathuoc_khole_vekhochan_chitiet]
      ADD [so_dky] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[t_phieutrathuoc_khole_vekhochan_chitiet]
      ADD [so_qdinhthau] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.t_phieutrathuoc_khole_vekhochan_chitiet Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.t_phieutrathuoc_khole_vekhochan_chitiet Table'
END
GO


--  
-- Script to Update dbo.t_thuockho in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Monday, October 19, 2015, at 10:42 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
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
      ALTER COLUMN [so_lo] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[t_thuockho]
      ADD [so_dky] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[t_thuockho]
      ADD [so_qdinhthau] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
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
-- Script to Update dbo.Thuoc_Laychitietphieunhaptrakholevekhochan in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Monday, October 19, 2015, at 10:42 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
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
	      p.GIA_BAN,p.id_phieu,p.so_dky,p.so_qdinhthau,p.don_gia,
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
-- Script to Update dbo.Thuoc_Laychitietphieunhapxuat in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Monday, October 19, 2015, at 10:42 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Thuoc_Laychitietphieunhapxuat Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('ALTER PROCEDURE [dbo].[Thuoc_Laychitietphieunhapxuat]
	@ID_PHIEUNHAP INT
AS
	SELECT P.id_phieuchitiet,
	       P.id_phieu,
	      CONVERT(VARCHAR(10), P.ngay_hethan,103) AS NGAY_HET_HAN,
	      ngay_hethan,ngay_nhap,
	      (CASE WHEN CONVERT(date, P.ngay_hethan,103) >= CONVERT(date,GETDATE(),103) THEN 1
			WHEN CONVERT(date,P.ngay_hethan,103) < CONVERT(date,GETDATE(),103) THEN 0
			END
			) AS IsHetHan,
	       P.ID_THUOC,
	       P.gia_nhap,P.Don_gia,
	       P.GIA_BAN,P.gia_phuthu_dungtuyen,P.gia_phuthu_traituyen,
	       P.gia_bhyt,P.gia_bhyt_cu,
	       P.co_bhyt,
	       P.SO_LUONG,
	       P.SO_LO,P.id_chuyen,
	       P.ma_nhacungcap,
	       (
	           SELECT TOP 1 m.TEN
	           FROM   DMUC_CHUNG m
	           WHERE  m.MA = p.MA_NHACUNGCAP
	           AND m.LOAI=''NHACUNGCAP''
	       ) AS ten_nha_ccap,
	       p.id_thuockho,
	       P.CHIET_KHAU,
	       P.THANH_TIEN,
	       P.VAT,
	       P.THANG_DU,p.so_dky,p.so_qdinhthau,
	       P.mota_them,
	       dbo.strGhepTenthuocBietduoc(LD.ten_thuoc,LD.hoat_chat) AS ten_thuoc_hoatchat,
	       (
	           SELECT TOP 1 m.TEN
	           FROM   DMUC_CHUNG m
	           WHERE  m.MA = ld.ma_donvitinh
	           AND LOAI=''DONVITINH''
	       ) AS   ten_donvitinh,
	       (SELECT ldt.ma_loaithuoc FROM dmuc_loaithuoc AS ldt
	        WHERE ldt.id_loaithuoc = ld.id_loaithuoc
	       ) AS ma_loaithuoc,
	       (SELECT ldt.ten_loaithuoc FROM dmuc_loaithuoc AS ldt
	        WHERE ldt.id_loaithuoc = ld.id_loaithuoc
	       ) AS ten_loaithuoc,
	       ld.*
	FROM   t_phieu_nhapxuatthuoc_chitiet P
	       JOIN dmuc_thuoc ld
	            ON  p.ID_THUOC = LD.id_thuoc
	WHERE  P.id_phieu = @ID_PHIEUNHAP
	ORDER BY ld.ten_thuoc
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Thuoc_Laychitietphieunhapxuat Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Thuoc_Laychitietphieunhapxuat Procedure'
END
GO


--  
-- Script to Update dbo.Thuoc_Laythuoctrongkhoxuat in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Monday, October 19, 2015, at 10:42 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
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
	                  dtk.VAT,dtk.so_dky,dtk.so_qdinhthau,
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
-- Script to Update dbo.Thuoc_Nhapkho_Output in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Monday, October 19, 2015, at 10:42 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
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
	@so_dky NVARCHAR(30),
	@so_qdinhthau NVARCHAR(30),
	@idthuockho BIGINT,
	@idthuockho_out BIGINT OUTPUT,
	@ngay_nhap DATETIME,
	@gia_bhyt DECIMAL(18, 2),
	@phuthu_dungtuyen DECIMAL(18, 2),
	@phuthu_traituyen DECIMAL(18, 2),
	@kieu_thuocvattu NVARCHAR(10)
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
	              AND P.so_dky = @so_dky
	              AND P.so_qdinhthau = @so_qdinhthau
	              AND p.VAT = @vat
	              AND gia_bhyt = @gia_bhyt
	              AND phuthu_dungtuyen = @phuthu_dungtuyen
	              AND phuthu_traituyen = @phuthu_traituyen
	              AND kieu_thuocvattu=@kieu_thuocvattu
	              
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
	        so_lo,so_dky,so_qdinhthau,
	        stt_ban,
	        ngay_nhap,
	        gia_bhyt,
	        phuthu_dungtuyen,
	        phuthu_traituyen,chophep_kedon,kieu_thuocvattu
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
	        @so_lo,@so_dky,@so_qdinhthau,
	        100,
	        dbo.trunc(@ngay_nhap),
	        @gia_bhyt,
	        @phuthu_dungtuyen,
	        @phuthu_traituyen,1,@kieu_thuocvattu
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
	           AND so_dky = @so_dky
	           AND so_qdinhthau = @so_qdinhthau
	           AND VAT            = @vat
	           --AND dbo.trunc(ngay_nhap) = dbo.trunc(@ngay_nhap)
	           AND gia_bhyt = @gia_bhyt
	           AND phuthu_dungtuyen = @phuthu_dungtuyen
	           AND phuthu_traituyen = @phuthu_traituyen
	           AND kieu_thuocvattu=@kieu_thuocvattu
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
