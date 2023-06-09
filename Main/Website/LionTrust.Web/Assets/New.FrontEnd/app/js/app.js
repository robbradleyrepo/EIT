import "core-js/es/number";
import "core-js/features/string/repeat";
import $ from "jquery";
window.jQuery = $;
window.$ = $;

import onboardingOverlay from "./components/onboarding";
import searchOvarlay from "./components/searchOvarlay";
import sidebarNav from "./components/sidebarNav";
import carouselSlider from "./components/carouselSlider";
import fundsSlider from "./components/fundsSlider";
import articleSlider from "./components/articleSlider";
import awardsSlider from "./components/awardsSlider";
import investmentCard from "./components/investmentCard";
import parallaxScrolling from "./components/parallaxScrolling";
import stickyNavbar from "./components/stickyNavbar";
import literatureOverlay from "./components/literatureOverlay";
import shareLink from "./components/shareLink";
import locationAndMap from "./components/locationAndMap";
import tabNav from "./components/tabNav";
import headerCtaNav from "./components/headerCtaNav";
import preferenceCenter from './components/preferenceCenter';
import common from "./components/common";
import peopleNav from "./components/peopleNav";
import modals from "./components/bs-modals";
import geographicalChart from "./components/geographicalChart";
import insightAuthor from "./components/insightAuthor";

document.addEventListener("DOMContentLoaded", () => {
    onboardingOverlay();
    sidebarNav();
    searchOvarlay();
    investmentCard();
    insightAuthor();
    carouselSlider();
    fundsSlider();
    geographicalChart();
    articleSlider();
    awardsSlider();
    literatureOverlay();
    locationAndMap();
    shareLink();
    tabNav();
    headerCtaNav();
    common();
    preferenceCenter();
    peopleNav();
    modals();
    if (document.querySelector(".page-anchor-link")) stickyNavbar();
    if (document.querySelector(".main-page")) parallaxScrolling();
    $(".navigation__checkbox").prop("checked", false); //Uncheck mobile menu navigation on load
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
    $(".imagepromo__contentbox p").each(function(){
        if ($(this).text().trim().length) {
            $(this).closest('.fundscroller__box').addClass("has-content");
        }
        // if empty
        if ($(this).trim().length > 0 ) {
            (this).addClass('mb-0');
        }
        // if any child is empty 
        if ($(this).children().trim().length > 0 ) {
            (this).parentElement('p').addClass('mb-0');
        }
    }); 

    // Check top header exists
    if ($(".header-top").length) {
        $("body").addClass("has-header-top");
    }
    
    
}); 