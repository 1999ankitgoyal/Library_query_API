# *Assignment 2*

**Specification**
Create a data model of the objects below and develop CRUD REST APIs to manage it. The API should be developed and published as in the first assignment.
You want to manage a set of Books and Authors. Each Book can have multiple Authors, and each Author can write multiple Books. Add some details for each Book (e.g.: title, date of first publication, original language, etc.) and for each Author (name, surname, dates of birth/death, country of birth).

**Steps**

1. Design the data schema for a relational database (use PostgreSQL), and use SQL statements to create the needed tables
2. Use DotNet tools to create the matching classes in C# and associate them with the database tables (probably DotNet has some ORM – Object Relational Mapping) to aid this
part, that is to persist in-memory objects to a database, and retrieve them from the database to memory
3. Design and implement the API endpoints to manage the data model (support CRUD operation through standart HTTP REST semantics)
4. Develop, Dockerize and Deploy as in the first Assignment.



**Shortcomings:**
1. The name of the author and book title should be unique.
2. The DOB and DOP are taken as strings rather than in date-time format.
3. Name of author and book title cannot be updated. Only DOB and DOP can be updated.
