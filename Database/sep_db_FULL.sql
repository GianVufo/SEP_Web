-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 07/10/2023 às 22:46
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

--
-- Despejando dados para a tabela `administrators`
--

INSERT INTO `administrators` (`Id`, `Masp`, `Name`, `Login`, `Password`, `Email`, `Phone`, `Position`, `UserType`, `RegisterDate`, `ModifyDate`) VALUES
(1, 14205, 'Gianluca Vialli Ribeiro Vieira da Silva Gonçalves', 'gianluca.vialli', '$2a$11$xuKD7jOEbn78QYKqIio4DuyzAOJid2MuO9x1Wt5DoExKjEDRqGa7C', 'gianluca19993m@gmail.com', '(38) 98808-7655', 'Técnico em Informática', 1, '2023-09-24 16:12:35.870940', NULL),
(2, 28405, 'Anna Caroline Ribeiro Vieira', 'anna.ribeiro', '$2a$11$DDH7ntuZ.SSA4x6ik5jg7.G1kLW08V5nSjPwtGGi/M4vwwxYOlU0C', 'anna@gmail.com', '(38) 98473-2120', 'Secretária', 1, '2023-09-24 16:14:02.332280', NULL),
(3, 40852, 'Amilton Vieira de Souza', 'amilton.vieira', '$2a$11$DzOQwmCK/5gmDeqJPoYEMOuYdkJEeGub94KjVNjOvIsv4TPO/tyUa', 'amilton@gmail.com', '(38) 98426-3654', 'Servente Escolar', 1, '2023-09-24 16:16:16.872747', NULL),
(4, 27406, 'André gomes Soares', 'andre.soares', '$2a$11$OXHXVrdczPq7ieBZr5rTZOcRdOKs6Yvs/xJLiu8m0xZ6LF.kqvKY.', 'andre@gmail.com', '(38) 95864-1006', 'Técnico em Informática', 1, '2023-10-07 17:35:51.175619', NULL);

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

--
-- Despejando dados para a tabela `divisions`
--

