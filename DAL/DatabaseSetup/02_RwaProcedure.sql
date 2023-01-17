IF TYPE_ID(N'KeyList') IS NULL
	CREATE TYPE dbo.KeyList AS TABLE (
		[Key] int NOT NULL
		PRIMARY KEY ([Key])
	)
GO

IF TYPE_ID(N'PictureList') IS NULL
	CREATE TYPE dbo.PictureList AS TABLE (
		Id int NULL, 
		[Path] nvarchar(250) NULL, 
		[Name] nvarchar(250) NULL, 
		IsRepresentative bit NOT NULL, 
		DoDelete bit NOT NULL
	)
GO

CREATE OR ALTER PROCEDURE dbo.GetApartment
	@id int = null
AS

	SELECT 
		  a.Id,
		  a.[Guid],
		  a.CreatedAt,
		  DeletedAt,
		  OwnerId,
		  OwnerName = ao.[Name],
		  TypeId,
		  StatusId,
		  StatusName = ast.[Name],
		  CityId,
		  CityName = c.[Name],
		  [Address],
		  a.[Name],
		  a.NameEng,
		  Price,
		  MaxAdults,
		  MaxChildren,
		  TotalRooms,
		  BeachDistance
	FROM 
		dbo.Apartment a
		JOIN dbo.ApartmentOwner ao ON ao.Id = a.OwnerId
		JOIN dbo.ApartmentStatus ast ON ast.Id = a.StatusId
		JOIN dbo.City c ON c.Id = a.CityId
	WHERE a.Id = @id
GO

CREATE OR ALTER PROCEDURE dbo.GetApartmentTags
	@apartmentId int
AS
	SELECT 
		t.Id,
		t.[Name]
	FROM 
		dbo.TaggedApartment ta
		JOIN dbo.Tag t ON t.Id = ta.TagId
	WHERE ta.ApartmentId = @apartmentId
	ORDER BY t.[Name]
GO

CREATE OR ALTER PROCEDURE dbo.GetApartmentPictures
	@apartmentId int
AS
	SELECT 
		  Id,
		  [Guid],
		  [Name],
		  [Path],
		  IsRepresentative
	FROM dbo.ApartmentPicture
	WHERE ApartmentId = @apartmentId
	ORDER BY IsRepresentative DESC, [Name] ASC
GO

CREATE OR ALTER PROCEDURE dbo.CreateApartment
	@guid uniqueidentifier,
	@ownerId int,
	@typeId int,
	@statusId int,
	@cityId int,
	@address nvarchar(250),
	@name nvarchar(250),
	@price money,
	@maxAdults int,
	@maxChildren int,
	@totalRooms int,
	@beachDistance int,
	@tags KeyList READONLY,
	@pictures PictureList READONLY
AS

	
	DECLARE @Output KeyList
	DECLARE @ApartmentId int

	INSERT INTO dbo.Apartment(
		[Guid],
		CreatedAt,
		OwnerId,
		TypeId,
		StatusId,
		CityId,
		[Address],
		[Name],
		NameEng,
		Price,
		MaxAdults,
		MaxChildren,
		TotalRooms,
		BeachDistance)
	OUTPUT INSERTED.ID INTO @Output([Key])
    SELECT
		@guid,
		SYSUTCDATETIME(),
		@ownerId,
		@typeId,
		@statusId,
		@cityId,
		@address,
		@name,
		'',
		@price,
		@maxAdults,
		@maxChildren,
		@totalRooms,
		@beachDistance

	SELECT @ApartmentId = [Key]
	FROM @Output

	INSERT INTO dbo.TaggedApartment(
		ApartmentId,
		TagId)
	SELECT
		@ApartmentId,
		[Key]
	FROM @tags

	INSERT INTO dbo.ApartmentPicture(
		ApartmentId,
		[Path],
		[Name],
		IsRepresentative)
	SELECT
		@ApartmentId,
		[Path],
		[Name],
		IsRepresentative
	FROM @pictures

GO

