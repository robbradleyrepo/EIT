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
        slidesOffsetAfter: 300,
      },
      2560: {
        slidesPerView: 4.5,
        slidesOffsetAfter: 300,
      },
    },
  });
};
