var QuestionService = {
    QuestionLst: (callback)=> {
        $.get("http://localhost:5207/api/Question/QuestionList", function (data, status) {
            callback(data);
        });
    },
    GetQuestionDetail: (questionId, callback) => {
        alter("service a asce");
        $.get("http://localhost:5207/api/QuestionDetail/" + questionId, function (data, status) {
            callback(data);
        });
    }
};
