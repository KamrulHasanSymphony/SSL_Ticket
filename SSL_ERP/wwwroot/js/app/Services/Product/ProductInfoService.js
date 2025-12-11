var ProductInfoService = function () {
    var save = function (masterObj, done, fail) {
        
        $.ajax({
            url: '/Product/CreateEdit',
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