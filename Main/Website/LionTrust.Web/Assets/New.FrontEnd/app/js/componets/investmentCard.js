import Swiper from 'swiper';

export default () => {    
    new Swiper('#investment-slider', {
        slidesPerView: 'auto',
        breakpointsInverse: true, 
      });
}