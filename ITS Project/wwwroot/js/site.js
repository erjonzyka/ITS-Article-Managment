var isClicked = sessionStorage.getItem('isClicked') === 'true'; 

function submitForm() {
    document.getElementById("updateQuantityForm").submit();
}

function getValueToMultiply() {
    isClicked = true;
    sessionStorage.setItem('isClicked', 'true'); 
    $('#multiplyButton').prop('disabled', true);

    $.ajax({
        url: '/api/Scrapping/exchange-rate',
        type: 'GET',
        success: function (response) {
            var multiplier = response.exchangeRate;
            multiplyPrice(multiplier);
        },
        error: function (xhr, status, error) {
            console.error('An error occurred while fetching the value to multiply with:', error);
        }
    });
}

function multiplyPrice(multiplier) {
    $('.td-price').each(function () {
        var priceText = $(this).text().replace('EUR ', '');
        var price = parseFloat(priceText); 
        var newPrice = (price * multiplier).toFixed(2); 
        $(this).text('LEK ' + newPrice); 
    });
}

function resetSession() {
    sessionStorage.setItem('isClicked', 'false');
}

$(document).ready(function () {
    if (isClicked) {
        getValueToMultiply(); 
    }
});
