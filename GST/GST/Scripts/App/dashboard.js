
var AppDashboard = {
    data() {
           return {
            showsubnav: false,
            subnavbar: [],
            navbar: {
                "Dashboard": {
                    isActive: false,
                    Title: "Dashboard",
                    Url: "Dashboard",
                    BgImg: "dashboard"
                },
                "Patient": {
                    isActive: true,
                    Title: "Patient",
                    Url: "Patient",
                    BgImg: "patient",
                    child: [{
                        Title: "All Patients",
                        Url: "Patient List"
                    },
                    {
                        Title: "Appointment Details",
                        Url: "Appointment Details"
                    },
                    {
                        Title: "Document Request",
                        Url: "Document Request"
                    },
                    {
                        Title: "Educational Materia",
                        Url: "Educational Materia"
                    },
                    {
                        Title: "Financials",
                        Url: "Financials"
                    },
                    {
                        Title: "Incident Reports",
                        Url: "Incident Reports"
                    }
                    ]
                },
                "Scheduler": {
                    isActive: false,
                    Title: "Scheduler",
                    Url: "Scheduler",
                    BgImg: "scheduler"
                },
                "Chart": {
                    isActive: false,
                    Title: "Charting",
                    Url: "chart",
                    BgImg: "charting"
                },
                "Billing": {
                    isActive: false,
                    Title: "Billing",
                    Url: "billing",
                    BgImg: "billing"
                },
                "Reports": {
                    isActive: false,
                    Title: "Reports",
                    Url: "report",
                    BgImg: "reports"
                },
                "Admin": {
                    isActive: false,
                    Title: "Admin",
                    Url: "admin",
                    BgImg: "admin",
                    child: [
                        {
                            Title: "Users",
                            Url: "Users"
                        },
                        {
                            Title: "Lookup",
                            Url: "Lookup"
                        },
                        {
                            Title: "Service Location",
                            Url: "Service Location"
                        },
                        {
                            Title: "Type Of Visit",
                            Url: "Type Of Visit"
                        }


                    ]
                },
                "Tools": {
                    isActive: false,
                    Title: "Tools",
                    Url: "tools",
                    BgImg: "tools"
                }
            }
        };
    },
    hideNav() {
        var effect ='drop';
        $(".appbtndashboardhide i").removeClass("fa-angle-left fa-angle-right");
        if ($(".appbtndashboardhide").hasClass("hidenav")) {
            $(".appbtndashboardhide").removeClass("hidenav");
            $(".appbtndashboardhide i").addClass("fa-angle-left");
            $("#tdleftnav,.appbtndashboardlblhide").show(effect);
        } else {
            $(".appbtndashboardhide").addClass("hidenav");
            $(".appbtndashboardhide i").addClass("fa-angle-right");
            $("#tdleftnav,.appbtndashboardlblhide").hide(effect);
        }
        var hideIcon=$(".leftnavrow .sidebar").toggleClass("hidelabel");
        //StorageApp.SetLocal(hideIcon,true);
    },
    hideNavlabel(){
        $(".leftnavrow .sidebar").toggleClass("hidelabel");
    },
    hideSubnav() {
        this.data.showsubnav = false;        
    },
    SetActive(navitem) {
        if (navitem.isActive) {
            this.data.showsubnav = !this.data.showsubnav;
        }
        for (const key in this.data.navbar) {
            const element = this.navbar[key];
            element.isActive = false;
        }
        navitem.isActive = true;
        this.data.subnavbar = navitem.child || [];
    }
}