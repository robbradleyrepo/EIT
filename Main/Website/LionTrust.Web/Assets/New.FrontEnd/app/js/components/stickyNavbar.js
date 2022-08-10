export default () => {
  document.addEventListener("scroll", stickyNav);
  const navbar = $(".scroll-nav");
  const sticky = navbar.offset()?.top;
  var sectionToScroll = $(document).find('.section-spy');
  var sectionHash = $(document).find('.page-anchor__right .page-anchor__link');
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
    }
    else {
      $(".page-anchor-link").removeClass("sticky");
    }
  }
  const openBtn = $("#open-page-anchor");
  openBtn.on("click", (e) => {
    e.stopPropagation();
    $(".page-anchor__links-mobile").stop().slideToggle();
    openBtn.toggleClass("active");
  });
  document.addEventListener("click", () => {
    $(".page-anchor__links-mobile").stop().slideUp();
    openBtn.removeClass("active");
  });
  (function ($) {
    $(window).on("load", function () {
      let offsetVal;
      const windowWidthCheck = 991,
            mobStickyNavSize = 70,
            desktopStickyNavSize = 70

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