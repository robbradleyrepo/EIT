import Swiper, { Nvaigation, Pagination } from "swiper/bundle";

export default () => {

  const swiper = new Swiper(".swiper-container-funds", {
    grabCursor: true,
    slidesPerView: "auto",
    breakpointsInverse: true,
    slidesOffsetAfter: 0,
    slidesOffsetBefore: 0,
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
    initialSlide: 0,
  });

  let description = document.getElementsByClassName("imagepromo__contentbox");
  let fundScroller = document.getElementsByClassName("fundscroller-container");
  let prevArrow = document.getElementsByClassName("swiper-button-prev");
  let nextArrow = document.getElementsByClassName("swiper-button-next");
 
  let allCards = document.getElementsByClassName("forSlides");
  console.log("number of slides " + allCards.length);
  
  if (description.length === 0) {
    fundScroller.classList.add("full-width-carousel");
  }
  
};
