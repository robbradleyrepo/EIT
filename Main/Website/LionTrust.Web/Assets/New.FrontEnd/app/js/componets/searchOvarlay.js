export default () => {
  const overlay = $(".search-overlay");
  $(".search-overlay__icon-close").on("click", () => {
    overlay.removeClass("active");
  });

  $(".header-navbar__btn-search, .search-header__search-header-mobile").on("click", () => {
    overlay.addClass("active");
  });
};
