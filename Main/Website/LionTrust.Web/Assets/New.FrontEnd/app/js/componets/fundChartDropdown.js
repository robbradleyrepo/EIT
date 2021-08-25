export default () => {
    $('.fund-chart-dropdown').on('change', function(e) {   
	const anchor = $(this).data('anchor');
			if(anchor && anchor.length > 0) {		
				document.location.hash = `#${anchor}`;
			}
			else {
				document.location.hash = '';
			}
			
			document.location.search = `?graph=${e.target.value}`;
		}
	)	
}