"use strict"; 
var $ = require("jquery");

export default () => {

    var FormComponent = /** @class */ (function () {

        function FormComponent($theComponentSelector) {
            this.$componentSelector = $theComponentSelector;     
            this.$formActual = this.$componentSelector.find('form');  

            this.formSubmit();
        }
          
        FormComponent.prototype.formSubmit = function () {
            var _self = this;  
            
            _self.$formActual.on('submit', function (e) {
                e.preventDefault();
                grecaptcha.ready(function() {
                    grecaptcha.execute('6Le7-6LfFyTAkAAAAAOARzlVsc5t3Omn324WCwt68vsLz', {action: 'create_comment'}).then(function(token) {
                         _self.$formActual.prepend('<input type="hidden" name="token" value="' + token + '">');
                         _self.$formActual.prepend('<input type="hidden" name="action" value="create_comment">');
                        // submit form now
                         _self.$formActual.on('submit');
                    });;
                });
            });
             
        };

 
        return FormComponent;


    }());

    $(function () {
        var formComponentHolder = '.f-forms--sitecore';
        $(formComponentHolder).each(function () {
            var formSelectComponent = new FormComponent($(this));
        });
    });

}