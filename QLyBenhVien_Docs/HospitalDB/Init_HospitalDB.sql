USE HospitalDB;
GO

-- 2. Tạo các bảng danh mục (Independent Tables) trước để tránh lỗi khóa ngoại

-- Bảng: INSURANCE (Bảo hiểm)
CREATE TABLE INSURANCE (
    InsuranceID INT IDENTITY(1,1) PRIMARY KEY,
    InsuranceProvider NVARCHAR(100) NOT NULL,
    PolicyNumber NVARCHAR(50) NOT NULL,
    PolicyHolderName NVARCHAR(100) NOT NULL,
    PolicyStartDate DATE,
    PolicyEndDate DATE,
    CoverageAmount DECIMAL(18, 2),
    Status NVARCHAR(20) DEFAULT 'Active'
);
GO

-- Bảng: DEPARTMENT (Khoa/Phòng ban)
CREATE TABLE DEPARTMENT (
    DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL,
    DepartmentCode NVARCHAR(20) NOT NULL UNIQUE,
    Description NVARCHAR(500),
    Location NVARCHAR(100),
    Phone NVARCHAR(20),
    HeadOfDepartment NVARCHAR(100),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    IsActive BIT DEFAULT 1
);
GO

-- Bảng: ROLE (Vai trò nhân viên)
CREATE TABLE ROLE (
    RoleID INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(200),
    Permissions NVARCHAR(MAX) -- Lưu JSON hoặc danh sách quyền
);
GO

-- Bảng: LAB_TEST_TYPE (Loại xét nghiệm)
CREATE TABLE LAB_TEST_TYPE (
    LabTestTypeID INT IDENTITY(1,1) PRIMARY KEY,
    TestName NVARCHAR(100) NOT NULL,
    TestCode NVARCHAR(20) NOT NULL,
    Category NVARCHAR(50),
    Price DECIMAL(18, 2) NOT NULL,
    Unit NVARCHAR(20),
    Description NVARCHAR(200)
);
GO

-- Bảng: LABORATORY (Phòng Lab)
CREATE TABLE LABORATORY (
    LaboratoryID INT IDENTITY(1,1) PRIMARY KEY,
    LabName NVARCHAR(100) NOT NULL,
    Location NVARCHAR(100),
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    IsActive BIT DEFAULT 1
);
GO

-- Bảng: MEDICATION (Thuốc)
CREATE TABLE MEDICATION (
    MedicationID INT IDENTITY(1,1) PRIMARY KEY,
    MedicationName NVARCHAR(100) NOT NULL,
    GenericName NVARCHAR(100),
    Category NVARCHAR(50),
    Manufacturer NVARCHAR(100),
    UnitPrice DECIMAL(18, 2) NOT NULL,
    StockQuantity INT DEFAULT 0,
    Unit NVARCHAR(20),
    ExpiryDate DATE,
    Description NVARCHAR(200),
    IsActive BIT DEFAULT 1
);
GO

-- 3. Tạo các bảng thực thể chính (Entities dependent on categories)

-- Bảng: PATIENT (Bệnh nhân)
CREATE TABLE PATIENT (
    PatientID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender NVARCHAR(10),
    BloodGroup NVARCHAR(5),
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    Address NVARCHAR(200),
    City NVARCHAR(50),
    EmergencyContact NVARCHAR(100),
    EmergencyPhone NVARCHAR(20),
    InsuranceID INT, -- Có thể Null nếu không có bảo hiểm
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    IsActive BIT DEFAULT 1,
    FOREIGN KEY (InsuranceID) REFERENCES INSURANCE(InsuranceID)
);
GO

-- Bảng: DOCTOR (Bác sĩ)
CREATE TABLE DOCTOR (
    DoctorID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Specialization NVARCHAR(100),
    LicenseNumber NVARCHAR(50) NOT NULL UNIQUE,
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    DepartmentID INT NOT NULL,
    ConsultationFee DECIMAL(18, 2),
    ExperienceYears INT,
    Qualification NVARCHAR(200),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    IsAvailable BIT DEFAULT 1,
    FOREIGN KEY (DepartmentID) REFERENCES DEPARTMENT(DepartmentID)
);
GO

-- Bảng: STAFF (Nhân viên khác)
CREATE TABLE STAFF (
    StaffID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    RoleID INT NOT NULL,
    DepartmentID INT NOT NULL,
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    Address NVARCHAR(200),
    HireDate DATE,
    Salary DECIMAL(18, 2),
    Status NVARCHAR(20) DEFAULT 'Active',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY (RoleID) REFERENCES ROLE(RoleID),
    FOREIGN KEY (DepartmentID) REFERENCES DEPARTMENT(DepartmentID)
);
GO

