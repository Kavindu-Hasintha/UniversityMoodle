select * from Roles;

insert into Roles(Name)
values ('Admin');

insert into Roles(Name)
values ('Teacher'), ('Student');

select * from Users;
select * from Courses;

insert into Courses(Name, Description) values
('Artificial Intelligence', 'Foundation of AI'),
('Enterprise Application Development', 'How to develop an Enterprise Application'),
('Advanced Database Management Systems', 'Non-Relational Databases'),
('Communication Skills', 'How to communicate in professional manner'),
('Network Programming', 'Building a Network application by using Java');

insert into UserCourses values
(3, 1), (4, 2), (5, 3), (6, 4), (7, 5);

insert into UserCourses values
(8, 1), (8, 2), (9, 3), (9, 4), (10, 5), (10, 3), (11, 1), (11, 3), (12, 5), (12, 2);

insert into TeacherStudents values 
(3, 8), (4, 8), (5, 9), (6, 9), (7, 10), (5, 10), (3, 11), (5, 11), (7, 12), (4, 12);