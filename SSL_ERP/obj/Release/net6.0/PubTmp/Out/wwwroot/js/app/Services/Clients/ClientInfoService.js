var ClientInfoService = function () {
    var save = function (masterObj, done, fail) {
        debugger;
        $.ajax({
            url: '/Clients/CreateEdit',
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