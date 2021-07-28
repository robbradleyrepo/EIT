export default () => {    

  // click on tab
  $("[data-tab-target]").on("click", function () {
    const el = $(this);
    const id = el.attr("data-tab-target");
    const parentEl = el.parents('[data-select-for]').attr('data-select-for');
    $(`#${parentEl} [data-tab-target]`).removeClass("active");
    el.addClass("active");
    changeTab(id, parentEl); 
    $(`#${parentEl} .document-select-filter`).val(id);
  });

  // change select
  $(".document-select-filter").on("change", function (e) {
    const parent = $(this).attr("data-select-for");
    const id = e.target.value;
    changeTab(id);
    $(`#${parent} [data-tab-target]`).removeClass("active");
    $(`#${parent} [data-tab-target="${id}"]`).addClass("active");
  });

  function changeTab(id, parent) {
    const tab = $(`#${id}`);
    if (tab.length) {
      $(`#${parent} [role="tabelement"]`).removeClass("active");
      tab.addClass("active");
    } else {
      console.error("Tab not found");
    }
  }
};
