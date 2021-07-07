var layout = {
    checklogin : () =>{
        if (localStorage["Token"]) {
            return true;
        }
        return false;
    },
    logout: () => {
        localStorage.clear();
        location.replace("./");
    }
}

$(function() {
    if (layout.checklogin()) 
    {
        $("#layoutuseraction").show();
    }
    else 
    {
        $("#layoutlogin").show();
    }
})