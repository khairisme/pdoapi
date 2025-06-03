-- Create the HR database
USE master;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'HRDatabase')
BEGIN
    CREATE DATABASE HRDatabase;
END
GO

USE HRDatabase;
GO

-- Create Department table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Department')
BEGIN
    CREATE TABLE Department (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Description NVARCHAR(500) NULL,
        CreatedAt DATETIME2 NOT NULL,
        CreatedBy NVARCHAR(100) NOT NULL,
        ModifiedAt DATETIME2 NULL,
        ModifiedBy NVARCHAR(100) NULL,
        IsDeleted BIT NOT NULL DEFAULT 0
    );
END
GO

-- Create Position table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Position')
BEGIN
    CREATE TABLE Position (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        Title NVARCHAR(100) NOT NULL,
        Description NVARCHAR(500) NULL,
        DepartmentId UNIQUEIDENTIFIER NOT NULL,
        CreatedAt DATETIME2 NOT NULL,
        CreatedBy NVARCHAR(100) NOT NULL,
        ModifiedAt DATETIME2 NULL,
        ModifiedBy NVARCHAR(100) NULL,
        IsDeleted BIT NOT NULL DEFAULT 0,
        CONSTRAINT FK_Position_Department FOREIGN KEY (DepartmentId) REFERENCES Department(Id)
    );
END
GO

-- Create Employee table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Employee')
BEGIN
    CREATE TABLE Employee (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        EmployeeId NVARCHAR(20) NOT NULL,
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        MiddleName NVARCHAR(50) NULL,
        DateOfBirth DATE NOT NULL,
        Email NVARCHAR(100) NOT NULL,
        Phone NVARCHAR(20) NOT NULL,
        Address NVARCHAR(200) NOT NULL,
        JoiningDate DATE NOT NULL,
        TerminationDate DATE NULL,
        Status INT NOT NULL,
        DepartmentId UNIQUEIDENTIFIER NOT NULL,
        PositionId UNIQUEIDENTIFIER NOT NULL,
        ManagerId UNIQUEIDENTIFIER NULL,
        CreatedAt DATETIME2 NOT NULL,
        CreatedBy NVARCHAR(100) NOT NULL,
        ModifiedAt DATETIME2 NULL,
        ModifiedBy NVARCHAR(100) NULL,
        IsDeleted BIT NOT NULL DEFAULT 0,
        CONSTRAINT FK_Employee_Department FOREIGN KEY (DepartmentId) REFERENCES Department(Id),
        CONSTRAINT FK_Employee_Position FOREIGN KEY (PositionId) REFERENCES Position(Id),
        CONSTRAINT FK_Employee_Manager FOREIGN KEY (ManagerId) REFERENCES Employee(Id)
    );
END
GO

-- Create Leave table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Leave')
BEGIN
    CREATE TABLE Leave (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        EmployeeId UNIQUEIDENTIFIER NOT NULL,
        LeaveType INT NOT NULL,
        StartDate DATE NOT NULL,
        EndDate DATE NOT NULL,
        Reason NVARCHAR(500) NULL,
        Status INT NOT NULL,
        CreatedAt DATETIME2 NOT NULL,
        CreatedBy NVARCHAR(100) NOT NULL,
        ModifiedAt DATETIME2 NULL,
        ModifiedBy NVARCHAR(100) NULL,
        IsDeleted BIT NOT NULL DEFAULT 0,
        CONSTRAINT FK_Leave_Employee FOREIGN KEY (EmployeeId) REFERENCES Employee(Id)
    );
END
GO

-- Create Attendance table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Attendance')
BEGIN
    CREATE TABLE Attendance (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        EmployeeId UNIQUEIDENTIFIER NOT NULL,
        Date DATE NOT NULL,
        CheckInTime TIME NULL,
        CheckOutTime TIME NULL,
        Status INT NOT NULL,
        CreatedAt DATETIME2 NOT NULL,
        CreatedBy NVARCHAR(100) NOT NULL,
        ModifiedAt DATETIME2 NULL,
        ModifiedBy NVARCHAR(100) NULL,
        IsDeleted BIT NOT NULL DEFAULT 0,
        CONSTRAINT FK_Attendance_Employee FOREIGN KEY (EmployeeId) REFERENCES Employee(Id)
    );
