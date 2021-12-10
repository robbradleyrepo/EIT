export default () => {
  document.addEventListener("scroll", stickyNav);

  const navbar = $("#sticky-navbar");
  const sticky = navbar.offset()?.top;
  const offset = navbar.outerHeight();

  function stickyNav() {
    if (window.pageYOffset >= sticky) {
      $("#sticky-navbar").addClass("sticky");
    } else {
      $("#sticky-navbar").removeClass("sticky");
    }
  }

  const section = document.querySelectorAll(".section-spy");
  const sections = {};
  let i = 0;

  Array.prototype.forEach.call(section, function (e) {    
	sections[e.id] = $("#sticky-navbar").hasClass('sticky') ? e.offsetTop - offset - 10 : e.offsetTop - offset * 2 - 10;
  });

  window.onscroll = function () {
    var scrollPosition =
      document.documentElement.scrollTop || document.body.scrollTop;
    for (i in sections) {
      if (sections[i] <= scrollPosition) {
        $(".active-link").removeClass("active-link");
        $("a[href*=" + i + "]").addClass("active-link");
      }
      if (scrollPosition < sections[section[0].id]) {
        $(".active-link").removeClass("active-link");
      }
    }
  };

  $("a.page-anchor__link").on("click", function (event) {
    // Make sure this.hash has a value before overriding default behavior
    if (this.hash !== "") {
      // Prevent default anchor click behavior
      event.preventDefault();
      // Store hash
      var hash = this.hash;
      // Using jQuery's animate() method to add smooth page scroll
      // The optional number (800) specifies the number of milliseconds it takes to scroll to the specified area
      // Calculate offsetTop
      const scrollTop = $("#sticky-navbar").hasClass('sticky') ? $(hash).offset().top - offset : $(hash).offset().top - offset * 2;
      $("html, body").stop().animate(
        { scrollTop },
        800,
        function () {
          // Add hash (#) to URL when done scrolling (default click behavior)
          history.pushState({}, '', hash);
        }
      );
    } // End if
  });

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

};
