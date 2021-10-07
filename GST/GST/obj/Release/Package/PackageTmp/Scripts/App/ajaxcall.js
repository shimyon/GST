var ajaxcall = {
	file: (param) => {
		return new Promise((resolve, reject) => {
			fetch(param.url, {
				body: JSON.stringify(param.data),
				method: 'POST',
				headers: {
					'Content-Type': 'application/json; charset=utf-8'
				}
			})
			.then(response => response.blob())
			.then(response => {
				resolve(response);
			}, error => {
				reject(response);
			});
		});
	},
    post: (param) => {
        return new Promise((resolve, reject) => {
            var usreid = localStorage.getItem('userid') || "";
            $.ajax({
                type: "POST",
                url: param.url,
                data: param.data || null,
                //dataType: "json",
                headers: {
                    'contentType': "application/x-www-form-urlencoded",
                    'userid': usreid
                },
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
            var usreid = localStorage.getItem('userid') || "";
            $.ajax({
                type: "GET",
                url: param.url,
                data: param.data || null,
                dataType: "json",
                headers: {
                    'contentType': "application/x-www-form-urlencoded",
                    'userid': usreid
                },
                //contentType: "application/x-www-form-urlencoded",
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
	$.ajaxSetup({
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + (localStorage['Token'] || ''));
        }
    });
    $(document).ajaxStart(function () {
        document.title = 'Wait...';
        $('#waitLoading').show();
    });

    $(document).ajaxStop(function () {
        document.title = title;
        $('#waitLoading').hide();
    });

    $(document).ajaxError(function (xhr, stats, urls, data) {
		if (data == "Unauthorized" ) {	
			alert('Session expired please login again.');
            location.replace("/Login");
		}
		document.title = title;
		$('#waitLoading').hide();
    });
});