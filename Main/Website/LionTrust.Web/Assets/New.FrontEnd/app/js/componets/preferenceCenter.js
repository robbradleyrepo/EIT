export default () => {
    var $list = $('.multi-select-card__group ');
    var yesIsChecked = $('.can-toggle input');
    var selectAllList = $('.multi-select-card__allSelected input');
    $list.each(function() {
        var $listItems = $(this).find(".multi-select-card__row");
        var numInList = $listItems.length;
        var checkedList = $listItems.find('input:checked').length;        
        $(this).parents('.accordion__card').find('.slected_count').text(checkedList);        
        $(this).parents('.accordion__card').find('.total_count').text(numInList);
        $listItems.find("input").on('change', function() {
            if ($(this).parents('.accordion__card').find('.multi-select-card__group input:checked').length == $(this).parents('.accordion__card').find('.multi-select-card__group input').length) {
                $(this).parents('.accordion__card').find('.multi-select-card__allSelected input').prop('checked', this.checked);                
            } else {
                $(this).parents('.accordion__card').find('.multi-select-card__allSelected input').prop('checked', false);
            }
            checkedList = $listItems.find('input:checked').length;            
            $listItems.parents('.accordion__card').find('.slected_count').text(checkedList);
        });
    });
    $(selectAllList).on('change', function() {
        $(this).parents('.multi-select-card').find('.multi-select-card__group input').prop('checked', this.checked).trigger('change');
    })
    $('.showPreferencesForms').on('click', function(e) {
        e.preventDefault();
        $('.retrive-preferences-form').toggleClass('retrive-preferences-form__show');
    })
    $('.yes-is-checked').addClass('yes-is-checked__show');
    yesIsChecked.on('change', function() {
        if (yesIsChecked.is(':checked')) {
            $('.yes-is-checked').addClass('yes-is-checked__show');
            $('.no-is-checked').removeClass('no-is-checked__show');
        } else {
            $('.yes-is-checked').removeClass('yes-is-checked__show');
            $('.no-is-checked').addClass('no-is-checked__show');
        }
    });
}