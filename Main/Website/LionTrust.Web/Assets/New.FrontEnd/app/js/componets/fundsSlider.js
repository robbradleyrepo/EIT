import Swiper from "swiper/bundle";

export default () => {
  const swiper = new Swiper(".swiper-container-funds", {
    grabCursor: true,
    slidesPerView: "auto",
    spaceBetween: 32,
    slidesOffsetAfter: 30,
    breakpointsInverse: true,
    breakpoints: {
      0: {
        slidesPerView: 1.2,
        spaceBetween: 16,
      },
      576: {
        slidesPerView: 2.5,
        spaceBetween: 16,
      },
      768: {
        slidesPerView: 2.5,
        spaceBetween: 20,
      },
      992: {
        slidesPerView: 1.2,
      },
      1200: {
        slidesPerView: 1.2,
      },
      1440: {
        slidesPerView: 1.8,
      },
    },
  });
};
