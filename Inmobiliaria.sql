-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: inmobiliaria
-- ------------------------------------------------------
-- Server version 8.0.46

DROP DATABASE IF EXISTS inmobiliaria;
CREATE DATABASE inmobiliaria;
USE inmobiliaria;

SET FOREIGN_KEY_CHECKS = 0;

-- ------------------------------------------------------
-- Table structure for table `propietario`
-- ------------------------------------------------------

DROP TABLE IF EXISTS `propietario`;

CREATE TABLE `propietario` (
  `id_propietario` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) NOT NULL,
  `Apellido` varchar(50) NOT NULL,
  `DNI` varchar(20) NOT NULL,
  `Telefono` varchar(30) DEFAULT NULL,
  `Mail` varchar(100) NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_propietario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- ------------------------------------------------------
-- Dumping data for table `propietario`
-- ------------------------------------------------------

INSERT INTO `propietario`
(`id_propietario`, `Nombre`, `Apellido`, `DNI`, `Telefono`, `Mail`, `Estado`)
VALUES
(1, 'Ana', 'Gómez', '40123456', '2664123456', 'ana.gomez@gmail.com', 1),
(2, 'Juan', 'Pérez', '38987654', '2664987654', 'juan.perez@gmail.com', 1),
(3, 'María', 'Rodríguez', '42567890', '2664567890', 'maria.rodriguez@gmail.com', 1),
(4, 'Carlos', 'Fernández', '36789012', '2664234567', 'carlos.fernandez@gmail.com', 0),
(5, 'Laura', 'Martínez', '41234567', '2665123456', 'laura.martinez@gmail.com', 1),
(6, 'Diego', 'González', '39567821', '2665345678', 'diego.gonzalez@gmail.com', 1),
(7, 'Sofía', 'Romero', '43890123', '2665456789', 'sofia.romero@gmail.com', 1),
(8, 'María', 'Sánchez', '44854789', '3511234567', 'maria.sanchez@gmail.com', 1),
(9, 'Brianna', 'Lucero', '45896231', '3511234567', 'lucero@gmail.com', 1),
(10, 'Luciana', 'Ramos', '36985210', '2665232628', 'luli@gmail.com', 1);

-- ------------------------------------------------------
-- Table structure for table `inquilino`
-- ------------------------------------------------------

DROP TABLE IF EXISTS `inquilino`;

