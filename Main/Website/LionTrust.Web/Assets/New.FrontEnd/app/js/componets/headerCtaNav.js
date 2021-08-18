import Swiper from "swiper/bundle";

export default () => {



  new Swiper(".header-btn-nav", {
    slidesPerView: "auto",
    spaceBetween: 32,
    breakpointsInverse: true,
    breakpoints: {
      0: {
        spaceBetween: 16,
      },
      992: {
        spaceBetween: 32,
      },
    },
  });
};