INSERT INTO `divisions` (`Id`, `Name`, `RegisterDate`, `ModifyDate`, `InstituitionId`, `UserAdministratorId`) VALUES
(1, 'Escola Municipal Antônio Fonseca Leal', '2023-09-27 13:39:48.921364', NULL, 11, 3),
(2, 'Escola Municipal Memorial Zumbi', '2023-09-27 13:52:41.191752', NULL, 11, 3),
(3, 'Sítio da Criança', '2023-09-27 13:55:16.883640', NULL, 10, 3),
(4, 'Centro de Assistência Psico-Social', '2023-09-27 13:56:16.607620', NULL, 7, 3),
(5, 'Centro de Educação Inf. Lucilênia A. O. Sil', '2023-09-27 13:57:15.912343', NULL, 11, 3),
(6, 'Creche Cantinho da Criança', '2023-09-27 13:58:06.465636', NULL, 11, 3),
(7, 'Creche Lar da Criança', '2023-09-27 13:58:38.047980', NULL, 11, 3),
(8, 'Creche Pequeno Polegar', '2023-09-27 13:59:02.961080', NULL, 11, 3),
(9, 'Divisão Ambulatorial', '2023-09-27 13:59:22.751990', NULL, 7, 3),
(10, 'Divisão Coord. do P. Saúde da Família - PSF', '2023-09-27 14:00:26.799950', NULL, 7, 3),
(11, 'Divisão de Administração', '2023-09-27 14:01:10.369448', NULL, 11, 3),
(12, 'Divisão de Administração financeira', '2023-09-27 14:03:58.597844', NULL, 6, 3),
(13, 'Divisão de Apoio Administrativo', '2023-09-27 14:04:33.725006', NULL, 8, 3),
(14, 'Divisão de Apoio Jurídico', '2023-09-27 14:05:35.479189', NULL, 4, 3),
(15, 'Divisão de Cadastro Imobiliário', '2023-09-27 14:06:17.198766', NULL, 6, 3),
(16, 'Divisão de Comunicação Social', '2023-09-27 14:07:16.495161', NULL, 14, 3),
(17, 'Divisão de Controle Interno', '2023-09-27 14:07:39.552575', NULL, 6, 3),
(18, 'Divisão de Cultura', '2023-09-27 14:08:29.881150', NULL, 11, 3),
(19, 'Divisão de Desenvolvimento Comunitário', '2023-09-27 14:08:58.129685', NULL, 10, 3),
(20, 'Divisão de Ensino', '2023-09-27 14:10:01.754540', NULL, 11, 3),
(21, 'Divisão de Esportes e Lazer', '2023-09-27 14:18:24.539964', NULL, 5, 1),
(22, 'Divisão de Execução Orçamentária', '2023-09-27 14:19:44.021563', NULL, 6, 1),
(23, 'Divisão de Fomento Comercial e Industrial', '2023-09-27 14:20:19.862295', NULL, 5, 1),
(24, 'Divisão de Garagens e Oficina', '2023-09-27 14:21:25.901370', NULL, 8, 1),
(25, 'Divisão de Informática', '2023-09-27 14:22:44.957759', NULL, 14, 1),
(26, 'Divisão de Material e Patrimônio', '2023-09-27 14:23:17.630001', NULL, 8, 1),
(27, 'Divisão de Modernização Administrativa', '2023-09-27 14:23:51.015744', NULL, 8, 1),
(28, 'Divisão de Odontologia', '2023-09-27 15:03:50.376335', NULL, 7, 1),
(29, 'Divisão de Orçamento', '2023-09-27 15:04:20.958485', NULL, 14, 1),
(30, 'Divisão de Orçamento E Contabilidade', '2023-09-27 15:04:57.950331', NULL, 6, 1),
(31, 'Divisão de Planejamento', '2023-09-27 15:05:21.487159', NULL, 14, 1),
(32, 'Divisão de Projetos', '2023-09-27 15:05:42.775588', NULL, 13, 1),
(33, 'Divisão de Promoção Humana e Social', '2023-09-27 15:06:26.796580', NULL, 10, 1),
(34, 'Divisão de Reabilitação', '2023-09-27 15:06:44.655919', NULL, 10, 1),
(35, 'Divisão de Recursos Humanos', '2023-09-27 15:07:29.814849', NULL, 8, 1),
(36, 'Divisão de Serviços Urbanos', '2023-09-27 15:07:55.616309', NULL, 13, 1),
(37, 'Divisão de Tributação e Arrecadação', '2023-09-27 15:09:00.913708', NULL, 6, 1),
(38, 'Divisão de Vias Urbanas', '2023-09-27 15:09:33.121986', NULL, 13, 1),
(39, 'Divisão de Vigilância Epidemiológica', '2023-09-27 15:10:41.434923', NULL, 7, 1),
(40, 'Divisão de Vigilância Sanitária', '2023-09-27 15:11:05.689127', NULL, 7, 1),
(41, 'Escola Municipal Geralda Márcia Pereira Gonçalves', '2023-09-27 15:13:19.566111', NULL, 11, 2),
(42, 'Escola Municipal Professor Johnsen', '2023-09-27 15:14:01.365257', NULL, 11, 2),
(43, 'Escola Municipal Rosa Pedroso de Almeida', '2023-09-27 15:14:22.733663', NULL, 11, 2),
(44, 'Escola Municipal Umes/Telecurso', '2023-09-27 15:14:53.181630', NULL, 11, 2),
(45, 'Escola Municipal Carlindo Nascimento Gaia', '2023-09-27 15:15:24.181736', NULL, 11, 2),
(46, 'Escola Municipal Clarinda Firmina A. Santos', '2023-09-27 15:15:45.597039', NULL, 11, 2),
(47, 'Escola Municipal Irene Castelo Branco', '2023-09-27 15:17:11.275038', NULL, 11, 2),
(48, 'Escola Municipal Policena Alves de Amorim', '2023-09-27 15:17:40.135669', NULL, 11, 2),
(49, 'Escola Municipal Pref. Joaquim Cândido Gonçalves', '2023-09-27 15:18:00.182792', NULL, 11, 2),
(50, 'Escolas Municipais Rurais', '2023-09-27 15:18:25.871588', NULL, 11, 2),
(51, 'Gabinete do Prefeito', '2023-09-27 15:18:50.023491', NULL, 2, 2),
(52, 'Instituto de Previdência Municipal de Três Marias', '2023-09-27 15:19:09.718930', NULL, 3, 2),
(53, 'Núcleo Pedagógico do Ensino Supletivo', '2023-09-27 15:19:57.446946', NULL, 11, 2),
(54, 'Secretaria Municipal de Educação e Cultura', '2023-09-27 15:20:26.288586', NULL, 11, 2),
(55, 'Secretaria Municipal de Des., Esportes e Turismo', '2023-09-27 15:20:59.992601', NULL, 5, 2),
(56, 'Secretaria Municipal da Fazenda', '2023-09-27 15:23:16.283027', NULL, 6, 2),
(57, 'Secretaria Municipal de Administração', '2023-09-27 15:23:46.393650', NULL, 8, 2),
(58, 'Secretaria Municipal de Agricultura', '2023-09-27 15:24:06.064707', NULL, 9, 2),
(59, 'Superintendência Administrativa', '2023-09-27 15:24:55.521047', NULL, 3, 2),
(60, 'Serviço de Fisioterapia', '2023-09-27 15:25:13.761825', NULL, 7, 2),
(61, 'Secretaria Municipal de Assist. e Promoção Social', '2023-09-27 15:25:51.897780', NULL, 10, 2),
(62, 'Secretaria Municipal de Meio Ambiente', '2023-09-27 15:26:29.522880', NULL, 12, 2),
(63, 'Secretaria Municipal de Obras e Serviços Urbanos', '2023-09-27 15:26:54.170229', NULL, 13, 2),
(64, 'Secretaria Municipal de Planejamento', '2023-09-27 15:27:29.241515', NULL, 14, 2),
(65, 'Sub-Prefeitura de Andrequicé', '2023-09-27 15:27:47.945982', NULL, 15, 2),
(66, 'Secretaria Municipal de Saúde', '2023-09-29 13:53:00.025948', NULL, 8, 3);

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

