DROP DATABASE ProyectoBDTienda;

CREATE DATABASE ProyectoBDTienda;
USE ProyectoBDTienda;

CREATE TABLE cliente(
	RFC varchar(13) PRIMARY KEY NOT NULL,
	nombre varchar(35),
	direccion varchar(35),
	telefono varchar(10)
);

CREATE TABLE empleado(
	idEmpleado int(4) PRIMARY KEY AUTO_INCREMENT NOT NULL,
	nombre varchar(35)
);

CREATE TABLE productos(
	idProducto varchar(13) PRIMARY KEY NOT NULL,
	nombre varchar(50),
	costoMayoreo decimal(5,2),
	precioVenta decimal(5,2),
	enInventario int(4)
);

CREATE TABLE venta(
	idVenta int(7) PRIMARY KEY AUTO_INCREMENT NOT NULL,
	idEmpleado int(4),
	idCliente varchar(13),
	montoTotal decimal(5,3),
	FOREIGN KEY (idEmpleado) REFERENCES empleado(idEmpleado),
	FOREIGN KEY (idCliente) REFERENCES cliente(RFC)
);

CREATE TABLE productosVenta(
	idProducto varchar(13),
	idVenta int(7),
	FOREIGN KEY (idProducto) REFERENCES productos(idProducto),
	FOREIGN KEY (idVenta) REFERENCES venta(idVenta)
);

DESCRIBE cliente;
DESCRIBE empleado;
DESCRIBE productos;
DESCRIBE venta;
DESCRIBE productosVenta;