CREATE TABLE `inquilino` (
  `ID_inquilino` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) NOT NULL,
  `Apellido` varchar(50) NOT NULL,
  `DNI` varchar(20) NOT NULL,
  `Telefono` varchar(30) DEFAULT NULL,
  `Mail` varchar(100) NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID_inquilino`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- ------------------------------------------------------
-- Dumping data for table `inquilino`
-- ------------------------------------------------------

INSERT INTO `inquilino`
(`ID_inquilino`, `Nombre`, `Apellido`, `DNI`, `Telefono`, `Mail`, `Estado`)
VALUES
(1, 'Federico', 'Ponce', '38765432', '2664123001', 'federico.ponce@gmail.com', 1),
(2, 'Pedro', 'García', '39876543', '2664987652', 'pedro.garcia@gmail.com', 1),
(3, 'Sofía', 'López', '43567890', '2664567891', 'sofia.lopez@gmail.com', 0),
(4, 'Martín', 'Sánchez', '37890123', '2664234568', 'martin.sanchez@gmail.com', 0),
(5, 'Carla', 'Montero', '45896231', '2664567891', 'carlita@gmail.com', 0),
(6, 'Valentina', 'Torres', '44678901', '2665678901', 'valentina.torres@gmail.com', 1),
(7, 'Nicolás', 'Castro', '40567812', '2665789012', 'nicolas.castro@gmail.com', 1),
(8, 'Camila', 'Vega', '43901234', '2665890123', 'camila.vega@gmail.com', 1),
(9, 'Tomás', 'Molina', '41678923', '2665901234', 'tomas.molina@gmail.com', 1),
(10, 'Julieta', 'Navarro', '45234567', '2665012345', 'julieta.navarro@gmail.com', 1);

-- ------------------------------------------------------
-- Table structure for table `tipoinmueble`
-- ------------------------------------------------------

DROP TABLE IF EXISTS `tipoinmueble`;

CREATE TABLE `tipoinmueble` (
  `ID_tipo` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(255) NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID_tipo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- ------------------------------------------------------
-- Dumping data for table `tipoinmueble`
-- ------------------------------------------------------

INSERT INTO `tipoinmueble`
(`ID_tipo`, `Nombre`, `Estado`)
VALUES
(1, 'Departamento', 1),
(2, 'Casa', 0),
(3, 'Local', 1),
(4, 'Monoambiente', 1),
(5, 'Loft', 1),
(6, 'Cabaña', 1),
(7, 'Dúplex', 1),
(8, 'Quinta', 0);

-- ------------------------------------------------------
-- Table structure for table `inmueble`
-- ------------------------------------------------------

DROP TABLE IF EXISTS `inmueble`;

CREATE TABLE `inmueble` (
  `ID_inmueble` int NOT NULL AUTO_INCREMENT,
  `ID_propietario` int NOT NULL,
  `Direccion` varchar(255) NOT NULL,
  `Cupo` int NOT NULL,
  `ID_tipo` int NOT NULL,
  `Coordenadas` decimal(10,7) NOT NULL,
  `PrecioPorDia` decimal(10,2) NOT NULL,
  `PorcentajeReserva` decimal(5,2) NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID_inmueble`),
  KEY `FK_Inmueble_Propietario` (`ID_propietario`),
  KEY `FK_Inmueble_Tipo` (`ID_tipo`),
  CONSTRAINT `FK_Inmueble_Propietario`
    FOREIGN KEY (`ID_propietario`)
    REFERENCES `propietario` (`id_propietario`),
  CONSTRAINT `FK_Inmueble_Tipo`
    FOREIGN KEY (`ID_tipo`)
    REFERENCES `tipoinmueble` (`ID_tipo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- ------------------------------------------------------
-- Dumping data for table `inmueble`
-- ------------------------------------------------------

INSERT INTO `inmueble`
(`ID_inmueble`, `ID_propietario`, `Direccion`, `Cupo`, `ID_tipo`,
 `Coordenadas`, `PrecioPorDia`, `PorcentajeReserva`, `Estado`)
VALUES
(1, 1, 'San Martin 1030', 2, 2, 153.0000000, 120000.00, 25.00, 1),
(2, 2, 'Av. España 1250', 4, 1, 154.0000000, 150000.00, 30.00, 1),
(3, 3, 'Mitre 845', 6, 2, 155.0000000, 180000.00, 30.00, 1),
(4, 5, 'Rivadavia 620', 2, 4, 156.0000000, 95000.00, 20.00, 1),
(5, 6, 'Belgrano 1420', 4, 5, 157.0000000, 175000.00, 25.00, 1),
(6, 7, 'Las Heras 930', 3, 1, 158.0000000, 135000.00, 25.00, 1),
(7, 8, 'Colon 450', 8, 6, 159.0000000, 220000.00, 35.00, 1),
(8, 9, 'Maipú 1120', 5, 7, 160.0000000, 195000.00, 30.00, 0),
(9, 10, 'Sarmiento 780', 2, 3, 161.0000000, 110000.00, 20.00, 1),
(10, 2, '9 de Julio 1550', 6, 1, 162.0000000, 160000.00, 25.00, 1);

SET FOREIGN_KEY_CHECKS = 1;
-- ------------------------------------------------------
-- Table structure for table `reserva`
-- ------------------------------------------------------

DROP TABLE IF EXISTS `reserva`;

CREATE TABLE `reserva` (
  `ID_reserva` int NOT NULL AUTO_INCREMENT,
  `ID_inquilino` int NOT NULL,
  `ID_inmueble` int NOT NULL,
  `FechaInicio` datetime NOT NULL,
  `FechaFin` datetime NOT NULL,
  `MontoPorDia` decimal(10,2) NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT '1',

  PRIMARY KEY (`ID_reserva`),

  KEY `FK_Reserva_Inquilino` (`ID_inquilino`),
  KEY `FK_Reserva_Inmueble` (`ID_inmueble`),

  CONSTRAINT `FK_Reserva_Inquilino`
    FOREIGN KEY (`ID_inquilino`)
    REFERENCES `inquilino` (`ID_inquilino`),

  CONSTRAINT `FK_Reserva_Inmueble`
    FOREIGN KEY (`ID_inmueble`)
    REFERENCES `inmueble` (`ID_inmueble`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- ------------------------------------------------------
-- Dumping data for table `reserva`
-- ------------------------------------------------------

INSERT INTO `reserva`
(`ID_reserva`, `ID_inquilino`, `ID_inmueble`,
 `FechaInicio`, `FechaFin`, `MontoPorDia`, `Estado`)
VALUES
(1, 1, 1, '2026-09-10 14:00:00', '2026-09-15 10:00:00', 120000.00, 1),
(2, 2, 2, '2026-09-20 14:00:00', '2026-09-25 10:00:00', 150000.00, 1),
(3, 6, 3, '2026-10-01 14:00:00', '2026-10-07 10:00:00', 180000.00, 1),
(4, 7, 4, '2026-10-10 14:00:00', '2026-10-13 10:00:00', 95000.00, 1),
(5, 8, 5, '2026-10-15 14:00:00', '2026-10-20 10:00:00', 175000.00, 1),
(6, 9, 6, '2026-11-01 14:00:00', '2026-11-05 10:00:00', 135000.00, 1),
(7, 10, 7, '2026-11-10 14:00:00', '2026-11-15 10:00:00', 220000.00, 1),
(8, 1, 9, '2026-11-20 14:00:00', '2026-11-23 10:00:00', 110000.00, 1),
(9, 2, 10, '2026-12-01 14:00:00', '2026-12-06 10:00:00', 160000.00, 1),
(10, 6, 1, '2027-01-05 14:00:00', '2027-01-10 10:00:00', 120000.00, 0);

-- ------------------------------------------------------
-- Dump completed
-- ------------------------------------------------------