--
-- Despejando dados para a tabela `instituitions`
--

INSERT INTO `instituitions` (`Id`, `Name`, `RegisterDate`, `ModifyDate`, `UserAdministratorId`) VALUES
(1, 'Fundação Municipal São Francisco', '2023-09-25 19:52:07.561796', NULL, 1),
(2, 'Gabinete do Prefeito', '2023-09-25 20:14:47.442879', NULL, 1),
(3, 'Instituto de Previdência Municipal de Três Marias', '2023-09-25 20:16:26.868157', NULL, 1),
(4, 'Procuradoria Geral do Município', '2023-09-25 20:16:53.769317', NULL, 1),
(5, 'Secretaria Municipal de Desenvolvimento, Esporte e Turimo', '2023-09-25 20:17:49.897612', NULL, 1),
(6, 'Secretaria Municipal da Fazenda', '2023-09-25 20:18:55.147004', NULL, 1),
(7, 'Secretaria Municipal da Saúde', '2023-09-25 20:19:09.595765', NULL, 1),
(8, 'Secretaria Municipal de Administração', '2023-09-25 20:19:30.657043', NULL, 1),
(9, 'Secretaria Municipal de Agricultura', '2023-09-25 20:19:52.385975', NULL, 1),
(10, 'Secretaria Municipal de Assistência e Promoção Social', '2023-09-25 20:20:26.322478', NULL, 1),
(11, 'Secretaria Municipal de Educação', '2023-09-25 20:20:53.599085', NULL, 1),
(12, 'Secretaria Municipal de Meio Ambiente', '2023-09-25 20:21:24.729883', NULL, 1),
(13, 'Secretaria Municipal de Obras e Serviços Urbanos', '2023-09-25 20:22:03.209412', NULL, 1),
(14, 'Secretaria Municipal de Planejamento', '2023-09-25 20:22:13.545660', NULL, 1),
(15, 'Sub-Prefeitura de Andrequicé', '2023-09-25 20:22:27.428194', NULL, 1);

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

--
-- Despejando dados para a tabela `sections`
--

