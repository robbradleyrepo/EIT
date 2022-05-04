import Swiper from "swiper/bundle";

export default () => {
  const swiper = new Swiper(".swiper-container-funds", {
    grabCursor: true,
    slidesPerView: "auto",
    slidesOffsetAfter: 600,
    breakpointsInverse: true,
    loop: true,

  });
};
