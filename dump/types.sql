CREATE TABLE `types`
(
   `id`      int(11) NOT NULL AUTO_INCREMENT,
   `name`    varchar(100) NOT NULL,
   PRIMARY KEY(`id`),
   UNIQUE KEY `uq`(`name`)
)
ENGINE = InnoDB
DEFAULT CHARSET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;