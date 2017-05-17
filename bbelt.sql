-- MySQL dump 10.13  Distrib 5.7.17, for osx10.12 (x86_64)
--
-- Host: localhost    Database: bbelt
-- ------------------------------------------------------
-- Server version	5.7.17

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Activities`
--

DROP TABLE IF EXISTS `Activities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Activities` (
  `ActivityId` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(45) DEFAULT NULL,
  `Duration` int(11) DEFAULT NULL,
  `DurationInc` varchar(45) DEFAULT NULL,
  `Description` varchar(1000) DEFAULT NULL,
  `CreatedAt` datetime DEFAULT NULL,
  `UpdatedAt` datetime DEFAULT NULL,
  `DateAt` datetime DEFAULT NULL,
  `CreatorId` int(11) DEFAULT NULL,
  `DateEnd` datetime DEFAULT NULL,
  PRIMARY KEY (`ActivityId`),
  KEY `CreatorFKId_idx` (`CreatorId`),
  CONSTRAINT `CreatorFKId` FOREIGN KEY (`CreatorId`) REFERENCES `Users` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Activities`
--

LOCK TABLES `Activities` WRITE;
/*!40000 ALTER TABLE `Activities` DISABLE KEYS */;
INSERT INTO `Activities` VALUES (9,'Hey there.',35,'Minute','Hello tehre hahahahaha','2017-02-27 12:00:39','2017-02-27 12:00:39','2017-03-27 15:50:00',4,'2017-03-27 16:25:00');
/*!40000 ALTER TABLE `Activities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `UserActivities`
--

DROP TABLE IF EXISTS `UserActivities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `UserActivities` (
  `UserActivityId` int(11) NOT NULL AUTO_INCREMENT,
  `ParticipantId` int(11) DEFAULT NULL,
  `ActivityId` int(11) DEFAULT NULL,
  PRIMARY KEY (`UserActivityId`),
  KEY `UserFKId_idx` (`ParticipantId`),
  KEY `ActivityFKId_idx` (`ActivityId`),
  CONSTRAINT `ActivityFKId` FOREIGN KEY (`ActivityId`) REFERENCES `Activities` (`ActivityId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `UserFKId` FOREIGN KEY (`ParticipantId`) REFERENCES `Users` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `UserActivities`
--

LOCK TABLES `UserActivities` WRITE;
/*!40000 ALTER TABLE `UserActivities` DISABLE KEYS */;
INSERT INTO `UserActivities` VALUES (32,4,9),(39,5,9);
/*!40000 ALTER TABLE `UserActivities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Users` (
  `UserId` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(45) DEFAULT NULL,
  `LastName` varchar(45) DEFAULT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `Password` varchar(255) DEFAULT NULL,
  `CreatedAt` datetime DEFAULT NULL,
  `UpdatedAt` datetime DEFAULT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES (1,'Matt','Pedersen','matt@faker.com','AQAAAAEAACcQAAAAEPCqa2KWlVssrcJ1QyGfBHe+ih55LGA2kiKFmyIbBgi2s3Er25jQdZq3GYM+Yb7ABg==','2017-02-24 10:15:00','2017-02-24 10:15:00'),(2,'matt','pedersen','matt@fake.com','AQAAAAEAACcQAAAAECeTGW8fFJl1eB/hQpugEz8nelo4dcycpQ5QuSebj7Xh4oBhkSUraZnni70vlFcc+Q==','2017-02-24 10:40:47','2017-02-24 10:40:47'),(3,'Paul','Bunyon','paul@bunyon.com','AQAAAAEAACcQAAAAEN3/N4ukW4r7FxKPDNrHeqdlFsufTFnWGfjwPmEEdGX4tdQniT5yGgNqscojenWamw==','2017-02-24 11:53:47','2017-02-24 11:53:47'),(4,'Marky','Mark','marky@mark.com','AQAAAAEAACcQAAAAEPoouQm9/TK95QqSAnxIAktLJrw/gAGYPwuMItr9p6e7hD0+CD0EKNF0HfuY4BpLLA==','2017-02-24 13:36:55','2017-02-24 13:36:55'),(5,'Michael','Jodan','mjordan@fake.com','AQAAAAEAACcQAAAAEP/v/0fo6NWKhGQSJgiGqqNCbOZY5hn1MfC71+ZHovd/T6LlpLE6q0wli0T5xLCsNQ==','2017-02-26 18:38:10','2017-02-26 18:38:10');
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-03-21 13:14:30
