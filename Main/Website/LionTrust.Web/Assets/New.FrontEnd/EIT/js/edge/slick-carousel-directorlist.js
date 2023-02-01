"use strict"; 
var $ = require("jquery");

export default () => {
    
    var DirectorListSlickComponent = /** @class */ (function () {

        function DirectorListSlickComponent($theComponentSelector) {
            this.$item = $theComponentSelector;  
            this.$itemActual = this.$item.find('.f-slick-single');
            this.initDirectorListSlickComponent(); 
        }
    
        DirectorListSlickComponent.prototype.initDirectorListSlickComponent = function () {
                
                var _self = this;
                if (!_self.$itemActual.hasClass('slick-initialized')) {
                    _self.$item.addClass("active");
                }

                _self.$itemActual.slick({
                    dots: false,
                    arrows: false,
                    infinite: false,
                    speed: 200, 
                    variableWidth: true,
                    mobileFirst: true,
                    responsive: [
                        {
                            breakpoint: 992,
                            settings: "unslick" 
                        },
                    ]
                }); 
 
                $(window).on('resize orientationchange', function () { 
                    if (_self.$itemActual.hasClass('slick-initialized')) {
                        _self.$itemActual.slick('resize');
                    } 
                });
        };

     
        return DirectorListSlickComponent;
    }());


    $(function () {
        var directorListSlickComponentHolder = '.f-slick-directorlist-master';
        $(directorListSlickComponentHolder).each(function () {
            var directorListSlickComponent = new DirectorListSlickComponent($(this));
        });
    });

}


