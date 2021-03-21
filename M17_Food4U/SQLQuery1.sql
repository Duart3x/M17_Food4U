/*

	0- Admins, 
	1- Donos, 
	2- Estafetas, 
	3- Users

	Estafetas -> Carta Condução; Maiores 18;
*/


CREATE TABLE users(
	id int identity primary key,
	email varchar(100) not null unique check (email like '%@%.%'),
	[name] varchar(100) not null,
	nif varchar(9) not null,
	[password] varchar(64) not null,
	estado int not null,
	perfil int not null check (perfil in ('0','1','2','3')),
	data_nasc Date not null,
	saldo DECIMAL(19,4) not null default 0,
	lnkRecuperar varchar(36),
	data_lnk date,
	drivingLicense varchar(10),
	drivingLicenseValidity Date,
	createDate Date
);

CREATE TABLE transacoes(
	id int identity primary key,
	[user] int not null references users(id),
	[source] varchar(100) not null,
	saldo DECIMAL(19,4) not null,
	valor DECIMAL(19,4) not null,
	createDate DateTime not null DEFAULT getdate()
);

CREATE TABLE pagamentos(
	id int identity primary key,
	[user] int not null references users(id),
	[restaurant] int references restaurants(id),
	[courier] int references users(id),
	saldo DECIMAL(19,4) not null,
	valor DECIMAL(19,4) not null, 
	createDate DateTime not null DEFAULT getdate()
);

CREATE TABLE addresses(
	id int identity primary key,
	[user] int references users(id),
	[city] varchar(100) not null,
	[cp] varchar(8) not null check(cp like '____-___'),
	[address] varchar(100) not null
);

CREATE TABLE restaurants(
	id int identity primary key,
	[owner] int references users(id),
	[name] varchar(100) not null,
	[city] varchar(100) not null,
	[cp] varchar(8) not null check(cp like '____-___'),
	[address] varchar(100) not null,
	saldo DECIMAL(19,4) not null default 0,
	[enabled] bit DEFAULT 1
);

CREATE TABLE menus(
	id int identity primary key,
	[restaurant] int references restaurants(id) ON DELETE CASCADE ON UPDATE CASCADE,
	[title] varchar(100) not null,
	[description] varchar(255) not null DEFAULT '',
	[price] DECIMAL(19,4) not null,
	[stars] int not null default 0 CHECK([stars] >= 0 or [stars] <= 5),
	[stock] bit DEFAULT 1,
	[enabled] bit DEFAULT 1
);

CREATE TABLE menu_comments(
	id int identity primary key,
	[user] int references users(id) ON DELETE CASCADE ON UPDATE CASCADE,
	[menu] int references menus(id) ON DELETE CASCADE ON UPDATE CASCADE,
	[stars] int not null default 0 CHECK([stars] >= 0 or [stars] <= 5),
	[comment] varchar(255) not null,
	[CreateDate] datetime not null DEFAULT getDate()
);

CREATE TABLE restaurant_comments(
	id int identity primary key,
	[user] int references users(id) ON DELETE CASCADE ON UPDATE CASCADE,
	[restaurant] int references restaurants(id) ON DELETE CASCADE ON UPDATE CASCADE,
	[stars] int not null default 0 CHECK([stars] >= 0 or [stars] <= 5),
	[comment] varchar(255) not null,
	[CreateDate] datetime not null DEFAULT getDate()
);

CREATE TABLE orders(
	id int identity primary key,
	[client] int references users(id),
	[courier] int references users(id),
	[address] int references addresses(id),
	[state] int DEFAULT 1 check ([state] in ('1','2','3','4','5')),
	deliveryDate Datetime default NULL,
	createDate Datetime default getdate()
);

/*
Orders States
- Em processamento 
- A ser preparada
- A caminho 
- Entregue
- Cancelada
*/


/*
orders_menus
- Em espera 
- A ser preparada
- Concluida
*/
CREATE TABLE orders_menus(
	id int identity primary key,
	[menu] int references menus(id),
	[quantity] INT NOT NULL check([quantity]>(0) AND [quantity]<=(5)),
	[order] int references orders(id),
	[state] int DEFAULT 1 check ([state] in ('1','2','3'))
);


CREATE TABLE shopping_carts(
	id int identity primary key,
	[user] int references users(id)
);

CREATE TABLE shopping_carts_menus(
	id int identity primary key,
	[shopping_carts] int references shopping_carts(id),
	[menu] int references menus(id),
	insertdate datetime DEFAULT getdate()
);

CREATE TRIGGER TR_CreateShoppingCart ON users
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
 
    DECLARE @UserId INT;
 
    SELECT @UserId = INSERTED.id FROM INSERTED;
 
    INSERT INTO shopping_carts([user]) VALUES (@UserId);
END

CREATE TRIGGER TR_CalcStarsMenu ON menu_comments
AFTER INSERT
AS
 
BEGIN
	SET NOCOUNT ON;
 
    DECLARE @MenuId INT;
    DECLARE @star INT;
    DECLARE @NumComments INT;
    DECLARE @totalstars INT;

 
    SELECT @MenuId = INSERTED.menu FROM INSERTED;
    SELECT @star = INSERTED.stars FROM INSERTED;

	SELECT @NumComments = COUNT(*) FROM menu_comments WHERE menu = @MenuId
	SELECT @totalstars = SUM(stars) FROM menu_comments WHERE menu = @MenuId


	UPDATE menus SET stars = (@totalstars / @NumComments) WHERE id = @MenuId
END


CREATE TRIGGER TR_CalcStarsMenu_DELETE ON menu_comments
FOR DELETE
AS
BEGIN
 
    DECLARE @MenuId INT;
    DECLARE @NumComments INT;
    DECLARE @totalstars INT;
 
    SELECT @MenuId = deleted.menu FROM deleted;

	SELECT @NumComments = COUNT(*) FROM menu_comments WHERE menu = @MenuId
    IF @NumComments > 0 BEGIN
	    SELECT @totalstars = SUM(stars) FROM menu_comments WHERE menu = @MenuId
	    UPDATE menus SET stars = (@totalstars / @NumComments) WHERE id = @MenuId
    END
    ELSE BEGIN
        UPDATE menus SET stars = 0 WHERE id = @MenuId
    END
END
