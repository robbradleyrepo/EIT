import $ from "jquery";
window.jQuery = $;
window.$ = $;

import onboardingOverlay from "./componets/onboarding";
import searchOvarlay from "./componets/searchOvarlay";
import searchPage from "./componets/searchPage";
import sidebarNav from "./componets/sidebarNav";

document.addEventListener("DOMContentLoaded", () => {
  onboardingOverlay();
  sidebarNav();
  searchOvarlay();
  searchPage();
});
