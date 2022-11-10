export default () => {
$('#CybotCookiebotDialogPoweredbyImage').attr('src','/-/media/cookie-logo');
  var avatarLength = $('.hero-manager-link__link').length;
  
if(avatarLength > 4)
{
  $('.hero-manager-link').addClass('avatarLinks-row');
  $('.hero-manager-link__link:nth-child(-n+4)').wrapAll('<div class="avatar-row"></div>');
  $('.hero-manager-link > .hero-manager-link__link').wrapAll('<div class="avatar-row"></div>');
}  
};