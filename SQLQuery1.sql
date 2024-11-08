-- Bảng Users
CREATE TABLE Users (
    user_id INT PRIMARY KEY IDENTITY(1,1),
    username NVARCHAR(50) NOT NULL,
    password NVARCHAR(255) NOT NULL,
    email NVARCHAR(100) NOT NULL UNIQUE,
    role NVARCHAR(20) NOT NULL CHECK (role IN ('EMPLOYER', 'CANDIDATE', 'ADMIN', 'HR', 'MANAGER', 'RECRUITER', 'SUPERVISOR')),  -- Các vai trò mở rộng
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);

-- Bảng Companies
CREATE TABLE Companies (
    company_id INT PRIMARY KEY IDENTITY(1,1),
    company_name NVARCHAR(100) NOT NULL,
    industry NVARCHAR(50),
    description NVARCHAR(MAX),
    location NVARCHAR(100),
    company_logo NVARCHAR(255) DEFAULT 'https://www.shutterstock.com/image-vector/ui-image-placeholder-wireframes-600nw-1037719192.jpg'  -- Đường dẫn mới cho logo công ty
);

-- Bảng Employers
CREATE TABLE Employers (
    employer_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT NOT NULL,
    company_id INT NOT NULL,
    role NVARCHAR(50),  -- Vai trò của nhà tuyển dụng
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (company_id) REFERENCES Companies(company_id)
);

-- Bảng Candidates
CREATE TABLE Candidates (
    candidate_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT NOT NULL,
    full_name NVARCHAR(100) NOT NULL,
    phone NVARCHAR(15),
    email NVARCHAR(100),
    experience NVARCHAR(MAX),
    resume NVARCHAR(255),
    location NVARCHAR(100),
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

-- Bảng Job_Categories
CREATE TABLE Job_Categories (
    category_id INT PRIMARY KEY IDENTITY(1,1),
    category_name NVARCHAR(50) NOT NULL UNIQUE,
    description NVARCHAR(MAX)
);

-- Bảng Jobs
CREATE TABLE Jobs (
    job_id INT PRIMARY KEY IDENTITY(1,1),
    company_id INT NOT NULL,
    category_id INT NOT NULL,
    title NVARCHAR(100) NOT NULL,
    description NVARCHAR(MAX),
    requirements NVARCHAR(MAX),
    location NVARCHAR(100),
    salary NVARCHAR(50),
    employment_type NVARCHAR(20) NOT NULL CHECK (employment_type IN ('full-time', 'part-time', 'internship')),
    posted_at DATETIME DEFAULT GETDATE(),
    deadline DATETIME,
    is_featured BIT DEFAULT 0,  -- Sử dụng BIT cho kiểu BOOLEAN
    views INT DEFAULT 0,
    application_count INT DEFAULT 0,
    FOREIGN KEY (company_id) REFERENCES Companies(company_id),
    FOREIGN KEY (category_id) REFERENCES Job_Categories(category_id)
);

-- Bảng Applications
CREATE TABLE Applications (
    application_id INT PRIMARY KEY IDENTITY(1,1),
    job_id INT NOT NULL,
    candidate_id INT NOT NULL,
    status NVARCHAR(20) DEFAULT 'applied' CHECK (status IN ('applied', 'shortlisted', 'interviewed', 'rejected', 'hired')),
    applied_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    notes NVARCHAR(MAX),
    FOREIGN KEY (job_id) REFERENCES Jobs(job_id),
    FOREIGN KEY (candidate_id) REFERENCES Candidates(candidate_id)
);

-- Bảng Reviews
CREATE TABLE Reviews (
    review_id INT PRIMARY KEY IDENTITY(1,1),
    job_id INT NOT NULL,
    candidate_id INT NOT NULL,
    rating INT CHECK (rating BETWEEN 1 AND 5),
    review_text NVARCHAR(MAX),
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (job_id) REFERENCES Jobs(job_id),
    FOREIGN KEY (candidate_id) REFERENCES Candidates(candidate_id)
);

-- Bảng Notifications
CREATE TABLE Notifications (
    notification_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT NOT NULL,
    message NVARCHAR(255) NOT NULL,
    is_read BIT DEFAULT 0,  -- Trạng thái đã đọc
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

-- Cấp quyền cho người dùng quản trị cơ sở dữ liệu
ALTER AUTHORIZATION ON DATABASE::DoAn TO sa;


