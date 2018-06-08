CREATE TABLE Majors(
  MajorID INT PRIMARY KEY NOT NULL,
  Name NVARCHAR(30) NOT NULL
)

CREATE TABLE Students(
  StudentID INT PRIMARY KEY NOT NULL,
  StudentNumber INT NOT NULL,
  StudentName NVARCHAR(50) NOT NULL,
  MajorID INT FOREIGN KEY REFERENCES Majors(MajorID)
)

CREATE TABLE Payments(
  PaymentID INT PRIMARY KEY NOT NULL,
  PaymentDate DATE NOT NULL,
  PaymentAmount INT NOT NULL,
  StudentID INT FOREIGN KEY REFERENCES Students(StudentID)
)

CREATE TABLE Subjects(
  SubjectID INT PRIMARY KEY NOT NULL,
  SubjectName NVARCHAR(50) NOT NULL
)

CREATE TABLE Agenda(
  StudentID INT FOREIGN KEY REFERENCES Students(StudentID),
  SubjectID INT FOREIGN KEY REFERENCES Subjects(SubjectID),
  CONSTRAINT PK_CompositeKey PRIMARY KEY (StudentID, SubjectID)
)

