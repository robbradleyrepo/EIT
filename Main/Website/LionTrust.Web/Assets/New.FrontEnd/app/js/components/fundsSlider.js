import Swiper, { Nvaigation, Pagination } from "swiper/bundle";

export default () => {

  const swiper = new Swiper(".swiper-container-funds", {
    grabCursor: true,
    slidesPerView: "auto",
    breakpointsInverse: true,
    slidesOffsetAfter: 0,
    slidesOffsetBefore: 0,
    watchOverflow: true, 
    observer: true,
    observeParents: true,
    watchSlidesVisibility: true,
    watchSlidesProgress: true,
    lazyLoading: true,
    slideToClickedSlide: true,
    initialSlide: 0,
 
   

    pagination: {
      el: ".swiper-pagination",
      clickable: true,
      renderBullet: function (index, className) {
        return `<span class="dot swiper-pagination-bullet"></span>`;
      },
      hidePaginatiom: function () {
        if (this.slidesPerView.length < 3) {
          prevArrow.classList.add("'swiper-button-disabled'");
          nextArrow.classList.add("'swiper-button-disabled'");
        }
      },
    },

    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },

    
  });

 let fundScroller = $(".fundscroller-container");
 
 fundScroller.each(function(){

  let nextButton= $(this).find(".swiper-button-next");
  let prevButton= $(this).find(".swiper-button-prev");
  let pagination= $(this).find(".swiper-pagination");
  let swiperSlides= $(this).find(".swiper-slide");
 
   
  let numberOfSlides = 3;
  if(swiperSlides.length < numberOfSlides){
    pagination.hide();
    prevButton.hide();
    nextButton.hide();
  }
 
  })


};
