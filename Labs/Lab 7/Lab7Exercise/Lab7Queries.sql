/* COMP3311 Lab 7 Exercise: Lab7Queries.sql */

clear screen
set serveroutput on
set pagesize 30
set termout off
@Lab7DB
set termout on
set feedback off

exec Lab7DuplicateEmailCheck;
exec Lab7CgaCalculations;
select studentId, firstName, lastName, cga from Student order by cga desc;
select studentId, firstName, lastName, cga from LowCga order by cga desc;