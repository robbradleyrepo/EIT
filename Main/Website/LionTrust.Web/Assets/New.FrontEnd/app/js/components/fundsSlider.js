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
      el: ".swiper-pagination",
      clickable: true,
      renderBullet: function (index, className) {
        return `<span class="dot swiper-pagination-bullet"></span>`;
      },
      hidePaginatiom: function () {
        if (this.slidesPerView.length > 3) {
          prevArrow.classList.add("hide-arrows");
          nextArrow.classList.add("hide-arrows");
        }
      },
    },

    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },

    lazyLoading: true,
    slideToClickedSlide: true,
    initialSlide: 1,
  });

  let description = document.getElementsByClassName("imagepromo__contentbox");
  let fundScroller = document.getElementsByClassName("fundscroller-container");
  let slides = document.getElementsByClassName("swiper-slides");
  let prevArrow = document.getElementsByClassName("swiper-button-prev");
  let nextArrow = document.getElementsByClassName("swiper-button-next");
  if (description.length === 0) {
    fundScroller.classList.add("full-width-carousel");
  }
  
};
