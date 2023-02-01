"use strict"; 
var $ = require("jquery");

export default () => {

    var ReadMoreComponent = /** @class */ (function () {

        function ReadMoreComponent($theComponentSelector) {
            this.$componentSelector = $theComponentSelector;    
            this.clickReadMoreComponent(); 
        }
          
       
        // event listeners
        ReadMoreComponent.prototype.clickReadMoreComponent = function () {
          
           
        };

       
 
        return ReadMoreComponent;
    }());

    $(function () {
        var readMoreComponentHolder = '.f-read-more';
        $(readMoreComponentHolder).each(function () {
            var readMoreComponent = new ReadMoreComponent($(this));
        });
    });

}