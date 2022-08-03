import Swiper from 'swiper/bundle';

// core version + navigation, pagination modules:
import SwiperCore, { Navigation, Pagination } from 'swiper/core';

// configure Swiper to use modules
SwiperCore.use([Navigation, Pagination]);

export default () => {
    // ✅ Count all elements with class of blue
    let countAll = document.querySelectorAll('.mainSlideDiv').length;


    console.log('l is', countAll); // 👉️ 3
    // const d = document.querySelectorAll('.mainSlideDiv')
    //     console.log('d is', d);
    if(countAll < 3) {
    //let slideDiv = document.getElementsByClassName('.swiper-wrapper')
    //if so get the last 2 elements and add the class
    // slideDiv.classList.add("awardscentered");
        console.log('thanks');
        let aiv = document.getElementById('awardsWrapper');
        console.log('slideDiv is', aiv);
        aiv.classList.add("awardscentered");

    //console.log('g is', slideDiv);
    }
    
    new Swiper('.swiper-container-awards', {

        // grabCursor: true,
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
            576: {
                slidesPerView: 2,
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
        },
    });
}
