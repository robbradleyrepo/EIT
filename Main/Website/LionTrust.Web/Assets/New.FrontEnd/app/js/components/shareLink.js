import copy from 'copy-to-clipboard';

export default () => {

  if($(".copy-to-clipbord").length){
    $(".copy-to-clipbord").tooltip({
      trigger: "click",
    });
  }

  const setTooltip = (message) => {
    $(".copy-to-clipbord")
      .tooltip("hide")
      .attr("data-title", message)
      .tooltip("show");
  };

  const hideTooltip = () => {
    setTimeout(() => {
      $(".copy-to-clipbord").tooltip("hide");
    }, 3000);
  };

  if($(".copy-to-clipbord").length){
    $(".copy-to-clipbord").on("click", function (e) {
      e.preventDefault();
      const self = $(this);
      const message = self.data("title");
      const copyLink = self.data("link");
      copy(copyLink);
      setTooltip(message);
      hideTooltip();  
    });
  }
};
