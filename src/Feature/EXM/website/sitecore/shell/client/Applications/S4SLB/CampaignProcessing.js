define(["sitecore"], function (Sitecore) {
    var model = Sitecore.Definitions.Models.ControlModel.extend({
        initialize: function (options) {
            this._super();

            //Read query string parameters and populate values
            var totalParticpants = Sitecore.Helpers.url.getQueryParameters(window.location.href)['TotalPartcipants'];
            var campaignIdString = Sitecore.Helpers.url.getQueryParameters(window.location.href)['CampaignIds'];
            var campaignListSortValue = Sitecore.Helpers.url.getQueryParameters(window.location.href)['CampaignListSortValue'];
            var campaignListSortOrder = Sitecore.Helpers.url.getQueryParameters(window.location.href)['CampaignListSortOrder'];
            var selectedPage = Sitecore.Helpers.url.getQueryParameters(window.location.href)['SelectedCampaignPage'];
            var connStringName = Sitecore.Helpers.url.getQueryParameters(window.location.href)['ConnStringName'];
            var campaignSearchTerm = Sitecore.Helpers.url.getQueryParameters(window.location.href)['CampaignSearchTerm'];

            //Show/hide information divs
            $(".info-div-before-campaignimport").show();
            $(".info-div-after-campaignimport").hide();

            if (totalParticpants == null || totalParticpants == undefined) {
                totalParticpants = 0;
            }

            if (campaignIdString == null || campaignIdString == undefined) {
                campaignIdString = "";
            }

            if (campaignListSortValue == null || campaignListSortValue == undefined) {
                campaignListSortValue = "";
            }

            if (campaignListSortOrder == null || campaignListSortOrder == undefined) {
                campaignListSortOrder = "";
            }

            if (selectedPage == null || selectedPage == undefined) {
                selectedPage = "";
            }

            if (connStringName == null || connStringName == undefined) {
                connStringName = "";
            }

            if (campaignSearchTerm == null || campaignSearchTerm == undefined) {
                campaignSearchTerm = "";
            }

            $(".createnewlist-radiobutton").prop("checked", true);

            //Set values to text fields and hidden fields
            $(".campaign-list-totalparticipants").text(totalParticpants);
            $(".hidden-totalparticpants-for-campaign").val(totalParticpants);
            $(".hidden-selected-campiagnIds").val(campaignIdString);
            $(".hidden-campaignlist-sortvalue").val(campaignListSortValue);
            $(".hidden-campaignlist-sortorder").val(campaignListSortOrder);
            $(".hidden-campaignlist-selectedpage").val(selectedPage);
            $(".hidden-conn-string-campaign-processing").val(connStringName);
            $(".hidden-campaignlist-searchterm").val(campaignSearchTerm);

            //Hide the 'Import button' if there are no campaigns selected
            if (campaignIdString == "" || !(campaignIdString.split(',').length > 0)) {
                $(".campaign-processingpage-Import").hide();
                $(".processing-success").html("");
                $(".processing-error").html("No campaigns selected. Please go back and select any campaign(s)");
            }

            //Empty the data list, text field and hidden fiel before fill with new data
            $(".ExistingSitecoreLists-Class").empty();
            $(".campaign-list-name").val("");
            $(".hidden-selected-sitecorelistid").val("");

            $.ajax({
                url: "/sitecore/api/ssc/FuseIT-S4S-SitecoreSalesforceListBuilder-Controllers/SalesforceCampaigns/1/GetExistingSitecoreLists",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var returnData = data;
                    if (returnData.ExistingSitecoreLists != null && returnData.ExistingSitecoreLists.length > 0) {
                        //Populate the dropdown and select predefined values
                        $.each(returnData.ExistingSitecoreLists, function (i, item) {
                            //Set the selected values if its available
                            $(".ExistingSitecoreLists-Class").append($("<option     />").val(item.SitecoreListName).attr({ "data-sitecorelistid": item.SitecoreListId, "data-sitecorelistdescription": item.SitecoreListDescription }));
                        });
                    }
                },
                error: function (jqXHr, textStatus, errorThrown) {
                }
            });

            //Event to catch change the value in autofill text box
            $(".campaign-list-name").on('input', function () {
                var selectedListName = $(this).val();
                var selectedItemFound = false;

                $(".ExistingSitecoreLists-Class").find("option").each(function () {
                    if ($(this).val() == selectedListName) {
                        $(".hidden-selected-sitecorelistid").val($(this).attr("data-sitecorelistid"));
                        $(".campaign-list-description").val($(this).attr("data-sitecorelistdescription"));
                        $(this).attr("selected", true);
                        selectedItemFound = true;
                    }
                    else {
                        $(this).attr("selected", false);
                    }
                });

                if (!selectedItemFound) {
                    $(".hidden-selected-sitecorelistid").val("");
                    $(".campaign-list-description").val("");
                }
            });

            //Click event for 'Back' button
            $(".campaign-processingpage-back").click(function () {
                var tempcampaignIdList = $(".hidden-selected-campiagnIds").val();
                var tempTotalParticipants = parseInt($(".hidden-totalparticpants-for-campaign").val()) || 0;
                var tempCampaignListSortValue = $(".hidden-campaignlist-sortvalue").val();
                var tempCampaignListSortOrder = $(".hidden-campaignlist-sortorder").val();
                var tempCampaignListSelectedPage = $(".hidden-campaignlist-selectedpage").val();
                var tempSelectedConnStringName = $(".hidden-conn-string-campaign-processing").val();
                var tempSelectedSerchTerm = $(".hidden-campaignlist-searchterm").val();
                window.location = '/sitecore/client/Applications/S4SLB/CampaignList?ConnStringName=' + tempSelectedConnStringName + '&CampaignIds=' + tempcampaignIdList + '&TotalCampaignPaticipants=' + tempTotalParticipants + '&SelectedCampaignListSortValue=' + tempCampaignListSortValue + '&SelectedCampaignListSortOrder=' + tempCampaignListSortOrder + '&SelectedCampaignPage=' + tempCampaignListSelectedPage + '&CampaignSearchTerm=' + tempSelectedSerchTerm;
            });

            //Click event for 'Import' button
            $(".campaign-processingpage-Import").click(function () {
                var tempcampaignIdList = $(".hidden-selected-campiagnIds").val();
                var tempCampaignListName = $(".campaign-list-name").val();
                var tempSelectedListId = $(".hidden-selected-sitecorelistid").val();
                var tempSelectedListMergeOption = $("input:radio[name='ListMergeOption']:checked").val();
                var tempTotalPartcipants = parseInt($(".hidden-totalparticpants-for-campaign").val());
                var tempCampiagnListDescription = $(".campaign-list-description").val();
                var tempSelectedConnStringName = $(".hidden-conn-string-campaign-processing").val();

                if (tempcampaignIdList != null && tempcampaignIdList != "" && tempCampaignListName != null && tempCampaignListName != "" && tempTotalPartcipants != null && tempTotalPartcipants > 0) {

                    var info = {
                        SelectedConnStringName: tempSelectedConnStringName, CampaignIdString: tempcampaignIdList, CustomListName: tempCampaignListName, CustomListDescription: tempCampiagnListDescription, SelectedListId: tempSelectedListId, SelectedListMergeOption: tempSelectedListMergeOption
                    };

                    $('.s4slb-loader-gif').show();
                    $.ajax({
                        //url: "/sitecore/api/ssc/FuseIT-S4S-SitecoreSalesforceListBuilder-Controllers/SalesforceCampaigns/1/ImportSaleforceCampaignsToSitecore",
                        url: "/sitecore/api/ssc/LionTrust-Feature-EXM-Controllers/SalesforceCampaigns/1/ImportSaleforceCampaignsToSitecore",
                        type: "POST",
                        data: JSON.stringify(info),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            $('.s4slb-loader-gif').hide();
                            var returnData = data;

                            if (!returnData.ShowProcessButton) {
                                $(".campaign-processingpage-Import").hide();
                            }

                            if (returnData.IsSuccess) {
                                $(".processing-error").html("");
                                $(".processing-success").html(returnData.ReturnMessage);

                                //Toggle information divs
                                $(".info-div-before-campaignimport").hide();
                                $(".info-div-after-campaignimport").show();
                            }
                            else {
                                $(".processing-success").html("");
                                $(".processing-error").html(returnData.ReturnMessage);

                                //Toggle information divs
                                $(".info-div-before-campaignimport").show();
                                $(".info-div-after-campaignimport").hide();
                            }
                        },
                        error: function (jqXHr, textStatus, errorThrown) {
                            $('.s4slb-loader-gif').hide();
                            $(".campaign-processingpage-Import").hide();
                            $(".processing-success").html("");
                            $(".processing-error").html("Exception thrown while processing. Check logs for more information");

                            //Toggle information divs
                            $(".info-div-before-campaignimport").show();
                            $(".info-div-after-campaignimport").hide();
                        }
                    });
                }
                else {
                    if (tempcampaignIdList == null || tempcampaignIdList == "") {
                        alert("Cannot proceed. Please try again by selecting at leaset one Salesforce campaign");
                    }
                    else if (tempCampaignListName == null || tempCampaignListName == "") {
                        alert("Cannot proceed. Please fill the List Name field");
                    }
                    else if (tempTotalPartcipants == null || !(tempTotalPartcipants > 0)) {
                        alert("Cannot proceed. Please try again by selecting Salesforce campaigns(s) containing Leads or Contacts");
                    }
                    else {
                        alert("Cannot proceed. Please try again");
                    }
                }
            });
        }
    });

    var view = Sitecore.Definitions.Views.ControlView.extend({
        initialize: function (options) {
            this._super();
        }
    });

    Sitecore.Factories.createComponent("CampaignProcessing", model, view, ".sc-CampaignProcessing");
});
