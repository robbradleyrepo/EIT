/* 
 * ---------------------------------------- *
 * Name: 	Listing JavaScripts             *
 * Type: 	JavaScript Module               *
 * Project: Liontrust                       *
 * Version: Not Versioned                   *
 * Author:	Codehouse Ltd                   *
 * Requisites: >=jQuery 1.10.2              *
 * ---------------------------------------- *
 */
(function ($) {
    $.fn.filterListing = function () {

        return this.each(function () {

            var $thisListing = $(this),
                $thisFilter = $thisListing.find('.filters'),
                filterType = $thisFilter.data('type'),
                $pagination = $thisListing.find('.pagination'),
                $pageNumber = $pagination.find('.pg-number'),
                $tableHead = $thisListing.find('.table-wrapper table'),
                $noResults = $thisListing.find('.message'),
                datasourceId = $thisListing.data('documents-module-datasource-id'),
                defaultSortOption = $thisListing.data('documents-module-defaultsortoption'),
                responseType,
                isMobile = client.Mobile || $(window).width() < 600,
                dataObj = {},
                serviceRequests = {},
                onLoad = false,
                markup,
                currentPageNumber,
                data = {
                    direction: {
                        next: function () {
                            currentPageNumber += 1;
                        },
                        prev: function () {
                            currentPageNumber -= 1;
                        }
                    },
                    baseHtml: {
                        fundListing: function (response) {
                            var responseData;
                            for (var prop in response.Funds) {
                                responseData = response.Funds[prop];

                                markup.push('<tr class="' + (prop % 2 === 1 ? 'bg-colour' : '') + '">');
                                markup.push('<td class="mobile">' + $tableHead.find('.fund-name').text() + '</td>');

                                if (responseData.FundLink == null) {
                                    markup.push('<td>' + responseData.FundName + '</td>');
                                }
                                else {
                                    markup.push('<td><a href="' + responseData.FundLink + '">' + responseData.FundName + '</a></td>');
                                }

                                markup.push('<td class="mobile">' + $tableHead.find('.type').text() + '</td>');
                                markup.push('<td>' + responseData.FundType + '</td>');
                                markup.push('<td class="mobile">' + $tableHead.find('.fund-manager').text() + '</td>');
                                if (responseData.FundManager == null) {
                                    markup.push('<td></td>');
                                }
                                else {

                                    markup.push('<td><a href="' +
                                        responseData.FundManagerLink +
                                        '">' +
                                        responseData.FundManager +
                                        '</td>');
                                }
                                markup.push('<td class="mobile">' + $tableHead.find('.process').text() + '</td>');
                                markup.push('<td>' + responseData.Process + '</td>');
                                markup.push('<td class="mobile">' + $tableHead.find('.literature').text() + '</td>');
                                markup.push('<td>' + '<a href="' + responseData.LiteratureLink + '">' + responseData.LiteratureText + '</td>');
                                markup.push('</tr>');
                            }
                        },
                        productLiterature: function (response) {
                            var responseData,
                                responseType = ($thisListing.hasClass('documents')) ? response.documents : response.Funds;
                            for (var prop in responseType) {
                                responseData = responseType[prop];

                                markup.push('<tr class="' + (prop % 2 === 1 ? 'bg-colour' : '') + '">');
                                if (responseData.LiteratureLink != '') {
                                    markup.push('<td>' + '<input type="checkbox" id="' + 'checkbox-' + [prop] + '"' + '>' + '<label for="checkbox-' + [prop] + '"' + '>' + '<span class="square checkbox" data-associated-item="' + responseData.ItemId + '">' + '<svg x="0px" y="0px" width="15px" height="13px" viewBox="0 0 15 13" enable-background="new 0 0 15 13" xml:space="preserve">' + '<path fill="#34B233" stroke="#FFFFFF" stroke-width="1" stroke-miterlimit="10" d="M14.613 0C10.051 2.81 6.74 6.355 5.252 8.135L1.609 5.268 0 6.57 6.295 13C7.379 10.213 10.811 4.766 15 0.895L14.613 0z"></path>' + '</svg>' + '</span>' + '</label>' + '</td>');
                                } else {
                                    markup.push('<td></td>');
                                }

                                markup.push('<td class="mobile">' + $tableHead.find('.document-name').text() + '</td>');
                                markup.push('<td>' + responseData.DocumentName + '</td>');
                                if (!$thisListing.hasClass('documents')) {
                                    markup.push('<td class="mobile">' + $tableHead.find('.document-fund').text() + '</td>');
                                    if (responseData.FundUrl == '') {
                                        markup.push('<td>' + responseData.FundName + '</td>');
                                    }
                                    else {
                                        markup.push('<td><a href="' + responseData.FundUrl + '">' + responseData.FundName + '</a></td>');
                                    }

                                }
                                markup.push('<td class="mobile">' + $tableHead.find('.document-download').first().text() + '</td>');
                                if (responseData.LiteratureLink != '') {
                                    markup.push('<td class="document-download">' + '<svg class="document" x=" 0px" y="0px" width="13.006px" height="17.002px" viewBox="0 0 13.006 17.002" enable-background="new 0 0 13.006 17.002" xml:space="preserve">' + '<polygon fill="#FFFFFF" points="8.118,0 0.577,0 0.577,16 12.577,16 12.577,3.961 ">' + '</polygon>' + '<path fill="#8E8E8E" d="M12.446 3.282c-0.154-0.182-0.34-0.389-0.562-0.62 -0.216-0.231-0.456-0.482-0.74-0.771 -0.283-0.288-0.53-0.532-0.751-0.752C10.158 0.915 9.955 0.728 9.776 0.57 9.48 0.301 9.252 0.119 9.079 0H1.577C0.708 0 0 0.715 0 1.598v13.802c0 0.883 0.708 1.604 1.577 1.603h9.852c0.869 0 1.577-0.72 1.577-1.603V3.989C12.889 3.814 12.711 3.589 12.446 3.282zM11.885 15.399c0 0.257-0.203 0.457-0.456 0.457H1.577c-0.247 0-0.45-0.201-0.45-0.457V1.598c0-0.251 0.203-0.458 0.45-0.458h6.485v2.424c0 0.802 0.647 1.459 1.437 1.459h2.386V15.399zM9.499 4.165c-0.327 0-0.592-0.27-0.592-0.602V1.14h0.233c0.808 0.728 2.035 1.974 2.744 2.787v0.238H9.499z"></path>' + '<image src="images/document.png" width="13.006px" height="17.002px">' + '</image>' + '</svg>' + '<a href="' + responseData.LiteratureLink + '"' + 'target="_blank' + '">' + responseData.LiteratureLabel + '</td>');
                                } else {
                                    markup.push('<td class="document-download">' + responseData.LiteratureLabel + '</td>');
                                }

                                markup.push('</tr>');
                            }
                            presentation.pagination(response.TotalPages, response.PageNumber);
                        }
                    },
                    helper: {},
                    // @param toSerialise {Boolean} send serialised data
                    serialiseToObj: function (toSerialise) {
                        if (toSerialise) {
                            dataObj = serialise.jQueryData($thisFilter, 'serialise-form');

                            if ($thisListing.hasClass('documents')) {
                                dataObj.datasourceId = datasourceId;
                                dataObj.defaultSortOption = defaultSortOption;
                            }

                            // check if pagination exists 
                            if (typeof currentPageNumber !== 'undefined') {
                                dataObj.Page = currentPageNumber;
                            }
                        }
                    },
                    hash: {
                        update: function () {
                            var hashString,
                                scrollposition = $(window).scrollTop();

                            if (onLoad) {
                                return false;
                            }

                            // sanitise page number for friendly url
                            dataObj.Page = dataObj.Page + 1;

                            hashString = serialise.obj(dataObj);

                            // sanitise page number for friendly url
                            dataObj.Page = dataObj.Page - 1;

                            window.location.href = window.location.href.split('#')[0] + '#' + hashString;

                            $(window).scrollTop(scrollposition);
                        },
                        read: function () {
                            var updateFilters,
                                hashString,
                                obj,
                                $el;

                            if (window.location.hash) {

                                hashString = '?' + window.location.href.split('#')[1];
                                obj = serialise.url(hashString);

                                // resets filters to default
                                presentation.resetFilters();

                                // update filters from hash
                                updateFilters = {
                                    fundListing: function () {
                                        for (var prop in obj) {
                                            $el = $thisFilter.find('[data-serialise-form-group="' + prop + '"] input[data-id="' + obj[prop] + '"]');

                                            $el.prop('checked', true).trigger('change');

                                            // update checkbox dropdown
                                            presentation.resetMultiSelectLabels($el);
                                        }
                                    },
                                    productLiterature: function () {
                                        for (var prop in obj) {
                                            $el = $thisFilter.find('[data-serialise-form="' + prop + '"] option[value="' + obj[prop] + '"]');

                                            $el.prop('selected', true).trigger('change');
                                        }
                                    }
                                };

                                if (typeof obj.Page !== 'undefined') {
                                    currentPageNumber = obj.Page - 1;
                                }

                                updateFilters[filterType]();

                                // populate results serialising form
                                actions.callService(true);
                            }
                            else {
                                if ($thisListing.hasClass('documents')) {
                                    var $filterdropdownOption = $thisFilter.find('.sort-filter-dropdown option[value="' + defaultSortOption + '"]');
                                    if ($filterdropdownOption != null) {
                                        $filterdropdownOption.prop('selected', true);
                                    }
                                }
                                // populate results no serialising form
                                actions.callService(false);
                            }
                        }
                    }
                },
                presentation = {
                    buildMarkup: function (response) {
                        markup = [];
                        responseType = ($thisListing.hasClass('documents')) ? response.documents : response.Funds;

                        if (responseType == undefined || responseType.length === 0) {
                            // toggle message & table
                            $noResults.show();
                            presentation.pagination(response.TotalPages, response.PageNumber);
                            $pagination.hide();
                            $tableHead.parents('.outer-container').hide();

                        } else {
                            // store markup 
                            data.baseHtml[filterType](response);

                            // append markup to table
                            $thisFilter.parent().find('tbody').html(markup.join(''));

                            // toggle message & table
                            $noResults.hide();
                            $tableHead.parents('.outer-container').show();
                        }
                    },
                    pagination: function (pageTotal, pageNumber) {
                        var ellipsisMarkup = '<span class="show ellipsis">...</span>',
                            $nextBtn = $pagination.find('.next'),
                            $prevBtn = $pagination.find('.prev'),
                            $pageNumber = $pagination.find('.pg-number'),
                            minimumPagesDisplay = (isMobile) ? 4 : 6,
                            $lastPage,
                            $firstPage,
                            $currentPage,
                            startIndex,
                            lastIndex,
                            currentPageIndex = pageNumber;

                        currentPageNumber = pageNumber;

                        if (typeof pageTotal === 'undefined' || pageTotal < 2) {
                            $pagination.hide();
                            return false;
                        }

                        $pagination.show();

                        $pageNumber.empty(); // clear pagination

                        $nextBtn.add($prevBtn).addClass('disabled');

                        // toggle next/prev btns
                        if (currentPageNumber + 1 < pageTotal) {
                            $nextBtn.removeClass('disabled');
                        }
                        if (currentPageNumber > 0) {
                            $prevBtn.removeClass('disabled');
                        }

                        // update pagination indexes
                        for (var i = 1; i <= pageTotal; i += 1) {
                            $pageNumber.append('<a class="' + (i < 2 ? 'show' : '') + '">' + i + '</a>');
                        }

                        $lastPage = $pageNumber.children().last();
                        $firstPage = $pageNumber.children().first();
                        $currentPage = $pageNumber.children('a').eq(currentPageIndex);

                        // add active state to current page
                        $currentPage.addClass('show active').find('span').addClass('active');

                        // show last page
                        $lastPage.addClass('show');

                        // always display two pages before and after the current page - except mobile
                        startIndex = (currentPageIndex - 2 < 0) ? 0 : currentPageIndex - 2;
                        lastIndex = pageNumber + 3;
                        $pageNumber.children().slice(startIndex, lastIndex).not('.active').addClass('show hide-mobile');

                        // exceptions - if only one page hidden after the first/last page, display it
                        if ($currentPage.index() - $firstPage.index() === 4 && !isMobile) {
                            $firstPage.next().addClass('show');
                        } else if ($lastPage.index() - $currentPage.index() === 4 && !isMobile) {
                            $lastPage.prev().addClass('show');
                        }

                        // add ellipsis 
                        if (pageNumber < minimumPagesDisplay && pageNumber < pageTotal - 5) {
                            $lastPage.prepend(ellipsisMarkup);
                        } else if (pageNumber >= minimumPagesDisplay && pageNumber < pageTotal - 5) {
                            $lastPage.prepend(ellipsisMarkup);
                            $firstPage.append(ellipsisMarkup);
                        } else if (pageNumber > minimumPagesDisplay && pageNumber >= pageTotal - 5) {
                            $firstPage.append(ellipsisMarkup);
                        }
                    },
                    // clear all filters
                    resetFilters: function () {
                        $thisFilter.find('input').each(function () {
                            var $thisInput = $(this);

                            if ($thisInput.parents('li').is(':first-child')) {
                                $thisInput.prop('checked', true).trigger('change');
                            }

                            // specific to checkbox dropdown
                            if ($thisInput.parents('.checked').length) {
                                $thisInput.prop('checked', false).trigger('change');
                            }
                        });
                    },
                    // checkbox dropdown placeholder text
                    resetMultiSelectLabels: function ($thisInput) {
                        var $thisSelect = $thisInput.parents('.checkbox-dropdown');

                        if ($thisInput.is(':checked')) {
                            $thisSelect.find('input.all').prop('checked', false).trigger('change');

                        } else if (!$thisSelect.find('input').is(':checked')) {
                            $thisSelect.find('input.all').prop('checked', true).trigger('change');
                        }
                    }
                },
                actions = {
                    // @param toSerialise {Boolean} send serialised data
                    callService: function (toSerialise) {

                        dataObj.datasourceId = datasourceId;

                        if ($thisListing.hasClass('documents')) {
                            dataObj.defaultSortOption = defaultSortOption;
                        }

                        // update data obj
                        data.serialiseToObj(toSerialise);

                        // hash in url
                        data.hash.update();

                        // prevent calls from stacking 
                        if (typeof serviceRequests[filterType] !== 'undefined') {
                            serviceRequests[filterType].abort();
                        }

                        serviceRequests[filterType] = $.ajax({
                            url: $thisListing.data('context-url'),
                            data: dataObj,
                            success: function (response) {
                                presentation.buildMarkup(response);
                                $thisListing.trigger('resultsUpdated');
                                onLoad = false;
                            }
                        });
                    }
                },
                events = {
                    fundListing: function () {

                        // clear btn
                        $thisFilter.next().find('a.clear').click(function (e) {
                            var scrollposition = $(window).scrollTop();

                            e.preventDefault();
                            presentation.resetFilters();

                            // reset url with hash in the end to prevent reload
                            window.location.hash = window.location.href.split('#')[0] + '#';

                            $(window).scrollTop(scrollposition);

                            actions.callService(true);
                        });

                        // multiple select labels
                        $thisFilter.find('.checkbox-dropdown input').not('.all').on('change', function () {
                            presentation.resetMultiSelectLabels($(this));
                        });

                        // submit
                        $thisFilter.next().find('.button').click(function (e) {
                            e.preventDefault();

                            // form data to obj
                            actions.callService(true);
                        });
                    },

                    productLiterature: function () {

                        // selects
                        $thisFilter.find('select[data-serialise-form]').on({
                            change: function () {
                                currentPageNumber = 0;
                                actions.callService(true);
                            }
                        });

                        // pagination
                        $pagination.on('click', 'a', function () {
                            var $el = $(this);

                            if ($el.hasClass('active') || $el.parent().hasClass('disabled')) {
                                return false;
                            }

                            if (typeof $el.data('direction') !== 'undefined') {
                                currentPageNumber = $pageNumber.find('.active').index();
                                data.direction[$el.data('direction')]();
                            } else {
                                currentPageNumber = $el.index();
                            }

                            actions.callService(true);
                        });
                    },
                    onLoad: function () {
                        onLoad = true;

                        // look for hash in url
                        data.hash.read();
                    }
                };

            // initialise events
            events.onLoad();
            events[filterType]();
        });
    };

}(jQuery));