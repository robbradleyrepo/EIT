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
        if (this.slidesPerView.length < 8) {
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

  // let description = document.getElementsByClassName("imagepromo__contentbox");
  // if(description.length !== 0){
  //   fundScroller.classList.add("full-width-carousel");
  // }
  let fundScroller = $(".fundscroller-container");
 
 fundScroller.each(function(){

  let nextButton= $(this).find(".swiper-button-next");
  let prevButton= $(this).find(".swiper-button-prev");
  let pagination= $(this).find(".swiper-pagination");
  let swiperSlides= $(this).find(".swiper-slide");
 
 
  nextButton.on("click", function(e){
    if ($("swiper-button-disabled").length){
      e.preventDefault();
    }
  })
  
  
  if(swiperSlides.length < 3){
    pagination.hide();
    prevButton.hide();
    nextButton.hide();
  }
 
  })


};