-- Bảng: ROOM (Phòng bệnh/Phòng khám)
CREATE TABLE ROOM (
    RoomID INT IDENTITY(1,1) PRIMARY KEY,
    RoomNumber NVARCHAR(20) NOT NULL,
    DepartmentID INT NOT NULL,
    RoomType NVARCHAR(50), -- VIP, Standard, ICU...
    Capacity INT DEFAULT 1,
    CurrentOccupancy INT DEFAULT 0,
    PricePerDay DECIMAL(18, 2),
    Status NVARCHAR(20) DEFAULT 'Available',
    Floor NVARCHAR(10),
    IsAvailable BIT DEFAULT 1,
    FOREIGN KEY (DepartmentID) REFERENCES DEPARTMENT(DepartmentID)
);
GO

-- Bảng: DOCTOR_SCHEDULE (Lịch làm việc bác sĩ)
CREATE TABLE DOCTOR_SCHEDULE (
    ScheduleID INT IDENTITY(1,1) PRIMARY KEY,
    DoctorID INT NOT NULL,
    DayOfWeek NVARCHAR(15), -- Monday, Tuesday...
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    MaxPatients INT,
    IsAvailable BIT DEFAULT 1,
    EffectiveDate DATE,
    EndDate DATE,
    FOREIGN KEY (DoctorID) REFERENCES DOCTOR(DoctorID)
);
GO

-- 4. Tạo các bảng nghiệp vụ (Transaction Tables)

-- Bảng: APPOINTMENT (Lịch hẹn)
CREATE TABLE APPOINTMENT (
    AppointmentID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    DoctorID INT NOT NULL,
    AppointmentDate DATETIME NOT NULL,
    AppointmentTime TIME NOT NULL,
    Duration INT DEFAULT 30, -- Phút
    AppointmentType NVARCHAR(50),
    Status NVARCHAR(20) DEFAULT 'Scheduled', -- Scheduled, Completed, Cancelled
    Reason NVARCHAR(200),
    RoomID INT,
    Notes NVARCHAR(500),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    IsCancelled BIT DEFAULT 0,
    FOREIGN KEY (PatientID) REFERENCES PATIENT(PatientID),
    FOREIGN KEY (DoctorID) REFERENCES DOCTOR(DoctorID),
    FOREIGN KEY (RoomID) REFERENCES ROOM(RoomID)
);
GO

-- Bảng: MEDICAL_RECORD (Hồ sơ bệnh án - EMR)
CREATE TABLE MEDICAL_RECORD (
    RecordID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    DoctorID INT NOT NULL,
    AppointmentID INT UNIQUE, -- 1 Lịch hẹn -> 1 Hồ sơ
    VisitDate DATETIME DEFAULT GETDATE(),
    ChiefComplaint NVARCHAR(500),
    Symptoms NVARCHAR(MAX),
    Examination NVARCHAR(MAX),
    Temperature DECIMAL(4, 1),
    BloodPressureSystolic INT,
    BloodPressureDiastolic INT,
    HeartRate INT,
    Weight DECIMAL(5, 2),
    Height DECIMAL(5, 2),
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY (PatientID) REFERENCES PATIENT(PatientID),
    FOREIGN KEY (DoctorID) REFERENCES DOCTOR(DoctorID),
    FOREIGN KEY (AppointmentID) REFERENCES APPOINTMENT(AppointmentID)
);
GO

-- Bảng: DIAGNOSIS (Chẩn đoán)
CREATE TABLE DIAGNOSIS (
    DiagnosisID INT IDENTITY(1,1) PRIMARY KEY,
    RecordID INT NOT NULL,
    DiagnosisCode NVARCHAR(20), -- ICD-10 Code
    DiagnosisName NVARCHAR(200),
    Description NVARCHAR(500),
    Severity NVARCHAR(20), -- Mild, Moderate, Severe
    DiagnosedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RecordID) REFERENCES MEDICAL_RECORD(RecordID)
);
GO

-- Bảng: PRESCRIPTION (Đơn thuốc)
CREATE TABLE PRESCRIPTION (
    PrescriptionID INT IDENTITY(1,1) PRIMARY KEY,
    RecordID INT NOT NULL,
    DoctorID INT NOT NULL,
    PrescriptionDate DATETIME DEFAULT GETDATE(),
    Instructions NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RecordID) REFERENCES MEDICAL_RECORD(RecordID),
    FOREIGN KEY (DoctorID) REFERENCES DOCTOR(DoctorID)
);
GO

