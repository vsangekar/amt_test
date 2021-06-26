$(document).ready(function () {
  
    init();
});

function init() {
    $("#aboutmodal").modal();
    $("#studentloginmodal").modal();
    $("#adminloginmodal").modal();

/*validation*/

    $("#uxadminlogin").click(function () {
        var savedata = true;

        if (($("#uxadminmail").val() == "") || ($("#uxadminpass").val() == "")) {
            var forms = document.querySelectorAll('.needs-validation')
            Array.prototype.slice.call(forms)
                .forEach(function (form) {
                    form.classList.add('was-validated')
                })
            savedata = false;
        }
        if (savedata == true) {
            adminlogin();
        }


    });
}

function adminlogin() {
    debugger;
    var model = {};
    model["emailid"] = $("#uxadminmail").val();
    model["loginpassword"] = $("#uxadminpass").val();

    //API to get admin list
    ajaxPost("amt_test_api/admin/adminlogin"
        , JSON.stringify(model)
        , function (value) {
            if (value.success == "1") {
                window.location.href = "studentmaster.html";
            }
            else {
                alertify.error(value.error);
            }
        }
        , function (errorText) {
        }
    );
}


