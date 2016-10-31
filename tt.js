riot.tag("nav", '<div class="nav"> <ul class="y-box nav-list" ga_event="mh_channel"> <li each="{options.navItem}" class="y-left nav-item"> <a class="nav-link {active: location.pathname == url}" href="{url}" target="_blank" ga_event="mh_channel_{en}">{name}</a> </li> <li class="y-left nav-item nav-more"> <a class="nav-link" href="javascript:;"> 鏇村<i class="y-icon icon-more"></i> </a> <div class="nav-layer"> <ul class="nav-more-list"> <li each="{options.navMore}" class="nav-more-item"> <a href="{url}" target="_blank" ga_event="mh_channel_{en}">{name}</a> </li> </ul> </div> </li> </ul></div>',
function() {
    this.options = {
        navItem: [{
            name: "鎺ㄨ崘",
            url: "/",
            en: "recommend"
        },
        {
            name: "鐑偣",
            url: "/news_hot/",
            en: "hot"
        },
        {
            name: "瑙嗛",
            url: "/video/",
            en: "video"
        },
        {
            name: "鍥剧墖",
            url: "/news_image/",
            en: "image"
        },
        {
            name: "绀句細",
            url: "/news_society/",
            en: "society"
        },
        {
            name: "濞变箰",
            url: "/news_entertainment/",
            en: "entertainment"
        },
        {
            name: "绉戞妧",
            url: "/news_tech/",
            en: "tech"
        },
        {
            name: "姹借溅",
            url: "/news_car/",
            en: "car"
        },
        {
            name: "浣撹偛",
            url: "/news_sports/",
            en: "sports"
        },
        {
            name: "璐㈢粡",
            url: "/news_finance/",
            en: "finance"
        },
        {
            name: "鍐涗簨",
            url: "/news_military/",
            en: "military"
        },
        {
            name: "鍥介檯",
            url: "/news_world/",
            en: "world"
        },
        {
            name: "鏃跺皻",
            url: "/news_fashion/",
            en: "fashion"
        },
        {
            name: "鏃呮父",
            url: "/news_travel/",
            en: "travel"
        }],
        navMore: [{
            name: "鎺㈢储",
            url: "/news_discovery/",
            en: "discovery"
        },
        {
            name: "鑲插効",
            url: "/news_baby/",
            en: "baby"
        },
        {
            name: "鍏荤敓",
            url: "/news_regimen/",
            en: "regimen"
        },
        {
            name: "鏁呬簨",
            url: "/news_story/",
            en: "story"
        },
        {
            name: "缇庢枃",
            url: "/news_essay/",
            en: "essay"
        },
        {
            name: "娓告垙",
            url: "/news_game/",
            en: "game"
        },
        {
            name: "鍘嗗彶",
            url: "/news_history/",
            en: "history"
        },
        {
            name: "缇庨",
            url: "/news_food/",
            en: "food"
        }]
    }
}),
riot.tag("weather", '<div class="w-header"> <span class="y-icon icon-location" onclick="{changeLocation}" ga_event="mh_change_city">&nbsp;{options.city}</span> <span class="wind">{options.wind}</span> <span class="air" riot-style="background: {options.level}">{options.air}</span> </div> <ul class="y-box days-weather {show: options.weather_show}"> <li class="y-left day"> <span class="title">浠婂ぉ</span> <div title="{options.weather.current_condition}" class="weather-icon weather-icon-{options.weather.weather_icon_id}"></div> <span class="temperature"> <em>{options.weather.low_temperature}</em>&#176; &#47; <em>{options.weather.high_temperature}</em>&#176; </span> </li> <li class="y-left day"> <span class="title">鏄庡ぉ</span> <div title="{options.weather.tomorrow_condition}" class="weather-icon weather-icon-{options.weather.tomorrow_weather_icon_id}"></div> <span class="temperature"> <em>{options.weather.tomorrow_low_temperature}</em>&#176; &#47; <em>{options.weather.tomorrow_high_temperature}</em>&#176; </span> </li> <li class="y-left day"> <span class="title">鍚庡ぉ</span> <div title="{options.weather.dat_condition}" class="weather-icon weather-icon-{options.weather.dat_weather_icon_id}"></div> <span class="temperature"> <em>{options.weather.dat_low_temperature}</em>&#176; &#47; <em>{options.weather.dat_high_temperature}</em>&#176; </span> </li> </ul> <div class="y-box city-select {show: !options.weather_show}"> <div class="y-box"> <div class="y-left select-style"> <p class="y-box"> <span class="y-left name">{options.current_province}</span> <span class="y-right y-icon icon-more" onclick="{showProvinces}"></span> </p> <div class="y-box province-list {show: options.province_show}"> <a class="y-left item" href="javascript:;" each="{item, i in options.locations}" onclick="{changeProvince}"> {item} </a> </div> </div> <div class="y-right select-style"> <p class="y-box"> <span class="y-left name">{options.current_city}</span> <span class="y-right y-icon icon-more" onclick="{showCities}"></span> </p> <div class="y-box city-list {show: options.city_show}"> <a class="y-left item" href="javascript:;" each="{item, i in options.cities}" onclick="{changeCity}"> {item} </a> </div> </div> </div> <div class="y-box action"> <a href="javascript:;" class="y-left ok-btn" onclick="{onSubmitClick}">纭畾</a> <a href="javascript:;" class="y-right cancel-btn" onclick="{onCancelClick}">鍙栨秷</a> </div></div>', 'class="y-weather"',
function() {
    function i(i) {
        var t = !0;
        return i >= 0 && 50 >= i ? {
            c: t ? "#5cbf4c": "#5c8828",
            t: "浼�"
        }: i >= 51 && 100 >= i ? {
            c: t ? "#5cbf4c": "#5c8828",
            t: "鑹�"
        }: i >= 101 && 150 >= i ? {
            c: t ? "#ff9f2d": "#c58120",
            t: "杞诲害姹℃煋"
        }: i >= 151 && 200 >= i ? {
            c: t ? "#ff9f2d": "#c58120",
            t: "涓害姹℃煋"
        }: i >= 201 && 300 >= i ? {
            c: t ? "#ff5f5f": "#cf3d3d",
            t: "閲嶅害姹℃煋"
        }: i >= 301 ? {
            c: t ? "#ff5f5f": "#cf3d3d",
            t: "涓ラ噸姹℃煋"
        }: {
            c: "rgba( 214, 117, 3, 0.8 )",
            t: "鍏朵粬"
        }
    }
    this.options = {
        current_province: "鍖椾含",
        current_city: "鍖椾含",
        province_show: !1,
        city_show: !1,
        weather_show: !0
    },
    riot.observable(this),
    this.on("weatherChange",
    function(i) {
        this._renderWeather(i)
    }),
    this.init = function() {
        this._getCities()
    }.bind(this),
    this.showProvinces = function() {
        this.options.city_show = !1,
        this.options.province_show = !this.options.province_show
    }.bind(this),
    this.showCities = function() {
        this.options.province_show = !1,
        this.options.city_show = !this.options.city_show
    }.bind(this),
    this.changeLocation = function() {
        this.options.weather_show = !1
    }.bind(this),
    this.changeProvince = function(i) {
        this.options.city_show = !1,
        this.options.province_show = !1,
        this.options.current_province = i.item.item,
        this._renderCities(i.item.item)
    }.bind(this),
    this.changeCity = function(i) {
        this.options.city_show = !1,
        this.options.province_show = !1,
        this.options.current_city = i.item.item
    }.bind(this),
    this.onSubmitClick = function() {
        var i = this;
        this.options.weather_show = !0,
        this._getWeather({
            city: i.options.current_city
        })
    }.bind(this),
    this.onCancelClick = function() {
        this.options.weather_show = !0
    }.bind(this),
    this._getWeather = function(i) {
        var t = this;
        http({
            url: "/stream/widget/local_weather/data/",
            method: "GET",
            data: i,
            success: function(i) {
                i = i || {},
                "success" === i.message && (t._renderWeather(i.data.weather), Cookies.set("WEATHER_CITY", i.data.city, {
                    expires: 100
                }), t.parent && t.parent.trigger("weatherChange", i.data.weather))
            }
        })
    }.bind(this),
    this._renderWeather = function(t) {
        this.options.weather = t,
        this.options.city = t.city_name,
        this.options.wind = t.wind_direction + t.wind_level + "绾�",
        this.options.air = t.quality_level + " " + t.aqi,
        this.options.level = i(t.aqi).c,
        this.update()
    }.bind(this),
    this._getCities = function() {
        var i = this;
        http({
            url: "/stream/widget/local_weather/city/",
            method: "GET",
            success: function(t) {
                t = t || {},
                "success" === t.message && (i.options.locations = t.data, i._renderCities(i.options.current_province))
            }
        })
    }.bind(this),
    this._renderCities = function(i) {
        this.options.cities = this.options.locations[i];
        for (var t in this.options.cities) {
            this.options.current_city = t;
            break
        }
        this.update()
    }.bind(this),
    this.on("mount",
    function() {
        this.init()
    })
}),
riot.tag("topbar", '<div class="y-box topbar"> <ul class="y-left" if="{opts.home}"> <li class="tb-item"> <a class="tb-link" href="http://app.toutiao.com/news_article/" target="_blank" ga_event="mh_nav_others">涓嬭浇APP</a> </li> </ul> <div class="y-left y-nav-topbar" riot-tag="nav" if="{!opts.home}"></div> <ul class="y-right"> <li class="tb-item weather" if="{opts.home}"> <a class="tb-link" href="javascript:;"> <i class="y-icon icon-location"></i><span>&nbsp;{ options.city }</span> <span class="city_state">{ options.state }</span> <span class="city_temperature"> <em>{options.low}</em>&#176; &nbsp;&#47;&nbsp; <em>{options.top}</em>&#176; </span> </a> <div class="weather-box"> <div riot-tag="weather"></div> </div> </li> <li class="tb-item"> <a class="tb-link" href="https://mp.toutiao.com/" target="_blank" ga_event="mh_nav_others">澶存潯鍙�</a> </li> <li class="tb-item"> <a class="tb-link" href="https://tuchong.com/" target="_blank" ga_event="mh_nav_others">鍥捐櫕</a> </li> <li class="tb-item"> <a onclick="{userFeedClick}" class="tb-link" href="javascript:void(0)">鍙嶉</a> </li> <li class="tb-item more"> <a class="tb-link" href="/about/">鏇村</a> <div class="layer"> <ul> <li> <a href="/articles_news_society/" class="layer-item">澶存潯绠€鐗�</a> </li> <li> <a href="/about/" class="layer-item" rel="nofollow">鍏充簬澶存潯</a> </li> <li> <a href="/join/" class="layer-item" rel="nofollow">鍔犲叆澶存潯</a> </li> <li> <a href="/report/" class="layer-item" rel="nofollow">濯掍綋鎶ラ亾</a> </li> <li> <a href="/media_partners/" class="layer-item" rel="nofollow">濯掍綋鍚堜綔</a> </li> <li> <a href="/cooperation/" class="layer-item" rel="nofollow">浜у搧鍚堜綔</a> </li> <li> <a href="/media_cooperation/" class="layer-item" rel="nofollow">鍚堜綔璇存槑</a> </li> <li> <a href="https://ad.toutiao.com/e/" class="layer-item" target="_blank" rel="nofollow">骞垮憡鎶曟斁</a> </li> <li> <a href="/contact/" class="layer-item" rel="nofollow">鑱旂郴鎴戜滑</a> </li> <li> <a href="/user_agreement/" class="layer-item" rel="nofollow">鐢ㄦ埛鍗忚</a> </li> <li> <a href="/complain/" class="layer-item" rel="nofollow">鎶曡瘔鎸囧紩</a> </li> </ul> </div> </li> </ul> <div riot-tag="userFeedback"></div></div>',
function(i) {
    this.options = {
        city: "",
        state: "",
        top: 0,
        low: 0
    },
    this.userFeedbackShow = !1;
    var t = this.tags.weather,
    e = this.tags.userFeedback;
    riot.observable(this),
    this.on("weatherChange",
    function(t) {
        this._renderWeather(t),
        i.weatherChange && i.weatherChange(t)
    }),
    this.init = function() {
        if (this.opts.home) {
            var i = Cookies.get("WEATHER_CITY") || "";
            this._getWeather({
                city: i
            })
        }
    }.bind(this),
    this.userFeedClick = function() {
        e.trigger("userFeedShow", this.userFeedbackShow),
        this.userFeedbackShow = !this.userFeedbackShow
    }.bind(this),
    this._getWeather = function(i) {
        var e = this;
        http({
            url: "/stream/widget/local_weather/data/",
            method: "GET",
            data: i,
            success: function(i) {
                i = i || {},
                "success" === i.message && (e._renderWeather(i.data.weather), t && t.trigger("weatherChange", i.data.weather))
            }
        })
    }.bind(this),
    this._renderWeather = function(i) {
        this.options.weather = i,
        this.options.city = i.city_name,
        this.options.state = i.current_condition,
        this.options.top = i.high_temperature,
        this.options.low = i.low_temperature,
        this.update()
    }.bind(this),
    this.init()
}),
riot.tag("searchbox", '<div name="searchBox" class="y-left search-box"> <form name="searchForm" action="/search/" method="get" target="_blank" onsubmit="{onSearchClick}"> <div class="y-box input-group"> <input class="y-left input-text" name="keyword" value="{options.keyword}" autocomplete="off" ga_event="mh_search" type="text" placeholder="璇疯緭鍏ュ叧閿瓧" onclick="{onSearchInputClick}" onfocus="{onFocus}" onblur="{onBlur}"> <div class="y-right btn-submit"> <button type="submit" href="javascript:;"> <i class="y-icon icon-search" ga_event="mh_search"></i> </button> </div> </div> </form> <div class="layer {layer-show: options.layershow}" if="{options.hotwords.length > 0}"> <div class="layer-inner">  <ul ga_event="mh_search"> <li each="{item, i in options.hotwords}" class="search-item" onclick="{onSearchItemClick}"> <a href="javascript:;"> <i class="search-no search-no-{i+1}">{i + 1}</i> <span class="search-txt">{item}</span> </a> </li> </ul> </div> </div></div>',
function() {
    this.options = {
        hotwords: [],
        keyword: "",
        searchTip: "澶у閮藉湪鎼滐細",
        layershow: !1
    },
    this.init = function() {
        this._getHotWords()
    }.bind(this),
    this._getHotWords = function() {
        var i = this;
        http({
            url: "/hot_words/",
            method: "GET",
            success: function(t) {
                t = t.hot_words || t || [],
                _.isArray(t) && 0 !== t.length && (i.options.hotwords = t, i.options.keyword = i.options.searchTip + t[0], i.update())
            }
        })
    }.bind(this),
    this.onFocus = function() {
        this.options.keyword = "",
        this.options.layershow = !0
    }.bind(this),
    this.onBlur = function() {
        this.options.layershow = !1
    }.bind(this),
    this.onSearchClick = function() {
        var i, t = this.keyword.value;
        return t ? (i = t.slice(0, 6), i !== this.options.searchTip || (this.options.keyword = t.slice(6), this.options.keyword) ? !0 : (this.keyword.focus(), !1)) : (this.keyword.focus(), !1)
    }.bind(this),
    this.onSearchItemClick = function(i) {
        this.options.keyword = i.item.item,
        this.update(),
        this.searchForm.submit()
    }.bind(this),
    this.on("mount",
    function() {
        this.init()
    })
}),
riot.tag("userbox", '<div class="y-right userbox"> <a if="{options.isPgc&&options.id}" class="y-right new-article" href="http://mp.toutiao.com/new_article/" ga_event="mh_write">鍙戞枃</a> <div if="{options.id}" class="y-right username"> <a ga_event="mh_nav_user" class="user-head" href="//web.toutiao.com/user/{options.id}/pin/" rel="nofollow"> <div class="user-image"> <img onload="this.style.opacity=1;" riot-src="{options.avatarUrl}"> </div> <span>{options.name}</span> </a> <div class="user-layer"> <ul ga_event="mh_nav_user"> <li><a href="http://web.toutiao.com/user/{options.id}/pin/" class="layer-item" rel="nofollow">鎴戠殑鏀惰棌</a></li> <li><a href="http://web.toutiao.com/user/{options.id}/subscribe/" class="layer-item" rel="nofollow">鎴戠殑璁㈤槄</a></li> <li><a href="http://web.toutiao.com/user/{options.id}/followers/" class="layer-item" rel="nofollow">鎴戠殑绮変笣</a></li> <li><a href="http://web.toutiao.com/user/{options.id}/followings/" class="layer-item" rel="nofollow">鎴戠殑鍏虫敞</a></li> <li><a href="http://web.toutiao.com/auth/logout/" class="layer-item" rel="nofollow">閫€鍑�</a></li> </ul> </div> </div> <div if="{!options.id}" class="y-right username"> <a ga_event="nav_login" class="nav-login" href="javascript:;" onclick="{onLoginClick}"> <span>鐧诲綍</span> </a> </div></div>',
function(i) {
    var t = this;
    riot.observable(this),
    this.options = {
        id: i.userInfo.id,
        name: i.userInfo.name,
        avatarUrl: i.userInfo.avatarUrl,
        isPgc: i.userInfo.isPgc || !1
    },
    this.onLoginClick = function() {
        window.trigger("login", {
            successCb: function(i) {
                window.trigger("userChange", i)
            },
            errorCb: function() {}
        })
    }.bind(this),
    window.on("userChange",
    function(i) {
        i && (t.options.id = i.user_id, t.options.name = i.name, t.options.avatarUrl = i.avatar_url, t._isPgc(), t.update())
    }),
    this._isPgc = function() {
        var i = this;
        http({
            url: "/user/pgc_info/",
            method: "get",
            cache: !1,
            success: function(t) {
                t = t || {},
                "success" === t.message && t.data.is_pgc_author && (i.options.isPgc = !0, i.update())
            }
        })
    }.bind(this)
}),
riot.tag("login", '<div class="login-dialog {hide: options.hide}"> <a class="btn" href="javascript:;" onclick="{hide}"> <i class="icon icon-close"></i> </a> <div class="login-dialog-header"> <h3>閭鐧诲綍</h3> </div> <div class="login-dialog-inner" data-node="inner"> <div class="login-pannel bottom-line"> <form action="/auth/login/" method="POST" onsubmit="{onFormSubmit}"> <ul> <li> <div class="input-group"> <div class="input"> <label>閭</label> <input class="name" name="name_or_email" type="text" placeholder="璇蜂娇鐢ㄦ偍鐨勬敞鍐岄偖绠�" autocomplete="off" spellcheck="false"> </div> </div> </li> <li> <div class="input-group"> <div class="input"> <label>瀵嗙爜</label> <input class="password" name="password" type="password" data-node="password" placeholder="瀵嗙爜" autocomplete="off" spellcheck="false"> </div> </div> </li> <li class="captcha-box {show: options.captchaImg}"> <div class="input-group"> <div class="input"> <input class="password" name="captcha" type="text" data-node="captcha" placeholder="楠岃瘉鐮�" autocomplete="off" spellcheck="false"> <img name="captchaImg" riot-src="{options.captchaImg}"> </div> </div> </li> <li> <div class="input-group"> <input type="checkbox" name="remember_me" checked="" style="vertical-align:top"> <label for="remember_me" class="label">璁颁綇璐﹀彿</label> </div> </li> <li> <div class="input-group" style="text-align: center;"> <input type="submit" class="submit-btn" value="鐧诲綍"> <p class="{error: options.errorMsg}">{options.errorMsg}</p> </div> </li> </ul> </form> </div> <div class="login-dialog-header"> <h3>鍚堜綔缃戠珯甯愬彿鐧诲綍</h3> </div> <div class=""> <ul class="y-box sns_login_list" onclick="{onSnsLoginClick}"> <li class="sinaweibo"> <a href="javascript:;" data-pid="sina_weibo" ga_event="login_sina_weibo"> <i class="icon"></i> 鏂版氮寰崥 </a> </li> <li class="qqweibo"> <a href="javascript:;" data-pid="qq_weibo" ga_event="login_tencnet_weibo"> <i class="icon"></i> 鑵捐寰崥 </a> </li> <li class="qzone"> <a href="javascript:;" data-pid="qzone_sns" ga_event="login_qqzone"> <i class="icon"></i> QQ绌洪棿 </a> </li> <li class="renren"> <a href="javascript:;" data-pid="renren_sns" ga_event="login_renren"> <i class="icon"></i> 浜轰汉缃� </a> </li> <li class="weixin"> <a href="javascript:;" style="margin-right:0;" data-pid="weixin" ga_event="login_wechat"> <i class="icon"></i> 寰俊 </a> </li> </ul> </div> </div> </div> <div class="mask {hide: options.hide}"></div>',
function(i) {
    var t = this;
    riot.observable(this),
    this.options = {
        hide: !0,
        errorMsg: "",
        captchaImg: ""
    },
    this.curSpec = {
        successCb: i.successCb ||
        function() {},
        errorCb: i.errorCb ||
        function() {}
    },
    this.hide = function() {
        this.options.hide = !0,
        this.update()
    }.bind(this),
    this.onFormSubmit = function(i) {
        i.preventDefault();
        var t = this,
        e = http.serialize(i.currentTarget);
        user.loginByLoc({
            data: e,
            successCb: function(i) {
                "function" == typeof t.curSpec.successCb && t.curSpec.successCb(i),
                t.hide()
            },
            errorCb: function(i) {
                t.password.value = "",
                i = i || {};
                var e = i.data || {};
                t.options.errorMsg = e.description || "鐧诲綍澶辫触",
                e.captcha ? (t.captcha.value = "", t.options.captchaImg = "data:image/gif;base64," + e.captcha) : (t.captcha.value = "", t.options.captchaImg = ""),
                "function" == typeof t.curSpec.errorCb && t.curSpec.errorCb(i),
                t.update()
            }
        })
    }.bind(this),
    this.onSnsLoginClick = function(i) {
        var t = utils.getTarget(i),
        e = utils.getAttribute(t, "data-pid") || utils.getAttribute(t.parentNode, "data-pid");
        this.hide(),
        user.loginByOther(e, this.curSpec)
    }.bind(this),
    window.on("login",
    function(i) {
        t.options.hide = !1,
        i = i || {},
        t.curSpec = {
            successCb: i.successCb ||
            function() {},
            errorCb: i.errorCb ||
            function() {}
        },
        t.update()
    })
}),
riot.tag("userFeedback", '<div class="feedback_dialog"> <div class="dialog-header"> <h3>鎰忚鍙嶉</h3> </div> <div class="dialog-inner"> <div class="feedback_panel"> <form onsubmit="{onFormSubmit}"> <p class="label">鑱旂郴鏂瑰紡锛堝繀濉級</p> <div class="input-group"> <input class="email" placeholder="鎮ㄧ殑閭/QQ鍙�" type="text" name="feedback-email"> </div> <p class="label">鎮ㄧ殑鎰忚锛堝繀濉級</p> <div class="input-group"> <textarea style="height:100px;" name="feedback-content" class="content" maxlength="140" placeholder="璇峰～鍐欐偍鐨勬剰瑙侊紝涓嶈秴杩�140瀛�"></textarea> </div> <div class="input-group"> <input type="submit" class="{submit-btn:true,disabled:disabled}" value="鎻愪氦" __disabled="{disabled}"> <span class="error">{msg}</span> <a class="close" href="javascript:void(0);" onclick="{hide}">[鍏抽棴]</a> </div> </form> </div> </div></div>', 'class="userFeedback" show="{userFeedShow}"',
function() {
    this.message = {
        success: "宸叉彁浜�,鎰熻阿鎮ㄧ殑鎰忚",
        fail: "鎻愪氦閿欒,璇风◢鍚庨噸璇�",
        mail_error: "璇疯緭鍏ユ纭殑鑱旂郴鏂瑰紡",
        content_error: "璇疯緭鍏ユ偍鐨勬剰瑙�",
        content_length_error: "鎰忚闀垮害瓒呭嚭闄愬埗"
    };
    var i = this;
    this.userFeedShow = !1,
    this.msg = "",
    this.disabled = !1,
    riot.observable(this),
    this.on("userFeedShow",
    function(t) {
        t ? i.hide() : i.show()
    }),
    this.showMessage = function(i) {
        this.msg = this.message[i],
        this.update()
    }.bind(this),
    this.show = function() {
        this.userFeedShow = !0,
        this.update()
    }.bind(this),
    this.hide = function() {
        this.userFeedShow = !1,
        this.msg = "",
        this.update()
    }.bind(this),
    this.onFormSubmit = function() {
        var t = this["feedback-email"],
        e = this["feedback-content"];
        return t.value.length < 5 ? (t.focus(), this.showMessage("mail_error")) : "" === e.value ? (e.focus(), this.showMessage("content_error")) : (this.msg = "", this.disabled = !0, this.update(), void http({
            headers: {
                "X-CSRFToken": Cookies.get("csrftoken")
            },
            url: "/post_message/",
            method: "post",
            data: {
                appkey: "web",
                uuid: t.value,
                content: "[" + window.location.host + "]" + e.value
            },
            success: function(s) {
                return "success" !== s.message ? i.showMessage("fail") : (t.value = "", e.value = "", i.disabled = !1, i.showMessage("success"), void setTimeout(function() {
                    i.hide()
                },
                2e3))
            },
            error: function() {
                i.disabled = !1,
                i.update(),
                i.showMessage("fail")
            }
        }))
    }.bind(this)
}),
riot.tag("toast", '<div name="toast" class="toast-inner" style="opacity: 10; filter:alpha(opacity=1000);"> <span>{opts.msg}</span></div>', 'class="toast"',
function() {
    var i = this;
    this.on("mount",
    function() {
        var t = this.toast,
        e = t.clientWidth,
        s = t.clientHeight,
        a = new TAnimation;
        t.style.cssText += "margin-top:-" + s / 2 + "px;margin-left:-" + e / 2 + "px",
        a.animate({
            el: t,
            prop: "opacity",
            to: 0,
            transitionDuration: 2e3
        },
        function() {
            i.unmount(!0)
        })
    })
}),
riot.tag("media-header", '<div class="media-box"> <div class="media-inner"> <div class="img-wrap"> <img riot-src="{options.mediaInfo.avartar_url}" alt=""> </div> <div class="media-info"> <div class="info-inner"> <p class="name">{options.mediaInfo.name}</p> <p class="desc" title="{options.mediaInfo.description}">{options.mediaInfo.description}</p> <p if="{!opts.isOwner}" ga_event="follow_pgc" class="btn-subscribe {has-subscribe: options.mediaInfo.isLike}" onclick="{subscribe}">{options.status}</p> </div> </div> </div></div>', 'id="media-header"',
function(i) {
    var t = this,
    e = !1;
    this.options = {
        mediaInfo: i.mediaInfo,
        status: i.mediaInfo.isLike ? "宸插叧娉�": "+鍏虫敞"
    },
    this.subscribe = function() {
        var i = this;
        user.checkLogin({
            successCb: function() {
                i._subscribe()
            },
            errorCb: function() {
                window.trigger("login", {
                    successCb: function(t) {
                        window.trigger("userChange", t),
                        i._subscribe()
                    }
                })
            }
        })
    }.bind(this),
    this._subscribe = function() {
        var i = this,
        t = this.options.mediaInfo.isLike,
        s = t ? "unlike": "like";
        e || (e = !0, http({
            url: "/pgc/" + s + "/",
            method: "POST",
            headers: {
                "X-CSRFToken": Cookies.get("csrftoken")
            },
            data: {
                media_id: i.options.mediaInfo.media_id
            },
            success: function(t) {
                "success" === t.message && (i.options.mediaInfo.isLike = !i.options.mediaInfo.isLike, window.trigger("media_subscribe", {
                    isLike: i.options.mediaInfo.isLike
                }), e = !1, i.update())
            }
        }))
    }.bind(this),
    window.on("media_subscribe",
    function(i) {
        t.options.mediaInfo.isLike = i.isLike,
        t.options.status = i.isLike ? "宸插叧娉�": "+鍏虫敞",
        t.update()
    })
}),
riot.tag("tab-header", '<a each="{item, i in options.tabs}" href="{item.url}" class="tab-item {active: item.page_type == options.page_type}" ga_event="{item.ga_event}">{item.name}</a> <a href="{options.baseUrl}" class="tab-item {active: options.page_type == 1}" ga_event="show_all">鍏ㄩ儴</a> <a if="{opts.show_video}" href="{options.baseUrl}?page_type=0" class="tab-item {active: options.page_type == 0}" ga_event="show_video">瑙嗛</a>', 'id="tab-header"',
function(i) {
    this.options = {
        baseUrl: "/m" + i.media_id + "/",
        page_type: i.page_type || "1"
    }
}),
riot.tag("history-articles", '<h2 class="dtag">杩戞湡鏈€浣虫枃绔�</h2> <ul class="article-list"> <li each="{item, i in options.articles}" class="item" if="{i<5}"> <a class="y-box" href="{item.url}" target="_blank"> <i class="y-left num">{i+1}</i> <div class="y-right info"> <h3>{item.title}</h3> <p class="extra"> <span>{item.go_detail_count}闃呰&nbsp;鈰�&nbsp;</span> <span>{item.publish_time}</span> </p> </div> </a> </li></ul>', 'id="history-articles" if="{!opts.isBanned && options.articles.length}"',
function(i) {
    this.options = {
        media_id: i.media_id,
        articles: []
    },
    this.init = function() {
        this._getArticles()
    }.bind(this),
    this._getArticles = function() {
        var i = this;
        http({
            url: "/media_hot/",
            method: "GET",
            data: {
                media_id: i.options.media_id
            },
            success: function(t) {
                "success" === t.message && (i.options.articles = t.data.hot_articles || [], i.update())
            }
        })
    }.bind(this),
    this.init()
}),
riot.tag("related-medias", '<h2 class="dtag">鐩稿叧澶存潯鍙�</h2> <ul class="media-list"> <li each="{item, i in options.medias}" class="item" if="{i<5}"> <a class="y-left img-wrap" target="_blank" href="{item.open_url}"> <img riot-src="{item.avatar_url}" alt=""> </a> <div class="media-info"> <div class="media-inner"> <a href="{item.open_url}" target="_blank" class="media-name">{item.name}</a> <p class="media-des">{item.description}</p> </div> </div> </li></ul>', 'id="related-medias" if="{options.medias.length}"',
function(i) {
    this.options = {
        media_id: i.media_id,
        medias: []
    },
    this.init = function() {
        this._getMedias()
    }.bind(this),
    this._getMedias = function() {
        var i = this;
        http({
            url: "/related_media/",
            method: "GET",
            data: {
                media_id: i.options.media_id
            },
            success: function(t) {
                "success" === t.message && (i.options.medias = t.data.related_media || [], i.update())
            }
        })
    }.bind(this),
    this.init()
}),
riot.tag("media-tool", '<div class="tool-inner"> <div if="{!opts.isOwner}" class="tool-box subscribe" onclick="{subscribe}" ga_event="follow_pgc_right"> <i class="y-icon {icon-follow: !options.mediaInfo.isLike} {icon-unfollow: options.mediaInfo.isLike}"></i> <span>{options.status}</span> </div> <div class="tool-box" onclick="{goTop}" ga_event="go_top"> <i class="y-icon icon-arrow-top"></i> </div></div>', 'id="media-tool"',
function(i) {
    var t = this,
    e = !1;
    this.options = {
        mediaInfo: i.mediaInfo,
        status: i.mediaInfo.isLike ? "鍙栨秷鍏虫敞": "鍏虫敞"
    },
    this.goTop = function() {
        window.scrollTo(0, 0)
    }.bind(this),
    this.subscribe = function() {
        var i = this;
        user.checkLogin({
            successCb: function() {
                i._subscribe()
            },
            errorCb: function() {
                window.trigger("login", {
                    successCb: function(t) {
                        window.trigger("userChange", t),
                        i._subscribe()
                    }
                })
            }
        })
    }.bind(this),
    this._subscribe = function() {
        var i = this,
        t = this.options.mediaInfo.isLike,
        s = t ? "unlike": "like";
        e || (e = !0, http({
            url: "/pgc/" + s + "/",
            method: "POST",
            headers: {
                "X-CSRFToken": Cookies.get("csrftoken")
            },
            data: {
                media_id: i.options.mediaInfo.media_id
            },
            success: function(t) {
                "success" === t.message && (i.options.mediaInfo.isLike = !i.options.mediaInfo.isLike, window.trigger("media_subscribe", {
                    isLike: i.options.mediaInfo.isLike
                }), e = !1, i.update())
            }
        }))
    }.bind(this),
    window.on("media_subscribe",
    function(i) {
        t.options.mediaInfo.isLike = i.isLike,
        t.options.status = i.isLike ? "鍙栨秷鍏虫敞": "鍏虫敞",
        t.update()
    })
}),
!
function(i) {
    var t = {};
    t.getHoney = function() {
        var i = Math.floor((new Date).getTime() / 1e3),
        t = i.toString(16).toUpperCase(),
        e = md5(i).toString().toUpperCase();
        if (8 != t.length) return {
            as: "479BB4B7254C150",
            cp: "7E0AC8874BB0985"
        };
        for (var s = e.slice(0, 5), a = e.slice( - 5), o = "", n = 0; 5 > n; n++) o += s[n] + t[n];
        for (var c = "",
        r = 0; 5 > r; r++) c += t[r + 3] + a[r];
        return {
            as: "A1" + o + t.slice( - 3),
            cp: t.slice(0, 3) + c + "E1"
        }
    },
    i.ascp = t
} (window, document),
!
function() {
    var i = document.querySelectorAll("#articles .article-item");
    articleCount = i && i.length,
    articleCount > 0 && taAnalysis && taAnalysis.send("event", {
        ev: "article_show_count",
        ext_id: articleCount
    })
} (window, void 0),
riot.tag("loading", '<div if="{options.cssAnimation}" class="loading ball-pulse"> <div></div> <div></div> <div></div> <span>{options.msg}&sdot;&sdot;&sdot;</span> </div> <div if="{!options.cssAnimation}" class="loading loading-normal"> <img src="http://s3b.pstatp.com/toutiao/resource/toutiao_web/static/style/image/loading_50c5e3e.gif" alt=""> <span>{options.msg}&sdot;&sdot;&sdot;</span></div>',
function(i) {
    var t = utils.cssAnimationSupport();
    this.options = {
        cssAnimation: t,
        msg: i.msg || "鎺ㄨ崘涓�"
    }
}),
riot.tag("mediaFeed", '<div class="mediaFeed"> <ul> <li class="item {item-hidden: item.honey} {J_add: item.ad_id}" each="{item, i in opts.list}" ga_event="click_feed_word" > <div class="item-inner y-box"> <div if="{item.middle_mode}" class="y-left lbox"> <a class="img-wrap" href="{item.source_url}" target="_blank"> <img riot-src="{item.pc_image_url}" alt=""> <i if="{item.has_video && item.video_duration_str}" class="ftype video"> <span>{item.video_duration_str}</span> </i> </a> </div> <div class="normal {rbox: item.middle_mode} {no-image: !item.more_mode&&!item.middle_mode}"> <div class="rbox-inner"> <a class="title-box link" href="{item.source_url}" target="_blank"> {item.title} </a> <p if="{!item.more_mode}" class="abstract"> {item.abstract} </p> <div if="{item.more_mode}" class="img-list y-box"> <a each="{imgItem, j in item.image_list}" class="img-wrap" href="{item.source_url}" target="_blank"> <img riot-src="{imgItem.pc_url}" alt=""> </a> <span if="{item.has_gallery}" class="img-num">{item.gallery_pic_count}鍥�</span> </div> <div class="y-box footer"> <div class="y-left"> <span if="{item.live_status}">{_liveStatusHandle(item.live_status)}&nbsp;&sdot;</span> <span if="{item.show_play_effective_count}">{item.play_effective_count}鎾斁&nbsp;&sdot;</span> <span if="{!item.show_play_effective_count}">{item.go_detail_count}闃呰&nbsp;&sdot;</span> <span>{item.comments_count}璇勮&nbsp;&sdot;</span> <span>{item.datetime}</span> </div> </div> </div> </div> </div> </li> </ul></div>',
function() {
    this._liveStatusHandle = function(i) {
        return "1" == i ? "鐩存挱鏈紑濮�": "2" == i ? "鐩存挱杩涜涓�": "鐩存挱宸茬粨鏉�"
    }.bind(this)
}),
riot.tag("feedBox", '<div class="feedBox" name="feedBox"> <div riot-tag="mediaFeed" list="{options.list}"></div> <div if="{options.isLoadmore}" riot-tag="loading" msg="鍔犺浇涓�"></div> <div if="{options.noMore}" class="no-more">娌℃湁鏇村鍟�</div> <div if="{opts.media_status == 9}" class="no-content"> <div class="img-wrap"> <img src="http://s3.pstatp.com/toutiao/resource/ntoutiao_web/static/image/media/no_articles_bg_5cdb2a7.png" alt=""> </div> <p class="desc">銆岃甯愬彿宸茶灏佺锛屽唴瀹规棤娉曟煡鐪嬨€�</p> </div></div>',
function(i) {
    function t() {
        o.options.isLoadmore = !0,
        o.update(),
        e("loadmore",
        function() {
            o.options.isLoadmore = !1
        })
    }
    function e(i, t) {
        if (!n) {
            n = !0;
            var e = s();
            http({
                url: "/pgc/ma/",
                method: "get",
                data: e,
                success: function(i) {
                    "success" === i.message && (o.options.list = o.options.list.concat(i.data), o.params.max_behot_time = i.next.max_behot_time, i.data.length > 0 && taAnalysis && taAnalysis.send("event", {
                        ev: "article_show_count",
                        ext_id: i.data.length
                    }), i.has_more || (utils.off(window, "scroll", a), o.options.noMore = !0)),
                    n = !1,
                    t && t(i),
                    o.update()
                }
            })
        }
    }
    function s() {
        var i, t = ascp.getHoney();
        return i = _.extend({},
        o.params, {
            as: t.as,
            cp: t.cp
        })
    }
    riot.observable(this);
    var a, o = this,
    n = !1;
    this.options = {
        list: [],
        isLoadmore: !1,
        noMore: !1
    },
    this.params = {
        media_id: i.media_id,
        page_type: i.page_type,
        max_behot_time: 0,
        count: 10,
        version: 2,
        platform: "pc"
    },
    this.on("mount",
    function() {
        9 != i.media_status && (t(), a = utils.on(window, "scroll", _.throttle(function() {
            var i = utils.scrollTop(),
            e = o.feedBox.clientHeight,
            s = window.screen.height;
            600 > e - i - s && t()
        },
        350)))
    })
}),
function(i, t) {
    var e = (t.getElementById("content-left"), t.getElementById("content-right-inner"));
    utils.on(i, "scroll", _.throttle(function() {
        var i = utils.scrollTop();
        e.style.cssText = i > 1e3 ? "position: fixed; bottom: 80px;": "position: static;"
    },
    60)),
    Cookies.set("cp", ascp.getHoney().cp, {
        expires: 30
    })
} (window, document, void 0);