END
GO

-- Insert sample data
-- Departments
IF NOT EXISTS (SELECT * FROM Department)
BEGIN
    INSERT INTO Department (Id, Name, Description, CreatedAt, CreatedBy, IsDeleted)
    VALUES 
        (NEWID(), 'Human Resources', 'HR Department', GETDATE(), 'System', 0),
        (NEWID(), 'Information Technology', 'IT Department', GETDATE(), 'System', 0),
        (NEWID(), 'Finance', 'Finance Department', GETDATE(), 'System', 0),
        (NEWID(), 'Marketing', 'Marketing Department', GETDATE(), 'System', 0),
        (NEWID(), 'Operations', 'Operations Department', GETDATE(), 'System', 0);
END
GO

-- Positions
IF NOT EXISTS (SELECT * FROM Position)
BEGIN
    DECLARE @HRDeptId UNIQUEIDENTIFIER;
    DECLARE @ITDeptId UNIQUEIDENTIFIER;
    DECLARE @FinanceDeptId UNIQUEIDENTIFIER;
    DECLARE @MarketingDeptId UNIQUEIDENTIFIER;
    DECLARE @OperationsDeptId UNIQUEIDENTIFIER;

    SELECT @HRDeptId = Id FROM Department WHERE Name = 'Human Resources';
    SELECT @ITDeptId = Id FROM Department WHERE Name = 'Information Technology';
    SELECT @FinanceDeptId = Id FROM Department WHERE Name = 'Finance';
    SELECT @MarketingDeptId = Id FROM Department WHERE Name = 'Marketing';
    SELECT @OperationsDeptId = Id FROM Department WHERE Name = 'Operations';

    INSERT INTO Position (Id, Title, Description, DepartmentId, CreatedAt, CreatedBy, IsDeleted)
    VALUES 
        (NEWID(), 'HR Manager', 'Human Resources Manager', @HRDeptId, GETDATE(), 'System', 0),
        (NEWID(), 'HR Specialist', 'Human Resources Specialist', @HRDeptId, GETDATE(), 'System', 0),
        (NEWID(), 'IT Manager', 'Information Technology Manager', @ITDeptId, GETDATE(), 'System', 0),
        (NEWID(), 'Software Developer', 'Software Developer', @ITDeptId, GETDATE(), 'System', 0),
        (NEWID(), 'System Administrator', 'System Administrator', @ITDeptId, GETDATE(), 'System', 0),
        (NEWID(), 'Finance Manager', 'Finance Manager', @FinanceDeptId, GETDATE(), 'System', 0),
        (NEWID(), 'Accountant', 'Accountant', @FinanceDeptId, GETDATE(), 'System', 0),
        (NEWID(), 'Marketing Manager', 'Marketing Manager', @MarketingDeptId, GETDATE(), 'System', 0),
        (NEWID(), 'Marketing Specialist', 'Marketing Specialist', @MarketingDeptId, GETDATE(), 'System', 0),
        (NEWID(), 'Operations Manager', 'Operations Manager', @OperationsDeptId, GETDATE(), 'System', 0),
        (NEWID(), 'Operations Specialist', 'Operations Specialist', @OperationsDeptId, GETDATE(), 'System', 0);
END
GO

