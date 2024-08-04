-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 04, 2024 at 11:21 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dbgame`
--

-- --------------------------------------------------------

--
-- Table structure for table `levels`
--

CREATE TABLE `levels` (
  `level_id` int(11) NOT NULL,
  `level_name` varchar(50) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `levels`
--

INSERT INTO `levels` (`level_id`, `level_name`, `created_at`, `updated_at`) VALUES
(1, 'Level1', '2024-07-28 22:58:03', '2024-07-28 22:58:03'),
(2, 'Level2', '2024-07-28 22:58:03', '2024-07-28 22:58:03'),
(3, 'Level3', '2024-08-03 20:55:49', '2024-08-03 20:55:49'),
(4, 'Level4', '2024-08-03 20:55:49', '2024-08-03 20:55:49'),
(5, 'Level5', '2024-08-03 20:55:49', '2024-08-03 20:55:49');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `sex` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `username`, `email`, `password`, `created_at`, `updated_at`, `sex`) VALUES
(22, 'sara', 'sara@sara.com', '$2y$10$15Xz/uQR.VKB7CL3Uc77B.f60VoljbnJaQwtAC4NJvGuHgO1XL2Qm', '2024-07-21 22:41:48', '2024-07-21 22:41:48', NULL),
(24, 'juanca', 'juanca@gmail.com', '$2y$10$gu3LC4TS6R2ik3vJqy/K5ejmd/7QPH203sMm0AuDF6uXranru/vLy', '2024-07-21 22:46:47', '2024-07-27 19:21:14', 'Masculino'),
(25, 'fran', 'fran@fram.com', '$2y$10$14IVOjM8KR5wH.9dQlCdkOvEyEYKtLTQq5wUtKg8xB06u365Ev13K', '2024-07-21 22:57:00', '2024-07-21 22:57:00', 'Masculino'),
(26, 'dan', 'dan@dan.com', '$2y$10$9Qg/kmjU7kcHrhdX4.94n.SjtqLdXnVL1fn3lsjd.aGlOUom0OoZ2', '2024-07-21 22:57:50', '2024-07-21 22:57:50', 'Seleccione'),
(27, 'camila', 'cami@gmail.com', '$2y$10$bnIcdwS3LO1OgdwupdAgm.j/IEy4ir1kaB32jhBSc9mwOJ.tx0u/C', '2024-07-21 23:01:08', '2024-07-21 23:01:08', 'Femenino'),
(30, 'pop', 'pop@g.co', '$2y$10$JciZln4qJzvQZeHeXxRJROZuXuBwMcHedW9AlASYP3K05/mzomCfe', '2024-07-22 05:00:32', '2024-07-22 05:00:32', 'Masculino'),
(31, 'asdf', 'asdf@asdf.com', '$2y$10$VOhHaxqRilDcUoH/Sva9weWJBKs5B7KDurNKIwdRDHUyX7voRBy7m', '2024-07-22 05:01:49', '2024-07-22 05:01:49', 'Femenino'),
(32, 'pedrita', 'pedra3@hotmail.com', '$2y$10$7DHDVr7pKX26F53QykK7L.wGsZmoltkvgfrtj1BVgAP9NWZf2i8z2', '2024-07-22 05:07:41', '2024-07-22 05:11:53', 'Femenino'),
(33, 'pepe', 'pepe@gmail.com', '$2y$10$jILbcyfBGsacxKOPV1lE4ukVz7AbQdWuX596McQJRCbXWzNA9KQMO', '2024-07-22 11:26:54', '2024-07-22 11:29:33', 'Masculino'),
(34, 'carla', 'carla@gmail.com', '$2y$10$i4P70YxzcQhMzlTalIXrf.HoieJfuo1M/u2AIL4xsLogApq3s6FlK', '2024-07-27 19:04:08', '2024-07-27 22:48:15', 'Femenino'),
(35, 'pepa', 'pepa@gmail.com', '$2y$10$m8x1r/v1uW4ubEStzz6xk.yu9xotvr78V0Vlzz47zdJXsYFQyQ4yy', '2024-07-28 17:43:47', '2024-07-28 17:43:47', 'Femenino'),
(36, 'jose', 'jose@gmail.com', '$2y$10$6QoAeZrJQ.aRDs0SVFw39OGDW2Zgyjmmq8CPFAO/B6c.OOMgboL9e', '2024-07-28 17:47:02', '2024-07-28 17:47:02', 'Masculino'),
(37, 'juan97', 'juan@gmail.com', '$2y$10$2PMeXGX4sSRtdAv9cxmm5OiXeEkCOb4DGaVRHd7LR5IfjaQJy5LU6', '2024-08-04 21:03:26', '2024-08-04 21:05:58', 'Masculino');

-- --------------------------------------------------------

--
-- Table structure for table `user_levels`
--

CREATE TABLE `user_levels` (
  `user_level_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `level_id` int(11) NOT NULL,
  `completion_status` tinyint(1) DEFAULT NULL,
  `score` int(11) NOT NULL,
  `completed_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user_levels`
--

INSERT INTO `user_levels` (`user_level_id`, `user_id`, `level_id`, `completion_status`, `score`, `completed_at`) VALUES
(143, 37, 1, 1, 3, '2024-08-04 21:03:55'),
(144, 37, 2, 1, 3, '2024-08-04 21:03:55'),
(145, 37, 3, 1, 0, '2024-08-04 21:04:19'),
(146, 37, 4, 1, 0, '2024-08-04 21:04:44'),
(147, 36, 1, 1, 3, '2024-08-04 21:10:19'),
(148, 36, 2, 1, 3, '2024-08-04 21:10:19'),
(149, 36, 3, 1, 0, '2024-08-04 21:10:45');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `levels`
--
ALTER TABLE `levels`
  ADD PRIMARY KEY (`level_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`);

--
-- Indexes for table `user_levels`
--
ALTER TABLE `user_levels`
  ADD PRIMARY KEY (`user_level_id`),
  ADD KEY `user_id` (`user_id`),
  ADD KEY `level_id` (`level_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `levels`
--
ALTER TABLE `levels`
  MODIFY `level_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=38;

--
-- AUTO_INCREMENT for table `user_levels`
--
ALTER TABLE `user_levels`
  MODIFY `user_level_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=150;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `user_levels`
--
ALTER TABLE `user_levels`
  ADD CONSTRAINT `user_levels_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`),
  ADD CONSTRAINT `user_levels_ibfk_2` FOREIGN KEY (`level_id`) REFERENCES `levels` (`level_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
