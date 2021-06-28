var blankarray = new Array();
var temppurchasedoc = new Array();
var dspurchasedoc = new Array();
var dsstudentprofile = new Array();

$(document).ready(function () {
    init();
});

function init() {
    $(".step1").show();
    $(".step2").hide();
    
    $("#uxnextstudent").click(function () {
        var nextdata = true;
        debugger;
        if (($("#uxstudentname").val() == "") || ($("#uxstudusername").val() == "") || ($("#uxemail").val() == "") || ($("#uxemail").val() == "") || ($("#uxmobileno").val() == "") || ($("#uxstudconfrimpass").val() == "") || ($("#uxstudpass").val() == "") || ($("#uxstudeducation").val() == "") || ($("#uxgender").val() == "")) {

        }
        if (nextdata == true) {
            $(".step2").show();
            $(".step1").hide();
        }
    });

    $("#uxsavestudentinfo").click(function () {
        var savedata = true;
        debugger;
        if (($("#uxstudentprofileimage").val() == "") || ($("#uxdocumentname").val() == "") ) {
     
        }
        if (savedata == true) {
            insertupdatestudentdata();
        }
    });

    $("#uxbacktohome").click(function () {
        window.location.href = "index.html";
    });

    $("#backtostep1").click(function () {
        $(".step1").show();
        $(".step2").hide();
    });
}

function insertupdatestudentdata() {
    debugger;
    var model = {};
    if ($("#uxstudid").val().length > 0) {
        model["studid"] = $("#uxstudid").val()
    }
    model["studfullname"]     = $("#uxstudentname").val();
    model["studusername"]     = $("#uxstudusername").val();
    model["studdob"]          = $("#uxstudentdob").val();
    model["studeducation"]    = $("#uxstudeducation").val();
    model["studgender"]       = $("#uxgender").val();
    model["studemailid"]      = $("#uxemail").val();
    model["studmobileno"]     = $("#uxmobileno").val();
    model["studpassword"]     = $("#uxstudpass").val();
    model["studentaddress"]   = $("#uxstudaddress").val();
    model["studdocumentname"] = $("#uxdocumentname").val();
    model["studdocumentfile"] = $("#uxdocumentfile").val();
    model["studprofileimg"] = $("#uxstudentprofileimage").val();
    ajaxPost("amt_test_api/auth/insertupdatestudentprofile"
        , JSON.stringify(model)
        , function (value) {
            var json = value.d;
            if ($("#uxstudid").val().length == 0) {
                alertify.success("Your Profile is registered  successfully");
                cleardata();
            } else {
                alertify.success("Student Profile is Updated successfully");
                backtomainlist();
            }
        }
        , function (errorText) {
        }
    );
}

function uploaddocumentforpurchase(evt) {
    debugger;
    var files = evt.target.files;
    var file = files[0];
    if (files[0].name.split('.').pop() == "pdf" || files[0].name.split('.').pop() == "jpg" || files[0].name.split('.').pop() == "png" || files[0].name.split('.').pop() == "jpeg") {
        uploadfiledata(file);
    } else {
        $("#uxdocumentfile").val("");
        alertify.error("Please upload PDF or image file.");
        $("#uxdocumentfile").addClass("form-control-required");
        setTimeout(function () {
            $("#uxdocumentfile").focus();
            $("#uxdocumentfile").removeClass("form-control-required");
        }, 5000)
    }
}

function uploadfiledata(fileinfo) {
    debugger;
    var formData = new FormData();
    if (fileinfo.name != undefined) {
        formData.append("filename", fileinfo.name);
        formData.append("filedata", fileinfo);
        ajaxPostFile("amt_test_api/admin/updatefiledata"
            , formData
            , function (value) {
                if (value.success == "1") {
                    temppurchasedoc = new Array();
                    for (var i = 0; i < value.filedata.length; i++) {
                        var model = {}
                        model["doctype"] = "noturl";
                        model["docdata"] = value.filedata[i].filedata;
                        model["docext"] = value.filedata[i].fileext;
                        model["filename"] = value.filedata[i].filename;
                        temppurchasedoc.push(model);
                       
                    }
                    addfileanddisplay();
                }
                else {

                }
            }
            , function (errorText) {
            }
        );
    }
}

function addfileanddisplay() {
    $.each(temppurchasedoc, function (key, val) {
        debugger;
        var model = {}
        model["srno"] = (dspurchasedoc.length + 1);
        model["docid"] = "0";
        model["doctype"] = "noturl";
        model["docdata"] = val.docdata;
        model["docext"] = val.docext;
        model["filename"] = val.filename;
        model["documentfor"] = $("#uxdocumentname").val();
        dspurchasedoc.push(model);
    })
    $("#uxdocumentname").val("");
    $("#uxdocumentfile").val("");
    $("#uxdocumentname").focus();
    printfileanddisplay();
}

