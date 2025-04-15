document.getElementById("sortSurnameForm").addEventListener("submit", function (event) {
    var ascendingInput = document.getElementById("ascendingInput");
    ascendingInput.value = !ascendingInput.value;
    var icon = document.getElementById("sortIcon");
    icon.classList.remove(ascendingInput.value ? "bi-arrow-up" : "bi-arrow-down");
    icon.classList.add(ascendingInput.value ? "bi-arrow-down" : "bi-arrow-up");
});