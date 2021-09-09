USE [InterviewTask]
GO

/****** Object:  Table [dbo].[tblEmployee]    Script Date: 9/9/2021 11:18:12 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblEmployee]') AND type in (N'U'))
DROP TABLE [dbo].[tblEmployee]
GO

/****** Object:  Table [dbo].[tblEmployee]    Script Date: 9/9/2021 11:18:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblEmployee](
	[EmpID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Designation] [varchar](50) NULL,
 CONSTRAINT [PK_tblEmployee] PRIMARY KEY CLUSTERED 
(
	[EmpID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [InterviewTask]
GO

/****** Object:  Table [dbo].[tblEmployeeDetails]    Script Date: 9/9/2021 11:20:20 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblEmployeeDetails]') AND type in (N'U'))
DROP TABLE [dbo].[tblEmployeeDetails]
GO

/****** Object:  Table [dbo].[tblEmployeeDetails]    Script Date: 9/9/2021 11:20:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblEmployeeDetails](
	[EmpId] [int] NOT NULL,
	[FileName] [varchar](8000) NULL,
	[FilePath] [varchar](8000) NULL,
	[CreatedDate] [date] NULL
) ON [PRIMARY]
GO


