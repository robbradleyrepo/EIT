import Swiper from "swiper/bundle";

// core version + navigation, pagination modules:
import SwiperCore, { Navigation, Pagination } from "swiper/core";

// configure Swiper to use modules
SwiperCore.use([Navigation, Pagination]);

export default () => {
  const swiper = new Swiper(".swiper-container-carousel", {
    watchSlidesVisibility: true,
    centeredSlides: true,
    breakpointsInverse: true,
    longSwipes: false,
    slideToClickedSlide: true,
    setWrapperSize: false,
    spaceBetween: 5,
    loop: true,
    breakpoints: {
      0: {
        slidesPerView: 1,
      },
      992: {
        slidesPerView: 3,
      },
    },
    pagination: {
      el: ".swiper-pagination",
      clickable: true,
    },
  });
};
