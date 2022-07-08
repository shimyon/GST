var layout = {
    checklogin: () => {
        if (localStorage["Token"]) {
            return true;
        }
        return false;
    },
    changepass: () => {
        //ajaxcall.post({
        //    url: AppConfig.ApiPath + "Site/AddData",
        //    data: {
        //        SiteName: $("#SiteName").val(),
        //        Address: $("#Address").val(),
        //        OwnerName: $("#OwnerName").val(),
        //        Developer: $("#Developer").val(),
        //        WebSite: $("#WebSite").val()
        //    }
        //}).then(res => {
        //    alert("Saved successfully");
        //    window.history.back();
        //}, err => {
        //    alert(err.responseJSON);
        //})
    },
    logout: () => {
        localStorage.clear();
        location.replace("./");
    }
}

$(function () {
    if (layout.checklogin()) {
        $("#layoutuseraction").show();
    }
    else {
        $("#layoutlogin").show();
    }
})


function GenBlobURL(blobData) {
    let blob = new Blob([blobData], { type: 'application/pdf' });
    let downloadUrl = null;
    if (window.webkitURL) {
        downloadUrl = window.webkitURL.createObjectURL(blob);
    } else if (window.URL && window.URL.createObjectURL) {
        downloadUrl = window.URL.createObjectURL(blob);
    }
    return downloadUrl;
}

function DownloadFileFromURL(blobData, filename) {
    let downloadUrl = GenBlobURL(blobData);
    let a = document.createElement("a");
    a.href = downloadUrl;
    a.download = filename || "file.pdf";
    document.body.appendChild(a);
    a.click();
}