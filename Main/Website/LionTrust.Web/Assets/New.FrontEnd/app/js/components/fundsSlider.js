  import Swiper, { Pagination } from "swiper/bundle";

  export default () => {
      //create new var for pagination
     
      //create new variable for the amount of carousel cards 
      // align carousel to left in awardsSlider.js
       let description = document.getElementsByClassName("imagepromo__contentbox p").length;
       let fundScroller = document.getElementsByClassName("fundscroller-container");
       let prevArrow = document.getElementsByClassName("swiper-button-prev");
       let nextArrow = document.getElementsByClassName("swiper-button-next");
       let allCards = document.getElementsByClassName("swiper-slide fund-card");
       let swiperPagination = document.getElementsByClassName("swiper-pagination");

      
    
     

    const swiper = new Swiper(".swiper-container-funds", {
      grabCursor: true,
      slidesPerView: "auto",
      breakpointsInverse: true,
      slidesOffsetAfter: 30,
      observer: true,
      loop: true,
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
      //left align if no description is available
      if(description.length === 0){
        fundScroller.classList.add("full-width-carousel");
      }

       //if carousel cards less than 3, hide pagination and arrows
       if(allCards.length > 3){
        swiperPagination.classList.add("hide-pagination");
        nextArrow.classList.add("hide-arrows");
        prevArrow.classList.add("hide-arrows");
       }
  };





  