var str = 'hello';
var number = 1;
var obj = new String();
//document.getElementById('message').innerHTML = "Dynamic Message";
console.log(typeof str);
console.log(typeof number);
console.log(typeof obj);
$(document).ready(function () {

    $("#txttrest").val("hello");
    $("#txttrest").attr('style', 'color:blue');
    $("#txttrest").addClass('txtstyle');
    //Each with call back 
    var result = '';
    $('#fruitList li').each(function (index, value) {
        result += $(value).text() + '|'
    });
    alert(result);
    //Each with call back  With Array
    var result = [];
    $('#fruitList li').each(function (index, value) {
        result += $(value).text() + '|'
    });
    alert(result);
    console.log(result)
    //Map function with call back 
    var mapResult = $('#fruitList li').map(function (index, value) {
        return $(value).text() + '|';
    }).get();
    alert(mapResult);
    console.log(mapResult)

    Cookies.set('name', 'Cookies Test');
    Cookies.set('linti', 'One');
    debugger;
    $.ajax({
        type: "GET",
        url: "/CustomerMasters/AjaxGetList",
        //url: "http://localhost:50519/api/CustomerMasters",
        dataType: "jsonp",
        contentType: 'application/json; charset=utf-8',
        success: function (dataResponse) {
            
            
            console.log(dataResponse)
            $.each(dataResponse, function (i, val) {

                var markup = "<tr><td>" + val.CustomerCode + "</td><td>" + val.CompanyCode +
                    "</td><td>" + val.CompanyName + "</td><td>" + val.Address1
                    + "</td> <td>" + val.Address2
                    + "</td> <td>" + val.City 
                    + "</td> <td>" + val.StateCode 
                    + "</td> <td>" + val.CountryCode 
                    + "</td> <td>" + val.ZipCode 
                    + "</td> <td>" + val.Phone 
                    + "</td> <td>" + val.Fax 
                    + "</td> <td>" + val.Email 
                    + "</td> <td>" + val.InActive 
                    + "</td> <td>" + val.CreatedDate 
                    "</td> </tr>";
                $("#tblEmp tbody").append(markup);
            });

        }
    });
});
function myFunction(p1, p2) {
    return p1 * p2;
}
console.log(myFunction(2, 3));
var person = { firstName: "John", lastName: "Doe", age: 50, eyeColor: "blue" };
console.log(person.firstName);

function keyDown(event) {

    console.log(event.keyCode);
}
function onBtnClick() {
    //document.getElementById("demo").innerHTML = "Hello JavaScript!"
    console.log($("#txttrest").val());
}
//$('#btnFormSubmit').on
$(document).on("click", '#btnFormSubmit', function () {
    //debugger;
    var Customer = {
        CustomerCode: $('#txtCustomerCode').val(),
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
        InActive: $("#txtInActive").val(),
        CreatedDate: $("#txtCreatedDate").val()
    };
    $.ajax({
        type: "POST",
        url: "/CustomerMasters/JQAjaxAddCustomerDB",
        data: JSON.stringify(Customer),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (dataResponse) {
            debugger;
            // alert(data.msg);
            console.log(dataResponse);
            // toastr("Successfully Added");
            dataResponse = dataResponse.Customer;
            var markup = "<tr><td>" + dataResponse.CustomerCode + "</td><td>" + dataResponse.CompanyCode +
                "</td><td>" + dataResponse.CompanyName + "</td><td>" + dataResponse.Address1
                + "</td> <td>" + val.Address2
                + "</td> <td>" + dataResponse.City 
                + "</td> <td>" + dataResponse.StateCode 
                + "</td> <td>" + dataResponse.CountryCode 
                + "</td> <td>" + dataResponse.ZipCode 
                + "</td> <td>" + dataResponse.Phone 
                + "</td> <td>" + dataResponse.Fax 
                + "</td> <td>" + dataResponse.Email 
                + "</td> <td>" + dataResponse.InActive 
                + "</td> <td>" + dataResponse.CreatedDate 
                "</td> </tr>";
            $("#tblEmp tbody").append(markup);
        },
        error: function () {
            alert("Error occured!!")
        }
    });
})