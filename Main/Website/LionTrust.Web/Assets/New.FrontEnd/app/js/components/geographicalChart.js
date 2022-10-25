export default () => {
 
 let geographicalRowCount = $('.geographical-breakdown tr').length;
 let sectorRowCount = $('.sector-breakdown tr').length;
 
 let toggleGeographicalButton = $('.toggle-geographical-button');
 let toggleSectorButton = $('.toggle-sector-button');
 
 if(geographicalRowCount <= 10 ) {
    toggleGeographicalButton.hide();
          
 }
 if(sectorRowCount <= 10){
     toggleSectorButton.hide();
 }
 
  let chartButton = $(".chartButton");
  $(".sector-breakdown, .geographical-breakdown").addClass("hide-rows");
  chartButton.each(function(index){
    $(this).on("click", function(){
     $(this).prev().toggleClass("hide-rows");
     $(this).text(function(i, v){
      return v === '+' ? '-' : '+'
   });
    })
  })
  
}


