
//打开加载层
var openloading = function () {
    layer.load(0, { time: 10 * 1000 });
};
//关闭所有加载层
var closeAllloading = function () {
    layer.closeAll('loading');
};
//判断值是否是为空
var IsUndefinedOrNull = function (value) {
    if (value = undefined || value == null || value == '') {
        return true;
    }
    return false;

};
//区划地址选择select加载数据
var loadingAreasOptions = function (initialLoading, thisSelectID, thisSelectedValue, nextSelectID, form, bvid) {
    var select = $('#' + thisSelectID);//当前操作ID
    if (initialLoading) {
        //隐藏所有select 
        $('.show-tick').selectpicker('hide');
        //加载省级下拉菜单
        $.ajax({
            url: "/API/GetAreasSelectOptions",
            type: "post",
            async: false,
            datatype: "json",
            data: { ParentID: 1000, SelectedID: thisSelectedValue },
            beforeSend: function () {
                $('.show-tick').selectpicker('hide');
                select.selectpicker('show');
                openloading();
            },
            success: function (jsondata) {
                if (jsondata.code == 1) {
                    select.empty();
                    select.append(jsondata.data);
                    select.selectpicker('show');
                    select.selectpicker('render');
                    select.selectpicker('refresh');
                }
            },
            complete: function () {
                if (!IsUndefinedOrNull(nextSelectID)) {
                    var next = $('#' + nextSelectID);//关联下级ID
                    next.prop('disabled', false);
                    next.selectpicker('refresh');
                }
                select.selectpicker('show');
                closeAllloading();
            }
        });
    }
    else {
        //NOT_VALIDATED：未校验的;
        //VALIDATING：校验中的;
        //INVALID ：校验失败的;
        //VALID：校验成功的。
        form.bootstrapValidator('updateStatus', bvid, 'NOT_VALIDATED');
        var selectedID = select.selectpicker('val');
        select.selectpicker('val', IsUndefinedOrNull(thisSelectedValue) ? selectedID : thisSelectedValue);
        select.selectpicker('refresh');
        var next = null;
        if (!IsUndefinedOrNull(nextSelectID)) {
            next = $('#' + nextSelectID);//关联下级ID
        }
        $.ajax({
            url: "/API/GetAreasSelectOptions",
            type: "post",
            datatype: "json",
            data: { ParentID: selectedID, SelectedID: thisSelectedValue },
            beforeSend: function () {
                select.prop('disabled', true);
                select.selectpicker('refresh');
                $('.show-tick').selectpicker('hide');
                select.selectpicker('show');
                select.parent().prevAll().find('select').selectpicker('show');
                openloading();
            },
            success: function (jsondata) {
                if (jsondata.code == 1 && next != null) {
                    next.empty();
                    next.append(jsondata.data);
                    next.selectpicker('show');
                    next.selectpicker('render');
                    next.selectpicker('refresh');
                }
            },
            complete: function () {
                select.prop('disabled', false);
                select.selectpicker('refresh');
                closeAllloading();
            }
        });

    }


};
//加载区划地址select
var loadingAreasSelect = function (from, bvid, provinceID, provinceValue, cityAreaID, cityValue, cistrictAreaID, cistrictValue, streetAreaID, streetValue) {

    if (IsUndefinedOrNull(from) || IsUndefinedOrNull(bvid) || IsUndefinedOrNull(provinceValue)) {
        layer.message("参数为空，请核实！", { icon: 2, time: 3 * 1000, comtent, title: "区划地址加载错误" });
        return false;
    }
    if (provinceValue == 35925 || provinceValue == 35945) {
        if (IsUndefinedOrNull(cityValue)) {
            layer.message("参数为空，请核实！", { icon: 2, time: 3 * 1000, comtent, title: "区划地址加载错误" });
            return false;
        }
        //todo 特别行政区
        loadingAreasOptions(true, provinceID, provinceValue, cityAreaID, from, bvid);
        loadingAreasOptions(false, cityAreaID, cityValue, "", from, bvid);
    }
    else if (IsUndefinedOrNull(cistrictValue) || IsUndefinedOrNull(streetValue)) {
        layer.message("参数为空，请核实！", { icon: 2, time: 3 * 1000, comtent, title: "区划地址加载错误" });
        return false;
    }
    else {
        loadingAreasOptions(true, provinceID, provinceValue, cityAreaID, from, bvid);
        loadingAreasOptions(false, cityAreaID, cityValue, cistrictAreaID, from, bvid);
        loadingAreasOptions(false, cistrictAreaID, cistrictValue, streetAreaID, from, bvid);
        loadingAreasOptions(false, streetAreaID, streetValue, "", from, bvid);
    }
};
