$(document).ready(function () {
    var winHeight = window.innerHeight;
    window.resize = onresizefn;
    onresizefn();
    function onresizefn() {
        $(".full-height").height($(document).height());
    }
});