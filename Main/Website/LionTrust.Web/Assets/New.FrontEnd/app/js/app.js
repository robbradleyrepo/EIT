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
// import articleFilter from "./componets/articleFilter";
import parallaxScrolling from "./componets/parallaxScrolling";
import stickyNavbar from "./componets/stickyNavbar";
import literatureOverlay from "./componets/literatureOverlay";
import creditRatingChart from "./componets/creditRatingChart";
import capitalisationChart from "./componets/capitalisationChart";
import fundChartDropdown from "./componets/fundChartDropdown";
import shareLink from "./componets/shareLink";
import locationAndMap from "./componets/locationAndMap";
import barStuckedChart from "./componets/barStuckedChart";
import galleryApp from "./componets/galleryApp";
import tabNav from "./componets/tabNav";

document.addEventListener("DOMContentLoaded", () => {
  onboardingOverlay();
  sidebarNav();
  searchOvarlay();
  investmentCard();
  carouselSlider();
  fundsSlider();
  articleSlider();
  awardsSlider();
  stickyNavbar();
  literatureOverlay();
  creditRatingChart();
  capitalisationChart();
  fundChartDropdown();
  shareLink();
  locationAndMap();
  barStuckedChart();
  galleryApp();
  tabNav();
  // if (document.querySelector(".article-page")) articleFilter();
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
