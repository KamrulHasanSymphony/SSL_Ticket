var TicketService = function () {   


    var save = function (masterObj, done, fail) {

        $.ajax({
            url: '/Ticket/CreateEdit',
            method: 'post',
            data: masterObj

        })
            .done(done)
            .fail(fail);
    };

    return {
        save
    }

}();