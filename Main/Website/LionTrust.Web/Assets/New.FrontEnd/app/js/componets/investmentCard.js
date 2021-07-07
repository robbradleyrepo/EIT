import Swiper from 'swiper';

export default () => {    
    const swiper = new Swiper('#investment-slider', {
        slidesPerView: 1.2,
        spaceBetween: 32,
        slidesOffsetAfter: 320,
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
                slidesPerView: 2.2,
                spaceBetween: 8,
            },
            992: {
                slidesPerView: 2.8,
                spaceBetween: 10,
            },
            1200: {
                slidesPerView: 3.5,
                spaceBetween: 32
            },
            1400: {
                slidesPerView: 4,
                spaceBetween: 32,
                slidesOffsetAfter: 700,
            },
            2560: {
                slidesPerView: 4.5,
                spaceBetween: 32,
                slidesOffsetAfter: 700,
            }
        }
      });
}