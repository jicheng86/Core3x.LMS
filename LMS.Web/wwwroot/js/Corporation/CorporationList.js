$(function () {
    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();
    //2.初始化Button的点击事件
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();





});

//初始化Table list数据
var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_list').bootstrapTable({
            // data: [],//要加载的数据。
            url: '/Corporation/CorporationListData', //从远程站点请求数据的URL。     请注意，所需的服务器响应格式取决于是否'sidePagination' 指定了该选项
            method: 'get', //请求方式,默认:'get'，可选"post"...
            toolbar: '#toolbar', //工具按钮用哪个容器
            striped: true, //是否显示行间隔色
            cache: false, //是否使用缓存，默认为true，设置false：禁用AJAX请求的缓存。
            contentType: "application/json", //请求远程数据的contentType，例如：application/x-www-form-urlencoded. 默认: 'application/json'
            dataType: "json",//您期望从服务器返回的数据类型。默认: 'json'
            //ajax: undefined,// Function 类型， 一种替换ajax调用的方法。应该实现与jQuery ajax方法相同的API。
            //ajaxOptions: {},//默认:{}
            sortable: true, //是否启用排序
            sortOrder: "desc", //排序方式,默认"asc"
            silentSort: "false",//设置false 为使用加载消息对数据进行排序。当sidePagination选项设置为"'server'"时，此选项有效 .默认: true
            queryParams: oTableInit.queryParams(queryParam), //请求远程数据时，可以通过修改queryParams发送其他参数。            如果 queryParamsType = 'limit'，params对象包含：limit, offset, search, sort, order.   否则，它包含：pageSize, pageNumber, searchText, sortName, sortOrder. 返回 false停止请求。       默认: function (params) { return params }
            responseHandler: function (res) {
                return res;
            },//默认： function(res) { return res }
            queryParamsType: "", //默认: 'limit'  "" = {pageSize:10, pageNumber:1, searchText, sortName, sortOrder}
            sidePagination: "server", //分页方式：client客户端分页，server服务端分页定义, 使用 'server'side需要设置'url' 或 'ajax' 选项。 请注意，根据 'sidePagination' 选项设置为 'client' 还是，所需的服务器响应格式会有所不同 'server'。
            pagination: true, //是否显示分页（*）
            pageNumber: 1, //初始化加载第一页，默认第一页
            pageSize: 10, //每页的记录行数（*）
            pageList: [10, 25, 50, 100], //默认:[10, 25, 50, 100],可供选择的每页的行数（*）
            paginationSuccessivelySize: 5,//可选页码数量。如："<①②③④⑤>"
            paginationPagesBySide:1,
            paginationUseIntermediate: true,//计算并显示中间页面以便快速访问。
            search: true, //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            searchOnEnterKey: false,//按下enter健才搜索
            strictSearch: true, //启用严格搜索。禁用比较检查。默认: false
            showColumns: true, //是否显示所有的列
            showRefresh: true, //是否显示刷新按钮
            showColumnsToggleAll: true,//设置true 为在列选项/下拉列表中显示“全部切换”复选框。
            showFullscreen: true,//设置true显示全屏按钮。
            minimumCountColumns: 2, //最少允许的列数
            escape: false,//转义用于插入HTML的字符串，并替换 &, <, >, “, `, and ‘ 字符。默认: false
            clickToSelect: true, //是否启用点击选中行
            checkboxHeader: true,//设置false为隐藏标题行中的所有复选框。默认: true
            singleSelect: true, //禁止多选
            cardView: false,//设置true 为显示名片视图表，例如移动视图。默认: false
            detailView: false,//设置true为显示详细视图表。默认: false
            detailViewByClick: false,//设置true单击以设置切换细节视图。默认: false
            height: 500,      //表的高度，启用表的固定标题。
            classes: "table table-bordered table-hover",//使用classes选项设置表格样式。默认classes 值为 'table table-bordered table-hover'.
            theadClasses: ".thead-light", //使用theadClasses选项设置表标题样式。三种模式  undefined(默认)  .thead-light 或 .thead-dark 
            headerStyle: function (column) {
                return {
                    css: { 'font-weight': 'normal' },
                    //classes: 'my-class'
                }
            }, //标头样式格式化程序函数采用一个参数
            rowStyle: function (row, index) {
            },//行样式
            locale: "zh-CN",//本地语言
            uniqueId: "ID", //每一行的唯一标识，一般为主键列
            showToggle: true, //是否显示详细视图和列表视图的切换按钮
            columns: [
                {
                    checkbox: true
                }, {
                    field: 'Name',
                    title: '公司名称'
                }, {
                    field: 'Cst_Name',
                    title: '客户姓名',
                    formatter: function (value, row, index) {
                        return '<a href="/Customer/CustomerDetail?CustomerId=' + row.Cst_ID + '" title="客户姓名">' + value + '</a>';
                    }
                }, {
                    field: 'Cst_CredentialsNum',
                    title: '证件号码',
                    formatter: function (value, row, index) {
                        if (value != null && value != '' && value != undefined) {
                            var length = value.length;
                            var hStr = value.substring(0, 4);
                            var fStr = value.substring(length - 4, length);
                            return hStr + '********' + fStr;
                        }
                    }
                }, {
                    field: 'Cst_Mobile',
                    title: '联系电话',
                    formatter: function (value, row, index) {
                        if (value != null && value != '' && value != undefined) {
                            var length = value.length;
                            var hStr = value.substring(0, 3);
                            var fStr = value.substring(length - 4, length);
                            return hStr + '****' + fStr;
                        }
                    }
                }, {
                    field: 'Cst_Sex',
                    title: '性别',
                    formatter: function (value, row, index) {
                        switch (value) {
                            case 1:
                                return "男";
                            case 0:
                                return "女";
                            default:
                                break;
                        }
                    }
                }, {
                    field: 'SNA_XWId',
                    title: '新网开户UserId',
                    visible: false,
                    formatter: function (value, row, index) {
                        if (value !== '' && value !== null && value !== undefined) {
                            return value;
                        }
                        return row.SNA_CstId;
                    }
                }, {
                    field: 'Cst_IsBlacklist',
                    title: '是否黑名单',
                    formatter: function (value, row, index) {
                        switch (value) {
                            case false:
                                return "否";
                            case true:
                                return "是";
                            default:
                                break;
                        }
                    }
                },
                {
                    field: 'Cst_DataError',
                    title: '是否暂存',
                    formatter: function (value, row, index) {
                        switch (value) {
                            case false:
                                return "否";
                            case true:
                                return "是";
                            case null:
                                return "否";
                            default:
                                break;
                        }
                    }
                }, {
                    field: 'Cst_Empower',
                    title: '是否授权',
                    formatter: function (value, row, index) {
                        switch (value) {
                            case false:
                            case null:
                                return "否";
                            case true:
                                return "是";
                            default:
                                break;
                        }
                    }
                }, {
                    field: 'ID',
                    title: '操作',
                    formatter: function (value, row, index) {
                        var button = '<button id="btnUpdateCustomer" class="btn btn-primary" style="margin-left:5px;" onclick="UpdateCustomer(' + value + ')">完善客户信息</button>';
                        return button;
                    }
                }
            ],
            formatNoMatches: function () { //没有匹配的结果
                return '没有找到匹配的记录';
            }
        });
    };
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var fromPage = {
            //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            //queryParamsType:"", pageSize:10, pageNumber:1, searchText, sortName, sortOrder
            pageSize: params.pageSize, //页面大小
            pageNumber: params.pageNumber, //当前页面及之前数据总和 
            searchText: params.searchText,//查询字符串
            sortName: params.sortName,//排序名称
            sortOrder:params.sortOrder//排序顺序
        };
        return fromPage;
    };
    return oTableInit;
};
//初始化按钮方法
var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};

    oInit.Init = function () {
        //初始化页面上面的按钮事件

        //提交数据
        $('#btnSubmit').click(function () {
            var oTable = new TableInit();
            //oTable.Init();
            //oTable.queryParams(postdata);
            alert("Submit");
        });


        //修改按钮事件注册
        $('#btnUpdate').click(function () {
            var row = $('#tb_list').bootstrapTable('getSelections')[0];
            if (row) {
                window.location.href = '/Customer/CustomerEdit?CustomerId=' + row.Cst_ID;
            } else {
                swal({ title: "请选择一条数据！", icon: "warning", button: "确定" });
            }
        });
        //3.列表查询
        $('#btnSelect').click(function () {
            //将当前页初始化第一页
            $('#tb_list').bootstrapTable('refreshOptions', { pageNumber: 1 });
            //列表刷新
            $('#tb_list').bootstrapTable('refresh');
        });
        //5.验证表单设置
        $('#formCustomerEdit').bootstrapValidator({
            excluded: [":disabled"],
            message: '这个值没有被验证',
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            fields: {
                Cst_Name: {
                    message: '必填',
                    validators: {
                        notEmpty: { message: '请输入用户姓名' }
                    }
                },
                Cst_CredentialsType: {
                    message: '必填',
                    validators: {
                        notEmpty: { message: '请选择个人证件类型' }
                    }
                },
                Cst_CredentialsNum: {
                    message: '必填',
                    validators: {
                        notEmpty: { message: '请输入证件号码' },
                        regexp: {
                            regexp: /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/,
                            message: '请输入正确的证件号码'
                        }
                    }
                },
                Cst_Phone: {
                    message: '必填',
                    validators: {
                        regexp: {
                            regexp: /^(0\d{2,3}-\d{7,8})$/,
                            message: '请输入正确的座机号码'
                        }
                    }
                },
                Cst_Mobile: {
                    message: '必填',
                    validators: {
                        notEmpty: { message: '请输入常用手机' },
                        stringLength: {
                            min: 11,
                            max: 11,
                            message: '请输入正确的常用手机'
                        },
                        digits: {
                            message: '请输入数字'
                        }
                    }
                },
                Cst_Mobile2: {
                    message: '必填',
                    validators: {
                        stringLength: {
                            min: 11,
                            max: 11,
                            message: '请输入正确的常用手机'
                        },
                        digits: {
                            message: '请输入数字'
                        }
                    }
                },
                Cst_MarriageStatus: {
                    message: '必填',
                    validators: {
                        notEmpty: { message: '请选择婚姻状况' }
                    }
                },
                Cst_Sex: {
                    message: '必填',
                    validators: {
                        notEmpty: { message: '请选择性别' }
                    }
                },
                Cst_Nation: {
                    message: '必填',
                    validators: {
                        notEmpty: { message: '请输入民族' }
                    }
                },
                Cst_Education: {
                    message: '必填',
                    validators: {
                        notEmpty: { message: '请选择学历' }
                    }
                },
                //seach_Province: {
                //    message: '必填',
                //    validators: {
                //        notEmpty: { message: '请选择身份证地址' }
                //    }
                //},
                Cst_AddressNew: {
                    message: '必填',
                    validators: {
                        notEmpty: { message: '请输入现住地址' }
                    }
                },
                Cst_Address: {
                    message: '必填',
                    validators: {
                        notEmpty: { message: '请输入详细地址' }
                    }
                },
                Cst_Residence: {
                    message: '必填',
                    validators: {
                        notEmpty: { message: '请选择住宅性质' }
                    }
                },
                Cst_DriverLicenseNum: {
                    message: '必填',
                    validators: {
                        notEmpty: { message: '请输入驾驶证编号' }
                        , regexp: {
                            regexp: /^[a-zA-Z0-9]+$/,
                            message: '驾驶证编号只包含字母/数字！'
                        }
                        //, regexp: {
                        //    regexp: /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/,
                        //    message: '请输入正确的驾驶证编号'
                        //}
                    }
                },
                Cst_DriverLicenseIssueDate: {
                    message: '必填',
                    validators: {
                        notEmpty: { message: '请选择驾驶证发证日期' }
                    }
                },
                Cst_SpouseMobile: {
                    message: '必填',
                    validators: {
                        stringLength: {
                            min: 11,
                            max: 11,
                            message: '请输入11位手机号码'
                        }, digits: {
                            message: '该值输入数字'
                        }
                    }
                },
                Cst_SpouseID: {
                    message: '必填',
                    validators: {
                        regexp: {
                            regexp: /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/,
                            message: '请输入正确的身份证编号'
                        }
                    }
                },
                Cst_Email: {
                    validators: {
                        regexp: {
                            regexp: /^[a-zA-Z0-9]+([._\\-]*[a-zA-Z0-9])*@([a-zA-Z0-9]+[-a-zA-Z0-9]*[a-zA-Z0-9]+.){1,63}[a-zA-Z0-9]+$/,
                            message: '请输入正确的电子邮箱'
                        }
                    }
                },
                Cst_WorkingLife: {
                    validators: {
                        digits: {
                            message: '请输入数字'
                        },
                        stringLength: {
                            min: 0,
                            max: 2,
                            message: '请输入正确的工作年限'
                        }
                    }
                },
                txtBA_BankID: {
                    validators: {
                        notEmpty: { message: '请选择开户银行' }
                    }
                },
                BA_AccountName: {
                    validators: {
                        notEmpty: { message: '请输入开户名称' }
                    }
                },
                BA_AccountNo: {
                    validators: {
                        notEmpty: { message: '请输入银行卡号' },
                        regexp: {
                            regexp: /^[0-9\s]*$/,
                            message: '请输入正确的银行卡号'
                        }
                    }
                },

                BA_BranchName: {
                    validators: {
                        notEmpty: { message: '请输入支行名称' }
                    }
                },
                CU_Sex: {
                    validators: {
                        notEmpty: { message: '请选择联系人性别' }
                    }
                },
                CU_Relationship: {
                    validators: {
                        notEmpty: { message: '请选择联系人与本人关系' }
                    }
                },

                CU_Sex2: {
                    validators: {
                        notEmpty: { message: '请选择联系人性别' }
                    }
                },
                CU_Relationship2: {
                    validators: {
                        notEmpty: { message: '请选择联系人与本人关系' }
                    }
                },
                CU_Sex3: {
                    validators: {
                        notEmpty: { message: '请选择联系人性别' }
                    }
                },
                CU_Relationship3: {
                    validators: {
                        notEmpty: { message: '请选择联系人与本人关系' }
                    }
                },
                Cst_liveLife: {
                    validators: {
                        digits: { message: '请输入数字' }
                    }
                },
                Cst_Socialrity: {
                    validators: {
                        notEmpty: { message: '请选择社保状态' }
                    }
                },
                chooseAreaID: {
                    trigger: "change",
                    validators: {
                        notEmpty: {
                            message: '请选择完整身份证地址'
                        }
                    }
                },
                Cst_Income: {
                    validators: {
                        callback: {
                            message: '请输入正确的金额',
                            callback: function (value, validator) {
                                if (value == "") {
                                    return true;
                                }
                                var reg = /(^[1-9]([0-9]+)?(\.[0-9]{1,2})?$)|(^(0){1}$)|(^[0-9]\.[0-9]([0-9])?$)/;
                                if (reg.test(value)) {
                                    return true;
                                } else {
                                    return false;
                                };
                            }
                        }
                    }
                },
                CU_Name: {
                    validators: {
                        notEmpty: {
                            message: '请输入联系人名称'
                        },
                        callback: {
                            message: '输入联系人名称已存在',
                            callback: function (value, validator) {
                                if (value == $('#CU_Name2').val() && $('#CU_Name2').val() != '' || value == $('#CU_Name3').val() && $('#CU_Name3').val() != '') {
                                    return false;
                                } else {
                                    return true;
                                }
                            }
                        }
                    }
                },
                CU_Name2: {
                    validators: {
                        notEmpty: {
                            message: '请输入联系人名称'
                        },
                        callback: {
                            message: '输入联系人名称已存在',
                            callback: function (value, validator) {
                                if (value == $('#CU_Name').val() && $('#CU_Name').val() != '' || value == $('#CU_Name3').val() && $('#CU_Name3').val() != '') {
                                    return false;
                                } else {
                                    return true;
                                }
                            }
                        }
                    }
                },
                CU_Name3: {
                    validators: {
                        notEmpty: {
                            message: '请输入联系人名称'
                        },
                        callback: {
                            message: '联系人名称已存在',
                            callback: function (value, validator) {
                                if (value == $('#CU_Name2').val() && $('#CU_Name2').val() != '' || value == $('#CU_Name').val() && $('#CU_Name').val() != '') {
                                    return false;
                                } else {
                                    return true;
                                }
                            }
                        }
                    }
                },
                CU_Phone: {
                    validators: {
                        notEmpty: {
                            message: '请输入联系人电话'
                        },
                        stringLength: {
                            min: 11,
                            max: 11,
                            message: '请输入11位联系电话'
                        },
                        digits: {
                            message: '请输入数字'
                        },
                        callback: {
                            message: '联系电话已存在',
                            callback: function (value, validator) {
                                if (value == $('#CU_Phone2').val() && $('#CU_Phone2').val() != '' || value == $('#CU_Phone3').val() && $('#CU_Phone3').val() != '') {
                                    return false;
                                } else {
                                    return true;
                                }
                            }
                        }
                    }
                },
                CU_Phone2: {
                    validators: {
                        notEmpty: {
                            message: '请输入联系电话'
                        },
                        stringLength: {
                            min: 11,
                            max: 11,
                            message: '请输入11位联系电话'
                        },
                        digits: {
                            message: '请输入数字'
                        },
                        callback: {
                            message: '联系电话已存在',
                            callback: function (value, validator) {
                                if (value == $('#CU_Phone').val() && $('#CU_Phone').val() != '' || value == $('#CU_Phone3').val() && $('#CU_Phone3').val() != '') {
                                    return false;
                                } else {
                                    return true;
                                }
                            }
                        }
                    }
                },
                CU_Phone3: {
                    validators: {
                        notEmpty: {
                            message: '请输入联系人电话'
                        },
                        stringLength: {
                            min: 11,
                            max: 11,
                            message: '请输入11位联系电话'
                        },
                        digits: {
                            message: '请输入数字'
                        },
                        callback: {
                            message: '联系电话已存在',
                            callback: function (value, validator) {
                                if (value == $('#CU_Phone').val() && $('#CU_Phone').val() != '' || value == $('#CU_Phone2').val() && $('#CU_Phone2').val() != '') {
                                    return false;
                                } else {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
        });
    };
    return oInit;
};