/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50634
Source Host           : localhost:3306
Source Database       : news

Target Server Type    : MYSQL
Target Server Version : 50634
File Encoding         : 65001

Date: 2016-10-31 21:51:21
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `t_author`
-- ----------------------------
DROP TABLE IF EXISTS `t_author`;
CREATE TABLE `t_author` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Author` varchar(100) NOT NULL,
  `AuthorId` varchar(50) NOT NULL DEFAULT '',
  `LastDealTime` datetime(4) NOT NULL,
  `IsDeal` int(11) NOT NULL DEFAULT '0',
  `CreateTime` datetime NOT NULL,
  `Url` varchar(250) NOT NULL DEFAULT '',
  `IntervalMinutes` int(11) NOT NULL DEFAULT '60',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Idx_Author_AuthorId` (`AuthorId`)
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8;



-- ----------------------------
-- Table structure for `t_news`
-- ----------------------------
DROP TABLE IF EXISTS `t_news`;
CREATE TABLE `t_news` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(200) NOT NULL DEFAULT '',
  `PubTime` datetime NOT NULL,
  `CreateTime` datetime NOT NULL,
  `FromUrl` varchar(250) NOT NULL DEFAULT '',
  `FromSiteName` varchar(50) NOT NULL DEFAULT '',
  `Author` varchar(50) NOT NULL DEFAULT '',
  `LogoUrl` varchar(250) NOT NULL DEFAULT '',
  `LogoOriginalUrl` varchar(250) NOT NULL DEFAULT '',
  `Contents` tinytext NOT NULL,
  `IsDeal` int(11) NOT NULL DEFAULT '0',
  `IsShow` int(11) NOT NULL DEFAULT '0',
  `NewsTypeId` int(11) NOT NULL DEFAULT '1',
  `AuthorId` varchar(30) NOT NULL DEFAULT '',
  `Tags` varchar(100) NOT NULL DEFAULT '',
  `CurReadTimes` int(11) NOT NULL DEFAULT '0',
  `LastReadTimes` int(11) NOT NULL DEFAULT '0',
  `NewsHotClass` int(11) NOT NULL DEFAULT '7',
  `IsHot` int(11) NOT NULL DEFAULT '0',
  `LastDealTime` datetime NOT NULL,
  `IntervalMinutes` int(11) NOT NULL DEFAULT '0',
  `TotalComments` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=110 DEFAULT CHARSET=utf8;
