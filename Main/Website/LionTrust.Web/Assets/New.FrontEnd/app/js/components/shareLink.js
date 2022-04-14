import copy from 'copy-to-clipboard';

export default () => {

  $(".copy-to-clipbord").tooltip({
    trigger: "click",
  });

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

  $(".copy-to-clipbord").on("click", function (e) {
    e.preventDefault();
    const self = $(this);
    const message = self.data("title");
    const copyLink = self.data("link");
    copy(copyLink);
    setTooltip(message);
    hideTooltip();  
  });
};
