select firstName, lastName
from Student
where regexp_like(firstName, '([a-zA-Z])\1' ,'i')or regexp_like(lastName,'([a-zA-Z])\1' , 'i')