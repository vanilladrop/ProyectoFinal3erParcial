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