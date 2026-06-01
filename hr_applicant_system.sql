
CREATE DATABASE IF NOT EXISTS hr_applicant_system;
USE hr_applicant_system;

-- Roles
CREATE TABLE Roles (
    RoleID INT AUTO_INCREMENT PRIMARY KEY,
    RoleName VARCHAR(50) NOT NULL,
    Description VARCHAR(255)
);

-- Users
CREATE TABLE Users (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    RoleID INT NOT NULL,
    IsActive TINYINT(1) DEFAULT 1,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

-- ApplicantAccounts
CREATE TABLE ApplicantAccounts (
    AccountID INT AUTO_INCREMENT PRIMARY KEY,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    IsActive TINYINT(1) DEFAULT 1,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Departments
CREATE TABLE Departments (
    DepartmentID INT AUTO_INCREMENT PRIMARY KEY,
    DepartmentName VARCHAR(100) NOT NULL,
    IsActive TINYINT(1) DEFAULT 1
);

-- Applicants
CREATE TABLE Applicants (
    ApplicantID INT AUTO_INCREMENT PRIMARY KEY,
    AccountID INT NOT NULL UNIQUE,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    MiddleName VARCHAR(50),
    BirthDate DATE,
    Gender VARCHAR(20),
    ContactNumber VARCHAR(20),
    Address VARCHAR(255),
    City VARCHAR(100),
    Province VARCHAR(100),
    HighestEducation VARCHAR(100),
    SchoolName VARCHAR(150),
    YearGraduated INT,
    Skills TEXT,
    WorkExperience TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (AccountID) REFERENCES ApplicantAccounts(AccountID)
);

-- JobVacancies
CREATE TABLE JobVacancies (
    VacancyID INT AUTO_INCREMENT PRIMARY KEY,
    JobTitle VARCHAR(100) NOT NULL,
    DepartmentID INT,
    EmploymentType VARCHAR(50),
    Description TEXT,
    Qualifications TEXT,
    Slots INT DEFAULT 1,
    Status VARCHAR(20) DEFAULT 'Open',  -- Open, Closed
    PostedBy INT,
    PostedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    ClosedAt DATETIME,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID),
    FOREIGN KEY (PostedBy) REFERENCES Users(UserID)
);

-- Applications
CREATE TABLE Applications (
    ApplicationID INT AUTO_INCREMENT PRIMARY KEY,
    ApplicantID INT NOT NULL,
    VacancyID INT NOT NULL,
    Status VARCHAR(30) DEFAULT 'Draft',
    -- Status flow:
    -- Draft > Submitted > Under Review > Shortlisted >
    -- For Interview > For Assessment > For Final Review >
    -- Accepted / Rejected / Withdrawn
    SubmittedAt DATETIME,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (ApplicantID) REFERENCES Applicants(ApplicantID),
    FOREIGN KEY (VacancyID) REFERENCES JobVacancies(VacancyID),
    UNIQUE KEY no_duplicate_application (ApplicantID, VacancyID)
    -- This UNIQUE KEY prevents same applicant applying twice to same job
);

-- RequirementTypes
CREATE TABLE RequirementTypes (
    RequirementTypeID INT AUTO_INCREMENT PRIMARY KEY,
    RequirementName VARCHAR(100) NOT NULL,
    IsRequired TINYINT(1) DEFAULT 1,
    IsActive TINYINT(1) DEFAULT 1
);

-- ApplicantDocuments
CREATE TABLE ApplicantDocuments (
    DocumentID INT AUTO_INCREMENT PRIMARY KEY,
    ApplicationID INT NOT NULL,
    RequirementTypeID INT NOT NULL,
    FileName VARCHAR(255),
    FilePath VARCHAR(500),
    Status VARCHAR(30) DEFAULT 'Missing', -- Missing, Submitted, Verified
    Remarks VARCHAR(255),
    SubmittedAt DATETIME,
    FOREIGN KEY (ApplicationID) REFERENCES Applications(ApplicationID),
    FOREIGN KEY (RequirementTypeID) REFERENCES RequirementTypes(RequirementTypeID)
);

-- ScreeningResults
CREATE TABLE ScreeningResults (
    ScreeningID INT AUTO_INCREMENT PRIMARY KEY,
    ApplicationID INT NOT NULL UNIQUE,
    ScreenedBy INT NOT NULL,
    Result VARCHAR(20) NOT NULL, -- Qualified, Not Qualified
    Remarks TEXT,
    ScreenedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ApplicationID) REFERENCES Applications(ApplicationID),
    FOREIGN KEY (ScreenedBy) REFERENCES Users(UserID)
);

-- InterviewSchedules
CREATE TABLE InterviewSchedules (
    ScheduleID INT AUTO_INCREMENT PRIMARY KEY,
    ApplicationID INT NOT NULL,
    InterviewDate DATE NOT NULL,
    InterviewTime TIME NOT NULL,
    Interviewer VARCHAR(100),
    Mode VARCHAR(50),     -- Online, Face-to-face
    Location VARCHAR(255),
    Status VARCHAR(30) DEFAULT 'Scheduled', -- Scheduled, Completed, Cancelled
    ScheduledBy INT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ApplicationID) REFERENCES Applications(ApplicationID),
    FOREIGN KEY (ScheduledBy) REFERENCES Users(UserID)
);

