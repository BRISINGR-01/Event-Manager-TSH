CREATE TABLE `user` (
  `id` CHAR(36) PRIMARY KEY,
  `branch_id` CHAR(36) NOT NULL,
  `user_name` CHAR(255) NOT NULL,
  `password` CHAR(36) NOT NULL,
  `role` ENUM ('event_organizer', 'administrator', 'student', 'student_comitee', 'guest') NOT NULL,
  `email` NVARCHAR(100)
);

CREATE TABLE `event_participance` (
  `id` CHAR(36) PRIMARY KEY,
  `user_id` CHAR(36) NOT NULL,
  `event_id` CHAR(36) NOT NULL,
  `state` ENUM ('signed', 'present', 'late', 'missed') NOT NULL
);

CREATE TABLE `event` (
  `id` CHAR(36) PRIMARY KEY NOT NULL,
  `branch_id` CHAR(36) NOT NULL,
  `title` NVARCHAR(200) NOT NULL,
  `description` NVARCHAR(4000) NOT NULL,
  `venue` NVARCHAR(255) DEFAULT NULL
);

CREATE TABLE `paid_event` (
  `event_id` CHAR(36) PRIMARY KEY NOT NULL,
  `max_participants` smallint DEFAULT NULL,
  `price` smallint NOT NULL
);

CREATE TABLE `timed_event` (
  `id` CHAR(36) PRIMARY KEY NOT NULL,
  `event_id` CHAR(36) PRIMARY KEY NOT NULL,
  `start` datetime NOT NULL,
  `end` datetime DEFAULT NULL
);

CREATE TABLE `branch` (
  `id` CHAR(36) PRIMARY KEY,
  `branch_name` NVARCHAR(255) NOT NULL
);

ALTER TABLE `event_participance` ADD FOREIGN KEY (`user_id`) REFERENCES `user` (`id`);

ALTER TABLE `event_participance` ADD FOREIGN KEY (`event_id`) REFERENCES `event` (`id`);

ALTER TABLE `user` ADD FOREIGN KEY (`branch_id`) REFERENCES `branch` (`id`);

ALTER TABLE `event` ADD FOREIGN KEY (`id`) REFERENCES `timed_event` (`event_id`);

ALTER TABLE `paid_event` ADD FOREIGN KEY (`event_id`) REFERENCES `event` (`id`);

ALTER TABLE `event` ADD FOREIGN KEY (`branch_id`) REFERENCES `branch` (`id`);
