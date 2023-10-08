-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 07/10/2023 às 22:16
-- Versão do servidor: 10.4.27-MariaDB
-- Versão do PHP: 7.4.33

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `sep_db`
--

-- --------------------------------------------------------

--
-- Estrutura para tabela `administrators`
--

CREATE TABLE `administrators` (
  `Id` int(11) NOT NULL,
  `Masp` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Login` varchar(35) NOT NULL,
  `Password` varchar(150) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Phone` varchar(15) NOT NULL,
  `Position` varchar(35) NOT NULL,
  `UserType` int(11) NOT NULL,
  `RegisterDate` datetime(6) NOT NULL,
  `ModifyDate` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura para tabela `divisions`
--

CREATE TABLE `divisions` (
  `Id` int(11) NOT NULL,
  `Name` varchar(60) NOT NULL,
  `RegisterDate` datetime(6) NOT NULL,
  `ModifyDate` datetime(6) DEFAULT NULL,
  `InstituitionId` int(11) NOT NULL,
  `UserAdministratorId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura para tabela `evaluators`
--

CREATE TABLE `evaluators` (
  `Id` int(11) NOT NULL,
  `Masp` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Login` varchar(35) NOT NULL,
  `Password` varchar(150) NOT NULL,
  `Email` longtext NOT NULL,
  `Phone` longtext NOT NULL,
  `Position` varchar(35) NOT NULL,
  `UserType` int(11) NOT NULL,
  `RegisterDate` datetime(6) NOT NULL,
  `ModifyDate` datetime(6) DEFAULT NULL,
  `UserAdministratorId` int(11) NOT NULL,
  `InstituitionId` int(11) NOT NULL,
  `DivisionId` int(11) NOT NULL,
  `SectionId` int(11) NOT NULL,
  `SectorId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura para tabela `instituitions`
--

CREATE TABLE `instituitions` (
  `Id` int(11) NOT NULL,
  `Name` varchar(60) NOT NULL,
  `RegisterDate` datetime(6) NOT NULL,
  `ModifyDate` datetime(6) DEFAULT NULL,
  `UserAdministratorId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura para tabela `sections`
--

CREATE TABLE `sections` (
  `Id` int(11) NOT NULL,
  `Name` varchar(60) NOT NULL,
  `RegisterDate` datetime(6) NOT NULL,
  `ModifyDate` datetime(6) DEFAULT NULL,
  `DivisionId` int(11) NOT NULL,
  `UserAdministratorId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura para tabela `sectors`
--

CREATE TABLE `sectors` (
  `Id` int(11) NOT NULL,
  `Name` varchar(60) NOT NULL,
  `RegisterDate` datetime(6) NOT NULL,
  `ModifyDate` datetime(6) DEFAULT NULL,
  `SectionId` int(11) NOT NULL,
  `UserAdministratorId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura para tabela `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20231007201553_SEP_CONTEXT', '6.0.0');

--
-- Índices para tabelas despejadas
--

--
-- Índices de tabela `administrators`
--
ALTER TABLE `administrators`
  ADD PRIMARY KEY (`Id`);

--
-- Índices de tabela `divisions`
--
ALTER TABLE `divisions`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Divisions_InstituitionId` (`InstituitionId`),
  ADD KEY `IX_Divisions_UserAdministratorId` (`UserAdministratorId`);

--
-- Índices de tabela `evaluators`
--
ALTER TABLE `evaluators`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Evaluators_DivisionId` (`DivisionId`),
  ADD KEY `IX_Evaluators_InstituitionId` (`InstituitionId`),
  ADD KEY `IX_Evaluators_SectionId` (`SectionId`),
  ADD KEY `IX_Evaluators_SectorId` (`SectorId`),
  ADD KEY `IX_Evaluators_UserAdministratorId` (`UserAdministratorId`);

--
-- Índices de tabela `instituitions`
--
ALTER TABLE `instituitions`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Instituitions_UserAdministratorId` (`UserAdministratorId`);

--
-- Índices de tabela `sections`
--
ALTER TABLE `sections`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Sections_DivisionId` (`DivisionId`),
  ADD KEY `IX_Sections_UserAdministratorId` (`UserAdministratorId`);

--
-- Índices de tabela `sectors`
--
ALTER TABLE `sectors`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Sectors_SectionId` (`SectionId`),
  ADD KEY `IX_Sectors_UserAdministratorId` (`UserAdministratorId`);

--
-- Índices de tabela `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT para tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `administrators`
--
ALTER TABLE `administrators`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `divisions`
--
ALTER TABLE `divisions`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `evaluators`
--
ALTER TABLE `evaluators`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `instituitions`
--
ALTER TABLE `instituitions`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `sections`
--
ALTER TABLE `sections`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `sectors`
--
ALTER TABLE `sectors`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Restrições para tabelas despejadas
--

--
-- Restrições para tabelas `divisions`
--
ALTER TABLE `divisions`
  ADD CONSTRAINT `FK_Divisions_Administrators_UserAdministratorId` FOREIGN KEY (`UserAdministratorId`) REFERENCES `administrators` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Divisions_Instituitions_InstituitionId` FOREIGN KEY (`InstituitionId`) REFERENCES `instituitions` (`Id`) ON DELETE CASCADE;

--
-- Restrições para tabelas `evaluators`
--
ALTER TABLE `evaluators`
  ADD CONSTRAINT `FK_Evaluators_Administrators_UserAdministratorId` FOREIGN KEY (`UserAdministratorId`) REFERENCES `administrators` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Evaluators_Divisions_DivisionId` FOREIGN KEY (`DivisionId`) REFERENCES `divisions` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Evaluators_Instituitions_InstituitionId` FOREIGN KEY (`InstituitionId`) REFERENCES `instituitions` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Evaluators_Sections_SectionId` FOREIGN KEY (`SectionId`) REFERENCES `sections` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Evaluators_Sectors_SectorId` FOREIGN KEY (`SectorId`) REFERENCES `sectors` (`Id`) ON DELETE CASCADE;

--
-- Restrições para tabelas `instituitions`
--
ALTER TABLE `instituitions`
  ADD CONSTRAINT `FK_Instituitions_Administrators_UserAdministratorId` FOREIGN KEY (`UserAdministratorId`) REFERENCES `administrators` (`Id`) ON DELETE CASCADE;

--
-- Restrições para tabelas `sections`
--
ALTER TABLE `sections`
  ADD CONSTRAINT `FK_Sections_Administrators_UserAdministratorId` FOREIGN KEY (`UserAdministratorId`) REFERENCES `administrators` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Sections_Divisions_DivisionId` FOREIGN KEY (`DivisionId`) REFERENCES `divisions` (`Id`) ON DELETE CASCADE;

--
-- Restrições para tabelas `sectors`
--
ALTER TABLE `sectors`
  ADD CONSTRAINT `FK_Sectors_Administrators_UserAdministratorId` FOREIGN KEY (`UserAdministratorId`) REFERENCES `administrators` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Sectors_Sections_SectionId` FOREIGN KEY (`SectionId`) REFERENCES `sections` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
