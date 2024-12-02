-- Створення нової бази даних, якщо її немає
CREATE DATABASE IF NOT EXISTS trolleydepot;
USE trolleydepot;

-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: trolleydepot
-- ------------------------------------------------------
-- Server version	8.0.40

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
-- Table structure for table `drivers`
--

DROP TABLE IF EXISTS `drivers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drivers` (
  `id` int NOT NULL AUTO_INCREMENT,
  `surname` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `name` varchar(255) NOT NULL,
  `middle_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `age` int DEFAULT NULL,
  `years_of_experience` int DEFAULT '0',
  `drivers_licence` varchar(9) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `category` enum('D','DE') DEFAULT 'D',
  `work_schedule` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `drivers_licence` (`drivers_licence`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drivers`
--

LOCK TABLES `drivers` WRITE;
/*!40000 ALTER TABLE `drivers` DISABLE KEYS */;
INSERT INTO `drivers` VALUES (1,'Іваненко','Петро','Олегович',42,15,'АА123456','D','5-23'),(2,'Петренко','Анна','Василівна',34,8,'АВ789012','D','5-23'),(3,'Коваленко','Микола','Іванович',47,20,'АI345678','DE','5-23'),(4,'Мельник','Світлана','Петрівна',36,9,'АІ456789','D','5-23'),(5,'Романюк','Олексій','Іванович',41,13,'АС123890','D','5-23');
/*!40000 ALTER TABLE `drivers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `expenses`
--

DROP TABLE IF EXISTS `expenses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `expenses` (
  `id` int NOT NULL AUTO_INCREMENT,
  `expenses_type` varchar(255) DEFAULT NULL,
  `amount` decimal(10,2) DEFAULT NULL,
  `expenses_date` date DEFAULT NULL,
  `description` text,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `expenses`
--

LOCK TABLES `expenses` WRITE;
/*!40000 ALTER TABLE `expenses` DISABLE KEYS */;
INSERT INTO `expenses` VALUES (1,'Аварія',12400.00,'2024-11-14','Вїхали в бік тролейбуса');
/*!40000 ALTER TABLE `expenses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `maintenance`
--

DROP TABLE IF EXISTS `maintenance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `maintenance` (
  `id` int NOT NULL AUTO_INCREMENT,
  `trolleybusID` int DEFAULT NULL,
  `service_date` date DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `cost` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `trolleybusID` (`trolleybusID`),
  CONSTRAINT `maintenance_ibfk_1` FOREIGN KEY (`trolleybusID`) REFERENCES `trolleybuses` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `maintenance`
--

LOCK TABLES `maintenance` WRITE;
/*!40000 ALTER TABLE `maintenance` DISABLE KEYS */;
INSERT INTO `maintenance` VALUES (1,4,'2024-11-19','Заміна колеса',1300.00);
/*!40000 ALTER TABLE `maintenance` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reports`
--

DROP TABLE IF EXISTS `reports`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reports` (
  `id` int NOT NULL AUTO_INCREMENT,
  `report_date` date DEFAULT NULL,
  `passengers_count` int DEFAULT NULL,
  `accidents_count` int DEFAULT NULL,
  `delay` int DEFAULT NULL,
  `driverID` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `driverID` (`driverID`),
  CONSTRAINT `reports_ibfk_1` FOREIGN KEY (`driverID`) REFERENCES `drivers` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reports`
--

LOCK TABLES `reports` WRITE;
/*!40000 ALTER TABLE `reports` DISABLE KEYS */;
INSERT INTO `reports` VALUES (1,'2024-11-19',24,1,12,3),(2,'2024-11-12',21,2,53,2);
/*!40000 ALTER TABLE `reports` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `routes`
--

DROP TABLE IF EXISTS `routes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `routes` (
  `id` int NOT NULL AUTO_INCREMENT,
  `route_num` int NOT NULL,
  `start_point` varchar(255) DEFAULT NULL,
  `end_point` varchar(255) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `routes`
--

LOCK TABLES `routes` WRITE;
/*!40000 ALTER TABLE `routes` DISABLE KEYS */;
INSERT INTO `routes` VALUES (1,2,'Південний вокзал','с. Розсошенці',NULL),(2,3,'Склозавод','Завод \'ГРЛ\'','через вул.Сінну'),(3,4,'Південний вокзал','Завод \'ГРЛ\'','через Центр'),(4,5,'Автовокзал','Автовокзал','КІЛЬЦЕВИЙ : Половки,Браїлки'),(5,7,'Інститут зв\'язку','с. Розсошенці','через м/н Алмазний'),(6,12,'Інститут зв\'язку','м/н Левада',NULL),(7,15,'Автовокзал','Автовокзал','КІЛЬЦЕВИЙ : Половки,Алмазний'),(8,16,'Київський вокзал','вул.Героїв України','через вул.Сінну');
/*!40000 ALTER TABLE `routes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stops`
--

DROP TABLE IF EXISTS `stops`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stops` (
  `id` int NOT NULL AUTO_INCREMENT,
  `stop_name` varchar(255) NOT NULL,
  `route_id` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `route_id` (`route_id`),
  CONSTRAINT `stops_ibfk_1` FOREIGN KEY (`route_id`) REFERENCES `routes` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stops`
--

LOCK TABLES `stops` WRITE;
/*!40000 ALTER TABLE `stops` DISABLE KEYS */;
INSERT INTO `stops` VALUES (1,'вул.Великотирнівська',1),(2,'Зупинка 2',3),(3,'Зупинка 3',NULL),(4,'Зупинка 4',NULL);
/*!40000 ALTER TABLE `stops` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `trolleybuses`
--

DROP TABLE IF EXISTS `trolleybuses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `trolleybuses` (
  `id` int NOT NULL AUTO_INCREMENT,
  `trolley_num` int DEFAULT NULL,
  `mileage` int DEFAULT '0',
  `status` enum('на лінії','в ремонті','на стоянці') DEFAULT 'на стоянці',
  `trolley_routeID` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `trolley_num` (`trolley_num`),
  KEY `trolley_routeID` (`trolley_routeID`),
  CONSTRAINT `trolleybuses_ibfk_1` FOREIGN KEY (`trolley_routeID`) REFERENCES `routes` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `trolleybuses`
--

LOCK TABLES `trolleybuses` WRITE;
/*!40000 ALTER TABLE `trolleybuses` DISABLE KEYS */;
INSERT INTO `trolleybuses` VALUES (1,166,12340,'на стоянці',1),(2,164,23145,'на стоянці',1),(3,146,7890,'на лінії',1),(4,113,56430,'на стоянці',2),(5,118,34205,'на лінії',2),(6,111,9510,'на стоянці',3),(7,139,41890,'в ремонті',3),(8,122,15370,'на стоянці',4),(9,119,28460,'на стоянці',4),(10,124,3456,'на лінії',4),(11,170,14892,'на лінії',4),(12,134,27543,'на лінії',4),(13,138,9857,'на лінії',4),(14,149,60120,'на лінії',4),(15,107,25034,'на лінії',5),(16,148,19728,'на лінії',5),(17,156,15472,'на лінії',5),(18,132,4596,'на лінії',5),(19,141,6574,'на лінії',6),(20,140,21643,'на лінії',6),(21,143,42371,'на лінії',6),(22,165,28943,'на лінії',6),(23,120,23813,'на лінії',7),(24,150,8123,'на лінії',7),(25,154,12452,'на лінії',7),(26,133,7124,'на лінії',7),(27,137,5478,'на лінії',7),(28,136,42323,'на лінії',7),(29,144,1032,'на лінії',7),(30,142,6213,'на лінії',8);
/*!40000 ALTER TABLE `trolleybuses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'trolleydepot'
--

--
-- Dumping routines for database 'trolleydepot'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-11-20 20:50:18
