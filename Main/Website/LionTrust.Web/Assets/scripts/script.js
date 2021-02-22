/*
 * ---------------------------------------- *
 * Name: 	Primary JavaScripts             *
 * Type: 	JavaScript                      *
 * Project: LionTrust                       *
 * Version: Not Versioned                   *
 * Author:	Matt O'Neill                    *
 * Requisites: chg.Responsive               *
 *             chg.Client                   *
 *             serialise.js 1.1.0           *
 * ---------------------------------------- *
 */

var gMaps = gMaps || {}, // globally accessible maps
    geocoder,
    s,
    FundInfo,
    fundListing;

(function ($) {

    var $html = $('html'),
        $body = $('body'),
        $window = $(window),
        actions,
        pageHasScrollOverride = false,
        DESKTOP_WIDTH = 1050,
        TABLET_WIDTH = 900;

    $html.removeClass('no-js');

    // call plugins
    client.domApply();

    var cookie = new Cookie();

    /* primary functions
       ---------------------------- */

    // hide cookie module on click
    function cookieHide() {
        $('.cookie .close').on('click', function () {
            var cookieUrl = $(this).data('url');
            $.get(url = cookieUrl);
            $(this).parents('.cookie').slideUp(400);
        });
    }

    function applyFilters() {
        $('.filtering select').each(function () {
            $(this).change(function () {
                var url = $(this).find('option:selected').data('url');
                if (url != undefined && url.length != 0) {
                    window.location = url;
                }
            });
        });
    }

    function roleSwitch() {
        var $select = $('.role-switcher'),
            $styledDropdown = $('.dropdown'),
            $styledUl = $('.dropdown ul'),
            $styledLis = $styledUl.children(),
            selectedOption = $select.find('option:selected').html(),
            //cookie = new Cookie(),
            isOpen = false,
            selectedOptionUrl,
            selectedOptionRole,
            $thisDropdown;

        // click on content to display style ul instead of select
        $styledDropdown.find('.content').on('click', function (e) { // todo on resize
            $thisDropdown = $(this);
            e.stopPropagation();
            if ($thisDropdown.hasClass('open')) {
                closeDropdown($thisDropdown);
                isOpen = false;
            } else {
                $thisDropdown.addClass('open');
                $thisDropdown.siblings('ul').fadeIn(400);
                $thisDropdown.siblings('.arrow').fadeIn(400);
                isOpen = true;
            }
        });
        SetVisitorRole(GetVisitor(true));
        $select.val(GetVisitor(true));

        if (GetUndetermined(false) != "undefined" && GetUndetermined(false)) {
            $styledDropdown.find('.content').append('Professional Investor');
        } else {
            selectedOption = $select.find('option:selected').html();
            $styledDropdown.find('.content').append(selectedOption);
        }

        function closeDropdown() {
            $thisDropdown.removeClass('open');
            $thisDropdown.siblings('ul').fadeOut(400);
            $thisDropdown.siblings('.arrow').fadeOut(400);
        }

        // load page based on role
        function switchLoad() {

            if (selectedOptionUrl !== '') {
                SetUndeterminedRole(selectedOptionRole);
                location.href = selectedOptionUrl;
            } else {
                SetVisitorRole(selectedOptionRole);
                SetUndeterminedRole(undefined);
                if (location.href.split('/').indexOf('register-professional-investor') > 0) {
                    location.href = '/register-private-investor';
                }
                else {
                    location.href = GetOriginalURL(true);
                }
            }
        }

        // switch roles mobile
        $select.on('change', function () {
            selectedOptionUrl = $(this).children(':selected').data('url');
            selectedOptionRole = $(this).children(':selected').data('role');

            var roleCookie = GetVisitor(false);

            if (roleCookie !== selectedOptionRole || typeof roleCookie !== 'undefined') {
                switchLoad();
            }
        });

        // switch roles desktop
        $styledLis.on('click', function () {
            selectedOptionUrl = $(this).data('url');
            selectedOptionRole = $(this).data('role');

            var roleCookie = GetVisitor(false);

            if (roleCookie !== selectedOptionRole || typeof roleCookie !== 'undefined') {
                closeDropdown();
                switchLoad();
            }
        });

        $body.on('click', function (e) {
            if (isOpen) {
                closeDropdown();
            }
        });
    }

    function GetOriginalURL(fallback) {
        var originalURL = cookie.check('OriginalURL');
        if (originalURL != 'undefined' && originalURL) {
            return originalURL;
        } else if (fallback) {
            return '/';
        } else {
            return '';
        }
    }

    function GetConfirmURL(fallback) {
        var confirmURL = cookie.check('ConfirmURL');
        if (confirmURL != 'undefined' && confirmURL) {
            return confirmURL;
        } else if (fallback) {
            return '/';
        } else {
            return '';
        }
    }

    function GetUndetermined(fallback) {

        var undeterminedRole = cookie.check('UndeterminedRole');

        if (undeterminedRole != 'undefined' && undeterminedRole) {
            return undeterminedRole;
        } else if (fallback || location.href.split('/').indexOf('confirm-role') > 0) {
            return '84AB6F41-E811-4545-AAF9-018F97CE7E83';
        } else {
            return '';
        }
    }

    function GetVisitor(fallback) {
        var visitorRole = cookie.check('VisitorRole');

        if (visitorRole != 'undefined' && visitorRole) {
            return visitorRole;
        } else if (fallback) {
            return '9B8F4782-739B-4B96-BFAD-0DA021609B25';
        } else {
            return '';
        }
    }

    function SetUndeterminedRole(role) {

        cookie.write('UndeterminedRole', role, 365);
    }

    function SetVisitorRole(role) {
        cookie.write('VisitorRole', role, 365);
    }

    function SetDropdown(role) {
        $select.val(role);
    }


    // update cookie
    function roleConfirmation() {

        $('.confirm-role').on('click', function () {
            SetVisitorRole(GetUndetermined(true));
            SetUndeterminedRole(undefined);
            SetDropdown(GetVisitor(true));

        });

        $('.decline-role').on('click', function () {
            SetUndeterminedRole(undefined);
            SetVisitorRole(undefined);
            SetVisitorRole(GetVisitor(true));
        });
    }

    function EditEmailPreferences() {
        //Iterate through all process items in the repeater
        $(".div-process-item").each(function () {
            var processSection = $(this);
            var fundSection = processSection.find(".div-fund-rpt");
            var collpseIcon = processSection.find(".rpt-process-panel-icon");

            if (fundSection != null && fundSection.length > 0 && collpseIcon != null && collpseIcon.length > 0) {
                //Check if there are any checkboxes in the fund repeater
                var fundItemCheckboxs = $(fundSection[0]).find(".chk-fund");
                if (fundItemCheckboxs != null && fundItemCheckboxs.length > 0) {
                    //If there are any selected checkboxes in the fund list, show extra information tool tip span
                    var checkedfundItems = $(fundSection[0]).find(".chk-fund:checked");
                    var showIndicator = false;
                    if (checkedfundItems != null & checkedfundItems.length > 0) {
                        showIndicator = true;
                    }

                    if (showIndicator) {
                        //Show fund section if there are any fund items in the repeater
                        $(fundSection[0]).show();
                        //Remove essential css class to span which shows + and -
                        $(collpseIcon[0]).removeClass("rpt-process-panel-icon-collapse");
                        $(collpseIcon[0]).text("-");
                    }
                    else {
                        //Hide fund section if there are any fund items in the repeater
                        $(fundSection[0]).hide();
                        //Set essential css class to span which shows + and -
                        $(collpseIcon[0]).addClass("rpt-process-panel-icon-collapse");
                        $(collpseIcon[0]).text("+");
                    }
                }
                else {
                    //Hide span which contans + and -
                    $(collpseIcon[0]).addClass("fund-span-hide");
                }
            }
        });

        //Change select all checkbox
        $(".chk-global").change(function () {
            var status = this.checked;

            $(".checkbox-common").each(function () {
                this.checked = status;
            });

            if (status) {
                $(".chk-unsubscribe").prop('checked', false);
            }
        });


        //Change unsubscribe checkbox
        $(".chk-unsubscribe").change(function () {
            var status = this.checked;
            //If unsubscrive checkbox is checked, uncheck all the fund checkboxes
            if (status) {
                $(".checkbox-common").each(function () {
                    this.checked = false;
                });

                $(".chk-global").prop('checked', false);
                $(".chk-lt-news").prop('checked', false);
                $(".chk-lt-InstitutionalBulletin").prop('checked', false);
            }
        });

        //Change news checkbox
        $(".chk-lt-news").change(function () {
            var status = this.checked;
            //If news checkbox is checked, uncheck unsubscribe checkbox
            if (status) {
                $(".chk-unsubscribe").prop('checked', false);
            }
        });

        //Change Institution Bulleting checkbox
        $(".chk-lt-InstitutionalBulletin").change(function () {
            var status = this.checked;
            //If Institution Bulleting checkbox is checked, uncheck unsubscribe checkbox
            if (status) {
                $(".chk-unsubscribe").prop('checked', false);
            }
        });

        //Change event for process level checkboxes
        $(".chk-process").change(function () {
            var status = this.checked;
            //Get parent process div
            var currentProcessDiv = $(this).closest('.div-process-item');
            if (currentProcessDiv != null) {
                currentProcessDiv.find(".chk-fund").each(function () {
                    this.checked = status;
                });
            }

            if ($(".checkbox-common:checked").length == $(".checkbox-common").length) {
                $(".chk-global").prop('checked', true);
            }
            else {
                $(".chk-global").prop('checked', false);
            }

            //uncheck unsubscribe checkbox if the status of the current checkbox is checked
            if (status) {
                $(".chk-unsubscribe").prop('checked', false);
            }
        });

        //Change event for fund level checkboxes
        $('.chk-fund').change(function () {
            var status = this.checked;
            //Get parent process div
            var currentProcessDiv = $(this).closest('.div-process-item');
            if (currentProcessDiv != null) {
                if (status == false) {
                    currentProcessDiv.find(".chk-process")[0].checked = false;
                }
                else {
                    if (currentProcessDiv.find(".chk-fund:checked").length == currentProcessDiv.find(".chk-fund").length) {
                        currentProcessDiv.find(".chk-process")[0].checked = true;
                    }
                }
            }

            if ($(".checkbox-common:checked").length == $(".checkbox-common").length) {
                $(".chk-global").prop('checked', true);
            }
            else {
                $(".chk-global").prop('checked', false);
            }

            //uncheck unsubscribe checkbox if the status of the current checkbox is checked
            if (status) {
                $(".chk-unsubscribe").prop('checked', false);
            }
        });

        //Click event of the span which contains "+" and "-"
        $('.rpt-process-panel-icon').click(function () {
            var spanSection = $(this);
            //Get parent process div
            var currentProcessDiv = spanSection.closest('.div-process-item');
            if (currentProcessDiv != null) {
                //Show/hide fund section and toggle "+" "-" icons
                var fundDiv = currentProcessDiv.find(".div-fund-rpt");
                if (fundDiv != null && fundDiv.length > 0) {
                    if (spanSection.hasClass("rpt-process-panel-icon-collapse")) {
                        $(fundDiv[0]).show();
                        spanSection.removeClass("rpt-process-panel-icon-collapse");
                        spanSection.text("-");
                    }
                    else {
                        $(fundDiv[0]).hide();
                        spanSection.addClass("rpt-process-panel-icon-collapse");
                        spanSection.text("+");
                    }
                }
            }
        });
    }

    // scroll top top on long pages
    function pageScroll() {
        var $backToTop = $('.back-to-top');
        $(window).scroll(function () {
            if ($(this).scrollTop() > 500) {
                $backToTop.fadeIn();
            } else {
                $backToTop.fadeOut();
            }
        });
        $backToTop.on('click', function () {
            $html.add($body).animate({ scrollTop: 0 }, 800);
        });
    }

    // for responsive tables but not tables on filter pages
    function onScrollGradient() {
        var $tableContainer = $(this),
            $table = $tableContainer.find('table:not(".fixed-column")');

        if ($table.length) {
            $tableContainer.on('scroll', function () {
                var maxScroll = ($table.length) ? $table.width() - $tableContainer.width() : $tableContainer.find('img').width() - $tableContainer.width();

                if ($tableContainer.scrollLeft() >= maxScroll - 10) {
                    $tableContainer.find('.gradient').fadeOut(400);
                } else {
                    $tableContainer.find('.gradient').fadeIn(400);
                }
            });
        }
    }

    function fixedColumn() {
        var $thisTableWrapper = $(this),
            $parentTab = $thisTableWrapper.parents('.tab-content'),
            parentTabClasses,
            $fixedColumn,
            $fixedColumnWidth,
            $fixedColumnCells;

        // store class for tabs
        if ($parentTab.length) {
            parentTabClasses = $parentTab.attr('class');
            $parentTab.addClass('active');
        }

        // set width
        $fixedColumn = $thisTableWrapper.find('table.fixed-column');
        $fixedColumnWidth = $thisTableWrapper.find('table:first-child thead tr th:first-child').outerWidth() + 2; // +2 for border
        $fixedColumnCells = $fixedColumn.find('th, td');

        $fixedColumnCells.css('width', $fixedColumnWidth + 'px');
        $fixedColumn.width($fixedColumnCells.outerWidth());

        // set height
        $fixedColumnCells.each(function (i) {
            $(this).css('height', $thisTableWrapper.find('table:first-child tr').eq(i).children().eq(2).innerHeight() + 'px');
        });

        // set classes for tabs
        if ($parentTab.length) {
            $parentTab.removeClass();
            $parentTab.addClass(parentTabClasses);
        }
    }

    function navigation() {
        var $nav = $('#primary-nav'),
            $firstLevelLis = $nav.find('.wrapper > ul > li'),
            fadeSpeed = client.Mobile ? 0 : 250,
            $searchButton = $nav.find('.search'),
            $searchBox = $searchButton.find('.search-box');

        function closeSearch() {
            $searchBox.stop().fadeOut(fadeSpeed);
            $searchButton.removeClass('active');
        }

        //click to display search box
        function searchSection() {
            var $searchInput = $searchBox.children('input[type="text"]');
            var $seachBoxCloseOpenIcon = $searchButton.find('span.icon');
            //$searchButton.on('click', function () {
            $seachBoxCloseOpenIcon.on('click', function () {
                if ($(window).width() <= 740) { // ensure fade doesn't happen on mobile width
                    return;
                }
                if ($searchButton.hasClass('active')) {
                    closeSearch();
                } else {
                    $searchButton.addClass('active');
                    $firstLevelLis.removeClass('active').find('.second-level').stop().fadeOut(fadeSpeed);
                    $searchBox.stop().fadeIn(fadeSpeed, function () {
                        $searchInput.focus();
                    });
                }
            });

            $searchBox.find('input').on({
                click: function (e) {
                    e.stopPropagation();
                }
            });
        }

        // mega nav hovering events for desktop
        function megaNav() {
            var hoverOffTimer,
                hoverOverTimer,
                $trigger = client.Mobile ? $nav.find('.arrow') : $firstLevelLis,
                timerSpeed = client.Mobile ? 0 : 150,
                openEvent = client.Mobile ? 'click' : 'mouseenter';

            $trigger.on(openEvent, function (e) {
                var $li = client.Mobile ? $(this).parent('li') : $(this);

                hoverOverTimer = setTimeout(function () {
                    timerSpeed = 0;
                    closeSearch();

                    // if on desktop, or on mobile device at desktop width, use fade transitions
                    if ((client.Mobile && $(window).width() > 740) || !client.Mobile) {
                        $firstLevelLis.not($li).find('.second-level').stop().fadeOut(fadeSpeed);
                        $li.find('.second-level').stop().fadeIn(fadeSpeed);
                    } else { // if on mobile device, at mobile width use slide transitions
                        if (!$li.hasClass('expanded')) {
                            $firstLevelLis.not($li).removeClass('expanded').find('.second-level').stop().slideUp();
                            $li.addClass('expanded').find('.second-level').stop().slideDown();
                        } else {
                            $li.removeClass('expanded').find('.second-level').stop().slideUp();
                        }

                        // animate scroll position to match parent ul due to length of navigation items
                        if (pageHasScrollOverride) {
                            $nav.animate({ scrollTop: $firstLevelLis.first().position().top }, 400);
                        } else {
                            $html.add($body).animate({ scrollTop: $firstLevelLis.first().offset().top }, 400);
                        }
                    }

                    // clear timeout if it's been set
                    if (hoverOffTimer) {
                        clearTimeout(hoverOffTimer);
                    }
                }, timerSpeed);
            });

            // if on desktop, bind mouseleave to set timeout
            if (!client.Mobile) {
                $firstLevelLis.on('mouseleave', function () {
                    var $li = $(this);
                    if (hoverOverTimer) {
                        clearTimeout(hoverOverTimer);
                    }
                    hoverOffTimer = setTimeout(function () {
                        $li.find('.second-level').fadeOut(400);
                        timerSpeed = 150;
                    }, $(window).width() > 740 ? '250' : '0');
                });
            }

            // close nav when clicking off of it on mobile
            if (client.Mobile && !client.iOS) {
                $(document).on('click', function (e) {
                    if ($(window).width() > 740 && !$nav.has($(e.target)).length) {
                        $firstLevelLis.find('.second-level').stop().fadeOut(fadeSpeed);
                    }
                });
            }
        }

        // mega nav click event for mobile
        function navResponsive() {
            var $siteHeader = $('#site-header'),
                $hamburguerButton = $('#mob-menu-opener'),
                bottomMarkup = $('<li class="bottom-links cf"></li>').append($siteHeader.find('.quick-links').clone(true)).append($siteHeader.find('.contact-links').clone(true)),
                isAnimating = false;

            $hamburguerButton.on('click', function (e) {
                if (!isAnimating) {
                    isAnimating = true;
                    $nav.slideToggle(400, function () {
                        isAnimating = false;
                    });
                    $hamburguerButton.toggleClass('active');
                }
            });

            $nav.find('ul:first').append(bottomMarkup);
        }

        searchSection();
        megaNav();
        navResponsive();
    }

    // google maps
    gMaps = {
        maps: [],
        init: function () {

            geocoder = new google.maps.Geocoder();

            var settings = {
                // universal map settings
                zoom: 16,
                disableDefaultUI: true,
                scrollwheel: false,
                draggable: !client.Mobile
            };

            $('.gmap').each(function () {

                // get coordinates
                var $thisMap = $(this),
                    lat,
                    lng,
                    address = $thisMap.data('address');

                // check for coords if not use address to get coords
                if (!$thisMap.data('coordinate').length == 0) {

                    lat = $thisMap.data('coordinate').split(',')[0];
                    lng = $thisMap.data('coordinate').split(',')[1];
                    mapCall();

                } else {
                    var myLatLong = new google.maps.Geocoder();

                    myLatLong.geocode({ 'address': address }, function (results, status) {
                        lat = results[0].geometry.location.lat();
                        lng = results[0].geometry.location.lng();

                        mapCall();
                    });
                }

                function mapCall() {

                    var map = new google.maps.Map($thisMap[0], settings),
                        position = new google.maps.LatLng(lat, lng);

                    var infowindow = new google.maps.InfoWindow({
                        content: address.split(',').join(', ')
                    });

                    var marker = new google.maps.Marker({
                        position: position,
                        map: map
                    });

                    google.maps.event.addListener(marker, 'click', function () {
                        infowindow.open(map, marker);
                    });

                    map.setOptions({
                        center: position
                    });
                    gMaps.maps.push(map);

                    var buttonLink = 'https://www.google.com/maps/place/' + address + '/';
                    $thisMap.siblings('.button').attr('href', buttonLink);
                }
            });
        },

        load: function () {

            var script = document.createElement('script');
            script.type = 'text/javascript';
            script.src = 'https://maps.googleapis.com/maps/api/js?key=AIzaSyDbR4JmLmwrtJhnIlkHkHzWgoJAwYN8aXc' +
                '&callback=gMaps.init';
            document.body.appendChild(script);
        }
    };

    // client specific styling for selects
    function clientStyling() {
        var $select = $('.select'),
            $droplist = $('.scfDropListGeneralPanel');
        if (client.Safari) {
            $select.add($droplist).addClass('safari');
        } else if (client.Firefox) {
            $select.add($droplist).addClass('firefox');
        }
    }

    // add arrows to selects in forms
    function formStyling() {
        var $form = $('.scfForm'),
            $select = $form.find('select'),
            $mandatory = $form.find('.scfRequired, .scfValidatorRequired'),
            $validation = $form.find('.scfValidator'),
            $checkbox = $form.find('.scfCheckbox'),
            $checkboxLabel = $checkbox.find('label'),
            $radioList = $form.find('.scfRadioButtonList'),
            $radioButtonRow = $radioList.find('td'),
            $radioList1ContainerForLondonPage = $form.find('.scLondonFormRadioButtonList1 .scfRadioButtonList'),
            $radioList2ContainerForLondonPage = $form.find('.scLondonFormRadioButtonList2 .scfRadioButtonList'),
            $radioList2WrapperForLondonPage = $form.find('.scLondonFormRadioButtonList2'),
            $multiTextContainerForLondonPage = $form.find('.scLondonFormMultipleLineTextBorder');

        $radioButtonRow.each(function () {
            $(this).prepend('<span class=""><span class="ico"></span></span>');
        });

        $radioList.each(function () {
            var $thisRadioList = $(this);
            $thisRadioList.find('input[type=radio]').change(function () {
                $thisRadioList.find('input[type=radio]').siblings('span').removeClass('checked');
                $(this).siblings('span').toggleClass('checked');
            });
        });

        //This section will only exceute if 'scLondonFormRadioButtonList1' and 'scLondonFormRadioButtonList2' css styles applied to the WFFM form fields.
        //Specific to the '2018-aic-london' WFFM form which is showing in https://www.liontrust.co.uk/london page.
        if ($radioList1ContainerForLondonPage.length > 0 && $radioList2ContainerForLondonPage.length > 0 && $radioList2WrapperForLondonPage.length > 0 && $multiTextContainerForLondonPage.length > 0) {

            var $tempRadioList1ControlerForLondonPage = $radioList1ContainerForLondonPage.find('input[type=radio]');
            $tempRadioList1ControlerForLondonPage.each(function () {
                if ($(this).is(':checked')) {
                    $(this).siblings('span').addClass('checked');
                }
            });

            var $tempRadioList1CheckedForLondonPage = $radioList1ContainerForLondonPage.find('input[type=radio]:checked')
            if ($tempRadioList1CheckedForLondonPage.length > 0 && ($tempRadioList1CheckedForLondonPage).val() == "Yes") {
                $radioList2WrapperForLondonPage.show();

                var $tempRadioList2ControlerForLondonPage = $radioList2ContainerForLondonPage.find('input[type=radio]');
                $tempRadioList2ControlerForLondonPage.each(function () {
                    if ($(this).is(':checked')) {
                        $(this).siblings('span').addClass('checked');
                    }
                });

                var $tempRadioList2CheckedForLondonPage = $radioList2ContainerForLondonPage.find('input[type=radio]:checked');
                if ($tempRadioList2CheckedForLondonPage.length > 0 && ($tempRadioList2CheckedForLondonPage).val() == "Yes") {
                    $multiTextContainerForLondonPage.show();
                }
                else {
                    var $multiTextbocForLondonPage = $multiTextContainerForLondonPage.find('.scfMultipleLineTextBox');
                    if ($multiTextbocForLondonPage != null) {
                        $multiTextbocForLondonPage.val("");
                    }
                    $multiTextContainerForLondonPage.hide();
                }
            }
            else {

                var $tempMultiTextboxForLondonPage = $multiTextContainerForLondonPage.find('.scfMultipleLineTextBox');
                if ($tempMultiTextboxForLondonPage != null) {
                    $tempMultiTextboxForLondonPage.val("");
                }

                var $tempRadioList2ControlerForLondonPage = $radioList2ContainerForLondonPage.find('input[type=radio]');
                $tempRadioList2ControlerForLondonPage.each(function () {
                    $(this).removeAttr('checked');
                    $(this).siblings('span').removeClass('checked');
                });

                $radioList2WrapperForLondonPage.hide();
                $multiTextContainerForLondonPage.hide();
            }

            //OnChange event for raio button list 2
            $radioList2ContainerForLondonPage.find('input[type=radio]').change(function () {
                if ($(this).val() == "Yes") {
                    $multiTextContainerForLondonPage.show();
                }
                else {
                    var $multiTextbocForLondonPage = $multiTextContainerForLondonPage.find('.scfMultipleLineTextBox');
                    if ($multiTextbocForLondonPage != null) {
                        $multiTextbocForLondonPage.val("");
                    }
                    $multiTextContainerForLondonPage.hide();
                }
            });

            //OnChange event for raio button list 1
            $radioList1ContainerForLondonPage.find('input[type=radio]').change(function () {
                if ($(this).val() == "Yes") {
                    $radioList2WrapperForLondonPage.show()
                }
                else {
                    var $multiTextboxForLondonPage = $multiTextContainerForLondonPage.find('.scfMultipleLineTextBox');
                    if ($multiTextboxForLondonPage != null) {
                        $multiTextboxForLondonPage.val("");
                    }

                    var $radioListControlerForLondonPage = $radioList2ContainerForLondonPage.find('input[type=radio]');
                    $radioListControlerForLondonPage.each(function () {
                        $(this).removeAttr('checked');
                        $(this).siblings('span').removeClass('checked');
                    });

                    $multiTextContainerForLondonPage.hide();
                    $radioList2WrapperForLondonPage.hide();
                }
            });

            $form.find('.scfSubmitButton').click(function () {
                if ($radioList2WrapperForLondonPage.is(":visible") && !(($radioList2ContainerForLondonPage.find('input[type=radio]:checked')).length > 0)) {
                    confirm("Dietary requirements Y/N is a required field");
                    return false;
                }

                if ($multiTextContainerForLondonPage.is(":visible")) {
                    var $multiTextboxForLondonPage = $multiTextContainerForLondonPage.find('.scfMultipleLineTextBox');
                    if ($multiTextboxForLondonPage != null && $multiTextboxForLondonPage.val() == "") {
                        confirm("Please enter your Dietary requirements");
                        return false;
                    }
                }
            });
        }

        $select.each(function () {
            $(this).wrap('<div class="select-wrap"></div>');
            $(this).after('<span class="arrow"></span>');
        });

        $mandatory.each(function () {
            $(this).appendTo($(this).parent().find('label'));
        });

        $validation.append('<span class="arrow"></span>');

        // prepend the appropriate span with classes to the checkboxes
        $checkbox.each(function () {
            var $thisCheckbox = $(this);

            $thisCheckbox.prepend("<span class='square checkbox'><svg x='0px' y='0px' width='15px' height='13px' viewBox='0 0 15 13' enable-background='new 0 0 15 13' xml:space='preserve'><path fill='#34B233' stroke='#FFFFFF' stroke-width='1' stroke-miterlimit='10' d='M14.613 0C10.051 2.81 6.74 6.355 5.252 8.135L1.609 5.268 0 6.57 6.295 13C7.379 10.213 10.811 4.766 15 0.895L14.613 0z'/></svg></span>");
            if ($thisCheckbox.children('input').is(':checked')) {
                $thisCheckbox.find('.checkbox').addClass('checked');
            }
        });

        $checkboxLabel.on('click', function () {
            var $thisLabel = $(this);

            $thisLabel.siblings('.checkbox').toggleClass('checked');
        });

        $checkbox.find('.square').on('click', function () {

            var $thisCheckbox = $(this),
                $thisCheckboxInput = $thisCheckbox.siblings('input');

            $thisCheckbox.toggleClass('checked');
            $thisCheckboxInput.prop('checked', !$thisCheckboxInput.prop('checked'));

        });

        $form.find('.scfEmailUsefulInfo').each(function () {
            $(this).appendTo($(this).parent());
        });
    }

    // form placeholder styling
    function emptyValue() {
        var $inputs = $('input[type="text"], input[type="email"], input[type="password"], textarea'),
            $input,
            $thisVal;

        if (client.OldIE || client.IE9) {
            $inputs.on({
                focus: function () {
                    $input = $(this),
                        $thisVal = $input.val();

                    if ($input.val() === $thisVal) {
                        $input.val('');
                        $input.css('color', '#000');
                    }
                },
                blur: function () {
                    $input = $(this);
                    if ($input.val().length === 0) {
                        $input.val($thisVal);
                        $input.css('color', '#8e8e8e');
                    }
                }
            });
        } else {
            $inputs.each(function () {
                var placeholder = $(this).val();
                $(this).attr('placeholder', placeholder).removeAttr('value');
            });
        }
    }

    function filterAssets() {

        var $table = $('#fund-literature-table'),
            $tableContainer = $table.parents('.filter-listing'),
            $downloadButton = $table.parents('.table').find('.download'),
            checkAll = false,
            itemCollection = [];

        // download selected items from assets table
        function downloadAssets() {
            var $assetsTable = $('.table.assets');

            $assetsTable.find('.download').on('click', function () {
                var items = [];

                for (var value = 0; value < itemCollection.length; value += 1) {
                    items.push('{' + itemCollection[value] + '}|');
                }

                if (checkAll) {
                    items.push("&all=true&template=" + ($tableContainer.hasClass('document-filter-listing') ? 'generaldoc' : 'funddoc'));
                }

                window.location.href = $assetsTable.find('.download').data('url') + items.join('');
            });
        }

        // toggle download btn
        function downloadButton() {
            if ($table.find('tbody .checked').length > 0) {
                $downloadButton.addClass('active');
            } else {
                $downloadButton.removeClass('active');
            }
        }

        // adding/removing download ids
        function downloadCollection(checked, id) {
            if (checked) {
                itemCollection.push(id);
            } else if (itemCollection.indexOf(id) > -1) {
                itemCollection.splice(itemCollection.indexOf(id), 1);
            }
        }

        // flush array
        function flushArr() {
            itemCollection = [];
        }

        // check checked items individually
        function singleItemCheck() {
            for (var value = 0; value < itemCollection.length; value += 1) {
                var $checkedItem = $table.find('[data-associated-item=' + itemCollection[value] + ']'),
                    $container = $checkedItem.parents('tr');

                $container.addClass('active');
                $container.find('input, span').addClass('checked');
            }
        }

        // check items on pagination change
        $('.product-literature').on('resultsUpdated', function () {
            if ($('#checkbox-all').hasClass('checked')) {
                checkAll = false;

                $table.find('.checkbox').removeClass('checked').parents('tr').removeClass('active');
                $table.find('input[type=checkbox]').removeClass('checked');
                flushArr();
                downloadButton();
            } else {
                singleItemCheck();
            }
        });

        // show/hide download button & font-weight change row
        $table.on('change', 'input[type="checkbox"]', function () {

            var $thisCheckbox = $(this),
                dataId = $thisCheckbox.siblings().find('.checkbox').data('associated-item');

            $thisCheckbox.toggleClass('checked').next('label').children('span').toggleClass('checked');

            if ($thisCheckbox.hasClass('checked')) {
                $thisCheckbox.parents('tr').addClass('active');

                if (!$thisCheckbox.is('#checkbox-all')) {
                    checkAll ? downloadCollection(false, dataId) : downloadCollection(true, dataId);
                } else {
                    flushArr();
                    checkAll = true;

                    $table.find('tbody .checkbox').addClass('checked').parents('tr').addClass('active');
                    $table.find('tbody tr input[type=checkbox]').addClass('checked');

                    $table.find('input[type="checkbox"]').not('#checkbox-all').each(function () {
                        dataId = $(this).siblings().find('.checkbox').data('associated-item');
                        downloadCollection(true, dataId);
                    });
                }
            } else {
                $thisCheckbox.parents('tr').removeClass('active');

                if (!$thisCheckbox.is('#checkbox-all')) {
                    downloadCollection(false, dataId);

                    if (checkAll) {
                        downloadCollection(true, dataId);
                    }
                } else {
                    checkAll = false;

                    $table.find('tbody .checkbox').removeClass('checked').parents('tr').removeClass('active');
                    $table.find('tr input[type=checkbox]').removeClass('checked');
                    flushArr();
                }
                downloadCollection(false, dataId);
            }

            // toggle download btn
            downloadButton();
        });

        downloadAssets();
    }

    function tabNavigation() {

        var $table = $('.nav-tabs'),
            $tabNav = $table.find('li'),
            $tabSelect = $('.tabs > .select select'),
            cHandler = new Cookie(),
            maxHeight = 0;

        // change active tab
        function switchContent(number) {
            var $thisTab = $tabNav.parent().find('[data-number=' + number + "]"),
                $thisTabContent = $('#tab-' + number);

            $thisTabContent.parent().find('.tab-content').not($thisTabContent).removeClass('active');
            $thisTabContent.addClass('active');
            $thisTab.parent().find('li').not($thisTab).removeClass('active');
            $thisTab.addClass('active');
        }

        function checkHash() {
            if (window.location.hash) {
                var hashNumber = window.location.hash.split("=")[1];
                switchContent(hashNumber);
            }
        }

        function updateHash(number) {
            // hashchange not supported in IE7, manually invoke change
            if (!client.IE7) {
                window.location.hash = "tab=" + number;
                return;
            }
            switchContent(number);
        }

        //height/width for IE7
        if (client.IE7) {
            var tabWidth = 0;

            $tabNav.each(function () {
                tabWidth += $(this).outerWidth(true);
            });

            var availableSpace = $table.width() - tabWidth,
                padding = Math.floor((availableSpace / $tabNav.length) / 2) - 1;

            $tabNav.children().css({ paddingLeft: padding + 'px', paddingRight: padding + 'px' });

            $tabNav.each(function () {
                var tabHeight = $(this).height();
                maxHeight = tabHeight > maxHeight ? tabHeight : maxHeight;
                $(this).css('height', maxHeight + 'px');
            });
        }

        // events

        // filter submit
        //$('.button.flsu').click(function (event) {
        //    event.preventDefault();
        //    if (window.location.hash) {
        //        cHandler.write('tabC', encodeURIComponent(window.location.hash));
        //    }
        //});

        //hide and show tabs
        $tabNav.on('click', function () {
            var $thisTab = $(this),
                number = $thisTab.data('number');

            updateHash(number);
        });

        // select - replace tabs on mobile
        $tabSelect.on('change', function () {
            var $thisOption = $(this).find('option:selected'),
                number = $thisOption.data('number');

            updateHash(number);
        });

        // hash change
        $(window).on('hashchange', function () {
            checkHash();
        });

        // check cookie for previously open tab
        var cookieValue = cHandler.check('tabC');
        if (typeof cookieValue !== 'undefined') {
            window.location.hash = decodeURIComponent(cookieValue);
            cHandler.erase('tabC');
        }

        checkHash(); // load
    }

    function submitFormsWhenHitEnter() {

        var $activeForm;

        // set active form on focus
        $('.search-box').each(function () {
            $(this).find('input[type=text]').on({
                focus: function () {
                    $activeForm = $(this).offsetParent('.search-box');
                }
            });
        });

        // enter keypress search submit
        $(document).on({
            keypress: function (e) {
                var keycode = (e.keyCode ? e.keyCode : e.which);
                if (keycode == 13) {
                    if (!$activeForm.find('input[type=text]').val() == '') {
                        $activeForm.find('input[type=submit]').click();
                        e.preventDefault();
                    } else {
                        return false;
                    }
                }
            },
            submit: function () {
                if ($activeForm.find('input[type=text]').val() == '') {
                    return false;
                }
            }
        });

    }

    function expandContent() {
        $(this).on('click', function () {
            $(this).fadeOut(400, function () {
                $(this).parent().siblings('.show-more').slideDown(400);
            });
        });
    }

    function filterInvestments() {

        function fundModule() {

            var $investSection = $('.how-to-invest.fund-module'),
                $check = $investSection.find('.filter-fund .square.checkbox'),
                $sectionNav = $investSection.find('.section-nav'),
                $navItem = $sectionNav.find('li'),
                $mobileNav = $sectionNav.find('select'),
                $fundSection = $investSection.find('.fund-name'),
                $fundName = $fundSection.children('div'),
                $filterResults = $investSection.find('.filter-results');

            // display chosen content
            $navItem.on('click', function () {
                id = $(this).data('id');
                sectionNav(id);
            });

            // select replace the section nav on mobile
            $mobileNav.on('change', function () {
                id = $(this).find('option:selected').data('id');
                sectionNav(id);
            });

            // hide/show checked items
            $check.on('click', function () {
                var $itemChecked = $(this),
                    number = $sectionNav.find('.active').data('id'),
                    name = $itemChecked.parent().data('name');

                if ($itemChecked.hasClass('checked')) {
                    $filterResults.find('[data-group=' + number + ']').show();
                    $filterResults.find('[data-group=' + number + ']').find('[data-name=' + name + ']').show();
                } else {
                    $filterResults.find('[data-group=' + number + ']').find('[data-name=' + name + ']').hide();
                }
            });

            $filterResults.find('div').hide();

            // hide/display content
            function sectionNav() {

                $navItem.removeClass('active');
                $('.section-nav ul').find('[data-id=' + id + ']').addClass('active');

                $fundName.hide();
                $fundSection.find('[data-id=' + id + ']').show();

                $mobileNav.find('option:selected').removeAttr('selected');
                $mobileNav.find('[data-id=' + id + ']').attr('selected', 'selected');

                $check.removeClass('checked');
                $filterResults.children().hide();
            }
        }

        function thirdParty() {

            var $thirdParty = $('.how-to-invest.third-party'),
                $fundInfo = $thirdParty.find($('.fund-info')),
                $fundInfoChild = $thirdParty.find($('.fund-info > div')),
                $fundCompanies = $thirdParty.find($('.fund-companies')),
                $sectionNav = $thirdParty.find('.section-nav'),
                $select = $sectionNav.find('select'),
                $radio = $sectionNav.find('.radio');

            $radio.on('click', function () {
                var $selectedRadio = $(this),
                    group = $selectedRadio.parent().data('group');

                $fundCompanies.show();
                $fundInfoChild.not($('div[data-group=' + group + ']')).hide();
                $fundInfo.find('div[data-group=' + group + ']').show();

                $radio.not($selectedRadio).siblings('.select').find('select').prop('disabled', true);

                $radio.parent().removeClass().addClass('inactive');
                $selectedRadio.parent().removeClass().addClass('active');

                if ($selectedRadio.parent().hasClass('active')) {
                    $selectedRadio.siblings('.select').find('select').prop('disabled', false);
                }
            });

            $sectionNav.find('.inactive select').prop('disabled', true);

            if ($sectionNav.children().hasClass('active')) {
                var groupId = $sectionNav.children('.active').data('group');
                $fundInfoChild.not($('div[data-group=' + groupId + ']')).hide();
            }

            // display
            $select.on('change', function () {
                var group = $(this).parents('.active').data('group'),
                    id = $(this).find('option:selected').data('id');

                $fundCompanies.hide();
                $fundInfo.find('[data-group=' + group + ']').find('[data-id=' + id + ']').show();
            });
        }

        fundModule();
        thirdParty();
    }

    function filters() {
        var $filter = $('.secondary-nav'),
            $select = $filter.find('select');

        // filters table data
        $select.on('change', function () {
            var url = $(this).find('option:selected').data('url');
            window.location.href = url;
        });
    }

    function emailUpdateForm() {
        var $signupForm = $('#signup');

        //third party script
        function validateSignup($frm) {
            var emailAddress = $frm.find('input[name="Email"]').val();
            var errorString = '';
            if (emailAddress == '' || emailAddress.indexOf('@') == -1) {
                errorString = 'Please enter your email address';
            }

            var isError = false;
            if (errorString.length > 0) {
                isError = true;
            }

            if (isError) {
                alert(errorString);
            }
            return !isError;
        }

        $signupForm.find('input[type="button"]').click(function () {
            if (validateSignup($signupForm)) {
                $.ajax({
                    type: 'POST',
                    url: $signupForm.data('action'),
                    dataType: 'jsonp',
                    data: $signupForm.find('input[type="hidden"], input[type="text"]').serialize()
                });
            }
        });
    }

    function responsiveIframes() {
        $(this).wrap('<div class="iframe-holder"></div>');
    }

    FundInfo = {
        settings: {
            // common variables
            $table: $('.table.assets table:first-child tbody'),
            $filters: $('.filters'),
            $button: $('input.button')
        },
        init: function () {
            s = this.settings;
            this.filter();
        },
        filter: function () {

            var $filterCategories = s.$filters.find('div[data-category]');

            // get, traverse categories to build data object
            function serialiseFilters() {

                var filterObj = {}; // object result

                // each category
                $filterCategories.each(function () {

                    var $el = $(this);

                    filterObj[$el.data('category')] = [];

                    $el.find('input:checked').each(function () {
                        filterObj[$el.data('category')].push($(this).data('querystringvalue'));
                    });

                    filterObj[$el.data('category')] = filterObj[$el.data('category')].join().replace(/,/g, "|"); // parse to pipe delimited well formed JSON

                });

                return filterObj;
            };

            //// submit the filter form
            //s.$button.on('click', function (e) {
            //    e.preventDefault();
            //    var filterData = serialiseFilters();

            //    // post
            //    window.location = window.location.pathname + '?' + serialise.obj(filterData);
            //});

        }
    };
    // run equal heights on a group
    function equalHeightsGroup($collection, targetSelector) {

        var heights = []; // set function variables

        // traverse each entity in collection, retrieve height
        $collection.each(function () {
            var $thisItem = $(this).find(targetSelector);
            $thisItem.height(''); // clear inline height
            heights.push($thisItem.height());
        });

        var tallest = Math.max.apply(Math, heights); // determine tallest from collection heights

        // traverse collection again, this time setting height to that of the tallest
        $collection.each(function () {
            $(this).find(targetSelector).height(tallest);
        });
    }

    // equal heights in rows
    function equalHeightsForRows($targetElement, targetSelector) {

        var thisGroup = []; // group collection

        // traverse elements for equal heights, first selecting by row (modulo 2)
        $targetElement.each(function (i) {

            thisGroup.push($(this)); // add group to collection

            // if last in row of 2
            if ((i + 1) % 2 === 0) {
                equalHeightsGroup($(thisGroup), targetSelector); // run equal heights
                thisGroup = []; // flush collection
            }
        });
    }

    function evenPanels() {

        if ($(window).width() > TABLET_WIDTH) {
            $('.fund-summary').each(function () {
                var $fundSummary = $(this);

                equalHeightsForRows($fundSummary.find('.equal-height'), '.header-fund-cards');
                //Stop displying quotes as requested in LSC-7
                //equalHeightsForRows($fundSummary.find('.fund-quote'), '.quote');
                equalHeightsForRows($fundSummary.find('.footer-fund-cards'), '.links');
                equalHeightsForRows($fundSummary.find('.footer-fund-cards'), '.logos');
            });
        }
    }

    function columnHeight() {
        var $sidebar = $(this);

        function calcHeight() {
            if ($(window).width() > DESKTOP_WIDTH) {
                // reset height to initial
                $sidebar.addClass('static');

                // if main-content taller remove class to position absolute and occupy 100% height of the parent element
                if ($('.main-content').outerHeight() > $sidebar.outerHeight()) {
                    $sidebar.removeClass('static');
                }
            }
        }

        $window.on({
            load: function () {
                calcHeight();
            },
            resize: function () {
                calcHeight();
            }
        });
    }

    function cascadeSelect() {
        var $selectsContainer = $('.posts-select'),
            $blogFilters = $selectsContainer.parent();

        $blogFilters.find('.select-container').hide();
        $blogFilters.find('[data-value="' + $selectsContainer.find('option:selected').val() + '"]').show();

        //When blog type is 'fund updates', do not shopw the year/month filter
        if ($('.fund-name-container').css('display') != 'none') {
            $('.year-month-filter-section').hide();
            $('.blog-search-box').css({ 'width': '100%' });
            $('.blogsearch-textbox-div').addClass('blogsearch-textbox-div-without-time-filter');
            $('.blogsearch-button-div').addClass('blogsearch-button-div-without-time-filter');
        }
        else {
            $('.blogsearch-textbox-div').addClass('blogsearch-textbox-div-with-time-filter');
            $('.blogsearch-button-div').addClass('blogsearch-button-div-with-time-filter');
        }
    }
    $(window).on({
        load: function () {
            if ($('.table.responsive').length) {
                $('.responsive .border').each(fixedColumn);
            }
        }
    });

    function showMoreCarousel() {
        var $thisCarousel = $(this),
            $slides = $thisCarousel.find('.slide'),
            $showMoreButton = $thisCarousel.find('.show-more-carousel'),

            // find the first and last slides in carousel and cache first
            $cachedSlide = $slides.first(),
            $lastSlide = $slides.last(),
            $cacheNext;

        $thisCarousel.find('.show-more-carousel').on('click', function () {

            // reaveal slide directly after cachedSlide and cache the newly revealed slide
            $cacheNext = $cachedSlide.next();
            $cacheNext.stop().slideDown(function () {
                equalHeightsGroup($('.eql-height article'), '.panel-container');
            });
            $cachedSlide = $cacheNext;

            // hide button when last slide has been reached
            if ($cacheNext.is($lastSlide)) {
                $showMoreButton.hide();
            }
        });
    }

    function carouselControl() {
        var $this = $(this),
            $step = $this.find('.step'),
            $controls = $this.find('.pagination-controls'),
            $carousel = $this.find('.ch-carousel');

        $step.add($controls).on('click', function () {
            $carousel.data('codehouseCarousel').update(null, false, { rotate: { auto: false } });
        });
    }

    function initCarousel() {
        var $this = $(this);

        $this.codehouseCarousel({
            modes: {
                slide: true,
                infinite: false,
                responsive: true,
                nudge: false
            },
            controls: {
                step: true,
                pager: true
            },
            rotate: {
                auto: $this.data('rotation'),
                direction: 'right',
                interval: $this.data('rotate-interval') || 5000,
                duration: 1000,
                type: 'quad'
            },
            dimensions: {
                fixedHeight: false,			// height does not change
                maxHeight: 600,				// grow in height no larger than this value
                baseWidth: 1200				// starting width from which to base scaling
            },
            options: {
                preload: false,
                stickySlides: false,
                setSlide: 1,
                visibleClassAfter: true,
                maskedOverflow: false,
                touchControl: true
            }
        });
    }

    // 630
    function mobile(mode) {
        if (mode === 'resize') {
            $('.responsive .border').each(fixedColumn);
        }
    }

    $(document).ready(function () {

        stylishForms.apply();

        if ($('.cookie').length) { cookieHide(); }

        if ($('a.back-to-top').length) { pageScroll(); }

        if ($('#primary-nav').length) { navigation(); }

        if ($('.role-confirmation').length) { roleConfirmation(); }

        if ($('.edit-email-preferences').length) { EditEmailPreferences(); }

        if ($('.gmap').length) { gMaps.load(); }

        if ($('.table.responsive').length) {
            // on horizontal scroll toggle gradient
            $('.table.responsive .container, .table.responsive .table-container').each(onScrollGradient);
            // set dimensions for fixed column
            $('.responsive .border').each(fixedColumn);
        }

        if ($('select').length) { clientStyling(); }

        if ($('.assets').length) { filterAssets(); }

        if ($('.nav-tabs').length) { tabNavigation(); }

        if ($('.form').length) { submitFormsWhenHitEnter(); }

        $('.show.cta').each(expandContent);

        if ($('.scfForm').length) {
            filterAssets();
            formStyling();
            emptyValue();
        }

        if ($('.filtering select').length) { applyFilters(); }

        if ($('.how-to-invest').length) {
            filterAssets();
            filterInvestments();
        }

        if ($('.role-switcher').length) {
            roleSwitch();
        }

        if ($('.toggle').length) { FundInfo.init(); }

        if ($('.filters, .archives, .secondary-nav').length) { filters(); }

        if ($('#signup').length) { emailUpdateForm(); }

        if (!client.OldIE) { $('iframe[src*="youtube"]').each(responsiveIframes); }

        if ($('.fund-summary').length) { evenPanels(); }

        if ($('.posts-select').length) { cascadeSelect(); }

        $('aside#side-content').each(columnHeight);

        if ($('.filter-listing').length) {
            boot.loadScript('listing.js', 'global', function () {
                $('.filter-listing').filterListing();
            });
        }

        var responsive = new Responsive();  // instantiate responsive class

        // responsive actions manifest
        actions = Responsive({
            630: mobile,
            1026: new Function()
        });

        // responsive images fluidImages is a jQuery member created by Responsive();
        $('.responsive-image').fluidImages({
            container: $('.responsive-image-container'),    // decorate containers accordingly
            objectFit: !client.IE,  // use object fit if not IE
            useMargins: client.OldIE    // use margins if old ie
        });

        $('.article-carousel').each(showMoreCarousel);

        $('.ch-carousel').each(initCarousel);

        $('.hero-carousel').each(carouselControl);

    }); // dom ready

    $(window).load(function () {
        equalHeightsGroup($('.eql-height article'), '.panel-container');
        equalHeightsGroup($('.slide-wrapper'), '.slide');
    });

    $(window).resize(function () {
        equalHeightsGroup($('.eql-height article'), '.panel-container');
    });

})(jQuery);