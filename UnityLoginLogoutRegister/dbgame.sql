-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 06, 2024 at 08:03 PM
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
(5, 'Level5', '2024-08-03 20:55:49', '2024-08-03 20:55:49'),
(6, 'Level6', '2024-08-03 20:55:49', '2024-08-03 20:55:49'),
(7, 'Level7', '2024-08-03 20:55:49', '2024-08-03 20:55:49');

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
(36, 'jose', 'jose@gmail.com', '$2y$10$/A2psgsvm6TBGbgkAArZBu6vp5rqNl7B7bE0jcWnjB/aMKG5Cmkeq', '2024-07-28 17:47:02', '2024-08-05 22:28:31', 'Masculino'),
(37, 'juan97', 'juan@gmail.com', '$2y$10$2PMeXGX4sSRtdAv9cxmm5OiXeEkCOb4DGaVRHd7LR5IfjaQJy5LU6', '2024-08-04 21:03:26', '2024-08-04 21:05:58', 'Masculino'),
(38, 'peter', 'piter@d.com', '$2y$10$3zJEYcZvhBaot9MQPrRnLuB1m3nejsVrQdwnyGN.7npqOLHY9Vp92', '2024-08-05 15:31:19', '2024-08-05 21:41:50', 'Masculino'),
(39, 'josh', 'josh@mail.com', '$2y$10$540diXcFftqzCG8x/.BeIeR3A/SmHkmBCXzVBT9Z1so7CCSVhyx46', '2024-08-05 15:33:28', '2024-08-05 15:33:28', 'Masculino'),
(40, 'mark', 'jerk@mail.com', '$2y$10$D5yJ9TBeR8miizMr7qDCDe.TNI9BZQwVL.nwG.u0pwx0onL.wbc9S', '2024-08-05 15:34:37', '2024-08-05 15:35:20', 'Masculino'),
(41, 'dev', 'dev@d.com', '$2y$10$3zJEYcZvhBaot9MQPrRnLuB1m3nejsVrQdwnyGN.7npqOLHY9Vp92', '2024-08-05 15:37:01', '2024-08-05 15:37:01', 'Masculino'),
(42, 'mike', 'mike@k.com', '$2y$10$/A2psgsvm6TBGbgkAArZBu6vp5rqNl7B7bE0jcWnjB/aMKG5Cmkeq', '2024-08-05 21:43:33', '2024-08-05 21:43:33', 'Masculino'),
(43, 'mario', 'mar@em.com', '$2y$10$iOw29UtSzQjn4fMBtQjwYud./LhGAlbtVuHX1F2evUbajIJEkLKJa', '2024-08-06 15:59:21', '2024-08-06 15:59:21', 'Masculino'),
(44, 'bandan', 'bandan@mail.com', '$2y$10$9qnFutX6wRgAfl5vA8jvIuNHG2/5fb2LngKPvSCcm4vJyXHMmfULy', '2024-08-06 16:03:14', '2024-08-06 16:03:14', 'Masculino'),
(45, 'sasuke', 'saske@mail.com', '$2y$10$ajN8XniIltnCoVfPkroR.e2uwq9g4tcCRHlljlcLPEK0tQkD6j7i2', '2024-08-06 16:10:33', '2024-08-06 16:10:33', 'Masculino'),
(46, 'madara', 'madara@gmail.com', '$2y$10$VKHyWIlSmzLU9xco2X/qq.XvZRrhv3bjt03tEvdQDIRn.bHuH13sG', '2024-08-06 16:11:44', '2024-08-06 16:11:44', 'Masculino'),
(47, 'buzz', 'buzz@mail.com', '$2y$10$H1l2UUu2UChMPv5VmeyNoulYCXM8gl3IdHHLKq5wzPFBvZ0PSOy/m', '2024-08-06 16:16:06', '2024-08-06 16:16:06', 'Masculino'),
(48, 'ashita', 'ashita@mail.com', '$2y$10$6pB4aad7kA4dJN8yviusLus4jKXb0rwXfX5.1aWUVbAoEdnSXolEG', '2024-08-06 17:08:46', '2024-08-06 17:08:46', 'Masculino'),
(49, 'berserk', 'bers@gmai.com', '$2y$10$Qdxzd93GFl15hjsp9J2wWuTP2HJtWKkUJSP/24ACbCr7szOJqNjTi', '2024-08-06 17:16:48', '2024-08-06 17:16:48', 'Masculino'),
(50, 'kyoga', 'kio@mail.com', '$2y$10$qCrSSvuFhjTTlDM8jqFdYO1sszQDHViBvrCT2c4wVepgIp6zD/E8u', '2024-08-06 17:46:13', '2024-08-06 17:46:13', 'Masculino'),
(51, 'tom', 'tom@asd.com', '$2y$10$5dyr5DHkdPD/nfgc4pI2rOTLbfeqEnoy/VwRs3CN6C6nuzO0BiF/i', '2024-08-06 17:58:11', '2024-08-06 17:58:11', 'Masculino'),
(52, 'billy', 'bill@mail.com', '$2y$10$NAAaAlFjFjOdk9ZtcgZPfuXjTmbKynFfHmUX6rlChE32G/rDwmd1.', '2024-08-06 18:00:17', '2024-08-06 18:00:17', 'Masculino');

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
(236, 36, 2, 0, 0, '2024-08-06 15:53:03'),
(237, 36, 3, 1, 3, '2024-08-06 15:53:03'),
(238, 36, 4, 1, 0, '2024-08-06 15:53:26'),
(239, 40, 2, 1, 3, '2024-08-06 15:54:22'),
(240, 40, 3, 1, 3, '2024-08-06 15:54:22'),
(242, 42, 2, 1, 3, '2024-08-06 15:55:39'),
(243, 42, 3, 1, 3, '2024-08-06 15:55:39'),
(244, 42, 4, 1, 3, '2024-08-06 15:55:59'),
(246, 42, 5, 1, 3, '2024-08-06 15:56:35'),
(247, 42, 6, 1, 0, '2024-08-06 15:58:21'),
(248, 43, 2, 0, 0, '2024-08-06 15:59:44'),
(249, 43, 3, 1, 0, '2024-08-06 15:59:44'),
(250, 44, 2, 0, 0, '2024-08-06 16:03:37'),
(251, 44, 3, 1, 0, '2024-08-06 16:03:37'),
(252, 45, 2, 1, 3, '2024-08-06 16:10:55'),
(253, 45, 3, 0, 0, '2024-08-06 16:10:55'),
(254, 40, 1, 1, 3, '2024-08-06 16:12:43'),
(255, 40, 4, 0, 0, '2024-08-06 16:13:18'),
(256, 46, 2, 0, 0, '2024-08-06 16:14:08'),
(257, 46, 3, 1, 3, '2024-08-06 16:14:08'),
(258, 46, 4, 0, 0, '2024-08-06 16:14:27'),
(259, 46, 1, 1, 3, '2024-08-06 16:14:51'),
(260, 47, 2, 1, 3, '2024-08-06 16:16:44'),
(261, 47, 3, 1, 3, '2024-08-06 16:17:42'),
(262, 47, 1, 1, 3, '2024-08-06 16:18:01'),
(263, 47, 4, 1, 3, '2024-08-06 16:18:35'),
(264, 47, 5, 1, 0, '2024-08-06 16:19:05'),
(265, 48, 2, 1, 3, '2024-08-06 17:12:56'),
(266, 48, 3, 1, 0, '2024-08-06 17:12:56'),
(267, 50, 2, 1, 3, '2024-08-06 17:47:27'),
(268, 50, 3, 1, 3, '2024-08-06 17:47:27'),
(269, 50, 4, 1, 0, '2024-08-06 17:48:03'),
(270, 51, 2, 1, 3, '2024-08-06 17:58:30'),
(271, 51, 3, 0, 0, '2024-08-06 17:58:30'),
(272, 52, 2, 0, 0, '2024-08-06 18:00:42'),
(273, 52, 3, 0, 0, '2024-08-06 18:00:42');

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
  MODIFY `level_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=53;

--
-- AUTO_INCREMENT for table `user_levels`
--
ALTER TABLE `user_levels`
  MODIFY `user_level_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=274;

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
