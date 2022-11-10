export default () => {
 
  let avatarLength = $('.hero-manager-link__link').length;
  let authors = $(".hero-manager-link__link");
  let maxNumberAuthors = 4;
  
   if (avatarLength >= maxNumberAuthors) {
        authors.hide();
       
   }
  
}