function printfileanddisplay() {
    var docstr = "";
    for (var i = 0; i < dspurchasedoc.length; i++) {
        docstr = docstr + "<div class='col-md-4' style='margin-top: 5px;'>" +
            '<div class="col-md-12 alert alert-success">'
        if (dspurchasedoc[i].doctype == "noturl") {
            var fileurl = "";
            if (dspurchasedoc[i].docext == ".pdf") {
                fileurl = "data:application/pdf;base64," + dspurchasedoc[i].docdata;
                docstr = docstr + dspurchasedoc[i].documentfor + " (new) <a href='#' style='float:right;margin-left:5px;' data-srno=" + dspurchasedoc[i].srno + " onclick='ondeletedocument(this)' class='ishideread'> <i class='fa fa-trash'/></a> <a href='" + fileurl + "' onclick='onviewpdfdocumentbase64(this, event)'  style='float:right;'> <i class='fa fa-eye'/></a>"

            } else if (dspurchasedoc[i].docext == ".png") {
                fileurl = "data:application/png;base64," + dspurchasedoc[i].docdata;
                docstr = docstr + dspurchasedoc[i].documentfor + "(new) <a href='#' style='float:right;margin-left:5px;' data-srno=" + dspurchasedoc[i].srno + " onclick='ondeletedocument(this)' class='ishideread'> <i class='fa fa-trash'/></a> <a href='" + fileurl + "' onclick='onviewimagedocumentbase64(this, event)'  style='float:right;'> <i class='fa fa-eye'/></a>"
            } else if (dspurchasedoc[i].docext == ".jpeg" || dspurchasedoc[i].docext == ".jpg") {
                fileurl = "data:application/jpeg;base64," + dspurchasedoc[i].docdata;
                docstr = docstr + dspurchasedoc[i].documentfor + " (new) <a href='#' style='float:right;margin-left:5px;' data-srno=" + dspurchasedoc[i].srno + " onclick='ondeletedocument(this)' class='ishideread'> <i class='fa fa-trash'/></a> <a href='" + fileurl + "' onclick='onviewimagedocumentbase64(this, event)'  style='float:right;'> <i class='fa fa-eye'/></a>"
            }
        } else {
            docstr = docstr + dspurchasedoc[i].documentfor + " <a href='#' style='float:right;margin-left:5px;' data-srno=" + dspurchasedoc[i].srno + " onclick='ondeletedocument(this)' class='ishideread'> <i class='fa fa-trash'/></a> <a href='../purchasefolder/" + dspurchasedoc[i].filename + "' onclick='onviewdocumenturl(this, event)'  style='float:right;'> <i class='fa fa-eye'/></a>"
        }
        docstr = docstr + "</div>" +
            "</div>"
    }

    $(".purchasedocument").html(docstr);
}


function onviewdocumenturl(cname, e) {
    debugger;
    //var filename = $(cname).attr("data-filename");
    e.preventDefault();
    var url = $(cname).attr('href');
    window.open(url, '_blank');
}

function onviewpdfdocumentbase64(cname, e) {
    debugger;
    //var filename = $(cname).attr("data-filename");
    e.preventDefault();
    var pdfResult = $(cname).attr('href');

    let pdfWindow = window.open("")
    pdfWindow.document.write("<iframe width='100%' height='100%' src='" + pdfResult + "'></iframe>")
}


function onviewimagedocumentbase64(cname, e) {
    debugger;
    //var filename = $(cname).attr("data-filename");
    e.preventDefault();
    var imgResult = $(cname).attr('href');
    var image = new Image();
    image.src = imgResult;
    var w = window.open("");
    w.document.write(image.outerHTML);
}

function ondeletedocument(cname) {
    debugger;
    var srno = $(cname).attr("data-srno");
    alertify.confirm("Do you want to delete selected document.", function () {

        dspurchasedoc = jQuery.grep(dspurchasedoc, function (element, index) {
            return element.srno != srno  // retain appropriate elements
        });

        var k = 1;
        for (var i = 0; i < dspurchasedoc.length; i++) {
            dspurchasedoc[i].srno = k;
            k = k + 1;
        }
        printfileanddisplay();

    }, function () {


    }).set({ "title": "Delete Confirmation" }).set('labels', { ok: 'Yes', cancel: 'No' });

}

function getstudentprofileimagedata(evt) {
    debugger;
    // dskeydocument = new Array();
    var files = evt.target.files;
    var file = files[0];
    uploadstudentphoto(file);

}

function uploadstudentphoto(fileinfo) {
    var formData = new FormData();
    if (fileinfo.name != undefined) {
        formData.append("filename", fileinfo.name);
        formData.append("filedata", fileinfo);
        ajaxPostFile("amt_test_api/admin/uploadstudentphoto"
            , formData
            , function (value) {
                if (value.success == "1") {
                    for (var i = 0; i < value.filedata.length; i++) {
                        var model = {}
                        model["docid"] = "0";
                        model["doctype"] = "noturl";
                        model["docdata"] = value.filedata[i].filedata;
                        model["docext"] = value.filedata[i].fileext;
                        model["filename"] = value.filedata[i].filename;
                        dsstudentprofile.push(model);

                        $("#uxstudentphoto").attr("src", "data:image/jpeg;base64," + value.filedata[i].filedata);
                        $("#uxstudentprofileimage").val("");
                    }
                }
                else {
                }
            }
            , function (errorText) {
            }
        );
    }
}