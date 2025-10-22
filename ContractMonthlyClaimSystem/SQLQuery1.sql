CREATE DATABASE ContractMonthlyClaimSystemDB;
GO
USE ContractMonthlyClaimSystemDB;
CREATE TABLE Claims(
ClaimID INT IDENTITY(1,1) PRIMARY KEY,
LecturerName NVARCHAR(100),
HoursWorked Decimal(5,2),
HourlyRate Decimal(10,2),
AdditionalNotes NVARCHAR(500),
DocumentPath NVARCHAR(255),
Status NVARCHAR(50) DEFAULT 'Pending',

);