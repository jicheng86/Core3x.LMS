
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
var loadingAreasOptions = function (initialLoading, thisSelectID, nextSelectID, thisSelectedValue, thisParentIDValue) {
    var select = $('#' + thisSelectID);//当前操作ID
    if (initialLoading) {
        //隐藏所有select 
        $('.show-tick').selectpicker('hide');
        if (IsUndefinedOrNull(thisSelectedValue)) {
            thisSelectedValue = 0;
        }
        if (IsUndefinedOrNull(thisParentIDValue)) {
            thisParentIDValue = 1000;
        }

        //加载区划下拉菜单
        $.ajax({
            url: "/API/GetAreasSelectOptions",
            type: "post",
            datatype: "json",
            data: { ParentID: thisParentIDValue, SelectedID: thisSelectedValue, HadEmptyItem: false },
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
                    if (thisSelectedValue != 0) {
                        var sid = select.find("option:selected").val();
                    }
                    select.selectpicker('refresh');
                    if (thisSelectedValue != 0) {
                        select.selectpicker('val', sid);
                    }
                }
            },
            complete: function () {
                closeAllloading();
            }
        });
    }
    else {
        if (IsUndefinedOrNull(thisSelectedValue)) {
            var selectedID = select.selectpicker('val');
            select.selectpicker('val', IsUndefinedOrNull(thisSelectedValue) ? selectedID : thisSelectedValue);
            select.selectpicker('refresh');
        }
        else {
            var selectedID = 1000;
        }
        var next = null;
        if (!IsUndefinedOrNull(nextSelectID)) {
            next = $('#' + nextSelectID);//关联下级ID
        }
        $.ajax({
            url: "/API/GetAreasSelectOptions",
            type: "post",
            async: true,
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
                    if (IsUndefinedOrNull(thisSelectedValue)) {
                        next.empty();
                        next.append(jsondata.data);
                        next.selectpicker('show');
                        next.selectpicker('render');
                        next.selectpicker('refresh');
                    }
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
