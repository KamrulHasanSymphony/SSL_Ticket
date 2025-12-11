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
                url: "/Product/GetGridData",
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

    $("#productGrid").kendoGrid({
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
            fileName: "Topics.xlsx",
            filterable: true
        },
        search: {
            fields: ["topicName", "productName"]
        },
        columns: [
            // {
            //     selectable: true, width: 50
            // },
            {
                title: "Action",
                width: 60,
                template: function (dataItem) {
                    console.log(dataItem);
                    return "<a href='/Product/Edit/" + dataItem.id + "' class='btn btn-primary btn-sm mr-2 edit'>" +
                        "<i class='fas fa-pencil-alt'></i>" +
                        "</a>";
                }
            },

            {
                field: "id", width: 150,hidden: true, sortable: true
            },
            {
                field: "name", title: "Name", width: 150, sortable: true
            },           
            {
                field: "isActive", title: "Is Active", sortable: true, width: 200
            }

        ],
        editable: false,
        selectable: "row",
        navigatable: true,
        columnMenu: true
    });

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