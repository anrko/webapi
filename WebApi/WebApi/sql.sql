use web
go
CREATE TABLE categoria (
IdCategoria            int primary key identity(1,1),
 Nombre    VARCHAR(50) 
);

CREATE TABLE productos(
IdProducto           int primary key identity(1,1),
 Nombre    VARCHAR(50) ,  
 Precio      DECIMAL(19,4) NOT NULL DEFAULT '0.0000',
 Stock    INT ,
 IdCategoria INT NOT NULL FOREIGN KEY REFERENCES categoria
 );
INSERT INTO [web].[dbo].[categoria]([Nombre])VALUES('bebidas')
GO
INSERT INTO [web].[dbo].[categoria]([Nombre])VALUES('Carnes')
GO
INSERT INTO [web].[dbo].[categoria]([Nombre])VALUES('Frutas')
GO

INSERT INTO [web].[dbo].[productos]
	([Nombre],[Precio],[Stock],[IdCategoria])
     VALUES ('gaseosa',1.7,10,1)
GO
INSERT INTO [web].[dbo].[productos]
([Nombre],[Precio],[Stock],[IdCategoria])
     VALUES('cervezas',1.5,30,1)
GO
