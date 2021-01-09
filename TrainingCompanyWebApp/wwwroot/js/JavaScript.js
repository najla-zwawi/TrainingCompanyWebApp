function clickFile() {
    var objfile = document.getElementById("fileAttach");
    objfile.click();
    return false;
}
function UploadAttach(input) {
    var file = input.files[0];
    if (/\.(jpe?g|png|bmp|tiff|gif|ico)$/i.test(file.name.toLowerCase()) === false) {
        alert("الملف المرفق ليس ملف صورة");
    }
    else {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById("imgAttach").src = e.target.result;
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
}
