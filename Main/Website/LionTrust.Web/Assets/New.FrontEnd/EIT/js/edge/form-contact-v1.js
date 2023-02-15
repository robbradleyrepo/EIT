"use strict"; 
var $ = require("jquery");

export default () => {

    var FormComponent = /** @class */ (function () {

        function FormComponent($theComponentSelector) {
            this.$componentSelector = $theComponentSelector;    
            this.$formButton = this.$componentSelector.find('button'); 
            this.$formActual = this.$componentSelector.find('form'); 
            this.$formNotice = this.$componentSelector.find('.f-forms__notice');
            this.$formSuccess = this.$componentSelector.find('.parsley-success');

            this.formSubmit();
        }
         
        FormComponent.prototype.showNotice = function () {
             
            this.$formNotice.removeClass("hidden");
        };
       
        FormComponent.prototype.formSubmit = function () {
            var _self = this;  
            
            _self.$formButton.on('click', function (e) {
                // e.preventDefault();
                if ($(".parsley-success").length > 0) { 
                    _self.showNotice();
                }
                // return false;
            });
            
        };

 
        return FormComponent;


    }());

    $(function () {
        var formComponentHolder = '.f-forms-parsley';
        $(formComponentHolder).each(function () {
            var formSelectComponent = new FormComponent($(this));
        });
    });

}