CREATE DATABASE proyectoCRUD
GO
use proyectoCRUD;

/*--CREACION TABLAS--*/

/*EMPRESAS*/
CREATE TABLE Empresas
(
    empresaid VARCHAR(10) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL
);


/*GRUPOS*/
CREATE TABLE Grupos
(
    empresaid VARCHAR(10) NOT NULL,
    grupoid VARCHAR(10) NOT NULL,

    nombre VARCHAR(100),

    CONSTRAINT PK_Grupos
        PRIMARY KEY(empresaid, grupoid),

    CONSTRAINT FK_Grupos_Empresas
        FOREIGN KEY(empresaid)
        REFERENCES Empresas(empresaid)
);

/*CLIENTES*/
CREATE TABLE Clientes
(
    empresaid VARCHAR(10) NOT NULL,
    grupoid VARCHAR(10) NOT NULL,
    cuentaid INT NOT NULL,

    nombre VARCHAR(100),
    comercial VARCHAR(100),

    direccion VARCHAR(200),
    direccion1 VARCHAR(200),

    poblacion VARCHAR(100),
    cpid VARCHAR(10),

    nifid VARCHAR(20),
    nifpaisid VARCHAR(10),

    telefono VARCHAR(30),
    fax VARCHAR(30),
    movil VARCHAR(30),
    correo VARCHAR(100),

    factura_electronica BIT,
    operativo BIT,

    observaciones VARCHAR(MAX),

    CONSTRAINT PK_Clientes
        PRIMARY KEY(empresaid, grupoid, cuentaid),

    CONSTRAINT FK_Clientes_Grupos
        FOREIGN KEY(empresaid, grupoid)
        REFERENCES Grupos(empresaid, grupoid)
);