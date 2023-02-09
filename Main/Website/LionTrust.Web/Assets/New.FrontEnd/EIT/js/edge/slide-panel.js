"use strict";
var $ = require("jquery");

export default () => {

    var SlidePanelComponent = /** @class */ (function () {

        function SlidePanelComponent($theComponentSelector) {
            this.$componentSelector = $theComponentSelector;
            this.$body = $('body');
            this.$navScreen = $('.u-screen'); 
            this.$navMasterControl = $('.f-slide-panel'); 
            this.$navClose = $('.g-slide-panel__close');   
            this.initSlidePanelComponent();
        }
        SlidePanelComponent.prototype.initSlidePanelComponent = function () {
            this.slidePanelToggle(); 
            this.refsSlidePanel()
        }; 
        SlidePanelComponent.prototype.openSlidePanel = function () { 
            this.$componentSelector.addClass('active').attr('aria-hidden', 'false');
            this.$navScreen.addClass('active');
            this.$body.addClass('overflow-hidden');  
        }; 
        SlidePanelComponent.prototype.closeSlidePanel = function () {
            var _self = this; 

            this.$componentSelector.removeClass('active').attr('aria-hidden', 'true');
            this.$navScreen.removeClass('active');  
            setTimeout(function () {      
                _self.$body.removeClass('overflow-hidden');
            }, 300);
            
        }; 
        SlidePanelComponent.prototype.slidePanelToggle = function () {
            var _self = this; 
            _self.$navMasterControl.on('click', function (e) { 
                e.preventDefault();  
                _self.openSlidePanel(); 
                
            });
            _self.$navClose.on('click', function (e) {
                e.preventDefault(); 
                _self.closeSlidePanel();
            });
            _self.$navScreen.on('click', function (e) {
                e.preventDefault(); 
                _self.closeSlidePanel();
            });
        };
        SlidePanelComponent.prototype.refsSlidePanel = function () {
            var _self = this; 
            
            if (window.location.hash.indexOf('activepanel') != -1) {
                _self.openSlidePanel(); 
            } 
        };
        return SlidePanelComponent;
    }());

    $(function () {
        var slidePanelComponentHolder = '.g-slide-panel';
        $(slidePanelComponentHolder).each(function () {
            var slidePanelComponent = new SlidePanelComponent($(this));
        });
    });

}