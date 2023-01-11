"use strict"; 
import copy from 'copy-to-clipboard';

export default () => {

    var ShareLinkComponent = /** @class */ (function () {

        function ShareLinkComponent($theComponentSelector) {
            this.$componentSelector = $theComponentSelector;   
            this.$control = this.$componentSelector.find('.f-share-link');  
            this.clickShareLinkComponent(); 
        }
          
       
        // event listeners
        ShareLinkComponent.prototype.clickShareLinkComponent = function () {
            var _self = this;
            _self.$control.on('click', function (e) {
                e.preventDefault(); 
                const self = $(this);
                const copyLink = self.data("link");
                copy(copyLink);
            });  
        };
 
        return ShareLinkComponent;
    }());

    $(function () {
        var shareLinkComponentHolder = '.u-edge';
        $(shareLinkComponentHolder).each(function () {
            var shareLinkComponent = new ShareLinkComponent($(this));
        });
    });

}