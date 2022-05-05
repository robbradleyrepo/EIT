import listFilter from "./components/listFilter";
document.addEventListener("DOMContentLoaded", () => {
    if ($('#lister-app').length > 0)
        listFilter();
    var anchorLinks = $('.page-anchor-link .page-anchor .page-anchor__right:not(.page-anchor__right-mobile) .page-anchor__links .page-anchor__link');
    var anchorLength = anchorLinks.length;
    console.log(anchorLength);
    if (anchorLength > 4) {
        $(anchorLinks).parent('.page-anchor__links').addClass('justify-content-between');
    } else {
        $(anchorLinks).parent('.page-anchor__links').addClass('justify-content-start m-link');
    }
    window.onload = function () {
        var imageSrcBg = document.getElementsByClassName('video-box__link');
        var imageSrc;
        for (var i = 0; i < imageSrcBg.length; i++) {
            imageSrc = imageSrcBg[i].style.backgroundImage.replace(/url\((['"])?(.*?)\1\)/gi, '$2').split(',')[0];
            var bgParents = imageSrcBg[i].closest('.accordion__smartcard');
            console.log(imageSrc);
            console.log(bgParents);
            var image = new Image();
            image.src = imageSrc;
        }
        if ($('.onboarding-overlay').hasClass('active') === true) {
            $('html').addClass('overflow-hidden');
        }
        else
        {
            $('html').removeClass('overflow-hidden');
        }
    };
});