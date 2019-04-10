
app.filter('unique', function () {
    return function (collection, keyname) {
        var output = [],
            keys = [];

        angular.forEach(collection, function (item) {
            var key = item[keyname];
            if (keys.indexOf(key) === -1) {
                keys.push(key);
                output.push(item);
            }
        });
        return output;
    };
});


app.filter('sum', function () {
    return function (ListRecords, FieldForSum) {
        var total = 0;
        for (i = 0; i < ListRecords.length; i++) {
            total = total + ListRecords[i][FieldForSum];
        };
        return total;
    };
});

app.filter('htmlToPlaintext', function () {
    return function (text) {
        return angular.element(text).text()
        .replace(/(?:\\[rn]|[\r\n])/g, "<br>")
    }
});

app.filter('trusted', ['$sce', function ($sce) {
    return function (url) {
        return $sce.trustAsResourceUrl(url);
    };
}]);

//app.filter('decodeHTML', [function (text) {
//     
//    if (text) {
//        return text.
//            replace(/&/g, '&amp;').
//            replace(/</g, '&lt;').
//            replace(/>/g, '&gt;').
//            replace(/'/g, '&#39;').
//            replace(/"/g, '&quot;');
//    }
//    return '';
//}]);

app.filter('noHTML', function () {
    return function (text) {
        return angular.element(text).text()
                .replace(/&/g, '&amp;')
                .replace(/>/g, '&gt;')
                .replace(/</g, '&lt;');
    }
});

app.filter('newlines', function () {
    return function (text) {
        var abc = angular.element(text).text()
        .replace(/(&#13;)?&#10;/g, '<br/>')
        .replace('\r\n', '<br/>')
        ;
        return abc;
    }
})

app.filter('HTMLtrusted', ['$sce', function ($sce) {
    var div = document.createElement('div');
    return function (text) {
        div.innerHTML = text;
        return $sce.trustAsHtml(div.textContent);
    };
}])