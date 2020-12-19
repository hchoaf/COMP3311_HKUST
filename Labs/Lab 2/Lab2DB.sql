/* COMP3311 Lab 2 Exercise: Lab2DB.sql */

/* Start with a clean database */
drop table Student;

/* Create the Student table */
create table Student (
	studentId       char(8) not null,
	firstName       varchar2(20) not null,
	lastName        varchar2(25) not null,
	email           varchar2(15) not null,
	phoneNo         char(8),
	cga             number(4,2),
    departmentId	char(4) not null,
	admissionYear   char(4) not null);

/* Populate the Student table with data */
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
insert into Student values ('28834512','Issac','Newton','manewton','22861987',2.98,'MATH','2017');
insert into Student values ('28918856','Alan','Turing','maturing','26679834',3.56,'MATH','2017');
insert into Student values ('29873381','Nikola','Tesla','eetesla','25671983',3.37,'ELEC','2017');
insert into Student values ('13782973','Edith','Clarke','eeclarke','28340180',3.15,'ELEC','2017');
insert into Student values ('18792018','Elon','Musk','bsmusk','28659910',3.25,'BUS','2018');

/* Write the data to disk */
commit;