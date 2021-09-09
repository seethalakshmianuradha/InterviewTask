USE [InterviewTask]
GO

/****** Object:  StoredProcedure [dbo].[sp_AddEmployee]    Script Date: 9/9/2021 11:21:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[sp_AddEmployee]
(
@Name VARCHAR(50),
@Email VARCHAR(50),
@Designation VARCHAR(50),
@FileName VARCHAR(5000),
@FilePath VARCHAR(8000)
)


AS BEGIN
Declare @EmpID  int;
INSERT INTO [dbo].[tblEmployee]
           ([Name]
           ,[Email]
           ,[Designation])
     VALUES
           (@Name,@Email,@Designation)
		
SELECT @EmpID =  SCOPE_IDENTITY();

INSERT INTO [dbo].[tblEmployeeDetails]
           ([EmpId]
           ,[FileName]
           ,[FilePath]
           ,[CreatedDate])
     VALUES
           (@EmpID, 
            @FileName, 
            @FilePath, 
            GETDATE())

END


GO


