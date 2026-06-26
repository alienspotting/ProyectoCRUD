use proyectoCRUD;

/*------------------- PROCEDIMIENTOS ---------------------*/
/*CLIENTES_MOD (LECTURA GENERAL)*/
CREATE PROCEDURE cargar_clientes_mod
(
    @empresaid VARCHAR(10),
    @operativo TINYINT,
    @nombre VARCHAR(100) = NULL
)
AS
BEGIN

    SELECT *
    FROM Clientes
    WHERE empresaid = @empresaid

    AND
    (
        @operativo = 3
        OR (@operativo = 1 AND operativo = 1)
        OR (@operativo = 2 AND operativo = 0)
    )

    AND
    (
        @nombre IS NULL
        OR @nombre = ''
        OR nombre LIKE '%' + @nombre + '%'
    )

    ORDER BY nombre;

END

/*DETALLE_CLIENTE_LECTURA */
create procedure cargar_detalle_cliente_lee
(
    @empresaid varchar(100),
    @grupoid varchar(100),
    @cuentaid int
)
as
begin
select 
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
    observaciones,
    operativo

from clientes
where empresaid=@empresaid
and grupoid=@grupoid
and cuentaid=@cuentaid
end


/*CLIENTE DETALLE MANTENIMIENTO (UPDATE, ELIMINAR, INSERTAR) */
CREATE PROCEDURE detalle_cliente_mantenimiento
(
    @modo INT,

    @empresaid VARCHAR(10),
    @grupoid VARCHAR(10),
    @cuentaid INT,

    @nombre NVARCHAR(100) = '',
    @comercial NVARCHAR(100) = '',
    @direccion NVARCHAR(200) = '',
    @direccion1 NVARCHAR(200) = '',
    @cpid NVARCHAR(10) = '',
    @poblacion NVARCHAR(100) = '',
    @nifid NVARCHAR(20) = '',
    @nifpaisid NVARCHAR(5) = '',
    @telefono NVARCHAR(30) = '',
    @observaciones NVARCHAR(MAX) = '',
    @operativo BIT = 1
)
AS
BEGIN
    SET NOCOUNT ON;

    /* =========================
       UPDATE
    ========================= */
    IF @modo = 1
    BEGIN

        UPDATE Clientes
        SET
            nombre = @nombre,
            comercial = @comercial,
            direccion = @direccion,
            direccion1 = @direccion1,
            cpid = @cpid,
            poblacion = @poblacion,
            nifid = @nifid,
            nifpaisid = @nifpaisid,
            telefono = @telefono,
            operativo = @operativo
        WHERE empresaid = @empresaid
          AND grupoid = @grupoid
          AND cuentaid = @cuentaid;

    END

    /* =========================
       INSERT
    ========================= */
    ELSE IF @modo = 2
    BEGIN

        DECLARE @nuevoCuentaId INT;

        SELECT @nuevoCuentaId = ISNULL(MAX(cuentaid), 0) + 1
        FROM Clientes
        WHERE empresaid = @empresaid
          AND grupoid = @grupoid;

        INSERT INTO Clientes
        (
            empresaid,
            grupoid,
            cuentaid,
            nombre,
            comercial,
            direccion,
            direccion1,
            cpid,
            poblacion,
            nifid,
            nifpaisid,
            telefono,
            observaciones,
            operativo
        )
        VALUES
        (
            @empresaid,
            @grupoid,
            @nuevoCuentaId,
            @nombre,
            @comercial,
            @direccion,
            @direccion1,
            @cpid,
            @poblacion,
            @nifid,
            @nifpaisid,
            @telefono,
            @observaciones,
            @operativo
        );

        SELECT @nuevoCuentaId AS nuevoCuentaId;

    END

    /* =========================
       DELETE
    ========================= */
    ELSE IF @modo = 3
    BEGIN

        DELETE FROM Clientes
        WHERE empresaid = @empresaid
          AND grupoid = @grupoid
          AND cuentaid = @cuentaid;

    END

END