CREATE OR ALTER PROCEDURE dbo.UpdateApartment
	@id int,
	@guid uniqueidentifier,
	@ownerId int,
	@typeId int,
	@statusId int,
	@cityId int,
	@address nvarchar(250),
	@name nvarchar(250),
	@price money,
	@maxAdults int,
	@maxChildren int,
	@totalRooms int,
	@beachDistance int,
	@tags KeyList READONLY,
	@pictures PictureList READONLY
AS

	
	DECLARE @Output KeyList

	UPDATE dbo.Apartment
	SET 
		[Guid] = @guid,
		OwnerId = @ownerId,
		TypeId = @typeId,
		StatusId = @statusId,
		CityId = @cityId,
		[Address] = @address,
		[Name] = @name,
		Price = @price,
		MaxAdults = @maxAdults,
		MaxChildren = @maxChildren,
		TotalRooms = @totalRooms,
		BeachDistance = @beachDistance
	WHERE id = @id

	MERGE dbo.TaggedApartment AS tgt
	USING @tags AS src
	ON (tgt.Id = src.[Key]) 
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (ApartmentId, TagId)
		VALUES (@id, [Key])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

	MERGE dbo.ApartmentPicture AS tgt
	USING @pictures AS src
	ON (tgt.Id = src.Id) 
	WHEN MATCHED THEN
		UPDATE SET 
			[Name] = src.[Name],
			IsRepresentative = src.IsRepresentative
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (ApartmentId, [Path], [Name], IsRepresentative)
		VALUES (@id, [Path], [Name], IsRepresentative)
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

GO

CREATE OR ALTER PROCEDURE dbo.DeleteApartment
	@id int
AS
	UPDATE dbo.Apartment
	SET DeletedAt = SYSUTCDATETIME()
	WHERE Id = @id
GO

CREATE OR ALTER PROCEDURE dbo.GetApartments
	@statusId int = null,
	@cityId int = null,
	@order int = null
AS
	SELECT 
		  a.Id,
		  a.[Guid],
		  a.CreatedAt,
		  DeletedAt,
		  OwnerId,
		  OwnerName = ao.[Name],
		  TypeId,
		  StatusId,
		  StatusName = ast.[Name],
		  CityId,
		  CityName = c.[Name],
		  [Address],
		  a.[Name],
		  a.NameEng,
		  Price,
		  MaxAdults,
		  MaxChildren,
		  TotalRooms,
		  BeachDistance
	FROM 
		dbo.Apartment a
		JOIN dbo.ApartmentOwner ao ON ao.Id = a.OwnerId
		JOIN dbo.ApartmentStatus ast ON ast.Id = a.StatusId
		JOIN dbo.City c ON c.Id = a.CityId
	WHERE
		(@statusId IS NULL OR @statusId IS NOT NULL AND StatusId = @statusId)
		AND
		(@cityId IS NULL OR @cityId IS NOT NULL AND CityId = @cityId)
		AND
		DeletedAt IS NULL
	ORDER BY 
        CASE
            WHEN @order is null THEN a.Id
            WHEN @order = 1 THEN TotalRooms
            WHEN @order = 2 THEN MaxAdults
            WHEN @order = 3 THEN MaxChildren
            WHEN @order = 4 THEN Price
        END
GO

CREATE OR ALTER PROCEDURE dbo.GetCities
AS
	SELECT 
		  Id,
		  [Guid],
		  [Name]
	FROM dbo.City
	ORDER BY [Name]
GO

CREATE OR ALTER PROCEDURE dbo.GetTags
AS
	SELECT 
		  Id,
		  [Guid],
		  [Name]
	FROM dbo.Tag
	ORDER BY [Name]
GO

CREATE OR ALTER PROCEDURE dbo.GetApartmentOwners
AS
	SELECT 
		  Id,
		  [Guid],
		  [Name]
	FROM dbo.ApartmentOwner
	ORDER BY [Name]
GO

