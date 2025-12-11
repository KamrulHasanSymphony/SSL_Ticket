var TicketController = function (TicketService) {



    var init = function () {

        $(".btnSave").on("click", function (e) {
            e.preventDefault();
            debugger;
            var isValid = true;

            var title = $("#Title").val().trim();
            if (title === "") {
                isValid = false;
                $("#Title").addClass("is-invalid"); // Add a red border (Bootstrap style)
                $("#titleError").text("Title is required").show(); // Show error message
            } else {
                $("#Title").removeClass("is-invalid");
                $("#titleError").text("").hide();
            }

            var stackHolder = $("#cmbStackHolder").val().trim();
            if (stackHolder === "" || stackHolder === 0) {
                isValid = false;
                $("#StackHolderUserId").addClass("is-invalid");
                $("#stackHolderError").text("Stackholder is required").show();
            } else {
                $("#StackHolderUserId").removeClass("is-invalid");
                $("#stackHolderError").text("").hide();
            }

            var client = $("#cmbClient").val().trim();
            if (client === "" || client === 0) {
                isValid = false;
                $("#T_ClientId").addClass("is-invalid");
                $("#clientError").text("Client is required").show();
            } else {
                $("#T_ClientId").removeClass("is-invalid");
                $("#clientError").text("").hide();
            }

            var topic = $("#cmbTopic").val().trim();
            if (topic === "" || topic === 0) {
                isValid = false;
                $("#T_TopicId").addClass("is-invalid");
                $("#topicError").text("Topic is required").show();
            } else {
                $("#T_TopicId").removeClass("is-invalid");
                $("#topicError").text("").hide();
            }

            var department = $("#cmbDepartment").val().trim();
            if (department === "" || department === 0) {
                isValid = false;
                $("#DepartmentId").addClass("is-invalid");
                $("#departmentError").text("Department is required").show();
            } else {
                $("#DepartmentId").removeClass("is-invalid");
                $("#departmentError").text("").hide();
            }

            var priority = $("#cmbPriority").val().trim();
            if (priority === "" || priority === 0) {
                isValid = false;
                $("#T_PriorityId").addClass("is-invalid");
                $("#priorityError").text("Priority is required").show();
            } else {
                $("#T_PriorityId").removeClass("is-invalid");
                $("#priorityError").text("").hide();
            }

            var status = $("#cmbStatus").val().trim();
            debugger;
            if (status === "" || status === 0) {
                isValid = false;
                $("#T_StatusId").addClass("is-invalid");
                $("#statusError").text("Status is required").show();
            } else {
                $("#T_StatusId").removeClass("is-invalid");
                $("#statusError").text("").hide();
            }

            var source = $("#cmbTktSource").val().trim();
            if (source === "" || source === 0) {
                isValid = false;
                $("#T_SourceId").addClass("is-invalid");
                $("#sourceError").text("Source is required").show();
            } else {
                $("#T_SourceId").removeClass("is-invalid");
                $("#sourceError").text("").hide();
            }

            var dueDate = $("#txtEndDate").val().trim();
            debugger;
            if (dueDate === "") {
                isValid = false;
                $("#txtEndDate").addClass("is-invalid");
                $("#dueDateError").text("Due Date is required").show();
            } else {
                $("#txtEndDate").removeClass("is-invalid");
                $("#dueDateError").text("").hide();
            }

            var product = $("#cmbTktProducts").val().trim();
            if (product === "" || product === 0) {
                isValid = false;
                $("#T_ProductId").addClass("is-invalid");
                $("#productError").text("Product is required").show();
            } else {
                $("#T_ProductId").removeClass("is-invalid");
                $("#productError").text("").hide();
            }

            var ticketType = $("#cmbTicketType").val().trim();
            if (ticketType === "" || ticketType === 0) {
                isValid = false;
                $("#TicketTypeId").addClass("is-invalid");
                $("#ticketTypeError").text("Ticket Type is required").show();
            } else {
                $("#TicketTypeId").removeClass("is-invalid");
                $("#ticketTypeError").text("").hide();
            }

            // If validation fails, STOP execution
            if (!isValid) {
                ShowNotification(2, "Please complete the form. Not proceeding to Save");
                return;
            }

            //var TicketStackHolder = $("#cmbStackHolder").data("kendoComboBox").value();
            $("#StackHolderUserId").val($("#cmbStackHolder").data("kendoMultiColumnComboBox").value());

            $("#T_ClientId").val($("#cmbClient").data("kendoComboBox").value());

            $("#T_SourceId").val($("#cmbTktSource").data("kendoComboBox").value());

            $("#T_ProductId").val($("#cmbTktProducts").data("kendoComboBox").value());

            $("#CreateDate").val($("#txtStartDate").val());

            $("#DueDate").val($("#txtEndDate").val());
            $("#T_TopicId").val($("#cmbTopic").data("kendoComboBox").value());
            $("#T_PriorityId").val($("#cmbPriority").data("kendoComboBox").value());
            $("#T_StatusId").val($("#cmbStatus").data("kendoComboBox").value());
            $("#DepartmentId").val($("#cmbDepartment").data("kendoComboBox").value());
            $("#TicketTypeId").val($("#cmbTicketType").data("kendoComboBox").value());

            var description = $("#editor").data("kendoEditor").value();
            $("#Description").val(description);
            Save();
        });

        $("#Title").on("input change", function () {
            $("#titleError").text("").hide();
        });        
        $("#cmbStackHolder").data("kendoMultiColumnComboBox").bind("change", function () {
            $("#stackHolderError").text("").hide();
        });

        $("#cmbClient").data("kendoComboBox").bind("change", function () {            
            $("#clientError").text("").hide();
        });
        $("#cmbTopic").on("input change", function () {
            $("#topicError").text("").hide();
        });

        $("#cmbDepartment").on("input change", function () {
            $("#departmentError").text("").hide();
        });

        $("#cmbPriority").on("input change", function () {
            $("#priorityError").text("").hide();
        });

        $("#cmbStatus").on("input change", function () {
            $("#statusError").text("").hide();
        });

        $("#cmbTktSource").on("input change", function () {
            $("#sourceError").text("").hide();
        });

        $("#txtEndDate").on("input change", function () {
            debugger;
            $("#dueDateError").text("").hide();
        });

        $("#cmbTktProducts").on("input change", function () {
            $("#productError").text("").hide();
        });

        $("#cmbTicketType").on("input change", function () {
            $("#ticketTypeError").text("").hide();
        });

    }

    function saveFail(result) {
        console.log(result);
        ShowNotification(3, result.message);
    }
    function Save() {
        debugger;
        var validator = $("#frm_Ticket").validate();

        if (!validator.form()) {
            ShowNotification(2, "Please complete the form");
            return;
        }

        var masterObj = $("#frm_Ticket").serialize();
        masterObj = queryStringToObj(masterObj);

        TicketService.save(masterObj, saveDone, saveFail);

    }

    function saveDone(result) {
        if (result.status == "200") {
            if (result.data.operation == "add") {
                console.log(result)

                ShowNotification(1, result.message);
                $(".btnSave").html('Update');
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


                //end

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

    return {
        init: init
    }

}(TicketService);