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
DROP DATABASE IF EXISTS `gustov`;
CREATE DATABASE IF NOT EXISTS `gustov` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `gustov`;

-- Volcando estructura para tabla gustov.platos
DROP TABLE IF EXISTS `platos`;
CREATE TABLE IF NOT EXISTS `platos` (
  `id_plato` int(11) NOT NULL AUTO_INCREMENT,
  `nombre_plato` varchar(50) DEFAULT NULL,
  `cantidad_plato` int(11) DEFAULT NULL,
  `precio_unitario` decimal(20,2) DEFAULT NULL,
  PRIMARY KEY (`id_plato`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4;

-- Volcando datos para la tabla gustov.platos: ~5 rows (aproximadamente)
DELETE FROM `platos`;
/*!40000 ALTER TABLE `platos` DISABLE KEYS */;
INSERT INTO `platos` (`id_plato`, `nombre_plato`, `cantidad_plato`, `precio_unitario`) VALUES
	(10, 'Picante de Pollo', 52, 20.50),
	(15, 'Silpancho', 20, 10.50),
	(16, 'Sopa de maní', 90, 11.20),
	(17, 'Arroz con pollo', 200, 30.50),
	(18, 'Nuevo Plato', 2, 20.50);
/*!40000 ALTER TABLE `platos` ENABLE KEYS */;

-- Volcando estructura para tabla gustov.registro_ventas
DROP TABLE IF EXISTS `registro_ventas`;
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
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COMMENT='Registro de las ventas de los platos del menu';

-- Volcando datos para la tabla gustov.registro_ventas: ~12 rows (aproximadamente)
DELETE FROM `registro_ventas`;
/*!40000 ALTER TABLE `registro_ventas` DISABLE KEYS */;
INSERT INTO `registro_ventas` (`id_ventas`, `id_plato`, `nombre_plato`, `cantidad_plato`, `precio_actual`, `fecha_venta`, `descripcion_venta`) VALUES
	(1, 0, NULL, NULL, NULL, NULL, NULL),
	(2, 10, NULL, 2, NULL, NULL, NULL),
	(3, 15, NULL, 2, NULL, NULL, NULL),
	(4, 14, NULL, 3, NULL, NULL, NULL),
	(5, 15, NULL, 1, NULL, NULL, NULL),
	(6, 14, NULL, 2, NULL, NULL, NULL),
	(7, 15, NULL, 2, NULL, NULL, NULL),
	(8, 14, NULL, 4, NULL, NULL, NULL),
	(9, 14, NULL, 1, NULL, NULL, NULL),
	(10, 15, NULL, 6, NULL, NULL, NULL),
	(11, 16, NULL, 10, NULL, NULL, NULL),
	(12, 10, 'Picante de Pollo', 5, 20.50, '2022-02-02', 'Example'),
	(13, 18, 'Nuevo Plato', 4, 20.50, '2022-02-02', 'Nueva venta'),
	(14, 15, 'Silpancho', 5, 10.50, '2022-02-03', 'Se vendió a la UAB'),
	(15, 16, 'Sopa de maní', 23, 11.20, '2022-02-03', 'Se vendió a la empresa de transportes Petito'),
	(16, 17, 'Arroz con pollo', 13, 30.50, '2022-02-03', 'Se vendió 10 al contado y queda 3 por pagar. Se vendió a la empresa de radio taxis Vinteño.'),
	(17, 10, 'Picante de Pollo', 9, 20.50, '2022-02-03', 'Estaba muy bueno!');
/*!40000 ALTER TABLE `registro_ventas` ENABLE KEYS */;

-- Volcando estructura para procedimiento gustov.reporte_diario
DROP PROCEDURE IF EXISTS `reporte_diario`;
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

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
