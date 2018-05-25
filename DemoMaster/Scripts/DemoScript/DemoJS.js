var str = 'hello';
var number = 1;
var obj = new String();
//document.getElementById('message').innerHTML = "Dynamic Message";
console.log(typeof str);
console.log(typeof number);
console.log(typeof obj);
$(document).ready(function () {
    debugger;
    $("#txttrest").val("hello");
})
function myFunction(p1, p2) {
    return p1 * p2;
}
console.log(myFunction(2, 3));
var person = { firstName: "John", lastName: "Doe", age: 50, eyeColor: "blue" };
console.log(person.firstName);

function keyDown(event) {
    debugger
    console.log(event.keyCode);
}
function onBtnClick() {
    //document.getElementById("demo").innerHTML = "Hello JavaScript!"
    console.log($("#txttrest").val());
}
function onFormSubmit() {
    debugger
    var CustomerMasters = {
        //CompanyName: $('#txtCustomerName').val(),
        CustomerCode: $("#txtCustomerCode").val(),
        CompanyCode: $("#txtCompanyCode").val(),
        CompanyName: $("#txtCompanyName").val(),
        Address1: $("#txtAddress1").val(),
        Address2: $("#txtAddress2").val(),
        City: $("#txtCity").val(),
        StateCode: $("#txtStateCode").val(),
        CountryCode: $("#txtCountryCode").val(),
        ZipCode: $("#txtZipCode").val(),
        Phone: $("#txtPhone").val(),
        Fax: $("#txtFax").val(),
        Email: $("#txtEmail").val(),
        InActive: $("#IsInActive").val(),
        CreatedDate: $("#txtCreatedDate").val()
    };
    debugger
    $.ajax({
        type: "POST",
        url: "/CustomerMasters/JQAjax",
        data: JSON.stringify(CustomerMasters),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (dataResponse) {
            // alert(data.msg);
            console.log(dataResponse);
            var popUp =
                "<div id=\"myModal\" class=\"modal fade\" role=\"dialog\" data-container=\"body\" data-backdrop=\"static\" style=\"z-index:9999999!important;\">" +
                "<div class=\"modal-dialog\">" +
                "<div class=\"modal-content\">" +
                "<div class=\"modal-header\">" +
                "<button type=\"button\" class=\"close\" data-dismiss=\"modal\">&times;</button>" +
                "<h3 class=\"modal-title\"><i class=\"fa fa-exclamation-triangle\" aria-hidden=\"true\"></i>" +
                "Success" +
                "</h3>" +
                "</div>" +
                "<div class=\"modal-body\">" +
                dataResponse.msg +
                
                "<div class=\"modal-footer\">" +
                "<button id=\"ErrorClose\" class=\"btnOK btn btn-primary m-r-sm\" data-dismiss=\"modal\">OK</button>" +
                "</div>" +
                "</div>" +
                "</div>" +
                "</div>";

            $(popUp).prependTo($("body"));

            $('#myModal').modal('show');
        },
        error: function () {
            alert("Unknown Error occured!!")
        }
    });
}


