var QuestionService = {
    QuestionLst: function (callback) {
        $.get("http://localhost:5207/api/Question/QuestionList", function (data, status) {
            callback(data);
        });
    }
};
