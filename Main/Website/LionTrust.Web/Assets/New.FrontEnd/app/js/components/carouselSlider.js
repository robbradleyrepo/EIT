import Swiper from "swiper/bundle";

export default () => {
  const swiper = new Swiper(".swiper-container-carousel", {
    watchSlidesVisibility: true,
    centeredSlides: true,
    breakpointsInverse: true,
    longSwipes: true,
    slideToClickedSlide: true,
    setWrapperSize: false,
    spaceBetween: 5,
    loop: true,
    breakpoints: {
      0: {
        slidesPerView: 1,
      },
      992: {
        slidesPerView: 3,
      },
    },
    pagination: {
      el: ".swiper-pagination",
      clickable: true,
    },
    on: {
      slideChange: function () {
        var activeIndex = this.activeIndex;
        var realIndex = this.slides.eq(activeIndex).attr('data-swiper-slide-index');
       $('.swiper-slide').removeClass('previousActiveClass nextActiveClass swiper-slide-duplicate-prev swiper-slide-prev swiper-slide-duplicate-next swiper-slide-next');
       $('.swiper-slide[data-swiper-slide-index="'+realIndex+'"]').removeClass('previousActiveClass nextActiveClass swiper-slide-prev swiper-slide-duplicate-next');
       $('.swiper-slide[data-swiper-slide-index="'+realIndex+'"]').prev().addClass('previousActiveClass');
       $('.swiper-slide[data-swiper-slide-index="'+realIndex+'"]').next().addClass('nextActiveClass');
      },
    }
  });
};
