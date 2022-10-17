//Can be used on any bootstrap modal (add modal class to below selectors)
export default () => {
    //execute on modal opening
    $('#peopleCard').on('show.bs.modal', function () {
      $("html").addClass("no-scroll");
    });

    //execute on modal close
    $('#peopleCard').on('hide.bs.modal', function () {
      $("html").removeClass("no-scroll");
    });
}