create or alter proc [dbo].[AddApartment]
	@name nvarchar(100),
	@nameEng nvarchar(100),
	@cityName nvarchar(100),
	@OwnerName nvarchar(100),
	@StatusName nvarchar(100),
	@adults int,
	@children int,
	@price money,
	@beachDistance int,
	@totalRooms int,
	@address nvarchar(200)
as
begin
	declare @guid uniqueidentifier
	set @guid = NEWID()
	declare @date datetime2
	set @date = GETDATE()
	insert into Apartment values (@guid, @date, null, 
								(select o.Id from ApartmentOwner o where o.Name=@OwnerName),
								99, 
								(select s.Id from ApartmentStatus s where s.Name=@StatusName),
								(select c.Id from City c where c.Name=@cityName),
								@address, @name, @nameEng, @price, @adults, @children, @totalRooms, @beachDistance
								)
end
go

create or alter proc [dbo].[AddReservationForExistingUser]
	@userId int,
	@apId int,
	@details nvarchar(max)
as
begin
	declare @guid uniqueidentifier
	set @guid = NEWID()
	declare @date datetime2
	set @date = GETDATE()
	declare @userName nvarchar(100)
	declare @email nvarchar(100)
	declare @phone nvarchar(100)
	declare @address nvarchar(100)
	select @userName=UserName, @email=Email, @phone=PhoneNumber, @address=Address from AspNetUsers where Id=@userId
	insert into ApartmentReservation values (@guid, @date, @apId, @details, @userId, @userName, @email, @phone, null)
end
go

create or alter proc [dbo].[AddReservationForNonExistingUser]
	@apId int, 
	@details nvarchar(max),
	@userName nvarchar(100),
	@email nvarchar(100),
	@phone nvarchar(100)
as
begin
	declare @guid uniqueidentifier
	set @guid = NEWID()
	declare @date datetime2
	set @date = GETDATE()
	insert into ApartmentReservation values (@guid, @date, @apId, @details, null, @userName, @email, @phone, null)
end
go

create or alter proc [dbo].[AddTag]
	@name nvarchar(100),
	@nameEng nvarchar(100),
	@type nvarchar(100)
as
begin
	declare @guid uniqueidentifier
	set @guid = NEWID()
	declare @date datetime2
	set @date = GETDATE()
	insert into Tag values (@guid, @date, (select id from TagType where Name=@type), @name, @nameEng)
end
go

create or alter proc [dbo].[AddTaggedApartment]
	@apName nvarchar(150),
	@tName nvarchar(150)
as
begin
declare @guid uniqueidentifier
		set @guid = NEWID()
	insert into TaggedApartment values (@guid, (select id from Apartment where Name=@apName), (select id from Tag where Name=@tName))
end
go

create or alter proc [dbo].[AddUser]
	@email nvarchar(100),
	@userName nvarchar(100),
	@passHash nvarchar(max),
	@phoneNumber nvarchar(50),
	@Address nvarchar(100)
as
	begin
		declare @guid uniqueidentifier
		set @guid = NEWID()
		declare @date datetime2
		set @date = GETDATE()
		insert into AspNetUsers values (@guid, @date, null, @email, 'true', @passHash, null, @phoneNumber, 'true', null, 'true', 0, @userName, @Address)
end
go

create or alter proc [dbo].[AuthUser]
	@email nvarchar(100),
	@passHash nvarchar(max)
as
	begin
		select * from AspNetUsers where Email=@email and PasswordHash=@passHash
	end
go

create or alter proc [dbo].[AuthUserWithoutHash]
	@email nvarchar(100),
	@pass nvarchar(max)
as
begin
	select * from AspNetUsers where PasswordHash=@pass and Email=@email
end
go

create or alter proc [dbo].[DeleteApartment]
	@id int
as
begin
	delete from Apartment where Id=@id
end
go

create or alter proc [dbo].[DeleteTag]
	@id int
as
begin
	delete from Tag where Id=@id
end
go

create or alter proc [dbo].[DeleteTaggedApartment]
	@apName nvarchar(150),
	@tName nvarchar(150)
