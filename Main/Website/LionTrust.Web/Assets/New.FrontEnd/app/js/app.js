import "core-js/es/number";
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
import articleFilder from "./componets/articleFilder";

document.addEventListener("DOMContentLoaded", () => {
  onboardingOverlay();
  sidebarNav();
  searchOvarlay();
  investmentCard();
  carouselSlider();
  fundsSlider();
  articleSlider();
  awardsSlider();
  if (document.querySelector(".article-page")) articleFilder();
});
