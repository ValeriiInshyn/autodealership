USE [master]
GO
ALTER DATABASE [AutoDealershipOLAP] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
USE [master]
GO
/****** Object:  Database [AutoDealershipOLAP]    Script Date: 5/2/2024 1:18:54 AM ******/
DROP DATABASE [AutoDealershipOLAP]
GO
EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'AutoDealershipOLAP'
GO
USE [master]
GO
ALTER DATABASE [AutoDealershipMetadata] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
USE [master]
GO
/****** Object:  Database [AutoDealershipMetadata]    Script Date: 5/2/2024 1:19:02 AM ******/
DROP DATABASE [AutoDealershipMetadata]
GO
EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'AutoDealershipMetadata'
GO
USE [master]
GO
ALTER DATABASE [AutoDealershipStaging] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
USE [master]
GO
/****** Object:  Database [AutoDealershipStaging]    Script Date: 5/2/2024 1:20:19 AM ******/
DROP DATABASE [AutoDealershipStaging]
GO
EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'AutoDealershipStaging'
GO
