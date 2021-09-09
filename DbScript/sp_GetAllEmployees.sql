USE [InterviewTask]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetAllEmployees]    Script Date: 9/9/2021 11:26:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetAllEmployees]
AS BEGIN
SELECT *
  FROM [dbo].[tblEmployee] te JOIN tblEmployeeDetails ted ON te.EmpID = ted.EmpId
END


GO


