-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: inmobiliaria
-- ------------------------------------------------------
-- Server version	8.0.46

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `imagenes`
--

DROP TABLE IF EXISTS `imagenes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `imagenes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `InmuebleId` int NOT NULL,
  `Url` varchar(500) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Imagenes_Inmueble` (`InmuebleId`),
  CONSTRAINT `FK_Imagenes_Inmueble` FOREIGN KEY (`InmuebleId`) REFERENCES `inmueble` (`ID_inmueble`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `imagenes`
--

LOCK TABLES `imagenes` WRITE;
/*!40000 ALTER TABLE `imagenes` DISABLE KEYS */;
INSERT INTO `imagenes` VALUES (4,14,'/Uploads/Inmuebles/14/c1080664-fee3-46cb-ad40-81591f42cb9e.avif'),(5,14,'/Uploads/Inmuebles/14/065cc92f-d180-4473-8b4c-7f9a2e9940a2.avif'),(6,14,'/Uploads/Inmuebles/14/1c193004-bee4-4b1b-943a-64d468fa5284.avif'),(8,15,'/Uploads/Inmuebles/15/9ce123c9-d645-4d22-b55e-ed655d7551f2.jpg'),(9,14,'/Uploads/Inmuebles/14/7c461aed-c0b8-4983-bb75-a21608282eb6.avif');
/*!40000 ALTER TABLE `imagenes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `inmueble`
--

