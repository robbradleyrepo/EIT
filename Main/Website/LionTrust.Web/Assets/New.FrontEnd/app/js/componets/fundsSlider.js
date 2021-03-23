


import Swiper from 'swiper/bundle';

// core version + navigation, pagination modules:
import SwiperCore, { Navigation, Pagination } from 'swiper/core';

// configure Swiper to use modules
SwiperCore.use([Navigation, Pagination]);

export default () => {
    
    const swiper = new Swiper('.swiper-container-funds', {
        slidesPerView: 2,
        spaceBetween: 20,
        grabCursor: true,
        centeredSlides: false,
        loop: true,
        pagination: {
            el: '.swiper-pagination',
        clickable: true,

        },
        // navigation: {
        //     nextEl: '.swiper-button-next',
        //     prevEl: '.swiper-button-prev',
        // },
    });
}
