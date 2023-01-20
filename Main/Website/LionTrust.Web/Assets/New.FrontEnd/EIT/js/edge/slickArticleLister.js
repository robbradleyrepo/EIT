"use strict"; 
var $ = require("jquery");

export default () => {

    $('.f-slick__controls--prev').on('click', function (e) {
        e.preventDefault(); 
        $('.c-article-list .f-slick-lg').slick('slickPrev');
    });  

    $('.f-slick__controls--next').on('click', function (e) {
        e.preventDefault(); 
        $('.c-article-list .f-slick-lg').slick('slickNext');
    });  

	 
    function slickOnResizeD(){
        $('.c-article-list .f-slick-lg').not('.slick-initialized').slick({
            dots: true,
            arrows: false,
            appendDots: $(".f-slick__controls--dots"),
            prevArrow: $(".f-slick__controls--prev"),
            nextArrow: $("f-slick__controls--next"),
            infinite: false,
            speed: 250, 
            slidesPerRow: 3,
            rows: 2, 
            fade: true,
            cssEase: 'linear',
            customPaging: function(slider, i) {
                var thumb = $(slider.$slides[i]).data();
                return (i + 1);
            },
            responsive: [
                {
                    breakpoint: 992,
                    settings: "unslick"  
                },
            ]
        });
    }

    function slickOnResizeM(){
        $('.c-article-list .f-slick-sm').not('.slick-initialized').slick({
            dots: false,
            arrows: false,
            infinite: false,
            speed: 200, 
            mobileFirst: true,
            variableWidth: true,
            responsive: [
                {
                    breakpoint: 992,
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