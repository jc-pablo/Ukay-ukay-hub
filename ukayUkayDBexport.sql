-- MySQL dump 10.13  Distrib 8.0.45, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: ukayukay_db
-- ------------------------------------------------------
-- Server version	8.0.45

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `categories`
--

DROP TABLE IF EXISTS `categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categories` (
  `category_id` int NOT NULL AUTO_INCREMENT,
  `category_name` varchar(100) NOT NULL,
  `date_added` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`category_id`),
  UNIQUE KEY `category_name` (`category_name`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categories`
--

LOCK TABLES `categories` WRITE;
/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories` VALUES (1,'TOP','2026-07-05 11:17:47'),(2,'BOTTOM','2026-07-05 11:17:55'),(3,'SHOES','2026-07-11 06:48:39'),(4,'Outerwear','2026-07-12 15:45:01'),(5,'Dresses','2026-07-12 15:45:01'),(6,'Bags','2026-07-12 15:45:01'),(7,'Headwear','2026-07-12 15:45:01');
/*!40000 ALTER TABLE `categories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `consignors`
--

DROP TABLE IF EXISTS `consignors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `consignors` (
  `consignor_id` int NOT NULL AUTO_INCREMENT,
  `first_name` varchar(100) NOT NULL,
  `last_name` varchar(100) NOT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`consignor_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `consignors`
--

LOCK TABLES `consignors` WRITE;
/*!40000 ALTER TABLE `consignors` DISABLE KEYS */;
INSERT INTO `consignors` VALUES (1,'Ron','Harry D.  Rebanal','09234267867','rondbanal@gmail.com'),(2,'Christian','Paulo Costelo','09342342768','chrisbrown@gmail.com'),(3,'JING','COOP THRIFTS','09342346789','jingjing@gmail.com');
/*!40000 ALTER TABLE `consignors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customers`
--

DROP TABLE IF EXISTS `customers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `customers` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `full_name` varchar(150) NOT NULL,
  `username` varchar(50) NOT NULL,
  `email` varchar(100) NOT NULL,
  `contact_no` varchar(20) DEFAULT NULL,
  `address` text,
  `password` varchar(255) NOT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customers`
--

LOCK TABLES `customers` WRITE;
/*!40000 ALTER TABLE `customers` DISABLE KEYS */;
INSERT INTO `customers` VALUES (1,'JC Pablo','admin','jcpablo@gmail.com','09123456789','Pasig City','admin123'),(2,'John Clarence G. Pablo','jcgpablo','jcpablo@gmail.com','09425243521','Pasig City','jcpogi123');
/*!40000 ALTER TABLE `customers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `donors`
--

DROP TABLE IF EXISTS `donors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `donors` (
  `donor_id` int NOT NULL AUTO_INCREMENT,
  `full_name` varchar(150) NOT NULL,
  `contact_number` varchar(20) DEFAULT NULL,
  `email_address` varchar(100) DEFAULT NULL,
  `address` text,
  `date_registered` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`donor_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `donors`
--

LOCK TABLES `donors` WRITE;
/*!40000 ALTER TABLE `donors` DISABLE KEYS */;
INSERT INTO `donors` VALUES (1,'Eirrich Fave Leonardo','09342342342','EriDB@gmail.com','Lower B','2026-07-05 11:28:55'),(2,'Mark Davis Lloren','09865412347','MarkDB@gmail.com','Triumph','2026-07-05 11:29:36'),(3,'JC THRIFT COMPANY','09212334546','juccomp@gmail.com','Taguig City','2026-07-11 06:50:19');
/*!40000 ALTER TABLE `donors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `inventory`
--

DROP TABLE IF EXISTS `inventory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `inventory` (
  `item_id` int NOT NULL AUTO_INCREMENT,
  `item_name` varchar(100) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `category_id` int DEFAULT NULL,
  `item_condition` varchar(50) DEFAULT NULL,
  `source_type` varchar(50) DEFAULT NULL,
  `price` decimal(10,2) NOT NULL,
  `status` varchar(50) DEFAULT 'Available',
  `donor_id` int DEFAULT NULL,
  `consignor_id` int DEFAULT NULL,
  PRIMARY KEY (`item_id`),
  KEY `category_id` (`category_id`),
  KEY `donor_id` (`donor_id`),
  KEY `consignor_id` (`consignor_id`),
  CONSTRAINT `inventory_category_fk` FOREIGN KEY (`category_id`) REFERENCES `categories` (`category_id`),
  CONSTRAINT `inventory_consignor_fk` FOREIGN KEY (`consignor_id`) REFERENCES `consignors` (`consignor_id`),
  CONSTRAINT `inventory_donor_fk` FOREIGN KEY (`donor_id`) REFERENCES `donors` (`donor_id`)
) ENGINE=InnoDB AUTO_INCREMENT=45 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inventory`
--

LOCK TABLES `inventory` WRITE;
/*!40000 ALTER TABLE `inventory` DISABLE KEYS */;
INSERT INTO `inventory` VALUES (1,'Carhartt','Vintage Tee',1,'Fair','Donated',250.00,'Sold',2,NULL),(2,'TUPAD Clothing','Tupad Longsleeve',1,'Like New','Donated',150.00,'Sold',2,NULL),(3,'Undrafted Mesh','Mesh Short Fire',2,'Like New','Consigned',450.00,'Sold',NULL,1),(4,'Adidas Tee','Like New Release',1,'Like New','Consigned',480.00,'Sold',NULL,2),(5,'Just Do It','Nike Shirt',1,'Brand New','Consigned',360.00,'Sold',NULL,2),(6,'Offset Sweatshorts','SweatShort Trend',2,'Like New','Consigned',550.00,'Sold',NULL,2),(7,'DBTK','DBTK Old Shirt',1,'Good','Donated',220.00,'Sold',1,NULL),(8,'Nkek','Fake Nike Shirt',1,'Good','Donated',80.00,'Sold',1,NULL),(9,'Coziest','Coziest Old Shirt',1,'Like New','Donated',150.00,'Sold',1,NULL),(10,'Jordan Shirt','Green Jordan Logo',1,'Good','Donated',160.00,'Sold',2,NULL),(11,'Air Jordan','Travis Scott High Jordan Original Shoe',3,'Good','Consigned',660.00,'Sold',NULL,3),(12,'Adidas Campus','Black CAMPUS Adidas Brand New',3,'Brand New','Consigned',1220.00,'Available',NULL,3),(13,'Nice','Blue Fake Nike Tee',1,'Good','Donated',50.00,'Available',2,NULL),(14,'Adios','Vintage Gray Tee',1,'Like New','Donated',50.00,'Available',2,NULL),(15,'Jonrad','Fake Air Jordan',3,'Like New','Donated',150.00,'Available',3,NULL),(16,'Ja Morant','Morant Pink New Release OEM',3,'Brand New','Consigned',780.00,'Available',NULL,3),(17,'Vintage Graphic Tee','Color: Faded Black, Size: L',1,'Good','Donation',180.00,'Available',1,NULL),(18,'Oversized Flannel Shirt','Color: Red/Blue Plaid, Size: XL',1,'Excellent','Consignment',250.00,'Available',NULL,1),(19,'Denim Button-Up Shirt','Color: Light Wash Blue, Size: M',1,'Good','Donation',220.00,'Available',1,NULL),(20,'Striped Knit Sweater','Color: Beige/White, Size: S',1,'Like New','Consignment',350.00,'Available',NULL,1),(21,'Basic White Crop Top','Color: Solid White, Size: S',1,'Good','Donation',90.00,'Available',1,NULL),(22,'Y2K Baby Tee','Color: Baby Pink, Size: XS',1,'Excellent','Consignment',150.00,'Available',NULL,1),(23,'High-Waisted Mom Jeans','Color: Dark Wash Blue, Size: 28',2,'Excellent','Donation',380.00,'Available',1,NULL),(24,'Cargo Pants','Color: Olive Green, Size: 32',2,'Good','Consignment',400.00,'Available',NULL,1),(25,'Corduroy Trousers','Color: Chocolate Brown, Size: 30',2,'Excellent','Donation',350.00,'Available',1,NULL),(26,'Pleated Mini Skirt','Color: Gray Tartan, Size: 26',2,'Like New','Consignment',200.00,'Available',NULL,1),(27,'Denim Shorts','Color: Medium Wash, Size: 29',2,'Good','Donation',120.00,'Available',1,NULL),(28,'Baggy Sweatpants','Color: Heather Gray, Size: L',2,'Good','Donation',180.00,'Available',1,NULL),(29,'Retro Canvas Sneakers','Color: Off-White, Size: US 8',3,'Good','Consignment',550.00,'Available',NULL,1),(30,'Chunky Leather Boots','Color: Dark Brown, Size: US 7',3,'Excellent','Consignment',950.00,'Available',NULL,1),(31,'Sporty Running Shoes','Color: Gray/Neon Green, Size: US 10',3,'Good','Donation',600.00,'Available',1,NULL),(32,'Leather Bomber Jacket','Color: Black, Size: L',4,'Good','Consignment',850.00,'Available',NULL,1),(33,'Vintage Windbreaker','Color: Neon Purple/Teal, Size: M',4,'Excellent','Donation',450.00,'Available',1,NULL),(34,'Zip-Up Hoodie','Color: Navy Blue, Size: XL',4,'Good','Donation',280.00,'Available',1,NULL),(35,'Denim Jacket with Sherpa','Color: Blue, Size: M',4,'Like New','Consignment',600.00,'Available',NULL,1),(36,'Knitted Cardigan','Color: Mustard Yellow, Size: Free Size',4,'Excellent','Donation',240.00,'Available',1,NULL),(37,'Floral Sundress','Color: Yellow Floral, Size: M',5,'Excellent','Donation',280.00,'Available',1,NULL),(38,'Little Black Dress','Color: Jet Black, Size: S',5,'Like New','Consignment',320.00,'Available',NULL,1),(39,'Maxi Linen Dress','Color: Oatmeal Cream, Size: L',5,'Excellent','Donation',420.00,'Available',1,NULL),(40,'Leather Crossbody Bag','Color: Tan Brown, Size: Medium',6,'Good','Donation',300.00,'Available',1,NULL),(41,'Canvas Tote Bag','Color: Beige with Print, Size: Large',6,'Like New','Donation',100.00,'Available',1,NULL),(42,'Vintage Backpack','Color: Forest Green, Size: Standard',6,'Good','Consignment',450.00,'Available',NULL,1),(43,'Beanie Hat','Color: Charcoal Gray, Size: One Size',6,'Like New','Donation',80.00,'Available',1,NULL),(44,'Woven Straw Hat','Color: Natural Straw, Size: Medium',6,'Excellent','Donation',150.00,'Available',1,NULL);
/*!40000 ALTER TABLE `inventory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payouts`
--

DROP TABLE IF EXISTS `payouts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `payouts` (
  `payout_id` int NOT NULL AUTO_INCREMENT,
  `consignor_id` int DEFAULT NULL,
  `payout_period` varchar(100) NOT NULL,
  `date_saved` date NOT NULL,
  PRIMARY KEY (`payout_id`),
  KEY `consignor_id` (`consignor_id`),
  CONSTRAINT `fk_payouts_consignors` FOREIGN KEY (`consignor_id`) REFERENCES `consignors` (`consignor_id`)
) ENGINE=InnoDB AUTO_INCREMENT=711022239 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payouts`
--

LOCK TABLES `payouts` WRITE;
/*!40000 ALTER TABLE `payouts` DISABLE KEYS */;
INSERT INTO `payouts` VALUES (711020238,1,'07/02/2026 - 07/11/2026','2026-07-11'),(711022238,2,'07/01/2026 - 07/11/2026','2026-07-11');
/*!40000 ALTER TABLE `payouts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `transactions`
--

DROP TABLE IF EXISTS `transactions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `transactions` (
  `transaction_id` int NOT NULL AUTO_INCREMENT,
  `item_id` int DEFAULT NULL,
  `selling_price` decimal(10,2) NOT NULL,
  `sale_date` date NOT NULL,
  `payout_status` varchar(20) NOT NULL DEFAULT 'Unpaid',
  PRIMARY KEY (`transaction_id`),
  KEY `item_id` (`item_id`),
  CONSTRAINT `fk_transactions_inventory` FOREIGN KEY (`item_id`) REFERENCES `inventory` (`item_id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transactions`
--

LOCK TABLES `transactions` WRITE;
/*!40000 ALTER TABLE `transactions` DISABLE KEYS */;
INSERT INTO `transactions` VALUES (1,1,250.00,'2026-07-06','Unpaid'),(2,2,150.00,'2026-07-06','Unpaid'),(3,3,450.00,'2026-07-06','Paid'),(4,6,550.00,'2026-07-07','Paid'),(5,9,150.00,'2026-07-11','Unpaid'),(6,11,660.00,'2026-07-11','Unpaid'),(7,10,160.00,'2026-07-11','Unpaid'),(8,7,220.00,'2026-07-12','Unpaid'),(9,8,80.00,'2026-07-12','Unpaid'),(10,5,360.00,'2026-07-12','Unpaid'),(11,4,480.00,'2026-07-12','Unpaid');
/*!40000 ALTER TABLE `transactions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL,
  `password` varchar(255) NOT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'admin','admin123');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-07-13  0:44:48
