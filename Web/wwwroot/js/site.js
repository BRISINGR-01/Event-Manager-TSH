$(document).ready(function () {
    $("#successModal").modal("show");
});
    
$("#closeErrorModal").on("click", () => {
    $("#successModal").modal("hide");
});