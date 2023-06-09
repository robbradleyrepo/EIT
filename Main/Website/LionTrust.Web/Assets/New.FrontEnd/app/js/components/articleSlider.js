import Swiper from "swiper/bundle";

export default () => {
  const swiper = new Swiper(".swiper-container-article", {
    // spaceBetween: 32,
    grabCursor: true,
    slidesPerView: 'auto',
    breakpointsInverse: true,
    pagination: {
      el: '.swiper-pagination-article',
      clickable: true,
      renderBullet: function(index, className) {
        return `<span class="dot swiper-pagination-bullet"></span>`;
      },
      },  

      navigation: {
        nextEl: '.swiper-button-next-article',
        prevEl: '.swiper-button-prev-article',
      }
    // breakpoints: {
    //   576: {
    //     slidesPerView: 1.5,
    //   },
    //   768: {
    //     slidesPerView: 2.2,
    //   },
    //   992: {
    //     slidesPerView: 2.7,
    //   },
    //   1200: {
    //     slidesPerView: 3.19,
    //     slidesOffsetAfter: 200,
    //   },
    //   1600: {
    //     slidesPerView: 3.27,
    //     slidesOffsetAfter: 200,
    //   },
    //   1800: {
    //     slidesPerView: 3.94,
    //     slidesOffsetAfter: 500,
    //   },
    //   2000: {
    //     slidesPerView: 4.3,
    //     slidesOffsetAfter: 500,
    //   },
    //   2550: {
    //     slidesPerView: 5.25,
    //     slidesOffsetAfter: 500,
    //   },
    // },
  });
};
