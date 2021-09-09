USE [InterviewTask]
GO

/****** Object:  StoredProcedure [dbo].[sp_DeleteEmployeeAndDetails]    Script Date: 9/9/2021 11:25:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[sp_DeleteEmployeeAndDetails]
(
@EmpID int
)
AS
BEGIN
DELETE FROM [dbo].[tblEmployeeDetails]
      WHERE 
		EmpId=@EmpID

DELETE FROM tblEmployee 
	WHERE 
		EmpID= @EmpID

END
GO


