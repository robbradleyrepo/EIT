"use strict"; 
var $ = require("jquery");

export default () => {
    
    var carouselDouble = $(".f-slick-double");
    var carouselSingle = $(".f-slick-single");

    carouselDouble.each(function() {   

        $('.f-slick-double__controls--prev').on('click', function (e) {
            e.preventDefault(); 
            $('.f-slick-double--lg').slick('slickPrev');
        });  

        $('.f-slick-double__controls--next').on('click', function (e) {
            e.preventDefault(); 
            $('.f-slick-double--lg').slick('slickNext');
        });  

        
        function slickOnResizeD(){
            $('.f-slick-double--lg').not('.slick-initialized').slick({
                dots: true,
                arrows: false,
                appendDots: $(".f-slick-double__controls--dots"),
                prevArrow: $(".f-slick-double__controls--prev"),
                nextArrow: $(".f-slick-double__controls--next"),
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
            $('.f-slick-double--sm').not('.slick-initialized').slick({
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

        function destroyDoubleCarousel() {
            if ($('.f-slick-double').hasClass('slick-initialized')) {
                $('.f-slick-double').slick('resize');
            }      
        }

        $(window).on('resize orientationchange', function () { 
            destroyDoubleCarousel();
            setTimeout(function () {
                slickOnResizeM();
                slickOnResizeD();
            }, 100);
            
        });
    
        slickOnResizeM(); 
        slickOnResizeD(); 
    
    
    });

    carouselSingle.each(function() {   

        $('.f-slick-single__controls--prev').on('click', function (e) {
            e.preventDefault(); 
            $('.f-slick-single').slick('slickPrev');
        });  

        $('.f-slick-single__controls--next').on('click', function (e) {
            e.preventDefault(); 
            $('.f-slick-single').slick('slickNext');
        });  

       

        function slickOnResize(){
            $('.f-slick-single').slick({
                
                infinite: false,
                speed: 200, 
                mobileFirst: false,
                variableWidth: true,
                dots: false,
                arrows: false, 
                 
                 
            }); 
        }
      
        
    
        slickOnResize();
    
    });

    
}