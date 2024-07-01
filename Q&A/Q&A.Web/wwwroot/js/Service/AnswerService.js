var AnswerService = {
    GetAnswerByQuestionId: (questionId, callback) => {
        console.log("GetAnswerByQuestionId service called");
        $.get("http://localhost:5207/api/Answer/GetAnswersByQuestion/"+questionId, function (data, status) {
            callback(data);
        });
    }
}