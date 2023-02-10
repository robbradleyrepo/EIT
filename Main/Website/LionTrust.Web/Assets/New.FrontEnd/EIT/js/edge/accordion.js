"use strict"; 
var $ = require("jquery");

export default () => {
    
    var AccordionComponent = /** @class */ (function () {

        function AccordionComponent($theComponentSelector) {
            
            $('.beefup').beefup({
                scrollOffset: -100, 
                openSingle: true,
                selfClose: true
            }); 

            var $beefup = $('.beefup-close').beefup();
            
            $('.beefup-close').on('click', function () {
                $beefup.close($('#beefup'));                
            });
        };
 
        return AccordionComponent;
    }());

    $(function () {
        var accordionComponentHolder = '.beefup';
        $(accordionComponentHolder).each(function () {
            var accordionComponent = new AccordionComponent($(this));
        });
    });
}