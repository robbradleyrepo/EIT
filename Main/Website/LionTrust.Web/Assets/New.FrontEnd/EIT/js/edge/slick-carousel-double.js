"use strict"; 
var $ = require("jquery");

export default () => {
    
    var SlickComponent = /** @class */ (function () {

        function SlickComponent($theComponentSelector) {
            this.$item = $theComponentSelector; 
            this.$controls = this.$item.find('.f-slick__controls'); 
            this.$itemActual = this.$item.find('.f-slick-double');
            this.$desktopItem = this.$item.find('.f-slick-double--lg');
            this.$touchItem = this.$item.find('.f-slick-double--sm');
            this.initSlickComponent();
            this.DesktopSlickComponent();
            this.TouchSlickComponent();
        }
    
        SlickComponent.prototype.initSlickComponent = function () {    
            var _self = this;
            _self.$controls.find('.f-slick-double__controls--prev').on('click', function (e) {
                e.preventDefault(); 
                _self.$desktopItem.slick('slickPrev');
            });
            _self.$controls.find('.f-slick-double__controls--next').on('click', function (e) {
                e.preventDefault(); 
                _self.$desktopItem.slick('slickNext');
            });  
            $(window).on('resize orientationchange', function () { 
                if (_self.$itemActual.hasClass('slick-initialized')) {
                    _self.$itemActual.slick('resize');
                    setTimeout(function () {
                        _self.DesktopSlickComponent();
                        _self.TouchSlickComponent();
                    }, 100);
                }
            });
        };

        SlickComponent.prototype.DesktopSlickComponent = function () { 
            var _self = this;
            _self.$desktopItem.not('.slick-initialized').slick({
                dots: true,
                arrows: false,
                appendDots: _self.$controls.find(".f-slick-double__controls--dots"),
                prevArrow: _self.$controls.find(".f-slick-double__controls--prev"),
                nextArrow: _self.$controls.find(".f-slick-double__controls--next"),
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
        };

        SlickComponent.prototype.TouchSlickComponent = function () { 
            var _self = this;
            _self.$touchItem.not('.slick-initialized').slick({
                dots: false,
                arrows: false,
                infinite: false,
                speed: 200, 
                slidesToShow: 1,
                variableWidth: true,
                mobileFirst: true,
                responsive: [
                    {
                        breakpoint: 992,
                        settings: "unslick" 
                    },
                ]
            });
        };
        return SlickComponent;
    }());

    $(function () {
        var slickComponentHolder = '.f-slick-double-master';
        $(slickComponentHolder).each(function () {
            var slickComponent = new SlickComponent($(this));
        });
    });
}