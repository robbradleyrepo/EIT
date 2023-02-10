"use strict"; 
import copy from 'copy-to-clipboard';

export default () => {

    var ShareLinkComponent = /** @class */ (function () {

        function ShareLinkComponent($theComponentSelector) {
            this.$control = $theComponentSelector;
            this.clickShareLinkComponent(); 
        }
           
        ShareLinkComponent.prototype.clickShareLinkComponent = function () {
            var _self = this;
            _self.$control.on('click', function (e) {
                e.preventDefault(); 
                const self = $(this);
                const copyLink = self.data("link");
                copy(copyLink);
                self.addClass("triggered");
                setTimeout(function() { 
                    self.removeClass('triggered');
                }, 4000);
            });  
        };
 
        return ShareLinkComponent;
    }());

    $(function () {
        var shareLinkComponentHolder = '.f-share-link';
        $(shareLinkComponentHolder).each(function () {
            var shareLinkComponent = new ShareLinkComponent($(this));
        });
    });

}