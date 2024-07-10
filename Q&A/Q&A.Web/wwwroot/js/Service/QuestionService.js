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
    },
    PostQuestion: (question, callback) => {
        console.log("PostQuestion service called");
        console.log("Sending question:", JSON.stringify(question));
        var token = localStorage.getItem('token');
        $.ajax({
            url: 'http://localhost:5207/api/Question/PostQuestion',
            type: 'POST',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            contentType: 'application/json',
            data: JSON.stringify(question),
            success: function (response) {
                console.log("Success response:", response);
                callback(response);
            },
            error: function (xhr, status, error) {
                console.log("Error:", error);
                if (xhr.status === 401) {
                    // Unauthorized, redirect to login
                    window.location.href = '/User/Login?redirectTo=askQuestion';
                } else {
                    callback(null);
                }
            }
        });
    }
};