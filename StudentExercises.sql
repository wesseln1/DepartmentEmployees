CREATE TABLE Cohort (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    CohortName VARCHAR(55) NOT NULL,
    StudentId INTEGER NOT NULL,
    InstructorId INTEGER NOT NULL,
    CONSTRAINT FK_Cohort_Student FOREIGN KEY(StudentId) REFERENCES Student (Id),
    CONSTRAINT FK_Cohort_Instructor FOREIGN KEY(InstructorId) REFERENCES Instructor(Id)
);
CREATE TABLE Instructor (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    FirstName VARCHAR(55) NOT NULL,
    LastName VARCHAR(55) NOT NULL,
    SlackHandle VARCHAR(55) NOT NULL,
    Speciality VARCHAR(55) NOT NULL,
    CohortId INTEGER,
    StudentId INTEGER,
    CONSTRAINT FK_Instructor_Cohort FOREIGN KEY(CohortId) REFERENCES Cohort(Id),
    CONSTRAINT FK_Instructor_Student FOREIGN KEY(StudentId) REFERENCES Student(Id)
);
CREATE TABLE Student (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    FirstName VARCHAR(55) NOT NULL,
    LastName VARCHAR(55) NOT NULL,
    SlackHandle VARCHAR(55) NOT NULL,
    CohortId INTEGER NOT NULL,
    ExerciseId INTEGER,
    CONSTRAINT FK_Student_Cohort FOREIGN KEY(CohortId) REFERENCES Cohort(Id),
    CONSTRAINT FK_Student_Exercise FOREIGN KEY(ExerciseId) REFERENCES Exercise(Id),
);
CREATE TABLE Exercise (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    ExerciseName VARCHAR(55) NOT NULL,
    ExerciseLanguage VARCHAR(55) NOT NULL,
);
CREATE TABLE StudentExercise (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    StudentId INTEGER NOT NULL,
    ExerciseId INTEGER NOT NULL,
)

-- Exercises --
INSERT INTO Exercise (ExerciseName, ExerciseLanguage) VALUES ('Student Exercise 1', 'Javascript');
INSERT INTO Exercise (ExerciseName, ExerciseLanguage) VALUES ('Student Exercise 2', 'Javascript');
INSERT INTO Exercise (ExerciseName, ExerciseLanguage) VALUES ('Student Exercise 3', 'React');
INSERT INTO Exercise (ExerciseName, ExerciseLanguage) VALUES ('Student Exercise 4', 'React');
INSERT INTO Exercise (ExerciseName, ExerciseLanguage) VALUES ('Student Exercise 5', 'React');
INSERT INTO Exercise (ExerciseName, ExerciseLanguage) VALUES ('Student Exercise 6', 'C#');
INSERT INTO Exercise (ExerciseName, ExerciseLanguage) VALUES ('Student Exercise 7', 'C#');
INSERT INTO Exercise (ExerciseName, ExerciseLanguage) VALUES ('Student Exercise 8', 'C#');
INSERT INTO Exercise (ExerciseName, ExerciseLanguage) VALUES ('Student Exercise 9', '.net');
INSERT INTO Exercise (ExerciseName, ExerciseLanguage) VALUES ('Student Exercise 10', 'SQL');
INSERT INTO Exercise (ExerciseName, ExerciseLanguage) VALUES ('Student Exercise 11', 'SQL');
INSERT INTO Exercise (ExerciseName, ExerciseLanguage) VALUES ('Student Exercise 12', 'SQL');

-- Instructors --
INSERT INTO Instructors (FirstName, LastName, Slack) VALUES ('Adam', 'Jack', 'adam.Jack89');


-- Cohorts --
INSERT INTO Cohort (CohortName, ExerciseLanguage) VALUES ('Student Exercise 1', 'Javascript');


-- Students --
INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId) VALUES ('Fortunato', 'Mugnano', 'fortunato.42', 1);
INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId) VALUES ('Seth', 'Oakly', 'seth.oakly', 1);
INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId) VALUES ('David', 'Cornish', 'david.cornish', 1);
INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId) VALUES ('Phil', 'Grismor', 'phil.grismor', 2);
INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId) VALUES ('Nick', 'Riviera', 'nick.riviera', 2);
INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId) VALUES ('Mark', 'Lendez', 'mark.lendez', 3);
INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId) VALUES ('Lauren', 'Ciccio', 'lauren.ciccio', 3);
INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId) VALUES ('Summer', 'Laddington', 'summer.laddington', 3);
INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId) VALUES ('Grady', 'Brown', 'gradi.brown', 2);