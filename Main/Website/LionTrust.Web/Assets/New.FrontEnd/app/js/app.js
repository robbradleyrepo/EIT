import $ from "jquery";
window.jQuery = $;
window.$ = $;

import onboardingOverlay from "./componets/onboarding";
import searchOvarlay from "./componets/searchOvarlay";
import sidebarNav from "./componets/sidebarNav";
import carouselSlider from "./componets/carouselSlider";
import fundsSlider from "./componets/fundsSlider";

import investmentCard from "./componets/investmentCard";

document.addEventListener("DOMContentLoaded", () => {
  onboardingOverlay();
  sidebarNav();
  searchOvarlay();
  carouselSlider();
  fundsSlider();
  investmentCard();
});
