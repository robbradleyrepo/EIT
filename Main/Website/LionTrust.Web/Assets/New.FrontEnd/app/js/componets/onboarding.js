// Onboarding overlay modal

export default () => {
  const onboarding = $(".onboarding-overlay");
  const btnStep = $("[data-set-step]");

  const showTab = (currentTab) => {
    // const urlParams = new URLSearchParams(window.location.search);
    // if(currentTab === 0) {
    // console.log('urlParams',urlParams);
    // urlParams.set('step', currentTab);
    // window.location.search = urlParams;
    // }

    const tabs = $("[data-tab-number]");
    tabs.removeClass("visible");
    $(tabs[currentTab]).addClass("visible");
    const step = $(".onboarding-overlay__step");
    console.log("step", step);
    step.removeClass("active");
    console.log("currentTab", currentTab);
    if (currentTab == 0 || currentTab == 1) $(step[0]).addClass("active");
    if (currentTab == 2) $(step[1]).addClass("active");
    if (currentTab == 3) $(step[2]).addClass("active");
  };

  showTab(2);

  btnStep.on("click", (e) => {
    e.preventDefault();
    console.log(e);
    const tab = e.target.dataset.setStep;
    console.log("tab", tab);
    showTab(tab);
  });

  $(".set-location__item").on("click", (e) => {
    const textValue = e.target.innerText;
    console.log("textValue", textValue);
    showTab(2);
  });

  $("#submit-board").on("click", () => {
    console.log(onboarding);
    onboarding.removeClass("active");
  });
};
