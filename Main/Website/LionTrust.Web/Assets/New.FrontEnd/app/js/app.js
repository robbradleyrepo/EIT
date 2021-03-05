import $ from "jquery";
window.jQuery = $;
window.$ = $;

import onboardingOverlay from "./componets/onboarding";
import sidebarNav from "./componets/sidebarNav";


document.addEventListener("DOMContentLoaded", () => {
    onboardingOverlay();
    sidebarNav();
  });

