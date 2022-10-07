import Swiper from 'swiper/bundle';

// core version + navigation, pagination modules:
import SwiperCore, { Navigation, Pagination } from 'swiper/core';

// configure Swiper to use modules
SwiperCore.use([Navigation, Pagination]);


export default () => {
    
  
  

    let nodeList = document.querySelectorAll(".swiper-container-awards");
    for (let i = 0; i < nodeList.length; i++) {
     

        let countAllSlides = nodeList[i].querySelectorAll('.slideDiv').length;
        console.log("all slides = "+ countAllSlides)
        let maxSlidersWhithNoPagination = 3;

        // remove pagination 
        if (countAllSlides <= maxSlidersWhithNoPagination) {
            nodeList[i].getElementsByClassName('swiper-wrapper slidesWrapper')[0].classList.add('centerawards');
            nodeList[i].getElementsByClassName('swiper-pagination awardpagination')[0].classList.add('hideawardsPagination');
        }

    }


    new Swiper('.swiper-container-awards', {

        slidesPerView: 3,
        spaceBetween: 32,
        speed: 800,
        loop: false,
        
        
     

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
