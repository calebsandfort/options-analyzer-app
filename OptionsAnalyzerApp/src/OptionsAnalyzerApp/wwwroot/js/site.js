var Site;
if (!Site) {
    Site = {
        RootUrl: ""
    };
}

$().ready(function () {
    Site.RootUrl = $("#uxInput_RootUrl").val();

});