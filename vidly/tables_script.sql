create table MembershipType 
(
	Id int identity not null primary key,
	SignupFee int not null,
	Name nvarchar(50) not null unique,
	DurationInMonth int not null,
	DiscountRate int not null
)
create table MovieGenres
(
	Id int identity not null primary key,
	Name nvarchar(50) not null unique
)
create table Customers
(
	Id int identity not null primary key,
	Name nvarchar(255) not null,
	IsSubscribed bit not null,
	DateOfBirth datetime null,
	MembershipTypeId int not null,
		foreign key (MembershipTypeId) references MembershipType(Id)
)
create table Movies
(
	Id int identity not null primary key,
	Name nvarchar(50) not null,
	ReleaseDate datetime not null,
	AddedDate datetime not null,
	NumberInStock int not null,
	GenreId int not null,
		foreign key (GenreId) references MovieGenres(Id)
)

insert into MembershipType(Name,SignupFee,DurationInMonth,DiscountRate) values
	('Pay as you go',0,0,0),
	('Monthly',30,1,5),
	('Quaterly',60,3,10),
	('yearly',90,12,15);

insert into MovieGenres(Name) values ('Kids'),('18+'),('Action'),('Crimee');

-- <add name="DefaultConnection" connectionString="Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\aspnet-vidly-20200807110541.mdf;Initial Catalog=aspnet-vidly-20200807110541;Integrated Security=True" providerName="System.Data.SqlClient" />


