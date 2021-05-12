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
import articleFilter from "./componets/articleFilter";
import parallaxScrolling from "./componets/parallaxScrolling";
import stickyNavbar from "./componets/stickyNavbar";
import literatureOverlay from "./componets/literatureOverlay";

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
  if (document.querySelector(".article-page")) articleFilter();
  if (document.querySelector(".main-page")) parallaxScrolling();

  $('[data-toggle="tooltip"]').tooltip({
    offset: 5,
  });
});
