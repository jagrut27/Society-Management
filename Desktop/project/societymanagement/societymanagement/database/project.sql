CREATE TABLE Members (
    MemberId INT IDENTITY(1,1) PRIMARY KEY,    -- Auto-incremented primary key
    Firstname NVARCHAR(100) NOT NULL,           -- Member's first name
    Lastname NVARCHAR(100) NOT NULL,            -- Member's last name
    Email NVARCHAR(255) NOT NULL,               -- Member's email (you can adjust length)
    Password NVARCHAR(255) NOT NULL,            -- Member's password (make sure to hash passwords)
    PhoneNumber NVARCHAR(15) NOT NULL,          -- Member's phone number (adjust length as needed)
    Gender NVARCHAR(10) NOT NULL,               -- Member's gender
    FlatNumber NVARCHAR(50) NOT NULL,           -- Member's flat number
    BlockNumber NVARCHAR(50) NOT NULL           -- Member's block number
);

ALTER TABLE Members ADD ImageURL VARCHAR(255) ;

--all data fetch stored procured
CREATE PROCEDURE sp_GetMembers1
AS
BEGIN
    SELECT * FROM Members;		
END;

exec sp_GetMembers1;	

--count email strored procured with using multiple email not stored in database
 create procedure sp_countmemnber1
	    @email NVARCHAR(255)             
 as
 begin
	select count(*) from Members where Email= @email;
 end;

 exec sp_countmemnber1 @email="string";

 --this is count flat number with using multiple flat number cannot stored in database

 alter procedure sp_countflat
			@flatNumber NVARCHAR(10),
			@blocknumber NVARCHAR(10)
	as
	begin
		select count(*) countflatandblock from Members where FlatNumber=@flatNumber AND BlockNumber=@blocknumber;
	end;

	exec sp_countflat @flatNumber='34' , @blocknumber='B';

--delete a member data

create procedure sp_deletemember
	@Memberid int
as
begin
	delete from Members where @Memberid=MemberId;
end;

exec sp_deletemember @Memberid=2008;
--this is insert stored procured

alter procedure sp_PostMember	
	@firstname NVARCHAR(100),        
    @lastname NVARCHAR(100),            

    @email NVARCHAR(255),               
    @password NVARCHAR(255),       
    @phoneNumber NVARCHAR(15),    
     
    @flatNumber NVARCHAR(50),         
    @blockNumber NVARCHAR(50) 
as
begin

	insert into Members(Firstname,Lastname,Email,Password,PhoneNumber,FlatNumber,BlockNumber)
	values(@firstname,@lastname,@email,@password,@phoneNumber,@flatNumber,@blockNumber);

	
end;

EXEC sp_PostMember 'John', 'Doe', 'john@example.com', 'hashed_password', '9876543210', 'Male', 'A-101', 'Block A';

--this work with login page
alter procedure sp_checkemail
	@email NVARCHAR(255)
as
begin
		select MemberId,Password from Members where Email=@email;
end;

exec sp_checkemail @email="smit15@gmail.com";

--this is f
alter procedure sp_getuserbyemail
		@Email NVARCHAR(255)
as 
begin
		select MemberId,Firstname,Lastname,PhoneNumber,Gender,FlatNumber,BlockNumber from Members where Email=@Email;
end;


exec sp_getuserbyemail @Email="smit15@gmail.com";



INSERT INTO Members (Firstname, Lastname, Email, Password, PhoneNumber, Gender, FlatNumber, BlockNumber)
VALUES
('John', 'Doe', 'john.doe@example.com', 'hashedpassword123', '123-456-7890', 'Male', '101', 'Block A'),
('Jane', 'Smith', 'jane.smith@example.com', 'hashedpassword456', '987-654-3210', 'Female', '102', 'Block B'),
('Mike', 'Johnson', 'mike.johnson@example.com', 'hashedpassword789', '555-123-4567', 'Male', '103', 'Block C'),
('Emily', 'Davis', 'emily.davis@example.com', 'hashedpassword012', '444-234-5678', 'Female', '104', 'Block D'),
('Chris', 'Brown', 'chris.brown@example.com', 'hashedpassword345', '333-345-6789', 'Male', '105', 'Block E');



delete from Members where MemberId=2007;

--exec sp_GetMembers1;


--this image upload api


	alter procedure sp_imageupload
		@MemberId int,
		@imageurl varchar(255)
	as
	begin
		update Members set ImageURL=@imageurl where MemberId=@MemberId;	
	end;

exec sp_imageupload @MemberId=2008,@imageurl='C:\Users\pcit99.PRUDENT\Desktop\pratham\.net\ado.net\societymanagement\societymanagement\firstimage.jpg';

--select * from Members as m join imageupload as i on m.memberid = i.memberid;	


	create procedure sp_imageget
	as
	begin
		select MemberId,ImageURL from Members;
	end;

	exec sp_imageget;	


