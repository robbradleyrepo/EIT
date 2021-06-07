import Swiper from "swiper/bundle";

export default () => {
  const swiper = new Swiper(".swiper-container-article", {
    // slidesPerView: 1.2,
    spaceBetween: 32,
    grabCursor: true,
    breakpointsInverse: true,
    breakpoints: {
      576: {
        slidesPerView: 1.5,
      },
      768: {
        slidesPerView: 2.2,
      },
      992: {
        slidesPerView: 2.7,
      },
      1200: {
        slidesPerView: 3.19,
        slidesOffsetAfter: 200,
      },
      1800: {
        slidesPerView: 4,
        slidesOffsetAfter: 500,
      },
      2000: {
        slidesPerView: 4.3,
        slidesOffsetAfter: 500,
      },
      2550: {
        slidesPerView: 5.3,
        slidesOffsetAfter: 500,
      },
    },
  });
};
