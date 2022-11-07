create table authors(
authorID serial,
name varchar(500) UNIQUE,
DOB varchar(500)	
);

insert into authors(name, DOB) values ('J.K.Rowling', '1972-04-01');
insert into authors(name, DOB) values ('Zimmerman' , '1989-06-05');
insert into authors(name, DOB) values ('Douglas Adams' , '1999-10-05');
insert into authors(name, DOB) values ('Mary Grandpra' , '1987-06-07');


select * from authors


create table books(
BookID serial,
title varchar(500) UNIQUE,
DOP varchar(500)	
);

insert into books(title, DOP) values ('Harry Potter 6', '2001-12-10');
insert into books(title, DOP) values ('Harry Potter 5', '2007-10-20');
insert into books(title, DOP) values ('Hitchhikers Guide 1', '2012-06-07');
insert into books(title, DOP) values ('Hitchhikers Guide 5', '2005-11-23');
insert into books(title, DOP) values ('History of Nearly Everything', '2020-12-10');

select * from books

create table relation(	
author varchar(500) REFERENCES authors(name) ON DELETE CASCADE,
book   varchar(500) REFERENCES books(title) ON DELETE CASCADE,
PRIMARY KEY (author,book)
);
	
insert into relation(author, book) values ('J.K.Rowling', 'Harry Potter 6');
insert into relation(author, book) values ('J.K.Rowling', 'Harry Potter 5');
insert into relation(author, book) values ('Zimmerman', 'Hitchhikers Guide 1');
insert into relation(author, book) values ('Zimmerman', 'Hitchhikers Guide 5');
insert into relation(author, book) values ('Douglas Adams', 'History of Nearly Everything');
insert into relation(author, book) values ('Mary Grandpra', 'Harry Potter 6');

select * from relation
