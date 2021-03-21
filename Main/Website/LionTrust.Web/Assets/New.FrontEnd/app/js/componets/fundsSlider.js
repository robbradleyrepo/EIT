


import Swiper from 'swiper/bundle';

// core version + navigation, pagination modules:
import SwiperCore, { Navigation, Pagination } from 'swiper/core';

// configure Swiper to use modules
SwiperCore.use([Navigation, Pagination]);

export default () => {
    
    const swiper = new Swiper('.swiper-container-funds', {
        // effect: 'coverflow',
        slidesPerView: 2,
        spaceBetween: 20,
        // grabCursor: true,
        centeredSlides: false,
        // coverflowEffect: {
        //     rotate: 0,
        //     stretch: 0,
        //     depth: 100,
        //     modifier: 1,
        //     slideShadows: true,
        // },
        // freeMode: false,
        loop: true,
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
