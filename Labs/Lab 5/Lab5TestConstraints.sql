/* COMP3311 Lab 5 Exercise: Lab5TestConstraints.sql */

clear screen

/* Reset the database and modify the table constraints. */
@Lab5DB
@Lab5AddConstraints
set feedback off
set linesize 150;

set heading off
select '<<<<<---------- STUDENTS WHOSE AVERAGE GRADE IS >90 ---------->>>>>' from dual;
set heading on

select studentId, firstName, lastName, avg(grade)
from Student natural join EnrollsIn
group by studentId, firstName, lastName
having avg(grade)>90
order by avg(grade) desc;

/* Tests for referential integrity constrainst */
set heading off
select '<<<<<---------- TESTS FOR REFERENTIAL INTEGRITY CONSTRAINTS ---------->>>>>' from dual;
select '*** 1. REFERENTIAL INTEGRITY on Facility table: No matching department id in Department table.' from dual;
set feedback on
insert into Facility values ('CHEM', 2, 5);
set feedback off

select '*** 2. REFERENTIAL INTEGRITY on Student table: No matching departmentId in Department table.' from dual;
set feedback on
insert into Student values ('22222222', '*** Student ***', 'RI: No matching dept id', 'afong', '22223334', 0.00, 'PHYS', '2016');
set feedback off
select '*** 3. REFERENTIAL INTEGRITY on Student table: Should delete 1 row and select 0 rows.' from dual;
set feedback on
delete from Student where studentId = '26184444';
select * from EnrollsIn where studentId = '26184444';
set feedback off

select '*** 4. REFERENTIAL INTEGRITY on Course table: No matching department id in Department table.' from dual;
set feedback on
insert into Course values ('PHYS4311', '*** ', '*** Course ***', 'RI: No matching dept id');
set feedback off

select '*** 5. REFERENTIAL INTEGRITY on EnrollsIn table: No matching course id in Course table.' from dual;
set feedback on
insert into EnrollsIn values ('13456789', 'PHYS4311', 80.6);
set feedback off
select '*** 6. REFERENTIAL INTEGRITY on EnrollsIn table: No matching student id in Student table.' from dual;
set feedback on
insert into EnrollsIn values ('12345678', 'COMP3311', 75.6);
set feedback off

/* Tests for check constraints */
select '<<<<<---------- TESTS FOR CHECK CONSTRAINTS ---------->>>>>' from dual;
select '*** 7. CHECK CONSTRAINT on Department table: departmentId must be one of BUS, COMP, ELEC, HUMA or MATH.' from dual;
set feedback on
insert into Department values ('DEPT','*** Department table: CHK: deptId','0000');
set feedback off

select '*** 8. CHECK CONSTRAINT on Student table: studentId must be all digits.' from dual;
set feedback on
insert into Student values ('234567*9', '*** Student table:', 'CHK: studentId', 'afong', '22223334', 0.00, 'COMP', '2016');
set feedback off

select '*** 9. CHECK CONSTRAINT on Student table: studentId must be exactly 8 digits.' from dual;
set feedback on
insert into Student values ('234567', '*** Student table:', 'CHK: studentId', 'bfong', '22223334', 0.00, 'COMP', '2016');
set feedback off

select '*** 10. CHECK CONSTRAINT on Student table: phoneNo must be all digits.' from dual;
set feedback on
insert into Student values ('33333333', '*** Student table:', 'CHK: studentId', 'cfong', '222a3334', 0.00, 'COMP', '2016');
set feedback off

select '*** 11. CHECK CONSTRAINT on Student table: phoneNo must be exactly 8 digits.' from dual;
set feedback on
insert into Student values ('44444444', '*** Student table:', 'CHK: studentId', 'dfong', '2222', 0.00, 'COMP', '2016');
set feedback off

select '*** 12. CHECK CONSTRAINT on Student table: cga must be between 0 and 4.' from dual;
set feedback on
insert into Student values ('55555555', '*** Student table:', 'CHK: cga', 'ali', '25524334', 4.50, 'COMP', '2016');
set feedback off

select '*** 13. CHECK CONSTRAINT on Student table: admissionYear must be all digits.' from dual;
set feedback on
insert into Student values ('77777777', '*** Student table:', 'CHK: admissionYear', 'bli', '25524334', 4.50, 'COMP', '200a');
set feedback off

select '*** 14. CHECK CONSTRAINT on Student table: admissionYear has exactly 4 digits.' from dual;
set feedback on
insert into Student values ('88888888', '*** Student table:', 'CHK: admissionYear', 'cli', '25524334', 4.50, 'COMP', '200');
set feedback off

select '*** 15. CHECK CONSTRAINT on Student table: admissionYear begins with a 2.' from dual;
set feedback on
insert into Student values ('99999999', '*** Student table:', 'CHK: admissionYear', 'dli', '25524334', 4.50, 'COMP', '1999');
set feedback off

select '*** 16. CHECK CONSTRAINT on Course table: course id must be exactly four uppercase letters followed by exactly four digits.' from dual;
set feedback on
insert into course values ('comp3311','COMP','** Course Table: CHK: courseId','Test Man');
set feedback off

select '*** 17. CHECK CONSTRAINT on EnrollsIn table: grade must be between 0 and 100.' from dual;
set feedback on
insert into EnrollsIn values ('99987654', 'COMP3311', 100.1);
set feedback off

/* Test for unique constraint */
select '<<<<<---------- TEST FOR UNIQUE CONSTRAINT ---------->>>>>' from dual;
select '*** 18. CHECK CONSTRAINT on Student table: email must be unique.' from dual;
set feedback on
insert into Student values ('10101010', '*** Student table:', 'CHK: eEmail', 'csgrande', '22223334', 0.00, 'COMP', '2018');
set feedback off

commit;