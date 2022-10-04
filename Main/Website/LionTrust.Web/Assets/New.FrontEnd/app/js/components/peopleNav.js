export default () => {
    var navLinks = $(".page-anchor__links li a");
    var pathname = location.pathname.split(".");
    pathname.pop();
    
    var pathnameTxt = pathname.join();
    

    navLinks.each(function(){
        if($(this).attr("href").indexOf(pathnameTxt) !== -1) {
            $(this).addClass("active")
        }
    })

}