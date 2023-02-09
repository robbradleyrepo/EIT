"use strict"; 
var $ = require("jquery");

export default () => {
    
    var AwardsSlickComponent = /** @class */ (function () {

        function AwardsSlickComponent($theComponentSelector) {
            this.$item = $theComponentSelector;  
            this.$itemActual = this.$item.find('.f-slick-awards');
            this.$controls = this.$item.find('.f-slick-awards__controls'); 
            this.initAwardsSlickComponent(); 
        }
    
        AwardsSlickComponent.prototype.initAwardsSlickComponent = function () {
                
                var _self = this;
                if (!_self.$itemActual.hasClass('slick-initialized')) {
                    _self.$item.addClass("active");
                }

                _self.$controls.find('.f-slick-awards__controls--prev').on('click', function (e) {
                    e.preventDefault(); 
                    _self.$itemActual.slick('slickPrev');
                });
                _self.$controls.find('.f-slick-awards__controls--next').on('click', function (e) {
                    e.preventDefault(); 
                    _self.$itemActual.slick('slickNext');
                }); 

                _self.$itemActual.slick({
                    dots: true,
                    arrows: false,
                    appendDots: _self.$controls.find(".f-slick-awards__controls--dots"),
                    prevArrow: _self.$controls.find(".f-slick-awards__controls--prev"),
                    nextArrow: _self.$controls.find(".f-slick-awards__controls--next"),
                    speed: 250, 
                    infinite: true, 
                    mobileFirst: true,
                    slidesToShow: 1, 
                    responsive: [
                        {
                            breakpoint: 576,
                            settings: {
                                slidesToShow: 2 
                            }  
                        },
                        {
                            breakpoint: 992,
                            settings: {
                                slidesToShow: 3 
                            }  
                        },
                    ]
                }); 
 
                $(window).on('resize orientationchange', function () { 
                    _self.$itemActual.slick('resize');
                });
 
        };

     
        return AwardsSlickComponent;
    }());


    $(function () {
        var awardsSlickComponentHolder = '.f-slick-awards-master';
        $(awardsSlickComponentHolder).each(function () {
            var awardsSlickComponent = new AwardsSlickComponent($(this));
        });
    });

}


