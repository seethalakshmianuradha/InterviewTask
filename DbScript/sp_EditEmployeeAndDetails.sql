USE [InterviewTask]
GO

/****** Object:  StoredProcedure [dbo].[sp_EditEmployeeAndDetails]    Script Date: 9/9/2021 11:26:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EditEmployeeAndDetails](
@EmpID INT,
@Name VARCHAR(50),
@Email VARCHAR(50),
@Designation VARCHAR(50),
@FileName VARCHAR(50),
@FilePath VARCHAR(200),
@CreatedDate DATE
)
AS
BEGIN
UPDATE [dbo].[tblEmployee]
   SET [Name] = @Name,
	[Email] = @Email,
      [Designation] = @Designation
 WHERE EmpID = @EmpID

UPDATE [dbo].[tblEmployeeDetails]
   SET [FileName] = @FileName,
      [FilePath] = @FilePath, 
      [CreatedDate] = @CreatedDate
 WHERE Empid = @EmpID

 END




GO


