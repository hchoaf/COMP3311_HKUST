/* COMP3311 Lab 6 Exercise: Lab6Queries.sql */

clear screen
set serveroutput on
set pagesize 30
set termout off
@Lab6DB
set termout on
set feedback off

exec Lab6CgaCalculations;
select studentId, firstName, lastName, cga from Student order by cga desc;
select studentId, firstName, lastName, cga from Lowcga order by cga desc;