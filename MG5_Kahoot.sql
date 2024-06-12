-- MySQL dump 10.13  Distrib 5.7.17, for Linux (x86_64)
--
-- Host: localhost    Database: MG5_Kahoot
-- ------------------------------------------------------
-- Server version	5.7.17-0ubuntu0.16.04.1

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
-- Table structure for table `jugadores`
--

DROP TABLE IF EXISTS `jugadores`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `jugadores` (
  `ID_usuario` int(11) NOT NULL AUTO_INCREMENT,
  `nombre_usuario` varchar(255) NOT NULL,
  `numero_puntos_totales` int(11) DEFAULT '0',
  `numero_partidas_jugadas` int(11) DEFAULT '0',
  PRIMARY KEY (`ID_usuario`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `jugadores`
--

LOCK TABLES `jugadores` WRITE;
/*!40000 ALTER TABLE `jugadores` DISABLE KEYS */;
INSERT INTO `jugadores` VALUES (1,'Jugador1',150,10),(2,'Jugador2',200,20),(3,'Jugador3',50,5);
/*!40000 ALTER TABLE `jugadores` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `partida`
--

DROP TABLE IF EXISTS `partida`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `partida` (
  `ID_partida` int(11) NOT NULL AUTO_INCREMENT,
  `ID_usuario` int(11) NOT NULL,
  `puesto` int(11) DEFAULT NULL,
  `puntos` int(11) DEFAULT '0',
  `numero_preguntas_correctas` int(11) DEFAULT '0',
  `numero_preguntas_incorrectas` int(11) DEFAULT '0',
  PRIMARY KEY (`ID_partida`),
  KEY `ID_usuario` (`ID_usuario`),
  CONSTRAINT `partida_ibfk_1` FOREIGN KEY (`ID_usuario`) REFERENCES `jugadores` (`ID_usuario`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `partida`
--

LOCK TABLES `partida` WRITE;
/*!40000 ALTER TABLE `partida` DISABLE KEYS */;
INSERT INTO `partida` VALUES (1,1,1,100,5,0),(2,2,2,50,2,3),(3,3,3,25,1,4);
/*!40000 ALTER TABLE `partida` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `partida_jugada`
--

DROP TABLE IF EXISTS `partida_jugada`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `partida_jugada` (
  `Id_partida` int(11) NOT NULL AUTO_INCREMENT,
  `nombre_jugadores` varchar(100) DEFAULT NULL,
  `puntos` varchar(100) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  PRIMARY KEY (`Id_partida`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `partida_jugada`
--

LOCK TABLES `partida_jugada` WRITE;
/*!40000 ALTER TABLE `partida_jugada` DISABLE KEYS */;
INSERT INTO `partida_jugada` VALUES (1,'v&victor&leo&mo','1000&2000&1500&3000',NULL),(2,'rafa&victor','2000&1500',NULL),(3,'rafa&v&leo','100&2000&3000','2024-06-08 00:00:00'),(4,'rafa&v&moha','100&200&3000','2024-06-06 00:00:00'),(5,'rafa&v&moha','100&200&3000','2024-06-09 00:00:00');
/*!40000 ALTER TABLE `partida_jugada` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `preguntas`
--

DROP TABLE IF EXISTS `preguntas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `preguntas` (
  `ID_pregunta` int(11) NOT NULL AUTO_INCREMENT,
  `pregunta` text NOT NULL,
  `respuesta_correcta` text NOT NULL,
  `respuesta_incorrecta_1` text NOT NULL,
  `respuesta_incorrecta_2` text NOT NULL,
  `respuesta_incorrecta_3` text NOT NULL,
  PRIMARY KEY (`ID_pregunta`)
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `preguntas`
--

LOCK TABLES `preguntas` WRITE;
/*!40000 ALTER TABLE `preguntas` DISABLE KEYS */;
INSERT INTO `preguntas` VALUES (1,'¿Cuál es la capital de Francia?','París','Berlín','Marsella','Lyon'),(2,'¿Cuánto es 2 + 2?','4','5','3','8'),(3,'¿En qué continente está Ecuador?','América','Europa','Asia','África'),(4,'¿Cuales el pais mas poblado del mundo?','China','India','Rusia','Estados Unidos'),(5,'¿Cuantas patas tiene una araña?','8','6','4','10'),(6,'¿Cada cuantos años tenemos un año bisiesto?','4','2','1','5'),(7,'¿Cuantas silabas tiene la palabra abecedario?','5','4','6','3'),(8,'¿Cual es el planeta que esta mas cerca del sol?','Mercurio','Venus','Saturno','Marte'),(9,'¿Por donde se pone el sol?','Oeste','Este','Norte','Sud'),(10,'¿Que selección de fútbol ha ganado mas mundiales?','Brasil','España','Argentina','Alemania'),(11,'¿Cuantas Champions League tiene el Atletico de Madrid?','0','1','2','5'),(12,'¿Quién descubrió América?','Colón','FelipeII','Juan Carlos','Victor'),(13,'¿Cuál es múltiplo de 21?','3','5','8','2'),(14,'¿Dónde desemboca el Ebro?','Tarragona','Valencia','Murcia','Girona'),(15,'¿Dónde desemboca el Guadalquivir?','Cádiz','Castelldefels','Murcia','Tarragona'),(16,'¿Dónde desemboca el Tajo?','Lisboa','Valencia','Alicante','Barcelona'),(17,'¿Cuántas CCAA hay en España?','17','19','15','20'),(18,'¿Cuántos países hay en el mundo?','194','190','200','198'),(19,'¿Quién escribió \"Cien años de soledad\"?','Gabriel García Márquez','Mario Vargas Llosa','Jorge Luis Borges','Isabel Allende'),(20,'¿ ¿Cuál es el río más largo del mundo??','Amazona','Nilo','Yangsté','Misisipi'),(21,'¿Cuál es el océano más grande del mundo?','Océano Pacífico','Océano Atlántico','Océano Índico','Océano Ártico'),(22,'¿Quién pintó la Mona Lisa?','Leonardo da Vinci','Vincent van Gogh','Pablo Picasso','Claude Monet'),(23,'¿Cuál es la capital de Japón?','Tokio','Osaka','Kioto','Hiroshima'),(24,'¿Cuál es la moneda oficial de Japón?','Yen','Won','Yuan','Peso'),(25,'¿En qué continente se encuentra Egipto?','África','Asia','Europa','América'),(26,'¿En qué país se originaron los Juegos Olímpicos?','Grecia','Italia','China','Egipto'),(27,'¿Quién escribió Hamlet?','William Shakespeare',' Charles Dickens','Oscar Wilde','Mark Twain'),(28,'¿Cuál es la fórmula química del agua?','H2O','CO2','H2O2','O2'),(29,'¿Qué tipo de animal es un tiburón?','Pez',' Mamífero','Reptil','Anfibio'),(30,'¿Qué gas es esencial para la fotosíntesis?','Dióxido de carbono','Oxígeno','Nitrógeno','Helio'),(31,'¿Cuál es la distancia media de la Tierra al sol?','150 millones de kilómetros','93 millones de kilómetros','400 millones de kilómetros','250 millones de kilómetros'),(32,'¿Qué órgano del cuerpo humano produce insulina?','Páncreas','Hígado','Riñón','Corazón'),(33,'¿Qué unidad se utiliza para medir la intensidad de corriente eléctrica?','Amperio','Voltio','Ohmio','Vatio'),(34,'¿Qué tipo de onda es la luz?','Electromagnética','Mecánica','Sonoro','Gravitacional'),(35,'¿Cuál es el órgano más grande del cuerpo humano?','Piel','Hígado','Riñón','Corazón'),(36,'¿Qué proceso celular produce dos células hijas idénticas?','Mitosis','Meiosis','Fusión','Fotosíntesis'),(37,'¿Qué tipo de célula no tiene núcleo?','Procariota','Eucariota','Neurona','Glóbulo rojo');
/*!40000 ALTER TABLE `preguntas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ronda`
--

DROP TABLE IF EXISTS `ronda`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ronda` (
  `ID_ronda` int(11) NOT NULL AUTO_INCREMENT,
  `ID_usuario` int(11) NOT NULL,
  `ID_pregunta` int(11) NOT NULL,
  `puntos` int(11) DEFAULT '0',
  `ID_partida` int(11) NOT NULL,
  PRIMARY KEY (`ID_ronda`),
  KEY `ID_usuario` (`ID_usuario`),
  KEY `ID_pregunta` (`ID_pregunta`),
  KEY `ID_partida` (`ID_partida`),
  CONSTRAINT `ronda_ibfk_1` FOREIGN KEY (`ID_usuario`) REFERENCES `jugadores` (`ID_usuario`),
  CONSTRAINT `ronda_ibfk_2` FOREIGN KEY (`ID_pregunta`) REFERENCES `preguntas` (`ID_pregunta`),
  CONSTRAINT `ronda_ibfk_3` FOREIGN KEY (`ID_partida`) REFERENCES `partida` (`ID_partida`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ronda`
--

LOCK TABLES `ronda` WRITE;
/*!40000 ALTER TABLE `ronda` DISABLE KEYS */;
INSERT INTO `ronda` VALUES (1,1,1,10,1),(2,1,2,20,1),(3,2,1,0,2),(4,2,3,50,2),(5,3,2,25,3);
/*!40000 ALTER TABLE `ronda` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usuarios` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `usuario` varchar(50) NOT NULL,
  `contrasena` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (1,'MOHA','MOHA'),(2,'MO','MO');
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-06-10 11:22:29
