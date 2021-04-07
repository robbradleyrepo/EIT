import Swiper from "swiper/bundle";

// core version + navigation, pagination modules:
import SwiperCore, { Navigation, Pagination } from "swiper/core";

// configure Swiper to use modules
SwiperCore.use([Navigation, Pagination]);

export default () => {
  const swiper = new Swiper(".swiper-container-carousel", {
    effect: "coverflow",
    grabCursor: true,
    centeredSlides: true,
    breakpointsInverse: true,
    slidesPerView: "auto",
    spaceBetween: 5,
    coverflowEffect: {
      rotate: 0,
      stretch: 0,
      depth: 300,
      modifier: 2,
      slideShadows: true,
    },

    loop: true,
    pagination: {
      el: ".swiper-pagination",
      clickable: true,
    },
    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },
  });
};
