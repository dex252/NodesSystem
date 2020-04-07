CREATE TABLE `units`
(
   `id`                     int(11) NOT NULL AUTO_INCREMENT,
   `type`                   varchar(100) NOT NULL,
   `name`                   varchar(100)
                            CHARACTER SET utf8mb4
                            COLLATE utf8mb4_0900_ai_ci
                            DEFAULT NULL,
   `position`               varchar(100)
                            CHARACTER SET utf8mb4
                            COLLATE utf8mb4_0900_ai_ci
                            DEFAULT NULL,
   `fullname`               varchar(100)
                            CHARACTER SET utf8mb4
                            COLLATE utf8mb4_0900_ai_ci
                            DEFAULT NULL,
   `dependence`             varchar(100)
                            CHARACTER SET utf8mb4
                            COLLATE utf8mb4_0900_ai_ci
                            DEFAULT NULL,
   `curatorial_position`    varchar(100)
                            CHARACTER SET utf8mb4
                            COLLATE utf8mb4_0900_ai_ci
                            DEFAULT NULL,
   `curatorial_fullname`    varchar(100)
                            CHARACTER SET utf8mb4
                            COLLATE utf8mb4_0900_ai_ci
                            DEFAULT NULL,
   `date_create`            date DEFAULT NULL,
   `base_create`            varchar(100)
                            CHARACTER SET utf8mb4
                            COLLATE utf8mb4_0900_ai_ci
                            DEFAULT NULL,
   `count_bets`             int(11) DEFAULT NULL,
   `count_closed_bets`      int(11) DEFAULT NULL,
   `date_close`             date DEFAULT NULL,
   `base_close`             varchar(100)
                            CHARACTER SET utf8mb4
                            COLLATE utf8mb4_0900_ai_ci
                            DEFAULT NULL,
   `availability`           varchar(100)
                            CHARACTER SET utf8mb4
                            COLLATE utf8mb4_0900_ai_ci
                            DEFAULT NULL,
   `remark`                 varchar(300)
                            CHARACTER SET utf8mb4
                            COLLATE utf8mb4_0900_ai_ci
                            DEFAULT NULL,
   PRIMARY KEY(`id`),
   KEY `type` (`type`),
   CONSTRAINT `units_ibfk_1` FOREIGN KEY(`type`)
   REFERENCES `types`(`name`) ON DELETE RESTRICT ON UPDATE RESTRICT
)
ENGINE = InnoDB
DEFAULT CHARSET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;