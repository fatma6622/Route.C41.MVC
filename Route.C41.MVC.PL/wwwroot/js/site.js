debugger;
var searchInp = document.getElementById("searchInp");
searchInp.addEventListener("keyup", function () {
    let xhr = new XMLHttpRequest();
    let url = `https://localhost:44309/Employee/Index?searchInp=${searchInp.value}`;
    xhr.open("POST", url, true);
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            debugger;
            console.log(this.response);
        }
    }
    xhr.send();
})