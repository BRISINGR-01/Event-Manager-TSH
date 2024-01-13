CREATE TABLE `tsh_user` (
  `id` CHAR(36) PRIMARY KEY,
  `branch_id` CHAR(36) NOT NULL,
  `user_name` CHAR(255) NOT NULL,
  `password` CHAR(36) NOT NULL,
  `role` ENUM ('event_organizer', 'administrator', 'student', 'student_comitee', 'guest') NOT NULL,
  `email` VARCHAR(100) NOT NULL
);

CREATE TABLE `event_participance` (
  `id` CHAR(36) PRIMARY KEY,
  `user_id` CHAR(36) NOT NULL,
  `event_id` CHAR(36) NOT NULL,
  `state` ENUM ('signed', 'present', 'late', 'missed') NOT NULL
);

CREATE TABLE `event` (
  `id` CHAR(36) PRIMARY KEY,
  `branch_id` CHAR(36) NOT NULL,
  `title` VARCHAR(200) NOT NULL,
  `description` VARCHAR(4000) NOT NULL,
  `venue` VARCHAR(255) DEFAULT NULL
);

CREATE TABLE `paid_event` (
  `timed_event_id` CHAR(36) PRIMARY KEY NOT NULL,
  `max_participants` smallint DEFAULT NULL,
  `price` smallint NOT NULL
);

CREATE TABLE `timed_event` (
  `event_id` CHAR(36) PRIMARY KEY NOT NULL,
  `start` datetime NOT NULL,
  `end` datetime DEFAULT NULL
);

CREATE TABLE `branch` (
  `id` CHAR(36) PRIMARY KEY,
  `branch_name` VARCHAR(255) NOT NULL
);

CREATE TABLE `event_suggestion` (
  `id` CHAR(36) PRIMARY KEY,
  `user_id` CHAR(36) NOT NULL,
  `branch_id` CHAR(36) NOT NULL,
  `title` VARCHAR(200) NOT NULL,
  `description` VARCHAR(4000) NOT NULL,
  `venue` VARCHAR(255) DEFAULT NULL
);

CREATE TABLE `image_user_tag` (
  `image_path` CHAR(36) NOT NULL,
  `user_id` CHAR(36) NOT NULL
);

CREATE TABLE `image_event_tag` (
  `image_path` CHAR(36) NOT NULL,
  `event_id` CHAR(36) NOT NULL
);

ALTER TABLE `event_participance` ADD FOREIGN KEY (`user_id`) REFERENCES `tsh_user` (`id`);

ALTER TABLE `event_participance` ADD FOREIGN KEY (`event_id`) REFERENCES `event` (`id`);

ALTER TABLE `tsh_user` ADD FOREIGN KEY (`branch_id`) REFERENCES `branch` (`id`);

ALTER TABLE `timed_event` ADD FOREIGN KEY (`event_id`) REFERENCES `event` (`id`);

ALTER TABLE `paid_event` ADD FOREIGN KEY (`timed_event_id`) REFERENCES `timed_event` (`event_id`);

ALTER TABLE `event` ADD FOREIGN KEY (`branch_id`) REFERENCES `branch` (`id`);

ALTER TABLE `event_suggestion` ADD FOREIGN KEY (`user_id`) REFERENCES `tsh_user` (`id`);

ALTER TABLE `event_suggestion` ADD FOREIGN KEY (`branch_id`) REFERENCES `branch` (`id`);

ALTER TABLE `image_user_tag` ADD FOREIGN KEY (`user_id`) REFERENCES `tsh_user` (`id`);

ALTER TABLE `image_event_tag` ADD FOREIGN KEY (`event_id`) REFERENCES `event` (`id`);
