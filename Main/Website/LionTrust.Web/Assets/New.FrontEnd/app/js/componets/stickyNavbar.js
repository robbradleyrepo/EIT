export default () => {
  document.addEventListener("scroll", stickyNav);

  const navbar = document.querySelector("#sticky-navbar");
  const sticky = navbar.offsetTop;
  const offset = navbar.offsetHeight;

  console.log('offset', offset);

  function stickyNav() {
    if (window.pageYOffset >= sticky) {
      navbar.classList.add("sticky");
    } else {
      navbar.classList.remove("sticky");
    }
  }

  const section = document.querySelectorAll(".section-spy");
  const sections = {};
  let i = 0;

  Array.prototype.forEach.call(section, function (e) {
    sections[e.id] = e.offsetTop - offset - 10
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

  $("a.page-anchor__link").on('click', function(event) {

    // Make sure this.hash has a value before overriding default behavior
    if (this.hash !== "") {
      // Prevent default anchor click behavior
      event.preventDefault();

      // Store hash
      var hash = this.hash;

      // Using jQuery's animate() method to add smooth page scroll
      // The optional number (800) specifies the number of milliseconds it takes to scroll to the specified area
      $('html, body').animate({
        scrollTop: $(hash).offset().top
      }, 800, function(){

        // Add hash (#) to URL when done scrolling (default click behavior)
        window.location.hash = hash;
      });
    } // End if
  });

};
