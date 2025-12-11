var TodayTaskSummaryController = function (TodayTaskSummaryService) {
    var init = function () {
        debugger;
        $("#idDetails").kendoEditor({
            tools: [
                "bold", "italic", "underline",
                "strikethrough", "justifyLeft", "justifyCenter", "justifyRight",
                "insertUnorderedList", "insertOrderedList",
                "createLink", "unlink",
                "formatting", "cleanFormatting",
                "fontName", "fontSize", "backColor"
            ]
        });

        $("#txtDate").kendoDatePicker({
            format: "dd-MMM-yyyy",     // display format (you can change)
            parseFormats: ["dd/MM/yyyy", "yyyy-MM-dd", "dd-MMM-yyyy"],
            value: new Date()          // optional: set today's date
        });
        $('.btnsave').click('click', function () {
            save();
        });

        $("#indexSearch").click(function () {
            indexTable.draw();
        });

        $(".chkAll").click(function () {
            $('.dSelected:input:checkbox').not(this).prop('checked', this.checked);
        });

        $('#singlePost').click(function () {
            debugger;
            // Get the single selected ID
            var selectedId = $("#Id").val();
            if (!selectedId) {
                ShowNotification(3, "No task selected.");
                return;
            }

            var poRequesitionMaster = serializeInputs("frm_todaytasksummarry");

            if (poRequesitionMaster.IsPost === true) {
                ShowNotification(3, "Data has already been Posted.");
            } else {
                poRequesitionMaster.IDs = selectedId; // assign single ID
                MultiplePostAjax(poRequesitionMaster, MultiplePost, MultiplePostFail);
            }

        });


    }
    var MultiplePostAjax = function (masterObj, done, fail) {

        $.ajax({
            url: '/TodayTaskSummary/MultiplePost',
            method: 'post',
            data: masterObj

        })
            .done(done)
            .fail(fail);


    };
    function MultiplePost(result) {
        console.log(result.message);

        if (result.status == "200") {
            ShowNotification(1, result.message);
            $("#singlePost").hide();
            debugger;
            $("#IsPost").val(true);
            if (result.IsPost != null) {
                $(".PostedBy").val(result.postedBy);
                $(".PostedOn").val(result.postedOn);
            }
            debugger;
            Visibility(true);

            $(".divSave").hide();
            $(".addNewButton").show();
            $(".divUpdate").show();
            $(".sslUpdate").hide();
            




        }
        else if (result.status == "400") {
            ShowNotification(3, result.message);
        }
        else if (result.status == "199") {
            ShowNotification(3, result.message);
        }
    }
    function MultiplePostFail(result) {

        ShowNotification(3, "Something gone wrong");

    }
    function save() {
        debugger;

        var validator = $("#frm_todaytasksummarry").validate();
        var task = serializeInputs("frm_todaytasksummarry");

        // ✔ Get JavaScript Date object
        var dateObj = $("#txtDate").data("kendoDatePicker").value();

        // ✔ Convert to required format: yyyy-MM-dd
        var date = kendo.toString(dateObj, "yyyy-MM-dd");

        // ✔ Get Kendo Editor details
        var details = $("#idDetails").data("kendoEditor").value();
        var operation = $("#Operation").val();
        debugger;
        var id = $("#Id").val();
        if (id !== 0 || id !== null) {
            task.Id = id;
        }
        task.Operation = operation;
        task.Date = date;
        task.Details = details;


        TodayTaskSummaryService.save(task, saveDone, saveFail);
    }


    function saveDone(result) {

        if (result.status == "200") {
            if (result.data.operation == "add") {

                ShowNotification(1, result.message);
                debugger;
                $(".btnsave").html('Update');
                $(".btnsave").addClass('sslUpdate');
                $("#Id").val(result.data.id);


                result.data.operation = "update";
                $("#Operation").val(result.data.operation);

            } else {
                ShowNotification(1, result.message);
            }
            setTimeout(function () {
                window.location.href = "/TodayTaskSummary/Index";
            }, 800);
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

}(TodayTaskSummaryService);