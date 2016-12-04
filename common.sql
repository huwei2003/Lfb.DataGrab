#SELECT * from t_author where isshow=2 ORDER BY id desc limit 0,400;
#SELECT * from t_author  where IsShow=2 ORDER BY id desc limit 0,100;
#SELECT * from t_author where GroupId<>'' LIMIT 0,100;
#SELECT * from t_author where RefreshTimes>0 LIMIT 0,100;
#SELECT * from t_author where GroupId<>'' LIMIT 0,10000;
#SELECT count(1) from t_author where LENGTH(GroupId)>1;
#SELECT * from T_News where GroupId<>'' LIMIT 0,100;
#SELECT * from T_News where LENGTH(Tags)>0 LIMIT 0,100;
#SELECT * from t_author where RefreshTimes>0 LIMIT 0,100;
#SELECT count(1) from T_News where RefreshTimes>0;# order by RefreshTimes  LIMIT 0,100;
#SELECT * from T_News ORDER BY CreateTime desc LIMIT 0,20;
select count(1) from t_author;
select count(1) from t_news;
#select count(1) from t_news WHERE IsShow=2;
select count(1) from t_news WHERE IsHot=1;
#SELECT count(1) from t_author where isshow=2; 
#SELECT count(1) from t_author where IsDeal=2; 
#select * from T_News where IsShow=0 order By Id DESC limit 0,100;
#SELECT * from t_news where fromurl='http://www.toutiao.com/item/6349308181226717698/';
#SELECT * from t_news ORDER BY PubTime desc LIMIT 0,100;
#select * from t_news  ORDER BY id desc limit 0,100;
#select count(1) from t_news where DATE_ADD(CreateTime,INTERVAL 1 HOUR)>'2016-11-04 22:00:00'; 
#select count(1) from t_news where DATE_ADD(CreateTime,INTERVAL 1 HOUR)>'2016-11-04 22:00:00'; 
#select * from t_news where DATE_ADD(CreateTime,INTERVAL 1 HOUR)>'2016-11-04 22:00:00' ORDER BY CreateTime asc ; 
#select * from t_news where LastReadTimes<>CurReadTimes;

#爆文
#select * from t_news where ishot=1 order by lastdealtime DESC LIMIT 0,100 ;

#select count(1) from t_news where ishot=1

#转存上上个月数据到新表
#select * from t_news where DATE_ADD(PubTime,INTERVAL 1 MONTH)<'2016-12-01 00:00:00' ORDER BY PubTime desc limit 0,100;
#create table t_news_201611 select * from t_news where DATE_ADD(PubTime,INTERVAL 1 MONTH)<'2016-12-01 00:00:00';
#select * from t_news_201611 where DATE_ADD(PubTime,INTERVAL 1 MONTH)<'2016-12-01 00:00:00' ORDER BY PubTime asc limit 0,100;
#DELETE from t_news where DATE_ADD(PubTime,INTERVAL 1 MONTH)<'2016-12-01 00:00:00';