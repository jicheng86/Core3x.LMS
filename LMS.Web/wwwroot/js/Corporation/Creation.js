﻿/// <reference path="../../lib/jquery/v2.1.4/dist/jquery.js" />
$(function () {
    //bvCorporationCreation();
    loadingAreaData();
    //初始化按钮注册事件
    var btnInit = new ButtonInit();
    btnInit.Init();
});

var ButtonInit = function () {
    var oInit = new Object();
    oInit.Init = function () {
        //初始化页面上面的按钮事件
        var FormCorporationCreation = $("#FormCorporationCreation");
        //按钮点击事件注册;
        /*
         * 新增公司提交事件注册
         */
        $('#btnSubmit').click(function () {
            var isValid = true;// FormCorporationCreation.data('bootstrapValidator').validate().isValid();
            if (isValid) {
                //触发from提交事件
                FormCorporationCreation.ajaxSubmit({
                    // url: "/Corporation/Creation",          //默认是form的action， 如果声明，则会覆盖  
                    // type: "post",      //默认是form的method（get or post），如果申明，则会覆盖  
                    //data: FormCorporationCreation.serialize(),//序列化表单，$("form").serialize()只能序列化数据，不能序列化文件
                    dataType: 'json',               //html(默认), xml, script, json...接受服务端返回的类型  
                    timeout: 5 * 1000,              //请求的超时时间：5秒  
                    clearForm: false,               //成功提交后，清除所有表单元素的值  
                    resetForm: false,               //成功提交后，重置所有表单元素的值  
                    processData: false,             //默认情况下，processData 的值是 true，其代表以对象的形式上传的数据都会被转换为字符串的形式上传。而当上传文件的时候，则不需要把其转换为字符串，因此上传文件需要改成false
                    contentType: false,             //前端发送数据的格式, 默认值："application/x-www-form-urlencoded;charset=UTF-8" 代表的是 ajax 的 data 是以键值对字符串的形式,使用这种传数据的格式，无法传输复杂的数据，比如多维数组、文件等。若 form 标签中设置了enctype = “multipart/form-data”,这样请求中的 contentType 就会默认为 multipart/form-data 。而我们在 ajax 中 contentType 设置为 false 是为了避免 JQuery 对其操作。
                    beforeSubmit: function () {
                        //提交之前处理
                        openloading();
                    },
                    success: function (resultData, txtState) {
                        //成功时候处理
                        if (resultData.code == 1) {
                            layer.alert(resultData.message, function (index) {
                                //do something
                                layer.close(index);
                            });
                        }
                        else if (resultData.code == 4) {
                            if (!IsUndefinedOrNull(resultData.data.areaIDs)) {
                                loadingAreaData(resultData.data.areaIDs);
                            }
                            if (!IsUndefinedOrNull(resultData.data.modelErrors)) {
                                $.each(resultData.data.modelErrors, function (key, value) {
                                    if (!IsUndefinedOrNull(key)) {
                                        var selector = $('span[data-valmsg-for=' + key + ']');
                                        selector.removeClass('field-validation-valid');
                                        selector.addClass('field-validation-error');
                                        selector.html(value);
                                    } else {
                                        $('#divModelErrors').empty();
                                        var error = "<ul><li> " + value + "</li></ul>";
                                        $('#divModelErrors').append(error).removeClass('hidden');
                                    }

                                });
                            }
                        }
                        else {
                            layer.alert(resultData.message, function (index) {
                                //do something
                                layer.close(index);
                            });

                        }
                    },
                    complete: function () {
                        //方法完成处理
                        closeAllloading();
                    },
                    error: function (resultData, txtState) {
                        //方法异常处理
                        // layer.msg("请求异常！" + txtState + resultData.message, { time: 3 * 1000, icon: 2 });
                        layer.alert("请求异常！" + txtState + "," + resultData.message, function (index) {
                            //do something
                            layer.close(index);
                        });
                        closeAllloading();
                    }
                });
            };
            return false; //阻止表单默认提交  

        });

        /*
         * 省级地址选择事件联动加载
         */
        $('#seltProvinceAreaID').change(function () {
            loadingAreasOptions(false, "seltProvinceAreaID", "seltCityAreaID");
        });
        /*
        * 市级地址选择联动加载
        */
        $('#seltCityAreaID').change(function () {
            loadingAreasOptions(false, "seltCityAreaID", "seltCistrictAreaID");
        });
        /*
        * 县/区级地址选择事件联动加载
        */
        $('#seltCistrictAreaID').change(function () {
            loadingAreasOptions(false, "seltCistrictAreaID", "seltStreetAreaID");
        });
        /*
         * 选择街道选择事件联动加载
         */
        $('#seltStreetAreaID').change(function () {
            loadingAreasOptions(false, "seltStreetAreaID");
        });

    };
    return oInit;
};
var FormCorporationCreation = $('#FormCorporationCreation');
//预加载验证表单设置
var bvCorporationCreation = function () {
    FormCorporationCreation.bootstrapValidator({
        excluded: [":disabled"],
        message: '这个值没有被验证',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            Name: {
                message: '必填',
                validators: {
                    notEmpty: { message: '请输入公司名称' }
                    , stringLength: {
                        min: 3,
                        max: 50,
                        message: '公司名称请填写5-30个字符'
                    }
                }
            },
            AreaID: {
                message: '必填',
                validators: {
                    notEmpty: { message: '请选择公司所在区划' }
                }
            },
            CorporationAddress: {
                message: '必填',
                validators: {
                    notEmpty: { message: '请填写公司所在区划之后的详细地址' }
                }
            }
        }
    });
};

//加载页面区划控件数据
var loadingAreaData = function (AreaIDs) {

    if (IsUndefinedOrNull(AreaIDs)) {
        /*
         * 页面载入时加载省级下拉菜单
         */
        loadingAreasOptions(true, "seltProvinceAreaID", "seltCityAreaID");
    }
    else {
        var AreaIDList = AreaIDs.split(',');
        if (AreaIDList.length == 2) {
            loadingAreasOptions(true, "seltProvinceAreaID", "seltCityAreaID", AreaIDList[0]);
            loadingAreasOptions(true, "seltCityAreaID", "seltCistrictAreaID", AreaIDList[1], AreaIDList[0]);
        }
        if (AreaIDList.length == 4) {
            loadingAreasOptions(true, "seltProvinceAreaID", "seltCityAreaID", AreaIDList[0]);
            loadingAreasOptions(true, "seltCityAreaID", "seltCistrictAreaID", AreaIDList[1], AreaIDList[0]);
            loadingAreasOptions(true, "seltCistrictAreaID", "seltStreetAreaID", AreaIDList[2], AreaIDList[1]);
            loadingAreasOptions(true, "seltStreetAreaID", "", AreaIDList[3], AreaIDList[2]);
        }
    }
};