as
begin
	delete from TaggedApartment where TagId=(select id from Tag where Name=@tName) and ApartmentId=(select id from Apartment where Name=@apName)
end
go

create or alter proc [dbo].[DeleteUser]
	@id int
as
begin
	delete from AspNetUsers where Id=@id
end
go

create or alter proc [dbo].[GetApartmentById]
	@id int
as
begin
	select a.*, c.Name CityName, o.Name OwnerName, s.Name StatusName from Apartment a
	inner join ApartmentOwner o on o.Id=a.OwnerId
	inner join ApartmentStatus s on s.Id=a.StatusId
	inner join City c on c.Id=a.CityId
	where a.Id=@id
end
go

create or alter proc [dbo].[GetCities]
as
begin
	select * from City
end
go

create or alter proc [dbo].[GetOwners]
as
begin
	select * from ApartmentOwner
end
go

create or alter proc [dbo].[GetStatus]
as
begin
	select * from ApartmentStatus
end
go

create or alter proc [dbo].[LoadApartments]
as
	begin
		select a.*, c.Name CityName, o.Name OwnerName, s.Name StatusName from Apartment a
		inner join ApartmentOwner o on o.Id=a.OwnerId
		inner join ApartmentStatus s on s.Id=a.StatusId
		inner join City c on c.Id=a.CityId
	end
go

create or alter proc [dbo].[LoadApartmentsByCityAndStatus]
	@status nvarchar(100),
	@city nvarchar(100)
as
	begin
		if @status = 'Odaberi' and @city='Odaberi'
			begin
				select a.*, c.Name CityName, o.Name OwnerName, s.Name StatusName from Apartment a
				inner join ApartmentOwner o on o.Id=a.OwnerId
				inner join ApartmentStatus s on s.Id=a.StatusId
				inner join City c on c.Id=a.CityId
			end
		else if @status = 'Odaberi'
			begin
				select a.*, c.Name CityName, o.Name OwnerName, s.Name StatusName from Apartment a
				inner join ApartmentOwner o on o.Id=a.OwnerId
				inner join ApartmentStatus s on s.Id=a.StatusId
				inner join City c on c.Id=a.CityId
				where c.Name = @city
			end
		else if @city = 'Odaberi'
			begin
				select a.*, c.Name CityName, o.Name OwnerName, s.Name StatusName from Apartment a
				inner join ApartmentOwner o on o.Id=a.OwnerId
				inner join ApartmentStatus s on s.Id=a.StatusId
				inner join City c on c.Id=a.CityId
				where s.Name = @status
			end
		else
			begin
				select a.*, c.Name CityName, o.Name OwnerName, s.Name StatusName from Apartment a
				inner join ApartmentOwner o on o.Id=a.OwnerId
				inner join ApartmentStatus s on s.Id=a.StatusId
				inner join City c on c.Id=a.CityId
				where s.Name=@status and c.Name=@city
			end
	end
go

create or alter proc [dbo].[LoadApartmentsByTagID]
	@id int
as
begin
	select a.*, c.Name CityName, o.Name OwnerName, s.Name StatusName from Apartment a
	inner join ApartmentOwner o on o.Id=a.OwnerId
	inner join ApartmentStatus s on s.Id=a.StatusId
	inner join City c on c.Id=a.CityId
	inner join TaggedApartment ta on ta.ApartmentId=a.Id
	inner join Tag t on t.Id=ta.TagId
	where t.Id=@id
end
go

create or alter proc [dbo].[LoadTags]
as
begin
	select t.*, tt.Name Type from Tag t
	inner join TagType tt on tt.Id=t.TypeId
end
go

create or alter proc [dbo].[LoadTagsForApartment]
	@id int
as
begin
	select t.*, tt.Name Type from Tag t
	inner join TagType tt on tt.Id=t.TypeId
	inner join TaggedApartment ta on ta.TagId=t.Id
	inner join Apartment a on ta.ApartmentId=a.Id
	where a.Id=@id
end
go

create or alter proc [dbo].[LoadTagTypes]
as
begin
	select * from TagType
