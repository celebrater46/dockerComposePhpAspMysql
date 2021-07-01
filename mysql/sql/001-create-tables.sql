---- drop ----
DROP TABLE IF EXISTS `test_table`;

---- create ----
create table IF not exists `test_table`
(
    `id`               INT(20) AUTO_INCREMENT,
    `name`             VARCHAR(20) NOT NULL,
    `age`              INT(3) NOT NULL,
    `created_at`       Datetime DEFAULT NULL,
    `updated_at`       Datetime DEFAULT NULL,
    PRIMARY KEY (`id`)
    ) DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

---- insert ----
insert into test_table (`name`, `age`) values (`hogetarou`, 46), (`hogehanako`, 111);