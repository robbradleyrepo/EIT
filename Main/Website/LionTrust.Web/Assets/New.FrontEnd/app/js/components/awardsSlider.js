    import Swiper from 'swiper/bundle';

    // core version + navigation, pagination modules:
    import SwiperCore, { Navigation, Pagination } from 'swiper/core';

    // configure Swiper to use modules
    SwiperCore.use([Navigation, Pagination]);
   
    export default (setWrapperStyles) => {

        new Swiper('.swiper-container-awards', {
            grabCursor: true,
            slidesPerView: 3,
            spaceBetween: 10,
            speed: 800,
            loop: false,
            simulateTouch : true,
            loopFillGroupWithBlank: false,
            breakpointsInverse: true,
            breakpoints: {
                576: {
                    slidesPerView: 2,
                    centeredSlides: true
                },           
            },


            pagination: {
                el: '.swiper-pagination',
                clickable: true,

            },
            navigation: {
                nextEl: '.swiper-button-next',
                prevEl: '.swiper-button-prev',
            }
        });
    }
