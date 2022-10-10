export default () => {
    //active class
    var navLinks = $(".page-anchor__links li a");
    var pathname = location.pathname;
   
    navLinks.each(function(){
        if($(this).attr("href") === pathname) {
            $(this).addClass("active")
            if(window.innerWidth < 992) {
                $(".page-anchor__title > span").text($(this).text()).addClass("text-primary");
            }
        }
    })

    //toggle nav mobile
    var selectBtn = $(".people-anchors .page-anchor__title");
    selectBtn.on('click', function(){
        if(window.innerWidth < 992) {
            $(this).toggleClass("active");
            $(this).next().slideToggle().toggleClass("open");
        }
    });

}