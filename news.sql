/*
Navicat MySQL Data Transfer

Source Server         : ali-rds-01
Source Server Version : 50616
Source Host           : rm-wz937wng991j20v4j.mysql.rds.aliyuncs.com:3306
Source Database       : news

Target Server Type    : MYSQL
Target Server Version : 50616
File Encoding         : 65001

Date: 2016-11-18 21:38:09
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `t_author`
-- ----------------------------
DROP TABLE IF EXISTS `t_author`;
CREATE TABLE `t_author` (
`Id`  int(11) NOT NULL AUTO_INCREMENT ,
`Author`  varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL ,
`AuthorId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' ,
`LastDealTime`  datetime(4) NOT NULL ,
`IsDeal`  int(11) NOT NULL DEFAULT 0 ,
`CreateTime`  datetime NOT NULL ,
`Url`  varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' ,
`IntervalMinutes`  int(11) NOT NULL DEFAULT 60 ,
`IsShow`  int(11) NOT NULL DEFAULT 0 ,
`RefreshTimes`  int(11) NOT NULL DEFAULT 0 ,
`GroupId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0' ,
PRIMARY KEY (`Id`),
UNIQUE INDEX `Idx_Author_AuthorId` (`AuthorId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
AUTO_INCREMENT=23670

;

-- ----------------------------
-- Table structure for `t_news`
-- ----------------------------
DROP TABLE IF EXISTS `t_news`;
CREATE TABLE `t_news` (
`Id`  int(11) NOT NULL AUTO_INCREMENT ,
`Title`  varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' ,
`PubTime`  datetime NOT NULL ,
`CreateTime`  datetime NOT NULL ,
`FromUrl`  varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' ,
`FromSiteName`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' ,
`Author`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' ,
`LogoUrl`  varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' ,
`LogoOriginalUrl`  varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' ,
`Contents`  tinytext CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL ,
`IsDeal`  int(11) NOT NULL DEFAULT 0 ,
`IsShow`  int(11) NOT NULL DEFAULT 0 ,
`NewsTypeId`  int(11) NOT NULL DEFAULT 1 ,
`AuthorId`  varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' ,
`Tags`  varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' ,
`CurReadTimes`  int(11) NOT NULL DEFAULT 0 ,
`LastReadTimes`  int(11) NOT NULL DEFAULT 0 ,
`NewsHotClass`  int(11) NOT NULL DEFAULT 7 ,
`IsHot`  int(11) NOT NULL DEFAULT 0 ,
`LastDealTime`  datetime NOT NULL ,
`IntervalMinutes`  int(11) NOT NULL DEFAULT 0 ,
`TotalComments`  int(11) NOT NULL DEFAULT 0 ,
`RefreshTimes`  int(11) NOT NULL DEFAULT 0 ,
`GroupId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0' ,
PRIMARY KEY (`Id`),
UNIQUE INDEX `idx_news_authorid_title` (`Title`, `AuthorId`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
AUTO_INCREMENT=1394285

;

-- ----------------------------
-- Table structure for `t_proxy`
-- ----------------------------
DROP TABLE IF EXISTS `t_proxy`;
CREATE TABLE `t_proxy` (
`Id`  int(11) NOT NULL ,
`Ip`  varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL ,
`Port`  int(11) NOT NULL ,
`CreateTime`  datetime NOT NULL ,
`FailTimes`  int(11) NULL DEFAULT NULL ,
INDEX `idx_proxy_ip_port` (`Ip`, `Port`) USING BTREE 
)
ENGINE=InnoDB
DEFAULT CHARACTER SET=utf8 COLLATE=utf8_general_ci
AUTO_INCREMENT=1

;

-- ----------------------------
-- Auto increment value for `t_author`
-- ----------------------------
ALTER TABLE `t_author` AUTO_INCREMENT=23670;

-- ----------------------------
-- Auto increment value for `t_news`
-- ----------------------------
ALTER TABLE `t_news` AUTO_INCREMENT=1394285;

-- ----------------------------
-- Auto increment value for `t_proxy`
-- ----------------------------
ALTER TABLE `t_proxy` AUTO_INCREMENT=1;
