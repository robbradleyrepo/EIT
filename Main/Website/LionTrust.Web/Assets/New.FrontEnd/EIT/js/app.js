import "core-js/es/number";
import "core-js/features/string/repeat";
import $ from "jquery";
window.jQuery = $;
window.$ = $; 

// import onboardingOverlay from "./components/onboarding";
// import searchOvarlay from "./components/searchOvarlay";
// import sidebarNav from "./components/sidebarNav";
// import carouselSlider from "./components/carouselSlider";
// import fundsSlider from "./components/fundsSlider";
// import articleSlider from "./components/articleSlider";
// import awardsSlider from "./components/awardsSlider";
// import investmentCard from "./components/investmentCard"; 
// import stickyNavbar from "./components/stickyNavbar";
// import literatureOverlay from "./components/literatureOverlay";
// import shareLink from "./components/shareLink";
// import locationAndMap from "./components/locationAndMap";
// import tabNav from "./components/tabNav";
// import headerCtaNav from "./components/headerCtaNav";
// import preferenceCenter from './components/preferenceCenter';
// import common from "./components/common";
// import peopleNav from "./components/peopleNav";
// import modals from "./components/bs-modals";
// import geographicalChart from "./components/geographicalChart";
// import insightAuthor from "./components/insightAuthor";

import mobileNav from "./edge/mobile-nav";

document.addEventListener("DOMContentLoaded", () => {
    // onboardingOverlay();
    // sidebarNav();
    // searchOvarlay();
    // investmentCard();
    // insightAuthor();
    // carouselSlider();
    // fundsSlider();
    // geographicalChart();
    // articleSlider();
    // awardsSlider();
    // literatureOverlay();
    // locationAndMap();
    // shareLink();
    // tabNav();
    // headerCtaNav();
    // common();
    // preferenceCenter();
    // peopleNav();
    // modals();
    mobileNav();
   
}); 