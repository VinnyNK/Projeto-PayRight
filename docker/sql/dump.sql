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

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE DATABASE IF NOT EXISTS `PayRight-Extrato` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `PayRight-Extrato`;

CREATE TABLE IF NOT EXISTS `atividades` (
  `Id` char(36) CHARACTER SET ascii NOT NULL,
  `ExtratoId` char(36) CHARACTER SET ascii DEFAULT NULL,
  `NomeAtividade_Nome` varchar(32) NOT NULL,
  `NomeAtividade_Descricao` varchar(128) NOT NULL,
  `Valor` decimal(13,2) NOT NULL DEFAULT 0.00,
  `TipoAtividade` int(11) NOT NULL,
  `Pago` tinyint(1) NOT NULL,
  `CriadoEm` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  `DataPagamento` date DEFAULT NULL,
  `EstimativaPagamento` date NOT NULL DEFAULT '0001-01-01',
  PRIMARY KEY (`Id`),
  KEY `IX_atividades_ExtratoId` (`ExtratoId`),
  KEY `IX_atividades_Id` (`Id`),
  CONSTRAINT `FK_atividades_extratos_contas_corrente_ExtratoId` FOREIGN KEY (`ExtratoId`) REFERENCES `extratos_contas_corrente` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `extratos_contas_corrente` (
  `Id` char(36) CHARACTER SET ascii NOT NULL,
  `ContaCorrenteId` char(36) CHARACTER SET ascii NOT NULL,
  `CriadoEm` datetime(6) NOT NULL DEFAULT current_timestamp(6),
  `UsuarioId` char(36) CHARACTER SET ascii NOT NULL,
  `mes` int(11) NOT NULL,
  `ano` int(11) NOT NULL,
  `Total` decimal(13,2) NOT NULL DEFAULT 0.00,
  `TotalEstimado` decimal(13,2) NOT NULL DEFAULT 0.00,
  PRIMARY KEY (`Id`),
  KEY `idx_conta_corrente_id` (`ContaCorrenteId`),
  KEY `idx_usuario_id` (`UsuarioId`),
  KEY `IX_extratos_contas_corrente_Id` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
