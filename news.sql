/*
Navicat MySQL Data Transfer

Source Server         : local
Source Server Version : 50624
Source Host           : localhost:3306
Source Database       : news

Target Server Type    : MYSQL
Target Server Version : 50624
File Encoding         : 65001

Date: 2016-10-23 18:09:10
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `t_author`
-- ----------------------------
DROP TABLE IF EXISTS `t_author`;
CREATE TABLE `t_author` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Author` varchar(100) NOT NULL,
  `AuthorId` varchar(50) NOT NULL,
  `LastDealTime` datetime DEFAULT NULL,
  `IsDeal` int(11) DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `Url` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Idx_Author_AuthorId` (`AuthorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_author
-- ----------------------------

-- ----------------------------
-- Table structure for `t_news`
-- ----------------------------
DROP TABLE IF EXISTS `t_news`;
CREATE TABLE `t_news` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(200) DEFAULT NULL,
  `PubTime` datetime DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `FromUrl` varchar(250) DEFAULT NULL,
  `FromSiteName` varchar(50) DEFAULT NULL,
  `Author` varchar(50) DEFAULT NULL,
  `LogoUrl` varchar(250) DEFAULT NULL,
  `LogoOriginalUrl` varchar(250) DEFAULT NULL,
  `Contents` text,
  `ImgFlag` int(11) DEFAULT NULL,
  `IsShow` int(11) DEFAULT NULL,
  `NewsTypeId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_news
-- ----------------------------
INSERT INTO `t_news` VALUES ('1', 'test', '2016-10-23 10:20:15', '2016-10-23 10:20:04', 'test', 'test', ' ', ' ', ' ', 'test', null, null, null);
INSERT INTO `t_news` VALUES ('2', 'test', '2016-10-23 00:00:00', '2016-10-23 00:00:00', '', '', '', '', '', 'test', null, null, null);
INSERT INTO `t_news` VALUES ('3', 'test', '2016-10-23 00:00:00', '2016-10-23 00:00:00', '', '', '', '', '', 'test', null, null, null);
INSERT INTO `t_news` VALUES ('4', 'test', '2016-10-23 00:00:00', '2016-10-23 00:00:00', '', '', '', '', '', 'test', null, null, null);
INSERT INTO `t_news` VALUES ('5', 'testddd', '2016-10-23 00:00:00', null, '', '', '', '', '', 'test', '1', '1', '100');
INSERT INTO `t_news` VALUES ('6', 'test12016/10/23 14:45:55', '2016-10-23 14:45:55', null, 'http://localhost', 'testsite', 'test', 'http://n.sinaimg.cn/fo/transform/20160705/pBto-fxtspsa6682768.jpg', '', '2016/10/23 14:45:55test', '0', '0', '100');
INSERT INTO `t_news` VALUES ('9', 'test12016/10/23 15:10:41', '2016-10-23 15:10:41', null, 'http://localhost', 'testsite', 'test', 'http://n.sinaimg.cn/fo/transform/20160705/pBto-fxtspsa6682768.jpg', '', '2016/10/23 15:10:40test', '0', '0', '100');
