"use strict"; 
var $ = require("jquery");

export default () => {

	 
    function slickOnResizeD(){
        $('.c-article-list .f-slick-lg').not('.slick-initialized').slick({
            dots: true,
            infinite: false,
            speed: 200, 
            slidesPerRow: 3,
            rows: 2,
            responsive: [
                {
                    breakpoint: 992,
                    settings: {
                    slidesPerRow: 2,
                    rows: 2,
                    }
                },
                {
                    breakpoint: 768,
                    settings: "unslick"
                    
                },
                
            ]
        });
    }

    function slickOnResizeM(){
        $('.c-article-list .f-slick-sm').not('.slick-initialized').slick({
            dots: true,
            infinite: false,
            speed: 200, 
            slidesToShow: 1,
            mobileFirst: true,
            responsive: [
                {
                    breakpoint: 768,
                    settings: "unslick"
                    
                },
                
            ]
        });
    }

    function destroyCarousel() {
        if ($('.c-article-list .f-slick').hasClass('slick-initialized')) {
          $('.f-slick').slick('resize');
        }      
    }

    $(window).on('resize orientationchange', function () { 
        destroyCarousel();
        setTimeout(function () {
            slickOnResizeM();
            slickOnResizeD();
        }, 100);
        
    });
 
    slickOnResizeM(); 
    slickOnResizeD(); 
}