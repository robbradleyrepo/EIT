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
  if($('.page-anchor-link').length)
  {
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

    var hashTagOffest,hashTag;
    $('.page-anchor__links a').on('click', function(){
       hashTag= $(this).attr('href');
       hashTagOffest= $(hashTag).offset().top;
       console.log(hashTagOffest);
      
    })
    $(window).on("load", function () {
      // $(".hero-manager-link__link:nth-child(1), .hero-manager-link__link:nth-child(2), .hero-manager-link__link:nth-child(3), .hero-manager-link__link:nth-child(4)").wrapAll('<div/>');
      // $(".hero-manager-link__link:nth-child(5), .hero-manager-link__link:nth-child(6), .hero-manager-link__link:nth-child(7)").wrapAll('<div/>');
      // $(".hero-manager-link__link:nth-child(8), .hero-manager-link__link:nth-child(9)").wrapAll('<div/>');

      $("#sticky-navbar a").mPageScroll2id({
        highlightSelector: "#sticky-navbar a",
        liveSelector: "#sticky-navbar a",
        offset: hashTagOffest,
        keepHighlightUntilNext: true
      });
    });
  })(jQuery);
};