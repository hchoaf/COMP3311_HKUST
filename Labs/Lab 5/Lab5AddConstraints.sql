/* COMP3311 Lab 5 Exercise: AddConstraints.sql */

set feedback off
/********************************************************************************************
/* For each line beginning with the text "--alter table", Complete the specification of the *
/* constraint by replacing the text ">>> COMPLETE THE CONSTRAINT SPECIFICATION <<<" with    *
/* your own constraint code. To make a constraint executable, remove the comment symbols    *
/* "--" at the beginning of the line. A referential integrity constraint for the Facility   *
/* table and a check constraint for the Department table are already completed as examples. *


/* REFERENTIAL INTEGRITY CONSTRAINTS - (see lab notes page 15)*/

-- Example referential integrity constraint for the Facility table.
alter table Facility modify (departmentId references Department(departmentId) on delete cascade);

-- ***** Below this line, complete the referential integrity constraint for the Student table *****
--alter table Student modify >>> COMPLETE THE CONSTRAINT SPECIFICATION <<<;
alter table Student modify (departmentId references Department(departmentId) on delete cascade);
-- ***** Below this line, complete the referential integrity constraint for the Course table *****
--alter table Course modify >>> COMPLETE THE CONSTRAINT SPECIFICATION <<<;
alter table Course modify (departmentId references Department(departmentId) on delete cascade);
-- ***** Below this line, complete the referential integrity constraints for the EnrollsIn table *****
--alter table EnrollsIn modify >>> COMPLETE THE CONSTRAINT SPECIFICATION <<<;
--alter table EnrollsIn modify >>> COMPLETE THE CONSTRAINT SPECIFICATION <<<;
alter table EnrollsIn modify (studentId references Student(studentId) on delete cascade);
alter table EnrollsIn modify (courseId references Course(courseId) on delete cascade);

/* CHECK CONSTRAINTS (see lab notes page 17)*/

-- Example constraint to check that departmentId in the Department table
-- has only a value in the set {BUS, COMP, ELEC, HUMA, MATH}.
alter table Department add constraint CHK_departmentId check (departmentId in ('BUS', 'COMP', 'ELEC', 'HUMA', 'MATH'));

-- ***** Below this line, complete the constraint to check that studentId in the Student table has exactly 8 digits only *****
--alter table Student add constraint CHK_studentId check >>> COMPLETE THE CONSTRAINT SPECIFICATION <<<;
alter table Student add constraint CHK_studentId check (length(studentId) = 8 and regexp_like(studentId, '^[0-9]'));
-- ***** Below this line, complete the constraint to check that phoneNo in the Student table has exactly 8 digits only *****
--alter table Student add constraint CHK_phoneNo check >>> COMPLETE THE CONSTRAINT SPECIFICATION <<<;
alter table Student add constraint CHK_phoneNo check (length(phoneNo) = 8 and regexp_like(phoneNo, '^[0-9]'));
-- ***** Below this line, complete the constraint to check that cga in the Student table has a value between 0 and 4 *****
--alter table Student add constraint CHK_cga check >>> COMPLETE THE CONSTRAINT SPECIFICATION <<<;
alter table Student add constraint CHK_cga check (cga between 0 and 4);
-- ***** Below this line, complete the constraint to check that admissionYear in the Student table begins with a 2 and has exactly 4 digits only *****
--alter table Student add constraint CHK_admissionYear check >>> COMPLETE THE CONSTRAINT SPECIFICATION <<<;
alter table Student add constraint CHK_admissionYear check (admissionYear like '2%' and length(admissionYear) = 4);
-- ***** Below this line, complete the constraint to check that course id in the Course table follows the pattern 
--      of exactly four uppercase letters followed by exactly four digits *****
--alter table Course add constraint CHK_courseId check >>> COMPLETE THE CONSTRAINT SPECIFICATION <<<;
alter table Course add constraint CHK_courseId check (regexp_like(courseId, '^[A-Z]{4}') and regexp_like(courseId, '[0-9]$'));
-- ***** Below this line, complete the constraint to check that grade in the EnrollsIn table has a value between 0 and 100 *****
--alter table EnrollsIn add constraint CHK_grade check >>> COMPLETE THE CONSTRAINT SPECIFICATION <<<;
alter table EnrollsIn add constraint CHK_grade check (grade between 0 and 100);

/* UNQUE CONSTRAINT (see lab notes page 14)*/
-- ***** Below this line, complete the constraint to enforce the uniqueness of email values in the Student table *****
--alter table Student add constraint UNIQUE_email >>> COMPLETE THE CONSTRAINT SPECIFICATION <<<;
alter table Student add constraint UNIQUE_email unique(email);

commit;