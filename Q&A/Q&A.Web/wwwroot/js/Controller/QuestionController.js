var QuestionController = {
    LstQuestions: () => {
        QuestionService.QuestionLst(function (response) {
            let questionContent = '';
            $.each(response, function (index, value) {
                questionContent += `
                    <div class="col-12 mb-3">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title text-primary" style="cursor: pointer;" onclick="window.location.href='/Question/Detail/${value.questionID}'" >${value.title}</h5>
                                <p class="card-text">${value.question}</p>
                                <div class="d-flex justify-content-between align-items-center mt-4">
                                    <small>Category: <span class="badge bg-secondary">${value.category}</span></small>
                                    <small class="text-muted">Asked by:  <span class="text-primary">${value.makeBy}</span> on ${new Date(value.makeDate).toLocaleDateString()}</small>
                                </div>
                            </div>
                        </div>
                    </div>
                `;
            });
            $('#questionContainer').html(questionContent);
        });
    },
    LoadQuestionDetail: (questionID) => {
        QuestionService.GetQuestionDetail(questionID, function (response) {
            let questionContent = `
                <h3 class="text-primary">${response.title}</h3>
                <p class="text-muted">#${response.questionID}</p>
                <p>${response.question}</p>
                <div class="d-flex justify-content-between">
                    <span class="badge bg-secondary">${response.category}</span>
                    <small class="text-primary">${response.makeBy}</small>
                    <small>${new Date(response.makeDate).toLocaleDateString()}</small>
                </div>
            `;
            $('#questionDetail').html(questionContent);
            AnswerController.LoadAnswer(response.questionID);
        });
    }
}
