$(function () {
    GetBillingProviders();
})
debugger
StorageApp.GetLocal(true);
function SaveBillingProvider() {
    var obj = {
        "BillingProviderId": $("#billingprovider_id").val(),
        "name": $("#crtDisplayName").val(),
        "address1": $("#crtAddressLine1").val(),
        "address2": $("#crtAddressLine2").val(),
        "city": $("#crtCity").val(),
        "state": $("#crtState").val(),
        "country": $("#crtCountry").val(),
        "zipcode": $("#crtZipcode").val(),
        "PayToProvider": $("#crtPaytoprovider").is(":checked"),
        "provider_name": $("#provider_name").val(),
        "provider_address1": $("#provider_address1").val(),
        "provider_address2": $("#provider_address2").val(),
        "provider_city": $("#provider_city").val(),
        "provider_state": $("#provider_state").val(),
        "provider_country": $("#provider_country").val(),
        "provider_zipcode": $("#provider_zipcode").val(),
        "provider_phone": $("#provider_phone").val(),
        "email": $("#crtEmail").val(),
        "fax": $("#crtFax").val(),
        "phone": $("#crtPhone").val(),
        "npi": $("#crtNPI").val(),
        "fedtaxid": $("#crtFedTaxId").val(),
        "ssn": $("#crtSSN").val(),
        "sendtoedi": $("#sendtoedi").val(),
        "contact": $("#contact").val(),
        "isdefault": $("#isdefault").is(":checked"),
        "notes": $("#notes").val(),
        "billing_entity_type": $("#billing_entity_type").val(),
        "billing_entity_firstname": $("#crtBillingFirstName").val(),
        "billing_entity_lastname": $("#crtBillingLastName").val(),
        "AR_statement": $("#AR_statement").is(":checked"),
        "AR_name": $("#AR_name").val(),
        "AR_address1": $("#AR_address1").val(),
        "AR_address2": $("#AR_address2").val(),
        "AR_city": $("#AR_city").val(),
        "AR_state": $("#AR_state").val(),
        "AR_country": $("#AR_country").val(),
        "AR_zip": $("#AR_zip").val(),
        "Taxonomy_code": $("#Taxonomy_code").val(),
        //"PTAN": $("#PTAN").val()
    };
    $.ajax({
        type: "POST",
        url: AppConfig.ApiPath + "BillingProvider/Create",
        data: obj,
        dataType: "json",
        contentType: "application/x-www-form-urlencoded",
        success: function (res) {
            //$("#AddEditTov").show();
            window.location.assign("/BillingProvider/List");
        },
        error: function (err) {
            if (err.message) {
                alert("Internal server error");
            } else {
                let errmessage = err.responseJSON.ExceptionMessage || err.responseJSON.Message;
                alert(errmessage);
            }
        }
    });

}

function GetBillingProviders() {
    var id = $("#billingprovider_id").val();
    if (id == "0") {
        return;
    }
    $.ajax({
        type: "GET",
        url: AppConfig.ApiPath + "BillingProvider/GetById/" + id,
        //data: obj,
        dataType: "json",
        contentType: "application/x-www-form-urlencoded",
        success: function (res) {
            //$("#AddEditTov").show();
            $("#crtDisplayName").val(res.name);
            $("#crtAddressLine1").val(res.address1);
            $("#crtAddressLine2").val(res.address2);
            $("#crtCity").val(res.city);
            $("#crtState").val(res.state);
            $("#crtCountry").val(res.country);
            $("#crtZipcode").val(res.zipcode);
            $("#crtPaytoprovider").prop("checked", res.PayToProvider);
            $("#provider_name").val(res.provider_name);
            $("#provider_address1").val(res.provider_address1);
            $("#provider_address2").val(res.provider_address2);
            $("#provider_city").val(res.provider_city);
            $("#provider_state").val(res.provider_state);
            $("#provider_country").val(res.provider_country);
            $("#provider_zipcode").val(res.provider_zipcode);
            $("#provider_phone").val(res.provider_phone);
            $("#crtEmail").val(res.email);
            $("#crtFax").val(res.fax);
            $("#crtPhone").val(res.phone);
            $("#crtNPI").val(res.npi);
            $("#crtFedTaxId").val(res.fedtaxid);
            $("#crtSSN").val(res.ssn);
            $("#sendtoedi").val(res.sendtoedi);
            $("#contact").val(res.contact);
            $("#isdefault").prop("checked", res.isdefault);
            $("#notes").val(res.notes);
            $("#billing_entity_type").val(res.billing_entity_type);
            $("#crtBillingFirstName").val(res.billing_entity_firstname);
            $("#crtBillingLastName").val(res.billing_entity_lastname);
            $("#AR_statement").prop("checked", res.AR_statement);
            $("#AR_name").val(res.AR_name);
            $("#AR_address1").val(res.AR_address1);
            $("#AR_address2").val(res.AR_address2);
            $("#AR_city").val(res.AR_city);
            $("#AR_state").val(res.AR_state);
            $("#AR_country").val(res.AR_country);
            $("#AR_zip").val(res.AR_zip);
            $("#Taxonomy_code").val(res.Taxonomy_code);

        },
        error: function (err) {
            if (err.message) {
                alert("Internal server error");
            } else {
                let errmessage = err.responseJSON.ExceptionMessage || err.responseJSON.Message;
                alert(errmessage);
            }
        }
    });

}

function ChangePaytoProvider() {
    var show = "animate__zoomIn", hide = "animate__zoomOut"; //d-none
    $(".divcrtPaytoprovider").removeClass(`${show} ${hide} d-none`);
    if ($("#crtPaytoprovider").is(":checked")) {
        $(".divcrtPaytoprovider").addClass(`${show}`);
    } else {
        $(".divcrtPaytoprovider").addClass(`${hide}  d-none`);
    }
}


function ArStatement() {
    var show = "animate__zoomIn", hide = "animate__zoomOut"; //d-none
    $(".divcrtar").removeClass(`${show} ${hide} d-none`);
    if ($("#crtar").is(":checked")) {
        $(".divcrtar").addClass(`${show}`);
    } else {
        $(".divcrtar").addClass(`${hide} d-none`);
    }
}