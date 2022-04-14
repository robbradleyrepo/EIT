import searchPage from "./components/searchPage";

document.addEventListener("DOMContentLoaded", () => {  
  if($('body').hasClass('search-page'))
    searchPage();
});