end
go

create or alter proc [dbo].[LoadUsers]
as
begin
	select * from AspNetUsers
end
go

create or alter proc [dbo].[SaveUser]
	@id int,
	@email nvarchar(100),
	@userName nvarchar(100),
	@passHash nvarchar(max),
	@phoneNumber nvarchar(50),
	@Address nvarchar(100)
as
	begin
		declare @guid uniqueidentifier
		set @guid = NEWID()
		declare @date datetime2
		set @date = GETDATE()
		update AspNetUsers set Email=@email, UserName=@userName, PasswordHash=@passHash, PhoneNumber=@phoneNumber, Address=@Address where Id=@id
	end
go

CREATE OR ALTER PROCEDURE [dbo].[SoftDeleteApartment]
	@id int
AS
	UPDATE dbo.Apartment
	SET DeletedAt = SYSUTCDATETIME()
	WHERE Id = @id
GO

CREATE OR ALTER PROCEDURE [dbo].[QueryApartmentDeletedStatus]
	@Id int,
	@DeletedStatus int output
AS
	if (select Apartment.DeletedAt from Apartment where Apartment.Id=@Id) is null
	begin
		set @DeletedStatus = 0
	end
	else
	begin
		set @DeletedStatus = 1
	end
GO

create or alter proc [dbo].[AddTaggedApartmentById]
	@ApartmentId int,
	@TagName nvarchar(150)
as
begin
	declare @guid uniqueidentifier
	set @guid = NEWID()
	insert into TaggedApartment values (@guid, @ApartmentId, (select Tag.Id from Tag where Name=@TagName))
end
go

create or alter proc [dbo].[RemoveAllTaggedApartmentsById]
	@ApartmentId nvarchar(150)
as
begin
	delete from TaggedApartment
	where TaggedApartment.ApartmentId=@ApartmentId
end
go

create or alter proc [dbo].[SaveApartment]
	@id int,
	@name nvarchar(100),
	@nameEng nvarchar(100),
	@cityName nvarchar(100),
	@OwnerName nvarchar(100),
	@StatusName nvarchar(100),
	@adults int,
	@children int,
	@price money,
	@beachDistance int,
	@totalRooms int,
	@address nvarchar(200)
as
begin
	update Apartment set
		[Name]=@name, NameEng=NameEng, 
		CityId=(select id from City where Name=@cityName),
		OwnerId=(select id from ApartmentOwner where Name=@OwnerName),
		StatusId=(select id from ApartmentStatus where Name=@StatusName),
		MaxAdults=@adults,
		MaxChildren=@children,
		Price=@price,
		BeachDistance=@beachDistance,
		TotalRooms=@totalRooms,
		[Address]=@address
	where Id=@id
end
go

create or alter proc [dbo].[QueryReservedApartmentsForUser]
	@UserId int
as
begin
	select distinct a.Id, a.[Name] from Apartment as a
	inner join ApartmentReservation as ar on a.Id=ar.ApartmentId
	inner join AspNetUsers as u on ar.UserId=u.Id
	where u.Id=@UserId
end
go

create or alter proc [dbo].[CreateApartmentReviewForUser]
	@UserId int,
	@ApartmentId int,
	@Details nvarchar(1000),
	@Stars int
as
begin
	declare @guid uniqueidentifier
	set @guid = NEWID()
	declare @date datetime2
	set @date = GETDATE()
	insert into ApartmentReview([Guid], CreatedAt, ApartmentId, UserId, Details, Stars)
	values (@guid, @date, @ApartmentId, @UserId, @Details, @Stars)
end
go

create or alter proc [dbo].[QueryReviewsForApartment]
	@ApartmentId int
as
begin
	select ar.Id, ar.ApartmentId, ar.UserId, ar.CreatedAt, ar.Details, ar.Stars, u.UserName from ApartmentReview as ar
	inner join [AspNetUsers] as u on ar.UserId=u.Id
	where ar.ApartmentId = @ApartmentId
end
go