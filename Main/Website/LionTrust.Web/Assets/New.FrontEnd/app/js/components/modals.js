export default () => {
    $('#peopleCard').on('shown.bs.modal', function () {
        $("html").addClass("no-scroll");
      })
}