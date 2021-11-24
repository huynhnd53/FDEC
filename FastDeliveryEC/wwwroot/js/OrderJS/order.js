$(document).ready(function () {



});


$(document).on('click', '#btnCalculateCash', function () {
    getEstimateCash();
});

function getEstimateCash() {
    let idAddressFrom = document.getElementById("formSelectAddressFrom").value;
    let idAddressOrder = document.getElementById("formSelectAddressOrder").value;
    let idWeight = document.getElementById("formSelectWeight").value;
    var urlget = "";
    urlget = $("#urlCalculateCash").val();
    urlget = !!urlget ? `${urlget}?addressFrom=${idAddressFrom}&addressOrder=${idAddressOrder}&weightID=${idWeight}` : '';
    $.ajax({
        url: urlget,
        datatype: 'json',
        type: 'get',
        success: function (result) {
            if (!!result) {
                document.getElementById("CashValue").value = result.data;
            }
        }
    });

}



function getCity() {
    var dataUrl = $("#urlGetListCity").val();
    $.ajax({
        url: dataUrl,
        datatype: 'json',
        type: 'get',
        success: function (result) {
            if (!!result) {
                var x = result.data;
                var selectBox = document.getElementById('formSelectAddressFrom');
                if (x != null && x.lenght > 0) {
                    var op = document.createElement('option');
                    op.innerHTML = 'Select City';
                    $(op).prop('disabled', true);
                    selectBox.appendChild(op);
                    x.forEach(item => {
                        var option = document.createElement('option');
                        option.value = item.ID;
                        option.setAttribute('data-team-name', item.Name);
                        option.innerHTML = item.Name;
                        selectBox.appendChild(option);
                    });
                }

                var selectBoxs = document.getElementById('formSelectAddressOrder');
                if (x != null && x.lenght > 0) {
                    var op = document.createElement('option');
                    op.innerHTML = 'Select City';
                    $(op).prop('disabled', true);
                    selectBoxs.appendChild(op);
                    x.forEach(item => {
                        var option = document.createElement('option');
                        option.value = item.ID;
                        option.setAttribute('data-team-name', item.Name);
                        option.innerHTML = item.Name;
                        selectBoxs.appendChild(option);
                    });
                }
            } 
        }
    });
}

function getWeight() {
    var dataUrl = $("#urlGetListWeight").val();
    $.ajax({
        url: dataUrl,
        datatype: 'json',
        type: 'get',
        success: function (result) {
            if (!!result) {
                var x = result.data;
                var selectBox = document.getElementById('formSelectWeight');
                if (x != null && x.lenght > 0) {
                    var op = document.createElement('option');
                    op.innerHTML = 'Select Weight';
                    $(op).prop('disabled', true);
                    selectBox.appendChild(op);
                    x.forEach(item => {
                        var option = document.createElement('option');
                        option.value = item.ID;
                        option.setAttribute('data-team-name', item.Weight);
                        option.innerHTML = item.Weight;
                        selectBox.appendChild(option);
                    });
                }
            }
        }
    });
}


