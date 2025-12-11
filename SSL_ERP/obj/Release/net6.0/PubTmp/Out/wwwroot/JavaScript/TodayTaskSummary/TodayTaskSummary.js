$(document).ready(function () {
    var gridDataSource = new kendo.data.DataSource({
        type: "json",
        serverPaging: true,
        serverSorting: true,
        serverFiltering: true,
        allowUnsort: true,
        autoSync: true,
        pageSize: 10,
        transport: {
            read: {
                url: "/TodayTaskSummary/GetGridData",
                type: "POST",
                dataType: "json",
                cache: false
            },
            parameterMap: function (options) {
                return options;
            }
        },
        batch: true,
        schema: {
            data: "items",
            total: "totalCount",
            model: {

            }
        }
    });

    $("#taskSummaryGrid").kendoGrid({
        dataSource: gridDataSource,
        pageable: {
            refresh: true,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true,
            pageSizes: [10, 20, 50, "all"]
        },
        noRecords: true,
        messages: {
            noRecords: "No Record Found!"
        },
        scrollable: true,
        filterable: {
            extra: true,
            operators: {
                string: {
                    startswith: "Starts with",
                    endswith: "Ends with",
                    contains: "Contains",
                    doesnotcontain: "Does not contain",
                    eq: "Is equal to",
                    neq: "Is not equal to",
                    gt: "Is greater then",
                    lt: "Is less then"
                }
            }
        },
        sortable: true,
        resizable: true,
        reorderable: true,
        groupable: true,
        toolbar: ["excel", "pdf", "search"],
        excel: {
            fileName: "TodayTaskSummary.xlsx",
            filterable: true
        },
        search: {
            fields: ["date", "details", "createdBy"]
        },
        columns: [
            //{
            //    selectable: true, width: 50
            //},
            {
                title: "Action",
                width: 60,
                template: function (dataItem) {

                    var today = new Date();
                    today.setHours(0, 0, 0, 0);

                    // Parse the grid date column
                    var rowDate = kendo.parseDate(dataItem.date);
                    rowDate.setHours(0, 0, 0, 0);

                    // If date is past -> disable edit
                    if (rowDate.getTime() < today.getTime()) {
                        return "<button class='btn btn-secondary btn-sm' disabled>" +
                            "<i class='fas fa-pencil-alt'></i>" +
                            "</button>";
                    }

                    // Allowed -> show enabled Edit button
                    return "<a href='/TodayTaskSummary/Edit/" + dataItem.id +
                        "' class='btn btn-primary btn-sm edit'>" +
                        "<i class='fas fa-pencil-alt'></i>" +
                        "</a>";
                }
            },


            {
                field: "id", hidden: true, sortable: true
            },
            {
                field: "date", title: "Date", sortable: true, template: '#= kendo.toString(kendo.parseDate(date), "dd-MMM-yyyy") #', filterable: {
                    ui: "datepicker"
                }
            },
            {
                field: "details",
                title: "Details",
                sortable: true,
                encoded: false
            },
            {
                field: "createdBy",
                title: "Created By",
                sortable: true
            },
            {
                field: "createdOn",
                title: "Created On",
                sortable: true,
                template: '#= kendo.toString(kendo.parseDate(createdOn), "dd-MMM-yyyy") #',
                hidden: true
            },
            {
                field: "createdFrom",
                title: "Created From",
                sortable: true,
                hidden: true
            },
            {
                field: "isPost", title: "Is Post", hidden: true, sortable: true
            }

        ],
        editable: false,
        selectable: "row",
        navigatable: true,
        columnMenu: true
    });

    $('#PostPO').click(function () {
        debugger;
        var grid = $("#taskSummaryGrid").data("kendoGrid");
        var selectedRows = grid.select(); // get selected rows
        var selectedIds = [];

        selectedRows.each(function () {
            var dataItem = grid.dataItem(this);
            selectedIds.push(dataItem.id);
        });

        if (selectedIds.length === 0) {
            ShowNotification(3, "Please select at least one task.");
            return;
        }

        var poRequesitionMaster = serializeInputs("frm_todaytasksummarry");

        if (poRequesitionMaster.IsPost == true) {
            ShowNotification(3, "Data has already been Posted.");
        } else {
            poRequesitionMaster.IDs = selectedIds;
            MultiplePostAjax(poRequesitionMaster, MultiplePost, MultiplePostFail);
        }

    });


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
            var grid = $('#taskSummaryGrid').data('kendoGrid');
            if (grid) {
                grid.dataSource.read();
            }
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
            $("#PostPO").hide();

            

            
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

    var product = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Product/GetAllProductsData",
                dataType: "json"
            }
        }
    });
    $(".kTextbox").each(function () {
        $(this).addClass("k-textbox");
        $(this).css("width", "100%");
    });

    //Place-Holder;
    $('.kLabel4').each(function () {
        var label = $(this).text().trim();
        var input = $(this).next('.kInput,.kInput8').find('input, textarea, select');
        input.attr('placeholder', label);
    });

    //Flex generic
    $(".kLabel").each(function () {
        $(this).addClass("col-md-2 col-lg-2 col-sm-2");
    });
    $(".kInput").each(function () {
        $(this).addClass("col-md-4 col-lg-4 col-sm-4");
    });
    $(".kLabel4").each(function () {
        $(this).addClass("col-md-4 col-lg-4 col-sm-4");
    });
    $(".kLabel3").each(function () {
        $(this).addClass("col-md-3 col-lg-3 col-sm-3");
    });
    $(".kLabel2").each(function () {
        $(this).addClass("col-md-2 col-lg-2 col-sm-2");
    });
    $(".kInput8").each(function () {
        $(this).addClass("col-md-8 col-lg-8 col-sm-8");
    });

    //Red Underline for validation
    $('input.required').each(function () {
        $(this).css('border-bottom', '1px solid red');
    });
    $('.required>.k-dropdown-wrap > .k-input').each(function () {
        $(this).css('border-bottom', '1px solid red');
    });
});