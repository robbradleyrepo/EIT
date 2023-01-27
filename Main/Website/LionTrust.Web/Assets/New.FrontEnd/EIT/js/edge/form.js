"use strict"; 
var $ = require("jquery");

export default () => {

    var FormComponent = /** @class */ (function () {

        function FormComponent($theComponentSelector) {
            this.$componentSelector = $theComponentSelector;   
            this.$control = this.$componentSelector.find('.f-share-link');  
            this.showSubFormComponent(); 
        }
          
       
        // event listeners
        FormComponent.prototype.showSubFormComponent = function () {
            var _self = this;
            _self.$control.on('click', function (e) {
                e.preventDefault(); 
                const self = $(this);
                const copyLink = self.data("link");
                copy(copyLink);
                self.addClass("active");
                setTimeout(function() { 
                    self.removeClass('active');
                }, 4000);
            });  
        };
 
        return FormComponent;


    }());

    $(function () {
        var formComponentHolder = '.f-forms';
        $(formComponentHolder).each(function () {
            var formSelectComponent = new FormComponent($(this));
        });
    });

}