DROP TABLE IF EXISTS `inmueble`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
  CONSTRAINT `FK_Inmueble_Propietario` FOREIGN KEY (`ID_propietario`) REFERENCES `propietario` (`id_propietario`),
  CONSTRAINT `FK_Inmueble_Tipo` FOREIGN KEY (`ID_tipo`) REFERENCES `tipoinmueble` (`ID_tipo`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inmueble`
--

LOCK TABLES `inmueble` WRITE;
/*!40000 ALTER TABLE `inmueble` DISABLE KEYS */;
INSERT INTO `inmueble` VALUES (1,1,'San Martin 1030',2,2,153.0000000,120000.00,25.00,1),(2,2,'Av. España 1250',4,1,154.0000000,150000.00,30.00,1),(3,3,'Mitre 845',6,2,155.0000000,180000.00,30.00,1),(4,5,'Rivadavia 620',2,4,156.0000000,95000.00,20.00,1),(5,6,'Belgrano 1420',4,5,157.0000000,175000.00,25.00,1),(6,7,'Las Heras 930',3,1,158.0000000,135000.00,25.00,1),(7,8,'Colon 450',8,6,159.0000000,220000.00,35.00,1),(8,9,'Maipú 1120',5,7,160.0000000,195000.00,30.00,0),(9,10,'Sarmiento 780',2,3,161.0000000,110000.00,20.00,1),(10,2,'9 de Julio 1550',6,1,162.0000000,160000.00,25.00,1),(14,8,'Rivadavia 456',2,6,-5.5000000,80000.00,50.00,1),(15,10,'Riobamba 123',2,5,-65.0000000,120000.00,50.00,1);
/*!40000 ALTER TABLE `inmueble` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `inquilino`
--

DROP TABLE IF EXISTS `inquilino`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `inquilino` (
  `ID_inquilino` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) NOT NULL,
  `Apellido` varchar(50) NOT NULL,
  `DNI` varchar(20) NOT NULL,
  `Telefono` varchar(30) DEFAULT NULL,
  `Mail` varchar(100) NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID_inquilino`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inquilino`
--

LOCK TABLES `inquilino` WRITE;
/*!40000 ALTER TABLE `inquilino` DISABLE KEYS */;
INSERT INTO `inquilino` VALUES (1,'Federico','Ponce','38765432','2664123001','federico.ponce@gmail.com',1),(2,'Pedro','García','39876543','2664987652','pedro.garcia@gmail.com',1),(3,'Sofía','López','43567890','2664567891','sofia.lopez@gmail.com',0),(4,'Martín','Sánchez','37890123','2664234568','martin.sanchez@gmail.com',0),(5,'Carla','Montero','45896231','2664567891','carlita@gmail.com',0),(6,'Valentina','Torres','44678901','2665678901','valentina.torres@gmail.com',1),(7,'Nicolás','Castro','40567812','2665789012','nicolas.castro@gmail.com',1),(8,'Camila','Vega','43901234','2665890123','camila.vega@gmail.com',1),(9,'Tomás','Molina','41678923','2665901234','tomas.molina@gmail.com',1),(10,'Julieta','Navarro','45234567','2665012345','julieta.navarro@gmail.com',1);
/*!40000 ALTER TABLE `inquilino` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pago`
--

DROP TABLE IF EXISTS `pago`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pago` (
  `ID_pago` int NOT NULL AUTO_INCREMENT,
  `ID_reserva` int NOT NULL,
  `FechaPago` date NOT NULL,
  `Monto` decimal(10,2) NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT '1',
  `Concepto` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID_pago`),
  KEY `FK_Pago_Reserva` (`ID_reserva`),
  CONSTRAINT `FK_Pago_Reserva` FOREIGN KEY (`ID_reserva`) REFERENCES `reserva` (`ID_reserva`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pago`
--

LOCK TABLES `pago` WRITE;
/*!40000 ALTER TABLE `pago` DISABLE KEYS */;
INSERT INTO `pago` VALUES (1,3,'2026-09-13',90000.00,1,'Multa por finalización anticipada (25,00%)');
/*!40000 ALTER TABLE `pago` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `propietario`
--

DROP TABLE IF EXISTS `propietario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `propietario` (
  `id_propietario` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) NOT NULL,
  `Apellido` varchar(50) NOT NULL,
  `DNI` varchar(20) NOT NULL,
  `Telefono` varchar(30) DEFAULT NULL,
  `Mail` varchar(100) NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id_propietario`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `propietario`
--

LOCK TABLES `propietario` WRITE;
/*!40000 ALTER TABLE `propietario` DISABLE KEYS */;
INSERT INTO `propietario` VALUES (1,'Ana','Gómez','40123456','2664123456','ana.gomez@gmail.com',1),(2,'Juan','Pérez','38987654','2664987654','juan.perez@gmail.com',1),(3,'María','Rodríguez','42567890','2664567890','maria.rodriguez@gmail.com',1),(4,'Carlos','Fernández','36789012','2664234567','carlos.fernandez@gmail.com',0),(5,'Laura','Martínez','41234567','2665123456','laura.martinez@gmail.com',1),(6,'Diego','González','39567821','2665345678','diego.gonzalez@gmail.com',1),(7,'Sofía','Romero','43890123','2665456789','sofia.romero@gmail.com',1),(8,'María','Sánchez','44854789','3511234567','maria.sanchez@gmail.com',1),(9,'Brianna','Lucero','45896231','3511234567','lucero@gmail.com',1),(10,'Luciana','Ramos','36985210','2665232628','luli@gmail.com',1);
/*!40000 ALTER TABLE `propietario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reserva`
--

DROP TABLE IF EXISTS `reserva`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reserva` (
  `ID_reserva` int NOT NULL AUTO_INCREMENT,
  `ID_inquilino` int NOT NULL,
  `ID_inmueble` int NOT NULL,
  `FechaInicio` datetime NOT NULL,
  `FechaFin` datetime NOT NULL,
  `MontoPorDia` decimal(10,2) NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT '1',
  `FechaFinEfectiva` datetime DEFAULT NULL,
  PRIMARY KEY (`ID_reserva`),
  KEY `FK_Reserva_Inquilino` (`ID_inquilino`),
  KEY `FK_Reserva_Inmueble` (`ID_inmueble`),
  CONSTRAINT `FK_Reserva_Inmueble` FOREIGN KEY (`ID_inmueble`) REFERENCES `inmueble` (`ID_inmueble`),
  CONSTRAINT `FK_Reserva_Inquilino` FOREIGN KEY (`ID_inquilino`) REFERENCES `inquilino` (`ID_inquilino`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reserva`
--

LOCK TABLES `reserva` WRITE;
/*!40000 ALTER TABLE `reserva` DISABLE KEYS */;
INSERT INTO `reserva` VALUES (1,1,1,'2026-09-10 14:00:00','2026-09-15 10:00:00',120000.00,1,NULL),(2,2,2,'2026-09-20 14:00:00','2026-09-25 10:00:00',150000.00,1,NULL),(3,6,3,'2026-10-01 14:00:00','2026-10-07 10:00:00',180000.00,0,'2026-10-05 00:00:00'),(4,7,4,'2026-10-10 14:00:00','2026-10-13 10:00:00',95000.00,1,NULL),(5,8,5,'2026-10-15 14:00:00','2026-10-20 10:00:00',175000.00,1,NULL),(6,9,6,'2026-11-01 14:00:00','2026-11-05 10:00:00',135000.00,1,NULL),(7,10,7,'2026-11-10 14:00:00','2026-11-15 10:00:00',220000.00,1,NULL),(8,1,9,'2026-11-20 14:00:00','2026-11-23 10:00:00',110000.00,1,NULL),(9,2,10,'2026-12-01 14:00:00','2026-12-06 10:00:00',160000.00,1,NULL),(10,6,1,'2027-01-05 14:00:00','2027-01-10 10:00:00',120000.00,0,NULL);
/*!40000 ALTER TABLE `reserva` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tipoinmueble`
--

DROP TABLE IF EXISTS `tipoinmueble`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tipoinmueble` (
  `ID_tipo` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(255) NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID_tipo`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tipoinmueble`
--

LOCK TABLES `tipoinmueble` WRITE;
/*!40000 ALTER TABLE `tipoinmueble` DISABLE KEYS */;
INSERT INTO `tipoinmueble` VALUES (1,'Departamento',1),(2,'Casa',1),(3,'Local',1),(4,'Monoambiente',1),(5,'Loft',1),(6,'Cabaña',1),(7,'Dúplex',1),(8,'Quinta',0);
/*!40000 ALTER TABLE `tipoinmueble` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuario` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) NOT NULL,
  `Apellido` varchar(100) NOT NULL,
  `Email` varchar(150) NOT NULL,
  `Clave` varchar(255) NOT NULL,
  `Avatar` varchar(255) DEFAULT NULL,
  `Rol` int NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` VALUES (1,'Ana','Quevedo','admin@inmobiliaria.com','100000.paPSaSVF/j4l1BwHT7deDQ==.Tx8jHKXaH/iOM0UFc28zZJSUrxq7wXZ2q9NwySwo2rY=',NULL,1,1);
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'inmobiliaria'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-09-14 16:32:47
