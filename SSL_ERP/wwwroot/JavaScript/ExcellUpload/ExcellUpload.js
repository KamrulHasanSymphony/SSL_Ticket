$(document).ready(function () {

    // Open file dialog on click
    $('#btnExcelUpload').on('click', function () {
        $('#excelFile').click();
    });

    // Display selected file name
    $('#excelFile').on('change', function (e) {
        const file = e.target.files[0];
        if (file) {
            $('#selectedFileName').text("Selected File: " + file.name);
            $('#btnUploadSubmit').prop('disabled', false);
        } else {
            $('#selectedFileName').text("");
            $('#btnUploadSubmit').prop('disabled', true);
        }
    });

    // Handle Upload via AJAX
    $('#btnUploadSubmit').on('click', function () {
        const fileInput = $('#excelFile')[0];
        if (!fileInput.files.length) {
            alert("Please select an Excel file first.");
            return;
        }

        const formData = new FormData();
        formData.append("file", fileInput.files[0]);

        $.ajax({
            url: '/ExcellUpload/UploadExcelFile',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                alert("File uploaded successfully!");
            },
            error: function (xhr, status, error) {
                alert("Error uploading file: " + error);
            }
        });
    });

});