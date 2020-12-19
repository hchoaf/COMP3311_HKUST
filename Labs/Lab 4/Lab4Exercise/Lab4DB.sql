/* COMP3311 Lab 4 Exercise: Lab4DB.sql */


select *
from EnrollsIn
order by studentid;

clear screen
set feedback off
set heading off
select '*** Creating Lab 4 database ***' from dual;
set heading on

/* Start with a clean database */
drop table EnrollsIn;
drop table Facility;
drop table Department;
drop table Student;
drop table Course;


/* Create the tables */
create table Student (
	studentId		char(8) not null,
	firstName		varchar2(20) not null,
	lastName		varchar2(25) not null,
	email			varchar2(15) not null,
	phoneNo         char(8),
	cga				number(3,2),
	departmentId	char(4) not null,
	admissionYear   char(4) not null);

create table EnrollsIn (
 	studentId	char(8) not null,
	courseId	char(8));

create table Course (
	courseId		char(8) not null,
	departmentId	char(4) not null,
	courseName		varchar2(40) not null,
	instructor		varchar2(30) not null);

create table Department (
	departmentId	char(4) not null,
	departmentName	varchar2(40) not null,
	roomNo		char(4));

create table Facility ( 
  	departmentId        char(4) not null,
	numberProjectors    int,
	numberComputers     int);

/* Populate the tables with data */
insert into Student values ('13455789','Harry','Potter','cspotter','23581234',2.76,'COMP','2017');
insert into Student values ('15456789','Leonardo','Da Vinci','csdavinci','23585678',2.72,'COMP','2017');
insert into Student values ('13556789','Legolas','Greenleaf','magreenleaf','23582468',3.36,'MATH','2018');
insert into Student values ('13456789','Ariana','Grande','csgrande','23581234',2.82,'COMP','2018');
insert into Student values ('15678989','Maria','Callas','cscallas','23589876',2.73,'COMP','2018');
insert into Student values ('15678901','Albert','Einstein','cseinstein','23585678',2.56,'COMP','2017');
insert into Student values ('16789012','Robert','Redford','maredford','23582468',2.57,'MATH','2018');
insert into Student values ('14567890','Julius','Caesar','eecaesar','23589876',1.9,'ELEC','2018');
insert into Student values ('99987654','Lazzy','Lazy','cslazy','23581357',null,'COMP','2018');
insert into Student values ('26184624','Bruce','Wayne','eewayne','28261057',2.47,'ELEC','2017');
insert into Student values ('26184444','Donald','Trump','bstrump','28255057',1.49,'BUS','2018');
insert into Student values ('26186666','Warren','Buffet','bsbuffet','28266027',3.42,'BUS','2017');
insert into Student values ('66666666','Ferris','Bueller','bsbueller','28282727',1.64,'BUS','2017');
insert into Student values ('15000655','Steve','Jobs','csjobs','26232244',3.45,'COMP','2017');
insert into Student values ('15085942','Bill','Gates','csgates','25678679',3.4,'COMP','2018');
insert into Student values ('28834512','Isaac','Newton','manewton','22861987',2.98,'MATH','2017');
insert into Student values ('28918856','Alan','Turing','maturing','26679834',3.56,'MATH','2017');
insert into Student values ('29873381','Nikola','Tesla','eetesla','25671983',3.37,'ELEC','2017');
insert into Student values ('13782973','Edith','Clarke','eeclarke','28340180',3.15,'ELEC','2017');
insert into Student values ('18792018','Elon','Musk','bsmusk','28659910',3.25,'BUS','2018');

insert into EnrollsIn values ('13455789','COMP3311');
insert into EnrollsIn values ('15456789','COMP3311');
insert into EnrollsIn values ('13556789','COMP3311');
insert into EnrollsIn values ('14567890','COMP3311');
insert into EnrollsIn values ('13456789','COMP3311');
insert into EnrollsIn values ('15678989','COMP3311');
insert into EnrollsIn values ('15678901','COMP3311');
insert into EnrollsIn values ('26184624','COMP3311');
insert into EnrollsIn values ('26184444','COMP3311');
insert into EnrollsIn values ('26186666','COMP3311');
insert into EnrollsIn values ('66666666','COMP3311');
insert into EnrollsIn values ('15000655','COMP3311');
insert into EnrollsIn values ('15085942','COMP3311');
insert into EnrollsIn values ('28834512','COMP3311');
insert into EnrollsIn values ('28918856','COMP3311');
insert into EnrollsIn values ('29873381','COMP3311');
insert into EnrollsIn values ('18792018','COMP3311');

