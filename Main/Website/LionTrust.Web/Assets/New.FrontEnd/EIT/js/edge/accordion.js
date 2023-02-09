"use strict"; 
var $ = require("jquery");

export default () => {
    
    var AccordionComponent = /** @class */ (function () {

        function AccordionComponent($theComponentSelector) {
            this.$componentSelector = $theComponentSelector;   
            this.$closeControl = this.$componentSelector.find('.beefup-close');  
            
            this.$componentSelector.beefup({
                scrollOffset: -100
            }); 

            this.closeAccordionComponent();
        };

        AccordionComponent.prototype.closeAccordionComponent = function () {
            var _self = this; 
            _self.$closeControl.on('click', function (e) {
                e.preventDefault(); 
             
                _self.$componentSelector.close($(_self.$componentSelector));
                _self.$componentSelector.scroll($(_self.$componentSelector));
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


