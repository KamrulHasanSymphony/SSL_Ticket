var TaskController = function (TaskService) {
    
    var init = function () {
        
        $(".btnSave").on("click", function (e) {
            e.preventDefault();

            var isValid = true;

            var title = $("#Title").val().trim();
            if (title === "") {
                isValid = false;
                $("#Title").addClass("is-invalid");
                $("#titleError").text("Title is required").show();
            } else {
                $("#Title").removeClass("is-invalid");
                $("#titleError").text("").hide();
            }

            var taskType = $("#cmbTaskType").val().trim();
            if (taskType === "" || taskType === 0) {
                isValid = false;
                $("#cmbTaskType").addClass("is-invalid");
                $("#taskTypeError").text("Task Type is required").show();
            } else {
                $("#cmbTaskType").removeClass("is-invalid");
                $("#taskTypeError").text("").hide();
            }

            var startTime = $("#txtStartTime").val().trim();
            debugger;
            if (startTime === "") {
                isValid = false;
                $("#txtStartTime").addClass("is-invalid");
                $("#startTimeError").text("Due Date is required").show();
            } else {
                $("#txtStartTime").removeClass("is-invalid");
                $("#startTimeError").text("").hide();
            }

            if (!isValid) {
                ShowNotification(2, "Please complete the form. Not proceeding to Save");
                return;
            }            

            
            var status = $("#cmbStatus").val();
            
            if (status == 2) {
                
                var grid = $("#kTimeGrid").data("kendoGrid");
                var data = grid.dataSource.view();
                if (data.length > 0 && data[0].startStatus === "S") {
                    ShowNotification(2, "Please complete Task Time.");
                    return;
                }
            }
            if (status == 3) {
                debugger;
                var grid = $("#kTimeGrid").data("kendoGrid");
                var data = grid.dataSource.view();
                if (data.length > 0 && data[0].startStatus !== "H") {
                    ShowNotification(2, "Please Hold Task Time.");
                    return;
                }
            }

            var tId = $("#T_TicketId").val();
            $("#T_TicketId").val(tId);
            var cd = $("#Code").val();
            $("#Code").val(cd);
            
            $("#T_SourceId").val($("#cmbSource").data("kendoComboBox").value());
            
            $("#T_TopicId").val($("#cmbTopic").data("kendoComboBox").value());
            
            
            $("#T_PriorityId").val($("#cmbPriority").data("kendoComboBox").value());
            $("#T_StatusId").val($("#cmbStatus").data("kendoComboBox").value());
            $("#DepartmentId").val($("#cmbDepartment").data("kendoComboBox").value());
            $("#TaskTypeId").val($("#cmbTaskType").data("kendoComboBox").value());
            
            $("#StartDate").val($("#txtStartDate").val());            

            $("#StartTime").val($("#txtStartTime").val());

            var description = $("#editor").data("kendoEditor").value();
            $("#Description").val(description);
            $("#ProgressInPercent").val($("#txtPercent").val());
            $("#RequiredTime").val($("#txtRequiredTime").val());
            Save();
        })

        $("#Title").on("input change", function () {
            $("#titleError").text("").hide();
        });
        $("#cmbTaskType").data("kendoComboBox").bind("change", function () {
            $("#taskTypeError").text("").hide();
        });
    }
    function saveFail(result) {
        console.log(result);
        ShowNotification(3, result.message);
    }
    function Save() {
        
        var validator = $("#frm_Task").validate();

        if (!validator.form()) {
            ShowNotification(2, "Please complete the form");
            return;
        }

        var form = $("#frm_Task")[0];
        var formData = new FormData(form);
        var tId = $("#T_TicketId").val();
        var code = $("#Code").val();
        formData.append('T_TicketId', tId);
        formData.append('Code', code);
        var files = $('#fileToUpload')[0].files;
        for (var i = 0; i < files.length; i++) {
            formData.append('Attachments', files[i]);
        }


        TaskService.save(formData, saveDone, saveFail);

    }

    function saveDone(result) {
        
        if (result.message == "You are not Authorized!") {
                toastr.options = {
                    positionClass: 'toast-center', // Custom position class to center the toast
                    toastClass: 'toastr-center'
                };
                toastr.warning(result.message);
        }
        addListItem(result);
        
        if (result.status == "200") {
            
            if (result.data.operation == "add") {
                console.log(result)

                ShowNotification(1, result.message);
                $(".btnSave").html('Update');
                $(".btnsave").addClass('sslUpdate');

                $("#Id").val(result.data.id);
                $("#Code").val(result.data.code);
                result.data.operation = "update";

                $("#Operation").val(result.data.operation);
                $("#TeamValue").val(result.data.teamId);

                //change

                //$(".btnSave").addClass('sslUpdate');
                $("#divUpdate").show();
                $("#divSave").hide();
                $("#SavePost").show();
                // Redirect to Edit page
                const editUrl = '/Task/Edit/' + result.data.id;
                window.location.href = editUrl;
                
                //var grid = $("#kTaskGrid").data("kendoGrid");
                //grid.dataSource.read();
                //end
                //detailIssueTable.ajax.url(getIssueIndexURL());
                //detailFeedbackTable.ajax.url(getIssueFeedIndexURL());
                //detailTable.ajax.url(getAreaIndexURL());
                //auditUserTable.ajax.url(getUserIndexURL());
                //showSections();
                //auditUserTable.draw();

            } else {
                ShowNotification(1, result.message);
                //change
                $("#divSave").hide();
                $("#divUpdate").show();
                //end
            }
        }
        else if (result.status == "400") {
            //ShowNotification(3, "Something gone wrong");
            ShowNotification(3, result.message || result.error);
        }
    }

    function saveFail(result) {
        console.log(result);
        //ShowNotification(3, "Something gone wrong");
        ShowNotification(3, result.message);
    }

    function addListItem(result) {
        var list = $(".fileGroup");

        result.data.attachmentsList.forEach(function (item) {

            var item = '<li class="list-group-item" id="file-' + item.id + '"> <span>' +
                item.displayName +
                '</span><a target="_blank" href="/Task/DownloadFile?filePath=' + item.fileName + '" class=" ml-2 btn btn-primary btn-sm float-right ml-2">Download</a>' +
                '<button onclick="TaskController.deleteFile(\'' + item.id + '\', \'' + item.fileName + '\')" class=" ml-2 btn btn-danger btn-sm float-right" type="button">Delete</button>' +
                '</li>';

           // list.append(item);
        });
    }
    var deleteFile = function deleteFile(fileId, filePath) {

        TaskService.deleteFile({ id: fileId, filePath: filePath }, (result) => {


            if (result.status == "200") {
                $("#file-" + fileId).remove();

                ShowNotification(1, result.message);
            }
            else if (result.status == "400") {
                ShowNotification(3, result.message);
            }



        }, saveFailDelete);

    };
    function saveFailDelete(result) {
        console.log(result);
        ShowNotification(3, "Something gone wrong");
    }
    return {
        init: init,
        deleteFile
    }

}(TaskService);