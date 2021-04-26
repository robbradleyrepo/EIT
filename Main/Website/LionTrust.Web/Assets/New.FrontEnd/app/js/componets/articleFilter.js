export default () => {
  const openSelectFilterBtn = document.querySelectorAll(
    ".article-filters__select"
  );
  const closeSubItems = document.querySelectorAll(".article-filters__title");
  const filter = document.querySelector(".article-filters");
  const closeFilterBtn = document.querySelector("#filters-close");
  const options = document.querySelectorAll(".article-filters__options");
  const optionCheckbox = document.querySelectorAll(".option-checkbox");
  const checkboxes = document.querySelectorAll(".option-checkbox__input");
  const sortDropdown = document.querySelector('.sort-dropdown')

  const hideSelectFilters = () => {
    document
      .querySelectorAll(".article-filters__select.active")
      .forEach((el) => {
        el.classList.remove("active");
      });
  };

  openSelectFilterBtn.forEach((el) => {
    el.addEventListener("click", (e) => {
      e.stopPropagation();
      if (!el.classList.contains("active")) hideSelectFilters();
      el.classList.toggle("active");
      el.parentNode
        .querySelector(".article-filters__options")
        .classList.add("active");
    });
  });

  options.forEach((option) => {
    option.addEventListener("click", (e) => {
      e.stopPropagation();
    });
  });

  const setSelectActive = () => {
    options.forEach((option) => {
      let setActive = false;
      const checkboxes = option.querySelectorAll(".option-checkbox__input");
      const title = option.parentNode.querySelector(
        ".article-filters__item-title"
      );
      if (title) title.classList.remove("active");
      checkboxes.forEach((checkbox) => {
        if (checkbox.checked) setActive = true;
        if (setActive && title) {
          title.classList.add("active");
        }
      });
    });
  };

  checkboxes.forEach((checkbox) => {
    checkbox.addEventListener("change", () => {
      setSelectActive();
    });
  });

  document.querySelector("body").addEventListener("click", () => {
    hideSelectFilters();
    sortDropdown.classList.remove('active')
  });

  closeSubItems.forEach((btn) => {
    btn.addEventListener("click", () => {
      const parentOfBtn = btn.parentElement;
      parentOfBtn.parentNode
        .querySelector(".article-filters__select")
        .classList.remove("active");
      parentOfBtn.classList.remove("active");
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

  document.querySelectorAll(".clear-filter").forEach((clear) => {
    clear.addEventListener("click", () => {
      checkboxes.forEach((checkbox) => (checkbox.checked = false));
      setSelectActive();
    });
  });

  
    document.querySelector('.button-sort').addEventListener("click", (e) => {
      e.stopPropagation();
      sortDropdown.classList.add("active");
    });
  
};
