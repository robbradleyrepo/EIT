export default () => {    
    var TabNavigationComponent = /** @class */ (function () {
      
      function TabNavigationComponent($theComponentSelector) {
          this.$componentSelector = $theComponentSelector;
          this.$select = this.$componentSelector.find(".document-select-filter");

          if(this.$select.length > 0) {
            this.changeNavigation()
          } else {
            this.clickNavigation();
          }
      }

      TabNavigationComponent.prototype.clickNavigation = function () {
        var _self = this;

        _self.$componentSelector.on("click", function (e) {
          const el = $(this);
          const id = el.attr("data-tab-target");
          const parentEl = el.parents('[data-select-for]').attr('data-select-for');
          $(`#${parentEl} [data-tab-target]`).removeClass("active");
          el.addClass("active");
          _self.changeTab(id, parentEl); 
          $(`#${parentEl} .document-select-filter`).val(id);
        });

      };

      TabNavigationComponent.prototype.changeNavigation = function () {
        var _self = this;

        _self.$select.on("change", function (e) {
          const parent = $(this).attr("data-select-for");
          const id = e.target.value;

          _self.changeTab(id, parent);
          $(`#${parent} [data-tab-target]`).removeClass("active");
          $(`#${parent} [data-tab-target="${id}"]`).addClass("active");
        });

      };

      TabNavigationComponent.prototype.changeTab = function (id, parent) {
        const tab = $(`#${id}`);
        if (tab.length) {
          $(`#${parent} [role="tabelement"]`).removeClass("active");
          tab.addClass("active");
        } else {
          console.error("Tab not found");
        }
      };

      return TabNavigationComponent;
    })();

  $(function () {
      var selectNavigationComponent = new TabNavigationComponent($(".select-mobile"));
      $('[data-tab-target]').each(function () {
          var tabNavigationComponent = new TabNavigationComponent($(this));
      });
  });
};
  