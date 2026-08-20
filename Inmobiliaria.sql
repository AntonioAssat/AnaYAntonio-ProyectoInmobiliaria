
CREATE DATABASE IF NOT EXISTS Inmobiliaria;

USE Inmobiliaria;


-- ============================================
-- TABLA: Propietario
-- ============================================

CREATE TABLE IF NOT EXISTS Propietario (
    id_propietario INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    DNI VARCHAR(20) NOT NULL,
    Telefono VARCHAR(30),
    Mail VARCHAR(100) NOT NULL,
    Estado BOOLEAN NOT NULL DEFAULT TRUE
);


-- ============================================
-- TABLA: Inquilino
-- ============================================

CREATE TABLE IF NOT EXISTS Inquilino (
    id_inquilino INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    DNI VARCHAR(20) NOT NULL,
    Telefono VARCHAR(30),
    Mail VARCHAR(100) NOT NULL,
    Estado BOOLEAN NOT NULL DEFAULT TRUE
);


-- ============================================
-- DATOS INICIALES: PROPIETARIOS
-- ============================================

INSERT INTO Propietario
    (Nombre, DNI, Telefono, Mail, Estado)
VALUES
    ('Ana Gómez', '40123456', '2664123456', 'ana.gomez@gmail.com', TRUE),
    ('Juan Pérez', '38987654', '2664987654', 'juan.perez@gmail.com', TRUE),
    ('María Rodríguez', '42567890', '2664567890', 'maria.rodriguez@gmail.com', TRUE),
    ('Carlos Fernández', '36789012', '2664234567', 'carlos.fernandez@gmail.com', FALSE);


-- ============================================
-- DATOS INICIALES: INQUILINOS
-- ============================================

INSERT INTO Inquilino
    (Nombre, DNI, Telefono, Mail, Estado)
VALUES
    ('Lucía Martínez', '41234567', '2664123451', 'lucia.martinez@gmail.com', TRUE),
    ('Pedro García', '39876543', '2664987652', 'pedro.garcia@gmail.com', TRUE),
    ('Sofía López', '43567890', '2664567891', 'sofia.lopez@gmail.com', TRUE),
    ('Martín Sánchez', '37890123', '2664234568', 'martin.sanchez@gmail.com', FALSE);