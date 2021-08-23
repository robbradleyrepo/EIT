// Onboarding overlay modal
import Cookies from "js-cookie";

export default () => {
	const onboarding = $(".onboarding-overlay");
	const btnStep = $("[data-set-step]");
	const rawAcceptText = $('.onboarding-overlay__text').text();
	var countryName = '';

	// default values
	const currentTab = Cookies.get("currentTab") || 0;

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

	if (onboarding.hasClass('active')) {
		$('body').addClass('overflow-hidden');
		showTab(currentTab);
	}

	// move to next step
	btnStep.on("click", function (e) {
		e.preventDefault();
		if (e.target.dataset.isoCountry) {
			SetCountry(e);
		}

		const tab = $(this).data('setStep');
		showTab(tab);
	});

	// navigation on tabs
	$('[data-change-step]').on("click", (e) => {
		changeStep(e);
	});

	// set country
	$(".set-location__item").on("click", (e) => {
		SetCountry(e);
		showTab(2);
	});

	$('.onboarding-overlay__link').on('click', function () {
		$('.onboarding-overlay__scroller').slideToggle();
	})

	const ChangeStep = (e) => {
		e.preventDefault();
		var tab = e.target.dataset.changeStep;
		if (tab > Cookies.get("currentTab")) return;
		else if (tab == 1) {
			tab = $('#correct-location').data('isoCountry')?.length > 0 ? 1 : 0
		}
		showTab(tab);
	}

	const SetCountry = (e) => {
		$('#Country').val(e.target.dataset.isoCountry);
		countryName = e.target.dataset.nameCountry;

		$.ajax({
			url: "/api/sitecore/Onboarding/GetInvestorRoles?countryIso=" + e.target.dataset.isoCountry
		}).done(function (data) {
			$(".choose-investor-role").html(data);

			if (e.target.dataset.isoCountry != "GB") {
				$('.onboarding-overlay__title.uk-title').hide();
				$('.onboarding-overlay__title.non-uk-title').show();
			}
			else {
				$('.onboarding-overlay__title.uk-title').show();
				$('.onboarding-overlay__title.non-uk-title').hide();
			}

			$("[data-investor-type]").on("click", (e) => {
				e.preventDefault();

				$('#InvestorId').val(e.target.dataset.investorType);
				var acceptText = $('.onboarding-overlay__text');
				$(acceptText).text(rawAcceptText.replace("{role}", e.target.dataset.investorName).replace("{country}", countryName));

				const tab = e.target.dataset.setStep;
				showTab(tab);
			});
		});

		$.ajax({
			url: "/api/sitecore/Onboarding/GetTermsAndConditions?countryIso=" + e.target.dataset.isoCountry
		}).done(function (data) {
			$(".onboarding-overlay__scroller.terms-text").html(data);
			$('.onboarding-overlay__scroller').toggle();
		});
	}
};
