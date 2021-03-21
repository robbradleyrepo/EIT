import Swiper from 'swiper/bundle';

// core version + navigation, pagination modules:
import SwiperCore, { Navigation, Pagination } from 'swiper/core';

// configure Swiper to use modules
SwiperCore.use([Navigation, Pagination]);

export default () => {
    
    const swiper = new Swiper('.swiper-container-article', {
        slidesPerView: 1,
        spaceBetween: 5,
        grabCursor: true,
        centeredSlides: false,
        loop: true,
        breakpoints: {
            // 480: {
            //     slidesPerView: 1,
            //     spaceBetween: 20,
            //   },
            640: {
                slidesPerView: 2,
                spaceBetween: 10,
            },
            // 768: {
            //     slidesPerView: 2,
            //     spaceBetween: 20,
            // },
            // 1024: {
            //     slidesPerView: 2,
            //     spaceBetween: 20,
            // },
            1124: {
                slidesPerView: 3,
                spaceBetween: 10,
            },
        },
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
