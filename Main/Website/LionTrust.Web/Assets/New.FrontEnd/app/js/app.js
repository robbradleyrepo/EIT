import $ from "jquery";
window.jQuery = $;
window.$ = $;

document.addEventListener("DOMContentLoaded", () => {
  // Toogle navigation menu

  const showMenu = () => {
    $(".sidebar").addClass("sidebar_active");
    $(".sidebar-overlay").addClass("sidebar-overlay_active");
  };
  const hideMenu = () => {
    $(".sidebar").removeClass("sidebar_active");
    $(".sidebar-overlay").removeClass("sidebar-overlay_active");
  };

  $(".header-navbar__btn-open").on("click", showMenu);
  $(".sidebar-overlay").on("click", hideMenu);
  $(".sidebar__close-btn").on("click", hideMenu);
});
