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
      //pag
      pagination: {
      el: '.swiper-pagination',
      clickable: true,
      renderBullet: function(index, className) {
        return `<span class="dot swiper-pagination-bullet"></span>`;
      },
      },  
      // Navigation arrows
      navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
      },

      lazyLoading: true,
      slideToClickedSlide: true,
      initialSlide: 1,

      });
  };





  