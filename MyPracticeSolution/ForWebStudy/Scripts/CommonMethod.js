//ajax异常处理,将在ajax.error之后执行
$(document).ajaxError(function (event, jqXHR, ajaxSettings, errorThrown) {
    if (jqXHR.status === 401) {
        if ($.messager) {
            $.messager.alert(errorThrown, '登陆失败!', 'error', function () {
                window.location.href = "../Account/Login.aspx?ReturnUrl=" + window.location.pathname;
            });
        } else {
            alert('登陆失败!');
            window.location.href = "../Account/Login.aspx?ReturnUrl=" + window.location.pathname;
        }
        return false;
    }
    console.info(event);
    console.info(jqXHR);
    console.info(ajaxSettings);
    console.info(errorThrown);
    if ($.messager) {
        $.messager.alert(errorThrown, jqXHR.responseText, 'error');
    } else {
        alert(jqXHR.responseText);
    }
});
////ajax成功处理,将在ajax.success之后执行
//$(document).ajaxSuccess(function (event, jqXHR, ajaxSettings, data) {
//    if (Object.prototype.toString.apply(data.code) === "[object Number]") {
//        if ($.messager) {
//            $.messager.alert(data.title, data.msg, data.icon, function () {
//                if (data.closeUrl != undefined) {
//                    window.location.href = ".." + data.closeUrl;
//                }
//            });
//        } else {
//            alert(data.msg);
//        }
//    }
//});
//ajax默认配置
$.ajaxSetup({
    type: 'POST',
    async: true,
    //data: { Method: 'nothing' },
    dataType: 'json'
});

