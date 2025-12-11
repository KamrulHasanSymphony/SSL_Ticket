var TopicInfoService = function () {
    var save = function (masterObj, done, fail) {
        
        $.ajax({
            url: '/Topics/CreateEdit',
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