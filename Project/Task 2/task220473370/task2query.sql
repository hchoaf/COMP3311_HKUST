/* Name: CHO, Hangsun   |   Student ID: 20473370*/
/* task2query.sql */


/*
1. 
For FYPs that have 2 supervisors, 
find the title and category of the FYP as well as the names of the supervisors.
Order the result first by category ascending, then by title ascending, and finally by name ascending.
*/


select title, category, F.name as supervisorName
from Supervises S, FYP, Faculty F
where S.fypId in (select S1.fypId
                    from Supervises S1, Supervises S2
                    where S1.fypId = S2.fypId
                    and S1.username <> S2.username)
and S.fypId = FYP.fypId
and S.username = F.username
order by category asc, title asc, F.name asc;



/*
2. For each project group that has been assigned to an FYP,
find the group code, the title of the FYP, and the name of the reader for the project group, if any.
Order the result by group code ascending.
*/



with temp(groupCode, username, title) as
    (select groupCode, reader as username, title
    from ProjectGroup, FYP
    where ProjectGroup.assignedFYP = fypId)
select groupCode, title, name
from temp natural left outer join Faculty
order by groupCode asc;




/*
3. For each FYP project category,
find the number of project groups that have been assigned to FYPs in that category.
Order the result first by the number of FYPs in descending order and then by category in ascending order.
If no project group has been assigned to an FYP in a project category, then 0 should be returned as the number of groups assigned. (not null)
*/


with temp(category, groupId) as
    (select category, groupId
    from FYP, ProjectGroup
    where fypId = assignedFYP)
select category, count(groupId) as numberOfGroups
from FYPCategory natural left outer join temp
group by category
order by count(groupId) desc, category asc;


/*
4. Find the title, supervisor name, names of the students in the project group,
and the priority specified for the FYP for those project groups that have been assigned to an FYP
for which they have specified a priority of greater than 1.
Order the result first by priority ascendign and then by title ascending.
*/



select title, FA.name as supervisorName, C.name as studentName, priority
from ProjectGroup P, FYP FY, Faculty FA, Supervises S, CSEStudent C, InterestedIn I
where P.groupId in (select ProjectGroup.groupId
                    from ProjectGroup, InterestedIn
                    where ProjectGroup.groupId = InterestedIn.groupId
                        and ProjectGroup.assignedFYP = InterestedIn.fypId
                        and priority > 1)
    and P.groupId = I.groupId
    and P.assignedFYP = FY.fypId
    and P.assignedFYP = S.fypId
    and P.assignedFYP = I.fypId
    and S.username = FA.username
    and P.groupId = C.groupId
order by priority asc, title asc;    
    

/*
5. For each faculty, 
find their name, the number of FYPs for which they are a supervisor, and the number of categories in which they are supervising FYPs.
Order the result by faculty name ascending.
If a faculty is not supervising any FYPs, then the number of FYPs and categories should be shown as 0.
*/



with temp(username, fypId, category) as
    (select username, fypId, category
    from Supervises natural join FYP)
select name, count(fypId) as numberOfFYPs, count(distinct category) as numberOfCategories
from Faculty natural left outer join temp
group by username, name
order by name asc;