-- Insert sample Employees
IF NOT EXISTS (SELECT * FROM Employee)
BEGIN
    DECLARE @HRDeptId UNIQUEIDENTIFIER;
    DECLARE @ITDeptId UNIQUEIDENTIFIER;
    DECLARE @FinanceDeptId UNIQUEIDENTIFIER;
    DECLARE @MarketingDeptId UNIQUEIDENTIFIER;
    DECLARE @OperationsDeptId UNIQUEIDENTIFIER;
    
    DECLARE @HRManagerId UNIQUEIDENTIFIER;
    DECLARE @ITManagerId UNIQUEIDENTIFIER;
    DECLARE @FinanceManagerId UNIQUEIDENTIFIER;
    DECLARE @MarketingManagerId UNIQUEIDENTIFIER;
    DECLARE @OperationsManagerId UNIQUEIDENTIFIER;
    
    DECLARE @HRSpecialistId UNIQUEIDENTIFIER;
    DECLARE @SoftwareDeveloperId UNIQUEIDENTIFIER;
    DECLARE @SystemAdminId UNIQUEIDENTIFIER;
    DECLARE @AccountantId UNIQUEIDENTIFIER;
    DECLARE @MarketingSpecialistId UNIQUEIDENTIFIER;
    DECLARE @OperationsSpecialistId UNIQUEIDENTIFIER;
    
    SELECT @HRDeptId = Id FROM Department WHERE Name = 'Human Resources';
    SELECT @ITDeptId = Id FROM Department WHERE Name = 'Information Technology';
    SELECT @FinanceDeptId = Id FROM Department WHERE Name = 'Finance';
    SELECT @MarketingDeptId = Id FROM Department WHERE Name = 'Marketing';
    SELECT @OperationsDeptId = Id FROM Department WHERE Name = 'Operations';
    
    SELECT @HRManagerId = Id FROM Position WHERE Title = 'HR Manager';
    SELECT @ITManagerId = Id FROM Position WHERE Title = 'IT Manager';
    SELECT @FinanceManagerId = Id FROM Position WHERE Title = 'Finance Manager';
    SELECT @MarketingManagerId = Id FROM Position WHERE Title = 'Marketing Manager';
    SELECT @OperationsManagerId = Id FROM Position WHERE Title = 'Operations Manager';
    
    SELECT @HRSpecialistId = Id FROM Position WHERE Title = 'HR Specialist';
    SELECT @SoftwareDeveloperId = Id FROM Position WHERE Title = 'Software Developer';
    SELECT @SystemAdminId = Id FROM Position WHERE Title = 'System Administrator';
    SELECT @AccountantId = Id FROM Position WHERE Title = 'Accountant';
    SELECT @MarketingSpecialistId = Id FROM Position WHERE Title = 'Marketing Specialist';
    SELECT @OperationsSpecialistId = Id FROM Position WHERE Title = 'Operations Specialist';
    
    -- Insert managers first
    DECLARE @JohnDoeId UNIQUEIDENTIFIER = NEWID();
    DECLARE @JaneSmithId UNIQUEIDENTIFIER = NEWID();
    DECLARE @RobertJohnsonId UNIQUEIDENTIFIER = NEWID();
    DECLARE @SarahWilliamsId UNIQUEIDENTIFIER = NEWID();
    DECLARE @MichaelBrownId UNIQUEIDENTIFIER = NEWID();
    
    -- HR Manager
    INSERT INTO Employee (Id, EmployeeId, FirstName, LastName, MiddleName, DateOfBirth, Email, Phone, Address, JoiningDate, TerminationDate, Status, DepartmentId, PositionId, ManagerId, CreatedAt, CreatedBy, IsDeleted)
    VALUES (@JohnDoeId, 'EMP001', 'John', 'Doe', NULL, '1980-05-15', 'john.doe@example.com', '555-123-4567', '123 Main St, Anytown, USA', '2020-01-15', NULL, 0, @HRDeptId, @HRManagerId, NULL, GETDATE(), 'System', 0);
    
    -- IT Manager
    INSERT INTO Employee (Id, EmployeeId, FirstName, LastName, MiddleName, DateOfBirth, Email, Phone, Address, JoiningDate, TerminationDate, Status, DepartmentId, PositionId, ManagerId, CreatedAt, CreatedBy, IsDeleted)
    VALUES (@JaneSmithId, 'EMP002', 'Jane', 'Smith', 'Marie', '1982-08-22', 'jane.smith@example.com', '555-234-5678', '456 Oak Ave, Anytown, USA', '2020-02-01', NULL, 0, @ITDeptId, @ITManagerId, NULL, GETDATE(), 'System', 0);
    
    -- Finance Manager
    INSERT INTO Employee (Id, EmployeeId, FirstName, LastName, MiddleName, DateOfBirth, Email, Phone, Address, JoiningDate, TerminationDate, Status, DepartmentId, PositionId, ManagerId, CreatedAt, CreatedBy, IsDeleted)
    VALUES (@RobertJohnsonId, 'EMP003', 'Robert', 'Johnson', NULL, '1979-11-10', 'robert.johnson@example.com', '555-345-6789', '789 Pine St, Anytown, USA', '2020-03-01', NULL, 0, @FinanceDeptId, @FinanceManagerId, NULL, GETDATE(), 'System', 0);
    
    -- Marketing Manager
    INSERT INTO Employee (Id, EmployeeId, FirstName, LastName, MiddleName, DateOfBirth, Email, Phone, Address, JoiningDate, TerminationDate, Status, DepartmentId, PositionId, ManagerId, CreatedAt, CreatedBy, IsDeleted)
    VALUES (@SarahWilliamsId, 'EMP004', 'Sarah', 'Williams', 'Jane', '1985-04-18', 'sarah.williams@example.com', '555-456-7890', '101 Maple Dr, Anytown, USA', '2020-04-01', NULL, 0, @MarketingDeptId, @MarketingManagerId, NULL, GETDATE(), 'System', 0);
    
    -- Operations Manager
    INSERT INTO Employee (Id, EmployeeId, FirstName, LastName, MiddleName, DateOfBirth, Email, Phone, Address, JoiningDate, TerminationDate, Status, DepartmentId, PositionId, ManagerId, CreatedAt, CreatedBy, IsDeleted)
    VALUES (@MichaelBrownId, 'EMP005', 'Michael', 'Brown', 'David', '1983-09-30', 'michael.brown@example.com', '555-567-8901', '202 Cedar Ln, Anytown, USA', '2020-05-01', NULL, 0, @OperationsDeptId, @OperationsManagerId, NULL, GETDATE(), 'System', 0);
    
    -- Now insert staff with managers
    -- HR Specialist
    INSERT INTO Employee (Id, EmployeeId, FirstName, LastName, MiddleName, DateOfBirth, Email, Phone, Address, JoiningDate, TerminationDate, Status, DepartmentId, PositionId, ManagerId, CreatedAt, CreatedBy, IsDeleted)
    VALUES (NEWID(), 'EMP006', 'Emily', 'Davis', NULL, '1988-07-12', 'emily.davis@example.com', '555-678-9012', '303 Birch Rd, Anytown, USA', '2021-01-15', NULL, 0, @HRDeptId, @HRSpecialistId, @JohnDoeId, GETDATE(), 'System', 0);
    
    -- Software Developer
    INSERT INTO Employee (Id, EmployeeId, FirstName, LastName, MiddleName, DateOfBirth, Email, Phone, Address, JoiningDate, TerminationDate, Status, DepartmentId, PositionId, ManagerId, CreatedAt, CreatedBy, IsDeleted)
    VALUES (NEWID(), 'EMP007', 'David', 'Wilson', NULL, '1990-03-25', 'david.wilson@example.com', '555-789-0123', '404 Elm St, Anytown, USA', '2021-02-01', NULL, 0, @ITDeptId, @SoftwareDeveloperId, @JaneSmithId, GETDATE(), 'System', 0);
    
    -- System Administrator
    INSERT INTO Employee (Id, EmployeeId, FirstName, LastName, MiddleName, DateOfBirth, Email, Phone, Address, JoiningDate, TerminationDate, Status, DepartmentId, PositionId, ManagerId, CreatedAt, CreatedBy, IsDeleted)
    VALUES (NEWID(), 'EMP008', 'Jennifer', 'Taylor', 'Lynn', '1987-12-05', 'jennifer.taylor@example.com', '555-890-1234', '505 Walnut Ave, Anytown, USA', '2021-03-01', NULL, 0, @ITDeptId, @SystemAdminId, @JaneSmithId, GETDATE(), 'System', 0);
    
    -- Accountant
    INSERT INTO Employee (Id, EmployeeId, FirstName, LastName, MiddleName, DateOfBirth, Email, Phone, Address, JoiningDate, TerminationDate, Status, DepartmentId, PositionId, ManagerId, CreatedAt, CreatedBy, IsDeleted)
    VALUES (NEWID(), 'EMP009', 'Christopher', 'Anderson', NULL, '1986-09-18', 'chris.anderson@example.com', '555-901-2345', '606 Cherry St, Anytown, USA', '2021-04-01', NULL, 0, @FinanceDeptId, @AccountantId, @RobertJohnsonId, GETDATE(), 'System', 0);
    
    -- Marketing Specialist
    INSERT INTO Employee (Id, EmployeeId, FirstName, LastName, MiddleName, DateOfBirth, Email, Phone, Address, JoiningDate, TerminationDate, Status, DepartmentId, PositionId, ManagerId, CreatedAt, CreatedBy, IsDeleted)
    VALUES (NEWID(), 'EMP010', 'Jessica', 'Martinez', 'Ann', '1992-05-20', 'jessica.martinez@example.com', '555-012-3456', '707 Spruce Dr, Anytown, USA', '2021-05-01', NULL, 0, @MarketingDeptId, @MarketingSpecialistId, @SarahWilliamsId, GETDATE(), 'System', 0);
    
    -- Operations Specialist
    INSERT INTO Employee (Id, EmployeeId, FirstName, LastName, MiddleName, DateOfBirth, Email, Phone, Address, JoiningDate, TerminationDate, Status, DepartmentId, PositionId, ManagerId, CreatedAt, CreatedBy, IsDeleted)
    VALUES (NEWID(), 'EMP011', 'Daniel', 'Thompson', NULL, '1989-11-15', 'daniel.thompson@example.com', '555-123-4567', '808 Ash Ln, Anytown, USA', '2021-06-01', NULL, 0, @OperationsDeptId, @OperationsSpecialistId, @MichaelBrownId, GETDATE(), 'System', 0);
    
    -- Employee on leave
    INSERT INTO Employee (Id, EmployeeId, FirstName, LastName, MiddleName, DateOfBirth, Email, Phone, Address, JoiningDate, TerminationDate, Status, DepartmentId, PositionId, ManagerId, CreatedAt, CreatedBy, IsDeleted)
    VALUES (NEWID(), 'EMP012', 'Amanda', 'Garcia', 'Rose', '1991-02-28', 'amanda.garcia@example.com', '555-234-5678', '909 Maple Ct, Anytown, USA', '2021-07-01', NULL, 1, @MarketingDeptId, @MarketingSpecialistId, @SarahWilliamsId, GETDATE(), 'System', 0);
    
    -- Terminated employee
    INSERT INTO Employee (Id, EmployeeId, FirstName, LastName, MiddleName, DateOfBirth, Email, Phone, Address, JoiningDate, TerminationDate, Status, DepartmentId, PositionId, ManagerId, CreatedAt, CreatedBy, IsDeleted)
    VALUES (NEWID(), 'EMP013', 'Brian', 'Lee', NULL, '1984-08-10', 'brian.lee@example.com', '555-345-6789', '1010 Oak St, Anytown, USA', '2021-08-01', '2023-01-15', 3, @ITDeptId, @SoftwareDeveloperId, @JaneSmithId, GETDATE(), 'System', 0);
    
    -- Retired employee
    INSERT INTO Employee (Id, EmployeeId, FirstName, LastName, MiddleName, DateOfBirth, Email, Phone, Address, JoiningDate, TerminationDate, Status, DepartmentId, PositionId, ManagerId, CreatedAt, CreatedBy, IsDeleted)
    VALUES (NEWID(), 'EMP014', 'Richard', 'Clark', 'James', '1960-06-20', 'richard.clark@example.com', '555-456-7890', '1111 Pine Ave, Anytown, USA', '2000-01-01', '2022-12-31', 4, @FinanceDeptId, @AccountantId, @RobertJohnsonId, GETDATE(), 'System', 0);
    
    -- Suspended employee
    INSERT INTO Employee (Id, EmployeeId, FirstName, LastName, MiddleName, DateOfBirth, Email, Phone, Address, JoiningDate, TerminationDate, Status, DepartmentId, PositionId, ManagerId, CreatedAt, CreatedBy, IsDeleted)
    VALUES (NEWID(), 'EMP015', 'Lisa', 'Rodriguez', NULL, '1993-10-05', 'lisa.rodriguez@example.com', '555-567-8901', '1212 Cedar St, Anytown, USA', '2022-01-15', NULL, 2, @OperationsDeptId, @OperationsSpecialistId, @MichaelBrownId, GETDATE(), 'System', 0);
