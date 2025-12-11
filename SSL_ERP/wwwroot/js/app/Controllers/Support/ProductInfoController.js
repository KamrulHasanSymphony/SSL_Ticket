var ProductInfoController = function (ProductInfoService) {
    var init = function () { 
        $('.btnsave').click('click', function () {
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
        var validator = $("#frm_Product").validate();
        var companyInfo = serializeInputs("frm_Product");

        var result = validator.form();
        //if (!result) {
        //    validator.focusInvalid();
        //    return;
        //}

        ProductInfoService.save(companyInfo, saveDone, saveFail);
    }
    function saveDone(result) {

        if (result.status == "200") {
            if (result.data.operation == "add") {
                
                ShowNotification(1, result.message);
                debugger;
                $(".btnsave").html('Update');
                $(".btnsave").addClass('sslUpdate');
                $("#Id").val(result.data.id);
                $("#Name").val(result.data.name);                

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
        ShowNotification(3, "Cannot insert another topic.");
    }
    return {
        init: init
    }

}(ProductInfoService);