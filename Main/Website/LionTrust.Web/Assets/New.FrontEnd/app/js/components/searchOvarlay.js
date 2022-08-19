export default () => {
  const overlay = $(".search-overlay");
  $(".search-overlay__icon-close").on("click", () => {
    overlay.removeClass("active");
  });

  $(".header-navbar__btn-search, .search-header__search-header-mobile, .sidebar-actions__link--open-search").on("click", () => {
    console.log("open-search")
    overlay.addClass("active");
  });
};