-- InterviewEvaluations
CREATE TABLE InterviewEvaluations (
    EvaluationID INT AUTO_INCREMENT PRIMARY KEY,
    ScheduleID INT NOT NULL UNIQUE,
    EvaluatedBy INT NOT NULL,
    Score DECIMAL(5,2),
    Remarks TEXT,
    Result VARCHAR(20), -- Pass, Fail
    EvaluatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ScheduleID) REFERENCES InterviewSchedules(ScheduleID),
    FOREIGN KEY (EvaluatedBy) REFERENCES Users(UserID)
);

-- HiringDecisions
CREATE TABLE HiringDecisions (
    DecisionID INT AUTO_INCREMENT PRIMARY KEY,
    ApplicationID INT NOT NULL UNIQUE,
    Decision VARCHAR(20) NOT NULL, -- Accepted, Rejected, On Hold
    Remarks TEXT,
    DecidedBy INT NOT NULL,
    DecidedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ApplicationID) REFERENCES Applications(ApplicationID),
    FOREIGN KEY (DecidedBy) REFERENCES Users(UserID)
);

-- ApplicationStatusHistory
CREATE TABLE ApplicationStatusHistory (
    HistoryID INT AUTO_INCREMENT PRIMARY KEY,
    ApplicationID INT NOT NULL,
    OldStatus VARCHAR(30),
    NewStatus VARCHAR(30) NOT NULL,
    ChangedBy VARCHAR(100),   -- Name or role of who changed it
    Remarks VARCHAR(255),
    ChangedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ApplicationID) REFERENCES Applications(ApplicationID)
);

-- AuditTrail
CREATE TABLE AuditTrail (
    AuditID INT AUTO_INCREMENT PRIMARY KEY,
    UserType VARCHAR(30),    -- Applicant or HR
    UserID INT,              -- ID from either Users or ApplicantAccounts
    Action VARCHAR(100),     -- e.g. "Logged In", "Edited Profile", "Accepted Applicant"
    Details TEXT,
    ActionAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Sample Data

-- Insert Roles
INSERT INTO Roles (RoleName, Description) VALUES
('HR Staff', 'Can review applicants, screen, and schedule interviews'),
('HR Manager', 'Can make final hiring decisions and manage vacancies'),
('Admin', 'Full system access including user management');

-- Insert Departments
INSERT INTO Departments (DepartmentName) VALUES
('Human Resources'),
('Information Technology'),
('Finance'),
('Operations'),
('Marketing');

-- Insert Requirement Types
INSERT INTO RequirementTypes (RequirementName, IsRequired) VALUES
('Resume', 1),
('Valid Government ID', 1),
('Transcript of Records', 1),
('Certificate of Employment', 0),
('NBI Clearance', 1);

-- Insert Admin User (Password: admin123 - in real app this should be hashed)
INSERT INTO Users (FullName, Email, Password, RoleID) VALUES
('System Admin', 'admin@hrapp.com', 'admin123', 3),
('HR Manager Juan', 'manager@hrapp.com', 'manager123', 2),
('HR Staff Maria', 'hrstaff@hrapp.com', 'staff123', 1);

-- Insert Sample Job Vacancies
INSERT INTO JobVacancies (JobTitle, DepartmentID, EmploymentType, Description, Qualifications, Slots, PostedBy) VALUES
('IT Support Specialist', 2, 'Full-time', 
 'Handles technical support and system maintenance.', 
 'Graduate of IT or Computer Science, 1 year experience preferred.', 
 3, 1),
('HR Assistant', 1, 'Full-time', 
 'Assists HR department with recruitment and records.', 
 'Graduate of HRDM or related course.', 
 2, 1),
('Accounting Staff', 3, 'Full-time', 
 'Handles bookkeeping and financial records.', 
 'Graduate of Accountancy or Management Accounting.', 
 1, 2);

-- Insert Sample Applicant Account
INSERT INTO ApplicantAccounts (Email, Password) VALUES
('dimitri@garregmach.edu', 'dimitri123'),
('felix@garregmach.edu', 'felix123'),
('sylvain@garregmach.edu', 'sylvain123'),
('ingrid@garregmach.edu', 'ingrid123'),
('annette@garregmach.edu', 'annette123'),
('mercedes@garregmach.edu', 'mercedes123'),
('ashe@garregmach.edu', 'ashe123'),
('dedue@garregmach.edu', 'dedue123');

-- Insert Sample Applicant Profiles
INSERT INTO Applicants (AccountID, FirstName, LastName, MiddleName, BirthDate, Gender, ContactNumber, Address, City, Province, HighestEducation, SchoolName, YearGraduated, Skills) VALUES
(1, 'Juan', 'Dela Cruz', 'Reyes', '2000-05-15', 'Male', '09171234567', '123 Rizal St', 'Dasmariñas', 'Cavite', 'Bachelor''s Degree', 'Cavite State University', 2023, 'C#, MySQL, Networking'),
(2, 'Maria', 'Santos', 'Lopez', '2001-08-22', 'Female', '09281234567', '456 Mabini Ave', 'Imus', 'Cavite', 'Bachelor''s Degree', 'De La Salle University', 2024, 'HR Management, Communication');