-- Bảng: PRESCRIPTION_DETAIL (Chi tiết đơn thuốc)
CREATE TABLE PRESCRIPTION_DETAIL (
    PrescriptionDetailID INT IDENTITY(1,1) PRIMARY KEY,
    PrescriptionID INT NOT NULL,
    MedicationID INT NOT NULL,
    Dosage NVARCHAR(50), -- Liều lượng (vd: 500mg)
    Frequency NVARCHAR(50), -- Tần suất (vd: 2 lần/ngày)
    Duration INT, -- Số ngày dùng
    Unit NVARCHAR(20), -- Viên, Gói...
    Instructions NVARCHAR(200),
    Quantity INT NOT NULL,
    FOREIGN KEY (PrescriptionID) REFERENCES PRESCRIPTION(PrescriptionID),
    FOREIGN KEY (MedicationID) REFERENCES MEDICATION(MedicationID)
);
GO

-- Bảng: LAB_TEST (Kết quả xét nghiệm)
CREATE TABLE LAB_TEST (
    LabTestID INT IDENTITY(1,1) PRIMARY KEY,
    RecordID INT NOT NULL,
    LabTestTypeID INT NOT NULL,
    LaboratoryID INT, -- Lab thực hiện
    OrderDate DATETIME DEFAULT GETDATE(),
    SampleCollectedDate DATETIME,
    ResultDate DATETIME,
    Result NVARCHAR(MAX),
    NormalRange NVARCHAR(100),
    Status NVARCHAR(20) DEFAULT 'Pending',
    TechnicianName NVARCHAR(100),
    Notes NVARCHAR(200),
    FOREIGN KEY (RecordID) REFERENCES MEDICAL_RECORD(RecordID),
    FOREIGN KEY (LabTestTypeID) REFERENCES LAB_TEST_TYPE(LabTestTypeID),
    FOREIGN KEY (LaboratoryID) REFERENCES LABORATORY(LaboratoryID)
);
GO

-- Bảng: ADMISSION (Nhập viện)
CREATE TABLE ADMISSION (
    AdmissionID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    DoctorID INT NOT NULL, -- Bác sĩ phụ trách
    RoomID INT NOT NULL,
    AdmissionDate DATETIME DEFAULT GETDATE(),
    DischargeDate DATETIME,
    AdmissionType NVARCHAR(50), -- Emergency, Transfer...
    Status NVARCHAR(20) DEFAULT 'Admitted',
    Reason NVARCHAR(200),
    DischargeNotes NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY (PatientID) REFERENCES PATIENT(PatientID),
    FOREIGN KEY (DoctorID) REFERENCES DOCTOR(DoctorID),
    FOREIGN KEY (RoomID) REFERENCES ROOM(RoomID)
);
GO

-- Bảng: BILLING (Hóa đơn)
CREATE TABLE BILLING (
    BillingID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    AdmissionID INT, -- Có thể null nếu chỉ khám ngoại trú
    BillingDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(18, 2) NOT NULL,
    DiscountAmount DECIMAL(18, 2) DEFAULT 0,
    TaxAmount DECIMAL(18, 2) DEFAULT 0,
    NetAmount DECIMAL(18, 2) NOT NULL,
    Status NVARCHAR(20) DEFAULT 'Unpaid',
    DueDate DATETIME,
    Notes NVARCHAR(200),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY (PatientID) REFERENCES PATIENT(PatientID),
    FOREIGN KEY (AdmissionID) REFERENCES ADMISSION(AdmissionID)
);
GO

-- Bảng: BILLING_DETAIL (Chi tiết hóa đơn)
CREATE TABLE BILLING_DETAIL (
    BillingDetailID INT IDENTITY(1,1) PRIMARY KEY,
    BillingID INT NOT NULL,
    ServiceType NVARCHAR(50), -- Medication, Consultation, LabTest, Room...
    Description NVARCHAR(200),
    Quantity INT DEFAULT 1,
    UnitPrice DECIMAL(18, 2) NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (BillingID) REFERENCES BILLING(BillingID)
);
GO

-- Bảng: PAYMENT (Thanh toán)
CREATE TABLE PAYMENT (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    BillingID INT NOT NULL,
    PaymentDate DATETIME DEFAULT GETDATE(),
    Amount DECIMAL(18, 2) NOT NULL,
    PaymentMethod NVARCHAR(50), -- Cash, Credit Card...
    TransactionID NVARCHAR(50),
    Status NVARCHAR(20) DEFAULT 'Completed',
    Notes NVARCHAR(200),
    FOREIGN KEY (BillingID) REFERENCES BILLING(BillingID)
);
GO