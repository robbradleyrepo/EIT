export default () => {
    $('.fund-chart-dropdown').on('change', function(e) {        
        document.location = `?graph=${e.target.value}`;
    })
}