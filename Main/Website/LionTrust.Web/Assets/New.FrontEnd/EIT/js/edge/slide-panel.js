"use strict";
var $ = require("jquery");

export default () => {

    var SlidePanelComponent = /** @class */ (function () {

        function SlidePanelComponent($theComponentSelector) {
            this.$componentSelector = $theComponentSelector;
            this.$body = $('body');
            this.$navScreen = $('.u-screen'); 
            this.$navMasterControl = this.$body.find('.f-slide-panel');
            this.$navClose = $('.g-slide-panel__close');   
            this.initSlidePanelComponent();
        }
        SlidePanelComponent.prototype.initSlidePanelComponent = function () {
            this.slidePanelToggle(); 
        }; 
        SlidePanelComponent.prototype.openSlidePanel = function () {
            this.$componentSelector.addClass('active').attr('aria-hidden', 'false');
            this.$navScreen.addClass('active');
            this.$body.addClass('overflow-hidden');
            this.$navClose.addClass('active');
        }; 
        SlidePanelComponent.prototype.closeSlidePanel = function () {
            var _self = this; 
            this.$componentSelector.removeClass('active').attr('aria-hidden', 'true');
            this.$navScreen.removeClass('active');
            this.$navClose.removeClass('active');
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
            // close nav
            _self.$navClose.on('click', function (e) {
                e.preventDefault();
                _self.closeSlidePanel();
                
            });
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