import "core-js/es/number";
import "core-js/features/string/repeat";
import $ from "jquery";
window.jQuery = $;
window.$ = $;

import onboardingOverlay from "./componets/onboarding";
import searchOvarlay from "./componets/searchOvarlay";
import sidebarNav from "./componets/sidebarNav";
import carouselSlider from "./componets/carouselSlider";
import fundsSlider from "./componets/fundsSlider";
import articleSlider from "./componets/articleSlider";
import awardsSlider from "./componets/awardsSlider";
import investmentCard from "./componets/investmentCard";
import parallaxScrolling from "./componets/parallaxScrolling";
import stickyNavbar from "./componets/stickyNavbar";
import literatureOverlay from "./componets/literatureOverlay";
import shareLink from "./componets/shareLink";
import locationAndMap from "./componets/locationAndMap";
import tabNav from "./componets/tabNav";
import headerCtaNav from "./componets/headerCtaNav";
import preferenceCenter from './componets/preferenceCenter';
import common from "./componets/common";

document.addEventListener("DOMContentLoaded", () => {
    onboardingOverlay();
    sidebarNav();
    searchOvarlay();
    investmentCard();
    carouselSlider();
    fundsSlider();
    articleSlider();
    awardsSlider();
    literatureOverlay();
    locationAndMap();
    shareLink();
    tabNav();
    headerCtaNav();
    common();
    preferenceCenter();
    if (document.querySelector(".page-anchor-link")) stickyNavbar();
    if (document.querySelector(".main-page")) parallaxScrolling();
    // init tooltips
    $('[data-toggle="tooltip"]').tooltip({
        offset: 5,
    });

    // set z-index for 
    $('.nav-desktop__item').on('mouseenter', () => {
        $('.header').css('z-index', '3')
    })
    $('.nav-desktop__item').on('mouseleave', () => {
        $('.header').css('z-index', '2')
    })

});