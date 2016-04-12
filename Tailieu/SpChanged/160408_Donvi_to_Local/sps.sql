--  
-- Script to Update dbo.Chidinhcls_Laythongtin_Chidinhcls_Theoid in KKYBBNL-PC\SQL2K8.BN160408 
-- Generated Friday, April 8, 2016, at 11:05 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.BN160408 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Chidinhcls_Laythongtin_Chidinhcls_Theoid Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('/************************************************************
 * Code formatted by SoftTree SQL Assistant C v6.0.70
 * Time: 14/12/2015 9:28:48 PM
 ************************************************************/

ALTER PROCEDURE [dbo].[Chidinhcls_Laythongtin_Chidinhcls_Theoid]
	@ID INT,
	@Cls_Thuoc NVARCHAR(20)
AS
	IF @Cls_Thuoc = ''DICHVU''
	BEGIN
	    SELECT P.*
	    FROM   (
	               SELECT tad.*,CONVERT(NVARCHAR(10),tad.ngayhen_trakq,103) AS sngayhen_trakq,
	                      (
	                          CASE 
	                               WHEN ISNULL(p.noitru, 0) = 1 THEN tad.ptram_bhyt_goc
	                               ELSE tad.ptram_bhyt
	                          END
	                      ) AS ptram_bhyt_noitru,
	                      0 AS IsNew,
	                      p.ma_chidinh,
	                      0 AS nosave,
	                      (
	                          (ISNULL(tad.don_gia, 0) + ISNULL(tad.phu_thu, 0)) 
	                          * ISNULL(tad.so_luong, 0)
	                      ) AS TT,
	                      (
	                          (ISNULL(tad.bnhan_chitra, 0) + ISNULL(tad.phu_thu, 0)) 
	                          * ISNULL(tad.so_luong, 0)
	                      ) AS TT_BN,
	                      (ISNULL(tad.bnhan_chitra, 0) * ISNULL(tad.so_luong, 0)) AS 
	                      TT_BN_KHONG_PHUTHU,
	                      (ISNULL(tad.bhyt_chitra, 0) * ISNULL(tad.so_luong, 0)) AS 
	                      TT_BHYT,
	                      (ISNULL(tad.don_gia, 0) * ISNULL(tad.so_luong, 0)) AS 
	                      TT_KHONG_PHUTHU,
	                      (ISNULL(tad.phu_thu, 0) * ISNULL(tad.so_luong, 0)) AS 
	                      TT_PHUTHU,
	                      0 AS IsLocked,
	                      (
	                          SELECT TOP 1 ls.ten_dichvu
	                          FROM   dmuc_dichvucls ls WITH (NOLOCK)
	                          WHERE  ls.id_dichvu = tad.id_dichvu
	                      ) AS ten_dichvu,
	                      (
	                          SELECT TOP 1 lsd.ten_chitietdichvu
	                          FROM   dmuc_dichvucls_chitiet lsd WITH (NOLOCK)
	                          WHERE  lsd.id_chitietdichvu = tad.id_chitietdichvu
	                      ) AS ten_chitietdichvu,
	                      (
	                          SELECT TOP 1 lsd.stt_hthi
	                          FROM   dmuc_dichvucls_chitiet lsd WITH (NOLOCK)
	                          WHERE  lsd.id_chitietdichvu = tad.id_chitietdichvu
	                      ) AS stt_hthi_chitiet,
	                      (
	                          SELECT TOP 1 lsd.stt_hthi
	                          FROM   dmuc_dichvucls lsd WITH (NOLOCK)
	                          WHERE  lsd.id_dichvu = tad.id_dichvu
	                      ) AS stt_hthi_dichvu
	               FROM   (
	                          SELECT *
	                          FROM   kcb_chidinhcls_chitiet kcc WITH(NOLOCK)
	                          WHERE  kcc.id_chidinh = @ID
	                      ) AS tad
	                      INNER JOIN (
	                               SELECT *
	                               FROM   kcb_chidinhcls kc WITH(NOLOCK)
	                               WHERE  kc.id_chidinh = @ID
	                           ) AS p
	                           ON  p.id_chidinh = tad.id_chidinh
	               WHERE  tad.id_chidinh > 0
	           ) AS P
	    ORDER BY
	           stt_hthi_dichvu,
	           stt_hthi_chitiet,
	           ten_chitietdichvu
	END
	
	IF @Cls_Thuoc = ''THUOC''
	BEGIN
	    SELECT PresDetail.*,
	           (
	               CASE 
	                    WHEN ISNULL(tp.noitru, 0) = 1 THEN PresDetail.ptram_bhyt_goc
	                    ELSE PresDetail.ptram_bhyt
	               END
	           ) AS ptram_bhyt_noitru,
	           1 AS CHON,
	           -1 AS Detail_ID,
	           '''' AS TotalMoney,
	           0.0 AS TM,
	           (
	               SELECT TOP 1 ten_thuoc
	               FROM   dmuc_thuoc WITH (NOLOCK)
	               WHERE  id_thuoc = PresDetail.id_thuoc
	           ) AS ten_thuoc,
	           1 AS IsNew,
	           0 AS IsLocked,
	           1 AS Nosave,
	           PresDetail.trangthai_huy,
	           PresDetail.tu_tuc,
	           (
	               SELECT TOP 1 ma_thuoc
	               FROM   dmuc_thuoc WITH (NOLOCK)
	               WHERE  id_thuoc = PresDetail.id_thuoc
	           ) AS ma_thuoc,
	           (
	               SELECT TOP 1 ten_loaithuoc
	               FROM   dmuc_loaithuoc WITH (NOLOCK)
	               WHERE  id_loaithuoc IN (SELECT  id_loaithuoc
	                                       FROM   dmuc_thuoc WITH (NOLOCK)
	                                       WHERE  id_thuoc = PresDetail.id_thuoc)
	           ) AS ten_loaithuoc,
	           (
	               SELECT TOP 1 TEN
	               FROM   dmuc_chung WITH (NOLOCK)
	               WHERE  LOAI = ''DONVITINH''
	                      AND MA IN (SELECT  ma_donvitinh 
	                                 FROM   dmuc_thuoc WITH (NOLOCK)
	                                 WHERE  id_thuoc = PresDetail.id_thuoc)
	           ) AS ten_donvitinh,
	           CONVERT(NVARCHAR(10), ngay_hethan, 103) AS sngay_hethan
	    FROM   (
	               SELECT *
	               FROM   kcb_donthuoc_chitiet WITH (NOLOCK)
	               WHERE  id_donthuoc = @ID
	           ) AS PresDetail
	           INNER JOIN (
	                    SELECT *
	                    FROM   kcb_donthuoc kd WITH (NOLOCK)
	                    WHERE  kd.id_donthuoc = @ID
	                ) AS tp
	                ON  tp.id_donthuoc = PresDetail.id_donthuoc
	    WHERE  TP.noitru >= 1
	           AND TP.kieu_donthuoc = 1
	           AND PresDetail.id_donthuoc > 0
	END
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Chidinhcls_Laythongtin_Chidinhcls_Theoid Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Chidinhcls_Laythongtin_Chidinhcls_Theoid Procedure'
END
GO


--  
-- Script to Update dbo.Cls_timkiemthongsoXN_nhapketqua in KKYBBNL-PC\SQL2K8.BN160408 
-- Generated Friday, April 8, 2016, at 11:05 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.BN160408 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Cls_timkiemthongsoXN_nhapketqua Procedure'
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
ALTER PROCEDURE [dbo].[Cls_timkiemthongsoXN_nhapketqua]
	@maluotkham NVARCHAR(10),
	@machidinh NVARCHAR(30),
	@mabenhpham NVARCHAR(30),
	@id_chidinh INT,
	@co_chitiet INT,
	@id_dichvu INT,
	@id_chitietdichvu INT
WITH RECOMPILE
AS
BEGIN
	IF @co_chitiet = 0
	    SELECT ddc.*,p.ten_donvitinh,
	   ISNULL(
           (
               SELECT TEN
               FROM   dmuc_chung
               WHERE  MA =(SELECT TOP 1 ma_quychuan_sosanh FROM dmuc_dichvucls dd WHERE dd.id_dichvu=p.id_dichvu) 
                      AND LOAI = ''QUYCHUAN''
           ),
           N''Unknown''
       ) AS ten_quychuan_sosanh,
       (
           SELECT ten
           FROM   dmuc_chung
           WHERE  MA = (SELECT TOP 1 ma_phuongphapthu FROM dmuc_dichvucls_chitiet dd WHERE dd.id_chitietdichvu=p.id_chitietdichvu)
                  AND LOAI = ''PHUONGPHAPTHUMAU''
       ) AS ten_phuongphapthu,
	           p.id_chidinh,
	           p.id_chitietchidinh,
	           p.id_dichvu,
	           p.id_chitietdichvu,
	           p.barcode,
	           ddc.binhthuong_nam AS bt_nam,
	           ddc.binhthuong_nu AS bt_nu,
	           c.id_kq,
	           c.ket_qua
	    FROM   (
	               SELECT  id_chidinh,
	           id_chitietchidinh,
	           id_dichvu,
	           id_chitietdichvu,
	           barcode,ten_donvitinh
	               FROM   v_kcb_chidinhcls
	               WHERE  id_dichvu = @id_dichvu
	                      AND id_chidinh = @id_chidinh
	           ) p
	           INNER JOIN (
	                    SELECT id_chitietdichvu, ten_chitietdichvu,binhthuong_nam,binhthuong_nu,stt_hthi
	                    FROM   dmuc_dichvucls_chitiet
	                    WHERE  ISNULL(co_chitiet, 0) = 0
	                           AND id_dichvu = @id_dichvu
	                           AND id_chitietdichvu=@id_chitietdichvu
	                ) ddc
	                ON  p.id_chitietdichvu = ddc.id_chitietdichvu
	           LEFT JOIN (
	                    SELECT *
	                    FROM   kcb_ketqua_cls
	                    WHERE  id_chidinh = @id_chidinh
	                ) c
	                ON  ddc.id_chitietdichvu = c.id_dichvuchitiet
	                    --WHERE  p.id_dichvu = @id_dichvu
	                    --AND isnull(ddc.co_chitiet,0)=0
	                    --AND p.id_chidinh=@id_chidinh
	ELSE
	    --Co chi tiet
	    SELECT ddc.*,p.ten_donvitinh,
	   ISNULL(
           (
               SELECT TEN
               FROM   dmuc_chung
               WHERE  MA =(SELECT TOP 1 ma_quychuan_sosanh FROM dmuc_dichvucls dd WHERE dd.id_dichvu=p.id_dichvu) 
                      AND LOAI = ''QUYCHUAN''
           ),
           N''Unknown''
       ) AS ten_quychuan_sosanh,
       (
           SELECT ten
           FROM   dmuc_chung
           WHERE  MA = (SELECT TOP 1 ma_phuongphapthu FROM dmuc_dichvucls_chitiet dd WHERE dd.id_chitietdichvu=p.id_chitietdichvu)
                  AND LOAI = ''PHUONGPHAPTHUMAU''
       ) AS ten_phuongphapthu,
	           p.id_chidinh,
	           p.id_chitietchidinh,
	           p.id_dichvu,
	           p.id_chitietdichvu,
	           p.barcode,
	           ddc.binhthuong_nam AS bt_nam,
	           ddc.binhthuong_nu AS bt_nu,
	           c.id_kq,
	           c.ket_qua
	    FROM 
	    (
	               SELECT id_chidinh,
	           id_chitietchidinh,
	           id_dichvu,
	           id_chitietdichvu,
	           barcode,ten_donvitinh
	               FROM   v_kcb_chidinhcls
	               WHERE  id_dichvu = @id_dichvu
	                      AND id_chidinh = @id_chidinh
	           ) p
	           INNER JOIN (
	                    SELECT id_chitietdichvu,id_cha, ten_chitietdichvu,binhthuong_nam,binhthuong_nu,stt_hthi
	                    FROM   dmuc_dichvucls_chitiet
	                    WHERE  
	                    
	                            id_dichvu = @id_dichvu
	                           AND 
	                           id_cha = @id_chitietdichvu
	                ) ddc
	                ON  p.id_chitietdichvu = ddc.id_cha
	           LEFT JOIN (
	                    SELECT *
	                    FROM   kcb_ketqua_cls
	                    WHERE  id_chidinh = @id_chidinh
	                ) c
	                ON  ddc.id_chitietdichvu = c.id_dichvuchitiet
	                  
	    --v_kcb_chidinhcls p
	    --       INNER JOIN v_dmuc_dichvucls_chitiet ddc
	    --            ON  p.id_chitietdichvu = ddc.id_cha
	    --       LEFT JOIN kcb_ketqua_cls c
	    --            ON  ddc.id_chitietdichvu = c.id_dichvuchitiet
	    --WHERE  ddc.id_cha = @id_chitietdichvu
	    --       AND p.id_chidinh = @id_chidinh
END
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Cls_timkiemthongsoXN_nhapketqua Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Cls_timkiemthongsoXN_nhapketqua Procedure'
END
GO


--  
-- Script to Update dbo.Kcb_Thanhtoan_Laythongtindvu_Chuathanhtoan in KKYBBNL-PC\SQL2K8.BN160408 
-- Generated Friday, April 8, 2016, at 11:05 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.BN160408 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Kcb_Thanhtoan_Laythongtindvu_Chuathanhtoan Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('ALTER PROCEDURE [dbo].[Kcb_Thanhtoan_Laythongtindvu_Chuathanhtoan]
	@ma_luotkham NVARCHAR(20),
	@id_benhnhan INT,
	@Hos_Status INT,
	@MA_KHOA_THIEN NVARCHAR(50),
	@MA_DOI_TUONG NVARCHAR(50)
AS
	DECLARE @idkham INT 
	SET @idkham = ISNULL(
	        (
	            SELECT TOP 1 kdk.id_kham
	            FROM   kcb_dangky_kcb kdk
	            WHERE  kdk.id_benhnhan = @id_benhnhan
	                   AND kdk.ma_luotkham = @ma_luotkham
	        ),
	        -1
	    )
	
	IF OBJECT_ID(''tempdb..#thongtinthuoc'') IS NOT NULL
	    DROP TABLE #thongtinthuoc
	
	SELECT tp.id_donthuoc,
	       tpd.trang_thai,
	       tpd.don_gia,
	       tpd.bnhan_chitra,
	       tpd.bhyt_chitra,
	       tpd.tile_chietkhau,
	       tpd.tien_chietkhau,
	       tpd.kieu_chietkhau,
	       tpd.phu_thu,
	       tpd.id_thuoc,
	       tp.ngay_tao,
	       tpd.trangthai_thanhtoan,
	       tpd.id_chitietdonthuoc,
	       tp.ma_luotkham,
	       tp.id_benhnhan,
	       tpd.so_luong,
	       tpd.tu_tuc AS tu_tuc,
	       tpd.trangthai_huy,
	       tp.id_khoadieutri,
	       id_phongkham,
	       tp.id_bacsi_chidinh,
	       tpd.trangthai_bhyt,
	       tpd.stt_in,
	       tp.id_kham,
	       tp.id_lichsu_doituong_Kcb,
	       tp.mathe_bhyt,
	       tp.kieu_donthuoc,
	       tp.kieu_thuocvattu,
	       tp.noitru
	       INTO #thongtinthuoc
	FROM   (
	           SELECT *
	           FROM   kcb_donthuoc_chitiet kdc WITH(NOLOCK)
	           WHERE  kdc.id_benhnhan = @id_benhnhan
	                  AND kdc.ma_luotkham = @ma_luotkham
	       ) tpd
	       JOIN (
	                SELECT *
	                FROM   kcb_donthuoc kd WITH(NOLOCK)
	                WHERE  kd.id_benhnhan = @id_benhnhan
	                       AND kd.ma_luotkham = @ma_luotkham
	            ) tp
	            ON  tpd.id_donthuoc = tp.id_donthuoc
	WHERE  NOT EXISTS(
	           SELECT 1
	           FROM   kcb_donthuoc WITH(NOLOCK)
	           WHERE  id_donthuocthaythe = tp.id_donthuoc
	       )
	
	IF OBJECT_ID(''tempdb..#thongtindichvu'') IS NOT NULL
	    DROP TABLE #thongtindichvu
	
	SELECT tai.id_chidinh,
	       tad.trang_thai,
	       tad.don_gia,
	       tad.bnhan_chitra,
	       tad.bhyt_chitra,
	       tad.tile_chietkhau,
	       tad.tien_chietkhau,
	       tad.kieu_chietkhau,
	       tad.phu_thu,
	       tad.id_dichvu,
	       tad.id_chitietdichvu,
	       tai.ngay_tao,
	       tad.trangthai_thanhtoan,
	       tad.id_chitietchidinh,
	       tai.ma_luotkham,
	       tai.id_benhnhan,
	       tad.so_luong,
	       tad.tu_tuc,
	       tad.trangthai_huy,
	       tad.id_khoa_thuchien,
	       tai.id_phong_chidinh,
	       tai.id_bacsi_chidinh,
	       tad.trangthai_bhyt,
	       tai.id_kham,
	       tai.id_lichsu_doituong_Kcb,
	       tai.mathe_bhyt,
	       tai.kieu_chidinh,
	       tai.noitru
	       INTO #thongtindichvu
	FROM   (
	           SELECT *
	           FROM   kcb_chidinhcls_chitiet kcc WITH(NOLOCK)
	           WHERE kcc.id_benhnhan = @id_benhnhan AND kcc.ma_luotkham = @ma_luotkham
	       ) tad
	       JOIN (
	                SELECT *
	                FROM   kcb_chidinhcls kc WITH(NOLOCK)
	                WHERE  kc.id_benhnhan = @id_benhnhan
	                       AND kc.ma_luotkham = @ma_luotkham
	            )tai
	            ON  tai.id_chidinh = tad.id_chidinh
	
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
	       (
	           CASE 
	                WHEN ISNULL(tu_tuc, 0) = 0 THEN (ISNULL(p.bnhan_chitra, 0) * ISNULL(p.so_luong, 0))
	                ELSE 0
	           END
	       ) AS TT_BN_KHONG_TUTUC,
	       (
	           CASE 
	                WHEN ISNULL(tu_tuc, 0) = 1 THEN (ISNULL(p.bnhan_chitra, 0) * ISNULL(p.so_luong, 0))
	                ELSE 0
	           END
	       ) AS TT_TUTUC,
	       (ISNULL(p.bnhan_chitra, 0) * ISNULL(p.so_luong, 0)) AS 
	       TT_BN_KHONG_PHUTHU,
	       (ISNULL(p.bhyt_chitra, 0) * ISNULL(p.so_luong, 0)) AS TT_BHYT,
	       (ISNULL(p.don_gia, 0) * ISNULL(p.so_luong, 0)) AS TT_KHONG_PHUTHU,
	       (ISNULL(p.phu_thu, 0) * ISNULL(p.so_luong, 0)) AS TT_PHUTHU
	FROM   (
	           SELECT (
	                      CASE ISNULL(la_phidichvukemtheo, 0)
	                           WHEN 0 THEN 1
	                           ELSE 0
	                      END
	                  ) AS id_loaithanhtoan,
	                  ISNULL(dake_donthuoc, 0) AS dake_donthuoc,
	                  ISNULL(dachidinh_cls, 0) AS dachidinh_cls,
	                  tre.id_kham AS id_phieu,
	                  trang_thai AS trangthai_chuyencls,
	                  tre.don_gia,
	                  1 AS tinh_chiphi,
	                  tre.bnhan_chitra,
	                  tre.bhyt_chitra,
	                  tre.tile_chietkhau,
	                  tre.tien_chietkhau,
	                  tre.kieu_chietkhau,
	                  tre.phu_thu,
	                  tre.id_dichvu_kcb AS id_dichvu,
	                  tre.id_kieukham AS id_chitietdichvu,
	                  tre.ngay_dangky AS CreatedDate,
	                  tre.trangthai_thanhtoan,
	                  tre.id_kham AS id_phieu_chitiet,
	                  tre.ma_luotkham,
	                  tre.id_benhnhan,
	                  (
	                      CASE ISNULL(la_phidichvukemtheo, 0)
	                           WHEN 0 THEN ten_dichvu_kcb
	                           ELSE N''Phi d?ch v? kem theo''
	                      END
	                  ) AS ten_chitietdichvu,
	                  (
	                      CASE ISNULL(la_phidichvukemtheo, 0)
	                           WHEN 0 THEN ten_dichvu_kcb
	                           ELSE N''Phi d?ch v? kem theo''
	                      END
	                  ) AS ten_bhyt,
	                  1 AS so_luong,
	                  ISNULL(tre.tu_tuc, 0) AS tu_tuc,
	                  ISNULL(tre.trangthai_huy, 0) AS trangthai_huy,
	                  ISNULL(id_khoakcb, 0) AS id_khoakcb,
	                  ISNULL(tre.id_phongkham, 0) AS id_phongkham,
	                  ISNULL(tre.id_bacsikham, 0) AS id_bacsi,
	                  1 AS trangthai_bhyt,
	                  1 AS stt_in,
	                  dbo.fLayDonvitinh(1, tre.id_khoakcb) AS ten_donvitinh,
	                  ISNULL(tre.trang_thai, 0) AS trang_thai,
	                  ISNULL(tre.id_kham, -1) AS id_kham,
	                  tre.id_lichsu_doituong_Kcb,
	                  tre.mathe_bhyt
	           FROM   kcb_dangky_kcb tre WITH (NOLOCK)
	           WHERE  tre.id_benhnhan = @id_benhnhan
	                  AND tre.ma_luotkham = @ma_luotkham
	                  AND ISNULL(la_phidichvukemtheo, 0) IN (0, 1)
	                  AND (@Hos_Status = -1 OR ISNULL(tre.noitru, 0) = @Hos_Status)
	           UNION--S? kham
	           SELECT 10 AS id_loaithanhtoan,
	                  0 AS dake_donthuoc,
	                  0 AS dachidinh_cls,
	                  tre.id_sokcb AS id_phieu,
	                  trang_thai AS trangthai_chuyencls,
	                  tre.don_gia,
	                  1 AS tinh_chiphi,
	                  tre.bnhan_chitra,
	                  tre.bhyt_chitra,
	                  0 AS tile_chietkhau,
	                  0 AS tien_chietkhau,
	                  ''%'' AS kieu_chietkhau,
	                  tre.phu_thu,
	                  tre.id_sokcb AS id_dichvu,
	                  tre.id_sokcb AS id_chitietdichvu,
	                  tre.ngay_tao AS CreatedDate,
	                  tre.trangthai_thanhtoan,
	                  tre.id_sokcb AS id_phieu_chitiet,
	                  tre.ma_luotkham,
	                  tre.id_benhnhan,
	                  dc.TEN AS ten_chitietdichvu,
	                  dc.TEN AS ten_bhyt,
	                  1 AS so_luong,
	                  ISNULL(tre.tu_tuc, 0) AS tu_tuc,
	                  0 AS trangthai_huy,
	                  ISNULL(id_khoakcb, 0) AS id_khoakcb,
	                  id_khoakcb AS id_phongkham,
	                  tre.id_nhanvien AS id_bacsi,
	                  1 AS trangthai_bhyt,
	                  1 AS stt_in,
	                  dbo.fLayDonvitinh(10, tre.id_khoakcb) AS ten_donvitinh,
	                  trangthai_thanhtoan AS trang_thai,
	                  ISNULL(tre.id_sokcb, -1) AS id_kham,
	                  tre.id_lichsu_doituong_Kcb,
	                  tre.mathe_bhyt
	           FROM   ( select *  From kcb_dangky_sokham kds WITH(NOLOCK) WHERE kds.id_benhnhan = @id_benhnhan AND kds.ma_luotkham = @ma_luotkham AND (@Hos_Status = -1 OR ISNULL(kds.noitru, 0) = @Hos_Status)) AS tre
	                  INNER JOIN dmuc_chung dc  WITH(NOLOCK) 
	                       ON  tre.ma_sokcb = dc.MA
	           WHERE  dc.LOAI = ''SO_KCB''
	           UNION 
	           -- th?c hi?n vi?c load thong tin c?a phan d?ch v? c?n lam sang
	           SELECT 2 AS id_loaithanhtoan,
	                  0 AS dake_donthuoc,
	                  0 AS dachidinh_cls,
	                  p.id_chidinh AS id_phieu,
	                  p.trang_thai AS trangthai_chuyencls,
	                  p.don_gia,
	                  1 AS tinh_chiphi,
	                  p.bnhan_chitra,
	                  p.bhyt_chitra,
	                  p.tile_chietkhau,
	                  p.tien_chietkhau,
	                  p.kieu_chietkhau,
	                  p.phu_thu,
	                  p.id_dichvu AS id_dichvu,
	                  p.id_chitietdichvu AS id_chitietdichvu,
	                  p.ngay_tao AS CreatedDate,
	                  p.trangthai_thanhtoan AS trangthai_thanhtoan,
	                  p.id_chitietchidinh AS id_phieu_chitiet,
	                  p.ma_luotkham,
	                  p.id_benhnhan,
	                  (
	                      SELECT TOP 1 lsd.ten_chitietdichvu
	                      FROM   dmuc_dichvucls_chitiet lsd  WITH(NOLOCK) 
	                      WHERE  lsd.id_chitietdichvu = p.id_chitietdichvu
	                  ) AS ten_chitietdichvu,
	                  (
	                      SELECT TOP 1 lsd.ten_chitietdichvu_bhyt
	                      FROM   dmuc_dichvucls_chitiet lsd  WITH(NOLOCK) 
	                      WHERE  lsd.id_chitietdichvu = p.id_chitietdichvu
	                  ) AS ten_bhyt,
	                  p.so_luong,
	                  ISNULL(p.tu_tuc, 0) AS tu_tuc,
	                  ISNULL(p.trangthai_huy, 0) AS trangthai_huy,
	                  ISNULL(p.id_khoa_thuchien, 0) AS id_khoakcb,
	                  ISNULL(p.id_phong_chidinh, 0) AS id_phongkham,
	                  ISNULL(p.id_bacsi_chidinh, 0) AS id_bacsi,
	                  p.trangthai_bhyt,
	                  2 AS stt_in,
	                  dbo.fLayDonvitinh(2, p.id_chitietdichvu) AS ten_donvitinh,
	                  (
	                      CASE 
	                           WHEN CONVERT(INT, ISNULL(p.trang_thai, 0))
	                                >= 1 THEN 1
	                           ELSE 0
	                      END
	                  ) AS trang_thai,
	                  ISNULL(p.id_kham, -1) AS id_kham,
	                  p.id_lichsu_doituong_Kcb,
	                  p.mathe_bhyt
	           FROM   #thongtindichvu AS p
	           WHERE  p.ma_luotkham = @ma_luotkham
	                  AND p.id_benhnhan = @id_benhnhan
	                  AND ISNULL(p.kieu_chidinh, 0) =0
	                      --  AND (dbo.trunc(tad.Input_Date) BETWEEN dbo.trunc(@FromDate) AND dbo.trunc(@toDate) )
	                  AND (@Hos_Status = -1 OR ISNULL(p.noitru, 0) = @Hos_Status)
	           --Chi phi them
	           UNION
	           SELECT 12 AS id_loaithanhtoan,
	                  0 AS dake_donthuoc,
	                  0 AS dachidinh_cls,
	                  p.id_chidinh AS id_phieu,
	                  p.trang_thai AS trangthai_chuyencls,
	                  p.don_gia,
	                  1 AS tinh_chiphi,
	                  p.bnhan_chitra,
	                  p.bhyt_chitra,
	                  p.tile_chietkhau,
	                  p.tien_chietkhau,
	                  p.kieu_chietkhau,
	                  p.phu_thu,
	                  p.id_dichvu AS id_dichvu,
	                  p.id_chitietdichvu AS id_chitietdichvu,
	                  p.ngay_tao AS CreatedDate,
	                  p.trangthai_thanhtoan AS trangthai_thanhtoan,
	                  p.id_chitietchidinh AS id_phieu_chitiet,
	                  p.ma_luotkham,
	                  p.id_benhnhan,
	                  (
	                      SELECT TOP 1 lsd.ten_chitietdichvu
	                      FROM   dmuc_dichvucls_chitiet lsd  WITH(NOLOCK) 
	                      WHERE  lsd.id_chitietdichvu = p.id_chitietdichvu
	                  ) AS ten_chitietdichvu,
	                  (
	                      SELECT TOP 1 lsd.ten_chitietdichvu_bhyt
	                      FROM   dmuc_dichvucls_chitiet lsd  WITH(NOLOCK) 
	                      WHERE  lsd.id_chitietdichvu = p.id_chitietdichvu
	                  ) AS ten_bhyt,
	                  p.so_luong,
	                  ISNULL(p.tu_tuc, 0) AS tu_tuc,
	                  ISNULL(p.trangthai_huy, 0) AS trangthai_huy,
	                  ISNULL(p.id_khoa_thuchien, 0) AS id_khoakcb,
	                  ISNULL(p.id_phong_chidinh, 0) AS id_phongkham,
	                  ISNULL(p.id_bacsi_chidinh, 0) AS id_bacsi,
	                  p.trangthai_bhyt,
	                  2 AS stt_in,
	                  dbo.fLayDonvitinh(2, p.id_chitietdichvu) AS ten_donvitinh,
	                  (
	                      CASE 
	                           WHEN CONVERT(INT, ISNULL(p.trang_thai, 0))
	                                >= 1 THEN 1
	                           ELSE 0
	                      END
	                  ) AS trang_thai,
	                  ISNULL(p.id_kham, -1) AS id_kham,
	                  p.id_lichsu_doituong_Kcb,
	                  p.mathe_bhyt
	           FROM   #thongtindichvu AS p
	           WHERE  p.ma_luotkham = @ma_luotkham
	                  AND p.id_benhnhan = @id_benhnhan
	                  AND ISNULL(p.kieu_chidinh, 0) =3
	                      --  AND (dbo.trunc(tad.Input_Date) BETWEEN dbo.trunc(@FromDate) AND dbo.trunc(@toDate) )
	                  AND (@Hos_Status = -1 OR ISNULL(p.noitru, 0) = @Hos_Status)
	           --Chi phi them
	           UNION
	           SELECT 9 AS id_loaithanhtoan,
	                  0 AS dake_donthuoc,
	                  0 AS dachidinh_cls,
	                  p.id_chidinh AS id_phieu,
	                  p.trang_thai AS trangthai_chuyencls,
	                  p.don_gia,
	                  1 AS tinh_chiphi,
	                  p.bnhan_chitra,
	                  p.bhyt_chitra,
	                  p.tile_chietkhau,
	                  p.tien_chietkhau,
	                  p.kieu_chietkhau,
	                  p.phu_thu,
	                  p.id_dichvu AS id_dichvu,
	                  p.id_chitietdichvu AS id_chitietdichvu,
	                  p.ngay_tao AS CreatedDate,
	                  p.trangthai_thanhtoan AS trangthai_thanhtoan,
	                  p.id_chitietchidinh AS id_phieu_chitiet,
	                  p.ma_luotkham,
	                  p.id_benhnhan,
	                  (
	                      SELECT TOP 1 lsd.ten_chitietdichvu
	                      FROM   dmuc_dichvucls_chitiet lsd  WITH(NOLOCK) 
	                      WHERE  lsd.id_chitietdichvu = p.id_chitietdichvu
	                  ) AS ten_chitietdichvu,
	                  (
	                      SELECT TOP 1 lsd.ten_chitietdichvu_bhyt
	                      FROM   dmuc_dichvucls_chitiet lsd  WITH(NOLOCK) 
	                      WHERE  lsd.id_chitietdichvu = p.id_chitietdichvu
	                  ) AS ten_bhyt,
	                  p.so_luong,
	                  ISNULL(p.tu_tuc, 0) AS tu_tuc,
	                  ISNULL(p.trangthai_huy, 0) AS trangthai_huy,
	                  ISNULL(p.id_khoa_thuchien, 0) AS id_khoakcb,
	                  ISNULL(p.id_phong_chidinh, 0) AS id_phongkham,
	                  ISNULL(p.id_bacsi_chidinh, 0) AS id_bacsi,
	                  p.trangthai_bhyt,
	                  2 AS stt_in,
	                  dbo.fLayDonvitinh(2, p.id_chitietdichvu) AS ten_donvitinh,
	                  (
	                      CASE 
	                           WHEN CONVERT(INT, ISNULL(p.trang_thai, 0))
	                                >= 1 THEN 1
	                           ELSE 0
	                      END
	                  ) AS trang_thai,
	                  ISNULL(p.id_kham, -1) AS id_kham,
	                  p.id_lichsu_doituong_Kcb,
	                  p.mathe_bhyt
	           FROM   #thongtindichvu AS p
	           WHERE  p.ma_luotkham = @ma_luotkham
	                  AND p.id_benhnhan = @id_benhnhan
	                  AND ISNULL(p.kieu_chidinh, 0) = 2
	                  AND (@Hos_Status = -1 OR ISNULL(p.noitru, 0) = @Hos_Status)
	           UNION
	           SELECT 11 AS id_loaithanhtoan,
	                  0 AS dake_donthuoc,
	                  0 AS dachidinh_cls,
	                  p.id_chidinh AS id_phieu,
	                  p.trang_thai AS trangthai_chuyencls,
	                  p.don_gia,
	                  1 AS tinh_chiphi,
	                  p.bnhan_chitra,
	                  p.bhyt_chitra,
	                  p.tile_chietkhau,
	                  p.tien_chietkhau,
	                  p.kieu_chietkhau,
	                  p.phu_thu,
	                  p.id_dichvu AS id_dichvu,
	                  p.id_chitietdichvu AS id_chitietdichvu,
	                  p.ngay_tao AS CreatedDate,
	                  p.trangthai_thanhtoan AS trangthai_thanhtoan,
	                  p.id_chitietchidinh AS id_phieu_chitiet,
	                  p.ma_luotkham,
	                  p.id_benhnhan,
	                  (
	                      SELECT TOP 1 lsd.ten_chitietdichvu
	                      FROM   dmuc_dichvucls_chitiet lsd  WITH(NOLOCK) 
	                      WHERE  lsd.id_chitietdichvu = p.id_chitietdichvu
	                  ) AS ten_chitietdichvu,
	                  (
	                      SELECT TOP 1 lsd.ten_chitietdichvu_bhyt
	                      FROM   dmuc_dichvucls_chitiet lsd  WITH(NOLOCK) 
	                      WHERE  lsd.id_chitietdichvu = p.id_chitietdichvu
	                  ) AS ten_bhyt,
	                  p.so_luong,
	                  ISNULL(p.tu_tuc, 0) AS tu_tuc,
	                  ISNULL(p.trangthai_huy, 0) AS trangthai_huy,
	                  ISNULL(p.id_khoa_thuchien, 0) AS id_khoakcb,
	                  ISNULL(p.id_phong_chidinh, 0) AS id_phongkham,
	                  ISNULL(p.id_bacsi_chidinh, 0) AS id_bacsi,
	                  p.trangthai_bhyt,
	                  2 AS stt_in,
	                  dbo.fLayDonvitinh(2, p.id_chitietdichvu) AS ten_donvitinh,
	                  (
	                      CASE 
	                           WHEN CONVERT(INT, ISNULL(p.trang_thai, 0))
	                                >= 1 THEN 1
	                           ELSE 0
	                      END
	                  ) AS trang_thai,
	                  ISNULL(p.id_kham, -1) AS id_kham,
	                  p.id_lichsu_doituong_Kcb,
	                  p.mathe_bhyt
	           FROM   #thongtindichvu AS p
	           WHERE  p.ma_luotkham = @ma_luotkham
	                  AND p.id_benhnhan = @id_benhnhan
	                  AND ISNULL(p.kieu_chidinh, 0) = 4
	                  AND (@Hos_Status = -1 OR ISNULL(p.noitru, 0) = @Hos_Status)
	           UNION	
	           SELECT 3 AS id_loaithanhtoan,
	                  0 AS dake_donthuoc,
	                  0 AS dachidinh_cls,
	                  p.id_donthuoc AS id_phieu,
	                  p.trang_thai AS trangthai_chuyencls,
	                  p.don_gia AS don_gia,
	                  1 AS tinh_chiphi,
	                  p.bnhan_chitra,
	                  p.bhyt_chitra,
	                  p.tile_chietkhau,
	                  p.tien_chietkhau,
	                  p.kieu_chietkhau,
	                  p.phu_thu,
	                  p.id_thuoc AS id_dichvu,
	                  p.id_thuoc AS id_chitietdichvu,
	                  p.ngay_tao AS CreatedDate,
	                  p.trangthai_thanhtoan AS trangthai_thanhtoan,
	                  p.id_chitietdonthuoc AS id_phieu_chitiet,
	                  p.ma_luotkham,
	                  p.id_benhnhan,
	                  (
	                      SELECT TOP 1 ld.ten_thuoc
	                      FROM   dmuc_thuoc ld  WITH(NOLOCK) 
	                      WHERE  ld.id_thuoc = p.id_thuoc
	                  ) AS ten_chitietdichvu,
	                  (
	                      SELECT TOP 1 ld.ten_bhyt
	                      FROM   dmuc_thuoc ld  WITH(NOLOCK) 
	                      WHERE  ld.id_thuoc = p.id_thuoc
	                  ) AS ten_bhyt,
	                  p.so_luong,
	                  p.tu_tuc AS tu_tuc,
	                  ISNULL(p.trangthai_huy, 0) AS trangthai_huy,
	                  ISNULL(p.id_khoadieutri, 0) AS id_khoakcb,
	                  id_phongkham AS id_phongkham,
	                  ISNULL(p.id_bacsi_chidinh, 0) AS id_bacsi,
	                  p.trangthai_bhyt,
	                  p.stt_in,
	                  dbo.fLayDonvitinh(3, p.id_thuoc) AS ten_donvitinh,
	                  CONVERT(INT, ISNULL(p.trang_thai, 0)) AS trang_thai,
	                  ISNULL(p.id_kham, -1) AS id_kham,
	                  p.id_lichsu_doituong_Kcb,
	                  p.mathe_bhyt
	           FROM   #thongtinthuoc AS p
	           WHERE  p.kieu_donthuoc <> 2--Ko lay don thuoc tai quay
	                  AND p.kieu_thuocvattu = ''THUOC''
	                  AND (@Hos_Status = -1 OR ISNULL(p.noitru, 0) = @Hos_Status)
	           
	           
	           
	           
	           UNION--Vat tu tieu hao
	           SELECT 5 AS id_loaithanhtoan,
	                  0 AS dake_donthuoc,
	                  0 AS dachidinh_cls,
	                  p.id_donthuoc AS id_phieu,
	                  p.trang_thai AS trangthai_chuyencls,
	                  p.don_gia AS don_gia,
	                  1 AS tinh_chiphi,
	                  p.bnhan_chitra,
	                  p.bhyt_chitra,
	                  p.tile_chietkhau,
	                  p.tien_chietkhau,
	                  p.kieu_chietkhau,
	                  p.phu_thu,
	                  p.id_thuoc AS id_dichvu,
	                  p.id_thuoc AS id_chitietdichvu,
	                  p.ngay_tao AS CreatedDate,
	                  p.trangthai_thanhtoan AS trangthai_thanhtoan,
	                  p.id_chitietdonthuoc AS id_phieu_chitiet,
	                  p.ma_luotkham,
	                  p.id_benhnhan,
	                  (
	                      SELECT TOP 1 ld.ten_thuoc
	                      FROM   dmuc_thuoc ld  WITH(NOLOCK) 
	                      WHERE  ld.id_thuoc = p.id_thuoc
	                  ) AS ten_chitietdichvu,
	                  (
	                      SELECT TOP 1 ld.ten_bhyt
	                      FROM   dmuc_thuoc ld  WITH(NOLOCK) 
	                      WHERE  ld.id_thuoc = p.id_thuoc
	                  ) AS ten_bhyt,
	                  p.so_luong,
	                  p.tu_tuc AS tu_tuc,
	                  ISNULL(p.trangthai_huy, 0) AS trangthai_huy,
	                  ISNULL(p.id_khoadieutri, 0) AS id_khoakcb,
	                  id_phongkham AS id_phongkham,
	                  ISNULL(p.id_bacsi_chidinh, 0) AS id_bacsi,
	                  p.trangthai_bhyt,
	                  p.stt_in,
	                  dbo.fLayDonvitinh(3, p.id_thuoc) AS ten_donvitinh,
	                  CONVERT(INT, ISNULL(p.trang_thai, 0)) AS trang_thai,
	                  ISNULL(p.id_kham, -1) AS id_kham,
	                  p.id_lichsu_doituong_Kcb,
	                  p.mathe_bhyt
	           FROM   #thongtinthuoc AS p
	           WHERE  p.kieu_donthuoc <> 2--Ko lay don thuoc tai quay
	                  AND p.kieu_thuocvattu = ''VT''
	                  AND (@Hos_Status = -1 OR ISNULL(p.noitru, 0) = @Hos_Status)
	       ) AS p
	ORDER BY
	       p.id_loaithanhtoan,
	       p.ten_chitietdichvu
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Kcb_Thanhtoan_Laythongtindvu_Chuathanhtoan Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Kcb_Thanhtoan_Laythongtindvu_Chuathanhtoan Procedure'
END
GO


--  
-- Script to Update dbo.Kcb_Tiepdon_Laychidinhcls_Khongquaphongkham in KKYBBNL-PC\SQL2K8.BN160408 
-- Generated Friday, April 8, 2016, at 11:05 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.BN160408 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Kcb_Tiepdon_Laychidinhcls_Khongquaphongkham Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('/************************************************************
 * Code formatted by SoftTree SQL Assistant C v6.0.70
 * Time: 14/12/2015 10:26:40 PM
 ************************************************************/


ALTER PROCEDURE [dbo].[Kcb_Tiepdon_Laychidinhcls_Khongquaphongkham]
	@Patient_Code NVARCHAR(50),
	@Patient_ID INT,
	@Exam_ID INT
AS
	SELECT p.id_chidinh,p.ma_chidinh,p.ma_benhpham,p.id_bacsi_chidinh,p.id_benhnhan,p.id_kham,p.ma_luotkham,
	p.id_phong_chidinh,p.id_khoa_chidinh,p.id_doituong_kcb,p.id_loaidoituong_kcb,
	p.noitru,
       0 AS CHON,
       (
           CASE 
                WHEN ISNULL(p.noitru, 0) = 1 THEN c.ptram_bhyt_goc
                ELSE c.ptram_bhyt
           END
       ) AS ptram_bhyt_noitru,
       (
           (ISNULL(c.don_gia, 0) + ISNULL(c.phu_thu, 0)) * ISNULL(c.so_luong, 0)
       ) AS TT,
       (
           (ISNULL(c.bnhan_chitra, 0) + ISNULL(c.phu_thu, 0)) * ISNULL(c.so_luong, 0)
       ) AS TT_BN,
       (ISNULL(c.bnhan_chitra, 0) * ISNULL(c.so_luong, 0)) AS TT_BN_KHONG_PHUTHU,
       (ISNULL(c.bhyt_chitra, 0) * ISNULL(c.so_luong, 0)) AS TT_BHYT,
       (ISNULL(c.don_gia, 0) * ISNULL(c.so_luong, 0)) AS TT_KHONG_PHUTHU,
       (ISNULL(c.phu_thu, 0) * ISNULL(c.so_luong, 0)) AS TT_PHUTHU,
       (
           SELECT ten_khoaphong
           FROM   dmuc_khoaphong
           WHERE  id_khoaphong = p.id_phong_chidinh
       ) AS ten_phongchidinh,
       (
           SELECT ten_khoaphong
           FROM   dmuc_khoaphong
           WHERE  id_khoaphong = p.id_khoa_chidinh
       ) AS ten_khoachidinh,
       (
           SELECT ten_nhanvien
           FROM   dmuc_nhanvien
           WHERE  id_nhanvien = p.id_bacsi_chidinh
       ) AS ten_bacsi_chidinh,
       (
           SELECT ten_doituong_kcb
           FROM   dmuc_doituongkcb
           WHERE  id_doituong_kcb = p.id_doituong_kcb
       ) AS ten_doituong_kcb,
       c.id_chitietchidinh,
       c.id_chitietdichvu,
       c.id_dichvu,
       c.ptram_bhyt,
       c.ptram_bhyt_goc,
       c.gia_danhmuc,
       c.don_gia,
       c.phu_thu,
       c.bhyt_chitra,
       c.bnhan_chitra,
       c.id_loaichidinh,
       c.trangthai_thanhtoan ,--AS tinhtrang_thanhtoan_chitiet,
       c.ngay_thanhtoan ,--AS ngay_thanhtoan_chitiet,
       c.trangthai_huy,
       c.tu_tuc,
       c.so_luong,
       c.trang_thai as trangthai_chuyencls ,
       dbo.fLaytentrangthaicls(isnull(c.trang_thai,0)) as ten_trangthai_chuyencls,
       c.trangthai_bhyt,
       c.hienthi_baocao,
       c.id_thanhtoan,
       c.id_khoa_thuchien,
       c.id_phong_thuchien,
       c.FTPImage,
       d.NHOM_CLS,
       d.ten_khoa_thuchien,
       d.ten_khoa_thuchien_chitiet,
       d.ten_phong_thuchien,
       d.ten_phong_thuchien_chitiet,
       d.chi_dan,
       d.chidan_dichvu,
       d.ma_chitietdichvu,
       d.ma_chitietdichvu_bhyt,
       d.ten_chitietdichvu,
       d.ten_chitietdichvu_bhyt,
       d.ma_dichvu,
       d.ma_bhyt,
       d.ten_dichvu,
       d.ten_bhyt,
       d.ma_loaidichvu,
       d.ten_loaidichvu,
       d.stt_hthi_dichvu,
       d.stt_hthi_chitiet,
       d.co_chitiet,
       d.ten_nhombaocao_chitiet,
       d.ten_donvitinh,
       d.idvungkhaosat,
       d.id_vungkhaosat,
       d.tenvungkhaosat,
       d.ten_vungkhaosat_chitiet,
       d.ten_nhombaocao_dichvu,
       d.ten_nhominphieucls,
       ISNULL(d.nhom_in_cls, ''ALL'') AS nhom_in_cls,
       d.nhom_baocao_dichvu,
       d.stt_hthi_nhominphieucls,
       d.binhthuong_nam,
       d.binhthuong_nu,
       c.id_bacsi_thuchien,
       c.nguoi_thuchien,
       c.ngay_thuchien,
       c.imgPath1,
       c.imgPath2,
       c.imgPath3,
       c.imgPath4,
       c.id_goi,
       ISNULL(c.trong_goi, 0) AS trong_goi,
       isnull(Convert(NVARCHAR(1000), c.ket_qua),'''') AS ket_qua
	FROM  (Select * FROM kcb_chidinhcls kc WITH(NOLOCK) WHERE kc.id_benhnhan = @Patient_ID AND kc.ma_luotkham = @Patient_Code) p
       INNER JOIN (Select * From kcb_chidinhcls_chitiet kcc WITH(NOLOCK) WHERE kcc.id_benhnhan = @Patient_ID AND kcc.ma_luotkham =@Patient_Code) AS c
            ON  p.id_chidinh = c.id_chidinh
       INNER JOIN v_dmuc_dichvucls_chitiet d
            ON  d.id_chitietdichvu = c.id_chitietdichvu
	WHERE  p.id_kham <= 0
	       AND p.noitru = 0
	       AND (p.id_kham = @Exam_ID OR @Exam_ID = 200)
	ORDER BY
	       p.id_chidinh DESC
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Kcb_Tiepdon_Laychidinhcls_Khongquaphongkham Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Kcb_Tiepdon_Laychidinhcls_Khongquaphongkham Procedure'
END
GO


--  
-- Script to Update dbo.sp_KCB_Themmoi_ChitietChidinh in KKYBBNL-PC\SQL2K8.BN160408 
-- Generated Friday, April 8, 2016, at 11:05 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.BN160408 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.sp_KCB_Themmoi_ChitietChidinh Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('/************************************************************
 * Code formatted by SoftTree SQL Assistant C v6.0.70
 * Time: 14/12/2015 11:20:11 AM
 ************************************************************/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_KCB_Themmoi_ChitietChidinh]
	@id_chitietchidinh BIGINT OUTPUT
	 ,
	@id_kham BIGINT
	 ,
	@id_chidinh BIGINT
	 ,
	@id_chidinh_chuyengoi BIGINT
	 ,
	@id_dichvu SMALLINT
	 ,
	@id_chitietdichvu INT
	 ,
	@ptram_bhyt_goc DECIMAL(10, 0)
	 ,
	@ptram_bhyt DECIMAL(10, 0)
	 ,
	@gia_danhmuc MONEY
	 ,
	@madoituong_gia NVARCHAR(5)
	 ,
	@don_gia MONEY
	 ,
	@phu_thu MONEY
	 ,
	@nguoi_tao VARCHAR(20)
	 ,
	@id_loaichidinh TINYINT
	 ,
	@ngay_tao DATETIME
	 ,
	@trangthai_thanhtoan TINYINT
	 ,
	@ngay_thanhtoan DATETIME
	 ,
	@trangthai_huy TINYINT
	 ,
	@tu_tuc TINYINT
	 ,
	@loai_chietkhau TINYINT
	 ,
	@id_doituong_kcb SMALLINT
	 ,
	@id_benhnhan BIGINT
	 ,
	@ma_luotkham NVARCHAR(10)
	 ,
	@so_luong INT
	 ,
	@trang_thai TINYINT
	 ,
	@trangthai_bhyt TINYINT
	 ,
	@hienthi_baocao TINYINT
	 ,
	@bhyt_chitra DECIMAL(18, 3)
	 ,
	@bnhan_chitra DECIMAL(18, 3)
	 ,
	@id_thanhtoan BIGINT
	 ,
	@id_khoa_thuchien SMALLINT
	 ,
	@id_phong_thuchien SMALLINT
	 ,
	@tile_chietkhau DECIMAL(18, 2)
	 ,
	@tien_chietkhau DECIMAL(18, 2)
	 ,
	@kieu_chietkhau NVARCHAR(5)
	 ,
	@id_goi INT
	 ,
	@trong_goi TINYINT
	 ,
	@id_bacsi_thuchien SMALLINT
	 ,
	@nguoi_thuchien NVARCHAR(30)
	 ,
	@ngay_thuchien DATETIME
	 ,
	@imgPath1 NVARCHAR(255)
	 ,
	@imgPath2 NVARCHAR(255)
	 ,
	@imgPath3 NVARCHAR(255)
	 ,
	@imgPath4 NVARCHAR(255)
	 ,
	@FTPImage TINYINT
	 ,
	@ket_qua NVARCHAR(100)
	 ,
	@chidinh_goidichvu TINYINT
	 ,
	@nguon_thanhtoan TINYINT
	 ,
	@ip_maytao NVARCHAR(30)
	 ,
	@ten_maytao NVARCHAR(100)
	 ,
	@chitieu_phantich SMALLINT
	 ,
	@mahoa_mau NVARCHAR(30) OUTPUT
	 ,
	@mau_uutien TINYINT
	 ,
	@ngayhen_trakq DATETIME
	 ,
	@thetichkhoiluong_mau SMALLINT
	 ,
	@tinhtrang_mau NVARCHAR(255)
AS
BEGIN
	DECLARE @Year NVARCHAR(5)
	DECLARE @CurrentYear INT
	DECLARE @Ma_dv NVARCHAR(10)
	DECLARE @MaxSTT INT
	SET @CurrentYear = YEAR(GETDATE()) 
	DELETE 
	FROM   dmuc_mahoamau_kiemnghiem
	WHERE  nam <> @CurrentYear
	
	SET @Ma_dv = ISNULL(
	        (
	            SELECT TOP 1 ma_dichvu
	            FROM   dmuc_dichvucls dd
	            WHERE  dd.id_dichvu = @id_dichvu
	        ),
	        ''UK''
	    )
	
	SET @MaxSTT = ISNULL(
	        (
	            SELECT MAX(Stt)
	            FROM   dmuc_mahoamau_kiemnghiem
	            WHERE  ma_dichvu = @Ma_dv
	                   AND nam = @CurrentYear
	        ),
	        0
	    )
	
	SET @MaxSTT = @MaxSTT + 1
	SET @Year = SUBSTRING(CONVERT(VARCHAR(4), YEAR(GETDATE())), 3, 2)
	SET @mahoa_mau = @Ma_dv + @Year + RIGHT(''0000'' + CONVERT(VARCHAR(4), @MaxSTT), 4)
	IF @MaxSTT = 1
	    INSERT INTO dmuc_mahoamau_kiemnghiem
	      (
	        ma_dichvu,
	        Nam,
	        STT
	      )
	    VALUES
	      (
	        @Ma_dv,
	        @CurrentYear,
	        @MaxSTT
	      )
	ELSE
	    UPDATE dmuc_mahoamau_kiemnghiem
	    SET    STT = @MaxSTT
	    WHERE  ma_dichvu = @Ma_dv
	           AND nam = @CurrentYear
	
	INSERT INTO [kcb_chidinhcls_chitiet]
	  (
	    [id_kham],
	    [id_chidinh],
	    [id_chidinh_chuyengoi],
	    [id_dichvu],
	    [id_chitietdichvu],
	    [ptram_bhyt_goc],
	    [ptram_bhyt],
	    [gia_danhmuc],
	    [madoituong_gia],
	    [don_gia],
	    [phu_thu],
	    [nguoi_tao],
	    [id_loaichidinh],
	    [ngay_tao],
	    [trangthai_thanhtoan],
	    [ngay_thanhtoan],
	    [trangthai_huy],
	    [tu_tuc],
	    [loai_chietkhau],
	    [id_doituong_kcb],
	    [id_benhnhan],
	    [ma_luotkham],
	    [so_luong],
	    [trang_thai],
	    [trangthai_bhyt],
	    [hienthi_baocao],
	    [bhyt_chitra],
	    [bnhan_chitra],
	    [id_thanhtoan],
	    [id_khoa_thuchien],
	    [id_phong_thuchien],
	    [tile_chietkhau],
	    [tien_chietkhau],
	    [kieu_chietkhau],
	    [id_goi],
	    [trong_goi],
	    [id_bacsi_thuchien],
	    [nguoi_thuchien],
	    [ngay_thuchien],
	    [imgPath1],
	    [imgPath2],
	    [imgPath3],
	    [imgPath4],
	    [FTPImage],
	    [ket_qua],
	    [chidinh_goidichvu],
	    [nguon_thanhtoan],
	    [ip_maytao],
	    [ten_maytao],
	    [chitieu_phantich],
	    [mahoa_mau],
	    [mau_uutien],
	    [ngayhen_trakq],
	    [thetichkhoiluong_mau],
	    [tinhtrang_mau]
	  )
	VALUES
	  (
	    @id_kham,
	    @id_chidinh,
	    @id_chidinh_chuyengoi,
	    @id_dichvu,
	    @id_chitietdichvu,
	    @ptram_bhyt_goc,
	    @ptram_bhyt,
	    @gia_danhmuc,
	    @madoituong_gia,
	    @don_gia,
	    @phu_thu,
	    @nguoi_tao,
	    @id_loaichidinh,
	    @ngay_tao,
	    @trangthai_thanhtoan,
	    @ngay_thanhtoan,
	    @trangthai_huy,
	    @tu_tuc,
	    @loai_chietkhau,
	    @id_doituong_kcb,
	    @id_benhnhan,
	    @ma_luotkham,
	    @so_luong,
	    @trang_thai,
	    @trangthai_bhyt,
	    @hienthi_baocao,
	    @bhyt_chitra,
	    @bnhan_chitra,
	    @id_thanhtoan,
	    @id_khoa_thuchien,
	    @id_phong_thuchien,
	    @tile_chietkhau,
	    @tien_chietkhau,
	    @kieu_chietkhau,
	    @id_goi,
	    @trong_goi,
	    @id_bacsi_thuchien,
	    @nguoi_thuchien,
	    @ngay_thuchien,
	    @imgPath1,
	    @imgPath2,
	    @imgPath3,
	    @imgPath4,
	    @FTPImage,
	    @ket_qua,
	    @chidinh_goidichvu,
	    @nguon_thanhtoan,
	    @ip_maytao,
	    @ten_maytao,
	    @chitieu_phantich,
	    @mahoa_mau,
	    @mau_uutien,
	    @ngayhen_trakq,
	    @thetichkhoiluong_mau,
	    @tinhtrang_mau
	  )
	
	SET @id_chitietchidinh = SCOPE_IDENTITY()
END
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.sp_KCB_Themmoi_ChitietChidinh Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.sp_KCB_Themmoi_ChitietChidinh Procedure'
END
GO


--  
-- Script to Create dbo.sp_maukiemnghiem_capnhattrangthai in KKYBBNL-PC\SQL2K8.BN160408 
-- Generated Friday, April 8, 2016, at 11:05 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.BN160408 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.sp_maukiemnghiem_capnhattrangthai Procedure'
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
CREATE PROCEDURE [dbo].[sp_maukiemnghiem_capnhattrangthai]
	@id_chitiet NVARCHAR(500),
	@trangthai_cu TINYINT,
	@trangthai_moi TINYINT
WITH RECOMPILE
AS
UPDATE kcb_chidinhcls_chitiet
SET trang_thai = @trangthai_moi
WHERE  exists (SELECT 1 FROM dbo.fromStringintoIntTable(@id_chitiet) fsit WHERE fsit.Number=kcb_chidinhcls_chitiet.id_chitietchidinh)
AND trang_thai=@trangthai_cu
AND isnull(trangthai_thanhtoan,0)=1
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.sp_maukiemnghiem_capnhattrangthai Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.sp_maukiemnghiem_capnhattrangthai Procedure'
END
GO


--  
-- Script to Create dbo.sp_maukiemnghiem_LayChitietDangky_Kiemnghiem in KKYBBNL-PC\SQL2K8.BN160408 
-- Generated Friday, April 8, 2016, at 11:05 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.BN160408 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.sp_maukiemnghiem_LayChitietDangky_Kiemnghiem Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('/************************************************************
 * Code formatted by SoftTree SQL Assistant C v6.0.70
 * Time: 14/12/2015 9:28:48 PM
 ************************************************************/

CREATE PROCEDURE [dbo].[sp_maukiemnghiem_LayChitietDangky_Kiemnghiem]
	@FromDate NVARCHAR(10),
	@ToDate NVARCHAR(10),
	@Patient_Name NVARCHAR(200),
	@Patient_ID INT,
	@Patient_Code NVARCHAR(10),
	@idDichvu INT,
	@idDichvuChitiet INT,
	@trangthai TINYINT
AS
BEGIN
	SELECT L.*
	FROM   (
	           SELECT tad.*,tad.trang_thai AS trangthai_chuyencls,
	           dbo.fLaytentrangthaicls(isnull(tad.trang_thai,0)) as ten_trangthai_chuyencls,
	                  CONVERT(NVARCHAR(10), tad.ngayhen_trakq, 103) AS 
	                  sngayhen_trakq,
	                  0 AS IsNew,
	                  0 AS nosave,
	                  (
	                      (ISNULL(tad.don_gia, 0) + ISNULL(tad.phu_thu, 0)) 
	                      * ISNULL(tad.so_luong, 0)
	                  ) AS TT,
	                  (
	                      (ISNULL(tad.bnhan_chitra, 0) + ISNULL(tad.phu_thu, 0)) 
	                      * ISNULL(tad.so_luong, 0)
	                  ) AS TT_BN,
	                  (ISNULL(tad.bnhan_chitra, 0) * ISNULL(tad.so_luong, 0)) AS 
	                  TT_BN_KHONG_PHUTHU,
	                  (ISNULL(tad.bhyt_chitra, 0) * ISNULL(tad.so_luong, 0)) AS 
	                  TT_BHYT,
	                  (ISNULL(tad.don_gia, 0) * ISNULL(tad.so_luong, 0)) AS 
	                  TT_KHONG_PHUTHU,
	                  (ISNULL(tad.phu_thu, 0) * ISNULL(tad.so_luong, 0)) AS 
	                  TT_PHUTHU,
	                  0 AS IsLocked,
	                  (
	                      SELECT TOP 1 ls.ten_dichvu
	                      FROM   dmuc_dichvucls ls WITH (NOLOCK)
	                      WHERE  ls.id_dichvu = tad.id_dichvu
	                  ) AS ten_dichvu,
	                  (
	                      SELECT TOP 1 lsd.ten_chitietdichvu
	                      FROM   dmuc_dichvucls_chitiet lsd WITH (NOLOCK)
	                      WHERE  lsd.id_chitietdichvu = tad.id_chitietdichvu
	                  ) AS ten_chitietdichvu,
	                  (
	                      SELECT TOP 1 lsd.stt_hthi
	                      FROM   dmuc_dichvucls_chitiet lsd WITH (NOLOCK)
	                      WHERE  lsd.id_chitietdichvu = tad.id_chitietdichvu
	                  ) AS stt_hthi_chitiet,
	                  (
	                      SELECT TOP 1 lsd.stt_hthi
	                      FROM   dmuc_dichvucls lsd WITH (NOLOCK)
	                      WHERE  lsd.id_dichvu = tad.id_dichvu
	                  ) AS stt_hthi_dichvu,
	                                  p.ten_benhnhan
	           FROM   (
	                      SELECT *
	                      FROM   kcb_chidinhcls_chitiet kcc WITH(NOLOCK)
	                      WHERE  (@idDichvu=-1 OR id_dichvu=@idDichvu)
	                      AND (@idDichvuChitiet=-1 OR id_chitietdichvu=@idDichvuChitiet)
	                      AND ((@trangthai=10 AND trang_thai>=1) OR trang_thai=@trangthai)
	                  ) AS tad
	                  INNER JOIN (
	                           SELECT p1.id_benhnhan,
	                                  p1.ten_benhnhan,
	                                  c1.ma_luotkham
	                           FROM   (
	                                      SELECT id_benhnhan,
	                                             ma_luotkham
	                                      FROM   kcb_luotkham
	                                      WHERE  (
	                                                 @FromDate = ''01/01/1900''
	                                                 OR dbo.fromDatetime2Date(ngay_tiepdon) 
	                                                    BETWEEN dbo.fromstring2Date(@FromDate) 
	                                                    AND dbo.fromstring2Date(@ToDate)
	                                             )
	                                             AND (
	                                                     ma_luotkham LIKE ''%'' + 
	                                                     @Patient_Code + ''%''
	                                                     OR @Patient_Code = ''''
	                                                 )
	                                  ) c1
	                                  INNER JOIN (
	                                           SELECT id_benhnhan,
	                                                  ten_benhnhan
	                                           FROM   kcb_danhsach_benhnhan
	                                           WHERE  (
	                                                      ten_benhnhan = ''''
	                                                      OR ten_benhnhan LIKE 
	                                                         ''%'' + @Patient_Name 
	                                                         +
	                                                         ''%''
	                                                  )
	                                                  AND (id_benhnhan = @Patient_ID OR @Patient_ID = - 1)
	                                       ) p1
	                                       ON  p1.id_benhnhan = c1.id_benhnhan
	                       ) AS P
	                       ON  tad.id_benhnhan = p.id_benhnhan
	                       AND tad.ma_luotkham = p.ma_luotkham
	       ) AS L
	ORDER BY
	       L.mau_uutien desc,
	       L.ngayhen_trakq desc,
	       stt_hthi_dichvu,
	       stt_hthi_chitiet,
	       ten_chitietdichvu
END
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.sp_maukiemnghiem_LayChitietDangky_Kiemnghiem Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.sp_maukiemnghiem_LayChitietDangky_Kiemnghiem Procedure'
END
GO


--  
-- Script to Create dbo.sp_maukiemnghiem_timkiemchitieu in KKYBBNL-PC\SQL2K8.BN160408 
-- Generated Friday, April 8, 2016, at 11:05 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.BN160408 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.sp_maukiemnghiem_timkiemchitieu Procedure'
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
CREATE PROCEDURE [dbo].[sp_maukiemnghiem_timkiemchitieu]
	@mahoamau NVARCHAR(10)
WITH RECOMPILE
AS
DECLARE @id_chidinh BIGINT
DECLARE @id_dichvu SMALLINT
DECLARE @id_chitietdichvu SMALLINT
DECLARE @id_chitietchidinh BIGINT
DECLARE @co_chitiet tinyint
SELECT @id_chitietchidinh=id_chitietchidinh,@id_chitietdichvu=id_chitietdichvu,@id_chidinh=kcc.id_chidinh,@id_dichvu=kcc.id_dichvu,@co_chitiet=0
                   FROM kcb_chidinhcls_chitiet kcc WHERE mahoa_mau=@mahoamau

SELECT kc.id_chidinh,@id_dichvu as id_dichvu,@id_chitietchidinh as id_chitietchidinh, @id_chitietdichvu AS id_chitietdichvu,kc.ma_luotkham,kc.id_benhnhan
,kc.ma_chidinh,kc.ma_benhpham,isnull((SELECT top 1 ddc.co_chitiet
                                        FROM dmuc_dichvucls_chitiet ddc WHERE ddc.id_chitietdichvu=@id_chitietdichvu),0) as co_chitiet
FROM kcb_chidinhcls kc
WHERE id_chidinh=@id_chidinh
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.sp_maukiemnghiem_timkiemchitieu Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.sp_maukiemnghiem_timkiemchitieu Procedure'
END
GO
