import Swiper from 'swiper/bundle';

// core version + navigation, pagination modules:
import SwiperCore, { Navigation, Pagination } from 'swiper/core';

// configure Swiper to use modules
SwiperCore.use([Navigation, Pagination]);

export default () => {
    
    const swiper = new Swiper('.swiper-container-awards', {

        grabCursor: true,
        slidesPerView: 'auto',
        centeredSlides: true,
        spaceBetween: 30,
        speed: 800,
        loop: true,
        simulateTouch : true,
        loopFillGroupWithBlank: false,
        // slidesOffsetAfter: 30,
        // breakpointsInverse: true,
        // breakpoints: {
        //     576: {
        //         slidesPerView: 1.5,
        //     },
        //     768: {
        //         slidesPerView: 2.2,
        //     },
        //     992: {
        //         slidesPerView: 2.7,
        //     },
        //     1200: {
        //         slidesPerView: 3.5,
        //     },
        //     2560: {
        //         slidesPerView: 4.5,
        //     }
        // },


        pagination: {
            el: '.swiper-pagination',
            clickable: true,

        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
    });
}
