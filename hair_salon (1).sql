-- phpMyAdmin SQL Dump
-- version 4.7.0
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Aug 20, 2017 at 12:37 AM
-- Server version: 5.6.34-log
-- PHP Version: 7.1.5

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `hair_salon`
--
CREATE DATABASE IF NOT EXISTS `hair_salon` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `hair_salon`;

-- --------------------------------------------------------

--
-- Table structure for table `clients`
--

CREATE TABLE `clients` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `first_name` varchar(255) DEFAULT NULL,
  `last_name` varchar(255) DEFAULT NULL,
  `phone` varchar(255) DEFAULT NULL,
  `email` varchar(255) NOT NULL,
  `stylist_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `clients`
--

INSERT INTO `clients` (`id`, `first_name`, `last_name`, `phone`, `email`, `stylist_id`) VALUES
(17, 'Anne', 'Bella', '978.345.2345', 'bells@gmail.com', 9),
(29, 'Betty', 'White', '206.234.6543', 'betty@mail.com', 14),
(30, 'Abe', 'Lincoln', '234.654.9764', 'abe@mail.com', 16),
(31, 'John', 'Lennon', '236.764.9876', 'jl@mail.com', 13),
(32, 'Tom', 'Hanks', '346.543.6564', 'tom@mail.com', 15),
(33, 'Marilyn', 'Monroe', '246.643.7534', 'mari@mail.com', 12),
(34, 'Emily', 'Blunt', '236.444.5543', 'em@mail.com', 5),
(35, 'Arya', 'Stark', '324.323.2341', 'arya@mail.com', 16),
(36, 'Jon', 'Snow', '236.435.6431', 'snow@mail.com', 11),
(37, 'Rilo', 'Kiley', '643.454.1454', 'rilo@mail.com', 12),
(39, 'Steve', 'Jobs', '645.324.1532', 'jobs@mail.com', 13),
(40, 'Nikola', 'Jokic', '765.234.1343', 'nikola@mail.com', 11),
(41, 'Peter', 'Dinklage', '543.345.2454', 'dink@mail.com', 15),
(42, 'Jennifer', 'Lawrence', '643.454.1343', 'jenn@mail.com', 10),
(43, 'Yayoi', 'Kusama', '234.354.1343', 'yay@mail.com', 10),
(44, 'Nicole', 'Kidman', '345.435.4543', 'kid@mail.com', 18),
(45, 'Joseph', 'Gordon-Levitt', '657.543.4321', 'joe@mail.com', 10),
(46, 'Serena', 'Williams', '754.245.1343', 'serena@mail.com', 9),
(47, 'Roger', 'Federer', '645.435.3445', 'fed@mail.com', 11),
(48, 'Halle', 'Berry', '543.454.3452', 'halle@mail.com', 12),
(49, 'Natalie', 'Portman', '454.435.2543', 'nat@mail.com', 18),
(50, 'Mila', 'Kunis', '543.453.2543', 'mila@mail.com', 13),
(51, 'Julia', 'Roberts', '324.134.3421', 'julia@mail.com', 5),
(52, 'Meryl', 'Streep', '867.456.3452', 'meryl@mail.com', 14),
(53, 'Emma', 'Stone', '756.345.2451', 'stone@mail.com', 15),
(54, 'Amy', 'Adams', '543.756.3543', 'amy@mail.com', 10),
(55, 'Jodie', 'Foster', '756.345.2452', 'jodie@mail.com', 14),
(56, 'Anna', 'Kendrick', '645.345.3453', 'anna@mail.com', 9),
(57, 'Penelope', 'Cruz', '764.456.2453', 'pen@mail.com', 5),
(58, 'Brad', 'Pitt', '364.345.1254', 'brad@mail.com', 17),
(59, 'Harrison', 'Ford', '643.345.5642', 'harri@mail.com', 17),
(60, 'Christian', 'Bale', '643.353.4532', 'bale@mail.com', 17),
(61, 'Kate', 'Winslet', '342.343.6431', 'kate@mail.com', 17),
(62, 'John', 'Oliver', '121.543.1546', 'john@mail.com', 12),
(66, 'tetesr', 'sdrsdf', 'dfdsf', 'dffd', 9);

-- --------------------------------------------------------

--
-- Table structure for table `stylists`
--

CREATE TABLE `stylists` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `first_name` varchar(255) DEFAULT NULL,
  `last_name` varchar(255) DEFAULT NULL,
  `womens_cut` int(11) DEFAULT NULL,
  `mens_cut` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `stylists`
--

INSERT INTO `stylists` (`id`, `first_name`, `last_name`, `womens_cut`, `mens_cut`) VALUES
(5, 'Kimberly', 'Kimble', 90, 80),
(9, 'John', 'Frieda', 90, 85),
(10, 'Ken', 'Paves', 600, 400),
(11, 'Chris', 'McMillan', 180, 165),
(12, 'Frederic', 'Fekkai', 200, 95),
(13, 'Paul', 'Mitchell', 70, 65),
(14, 'Vidal', 'Sassoon', 170, 150),
(15, 'Kevin', 'Lee', 80, 70),
(17, 'Oribe', '', 150, 130),
(18, 'Yolanda', 'Toussieng', 90, 80);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `clients`
--
ALTER TABLE `clients`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id` (`id`);

--
-- Indexes for table `stylists`
--
ALTER TABLE `stylists`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id` (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `clients`
--
ALTER TABLE `clients`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=67;
--
-- AUTO_INCREMENT for table `stylists`
--
ALTER TABLE `stylists`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
