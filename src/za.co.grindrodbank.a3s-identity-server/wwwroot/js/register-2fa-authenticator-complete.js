// Redirect click event
var btnContinueWithRedirect = document.getElementById("btnContinueWithRedirect")
if (btnContinueWithRedirect) {
    btnContinueWithRedirect.addEventListener("click", function () {
        window.location.href = document.getElementById("RedirectUrl").value;
    });
}