insert into EnrollsIn values ('13455789','COMP4021');
insert into EnrollsIn values ('15456789','COMP4021');
insert into EnrollsIn values ('13556789','COMP4021');
insert into EnrollsIn values ('14567890','COMP4021');
insert into EnrollsIn values ('13456789','COMP4021');
insert into EnrollsIn values ('15678989','COMP4021');
insert into EnrollsIn values ('15678901','COMP4021');
insert into EnrollsIn values ('16789012','COMP4021');
insert into EnrollsIn values ('26184624','COMP4021');
insert into EnrollsIn values ('26186666','COMP4021');
insert into EnrollsIn values ('66666666','COMP4021');
insert into EnrollsIn values ('15000655','COMP4021');
insert into EnrollsIn values ('15085942','COMP4021');
insert into EnrollsIn values ('28918856','COMP4021');
insert into EnrollsIn values ('13782973','COMP4021');
insert into EnrollsIn values ('18792018','COMP4021');

insert into EnrollsIn values ('13455789','ELEC3100');
insert into EnrollsIn values ('15456789','ELEC3100');
insert into EnrollsIn values ('13556789','ELEC3100');
insert into EnrollsIn values ('14567890','ELEC3100');
insert into EnrollsIn values ('13456789','ELEC3100');
insert into EnrollsIn values ('15678989','ELEC3100');
insert into EnrollsIn values ('15678901','ELEC3100');
insert into EnrollsIn values ('26184624','ELEC3100');
insert into EnrollsIn values ('26184444','ELEC3100');
insert into EnrollsIn values ('26186666','ELEC3100');
insert into EnrollsIn values ('66666666','ELEC3100');
insert into EnrollsIn values ('15085942','ELEC3100');
insert into EnrollsIn values ('28918856','ELEC3100');
insert into EnrollsIn values ('29873381','ELEC3100');
insert into EnrollsIn values ('13782973','ELEC3100');

insert into EnrollsIn values ('13455789','HUMA1020');
insert into EnrollsIn values ('15456789','HUMA1020');
insert into EnrollsIn values ('13556789','HUMA1020');
insert into EnrollsIn values ('14567890','HUMA1020');
insert into EnrollsIn values ('13456789','HUMA1020');
insert into EnrollsIn values ('15678989','HUMA1020');
insert into EnrollsIn values ('66666666','HUMA1020');
insert into EnrollsIn values ('15085942','HUMA1020');
insert into EnrollsIn values ('13782973','HUMA1020');
insert into EnrollsIn values ('18792018','HUMA1020');

insert into EnrollsIn values ('13455789','MATH2421');
insert into EnrollsIn values ('15456789','MATH2421');
insert into EnrollsIn values ('14567890','MATH2421');
insert into EnrollsIn values ('13556789','MATH2421');
insert into EnrollsIn values ('13456789','MATH2421');
insert into EnrollsIn values ('15678989','MATH2421');
insert into EnrollsIn values ('15678901','MATH2421');
insert into EnrollsIn values ('16789012','MATH2421');
insert into EnrollsIn values ('26184624','MATH2421');
insert into EnrollsIn values ('26184444','MATH2421');
insert into EnrollsIn values ('26186666','MATH2421');
insert into EnrollsIn values ('66666666','MATH2421');
insert into EnrollsIn values ('15000655','MATH2421');
insert into EnrollsIn values ('15085942','MATH2421');
insert into EnrollsIn values ('28834512','MATH2421');
insert into EnrollsIn values ('28918856','MATH2421');
insert into EnrollsIn values ('29873381','MATH2421');
insert into EnrollsIn values ('13782973','MATH2421');

insert into Course values ('COMP3311','COMP','Database Management Systems','Chen Lei');
insert into Course values ('COMP4021','COMP','Internet Computing','David Rossiter');
insert into Course values ('ELEC3100','ELEC','Signal Processing and Communications','Electronic Man');
insert into Course values ('MATH2421','MATH','Probability','Isaac Newton');
insert into Course values ('HUMA1020','HUMA','Chinese Writing and Culture','Human Man');

insert into Department values ('COMP','Computer Science',3528);
insert into Department values ('MATH','Mathematics',3461);
insert into Department values ('ELEC','Electronic Engineering',2528);
insert into Department values ('BUS','Business',4528);
insert into Department values ('HUMA','Humanities',1200);

insert into Facility values ('COMP',15,250);
insert into Facility values ('MATH',5,50);
insert into Facility values ('ELEC',14,150);
insert into Facility values ('BUS',10,130);

/* Write the data to disk */
set feedback on
commit;

set feedback off
set heading off
select '*** Inserting your student and enrollment records ***' from dual;
set heading on
set feedback on

/* Execute the referenced script file */
@InsertMyself