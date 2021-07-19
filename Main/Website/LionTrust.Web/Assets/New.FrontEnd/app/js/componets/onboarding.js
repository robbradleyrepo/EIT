// Onboarding overlay modal
import Cookies from "js-cookie";

export default () => {
  const onboarding = $(".onboarding-overlay");
  const btnStep = $("[data-set-step]");
  const rawAcceptText = $('.onboarding-overlay__text').text();

  // default values
  const currentTab = Cookies.get("currentTab") || 1;

  const showTab = (currentTab) => {
    const tabs = $("[data-tab-number]");
    tabs.removeClass("visible");
    $(tabs[currentTab]).addClass("visible");

    const step = $(".onboarding-overlay__step");
    step.removeClass("active");

    if (currentTab == 0 || currentTab == 1) $(step[0]).addClass("active");
    if (currentTab == 2) $(step[1]).addClass("active");
    if (currentTab == 3) $(step[2]).addClass("active");

    Cookies.set("currentTab", currentTab);
  };

	if(onboarding.hasClass('active')){
		$('body').addClass('overflow-hidden');
		showTab(currentTab);
	}

  // move to next step
  btnStep.on("click", (e) => {
    e.preventDefault();
	
	if(e.target.dataset.isoCountry) {
		SetCountry(e);
	}
	
    const tab = e.target.dataset.setStep;
    showTab(tab);
  });

  // navigation on tabs
  $('[data-change-step]').on("click", (e) => {
    e.preventDefault();
    var tab = e.target.dataset.changeStep;
    if (tab > Cookies.get("currentTab")) return;
	else if(tab == 1){
		tab = $('#correct-location').data('isoCountry').length > 0 ? 1 : 0 
	}
    showTab(tab);
  });  

  // set investor type to cookie
  $("[data-investor-type]").on("click", (e) => {
	 $('#Role').val(e.target.dataset.investorType);
	 var acceptText = $('.onboarding-overlay__text');
	 $(acceptText).text(rawAcceptText.replace("{role}", e.target.dataset.investorName));
  });

  // set country
  $(".set-location__item").on("click", (e) => {  
	SetCountry(e);
    showTab(2);
  });

  // finish onboarding, close modal
  $("#submit-board").on("click", () => {
	  $.ajax({
		type: "POST",
		url: "/api/sitecore/Onboarding/Submit",
		data: {Country : $('#Country').val(), Role: $('#Role').val()},
		success: function(data){
			onboarding.removeClass("active");
			$('body').removeClass('overflow-hidden')
		},
		error: function(data) {
			console.log(data.message);
		}
	  });
  });

  $('.onboarding-overlay__link').on('click', function() {
    $('.onboarding-overlay__scroller').slideToggle();
  })
  
  const SetCountry = (e) => {
	  $('#Country').val(e.target.dataset.isoCountry);
	 var acceptText = $('.onboarding-overlay__text');
	 $(acceptText).text(rawAcceptText.replace("{country}", e.target.dataset.nameCountry));

	 if(e.target.dataset.isoCountry != "GB"){
		 $('.btn.private-investor').hide();
		 $('.onboarding-overlay__title.uk-title').hide();
		 $('.onboarding-overlay__title.non-uk-title').show();
	 }
	 else{
		 $('.btn.private-investor').show();
		 $('.onboarding-overlay__title.uk-title').show();
		 $('.onboarding-overlay__title.non-uk-title').hide();
	 }
	 
	 $.ajax({
		url: "/api/sitecore/Onboarding/GetTermsAndConditions?countryIso=" + e.target.dataset.isoCountry
	 }).done(function(data) {
		$(".onboarding-overlay__scroller.terms-text").html(data);
		$('.onboarding-overlay__scroller').toggle();
	});
  }
};
