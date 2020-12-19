/* COMP3311 Lab 5 Exercise: Lab5DB.sql */

clear screen
set feedback off
set heading off
select '*** Creating Lab 5 database ***' from dual;
set heading on

/* Start with a clean database */
drop table EnrollsIn;
drop table Facility;
drop table Student;
drop table Course;
drop table Department;

/* Create the tables */
create table Department (
	departmentId    char(4) primary key,
	departmentName  varchar2(40) not null,
	roomNo          char(4));

create table Student (
	studentId       char(8) primary key,
	firstName       varchar2(20) not null,
	lastName        varchar2(25) not null,
	email           varchar2(15) not null,
	phoneNo         char(8),
	cga             number(3,2),
	departmentId    char(4) not null,
	admissionYear   char(4) not null);

create table Course (
	courseId        char(8) primary key,
	departmentId    char(4) not null,
	courseName     varchar2(40) not null,
	instructor      varchar2(30) not null);

create table EnrollsIn (
 	studentId   char(8),
	courseId    char(8),
	grade       number(4,1),
	primary key(studentId,courseId));

create table Facility ( 
	departmentId        char(4) primary key,
	numberProjectors    int default 0,
	numberComputers     int default 0);

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

insert into EnrollsIn values ('13455789','COMP3311',85.6);
insert into EnrollsIn values ('15456789','COMP3311',77.9);
insert into EnrollsIn values ('13556789','COMP3311',89.5);
insert into EnrollsIn values ('14567890','COMP3311',60.4);
insert into EnrollsIn values ('13456789','COMP3311',66.9);
insert into EnrollsIn values ('15678989','COMP3311',71.8);
insert into EnrollsIn values ('15678901','COMP3311',64.3);
insert into EnrollsIn values ('26184624','COMP3311',62.1);
insert into EnrollsIn values ('26184444','COMP3311',52.1);
insert into EnrollsIn values ('26186666','COMP3311',82.1);
insert into EnrollsIn values ('66666666','COMP3311',50.0);
insert into EnrollsIn values ('15000655','COMP3311',92.1);
insert into EnrollsIn values ('15085942','COMP3311',90.0);
insert into EnrollsIn values ('28834512','COMP3311',62.1);
insert into EnrollsIn values ('28918856','COMP3311',95.1);
insert into EnrollsIn values ('29873381','COMP3311',78.0);
insert into EnrollsIn values ('18792018','COMP3311',79.2);

insert into EnrollsIn values ('13455789','COMP4021',61.3);
insert into EnrollsIn values ('15456789','COMP4021',65.9);
insert into EnrollsIn values ('13556789','COMP4021',83.1);
insert into EnrollsIn values ('14567890','COMP4021',56.8);
insert into EnrollsIn values ('13456789','COMP4021',71.3);
insert into EnrollsIn values ('15678989','COMP4021',55.2);
insert into EnrollsIn values ('15678901','COMP4021',82.1);
insert into EnrollsIn values ('16789012','COMP4021',75.3);
insert into EnrollsIn values ('26184624','COMP4021',77.1);
insert into EnrollsIn values ('26186666','COMP4021',92.1);
insert into EnrollsIn values ('66666666','COMP4021',55.4);
insert into EnrollsIn values ('15000655','COMP4021',88.1);
insert into EnrollsIn values ('15085942','COMP4021',89.4);
insert into EnrollsIn values ('28918856','COMP4021',85.3);
insert into EnrollsIn values ('13782973','COMP4021',77.2);
insert into EnrollsIn values ('18792018','COMP4021',92.5);

insert into EnrollsIn values ('13455789','ELEC3100',74.1);
insert into EnrollsIn values ('15456789','ELEC3100',60.0);
insert into EnrollsIn values ('13556789','ELEC3100',86.2);
insert into EnrollsIn values ('14567890','ELEC3100',59.1);
insert into EnrollsIn values ('13456789','ELEC3100',68.3);
insert into EnrollsIn values ('15678989','ELEC3100',74.6);
insert into EnrollsIn values ('15678901','ELEC3100',72.9);
insert into EnrollsIn values ('26184624','ELEC3100',83.7);
insert into EnrollsIn values ('26184444','ELEC3100',55.6);
insert into EnrollsIn values ('26186666','ELEC3100',95.1);
insert into EnrollsIn values ('66666666','ELEC3100',58.3);
insert into EnrollsIn values ('15085942','ELEC3100',85.3);
insert into EnrollsIn values ('28918856','ELEC3100',88.6);
insert into EnrollsIn values ('29873381','ELEC3100',94.0);
insert into EnrollsIn values ('13782973','ELEC3100',95.6);

insert into EnrollsIn values ('13455789','HUMA1020',82.4);
insert into EnrollsIn values ('15456789','HUMA1020',95.2);
insert into EnrollsIn values ('13556789','HUMA1020',88.4);
insert into EnrollsIn values ('14567890','HUMA1020',56.2);
insert into EnrollsIn values ('13456789','HUMA1020',91.6);
insert into EnrollsIn values ('15678989','HUMA1020',99.0);
insert into EnrollsIn values ('66666666','HUMA1020',50.2);
insert into EnrollsIn values ('15085942','HUMA1020',86.7);
insert into EnrollsIn values ('13782973','HUMA1020',72.1);
insert into EnrollsIn values ('18792018','HUMA1020',83.0);

insert into EnrollsIn values ('13455789','MATH2421',73.5);
insert into EnrollsIn values ('15456789','MATH2421',77.2);
insert into EnrollsIn values ('14567890','MATH2421',57.5);
insert into EnrollsIn values ('13556789','MATH2421',88.3);
insert into EnrollsIn values ('13456789','MATH2421',84.3);
insert into EnrollsIn values ('15678989','MATH2421',73.1);
insert into EnrollsIn values ('15678901','MATH2421',66.4);
insert into EnrollsIn values ('16789012','MATH2421',68.4);
insert into EnrollsIn values ('26184624','MATH2421',55.1);
insert into EnrollsIn values ('26184444','MATH2421',42.1);
insert into EnrollsIn values ('26186666','MATH2421',83.5);
insert into EnrollsIn values ('66666666','MATH2421',49.5);
insert into EnrollsIn values ('15000655','MATH2421',87.4);
insert into EnrollsIn values ('15085942','MATH2421',89.5);
insert into EnrollsIn values ('28834512','MATH2421',92.6);
insert into EnrollsIn values ('28918856','MATH2421',95.4);
insert into EnrollsIn values ('29873381','MATH2421',88.0);
insert into EnrollsIn values ('13782973','MATH2421',82.9);

insert into course values ('COMP3311','COMP','Database Management Systems','Chen Lei');
insert into course values ('COMP4021','COMP','Internet Computing','David Rossiter');
insert into course values ('ELEC3100','ELEC','Signal Processing and Communications','Electronic Man');
insert into course values ('MATH2421','MATH','Probability','Isaac Newton');
insert into course values ('HUMA1020','HUMA','Chinese Writing and Culture','Human Man');

insert into Department values ('COMP','Computer Science','3528');
insert into Department values ('MATH','Mathematics','3461');
insert into Department values ('ELEC','Electronic Engineering','2528');
insert into Department values ('BUS','Business','4528');
insert into Department values ('HUMA','Humanities','1200');

insert into Facility values ('COMP',15,250);
insert into Facility values ('MATH',5,50);
insert into Facility values ('ELEC',14,150);
insert into Facility values ('BUS',10,130);

set heading off
select '*** Inserting your student and enrollment records ***' from dual;
set heading on

/* Execute the referenced script files */
@InsertMyself
set feedback on

/* Write the data to disk */
commit;

