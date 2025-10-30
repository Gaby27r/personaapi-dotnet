
-- ================================================
-- DDL: Estructura Base de Datos
-- ================================================

USE persona_db;
GO

IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'arq_per_db')
    EXEC('CREATE SCHEMA arq_per_db');
GO

-- Personas
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('arq_per_db.persona') AND type = 'U')
BEGIN
    CREATE TABLE arq_per_db.persona (
        cc        BIGINT       NOT NULL,          
        nombre    NVARCHAR(45) NOT NULL,
        apellido  NVARCHAR(45) NOT NULL,
        genero    CHAR(1)      NOT NULL,          
        edad      TINYINT      NULL,             
        CONSTRAINT PK_persona PRIMARY KEY (cc),
        CONSTRAINT CK_persona_genero CHECK (genero IN ('M','F'))
    );
END
GO

-- Profesiones
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('arq_per_db.profesion') AND type = 'U')
BEGIN
    CREATE TABLE arq_per_db.profesion (
        id   INT           NOT NULL,              
        nom  NVARCHAR(90)  NOT NULL,
        des  NVARCHAR(MAX) NULL,                  
        CONSTRAINT PK_profesion PRIMARY KEY (id)
    );
END
GO

-- Estudios
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('arq_per_db.estudios') AND type = 'U')
BEGIN
    CREATE TABLE arq_per_db.estudios (
        id_prof INT      NOT NULL,
        cc_per  BIGINT   NOT NULL,
        fecha   DATE     NULL,
        univer  NVARCHAR(50) NULL,
        CONSTRAINT PK_estudios PRIMARY KEY (id_prof, cc_per),
        CONSTRAINT FK_estudios_persona
            FOREIGN KEY (cc_per)  REFERENCES arq_per_db.persona(cc),
        CONSTRAINT FK_estudios_profesion
            FOREIGN KEY (id_prof) REFERENCES arq_per_db.profesion(id)
    );

    CREATE INDEX IX_estudios_cc_per  ON arq_per_db.estudios(cc_per);
    CREATE INDEX IX_estudios_id_prof ON arq_per_db.estudios(id_prof);
END
GO

-- Telefono
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('arq_per_db.telefono') AND type = 'U')
BEGIN
    CREATE TABLE arq_per_db.telefono (
        num    VARCHAR(15)   NOT NULL,            
        oper   NVARCHAR(45)  NOT NULL,
        duenio BIGINT        NOT NULL,
        CONSTRAINT PK_telefono PRIMARY KEY (num),
        CONSTRAINT FK_telefono_persona
            FOREIGN KEY (duenio) REFERENCES arq_per_db.persona(cc)
    );

    CREATE INDEX IX_telefono_duenio ON arq_per_db.telefono(duenio);
END
GO

-- ================================================
-- DML: Inserción de Datos de Prueba
-- ================================================

-- Profesiones
INSERT INTO arq_per_db.profesion (id, nom, des)
VALUES
(4, 'Psicología', 'Estudio del comportamiento humano'),
(5, 'Ingeniería Civil', 'Diseño y construcción de obras'),
(6, 'Diseño Gráfico', 'Comunicación visual y creatividad');

-- Personas
INSERT INTO arq_per_db.persona (cc, nombre, apellido, genero, edad)
VALUES
(1004, 'Camila', 'Moreno', 'F', 24),
(1005, 'Andrés', 'Patiño', 'M', 30),
(1006, 'Valeria', 'Ríos', 'F', 26);

-- Estudios
INSERT INTO arq_per_db.estudios (id_prof, cc_per, fecha, univer)
VALUES
(4, 1004, '2021-02-10', 'Universidad del Rosario'),
(5, 1005, '2019-07-05', 'Universidad de La Sabana'),
(6, 1006, '2020-09-01', 'Universidad Externado');

-- Teléfonos
INSERT INTO arq_per_db.telefono (num, oper, duenio)
VALUES
('3004455123', 'WOM', 1004),
('3105566234', 'Virgin', 1005),
('3016677345', 'Movistar', 1006);

-- ================================================
-- CONSULTAS
-- ================================================

SELECT * FROM arq_per_db.persona;
SELECT * FROM arq_per_db.profesion;
SELECT * FROM arq_per_db.estudios;
SELECT * FROM arq_per_db.telefono;

-- Join de ejemplo
SELECT p.nombre + ' ' + p.apellido AS Persona, pr.nom AS Profesion, e.univer
FROM arq_per_db.persona p
JOIN arq_per_db.estudios e ON e.cc_per = p.cc
JOIN arq_per_db.profesion pr ON pr.id = e.id_prof;
GO
