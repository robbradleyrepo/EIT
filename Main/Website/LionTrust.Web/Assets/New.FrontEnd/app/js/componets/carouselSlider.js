import Swiper from "swiper/bundle";

// core version + navigation, pagination modules:
import SwiperCore, { Navigation, Pagination } from "swiper/core";

// configure Swiper to use modules
SwiperCore.use([Navigation, Pagination]);

export default () => {
  const swiper = new Swiper(".swiper-container-carousel", {
    // effect: "coverflow",
    // grabCursor: true,
    slidesPerView: 3,
    watchSlidesVisibility: true,
    centeredSlides: true,
    breakpointsInverse: true,
    longSwipes: false,
    slideToClickedSlide: true,
    setWrapperSize: false,
    spaceBetween: 5,
    coverflowEffect: {
      rotate: 0,
      stretch: 225,
      depth: 200,
      modifier: 1.8,
      slideShadows: true,
    },
    breakpoints: {
      576: {},
      768: {
        coverflowEffect: {
          rotate: 0,
          stretch: 100,
          depth: 600,
          modifier: 1.1,
          slideShadows: true,
        },
      },
      992: {
        coverflowEffect: {
          rotate: 0,
          stretch: 225,
          depth: 500,
          modifier: 1.2,
          slideShadows: true,
        },
      },
      1200: {
        coverflowEffect: {
          modifier: 1.8,
        },
      },
      1800: {
        coverflowEffect: {
          modifier: 1.8,
        },
      },
      2560: {
        stretch: 225,
        depth: 300,
        modifier: 1.5,
      },
    },
    loop: true,
    pagination: {
      el: ".swiper-pagination",
      clickable: true,
    },
  });
};
