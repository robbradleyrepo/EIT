import Swiper from 'swiper';

export default () => {    
    const swiper = new Swiper('#investment-slider', {
        slidesPerView: 1.2,
        spaceBetween: 32,
        slidesOffsetAfter: 30,
        breakpointsInverse: true,
        breakpoints: {
            576: {
                slidesPerView: 1.5,
            },
            768: {
                slidesPerView: 2.2,
            },
            992: {
                slidesPerView: 2.7,
            },
            1200: {
                slidesPerView: 3.1,
            },
            2560: {
                slidesPerView: 4.5,
            }
        }
      });
}