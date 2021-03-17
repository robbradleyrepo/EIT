import $ from "jquery";
window.jQuery = $;
window.$ = $;

import onboardingOverlay from "./componets/onboarding";
import searchOvarlay from "./componets/searchOvarlay";
import sidebarNav from "./componets/sidebarNav";
import slider from "./componets/slider";


document.addEventListener("DOMContentLoaded", () => {
  onboardingOverlay();
  sidebarNav();
  searchOvarlay();
  slider();
});
