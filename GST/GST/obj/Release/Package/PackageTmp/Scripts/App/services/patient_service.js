var patient_service = {
    GetByPatientId: (userid) => {
        var call = ajaxcall.get({
            url: AppConfig.ApiPath + "/Patient/GetByPatientId/" + userid,
            data: null
        })
        return call;
    }
}

