
window.MyLib = {
    BlockerShow: function (inText) {
        document.getElementById('BlockerUpParagraph').innerText = inText;
        document.getElementById('BlockerUP').style.visibility = "visible";
    },
    BlockerHide: function () {
        document.getElementById('BlockerUP').style.visibility = "hidden";
    }
}
