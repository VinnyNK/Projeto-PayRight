/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE DATABASE IF NOT EXISTS `PayRight-Cadastro` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `PayRight-Cadastro`;

CREATE TABLE IF NOT EXISTS `usuarios` (
  `Id` char(36) CHARACTER SET ascii NOT NULL,
  `primeiro_nome` varchar(64) NOT NULL,
  `sobrenome` varchar(64) NOT NULL,
  `email` varchar(80) NOT NULL,
  `documento` varchar(14) NOT NULL,
  `tipo_documento` int(11) NOT NULL,
  `Senha` longtext NOT NULL,
  `Ativo` tinyint(1) NOT NULL DEFAULT 1,
  `UltimaAtualizacaoEm` datetime(6) NOT NULL,
  `CriadoEm` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  PRIMARY KEY (`Id`),
  UNIQUE KEY `idx_email` (`email`),
  UNIQUE KEY `idx_numero_documento` (`documento`),
  KEY `IX_usuarios_Id` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


CREATE DATABASE IF NOT EXISTS `PayRight-Conta` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `PayRight-Conta`;

CREATE TABLE IF NOT EXISTS `contas_correntes` (
  `Id` char(36) CHARACTER SET ascii NOT NULL,
  `Saldo` decimal(13,2) NOT NULL DEFAULT 0.00,
  `CriadoEm` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  `UsuarioId` char(36) CHARACTER SET ascii NOT NULL,
  `nome` varchar(24) NOT NULL,
  `apelido` varchar(15) DEFAULT NULL,
  `ultima_alteracao` datetime(6) NOT NULL,
  `Ativo` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`Id`),
  KEY `idx_usuario_id` (`UsuarioId`),
  KEY `IX_contas_correntes_Id` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
