CREATE DATABASE WebApiBd

create table PRODUCTO(
IdProducto int primary key identity,
CodigoBarra varchar(50) unique,
Nombre varchar(50),
Marca varchar(50),
Categoria varchar(100),
Precio decimal(10,2)
)

INSERT INTO PRODUCTO(CodigoBarra,Nombre,Marca,Categoria,Precio) values
('50910010','Monitor Aoc  - Curvo� Gaming�','AOC','Tecnologia','1200'),
('50910011','IdeaPad 3i','LENOVO','Tecnologia','1700'),
('50910012','SoyMomo Tablet Lite','SOYMOMO','Tecnologia','300'),
('50910013','Lavadora 21 KG WLA-21','WINIA','ElectroHogar','1749'),
('50910014','Congelador 145 Lt Blanco','ELECTROLUX','ElectroHogar','779'),
('50910015','Cafetera TH-130','THOMAS','ElectroHogar','119'),
('50910016','Reloj an�logo Hombre 058','GUESS','Accesorios','699'),
('50910017','Billetera de Cuero Mujer Sophie','REYES','Accesorios','270'),
('50910018','Bufanda Rec Mango Mujer','MANGO','Accesorios','169.90'),
('50910019','Sof� Continental 3 Cuerpos','MICA','Muebles','1299'),
('50910020','Fut�n New Elina 3 Cuerpos','MICA','Muebles','1349'),
('50910021','Mesa Comedor Volterra 6 Sillas','TUHOME','Muebles','624.12')

select * from PRODUCTO


create proc sp_lista_productos
as
begin
	select 
	IdProducto,CodigoBarra,Nombre,
	Marca,Categoria,Precio
	from PRODUCTO
end

go

create proc sp_guardar_producto(
@codigoBarra varchar(50),
@nombre varchar(50),
@marca varchar(50),
@categoria varchar(100),
@precio decimal(10,2)
)as
begin
	insert into PRODUCTO(CodigoBarra,Nombre,Marca,Categoria,Precio)
	values(@codigoBarra,@nombre,@marca,@categoria,@precio)
end

go

create proc sp_editar_producto(
@idProducto int,
@codigoBarra varchar(50) null,
@nombre varchar(50) null,
@marca varchar(50) null,
@categoria varchar(100) null,
@precio decimal(10,2) null
)as
begin

	update PRODUCTO set
	CodigoBarra = isnull(@codigoBarra,CodigoBarra),
	Nombre = isnull(@nombre,Nombre),
	Marca = isnull(@marca,Marca),
	Categoria = isnull(@categoria,Categoria),
	Precio = ISNULL(@precio,Precio)
	where IdProducto = @idProducto

end

go

create proc sp_eliminar_producto(
@idProducto int
)as
begin

 delete from PRODUCTO where IdProducto = @idProducto

end