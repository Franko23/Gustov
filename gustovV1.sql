-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         10.4.21-MariaDB - mariadb.org binary distribution
-- SO del servidor:              Win64
-- HeidiSQL Versión:             11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Volcando estructura de base de datos para gustov
CREATE DATABASE IF NOT EXISTS `gustov` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `gustov`;

-- Volcando estructura para tabla gustov.platos
CREATE TABLE IF NOT EXISTS `platos` (
  `id_plato` int(11) NOT NULL AUTO_INCREMENT,
  `nombre_plato` varchar(50) DEFAULT NULL,
  `cantidad_plato` int(11) DEFAULT NULL,
  `precio_unitario` decimal(20,2) DEFAULT NULL,
  PRIMARY KEY (`id_plato`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4;

-- Volcando datos para la tabla gustov.platos: ~4 rows (aproximadamente)
DELETE FROM `platos`;
/*!40000 ALTER TABLE `platos` DISABLE KEYS */;
INSERT INTO `platos` (`id_plato`, `nombre_plato`, `cantidad_plato`, `precio_unitario`) VALUES
	(1, 'Picante de Pollo', 13, 12.50),
	(2, 'Charque', 10, 10.00),
	(3, 'Pique', 0, 10.00),
	(4, 'Pailita', 12, 18.50);
/*!40000 ALTER TABLE `platos` ENABLE KEYS */;

-- Volcando estructura para tabla gustov.registro_ventas
CREATE TABLE IF NOT EXISTS `registro_ventas` (
  `id_ventas` int(11) NOT NULL AUTO_INCREMENT,
  `id_plato` int(11) NOT NULL DEFAULT 0,
  `nombre_plato` varchar(50) DEFAULT NULL,
  `cantidad_plato` int(11) DEFAULT NULL,
  `precio_actual` decimal(20,2) DEFAULT NULL,
  `fecha_venta` date DEFAULT NULL,
  `descripcion_venta` text DEFAULT NULL,
  PRIMARY KEY (`id_ventas`),
  KEY `id_plato` (`id_plato`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COMMENT='Registro de las ventas de los platos del menu';

-- Volcando datos para la tabla gustov.registro_ventas: ~0 rows (aproximadamente)
DELETE FROM `registro_ventas`;
/*!40000 ALTER TABLE `registro_ventas` DISABLE KEYS */;
INSERT INTO `registro_ventas` (`id_ventas`, `id_plato`, `nombre_plato`, `cantidad_plato`, `precio_actual`, `fecha_venta`, `descripcion_venta`) VALUES
	(1, 1, 'Picante de Pollo', 2, 12.50, '2022-02-04', 'Venta para llevar'),
	(2, 2, 'Charque', 10, 10.00, '2022-02-04', 'Se vendió a un grupo de la iglesia'),
	(3, 3, 'Pique', 30, 10.00, '2022-02-04', 'Se vendió a una delegación'),
	(4, 4, 'Pailita', 13, 18.50, '2022-02-04', 'Se vendió al evento de Caleb');
/*!40000 ALTER TABLE `registro_ventas` ENABLE KEYS */;

-- Volcando estructura para procedimiento gustov.reporte_diario
DELIMITER //
CREATE PROCEDURE `reporte_diario`(
	IN `fecha` DATE
)
BEGIN


SELECT 
	v.fecha_venta,
	v.nombre_plato,
	v.cantidad_plato,
	v.precio_actual,
	v.descripcion_venta,
@total:=(v.cantidad_plato * v.precio_actual) AS Total_pagado
 FROM registro_ventas v
 LEFT JOIN platos p ON v.id_plato = p.id_plato
 
	WHERE v.fecha_venta = fecha;

END//
DELIMITER ;

-- Volcando estructura para disparador gustov.registro_ventas_after_insert
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_ZERO_IN_DATE,NO_ZERO_DATE,NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER `registro_ventas_after_insert` AFTER INSERT ON `registro_ventas` FOR EACH ROW BEGIN
UPDATE platos p SET p.cantidad_plato = p.cantidad_plato - NEW.cantidad_plato WHERE p.id_plato = NEW.id_plato;
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
