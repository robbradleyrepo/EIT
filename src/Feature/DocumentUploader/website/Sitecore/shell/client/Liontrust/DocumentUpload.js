define(["sitecore"], function (Sitecore) {
    var model = Sitecore.Definitions.Models.ControlModel.extend({
        initialize: function (options) {
            this._super();
            //$('.ltadmin-loader-gif').show();
            $(".ltadmin-mediaitem-mappings-div").hide();
            //$.ajax({
            //    url: "/DocumentsUploader/DocumentUpload",
            //    type: "POST",
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (data) {

            //        $(".ltadmin-loader-gif").hide();
            //        $(".ltadmin-documentupload-form-div").show();
            //        $(".ltadmin-documentupload-form-error-div").hide();
            //        $(".ltadmin-documentupload-status-div").html("");

            //        var returnData = data;

            //        $(".dropdown-documenttype").empty();
            //        $(".dropdown-documenttype").append($("<option     />").val("").text("-- Select Document Type --"));

            //        if (returnData.DocumentTypes != null) {
            //            $.each(returnData.DocumentTypes, function (i, item) {
            //                $(".dropdown-documenttype").append($("<option     />").val(i).text(item));
            //            });
            //        }

            //        $(".hidden-doctypeid-factsheet").val(returnData.DocTypeIdForFactsheet);
            //        $(".hidden-doctypeid-kiid").val(returnData.DocTypeIdForKiid);
            //    },
            //    error: function (jqXHr, textStatus, errorThrown) {
            //        $(".ltadmin-loader-gif").hide();
            //        $(".ltadmin-documentupload-form-div").hide();
            //        $(".ltadmin-documentupload-form-container").append('<div class="ltadmin-documentupload-form-error-div">Exception occured when loading this page. Check logs for more information.<div>');
            //    }
            //});

            //Global variabe to hold uploaded file content
            var fileContents = [];

            //File uploader control change event
            $('#InputDocumentUpload').on('change', function (e) {

                var predefinedCurrencyCodes = ["USD", "GBP", "EUR"];

                var selectedDocType = $(".dropdown-documenttype").val();
                if (selectedDocType == '') {
                    alert("Please select a document type");
                    this.value = '';
                    return;
                }

                var docTypeIdForFactsheet = $(".hidden-doctypeid-factsheet").val();
                var docTypeIdForKiid = $(".hidden-doctypeid-kiid").val();

                $(".ltadmin-documentupload-status-div").html("");
                $(".mediapath-mapping-table-row").remove();
                $(".mediapath-mapping-table-header").remove();
                $(".ltadmin-mediaitem-mappings-div").hide();

                fileContents = [];

                var files = e.target.files;
                if (files.length > 0) {
                    for (var x = 0; x < files.length; x++) {
                        if (files[x].type != "text/xml" && files[x].type != "application/pdf") {
                            alert("Only XML/PDF files are allowed!");
                            this.value = '';
                            return;
                        }

                        //var fileNameWithoutExtension = (files[x].name.split('.').slice(0, -1)).join('.');

                        //var $sitecoreItemNameRegx = /^[\w\*\$][\w\s\-\$]*(\(\d{1,}\)){0,1}$/;
                        //if (!fileNameWithoutExtension.match($sitecoreItemNameRegx)) {
                        //    alert("Invalid characters found in the file name: " + files[x].name);
                        //    this.value = '';
                        //    return;
                        //}                        
                    }

                    $(".ltadmin-mediaitem-mappings-div").show();

                    if (selectedDocType == docTypeIdForFactsheet) {
                        var tabeheader = $('<tr class="mediapath-mapping-table-header">').append(
                                                                $('<th>').text("File Name"),
                                                                $('<th>').text("Fund Name"),
                                                                $('<th>').text("Factsheet"),
                                                                $('<th>').text("Share Class"),
                                                                $('<th class="ltadmin-textcenter">').text("Overwrite"),
                                                                $('<th>').text(""));

                        $(".ltadmin-mediapath-mapping-table").append(tabeheader);

                    }
                    else if (selectedDocType == docTypeIdForKiid) {
                        var tabeheader = $('<tr class="mediapath-mapping-table-header">').append(
                                                                $('<th>').text("File Name"),
                                                                $('<th>').text("Fund Name"),
                                                                $('<th>').text("Share Class"),
                                                                $('<th>').text("Language Code"),
                                                                $('<th class="ltadmin-textcenter">').text("Overwrite"),
                                                                $('<th>').text(""));

                        $(".ltadmin-mediapath-mapping-table").append(tabeheader);
                    }

                    for (var x = 0; x < files.length; x++) {
                        var reader = new FileReader();
                        reader.fileName = files[x].name;

                        reader.onload = function (f) {
                            var uploadedFileObj = { FileName: f.target.fileName, FileAsBinary: f.target.result, FileIndex: x };
                            fileContents.push(uploadedFileObj);

                            //Sample factsheet filename - Liontrust_Balanced_Fund_Factsheet_D_Acc.pdf
                            //Correct file name format for factsheet - Liontrust_{Fund Name}_FactSheet_{ShareClass}.pdf

                            //Sample KIID filename - GB00B0N6YF70_Liontrust_Special_Situations_R_Inc_EN_KID.pdf
                            //Correct file name format for KIIDs - {ISIN}_Liontrust_{Fund Name}_{ShareClass}_{LanguageCode}_KID.pdf

                            var fileNameWithoutExtension = (f.target.fileName.split('.').slice(0, -1)).join('.');

                            //Getting rid of the section before first underscor 
                            //For FactSheets, it is going to be "Liontrust". For KIIDs, it is going to be ISIN
                            var relavantFileNameSection = fileNameWithoutExtension.substr(fileNameWithoutExtension.indexOf("_") + 1);
                            
                            //If the selected document type is "Factsheet"
                            if (selectedDocType == docTypeIdForFactsheet) {
                                //Split the rest of the name by "_Factsheet_" and seperate out {Fund Name} and {ShareClass}
                                var elementsFromFileNameSection = relavantFileNameSection.split('_Factsheet_');
                                                               
                                var extractedFundName = (elementsFromFileNameSection[0] != undefined)? elementsFromFileNameSection[0].replace(/_/g, " ").trim() : "";
                                var extractedFactsheetValue = "Factsheet";
                                var extractedShareClass = (elementsFromFileNameSection[1] != undefined)? elementsFromFileNameSection[1].replace(/_/g, " ").trim(): "";

                                var tempFundNameInput = $('<input type="text" class="input-text-fundName" />');
                                tempFundNameInput.val(extractedFundName);

                                var tempFactSheetInput = $('<input type="text" class="input-text-factsheet" />');
                                tempFactSheetInput.val(extractedFactsheetValue);

                                var tempShareClassInput = $('<input type="text" class="input-text-shareclass" />');
                                tempShareClassInput.val(extractedShareClass);

                                var tempOverwriteCheckbox = $('<input type="checkbox" class="checkbox-overwrite-mediaitem" checked />');
                                var tempRemoveButton = $('<img class="img-delete-mediaitem-record ltadmin-cursor-pointer" title="Remove File" src="/sitecore/shell/client/Liontrust/delete.png" data-fileindex="' + x + '" data-filename="' + f.target.fileName + '" />');

                                var tr = $('<tr class="mediapath-mapping-table-row">').append(
                                                $('<td class="filename-td">').text(f.target.fileName),
                                                $('<td class="fundname-td">').append(tempFundNameInput),
                                                $('<td class="factsheet-td">').append(tempFactSheetInput),
                                                $('<td class="shareclass-td">').append(tempShareClassInput),
                                                $('<td class="overwriteMediaItem-td ltadmin-textcenter">').append(tempOverwriteCheckbox),
                                                $('<td class="deleterecord-td ltadmin-textcenter">').append(tempRemoveButton));

                                //Append table row into the table
                                $(".ltadmin-mediapath-mapping-table").append(tr);
                            }
                            else if (selectedDocType == docTypeIdForKiid) //If the selected document type is "Kiid"
                            {
                                //Getting rid of "Liontrust" part which apprear in the begining of the text 
                                var relavantKiidFileNameSection = relavantFileNameSection.substr(relavantFileNameSection.indexOf("_") + 1);

                                //Getting rid of "KID" part which appear in the end of the text
                                relavantKiidFileNameSection = relavantKiidFileNameSection.substr(0, relavantKiidFileNameSection.lastIndexOf("_"));
                                
                                //Extract the language code appear in the end of the text
                                var languageCode = relavantKiidFileNameSection.substr(relavantKiidFileNameSection.lastIndexOf("_") + 1);
                                relavantKiidFileNameSection = relavantKiidFileNameSection.substr(0, relavantKiidFileNameSection.lastIndexOf("_"));

                                //This intermediate section can be found as a currency code. If it identified as a currency code, strip that currency code before move forward and extract the shared class.
                                //This extra check applies because some documents names consists the currency code and some are not.
                                var intermediateSection = relavantKiidFileNameSection.substr(relavantKiidFileNameSection.lastIndexOf("_") + 1);
                                if (predefinedCurrencyCodes.indexOf(intermediateSection.toUpperCase()) > -1)
                                {
                                    relavantKiidFileNameSection = relavantKiidFileNameSection.substr(0, relavantKiidFileNameSection.lastIndexOf("_"));
                                }
                               
                                //Extract latter part of the shared class
                                var latterPartOfSharedClass = relavantKiidFileNameSection.substr(relavantKiidFileNameSection.lastIndexOf("_") + 1);
                                relavantKiidFileNameSection = relavantKiidFileNameSection.substr(0, relavantKiidFileNameSection.lastIndexOf("_"));

                                //Extract first part of the shared class
                                var firstPartOfSharedClass = relavantKiidFileNameSection.substr(relavantKiidFileNameSection.lastIndexOf("_") + 1);

                                var restOfFundName = relavantKiidFileNameSection.substr(0, relavantKiidFileNameSection.lastIndexOf("_"));
                                
                                var extractedFundName = restOfFundName.replace(/_/g, " ").trim() + " Fund";
                                var extractedShareClass = firstPartOfSharedClass.trim() + " " + latterPartOfSharedClass.trim();
                                var extractedLanguageCode = languageCode;

                                var tempFundNameInput = $('<input type="text" class="input-text-fundName" />');
                                tempFundNameInput.val(extractedFundName.trim());

                                var tempShareClassInput = $('<input type="text" class="input-text-shareclass" />');
                                tempShareClassInput.val(extractedShareClass.trim());

                                var tempLanguageCodeInput = $('<input type="text" class="input-text-languagecode" />');
                                tempLanguageCodeInput.val(extractedLanguageCode.trim());

                                var tempOverwriteCheckbox = $('<input type="checkbox" class="checkbox-overwrite-mediaitem" checked />');
                                var tempRemoveButton = $('<img class="img-delete-mediaitem-record ltadmin-cursor-pointer" title="Remove File" src="/sitecore/shell/client/Liontrust/delete.png" data-fileindex="' + x + '" data-filename="' + f.target.fileName + '" />');

                                var tr = $('<tr class="mediapath-mapping-table-row">').append(
                                                $('<td class="filename-td">').text(f.target.fileName),
                                                $('<td class="fundname-td">').append(tempFundNameInput),
                                                $('<td class="shareclass-td">').append(tempShareClassInput),
                                                $('<td class="languagecode-td">').append(tempLanguageCodeInput),
                                                $('<td class="overwriteMediaItem-td ltadmin-textcenter">').append(tempOverwriteCheckbox),
                                                $('<td class="deleterecord-td ltadmin-textcenter">').append(tempRemoveButton));

                                //Append table row into the table
                                $(".ltadmin-mediapath-mapping-table").append(tr);
                            }
                        };

                        reader.readAsDataURL(files[x]);
                    }
                }
            });

            //Delete record button click
            $(document).on('click', '.img-delete-mediaitem-record', function () {
                var dataAttributeFileIndex = $(this).attr("data-fileindex");
                var dataAttributeFileName = $(this).attr("data-filename");

                for (var i = 0; i < fileContents.length; i++) {
                    if (fileContents[i].FileIndex == dataAttributeFileIndex && fileContents[i].FileName == dataAttributeFileName) {
                        fileContents.splice(i, 1);
                        break;
                    }
                }

                $(this).closest(".mediapath-mapping-table-row").remove();

                if (fileContents.length == 0) {
                    $('#InputDocumentUpload').val('');
                    $(".ltadmin-mediaitem-mappings-div").hide();
                }
            });

            //Submit button action
            $(".btn-submit-documentUpload").click(function () {

                //var selectedDocType = $(".dropdown-documenttype").val();
                //if (selectedDocType == '') {
                //    alert("Please select a document type");
                //    return;
                //}

                //var mappingTableRows = $(".mediapath-mapping-table-row");
                if (fileContents.length == 0 /*|| mappingTableRows.length == 0*/) {
                    alert("Please select file");
                    return;
                }

                var docTypeIdForFactsheet = $(".hidden-doctypeid-factsheet").val();
                var docTypeIdForKiid = $(".hidden-doctypeid-kiid").val();

                var customUploadedFileList = [];

                var mappingTableRows = $(".mediapath-mapping-table-row");

                //if (selectedDocType == docTypeIdForFactsheet) {
                //    for (var x = 0; x < mappingTableRows.length; x++) {
                //        var tempRow = $(mappingTableRows[x]);
                //        var uploadedFileName = tempRow.find(".filename-td");
                //        var fundNameInput = tempRow.find(".input-text-fundName");
                //        var factsheetInput = tempRow.find(".input-text-factsheet");
                //        var shareclassInput = tempRow.find(".input-text-shareclass");
                //        var overwriteCheckbox = tempRow.find(".checkbox-overwrite-mediaitem");

                //        if (fundNameInput.val() == '') {
                //            alert("Please fill fund name for all the uploaded files");
                //            return;
                //        }

                //        var $sitecoreItemNameRegx = /^[\w\*\$][\w\s\-\$]*(\(\d{1,}\)){0,1}$/;

                //        if (!fundNameInput.val().match($sitecoreItemNameRegx)) {
                //            alert("Invalid characters found in the Fund Name field: " + fundNameInput.val());
                //            return;
                //        }

                //        if (factsheetInput.val() == '') {
                //            alert("Please fill factsheet name for all the uploaded files");
                //            return;
                //        }

                //        if (!factsheetInput.val().match($sitecoreItemNameRegx)) {
                //            alert("Invalid characters found in the factsheet field: " + factsheetInput.val());
                //            return;
                //        }

                //        if (shareclassInput.val() == '') {
                //            alert("Please fill share class for all the uploaded files");
                //            return;
                //        }

                //        if (!shareclassInput.val().match($sitecoreItemNameRegx)) {
                //            alert("Invalid characters found in the share class field: " + shareclassInput.val());
                //            return;
                //        }

                //        var tempObj = { TempFileName: uploadedFileName.html(), TempFundName: fundNameInput.val(), TempFactsheet: factsheetInput.val(), TempShareClass: shareclassInput.val(), TempOverwrite: overwriteCheckbox.prop('checked'), IsFactSheet: true };
                //        customUploadedFileList.push(tempObj);
                //    }
                //}
                //else if (selectedDocType == docTypeIdForKiid) {
                //    for (var x = 0; x < mappingTableRows.length; x++) {
                //        var tempRow = $(mappingTableRows[x]);
                //        var uploadedFileName = tempRow.find(".filename-td");
                //        var fundNameInput = tempRow.find(".input-text-fundName");
                //        var shareclassInput = tempRow.find(".input-text-shareclass");
                //        var languagecodeInput = tempRow.find(".input-text-languagecode");
                //        var overwriteCheckbox = tempRow.find(".checkbox-overwrite-mediaitem");

                //        if (fundNameInput.val() == '') {
                //            alert("Please fill fund name for all the uploaded files");
                //            return;
                //        }

                //        var $sitecoreItemNameRegx = /^[\w\*\$][\w\s\-\$]*(\(\d{1,}\)){0,1}$/;

                //        if (!fundNameInput.val().match($sitecoreItemNameRegx)) {
                //            alert("Invalid characters found in the Fund Name field: " + fundNameInput.val());
                //            return;
                //        }

                //        if (shareclassInput.val() == '') {
                //            alert("Please fill share class for all the uploaded files");
                //            return;
                //        }

                //        if (!shareclassInput.val().match($sitecoreItemNameRegx)) {
                //            alert("Invalid characters found in the share class field: " + shareclassInput.val());
                //            return;
                //        }

                //        if (languagecodeInput.val() == '') {
                //            alert("Please fill language code for all the uploaded files");
                //            return;
                //        }

                //        if (!languagecodeInput.val().match($sitecoreItemNameRegx)) {
                //            alert("Invalid characters found in the language code field: " + languagecodeInput.val());
                //            return;
                //        }

                //        var tempObj = { TempFileName: uploadedFileName.html(), TempFundName: fundNameInput.val(), TempShareClass: shareclassInput.val(), TempLanguageCode: languagecodeInput.val(), TempOverwrite: overwriteCheckbox.prop('checked'), IsFactSheet: false };
                //        customUploadedFileList.push(tempObj);
                //    }
                //}

                var uploadedFileList = [];

                $.each(fileContents, function (i, item) {

                    uploadedFileList.push(item);

                    //var filteredList = $.grep(customUploadedFileList, function (n, i) {
                    //    return (n.TempFileName == item.FileName);
                    //});

                    //if (filteredList.length == 1) {
                    //    var customFileObj = filteredList[0];

                    //    if (customFileObj != null) {
                    //        var customFileName = customFileObj.TempFileName;
                    //        var customFundName = customFileObj.TempFundName;
                    //        var customFactsheet = (customFileObj.IsFactSheet) ? customFileObj.TempFactsheet : "";
                    //        var customShareclass = customFileObj.TempShareClass;
                    //        var customLanguagecode = (!customFileObj.IsFactSheet) ? customFileObj.TempLanguageCode : "";

                    //        var uploadedFileObj = { FileName: customFileName, FundName: customFundName, FactsheetName: customFactsheet, ShareClassName: customShareclass, LanguageCode: customLanguagecode, SitecoreMediaItemOverwrite: customFileObj.TempOverwrite, FileAsBinary: item.FileAsBinary };
                    //        uploadedFileList.push(uploadedFileObj);
                    //    }
                    //}
                });

                var documentUploadEntity = {};
                documentUploadEntity.SeletedDocumentType = "xml";//selectedDocType;
                documentUploadEntity.UploadedFiles = uploadedFileList;

                $('.ltadmin-loader-gif').show();
                $.ajax({
                    url: "/api/sitecore/DocumentsUploader/UploadDocuments",
                    type: "POST",
                    data: JSON.stringify(documentUploadEntity),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $(".ltadmin-loader-gif").hide();

                        var returnData = data;
                        //if (returnData.UploadSuccess) {
                        //    $(".ltadmin-documentupload-status-div").removeClass("ltadmin-documentupload-form-success-div");
                        //    $(".ltadmin-documentupload-status-div").removeClass("ltadmin-documentupload-form-error-div");
                        //    $(".ltadmin-documentupload-status-div").html("");

                        //    var sub_ul = $("<ul/>");
                        //    sub_ul.addClass("ltadmin-ul-leftpadding");

                        //    $.each(returnData.UploadErrorDictionary, function (fileName, uploadErrorMessage) {
                        //        var liText = "";
                        //        var sub_li = $("<li/>");

                        //        if (uploadErrorMessage == '') {
                        //            sub_li.addClass("ltadmin-documentupload-form-success-div");
                        //            liText = fileName + ":  Successfully Uploaded";
                        //        }
                        //        else {
                        //            sub_li.addClass("ltadmin-documentupload-form-error-div");
                        //            liText = fileName + ":  Upload Failed - " + uploadErrorMessage;
                        //        }

                        //        sub_li.html(liText);
                        //        sub_ul.append(sub_li);
                        //    });

                        //    $(".ltadmin-documentupload-status-div").append(sub_ul);
                        //}
                        //else {
                        //    $(".ltadmin-documentupload-status-div").addClass("ltadmin-documentupload-form-error-div");
                        //    $(".ltadmin-documentupload-status-div").removeClass("ltadmin-documentupload-form-success-div");
                        //    $(".ltadmin-documentupload-status-div").html("Error occured when uploading the document(s). Please try again.");
                        //}
                    },
                    error: function (jqXHr, textStatus, errorThrown) {
                        $(".ltadmin-loader-gif").hide();
                        $(".ltadmin-documentupload-status-div").addClass("ltadmin-documentupload-form-error-div");
                        $(".ltadmin-documentupload-status-div").removeClass("ltadmin-documentupload-form-success-div");
                        $(".ltadmin-documentupload-status-div").html("Error occured when uploading the document(s). Please try again.");
                    }
                });
            });

            $(".dropdown-documenttype").change(function () {
                $(".ltadmin-documentupload-status-div").html("");
                $(".mediapath-mapping-table-row").remove();
                $(".mediapath-mapping-table-header").remove();
                $(".ltadmin-mediaitem-mappings-div").hide();

                $('#InputDocumentUpload').val('');
                fileContents = [];
            });
        }
    });

    var view = Sitecore.Definitions.Views.ControlView.extend({
        initialize: function (options) {
            this._super();
        }
    });

    Sitecore.Factories.createComponent("DocumentUpload", model, view, ".sc-DocumentUpload");
});
