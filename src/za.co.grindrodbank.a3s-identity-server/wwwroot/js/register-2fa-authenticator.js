window.addEventListener("load", function () {
    new QRCode(document.getElementById("qrCode"),
        {
            text: document.getElementById("qrCodeData").value,
            width: 150,
            height: 150
        });
});


// Redirect click event
var btnContinueWithout = document.getElementById("btnContinueWithout")
if (btnContinueWithout) {
    btnContinueWithout.addEventListener("click", function () {
        window.location.href = document.getElementById("RedirectUrl").value;
    });
}

// Cancel click event
var btnCancel = document.getElementById("btnCancel")
if (btnCancel) {
    btnCancel.addEventListener("click", function () {
        window.location.href = document.getElementById("Cancel2FARegistrationUrl").value;
    });
}
