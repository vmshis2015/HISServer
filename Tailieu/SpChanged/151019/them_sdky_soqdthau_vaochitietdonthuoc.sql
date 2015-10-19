--  
-- Script to Update dbo.Kcb_Thanhtoan_Laydanhsach_Benhnhan_Thanhtoan in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Tuesday, October 20, 2015, at 01:51 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.Kcb_Thanhtoan_Laydanhsach_Benhnhan_Thanhtoan Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('ALTER PROCEDURE [dbo].[Kcb_Thanhtoan_Laydanhsach_Benhnhan_Thanhtoan]
	@id_benhnhan INT ,
	@Maluotkham [nvarchar](20),
	@patientName [nvarchar](100),
	@Tungay [datetime],
	@Denngay [datetime],
	@Madoituongkcb NVARCHAR(50),
    @BHYT INT,
    @noitru TINYINT,
	@KieuTimKiem NVARCHAR(50),
	@MA_KHOA_THIEN NVARCHAR(50),
	@loaiBN NVARCHAR(255)

WITH RECOMPILE
AS
SET @Tungay=dbo.trunc(@Tungay)
SET @Denngay=dbo.trunc(@Denngay)
	SELECT 1 AS CHON,p.*,
	       (
	           SELECT TOP 1 ten_diachinh
	           FROM   dmuc_diachinh
	           WHERE  ma_diachinh = p.ma_tinh_thanhpho
	       ) AS ten_diachinh,
	      
	       (CASE WHEN (SELECT COUNT(*) FROM kcb_thanhtoan tp WHERE tp.ma_luotkham=p.ma_luotkham )<=0 THEN 0
	        ELSE 1
	      
	       END 
	       )AS trangthai_thanhtoan,
	       (
	           SELECT TOP 1 lc.ten_kcbbd
	           FROM   dmuc_noiKCBBD lc
	           WHERE  lc.ma_kcbbd = p.ma_kcbbd
	       ) AS ten_kcbbd

	FROM   v_kcb_luotkham p
	WHERE 
	       (@id_benhnhan = -1 or p.id_benhnhan = @id_benhnhan  )
	       AND(@loaiBN=''ALL'' OR '',''+@loaiBN+'','' LIKE ''%,''+ p.kieu_kham +'',%'')
	       AND (@patientName = '''' OR p.ten_benhnhan LIKE ''%'' + @patientName+''%''
	           )
	       AND (@Maluotkham = '''' OR p.ma_luotkham = @Maluotkham )
	       AND (@Madoituongkcb =''-1'' OR p.ma_doituong_kcb = @Madoituongkcb  )
	       AND 
	       	(@KieuTimKiem=''DANGKY'' AND (
	               (dbo.trunc(p.ngay_tiepdon) BETWEEN @Tungay AND 
	               @Denngay)
	               OR p.ma_luotkham IN (SELECT tre.ma_luotkham
	                        FROM kcb_dangky_kcb tre WHERE DBO.trunc(tre.ngay_dangky)BETWEEN @Tungay AND @Denngay)
	       )
	       OR (@KieuTimKiem=''CLS'' AND ( (dbo.trunc(p.ngay_tiepdon) BETWEEN @Tungay AND 
	               @Denngay) OR p.ma_luotkham IN (SELECT tai.ma_luotkham
	                                                  FROM kcb_chidinhcls tai WHERE dbo.trunc(tai.ngay_tao) BETWEEN @Tungay AND @Denngay))
	               
	       )
	         OR (@KieuTimKiem=''CLS'' AND ( (dbo.trunc(p.ngay_tiepdon) BETWEEN @Tungay AND 
	               @Denngay) OR p.ma_luotkham IN (SELECT tp.ma_luotkham
	                                                  FROM kcb_donthuoc tp WHERE dbo.trunc(tp.ngay_tao) BETWEEN @Tungay AND @Denngay))
	               
	       ))
	      
	       --AND (@BHYT=-1 OR p.dung_tuyen=@BHYT )
	       AND(( @noitru =0 AND ISNULL(p.trangthai_noitru,0)<=1) OR (@noitru =1 AND ISNULL(p.trangthai_noitru,0)>=2))
--	       

	ORDER BY p.ma_luotkham
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Kcb_Thanhtoan_Laydanhsach_Benhnhan_Thanhtoan Procedure Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.Kcb_Thanhtoan_Laydanhsach_Benhnhan_Thanhtoan Procedure'
END
GO


--  
-- Script to Update dbo.Thuoc_Laythuoc_Trongkho_Kedon in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Tuesday, October 20, 2015, at 01:51 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
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
	IF(@THUOC_KIEUXUATTHUOC=''BYSTOCK'')--Lay kieu bien dong theo tung thuoc
	SET @THUOC_KIEUXUATTHUOC = ISNULL(
	        (
	            SELECT TOP 1 dt.kieu_biendong
	            FROM   t_dmuc_kho dt 
	            WHERE  dt.id_kho = @id_kho
	        ),
	        ''EXP''
	    )
	IF(@THUOC_KIEUXUATTHUOC=''BYDRUG'')--Lay kieu bien dong theo tung thuoc
	SET @THUOC_KIEUXUATTHUOC = ISNULL(
	        (
	            SELECT TOP 1 dt.kieu_biendong
	            FROM   dmuc_thuoc dt 
	            WHERE  dt.id_thuoc = @id_thuoc
	        ),
	        ''EXP''
	)
	
	IF @THUOC_KIEUXUATTHUOC = ''STT''
	    SELECT @THUOC_KIEUXUATTHUOC as kieubiendong,P1.ID_THUOC,P1.so_luong,P1.id_kho,P1.gia_nhap,P1.gia_bhyt,P1.vat,P1.so_lo,P1.ma_nhacungcap,P1.so_dky,P1.so_qdinhthau,
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
	               SELECT tt.ID_THUOC,tt.so_luong,tt.id_kho,tt.gia_nhap,tt.gia_bhyt,tt.vat,tt.so_lo,tt.ma_nhacungcap,tt.so_dky,tt.so_qdinhthau,
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
	                      AND ISNULL(tt.chophep_kedon,0)=1
	                      AND tt.id_thuoc = @id_thuoc
	                      AND so_luong > 0
	                      AND dbo.trunDateTime(tt.ngay_hethan) >= dbo .trunDateTime (GETDATE())
	           ) AS P1
	    ORDER BY
	           P1.stt_ban,
	           P1.ngay_hethan
	ELSE 
	IF @THUOC_KIEUXUATTHUOC = ''FIFO''
	    SELECT  @THUOC_KIEUXUATTHUOC as kieubiendong,P1.ID_THUOC,P1.so_luong,P1.id_kho,P1.gia_nhap,P1.gia_bhyt,P1.vat,P1.so_lo,P1.ma_nhacungcap,P1.so_dky,P1.so_qdinhthau,
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
	               SELECT tt.ID_THUOC,tt.so_luong,tt.id_kho,tt.gia_nhap,tt.gia_bhyt,tt.vat,tt.so_lo,tt.ma_nhacungcap,tt.so_dky,tt.so_qdinhthau,
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
	                      AND ISNULL(tt.chophep_kedon,0)=1
	                      AND tt.id_thuoc = @id_thuoc
	                      AND so_luong > 0
	                      AND dbo.trunDateTime(tt.ngay_hethan) >= dbo .trunDateTime (GETDATE())
	           ) AS P1
	    ORDER BY
	           P1.ngay_nhap,
	           P1.ngay_hethan
	ELSE 
	IF @THUOC_KIEUXUATTHUOC = ''LIFO''
	    SELECT  @THUOC_KIEUXUATTHUOC as kieubiendong,P1.ID_THUOC,P1.so_luong,P1.id_kho,P1.gia_nhap,P1.gia_bhyt,P1.vat,P1.so_lo,P1.ma_nhacungcap,P1.so_dky,P1.so_qdinhthau,
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
	               SELECT tt.ID_THUOC,tt.so_luong,tt.id_kho,tt.gia_nhap,tt.gia_bhyt,tt.vat,tt.so_lo,tt.ma_nhacungcap,tt.so_dky,tt.so_qdinhthau,
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
	                      AND ISNULL(tt.chophep_kedon,0)=1
	                      AND tt.id_thuoc = @id_thuoc
	                      AND so_luong > 0
	                      AND dbo.trunDateTime(tt.ngay_hethan) >= dbo .trunDateTime (GETDATE())
	           ) AS P1
	    ORDER BY
	           P1.ngay_nhap desc,
	           P1.ngay_hethan
	ELSE 
	IF @THUOC_KIEUXUATTHUOC = ''EXP''
	     SELECT @THUOC_KIEUXUATTHUOC as kieubiendong,P1.ID_THUOC,P1.so_luong,P1.id_kho,P1.gia_nhap,P1.gia_bhyt,P1.vat,P1.so_lo,P1.ma_nhacungcap,P1.so_dky,P1.so_qdinhthau,
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
	               SELECT tt.ID_THUOC,tt.so_luong,tt.id_kho,tt.gia_nhap,tt.gia_bhyt,tt.vat,tt.so_lo,tt.ma_nhacungcap,tt.so_dky,tt.so_qdinhthau,
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
	                      AND ISNULL(tt.chophep_kedon,0)=1
	                      AND tt.id_thuoc = @id_thuoc
	                      AND so_luong > 0
	                      AND dbo.trunDateTime(tt.ngay_hethan) >= dbo .trunDateTime (GETDATE())
	           ) AS P1
	    ORDER BY
	           P1.ngay_hethan,P1.stt_ban
	           ELSE 
	IF @THUOC_KIEUXUATTHUOC = ''UP''--Gia thap xuat truoc
	     SELECT @THUOC_KIEUXUATTHUOC as kieubiendong,P1.ID_THUOC,P1.so_luong,P1.id_kho,P1.gia_nhap,P1.gia_bhyt,P1.vat,P1.so_lo,P1.ma_nhacungcap,P1.so_dky,P1.so_qdinhthau,
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
	               SELECT tt.ID_THUOC,tt.so_luong,tt.id_kho,tt.gia_nhap,tt.gia_bhyt,tt.vat,tt.so_lo,tt.ma_nhacungcap,tt.so_dky,tt.so_qdinhthau,
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
	                      AND ISNULL(tt.chophep_kedon,0)=1
	                      AND tt.id_thuoc = @id_thuoc
	                      AND so_luong > 0
	                      AND dbo.trunDateTime(tt.ngay_hethan) >= dbo .trunDateTime (GETDATE())
	           ) AS P1
	    ORDER BY
	           P1.Gia_ban ,P1.ngay_hethan,P1.stt_ban
	           ELSE 
	IF @THUOC_KIEUXUATTHUOC = ''DOWN''--gia cao xuat truoc
	     SELECT @THUOC_KIEUXUATTHUOC as kieubiendong,P1.ID_THUOC,P1.so_luong,P1.id_kho,P1.gia_nhap,P1.gia_bhyt,P1.vat,P1.so_lo,P1.ma_nhacungcap,P1.so_dky,P1.so_qdinhthau,
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
	               SELECT tt.ID_THUOC,tt.so_luong,tt.id_kho,tt.gia_nhap,tt.gia_bhyt,tt.vat,tt.so_lo,tt.ma_nhacungcap,tt.so_dky,tt.so_qdinhthau,
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
	                      AND ISNULL(tt.chophep_kedon,0)=1
	                      AND tt.id_thuoc = @id_thuoc
	                      AND so_luong > 0
	                      AND dbo.trunDateTime(tt.ngay_hethan) >= dbo .trunDateTime (GETDATE())
	           ) AS P1
	    ORDER BY
	           P1.Gia_ban desc, P1.ngay_hethan,P1.stt_ban
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
