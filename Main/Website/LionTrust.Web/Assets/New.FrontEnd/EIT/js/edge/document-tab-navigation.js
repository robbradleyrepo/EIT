export default () => {    
    var DocumentTabNavigationComponent = /** @class */ (function () {
      
      function DocumentTabNavigationComponent($theComponentSelector) {
          this.$componentSelector = $theComponentSelector;
          this.$select = this.$componentSelector.find(".document-select-filter");
          this.$tabNavs = this.$componentSelector.find("[data-tab-target]");
          this.$tabNavContent = this.$componentSelector.find('.tab-nav-content');

          var _self = this;
          this.changeNavigation();

          this.$tabNavs.each(function () {
            _self.clickNavigation(this);
          });
      }

      DocumentTabNavigationComponent.prototype.clickNavigation = function (tabNav) {
        var _self = this;

        $(tabNav).on("click", function (e) {
          const tab = $(this);
          const id = tab.attr("data-tab-target");
          _self.$tabNavs.removeClass("active");
          tab.addClass("active");

          _self.changeTab(id); 
          _self.$select.val(id);
        });

      };

      DocumentTabNavigationComponent.prototype.changeNavigation = function () { 
        var _self = this;

        _self.$select.on("change", function (e) {
          const id = e.target.value;

          _self.changeTab(id);
          _self.$tabNavs.removeClass("active");
          _self.$tabNavs.parents().find(`[data-tab-target="${id}"]`).addClass('active');
        });

      };

      DocumentTabNavigationComponent.prototype.changeTab = function (id) {
        const tab = this.$tabNavContent.find(`#${id}`);
        if (tab.length) {
          this.$tabNavContent.find('.doc-list').removeClass("active");
          tab.addClass("active");
        } else {
          console.error("Tab not found");
        }
      };

      return DocumentTabNavigationComponent;
    })();

  $(function () {
      var documentTabNavigationComponent = new DocumentTabNavigationComponent($(".f-tab-content"));
  });
};
  