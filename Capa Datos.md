En esta capa el objetivo principal es conectarse a la base de datos y exponer métodos para leer/escribir datos, sin mezclar reglas de negocio ni lógica de interfaz.

Columnas Del Excel:
RADICADO //Es una columna con datos del Excel Origen
ID //Es una columna con datos del Excel Origen
EMPLEADO //Es una columna con datos del Excel Origen
IDENTIFICACION //Es una columna con datos del Excel Origen
TIPO_DOCUMENTAL //Es una columna con datos del Excel Origen
CODIGO_DE_BARRAS_RECEPCION //Es una columna con datos del Excel Origen
CB Documento //Es una columna con datos del Excel Origen Viene vacia es la quevamos a llenar
CB Expediente //Es una columna con datos del Excel Origen Viene vacia es la quevamos a llenar
CB Caja //Es una columna con datos del Excel Origen Viene vacia es la quevamos a llenar

Atributos de la tabla en db:
--CREATE TABLE CodigoDeBarrasData (
--    IdIdentity INT IDENTITY(1,1) NOT NULL,
--    RADICADO BIGINT,
--    ID BIGINT,
--    EMPLEADO VARCHAR(200),
--    IDENTIFICACION VARCHAR(200),
--    TIPO_DOCUMENTAL VARCHAR(200),
--    CODIGO_DE_BARRAS_RECEPCION VARCHAR(200) NULL,
--    CB_Documento VARCHAR(200) NULL,
--    CB_Expediente VARCHAR(200) NULL,
--    CB_Caja VARCHAR(200) NULL
--);


