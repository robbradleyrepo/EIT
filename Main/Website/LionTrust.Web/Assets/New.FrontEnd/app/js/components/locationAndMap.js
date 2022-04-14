export default () => {
  $(".select-office__tab").on("click", function () {
    $(".select-office__tab").removeClass("active");
    const tab = $(this);
    tab.addClass("active");
    const activeTab = tab.data("tab");
    $(".location-item.active").removeClass("active");
    $(`#${activeTab}`).addClass("active");
  });
};
