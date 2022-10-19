//Can be used on any bootstrap modal (add modal class to below selectors)
export default () => {
    //execute on modal opening
    $('.modal-people').on('show.bs.modal', function () {
      $("html").addClass("no-scroll");
    });

    //execute on modal close
    $('.modal-people').on('hide.bs.modal', function () {
      $("html").removeClass("no-scroll");
    });
}