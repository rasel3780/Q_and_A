var QuestionAnswerService = {
    QuestionLst: function (callback) {
        $.get("http://localhost:5207/api/QuestionAnswer/QuestionList", function (data, status) {
            callback(data);
        });
    }
};
