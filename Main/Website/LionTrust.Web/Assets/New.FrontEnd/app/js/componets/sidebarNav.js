export default () => {
    // Toogle navigation menu
    const navLinks = $(".navigation__link, .sidebar-actions__link");
    const checkboxes = $(".navigation__checkbox");
    const sidebar = $(".sidebar");

    const showMenu = () => {
      sidebar.addClass("sidebar_active");
      $(".sidebar-overlay").addClass("sidebar-overlay_active");
    };

    const hideMenu = () => {
      sidebar.removeClass("sidebar_active");
      $(".sidebar-overlay").removeClass("sidebar-overlay_active");
      checkboxes.prop("checked", false);
      navLinks.removeClass("active");
      $(".navigation__submenu").removeClass(
        "navigation__submenu_stop-animation"
      );
    };

    // togle sidebar menu
    $(".header-navbar__btn-open").on("click", showMenu);
    $(".sidebar-overlay").on("click", hideMenu);
    $(".sidebar__close-btn").on("click", hideMenu);

    // add event to navlinks
    navLinks.on("click", function () {
      // remove transition if checkbox is cheched
      if ($(".navigation__checkbox:checked").length > 0)
        $(".navigation__submenu").addClass(
          "navigation__submenu_stop-animation"
        );

      checkboxes.prop("checked", false);

      // add transotion for clicked element
      navLinks.removeClass("active");
      $(this).toggleClass("active");
      // toogle border from sidebar
      if (navLinks.hasClass("active")) sidebar.removeClass("sidebar_border");
      else sidebar.addClass("sidebar_border");
    });

    // set default state when close submenu
    $(".navigation__close-submenu").on("click", () => {
      $(".navigation__submenu").removeClass(
        "navigation__submenu_stop-animation"
      );
      navLinks.removeClass("active");
      sidebar.addClass("sidebar_border");
    });
};
