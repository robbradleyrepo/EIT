export default () => {
    $('[data-tab-target]').on('click', function() {
        const el = $(this);
        const id = el.attr('data-tab-target');
        $('[data-tab-target]').removeClass('active');
        el.addClass('active');
        $('[role="tabelement"]').removeClass('active');
        $(`#${id}`).addClass('active');
    })
}