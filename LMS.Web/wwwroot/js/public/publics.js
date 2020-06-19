//打开加载层
var openloading = function () {
    layer.load(0, { time: 10 * 1000 });
};
//关闭所有加载层
var closeAllloading = function () {
    layer.closeAll('loading');
};
