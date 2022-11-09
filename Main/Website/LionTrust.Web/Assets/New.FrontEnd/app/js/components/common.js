export default () => {
$('#CybotCookiebotDialogPoweredbyImage').attr('src','/-/media/cookie-logo');
  var avatarLength = $('.hero-manager-link__link').length;
  
  let numberOfAuthors = $(".hero-manager-link__link").length;
  let authors = $(".hero-manager-link__link");
  let maxNumberAuthors = 4;

if(avatarLength > 4)
{
  $('.hero-manager-link').addClass('avatarLinks-row');
  $('.hero-manager-link__link:nth-child(-n+4)').wrapAll('<div class="avatar-row"></div>');
  $('.hero-manager-link > .hero-manager-link__link').wrapAll('<div class="avatar-row"></div>');
}

 if (numberOfAuthors >= maxNumberAuthors) {
        authors.hide();
       
   }  
  
};