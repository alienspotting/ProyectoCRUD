
use proyectoCRUD;

/*--INSERCION DE DATOS DE PRUEBA--*/

/*EMPRESAS*/
INSERT INTO Empresas VALUES ('A','Empresa Principal');
INSERT INTO Empresas VALUES ('B','Empresa Secundaria');


/*GRUPOS*/
INSERT INTO Grupos VALUES ('A','1001','Clientes Nacionales');
INSERT INTO Grupos VALUES ('A','1002','Clientes Internacionales');
INSERT INTO Grupos VALUES ('A','1003','Clientes VIP');
INSERT INTO Grupos VALUES ('B','2001','Clientes Empresa B');
INSERT INTO Grupos VALUES ('B','2002','Clientes VIP Empresa B');

/*CLIENTES*/
INSERT INTO Clientes
(
    empresaid,
    grupoid,
    cuentaid,
    nombre,
    comercial,
    direccion,
    direccion1,
    poblacion,
    cpid,
    nifid,
    nifpaisid,
    telefono,
    fax,
    movil,
    correo,
    factura_electronica,
    operativo,
    observaciones
)
VALUES

-- Empresa A / Grupo 1001
(
    'A','1001',1,
    'Transportes Aragón',
    'TA Logística',
    'Calle Mayor 15',
    '',
    'Zaragoza',
    '50001',
    'B12345678',
    'ES',
    '976111111',
    '',
    '600111111',
    'info@transportesaragon.es',
    1,
    1,
    'Cliente habitual'
),

(
    'A','1001',2,
    'Distribuciones Ebro',
    'Ebro Distribución',
    'Avenida Navarra 20',
    '',
    'Zaragoza',
    '50010',
    'B23456789',
    'ES',
    '976222222',
    '',
    '600222222',
    'contacto@ebro.es',
    0,
    1,
    'Descuento especial'
),

(
    'A','1001',3,
    'Papelería Central',
    'Papelería Central',
    'Paseo Independencia 8',
    '',
    'Zaragoza',
    '50004',
    'B34567890',
    'ES',
    '976333333',
    '',
    '600333333',
    'ventas@papeleriacentral.es',
    1,
    0,
    'Cliente inactivo'
),

-- Empresa A / Grupo 1002
(
    'A','1002',1,
    'Berlin Import GmbH',
    'Berlin Import',
    'Alexanderplatz 5',
    '',
    'Berlín',
    '10178',
    'DE123456789',
    'DE',
    '004930123456',
    '',
    '0049170123456',
    'sales@berlinimport.de',
    1,
    1,
    'Cliente internacional'
),

(
    'A','1002',2,
    'Paris Logistics',
    'Paris Logistics',
    'Rue Victor Hugo 10',
    '',
    'París',
    '75001',
    'FR987654321',
    'FR',
    '003314567890',
    '',
    '003361234567',
    'contact@parislogistics.fr',
    1,
    1,
    'Facturación mensual'
),

-- Empresa A / Grupo 1003
(
    'A','1003',1,
    'Grupo VIP Europa',
    'VIP Europa',
    'Gran Vía 100',
    'Planta 7',
    'Madrid',
    '28013',
    'B45678901',
    'ES',
    '915111111',
    '',
    '600444444',
    'vip@europa.es',
    1,
    1,
    'Cliente preferente'
),

-- Empresa B / Grupo 2001
(
    'B','2001',1,
    'Construcciones Delta',
    'Delta Obras',
    'Calle Sol 25',
    '',
    'Valencia',
    '46001',
    'B56789012',
    'ES',
    '961111111',
    '',
    '600555555',
    'info@delta.es',
    0,
    1,
    'Pago a 60 días'
),

(
    'B','2001',2,
    'Hormigones Levante',
    'Levante Hormigones',
    'Polígono Industrial Sur',
    '',
    'Valencia',
    '46015',
    'B67890123',
    'ES',
    '962222222',
    '',
    '600666666',
    'ventas@levante.es',
    1,
    1,
    'Cliente estratégico'
),

-- Empresa B / Grupo 2002
(
    'B','2002',1,
    'Premium Partners',
    'Premium Partners',
    'Calle Serrano 45',
    '',
    'Madrid',
    '28001',
    'B78901234',
    'ES',
    '913333333',
    '',
    '600777777',
    'contacto@premium.es',
    1,
    1,
    'Grupo premium'
);