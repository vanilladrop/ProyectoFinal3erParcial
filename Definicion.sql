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

LOAD DATA INFILE "C:/Users/Mariano CH/Documents/UPP/3/MATERIAS/FUNDAMENTOS DE BASES DE DATOS/3/Proyecto/productos.csv"
INTO TABLE productos
FIELDS TERMINATED BY ','
ENCLOSED BY '"'
LINES TERMINATED BY '\n'
IGNORE 1 ROWS;

LOAD DATA INFILE "C:/Users/Mariano CH/Documents/UPP/3/MATERIAS/FUNDAMENTOS DE BASES DE DATOS/3/Proyecto/clientes.csv"
INTO TABLE cliente
FIELDS TERMINATED BY ','
ENCLOSED BY '"'
LINES TERMINATED BY '\n'
IGNORE 1 ROWS;

LOAD DATA INFILE "C:/Users/Mariano CH/Documents/UPP/3/MATERIAS/FUNDAMENTOS DE BASES DE DATOS/3/Proyecto/empelados.csv"
INTO TABLE empleado
FIELDS TERMINATED BY ','
ENCLOSED BY '"'
LINES TERMINATED BY '\n'
IGNORE 1 ROWS;

LOAD DATA INFILE "C:/Users/Mariano CH/Documents/UPP/3/MATERIAS/FUNDAMENTOS DE BASES DE DATOS/3/Proyecto/ventas.csv"
INTO TABLE venta
FIELDS TERMINATED BY ','
ENCLOSED BY '"'
LINES TERMINATED BY '\n'
IGNORE 1 ROWS;

LOAD DATA INFILE "C:/Users/Mariano CH/Documents/UPP/3/MATERIAS/FUNDAMENTOS DE BASES DE DATOS/3/Proyecto/productosVenta.csv"
INTO TABLE productosVenta
FIELDS TERMINATED BY ','
ENCLOSED BY '"'
LINES TERMINATED BY '\n'
IGNORE 1 ROWS;

SELECT * FROM productos;
SELECT * FROM cliente;
SELECT * FROM empleado;
SELECT * FROM venta;
SELECT * FROM productosVenta;

SELECT SUM(productos.precioVenta) FROM productos
JOIN productosVenta ON(productosVenta.idProducto = productos.idProducto)
GROUP BY productosVenta.idVenta;