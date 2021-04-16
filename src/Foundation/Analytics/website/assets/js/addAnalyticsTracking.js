$("a[data-goal-trigger]").click(function (e) {
	e.preventDefault();
	var $goalId = $(this).data("goal-trigger");
	if ($goalId != undefined && $goalId !== "00000000-0000-0000-0000-000000000000") {
		var $goalURL = "https://" + document.domain + "/-/media/foundation/analytics/pixel?sc_trk=" + $goalId;
		$.get($goalURL);
	}
	var $target = $(this).attr("href");
	// Use setTimeout to allow for the above get call to complete
	setTimeout(function () { window.location = $target; }, 500);
});