INSERT INTO `sections` (`Id`, `Name`, `RegisterDate`, `ModifyDate`, `DivisionId`, `UserAdministratorId`) VALUES
(1, 'Centro de Assistência Psico Social', '2023-09-29 12:28:02.500711', NULL, 4, 1),
(2, 'Centro de Educação Inf. Luculênia A. O. Sil', '2023-09-29 12:29:17.818665', NULL, 5, 1),
(3, 'Centro Público de Prom. Trabalho', '2023-09-29 12:29:54.155940', NULL, 21, 1),
(4, 'Coordenadoria do SIAT', '2023-09-29 12:30:26.683266', NULL, 37, 1),
(5, 'Creche Lar da Criança', '2023-09-29 12:30:56.010718', '2023-09-29 12:44:17.469524', 7, 1),
(6, 'Creche Pequeno Polegar', '2023-09-29 12:50:31.263164', NULL, 8, 1),
(7, 'Creche Cantinho da Criança', '2023-09-29 12:51:23.352579', NULL, 6, 1),
(8, 'Divisão Ambulatorial', '2023-09-29 12:51:49.890135', NULL, 9, 1),
(9, 'Divisão Coord. do P.Saúde Da Familia - PSF', '2023-09-29 12:52:58.515628', NULL, 10, 1),
(10, 'Divisão de Administração', '2023-09-29 12:53:25.275656', NULL, 11, 1),
(11, 'Divisão de Administração Financeira', '2023-09-29 12:54:42.992967', NULL, 12, 1),
(12, 'Divisão de Apoio Administrativo', '2023-09-29 12:55:15.475195', NULL, 13, 1),
(13, 'Divisão de Apoio Jurídico', '2023-09-29 12:55:38.970093', NULL, 14, 1),
(14, 'Divisão de Cadastro Imobiliário', '2023-09-29 12:56:00.028518', NULL, 15, 1),
(15, 'Divisão de Comunicação Social', '2023-09-29 12:56:22.458815', NULL, 16, 1),
(16, 'Divisão de Controle Interno', '2023-09-29 12:56:56.611264', NULL, 17, 1),
(17, 'Divisão de Cultura', '2023-09-29 12:57:18.267964', NULL, 18, 1),
(18, 'Divisão de Desenvolvimento Comunitário', '2023-09-29 12:57:40.331803', NULL, 19, 1),
(19, 'Divisão de Ensino', '2023-09-29 12:58:25.440049', NULL, 20, 1),
(20, 'Divisão de Esportes e Lazer', '2023-09-29 12:59:04.299305', NULL, 21, 1),
(21, 'Divisão de Execução Orçamentária', '2023-09-29 12:59:27.659227', NULL, 22, 1),
(22, 'Divisão de Garagens e Oficina', '2023-09-29 12:59:53.717086', NULL, 24, 1),
(23, 'Divisão de Informática', '2023-09-29 13:00:25.305998', NULL, 25, 1),
(24, 'Divisão de Material e Patrimônio', '2023-09-29 13:00:51.212589', NULL, 26, 1),
(25, 'Divisão de Modernização Administrativa', '2023-09-29 13:01:22.651781', NULL, 27, 1),
(26, 'Divisão de Odontologia', '2023-09-29 13:01:45.914288', NULL, 28, 1),
(27, 'Divisão de Orçamento', '2023-09-29 13:02:05.125851', NULL, 29, 1),
(28, 'Divisão de Planejamento', '2023-09-29 13:02:28.819454', NULL, 31, 1),
(29, 'Divisão de Projetos', '2023-09-29 13:02:41.068601', NULL, 32, 1),
(30, 'Divisão de Promoção Humana e Social', '2023-09-29 13:03:02.853669', NULL, 33, 1),
(31, 'Divisão de Reabilitação', '2023-09-29 13:03:22.044506', NULL, 34, 1),
(32, 'Divisão de Recursos Humanos', '2023-09-29 13:03:41.037760', NULL, 35, 1),
(33, 'Divisão de Serviços Urbanos', '2023-09-29 13:04:00.155388', NULL, 36, 1),
(34, 'Divisão de Tributação e Arrecadação', '2023-09-29 13:04:35.148005', NULL, 37, 1),
(35, 'Divisão de Vias Urbanas', '2023-09-29 13:04:53.427467', NULL, 38, 1),
(36, 'Divisão de Vigilância Epidemiológica', '2023-09-29 13:05:30.823122', NULL, 39, 1),
(37, 'Divisão de Vigilância Sanitária', '2023-09-29 13:05:55.591793', NULL, 40, 1),
(38, 'Escola Municipal Antônio Fonseca Leal', '2023-09-29 13:06:17.414484', NULL, 1, 1),
(39, 'Escola Municipal Carlindo Nascimento Gaia', '2023-09-29 13:08:08.210192', NULL, 45, 2),
(40, 'Escola Municipal Clarinda Firmina A. Santos', '2023-09-29 13:09:08.226715', NULL, 46, 2),
(41, 'Escola Municipal Geralda Márica Pereira Ginçalves', '2023-09-29 13:09:38.741186', NULL, 41, 2),
(42, 'Escola Municipal Irene Castelo Branco', '2023-09-29 13:10:03.091967', NULL, 47, 2),
(43, 'Escola Municipal Memorial Zumbi', '2023-09-29 13:10:47.485390', NULL, 2, 2),
(44, 'Escola Municipal Policena Alves de Amorim', '2023-09-29 13:11:19.948919', NULL, 48, 2),
(45, 'Escola Municipal Pref. Joaquim Cândido Gonçalves', '2023-09-29 13:11:49.659003', NULL, 49, 2),
(46, 'Escola Municipal Professor Johnsen', '2023-09-29 13:12:12.006054', NULL, 42, 2),
(47, 'Escola Municipal Rosa Pedroso de Almeida', '2023-09-29 13:12:52.074701', NULL, 43, 2),
(48, 'Escola Municipal Umes/Telecurso', '2023-09-29 13:13:16.118953', NULL, 44, 2),
(49, 'Gabinete do Prefeito', '2023-09-29 13:13:30.197779', NULL, 51, 2),
(50, 'Horta Comunitária', '2023-09-29 13:14:09.877549', NULL, 33, 2),
(51, 'Instituto de Previdência Municipal de Três Marias', '2023-09-29 13:14:45.469514', NULL, 52, 2),
(52, 'Núcleo de Apoio a Família 1', '2023-09-29 13:15:14.374076', NULL, 33, 2),
(53, 'Núcleo de Apoio a Família 2', '2023-09-29 13:15:39.413814', NULL, 33, 2),
(54, 'Núcleo de Apoio a Família 3', '2023-09-29 13:15:58.077862', NULL, 33, 2),
(55, 'Núcleo de Apoio a Família 4', '2023-09-29 13:16:14.086886', NULL, 33, 2),
(56, 'Núcleo de Fiscalização', '2023-09-29 13:16:32.655751', NULL, 36, 2),
(57, 'Núcleo Pedagógico de Ensino Supletivo e Alf. J. Adult', '2023-09-29 13:17:23.040314', NULL, 20, 2),
(58, 'Núcleo Pedagógico do Ensino Supletivo', '2023-09-29 13:17:48.663153', NULL, 53, 2),
(59, 'Sessão de Agropecuária e Comercialização', '2023-09-29 13:24:10.810136', NULL, 58, 2),
(60, 'Seção de Almoxarifado', '2023-09-29 13:24:43.678868', NULL, 9, 2),
(61, 'Seção de Carpintaria e Estoques', '2023-09-29 13:25:22.998168', NULL, 36, 2),
(62, 'Seção de Controle Ambiental', '2023-09-29 13:26:03.882254', NULL, 62, 2),
(63, 'Seção de Controle e Fito Sanitário', '2023-09-29 13:26:39.277917', NULL, 58, 2),
(64, 'Seção de Desenvolvimento, Emprego e Renda', '2023-09-29 13:27:24.398470', NULL, 23, 2),
(65, 'Seção de Estradas Vic. E Mecanização Agrícola', '2023-09-29 13:28:03.701161', NULL, 58, 2),
(66, 'Sessão de Lazer', '2023-09-29 13:28:36.571932', NULL, 21, 2),
(67, 'Sessão de Licitação', '2023-09-29 13:29:06.461877', NULL, 26, 2),
(68, 'Sessão de Limpeza Urbana', '2023-09-29 13:29:44.175776', NULL, 36, 2),
(69, 'Seção de Manutenção das Torres de Ret de Sinal de Tv', '2023-09-29 13:39:25.786712', NULL, 36, 2),
(70, 'Seção de Mnauteção de Mecânica de Autos', '2023-09-29 13:39:59.145529', NULL, 24, 2),
(71, 'Seção de Patrimônio', '2023-09-29 13:40:20.906715', NULL, 26, 2),
(72, 'Seção de Praças e Jardins', '2023-09-29 13:42:52.579024', NULL, 62, 3),
(73, 'Seção de Reformas e Manutenção', '2023-09-29 13:43:23.444983', NULL, 36, 3),
(74, 'Sessão de Serviços Públicos', '2023-09-29 13:43:44.300641', NULL, 36, 3),
(75, 'Sessão de Vigilância Patrimonial', '2023-09-29 13:44:19.446612', NULL, 26, 3),
(76, 'Seção de Desenvolvimento da Pequena Empresa', '2023-09-29 13:44:56.495959', NULL, 23, 3),
(77, 'Secretaria Municipal de Des., Esportes e Turismo', '2023-09-29 13:46:01.332223', NULL, 55, 3),
(78, 'Secretaria Municipal da Fazenda', '2023-09-29 13:46:57.198221', NULL, 56, 3),
(79, 'Secretaria Municipal de Administração', '2023-09-29 13:47:17.671347', NULL, 57, 3),
(80, 'Secretaria Municipal de Agricultura', '2023-09-29 13:47:46.918988', NULL, 58, 3),
(81, 'Secretaria Municipal de Assist. e Promoção Social', '2023-09-29 13:48:25.209056', NULL, 61, 3),
(82, 'Secretaria Municipal de Educação e Cultura', '2023-09-29 13:48:50.351967', NULL, 54, 3),
(83, 'Secretaria Municipal de Meio Ambiente', '2023-09-29 13:49:23.855763', NULL, 62, 3),
(84, 'Secretaria Municipal de Obras e Serviços Urbanos', '2023-09-29 13:50:04.233786', NULL, 63, 3),
(85, 'Secretaria Municipal de Planejamento', '2023-09-29 13:50:31.506225', NULL, 64, 3),
(86, 'Secretaria Municipal de Saúde', '2023-09-29 13:53:37.915049', NULL, 66, 3),
(87, 'Serviço de Fisioterapia', '2023-09-29 13:53:58.111821', NULL, 60, 3),
(88, 'Setor de Limpeza Urbana 1', '2023-09-29 13:54:36.625983', NULL, 36, 3),
(89, 'Setor de Limpeza Urbana 2', '2023-09-29 13:54:50.386004', NULL, 36, 3),
(90, 'Setor de Limpeza Urbana 3', '2023-09-29 13:55:07.234799', NULL, 36, 3),
(91, 'Sítio da Criança e do Adolescente', '2023-09-29 13:55:35.299894', NULL, 3, 3),
(92, 'Sub-Prefeitura de Andrequicé', '2023-09-29 13:55:55.498691', NULL, 65, 3);

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

