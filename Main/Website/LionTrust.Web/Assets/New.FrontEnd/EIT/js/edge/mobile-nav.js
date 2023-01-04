"use strict";
var $ = require("jquery");

export default () => {

    var MobileNavComponent = /** @class */ (function () {

        function MobileNavComponent($theComponentSelector) {
            this.$componentSelector = $theComponentSelector;
            this.$body = $('body');
            this.$navScreen = $('.u-screen');
            this.$navDesktop = $('.g-nav-primary');
            this.$navMasterControl = $('.mobile-control');
            this.$navClose = $('.mobile-control__close');
            this.$navActualMaster = $('.g-nav-mobile');
            this.$navActual = this.$componentSelector.find('.g-nav-mobile__nav');
            this.$navActualMain = this.$componentSelector.find('.g-nav-mobile');
            this.$navControl = this.$componentSelector.find('.g-nav-mobile a');  
            this.initMobileNavComponent();
        }
        MobileNavComponent.prototype.initMobileNavComponent = function () {
            this.navMobileToggle();
            this.$navActualMaster.delay(500).show(); // delay to ensure no flickering as page loading
        };
        // open mobile nav
        MobileNavComponent.prototype.openMobileNav = function () {
            this.$navDesktop.addClass('hide').attr('aria-hidden', 'true');
            this.$navActualMaster.addClass('active').attr('aria-hidden', 'false');
            this.$navScreen.addClass('active');
            this.$body.addClass('overflow-hidden');
            this.$navClose.addClass('active');
        };
        // close mobile nav
        MobileNavComponent.prototype.closeMobileNav = function () {
            var _self = this;
            this.$navDesktop.removeClass('hide').attr('aria-hidden', 'false');
            this.$navActualMaster.removeClass('active').attr('aria-hidden', 'true');
            this.$navScreen.removeClass('active');
            this.$navClose.removeClass('active');
            setTimeout(function () {
                _self.$body.removeClass('overflow-hidden');
            }, 300);
        };
        // event listeners
        MobileNavComponent.prototype.navMobileToggle = function () {
            var _self = this;
            // on primary nav open mobile
            _self.$navMasterControl.on('click', function (e) {
                e.preventDefault(); 
                _self.openMobileNav();
            });
            // close nav
            _self.$navClose.on('click', function (e) {
                e.preventDefault();
                _self.closeMobileNav();
                // resets
                setTimeout(function () {
                    _self.$navActualMain.css('transform', 'translateX(0%)').css('visibility', 'visible').attr('aria-hidden', 'true');
                }, 500);
            });
        };
        return MobileNavComponent;
    }());

    $(function () {
        var mobileNavComponentHolder = '.g-nav-mobile';
        $(mobileNavComponentHolder).each(function () {
            var mobileNavComponent = new MobileNavComponent($(this));
        });
    });

}