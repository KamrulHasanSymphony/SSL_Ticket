var TodayTaskSummaryService = function () {
    var save = function (masterObj, done, fail) {
        
        $.ajax({
            url: '/TodayTaskSummary/CreateEdit',
            method: 'post',
            data: masterObj

        })
            .done(done)
            .fail(fail);

    };
    return {
        save: save,

    }
}();