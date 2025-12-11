var CompanyInfoController = function ( CompanyInfoService) {

    var init = function () {

        //var indexConfig = GetIndexTable();
        //var indexTable = $("#BkTransfersLists").DataTable(indexConfig);

        //var indexTable = BpTable();


        $("#btnAdd").on("click", function () {

            rowAdd(detailTable);

        });

        $('#Post').on('click', function () {

            SelectData(true);

        });

    

        $("#ModalButtonCloseFooter").click(function () {
            addPrevious(detailTable);
        });


        $("#ModalButtonCloseHeader").click(function () {
            addPrevious(detailTable);
        });


       



        $('.btnsave').click('click', function () {
            debugger;
            save();
        });

        $("#indexSearch").click(function () {
            indexTable.draw();
        });

       

        $(".chkAll").click(function () {
            $('.dSelected:input:checkbox').not(this).prop('checked', this.checked);
        });


    }
    

    function save() {
        debugger;
        var validator = $("#frm_Company").validate();
        var companyInfo = serializeInputs("frm_Company");

        var result = validator.form();
        //if (!result) {
        //    validator.focusInvalid();
        //    return;
        //}

        CompanyInfoService.save(companyInfo, saveDone, saveFail);
    }
    function saveDone(result) {

        if (result.status == "200") {
            if (result.data.operation == "add") {
                ShowNotification(1, result.message);
                $(".btnsave").html('Update');
                $(".btnsave").addClass('sslUpdate');
                $("#CompanyID").val(result.data.companyID);
                $("#Code").val(result.data.code);
                result.data.operation = "update";
                $("#Operation").val(result.data.operation);

            } else {
                ShowNotification(1, result.message);
            }
        }
        else if (result.status == "400") {
            ShowNotification(3, result.error); // <-- display the error message here
        }
        else if (result.status == "199") {
            ShowNotification(2, result.message); // <-- display the error message here
        }
    }

    function saveFail(result) {
        console.log(result);
        ShowNotification(3, "Cannot insert another company.");
    }









    return {
        init: init
    }


}(CompanyInfoService);