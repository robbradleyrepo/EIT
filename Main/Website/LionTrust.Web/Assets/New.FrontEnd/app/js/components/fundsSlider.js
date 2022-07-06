  import Swiper, { Pagination } from "swiper/bundle";

  export default () => {
    const swiper = new Swiper(".swiper-container-funds", {
      grabCursor: true,
      slidesPerView: "auto",
      slidesOffsetAfter: 600,
      breakpointsInverse: true,
      loop: true,
      observer: true,
      observeParents: true,
      watchSlidesVisibility: true,
      watchSlidesProgress: true,

      // Navigation arrows
      navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
      },

      // And if we need scrollbar
      scrollbar: {
        el: '.swiper-scrollbar',
      },
      lazyLoading: true,
      slideToClickedSlide: true,
      initialSlide: 1,

      });
  };





  