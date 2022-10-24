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
    }); 

    // Check top header exists
    if ($(".header-top").length) {
        $("body").addClass("has-header-top");
    }
    
    //Geographical breakdown chart button
    $(document).ready(function(){
        $(".geographical-breakdown").addClass("hide-rows");
        $(".toggle-geographical-button").click(function(){
          $(".geographical-breakdown").toggleClass("hide-rows");
          $(this).text(function(i, v){
            return v === '+' ? '-' : '+'
         });
        });
      });

    //Sector breakdown chart button
    $(document).ready(function(){
        $(".sector-breakdown").addClass("hide-rows");
        $(".toggle-sector-button").click(function(){
          $(".sector-breakdown").toggleClass("hide-rows");
          $(this).text(function(i, v){
            return v === '+' ? '-' : '+'
         });
        });
      });
    
}); 