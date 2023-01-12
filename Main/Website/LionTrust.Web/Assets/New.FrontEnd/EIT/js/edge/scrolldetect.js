"use strict";
var $ = require("jquery");

export default () => {

	// Get all of the images that are marked up to fade in
	const images = document.querySelectorAll('.js-lazyload-image');

	const sections = document.querySelectorAll('.t-global main .u-edge');

	let config = {
		rootMargin: '0px',
		threshold: .5
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
		const current = document.querySelector('.section.active');
		const next = entry.target; 

		if (current) {
		current.classList.remove('active');
		}
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