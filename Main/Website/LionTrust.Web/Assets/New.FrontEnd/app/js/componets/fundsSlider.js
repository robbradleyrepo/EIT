import Swiper from 'swiper/bundle';

export default () => {
    

    const swiper = new Swiper('.swiper-container-funds', {

        grabCursor: true,
        // slidesPerView: 1.2,
        slidesPerView: 'auto',

        spaceBetween: 32,
        slidesOffsetAfter: 30,
        breakpointsInverse: true,
        loop: true,
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
        // }
   
    });
}
