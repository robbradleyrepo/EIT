export default () => {
  $("#view-documents").on("click", () => {
    $(".lit-overlay").addClass("active");
    $(".sidebar-overlay").addClass("sidebar-overlay_active");
  });

  $("#lit-overlay-close").on("click", () => {
    $(".lit-overlay").removeClass("active");
    $(".sidebar-overlay").removeClass("sidebar-overlay_active");
  });

  $(".sidebar-overlay").on("click", () => {
    $(".lit-overlay").removeClass("active");
  });
};