//字符串扩展方法
String.prototype.contains = function (str) {
    return !(this.replace(str, "") == this);
};
//字符串扩展方法
String.prototype.trim = function (str) {
    if (str == undefined) {
        str = ' ';
    }
    var reg = new RegExp("^(" + str + ")*|(" + str + ")*$", "g");
    return this.replace(reg, "");
}
//字符串扩展方法
String.prototype.trimEnd = function (str) {
    if (str == undefined) {
        str = ' ';
    }
    var reg = new RegExp("(" + str + ")*$", "g");
    return this.replace(reg, "");
}
//字符串扩展方法
String.prototype.trimBegin = function (str) {
    if (str == undefined) {
        str = ' ';
    }
    var reg = new RegExp("^(" + str + ")*", "g");
    return this.replace(reg, "");
}
//字符串扩展方法
String.prototype.format = function (args) {
    if (arguments.length > 0) {
        var result = this;
        if (arguments.length == 1 && typeof (args) == "object") {
            for (var key in args) {
                var reg = new RegExp("({" + key + "})", "g");
                result = result.replace(reg, args[key]);
            }
        }
        else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] == undefined) {
                    return "";
                }
                else {
                    var reg = new RegExp("({[" + i + "]})", "g");//当i超过10后可能会出现bug，如果有bug可以尝试改中括号[]为小括号()，或删掉[]
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
        return result;
    }
    else {
        return this;
    }
}
//字符串扩展方法
String.prototype.cutstr = function (len) {
    var length = 0;
    var cut = '';
    for (var i = 0; i < this.length; i++) {
        var a = this.charAt(i);
        length++; //英文一个字节
        if (escape(a).length > 4) {
            //中文字符的长度经编码之后大于4，中文两个字节 
            length++;
        }
        cut = cut.concat(a);
        if (length >= len && cut != this) {
            cut = cut.concat("...");
            return cut;
        }
    }
    //如果给定字符串小于指定长度，则返回源字符串；  
    return this;
};
//修改easyui-datagrid默认配置，减少冗余代码
if ($.fn.datagrid != undefined) {
    var oldView = $.extend({}, $.fn.datagrid.defaults.view);//深拷贝
    $.extend($.fn.datagrid.defaults.view, {
        renderRow: function (target, fields, frozen, rowIndex, rowData) {
            rowIndex = parseInt(rowIndex);  //强制转换行号为数字,修正easyui行号bug
            return oldView.renderRow.apply(this, arguments);
        },
        onAfterRender: function (target) {
            oldView.onAfterRender.apply(this, arguments);
            var viewPanel = $(target).datagrid('getPanel').children('div.datagrid-view');
            viewPanel.children('div.datagrid-empty').remove();
            if (!$(target).datagrid('getRows').length) {
                var opts = $(target).datagrid('options');
                $('<div class="datagrid-empty"></div>').html(opts.emptyMsg || 'no records').appendTo(viewPanel);
            }
        }
    });
    $.extend($.fn.datagrid.defaults, {
        emptyMsg: '查询结果为空!',
        minHeight: 150,
        loadSwitch: true,
        singleSelect: 'true',
        iconCls: 'icon-site',
        pagination: true,
        rownumbers: true,
        striped: true,
        //fitColumns: true,
        autoRowHeight: false, //自适应行高设置为false可以加快渲染速度(之前不是一般的慢啊)
        pageSize: 20,
        pageList: [20, 30, 40, 50, 100, 200],
        onLoadSuccess: function (param) {
            $(this).datagrid('resize'); //修正页面加载或修改页码时datagrid宽度超出后没有滚动条问题
        },
        onBeforeLoad: function (param) {
            if (!$(this).datagrid('options').loadSwitch) {
                return false;
            }
        }
    });
};
//修改easyui-window默认配置，减少冗余代码
if ($.fn.window != undefined) {
    $.extend($.fn.window.defaults, {
        modal: true,
        //closed: true,  //easyui源码问题，这里设置了默认关闭后，$.messager插件就完全弹不出来了，蛋疼
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () {
            //使弹出框出现在视角正中
            var content = $(window);
            $(this).panel('resize', {
                left: content.scrollLeft() + (content.innerWidth() - $(this).width()) * 0.5,
                top: content.scrollTop() + (content.innerHeight() - $(this).height()) * 0.5
            });
        }
    });
}
//修改easyui-combobox默认配置，减少冗余代码
if ($.fn.combobox != undefined) {
    $.fn.combobox.defaults.panelHeight = 'auto';
}
/*
1、代替form来实现数据提取功能，返回json数据，支持多层json，依赖：String.trim
2、data-type决定数据类型，name、data-name决定键值对中的键，键值对中的值的提取方式取决于数据类型
3、请在根节点标签中指定data-type，或者在方法调用时传参作为data-type(JArray或者JObject)
4、JArray节点的子节点如果不指定data-type，默认也是JArray类型，请为特定的子节点指定特定数据类型(JObject、JValue、JCheckbox)，否则可能无法提取数据
5、JObject节点的子节点如果不指定name或data-name，默认也是JObject，请为特定的子节点指定name、data-name作为键，否则可能无法提取数据
6、标签值的获取方式默认是JValue，即根据标签类型来取值(比如label、div获取html内容作为值，input、textarea获取value属性作为值)，
但也可以人为指定获取方式，比如JCheckbox，
(checkbox默认获取选中状态作为一个布尔值，但JCheckbox则根据标签的选中状态获取其value值，用法，input--checkbox标签属性中添加data-type="JCheckbox")
*/
$.fn.serializeMyJson = (function () {
    /*
    serializeMyJson的子方法，向object添加新的名值对，
    1、如果key已存在，则将key对应的值转化成数组进行添加；
    2、如果value为undifined，则不作任何处理。
    */
    var addItemToObject = function (obj, key, value) {
        if (value != undefined) {
            if (obj[key] != undefined) {
                if (!obj[key].push) {
                    obj[key] = [obj[key]];
                }
                obj[key].push(value);
            } else {
                obj[key] = value;
            }
        }
    };
    return function (type) {
        var o;
        if (type == undefined) {
            type = this.attr("data-type");
        }
        switch (type) {
            case "JArray":
                o = [];
                $.each(this.children(), function () {
                    if ($(this).attr("data-type") == undefined) {
                        o = o.concat($(this).serializeMyJson("JArray"));
                    } else {
                        var temp = $(this).serializeMyJson();
                        if (temp != undefined) {
                            o.push(temp);
                        }
                    }
                });
                break;
            case "JObject":
                o = {};
                $.each(this.children(), function () {
                    if ($(this).attr("type") == "radio" && $(this).prop("checked") == false) {
                        return true;
                    }
                    if ($(this).attr("name") != undefined && $(this).attr("name") != '') {
                        addItemToObject(o, $(this).attr("name"), $(this).serializeMyJson());
                    }
                    if ($(this).attr("data-name") != undefined) {
                        addItemToObject(o, $(this).attr("data-name"), $(this).serializeMyJson());
                    }

                    if ($(this).attr("name") == undefined && $(this).attr("data-name") == undefined) {
                        var tempObj = $(this).serializeMyJson("JObject");
                        for (var name in tempObj) {
                            addItemToObject(o, name, tempObj[name]);
                        }
                    }
                });
                break;
            case "JCheckbox":
                if ($(this).prop("checked")) {
                    o = this.val().trim();
                }
                break;
            case "JValue":
            default: //获取输入框的值，此方法需要根据实际使用进行扩充
                if (this.prop('tagName').toLowerCase() == 'label') {
                    o = this.html().trim();
                } else if (this.prop('type') == 'checkbox') {
                    o = this.prop('checked');
                } else {
                    o = this.val().trim();
                }
                break;
        }
        return o;
    }
})()


    
