var AnswerService = {
    GetAnswerByQuestionId: (questionId, callback) => {
        console.log("GetAnswerByQuestionId service called");
        $.get("http://localhost:5207/api/Answer/GetAnswersByQuestion/"+questionId, function (data, status) {
            callback(data);
        });
    },
    PostAnswer: (answer, callback) => {
        console.log("PostAnswer service called");
        $.post({
            url: 'http://localhost:5207/api/Answer/PostAnswer',
            contentType: 'application/json',
            data: JSON.stringify(answer),
            success: function (response) {
                console.log("Success response:", response);
                callback(response);
            }
            
        });
    }
}