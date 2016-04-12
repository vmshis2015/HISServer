--  
-- Script to Update dbo.v_dmuc_dichvucls_chitiet in KKYBBNL-PC\SQL2K8.BN160408 
-- Generated Friday, April 8, 2016, at 11:00 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.BN160408 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.v_dmuc_dichvucls_chitiet View'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('ALTER VIEW [dbo].[v_dmuc_dichvucls_chitiet]
AS
SELECT p.*,
       0 AS CHON,
       c.ma_dichvu,
       c.ma_bhyt,
       c.ten_dichvu,
       c.ten_bhyt,
       c.dichvu_ktc,
       p.stt_hthi AS stt_hthi_chitiet,
       c.id_vungkhaosat AS idvungkhaosat,
       c.ten_vungkhaosat AS tenvungkhaosat,
       c.hienthi_chitiet,
       c.id_khoa_thuchien AS id_khoa_thuchien_dichvu,
       ISNULL(c.chi_dan, '''') AS chidan_dichvu,
       c.ma_loaidichvu,
       c.ten_loaidichvu,
       c.stt_hthi AS stt_hthi_dichvu,
       c.stt_hthi_loaidvu,
       c.NHOM_CLS,
       ISNULL(
           (
               SELECT ten_khoaphong
               FROM   dmuc_khoaphong
               WHERE  id_khoaphong = p.id_khoa_thuchien
           ),
           ''''
       ) AS ten_khoa_thuchien_chitiet,
       ISNULL(
           (
               SELECT ma_khoaphong
               FROM   dmuc_khoaphong
               WHERE  id_khoaphong = p.id_khoa_thuchien
           ),
           ''''
       ) AS ma_khoa_thuchien_chitiet,
       ISNULL(
           (
               SELECT ten_khoaphong
               FROM   dmuc_khoaphong
               WHERE  id_khoaphong = c.id_khoa_thuchien
           ),
           ''''
       ) AS ten_khoa_thuchien,
       ISNULL(
           (
               SELECT ma_khoaphong
               FROM   dmuc_khoaphong
               WHERE  id_khoaphong = c.id_khoa_thuchien
           ),
           ''''
       ) AS ma_khoa_thuchien,
       ISNULL(
           (
               SELECT ten_khoaphong
               FROM   dmuc_khoaphong
               WHERE  id_khoaphong = p.id_phong_thuchien
           ),
           ''''
       ) AS ten_phong_thuchien_chitiet,
       ISNULL(
           (
               SELECT ma_khoaphong
               FROM   dmuc_khoaphong
               WHERE  id_khoaphong = p.id_phong_thuchien
           ),
           ''''
       ) AS ma_phong_thuchien_chitiet,
       ISNULL(
           (
               SELECT ten_khoaphong
               FROM   dmuc_khoaphong
               WHERE  id_khoaphong = c.id_phong_thuchien
           ),
           ''''
       ) AS ten_phong_thuchien,
       ISNULL(
           (
               SELECT ma_khoaphong
               FROM   dmuc_khoaphong
               WHERE  id_khoaphong = c.id_phong_thuchien
           ),
           ''''
       ) AS ma_phong_thuchien,
       (
           SELECT ten
           FROM   dmuc_chung
           WHERE  MA = p.ma_donvitinh
                  AND LOAI = ''DONVITINH''
       ) AS ten_donvitinh,
       (
           SELECT ten
           FROM   dmuc_chung
           WHERE  MA = p.ma_phuongphapthu
                  AND LOAI = ''PHUONGPHAPTHUMAU''
       ) AS ten_phuongphapthu,
       (
           SELECT TEN
           FROM   dmuc_chung
           WHERE  MA = p.nhom_baocao
                  AND LOAI = ''NHOMBAOCAOCLS''
       ) AS ten_nhombaocao_chitiet,
       '''' AS ten_vungkhaosat_chitiet,
       c.ten_nhombaocao_dichvu,
       c.ten_nhominphieucls,
       c.nhom_in_cls,
       c.nhom_baocao AS nhom_baocao_dichvu,
       c.stt_hthi_nhominphieucls,
       ISNULL(
           (
               SELECT TOP 1 1
               FROM   qhe_doituong_dichvucls
               WHERE  id_chitietdichvu = p.id_chitietdichvu
           ),
           0
       ) AS co_qhe
FROM   dmuc_dichvucls_chitiet p
       INNER JOIN dbo.v_dmuc_dichvucls AS c
            ON  p.id_dichvu = c.id_dichvu
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.v_dmuc_dichvucls_chitiet View Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.v_dmuc_dichvucls_chitiet View'
END
GO
