"use strict"; 
var $ = require("jquery");
export default () => {


	const images = document.querySelectorAll('.js-lazyload-image');
	const sections = document.querySelectorAll('.t-global main .u-edge');

	function widthResizer(){
		var width = window.innerWidth
		if (width > 992) {
			engageObserver(); 
		}  

	}
	  
	function engageObserver() {
		let config = {
			rootMargin: '100px',
			threshold: 0.4,
			triggerOnce: true,
		};

		let observer = new IntersectionObserver((entries) => {
			entries.forEach(entry => {
			if (entry.isIntersecting) {
				intersectionHandler(entry);
			} 

			});
		}, config);

		sections.forEach(section => {
			observer.observe(section);
		});

		function intersectionHandler(entry) {
			const next = entry.target; 
			if (next) {
				next.classList.add('active'); 
			}
		}

		images.forEach(image => {
			observer.observe(image);
		});

		function preloadImage(img) {
			const src = img.getAttribute('data-src');
			if (!src) { return; }
			img.src = src;
		}
	}

	widthResizer();
	window.addEventListener('resize', widthResizer);

}