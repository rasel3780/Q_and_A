var QuestionService = {
    QuestionLst: (callback) => {
        console.log("QuestionLst service called");
        $.get("http://localhost:5207/api/Question/QuestionList", function (data, status) {
            callback(data);
      
        });
    },
    GetQuestionDetail: (questionId, callback) => {
        console.log("QuestionDetail service called");
        $.get("http://localhost:5207/api/Question/QuestionDetail/${questionId}", function (data, status) {
            callback(data);
        });
    }
}
