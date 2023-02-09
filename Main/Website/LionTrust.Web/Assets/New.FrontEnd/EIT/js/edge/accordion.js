"use strict"; 
var $ = require("jquery");

export default () => {
    
    var AccordionComponent = /** @class */ (function () {

        function AccordionComponent($theComponentSelector) {
            $theComponentSelector.beefup();
        }
      
        return AccordionComponent;
    }());


    $(function () {
        var accordionComponentHolder = '.beefup';
        $(accordionComponentHolder).each(function () {
            var accordionComponent = new AccordionComponent($(this));
        });
    });

}


