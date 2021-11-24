$(document).ready(function () {



});


$(document).on('click', '#newOrder', function () {
    //location.href = "/Order/CreateOrder";
})



function CreateNewOrder() {
    alert("click button Create new Order");
    getUserNameByUrl();
}

function getUserNameByUrl() {
    var dataUrl = $("#urlGetSomeThing").val();
    $.ajax({
        url: dataUrl,
        datatype: 'json',
        type: 'get',
        success: function (result) {
            if (!!result) {
                alert("Result is " + result.message);
            } else {
                alert("Fail ");
            }
        }
    });
}

function RedirectUserProfile() {
    location.href = "/User/UserProfile";
}