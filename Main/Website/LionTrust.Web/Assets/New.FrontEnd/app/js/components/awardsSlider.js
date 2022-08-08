    import Swiper from 'swiper/bundle';

    // core version + navigation, pagination modules:
    import SwiperCore, { Navigation, Pagination } from 'swiper/core';

    // configure Swiper to use modules
    SwiperCore.use([Navigation, Pagination]);
    

    export default () => {
        const countAllSlides = document.querySelectorAll('.mainSlideDiv').length;
        const threeSliders = 3;

        if(countAllSlides <= threeSliders) {
            let swrapper = document.getElementById('awardsWrapper');
            swrapper.classList.add("awardscentered");
            let hidepagination = document.getElementById('awardspag');
            hidepagination.classList.add("hidePagination");
        }
        
        new Swiper('.swiper-container-awards', {

            grabCursor: true,
            slidesPerView: 3,
            centeredSlides: true,
            spaceBetween: 32,
            speed: 800,
            loop: false,
            simulateTouch : true,
            loopFillGroupWithBlank: false,
            breakpointsInverse: true,
            breakpoints: {
                0: {
                    slidesPerView: 1.8,
                    centeredSlides: false
                },
                768: {
                    slidesPerView: 2.2,
                    centeredSlides: false
                },
                992: {
                    slidesPerView: 3,
                    centeredSlides: false
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
