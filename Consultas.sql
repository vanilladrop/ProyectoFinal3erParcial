SELECT DISTINCT cliente.telefono FROM cliente
JOIN venta ON venta.idCliente = cliente.RFC
JOIN productosVenta ON productosVenta.idVenta = venta.idVenta
JOIN productos ON productos.idProducto = productosVenta.idProducto
WHERE productos.nombre LIKE 'Alcohol%';

SELECT venta.idVenta FROM venta
JOIN empleado ON venta.idEmpleado = empleado.idEmpleado
WHERE venta.montoTotal > 150.00 AND empleado.nombre LIKE 'Victor%';

SELECT COUNT(productosVenta.idProducto) FROM productosVenta
JOIN productos ON productos.idProducto = productosVenta.idProducto
WHERE productosVenta.idProducto = 7501011105171;

SELECT DISTINCT empleado.nombre FROM empleado
JOIN venta ON venta.idEmpleado = empleado.idEmpleado
JOIN productosVenta on productosVenta.idVenta = venta.idVenta
JOIN productos ON productos.idProducto = productosVenta.idProducto
WHERE productos.nombre LIKE 'Jabón Zote Blanco 400g';

SELECT DISTINCT productos.nombre FROM productos
JOIN productosVenta ON productosVenta.idProducto = productos.idProducto
JOIN venta ON venta.idVenta = productosVenta.idVenta
JOIN empleado ON empleado.idEmpleado = venta.idEmpleado
WHERE productos.precioVenta > 20.00 AND empleado.nombre LIKE 'Victor%';

SELECT DISTINCT cliente.nombre FROM cliente
JOIN venta ON venta.idCliente = cliente.RFC
JOIN empleado ON empleado.idEmpleado = venta.idEmpleado
WHERE empleado.nombre LIKE 'Gilberto%';

SELECT productos.costoMayoreo