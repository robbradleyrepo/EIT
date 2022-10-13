  import Swiper, { Pagination } from "swiper/bundle";

  export default () => {
    const swiper = new Swiper(".swiper-container-funds", {
      grabCursor: true,
      slidesPerView: "auto",
      breakpointsInverse: true,
      slidesOffsetAfter: 600,
      loop: true,
      observer: true,
      observeParents: true,
      watchSlidesVisibility: true,
      watchSlidesProgress: true,

      pagination: {
      el: '.swiper-pagination',
      clickable: true,
      renderBullet: function(index, className) {
        return `<span class="dot swiper-pagination-bullet"></span>`;
      },
      },  

      navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
      },

      lazyLoading: true,
      slideToClickedSlide: true,
      initialSlide: 1,

      });
  };





  