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
-- Table structure for table `auditoria`
--

DROP TABLE IF EXISTS `auditoria`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `auditoria` (
  `ID_auditoria` int NOT NULL AUTO_INCREMENT,
  `ID_usuario` int NOT NULL,
  `Entidad` varchar(50) NOT NULL,
  `ID_entidad` int NOT NULL,
  `Accion` varchar(50) NOT NULL,
  `Fecha` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID_auditoria`),
  KEY `FK_Auditoria_Usuario` (`ID_usuario`),
  CONSTRAINT `FK_Auditoria_Usuario` FOREIGN KEY (`ID_usuario`) REFERENCES `usuario` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `auditoria`
--

LOCK TABLES `auditoria` WRITE;
/*!40000 ALTER TABLE `auditoria` DISABLE KEYS */;
INSERT INTO `auditoria` VALUES (1,1,'Reserva',12,'CREACION','2026-09-14 20:00:42'),(2,1,'Reserva',5,'FINALIZACION','2026-09-14 20:04:12'),(3,1,'Reserva',1,'FINALIZACION','2026-09-16 18:15:20');
/*!40000 ALTER TABLE `auditoria` ENABLE KEYS */;
UNLOCK TABLES;

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
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `imagenes`
--

LOCK TABLES `imagenes` WRITE;
/*!40000 ALTER TABLE `imagenes` DISABLE KEYS */;
INSERT INTO `imagenes` VALUES (4,14,'/Uploads/Inmuebles/14/c1080664-fee3-46cb-ad40-81591f42cb9e.avif'),(5,14,'/Uploads/Inmuebles/14/065cc92f-d180-4473-8b4c-7f9a2e9940a2.avif'),(6,14,'/Uploads/Inmuebles/14/1c193004-bee4-4b1b-943a-64d468fa5284.avif'),(8,15,'/Uploads/Inmuebles/15/9ce123c9-d645-4d22-b55e-ed655d7551f2.jpg'),(9,14,'/Uploads/Inmuebles/14/7c461aed-c0b8-4983-bb75-a21608282eb6.avif'),(10,1,'/Uploads/Inmuebles/1/81c000ca-ae5d-4e24-9d43-b04114024ba3.jpg'),(11,1,'/Uploads/Inmuebles/1/bcb7267f-098c-4c77-b228-b4cb6672cd99.webp'),(12,1,'/Uploads/Inmuebles/1/83e66d49-f082-49c3-a03a-2ea6a398ba74.jpg'),(13,2,'/Uploads/Inmuebles/2/10c9b3ac-5eb5-4d0b-9042-215c314549c8.avif'),(14,2,'/Uploads/Inmuebles/2/d2f36197-af40-4e96-b144-020d96f2b3fd.avif'),(15,2,'/Uploads/Inmuebles/2/d3cb061a-0a66-47dc-9c0e-c8336deea881.png'),(16,2,'/Uploads/Inmuebles/2/d8c37834-605b-4b0c-9154-531a39c2517e.avif'),(17,3,'/Uploads/Inmuebles/3/09a6d756-b7f0-4f34-b4de-785ca5e3a8ef.avif'),(18,3,'/Uploads/Inmuebles/3/65146fc5-a7dd-46b5-8226-afc24ad29527.avif'),(19,3,'/Uploads/Inmuebles/3/fd995f02-9088-4a57-97fa-5304095406ea.png'),(20,3,'/Uploads/Inmuebles/3/e7346ad4-568c-43f8-8e69-bd3959601366.avif'),(21,4,'/Uploads/Inmuebles/4/5185b0a1-a1a0-410c-81ce-8485ba266d2b.png'),(22,4,'/Uploads/Inmuebles/4/6b68700c-14ac-45da-8307-5e2475bc9352.avif'),(23,5,'/Uploads/Inmuebles/5/dbfb7698-30e4-4a26-b13f-5366770e94ae.png'),(24,5,'/Uploads/Inmuebles/5/a08c0239-7254-4cb0-85f4-1ae777c72190.png'),(25,5,'/Uploads/Inmuebles/5/5aa40b50-9eef-4242-be7c-bcdfa66a651c.png'),(26,5,'/Uploads/Inmuebles/5/ea138210-8198-43ae-88a5-cbe916fc574f.png'),(27,5,'/Uploads/Inmuebles/5/eba8cb98-4567-4e66-8b0d-89e1ee5d05f3.png'),(28,6,'/Uploads/Inmuebles/6/cfbdfec0-176c-4df1-aaac-6114fc5bf89f.avif'),(29,6,'/Uploads/Inmuebles/6/8184512a-adce-4026-9029-05ac79f8346c.png'),(30,6,'/Uploads/Inmuebles/6/5f2c29e2-1582-4b00-b35b-bf6dbdb6a618.avif'),(31,6,'/Uploads/Inmuebles/6/17cc9fcf-61d3-4370-801a-3915cfc2d91a.avif'),(32,8,'/Uploads/Inmuebles/8/b59bb2ce-7846-49d7-a53f-7549e5399cbb.avif'),(33,8,'/Uploads/Inmuebles/8/4a83f2b7-6afb-4fe5-b569-5a02018a922d.avif'),(34,8,'/Uploads/Inmuebles/8/f4f2a8a0-b486-4075-b76a-000d4fdc9e27.png'),(35,8,'/Uploads/Inmuebles/8/e03235fa-d24f-4830-b78d-7df5978c77f2.avif'),(36,8,'/Uploads/Inmuebles/8/2de5e390-cd08-4c96-8210-8cfbcf246503.avif'),(37,7,'/Uploads/Inmuebles/7/760ce445-7c01-46dc-9d6b-3d43d3d7f36d.png'),(38,7,'/Uploads/Inmuebles/7/48bb0d94-85fa-41ce-a3fe-0af3d2a99104.png'),(39,7,'/Uploads/Inmuebles/7/408d14e4-9d12-45e7-9a71-f6b1500a22d0.png'),(40,7,'/Uploads/Inmuebles/7/1a8bb2d1-5fdc-4542-b90c-6ad61a79fab9.png'),(41,7,'/Uploads/Inmuebles/7/7dfcfaba-5604-4ea4-934d-d289292040ba.png'),(42,9,'/Uploads/Inmuebles/9/a5003c63-5f6a-4a58-9b5d-3d3c552a8801.png'),(43,9,'/Uploads/Inmuebles/9/1e3644d6-1a94-4423-9f9e-c46ce55a678d.png'),(44,9,'/Uploads/Inmuebles/9/68a15d0e-6a81-4483-978f-cb256dd83b33.png'),(45,9,'/Uploads/Inmuebles/9/ff44b19c-0190-448e-b6fa-1edbf8ef60cc.png'),(46,9,'/Uploads/Inmuebles/9/c99636fd-20f8-4aca-8c49-1966605d5411.png'),(47,10,'/Uploads/Inmuebles/10/a19e54cf-e42f-4e25-b737-b22e9804a2b4.avif'),(48,10,'/Uploads/Inmuebles/10/c9f3b8dc-baa1-4b76-a931-7e487050a3dc.avif'),(49,10,'/Uploads/Inmuebles/10/99bc8f0c-2dcf-4869-9bf3-7eae01507867.png'),(50,10,'/Uploads/Inmuebles/10/e7a02449-9dc0-48d2-b860-69743c78731b.avif');
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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pago`
--

LOCK TABLES `pago` WRITE;
/*!40000 ALTER TABLE `pago` DISABLE KEYS */;
INSERT INTO `pago` VALUES (1,3,'2026-09-13',90000.00,1,'Multa por finalización anticipada (25,00%)'),(2,9,'2026-09-15',800000.00,1,'pago parcial'),(3,9,'2026-09-14',240000.00,1,'Multa por finalización anticipada (50,00%)'),(4,5,'2026-09-14',262500.00,1,'Multa por finalización anticipada (50,00%)'),(5,1,'2026-09-16',30000.00,1,'Multa por finalización anticipada (25,00%)');
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
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reserva`
--

LOCK TABLES `reserva` WRITE;
/*!40000 ALTER TABLE `reserva` DISABLE KEYS */;
INSERT INTO `reserva` VALUES (1,1,1,'2026-09-10 14:00:00','2026-09-15 10:00:00',120000.00,0,'2026-09-14 00:00:00'),(2,2,2,'2026-09-20 14:00:00','2026-09-25 10:00:00',150000.00,1,NULL),(3,6,3,'2026-10-01 14:00:00','2026-10-07 10:00:00',180000.00,0,'2026-10-05 00:00:00'),(4,7,4,'2026-10-10 14:00:00','2026-10-13 10:00:00',95000.00,1,NULL),(5,8,5,'2026-10-15 14:00:00','2026-10-20 10:00:00',175000.00,0,'2026-10-17 00:00:00'),(6,9,6,'2026-11-01 14:00:00','2026-11-05 10:00:00',135000.00,1,NULL),(7,10,7,'2026-11-10 14:00:00','2026-11-15 10:00:00',220000.00,1,NULL),(8,1,9,'2026-11-20 14:00:00','2026-11-23 10:00:00',110000.00,1,NULL),(9,2,10,'2026-12-01 14:00:00','2026-12-06 10:00:00',160000.00,0,'2026-12-03 00:00:00'),(10,6,1,'2027-01-05 14:00:00','2027-01-10 10:00:00',120000.00,0,NULL),(11,2,4,'2026-09-24 00:00:00','2026-09-30 00:00:00',95000.00,1,NULL),(12,2,4,'2026-10-22 00:00:00','2026-10-30 00:00:00',95000.00,1,NULL);
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
INSERT INTO `tipoinmueble` VALUES (1,'Departamento',1),(2,'Casa',1),(3,'Hostal',1),(4,'Monoambiente',1),(5,'Loft',1),(6,'Cabaña',1),(7,'Dúplex',1),(8,'Quinta',0);
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` VALUES (1,'Ana','Quevedo','admin@inmobiliaria.com','100000.paQUA95CPmY0S+JL4xL7hg==.5VOrUFEZV80Y2MbZWA2d8FR/RVBXIMBVaXd9GZrtA2o=',NULL,1,1),(2,'Antonio','Assat','empleado@inmobiliaria.com','100000./NZQUsSOtsJWR/5ubE3XRQ==.Q/W8pSbwnr1ioqmuJAAmN6HsDviglKNCE5kRK4TSCXk=','/Uploads/Usuarios/2/9b2524c8-f19d-451f-9368-74620fa3c35a.jpg',2,1);
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

-- Dump completed on 2026-09-16 19:18:29