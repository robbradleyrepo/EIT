"use strict"; 
var $ = require("jquery");

export default () => {
    
    var SingleSlickComponent = /** @class */ (function () {

        function SingleSlickComponent($theComponentSelector) {
            this.$item = $theComponentSelector;  
            this.$itemActual = this.$item.find('.f-slick-single');
            this.initSingleSlickComponent(); 
        }
    
        SingleSlickComponent.prototype.initSingleSlickComponent = function () {
                
                var _self = this;

                _self.$itemActual.slick({
                    infinite: false,
                    speed: 200, 
                    mobileFirst: false,
                    variableWidth: true,
                    dots: false,
                    arrows: false, 
                }); 
 
                $(window).on('resize orientationchange', function () { 
                    if (_self.$itemActual.hasClass('slick-initialized')) {
                        _self.$itemActual.slick('resize');
                    } 
                });
        };

     
        return SingleSlickComponent;
    }());


    $(function () {
        var singleSlickComponentHolder = '.f-slick-single-master';
        $(singleSlickComponentHolder).each(function () {
            var singleSlickComponent = new SingleSlickComponent($(this));
        });
    });

}


