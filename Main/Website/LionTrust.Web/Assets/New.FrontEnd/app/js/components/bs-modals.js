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

    //Trigger people card modal
    $(".people-card a").on("click", function(e){
      e.preventDefault();
      $(this).parents(".people-card").next(".modal").modal("show");
    });
}