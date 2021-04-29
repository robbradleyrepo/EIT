import { gsap } from "gsap";
import { ScrollTrigger } from "gsap/ScrollTrigger";

export default () => {
  // Check if EI
  if (window.document.documentMode) {
    // Do IE stuff
    window.requestAnimationFrame = window.requestAnimationFrame.bind(window);
  }

  gsap.registerPlugin(ScrollTrigger);
  gsap.to(".paralax-circle__item", {
    scrollTrigger: {
      trigger: ".paralax-circle__item",
      start: "top center",
      // markers: true,
      scrub: true,
    },
    y: 100,
  });

  gsap.to(".shevron-bg", {
    scrollTrigger: {
      trigger: ".shevron-bg",
      start: "top center",
      // markers: true,
      scrub: true,
    },
    y: 600,
  });
};
