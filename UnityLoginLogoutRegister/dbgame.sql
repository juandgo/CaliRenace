-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jul 21, 2024 at 07:43 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

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
  `difficulty` varchar(20) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `username`, `email`, `password`, `created_at`, `updated_at`) VALUES
(1, 'Juan97', 'juan@gmail.com', '$2y$10$Z.CMLkd9njrFkaP5cf9G/u6sH3Opmf6JxrOp.FYRrpo7ErbVpOmWe', '2024-07-02 04:05:06', '2024-07-07 03:27:02'),
(2, 'cam', 'cam', '$2y$10$hq4brudOq5WUJ5vfYhJPnOJGBAUehpJwyxWstdc5vAXNCdD7CyVDK', '2024-07-02 04:29:54', '2024-07-02 04:29:54'),
(3, 'Jose', 'jdgo', '$2y$10$UYNDMg47fUATIMECtpc4aO/R8uKTqh.ZW5fEeqDgjbMh/BZ1nbNWe', '2024-07-07 03:57:40', '2024-07-07 03:57:40'),
(4, 'pedro', 'pedro', '$2y$10$Y2N6gIZiWPAbjrgXcagzFuR6YgiOo4RCkPGz1CoDryCbc35WlDP4e', '2024-07-09 00:26:22', '2024-07-09 00:26:22'),
(5, 'camilo', 'cam@gamil.com', '$2y$10$MsGnhd9W0aboKzpcgbcSqe4VDJ6Xn9qwu11tsC/PS9W9xKAipFIsy', '2024-07-16 01:47:54', '2024-07-16 01:47:54'),
(6, 'pepa', 'pepa@cali.gov', '$2y$10$t6lg62Y5rV6H2JgNhk7ynOMI.kZjPpBe6iHGj4GM87cDBV/EL/ey.', '2024-07-16 01:57:03', '2024-07-16 01:57:03'),
(7, 'sara', 'sara@develotech.co', '$2y$10$AjN/AvinG.3FuxVoMc4vpeSTa4mTW4J3lkxDMMyKzD8o9OEqLVfCy', '2024-07-16 02:21:50', '2024-07-16 02:21:50'),
(8, 'dani', 'dani@dani.com', '$2y$10$kuHlvME.mfYSCU3gizO/2eJECyQSsdsce2pH.kzHYr5c1aOrsqEvO', '2024-07-16 02:23:41', '2024-07-16 02:23:41'),
(9, 'set', 'set@see.com', '$2y$10$wfkj3YJoUwkxhTFGm0mJ5el3Fg1wL7aA2sMuZggufuOPqOd7yXSya', '2024-07-16 02:24:51', '2024-07-16 02:24:51'),
(10, 'jk', 'jk@jk.com', '$2y$10$QUM8.hRsDoyeJwHVqM4tj.kZ1OGj5dCGvKZnK9QmreqWg7B8p1gM6', '2024-07-16 02:26:01', '2024-07-16 02:26:01'),
(11, 'df', 'df@df.com', '$2y$10$DEAA1n3bzMH4R11VP1oQj.hQvvnzWLlN0l3eKsOWha6KeJ6Pp7DKS', '2024-07-16 02:40:53', '2024-07-16 02:40:53'),
(12, 'jd', 'jd@jd.com', '$2y$10$GVOrj4zx84QvKPjYNntLTOwdNK.q.utNuMWZaDKpx56MvLQrW4LKS', '2024-07-16 02:52:08', '2024-07-16 02:52:08'),
(13, '1', '1@1.com', '$2y$10$LZJYtD8lGhy0G4z1ead1UuVDMOEolHtCrEt3LXvEX0MidTVGc88Y.', '2024-07-16 03:12:32', '2024-07-16 03:12:32'),
(14, 'mateo', 'mateo@mat.com', '$2y$10$2bTET.HNBX/ajpV/7MTY0uYCKcnZWxIaZcsrzuAQ5qdDM/b1DhHpG', '2024-07-16 03:18:58', '2024-07-16 03:18:58'),
(15, 'juan', 'juan@co.oc', '$2y$10$fYMUSSK6rL5QBLw6OxOFU.vwRpa1ytiXEDt0jmTefYM/RTXcVDZD2', '2024-07-16 03:31:23', '2024-07-16 03:31:23'),
(16, 'pepe', 'pe@gmail.com', '$2y$10$6wm8M.6XyzWhzS/B6ujGj.6w79CiXJT/2AX3kSIeKFj1s8Il3Bzwa', '2024-07-16 03:33:46', '2024-07-16 03:33:46'),
(17, 'pop', 'pop@mail.com', '$2y$10$bgFHtJGvIGBgiSkuCH4ZOOahpBFJhjQ9R17PRFaFaqe9niem8MMOm', '2024-07-16 03:35:06', '2024-07-16 03:35:06');

-- --------------------------------------------------------

--
-- Table structure for table `user_levels`
--

CREATE TABLE `user_levels` (
  `user_level_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `level_id` int(11) NOT NULL,
  `completion_status` varchar(20) NOT NULL,
  `score` int(11) NOT NULL,
  `completed_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
  MODIFY `level_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `user_levels`
--
ALTER TABLE `user_levels`
  MODIFY `user_level_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

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
