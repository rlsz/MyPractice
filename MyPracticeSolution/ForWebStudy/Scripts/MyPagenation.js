;
(function ($) {
    if (!String.prototype.format) {
        /*
           扩展字符串拼接方法，便于书写代码
        */
        String.prototype.format = function () {
            var result = this;
            for (var key in arguments) {
                var reg = new RegExp("(\\{" + key + "\\})", "g");
                result = result.replace(reg, arguments[key]);
            }
            return result;
        }
    }
    /*
        分页插件约定：
        1、$(...).MyPagenation()方法返回的是控件实例instance，而非DOM对象，如需操作DOM对象，可以通过instance.target获得，或者直接使用$(...);
        2、必须为容器指定id属性，用于将DOM和instance一对一联系起来;
        3、返回数据时，请使用json Object，并指定数据总数;
        4、返回数据时，如果页码与请求的页码不同，则需要指定页码;
        5、插件内部方法调用有两种方式，即instance.FunctionName(args...)和$(...).MyPagenation("FunctionName", args...);
    */
    var defaultSetting = {
        options: {
            //ulContentClass: "",   //定义容器样式表
            //type: "GET",  //ajax参数
            //dataType: "json",  //ajax参数
            url: null,  //ajax参数
            defaultLoad: true,  //控件初始化后是否立即加载数据
            showSkipArea: false, //是否使用跳页功能
            showTotalArea: false,   //是否显示数据总数
            showOmit: false,    //是否使用页码省略号
            totalCountName: "total",    //约定返回数据中的totalCount字段名，必须指定
            pageIndexName: "index",     //约定返回数据中的pageIndex字段名，如果不指定则使用请求参数中的页码
            data: {},  //附加请求参数
            pageSize: 30,  //附加请求参数pageSize
            pageOffset: 2,  //页码偏移宽度，例如，页码偏移宽度为2时，分页如下：1 ... i-2 i-1 i i+1 i+2 ... max
            beforeLoad: function (params) { },  //数据加载前处理请求参数,返回false可以取消数据请求
            success: function (data, textStatus, jqXHR) { },   //ajax参数,数据加载成功
            error: function (jqXHR, textStatus, errorThrown) {   //ajax参数,请求失败处理
                console.trace();
                console.log("error");
            },
            complete: function (jqXHR, textStatus) { }      //ajax参数,请求完成后的处理，无论成功失败都会执行
        },
        classDefine: {
            namespace: "my-pagenation",         //容器外壳
            ulContent: "pagenation-content",      //容器, 多个class使用空格分隔，可以被options.ulContentClass覆盖使得默认样式无效化
            previous: "page-previous",          //上一页
            next: "page-next",           //下一页
            numberArea: "number-area",          //页码区域
            number: "page-number",          //页码
            omit: "page-omit",            //页码省略号
            skipArea: "skip-area",      //跳页区域
            totalArea: "total-area",          //数据总数
            selectNumber: "active"      //当前页class，用于定义高亮样式
        },
        pageNumberHtml: function (pageNumber) {
            return '<li class="{0}" index="{1}"><a href="javascript:;">{1}</a></li>'.format(this.classDefine.number, pageNumber);
        },
        pageOmitHtml: function () {
            return '<li class="{0}">...</li>'.format(this.classDefine.omit);
        },
        pageTotalHtml: function (totalCount) {
            return '<a>共{0}条数据</a>'.format(totalCount);
        },
        initHtml: function (options) {
            var str = '<ul class="{0}"><li class="{1}"><a href="javascript:;">上一页</a></li><li><ul class="{2}"></ul></li><li class="{3}"><a href="javascript:;">下一页</a></li>'.format(options.ulContentClass || this.classDefine.ulContent, this.classDefine.previous, this.classDefine.numberArea, this.classDefine.next);
            if (options.showSkipArea) {
                str += '<li class="{0}"><span>跳至第</span><input type="text" value=""/><span>页</span></li>'.format(this.classDefine.skipArea);
            }
            if (options.showTotalArea) {
                str += '<li class="{0}"></li>'.format(this.classDefine.totalArea);
            }
            str += '</ul>';
            return str;
        }
    }
    /*
       分页插件构造函数
    */
    function MyPagenation(target, options) {
        this.target = target;  //一个或多个jquery对象
        this.options = $.extend({}, defaultSetting.options, options);
        this.init();
    }
    /*
        初始化分页插件
    */
    MyPagenation.prototype.init = function () {
        $(this.target).addClass(defaultSetting.classDefine.namespace);
        $(this.target).html(defaultSetting.initHtml(this.options));
        this.randerPageNumber(0, 1);
        this.eventBind();
        if (this.options.defaultLoad && this.options.url) {
            this.getData();
        }
    }
    /*
        计算并刷新页码
    */
    MyPagenation.prototype.randerPageNumber = function (totalCount, pageIndex) {
        var lastPage = Math.ceil(totalCount / this.options.pageSize);
        var offset = this.options.pageOffset;

        var minPage = 1;
        var maxPage = lastPage;
        if (this.options.showOmit) {
            minPage = Math.max(pageIndex - offset, 1);
            maxPage = Math.min(pageIndex + offset, lastPage);
        }
        //设置minPage、maxPage、pageIndex最小为1，保证数据异常时分页插件正常运作
        maxPage = Math.max(maxPage, 1);
        pageIndex = Math.max(pageIndex, 1);

        var str = '';
        if (minPage > 1) {
            str += defaultSetting.pageNumberHtml(1);
        }
        if (minPage > 2) {
            str += defaultSetting.pageOmitHtml();
        }
        for (var i = minPage; i <= maxPage; i++) {
            str += defaultSetting.pageNumberHtml(i);
        }
        if (maxPage + 1 < lastPage) {
            str += defaultSetting.pageOmitHtml();
        }
        if (maxPage < lastPage) {
            str += defaultSetting.pageNumberHtml(lastPage);
        }

        var content = $(this.target).find("." + defaultSetting.classDefine.numberArea);
        content.html(str);
        content.find(".{0}[index={1}]".format(defaultSetting.classDefine.number, pageIndex)).addClass(defaultSetting.classDefine.selectNumber);
        $(this.target).find("." + defaultSetting.classDefine.totalArea).html(defaultSetting.pageTotalHtml(totalCount));
    }
    /*
        绑定容器冒泡事件
    */
    MyPagenation.prototype.eventBind = function () {
        var self = this;
        $(self.target).off();
        $(self.target).on("click", "." + defaultSetting.classDefine.number, function (e) {
            if ($(this).hasClass(defaultSetting.classDefine.selectNumber)) {
                return;
            }
            self.getData(Number($(this).attr("index")));
        })
        $(self.target).on("click", "." + defaultSetting.classDefine.previous, function (e) {
            var current = self.getCurrentPageIndex();
            if (current > 1) {
                self.getData(current - 1);
            }
        })
        $(self.target).on("click", "." + defaultSetting.classDefine.next, function (e) {
            var current = self.getCurrentPageIndex();
            var last = self.getLastPageIndex();
            if (current < last) {
                self.getData(current + 1);
            }
        })
        $(self.target).on("blur", "." + defaultSetting.classDefine.skipArea + " input", function (e) {
            try {
                var skip = Number($(this).val());
                var current = self.getCurrentPageIndex();
                var last = self.getLastPageIndex();
                if (skip == current) return;
                if (skip > last) return;
                if (skip < 1) return;
                self.getData(skip);
            } catch (e) { }
        })
    }
    /*
        分页插件请求数据;
        pageIndex为页码(null、undefine时获取当前高亮页码),data为请求参数
    */
    MyPagenation.prototype.getData = function (pageIndex, data) {
        var self = this;
        var params = $.extend({}, self.options.data, data);
        params["pageIndex"] = pageIndex || self.getCurrentPageIndex();
        params["pageSize"] = self.options.pageSize;
        if (self.options.beforeLoad(params) === false) return false;
        $.ajax({
            type: self.options.type || "GET",
            url: self.options.url || null,
            data: params,
            dataType: self.options.dataType || "json",
            async: true,
            success: function (json) {
                if (Object.prototype.toString.apply(json) == "[object Object]") {
                    self.randerPageNumber(json[self.options.totalCountName], json[self.options.pageIndexName] || params["pageIndex"])
                } else if (Object.prototype.toString.apply(json) == "[object Array]") {
                    self.randerPageNumber(json.length, params["pageIndex"])
                }
                self.options.success.apply(this, arguments);
            },
            error: self.options.error,
            complete: self.options.complete
        });
        return true;
    }
    MyPagenation.prototype.getCurrentPageIndex = function () {
        return Number($(this.target).find("." + defaultSetting.classDefine.number + "." + defaultSetting.classDefine.selectNumber).attr("index"));  //如果target含有多个分JQuery Element，则attr("index")以第一个element的值为准(这些element应该是保持同步的)
    }
    MyPagenation.prototype.getLastPageIndex = function () {
        return Number($(this.target).find("." + defaultSetting.classDefine.number + ":last").attr("index"));
    }

    var cache = {};
    /*
        分页插件销毁方法，$(...).MyPagenation("dispose");
        删除实例、target解耦、清空html，保留容器外壳。
    */
    MyPagenation.prototype.dispose = function (target) {
        var self = this;
        $(target || self.target).each(function () {
            delete cache[this.id];
            self.target = $(self.target).not(this);
            $(this).empty();
            $(this).removeClass(defaultSetting.classDefine.namespace);
        });
    }
    $.fn.MyPagenation = function (param) {
        if (this.size() < 1) return null;
        var target = this[0];
        if (param == undefined || typeof param === "object") {
            if (cache[target.id]) {
                $.extend(cache[target.id].options, param);
            } else {
                var instance = new MyPagenation(this, param);
                this.each(function (index, item) {
                    cache[item.id] = instance;
                });
            }
            return cache[target.id];
        } else if (typeof param == "string") {
            var args = Array.from(arguments);
            args.shift();
            if (param === "dispose") args.push(this);
            return MyPagenation.prototype[param].apply(cache[target.id], args);
        }
    }
    $.fn.MyPagenation.default = defaultSetting;
})(jQuery)