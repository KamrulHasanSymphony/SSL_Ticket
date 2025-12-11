var UserProfileController = function (CommonService,UserProfileService) {

    var init = function () {

        var i = 90;
        //NewAddingForBranch
        if ($("#BranchID").length) {
            //LoadCombo("BranchID", '/Common/BranchName');
            LoadCombo("BranchID", '/Common/BranchName?UserId=' + $('#Id').val());

        }


        /*var indexTable = BpTable();*/

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

        $('.btn-SageUserName').on('click', function () {
            var originalRef = $(this);
            CommonService.sageUserNameModal({}, fail, function (row) { modalUserIdDblClick(row, originalRef) });
        });  
        
        $('.btnsave').click('click', function () {
            
            $("#ClientId").val($("#cmbClient").data("kendoComboBox").value());
            save();
        });

        $("#indexSearch").click(function () {
            indexTable.draw();
        }); 
        
        $(".chkAll").click(function () {
            $('.dSelected:input:checkbox').not(this).prop('checked', this.checked);
        });

        // Click event handler for the verification button
        $("#btnvarified").click(function () {
            var userId = $("#UserId").val();
            var verification = $("#VerificationCode").val();

            IsVerified(userId, verification, function (data) {
                if (data === true) {
                    toastr.options = {
                        positionClass: 'toast-center',
                        toastClass: 'toastr-center',
                        closeButton: true,
                        timeOut: 3000
                    };
                    toastr.success("You are verified");
                    setTimeout(function () {
                        window.location.href = '/Login/LogOff/';
                    }, 3000);
                } else {
                    toastr.options = {
                        closeButton: true,
                        timeOut: 3000
                    };
                    toastr.warning("Verification Code is Incorrect!");
                }
            });
        });

        // Function to verify user based on userId and verification code
        function IsVerified(userId, verification, callback) {
            $.ajax({
                url: '/UserProfile/GetVerifiedById',
                method: 'GET',
                data: { userId: userId, verification: verification },
                success: function (response) {
                    callback(!!response); // Ensure response is a boolean
                },
                error: function (xhr, status, error) {
                    console.error('Error fetching data:', error);
                    callback(false); // Handle error case
                }
            });
        }


        var client = new kendo.data.DataSource({
            transport: {
                read: {
                    url: "/Task/GetAllClient",
                    dataType: "json"
                }
            }
        });

        $("#cmbClient").kendoComboBox({
            dataTextField: "name",
            dataValueField: "id",
            dataSource: client,
            filter: "contains",
            suggest: true
        });


    }


    //DeActive

    $('.btnIsActive').click('click', function () {
        Confirmation("Are you sure? Do You Want to DeActive Data?", function (result) {
            console.log(result);
            if (result) {

                
                var form = $("#frm_user")[0];
                var formData = new FormData(form);

                var Id = formData.get("Id");
                formData.append("UserId", Id);

                UserProfileService.DeActiveUser(formData, deActiveUserDone, deActiveUserFail);

            }
        });

    });
    function deActiveUserDone(result) {
        if (result.status == "200") {
            ShowNotification(1, result.message);
        }
        else if (result.status == "400") {

            ShowNotification(3, result.message);
        }
    }
    function deActiveUserFail(result) {
        console.log(result);
        ShowNotification(3, result.message);
    }

    //Active

    $('.btnActiveIsActive').click('click', function () {
        Confirmation("Are you sure? Do You Want to Active Data?", function (result) {
            console.log(result);
            if (result) {

                
                var form = $("#frm_user")[0];
                var formData = new FormData(form);

                var Id = formData.get("Id");
                formData.append("UserId", Id);

                UserProfileService.ActiveUser(formData, ActiveUserDone, ActiveUserFail);

            }
        });

    });

    function ActiveUserDone(result) {
        if (result.status == "200") {
            ShowNotification(1, result.message);
        }
        else if (result.status == "400") {

            ShowNotification(3, result.message);
        }
    }

    function ActiveUserFail(result) {
        console.log(result);
        ShowNotification(3, result.message);
    }

    

    function modalUserIdDblClick(row, originalRow) {

        var userId = row.find("td:first").text();
        

        originalRow.closest("div.input-group").find("input").val(userId);
        originalRow.closest("div.input-group").find("input").focus();

        $("#sageUserNameModal").modal("hide");

    }

    function fail(err) {

        ShowNotification(3, "Something gone wrong");
    }

    function save() {
        var validator = $("#frm_user").validate();
        var companyInfo = serializeInputs("frm_user");

        
        var form = $("#frm_user")[0];
        var UP = new FormData(form);


        //var result = validator.form();
        //if (!result) {
        //    validator.focusInvalid();
        //    return;
        //}  
        //UserProfileService.save(companyInfo, saveDone, saveFail);

        UserProfileService.save(UP, saveDone, saveFail);
    }

    function saveDone(result) {

        if (result.status == "200") {
            if (result.data.operation == "add") {
                
                ShowNotification(1, result.message);
                $(".btnsave").html('Update');
                $(".btnsave").addClass('sslUpdate');
                $("#Id").val(result.data.id);
                $("#Code").val(result.data.code);
                result.data.operation = "update";
                $("#Operation").val(result.data.operation);

                //setTimeout(function () {
                //    window.location.href = "/UserProfile/Index";
                //}, 3000);
                setTimeout(function () {
                    window.location.href = "/UserProfile/Varification?id=" + result.data.userId;
                }, 2000);
             
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
        else if (result.status == "0") {
            ShowNotification(2, result.message); // <-- display the error message here
        }

    }


    function saveFail(result) {
        console.log(result);
        ShowNotification(3, "Something Gone Wrong");
    }

    return {
        init: init
    }

}(CommonService, UserProfileService);
   