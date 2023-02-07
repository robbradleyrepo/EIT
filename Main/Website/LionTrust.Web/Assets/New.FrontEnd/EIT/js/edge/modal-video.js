"use strict"; 
var $ = require("jquery");

export default () => {
    
    var ModalVideoComponent = /** @class */ (function () {

        function ModalVideoComponent($theComponentSelector) {
            $($theComponentSelector).modalVideo({
                animationSpeed: 200
            });
        }
    
        return ModalVideoComponent;
    }());


    $(function () {
        var modalVideoComponentHolder = '.js-modal-btn';
        $(modalVideoComponentHolder).each(function () {
            var modalVideoComponent = new ModalVideoComponent($(this));
        });
    });

}


