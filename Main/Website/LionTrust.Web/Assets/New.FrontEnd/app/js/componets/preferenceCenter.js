 export default () => {
     var $list = $('.multi-select-card__group ');
     $list.each(function() {
         var $listItems = $(this).find(".multi-select-card__row");
         var numInList = $listItems.length;
         var checkedList = $list.find('input:checked').length;
         console.log(checkedList);
         $('.slected_count').text(checkedList);
         console.log(numInList);
         $($listItems).parents('.accordion__card').find('.total_count').text(numInList);
         $listItems.find("input").on('change', function() {
             if ($(this).parents('.accordion__card').find('.multi-select-card__group input:checked').length == $(this).parents('.accordion__card').find('.multi-select-card__group input').length) {
                 $(this).parents('.accordion__card').find('.multi-select-card__allSelected input').attr('checked', 'checked');
                 //alert('all selected');
             } else {
                 $(this).parents('.accordion__card').find('.multi-select-card__allSelected input').removeAttr('checked');
             }
             checkedList = $listItems.find('input:checked').length;
             console.log(checkedList);
             $listItems.parents('.accordion__card').find('.slected_count').text(checkedList);
         });
     });
 }