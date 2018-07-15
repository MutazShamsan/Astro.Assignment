function toggleFavorite(element, channelId, url, isLoggedIn, requireLogin, loginUrl, onSuccess, onFail) {
    if (requireLogin && isLoggedIn.value.toLowerCase() === 'false') {
        if (confirm("You need to login first, Do it now?")) {
            if (loginUrl.value !== null)
                window.location.href = loginUrl.value;
        }
    } else {
        if ($(element).hasClass('glyphicon glyphicon-heart-empty')) {
            $(element).removeClass('glyphicon glyphicon-heart-empty');
            $(element).addClass('glyphicon glyphicon-heart');
        } else {
            $(element).removeClass('glyphicon glyphicon-heart');
            $(element).addClass('glyphicon glyphicon-heart-empty');
        }

        updateFavoriteChannelInServer(channelId, url, onSuccess, onFail);
    }
}

function updateFavoriteChannelInServer(channelId, url, onSuccess, onFail) {

    ajaxCall(url.value, { 'channelId': channelId }, 'json', 'application/x-www-form-urlencoded', 'POST', onSuccess, onFail);

    //$.ajax({
    //    url: '@Url.Action("ToggleChannelFavorit")',
    //    data: { 'channelId': channelId },
    //    dataType: "json",
    //    contentType: "application/x-www-form-urlencoded",
    //    type: 'POST',
    //    success: function (data) {
    //        console.log(data);
    //    },
    //    error: function (xhr, statusText, error) {
    //        console.log(error);
    //    }
    //});
}

function ajaxCall(url, data, dataType, contentType, type, onSuccess, onFail) {

    if (data === null) {
        $.ajax({
            url: url,
            dataType: dataType,
            type: type,
            success: function (data) {
                onSuccess(data);
            },
            error: function (xhr, statusText, error) {
                onFail(error);
            }
        });
    }
    else {

        $.ajax({
            url: url,
            data: data,
            dataType: dataType,
            contentType: contentType,
            type: type,
            success: function (data) {
                onSuccess(data);
            },
            error: function (xhr, statusText, error) { onFail(error); }
        });
    }

}