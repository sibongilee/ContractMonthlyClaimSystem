CREATE DATABASE ContractMonthlyClaimSystemDB;
GO
USE ContractMonthlyClaimSystemDB;

CREATE TABLE Users(
UserId INT IDENTITY(1,1) PRIMARY KEY,
Name NVARCHAR(100) NOT NULL,
Email NVARCHAR(100) NOT NULL UNIQUE,
Password NVARCHAR(255) NOT NULL,
Role NVARCHAR(50) NOT NULL CHECK(Role IN('Lecturer','Program Coordinator','Manager'))
);
GO

CREATE TABLE Claims(
claimId INT IDENTITY(1,1) PRIMARY KEY,
LecturerId INT NOT NULL,
HoursWorked Decimal(5,2),
HourlyRate Decimal(10,2),
Notes NVARCHAR(500),
DocumentPath NVARCHAR(255),
Status NVARCHAR(50) NOT NULL CHECK(Status IN('Pending','Approved','Rejected')),
DateSubmitted DATETIME NOT NULL DEFAULT GETDATE(),
FOREIGN KEY (LecturerId) REFERENCES Lecturers(LecturerId)
);
GO

CREATE TABLE Lecturers(
LecturerId INT IDENTITY(1,1) PRIMARY KEY,
LecturerName NVARCHAR(100) NOT NULL,
Email NVARCHAR(100) NOT NULL UNIQUE,
Department NVARCHAR(100),
UserId INT NULL,
FOREIGN KEY (UserId) REFERENCES Users(UserID),
Description NVARCHAR(500)
);