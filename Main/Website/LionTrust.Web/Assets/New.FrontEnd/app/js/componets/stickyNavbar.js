export default () => {
  document.addEventListener("scroll", stickyNav);
  const navbar = $(".scroll-nav");
  const sticky = navbar.offset()?.top;
  var sectionToScroll = $(document).find('.section-spy');
  var sectionHash = $(document).find('.page-anchor__right .page-anchor__link');
  sectionHash.each(function (index) {
    var hashText = $(sectionHash[index]).text();
    $(sectionToScroll[index]).text(hashText);
  });

  sectionToScroll.each(function (index) {
    var nextOfHash = $(sectionToScroll[index] ).next()
    $(sectionToScroll[index] ).prependTo(nextOfHash);
    $(sectionToScroll).parent().css('position','relative')
  });

  const content = document.querySelector('.main-content');
  scrollnav.init(content, {
    sections: '.section-spy',
  });
  $('.scroll-nav').prepend('<span class="scroll-nav__hidden">On this page</spna>');
  $('.scroll-nav').children().wrapAll('<div class="container d-flex"></div>');
  const stickyPostion = $('.scroll-nav').offset().top;
  function stickyNav() {
    if (window.pageYOffset >= stickyPostion) {
      $(".scroll-nav").addClass("sticky");
    }
    else {
      $(".scroll-nav").removeClass("sticky");
    }
  }
  const openBtn = $(".scroll-nav__hidden");
  openBtn.on("click", (e) => {
    e.stopPropagation();
    $(".scroll-nav__list").stop().slideToggle();
    openBtn.toggleClass("active");
  });
  document.addEventListener("click", () => {
    $(".scroll-nav__list").stop().slideUp();
    openBtn.removeClass("active");
  });
};