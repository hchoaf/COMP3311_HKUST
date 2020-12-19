/* Name: CHO, Hangsun   |   Student ID: 20473370*/
/* task2create.sql */





/*Initial Settings*/

clear screen;
set feedback off;
set heading on;


/*Drop any existing tables*/

drop table RequirementGrades;
drop table CSEStudent;
drop table InterestedIn;
drop table ProjectGroup;
drop table Supervises;
drop table FYP;
drop table Faculty;
drop table FYPCategory;



/*Create Tables*/
clear screen;


create table FYPCategory (
    category varchar2(30) primary key,

    constraint FYPCategory_category_domain check(category in ('Artificial Intelligence', 'Computer Games', 'Computer Security', 
    'Database', 'Embedded Systems and Software', 'Human Language Technology', 'Miscellaneous', 'Mobile and Wireless Computing', 
    'Mobile Applications', 'Mobile Gaming', 'Networking', 'Operating Systems', 'Software Technology', 'Theory', 'Vision and Graphics'))
);




create table Faculty (
    username char(15) primary key,
    name varchar2(30) not null,
    roomNo char(5) not null,
    facultyCode char(2) not null unique,
    
    constraint Faculty_facultyCode_Domain check (regexp_like(rtrim(facultyCode), '^[A-Z]{2}$')),
    constraint Faculty_roomNo_Domain check (regexp_like(rtrim(roomNo), '^(\d{4}|\d{4}[A-Z])$')),
    constraint Faculty_userName_Domain check (regexp_like(rtrim(username), '^[a-z]{3,15}$'))
);


create table FYP (
    fypId smallint primary key,
    title varchar2(100) not null,
    description varchar2(1200) not null,
    category varchar2(30) not null,
    type char(7) not null,
    otherRequirements varchar2(200) default null,
    minStudents smallint not null,
    maxStudents smallint not null,
    isAvailable char(1) not null,

    constraint FK_FYP_FYPCategory foreign key (category) references FYPCategory (category) on delete cascade,

    constraint FYP_Type_Domain check ((type = 'project') or (type = 'thesis' and minStudents = 1 and maxStudents = 1)),
    -- Check if type is either project or thesis. Also check if type and number of students match. Thesis must have only one student
    

    constraint FYP_minStudents_Domain check (minStudents between 1 and maxStudents), -- check minStudents of FYP
    constraint FYP_maxStudents_Domain check (maxStudents between minStudents and 4), -- check maxStudents of FYP
    constraint FYP_isAvailable_Domain check (isAvailable in ('Y', 'N')) -- check availability of FYP
);
    



create table Supervises (
    username char(15),
    fypId smallint,

    constraint PK_Supervises primary key (username, fypId),
    constraint FK_Supervises_Faculty foreign key (username) references Faculty (username) on delete cascade,
    constraint FK_Supervises_FYP foreign key (fypId) references FYP (fypId) on delete cascade

);

-- My Submission
create table ProjectGroup (
    groupId smallint primary key,
    groupCode char(5) default null unique,
    assignedFYP smallint default null,
    reader char(15) default null,

    constraint FK_ProjectGroup_FYP foreign key (assignedFYP) references FYP (fypId) on delete set null,
    constraint FK_ProjectGroup_Faculty foreign key (reader) references Faculty (username) on delete set null,

    constraint ProjectGroup_groupCode_Domain check (regexp_like(rtrim(groupCode), '^[A-Z]{2,4}[1-4]$'))
);
    
    
create table InterestedIn (
    fypId smallint,
    groupId smallint,
    priority smallint not null,

    constraint PK_InterestedIn primary key (fypId, groupId),
    constraint FK_InterestedIn_FYP foreign key (fypId) references FYP (fypId) on delete cascade,
    constraint FK_InterestedIn_ProjectGroup foreign key (groupId) references ProjectGroup (groupId) on delete cascade,

    constraint InterestedIn_priority_domain check (priority between 1 and 5)
);
    
create table CSEStudent (
    username char(15) primary key,
    name varchar2(30) not null,
    groupId smallint default null,
    
    constraint FK_CSEStudent_ProjectGroup foreign key (groupId) references ProjectGroup(groupId) on delete set null,
    
    constraint CSEStudent_userName_Domain check (regexp_like(rtrim(userName), '^[a-z]{3,15}$'))
);



create table RequirementGrades (
    facultyUsername char(15),
    studentUsername char(15),
    
    
    -- 4 graded requirements are not null because a student that doesn't have a project group and doesn't do FYP won't have a record.
    proposalReport number(4,1) not null,    
    progressReport number(4,1) not null,
    finalReport number(4,1) not null,
    presentation number(4,1) not null,
    
    constraint PK_RequirementGrades primary key (facultyUsername, studentUsername),
    constraint FK_RequirementGrades_Faculty foreign key (facultyUsername) references Faculty (username) on delete cascade,
    constraint FK_RequirementGrades_CSEStudent foreign key (studentUsername) references CSEStudent (username) on delete cascade,
    
    constraint RequirementGrades_proposalReport_Domain check (proposalReport between 0 and 100),
    constraint RequirementGrades_progressReport_Domain check (progressReport between 0 and 100),
    constraint RequirementGrades_finalReport_Domain check (finalReport between 0 and 100),
    constraint RequirementGrades_presentation_Domain check (presentation between 0 and 100)
    
);
    

commit;
    

