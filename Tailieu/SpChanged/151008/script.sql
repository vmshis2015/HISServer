--  
-- Script to Update dbo.dmuc_thuoc in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Thursday, October 8, 2015, at 11:07 PM 
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
      ADD [ma_QD40] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[dmuc_thuoc]
      ADD [ma_QDTinh] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[dmuc_thuoc]
      ADD [sluong_vuottran] [int] NULL
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
-- Script to Update dbo.kcb_danhsach_benhnhan in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Thursday, October 8, 2015, at 11:07 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.kcb_danhsach_benhnhan Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[kcb_danhsach_benhnhan]
      ADD [so_tiemchung_QG] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.kcb_danhsach_benhnhan Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.kcb_danhsach_benhnhan Table'
END
GO


--  
-- Script to Update dbo.kcb_luotkham in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Thursday, October 8, 2015, at 11:07 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.kcb_luotkham Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'DF_T_Patient_Exam_Input_Date')
      ALTER TABLE [dbo].[kcb_luotkham] DROP CONSTRAINT [DF_T_Patient_Exam_Input_Date]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'DF_T_Patient_Exam_CorrectLine')
      ALTER TABLE [dbo].[kcb_luotkham] DROP CONSTRAINT [DF_T_Patient_Exam_CorrectLine]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'DF_T_Patient_Exam_Emergency_Hos')
      ALTER TABLE [dbo].[kcb_luotkham] DROP CONSTRAINT [DF_T_Patient_Exam_Emergency_Hos]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'DF_T_Patient_Exam_DisplayOnReport')
      ALTER TABLE [dbo].[kcb_luotkham] DROP CONSTRAINT [DF_T_Patient_Exam_DisplayOnReport]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'DF_T_Patient_Exam_MedicalNumber')
      ALTER TABLE [dbo].[kcb_luotkham] DROP CONSTRAINT [DF_T_Patient_Exam_MedicalNumber]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'DF_T_Patient_Exam_TrangThai_Status')
      ALTER TABLE [dbo].[kcb_luotkham] DROP CONSTRAINT [DF_T_Patient_Exam_TrangThai_Status]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'DF__T_Patient__Hos_s__22FF2F51')
      ALTER TABLE [dbo].[kcb_luotkham] DROP CONSTRAINT [DF__T_Patient__Hos_s__22FF2F51]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'DF_T_Patient_Exam_Locked')
      ALTER TABLE [dbo].[kcb_luotkham] DROP CONSTRAINT [DF_T_Patient_Exam_Locked]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'DF_kcb_luotkham_so_ravien')
      ALTER TABLE [dbo].[kcb_luotkham] DROP CONSTRAINT [DF_kcb_luotkham_so_ravien]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'DF_kcb_luotkham_cach_tao')
      ALTER TABLE [dbo].[kcb_luotkham] DROP CONSTRAINT [DF_kcb_luotkham_cach_tao]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   IF EXISTS (SELECT name FROM sysobjects WHERE name = N'DF_T_Patient_Exam_NGAY_TAO')
      ALTER TABLE [dbo].[kcb_luotkham] DROP CONSTRAINT [DF_T_Patient_Exam_NGAY_TAO]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   CREATE TABLE [dbo].[tmp_kcb_luotkham] (
   [ma_luotkham] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
   [id_benhnhan] [bigint] NOT NULL,
   [ngay_tiepdon] [datetime] NOT NULL CONSTRAINT [DF_T_Patient_Exam_Input_Date] DEFAULT (getdate()),
   [nguoi_tiepdon] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
   [tuoi] [int] NOT NULL,
   [loai_tuoi] [tinyint] NOT NULL,
   [id_doituong_kcb] [smallint] NOT NULL,
   [madoituong_gia] [nvarchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [ma_doituong_kcb] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [id_loaidoituong_kcb] [tinyint] NULL,
   [ptram_bhyt_goc] [decimal] (5, 0) NULL,
   [ptram_bhyt] [decimal] (5, 0) NULL,
   [mathe_bhyt] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [ngaybatdau_bhyt] [datetime] NULL,
   [ngayketthuc_bhyt] [datetime] NULL,
   [noicap_bhyt] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [ma_noicap_bhyt] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [ma_doituong_bhyt] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [ma_quyenloi] [int] NULL,
   [noi_dongtruso_kcbbd] [nvarchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [ma_kcbbd] [varchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [dung_tuyen] [tinyint] NULL CONSTRAINT [DF_T_Patient_Exam_CorrectLine] DEFAULT ((0)),
   [CMT] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [ngay_thanhtoan] [datetime] NULL,
   [luong_coban] [money] NULL,
   [trangthai_capcuu] [int] NULL CONSTRAINT [DF_T_Patient_Exam_Emergency_Hos] DEFAULT ((0)),
   [trieu_chung] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [chan_doan] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [ket_luan] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [huong_dieutri] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [mabenh_chinh] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [mabenh_phu] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [hienthi_baocao] [tinyint] NOT NULL CONSTRAINT [DF_T_Patient_Exam_DisplayOnReport] DEFAULT ((1)),
   [so_benh_an] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_T_Patient_Exam_MedicalNumber] DEFAULT ((-1)),
   [songay_dieutri] [int] NULL,
   [id_khoatiepnhan] [smallint] NULL,
   [solan_kham] [smallint] NULL,
   [stt_kham] [int] NULL,
   [noitru] [tinyint] NULL CONSTRAINT [DF_T_Patient_Exam_TrangThai_Status] DEFAULT ((0)),
   [ma_khoa_thuchien] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [chandoan_kemtheo] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [nguoi_ketthuc] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [ngay_ketthuc] [datetime] NULL,
   [Lydo_ketthuc] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [dia_chi] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [diachi_bhyt] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [id_benhvien_den] [smallint] NULL,
   [id_benhvien_di] [smallint] NULL,
   [tthai_chuyenden] [tinyint] NULL,
   [tthai_chuyendi] [tinyint] NULL,
   [id_bacsi_chuyenvien] [smallint] NULL,
   [trangthai_ngoaitru] [tinyint] NULL,
   [trangthai_noitru] [tinyint] NOT NULL CONSTRAINT [DF__T_Patient__Hos_s__22FF2F51] DEFAULT ((0)),
   [Locked] [tinyint] NOT NULL CONSTRAINT [DF_T_Patient_Exam_Locked] DEFAULT ((0)),
   [tthai_thop_noitru] [tinyint] NULL,
   [tthai_thanhtoannoitru] [tinyint] NULL,
   [id_nhapvien] [bigint] NULL,
   [ngay_nhapvien] [datetime] NULL,
   [ngay_ravien] [datetime] NULL,
   [so_ravien] [int] NULL CONSTRAINT [DF_kcb_luotkham_so_ravien] DEFAULT ((0)),
   [mota_nhapvien] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [id_ravien] [bigint] NULL,
   [id_khoanoitru] [smallint] NULL,
   [id_buong] [smallint] NULL,
   [id_giuong] [smallint] NULL,
   [ngay_kedon] [datetime] NULL,
   [ngay_nhanthuoc] [datetime] NULL,
   [ngay_nhanketqua_cls] [datetime] NULL,
   [noi_gioithieu] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [bo_vien] [tinyint] NULL,
   [email] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [nhom_benhnhan] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [giay_bhyt] [tinyint] NULL,
   [madtuong_sinhsong] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [ip_maytao] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [ip_maysua] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [ten_maytao] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [id_lichsu_doituong_Kcb] [bigint] NULL,
   [ten_maysua] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [cach_tao] [tinyint] NULL CONSTRAINT [DF_kcb_luotkham_cach_tao] DEFAULT ((0)),
   [kieu_kham] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [traKQ_Phongchuyenmon] [tinyint] NULL,
   [traKQ_Fax] [tinyint] NULL,
   [traKQ_Mail] [tinyint] NULL,
   [traKQ_Email] [tinyint] NULL,
   [sosanh_QCVN] [tinyint] NULL,
   [mota_them] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
   [ngay_tao] [datetime] NOT NULL CONSTRAINT [DF_T_Patient_Exam_NGAY_TAO] DEFAULT (getdate()),
   [nguoi_tao] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
   [ngay_sua] [datetime] NULL,
   [nguoi_sua] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   INSERT INTO [dbo].[tmp_kcb_luotkham] ([ma_luotkham], [id_benhnhan], [ngay_tiepdon], [nguoi_tiepdon], [tuoi], [loai_tuoi], [id_doituong_kcb], [madoituong_gia], [ma_doituong_kcb], [id_loaidoituong_kcb], [ptram_bhyt_goc], [ptram_bhyt], [mathe_bhyt], [ngaybatdau_bhyt], [ngayketthuc_bhyt], [noicap_bhyt], [ma_noicap_bhyt], [ma_doituong_bhyt], [ma_quyenloi], [noi_dongtruso_kcbbd], [ma_kcbbd], [dung_tuyen], [CMT], [ngay_thanhtoan], [luong_coban], [trangthai_capcuu], [trieu_chung], [chan_doan], [ket_luan], [huong_dieutri], [mabenh_chinh], [mabenh_phu], [hienthi_baocao], [so_benh_an], [songay_dieutri], [id_khoatiepnhan], [solan_kham], [stt_kham], [noitru], [ma_khoa_thuchien], [chandoan_kemtheo], [nguoi_ketthuc], [ngay_ketthuc], [Lydo_ketthuc], [dia_chi], [diachi_bhyt], [id_benhvien_den], [id_benhvien_di], [tthai_chuyenden], [tthai_chuyendi], [id_bacsi_chuyenvien], [trangthai_ngoaitru], [trangthai_noitru], [Locked], [tthai_thop_noitru], [tthai_thanhtoannoitru], [id_nhapvien], [ngay_nhapvien], [ngay_ravien], [so_ravien], [mota_nhapvien], [id_ravien], [id_khoanoitru], [id_buong], [id_giuong], [ngay_kedon], [ngay_nhanthuoc], [ngay_nhanketqua_cls], [noi_gioithieu], [bo_vien], [email], [nhom_benhnhan], [giay_bhyt], [madtuong_sinhsong], [ip_maytao], [ip_maysua], [ten_maytao], [id_lichsu_doituong_Kcb], [ten_maysua], [cach_tao], [kieu_kham], [traKQ_Phongchuyenmon], [traKQ_Fax], [traKQ_Mail], [traKQ_Email], [sosanh_QCVN], [mota_them], [ngay_tao], [nguoi_tao], [ngay_sua], [nguoi_sua])
   SELECT [ma_luotkham], [id_benhnhan], [ngay_tiepdon], [nguoi_tiepdon], 0, 0, [id_doituong_kcb], [madoituong_gia], [ma_doituong_kcb], [id_loaidoituong_kcb], [ptram_bhyt_goc], [ptram_bhyt], [mathe_bhyt], [ngaybatdau_bhyt], [ngayketthuc_bhyt], [noicap_bhyt], [ma_noicap_bhyt], [ma_doituong_bhyt], [ma_quyenloi], [noi_dongtruso_kcbbd], [ma_kcbbd], [dung_tuyen], [CMT], [ngay_thanhtoan], [luong_coban], [trangthai_capcuu], [trieu_chung], [chan_doan], [ket_luan], [huong_dieutri], [mabenh_chinh], [mabenh_phu], [hienthi_baocao], [so_benh_an], [songay_dieutri], [id_khoatiepnhan], [solan_kham], [stt_kham], [noitru], [ma_khoa_thuchien], [chandoan_kemtheo], [nguoi_ketthuc], [ngay_ketthuc], [Lydo_ketthuc], [dia_chi], [diachi_bhyt], [id_benhvien_den], [id_benhvien_di], [tthai_chuyenden], [tthai_chuyendi], [id_bacsi_chuyenvien], [trangthai_ngoaitru], [trangthai_noitru], [Locked], [tthai_thop_noitru], [tthai_thanhtoannoitru], [id_nhapvien], [ngay_nhapvien], [ngay_ravien], [so_ravien], [mota_nhapvien], [id_ravien], [id_khoanoitru], [id_buong], [id_giuong], [ngay_kedon], [ngay_nhanthuoc], [ngay_nhanketqua_cls], [noi_gioithieu], [bo_vien], [email], [nhom_benhnhan], [giay_bhyt], [madtuong_sinhsong], [ip_maytao], [ip_maysua], [ten_maytao], [id_lichsu_doituong_Kcb], [ten_maysua], [cach_tao], [kieu_kham], [traKQ_Phongchuyenmon], [traKQ_Fax], [traKQ_Mail], [traKQ_Email], [sosanh_QCVN], [mota_them], [ngay_tao], [nguoi_tao], [ngay_sua], [nguoi_sua]
   FROM [dbo].[kcb_luotkham]
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   DROP TABLE [dbo].[kcb_luotkham]
GO

sp_rename N'[dbo].[tmp_kcb_luotkham]', N'kcb_luotkham'

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[kcb_luotkham] ADD CONSTRAINT [PK_kcb_luotkham] PRIMARY KEY CLUSTERED ([ma_luotkham])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   CREATE INDEX [Patient_Code] ON [dbo].[kcb_luotkham] ([ma_luotkham], [id_benhnhan])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.kcb_luotkham Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.kcb_luotkham Table'
END
GO


--  
-- Script to Update dbo.fLaytendichvuthanhtoan in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Thursday, October 8, 2015, at 11:07 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
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
RETURNS [nvarchar](30)   
AS 
BEGIN
	DECLARE @PayementType_Name NVARCHAR(30)
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
-- Script to Update dbo.v_kcb_luotkham in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Thursday, October 8, 2015, at 11:07 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.v_kcb_luotkham View'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('ALTER VIEW [dbo].[v_kcb_luotkham]
AS
select p.id_benhnhan
      ,p.ten_benhnhan
      ,p.ma_tinh_thanhpho
      ,p.ma_quanhuyen
      ,p.ngay_sinh
      ,p.nam_sinh
      ,CONVERT(NVARCHAR(10), p.ngay_sinh,103) AS sngay_sinh
      --,(YEAR(c.ngay_tiepdon) -p.nam_sinh) AS Tuoi
      ,c.tuoi,c.loai_tuoi,p.so_tiemchung_QG
      ,p.id_gioitinh
      ,p.gioi_tinh
      ,p.nghe_nghiep
      ,p.co_quan
      ,p.ma_quocgia
      ,p.dien_thoai
      ,p.email
      ,p.ngay_tiepdon as ngay_tiepdon_landau
      ,p.nguoi_tiepdon as nguoi_tiepdon_landau
      ,p.dan_toc
      ,p.ton_giao
      ,p.ngay_sua
      ,p.ngay_tao
      ,p.nguoi_sua
      ,p.nguoi_tao
     
      ----------------------------
      ,c.ma_luotkham
      ,c.id_doituong_kcb
      ,c.tthai_chuyenden 
      ,c.tthai_chuyendi
      ,c.id_benhvien_den
      ,c.id_benhvien_di
      ,p.nguoi_lienhe
      ,p.dienthoai_lienhe
      ,p.diachi_lienhe,p.fax
      ,c.mathe_bhyt
      ,c.ngaybatdau_bhyt
      ,c.ngayketthuc_bhyt
      ,c.noicap_bhyt
      ,c.ma_noicap_bhyt
      ,c.ma_doituong_bhyt
      ,(select ma_nhombhyt from dmuc_doituongbhyt where ma_doituongbhyt=c.ma_doituong_bhyt ) as ma_nhombhyt
      ,(select TEN from dmuc_chung dm where loai=''NHOMBHYT'' and exists (select 1 from dmuc_doituongbhyt where ma_doituongbhyt=c.ma_doituong_bhyt and ma_nhombhyt=dm.MA )) as ten_nhombhyt
      ,(select TEN from dmuc_chung dm where loai=''KIEUKHAM'' and MA=c.kieu_kham) as ten_kieukham
      ,(select stt_hthi from dmuc_chung dm where loai=''NHOMBHYT'' and exists (select 1 from dmuc_doituongbhyt where ma_doituongbhyt=c.ma_doituong_bhyt and ma_nhombhyt=dm.MA )) as stt_nhombhyt
      
      ,c.ngay_thanhtoan
      ,c.ma_quyenloi,c.ptram_bhyt_goc
      ,c.noi_dongtruso_kcbbd
      ,c.ma_kcbbd
      ,c.ngay_tiepdon
      ,c.ngay_tiepdon as sngay_tiepdon
      ,c.nguoi_tiepdon
      ,c.trangthai_noitru,
      dbo.ten_trangthai_kcb(trangthai_noitru)  AS ten_trangthai_noitru
      ,c.ptram_bhyt
      ,c.Locked
      ,c.CMT
      ,c.stt_kham
      ,c.luong_coban
      ,c.trangthai_capcuu
      ,c.trieu_chung
      ,c.dung_tuyen
      ,c.chan_doan
      ,c.ket_luan
      ,c.cach_tao
      ,c.huong_dieutri
      ,c.mabenh_chinh
      ,c.mabenh_phu
      ,c.hienthi_baocao
      ,c.so_benh_an
      ,c.songay_dieutri
      ,c.ma_doituong_kcb
      ,c.id_loaidoituong_kcb
      ,c.id_khoatiepnhan
      ,c.solan_kham
      ,c.noitru
      ,c.kieu_kham
      ,c.so_ravien
      ,c.ma_khoa_thuchien
      ,c.chandoan_kemtheo
      ,c.nguoi_ketthuc
      ,c.ngay_ketthuc
      ,c.Lydo_ketthuc
      ,c.id_nhapvien,c.id_ravien
      ,c.id_bacsi_chuyenvien,c.ngay_ravien
      ,ISNULL((SELECT ten_nhanvien FROM dmuc_nhanvien dn WHERE dn.id_nhanvien=c.id_bacsi_chuyenvien),'''') AS ten_bacsi_chuyenvien
      ,c.trangthai_ngoaitru,c.giay_bhyt,c.madtuong_sinhsong
      ,isnull(c.dia_chi,p.dia_chi) AS dia_chi
      ,c.diachi_bhyt,c.id_buong,c.id_giuong
      ,c.ngay_tao as ngaytao_luotkham
      ,c.nguoi_tao as nguoitao_luotkham
      ,c.ngay_sua as ngaysua_luotkham
      ,c.nguoi_sua as nguoisua_luotkham
       ,c.traKQ_Phongchuyenmon,c.traKQ_Fax,c.traKQ_Mail,c.traKQ_Email,c.sosanh_QCVN,
       c.mota_them
      ,p.kieu_benhnhan
      ,d.ten_doituong_kcb,c.mota_nhapvien,c.ngay_nhapvien,convert(nvarchar(10),c.ngay_nhapvien,103) AS sngay_nhapvien,c.id_khoanoitru,
      ISNULL((SELECT  top 1 dk.ten_benhvien
         FROM dmuc_benhvien dk WHERE dk.id_benhvien=c.id_benhvien_den),'''') AS ten_noichuyenden,
         ISNULL((SELECT  top 1 dk.ten_benhvien
         FROM dmuc_benhvien dk WHERE dk.id_benhvien=c.id_benhvien_di),'''') AS ten_noichuyendi,
      ISNULL((SELECT  top 1 dk.ten_khoaphong
         FROM dmuc_khoaphong dk WHERE dk.id_khoaphong=c.id_khoanoitru),'''') AS ten_khoanoitru,
          ISNULL((SELECT  top 1 dk.ten_buong
         FROM noitru_dmuc_buong dk WHERE dk.id_buong=c.id_buong),'''') AS ten_buong,
          ISNULL((SELECT top 1 dk.ten_giuong
         FROM noitru_dmuc_giuongbenh dk WHERE dk.id_giuong=c.id_giuong),'''') AS ten_giuong,
     (select top 1 ten_kcbbd from dmuc_noiKCBBD where ma_kcbbd=c.ma_kcbbd and ma_diachinh=c.noi_dongtruso_kcbbd) as ten_kcbbd
     ,ROW_NUMBER()OVER(ORDER BY c.ngay_tiepdon DESC) AS STT
     
from kcb_danhsach_benhnhan p 
left join 
kcb_luotkham as c
on p.id_benhnhan=c.id_benhnhan
left join dmuc_doituongkcb d
on c.id_doituong_kcb=d.id_doituong_kcb
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.v_kcb_luotkham View Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.v_kcb_luotkham View'
END
GO


--  
-- Script to Update dbo.Kcb_Thanhtoan_Laythongtindvu_Chuathanhtoan in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Thursday, October 8, 2015, at 11:07 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
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


exec('/************************************************************
 * Code formatted by SoftTree SQL Assistant C v4.7.11
 * Time: 09/09/2013 8:48:25 PM
 ************************************************************/

ALTER PROCEDURE [dbo].[Kcb_Thanhtoan_Laythongtindvu_Chuathanhtoan]
	@ma_luotkham NVARCHAR(20),
	@id_benhnhan INT,
	@Hos_Status INT,
	@MA_KHOA_THIEN NVARCHAR(50),
	@MA_DOI_TUONG NVARCHAR(50)
AS

IF OBJECT_ID(''tempdb..#thongtinthuoc'') IS NOT NULL
	    DROP TABLE #thongtinthuoc
 SELECT 
	                  tp.id_donthuoc ,
	                  tpd.trang_thai ,
	                  tpd.don_gia ,
	                
	                  tpd.bnhan_chitra,
	                  tpd.bhyt_chitra,
	                   tpd.tile_chietkhau,
	                  tpd.tien_chietkhau,
	                  tpd.kieu_chietkhau,
	                  tpd.phu_thu,
	                  tpd.id_thuoc ,
	                  tp.ngay_tao ,
	                  tpd.trangthai_thanhtoan ,
	                  tpd.id_chitietdonthuoc ,
	                  tp.ma_luotkham,
	                  tp.id_benhnhan,
	                  
	                  tpd.so_luong,
	                  tpd.tu_tuc AS tu_tuc,
	                 tpd.trangthai_huy,
	                  tp.id_khoadieutri,
	                   id_phongkham ,
	                 tp.id_bacsi_chidinh,
	                  tpd.trangthai_bhyt,
	                  tpd.stt_in,
	                  tp.id_kham
	                  ,tp.id_lichsu_doituong_Kcb,tp.mathe_bhyt,tp.kieu_donthuoc,tp.kieu_thuocvattu,tp.noitru
	                  INTO #thongtinthuoc
	           FROM   kcb_donthuoc_chitiet tpd
	                  JOIN kcb_donthuoc tp
	                       ON  tpd.id_donthuoc = tp.id_donthuoc
	           WHERE  NOT EXISTS(
	                      SELECT 1
	                      FROM   kcb_donthuoc
	                      WHERE  id_donthuocthaythe = tp.id_donthuoc
	                  )
	                  AND tp.ma_luotkham = @ma_luotkham
	                  AND tp.id_benhnhan = @id_benhnhan
	                  	    
IF OBJECT_ID(''tempdb..#thongtindichvu'') IS NOT NULL
	    DROP TABLE #thongtindichvu
   SELECT 
	                  tai.id_chidinh ,
	                  tad.trang_thai ,
	                  tad.don_gia,
	               
	                  tad.bnhan_chitra,
	                  tad.bhyt_chitra,
	                  tad.tile_chietkhau,
	                  tad.tien_chietkhau,
	                  tad.kieu_chietkhau,
	                  tad.phu_thu,
	                  tad.id_dichvu ,
	                  tad.id_chitietdichvu ,
	                  tai.ngay_tao ,
	                  tad.trangthai_thanhtoan ,
	                  tad.id_chitietchidinh ,
	                  tai.ma_luotkham,
	                  tai.id_benhnhan,
	                 
	                  tad.so_luong,
	                  tad.tu_tuc,
	                 tad.trangthai_huy,
	                 tad.id_khoa_thuchien,
	                 tai.id_phong_chidinh,
	                  tai.id_bacsi_chidinh,
	                  tad.trangthai_bhyt,
	                tai.id_kham
	                  ,tai.id_lichsu_doituong_Kcb,tai.mathe_bhyt,tai.kieu_chidinh,tai.noitru
	                  INTO #thongtindichvu
	           FROM   kcb_chidinhcls_chitiet tad
	                  JOIN kcb_chidinhcls tai
	                       ON  tai.id_chidinh = tad.id_chidinh
	           WHERE  tai.ma_luotkham = @ma_luotkham
	                  AND tai.id_benhnhan = @id_benhnhan
	                  	    	    
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
	       
	          (CASE WHEN isnull(tu_tuc,0)=0 THEN 
	          	(ISNULL(p.bnhan_chitra, 0) * ISNULL(p.so_luong, 0))
	        ELSE 0 END ) AS TT_BN_KHONG_TUTUC,
	          (CASE WHEN isnull(tu_tuc,0)=1 THEN 
	          	(ISNULL(p.bnhan_chitra, 0) * ISNULL(p.so_luong, 0))
	        ELSE 0 END ) AS TT_TUTUC,
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
	                 ISNULL( dake_donthuoc,0) AS dake_donthuoc,
	                 ISNULL( dachidinh_cls,0) AS dachidinh_cls,
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
	                  ISNULL(tre.trang_thai, 0) AS trang_thai,isnull(tre.id_kham,-1) AS id_kham
	                  ,tre.id_lichsu_doituong_Kcb,tre.mathe_bhyt
	           FROM   kcb_dangky_kcb tre
	           WHERE  tre.id_benhnhan = @id_benhnhan
	                  AND tre.ma_luotkham = @ma_luotkham
	                  AND ISNULL(la_phidichvukemtheo, 0) IN (0, 1)
	                  AND(@Hos_Status=-1 OR ISNULL(tre.noitru,0)=@Hos_Status)
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
	                  0 as tile_chietkhau,
	                  0 as tien_chietkhau,
	                  ''%'' as kieu_chietkhau,
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
	                  trangthai_thanhtoan AS trang_thai,isnull(tre.id_sokcb,-1) AS id_kham
	                  ,tre.id_lichsu_doituong_Kcb,tre.mathe_bhyt
	           FROM   kcb_dangky_sokham tre
	           INNER JOIN dmuc_chung dc ON tre.ma_sokcb=dc.MA
	            WHERE
	            dc.LOAI=''SO_KCB''  
	           AND tre.id_benhnhan = @id_benhnhan
	                  AND tre.ma_luotkham = @ma_luotkham
	                  AND(@Hos_Status=-1 OR ISNULL(tre.noitru,0)=@Hos_Status)   
	           UNION 
	           -- th?c hi?n vi?c load thong tin c?a phan d?ch v? c?n lam sang
	           SELECT 2 AS id_loaithanhtoan,0 as dake_donthuoc,0 as dachidinh_cls,
	                  p.id_chidinh AS id_phieu,
	                  p.trang_thai as trangthai_chuyencls,
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
	                      FROM   dmuc_dichvucls_chitiet lsd
	                      WHERE  lsd.id_chitietdichvu = p.id_chitietdichvu
	                  ) AS ten_chitietdichvu,
	                  (
	                      SELECT TOP 1 lsd.ten_chitietdichvu_bhyt
	                      FROM   dmuc_dichvucls_chitiet lsd
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
	                  dbo.fLayDonvitinh(2, p.id_chitietdichvu) AS 
	                  ten_donvitinh,
	                  (
	                      CASE 
	                           WHEN CONVERT(INT, ISNULL(p.trang_thai, 0))
	                                >= 1 THEN 1
	                           ELSE 0
	                      END
	                  ) AS trang_thai,isnull(p.id_kham,-1) AS id_kham
	                  ,p.id_lichsu_doituong_Kcb,p.mathe_bhyt
	           FROM   #thongtindichvu AS p
	           WHERE  p.ma_luotkham = @ma_luotkham
	                  AND p.id_benhnhan = @id_benhnhan
	                  AND ISNULL(p.kieu_chidinh, 0) = 0
	                      --  AND (dbo.trunc(tad.Input_Date) BETWEEN dbo.trunc(@FromDate) AND dbo.trunc(@toDate) )
	                  AND(@Hos_Status=-1 OR ISNULL(p.noitru,0)=@Hos_Status)
	           --Chi phi them
	           UNION
	           SELECT 9 AS id_loaithanhtoan,0 as dake_donthuoc,0 as dachidinh_cls,
	                  p.id_chidinh AS id_phieu,
	                  p.trang_thai as trangthai_chuyencls,
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
	                      FROM   dmuc_dichvucls_chitiet lsd
	                      WHERE  lsd.id_chitietdichvu = p.id_chitietdichvu
	                  ) AS ten_chitietdichvu,
	                  (
	                      SELECT TOP 1 lsd.ten_chitietdichvu_bhyt
	                      FROM   dmuc_dichvucls_chitiet lsd
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
	                  dbo.fLayDonvitinh(2, p.id_chitietdichvu) AS 
	                  ten_donvitinh,
	                  (
	                      CASE 
	                           WHEN CONVERT(INT, ISNULL(p.trang_thai, 0))
	                                >= 1 THEN 1
	                           ELSE 0
	                      END
	                  ) AS trang_thai,
	                  ISNULL(p.id_kham, -1) AS id_kham
	                  ,p.id_lichsu_doituong_Kcb,p.mathe_bhyt
	           FROM   #thongtindichvu AS p
	           WHERE  p.ma_luotkham = @ma_luotkham
	                  AND p.id_benhnhan = @id_benhnhan
	                   AND ISNULL(p.kieu_chidinh, 0) = 2
	                   AND(@Hos_Status=-1 OR ISNULL(p.noitru,0)=@Hos_Status)
	           UNION
	           SELECT 11 AS id_loaithanhtoan,0 as dake_donthuoc,0 as dachidinh_cls,
	                  p.id_chidinh AS id_phieu,
	                  p.trang_thai as trangthai_chuyencls,
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
	                      FROM   dmuc_dichvucls_chitiet lsd
	                      WHERE  lsd.id_chitietdichvu = p.id_chitietdichvu
	                  ) AS ten_chitietdichvu,
	                  (
	                      SELECT TOP 1 lsd.ten_chitietdichvu_bhyt
	                      FROM   dmuc_dichvucls_chitiet lsd
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
	                  dbo.fLayDonvitinh(2, p.id_chitietdichvu) AS 
	                  ten_donvitinh,
	                  (
	                      CASE 
	                           WHEN CONVERT(INT, ISNULL(p.trang_thai, 0))
	                                >= 1 THEN 1
	                           ELSE 0
	                      END
	                  ) AS trang_thai,
	                  ISNULL(p.id_kham, -1) AS id_kham
	                  ,p.id_lichsu_doituong_Kcb,p.mathe_bhyt
	           FROM   #thongtindichvu AS p
	           WHERE  p.ma_luotkham = @ma_luotkham
	                  AND p.id_benhnhan = @id_benhnhan
	                   AND ISNULL(p.kieu_chidinh, 0) = 4
	                   AND(@Hos_Status=-1 OR ISNULL(p.noitru,0)=@Hos_Status)
	           UNION	
	           SELECT 3 AS id_loaithanhtoan,0 as dake_donthuoc,0 as dachidinh_cls,
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
	                      FROM   dmuc_thuoc ld
	                      WHERE  ld.id_thuoc = p.id_thuoc
	                  ) AS ten_chitietdichvu,
	                  (
	                      SELECT TOP 1 ld.ten_bhyt
	                      FROM   dmuc_thuoc ld
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
	                  CONVERT(INT, ISNULL(p.trang_thai, 0)) AS trang_thai
	                  ,isnull(p.id_kham,-1) AS id_kham
	                  ,p.id_lichsu_doituong_Kcb,p.mathe_bhyt
	           FROM   #thongtinthuoc AS p
	           WHERE   p.kieu_donthuoc <> 2--Ko lay don thuoc tai quay
	                   AND p.kieu_thuocvattu=''THUOC''
	                  AND(@Hos_Status=-1 OR ISNULL(p.noitru,0)=@Hos_Status)
	                  
	                 
	           
	           
	           UNION--Vat tu tieu hao
	           SELECT 5 AS id_loaithanhtoan,0 as dake_donthuoc,0 as dachidinh_cls,
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
	                      FROM   dmuc_thuoc ld
	                      WHERE  ld.id_thuoc = p.id_thuoc
	                  ) AS ten_chitietdichvu,
	                  (
	                      SELECT TOP 1 ld.ten_bhyt
	                      FROM   dmuc_thuoc ld
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
	                  CONVERT(INT, ISNULL(p.trang_thai, 0)) AS trang_thai
	                  ,isnull(p.id_kham,-1) AS id_kham
	                  ,p.id_lichsu_doituong_Kcb,p.mathe_bhyt
	           FROM   #thongtinthuoc AS p
	           WHERE  p.kieu_donthuoc <> 2--Ko lay don thuoc tai quay
	                   AND p.kieu_thuocvattu=''VT''
	                  AND(@Hos_Status=-1 OR ISNULL(p.noitru,0)=@Hos_Status)
	                 
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
-- Script to Create dbo.Thuoc_TongnhapngoaiTrongNam in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Thursday, October 8, 2015, at 11:07 PM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.Thuoc_TongnhapngoaiTrongNam Procedure'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


exec('CREATE PROCEDURE [dbo].[Thuoc_TongnhapngoaiTrongNam]
	@Nam INT,
	@Idthuoc INT,
	@Soluong INT OUT
AS
	SET @Soluong = ISNULL(
	        (SELECT SUM(c.so_luong)
	        FROM   (
	                   SELECT id_phieu,
	                          so_luong
	                   FROM   t_phieu_nhapxuatthuoc_chitiet
	                   WHERE  id_thuoc = @Idthuoc
	               ) AS c
	               INNER JOIN (
	                        SELECT id_phieu
	                        FROM   t_phieu_nhapxuatthuoc
	                        WHERE  
	                        loai_phieu=1
	                        AND YEAR(ngay_hoadon) = @Nam
	                    ) AS p
	                    ON  p.id_phieu = c.id_phieu),
	               0
	    )
')
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.Thuoc_TongnhapngoaiTrongNam Procedure Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.Thuoc_TongnhapngoaiTrongNam Procedure'
END
GO
--  
-- Script to Update dbo.dmuc_thuoc in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Friday, October 9, 2015, at 10:09 AM 
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
      ADD [LastActionName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
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
-- Script to Update dbo.kcb_benh_an in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Friday, October 9, 2015, at 10:09 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.kcb_benh_an Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[kcb_benh_an]
      ADD [LastActionName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.kcb_benh_an Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.kcb_benh_an Table'
END
GO


--  
-- Script to Update dbo.kcb_chidinhcls in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Friday, October 9, 2015, at 10:09 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.kcb_chidinhcls Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[kcb_chidinhcls]
      ADD [LastActionName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.kcb_chidinhcls Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.kcb_chidinhcls Table'
END
GO


--  
-- Script to Update dbo.kcb_danhsach_benhnhan in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Friday, October 9, 2015, at 10:09 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.kcb_danhsach_benhnhan Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[kcb_danhsach_benhnhan]
      ADD [LastActionName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.kcb_danhsach_benhnhan Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.kcb_danhsach_benhnhan Table'
END
GO


--  
-- Script to Update dbo.kcb_donthuoc in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Friday, October 9, 2015, at 10:09 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.kcb_donthuoc Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[kcb_donthuoc]
      ADD [LastActionName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.kcb_donthuoc Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.kcb_donthuoc Table'
END
GO


--  
-- Script to Update dbo.kcb_luotkham in KKYBBNL-PC\SQL2K8.QLBV_BN 
-- Generated Friday, October 9, 2015, at 10:09 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.QLBV_BN before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Updating dbo.kcb_luotkham Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO


IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[kcb_luotkham]
      ADD [LastActionName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.kcb_luotkham Table Updated Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Update dbo.kcb_luotkham Table'
END
GO
