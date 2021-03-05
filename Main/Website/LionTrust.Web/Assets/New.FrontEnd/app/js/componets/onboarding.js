// Onboarding overlay modal
import Cookies from "js-cookie";

export default () => {
  const onboarding = $(".onboarding-overlay");
  const btnStep = $("[data-set-step]");

  // default values
  const currentTab = Cookies.get("currentTab") || 1;
//   const country = Cookies.get("country") || "United Kingdom";
//   const inverstorType = Cookies.get("inverstorType") || 0;
  const agreePolicy = Cookies.get("agreePolicy") || false;

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

  // start showing tab
  if(!agreePolicy) {
    showTab(currentTab);
    onboarding.addClass('active')
  }

  // move to next step
  btnStep.on("click", (e) => {
    e.preventDefault();
    const tab = e.target.dataset.setStep;
    showTab(tab);
  });

  // navigation on tabs
  $('[data-change-step]').on("click", (e) => {
    e.preventDefault();
    const tab = e.target.dataset.changeStep;
    if (tab > currentTab) return;
    showTab(tab);
  });  

  // set investor type to cookie
  $("[data-investor-type]").on("click", (e) => {
    Cookies.set("inverstorType", e.target.dataset.investorType);
  });

  // set country to cookie
  $(".set-location__item").on("click", (e) => {
    const country = e.target.dataset.isoCountry;
    Cookies.set("country", country);
    showTab(2);
  });

  // finish onboarding, close modal
  $("#submit-board").on("click", () => {
    onboarding.removeClass("active");
    Cookies.set("agreePolicy", 1);
  });
};
