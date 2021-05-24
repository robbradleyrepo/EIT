import searchPage from "./componets/searchPage";

document.addEventListener("DOMContentLoaded", () => {  
  if($('body').hasClass('search-page'))
    searchPage();
});