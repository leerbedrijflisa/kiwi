var data = new FormData();
var fileObj = new Object;
var listCount = 0;

// Event listeners
$('form#uploadfiles input[type=file]').on('change', function (e) {
    var files = e.target.files;
    if (files.length > 0) {
        processFileSelection(e, files);
    }
});

// Upload methods
function upload() {
    if (objectHasproperties(fileObj)) {
        // Add all non-standard properties of the file object to the form data
        for (var key in fileObj) {
            if (fileObj.hasOwnProperty(key)) {
                data.append(fileObj[key].name, fileObj[key]);
            }
        }

        ActivateFileUploadOverlay();

        // Send the ajax call
        $.ajax({
            type: "POST",
            url: '/Report/UploadFiles',
            contentType: false,
            processData: false,
            data: data,
            success: function(data, textStatus, xhr) {
                uploadSuccess(data, textStatus, xhr);
            },
            error: function (xhr, textStatus, errorThrown) {
                uploadFailed(xhr, textStatus, errorThrown);
            },
            complete: function (xhr, textStatus) {
                requestComplete(xhr, textStatus);
            }
        });
    }
    else {
        window.location.href = '/Report/Done';
    }
}

function requestComplete(xhr, textStatus) {
    // Clear formdata so that it can be refilled when needed
    data = new FormData();
}

function uploadSuccess(data, textStatus, xhr) {
    // Clear the file object and file list
    fileObj = new Object;
    $('#uploadqueue').empty();
    window.location.href = '/Report/Done';
}

function uploadFailed(xhr, textStatus, errorThrown) {
    DeactivateFileUploadOverlay();
    var err = "Error : " + status + " " + errorThrown + " " + xhr.responseText;
    showError(xhr);
}

function showError(xhr) {
    var errorField = $('#fileUpload p.errors');
    var errorText = xhr.responseText;
    errorField.html(errorText);
    errorField.show();
}

// Client side file processing
function deleteFileFromList(element) {
    var fileId = $(element).parents('tr').attr('id');
    $("#" + fileId).remove();
    delete fileObj[fileId];
}

function processFileSelection(event, files) {
    for (var x = 0; x < files.length; x++) {
        var counter = 0;
        // Compare files for duplicates
        for (var key in fileObj) {
            if (fileObj.hasOwnProperty(key) && compareFilesForDuplicate(fileObj[key], files[x])) {
                counter++;
            }
        }
        
        // Only process the files when no duplicates are found
        if (counter < 1) {
            // Instead of fancy counting systems, simply increase a variable every file that's added for simplicity.
            var fileKey = "file-" + listCount;
            listCount++;

            // Add the file to object as property
            fileObj[fileKey] = files[x];

            appendTableWithFileData(fileKey, files[x], "#uploadqueue");
        }
    }
    // Reset the form field so the upload button in empty again
    $(event.target).parents('form').trigger('reset');
}

function appendTableWithFileData(fileKey, file, table) {
    // Generate new table row for the added file
    var row = $('<tr></tr>', {
        id: fileKey
    });

    $('<td class="filePreview"></td>').html('<img src="' + URL.createObjectURL(file) + '">').appendTo(row);
    $('<td class="fileName"></td>').html(file.name).appendTo(row);
    $('<td class="fileRemove"></td>').html('<button type="submit" onclick="deleteFileFromList(this)"><img src="/Content/cross.svg" /></button>').appendTo(row);

    row.appendTo(table);
}

function ActivateFileUploadOverlay() {
    var overlay = $('#fileUpload #overlay');
    overlay.css('height', overlay.parents('fieldset').height() - 40);
    overlay.show();
}

function DeactivateFileUploadOverlay() {
    var overlay = $('#fileUpload #overlay');
    overlay.hide();
}

// Utilities
function objectHasproperties(object) {
    for (var prop in object) {
        if (object.hasOwnProperty(prop)) {
            return true;
        }
    }
    return false;
}

function compareFilesForDuplicate(file1, file2) {
    return file1.size === file2.size && file1.lastModified === file2.lastModified;
}