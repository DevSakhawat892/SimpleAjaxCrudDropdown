//$(document).ready(function () {
//   getCustomer();
//});

//var baseUrl = "https://localhost:7107/";

//function getCustomer() {
//   $("#customer option").remove();

//   $.ajax({
//      url: baseUrl + "Customer/ReadCustomers",
//      type: "GET",
//      dataType: "json",
//      contextType: "application/josn",

//      success: function (res) {
//         console.log(res);
//         $('#customer').append($('<option>').text('Select').attr({ 'value': '' }));
//         $.each(res, function (index, v) {
//            $('#customer').append($('<option>').text(v.name).attr({ 'value': v.customerID }));
//         });
//      }
//   });
//}