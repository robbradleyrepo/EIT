export default () => {
    $(document).on("click", "#view-documents", () => {
        $(".lit-overlay__wrapper").addClass("active");
        $(".sidebar-overlay").addClass("sidebar-overlay_active");
    });

    $(document).on("click", "#lit-overlay-close", () => {
        $(".lit-overlay__wrapper").removeClass("active");
        $(".sidebar-overlay").removeClass("sidebar-overlay_active");
    });

    $(".sidebar-overlay").on("click", () => {
        $(".lit-overlay__wrapper").removeClass("active");
    });
};