-- phpMyAdmin SQL Dump
-- version 4.0.10
-- http://www.phpmyadmin.net
--
-- Хост: 127.0.0.1:3306
-- Время создания: Окт 18 2014 г., 19:35
-- Версия сервера: 5.5.35-log
-- Версия PHP: 5.3.27

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- База данных: `sts`
--

-- --------------------------------------------------------

--
-- Структура таблицы `asset`
--

CREATE TABLE IF NOT EXISTS `asset` (
  `id` int(20) unsigned NOT NULL AUTO_INCREMENT,
  `value` int(11) NOT NULL,
  `asset_date` datetime NOT NULL,
  `enterprise_id` int(20) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `enterprise_id` (`enterprise_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Дамп данных таблицы `asset`
--

INSERT INTO `asset` (`id`, `value`, `asset_date`, `enterprise_id`) VALUES
(1, 0, '2014-05-12 00:00:00', 1);

-- --------------------------------------------------------

--
-- Структура таблицы `character`
--

CREATE TABLE IF NOT EXISTS `character` (
  `id` int(20) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(40) NOT NULL,
  `gender` varchar(1) NOT NULL,
  `level` int(11) NOT NULL,
  `id_enterprise` int(11) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `Id` (`id`),
  UNIQUE KEY `id_2` (`id`),
  KEY `FK_character_enterprise` (`id_enterprise`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Дамп данных таблицы `character`
--

INSERT INTO `character` (`id`, `title`, `gender`, `level`, `id_enterprise`) VALUES
(1, 'Rita', 'f', 0, NULL);

-- --------------------------------------------------------

--
-- Структура таблицы `company`
--

CREATE TABLE IF NOT EXISTS `company` (
  `id` int(20) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(50) NOT NULL,
  `share` double NOT NULL,
  `period` int(11) unsigned zerofill NOT NULL,
  `investment` decimal(10,0) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Дамп данных таблицы `company`
--

INSERT INTO `company` (`id`, `title`, `share`, `period`, `investment`) VALUES
(1, 'DigiSoft', 0, 00000000000, '0');

-- --------------------------------------------------------

--
-- Структура таблицы `competitor`
--

CREATE TABLE IF NOT EXISTS `competitor` (
  `id` int(20) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(45) NOT NULL,
  `success_rate` double NOT NULL,
  `enterprise_id` int(20) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `comp_enterprise_id` (`enterprise_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Дамп данных таблицы `competitor`
--

INSERT INTO `competitor` (`id`, `title`, `success_rate`, `enterprise_id`) VALUES
(1, '', 0, 1);

-- --------------------------------------------------------

--
-- Структура таблицы `document`
--

CREATE TABLE IF NOT EXISTS `document` (
  `id` int(20) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(100) NOT NULL,
  `type` varchar(1) NOT NULL,
  `path` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Дамп данных таблицы `document`
--

INSERT INTO `document` (`id`, `title`, `type`, `path`) VALUES
(1, 'testdoc', '', 0);

-- --------------------------------------------------------

--
-- Структура таблицы `employee`
--

CREATE TABLE IF NOT EXISTS `employee` (
  `id` int(20) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(45) NOT NULL,
  `qualification` double NOT NULL,
  `salary` decimal(10,0) NOT NULL,
  `role_id` int(20) unsigned NOT NULL,
  `enterprise_id` int(20) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `enterprise_id` (`enterprise_id`),
  KEY `role_id` (`role_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Дамп данных таблицы `employee`
--

INSERT INTO `employee` (`id`, `title`, `qualification`, `salary`, `role_id`, `enterprise_id`) VALUES
(1, 'Rab', 1, '500', 1, 1),
(2, 'Rab2', 2, '400', 2, 1),
(3, 'Rab3', 3, '400', 3, 1),
(4, 'Rab4', 4, '350', 4, 1),
(5, 'Rab5', 2, '600', 1, 1);

-- --------------------------------------------------------

--
-- Структура таблицы `enterprise`
--

CREATE TABLE IF NOT EXISTS `enterprise` (
  `id` int(20) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(50) NOT NULL,
  `balance` decimal(10,0) NOT NULL,
  `stationary` double NOT NULL,
  `type` tinyint(4) DEFAULT NULL,
  `taxation_id` int(20) unsigned NOT NULL,
  UNIQUE KEY `шв` (`id`),
  KEY `character_id` (`taxation_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Дамп данных таблицы `enterprise`
--

INSERT INTO `enterprise` (`id`, `title`, `balance`, `stationary`, `type`, `taxation_id`) VALUES
(1, 'digiinterprise', '0', 0, 3, 1);

-- --------------------------------------------------------

--
-- Структура таблицы `enterprise_docs`
--

CREATE TABLE IF NOT EXISTS `enterprise_docs` (
  `document_id` int(20) unsigned NOT NULL,
  `availability` tinyint(1) NOT NULL,
  `is_active` tinyint(1) NOT NULL,
  `expiration_date` date NOT NULL,
  `enterprise_id` int(20) unsigned NOT NULL,
  KEY `document_id` (`document_id`),
  KEY `enterprise_id` (`enterprise_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Дамп данных таблицы `enterprise_docs`
--

INSERT INTO `enterprise_docs` (`document_id`, `availability`, `is_active`, `expiration_date`, `enterprise_id`) VALUES
(1, 0, 0, '0000-00-00', 1),
(1, 1, 0, '0000-00-00', 1),
(1, 1, 0, '0000-00-00', 1);

-- --------------------------------------------------------

--
-- Структура таблицы `enterprise_equipment`
--

CREATE TABLE IF NOT EXISTS `enterprise_equipment` (
  `enterprise_id` int(20) unsigned NOT NULL AUTO_INCREMENT,
  `equipment_id` int(20) unsigned DEFAULT '0',
  `purchase_date` date DEFAULT '0000-00-00',
  `quantity` int(11) DEFAULT '0',
  `lease_term` int(11) unsigned zerofill DEFAULT '00000000000',
  `isRunning` tinyint(1) DEFAULT '0',
  KEY `enterprise1_id` (`enterprise_id`),
  KEY `equipment2_id` (`equipment_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Дамп данных таблицы `enterprise_equipment`
--

INSERT INTO `enterprise_equipment` (`enterprise_id`, `equipment_id`, `purchase_date`, `quantity`, `lease_term`, `isRunning`) VALUES
(1, 1, '0000-00-00', 0, 00000000000, 0);

-- --------------------------------------------------------

--
-- Структура таблицы `equipment`
--

CREATE TABLE IF NOT EXISTS `equipment` (
  `id` int(20) unsigned NOT NULL AUTO_INCREMENT,
  `title` double NOT NULL,
  `price` decimal(10,0) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Дамп данных таблицы `equipment`
--

INSERT INTO `equipment` (`id`, `title`, `price`) VALUES
(1, 0, '0');

-- --------------------------------------------------------

--
-- Структура таблицы `human_resourses`
--

CREATE TABLE IF NOT EXISTS `human_resourses` (
  `id` int(20) unsigned NOT NULL AUTO_INCREMENT,
  `role_id` int(20) unsigned NOT NULL,
  `hours` int(11) NOT NULL,
  `project_id` int(20) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `role_id` (`role_id`),
  KEY `project_id` (`project_id`),
  KEY `project_id_2` (`project_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Дамп данных таблицы `human_resourses`
--

INSERT INTO `human_resourses` (`id`, `role_id`, `hours`, `project_id`) VALUES
(1, 1, 0, 1);

-- --------------------------------------------------------

--
-- Структура таблицы `product`
--

CREATE TABLE IF NOT EXISTS `product` (
  `id` int(11) NOT NULL,
  `title` varchar(100) NOT NULL,
  `price` decimal(10,0) NOT NULL,
  `quality` double NOT NULL,
  `prime_cost` decimal(10,0) NOT NULL,
  `project_id` int(20) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `project_id` (`project_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Дамп данных таблицы `product`
--

INSERT INTO `product` (`id`, `title`, `price`, `quality`, `prime_cost`, `project_id`) VALUES
(1, '', '0', 0, '0', 1);

-- --------------------------------------------------------

--
-- Структура таблицы `project`
--

CREATE TABLE IF NOT EXISTS `project` (
  `id` int(20) unsigned NOT NULL AUTO_INCREMENT,
  `planned_begin_date` date NOT NULL,
  `planned_end_date` date NOT NULL,
  `real_begin_date` date NOT NULL,
  `real_end_date` date NOT NULL,
  `state` int(11) NOT NULL,
  `stated_budget` decimal(10,0) NOT NULL,
  `expenditures` decimal(10,0) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Дамп данных таблицы `project`
--

INSERT INTO `project` (`id`, `planned_begin_date`, `planned_end_date`, `real_begin_date`, `real_end_date`, `state`, `stated_budget`, `expenditures`) VALUES
(1, '2014-05-08', '2014-06-08', '2014-05-12', '2014-06-10', 0, '0', '0'),
(2, '2014-05-08', '2014-05-08', '2014-05-08', '2014-05-08', 1, '1000', '0');

-- --------------------------------------------------------

--
-- Структура таблицы `project_stage`
--

CREATE TABLE IF NOT EXISTS `project_stage` (
  `project_id` int(10) unsigned DEFAULT NULL,
  `conception_hours` int(10) DEFAULT NULL,
  `programming_hours` int(10) DEFAULT NULL,
  `testing_hours` int(10) DEFAULT NULL,
  `design_hours` int(10) DEFAULT NULL,
  `conception_done` double DEFAULT NULL,
  `programming_done` double DEFAULT NULL,
  `testing_done` double DEFAULT NULL,
  `design_done` double DEFAULT NULL,
  KEY `project_stages` (`project_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Дамп данных таблицы `project_stage`
--

INSERT INTO `project_stage` (`project_id`, `conception_hours`, `programming_hours`, `testing_hours`, `design_hours`, `conception_done`, `programming_done`, `testing_done`, `design_done`) VALUES
(1, NULL, NULL, NULL, NULL, 3, 2, 3, NULL);

-- --------------------------------------------------------

--
-- Структура таблицы `purchase`
--

CREATE TABLE IF NOT EXISTS `purchase` (
  `id` int(20) unsigned NOT NULL,
  `equipment_id` int(20) unsigned NOT NULL,
  `quantity` int(11) NOT NULL,
  `asset_id` int(20) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `asset_id` (`asset_id`),
  KEY `equipment_id` (`equipment_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Дамп данных таблицы `purchase`
--

INSERT INTO `purchase` (`id`, `equipment_id`, `quantity`, `asset_id`) VALUES
(1, 1, 0, 1);

-- --------------------------------------------------------

--
-- Структура таблицы `revenue`
--

CREATE TABLE IF NOT EXISTS `revenue` (
  `id` int(11) NOT NULL,
  `revenue_date` date NOT NULL,
  `value` decimal(10,0) NOT NULL,
  `enterprise_id` int(20) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `enterprise_id` (`enterprise_id`),
  KEY `enterprise_id_2` (`enterprise_id`),
  KEY `enterprise_id_3` (`enterprise_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Дамп данных таблицы `revenue`
--

INSERT INTO `revenue` (`id`, `revenue_date`, `value`, `enterprise_id`) VALUES
(1, '0000-00-00', '0', 1);

-- --------------------------------------------------------

--
-- Структура таблицы `role`
--

CREATE TABLE IF NOT EXISTS `role` (
  `id` int(20) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(45) NOT NULL,
  `min_salary` decimal(10,0) NOT NULL,
  `max_salary` decimal(10,0) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `id_2` (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Дамп данных таблицы `role`
--

INSERT INTO `role` (`id`, `title`, `min_salary`, `max_salary`) VALUES
(1, 'Analyst', '150', '400'),
(2, 'Programmer', '300', '1500'),
(3, 'Tester', '200', '600'),
(4, 'Designer', '50', '200');

-- --------------------------------------------------------

--
-- Структура таблицы `salary_payment`
--

CREATE TABLE IF NOT EXISTS `salary_payment` (
  `id` int(20) unsigned NOT NULL AUTO_INCREMENT,
  `date` date NOT NULL DEFAULT '0000-00-00',
  `hours_worked` int(10) unsigned DEFAULT '0',
  `salary` decimal(10,0) unsigned DEFAULT '0',
  `employee_id` int(20) unsigned DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `employee_salary_payment` (`employee_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=8 ;

--
-- Дамп данных таблицы `salary_payment`
--

INSERT INTO `salary_payment` (`id`, `date`, `hours_worked`, `salary`, `employee_id`) VALUES
(1, '2014-05-06', 1, '200', 1),
(2, '2014-05-15', 2, '100', 2),
(3, '2014-05-15', 2, '300', 3),
(4, '2014-05-15', 1, '400', 4),
(5, '2014-05-15', 5, '200', 1),
(6, '2014-05-15', 2, '500', 5);

-- --------------------------------------------------------

--
-- Структура таблицы `service`
--

CREATE TABLE IF NOT EXISTS `service` (
  `id` int(20) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(45) NOT NULL,
  `price` decimal(10,0) NOT NULL,
  `period` int(11) NOT NULL,
  `effectiveness` decimal(10,0) unsigned NOT NULL,
  `asset_id` int(20) unsigned NOT NULL,
  `company_id` int(20) unsigned NOT NULL,
  `periods_paid` int(20) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `company_id` (`company_id`),
  KEY `asset_id` (`asset_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Дамп данных таблицы `service`
--

INSERT INTO `service` (`id`, `title`, `price`, `period`, `effectiveness`, `asset_id`, `company_id`, `periods_paid`) VALUES
(1, '', '0', 0, '0', 1, 1, 0);

-- --------------------------------------------------------

--
-- Структура таблицы `taxation`
--

CREATE TABLE IF NOT EXISTS `taxation` (
  `id` int(20) unsigned NOT NULL AUTO_INCREMENT,
  `taxation_group` int(11) NOT NULL,
  `max_revenue` decimal(10,0) NOT NULL,
  `max_employee` int(11) NOT NULL,
  `VAT` double NOT NULL,
  `income_duty` double NOT NULL,
  `type` tinyint(1) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Дамп данных таблицы `taxation`
--

INSERT INTO `taxation` (`id`, `taxation_group`, `max_revenue`, `max_employee`, `VAT`, `income_duty`, `type`) VALUES
(1, 1, '2', 5, 3, 7, 0);

-- --------------------------------------------------------

--
-- Структура таблицы `team_member`
--

CREATE TABLE IF NOT EXISTS `team_member` (
  `employee_id` int(20) unsigned NOT NULL,
  `project_id` int(20) unsigned NOT NULL,
  KEY `project_id` (`project_id`),
  KEY `employee_id` (`employee_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Дамп данных таблицы `team_member`
--

INSERT INTO `team_member` (`employee_id`, `project_id`) VALUES
(1, 1),
(2, 1),
(3, 1),
(4, 1),
(5, 1);

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `asset`
--
ALTER TABLE `asset`
  ADD CONSTRAINT `FK_asset_enterprise` FOREIGN KEY (`enterprise_id`) REFERENCES `enterprise` (`id`);

--
-- Ограничения внешнего ключа таблицы `character`
--
ALTER TABLE `character`
  ADD CONSTRAINT `FK_character_enterprise` FOREIGN KEY (`id_enterprise`) REFERENCES `enterprise` (`id`);

--
-- Ограничения внешнего ключа таблицы `competitor`
--
ALTER TABLE `competitor`
  ADD CONSTRAINT `comp_enterprise_id` FOREIGN KEY (`enterprise_id`) REFERENCES `enterprise` (`id`);

--
-- Ограничения внешнего ключа таблицы `employee`
--
ALTER TABLE `employee`
  ADD CONSTRAINT `employee_role_id` FOREIGN KEY (`role_id`) REFERENCES `role` (`id`),
  ADD CONSTRAINT `enterprise_id` FOREIGN KEY (`enterprise_id`) REFERENCES `enterprise` (`id`);

--
-- Ограничения внешнего ключа таблицы `enterprise`
--
ALTER TABLE `enterprise`
  ADD CONSTRAINT `enterprise_taxation_id` FOREIGN KEY (`taxation_id`) REFERENCES `taxation` (`id`);

--
-- Ограничения внешнего ключа таблицы `enterprise_docs`
--
ALTER TABLE `enterprise_docs`
  ADD CONSTRAINT `doc_enterprise_id` FOREIGN KEY (`enterprise_id`) REFERENCES `enterprise` (`id`),
  ADD CONSTRAINT `doc_id` FOREIGN KEY (`document_id`) REFERENCES `document` (`id`);

--
-- Ограничения внешнего ключа таблицы `enterprise_equipment`
--
ALTER TABLE `enterprise_equipment`
  ADD CONSTRAINT `enterprise1_id` FOREIGN KEY (`enterprise_id`) REFERENCES `enterprise` (`id`),
  ADD CONSTRAINT `equipment2_id` FOREIGN KEY (`equipment_id`) REFERENCES `equipment` (`id`);

--
-- Ограничения внешнего ключа таблицы `human_resourses`
--
ALTER TABLE `human_resourses`
  ADD CONSTRAINT `hr_role_id` FOREIGN KEY (`role_id`) REFERENCES `role` (`id`),
  ADD CONSTRAINT `res_proj_id` FOREIGN KEY (`project_id`) REFERENCES `project` (`id`);

--
-- Ограничения внешнего ключа таблицы `product`
--
ALTER TABLE `product`
  ADD CONSTRAINT `product_project_id` FOREIGN KEY (`project_id`) REFERENCES `project` (`id`);

--
-- Ограничения внешнего ключа таблицы `project_stage`
--
ALTER TABLE `project_stage`
  ADD CONSTRAINT `project_stages` FOREIGN KEY (`project_id`) REFERENCES `project` (`id`);

--
-- Ограничения внешнего ключа таблицы `purchase`
--
ALTER TABLE `purchase`
  ADD CONSTRAINT `FK_purchase_equipment` FOREIGN KEY (`equipment_id`) REFERENCES `equipment` (`id`),
  ADD CONSTRAINT `purchase_asset_id` FOREIGN KEY (`asset_id`) REFERENCES `asset` (`id`);

--
-- Ограничения внешнего ключа таблицы `revenue`
--
ALTER TABLE `revenue`
  ADD CONSTRAINT `enterprise_rev_id` FOREIGN KEY (`enterprise_id`) REFERENCES `enterprise` (`id`);

--
-- Ограничения внешнего ключа таблицы `salary_payment`
--
ALTER TABLE `salary_payment`
  ADD CONSTRAINT `employee_salary_payment` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`id`);

--
-- Ограничения внешнего ключа таблицы `service`
--
ALTER TABLE `service`
  ADD CONSTRAINT `service_asset_id` FOREIGN KEY (`asset_id`) REFERENCES `asset` (`id`),
  ADD CONSTRAINT `service_company_id` FOREIGN KEY (`company_id`) REFERENCES `company` (`id`);

--
-- Ограничения внешнего ключа таблицы `team_member`
--
ALTER TABLE `team_member`
  ADD CONSTRAINT `project_employee_id` FOREIGN KEY (`project_id`) REFERENCES `project` (`id`),
  ADD CONSTRAINT `team_employee_id` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`id`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
