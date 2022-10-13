export default () => {
  document.addEventListener("scroll", stickyNav);
  const navbar = $(".scroll-nav");
  const sticky = navbar.offset()?.top;
  var sectionToScroll = $(document).find('.section-spy');
  var sectionHash = $(document).find('.page-anchor__right .page-anchor__link');
  let paddingsize;
  const StickyNavSize = $("#sticky-navbar").outerHeight();

  paddingsize = StickyNavSize;

  sectionToScroll.each(function (index) {
    var addID = $(sectionToScroll[index]).attr('id');
    $(sectionToScroll[index]).nextUntil('.section-spy').wrapAll('<div class="hash-section"/>')
    $(this).removeAttr('id');
    var nextOfHash = $(sectionToScroll[index]).next()
    $(nextOfHash).attr('id', addID);
  });
  if ($('.page-anchor-link').length) {
    var stickyPostion = $('.page-anchor-link').offset().top;
  }
  function stickyNav() {
    if (window.pageYOffset >= stickyPostion) {
      $(".page-anchor-link").addClass("sticky");
      $(".sticky-navbar-wrapper").css("padding-top", paddingsize);
    }
    else {
      $(".page-anchor-link").removeClass("sticky");
      $(".sticky-navbar-wrapper").removeClass("add-paddings");
      $(".sticky-navbar-wrapper").css("padding-top", "0");
    }
    $(".page-anchor__links-mobile").slideUp();
    $("#open-page-anchor").removeClass("active");
  }
  const openBtn = $("#open-page-anchor");
  openBtn.on("click", (e) => {
    e.stopPropagation();
    $(".page-anchor__links-mobile").slideToggle();
    openBtn.toggleClass("active");
  });
  document.addEventListener("click", () => {
    $(".page-anchor__links-mobile").slideUp();
    openBtn.removeClass("active");
  });
  (function ($) {
    $(window).on("load", function () {
      let offsetVal;
      const windowWidthCheck = 991,
            mobStickyNavSize = 70,
            desktopStickyNavSize = 84;

      if (window.innerWidth < windowWidthCheck) {
        offsetVal = mobStickyNavSize
      } else {
        offsetVal = desktopStickyNavSize
      }

      $("#sticky-navbar a").mPageScroll2id({
        highlightSelector: "#sticky-navbar a",
        liveSelector: "#sticky-navbar a",
        offset: offsetVal,
        keepHighlightUntilNext: true
      });  
    });
  })(jQuery);
};