import Swiper from 'swiper';

export default () => {    
    const swiper = new Swiper('#investment-slider', {
        slidesPerView: 1.2,
        spaceBetween: 32,
        slidesOffsetAfter: 30,
        breakpointsInverse: true,
        breakpoints: {
            0: {
                slidesPerView: 1.2,
                spaceBetween: 8,
            },
            576: {
                slidesPerView: 1.5,
                spaceBetween: 8,
            },
            768: {
                slidesPerView: 2.7,
                spaceBetween: 8,
            },
            992: {
                slidesPerView: 3.3,
                spaceBetween: 10,
            },
            1200: {
                slidesPerView: 4,
                spaceBetween: 32
            },
            2560: {
                slidesPerView: 4.5,
                spaceBetween: 32
            }
        }
      });
}