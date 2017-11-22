function GetLeaderboard() {
    var ret = {};
    $.ajax({
        url: 'http://localhost:5000/Score/GetLeaderboard',        
        type: 'GET',
        async: false,
        success: function(data) {
            ret = data;
        }
    });
    return ret;
}

function AddScore(name, score) {
    $.ajax({
        url: 'http://localhost:5000/Score/AddScore',        
        type: 'POST',
        data:{
            name: name,
            score: score
        },
        async: false,
        success: function(data) {
            ret = data;
        }
    });
}