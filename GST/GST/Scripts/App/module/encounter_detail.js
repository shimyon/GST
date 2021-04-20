function encounter_detail() {
    this.data = {
        patientid: null,
        EnoNo:null,
        ServiceLocationId:null,
        patientData: {},
        cptList:[]
    }

    this.init = async () => {
        this.setAutoComplete();
        this.setDatepicker();
        this.callAjax();
        this.data.patientData = await patient_service.GetByPatientId(this.data.patientid);
        this.setData();
    }

    this.setData = () => {
        if (this.data.patientData) {
            let pdet = this.data.patientData;
            $("#patientname_label").html(`${pdet.fname} ${pdet.lname} (${pdet.SexF.Description})`);
            $("#patientdob_label").html(pdet.dobstr);
        }
    }

    this.setDatepicker = () =>{        
        $("#txtdos").datepicker({
            dateFormat: "mm/dd/yy",
            changeMonth: true,
            changeYear: true,
            autohide: true,
            showOptions: true,
            showButtonPanel: false
        });

        $("#txtadmindate").datepicker({
            dateFormat: "mm/dd/yy",
            changeMonth: true,
            changeYear: true,
            autohide: true,
            showOptions: true,
            showButtonPanel: false
        });
    }

    this.callAjax = () =>{
        let that = this;
        ajaxcall.get({
            url:AppConfig.ApiPath + "ServiceLocation/GetAll"
        }).then(res => {
            var sourcedata = res.map(r => r.facilityName);
            $('#servicelocationselect').autocomplete({
                minLength: 0,
                source: function(request, response) {            
                    var data = $.grep(sourcedata, function(src) {
                        return src.substring(0, request.term.length).toLowerCase() == request.term.toLowerCase();
                    });            

                    response(data);
                },
                select: function(event, ui) {
                    that.data.ServiceLocationId = ui.item.value;
                }
            })            
            .focus(function () {
                $(this).autocomplete("search", "");
            });            
        }, err => {
            alertify.notify("Error while searching for cpt data");
        });
    }

    this.setAutoComplete = () => {        
        var that = this;
        
        ajaxcall.get({
            url: AppConfig.ApiPath + "User/GetAllRefPhysician"
        }).then(res => {
            res.forEach(f => {
                $("<option>").text(f.label).val(f.value).appendTo("#refprovider");
            })
            $("#refprovider").select2();
        }, err => {
            alertify.notify("Error occurred while getting referring physician details");
        });

        $('#typeofvisit').autocomplete({
            source: function (request, response) {
                ajaxcall.get({
                    url:AppConfig.ApiPath + "TOV/SearchTOV?search=" + request.term
                }).then(res => {
                    response(res);
                }, err => {
                    alertify.notify("Error while searching for cpt data");
                });
            },
            minLength: 3,
            select: function (event, ui) {
                //event.preventDefault();
            },
            open: function () {
                $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
            },
            close: function () {
                $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
            }
        })          
        .focus(function () {
            $(this).autocomplete("search", "");
        }); 

        $('#procedure').autocomplete({
            source: function (request, response) {
                ajaxcall.get({
                    url:AppConfig.ApiPath + "cpt4/SearchCPT?search=" + request.term
                }).then(res => {
                    response(res);
                }, err => {
                    alertify.notify("Error while searching for cpt data");
                });
            },
            minLength: 3,
            select: function (event, ui) {
                event.preventDefault();                
                $("#procedure").val('');
                var reccheck = that.data.cptList.find(f => f.label == ui.item.label);
                if (reccheck) {
                    alertify.error("Item already added");
                    return;
                }else{
                    that.data.cptList.push(ui.item);
                }
                                
                // notesadded / nonotes
                var div =`<div class="section cb pt10 newform">
                        <div class="p10 pb0 text-primary">${ui.item.label}</div><span class="remove bgimg dib fr cp"></span>
                        <div class="row pl10">
                            <div class="column fw bdrbtm2 pb0">
                                <div class="form-group row mb10">
                                    <div class="col-sm-1">
                                        <input id="name" name="name" type="text" required>
                                        <label for="name" class="newlabel">Units</label>
                                    </div>

                                    <div class="col-sm-1">
                                        <input id="name" name="name" type="text" value='${ui.item.Price}' required>
                                        <label for="name" class="newlabel">Price</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <input id="name" name="name" type="text" required>
                                        <label for="name" class="newlabel">Diagnosis Pointers</label>
                                    </div>
                                    <div class="col-sm-3 posrel ml5" style="top: -10px">
                                        <span class="db pb5">Modifier</span>
                                        <input style="" class="modtxt" type="text" placeholder="M1" value='${ui.item.ModifierId}' />
                                        <input class="modtxt ml5" type="text" placeholder="M2" value='${ui.item.ModifierId2}' />
                                        <input class="modtxt ml5" type="text" placeholder="M3" value='${ui.item.ModifierId3}' />
                                        <input class="modtxt ml5" type="text" placeholder="M4" value='${ui.item.ModifierId4}' />

                                    </div>

                                </div>

                                <div class="form-group row">
                                    <div class="col-sm-3">
                                        <input name="name" type="text" required class="cptfromdate">
                                        <label for="name" class="newlabel">From</label>
                                        <img class="bgimg minical" src="~/Content/images/spacer.gif" />
                                    </div>

                                    <div class="col-sm-3">
                                        <input name="name" type="text" required class="cpttodate">
                                        <label for="name" class="newlabel">To</label>
                                        <img class="bgimg minical" src="~/Content/images/spacer.gif" />
                                    </div>

                                    <div class="col-sm-4 mt10">
                                        <span class="ml10  dib">Notes : </span>
                                        <span class="nonotes bgimg dib ml5 posrel" style="top: 2px">&nbsp;</span>
                                        <span class="ml20  dib">NOC : </span><span class="greencircle dib posrel ml5" style="top: 4px">&nbsp;</span>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>`;
                $("#procedurediv").append(div);
                alertify.success("Added Successfully");
                $(".cptfromdate:last, .cpttodate:last").datepicker({
                    dateFormat: "mm/dd/yy",
                    changeMonth: true,
                    changeYear: true,
                    autohide: true,
                    showOptions: true,
                    showButtonPanel: false
                });
            },
            open: function () {
                $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
            },
            close: function () {
                $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
            }
        });
    }
}
