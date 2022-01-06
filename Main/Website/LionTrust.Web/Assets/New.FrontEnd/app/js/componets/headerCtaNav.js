import Swiper from "swiper/bundle";
export default () => {
  const swiper = new Swiper(".header-btn-nav", {
    slidesPerView: 'auto',
    grabCursor: true,
    breakpointsInverse: true,
  });
};
