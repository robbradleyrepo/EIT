import "core-js/es/number";
import "core-js/features/string/repeat";
import $ from "jquery";
window.jQuery = $;
window.$ = $;  
 

import scrollDetect from "./edge/scrolldetect"; 
import mobileNav from "./edge/mobile-nav";
import shareLink from "./edge/share-link"; 
import slickCarousel from "./edge/slick-carousel-double"; 
import slickCarouselSingle from "./edge/slick-carousel-single";  
import DirectorListSlickComponent from "./edge/slick-carousel-directorlist"; 
import slidePanel from "./edge/slide-panel"; 
import tableRowCount from "./edge/table-row-count";  
import ModalVideoComponent from "./edge/modal-video";
import slickCarouselAwards from "./edge/slick-carousel-awards";
// import form from "./edge/form";


document.addEventListener("DOMContentLoaded", () => {
 
    scrollDetect(); 
    mobileNav();
    shareLink();    
    slickCarousel();
    slickCarouselSingle();
    slidePanel();
    tableRowCount();
    DirectorListSlickComponent();  
    ModalVideoComponent();
    slickCarouselAwards();
    // form();
}); 

 