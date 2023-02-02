"use strict"; 
var $ = require("jquery");

export default () => {
    
    var DirectorListSlickComponent = /** @class */ (function () {

        function DirectorListSlickComponent($theComponentSelector) {
            this.$item = $theComponentSelector;  
            this.$itemActual = this.$item.find('.f-slick-single');
            this.$moreControl = this.$item.find('.f-read-more');
            this.$lessControl = this.$item.find('.f-read-less');
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
                    _self.$itemActual.slick('resize');
                });

                _self.$moreControl.on('click', function (e) {
                    e.preventDefault(); 
                    _self.$lessControl.removeClass('hidden');
                    _self.$moreControl.addClass('hidden');
                    _self.$itemActual.addClass('expand'); 
                });
                _self.$lessControl.on('click', function (e) {
                    e.preventDefault(); 
                    _self.$lessControl.addClass('hidden');
                    _self.$moreControl.removeClass('hidden');
                    _self.$itemActual.removeClass('expand'); 
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


