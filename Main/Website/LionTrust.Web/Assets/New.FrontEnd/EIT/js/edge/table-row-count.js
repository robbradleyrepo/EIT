export default () => {
    var TableRowCountComponent = /** @class */ (function () {
      
      function TableRowCountComponent($theComponentSelector) {
          this.$componentSelector = $theComponentSelector;
          this.$control = this.$componentSelector.find(".f-toggle-row");
          this.clickToggleButton();
      }

      TableRowCountComponent.prototype.clickToggleButton = function () {
        var _self = this;
        let tableRowCount = $(this.$componentSelector).find('tbody tr').length;

        if (tableRowCount <= 10) {
            _self.$control.hide();
        } 

        _self.$control.on("click", function (e) {
            e.preventDefault();
            $(this).prevAll('table').toggleClass("hide-rows");
            $(this).text(function (i, v) {
                return v === "+" ? "-" : "+";
            });
        });
      };
      return TableRowCountComponent;
    })();

  $(function () {
      var tableRowCountComponentHolder = ".g-table-row-count";
      $(tableRowCountComponentHolder).each(function () {
          var tableRowCountComponent = new TableRowCountComponent($(this));
      });
  });
};
