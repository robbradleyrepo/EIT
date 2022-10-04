export default () => {
    //active class
    var navLinks = $(".page-anchor__links li a");
    var pathname = location.pathname.split(".");
    pathname.pop();
    
    var pathnameTxt = pathname.join();
    
    navLinks.each(function(){
        if($(this).attr("href").indexOf(pathnameTxt) !== -1) {
            $(this).addClass("active")
        }
    })

    //toggle nav mobile
    var selectBtn = $(".people-anchors .page-anchor__title");
    selectBtn.on('click', function(){
        if(window.innerWidth < 992) {
            $(this).toggleClass("active");
            $(this).next().toggleClass("open");
        }
    });
}