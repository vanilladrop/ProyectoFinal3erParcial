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
	montoTotal decimal(5,2),
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

LOAD DATA INFILE "F:/Documents/UPP/3er Cuatrimestre/Fundamentos de Bases de Datos/Proyecto/ProyectoFinal3erParcial/productos.csv"
INTO TABLE productos
FIELDS TERMINATED BY ','
ENCLOSED BY '"'
LINES TERMINATED BY '\n'
IGNORE 1 ROWS;

LOAD DATA INFILE "F:/Documents/UPP/3er Cuatrimestre/Fundamentos de Bases de Datos/Proyecto/ProyectoFinal3erParcial/clientes.csv"
INTO TABLE cliente
FIELDS TERMINATED BY ','
ENCLOSED BY '"'
LINES TERMINATED BY '\n'
IGNORE 1 ROWS;

LOAD DATA INFILE "F:/Documents/UPP/3er Cuatrimestre/Fundamentos de Bases de Datos/Proyecto/ProyectoFinal3erParcial/empleados.csv"
INTO TABLE empleado
FIELDS TERMINATED BY ','
ENCLOSED BY '"'
LINES TERMINATED BY '\n'
IGNORE 1 ROWS;

LOAD DATA INFILE "F:/Documents/UPP/3er Cuatrimestre/Fundamentos de Bases de Datos/Proyecto/ProyectoFinal3erParcial/ventas.csv"
INTO TABLE venta
FIELDS TERMINATED BY ','
ENCLOSED BY '"'
LINES TERMINATED BY '\n'
IGNORE 1 ROWS;

LOAD DATA INFILE "F:/Documents/UPP/3er Cuatrimestre/Fundamentos de Bases de Datos/Proyecto/ProyectoFinal3erParcial/productosVenta.csv"
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

SELECT DISTINCT cliente.telefono AS "Teléfono de todos los clientes que compraron alcohol" FROM cliente
JOIN venta ON venta.idCliente = cliente.RFC
JOIN productosVenta ON productosVenta.idVenta = venta.idVenta
JOIN productos ON productos.idProducto = productosVenta.idProducto
WHERE productos.nombre LIKE 'Alcohol%';

SELECT venta.idVenta AS "Números de venta mayores a $150 vendidas por Víctor" FROM venta
JOIN empleado ON venta.idEmpleado = empleado.idEmpleado
WHERE venta.montoTotal > 150.00 AND empleado.nombre LIKE 'Victor%';

SELECT COUNT(productosVenta.idProducto) AS "Cantidad de productos vendidos con el código de barras '7501011105171'" FROM productosVenta
JOIN productos ON productos.idProducto = productosVenta.idProducto
WHERE productosVenta.idProducto = 7501011105171;

SELECT DISTINCT empleado.nombre AS "Nombre del empleado que ha vendido Jabón Zote Blanco de 400g" FROM empleado
JOIN venta ON venta.idEmpleado = empleado.idEmpleado
JOIN productosVenta on productosVenta.idVenta = venta.idVenta
JOIN productos ON productos.idProducto = productosVenta.idProducto
WHERE productos.nombre LIKE 'Jabón Zote Blanco 400g';

SELECT DISTINCT productos.nombre "Nombre de los productos mayores a 20 pesos vendidos por Víctor" FROM productos
JOIN productosVenta ON productosVenta.idProducto = productos.idProducto
JOIN venta ON venta.idVenta = productosVenta.idVenta
JOIN empleado ON empleado.idEmpleado = venta.idEmpleado
WHERE productos.precioVenta > 20.00 AND empleado.nombre LIKE 'Victor%';

SELECT DISTINCT cliente.nombre "Nombre de los clientes que han sido atendidos por Gilberto" FROM cliente
JOIN venta ON venta.idCliente = cliente.RFC
JOIN empleado ON empleado.idEmpleado = venta.idEmpleado
WHERE empleado.nombre LIKE 'Gilberto%';

SELECT productos.nombre AS "Nombre de todos los productos vendidos en la venta 6 con costo de mayoreo menor a $10.00", productos.costoMayoreo AS "Costo de mayoreo" FROM productos
JOIN productosVenta ON productosVenta.idProducto = productos.idProducto
JOIN venta ON venta.idVenta = productosVenta.idVenta
WHERE venta.idVenta = 6 AND productos.costoMayoreo < 10.00;

SELECT COUNT(productos.idProducto) AS "Cantidad total de Sun Chips compradas por el cliente con RFC 'CEHM0109S99'" FROM productos
JOIN productosVenta ON productosVenta.idProducto = productos.idProducto
JOIN venta ON venta.idVenta = productosVenta.idVenta
JOIN cliente ON cliente.RFC = venta.idCliente
WHERE cliente.RFC LIKE 'CEHM0109S99' AND productos.nombre LIKE 'Sun Chips%';

SELECT venta.idVenta AS "Número de venta en las cuales se han vendido Triki-Trakes" FROM venta
JOIN productosVenta ON productosVenta.idVenta = venta.idVenta
JOIN productos ON productos.idProducto = productosVenta.idProducto
WHERE productos.nombre LIKE '%Triki-Trakes%';

SELECT venta.idVenta AS "Números de venta", productos.nombre AS "Productos", empleado.nombre AS "Nombre del empleado" FROM venta
JOIN productosVenta ON productosVenta.idVenta = venta.idVenta
JOIN productos ON productos.idProducto = productosVenta.idProducto
JOIN empleado ON empleado.idEmpleado = venta.idEmpleado
WHERE productos.nombre LIKE "%Sonric%";