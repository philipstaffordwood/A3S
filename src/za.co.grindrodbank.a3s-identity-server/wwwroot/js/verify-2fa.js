// Alternate 2FA registration click event
var btnAlternate2fa = document.getElementById("btnAlternate2fa")
if (btnAlternate2fa) {
    btnAlternate2fa.addEventListener("click", function () {
        window.location.href = document.getElementById("btnAlternate2faUrl").value;
    });
}