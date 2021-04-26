var ajaxcall = {
    post: (param) => {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: param.url,
                data: param.data || null,
                //dataType: "json",
                contentType: "application/x-www-form-urlencoded",
                success: function (res) {
                    resolve(res);
                },
                error: function (err) {
                    reject(err);
                }
            });
        });
    },
    get: (param) => {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "GET",
                url: param.url,
                data: param.data || null,
                dataType: "json",
                contentType: "application/x-www-form-urlencoded",
                success: function (res) {
                    resolve(res);
                },
                error: function (err) {
                    reject(err);
                }
            });
        });
    }
}

// Global ajax settings....
$(function () {
    var title = document.title;
    $(document).ajaxStart(function () {
        document.title = 'Wait...';
        $('#waitLoading').show();
    });

    $(document).ajaxStop(function () {
        document.title = title;
        $('#waitLoading').hide();
    });

    $(document).ajaxError(function () {
        document.title = title;
        $('#waitLoading').hide();
    });
});