--
-- Despejando dados para a tabela `sectors`
--

INSERT INTO `sectors` (`Id`, `Name`, `RegisterDate`, `ModifyDate`, `SectionId`, `UserAdministratorId`) VALUES
(1, 'Escola Municipal Policena Alves de Amorim', '2023-10-02 09:54:06.667739', NULL, 44, 1),
(2, 'Centro de Assistência Psico-Social', '2023-10-02 09:54:32.571313', NULL, 1, 1),
(3, 'Centro de Educação Inf. Lucilênia A. O. Sil', '2023-10-02 09:55:09.947182', NULL, 2, 1),
(4, 'Coordenadoria do SIAT', '2023-10-02 09:55:32.543196', NULL, 4, 1),
(5, 'Creche Cantinho da Criança', '2023-10-02 09:56:13.123118', NULL, 7, 1),
(6, 'Creche Lar da Criança', '2023-10-02 09:56:36.345509', NULL, 5, 1),
(7, 'Creche Pequeno Polegar', '2023-10-02 09:57:36.236552', NULL, 6, 1),
(8, 'Divisão Ambulatorial', '2023-10-02 09:58:17.056375', NULL, 8, 1),
(9, 'Divisão Coord. do P.Saúde da Família - PSF', '2023-10-02 09:59:13.036717', NULL, 9, 1),
(10, 'Divisão de Administração', '2023-10-02 09:59:31.979794', NULL, 10, 1),
(11, 'Divisão de Administração Financeira', '2023-10-02 10:00:31.666332', NULL, 11, 1),
(12, 'Divisão de Apoio Jurídico', '2023-10-02 10:01:36.558261', NULL, 13, 1),
(13, 'Divisão de Cadastro Imobiliário', '2023-10-02 10:02:08.768270', NULL, 14, 1),
(14, 'Divisão de Controle Interno', '2023-10-02 10:03:28.117909', NULL, 16, 1),
(15, 'Divisão de Cultura', '2023-10-02 10:03:46.413967', NULL, 17, 1),
(16, 'Divisão de Desenvolvimento Comunitário', '2023-10-02 10:04:38.036447', NULL, 18, 1),
(17, 'Divisão de Ensino', '2023-10-02 10:05:17.788683', NULL, 19, 1),
(18, 'Divisão de Esportes e Lazer', '2023-10-02 10:05:38.853765', NULL, 20, 1),
(19, 'Divisão de Execução Orçamentária', '2023-10-02 10:07:32.354614', NULL, 21, 1),
(20, 'Divisão de Garagens e Oficina', '2023-10-02 10:07:54.399445', NULL, 22, 1),
(21, 'Divisão de Informática', '2023-10-02 10:09:09.735688', NULL, 23, 1),
(22, 'Divisão de Modernização Administrativa', '2023-10-02 10:09:35.761336', NULL, 25, 1),
(23, 'Divisão de Odontologia', '2023-10-02 10:10:23.952875', NULL, 26, 1),
(24, 'Divisão de Planejamento', '2023-10-02 10:11:03.328265', NULL, 28, 1),
(25, 'Divisão de Orçamento', '2023-10-02 10:11:36.239351', NULL, 27, 1),
(26, 'Divisão de Projetos', '2023-10-02 10:11:58.616641', NULL, 29, 1),
(27, 'Divisão de Promoção Humana Social', '2023-10-02 10:12:33.480718', NULL, 30, 1),
(28, 'Divisão de Reabilitação', '2023-10-02 10:13:14.681466', NULL, 31, 1),
(29, 'Divisão de Recursos Humanos', '2023-10-02 10:13:58.265111', NULL, 32, 1),
(30, 'Divisão de Serviços Urbanos', '2023-10-02 10:14:41.135417', NULL, 33, 1),
(31, 'Divisão de Tributação e Arrecadação', '2023-10-02 10:15:03.528881', NULL, 34, 1),
(32, 'Divisão de Vias Urbanas', '2023-10-02 10:15:20.095974', NULL, 35, 1),
(33, 'Divisão de Vigilância Epidemiológica', '2023-10-02 10:15:51.067447', NULL, 36, 1),
(34, 'Divisão de Vigilância Sanitária', '2023-10-02 10:16:13.755785', NULL, 37, 1),
(35, 'Escola Municipal Antônio Fonseca Leal', '2023-10-02 10:16:28.130206', NULL, 38, 1),
(36, 'Escola Municipal Carlindo Nascimento Gaia', '2023-10-02 10:16:57.815328', NULL, 39, 1),
(37, 'Escola Municipal Clarinda Firmina de A. Santos', '2023-10-02 10:17:33.382686', NULL, 40, 1),
(38, 'Escola Municipal Geralda Márcia Pereira Gonçalves', '2023-10-02 10:20:50.804449', NULL, 41, 1),
(39, 'Escola Municipal Irene Castelo Branco', '2023-10-02 18:58:31.702795', NULL, 42, 2),
(40, 'Escola Municipal Memorial Zumbi', '2023-10-02 18:58:55.465788', NULL, 43, 2),
(41, 'Escola Municipal Policena Alves de Amorim', '2023-10-02 19:00:16.716993', NULL, 44, 2),
(42, 'Escola Municipal Pref. Joaquim Cândido Gonçalves', '2023-10-02 19:00:47.138205', NULL, 45, 2),
(43, 'Escola Municipal Professor Johnsen', '2023-10-02 19:01:48.205940', NULL, 46, 2),
(44, 'Escola Municipal Rosa Pedroso de Almeida', '2023-10-02 19:02:22.130390', NULL, 47, 2),
(45, 'Escola Municipal Umes/Telecurso', '2023-10-02 19:02:44.409677', NULL, 48, 2),
(46, 'Gabinete do Prefeito', '2023-10-02 19:03:03.161758', NULL, 49, 2),
(47, 'Horta Comunitária', '2023-10-02 19:03:25.844588', NULL, 50, 2),
(48, 'Instituto de Previdência Municipal de Três Marias', '2023-10-02 19:03:50.353460', NULL, 51, 2),
(49, 'Núcleo de Apoio a Família 1', '2023-10-02 19:04:19.195129', NULL, 52, 2),
(50, 'Núcleo de Apoio a Família 2', '2023-10-02 19:04:32.954606', NULL, 53, 2),
(51, 'Núcleo de Apoio a Família 3', '2023-10-02 19:05:14.459695', NULL, 54, 2),
(52, 'Núcleo de Apoio a Família 4', '2023-10-02 19:05:25.130619', NULL, 55, 2),
(53, 'Núcleo de Compras', '2023-10-02 19:13:24.082779', NULL, 24, 2),
(54, 'Núcleo de Fiscalização', '2023-10-02 19:13:55.645760', NULL, 56, 2),
(55, 'Núcleo de Merenda Escolar', '2023-10-02 19:14:35.307718', NULL, 10, 2),
(56, 'Núcleo Pedagógico do Ensino Supletivo', '2023-10-02 19:15:09.719313', NULL, 58, 2),
(57, 'Seção de Agropecuária e Comercialização', '2023-10-02 19:15:36.601639', NULL, 59, 2),
(58, 'Seção de Carpintaria e Estoques', '2023-10-02 19:16:01.679365', NULL, 61, 2),
(59, 'Seção de Controle Ambiental', '2023-10-02 19:17:25.431057', NULL, 62, 2),
(60, 'Seção de Controle Fito Sanitário', '2023-10-02 19:48:02.600434', NULL, 63, 2),
(61, 'Seção de Estradas Vic. e Mecanização Agrícola', '2023-10-02 19:48:30.057875', NULL, 65, 2),
(62, 'Seção de Lazer', '2023-10-02 19:48:40.666072', NULL, 66, 2),
(63, 'Seção de Limpeza Urbana', '2023-10-02 19:48:59.163714', NULL, 68, 2),
(64, 'Seção de Manutenção de Mecânica de Autos', '2023-10-02 19:50:10.196626', NULL, 70, 2),
(65, 'Seção de Patrimônio', '2023-10-02 19:51:00.086214', NULL, 71, 2),
(66, 'Seção de Praças e Jardins', '2023-10-02 19:51:49.820429', NULL, 72, 2),
(67, 'Seção de Reformas e Manutenção', '2023-10-02 19:53:22.420460', NULL, 73, 2),
(68, 'Seção de Serviços Públicos', '2023-10-02 19:53:59.174499', NULL, 74, 2),
(69, 'Seção de Vigilância Patrimonial', '2023-10-02 19:54:29.369828', NULL, 75, 2),
(70, 'Secretaria Municipal de Saúde', '2023-10-02 19:55:03.142213', NULL, 86, 2),
(71, 'Secretaria Municipal de Des., Esportes e Turismo', '2023-10-02 19:55:38.874934', NULL, 77, 2),
(72, 'Secretaria Municipal da Fazenda', '2023-10-02 19:56:04.918432', NULL, 78, 2),
(73, 'Secretaria Municipal de Administração', '2023-10-02 19:56:29.594805', NULL, 79, 2),
(74, 'Secretaria Municipal de Agricultura', '2023-10-02 19:56:55.948673', NULL, 80, 2),
(75, 'Secretaria Municipal de Assist. e Promoção Social', '2023-10-02 20:00:20.536098', NULL, 81, 3),
(76, 'Secretaria Municipal de Educação e Cultura', '2023-10-02 20:04:23.272865', NULL, 82, 3),
(77, 'Secretaria Municipal de Meio Ambiente', '2023-10-02 20:14:42.569589', NULL, 83, 3),
(78, 'Secretaria Municipal de Obras e Serviços Urbanos', '2023-10-02 20:15:10.510797', NULL, 84, 3),
(79, 'Secretaria Municipal de Planejamento', '2023-10-02 20:15:45.674271', NULL, 85, 3),
(80, 'Serviço de Fisioterpia', '2023-10-02 20:17:14.407219', NULL, 87, 3),
(81, 'Setor de Formação Musical', '2023-10-02 20:17:52.124383', NULL, 19, 3),
(82, 'Setor de Limpeza Urbana 1', '2023-10-02 20:18:16.651265', NULL, 74, 3),
(83, 'Setor de Limpeza Urbana 1', '2023-10-02 20:18:16.665616', NULL, 74, 3),
(84, 'Setor de Limpeza Urbana 2', '2023-10-02 20:18:55.698952', NULL, 89, 3),
(85, 'Setor de Limpeza Urbana 2', '2023-10-02 20:19:23.803194', NULL, 74, 3),
(86, 'Setor de Limpeza Urbana 3', '2023-10-02 20:19:43.657879', NULL, 90, 3),
(87, 'Setor de Limpeza Urbana 3', '2023-10-02 20:20:01.642605', NULL, 74, 3),
(88, 'Setor de Limpeza Urbana 1', '2023-10-02 20:20:24.019388', NULL, 88, 3),
(89, 'Setor de Manutenção Elétrica', '2023-10-02 20:21:22.388837', NULL, 74, 3),
(90, 'Setor de Manutenção Hidráulica', '2023-10-02 20:21:55.255547', NULL, 74, 3),
(91, 'Setor de Produção Artística', '2023-10-02 20:23:17.551224', NULL, 19, 3),
(92, 'Setor de Produção Artística', '2023-10-02 20:24:20.527998', NULL, 17, 3),
(93, 'Setor de Promoção de Eventos', '2023-10-02 20:24:58.765638', NULL, 66, 3),
(94, 'Setor de Reformas e Manutenção de Prédios', '2023-10-02 20:25:35.449736', NULL, 74, 3),
(95, 'Sítio da Criança e do Adolescente', '2023-10-02 20:26:11.637592', NULL, 91, 3),
(96, 'Sub-Prefeitura de Andrequicé', '2023-10-02 20:26:28.358315', NULL, 92, 3);

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
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de tabela `divisions`
--
ALTER TABLE `divisions`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=67;

--
-- AUTO_INCREMENT de tabela `evaluators`
--
ALTER TABLE `evaluators`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `instituitions`
--
ALTER TABLE `instituitions`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de tabela `sections`
--
ALTER TABLE `sections`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=93;

--
-- AUTO_INCREMENT de tabela `sectors`
--
ALTER TABLE `sectors`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=97;

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