END
GO

-- Insert sample Leave records
IF NOT EXISTS (SELECT * FROM Leave)
BEGIN
    DECLARE @EmployeeIds TABLE (Id UNIQUEIDENTIFIER);
    INSERT INTO @EmployeeIds SELECT Id FROM Employee;
    
    DECLARE @EmployeeId UNIQUEIDENTIFIER;
    
    -- Get first employee for annual leave
    SELECT TOP 1 @EmployeeId = Id FROM @EmployeeIds;
    INSERT INTO Leave (Id, EmployeeId, LeaveType, StartDate, EndDate, Reason, Status, CreatedAt, CreatedBy, IsDeleted)
    VALUES (NEWID(), @EmployeeId, 0, '2023-06-01', '2023-06-07', 'Annual vacation', 1, GETDATE(), 'System', 0);
    
    -- Get second employee for sick leave
    SELECT TOP 1 @EmployeeId = Id FROM @EmployeeIds WHERE Id NOT IN (SELECT Id FROM Leave);
    INSERT INTO Leave (Id, EmployeeId, LeaveType, StartDate, EndDate, Reason, Status, CreatedAt, CreatedBy, IsDeleted)
    VALUES (NEWID(), @EmployeeId, 1, '2023-07-10', '2023-07-12', 'Sick leave - flu', 1, GETDATE(), 'System', 0);
    
    -- Get third employee for personal leave
    SELECT TOP 1 @EmployeeId = Id FROM @EmployeeIds WHERE Id NOT IN (SELECT Id FROM Leave);
    INSERT INTO Leave (Id, EmployeeId, LeaveType, StartDate, EndDate, Reason, Status, CreatedAt, CreatedBy, IsDeleted)
    VALUES (NEWID(), @EmployeeId, 2, '2023-08-15', '2023-08-16', 'Personal appointment', 1, GETDATE(), 'System', 0);
    
    -- Get fourth employee for maternity leave
    SELECT TOP 1 @EmployeeId = Id FROM @EmployeeIds WHERE Id NOT IN (SELECT Id FROM Leave);
    INSERT INTO Leave (Id, EmployeeId, LeaveType, StartDate, EndDate, Reason, Status, CreatedAt, CreatedBy, IsDeleted)
    VALUES (NEWID(), @EmployeeId, 3, '2023-09-01', '2023-12-01', 'Maternity leave', 1, GETDATE(), 'System', 0);
    
    -- Get fifth employee for pending leave request
    SELECT TOP 1 @EmployeeId = Id FROM @EmployeeIds WHERE Id NOT IN (SELECT Id FROM Leave);
    INSERT INTO Leave (Id, EmployeeId, LeaveType, StartDate, EndDate, Reason, Status, CreatedAt, CreatedBy, IsDeleted)
    VALUES (NEWID(), @EmployeeId, 0, '2023-10-10', '2023-10-15', 'Family vacation', 0, GETDATE(), 'System', 0);
    
    -- Get sixth employee for rejected leave request
    SELECT TOP 1 @EmployeeId = Id FROM @EmployeeIds WHERE Id NOT IN (SELECT Id FROM Leave);
    INSERT INTO Leave (Id, EmployeeId, LeaveType, StartDate, EndDate, Reason, Status, CreatedAt, CreatedBy, IsDeleted)
    VALUES (NEWID(), @EmployeeId, 0, '2023-11-20', '2023-11-30', 'Extended vacation', 2, GETDATE(), 'System', 0);
    
    -- Get seventh employee for cancelled leave request
    SELECT TOP 1 @EmployeeId = Id FROM @EmployeeIds WHERE Id NOT IN (SELECT Id FROM Leave);
    INSERT INTO Leave (Id, EmployeeId, LeaveType, StartDate, EndDate, Reason, Status, CreatedAt, CreatedBy, IsDeleted)
    VALUES (NEWID(), @EmployeeId, 1, '2023-12-05', '2023-12-06', 'Doctor appointment', 3, GETDATE(), 'System', 0);
