$(function () {
    var totalSize = 0;
    $("#tree").fancytree({
        source: {
            url: "api/files",
            success: function (result) {
                $.each(result, function (index, data) {
                    totalSize += data.size;
                });
            }
        },
        extensions: ["table"],
        renderColumns: function (event, data) {
            var node = data.node;
            var percentOfParent = parseFloat((node.data.size / totalSize) * 100).toFixed(2);
            $tdList = $(node.tr).find(">td");
            $tdList.eq(1).text(bytesToSize(node.data.size));
            $tdList.eq(2).text(node.data.date);
            $tdList.eq(3).text(node.data.files);
            $tdList.eq(4).text(node.data.folders);
            $tdList.eq(5).append("<progress value='" + percentOfParent + "' max='100' class='percentOfParent'></progress><span class='progress-value'>" + percentOfParent + "%" + "</span>");
        },
        lazyLoad: function (event, data) {
            var fullPath = data.node.data.fullPath;
            data.result = $.ajax({
                type: "POST",
                url: "api/files",
                dataType: "json",
                data: "=" + fullPath
            });
        },
        strings: {
            loadError: "Error while loading the files"
        }
    });

    function bytesToSize(bytes) {
        if (bytes == 0) return '0 KB';
        var k = 1000;
        var sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
        var i = Math.floor(Math.log(bytes) / Math.log(k));
        return (bytes / Math.pow(k, i)).toPrecision(3) + ' ' + sizes[i];
    }
});