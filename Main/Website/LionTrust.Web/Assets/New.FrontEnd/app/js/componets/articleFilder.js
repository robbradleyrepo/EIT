export default () => {
  const openSelectFilterBtn = document.querySelectorAll(
    ".article-filters__select"
  );
  const closeSubItems = document.querySelectorAll(".article-filters__title");
  const filter = document.querySelector(".article-filters");
  const closeFilterBtn = document.querySelector("#filters-close");
  const options = document.querySelectorAll(".article-filters__options");
  const optionCheckbox = document.querySelectorAll(".option-checkbox");

  openSelectFilterBtn.forEach((el) => {
    el.addEventListener("click", (e) => {
      console.log(e);
      console.log("parentNode", el.parentNode);
      el.parentNode.querySelector('.article-filters__options').classList.add("active");
    });
  });

  // optionCheckbox.forEach(checkbox => {
  //   checkbox.addEventListener('click', function(e) {
  //     e.stopPropagation()
  //     console.log('e', e);
  //   })
  // })

  closeSubItems.forEach((btn) => {
    btn.addEventListener("click", () => {
      console.log("btn", btn);
      btn.parentElement.classList.remove("active");
    });
  });

  closeFilterBtn.addEventListener("click", () => {
    filter.classList.remove("active");
  });

  document
    .querySelector("#show-article-fliter")
    .addEventListener("click", () => {
      filter.classList.add("active");
    });
};
