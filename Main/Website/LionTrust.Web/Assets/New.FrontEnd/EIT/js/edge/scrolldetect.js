"use strict"; 

export default () => {

	// Get all of the images that are marked up to fade in
	const images = document.querySelectorAll('.js-lazyload-image');
	const sections = document.querySelectorAll('.t-global main .u-edge:not(:first-child)');

	let config = {
		rootMargin: '0px',
		threshold: .3
	};

	let observer = new IntersectionObserver((entries) => {
		console.log(entries);
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