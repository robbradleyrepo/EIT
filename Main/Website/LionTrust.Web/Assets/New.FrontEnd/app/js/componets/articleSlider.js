import Swiper from 'swiper/bundle';

export default () => {
    
    const swiper = new Swiper('.swiper-container-article', {

        slidesPerView: 1.2,
        spaceBetween: 0,
        // slidesOffsetAfter: 30,
        breakpointsInverse: true,
        loop: true,
        breakpoints: {
            576: {
                slidesPerView: 1.8,
                spaceBetween: 10,

            },
            768: {
                slidesPerView: 2.2,
            },
            992: {
                slidesPerView: 2.7,
            },
            1200: {
                slidesPerView: 3.5,
            },
            2560: {
                slidesPerView: 4.5,
            }
        }
     
    });
}