END
GO

-- Insert sample Attendance records
IF NOT EXISTS (SELECT * FROM Attendance)
BEGIN
    DECLARE @EmployeeIds TABLE (Id UNIQUEIDENTIFIER);
    INSERT INTO @EmployeeIds SELECT Id FROM Employee WHERE Status = 0; -- Only active employees
    
    DECLARE @EmployeeId UNIQUEIDENTIFIER;
    DECLARE @CurrentDate DATE = DATEADD(DAY, -14, GETDATE()); -- Start from 14 days ago
    
    WHILE @CurrentDate <= GETDATE()
    BEGIN
        -- Skip weekends
        IF DATEPART(WEEKDAY, @CurrentDate) NOT IN (1, 7) -- Sunday and Saturday
        BEGIN
            -- For each active employee
            DECLARE employee_cursor CURSOR FOR 
            SELECT Id FROM @EmployeeIds;
            
            OPEN employee_cursor;
            FETCH NEXT FROM employee_cursor INTO @EmployeeId;
            
            WHILE @@FETCH_STATUS = 0
            BEGIN
                -- Regular attendance (9:00 AM - 5:00 PM)
                IF RAND() < 0.8 -- 80% chance of regular attendance
                BEGIN
                    INSERT INTO Attendance (Id, EmployeeId, Date, CheckInTime, CheckOutTime, Status, CreatedAt, CreatedBy, IsDeleted)
                    VALUES (NEWID(), @EmployeeId, @CurrentDate, '09:00:00', '17:00:00', 0, GETDATE(), 'System', 0);
                END
                -- Late arrival
                ELSE IF RAND() < 0.5 -- 10% chance of late arrival
                BEGIN
                    INSERT INTO Attendance (Id, EmployeeId, Date, CheckInTime, CheckOutTime, Status, CreatedAt, CreatedBy, IsDeleted)
                    VALUES (NEWID(), @EmployeeId, @CurrentDate, '09:30:00', '17:00:00', 1, GETDATE(), 'System', 0);
                END
                -- Early departure
                ELSE IF RAND() < 0.5 -- 5% chance of early departure
                BEGIN
                    INSERT INTO Attendance (Id, EmployeeId, Date, CheckInTime, CheckOutTime, Status, CreatedAt, CreatedBy, IsDeleted)
                    VALUES (NEWID(), @EmployeeId, @CurrentDate, '09:00:00', '16:30:00', 2, GETDATE(), 'System', 0);
                END
                -- Absent
                ELSE -- 5% chance of absence
                BEGIN
                    INSERT INTO Attendance (Id, EmployeeId, Date, CheckInTime, CheckOutTime, Status, CreatedAt, CreatedBy, IsDeleted)
                    VALUES (NEWID(), @EmployeeId, @CurrentDate, NULL, NULL, 3, GETDATE(), 'System', 0);
                END
                
                FETCH NEXT FROM employee_cursor INTO @EmployeeId;
            END
            
            CLOSE employee_cursor;
            DEALLOCATE employee_cursor;
        END
        
        SET @CurrentDate = DATEADD(DAY, 1, @CurrentDate);
    END
END
GO
