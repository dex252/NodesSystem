CREATE TABLE `bonds`
(
   `id`          int(11) NOT NULL AUTO_INCREMENT,
   `nodeId`      int(11) NOT NULL,
   `parentId`    int(11) DEFAULT NULL,
   PRIMARY KEY(`id`),
   UNIQUE KEY `uq`(`nodeId`, `parentId`)
)
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARSET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;