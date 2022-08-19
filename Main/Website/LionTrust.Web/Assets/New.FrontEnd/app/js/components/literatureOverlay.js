export default () => {
    var offsetY = window.pageYOffset,
    $body = $('body'),
    $win = $(window);

    $(document).on("click", ".view-fund-literature", () => {
        $(".lit-overlay__wrapper").addClass("active");
        $(".sidebar-overlay").addClass("sidebar-overlay_active");
        offsetY = window.pageYOffset;
        $body.css({
            'position': 'fixed',
            'top': -offsetY +'px',
            'transition': 'all 0.75s ease-in-out'
        });
    });

    $(document).on("click", "#lit-overlay-close", () => {
        $(".lit-overlay__wrapper").removeClass("active");
        $(".sidebar-overlay").removeClass("sidebar-overlay_active");
         $body.css({
            'position': 'static',
            'transition': 'all 0.75s ease-in-out'
        });
        $win.scrollTop(offsetY);
    });

    $(".sidebar-overlay").on("click", () => {
        $(".lit-overlay__wrapper").removeClass("active");
		$(".sidebar-overlay").removeClass("sidebar-overlay_active");
         $body.css({
            'position': 'static',
            'transition': 'all 0.75s ease-in-out'
        });
        $win.scrollTop(offsetY);
    });

    $(document).on("click", () => {
    $(".lit-overlay__wrapper").removeClass("active");
    $(".sidebar-overlay").removeClass("sidebar-overlay_active");
    $body.css({
      'position': 'static',
      'transition': 'all 0.75s ease-in-out'
    });
    
  });


};

