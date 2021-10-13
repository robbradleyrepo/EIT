import listFilter from "./componets/listFilter";
document.addEventListener("DOMContentLoaded", () => {
    if ($('#lister-app').length > 0)
        listFilter();
    var anchorLinks = $('.page-anchor-link .page-anchor .page-anchor__right:not(.page-anchor__right-mobile) .page-anchor__links .page-anchor__link');
    var anchorLength = anchorLinks.length;
    console.log(anchorLength);
    if (anchorLength > 4) {
        $(anchorLinks).parent('.page-anchor__links').addClass('d-flex justify-content-between');
    } else {
        $(anchorLinks).parent('.page-anchor__links').addClass('d-flex justify-content-start m-link');
    }
});