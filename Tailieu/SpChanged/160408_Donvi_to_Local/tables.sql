--  
-- Script to Create dbo.dmuc_mahoamau_kiemnghiem in KKYBBNL-PC\SQL2K8.BN160408 
-- Generated Friday, April 8, 2016, at 11:00 AM 
--  
-- Please backup KKYBBNL-PC\SQL2K8.BN160408 before executing this script
--  
-- ** Script Begin **
BEGIN TRANSACTION
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO

PRINT 'Creating dbo.dmuc_mahoamau_kiemnghiem Table'
GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

CREATE TABLE [dbo].[dmuc_mahoamau_kiemnghiem] (
   [id] [bigint] IDENTITY (1, 1) NOT NULL,
   [ma_dichvu] [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
   [Nam] [int] NOT NULL,
   [STT] [int] NOT NULL
)
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
   ALTER TABLE [dbo].[dmuc_mahoamau_kiemnghiem] ADD CONSTRAINT [PK_dmuc_mahoamau_kiemnghiem] PRIMARY KEY CLUSTERED ([id])
GO

IF @@ERROR <> 0
   IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION
GO

IF @@TRANCOUNT = 1
BEGIN
   PRINT 'dbo.dmuc_mahoamau_kiemnghiem Table Added Successfully'
   COMMIT TRANSACTION
END ELSE
BEGIN
   PRINT 'Failed To Add dbo.dmuc_mahoamau_kiemnghiem Table'
END
GO
