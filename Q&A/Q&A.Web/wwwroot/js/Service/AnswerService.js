var AnswerService = {
    GetAnswerByQuestionId: (questionId, callback) => {
        console.log("GetAnswerByQuestionId service called");
        $.get("http://localhost:5207/api/Answer/GetAnswersByQuestion/" + questionId, function (data, status) {
            callback(data);
        });
    },

    PostAnswer: (answer, callback) => {
        console.log("PostAnswer service called");
        var token = localStorage.getItem('token');
        $.post({
            url: 'http://localhost:5207/api/Answer/PostAnswer',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            contentType: 'application/json',
            data: JSON.stringify(answer),
            success: function (response) {
                console.log("Success response:", response);
                callback(response);
            }
        });
    },

    AcceptAnswer: (answerId, callback) => {
        console.log("AcceptAnswer service called: " + answerId);
        var token = localStorage.getItem('token');
        $.ajax({
            url: 'http://localhost:5207/api/Answer/AcceptAnswer',
            type: 'POST',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            contentType: 'application/json',
            data: JSON.stringify(answerId), // Send plain integer
            success: function (response) {
                console.log("Success response:", response);
                callback(response);
            },
            error: function (xhr, status, error) {
                console.error("Error accepting answer:", error);
                callback(null, error);
            }
        });
    }
};
