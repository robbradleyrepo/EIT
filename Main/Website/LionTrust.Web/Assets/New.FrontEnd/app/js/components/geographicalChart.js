export default () => {
 
 let geographicalRowCount = $('.geographical-breakdown tr').length;
 let sectorRowCount = $('.sector-breakdown tr').length;
 
 let toggleGeographicalButton = $('.toggle-geographical-button');
 let toggleSectorButton = $('.toggle-sector-button');
 console.log("number of sector rows = "+ sectorRowCount, "number of geo rows =" + geographicalRowCount);
 
 if(geographicalRowCount <= 10 ) {
    toggleGeographicalButton.hide();
          
 }
 if(sectorRowCount <= 10){
     toggleSectorButton.hide();
 }

  $(".sector-breakdown, .geographical-breakdown").addClass("hide-rows");
  $(".toggle-sector-button, .toggle-geographical-button").click(function(){
    $(".sector-breakdown, .geographical-breakdown").toggleClass("hide-rows");
    $(this).text(function(i, v){
      return v === '+' ? '-' : '+'
   });
  });
  
}


