export default () => {
    if ($(".modal-right").data('bs.modal') && $(".modal-right").data('bs.modal').isShown) {
        console.log('hello')
    }
}