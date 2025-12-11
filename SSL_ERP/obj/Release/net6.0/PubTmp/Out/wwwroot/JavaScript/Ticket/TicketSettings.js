$(document).ready(function () {    

    TicketSummaryHelper.InitTicketSummary();
});

var TicketSummaryManager = {
    gridDataSource: function (AssigneeUserId) {
        var gridDataSource = new kendo.data.DataSource({
            type: "json",
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            allowUnsort: true,
            autoSync: true,
            batch: true,
            pageSize: 10,
            transport: {
                read: {
                    url: "/Ticket/GetAllTicketMData",
                    type: "POST",
                    dataType: "json",
                    cache: false,
                    data: { AssigneeUserId: AssigneeUserId }

                },
                parameterMap: function (options) {
                    if (options.filter && options.filter.filters) {
                        options.filter.filters.forEach(function (filter) {
                            if (filter.field === "createDate" || filter.field === "dueDate" ){
                                filter.value = kendo.toString(filter.value, "yyyy-MM-dd HH:mm:ss");
                            }
                        });
                    }
                    return options;
                }

            },
            batch: true,
            schema: {
                data: "items",
                total: "totalCount",
                model: {
                    id: "id",
                    fields: {
                        createDate: { type: "date" },
                        dueDate: { type: "date" }
                        
                    }
                }
            }
        });
        return gridDataSource;
    },
    gridNoteDataSource: function (Id) {
        var gridDataSource = new kendo.data.DataSource({
            type: "json",
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            allowUnsort: true,
            autoSync: true,
            batch: true,
            pageSize: 10,
            transport: {
                read: {
                    url: "/Ticket/GetAllEnternalNoteData",
                    type: "POST",
                    dataType: "json",
                    cache: false,
                    data: { Id: Id }
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
                    id: "id",
                    fields: {

                    }
                }
            }
        });
        return gridDataSource;
    },
    GetinternalDataById: function (id, callback) {
        $.ajax({
            url: '/Ticket/GetInternalById',
            method: 'get',
            data: { id: id },
            success: function (response) {

                callback(response);
            },
            error: function (xhr, status, error) {
                console.error('Error fetching Vendor:', error);
            }

        });
    },
    SaveEnterNalNotes: function (event) {
        event.preventDefault();
        var objNote = TicketSummaryHelper.CreateNoteObject();
        $.ajax({
            url: '/Ticket/SaveEnternalNotes', // Adjust the URL according to your routing
            method: 'post',
            data: objNote,
            success: function () {
                debugger;
                var grid = $("#kENotesTktGrid").data("kendoGrid");
                grid.dataSource.read();
                toastr.options = {
                    positionClass: 'toast-center',
                    toastClass: 'toastr-center'
                };
                toastr.success("Save Successfully!");
            },
            error: function (xhr, status, error) {
                console.log("Error Response:", xhr.responseText);
                toastr.options = {
                    positionClass: 'toast-center', // Custom position class to center the toast
                    toastClass: 'toastr-center'
                };
                toastr.warning("Error: " + error);
            }
        });
    },
    SaveEnterNalActive: function (event) {
        event.preventDefault();
        var objActNote = TicketSummaryHelper.CreateNoteActiveObject();
        $.ajax({
            url: '/Ticket/SaveEnternalActiveNotes',
            method: 'post',
            data: objActNote,
            success: function () {
                var grid = $("#kENotesTktGrid").data("kendoGrid");
                grid.dataSource.read();
                toastr.options = {
                    positionClass: 'toast-center', // Custom position class to center the toast
                    toastClass: 'toastr-center'
                };
                toastr.success("Save Successfully!");
            },
            error: function (xhr, status, error) {
                console.log("Error Response:", xhr.responseText);
                toastr.options = {
                    positionClass: 'toast-center', // Custom position class to center the toast
                    toastClass: 'toastr-center'
                };
                toastr.warning("Error: " + error);
            }
        });
    },

}
var TicketSummaryHelper = {
    InitTicketSummary: function () {

        TicketSummaryHelper.GenerateTicketeGrid();
        TicketSummaryHelper.GenerateEnternalNotesGrid();
        TicketSummaryHelper.GenerateTicketeStyle();
        TicketSummaryHelper.GenerateKendoComboBox();
        TicketSummaryHelper.GenerateKendoTextBox();
        TicketSummaryHelper.GenerateKendoDatePicker();
        TicketSummaryHelper.GenerateKendoTextArea();
        TicketSummaryHelper.ToogleGenerate();
        TicketSummaryHelper.GenerateModal();
        TicketSummaryHelper.ClickEvents();
       


        //Kendo Validator
        $("#UserTktForm").kendoValidator().data("kendoValidator");
        $("#tktInfoOptionForm").kendoValidator().data("kendoValidator");
        
    },
    GenerateTicketeGrid: function () {
        var AssigneeUserId = $("#AssigneeUserId").val();
        $("#kTicketGrid").kendoGrid({
            dataSource: TicketSummaryManager.gridDataSource(AssigneeUserId),
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
            //detailInit: detailInitProduct,
            sortable: true,
            resizable: true,
            reorderable: true,
            groupable: true,

            toolbar: ["excel", "pdf", "search"],
            excel: {
                fileName: "Ticket Excel.xlsx",
                filterable: true
            },
            search: {
                fields: ["ticketCode", "ticketTitle"]
            },
            columns: [  
                //{
                //    title: "Action",
                //    width: 170,
                //    template: function (dataItem) {
                //        debugger;
                //        console.log(dataItem.t_TopicId);
                //        return "<a class='btn btn-primary btn-sm mr-2 edit' title='Modify' href='/Ticket/Edit/" + dataItem.ticketId + "'>" +
                //            "<i class='fas fa-pencil-alt'></i>" +
                //            "</a>" +
                //            "<a class='btn btn-success btn-sm mr-2' title='Task List' href='/Task/TaskIndex?id=" + dataItem.ticketId + "&Self=&ticketTitle=" + encodeURIComponent(dataItem.ticketTitle) + "&ticketCode=" + encodeURIComponent(dataItem.ticketCode) + "'>" +
                //            "<i class='fas fa-th-list'></i>" +
                //            "</a>" +
                //            "<a class='btn btn-info btn-sm upload-excel' title='Generate Task/Upload Excel' href='javascript:void(0);' " +
                //            "data-ticketid='" + dataItem.ticketId + "' " +
                //            "data-tickettitle='" + encodeURIComponent(dataItem.ticketTitle) + "' " +
                //            "data-ticketdescription='" + encodeURIComponent(dataItem.ticketDescription || '') + "' " +
                //            "data-sourceid='" + dataItem.t_SourceId + "' " +
                //            "data-topicid='" + dataItem.t_TopicId + "' " +
                //            "data-priorityid='" + dataItem.priorityId + "' " +
                //            "data-departmentid='" + dataItem.departmentId + "' " +
                //            "data-tasktypeid='" + dataItem.ticketTypeId + "'>" +
                //            "<i class='fas fa-file-excel'></i>" +
                //            "</a>" +
                //            "<input type='file' class='hidden excel-input' style='display:none;' accept='.xlsx,.xls' />";
                //    }
                //},
                {
                    title: "Action",
                    width: 190,
                    template: function (dataItem) {
                        return "<a class='btn btn-primary btn-sm mr-2 edit' title='Modify' href='/Ticket/Edit/" + dataItem.ticketId + "'>" +
                            "<i class='fas fa-pencil-alt'></i>" +
                            "</a>" +
                            "<a class='btn btn-success btn-sm mr-2' title='Task List' href='/Task/TaskIndex?id=" + dataItem.ticketId + "&Self=&ticketTitle=" + encodeURIComponent(dataItem.ticketTitle) + "&ticketCode=" + encodeURIComponent(dataItem.ticketCode) + "'>" +
                            "<i class='fas fa-th-list'></i>" +
                            "</a>" +
                            "<a class='btn btn-info btn-sm upload-excel' title='Generate Task/Upload Excel' href='javascript:void(0);' " +
                            "data-ticketid='" + dataItem.ticketId + "' " +
                            "data-tickettitle='" + encodeURIComponent(dataItem.ticketTitle) + "' " +
                            "data-ticketdescription='" + encodeURIComponent(dataItem.ticketDescription || '') + "' " +
                            "data-sourceid='" + dataItem.t_SourceId + "' " +
                            "data-topicid='" + dataItem.t_TopicId + "' " +
                            "data-priorityid='" + dataItem.priorityId + "' " +
                            "data-departmentid='" + dataItem.departmentId + "' " +
                            "data-tasktypeid='" + dataItem.ticketTypeId + "'>" +
                            "<i class='fas fa-file-excel'></i>" +
                            "</a>" +
                            "<a class='btn btn-warning btn-sm download-excel' title='Download Template' style='margin-left:5px' href='javascript:void(0);' " +
                            "data-ticketid='" + dataItem.ticketId + "'>" +
                            "<i class='fas fa-download'></i></a>" +
                            "<input type='file' class='hidden excel-input' style='display:none;' accept='.xlsx,.xls' />";
                    }
                },
                {
                    field: "ticketId", title: "ID", width: 150, hidden: true, sortable: true
                },
                {
                    field: "ticketCode", title: "Code", sortable: true, width: 150
                },
                {
                    field: "ticketTitle", title: "Title", sortable: true, width: 220
                },
                {
                    field: "ticketDescription", title: "Description", hidden:true, sortable: true, width: 220
                },
                {
                    field: "clientsName", title: "Client", sortable: true, width: 200
                },
                {
                    field: "profileName", title: "Stack Holder", sortable: true, width: 220
                },
                {
                    field: "createdBy", title: "Creator", sortable: true, hidden: true, width: 150
                },
                {
                    field: "productName", title: "Product", sortable: true, width: 120
                },
                {
                    field: "sourceName", title: "Source", sortable: true, width: 120
                },
                {
                    field: "createDate", title: "Create Date", sortable: true, width: 200, template: '#= kendo.toString(kendo.parseDate(createDate), "yyyy-MM-dd HH:mm:ss") #', filterable: {
                        ui: "datepicker"
                    }
                },
                {
                    field: "dueDate", title: "Due Date", sortable: true, width: 200, template: '#= kendo.toString(kendo.parseDate(dueDate), "yyyy-MM-dd HH:mm:ss") #', filterable: {
                        ui: "datepicker"
                    }
                },
                {
                    field: "taskCount", title: "Task Qty.", sortable: true, width: 120
                }

            ],
            editable: false,
            selectable: "row",
            navigatable: true,
            columnMenu: true
        });
    },

    GenerateEnternalNotesGrid: function () {
        var ticketId = $("#Id").val();
        $("#kENotesTktGrid").kendoGrid({
            dataSource: TicketSummaryManager.gridNoteDataSource(ticketId),
            pageable: false,
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
            groupable: false,

            toolbar: ["search"],
            search: {
                fields: ["shortNote"]
            },
            columns: [
                {
                    title: "Action",
                    width: 80,
                    template: function (dataItem) {
                        console.log(dataItem);
                        return "<a class='btn btn-primary btn-sm mr-2 edit' title='Modify' onclick='TicketSummaryHelper.ClickEventForEnternal(" + dataItem.id + ")'>" +
                            "<i class='fas fa-pencil-alt'></i>" +
                            "</a>";
                    }
                },
                {
                    field: "id", title: "ID", hidden: true, sortable: true
                },
                {
                    field: "t_TicketId", title: "T_TicketId", hidden: true
                },
                {
                    field: "shortNote", title: "ShortNote", sortable: true
                },

                {
                    field: "assigneeUserId", title: "User", width: 250, sortable: true
                },
                {
                    field: "isActive", title: "Active", width: 100, sortable: true
                }



            ],
            editable: false,
            selectable: "row",
            navigatable: true,
            columnMenu: true
        });
    },
    GenerateTicketeStyle: function () {
        //Kendo Text-Box
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

        //Text Area Style
        $(".kTextArea").each(function () {
            $(this).css("width", "100%");
            $(this).css("height", "70px");
            $(this).css("border-color", "#a3d0e4");
            $(this).css("border-radius", "3px");
        });

        //Kendo Numeric TextBox
        $(".KNumericTextBox").kendoMaskedTextBox({
            mask: "000.00"
        });
        $(".KNumericTextBox").each(function () {
            $(this).css("width", "100%");
        });

        //Dropdown Width
        $(".KDropDown").each(function () {
            $(this).css("width", "100%");
        });
    },
    GenerateKendoTextBox: function () {
        $("#Code").kendoTextBox();
        $("#Title").kendoTextBox();
    },
    GenerateKendoComboBox: function () {

        var topic = new kendo.data.DataSource({
            transport: {
                read: {
                    url: "/Task/GetAllTopicData",
                    dataType: "json"
                }
            }
        });
        $("#cmbTopic").kendoComboBox({
            dataTextField: "name",
            dataValueField: "id",
            dataSource: topic,
            filter: "contains",
            suggest: true
        });


        var priority = new kendo.data.DataSource({
            transport: {
                read: {
                    url: "/Task/GetAllPriorityData",
                    dataType: "json"
                }
            }
        });
        $("#cmbPriority").kendoComboBox({
            dataTextField: "name",
            dataValueField: "id",
            dataSource: priority,
            filter: "contains",
            suggest: true,
            index: 1
        });

        var status = new kendo.data.DataSource({
            transport: {
                read: {
                    url: "/Task/GetAllStatusData",
                    dataType: "json"
                }
            }
        });
        $("#cmbStatus").kendoComboBox({
            dataTextField: "name",
            dataValueField: "id",
            dataSource: status,
            filter: "contains",
            suggest: true,
            index: 0
        });


        var multiComboBox = $("#cmbStackHolder").kendoMultiColumnComboBox({
            dataTextField: "profileName",
            dataValueField: "id",
            height: 400,
            columns: [
                { field: "profileName", title: "Profile", width: 200 },
                { field: "logId", title: "Email", width: 200 }
                
            ],

            filter: "contains",
            filterFields: ["logId","profileName"],
            dataSource: {
                transport: {
                    read: "/Ticket/GetAllStackHolder"
                }
            }
        }).data("kendoMultiColumnComboBox");       

        var client = new kendo.data.DataSource({
            transport: {
                read: {
                    url: "/Ticket/GetAllClient",
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

        var ticketSourceData = new kendo.data.DataSource({
            transport: {
                read: {
                    url: "/Ticket/GetAllticketSourceData",
                    dataType: "json"
                }
            }
        });
        $("#cmbTktSource").kendoComboBox({
            dataTextField: "name",
            dataValueField: "id",
            dataSource: ticketSourceData,
            filter: "contains",
            suggest: true
        });        

        var products = new kendo.data.DataSource({
            transport: {
                read: {
                    url: "/Ticket/GetAllProductsData",
                    dataType: "json"
                }
            }
        });

        $("#cmbTktProducts").kendoComboBox({
            dataTextField: "name",
            dataValueField: "id",
            dataSource: products,
            filter: "contains",
            suggest: true
            //index: 0
        });

        var ticketType = new kendo.data.DataSource({
            transport: {
                read: {
                    url: "/Ticket/GetAllTicketTypeData",
                    dataType: "json"
                }
            }
        });

        $("#cmbTicketType").kendoComboBox({
            dataValueField: "ticketTypeId",
            dataTextField: "ticketTypeName",
            dataSource: ticketType,
            filter: "contains",
            suggest: true
            //index: 0
        });

        var department = new kendo.data.DataSource({
            transport: {
                read: {
                    url: "/Ticket/GetAllDepartmentData",
                    dataType: "json"
                }
            }
        });

        $("#cmbDepartment").kendoComboBox({
            dataValueField: "departmentId",
            dataTextField: "departmentName",
            dataSource: department,
            filter: "contains",
            suggest: true
            //index: 0
        });
       
    },
    GenerateKendoDatePicker: function () {
        $("#txtStartDate").kendoDateTimePicker({
            value: new Date(), // sets the initial value to the current date and time
            dateInput: true
        });
        $("#txtEndDate").kendoDateTimePicker({
            //value: new Date(), // sets the initial value to the current date and time
            //dateInput: true
        });
    },
    GenerateKendoTextArea: function () {
        var editor = $("#editor").kendoEditor({
            stylesheets: [
                "../content/shared/styles/editor.css",
            ],
            tools: [
                "bold",
                "italic",
                "underline",
                "undo",
                "redo",
                "justifyLeft",
                "justifyCenter",
                "justifyRight",
                "insertUnorderedList",
                "createLink",
                "unlink",
                "insertImage",
                "tableWizard",
                "tableProperties",
                "tableCellProperties",
                "createTable",
                "addRowAbove",
                "addRowBelow",
                "addColumnLeft",
                "addColumnRight",
                "deleteRow",
                "deleteColumn",
                "mergeCellsHorizontally",
                "mergeCellsVertically",
                "splitCellHorizontally",
                "splitCellVertically",
                "tableAlignLeft",
                "tableAlignCenter",
                "tableAlignRight",
                "formatting",
                {
                    name: "fontName",
                    items: [
                        { text: "Andale Mono", value: "\"Andale Mono\"" }, // Font-family names composed of several words should be wrapped in \" \"
                        { text: "Arial", value: "Arial" },
                        { text: "Arial Black", value: "\"Arial Black\"" },
                        { text: "Book Antiqua", value: "\"Book Antiqua\"" },
                        { text: "Comic Sans MS", value: "\"Comic Sans MS\"" },
                        { text: "Courier New", value: "\"Courier New\"" },
                        { text: "Georgia", value: "Georgia" },
                        { text: "Helvetica", value: "Helvetica" },
                        { text: "Impact", value: "Impact" },
                        { text: "Symbol", value: "Symbol" },
                        { text: "Tahoma", value: "Tahoma" },
                        { text: "Terminal", value: "Terminal" },
                        { text: "Times New Roman", value: "\"Times New Roman\"" },
                        { text: "Trebuchet MS", value: "\"Trebuchet MS\"" },
                        { text: "Verdana", value: "Verdana" },
                    ]
                }
            ]
        });
        var editor = $("#collaborationEditor").kendoEditor({
            stylesheets: [
                "../content/shared/styles/editor.css",
            ],
            tools: [
                "bold",
                "italic",
                "underline",
                "undo",
                "redo",
                "justifyLeft",
                "justifyCenter",
                "justifyRight",
                "insertUnorderedList",
                "createLink",
                "unlink",
                "insertImage",
                "tableWizard",
                "tableProperties",
                "tableCellProperties",
                "createTable",
                "addRowAbove",
                "addRowBelow",
                "addColumnLeft",
                "addColumnRight",
                "deleteRow",
                "deleteColumn",
                "mergeCellsHorizontally",
                "mergeCellsVertically",
                "splitCellHorizontally",
                "splitCellVertically",
                "tableAlignLeft",
                "tableAlignCenter",
                "tableAlignRight",
                "formatting",
                {
                    name: "fontName",
                    items: [
                        { text: "Andale Mono", value: "\"Andale Mono\"" }, // Font-family names composed of several words should be wrapped in \" \"
                        { text: "Arial", value: "Arial" },
                        { text: "Arial Black", value: "\"Arial Black\"" },
                        { text: "Book Antiqua", value: "\"Book Antiqua\"" },
                        { text: "Comic Sans MS", value: "\"Comic Sans MS\"" },
                        { text: "Courier New", value: "\"Courier New\"" },
                        { text: "Georgia", value: "Georgia" },
                        { text: "Helvetica", value: "Helvetica" },
                        { text: "Impact", value: "Impact" },
                        { text: "Symbol", value: "Symbol" },
                        { text: "Tahoma", value: "Tahoma" },
                        { text: "Terminal", value: "Terminal" },
                        { text: "Times New Roman", value: "\"Times New Roman\"" },
                        { text: "Trebuchet MS", value: "\"Trebuchet MS\"" },
                        { text: "Verdana", value: "Verdana" },
                    ]
                }
            ]
        });
        var editorEnternal = $("#editorShortNote").kendoEditor({
            stylesheets: [
                "../content/shared/styles/editor.css",
            ],
            tools: [
                "bold",
                "italic",
                "underline",
                "undo",
                "redo",
                "justifyLeft",
                "justifyCenter",
                "justifyRight",
                "insertUnorderedList",
                "createLink",
                "unlink",
                "insertImage",
                "tableWizard",
                "tableProperties",
                "tableCellProperties",
                "createTable",
                "addRowAbove",
                "addRowBelow",
                "addColumnLeft",
                "addColumnRight",
                "deleteRow",
                "deleteColumn",
                "mergeCellsHorizontally",
                "mergeCellsVertically",
                "splitCellHorizontally",
                "splitCellVertically",
                "tableAlignLeft",
                "tableAlignCenter",
                "tableAlignRight",
                "formatting",
                {
                    name: "fontName",
                    items: [
                        { text: "Andale Mono", value: "\"Andale Mono\"" }, // Font-family names composed of several words should be wrapped in \" \"
                        { text: "Arial", value: "Arial" },
                        { text: "Arial Black", value: "\"Arial Black\"" },
                        { text: "Book Antiqua", value: "\"Book Antiqua\"" },
                        { text: "Comic Sans MS", value: "\"Comic Sans MS\"" },
                        { text: "Courier New", value: "\"Courier New\"" },
                        { text: "Georgia", value: "Georgia" },
                        { text: "Helvetica", value: "Helvetica" },
                        { text: "Impact", value: "Impact" },
                        { text: "Symbol", value: "Symbol" },
                        { text: "Tahoma", value: "Tahoma" },
                        { text: "Terminal", value: "Terminal" },
                        { text: "Times New Roman", value: "\"Times New Roman\"" },
                        { text: "Trebuchet MS", value: "\"Trebuchet MS\"" },
                        { text: "Verdana", value: "Verdana" },
                    ]
                }
            ]
        });
    },   
    ToogleGenerate: function () {
        $("#usrcollaborationTab").click(function () {
            $("#UserTktForm").toggleClass("hidden");
        });
        $("#infoOptionTab").click(function () {
            $("#tktInfoOptionForm").toggleClass("hidden");
        });
        $("#responseTab").click(function () {
            $("#UserTktResponseForm").toggleClass("hidden");
        });
        $("#collaborationTab").click(function () {
            $("#collaborationForm").toggleClass("hidden");
        });
        $("#enternalTktTab").click(function () {
            $("#enternlaTktNotes").toggleClass("hidden");
        });
    },
    GenerateModal: function () {
        $("#divEnternalTktModal").kendoWindow({
            visible: false,
            modal: true,
            width: "50%",
            title: "Enternal Notes"
        });
        
    },
    ClickEvents: function () {
        $("#btnNew").click(function () {
            $("#divGrid").hide();
            $("#divForm").show();
            $("#btnNew").hide();
            $("#btnIndex").show();
            $("#btnSave").show();
            $("#btnUpdate").hide();

        });
        $("#btnCancle").click(function () {
            window.history.back();
        });
        $("#btnNewTktEnt").click(function () {
            var ticketId = $("#Id").val();

            if (ticketId == 0 || ticketId == null) {
                alert("Create Task first!");
            }
            else {
                TicketSummaryHelper.ClearEnternalForm();
                $("#divEnternalTktModal").data("kendoWindow").center().open();
                $('#txtShortNote').prop('disabled', false);

                $($('#editorShortNote').data().kendoEditor.body).attr('contenteditable', true)
                $("#btnSaveTktEnternal").show();
                $("#btnEntTktIsAct").hide();
            }
            
        });
        $("#canceltktEntButton").click(function () {
            $("#divEnternalTktModal").data("kendoWindow").close();
        });
        $("#btnSaveTktEnternal").click(function () {
            TicketSummaryManager.SaveEnterNalNotes(event);
            $("#divEnternalTktModal").data("kendoWindow").close();
        });
        $("#btnEntTktIsAct").click(function () {
            TicketSummaryManager.SaveEnterNalActive(event);
            $("#divEnternalTktModal").data("kendoWindow").close();
        });

        $(document).on("click", ".upload-excel", function () {
            var $btn = $(this);
            var $fileInput = $btn.siblings(".excel-input");
            $fileInput.click();
        });

        $(document).on("change", ".excel-input", function () {
            var $input = $(this);
            var file = $input[0].files[0];
            if (!file) return;

            var $btn = $input.siblings(".upload-excel");

            var formData = new FormData();
            formData.append("excelFile", file);
            formData.append("T_TicketId", $btn.data("ticketid"));
            formData.append("T_SourceId", $btn.data("sourceid"));
            formData.append("T_TopicId", $btn.data("topicid"));
            formData.append("T_PriorityId", $btn.data("priorityid"));
            formData.append("DepartmentId", $btn.data("departmentid"));
            formData.append("TaskTypeId", $btn.data("tasktypeid"));

            $.ajax({
                url: '/Task/UploadExcel',
                type: 'POST',
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response.status === "Success") {
                        toastr.options = {
                            positionClass: 'toast-center',
                            toastClass: 'toastr-center'
                        };
                        toastr.success("Excel uploaded and tasks created successfully!");                        
                        $("#kTicketGrid").data("kendoGrid").dataSource.read();
                    } else {
                        toastr.options = {
                            positionClass: 'toast-center',
                            toastClass: 'toastr-center'
                        };
                        toastr.success(response.message || "Failed to upload Excel.");
                    }
                },
                error: function (xhr) {
                    console.error(xhr.responseText);
                    alert("❌ Error while uploading Excel.");
                }
            });

            $input.val("");
        });

        // Download Excel button click
        $("#kTicketGrid").on("click", ".download-excel", function () {
            var ticketId = $(this).data("ticketId");

            window.location.href = "/Task/DownloadExcelTemplate?ticketId=" + ticketId;
        });
    },
    ClearEnternalForm: function () {
        $("#txtShortNote").val("");
        var editor = $("#editorShortNote").data("kendoEditor");
        editor.value("");

    },
    CreateTicketObject: function () {
        var obj = new Object();
        obj.id = $("#hdnTicketId").val();
        obj.ticketCode = $("#tktCode").val();
        obj.ticketTitle = $("#tktTitle").val();
        obj.creatorEmail = $("#tktEmail").val();
        obj.creatorPhone = $("#tktPhone").val();
        obj.ticketStackHolderCC = $("#tktCC").val();
        obj.ticketStackHolder = $("#cmbStackHolder").data("kendoComboBox").value();
        obj.organization = $("#cmbOrganization").data("kendoComboBox").value();
        obj.ticketSource = $("#cmbTktSource").data("kendoComboBox").value();
        obj.ticketTopic = $("#cmbTktTopic").data("kendoComboBox").value();
        obj.departmentId = $("#cmbTktDepartment").data("kendoComboBox").value();
        obj.status = $("#cmbTktStatus").data("kendoComboBox").value();
        obj.productId = $("#cmbTktProducts").data("kendoComboBox").value();
        obj.AssignToList = $("#cmbAssignTo").data("kendoMultiSelect").value();

        var openingDate = $("#txtStartDate").data("kendoDateTimePicker");
        var dateValue = openingDate.value();
        function pad(number) {
            if (number < 10) {
                return '0' + number;
            }
            return number;
        }
        var year = dateValue.getFullYear();
        var month = pad(dateValue.getMonth() + 1);
        var day = pad(dateValue.getDate());
        var hours = pad(dateValue.getHours());
        var minutes = pad(dateValue.getMinutes());
        var seconds = pad(dateValue.getSeconds());

        var formattedDate = year + '-' + month + '-' + day + ' ' + hours + ':' + minutes + ':' + seconds + '.000';
        obj.createDate = formattedDate;

        var closingDate = $("#txtEndDate").data("kendoDateTimePicker");
        var closingDateValue = closingDate.value();
        function pad(number) {
            if (number < 10) {
                return '0' + number;
            }
            return number;
        }
        var year = closingDateValue.getFullYear();
        var month = pad(closingDateValue.getMonth() + 1);
        var day = pad(closingDateValue.getDate());
        var hours = pad(closingDateValue.getHours());
        var minutes = pad(closingDateValue.getMinutes());
        var seconds = pad(closingDateValue.getSeconds());

        var formattedClosingDate = year + '-' + month + '-' + day + ' ' + hours + ':' + minutes + ':' + seconds + '.000';
        obj.dueDate = formattedClosingDate;

        //obj.CreateDate = $("#txtStartDate").data("kendoDateTimePicker").value();
        //obj.DueDate = $("#txtEndDate").data("kendoDateTimePicker").value();

        obj.description = $("#editor").data("kendoEditor").value();
        debugger;
        var attachment = TicketSummaryHelper.getSelectedFiles();
        obj.files = attachment[0].name;
        //obj.Remarks = $("#collaborationEditor").data("kendoEditor").value();
        return obj;

    },
    ClickEventForEditButton: function (obj) {        
        TicketSummaryHelper.FillForm(obj);
    },
    GetAssignToByTicketId: function (id, callback) {
        $.ajax({
            url: '/Tools/Support/GetAssignToByTicketId',
            method: 'get',
            data: { id: id },
            success: function (response) {
                // Call the callback function with the response data
                if (callback) callback(response);
            },
            error: function (xhr, status, error) {
                console.error('Error fetching data:', error);
            }
        });
    },
    getSelectedFiles: function () {
        // Get the file input element
        var fileInput = document.getElementById('files');

        // Get the list of selected files
        var files = fileInput.files;

        // Store the files in an array or directly use the FileList
        var selectedFiles = [];

        for (var i = 0; i < files.length; i++) {
            selectedFiles.push(files[i]);
        }

        // Now you have all the selected files in the selectedFiles array
        return selectedFiles;
    },
    ClickEventForEnternal: function (id) {
        $("#divEnternalTktModal").data("kendoWindow").center().open();
        $('#txtShortNote').prop('disabled', true);
        $($('#editorShortNote').data().kendoEditor.body).attr('contenteditable', false)
        $("#btnSaveTktEnternal").hide();
        $("#btnEntTktIsAct").show();
        var internalData = TicketSummaryManager.GetinternalDataById(id, function (internalData) {
            $("#hdnTktEnternalNId").val(internalData[0].id);
            $("#Id").val(internalData[0].t_TicketId);
            $("#txtShortNote").val(internalData[0].shortNote);
            $("#editorShortNote").data("kendoEditor").value(internalData[0].description);
        });
        TaskSummaryHelper.FillInternalForm(selectedItem);
    },
    CreateNoteObject: function () {
        var obj = new Object();
        obj.id = $("#hdnTktEnternalNId").val();
        obj.t_TicketId = $("#Id").val();
        obj.shortNote = $("#txtShortNote").val();
        obj.description = $("#editorShortNote").data("kendoEditor").value();
        return obj;
    },
    CreateNoteActiveObject: function () {
        var obj = new Object();
        obj.id = $("#hdnTktEnternalNId").val();
        obj.IsActive = 0;
        return obj;
    }
}